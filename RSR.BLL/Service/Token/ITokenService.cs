using RSR.DAL.Models.User;
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
    }
}
