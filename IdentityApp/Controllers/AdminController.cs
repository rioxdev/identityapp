using IdentityApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IdentityApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ProductDbContext _productContext;

        public AdminController(ProductDbContext productContext)
        {
            _productContext = productContext;   
        }

        public IActionResult Index() => View(_productContext.Products.ToList());


        [HttpGet]
        public IActionResult Create() => View("Edit", new Product());
    
        [HttpGet]
        public IActionResult Edit(long id)
        {
            Product product = _productContext.Products.Find(id);
            if (product != null)
                return View("Edit", product);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Save(Product model)
        {
            _productContext.Update(model);
            _productContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var product = _productContext.Products.Find(id);
            if (product != null)
            {
                _productContext.Remove(product);
                _productContext.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
