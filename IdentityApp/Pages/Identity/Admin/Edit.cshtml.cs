using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace IdentityApp.Pages.Identity.Admin
{
    public class EditModel : AdminPageModel
    {
        public UserManager<IdentityUser> UserManager { get; set; }

        [BindProperty(SupportsGet = true)]
        public IdentityUser IdentityUser { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public EditModel(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(Id))
            {
                return RedirectToPage("Selectuser",
                    new
                    {
                        Label = "Edit User",
                        Callback = "Edit"
                    });

            }

            IdentityUser = await UserManager.FindByIdAsync(Id);

            return Page();
        }

        public async Task<ActionResult> OnPostAsync([FromForm(Name = "IdentityUser")] EditBindingTarget userData)
        {
            if (!string.IsNullOrEmpty(Id) && ModelState.IsValid)
            {
                IdentityUser user = await UserManager.FindByIdAsync(Id);
                if (user != null)
                {
                    user.UserName = userData.UserName;
                    user.Email = userData.Email;
                    user.EmailConfirmed = true;
                    if (userData.PhoneNumber != null)
                        user.PhoneNumber = userData.PhoneNumber;

                }

                var result = await UserManager.UpdateAsync(user);
                if (result.Process(ModelState))
                    return RedirectToPage();

            }

            IdentityUser = await UserManager.FindByIdAsync(Id);
            return Page();
        }
    }


    public class EditBindingTarget
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }

}
