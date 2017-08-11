using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Book;

namespace Case.Test.TestUtils
{
    internal static class BookMapTestUtils
    {
        internal static IMap GenerateValidBookMap()
        {
            return new BookMap()
            {
                Author = "J.K. Rowling",
                Binding = "2",
                Description = "Harry Potter",
                Edition = "1st",
                ISBN = "0439139600",
                Language = "English",
                Loanable = true,
                Subject = "Fiction",
                Title = "Harry Potter And The Goblet Of Fire"
            };
        }
    }
}
