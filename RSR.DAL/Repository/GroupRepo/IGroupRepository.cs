using RSR.DAL.Models.ProjectGroupModel;
using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.GroupRepo
{
    public  interface IGroupRepository
    {
        Task<List<Group>> GetSupervisorGroup(string supervisorId);
        Task<Group> CreateGroup(Group group);
        Task<Group> GroupByIdRepo(Guid GroupId);
        Task<Group> UpdateGroup(Group group);
        Task<List<SupervisorProfile>> GetAllSupervisorsWithGroups();
        Task<Group> GetGroupByStudent(string studentId);
    }
}
