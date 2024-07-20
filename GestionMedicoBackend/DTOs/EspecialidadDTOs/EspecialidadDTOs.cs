namespace GestionMedicoBackend.DTOs.Especialidad
{
    public class EspecialidadDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class CreateEspecialidadDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class UpdateEspecialidadDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
