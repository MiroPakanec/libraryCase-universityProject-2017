using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityMaps.BookCopy
{
    public interface IBookCopyMap : IMap
    {
        int Id { get; set; }
        string BookIsbn { get; set; }
        bool Available { get; set; }
    }
}
