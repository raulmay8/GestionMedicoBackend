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
        public async Task<IEnumerable<PermissionsDto>> GetAllPermissionsAsync()
        {
            return await _context.Permissions
                .Select(p => new PermissionsDto
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToListAsync();
        }

        public async Task<PermissionsDto> GetPermissionByIdAsync(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null)
            {
                throw new KeyNotFoundException("Permiso no encontrado");
            }

            return new PermissionsDto
            {
                Id = permission.Id,
                Name = permission.Name
            };
        }

        public async Task<PermissionsDto> CreatePermissionAsync(CreatePermissionsDto createPermissionsDto)
        {
            var permission = new Permission
            {
                Name = createPermissionsDto.Name
            };

            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();

            return new PermissionsDto
            {
                Id = permission.Id,
                Name = permission.Name
            };
        }

        public async Task<PermissionsDto> UpdatePermissionAsync(int id, UpdatePermissionsDto updatePermissionsDto)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null)
            {
                throw new KeyNotFoundException("Permiso no encontrado");
            }

            permission.Name = updatePermissionsDto.Name;

            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();

            return new PermissionsDto
            {
                Id = permission.Id,
                Name = permission.Name
            };
        }

        public async Task<bool> DeletePermissionAsync(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null)
            {
                throw new KeyNotFoundException("Permiso no encontrado");
            }

            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}