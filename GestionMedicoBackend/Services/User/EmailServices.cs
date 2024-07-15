using System.Net;
using System.Net.Mail;
using GestionMedicoBackend.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GestionMedicoBackend.Services.User
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
        Task SendConfirmationEmailAsync(string toEmail, string username, string confirmationLink);
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
    }
}
