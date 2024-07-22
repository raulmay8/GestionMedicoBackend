using GestionMedicoBackend.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace GestionMedicoBackend.Models
{
    public class Appointments
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Reason { get; set; }

        [Required]
        public int? PatientId { get; set; }
        public Patient Patient { get; set; }

        [Required]
        public int MedicId { get; set; }
        public Medic Medic { get; set; }

        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string Apellido { get; set; }

        [MaxLength(20)]
        public string Genero { get; set; }

        [MaxLength(255)]
        public string Correo { get; set; }

        [MaxLength(15)]
        public string NumeroTelefono { get; set; }

        [MaxLength(100)]
        public string Estado { get; set; }

        [MaxLength(10)]
        public string CodigoPostal { get; set; }

        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        [Required]
        public DateTime FechaCita { get; set; }
    }
}