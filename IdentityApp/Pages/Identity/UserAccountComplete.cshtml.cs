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
    public class UserAccountCompleteModel : UserPageModel
    {

        private UserManager<IdentityUser> _userManager;
        private TokenUrlEncoderUrlService _tokenUrlEncoder;

        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Token { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }

        [BindProperty]
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        public UserAccountCompleteModel(UserManager<IdentityUser> userManager, TokenUrlEncoderUrlService tokenUrlEncoder)
        {
            _userManager = userManager;
            _tokenUrlEncoder = tokenUrlEncoder;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByEmailAsync(Email);
                string decodedToken = _tokenUrlEncoder.DecodeToken(Token);

                IdentityResult result = await _userManager.ResetPasswordAsync(user, decodedToken, Password);

                if (result.Process(ModelState))
                {
                    return RedirectToPage("SignIn", new { });
                }

            }

            return Page();
        }
    }
}
