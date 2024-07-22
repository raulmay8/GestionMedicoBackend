using System.ComponentModel.DataAnnotations;

namespace GestionMedicoBackend.DTOs.Appointments
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public int? PatientId { get; set; }
        public string PatientName { get; set; }
        public int MedicId { get; set; }
        public string MedicName { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Genero { get; set; }
        public string Correo { get; set; }
        public string NumeroTelefono { get; set; }
        public string Estado { get; set; }
        public string CodigoPostal { get; set; }
        public int SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public DateTime FechaCita { get; set; }
    }
    public class CreateAppointmentDto
    {
        [Required]
        public string Reason { get; set; }
        public int? PatientId { get; set; }
        [Required]
        public int MedicId { get; set; }
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
        [Required]
        public int SpecialtyId { get; set; }
        [Required]
        public DateTime FechaCita { get; set; }
    }

    public class UpdateAppointmentDto
    {
        public string Reason { get; set; }
        public int? PatientId { get; set; }
        public int MedicId { get; set; }
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
        public DateTime FechaCita { get; set; }
    }
}
