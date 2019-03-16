using Store.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class PhotosViewModel
    {
        public List<PhotoModel> Photos { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
