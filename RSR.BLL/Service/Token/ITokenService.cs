using RSR.DAL.Models.User;
<<<<<<< HEAD
using System.Security.Claims;

namespace RSR.BLL.Service.Token
{
    public interface ITokenService
    {
        Task<string> GeneraterAccessToken(
            ApplicationUser user,
            string role);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(
            string token);
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.Token
{
    public  interface ITokenService
    {
        Task<string> GeneraterAccessToken(ApplicationUser user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
>>>>>>> origin/master
    }
}
