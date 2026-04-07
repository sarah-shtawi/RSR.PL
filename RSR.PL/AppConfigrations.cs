using Microsoft.AspNetCore.Identity;
using RSR.BLL.Service.Authentication;
using RSR.BLL.Service.EmailSender;
using RSR.BLL.Service.Files;
using RSR.BLL.Service.GroupService;
using RSR.BLL.Service.Semester;
using RSR.BLL.Service.semesterService;
using RSR.BLL.Service.Token;
using RSR.BLL.Service.Users;
using RSR.DAL.Repository.GroupRepo;
using RSR.DAL.Repository.ProjectRepo;
using RSR.DAL.Repository.SemesterRepo;
using RSR.DAL.Repository.StudentRepo;
using RSR.DAL.Utils;

namespace RSR.PL
{
    public static class AppConfigrations
    {
        public static void Config(IServiceCollection Services)
        {
            Services.AddScoped<ISeedData, RoleSeedData>();
            Services.AddScoped<ISeedData, UserSeedData>();

            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<ITokenService, TokenService>();
            Services.AddScoped<IEmailSenderService, EmailSenderService>();
            Services.AddScoped<IUserService , UserService>();
            Services.AddScoped<IFileService, FileService>();
            Services.AddScoped<ISemesterRepository, SemesterRepository>();
            Services.AddScoped<ISemesterService, SemesterService>();

            Services.AddScoped<IGroupService, GroupService>();
            Services.AddScoped<IGroupRepository, GroupRepository>();
            Services.AddScoped<IProjectRepository, ProjectRepository>();
            Services.AddScoped<IStudentRepository, StudentRepository>();
            Services.AddHostedService<SemesterBackgroundService>();










        }
    }
}
