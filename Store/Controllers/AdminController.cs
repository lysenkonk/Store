using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Store.Services;
using Store.Models.ViewModels;

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
            return View("Products", _productsService.Products);
        }

        public IActionResult Edit(int productId)
        {
            var product = _productsService.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product == null)
                return NotFound();

            var viewModel = new AdminProductViewModel
            {
                Product = product,
                Categories = _productsService.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x)
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            
            if (ModelState.IsValid)
            {
                await _productsService.SaveProductAsync(product);
                TempData["message"] = $"{product.Name} has been saved";
            }

            var viewModel = new AdminProductViewModel
            {
                Product = product,
                Categories = _productsService.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x)
            };
            return View(viewModel);
        }

        public async Task<IActionResult> RemoveImage(int productId, string imageName)
        {
            if(!isProduct(productId))
            {
                return RedirectToAction("Create");
            }

            await _productsService.RemoveImage(productId, imageName);

            return RedirectToAction("Edit", new { productId });
        }

        public async Task<IActionResult> AddImage(int productId, IFormFile uploadedFile)
        {
            if(!isProduct(productId))
            {
                return RedirectToAction("Create");
            }

            await _productsService.AddImage(productId, uploadedFile);

            return RedirectToAction("Edit", new { productId });
        }

        public IActionResult Create()
        {
            var viewModel = new AdminProductViewModel
            {
                Product = new Product(),
                Categories = _productsService.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x)
            };
            return View("Edit", viewModel);
        }

        
        public async Task<IActionResult> Delete(int productId)
        {
            Product deletedProduct =  await _productsService.DeleteProductAsync(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }
            return RedirectToAction("Index");
        }

        private bool isProduct(int productId)
        {
            var product = _productsService.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product == null)
            {
                TempData["message"] = $"That product doesn't exist";
                return false;
            }
            return true;
        }
    }
}