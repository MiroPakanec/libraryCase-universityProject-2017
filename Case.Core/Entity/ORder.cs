using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Core.Entity
{
    public class Order
    {
        public virtual int Id { get; set; }
        public virtual string MemberId { get; set; } // It's actually ssn
        public virtual DateTime Created { get; set; }
    }
}
