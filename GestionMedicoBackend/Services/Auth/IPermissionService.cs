using GestionMedicoBackend.DTOs.Permissions;
using GestionMedicoBackend.Models.Auth;

namespace GestionMedicoBackend.Services.Auth
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionsDto>> GetAllPermissionsAsync();
        Task<PermissionsDto> GetPermissionByIdAsync(int id);
        Task<PermissionsDto> CreatePermissionAsync(CreatePermissionsDto createPermissionsDto);
        Task<PermissionsDto> UpdatePermissionAsync(int id, UpdatePermissionsDto updatePermissionsDto);
        Task<bool> DeletePermissionAsync(int id);
    }
}
