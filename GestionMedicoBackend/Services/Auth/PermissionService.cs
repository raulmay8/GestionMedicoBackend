using GestionMedicoBackend.Data;
using GestionMedicoBackend.DTOs.Permissions;
using GestionMedicoBackend.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace GestionMedicoBackend.Services.Auth
{
    public class PermissionService : IPermissionService
    {
        private readonly ApplicationDbContext _context;

        public PermissionService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission> GetPermissionByIdAsync(int id)
        {
            return await _context.Permissions.FindAsync(id);
        }

        public async Task<Permission> CreatePermissionAsync(CreatePermissionsDto createPermissionsDto)
        {
            var permission = new Permission
            {
                Name = createPermissionsDto.Name
            };

            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task<Permission> UpdatePermissionAsync(int id, UpdatePermissionsDto updatePermissionsDto)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null)
            {
                throw new KeyNotFoundException("Permission not found");
            }

            permission.Name = updatePermissionsDto.Name;

            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task<bool> DeletePermissionAsync(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null)
            {
                throw new KeyNotFoundException("Permission not found");
            }

            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
