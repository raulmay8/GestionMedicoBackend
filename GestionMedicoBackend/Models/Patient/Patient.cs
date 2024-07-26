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
        public string Phone {  get; set; }
        public string BloodGroup { get; set; }
        public string MaritalStatus { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Appointments> Appointments { get; set; }
        public HistorialClinico HistorialClinico { get; set; }
    }
}
