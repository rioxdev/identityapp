using IdentityApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace IdentityApp.Pages.Identity.Admin
{
    public class CreateModel : AdminPageModel
    {
        private UserManager<IdentityUser> _userManager;
        private IdentityEmailService _emailService;

        [BindProperty(SupportsGet = true)]
        [EmailAddress]
        public string Email { get; set; }   

        public CreateModel(UserManager<IdentityUser> userManager, IdentityEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = Email,
                    Email = Email,
                    EmailConfirmed = true
                };

                IdentityResult result = await _userManager.CreateAsync(user);

                if (result.Process(ModelState))
                {
                    await _emailService.SendPasswordRecovery(user, "/identity/useraccountcomplete");

                    TempData["message"] = "User created";
                    return RedirectToPage();
                }
            }

            return Page();
        }
    }
}
