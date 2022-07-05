using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace IdentityApp.Pages.Identity.Admin
{
    public class FeaturesModel : AdminPageModel
    {

        public UserManager<IdentityUser> UserManager { get; set; }

        public IEnumerable<(string, string)> Features { get; set; }


        public FeaturesModel(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
        }

        public void OnGet()
        {
            Features = UserManager.GetType().GetProperties()
                .Where(prop => prop.Name.StartsWith("Supports"))
                .OrderBy(p => p.Name)
                .Select(prop => (prop.Name, prop.GetValue(UserManager).ToString()));
                
        }
    }
}
