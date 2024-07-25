using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionMedicoBackend.DTOs.Horario
{
    public class HorarioDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Fecha { get; set; }
        public string Turno { get; set; }
        public TimeSpan Entrada { get; set; }
        public TimeSpan Salida { get; set; }
        public int MedicId { get; set; }
        public string MedicName { get; set; }
    }

    public class CreateHorarioDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public string Turno { get; set; }
        [Required]
        public TimeSpan Entrada { get; set; }
        [Required]
        public TimeSpan Salida { get; set; }
    }

    public class UpdateHorarioDto
    {
        public string Name { get; set; }
        public DateTime Fecha { get; set; }
        public string Turno { get; set; }
        public TimeSpan Entrada { get; set; }
        public TimeSpan Salida { get; set; }
    }
}
