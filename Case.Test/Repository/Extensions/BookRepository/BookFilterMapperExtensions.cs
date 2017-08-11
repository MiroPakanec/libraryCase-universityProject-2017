using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Mapper;
using Case.Core.Model;
using DataAccess.EntityMaps;
using Moq;

namespace Case.Test.Repository.Extensions.BookRepository
{
    internal static class BookFilterMapperExtensions
    {
        internal static Mock<IMapper<BookCatalogFilter>> WithMap(this Mock<IMapper<BookCatalogFilter>> mock,
            IMap filterMap)
        {
            mock.Setup(m => m.Map(It.IsAny<BookCatalogFilter>())).Returns(filterMap);
            return mock;
        }
    }
}
