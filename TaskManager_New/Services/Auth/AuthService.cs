using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskManager_New.Data;
using TaskManager_New.DTOs;
using TaskManager.Models;

namespace TaskManager_New.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public string GeneratieToken(TokenRequestDto request)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", request.UserId.ToString()),
                new Claim(ClaimTypes.Name, request.Login),
                new Claim(ClaimTypes.GivenName, request.Name)
            };

            var key = Encoding.UTF8.GetBytes("123456789_task_manager_123456789");
            var signingKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: "TaskManager",
            audience: "TaskManagerUsers",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
