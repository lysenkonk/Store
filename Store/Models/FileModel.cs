using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class FileModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //TODO remove it and make migration to delete column
        public string Path { get; set; }
    }
}
