using System.ComponentModel.DataAnnotations;

namespace GestionMedicoBackend.DTOs.Consultorio
{
    public class ConsultorioDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status  { get; set; }
        public bool Availability { get; set; }
        public List<string> MedicNames { get; set; }
    }

    public class CreateConsultorioDto
    {
        [Required]
        public string Name { get; set; }
    }

    public class UpdateConsultorioDto
    {
        public string Name { get; set; }
    }
}
