using GestionMedicoBackend.Data;
using GestionMedicoBackend.DTOs.RolePermissions;
using GestionMedicoBackend.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace GestionMedicoBackend.Services.Auth
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly ApplicationDbContext _context;

        public RolePermissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RolePermission>> GetAllRolePermissionsAsync()
        {
            return await _context.RolePermissions.ToListAsync();
        }

        public async Task<RolePermission> GetRolePermissionByIdAsync(int id)
        {
            return await _context.RolePermissions.FindAsync(id);
        }

        public async Task<RolePermission> CreateRolePermissionAsync(CreateRolePermissionDto createRolePermissionDto)
        {
            var rolePermission = new RolePermission
            {
                RoleId = createRolePermissionDto.RoleId,
                PermissionId = createRolePermissionDto.PermissionId
            };

            _context.RolePermissions.Add(rolePermission);
            await _context.SaveChangesAsync();
            return rolePermission;
        }

        public async Task<RolePermission> UpdateRolePermissionAsync(int id, UpdateRolePermissionDto updateRolePermissionDto)
        {
            var rolePermission = await _context.RolePermissions.FindAsync(id);
            if (rolePermission == null)
            {
                throw new KeyNotFoundException("RolePermission not found");
            }

            rolePermission.RoleId = updateRolePermissionDto.RoleId;
            rolePermission.PermissionId = updateRolePermissionDto.PermissionId;

            _context.RolePermissions.Update(rolePermission);
            await _context.SaveChangesAsync();
            return rolePermission;
        }

        public async Task<bool> DeleteRolePermissionAsync(int id)
        {
            var rolePermission = await _context.RolePermissions.FindAsync(id);
            if (rolePermission == null)
            {
                throw new KeyNotFoundException("RolePermission not found");
            }

            _context.RolePermissions.Remove(rolePermission);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
