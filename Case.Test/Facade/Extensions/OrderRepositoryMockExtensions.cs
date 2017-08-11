using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Repository.Interface;
using Moq;

namespace Case.Test.Facade.Extensions
{
    public static class OrderRepositoryMockExtensions
    {
        public static Mock<IOrderRepository> WithValidInsertWithIdReturn(this Mock<IOrderRepository> repository)
        {
            repository.Setup(x => x.InsertAsyncWithIdReturn(It.IsAny<Order>())).ReturnsAsync(42);
            return repository;
        }
    }
}
