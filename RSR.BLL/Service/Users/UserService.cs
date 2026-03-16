using Mapster;
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
       
        public async Task<BaseResponse> AssignUserWithProfile<TProfile>(AssignUserRequest request, string Role)
            where TProfile : class, IUserProfile, new()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName)
                           ?? await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    user = request.Adapt<ApplicationUser>();
                    var result = await _userManager.CreateAsync(user, request.Password);
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

                if (!await _userManager.IsInRoleAsync(user, Role))
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, Role);
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

                var existingProfile = await _context.Set<TProfile>()
                                                    .FirstOrDefaultAsync(p => p.UserId == user.Id);
                if (existingProfile == null)
                {
                    var profile = request.Adapt<TProfile>();
                    profile.UserId = user.Id;
                    await _context.Set<TProfile>().AddAsync(profile);
                }
                else
                {
                    _context.Set<TProfile>().Update(existingProfile);
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new BaseResponse
                {
                    Success = true,
                    Message = $"{Role} added successfully"
                };
            }
            catch (DbUpdateException dbEx) when
                  (dbEx.InnerException?.Message.Contains("PRIMARY KEY") == true
                   || dbEx.InnerException?.Message.Contains("UNIQUE") == true)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "User or Profile already exists."
                };
            }
            catch (Exception ex)
            {
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
     
    }
}
