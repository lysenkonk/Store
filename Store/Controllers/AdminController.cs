using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Store.Services;

namespace Store.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ProductsService _productsService;
        IHostingEnvironment _appEnvironment;

        public AdminController(ProductsService service, IHostingEnvironment appEnvironment)
        {
            _productsService = service;
            _appEnvironment = appEnvironment;
        }
        public ViewResult Index()
        {
            return View(_productsService.Products);
        }
            

        public ViewResult Edit(int productId) =>
            View(_productsService.Products
                .FirstOrDefault(p => p.ProductID == productId));

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

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddImage(int productId, IFormFile uploadedFile)
        {
            await _productsService.AddImage(productId, uploadedFile);

            return RedirectToAction("Index");
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