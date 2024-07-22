namespace GestionMedicoBackend.Models
{
    public class AppointmentTemplateModel
    {
        public string MedicName { get; set; }
        public string PatientName { get; set; }
        public DateTime FechaCita { get; set; }
        public string Reason { get; set; }
    }
}
