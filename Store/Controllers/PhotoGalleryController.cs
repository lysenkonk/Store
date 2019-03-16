using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Models.ViewModels;

namespace Store.Controllers
{
    public class PhotoGalleryController : Controller
    {
        private IPhotoModelRepository repository;
        public int pageSize = 20;

        public PhotoGalleryController(IPhotoModelRepository repo)
        {
            repository = repo;
        }

        public IActionResult List(int page = 1)
        {
            IQueryable<PhotoModel> photos = repository.PhotoModels.Skip((page - 1)*pageSize).Take(pageSize);

            PhotosViewModel viewModel = new PhotosViewModel
            {
                Photos = new List<PhotoModel>(repository.PhotoModels.Skip((page - 1) * pageSize).Take(pageSize)),
                PageInfo = new PageInfo(repository.PhotoModels.Count(), page, pageSize)
            };
            return View(viewModel);
        }
    }
}
