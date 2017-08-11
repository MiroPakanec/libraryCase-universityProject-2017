using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityMaps.Book
{
    public interface ICatalogueFilterMap : IMap
    {
        string Author { get; set; }
        string Title { get; set; }
        string Subject { get; set; }
        string Isbn { get; set; }
        bool CanLoan { get; set; }

        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}
