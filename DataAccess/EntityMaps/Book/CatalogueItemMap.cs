using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityMaps.Book
{
    public class CatalogueItemMap : ICatalogueItemMap
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
    }
}
