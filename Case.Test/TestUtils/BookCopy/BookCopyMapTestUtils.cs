using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Book;
using DataAccess.EntityMaps.BookCopy;

namespace Case.Test.TestUtils
{
    internal class BookCopyMapTestUtils
    {
        internal static IMap GenerateValidBookCopyMap()
        {
            return new BookCopyMap()
            {
                Id = new Random().Next(),
                BookIsbn = "978-3-16-148410-0",
                Available = true
            };
        }

        internal static IEnumerable<IBookCopyMap> GenerateValidBookCopyMapEnumerable()
        {
            return new List<IBookCopyMap>
            {
                new BookCopyMap()
                {
                    Id = new Random().Next(),
                    BookIsbn = "978-3-16-148410-0",
                    Available = true
                },

                new BookCopyMap()
                {
                    Id = new Random().Next(),
                    BookIsbn = "978-3-16-148410-2",
                    Available = true
                },

                new BookCopyMap()
                {
                    Id = new Random().Next(),
                    BookIsbn = "978-3-16-148410-1",
                    Available = false
                }
            };
        }
    }
}
