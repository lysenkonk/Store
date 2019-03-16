using Store.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public interface IPhotoModelRepository
    {
        IQueryable<PhotoModel> PhotoModels { get; }

        Task<PhotoModel> SavePhotoModelAsync(PhotoModel photo);

        Task<PhotoModel> RemovePhotoModelAsync(string photoName);
    }
}
