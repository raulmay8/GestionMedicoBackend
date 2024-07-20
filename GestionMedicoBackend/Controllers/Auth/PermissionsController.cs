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
        public async Task<ActionResult<IEnumerable<PermissionsDto>>> GetPermissions()
        {
            var permissions = await _permissionService.GetAllPermissionsAsync();
            return Ok(permissions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PermissionsDto>> GetPermission(int id)
        {
            try
            {
                var permission = await _permissionService.GetPermissionByIdAsync(id);
                return Ok(permission);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<PermissionsDto>> CreatePermission(CreatePermissionsDto createPermissionsDto)
        {
            var permission = await _permissionService.CreatePermissionAsync(createPermissionsDto);
            return CreatedAtAction(nameof(GetPermission), new { id = permission.Id }, permission);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePermission(int id, UpdatePermissionsDto updatePermissionsDto)
        {
            try
            {
                var permission = await _permissionService.UpdatePermissionAsync(id, updatePermissionsDto);
                return Ok(permission);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
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
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}