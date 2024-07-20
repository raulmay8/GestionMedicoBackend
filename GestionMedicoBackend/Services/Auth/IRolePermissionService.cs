using GestionMedicoBackend.DTOs.RolePermissions;
using GestionMedicoBackend.Models.Auth;

namespace GestionMedicoBackend.Services.Auth
{
    public interface IRolePermissionService
    {
        Task<string> CreateRolePermissionAsync(CreateRolePermissionDto createRolePermissionDto);
        Task<string> DeleteRolePermissionAsync(int id);
    }
}
