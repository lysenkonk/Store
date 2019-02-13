using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Store.Models.ViewModels
{
    public class FilterViewModel
    {
       public FilterViewModel(List<string> categories, string category)
        {
            categories.Insert(0, "all");
            Categories = new SelectList(categories, "Name", category);
            SelectedCategory = category;
        }

        public SelectList Categories { get; private set; }
        public string SelectedCategory { get; private set; }
    }
}
