using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityMaps.BookCopy
{
    public class BookCopyMap : IBookCopyMap
    {
        public int Id { get; set; }
        public string BookIsbn { get; set; }
        public bool Available { get; set; }
    }
}
