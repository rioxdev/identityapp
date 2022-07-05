using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Routing;

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

    }
}
