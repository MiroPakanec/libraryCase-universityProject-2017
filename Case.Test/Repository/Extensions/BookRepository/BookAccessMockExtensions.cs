using System.Collections.Generic;
using DataAccess.EntityMaps.Book;
using DataAccess.Queries.Book;
using Moq;

namespace Case.Test.Repository.Extensions.BookRepository
{
    public static class BookAccessMockExtensions
    {
        public static Mock<IBookAccess> WithInsertResult(this Mock<IBookAccess> mock, bool result)
        {
            mock.Setup(m => m.InsertOneAsync(It.IsAny<IBookMap>())).ReturnsAsync(result);
            return mock;
        }

        public static Mock<IBookAccess> WithUpdateResult(this Mock<IBookAccess> mock, bool result)
        {
            mock.Setup(m => m.UpdateOneAsync(It.IsAny<IBookMap>())).ReturnsAsync(result);
            return mock;
        }

        public static Mock<IBookAccess> WithInsertUpdateResult(this Mock<IBookAccess> mock, bool result)
        {
            mock.Setup(m => m.InsertOrUpdateAsync(It.IsAny<IBookMap>())).ReturnsAsync(result);
            return mock;
        }
       
        public static Mock<IBookAccess> WithDeleteResult(this Mock<IBookAccess> mock, bool result)
        {
            mock.Setup(m => m.DeleteOneAsync(It.IsAny<BookMap>())).ReturnsAsync(result);
            return mock;
        }

        public static Mock<IBookAccess> WithDeleteWithIdResult(this Mock<IBookAccess> mock, bool result)
        {
            mock.Setup(m => m.DeleteWithIdAsync(It.IsAny<string>())).ReturnsAsync(result);
            return mock;
        }

        public static Mock<IBookAccess> WithSelectWithIdResult(this Mock<IBookAccess> mock, IBookMap bookMap)
        {
            mock.Setup(m => m.SelectWithIdAsync(It.IsAny<string>())).ReturnsAsync(bookMap);
            return mock;
        }

        public static Mock<IBookAccess> WithSelectDescriptionWithIdResult(this Mock<IBookAccess> mock, string bookDescription)
        {
            mock.Setup(m => m.SelectDescriptionWithId(It.IsAny<string>())).ReturnsAsync(bookDescription);
            return mock;
        }

        public static Mock<IBookAccess> WithValidCatalogueSearch(this Mock<IBookAccess> mock,
            IEnumerable<CatalogueItemMap> result)
        {
            mock.Setup(m => m.SearchCatalogue(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ICatalogueFilterMap>()))
                .ReturnsAsync(result);
            return mock;
        }

        public static Mock<IBookAccess> WithValidGetBookCount(this Mock<IBookAccess> mock)
        {
            mock.Setup(m => m.Count()).ReturnsAsync(It.IsAny<int>());
            return mock;
        }
    }
}
