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
    public class SignUpResendModel : UserPageModel
    {

        private UserManager<IdentityUser> _userManager;
        private IdentityEmailService _emailService;

        [BindProperty(SupportsGet = true)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public SignUpResendModel(UserManager<IdentityUser> userManager, IdentityEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByEmailAsync(Email);
                if(user != null && !await _userManager.IsEmailConfirmedAsync(user))
                {
                    await _emailService.SendAccountConfirmEmail(user, "SignUpConfirm");
                }

                TempData["message"] = "Confirmation email sent, chexk your inbox";
                return RedirectToPage(new { Email });
            }

            return Page();
        }
    }
}
