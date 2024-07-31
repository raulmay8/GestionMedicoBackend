using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GestionMedicoBackend.Data;
using GestionMedicoBackend.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
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
            var user = await _context.Users
                .Include(u => u.Role)
                .ThenInclude(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
                throw new ApplicationException("Email o contraseña incorrecta");

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                throw new ApplicationException("Email o contraseña incorrecta");

            if (!user.Status)
                throw new ApplicationException("Cuenta no confirmada");

            var userPermissions = user.Role.RolePermissions.Select(rp => rp.Permission.Name).ToList();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("permissions", string.Join(",", userPermissions)),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenValue = tokenHandler.WriteToken(token);

            user.Token = new Token { Value = tokenValue };
            return user;
        }


        public async Task<UserModel> AuthenticateWithFacebook(string accessToken)
        {
            var appAccessTokenResponse = await new HttpClient().GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["Facebook:AppId"]}&client_secret={_configuration["Facebook:AppSecret"]}&grant_type=client_credentials");
            var appAccessToken = JObject.Parse(appAccessTokenResponse)["access_token"].ToString();

            var userAccessTokenValidationResponse = await new HttpClient().GetStringAsync($"https://graph.facebook.com/debug_token?input_token={accessToken}&access_token={appAccessToken}");
            var isValid = JObject.Parse(userAccessTokenValidationResponse)["data"]["is_valid"].Value<bool>();

            if (!isValid)
                throw new ApplicationException("Invalid Facebook token.");

            var userInfoResponse = await new HttpClient().GetStringAsync($"https://graph.facebook.com/me?fields=id,name,email&access_token={accessToken}");
            var userInfo = JObject.Parse(userInfoResponse);

            var email = userInfo["email"].ToString();
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                // Crear nuevo usuario si no existe
                user = new UserModel
                {
                    Email = email,
                    Username = userInfo["name"].ToString(),
                    Status = true,
                    Password = null, 
                    RoleId = null 
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

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

            user.Token = new Token { Value = tokenValue };
            return user;
        }

    }
}
