using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IdentityApp.Services
{
    public class GmailEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage message = new MailMessage();
            message.Subject = subject;
            message.Body = htmlMessage;
            message.IsBodyHtml = true;
            message.From = new MailAddress("henrio.devops@outlook.fr");
            message.To.Add(new MailAddress(email));

            SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("henrio.devops@outlook.fr", "PassRiox2022!!!");
            smtp.EnableSsl = true;

            smtp.Send(message);

            return Task.CompletedTask;
        }
    }
}
