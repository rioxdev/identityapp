using IdentityApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace IdentityApp.Pages.Identity
{
    [AllowAnonymous]
    public class UserPasswordRecoveryModel : UserPageModel
    {

        private UserManager<IdentityUser> _userManager;
        private IdentityEmailService _emailService;

        public UserPasswordRecoveryModel(UserManager<IdentityUser> userManager, IdentityEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<ActionResult> OnPostAsync([Required] string email)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    await _emailService.SendPasswordRecovery(user, "PasswordRecoveryPost");
                }

                TempData["message"] = "We have sent you an email, click the link to choose a new password";
                return RedirectToPage();
            }

            return Page();
        }
    }
}
