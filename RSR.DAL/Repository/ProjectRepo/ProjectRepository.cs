using RSR.DAL.Data;
using RSR.DAL.Models.ProjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.ProjectRepo
{
    public  class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Project> CreateProject(Project project)
        {
            await _context.AddAsync(project);
            await _context.SaveChangesAsync();
            return project;
        }
        public async Task<Project> FindById(Guid ProjectId)
        {
            var project = await _context.Projects.FindAsync(ProjectId);
            if (project == null)
            {
                return null;
            }
            return project;
        }
        public async Task<Project> UpdateProject(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return project;
        }
    }
}
