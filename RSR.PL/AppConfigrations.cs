using Microsoft.AspNetCore.Identity;
using RSR.BLL.Service.Authentication;
using RSR.BLL.Service.EmailSender;
using RSR.BLL.Service.Files;
using RSR.BLL.Service.Token;
using RSR.BLL.Service.Users;
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






        }
    }
}
