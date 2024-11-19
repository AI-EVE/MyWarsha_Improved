using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyWarsha_Interfaces.ServicesInterfaces.IdentityServicesInterfaces;
using MyWarsha_Models.Models;

namespace MyWarsha_Services.IdentityServices
{
    public class JWTTokenGeneratorService : IJWTTokenGeneratorService
    {

        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public JWTTokenGeneratorService(IConfiguration configuration, UserManager<AppUser> UserManager)
        {
            _configuration = configuration;
            _userManager = UserManager;
        }
        public async Task<string> GenerateJwtTokenAsync(AppUser user)
            {
                var userRoles = await  _userManager.GetRolesAsync(user);

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
    }
}