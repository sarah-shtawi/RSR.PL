using Azure.Core;
using Microsoft.EntityFrameworkCore;
using RSR.DAL.Data;
using RSR.DAL.Models.ProjectGroupModel;
using RSR.DAL.Models.SemesterModel;
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
        public async Task<Group> CreateGroup(Group group)
        {
            await _context.AddAsync(group);
            await _context.SaveChangesAsync();
            return group;
        }
        public async Task<Group> FindById(Guid GroupId)
        {
            var group = _context.Groups.Include(g=>g.Project).FirstOrDefault(g=>g.GroupId == GroupId);
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
    }
}
