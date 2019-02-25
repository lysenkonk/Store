using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Models;
using Store.Models.ViewModels;

namespace Store.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int pageSize = 20;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Product(int id)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == id);

            if (product == null)
            {
                return View("Product not found");
            }
            return View(product);
        }

                
        public  async Task<IActionResult> List(string category, int page = 1, SortState sortOrder = SortState.NameAsc)
        {

               IQueryable<Product> products = repository.Products
                .Where(p => category == null || p.Category == category );
            products = sortProducts(sortOrder, products);



            var count = await products.CountAsync();
            var items = await products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ProductsViewModel viewModel = new ProductsViewModel
            {
                PageInfo = new PageInfo(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(repository.Categories.ToList(), category),
                Products = items
            };
            return View("~/Views/Product/List.cshtml", viewModel);
        }

        private IQueryable<Product> sortProducts(SortState sortOrder, IQueryable<Product> products)
        {
            IQueryable<Product> sortedProducts = products;
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case SortState.PriceAsc:
                    products = products.OrderBy(p => p.Price);
                    break;
                case SortState.PriceDesc:
                    products = products.OrderByDescending(p => p.Price);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;   
            }
            return products;
        }
          

    }
}
