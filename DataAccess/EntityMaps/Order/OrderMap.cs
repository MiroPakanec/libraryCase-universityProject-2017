using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityMaps.Order
{
    public class OrderMap : IOrderMap
    {
        public int Id { get; set; }
        public string MemberSsn { get; set; }
        public DateTime Created { get; set; }
    }
}
