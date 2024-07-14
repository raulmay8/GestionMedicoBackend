﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GestionMedicoBackend.Data;
using GestionMedicoBackend.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using UserModel = GestionMedicoBackend.Models.User;

namespace GestionMedicoBackend.Services.Auth
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

        public async Task<UserModel> Authenticate(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenValue = tokenHandler.WriteToken(token);

            // Save token to the database
            var userToken = new Token
            {
                Value = tokenValue,
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id
            };
            _context.Tokens.Add(userToken);
            await _context.SaveChangesAsync();

            user.Token = userToken;

            return user;
        }
    }
}
