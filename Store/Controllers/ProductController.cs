using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Models.ViewModels;

namespace Store.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 8;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Product(int productId)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product == null)
            {
                return View("Product not found");
            }
            return View(product);
        }

        public ViewResult List(string category, int productPage = 1)
            => View(new ProductsListViewModel
            {
                Products = repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems =  category == null ?
                        repository.Products.Count() :
                        repository.Products.Where(e =>
                            e.Category == category).Count()
                },
                CurrentCategory = category
            });
    }
}
