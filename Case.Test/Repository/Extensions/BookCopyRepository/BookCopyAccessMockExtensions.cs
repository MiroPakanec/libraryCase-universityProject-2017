using System.Collections.Generic;
using DataAccess.EntityMaps.BookCopy;
using DataAccess.Queries.BookCopy;
using Moq;

namespace Case.Test.Repository.Extensions.BookCopyRepository
{
    public static class BookCopyAccessMockExtensions
    {
        public static Mock<IBookCopyAccess> WithInsertResult(this Mock<IBookCopyAccess> mock, bool result)
        {
            mock.Setup(m => m.InsertOneAsync(It.IsAny<IBookCopyMap>())).ReturnsAsync(result);
            return mock;
        }

        public static Mock<IBookCopyAccess> WithUpdateResult(this Mock<IBookCopyAccess> mock, bool result)
        {
            mock.Setup(m => m.UpdateOneAsync(It.IsAny<IBookCopyMap>())).ReturnsAsync(result);
            return mock;
        }

        public static Mock<IBookCopyAccess> WithInsertOrUpdateResult(this Mock<IBookCopyAccess> mock, bool result)
        {
            mock.Setup(m => m.InsertOrUpdateAsync(It.IsAny<IBookCopyMap>())).ReturnsAsync(result);
            return mock;
        }

        public static Mock<IBookCopyAccess> WithDeleteResult(this Mock<IBookCopyAccess> mock, bool result)
        {
            mock.Setup(m => m.DeleteWithIdAsync(It.IsAny<int>())).ReturnsAsync(result);
            return mock;
        }

        public static Mock<IBookCopyAccess> WithDeleteWithIsbnResult(this Mock<IBookCopyAccess> mock, bool result)
        {
            mock.Setup(m => m.DeleteWithIsbnAsync(It.IsAny<string>())).ReturnsAsync(result);
            return mock;
        }

        public static Mock<IBookCopyAccess> WithGetByIdAsyncResult(this Mock<IBookCopyAccess> mock, IBookCopyMap bookCopyMap)
        {
            mock.Setup(m => m.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(bookCopyMap);
            return mock;
        }

        public static Mock<IBookCopyAccess> WithGetAllAsyncResult(this Mock<IBookCopyAccess> mock, IEnumerable<IBookCopyMap> bookCopyMap)
        {
            mock.Setup(m => m.SelectAllAsync()).ReturnsAsync(bookCopyMap);
            return mock;
        }

        public static Mock<IBookCopyAccess> WithGetByIsbnAsyncResult(this Mock<IBookCopyAccess> mock,
            IEnumerable<IBookCopyMap> bookCopyMap)
        {
            mock.Setup(m => m.GetByIsbnAsync(It.IsAny<string>())).ReturnsAsync(bookCopyMap);
            return mock;
        }
    }
}
