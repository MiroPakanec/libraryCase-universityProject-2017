using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Core.Exceptions
{
    public class BookRentException : Exception
    {
        public BookRentException(string message) : base(message) { }
    }
}
