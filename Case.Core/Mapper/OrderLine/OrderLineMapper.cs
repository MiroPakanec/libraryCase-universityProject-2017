using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.OrderLine;

namespace Case.Core.Mapper.OrderLine
{
    public class OrderLineMapper : IMapper<Entity.OrderLine>
    {
        public IMap Map(Entity.OrderLine item)
        {
            return new OrderLineMap()
            {
                BookCopyId = item.BookCopyId,
                Extended = item.Extended,
                OrderId = item.OrderId,
                Returned = item.Returned
            };
        }

        public Entity.OrderLine Unmap(IMap map)
        {
            var orderLineMap = map as IOrderLineMap;
            if (orderLineMap == null)
            {
                throw new Exception("Unable to unmap orderline");
            }
            return new Entity.OrderLine()
            {
                BookCopyId = orderLineMap.BookCopyId,
                Extended = orderLineMap.Extended,
                OrderId = orderLineMap.OrderId,
                Returned = orderLineMap.Returned
            };
        }
    }
}
