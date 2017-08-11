using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Core.Entity
{
    public class OrderLine
    {
        public virtual int OrderId { get; set; }
        public virtual int BookCopyId { get; set; }
        public virtual bool Returned { get; set; }
        public virtual bool Extended { get; set; }
    }
}
