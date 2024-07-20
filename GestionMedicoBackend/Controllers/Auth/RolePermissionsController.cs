using GestionMedicoBackend.DTOs.RolePermissions;
using GestionMedicoBackend.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        [HttpPost]
        public async Task<ActionResult> CreateRolePermission([FromBody] CreateRolePermissionDto createRolePermissionDto)
        {
            try
            {
                var message = await _rolePermissionService.CreateRolePermissionAsync(createRolePermissionDto);
                return Ok(new { message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
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
                var message = await _rolePermissionService.DeleteRolePermissionAsync(id);
                return Ok(new { message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
