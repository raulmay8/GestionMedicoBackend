using System.ComponentModel.DataAnnotations;

namespace GestionMedicoBackend.DTOs.Appointments
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int MedicId { get; set; }
        public string MedicName { get; set; }
    }
    public class CreateAppointmentDto
    {
        [Required]
        public string Reason { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int MedicId { get; set; }
    }

    public class UpdateAppointmentDto
    {
        public string Reason { get; set; }
        public int PatientId { get; set; }
        public int MedicId { get; set; }
    }
}
