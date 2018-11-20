using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models.ViewModels
{
    public class AdminProductViewModel
    {
        public IEnumerable<string> Categories { get; set; }

        public Product Product { get; set; }
    }
}
