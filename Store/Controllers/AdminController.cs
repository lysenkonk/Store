using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Store.Services;

namespace Store.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ProductsService _productsService;

        public AdminController(ProductsService service)
        {
            _productsService = service;
        }

        public ViewResult Index()
        {
            return View(_productsService.Products);
        }

        public IActionResult Edit(int productId)
        {
            var product = _productsService.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            
            if (ModelState.IsValid)
            {
                await _productsService.SaveProductAsync(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public async Task<IActionResult> RemoveImage(int productId, string imageName)
        {
            await _productsService.RemoveImage(productId, imageName);

            return RedirectToAction("Edit", new { productId });
        }

        public async Task<IActionResult> AddImage(int productId, IFormFile uploadedFile)
        {
            await _productsService.AddImage(productId, uploadedFile);

            return RedirectToAction("Edit", new { productId });
        }

        public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        public async Task<IActionResult> Delete(int productId)
        {
            Product deletedProduct =  await _productsService.DeleteProductAsync(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }
            return RedirectToAction("Index");
        }
    }
}