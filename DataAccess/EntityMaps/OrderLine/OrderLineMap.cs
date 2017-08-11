using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityMaps.OrderLine
{
    public class OrderLineMap : IOrderLineMap
    {
        public int OrderId { get; set; }
        public int BookCopyId { get; set; }
        public bool Returned { get; set; }
        public bool Extended { get; set; }
    }
}
