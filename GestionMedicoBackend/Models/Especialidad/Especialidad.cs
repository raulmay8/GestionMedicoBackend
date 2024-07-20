using System.ComponentModel.DataAnnotations;

namespace GestionMedicoBackend.Models.Especialidad
{
	public class Especialidad
	{
		[Key]
		public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string Descripcion { get; set; }
	}
}
