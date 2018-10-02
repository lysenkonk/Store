using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Store.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;
        //IHostingEnvironment _appEnviroment;

        public EFProductRepository(ApplicationDbContext ctx /*IHostingEnvironment appEnvironment*/)
        {
            context = ctx;
            //_appEnviroment = appEnvironment;
        }
        public IQueryable<Product> Products => context.Products.Include(p => p.Image);

        //public IQueryable<Product> Products()
        //{
        //    context.Products.I
        //}

        public void SaveProduct(Product product, FileModel image)
        {
            if(product.ProductID == 0)
            {
                context.Products.Add(product);
            }else
            {
                Product dbEntry = context.Products
                    .FirstOrDefault(p => p.ProductID == product.ProductID);
                if(dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.Image = image;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = context.Products
                .FirstOrDefault(p => p.ProductID == productID);
            if(dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }

}
