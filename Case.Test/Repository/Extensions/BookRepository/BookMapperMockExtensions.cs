using Case.Core.Entity;
using Case.Core.Mapper;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Book;
using Moq;

namespace Case.Test.Repository.Extensions.BookRepository
{
    public static class BookMapperMockExtensions
    {
        public static Mock<IMapper<Book>> WithMap(this Mock<IMapper<Book>> mock, IMap bookMap)
        {
            mock.Setup(m => m.Map(It.IsAny<Book>())).Returns(bookMap);
            return mock;
        }

        public static Mock<IMapper<Book>> WithUnmap(this Mock<IMapper<Book>> mock, Book book)
        {
            mock.Setup(m => m.Unmap(It.IsAny<BookMap>())).Returns(book);
            return mock;
        }
    }
}
