namespace GestionMedicoBackend.DTOs.Permissions
{
    public class PermissionsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CreatePermissionsDto
    {
        public string Name { get; set; }
    }

    public class UpdatePermissionsDto
    {
        public string Name { get; set; }
    }
}
