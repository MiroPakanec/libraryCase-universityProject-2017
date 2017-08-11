using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Mapper;
using DataAccess.EntityMaps.Book;
using Moq;

namespace Case.Test.Repository.Extensions.BookRepository
{
    internal static class BookCatalogueItemMapperExtensions
    {
        internal static Mock<IDualMapper<Book, CatalogueItemMap>> WithUnMap(
            this Mock<IDualMapper<Book, CatalogueItemMap>> mock, Book book)
        {
            mock.Setup(m => m.Unmap(It.IsAny<CatalogueItemMap>())).Returns(book);
            return mock;
        }
    }
}
