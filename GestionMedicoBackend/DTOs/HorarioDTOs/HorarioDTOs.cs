namespace GestionMedicoBackend.DTOs.Horario
{
    public class HorarioDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Fecha { get; set; }
        public string Turno { get; set; }
        public TimeOnly Entrada { get; set; }
        public TimeOnly Salida { get; set; }
    }

    public class CreateHorarioDto
    {
        public string Name { get; set; }
        public DateOnly Fecha { get; set; }
        public string Turno { get; set; }
        public TimeOnly Entrada { get; set; }
        public TimeOnly Salida { get; set; }
    }

    public class UpdateHorarioDto
    {
        public string Name { get; set; }
        public DateOnly Fecha { get; set; }
        public string Turno { get; set; }
        public TimeOnly Entrada { get; set; }
        public TimeOnly Salida { get; set; }
    }
}
