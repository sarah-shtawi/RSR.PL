using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RSR.BLL.Service.Files;
using RSR.DAL.Data;
using RSR.DAL.DTOs.Request.UserRequest;
using RSR.DAL.DTOs.Response;
using RSR.DAL.DTOs.Response.AuthenticationResponse;
using RSR.DAL.DTOs.Response.User;
using RSR.DAL.Models;
using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;
        private readonly IConfiguration _configration;

        public UserService(UserManager<ApplicationUser> userManager , ApplicationDbContext context , IFileService fileService , IConfiguration configration)
        {
            _userManager = userManager;
            _context = context;
            _fileService = fileService;
            _configration = configration;
        }

        // Assign user 
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
                return new BaseResponse
                {
                    Success = false,
                    Message = "Unexpected error",
                    Errors = new List<string> { ex.Message },
                   
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

        // get all user [ generic ] 
        public async Task<List<TGetResponse>> GetAllUsersWithProfile<TProfile , TGetResponse>() where TProfile : class , IUserProfile
        {
            var usersProfile = await _context.Set<TProfile>().Include("User").ToListAsync();
            return usersProfile.Adapt<List<TGetResponse>>();
        }
        public async Task<List<StudentGetResponse>> GetStudents()
        {
            return await  GetAllUsersWithProfile <StudentProfile, StudentGetResponse>();
        }
        public async Task<List<CoordinatorGetResponse>> GetCoordinators()
        {
            return await GetAllUsersWithProfile<CoordinatorProfile, CoordinatorGetResponse>();
        }
        public async Task<List<SupervisorGetResponse>> GetSupervisors()
        {
            return await GetAllUsersWithProfile<SupervisorProfile, SupervisorGetResponse>();
        }
        public async Task<List<ExaminerGetResponse>> GetExaminers()
        {
            return await GetAllUsersWithProfile<ExaminerProfile, ExaminerGetResponse>();

        }

        // get user by id [ generic method  ] 
        public async Task<TGetResponse> GetUserById<TProfile , TGetResponse>(string userId) where TProfile : class , IUserProfile
        {
            var profile = await _context.Set<TProfile>().Include("User").FirstOrDefaultAsync(u => u.UserId == userId);
            if(profile == null)
            {
                return default;
            }
            return profile.Adapt<TGetResponse>();
        }
        public async Task<StudentGetResponse> GetStudentById(string userId)
        {
           var student =  await GetUserById<StudentProfile, StudentGetResponse>(userId);
           return student ;
        }
        public async Task<SupervisorGetResponse> GetSupervisorById(string userId)
        {
            var supervisor = await GetUserById<SupervisorProfile, SupervisorGetResponse>(userId);
            return supervisor;
        }
        public async Task<CoordinatorGetResponse> GetCoordinaterById(string userId)
        {
            var coordinator = await GetUserById<CoordinatorProfile, CoordinatorGetResponse>(userId);
            return coordinator;
        }
        public async Task<ExaminerGetResponse> GetExaminerById(string userId)
        {
            var Examiner  = await GetUserById<ExaminerProfile, ExaminerGetResponse>(userId);
            return Examiner;
        }



    }
}
