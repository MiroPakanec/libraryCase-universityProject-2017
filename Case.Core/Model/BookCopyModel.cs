using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Core.Model
{
    public class BookCopyModel
    {
        public int Id { get; set; }
        public string Isbn { get; set; }
        public bool Available { get; set; }
    }
}
