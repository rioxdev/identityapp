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
    public class PasswordRecoveryPostModel : UserPageModel
    {

        private UserManager<IdentityUser> _userManager { get; set; }
        private TokenUrlEncoderUrlService _tokenUrlEncoderUrlService { get; set; }

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

        public PasswordRecoveryPostModel(UserManager<IdentityUser> userManager, TokenUrlEncoderUrlService tokenUrlEncoderUrlService)
        {
            _userManager = userManager;
            _tokenUrlEncoderUrlService = tokenUrlEncoderUrlService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByEmailAsync(Email);
                string decodedToken = _tokenUrlEncoderUrlService.DecodeToken(Token);
                IdentityResult result = await _userManager.ResetPasswordAsync(user, decodedToken, Password);

                if (result.Process(ModelState))
                {
                    TempData["message"] = "Password changed";
                    return RedirectToPage();
                }
            }

            return Page();
        }
    }
}
