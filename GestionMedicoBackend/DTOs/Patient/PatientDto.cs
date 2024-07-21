using System.ComponentModel.DataAnnotations;

namespace GestionMedicoBackend.DTOs.Patient
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Occupation { get; set; }
        public string Picture { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class CreatePatientDto
    {
        [Required(ErrorMessage = "El nombre de la ocupación es requerido.")]
        [MaxLength(50, ErrorMessage = "El nombre de la ocupación no puede exceder los 50 caracteres.")]
        public string Occupation { get; set; }
        public string Picture { get; set; }
        [Required]
        public int UserId { get; set; }
    }

    public class UpdatePatientDto
    {
        public string Occupation { get; set; }
        public string Picture { get; set; }
        public int UserId { get; set; }
    }
}