using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionMedicoBackend.Models.Consultorio
{
    public class Consultorio
    {
        [Key]
        public int PkConsultorio { get; set; }
        public bool Status { get; set; }
        public bool Disponibilidad { get; set; }
        public string Nombre { get; set; }
    }
}
