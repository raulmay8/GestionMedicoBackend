using GestionMedicoBackend.DTOs.Role;
using GestionMedicoBackend.Models.Auth;

namespace GestionMedicoBackend.Services.Auth
{
    public interface IRoleService
    {
        Task<Role> CreateRoleAsync(CreateRoleDto createRoleDto);
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task<RoleDto> GetRoleByIdAsync(int id);
        Task<Role> UpdateRoleAsync(int id, UpdateRoleDto updateRoleDto);
        Task<bool> DeleteRoleAsync(int id);
    }
}
