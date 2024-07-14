using System.ComponentModel.DataAnnotations;

namespace GestionMedicoBackend.Models.Auth
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
