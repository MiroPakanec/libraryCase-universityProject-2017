using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Mapper;
using Case.Core.Repository;
using Case.Test.Repository.Extensions;
using Case.Test.Repository.Extensions.BookCopyRepository;
using Case.Test.TestUtils;
using DataAccess.EntityMaps.Book;
using DataAccess.EntityMaps.BookCopy;
using DataAccess.Queries.Book;
using DataAccess.Queries.BookCopy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Case.Test.Repository
{
    [TestClass]
    public class BookCopyRepositoryTests
    {
        private BookCopyRepository _sut;
        private Mock<IBookCopyAccess> _access;
        private Mock<IMapper<BookCopy>> _mapper;

        [TestInitialize]
        public void SetUp()
        {
            _mapper = new Mock<IMapper<BookCopy>>();
            _access = new Mock<IBookCopyAccess>();
        }

        [TestCleanup]
        public void TearDown()
        {
            _sut = null;
            _mapper = null;
            _access = null;
        }

        [TestMethod]
        public async Task InsertAsyncTest_ValidBookCopy_HappyPath()
        {
            //arrange
            _mapper.WithMap(BookCopyMapTestUtils.GenerateValidBookCopyMap());
            _access.WithInsertResult(true);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.InsertAsync(BookCopyTestUtils.GenerateValidBookCopy());

            //assert
            Assert.IsTrue(result);
            _access.Verify(m => m.InsertOneAsync(It.IsAny<BookCopyMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Book copy cannot be null.")]
        public async Task InsertAsyncTest_NullBookCopy_ExceptionalCase()
        {
            //arrange
            _mapper.WithMap(BookCopyMapTestUtils.GenerateValidBookCopyMap());
            _access.WithInsertResult(true);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.InsertAsync(null);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.InsertOneAsync(It.IsAny<BookCopyMap>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Book copy map cannot be null.")]
        public async Task InsertAsyncTest_NullBookCopyMap_ExceptionalCase()
        {
            //arrange
            _mapper.WithMap(null);
            _access.WithInsertResult(true);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.InsertAsync(BookCopyTestUtils.GenerateValidBookCopy());

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.InsertOneAsync(It.IsAny<BookCopyMap>()), Times.Never);
        }

        [TestMethod]
        public async Task GetAsyncTest_ValidId_HappyPath()
        {
            //arrange            
            const int validId = 25;
            _access.WithGetByIdAsyncResult(BookCopyMapTestUtils.GenerateValidBookCopyMap() as IBookCopyMap);
            _mapper.WithUnmap(BookCopyTestUtils.GenerateValidBookCopy());

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetAsync(validId);

            //assert
            Assert.IsNotNull(result);
            _access.Verify(m => m.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid book copy id.")]
        public async Task GetAsyncTest_ZeroId_ExceptionalCase()
        {
            //arrange            
            const int invalidId = 0;
            _access.WithGetByIdAsyncResult(BookCopyMapTestUtils.GenerateValidBookCopyMap() as IBookCopyMap);
            _mapper.WithUnmap(BookCopyTestUtils.GenerateValidBookCopy());

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetAsync(invalidId);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid book copy id.")]
        public async Task GetAsyncTest_NegativeId_ExceptionalCase()
        {
            //arrange            
            const int invalidId = -10;
            _access.WithGetByIdAsyncResult(BookCopyMapTestUtils.GenerateValidBookCopyMap() as IBookCopyMap);
            _mapper.WithUnmap(BookCopyTestUtils.GenerateValidBookCopy());

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetAsync(invalidId);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public async Task UpdateAsyncTest_ValidBookCopy_HappyPath()
        {
            //arrange
            _mapper.WithMap(BookCopyMapTestUtils.GenerateValidBookCopyMap());
            _access.WithUpdateResult(true);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.UpdateAsync(BookCopyTestUtils.GenerateValidBookCopy());

            //assert
            Assert.IsTrue(result);
            _access.Verify(m => m.UpdateOneAsync(It.IsAny<BookCopyMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Book copy cannot be null.")]
        public async Task UpdateAsyncTest_NullBookCopy_ExceptionalCase()
        {
            //arrange
            _mapper.WithMap(BookCopyMapTestUtils.GenerateValidBookCopyMap());
            _access.WithUpdateResult(true);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.UpdateAsync(null);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.UpdateOneAsync(It.IsAny<BookCopyMap>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Book copy cannot be null.")]
        public async Task UpdateAsyncTest_NullBookCopyMap_ExceptionalCase()
        {
            //arrange
            _mapper.WithMap(null);
            _access.WithUpdateResult(true);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.UpdateAsync(BookCopyTestUtils.GenerateValidBookCopy());

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.UpdateOneAsync(It.IsAny<BookCopyMap>()), Times.Never);
        }

        [TestMethod]
        public async Task DeleteAsyncBookTest_ValidBookCopy_HappyPath()
        {
            //arrange
            _access.WithDeleteResult(true);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(BookCopyTestUtils.GenerateValidBookCopy());

            //assert
            Assert.IsTrue(result);
            _access.Verify(m => m.DeleteWithIdAsync(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Book copy cannot be null.")]
        public async Task DeleteAsyncBookTest_NullBookCopy_ExceptionalCase()
        {
            //arrange
            _access.WithDeleteResult(true);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(null);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.DeleteWithIdAsync(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid book copy id.")]
        public async Task DeleteAsyncBookTest_ZeroBookCopyId_ExceptionalCase()
        {
            //arrange
            var book = BookCopyTestUtils.GenerateValidBookCopy();
            book.Id = 0;

            _access.WithDeleteResult(true);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(book);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.DeleteWithIdAsync(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid book copy id.")]
        public async Task DeleteAsyncBookTest_NegativeBookCopyId_ExceptionalCase()
        {
            //arrange
            var book = BookCopyTestUtils.GenerateValidBookCopy();
            book.Id = -10;

            _access.WithDeleteResult(true);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(book);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.DeleteWithIdAsync(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public async Task DeleteAsyncBookTest_ValidBookCopyId_HappyPath()
        {
            //arrange
            _access.WithDeleteResult(true);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(BookCopyTestUtils.GenerateValidBookCopy().Id);

            //assert
            Assert.IsTrue(result);
            _access.Verify(m => m.DeleteWithIdAsync(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid book copy id.")]
        public async Task DeleteAsyncIdTest_ZeroBookCopyId_ExceptionalCase()
        {
            //arrange
            const int invalidId = 0;

            _access.WithDeleteResult(true);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(invalidId);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.DeleteWithIdAsync(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid book copy id.")]
        public async Task DeleteAsyncIdTest_NegativeBookCopyId_ExceptionalCase()
        {
            //arrange
            const int invalidId = -10;

            _access.WithDeleteResult(true);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(invalidId);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.DeleteWithIdAsync(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public async Task InsertOrUpdateAsyncTest_ValidBookCopy_HappyPath()
        {
            //arrange
            _mapper.WithMap(BookCopyMapTestUtils.GenerateValidBookCopyMap());
            _access.WithInsertOrUpdateResult(true);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.InsertOrUpdateAsync(BookCopyTestUtils.GenerateValidBookCopy());

            //assert
            Assert.IsTrue(result);
            _access.Verify(m => m.InsertOrUpdateAsync(It.IsAny<BookCopyMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Book copy cannot be null.")]
        public async Task InsertOrUpdateAsyncTest_NullBookCopy_ExceptionalCase()
        {
            //arrange
            _mapper.WithMap(BookCopyMapTestUtils.GenerateValidBookCopyMap());
            _access.WithInsertOrUpdateResult(true);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.InsertOrUpdateAsync(null);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.InsertOrUpdateAsync(It.IsAny<BookCopyMap>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Book copy map cannot be null.")]
        public async Task InsertOrUpdateAsyncTest_NullBookCopyMap_ExceptionalCase()
        {
            //arrange
            _mapper.WithMap(null);
            _access.WithInsertOrUpdateResult(true);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.InsertOrUpdateAsync(BookCopyTestUtils.GenerateValidBookCopy());

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.InsertOrUpdateAsync(It.IsAny<BookCopyMap>()), Times.Never);
        }

        [TestMethod]
        public async Task GetByIdAsync_ValidBookCopyId_HappyPath()
        {
            //arrange
            var validId = BookCopyTestUtils.GenerateValidBookCopy().Id;
            _access.WithGetByIdAsyncResult(BookCopyMapTestUtils.GenerateValidBookCopyMap() as IBookCopyMap);
            _mapper.WithUnmap(BookCopyTestUtils.GenerateValidBookCopy());

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetByIdAsync(validId);

            //assert
            Assert.IsNotNull(result);
            _access.Verify(m => m.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid book copy id.")]
        public async Task GetByIdAsync_ZeroBookCopyId_ExceptionalCase()
        {
            //arrange
            const int invalidId = 0;
            _access.WithGetByIdAsyncResult(BookCopyMapTestUtils.GenerateValidBookCopyMap() as IBookCopyMap);
            _mapper.WithUnmap(BookCopyTestUtils.GenerateValidBookCopy());

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetByIdAsync(invalidId);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid book copy id.")]
        public async Task GetByIdAsync_NegativeBookCopyId_ExceptionalCase()
        {
            //arrange
            const int invalidId = -10;
            _access.WithGetByIdAsyncResult(BookCopyMapTestUtils.GenerateValidBookCopyMap() as IBookCopyMap);
            _mapper.WithUnmap(BookCopyTestUtils.GenerateValidBookCopy());

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetByIdAsync(invalidId);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Book copy map cannot be null.")]
        public async Task GetByIdAsync_NullBookCopyMap_ExceptionalCase()
        {
            //arrange
            var validId = BookCopyTestUtils.GenerateValidBookCopy().Id;
            _access.WithGetByIdAsyncResult(null);
            _mapper.WithUnmap(BookCopyTestUtils.GenerateValidBookCopy());

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetByIdAsync(validId);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public async Task GetAllAsync_HappyPath()
        {
            //arrange
            _access.WithGetAllAsyncResult(BookCopyMapTestUtils.GenerateValidBookCopyMapEnumerable());
            _mapper.WithUnmap(BookCopyTestUtils.GenerateValidBookCopy());

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetAllAsync();

            //assert
            Assert.IsNotNull(result);
            _access.Verify(m => m.SelectAllAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetAllAsync_EmptyEnumerable_HappyPath()
        {
            //arrange
            _access.WithGetAllAsyncResult(new List<IBookCopyMap>());
            _mapper.WithUnmap(BookCopyTestUtils.GenerateValidBookCopy());

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetAllAsync();

            //assert
            Assert.IsNotNull(result);
            _access.Verify(m => m.SelectAllAsync(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Book copy maps cannot be null.")]
        public async Task GetAllAsync_NullEnumerable_ExceptionalCase()
        {
            //arrange
            _access.WithGetAllAsyncResult(null);
            _mapper.WithUnmap(BookCopyTestUtils.GenerateValidBookCopy());

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetAllAsync();

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.SelectAllAsync(), Times.Once);
        }

        [TestMethod]
        public async Task DeleteWithIsbnAsync_ValidIsbn_HappyPath()
        {
            //arrange
            var validIsbn = BookTestUtils.GenerateValidBook().Isbn;
            _access.WithDeleteWithIsbnResult(true);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteWithIsbnAsync(validIsbn);

            //assert
            Assert.IsTrue(result);
            _access.Verify(m => m.DeleteWithIsbnAsync(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Isbn is not valid.")]
        public async Task DeleteWithIsbnAsync_InvalidIsbn_ExceptionalCase()
        {
            //arrange
            var validIsbn = BookTestUtils.GenerateInvalidBook().Isbn;
            _access.WithDeleteWithIsbnResult(true);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteWithIsbnAsync(validIsbn);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.DeleteWithIsbnAsync(It.IsAny<string>()), Times.Never);
        }

        #region GetByIsbnAsync

        [TestMethod]
        public async Task GetWithIsbnAsync_ValidIsbn_HappyPath()
        {
            //arrange
            var validBooks = BookCopyMapTestUtils.GenerateValidBookCopyMapEnumerable();
            var isbn = BookTestUtils.GenerateValidBook().Isbn;
            _access.WithGetByIsbnAsyncResult(validBooks);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetByIsbnAsync(isbn);

            //assert
            _access.Verify(m => m.GetByIsbnAsync(It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Isbn is not valid.")]
        public async Task GetWithIsbnAsync_InvalidIsbn_Fails()
        {
            //arrange
            var invalidBooks = BookCopyMapTestUtils.GenerateValidBookCopyMapEnumerable();
            var isbn = BookTestUtils.GenerateInvalidBook().Isbn;
            _access.WithGetByIsbnAsyncResult(invalidBooks);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetByIsbnAsync(isbn);

            //assert
            _access.Verify(m => m.GetByIsbnAsync(It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Isbn is not valid.")]
        public async Task GetWithIsbnAsync_NullIsbn_Fails()
        {
            //arrange
            var invalidBooks = BookCopyMapTestUtils.GenerateValidBookCopyMapEnumerable();
            _access.WithGetByIsbnAsyncResult(invalidBooks);

            _sut = new BookCopyRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetByIsbnAsync(null);

            //assert
            _access.Verify(m => m.GetByIsbnAsync(It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }
        #endregion
    }
}
