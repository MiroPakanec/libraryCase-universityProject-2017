using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityMaps.OrderLine
{
    public interface IOrderLineMap : IMap
    {
        int OrderId { get; set; }
        int BookCopyId { get; set; }
        bool Returned { get; set; }
        bool Extended { get; set; }
    }
}
