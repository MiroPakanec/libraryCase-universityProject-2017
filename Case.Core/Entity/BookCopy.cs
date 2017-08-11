using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Core.Entity
{
    public class BookCopy
    {
        public virtual int Id { get; set; }
        public virtual string Isbn { get; set; }
        public virtual bool Available { get; set; }
    }
}
