using DataAccess.EntityMaps.Wishlist;
using DataAccess.Queries.Wishlist;
using Moq;

namespace Case.Test.Repository.Extensions.WishlistBookRepository
{
    public static class WishlistAccessMockExensions
    {
        public static Mock<IWishlistAccess> WithInsertResult(this Mock<IWishlistAccess> mock, bool result)
        {
            mock.Setup(m => m.InsertOneAsync(It.IsAny<IWishlistMap>())).ReturnsAsync(result);
            return mock;
        }

        public static Mock<IWishlistAccess> WithUpdateResult(this Mock<IWishlistAccess> mock, bool result)
        {
            mock.Setup(m => m.UpdateOneAsync(It.IsAny<IWishlistMap>())).ReturnsAsync(result);
            return mock;
        }

        public static Mock<IWishlistAccess> WithInsertUpdateResult(this Mock<IWishlistAccess> mock, bool result)
        {
            mock.Setup(m => m.InsertOrUpdateAsync(It.IsAny<IWishlistMap>())).ReturnsAsync(result);
            return mock;
        }

        public static Mock<IWishlistAccess> WithDeleteResult(this Mock<IWishlistAccess> mock, bool result)
        {
            mock.Setup(m => m.DeleteOneAsync(It.IsAny<IWishlistMap>())).ReturnsAsync(result);
            return mock;
        }

        public static Mock<IWishlistAccess> WithDeleteWithIdResult(this Mock<IWishlistAccess> mock, bool result)
        {
            mock.Setup(m => m.DeleteWithIdAsync(It.IsAny<int>())).ReturnsAsync(result);
            return mock;
        }

        public static Mock<IWishlistAccess> WithSelectWithIdResult(this Mock<IWishlistAccess> mock, IWishlistMap bookMap)
        {
            mock.Setup(m => m.SelectWithIdAsync(It.IsAny<int>())).ReturnsAsync(bookMap);
            return mock;
        }
    }
}
