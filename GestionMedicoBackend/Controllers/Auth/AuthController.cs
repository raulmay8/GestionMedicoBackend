using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GestionMedicoBackend.Services.Auth;
using GestionMedicoBackend.Models.Auth;
using System;

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
