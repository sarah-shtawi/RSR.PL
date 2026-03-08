using RSR.BLL.Service.Authentication;
using RSR.BLL.Service.Token;
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


        }
    }
}
