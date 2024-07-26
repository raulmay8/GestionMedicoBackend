using System.Net;
using System.Net.Mail;
using GestionMedicoBackend.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using GestionMedicoBackend.Models.Contact;

namespace GestionMedicoBackend.Services.User
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
        Task SendConfirmationEmailAsync(string toEmail, string username, string confirmationLink);
        Task SendPasswordResetEmailAsync(string toEmail, string username, string resetLink);
        Task SendAppointmentConfirmationEmailAsync(string toEmail, Appointments appointment);
        Task SendContactConfirmationEmailAsync(string toEmail, ContactMessage contact);
    }

    public class EmailServices : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly IConfiguration _configuration;
        private readonly EmailTemplateService _emailTemplateService;

        public EmailServices(IConfiguration configuration, EmailTemplateService emailTemplateService)
        {
            _configuration = configuration;
            _emailTemplateService = emailTemplateService;

            var smtpSettings = _configuration.GetSection("SmtpSettings");
            _smtpClient = new SmtpClient(smtpSettings["Host"], int.Parse(smtpSettings["Port"]))
            {
                Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]),
                EnableSsl = true
            };
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpSettings["SenderEmail"], smtpSettings["SenderName"]),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            await _smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendConfirmationEmailAsync(string toEmail, string username, string confirmationLink)
        {
            var templateModel = new EmailTemplateModel
            {
                Username = username,
                ConfirmationLink = confirmationLink
            };

            string message = await _emailTemplateService.RenderTemplateAsync("EmailTemplate", templateModel);
            await SendEmailAsync(toEmail, "Confirma tu cuenta", message);
        }

        public async Task SendPasswordResetEmailAsync(string toEmail, string username, string resetLink)
        {
            var templateModel = new PasswordTemplate
            {
                Username = username,
                ResetLink = resetLink
            };

            string message = await _emailTemplateService.RenderTemplateAsync("PasswordResetTemplate", templateModel);
            await SendEmailAsync(toEmail, "Restablece tu contraseña", message);
        }

        public async Task SendAppointmentConfirmationEmailAsync(string toEmail, Appointments appointment)
        {
            var templateModel = new AppointmentTemplateModel
            {
                MedicName = appointment.Medic.User.Username,
                Nombre = appointment.Nombre,
                FechaCita = appointment.FechaCita,
                Reason = appointment.Reason
            };

            string message = await _emailTemplateService.RenderTemplateAsync("AppointmentTemplate", templateModel);
            await SendEmailAsync(toEmail, "Confirmación de Cita", message);
        }

        public async Task SendContactConfirmationEmailAsync(string toEmail, ContactMessage contact)
        {
            var templateModel = new ContactTemplateModel
            {
                Name = contact.Name,
                Email = contact.Email,
                Message = contact.Message,
                CreatedAt = contact.CreatedAt
            };

            string message = await _emailTemplateService.RenderTemplateAsync("ContactTemplate", templateModel);
            await SendEmailAsync(toEmail, "Confirmación de Contacto", message);
        }
    }
}
