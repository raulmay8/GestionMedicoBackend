﻿using System.ComponentModel.DataAnnotations;

namespace GestionMedicoBackend.DTOs.Medic
{
    public class MedicDto
    {
        public int Id { get; set; }
        public string ProfessionalId { get; set; }
        public string School { get; set; }
        public int YearExperience { get; set; }
        public DateOnly DateGraduate { get; set; }
        public bool Availability { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int HorarioId { get; set; }
        public string HorarioName { get; set; }
        public int ConsultorioId { get; set; }
        public string ConsultorioName { get; set; }
    }

    public class CreateMedicDto
    {
        [Required]
        public string ProfessionalId { get; set; }
        [Required]
        public string School { get; set; }
        [Required]
        public int YearExperience { get; set; }
        [Required]
        public int Year { get; set; } 
        [Required]
        public int Month { get; set; }
        [Required]
        public int Day { get; set; }
        [Required]
        public int HorarioId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ConsultorioId { get; set; }
    }

    public class UpdateMedicDto
    {
        public string ProfessionalId { get; set; }
        public string School { get; set; }
        public int YearExperience { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int HorarioId { get; set; }
        public int UserId { get; set; }
        public int ConsultorioId { get; set; }
    }
}