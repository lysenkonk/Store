using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Store.Models
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public EFProductRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        public IQueryable<Product> Products => _context.Products.Include(p => p.Images);

        public IEnumerable<string> Categories => _context.Products.Select(x => x.Category)
                                                                  .Distinct()
                                                                  .OrderBy(x => x);

        public async Task<Product> SaveProductAsync(Product product)
        {
            if(product.ProductID == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                Product dbEntry = _context.Products.FirstOrDefault(p => p.ProductID == product.ProductID);

                if(dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProductAsync(int productID)
        {
            Product dbEntry = _context.Products
                .FirstOrDefault(p => p.ProductID == productID);
            if (dbEntry != null)
            {
                _context.Products.Remove(dbEntry);
                await _context.SaveChangesAsync();
            }

            return dbEntry;
        }

        public async Task<FileModel> AddImageAsync(int productId, FileModel image)
        {
            Product dbEntry = _context.Products.Include(p => p.Images).FirstOrDefault(p => p.ProductID == productId);

            if (dbEntry == null)
                throw new Exception("404 Not Found product");

            dbEntry.Images.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task<FileModel> RemoveImageAsync(int productId, string fileName)
        {
            Product dbEntry = _context.Products.Include(p => p.Images).FirstOrDefault(p => p.ProductID == productId);

            if (dbEntry == null)
                throw new Exception("404 Not Found product");

            FileModel image = dbEntry.Images.FirstOrDefault(img => img.Name == fileName);

            if (image == null)
                throw new Exception("404 Not Found Image");

            dbEntry.Images.Remove(image);
            await _context.SaveChangesAsync();
            return image;
        }
    }
}
