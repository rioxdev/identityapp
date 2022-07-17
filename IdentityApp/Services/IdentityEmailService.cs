using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace IdentityApp.Services
{
    public class IdentityEmailService
    {
        public IEmailSender EmailSender { get; set; }
        public UserManager<IdentityUser> UserManager { get; set; }
        public IHttpContextAccessor ContextAccessor { get; set; }
        public LinkGenerator LinkGenerator { get; set; }
        public TokenUrlEncoderUrlService TokenEncoder { get; set; }

        public IdentityEmailService(IEmailSender emailSender, UserManager<IdentityUser> userManager, IHttpContextAccessor contextAccessor, LinkGenerator linkGenerator, TokenUrlEncoderUrlService tokenEncoder)
        {
            EmailSender = emailSender;
            UserManager = userManager;
            ContextAccessor = contextAccessor;
            LinkGenerator = linkGenerator;
            TokenEncoder = tokenEncoder;
        }

        private string GetUrl(string emailAddress, string token, string page)
        {
            string safeToken = TokenEncoder.EncodeToken(token);

            return LinkGenerator.GetUriByPage(ContextAccessor.HttpContext, page, null, new { email = emailAddress, token = safeToken });
        }

        public async Task SendPasswordRecovery(IdentityUser user, string confirmationPage)
        {
            string token = await UserManager.GeneratePasswordResetTokenAsync(user);

            string url = GetUrl(user.Email, token, confirmationPage);

            string body = $"Please set your password by <a href={url}>clicking here</a>.";

            await EmailSender.SendEmailAsync(user.Email, "Set your password", body);
        }

        public async Task SendAccountConfirmEmail(IdentityUser user, string confirmPage)
        {
            string token = await UserManager.GenerateEmailConfirmationTokenAsync(user);
            string url = GetUrl(user.Email, token, confirmPage);

            await EmailSender.SendEmailAsync(user.Email, "Complete your account setup",
                $"Please setup your account by <a href={url}>clicking here</a>");
        }

    }
}
