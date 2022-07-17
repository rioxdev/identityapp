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
    public class SignUpModel : UserPageModel
    {

        private UserManager<IdentityUser> _userManager;
        private IdentityEmailService _emailService;

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }

        public SignUpModel(UserManager<IdentityUser> userManager, IdentityEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByEmailAsync(Email);
                if (user != null && !await _userManager.IsEmailConfirmedAsync(user))
                    return RedirectToPage("SignUpConfirm");

                user = new IdentityUser
                {
                    Email = Email,
                    UserName = Email
                };

                IdentityResult result = await _userManager.CreateAsync(user);   
                if (result.Process(ModelState))
                {
                    result = await _userManager.AddPasswordAsync(user, Password);
                    if (result.Process(ModelState))
                    {
                        await _emailService.SendAccountConfirmEmail(user, "/identity/SignUpConfirm");
                        return RedirectToPage("SignUpConfirm");
                    }
                    else
                    {
                        await _userManager.DeleteAsync(user);
                    }
                }
            }

            return Page();
        }
    }
}
