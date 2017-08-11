using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityMaps.Book
{
    public interface ICatalogueItemMap : IMap
    {
        string Title { get; set; }
        string Author { get; set; }
        string Isbn { get; set; }
    }
}
