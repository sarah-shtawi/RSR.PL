using RSR.DAL.DTOs.Request.UserRequest;
using RSR.DAL.DTOs.Request.UserRequest.update;
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
        // assign user
        Task<BaseResponse> AssignUserWithProfile<TProfile>(AssignUserRequest request, string Role) where TProfile : class, IUserProfile, new();

        // get user 
        Task<List<TGetResponse>> GetAllUsersWithProfile<TProfile, TGetResponse>() where TProfile : class, IUserProfile;
     
        // get user id 
        Task<TGetResponse> GetUserById<TProfile, TGetResponse>(string userId) where TProfile : class, IUserProfile;

        // assign image profile 
        Task<BaseResponse> AssignImage<TProfile>(UploadImageRequest request, string userId) where TProfile : class, IUserProfile, new();
        // update user 
        Task<BaseResponse> UpdateUserWithProfile(string userId, UpdateUserRequest request);
         // block user
         Task<BaseResponse> BlockUser(string userId);

        // unblock user
        Task<BaseResponse> unBlockUser(string userId);


    }
}
