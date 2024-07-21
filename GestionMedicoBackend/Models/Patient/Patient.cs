using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GestionMedicoBackend.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Occupation { get; set; }

        [Required]
        [MaxLength(50)]
        public string Picture { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Appointments> Appointments { get; set; }
    }
}
