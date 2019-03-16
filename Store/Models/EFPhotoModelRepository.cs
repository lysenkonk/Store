using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Models.ViewModels;

namespace Store.Models
{
    public class EFPhotoModelRepository : IPhotoModelRepository
    {
        private readonly ApplicationDbContext _context;

        public EFPhotoModelRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }


        public IQueryable<PhotoModel> PhotoModels => _context.PhotoModels;

        public async Task<PhotoModel> SavePhotoModelAsync(PhotoModel photo)
        {
            if (photo.Id == 0)
            {
                _context.PhotoModels.Add(photo);
            }
            else
            {
                PhotoModel dbEntry = _context.PhotoModels.FirstOrDefault(p => p.Id == photo.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = photo.Name;
                    dbEntry.Path = photo.Path;
                }
            }
            await _context.SaveChangesAsync();
            return photo;
        }

        public async Task<PhotoModel> RemovePhotoModelAsync(string photoName)
        {
            PhotoModel dbEntry = _context.PhotoModels.FirstOrDefault(p => p.Name == photoName);

            if (dbEntry != null)
            {
                _context.PhotoModels.Remove(dbEntry);
                await _context.SaveChangesAsync();
            }
            return dbEntry;
        }
    }
}
