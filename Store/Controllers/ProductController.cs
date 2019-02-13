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
        public int PageSize = 20;

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

        //public ViewResult List(string category, int page = 1)
        //{
        //    var viewModel = new ProductsListViewModel
        //    {
        //        Products = repository.Products
        //                    .Where(p => category == null || p.Category == category)
        //                    .OrderBy(p => p.ProductID)
        //                    .Skip((page - 1) * PageSize)
        //                    .Take(PageSize),
        //        PagingInfo = new PagingInfo
        //        {
        //            CurrentPage = page,
        //            ItemsPerPage = PageSize,
        //            TotalItems = category == null ?
        //                            repository.Products.Count() :
        //                            repository.Products.Where(e =>
        //                                e.Category == category).Count()
        //        },
        //        CurrentCategory = category
        //    };
        //    return View("~/Views/Product/List.cshtml", viewModel);
        //}
            
        public  ViewResult List(string category, int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            IEnumerable<Product> products = repository.Products
                .Where(p => category == null || p.Category == category);

            var viewModel = new ProductsListViewModel
            {
                Products = (sortProducts(sortOrder, products))
                    .Skip((page - 1) * PageSize)
                            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                                    repository.Products.Count() :
                                    products.Where(e =>
                                        e.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View("~/Views/Product/List.cshtml", viewModel);
        }




        private IEnumerable<Product> sortProducts(SortState sortOrder, IEnumerable<Product> products)
        {
            IEnumerable<Product> sortedProducts = products;
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
