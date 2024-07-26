using System.ComponentModel.DataAnnotations;

namespace GestionMedicoBackend.DTOs.HistorialClinico
{
    public class HistorialClinicoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string? Smoke { get; set; }
        public string? Alcohol { get; set; }
        public string? Coffee { get; set; }
        public string? Allergic { get; set; }
        public string? Allergies { get; set; }
        public string? TakesMedication { get; set; }
        public string? Medication { get; set; }
        public string? MedicalHistory { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
    }

    public class CreateHistorialClinicoDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalCode { get; set; }
        public string? Smoke { get; set; }
        public string? Alcohol { get; set; }
        public string? Coffee { get; set; }
        public string? Allergic { get; set; }
        public string? Allergies { get; set; }
        public string? TakesMedication { get; set; }
        public string? Medication { get; set; }
        public string? MedicalHistory { get; set; }
        [Required]
        public int PatientId { get; set; }
    }

    public class UpdateHistorialClinicoDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string? Smoke { get; set; }
        public string? Alcohol { get; set; }
        public string? Coffee { get; set; }
        public string? Allergic { get; set; }
        public string? Allergies { get; set; }
        public string? TakesMedication { get; set; }
        public string? Medication { get; set; }
        public string? MedicalHistory { get; set; }
        public int PatientId { get; set; }
    }
}
