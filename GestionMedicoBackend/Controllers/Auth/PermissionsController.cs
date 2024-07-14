using GestionMedicoBackend.DTOs.Permissions;
using GestionMedicoBackend.Models.Auth;
using GestionMedicoBackend.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace GestionMedicoBackend.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permission>>> GetPermissions()
        {
            var permissions = await _permissionService.GetAllPermissionsAsync();
            return Ok(permissions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Permission>> GetPermission(int id)
        {
            var permission = await _permissionService.GetPermissionByIdAsync(id);
            if (permission == null)
            {
                return NotFound();
            }
            return Ok(permission);
        }

        [HttpPost]
        public async Task<ActionResult<Permission>> CreatePermission([FromBody] CreatePermissionsDto createPermissionsDto)
        {
            try
            {
                var newPermission = await _permissionService.CreatePermissionAsync(createPermissionsDto);
                return CreatedAtAction(nameof(GetPermission), new { id = newPermission.Id }, newPermission);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Permission>> UpdatePermission(int id, [FromBody] UpdatePermissionsDto updatePermissionsDto)
        {
            try
            {
                var updatedPermission = await _permissionService.UpdatePermissionAsync(id, updatePermissionsDto);
                return Ok(updatedPermission);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            try
            {
                var result = await _permissionService.DeletePermissionAsync(id);
                if (!result)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
