namespace GestionMedicoBackend.DTOs.Medico
{
    public class MedicoDto
    {
        public int PkMedico { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public char Genero { get; set; }
        public string CedulaProfesional { get; set; }
        public string Escuela { get; set; }
        public int AnosDeExperiencia { get; set; }
        public bool Disponibilidad { get; set; }
        public bool Status { get; set; }
        public string Habilidades { get; set; }
    }

    public class CreateMedicoDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Password { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public char Genero { get; set; }
        public string CedulaProfesional { get; set; }
        public string Escuela { get; set; }
        public int AnosDeExperiencia { get; set; }
        public bool Disponibilidad { get; set; }
        public bool Status { get; set; }
        public string Habilidades { get; set; }
    }

    public class UpdateMedicoDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Password { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public char Genero { get; set; }
        public string CedulaProfesional { get; set; }
        public string Escuela { get; set; }
        public int AnosDeExperiencia { get; set; }
        public bool Disponibilidad { get; set; }
        public bool Status { get; set; }
        public string Habilidades { get; set; }
    }
}
