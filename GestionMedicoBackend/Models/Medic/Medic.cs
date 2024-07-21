using GestionMedicoBackend.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionMedicoBackend.Models
{
    public class Medic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProfessionalId { get; set; }

        [Required]
        [MaxLength(50)]
        public string School { get; set; }

        [Required]
        public int YearExperience { get; set; }

        [Required]
        public DateOnly DateGraduate { get; set; }

        public bool Availability { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Appointments> Appointments { get; set; }

    }
}
