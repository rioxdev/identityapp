using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace IdentityApp.Pages.Identity.Admin
{
    public class EditModel : AdminPageModel
    {
        public UserManager<IdentityUser> UserManager { get; set; }

        public EditModel(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
        }

        public void OnGet()
        {
        }
    }


    public class EditBindingTarget
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }
    }

}
