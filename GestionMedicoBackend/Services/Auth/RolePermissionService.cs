using GestionMedicoBackend.Data;
using GestionMedicoBackend.DTOs.RolePermissions;
using GestionMedicoBackend.Models.Auth;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestionMedicoBackend.Services.Auth
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly ApplicationDbContext _context;

        public RolePermissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateRolePermissionAsync(CreateRolePermissionDto createRolePermissionDto)
        {
            var role = await _context.Roles.FindAsync(createRolePermissionDto.RoleId);
            if (role == null)
            {
                throw new KeyNotFoundException("El rol especificado no existe.");
            }

            var permission = await _context.Permissions.FindAsync(createRolePermissionDto.PermissionId);
            if (permission == null)
            {
                throw new KeyNotFoundException("El permiso especificado no existe.");
            }

            var rolePermission = new RolePermission
            {
                RoleId = createRolePermissionDto.RoleId,
                PermissionId = createRolePermissionDto.PermissionId
            };

            _context.RolePermissions.Add(rolePermission);
            await _context.SaveChangesAsync();
            return "Permiso asignado correctamente al rol.";
        }

        public async Task<string> DeleteRolePermissionAsync(int id)
        {
            var rolePermission = await _context.RolePermissions.FindAsync(id);
            if (rolePermission == null)
            {
                throw new KeyNotFoundException("Asociación no encontrada.");
            }

            _context.RolePermissions.Remove(rolePermission);
            await _context.SaveChangesAsync();
            return "Permiso eliminado del rol correctamente.";
        }
    }
}
