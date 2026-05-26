using RSR.DAL.Models.User;
using System.Security.Claims;

namespace RSR.BLL.Service.Token
{
    public interface ITokenService
    {
        Task<string> GeneraterAccessToken(
            ApplicationUser user,
            string loginAs);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(
            string token);
    }
}