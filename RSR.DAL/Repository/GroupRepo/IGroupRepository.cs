using RSR.DAL.Models.ProjectGroupModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.GroupRepo
{
    public  interface IGroupRepository
    {
        Task<Group> CreateGroup(Group group);
        Task<Group> FindById(Guid GroupId);
        Task<Group> UpdateGroup(Group group);
    }
}
