using System.Collections.Generic;
using Store.Models;

namespace Store.Models.ViewModels
{    
    public class ProductsViewModel
    {

       public IEnumerable<Product> Products { get; set; }
       public PagingInfo PageInfo             { get; set; }
       public SortViewModel SortViewModel   { get; set; }
       public FilterViewModel FilterViewModel { get; set; }
    }
}
 