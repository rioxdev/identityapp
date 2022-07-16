using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApp.Pages.Identity.Admin
{
    public class DashboardModel : AdminPageModel
    {
        public int UsersCount { get; set; } = 0;
        public int UsersUnconfirmed { get; set; } = 0;
        public int UsersLockedout { get; set; } = 0;
        public int UsersTwoFactor { get; set; } = 0;

        public UserManager<IdentityUser> UserManager { get; set; }

        private readonly string[] emails =
        {
            "alice@example.com",
            "bob@example.com",
            "charlie@example.com",
            "rioxa11@yopmail.com",
            "rioxdev@gmail.com"
        };

        public DashboardModel(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
        }

        public void OnGet()
        {
            UsersCount = UserManager.Users.Count();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            foreach (IdentityUser existingUser in UserManager.Users.ToList())
            {
                IdentityResult result = await UserManager.DeleteAsync(existingUser);
                result.Process(ModelState);
            }

            foreach (var email in emails)
            {
                IdentityUser user = new IdentityUser()
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                IdentityResult result = await UserManager.CreateAsync(user);

                if (result.Process(ModelState))
                {
                    result = await UserManager.AddPasswordAsync(user, "PassRiox2022!");
                    result.Process(ModelState);
                }
            }

            if (ModelState.IsValid)
                return RedirectToPage();

            return Page();
        }
    }
}
