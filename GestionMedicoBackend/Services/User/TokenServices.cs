using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GestionMedicoBackend.Models;
using GestionMedicoBackend.Data;

namespace GestionMedicoBackend.Services.User
{
    public class TokenServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public TokenServices(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> GenerateTokenAsync(Models.User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["DurationInMinutes"])), 
                signingCredentials: creds);

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            var userToken = new Token
            {
                Value = tokenValue,
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id
            };

            _context.Tokens.Add(userToken);
            await _context.SaveChangesAsync();

            return tokenValue;
        }

    }
}
