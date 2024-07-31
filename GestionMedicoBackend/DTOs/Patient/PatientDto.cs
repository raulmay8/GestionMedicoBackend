using System.ComponentModel.DataAnnotations;

namespace GestionMedicoBackend.DTOs.Patient
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Occupation { get; set; }
        public string Picture { get; set; }
        public string Phone { get; set; }
        public string BloodGroup { get; set; }
        public string MaritalStatus { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public class CreatePatientDto
    {
        [Required(ErrorMessage = "El nombre de la ocupación es requerido.")]
        [MaxLength(50, ErrorMessage = "El nombre de la ocupación no puede exceder los 50 caracteres.")]
        public string Occupation { get; set; }
        [MaxLength(50)]
        public string Picture { get; set; }
        public string Phone { get; set; }
        public string BloodGroup { get; set; }
        public string MaritalStatus { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        [Required]
        public int UserId { get; set; }
    }

    public class UpdatePatientDto
    {
        [MaxLength(50)]
        public string Occupation { get; set; }
        [MaxLength(50)]
        public string Picture { get; set; }
        public string Phone { get; set; }
        public string BloodGroup { get; set; }
        public string MaritalStatus { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int UserId { get; set; }
    }
}