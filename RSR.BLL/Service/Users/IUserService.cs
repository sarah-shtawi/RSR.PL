using RSR.DAL.DTOs.Request.UserRequest;
using RSR.DAL.DTOs.Response;
using RSR.DAL.DTOs.Response.User;
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
        Task<List<StudentGetResponse>> GetStudents();
        Task<List<CoordinatorGetResponse>> GetCoordinators();
        Task<List<SupervisorGetResponse>> GetSupervisors();
        Task<List<ExaminerGetResponse>> GetExaminers();
        Task<TGetResponse> GetUserById<TProfile, TGetResponse>(string userId) where TProfile : class, IUserProfile;
        Task<StudentGetResponse> GetStudentById(string userId);
        Task<SupervisorGetResponse> GetSupervisorById(string userId);
        Task<CoordinatorGetResponse> GetCoordinaterById(string userId);
        Task<ExaminerGetResponse> GetExaminerById(string userId);

    }
}
