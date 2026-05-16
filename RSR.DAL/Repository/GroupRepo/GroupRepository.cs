using Azure.Core;
using Microsoft.EntityFrameworkCore;
using RSR.DAL.Data;
using RSR.DAL.Models.ProjectGroupModel;
using RSR.DAL.Models.SemesterModel;
using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.GroupRepo
{
    public  class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task <List<Group>> GetSupervisorGroup(string supervisorId)
        {
            var groups = await _context.Groups.Where(g=>g.SupervisorId == supervisorId)
                .Include(g=>g.Supervisor).ThenInclude(s=>s.User)
                .Include(g=>g.Project)
                .Include(g=>g.Students).ThenInclude(s=>s.User).ToListAsync();
            return groups;
        }
        public async Task<List<SupervisorProfile>> GetAllSupervisorsWithGroups()
        {
            var groups = await _context.Supervisors
                .Include(s => s.User)
                .Include(s=>s.Groups).ThenInclude(g=>g.Students).ThenInclude(st=>st.User)
                .Include(s=>s.Groups).ThenInclude(g=>g.Project)
                .ToListAsync();
            return groups;
        }
        public async Task<Group> CreateGroup(Group group)
        {
            await _context.AddAsync(group);
            await _context.SaveChangesAsync();
            return group;
        }
        public async Task<Group> GroupByIdRepo(Guid GroupId)
        {
            var group = await  _context.Groups.Include(g=>g.Project)
                .Include(g=>g.Students).ThenInclude(s=>s.User)
                .Include(g=>g.Thesis)
                .FirstOrDefaultAsync(g=>g.GroupId == GroupId);
            if (group == null)
            {
                return null;
            }
            return group;
        }
        public async Task<Group> UpdateGroup(Group group)
        {
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<Group> GetGroupByStudent(string studentId)
        {
            var student = await _context.Students
                .Include(s=>s.Group).ThenInclude(g=>g.Project)
                .Include(s=>s.Group).ThenInclude(g=>g.Students).ThenInclude(s=>s.User)
                .Where(s => s.User.Id == studentId)
                .FirstOrDefaultAsync();

            return student.Group;
        }


    }
}
