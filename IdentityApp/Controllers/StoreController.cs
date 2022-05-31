using IdentityApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IdentityApp.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {
        private readonly ProductDbContext _productContext;

        public StoreController(ProductDbContext productContext)
        {
            _productContext = productContext;
        }

        public IActionResult Index()
        {
            return View(_productContext.Products.ToList());
        }
    }
}
