using Mapster;
using Microsoft.AspNetCore.Identity;
using RSR.BLL.Service.File;
using RSR.DAL.Data;
using RSR.DAL.DTOs.Request.UserRequest;
using RSR.DAL.DTOs.Response;
using RSR.DAL.DTOs.Response.AuthenticationResponse;
using RSR.DAL.Models;
using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public UserService(UserManager<ApplicationUser> userManager , ApplicationDbContext context , IFileService fileService)
        {
            _userManager = userManager;
            _context = context;
            _fileService = fileService;
        }
        
        public async Task<BaseResponse> AssignUserWithProfile<TProfile>(AssignUserRequest request, string Role)  where TProfile :class , IUserProfile , new() 
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // add user to user table 
                var user = request.Adapt<ApplicationUser>();
                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    return new BaseResponse()
                    {
                        Success = false,
                        Message = "User Creation falid"
                    };
                }
                var roleResult = await _userManager.AddToRoleAsync(user , Role);
                if (!roleResult.Succeeded)
                {
                    return new BaseResponse()
                    {
                        Success = false,
                        Message = "Adding Role falid"
                    };
                }
                // add to profile 
                var profile = request.Adapt<TProfile>();
                profile.UserId = user.Id;
                if (request.MainImage != null)
                {
                    var fileName = await _fileService.UploadeImageFile(request.MainImage);
                    profile.PictureProfileURL = fileName;
                }
                await _context.Set<TProfile>().AddAsync(profile);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new BaseResponse()
                {
                    Success = true,
                    Message = $"{Role} added successfully"
                };
            }catch (Exception ex)
             {
                await transaction.RollbackAsync();
                return new BaseResponse
                {
                    Success = false,
                    Message = "Unexpected error",
                    Errors = new List<string> { ex.Message }
                };
             }
        }

        public async Task<BaseResponse> AssignStudent(AssignStudentRequest request)
        {
            return await AssignUserWithProfile<StudentProfile>(request , "Student");
        }

        public async Task<BaseResponse> AssignSupervisor(AssignSupervisorRequest request)
        {
            return await AssignUserWithProfile<SupervisorProfile>(request, "Supervisor");
        }

        public async Task<BaseResponse> AssignCoordinator(AssignCoordinaterRequest request)
        {
            return await AssignUserWithProfile<CoordinatorProfile>(request, "Coordinator");
        }

        public async Task<BaseResponse> AssignExaminer(AssignExaminerRequest request)
        {
            return await AssignUserWithProfile<ExaminerProfile>(request, "Examiner");
        }
    }
}
