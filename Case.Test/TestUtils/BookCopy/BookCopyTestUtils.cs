using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;

namespace Case.Test.TestUtils
{
    public static class BookCopyTestUtils
    {
        public static BookCopy GenerateValidBookCopy()
        {
            return new BookCopy()
            {
                Id = new Random().Next(),
                Isbn = "978-3-16-148410-0",
                Available = true
            };
        }

        public static BookCopy GenerateInvalidBookCopy()
        {
            return new BookCopy()
            {
                Id = new Random().Next(),
                Isbn = "Isbn",
                Available = true
            };
        }

        public static string GenerateValidIsbn13()
        {
            return "978-3-16-148410-0";
        }

        public static string GenerateInvalidIsbn13()
        {
            return "111-1-11-111111-1";
        }

        public static string GenerateValidIsbn10()
        {
            return "0-19-852663-6";
        }

        public static string GenerateInvalidIsbn10()
        {
            return "99921-58-10-6";
        }
    }
}
