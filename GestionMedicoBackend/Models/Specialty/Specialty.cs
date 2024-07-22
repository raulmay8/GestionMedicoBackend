using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionMedicoBackend.Models
{
    public class Specialty
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Descripcion { get; set; }
        public ICollection<Appointments> Appointments { get; set; }
    }
}
