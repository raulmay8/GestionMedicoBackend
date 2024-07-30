using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GestionMedicoBackend.Services.Auth;
using GestionMedicoBackend.Models.Auth;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace GestionMedicoBackend.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var user = await _authService.Authenticate(model.Email, model.Password);

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(user.Token.Value);
                var permissionsClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "permissions")?.Value;
                var permissions = permissionsClaim?.Split(',');

                return Ok(new
                {
                    Token = user.Token.Value,
                    Permissions = permissions
                });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("facebook")]
        public async Task<IActionResult> FacebookLogin([FromBody] FacebookLoginRequest model)
        {
            try
            {
                var user = await _authService.AuthenticateWithFacebook(model.AccessToken);

                return Ok(new
                {
                    Token = user.Token.Value
                });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
