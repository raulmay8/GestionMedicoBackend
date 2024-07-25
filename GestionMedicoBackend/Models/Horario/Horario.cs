using System.ComponentModel.DataAnnotations;

namespace GestionMedicoBackend.Models
{
    public class Horario
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Fecha { get; set; }
        public string Turno { get; set; }
        public TimeOnly Entrada { get; set; }
        public TimeOnly Salida { get; set; }
        public ICollection<Medic> Medics { get; set; }
    }
}