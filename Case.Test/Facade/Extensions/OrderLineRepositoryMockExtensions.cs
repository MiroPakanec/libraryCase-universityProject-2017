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
    public static class OrderLineRepositoryMockExtensions
    {
        public static Mock<IOrderLineRepository> WithValidInsert(this Mock<IOrderLineRepository> repository)
        {
            repository.Setup(x => x.InsertAsync(It.IsAny<OrderLine>())).ReturnsAsync(true);
            return repository;
        }
    }
}
