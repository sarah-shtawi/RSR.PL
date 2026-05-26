using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RSR.DAL.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RSR.BLL.Service.Token
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IConfiguration _configuration;

        public TokenService(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;

            _configuration = configuration;
        }

        // =========================
        // GENERATE ACCESS TOKEN
        // =========================
        public async Task<string> GeneraterAccessToken(
            ApplicationUser user,
            string loginAs)
        {
            // GET USER ROLES
            var roles =
                await _userManager.GetRolesAsync(user);

            // CHECK ROLE
            if (!roles.Contains(loginAs))
            {
                throw new Exception("Invalid role");
            }

            // CLAIMS
            var userClaims = new List<Claim>()
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id),

                new Claim(
                    ClaimTypes.Name,
                    user.UserName),

                new Claim(
                    ClaimTypes.Email,
                    user.Email),

                // ONLY SELECTED ROLE
                new Claim(
                    ClaimTypes.Role,
                    loginAs)
            };

            // SECURITY KEY
            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _configuration["Jwt:SecurityKey"]));

            // CREDENTIALS
            var creds =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            // TOKEN
            var token =
                new JwtSecurityToken(
                    issuer:
                        _configuration["Jwt:Issuer"],

                    audience:
                        _configuration["Jwt:Audience"],

                    claims:
                        userClaims,

                    expires:
                        DateTime.UtcNow.AddMinutes(30),

                    signingCredentials:
                        creds);

            // RETURN TOKEN
            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

        // =========================
        // GENERATE REFRESH TOKEN
        // =========================
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var rng =
                RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);

                return Convert
                    .ToBase64String(randomNumber);
            }
        }

        // =========================
        // GET PRINCIPAL FROM TOKEN
        // =========================
        public ClaimsPrincipal GetPrincipalFromExpiredToken(
            string token)
        {
            var tokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateAudience = false,

                    ValidateIssuer = false,

                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                _configuration["Jwt:SecurityKey"])),

                    ValidateLifetime = false
                };

            var tokenHandler =
                new JwtSecurityTokenHandler();

            SecurityToken securityToken;

            var principal =
                tokenHandler.ValidateToken(
                    token,
                    tokenValidationParameters,
                    out securityToken);

            var jwtSecurityToken =
                securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException(
                    "Invalid token");
            }

            return principal;
        }
    }
}