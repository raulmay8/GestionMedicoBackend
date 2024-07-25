namespace GestionMedicoBackend.Models
{
    public class Consultorio
    {
        public int Id { get; set; }
        public string Name {  get; set; }
        public bool Status { get; set; }
        public bool Availability { get; set; }
        public ICollection<Medic> Medics { get; set; }
    }
}
