using GestionMedicoBackend.DTOs.RolePermissions;
using GestionMedicoBackend.Models.Auth;

namespace GestionMedicoBackend.Services.Auth
{
    public interface IRolePermissionService
    {
        Task<RolePermission> CreateRolePermissionAsync(CreateRolePermissionDto createRolePermissionDto);
        Task<IEnumerable<RolePermission>> GetAllRolePermissionsAsync();
        Task<RolePermission> GetRolePermissionByIdAsync(int id);
        Task<RolePermission> UpdateRolePermissionAsync(int id, UpdateRolePermissionDto updateRolePermissionDto);
        Task<bool> DeleteRolePermissionAsync(int id);
    }
}
