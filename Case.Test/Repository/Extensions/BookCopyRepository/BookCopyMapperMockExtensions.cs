using Case.Core.Entity;
using Case.Core.Mapper;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.BookCopy;
using Moq;

namespace Case.Test.Repository.Extensions.BookCopyRepository
{
    public static class BookCopyMapperMockExtensions
    {
        public static Mock<IMapper<BookCopy>> WithMap(this Mock<IMapper<BookCopy>> mock, IMap bookCopyMap)
        {
            mock.Setup(m => m.Map(It.IsAny<BookCopy>())).Returns(bookCopyMap);
            return mock;
        }

        public static Mock<IMapper<BookCopy>> WithUnmap(this Mock<IMapper<BookCopy>> mock, BookCopy bookCopy)
        {
            mock.Setup(m => m.Unmap(It.IsAny<BookCopyMap>())).Returns(bookCopy);
            return mock;
        }
    }
}
