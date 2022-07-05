using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace IdentityApp.Pages.Identity
{
    public class IndexModel : UserPageModel
    {
        public string Email { get; set; }
        public string Phone { get; set; }

        private readonly UserManager<IdentityUser> _userManager;
        
        public IndexModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            Email = user?.Email ?? "<none>";
            Phone = user?.PhoneNumber ?? "<none>";
        }
    }
}
