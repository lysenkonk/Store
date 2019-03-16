using Microsoft.AspNetCore.Http;
using Store.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }

        IEnumerable<string> Categories { get; }

        Task<Product> SaveProductAsync(Product product);

        Task<Product> DeleteProductAsync(int productID);

        Task<FileModel> AddImageAsync(int productId, FileModel image);

        Task<FileModel> RemoveImageAsync(int productId, string fileName);
    }
}
