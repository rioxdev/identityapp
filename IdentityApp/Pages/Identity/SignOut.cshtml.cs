using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace IdentityApp.Pages.Identity
{
    [AllowAnonymous]
    public class SignOutModel : UserPageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public SignOutModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager; 
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync();

            return RedirectToPage();
        }

    }
}
