using Case.Core.Entity;
using Case.Core.Repository.Interface;
using Moq;
using static Case.Test.TestUtils.Wishlist.WishlistBookTestUtils;

namespace Case.Test.Facade.Extensions
{
    public static class WishlistRepositoryMockExtensions
    {
        public static Mock<IWishlistRepository> WithValidInsert(this Mock<IWishlistRepository> repository)
        {
            repository.Setup(x => x.InsertAsync(It.IsAny<WishlistBook>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IWishlistRepository> WithValidDelete(this Mock<IWishlistRepository> repository)
        {
            repository.Setup(x => x.DeleteAsync(It.IsAny<WishlistBook>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IWishlistRepository> WithValidUpdate(this Mock<IWishlistRepository> repository)
        {
            repository.Setup(x => x.UpdateAsync(It.IsAny<WishlistBook>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IWishlistRepository> WithValidGet(this Mock<IWishlistRepository> repository)
        {
            repository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(GenerateValidWishlistBook());
            return repository;
        }

        public static Mock<IWishlistRepository> WithValidGetById(this Mock<IWishlistRepository> repository)
        {
            repository.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(GenerateValidWishlistBook());
            return repository;
        }

        public static Mock<IWishlistRepository> WithValidInserOrUpdate(this Mock<IWishlistRepository> repository)
        {
            repository.Setup(x => x.InsertOrUpdateAsync(It.IsAny<WishlistBook>())).ReturnsAsync(true);
            return repository;
        }
    }
}