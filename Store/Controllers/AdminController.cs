using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Store.Services;
using Store.Models.ViewModels;
using System.Collections.Generic;

namespace Store.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ProductsService _productsService;
        private readonly PhotosService _photosService;

        public int pageSize = 20;

        public AdminController(ProductsService service, PhotosService photosService)
        {
            _productsService = service;
            _photosService = photosService;
        }

        //-----------------------------------------------------------------------------------------------------------------Gallery actions------------------------------------------------------------------------------------------------
        public async Task<IActionResult> AddImageToGallery(IFormFile uploadedFile)
        {
            await _photosService.SavePhoto(uploadedFile);

            return RedirectToAction("List");
        }

        public IActionResult Gallery(int page = 1)
        {
            List<PhotoModel> photos = new List<PhotoModel>(_photosService.Photos.Skip((page - 1) * pageSize).Take(pageSize));

            PhotosViewModel viewModel = new PhotosViewModel
            {
                Photos = new List<PhotoModel>(_photosService.Photos.Skip((page - 1) * pageSize).Take(pageSize)),
                PageInfo = new PageInfo(_photosService.Photos.Count(), page, pageSize)
            };
            return View(viewModel);
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------------------------Product actions-------------------------------------------------------------------------------------------------
        public ViewResult Index()
        {
            return View("Products", _productsService.Products);
        }

        public ViewResult List(string category, int page = 1)
        {
            IEnumerable<Product> products = _productsService.Products
             .Where(p => category == null || p.Category == category);

            var count = products.Count();
            var items = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ProductsViewModel viewModel = new ProductsViewModel
            {
                PageInfo = new PageInfo(count, page, pageSize),
                SortViewModel = new SortViewModel(SortState.NameAsc),
                FilterViewModel = new FilterViewModel(_productsService.Categories.ToList(), category),
                Products = items
            };
            return View("~/Views/Admin/Products.cshtml", viewModel.Products);
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
            if (!isProduct(productId))
            {
                return RedirectToAction("Create");
            }

            await _productsService.RemoveImage(productId, imageName);

            return RedirectToAction("Edit", new { productId });
        }

        public async Task<IActionResult> AddImage(int productId, IFormFile uploadedFile)
        {
            if (!isProduct(productId))
            {
                return RedirectToAction("Create");
            }

            await _productsService.AddImage(productId, uploadedFile);

            return RedirectToAction("Edit", new { productId });
        }

        //public IActionResult ListPhotos(int page = 1)
        //{
        //    IEnumerable<PhotoModel> photos = _photosService.Photos.Skip((page - 1) * pageSize).Take(pageSize);

        //    PhotosViewModel viewModel = new PhotosViewModel
        //    {
        //        Photos = _photosService.Photos.Skip((page - 1) * pageSize).Take(pageSize),
        //        PageInfo = new PageInfo(_photosService.Photos.Count(), page, pageSize)
        //    };
        //    return View(viewModel);
        //}

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