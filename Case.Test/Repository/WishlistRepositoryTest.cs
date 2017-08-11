using System;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Mapper;
using Case.Core.Repository;
using Case.Test.TestUtils;
using DataAccess.EntityMaps.Wishlist;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DataAccess.Queries.Wishlist;
using Case.Test.TestUtils.Wishlist;
using Case.Test.Repository.Extensions.WishlistBookRepository;

namespace Case.Test.Repository
{
    [TestClass]
    public class WishlistRepositoryTest
    {
        private WishlistRepository _sut;
        private Mock<IWishlistAccess> _access;
        private Mock<IMapper<WishlistBook>> _mapper;

        [TestInitialize]
        public void SetUp()
        {
            _mapper = new Mock<IMapper<WishlistBook>>();
            _access = new Mock<IWishlistAccess>();
        }

        [TestCleanup]
        public void TearDown()
        {
            _sut = null;
            _mapper = null;
            _access = null;
        }

        #region InsertOneAsync

        [TestMethod]
        public async Task InsertOneAsyncTest_ValidWishlistBook_Passes()
        {
            //arrange
            _mapper.WithMap(WishlistMapTestUtils.GenerateValidWishlistBookMap());
            _access.WithInsertResult(true);

            _sut = new WishlistRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.InsertAsync(WishlistBookTestUtils.GenerateValidWishlistBook());

            //assert
            Assert.IsTrue(result);
            _access.Verify(m => m.InsertOneAsync(It.IsAny<WishlistMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Wishlist book cannot be null.")]
        public async Task InsertOneAsyncTest_NullWishlistBook_ThrowsNullReferenceException()
        {
            //arrange
            _mapper.WithMap(WishlistMapTestUtils.GenerateValidWishlistBookMap());
            _access.WithInsertResult(true);

            _sut = new WishlistRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.InsertAsync(null);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.InsertOneAsync(It.IsAny<WishlistMap>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Wishlist book map cannot be null.")]
        public async Task InsertOneAsyncTest_NullWishlistBookMap_ThrowsNullReferenceException()
        {
            //arrange
            _mapper.WithMap(null);
            _access.WithInsertResult(true);

            _sut = new WishlistRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.InsertAsync(WishlistBookTestUtils.GenerateInvalidWishlistBook());

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.InsertOneAsync(It.IsAny<WishlistMap>()), Times.Never);
        }

        #endregion

        #region GetAsyncTest

        [TestMethod]
        public async Task GetAsyncTest_Passes()
        {
            //arrange
            _sut = new WishlistRepository(_access.Object, _mapper.Object);

            //act
            //var result = await _sut.GetAsync(It.IsAny<int>());
            var result = await _sut.GetAsync(1);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.SelectWithIdAsync(It.IsAny<int>()), Times.Once);
        }

        #endregion

        #region UpdateAsyncTest

        [TestMethod]
        public async Task UpdateAsyncTest_ValidWishlistBook_HappyPath()
        {
            //arrange
            _mapper.WithMap(BookMapTestUtils.GenerateValidBookMap());
            _access.WithUpdateResult(true);

            _sut = new WishlistRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.UpdateAsync(WishlistBookTestUtils.GenerateValidWishlistBook());

            //assert
            Assert.IsTrue(result);
            _access.Verify(m => m.UpdateOneAsync(It.IsAny<WishlistMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Wishlist book cannot be null.")]
        public async Task UpdateAsyncTest_NullWishlistBook_ThrowsNullReferenceException()
        {
            //arrange
            _mapper.WithMap(BookMapTestUtils.GenerateValidBookMap());
            _access.WithUpdateResult(true);

            _sut = new WishlistRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.UpdateAsync(null);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.UpdateOneAsync(It.IsAny<WishlistMap>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Book map cannot be null.")]
        public async Task UpdateAsyncTest_NullWishlistBookMap_ThrowsNullReferenceException()
        {
            //arrange
            _mapper.WithMap(null);
            _access.WithUpdateResult(true);

            _sut = new WishlistRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.UpdateAsync(WishlistBookTestUtils.GenerateValidWishlistBook());

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.UpdateOneAsync(It.IsAny<WishlistMap>()), Times.Never);
        }

        #endregion

        #region DeleteAsyncTest

        [TestMethod]
        public async Task DeleteAsyncTest_ValidWishlistBook_HappyPath()
        {
            //arrange
            _access.WithDeleteResult(true);
            _mapper.WithMap(BookMapTestUtils.GenerateValidBookMap());

            _sut = new WishlistRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(WishlistBookTestUtils.GenerateValidWishlistBook());

            //assert
            Assert.IsTrue(result);
            _access.Verify(m => m.DeleteOneAsync(It.IsAny<WishlistMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Wishlist book cannot be null.")]
        public async Task DeleteAsyncTest_NullWishlistBook_ThrowsNullReferenceException()
        {
            //arrange
            _access.WithDeleteWithIdResult(true);

            _sut = new WishlistRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(null);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.DeleteOneAsync(It.IsAny<WishlistMap>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "WishlistBookMap cannot be null")]
        public async Task DeleteAsyncTest_NegativeIdWishlistBook_ThrowsException()
        {
            //arrange
            var book = WishlistBookTestUtils.GenerateValidWishlistBook();
            book.Id = -4;

            _access.WithDeleteWithIdResult(true);
            _sut = new WishlistRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(book);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.DeleteWithIdAsync(It.IsAny<int>()), Times.Never);
        }

        #endregion

        #region InsertOrUpdateAsyncTest

        [TestMethod]
        public async Task InsertOrUpdateAsyncTest_ValidWishlistBook_HappyPath()
        {
            //arrange
            _mapper.WithMap(WishlistMapTestUtils.GenerateValidWishlistBookMap());
            _access.WithInsertUpdateResult(true);

            _sut = new WishlistRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.InsertOrUpdateAsync(WishlistBookTestUtils.GenerateValidWishlistBook());

            //assert
            Assert.IsTrue(result);
            _access.Verify(m => m.InsertOrUpdateAsync(It.IsAny<WishlistMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Wishlist book cannot be null.")]
        public async Task InsertOrUpdateAsyncTest_NullWishlistBook_ThrowsNullReferenceException()
        {
            //arrange
            _mapper.WithMap(BookMapTestUtils.GenerateValidBookMap());
            _access.WithInsertUpdateResult(true);

            _sut = new WishlistRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.InsertOrUpdateAsync(null);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.InsertOrUpdateAsync(It.IsAny<WishlistMap>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Wishlist book map cannot be null.")]
        public async Task InsertOrUpdateAsyncTest_NullWishlistBookMap_ThrowsNullReferenceException()
        {
            //arrange
            _mapper.WithMap(null);
            _access.WithInsertUpdateResult(true);

            _sut = new WishlistRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.InsertOrUpdateAsync(WishlistBookTestUtils.GenerateValidWishlistBook());

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.InsertOrUpdateAsync(It.IsAny<WishlistMap>()), Times.Never);
        }

        #endregion

        #region DeleteWithIdAsyncTest

        [TestMethod]
        public async Task DeleteWithIdAsyncTest_ValidId_HappyPath()
        {
            //arrange
            _access.WithDeleteWithIdResult(true);

            _sut = new WishlistRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(WishlistBookTestUtils.GenerateValidWishlistBook().Id);

            //assert
            Assert.IsTrue(result);
            _access.Verify(m => m.DeleteWithIdAsync(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Wishlist book id is not valid.")]
        public async Task DeleteWithIdAsyncTest_NegativeId_ThrowsException()
        {
            //arrange
            var book = WishlistBookTestUtils.GenerateValidWishlistBook();
            book.Id = -3;

            _access.WithInsertUpdateResult(true);

            _sut = new WishlistRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(book.Id);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.DeleteWithIdAsync(It.IsAny<int>()), Times.Never);
        }

        #endregion
    }
}