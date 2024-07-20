namespace GestionMedicoBackend.Models
{
    public class EmailTemplateModel
    {
        public string Username { get; set; }
        public string ConfirmationLink { get; set; }
        public string ResetLink { get; set; }
    }
}
