using GestionMedicoBackend.Data;
using GestionMedicoBackend.DTOs.Role;
using GestionMedicoBackend.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace GestionMedicoBackend.Services.Auth
{
    public class RolService : IRoleService
    {
        private readonly ApplicationDbContext _context;

        public RolService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            return await _context.Roles
                .Include(r => r.Users)
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .Select(r => new RoleDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    UserNames = r.Users.Select(u => u.Username).ToList(),
                    Permissions = r.RolePermissions.Select(rp => rp.Permission.Name).ToList()
                })
                .ToListAsync();
        }

        public async Task<RoleDto> GetRoleByIdAsync(int id)
        {
            var role = await _context.Roles
                .Include(r => r.Users)
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (role == null)
            {
                throw new KeyNotFoundException("Role no encontrado");
            }

            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                UserNames = role.Users.Select(u => u.Username).ToList(),
                Permissions = role.RolePermissions.Select(rp => rp.Permission.Name).ToList()
            };
        }

        public async Task<Role> CreateRoleAsync(CreateRoleDto createRoleDto)
        {
            var role = new Role
            {
                Name = createRoleDto.Name
            };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> UpdateRoleAsync(int id, UpdateRoleDto updateRoleDto)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                throw new KeyNotFoundException("Rol no encontrado");
            }

            role.Name = updateRoleDto.Name;

            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                throw new KeyNotFoundException("Rol no encontrado");
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
