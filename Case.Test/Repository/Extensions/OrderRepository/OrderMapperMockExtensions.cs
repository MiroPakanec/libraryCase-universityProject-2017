using Case.Core.Entity;
using Case.Core.Mapper;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Order;
using Moq;

namespace Case.Test.Repository.Extensions.OrderRepository
{
    public static class OrderMapperMockExtensions
    {
        public static Mock<IMapper<Order>> WithMap(this Mock<IMapper<Order>> mock, IMap orderMap)
        {
            mock.Setup(m => m.Map(It.IsAny<Order>())).Returns(orderMap);
            return mock;
        }

        public static Mock<IMapper<Order>> WithUnmap(this Mock<IMapper<Order>> mock, Order order)
        {
            mock.Setup(m => m.Unmap(It.IsAny<OrderMap>())).Returns(order);
            return mock;
        }
    }
}