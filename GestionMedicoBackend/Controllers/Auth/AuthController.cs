using GestionMedicoBackend.Models.Auth;
using GestionMedicoBackend.Services;
using GestionMedicoBackend.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
    }
}
