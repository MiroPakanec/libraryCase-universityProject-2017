using Case.Core.Facade;
using Case.Core.Repository.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using Case.Test.Facade.Extensions;
using static Case.Test.TestUtils.Wishlist.WishlistBookTestUtils;
using Case.Core.Entity;

namespace Case.Test.Facade
{
    [TestClass]
    public class WishlistFacadeTest
    {
        private WishlistFacade _sut;
        private IWishlistRepository _wishlistRepository;
        private Mock<IWishlistRepository> _wishlistRepositoryMock;

        [TestInitialize]
        public void Setup()
        {
            _wishlistRepositoryMock = new Mock<IWishlistRepository>();
            _wishlistRepository = _wishlistRepositoryMock.Object;
            _sut = new WishlistFacade(_wishlistRepository);
        }

        #region AddBookToWishlistAsync

        [TestMethod]
        public async Task AddBookToWishlistAsyncTest_ValidWishlistBook_Passes()
        {
            //arrange
            _wishlistRepositoryMock.WithValidInsert();
            var wishlistBook = GenerateValidWishlistBook();

            //act
            var result = await _sut.AddBookToWishlistAsync(wishlistBook);

            //assert
            _wishlistRepositoryMock.Verify(x => x.InsertAsync(It.IsAny<WishlistBook>()), Times.Once);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Wishlist book cannot be null.")]
        public async Task AddBookToWishlistAsyncTest_NullWishlistBook_ThrowsNullReferenceException()
        {
            //arrange
            _wishlistRepositoryMock.WithValidInsert();
            WishlistBook wishlistBook = null;

            //act 
            var result = await _sut.AddBookToWishlistAsync(wishlistBook);

            //assert
            _wishlistRepositoryMock.Verify(x => x.InsertAsync(It.IsAny<WishlistBook>()), Times.Never);
            Assert.IsFalse(result);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception), "Wishlist book cannot be invalid.")]
        public async Task AddBookToWishlistAsyncTest_InvalidWishlistBook_Fails()
        {
            //arrange
            _wishlistRepositoryMock.WithValidInsert();
            var wishlistBook = GenerateInvalidWishlistBook();

            //act
            var result = await _sut.AddBookToWishlistAsync(wishlistBook);

            //assert
            _wishlistRepositoryMock.Verify(x => x.InsertAsync(It.IsAny<WishlistBook>()), Times.Never);
            Assert.IsFalse(result);
        }

        #endregion

        #region GetWishlistBookAsync
        [TestMethod]
        public async Task GetWishlistBookAsyncTest_ValidWishlistBookId_Passes()
        {
            //arrange
            _wishlistRepositoryMock.WithValidGet();
            var wishlistBook = GenerateValidWishlistBook();

            //act
            var result = await _sut.GetWishlistBookAsync(wishlistBook.Id);

            //assert
            _wishlistRepositoryMock.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Wishlist book Id cannot be invalid.")]
        public async Task GetWishlistBookAsyncTest_InvalidWishlistBookId_Fails()
        {
            //arrange
            _wishlistRepositoryMock.WithValidGet();
            var wishlistBook = GenerateInvalidWishlistBook();

            //act
            var result = await _sut.GetWishlistBookAsync(wishlistBook.Id);

            //assert
            _wishlistRepositoryMock.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Never);
            Assert.IsNull(result);
        }

        #endregion

        #region UpdateWishlistBookAsync

        [TestMethod]
        public async Task UpdateWishlistBookAsyncTest_ValidWishlistBook_Passes()
        {
            //arrange
            _wishlistRepositoryMock.WithValidUpdate();
            var wishlistBook = GenerateValidWishlistBook();

            //act
            var result = await _sut.UpdateWishlistBookAsync(wishlistBook);

            //assert
            _wishlistRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<WishlistBook>()), Times.Once);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Wishlist book id cannot be invalid.")]
        public async Task UpdateWishlistBookAsyncTest_InvalidWishlistBook_Fails()
        {
            //arrange
            _wishlistRepositoryMock.WithValidUpdate();
            var wishlistBook = GenerateInvalidWishlistBook();

            //act
            var result = await _sut.UpdateWishlistBookAsync(wishlistBook);

            //assert
            _wishlistRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<WishlistBook>()), Times.Once);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Wishlist book cannot be invalid.")]
        public async Task UpdateWishlistBookAsyncTest_NullWishlistBook_Fails()
        {
            //arrange
            _wishlistRepositoryMock.WithValidUpdate();

            //act
            var result = await _sut.UpdateWishlistBookAsync(null);

            //assert
            _wishlistRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<WishlistBook>()), Times.Never);
            Assert.IsFalse(result);
        }

        #endregion

        #region RemoveWishlistBookAsyncByWishlistBook

        [TestMethod]
        public async Task RemoveWishlistBookByWishlistBookAsyncTest_ValidBook_Passes()
        {
            //arrange
            _wishlistRepositoryMock.WithValidDelete();
            var wishlistBook = GenerateValidWishlistBook();

            //act
            var result = await _sut.RemoveWishlistBookAsync(wishlistBook);

            //assert
            _wishlistRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<WishlistBook>()), Times.Once);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Wishlist book cannot be invalid.")]
        public async Task RemoveWishlistBookByWishlistBookAsyncTest_InvalidBook_Fails()
        {
            //arrange
            _wishlistRepositoryMock.WithValidDelete();
            var wishlistBook = GenerateInvalidWishlistBook();

            //act
            var result = await _sut.RemoveWishlistBookAsync(wishlistBook);

            //assert
            _wishlistRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<WishlistBook>()), Times.Never);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Wishlist book cannot be invalid.")]
        public async Task RemoveWishlistBookByWishlistBookAsyncTest_NullBook_Fails()
        {
            //arrange
            _wishlistRepositoryMock.WithValidDelete();

            //act
            var result = await _sut.RemoveWishlistBookAsync(null);

            //assert
            _wishlistRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<WishlistBook>()), Times.Never);
            Assert.IsFalse(result);
        }
        #endregion

        #region RemoveWishlistBookAsyncById

        [TestMethod]
        public async Task RemoveWishlistBookAsyncByWishlistBookIdAsyncTest_ValidBook_Passes()
        {
            //arrange
            _wishlistRepositoryMock.WithValidDelete();
            var wishlistBook = GenerateValidWishlistBook();

            //act
            var result = await _sut.RemoveWishlistBookAsync(wishlistBook.Id);

            //assert
            _wishlistRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Once);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Wishlist book Id cannot be invalid.")]
        public async Task RemoveWishlistBookAsyncByWishlistBookIdAsyncTest_InvalidBook_Fails()
        {
            //arrange
            _wishlistRepositoryMock.WithValidDelete();
            var wishlistBook = GenerateInvalidWishlistBook();

            //act
            var result = await _sut.RemoveWishlistBookAsync(wishlistBook.Id);

            //assert
            _wishlistRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Never);
            Assert.IsFalse(result);
        }

        #endregion

        #region FindWishlistBookById

        [TestMethod]
        public async Task FindWishlistBookByIdAsyncTest_ValidBook_Passes()
        {
            //arrange
            _wishlistRepositoryMock.WithValidGetById();
            var wishlistBook = GenerateValidWishlistBook();

            //act
            var result = await _sut.FindWishlistBookById(wishlistBook.Id);

            //assert
            _wishlistRepositoryMock.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Wishlist book Id cannot be invalid.")]
        public async Task FindWishlistBookByIdAsyncTest_InvalidBook_Fails()
        {
            //arrange
            _wishlistRepositoryMock.WithValidGet();
            var wishlistBook = GenerateInvalidWishlistBook();

            //act
            var result = await _sut.FindWishlistBookById(wishlistBook.Id);

            //assert
            _wishlistRepositoryMock.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Never);
            Assert.IsNull(result);
        }

        #endregion

    }
}
