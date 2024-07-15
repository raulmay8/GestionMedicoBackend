namespace GestionMedicoBackend.DTOs.Consultorio
{
    public class ConsultorioDto
    {
        public int PkConsultorio { get; set; }
        public bool Status { get; set; }
        public bool Disponibilidad { get; set; }
        public string Nombre { get; set; }
    }

    public class CreateConsultorioDto
    {
        public bool Status { get; set; }
        public bool Disponibilidad { get; set; }
        public string Nombre { get; set; }
    }

    public class UpdateConsultorioDto
    {
        public bool Status { get; set; }
        public bool Disponibilidad { get; set; }
        public string Nombre { get; set; }
    }
}
