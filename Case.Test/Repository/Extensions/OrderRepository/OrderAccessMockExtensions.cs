using DataAccess.EntityMaps.Order;
using DataAccess.Queries.Order;
using Moq;

namespace Case.Test.Repository.Extensions.OrderRepository
{
    public static class OrderAccessMockExtensions
    {
        public static Mock<IOrderAccess> WithInsertWithIdReturn(this Mock<IOrderAccess> repository)
        {
            repository.Setup(x => x.InsertWithIdReturn(It.IsAny<OrderMap>())).ReturnsAsync(42);
            return repository;
        }

        public static Mock<IOrderAccess> WithInsert(this Mock<IOrderAccess> repository, bool result)
        {
            repository.Setup(x => x.InsertOneAsync(It.IsAny<OrderMap>())).ReturnsAsync(result);
            return repository;
        }
    }
}