using IdentityApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace IdentityApp.Pages.Identity.Admin
{
    public class PasswordsModel : AdminPageModel
    {

        private UserManager<IdentityUser> _userManager;
        private IdentityEmailService _emailService;

        public IdentityUser IdentityUser { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }

        [BindProperty]
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        public PasswordsModel(UserManager<IdentityUser> userManager, IdentityEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(Id))
                return RedirectToPage("SelectUser", new { Callback = "Passwords", Label = "Password" });

            IdentityUser  = await _userManager.FindByIdAsync(Id);
            return Page();
        }

        public async Task<IActionResult> OnPostSetPasswordAsync()
        {
            if (ModelState.IsValid)
            {

                IdentityUser = await _userManager.FindByIdAsync(Id);

                if (await _userManager.HasPasswordAsync(IdentityUser))
                    await _userManager.RemovePasswordAsync(IdentityUser);

                IdentityResult result = await _userManager.AddPasswordAsync(IdentityUser, Password);

                if (result.Process(ModelState))
                {
                    TempData["message"] = "Password changed";

                    return RedirectToPage();
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUserChangeAsync()
        {
            IdentityUser = await _userManager.FindByIdAsync(Id);
            await _userManager.RemovePasswordAsync(IdentityUser);
            await _emailService.SendPasswordRecovery(IdentityUser, "/identity/PasswordRecoveryPost");
            TempData["message"] = "Email sent to User";

            return RedirectToPage();
        }

    }
}
