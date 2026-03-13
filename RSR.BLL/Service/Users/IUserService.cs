using RSR.DAL.DTOs.Request.UserRequest;
using RSR.DAL.DTOs.Response;
using RSR.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.Users
{
    public interface IUserService
    {
        Task<BaseResponse> AssignUserWithProfile<TProfile>(AssignUserRequest request, string Role) where TProfile : class, IUserProfile, new();
        Task<BaseResponse> AssignStudent(AssignStudentRequest request);
        Task<BaseResponse> AssignSupervisor(AssignSupervisorRequest request);
        Task<BaseResponse> AssignCoordinator(AssignCoordinaterRequest request);
        Task<BaseResponse> AssignExaminer(AssignExaminerRequest request);

    }
}
