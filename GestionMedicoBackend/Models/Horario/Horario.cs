using GestionMedicoBackend.Models;
using GestionMedicoBackend.Models.Especialidad;
using System;
using System.ComponentModel.DataAnnotations;

namespace GestionMedicoBackend.Models.Horario
{
	public class Horario
	{
		[Key]
		public int Id { get; set; }
        [Required]
        [MaxLength(50)]
		public string Name { get; set; }
        [Required]
        public DateOnly Fecha { get; set; }
        [Required]
        [MaxLength(50)]
        public string Turno { get; set; }
        [Required]
        public TimeOnly Entrada { get; set; }
        [Required]
        public TimeOnly Salida { get; set; }
	}
}
