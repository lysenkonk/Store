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

namespace Store.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;
        IHostingEnvironment _appEnvironment;

        public AdminController(IProductRepository repo, IHostingEnvironment appEnvironment)
        {
            repository = repo;
            _appEnvironment = appEnvironment;
        }
        public ViewResult Index()
        {
            return View(repository.Products);
        }
            

        public ViewResult Edit(int productId) =>
            View(repository.Products
                .FirstOrDefault(p => p.ProductID == productId));

        [HttpPost]
        public async Task<IActionResult> Edit(Product product, IFormFile uploadedFile)
        {
            FileModel file = null;
            if (uploadedFile != null)
            {
                string path = "/Files/Bg/" + uploadedFile.FileName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/Files/Bg/" + uploadedFile.FileName, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                Bitmap resized = ResizeImage(uploadedFile.OpenReadStream(), 195, 195);
                resized.Save(_appEnvironment.WebRootPath + "/Files/Sm/" + uploadedFile.FileName, ImageFormat.Png);
                file = new FileModel { Name = uploadedFile.FileName, Path = path };
            }
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product, file);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }
        
        public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }
            return RedirectToAction("Index");
        }

        private static Bitmap ResizeImage(Stream stream, int width, int height)
        {
            var resized = new Bitmap(width, height);
            using (var image = new Bitmap(stream))
            using (var graphics = Graphics.FromImage(resized))
            {
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.DrawImage(image, 0, 0, width, height);
            }

            return resized;
        }
    }
}