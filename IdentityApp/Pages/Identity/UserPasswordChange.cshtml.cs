using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace IdentityApp.Pages.Identity
{
    public class UserPasswordChangeModel : PageModel
    {

        private readonly UserManager<IdentityUser> _identityUser;

        public UserPasswordChangeModel(UserManager<IdentityUser> identityUser)
        {
            _identityUser = identityUser;
        }

        public async Task<IActionResult> OnPostAsync(PasswordChangeBindingTarget data)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _identityUser.GetUserAsync(User);
                IdentityResult result = await _identityUser.ChangePasswordAsync(user, data.Current, data.NewPassword);

                if (result.Process(ModelState))
                {
                    TempData["message"] = "Password changed";
                    return RedirectToPage();
                }
            }

            return Page();
        }

    }

    public class PasswordChangeBindingTarget
    {
        [Required]
        public string Current { get; set; }
        [Required]
        public string NewPassword { get; set; }

        [Required]
        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
    }

}
