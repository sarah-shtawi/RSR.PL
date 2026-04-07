using RSR.DAL.DTOs.Request.GroupRequest;
using RSR.DAL.DTOs.Response;
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
    }
}
