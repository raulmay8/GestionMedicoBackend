using System.ComponentModel.DataAnnotations;

namespace GestionMedicoBackend.Validations
{
    public class ConsultorioValidations
    {
        public int PkConsultorio { get; set; }

        [Required(ErrorMessage = "El estado es requerido..")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "La disponibilidad es requerida..")]
        public bool Disponibilidad { get; set; }

        [Required(ErrorMessage = "El nombre es requerido..")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres..")]
        public string Nombre { get; set; }
    }
}
