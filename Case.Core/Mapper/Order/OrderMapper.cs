using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Order;

namespace Case.Core.Mapper.Order
{
    public class OrderMapper : IMapper<Entity.Order>
    {
        public IMap Map(Entity.Order item)
        {
            return new OrderMap()
            {
                Id = item.Id,
                Created = item.Created,
                MemberSsn = item.MemberId
            };
        }

        public Entity.Order Unmap(IMap map)
        {
            var orderMap = map as IOrderMap;
            if (orderMap == null)
            {
                throw new Exception("Unable to unmap Order");
            }
            return new Entity.Order()
            {
                Id  = orderMap.Id,
                Created = orderMap.Created,
                MemberId = orderMap.MemberSsn
            };
        }
    }
}
