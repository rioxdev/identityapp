using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Threading.Tasks;
using System.Web;

namespace IdentityApp.Services
{
    public class ConsoleEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine(" --- New email ---");
            Console.WriteLine($"To : {email}");
            Console.WriteLine($"Subject : {subject}");
            Console.WriteLine(HttpUtility.HtmlDecode(htmlMessage));
            Console.WriteLine("-------");
            
            return Task.CompletedTask;
        }
    }
}
