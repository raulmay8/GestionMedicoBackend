using GestionMedicoBackend.DTOs.User;

namespace GestionMedicoBackend.DTOs.Role
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> UserNames { get; set; }
        public List<string> Permissions { get; set; }
    }

    public class CreateRoleDto
    {
        public string Name { get; set; }
    }

    public class UpdateRoleDto
    {
        public string Name { get; set; }
    }
}
