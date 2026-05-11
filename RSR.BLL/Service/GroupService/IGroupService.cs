using RSR.DAL.DTOs.Request.GroupRequest;
using RSR.DAL.DTOs.Response;
using RSR.DAL.DTOs.Response.GroupRes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.GroupService
{
    public  interface IGroupService
    {
        Task<BaseResponse> CreateGroup(GroupRequest request, string SupervisorId);
        Task<BaseResponse> UpdateGroup(GroupRequest request, string SupervisorId, Guid groupId);
        Task<List<GroupResponse>> GetSupervisorGroups(string supervisorId);
        Task<List<GetAllSupervisorsWithGroups>> GetCoordinatersGroups();
        Task<GroupResponse> GetGroupById(Guid groupId , string userId , string role);
    }
}
