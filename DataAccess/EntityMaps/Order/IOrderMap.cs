using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityMaps.Order
{
    public interface IOrderMap : IMap
    {
        int Id { get; set; }
        string MemberSsn { get; set; }
        DateTime Created { get; set; }
    }
}
