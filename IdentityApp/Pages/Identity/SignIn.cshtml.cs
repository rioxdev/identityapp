using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace IdentityApp.Pages.Identity
{
    [AllowAnonymous]
    public class SignInModel : UserPageModel
    {

        private readonly SignInManager<IdentityUser> _signInManager;

        [EmailAddress]
        [Required]
        [BindProperty]
        public string Email { get; set; }

        [Required]
        [BindProperty]
        public string Password { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }

        public SignInModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(Email, Password, true, false);

            if (result.Succeeded)
                return Redirect(ReturnUrl ?? "/");
            else if (result.IsLockedOut)
                TempData["message"] = "Account locked";
            else if (result.IsNotAllowed)
                TempData["message"] = "Sign in not allowed";
            else if (result.RequiresTwoFactor)
                return RedirectToPage("SingInTwoFactor", new { ReturnUrl });
            else
                TempData["message"] = "Sign in failed";

            return Page();
        }
    }
}
