using System.ComponentModel.DataAnnotations;

namespace GestionMedicoBackend.DTOs.Specialty
{
    public class SpecialtyDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class CreateSpecialtyDto
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
    }

    public class UpdateSpecialtyDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}