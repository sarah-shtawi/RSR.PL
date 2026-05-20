using RSR.DAL.Models.ProjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.ProjectRepo
{
    public  interface IProjectRepository
    {
        Task<Project> CreateProject(Project project);
        Task<Project> UpdateProject(Project project);
        Task<Project> FindById(Guid ProjectId);
    }
}
