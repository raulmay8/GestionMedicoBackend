using GestionMedicoBackend.DTOs.RolePermissions;
using GestionMedicoBackend.Models.Auth;
using GestionMedicoBackend.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace GestionMedicoBackend.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolePermissionsController : ControllerBase
    {
        private readonly IRolePermissionService _rolePermissionService;

        public RolePermissionsController(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolePermission>>> GetRolePermissions()
        {
            var rolePermissions = await _rolePermissionService.GetAllRolePermissionsAsync();
            return Ok(rolePermissions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RolePermission>> GetRolePermission(int id)
        {
            var rolePermission = await _rolePermissionService.GetRolePermissionByIdAsync(id);
            if (rolePermission == null)
            {
                return NotFound();
            }
            return Ok(rolePermission);
        }


        [HttpPost]
        public async Task<ActionResult<RolePermission>> CreateRolePermission([FromBody] CreateRolePermissionDto createRolePermissionDto)
        {
            try
            {
                var newRolePermission = await _rolePermissionService.CreateRolePermissionAsync(createRolePermissionDto);
                return CreatedAtAction(nameof(GetRolePermission), new { id = newRolePermission.Id }, newRolePermission);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RolePermission>> UpdateRolePermission(int id, [FromBody] UpdateRolePermissionDto updateRolePermissionDto)
        {
            try
            {
                var updatedRolePermission = await _rolePermissionService.UpdateRolePermissionAsync(id, updateRolePermissionDto);
                return Ok(updatedRolePermission);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRolePermission(int id)
        {
            try
            {
                var result = await _rolePermissionService.DeleteRolePermissionAsync(id);
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
