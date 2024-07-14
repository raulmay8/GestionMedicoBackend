using GestionMedicoBackend.DTOs.Permissions;
using GestionMedicoBackend.Models.Auth;

namespace GestionMedicoBackend.Services.Auth
{
    public interface IPermissionService
    {
        Task<Permission> CreatePermissionAsync(CreatePermissionsDto createPermissionsDto);
        Task<IEnumerable<Permission>> GetAllPermissionsAsync();
        Task<Permission> GetPermissionByIdAsync(int id);
        Task<Permission> UpdatePermissionAsync(int id, UpdatePermissionsDto updatePermissionsDto);
        Task<bool> DeletePermissionAsync(int id);
    }
}
