using HRM.Applicatin.IRepository.Authentication;
using HRM.Domain;
using HRM.Domain.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRM.Infrastructure.Repository.Authentication
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _config;

        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateAccessToken(Users employee)
        {
            var claims = new List<Claim>
            {
                //new Claim(ClaimTypes.NameIdentifier, employee.ID.ToString()),
                //new Claim(ClaimTypes.Name, employee.FullName),
                //new Claim(ClaimTypes.Email, employee.Email),
                //new Claim("companyId", employee.CompanyID.ToString()),
                //new Claim(ClaimTypes.Role, employee.IsAdmin ? "Admin" : "User")

                // Standard JWT claims
                new Claim(ClaimTypes.NameIdentifier, employee.ID.ToString()), 
                new Claim(JwtRegisteredClaimNames.Email, employee.Email),

                // Custom application-specific claims
                new Claim("FullName", employee.FullName),
                new Claim("companyId", employee.CompanyID.ToString()),
                new Claim("role", employee.IsAdmin ? "Admin" : "User"),
                new Claim("isActive", employee.IsActive.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:AccessTokenExpiryMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(int userId, string jwtId)
        {
            return new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(_config["Jwt:RefreshTokenExpiryDays"])),
                CreatedAt = DateTime.UtcNow,
                JwtId = jwtId,
                UserId = userId,
                IsRevoked = false,
                IsUsed = false
            };
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false, // Important: allow expired token
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                if (validatedToken is not JwtSecurityToken jwtToken ||
                    !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
