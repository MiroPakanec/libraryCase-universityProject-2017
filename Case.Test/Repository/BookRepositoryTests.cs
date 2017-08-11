using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Mapper;
using Case.Core.Model;
using Case.Core.Repository;
using Case.Test.Repository.Extensions.BookRepository;
using Case.Test.TestUtils;
using Case.Test.TestUtils.Book;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Book;
using DataAccess.Queries.Book;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Case.Test.Repository
{
    [TestClass]
    public class BookRepositoryTests
    {
        private BookRepository _sut;
        private Mock<IBookAccess> _access;
        private Mock<IMapper<Book>> _mapper;
        private Mock<IMapper<BookCatalogFilter>> _filterMapper;
        private Mock<IDualMapper<Book, CatalogueItemMap>> _catalogueMapper;

        [TestInitialize]
        public void SetUp()
        {
            _access = new Mock<IBookAccess>();

            _mapper = new Mock<IMapper<Book>>();
            _filterMapper = new Mock<IMapper<BookCatalogFilter>>();
            _catalogueMapper = new Mock<IDualMapper<Book, CatalogueItemMap>>();
        }

        [TestCleanup]
        public void TearDown()
        {
            _sut = null;
            _mapper = null;
            _access = null;
        }

        [TestMethod]
        public async Task InsertOneAsyncTest_ValidBook_HappyPath()
        {
            //arrange
            _mapper.WithMap(BookMapTestUtils.GenerateValidBookMap());
            _access.WithInsertResult(true);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.InsertAsync(BookTestUtils.GenerateValidBook());

            //assert
            Assert.IsTrue(result);
            _access.Verify(m => m.InsertOneAsync(It.IsAny<BookMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Book cannot be null.")]
        public async Task InsertOneAsyncTest_NullBook_ThrowsNullReferenceException()
        {
            //arrange
            _mapper.WithMap(BookMapTestUtils.GenerateValidBookMap());
            _access.WithInsertResult(true);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.InsertAsync(null);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.InsertOneAsync(It.IsAny<BookMap>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Book map cannot be null.")]
        public async Task InsertOneAsyncTest_NullBookMap_ThrowsNullReferenceException()
        {
            //arrange
            _mapper.WithMap(null);
            _access.WithInsertResult(true);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.InsertAsync(BookTestUtils.GenerateInvalidBook());

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.InsertOneAsync(It.IsAny<BookMap>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Book cannot be searched by id.")]
        public async Task GetAsyncTest_ThrowsException()
        {
            //arrange
            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetAsync(It.IsAny<int>());

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.SelectWithIdAsync(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task UpdateAsyncTest_ValidBook_HappyPath()
        {
            //arrange
            _mapper.WithMap(BookMapTestUtils.GenerateValidBookMap());
            _access.WithUpdateResult(true);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.UpdateAsync(BookTestUtils.GenerateValidBook());

            //assert
            Assert.IsTrue(result);
            _access.Verify(m => m.UpdateOneAsync(It.IsAny<BookMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Book cannot be null.")]
        public async Task UpdateAsyncTest_NullBook_ThrowsNullReferenceException()
        {
            //arrange
            _mapper.WithMap(BookMapTestUtils.GenerateValidBookMap());
            _access.WithUpdateResult(true);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.UpdateAsync(null);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.UpdateOneAsync(It.IsAny<BookMap>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Book map cannot be null.")]
        public async Task UpdateAsyncTest_NullBookMap_ThrowsNullReferenceException()
        {
            //arrange
            _mapper.WithMap(null);
            _access.WithUpdateResult(true);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.UpdateAsync(BookTestUtils.GenerateValidBook());

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.UpdateOneAsync(It.IsAny<BookMap>()), Times.Never);
        }

        [TestMethod]
        public async Task DeleteAsyncTest_ValidBook_HappyPath()
        {
            //arrange
            _access.WithDeleteResult(true);
            _mapper.WithMap(BookMapTestUtils.GenerateValidBookMap());

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(BookTestUtils.GenerateValidBook());

            //assert
            Assert.IsTrue(result);
            _access.Verify(m => m.DeleteOneAsync(It.IsAny<BookMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Book cannot be null.")]
        public async Task DeleteAsyncTest_NullBook_ThrowsNullReferenceException()
        {
            //arrange
            _access.WithDeleteWithIdResult(true);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(null);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.DeleteOneAsync(It.IsAny<BookMap>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Book isbn is not valid.")]
        public async Task DeleteAsyncTest_NullIsbnBook_ThrowsException()
        {
            //arrange
            var book = BookTestUtils.GenerateValidBook();
            book.Isbn = null;

            _access.WithDeleteWithIdResult(true);
            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(book);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.DeleteWithIdAsync(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Book isbn is not valid.")]
        public async Task DeleteAsyncTest_InvalidIsbnBook_ThrowsException()
        {
            //arrange
            var book = BookTestUtils.GenerateValidBook();
            book.Isbn = "invalid-isbn";

            _access.WithDeleteWithIdResult(true);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(book);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.DeleteWithIdAsync(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task InsertOrUpdateAsyncTest_ValidBook_HappyPath()
        {
            //arrange
            _mapper.WithMap(BookMapTestUtils.GenerateValidBookMap());
            _access.WithInsertUpdateResult(true);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.InsertOrUpdateAsync(BookTestUtils.GenerateValidBook());

            //assert
            Assert.IsTrue(result);
            _access.Verify(m => m.InsertOrUpdateAsync(It.IsAny<BookMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Book cannot be null.")]
        public async Task InsertOrUpdateAsyncTest_NullBook_ThrowsNullReferenceException()
        {
            //arrange
            _mapper.WithMap(BookMapTestUtils.GenerateValidBookMap());
            _access.WithInsertUpdateResult(true);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.InsertOrUpdateAsync(null);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.InsertOrUpdateAsync(It.IsAny<BookMap>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Book map cannot be null.")]
        public async Task InsertOrUpdateAsyncTest_NullBookMap_ThrowsNullReferenceException()
        {
            //arrange
            _mapper.WithMap(null);
            _access.WithInsertUpdateResult(true);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.InsertOrUpdateAsync(BookTestUtils.GenerateValidBook());

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.InsertOrUpdateAsync(It.IsAny<BookMap>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Book cannot be deleted by id.")]
        public async Task DeleteAsyncTest_NullBookMap_ThrowsNullReferenceException()
        {
            //arrange
            _access.WithInsertUpdateResult(true);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(It.IsAny<int>());

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.DeleteWithIdAsync(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task DeleteWithIsbnAsyncTest_ValidIsbn_HappyPath()
        {
            //arrange
            _access.WithDeleteResult(true);
            _mapper.WithMap(BookMapTestUtils.GenerateValidBookMap());

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(BookTestUtils.GenerateValidBook());

            //assert
            Assert.IsTrue(result);
            _access.Verify(m => m.DeleteOneAsync(It.IsAny<IMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Book isbn is not valid.")]
        public async Task DeleteWithIsbnAsyncTest_NullIsbn_ThrowsException()
        {
            //arrange
            var book = BookTestUtils.GenerateValidBook();
            book.Isbn = null;

            _access.WithInsertUpdateResult(true);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(book);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.DeleteOneAsync(It.IsAny<IMap>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Book isbn is not valid.")]
        public async Task DeleteWithIsbnAsyncTest_InvalidIsbn_ThrowsException()
        {
            //arrange
            var book = BookTestUtils.GenerateValidBook();
            book.Isbn = "invalid-isbn";

            _access.WithInsertUpdateResult(true);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteAsync(book);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.DeleteOneAsync(It.IsAny<IMap>()), Times.Never);
        }

        [TestMethod]
        public async Task GetByIsbnAsyncTest_ValidIsbn_HappyPath()
        {
            //arrange
            _mapper.WithUnmap(BookTestUtils.GenerateValidBook());
            _access.WithSelectWithIdResult(BookMapTestUtils.GenerateValidBookMap() as IBookMap);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetByIsbnAsync(BookTestUtils.GenerateValidBook().Isbn);

            //assert
            Assert.IsNotNull(result);
            _access.Verify(m => m.SelectWithIdAsync(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task GetByIsbnAsyncTest_NonExistingIsbn_HappyPath()
        {
            //arrange
            _access.WithSelectWithIdResult(null);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetByIsbnAsync(BookTestUtils.GenerateValidBook().Isbn);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.SelectWithIdAsync(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Book isbn is not valid.")]
        public async Task GetByIsbnAsyncTest_NullIsbn_ThrowsException()
        {
            //arrange
            var book = BookTestUtils.GenerateValidBook();
            book.Isbn = null;

            _mapper.WithUnmap(BookTestUtils.GenerateValidBook());
            _access.WithSelectWithIdResult(BookMapTestUtils.GenerateValidBookMap() as IBookMap);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetByIsbnAsync(book.Isbn);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.SelectWithIdAsync(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Book isbn is not valid.")]
        public async Task GetByIsbnAsyncTest_InvalidIsbn_ThrowsException()
        {
            //arrange
            var book = BookTestUtils.GenerateValidBook();
            book.Isbn = "invalid-isbn";

            _mapper.WithUnmap(BookTestUtils.GenerateValidBook());
            _access.WithSelectWithIdResult(BookMapTestUtils.GenerateValidBookMap() as IBookMap);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetByIsbnAsync(book.Isbn);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.SelectWithIdAsync(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task GetDescriptionTest_ValidIsbn_HappyPath()
        {
            //arrange
            _access.WithSelectDescriptionWithIdResult(BookTestUtils.GenerateValidDescription());

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetDescription(BookTestUtils.GenerateValidBook().Isbn);

            //assert
            Assert.IsNotNull(result);
            _access.Verify(m => m.SelectDescriptionWithId(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Book isbn is not valid.")]
        public async Task GetDescriptionTest_NullIsbn_Exception()
        {
            //arrange
            _access.WithSelectDescriptionWithIdResult(BookTestUtils.GenerateValidDescription());

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetDescription(null);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.SelectDescriptionWithId(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Book isbn is not valid.")]
        public async Task GetDescriptionTest_InvalidIsbn_Exception()
        {
            //arrange
            const string isbn = "invalid-isbn";
            _access.WithSelectDescriptionWithIdResult(BookTestUtils.GenerateValidDescription());

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.GetDescription(isbn);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.SelectDescriptionWithId(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task GetPagedBooks_ValidFilterAndPaging_HappyPath()
        {
            //arrange
            const int validPageId = 0;
            const int validPageSize = 10;

            _access.WithValidCatalogueSearch(BookCatalogueTestUtils.GetValidCatalogueSearchResult);
            _filterMapper.WithMap(BookCatalogueFilterTestUtils.GetValidCatalogueFilterMap);
            _catalogueMapper.WithUnMap(BookTestUtils.GenerateInvalidBook());

            _sut = new BookRepository(_access.Object, _mapper.Object, _filterMapper.Object, _catalogueMapper.Object);

            //act
            var result = await _sut.GetPagedBooks(validPageId, validPageSize);

            //assert
            Assert.IsNotNull(result);
            _access.Verify(m => m.SearchCatalogue(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CatalogueFilterMap>()), Times.Once);
        }

        [TestMethod]
        public async Task GetPagedBooks_EmptyCatalogueItems_HappyPath()
        {
            //arrange
            const int validPageId = 10;
            const int invalidPageSize = 10;

            _access.WithValidCatalogueSearch(new List<CatalogueItemMap>());
            _filterMapper.WithMap(BookCatalogueFilterTestUtils.GetValidCatalogueFilterMap);
            _catalogueMapper.WithUnMap(BookTestUtils.GenerateInvalidBook());

            _sut = new BookRepository(_access.Object, _mapper.Object, _filterMapper.Object, _catalogueMapper.Object);

            //act
            var result = await _sut.GetPagedBooks(validPageId, invalidPageSize);

            //assert
            Assert.IsNotNull(result);
            _access.Verify(m => m.SearchCatalogue(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CatalogueFilterMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Page id cannot be null.")]
        public async Task GetPagedBooks_InvalidPageId_Exception()
        {
            //arrange
            const int invalidPageId = -1;
            const int validPageSize = 10;

            _sut = new BookRepository(_access.Object, _mapper.Object, _filterMapper.Object, _catalogueMapper.Object);

            //act
            var result = await _sut.GetPagedBooks(invalidPageId, validPageSize);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.SelectDescriptionWithId(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Page size has to be greater than 0.")]
        public async Task GetPagedBooks_InvalidPageSize_Exception()
        {
            //arrange
            const int validPageId = 10;
            const int invalidPageSize = 0;

            _sut = new BookRepository(_access.Object, _mapper.Object, _filterMapper.Object, _catalogueMapper.Object);

            //act
            var result = await _sut.GetPagedBooks(validPageId, invalidPageSize);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.SelectDescriptionWithId(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Filter map cannot be null.")]
        public async Task GetPagedBooks_NullFilterMap_Exception()
        {
            //arrange
            const int validPageId = 10;
            const int invalidPageSize = 10;

            _access.WithValidCatalogueSearch(BookCatalogueTestUtils.GetValidCatalogueSearchResult);
            _filterMapper.WithMap(null);
            _catalogueMapper.WithUnMap(BookTestUtils.GenerateInvalidBook());

            _sut = new BookRepository(_access.Object, _mapper.Object, _filterMapper.Object, _catalogueMapper.Object);

            //act
            var result = await _sut.GetPagedBooks(validPageId, invalidPageSize);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.SelectDescriptionWithId(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Catalogue item maps cannot be null.")]
        public async Task GetPagedBooks_NullCatalogueItems_Exception()
        {
            //arrange
            const int validPageId = 10;
            const int invalidPageSize = 10;

            _access.WithValidCatalogueSearch(null);
            _filterMapper.WithMap(BookCatalogueFilterTestUtils.GetValidCatalogueFilterMap);
            _catalogueMapper.WithUnMap(BookTestUtils.GenerateInvalidBook());

            _sut = new BookRepository(_access.Object, _mapper.Object, _filterMapper.Object, _catalogueMapper.Object);

            //act
            var result = await _sut.GetPagedBooks(validPageId, invalidPageSize);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.SelectDescriptionWithId(It.IsAny<string>()), Times.Never);
        }

        #region DeleteWithIsbnAsync

        [TestMethod]
        public async Task DeleteWithIsbnAsyncTest_ValidBook_HappyPath()
        {
            //arrange
            _access.WithDeleteWithIdResult(true);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteWithIsbnAsync(BookTestUtils.GenerateValidBook().Isbn);

            //assert
            Assert.IsTrue(result);
            _access.Verify(m => m.DeleteWithIdAsync(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Book cannot be null.")]
        public async Task DeleteWithIsbnAsyncTest_InvalidBook_Fails()
        {
            //arrange
            _access.WithDeleteWithIdResult(true);

            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteWithIsbnAsync(BookTestUtils.GenerateInvalidBook().Isbn);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.DeleteWithIdAsync(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Book isbn is not valid.")]
        public async Task DeleteWithIsbnAsyncTest_NullIsbnBook_Fails()
        {
            //arrange

            _access.WithDeleteWithIdResult(true);
            _sut = new BookRepository(_access.Object, _mapper.Object);

            //act
            var result = await _sut.DeleteWithIsbnAsync(null);

            //assert
            Assert.IsFalse(result);
            _access.Verify(m => m.DeleteWithIdAsync(It.IsAny<string>()), Times.Never);
        }

        #endregion

        #region GetPagedBooksWithFilter

        [TestMethod]
        public async Task GetPagedBooksFilterTest_ValidFilterAndPaging_HappyPath()
        {
            //arrange
            const int validPageId = 0;
            const int validPageSize = 10;

            _access.WithValidCatalogueSearch(BookCatalogueTestUtils.GetValidCatalogueSearchResult);
            _filterMapper.WithMap(BookCatalogueFilterTestUtils.GetValidCatalogueFilterMap);
            _catalogueMapper.WithUnMap(BookTestUtils.GenerateInvalidBook());

            _sut = new BookRepository(_access.Object, _mapper.Object, _filterMapper.Object, _catalogueMapper.Object);

            //act
            var result = await _sut.GetPagedBooks(validPageId, validPageSize, BookCatalogueFilterTestUtils.GetValidCatalogueFilter);

            //assert
            Assert.IsNotNull(result);
            _access.Verify(m => m.SearchCatalogue(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CatalogueFilterMap>()), Times.Once);
        }

        [TestMethod]
        public async Task GetPagedBooksFilterTest_EmptyCatalogueItems_HappyPath()
        {
            //arrange
            const int validPageId = 10;
            const int invalidPageSize = 10;

            _access.WithValidCatalogueSearch(new List<CatalogueItemMap>());
            _filterMapper.WithMap(BookCatalogueFilterTestUtils.GetValidCatalogueFilterMap);
            _catalogueMapper.WithUnMap(BookTestUtils.GenerateInvalidBook());

            _sut = new BookRepository(_access.Object, _mapper.Object, _filterMapper.Object, _catalogueMapper.Object);

            //act
            var result = await _sut.GetPagedBooks(validPageId, invalidPageSize, BookCatalogueFilterTestUtils.GetValidCatalogueFilter);

            //assert
            Assert.IsNotNull(result);
            _access.Verify(m => m.SearchCatalogue(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CatalogueFilterMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Page id cannot be null.")]
        public async Task GetPagedBooksFilterTest_InvalidPageId_Exception()
        {
            //arrange
            const int invalidPageId = -1;
            const int validPageSize = 10;

            _sut = new BookRepository(_access.Object, _mapper.Object, _filterMapper.Object, _catalogueMapper.Object);

            //act
            var result = await _sut.GetPagedBooks(invalidPageId, validPageSize, BookCatalogueFilterTestUtils.GetValidCatalogueFilter);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.SelectDescriptionWithId(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Page size has to be greater than 0.")]
        public async Task GetPagedBooksFilterTest_InvalidPageSize_Exception()
        {
            //arrange
            const int validPageId = 10;
            const int invalidPageSize = 0;

            _sut = new BookRepository(_access.Object, _mapper.Object, _filterMapper.Object, _catalogueMapper.Object);

            //act
            var result = await _sut.GetPagedBooks(validPageId, invalidPageSize, BookCatalogueFilterTestUtils.GetValidCatalogueFilter);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.SelectDescriptionWithId(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Filter map cannot be null.")]
        public async Task GetPagedBooksFilterTest_NullFilterMap_Exception()
        {
            //arrange
            const int validPageId = 10;
            const int invalidPageSize = 10;

            _access.WithValidCatalogueSearch(BookCatalogueTestUtils.GetValidCatalogueSearchResult);
            _filterMapper.WithMap(null);
            _catalogueMapper.WithUnMap(BookTestUtils.GenerateInvalidBook());

            _sut = new BookRepository(_access.Object, _mapper.Object, _filterMapper.Object, _catalogueMapper.Object);

            //act
            var result = await _sut.GetPagedBooks(validPageId, invalidPageSize, BookCatalogueFilterTestUtils.GetValidCatalogueFilter);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.SelectDescriptionWithId(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Catalogue item maps cannot be null.")]
        public async Task GetPagedBooksFilterTest_NullCatalogueItems_Exception()
        {
            //arrange
            const int validPageId = 10;
            const int invalidPageSize = 10;

            _access.WithValidCatalogueSearch(null);
            _filterMapper.WithMap(BookCatalogueFilterTestUtils.GetValidCatalogueFilterMap);
            _catalogueMapper.WithUnMap(BookTestUtils.GenerateInvalidBook());

            _sut = new BookRepository(_access.Object, _mapper.Object, _filterMapper.Object, _catalogueMapper.Object);

            //act
            var result = await _sut.GetPagedBooks(validPageId, invalidPageSize, BookCatalogueFilterTestUtils.GetValidCatalogueFilter);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.SelectDescriptionWithId(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Catalogue item maps cannot be null.")]
        public async Task GetPagedBooksFilterTest_NullFilter_Exception()
        {
            //arrange
            const int validPageId = 10;
            const int invalidPageSize = 10;

            _access.WithValidCatalogueSearch(null);
            _filterMapper.WithMap(BookCatalogueFilterTestUtils.GetValidCatalogueFilterMap);
            _catalogueMapper.WithUnMap(BookTestUtils.GenerateInvalidBook());

            _sut = new BookRepository(_access.Object, _mapper.Object, _filterMapper.Object, _catalogueMapper.Object);

            //act
            var result = await _sut.GetPagedBooks(validPageId, invalidPageSize, null);

            //assert
            Assert.IsNull(result);
            _access.Verify(m => m.SelectDescriptionWithId(It.IsAny<string>()), Times.Never);
        }

        #endregion

        #region GetBookCount

        [TestMethod]
        public async Task GetBookCountTest_HappyPath()
        {
            //arrange
            _access.WithValidGetBookCount();

            _sut = new BookRepository(_access.Object, _mapper.Object, _filterMapper.Object, _catalogueMapper.Object);

            //act
            await _sut.GetBookCount();

            //assert
            _access.Verify(m => m.Count(), Times.Once);
        }

        #endregion
    }
}
