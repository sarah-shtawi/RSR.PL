using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;


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
        public async Task<BaseResponse> AssignImage<TProfile>(UploadImageRequest request , string userId) where TProfile : class , IUserProfile , new()
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) 
            {
              return new BaseResponse
              {
                  Success = false,
                  Message = "user not found"
              };
            }
            var UserProfile = await _context.Set<TProfile>().FirstOrDefaultAsync(p=>p.UserId == userId);
            if (UserProfile == null) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "profile not found"
                };
            }
            if(request.MainImage == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "profile not found"
                };
            }
            var fileName = await _fileService.UploadeImageFile(request.MainImage);
            UserProfile.PictureProfileURL = fileName;
            _context.Set<TProfile>().Update(UserProfile);
            await _context.SaveChangesAsync();
            return new BaseResponse
            {
                Success = true,
                Message = "Image uploaded successfully"
            };

        }
        public async Task<BaseResponse> AssignUserWithProfile<TProfile>(AssignUserRequest request, string role) where TProfile : class, IUserProfile, new()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // get user form user table 
                var targetUser = await _userManager.FindByEmailAsync(request.Email);

                if (targetUser == null)
                {
                    // user not found ==> create 
                    targetUser = request.Adapt<ApplicationUser>();
                    var result = await _userManager.CreateAsync(targetUser, request.Password);
                    if (!result.Succeeded)
                    {
                        return new BaseResponse
                        {
                            Success = false,
                            Message = "User creation failed",
                            Errors = result.Errors.Select(e => e.Description).ToList()
                        };
                    }
                }
                // get roles for user 
                var userRoles = await _userManager.GetRolesAsync(targetUser);

                if (userRoles.Contains("Student") && role != "Student")
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Student cannot have other roles"
                    };
                }

                if (role == "Student" && userRoles.Any())
                {
                    if(role == "Student")
                    {
                        return new BaseResponse
                        {
                            Success = false,
                            Message = "Student already has a profile "
                        };
                    }
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Cannot assign Student role to a user who already has roles"
                    };
                }
                // add roles and don't duplicate 
                var rolesToAdd = new List<string>();

                if (role == "Coordinator")
                {
                    rolesToAdd.Add("Coordinator");
                    rolesToAdd.Add("Supervisor");
                    rolesToAdd.Add("Examiner");
                }
                else if (role == "Supervisor")
                {
                    rolesToAdd.Add("Supervisor");
                    rolesToAdd.Add("Examiner");
                }
                else
                {
                    rolesToAdd.Add(role);
                }
                foreach (var r in rolesToAdd)
                {
                    if (!await _userManager.IsInRoleAsync(targetUser, r))
                    {
                        var roleResult = await _userManager.AddToRoleAsync(targetUser, r);

                        if (!roleResult.Succeeded)
                        {
                            return new BaseResponse
                            {
                                Success = false,
                                Message = "Adding role failed",
                                Errors = roleResult.Errors.Select(e => e.Description).ToList()
                            };
                        }
                    }
                }
                // add to profile 
                var existingProfile = await _context.Set<TProfile>().FirstOrDefaultAsync(p => p.UserId == targetUser.Id);
                TProfile profile;
                if (existingProfile == null)
                {
                    profile = request.Adapt<TProfile>();
                    profile.UserId = targetUser.Id;
                    await _context.Set<TProfile>().AddAsync(profile);
                }
                else
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = $"User already has a {role} profile"
                    };
                }
                if (role == "Coordinator" && profile is CoordinatorProfile coordinatorProfile)
                {
                    // add to supervisor profile 
                    var SupervisorProfile = request.Adapt<SupervisorProfile>();
                    SupervisorProfile.UserId = targetUser.Id;
                    SupervisorProfile.SupervisorNumber = coordinatorProfile.CoordinatorNumber;
                    SupervisorProfile.Department = coordinatorProfile.Department;

                    // add to examiner profile 
                    var ExaminerProfile = request.Adapt<ExaminerProfile>();
                    ExaminerProfile.UserId = targetUser.Id;
                    ExaminerProfile.ExaminerNumber = coordinatorProfile.CoordinatorNumber;
                    ExaminerProfile.Department = coordinatorProfile.Department;

                    if( !await _context.Supervisors.AnyAsync( p=>p.UserId == targetUser.Id))
                    {
                        await _context.Supervisors.AddAsync(SupervisorProfile);
                    }
                    if (!await _context.Examiners.AnyAsync(p => p.UserId == targetUser.Id))
                    {
                        await _context.Examiners.AddAsync(ExaminerProfile);
                    }
                }

                if (role == "Supervisor" && profile is SupervisorProfile supervisorProfile)
                {
                    var ExaminerProfile = request.Adapt<ExaminerProfile>();
                    ExaminerProfile.UserId = targetUser.Id;
                    ExaminerProfile.ExaminerNumber = supervisorProfile.SupervisorNumber ;
                    ExaminerProfile.Department = supervisorProfile.Department ;
                    if (!await _context.Examiners.AnyAsync(p => p.UserId == targetUser.Id))
                    {
                        await _context.Examiners.AddAsync(ExaminerProfile);
                    }
                }
                // changed saved 
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new BaseResponse
                {
                    Success = true,
                    Message = $"{role} added successfully"
                };
            }
            catch (Exception ex)
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
        public async Task<List<TGetResponse>> GetAllUsersWithProfile<TProfile , TGetResponse>() where TProfile : class , IUserProfile
        {
            var usersProfile = await _context.Set<TProfile>().Include("User").ToListAsync();
            return usersProfile.Adapt<List<TGetResponse>>();
        }    
        public async Task<TGetResponse> GetUserById<TProfile , TGetResponse>(string userId) where TProfile : class , IUserProfile
        {
            var profile = await _context.Set<TProfile>().Include("User").FirstOrDefaultAsync(u => u.UserId == userId);
            if(profile == null)
            {
                return default;
            }
            return profile.Adapt<TGetResponse>();
        }

        public async Task<BaseResponse> BlockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "User Not Found"
                };
            }
            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTime.MaxValue);
            await _userManager.UpdateAsync(user);
            return new BaseResponse()
            {
                Success = true,
                Message = "User Is Blocked"
            };
        }
        public async Task<BaseResponse> unBlockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "User Not Found"
                };
            }
            await _userManager.SetLockoutEnabledAsync(user, false);
            await _userManager.SetLockoutEndDateAsync(user, null);
            await _userManager.UpdateAsync(user);
            return new BaseResponse()
            {
                Success = true,
                Message = "User Is unBlocked"
            };
        }
    }
}
