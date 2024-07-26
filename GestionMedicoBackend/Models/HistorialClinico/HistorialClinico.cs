namespace GestionMedicoBackend.Models
{
    public class HistorialClinico
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
        public Patient Patient { get; set; }
    }
}
