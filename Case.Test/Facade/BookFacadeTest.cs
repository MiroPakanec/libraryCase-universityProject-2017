using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Case.Core.Repository.Interface;
using Case.Core.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Case.Core.Facade;
using Case.Test.Facade.Extensions;
using static Case.Test.TestUtils.BookTestUtils;
using static Case.Test.TestUtils.BookCopyTestUtils;
using static Case.Test.TestUtils.MemberTestUtils;
using static Case.Test.Facade.Extensions.BookRepositoryMockExtensions;
using static Case.Test.Facade.Extensions.OrderRepositoryMockExtensions;
using static Case.Test.Facade.Extensions.OrderLineRepositoryMockExtensions;
using Moq;

namespace Case.Test.Facade
{
    [TestClass]
    public class BookFacadeTest
    {
        private BookFacade _sut;
        private IBookRepository _bookRepository;
        private IMemberRepository _memberRepository;
        private IBookCopyRepository _bookCopyRepository;
        private IOrderRepository _orderRepository;
        private IOrderLineRepository _orderLineRepository;
        private Mock<IBookRepository> _bookRepositoryMock;
        private Mock<IMemberRepository> _memberRepositoryMock;
        private Mock<IBookCopyRepository> _bookCopyRepositoryMock;
        private Mock<IOrderRepository> _orderRepositoryMock;
        private Mock<IOrderLineRepository> _orderLineRepositoryMock;


        [TestInitialize]
        public void Setup()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _memberRepositoryMock = new Mock<IMemberRepository>();
            _bookCopyRepositoryMock = new Mock<IBookCopyRepository>();
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _orderLineRepositoryMock = new Mock<IOrderLineRepository>();
            _bookRepository = _bookRepositoryMock.Object;
            _memberRepository = _memberRepositoryMock.Object;
            _bookCopyRepository = _bookCopyRepositoryMock.Object;
            _orderRepository = _orderRepositoryMock.Object;
            _orderLineRepository = _orderLineRepositoryMock.Object;
            _sut = new BookFacade(_bookRepository, _memberRepository, _bookCopyRepository, _orderRepository, _orderLineRepository);
        }

        #region AddBook
        [TestMethod]
        public async Task AddValidBook()
        {
            //arrange
            _bookRepositoryMock.WithValidInsert();
            var book = GenerateValidBook();
            
            //act
            bool result = await _sut.AddBookAsync(book);

            //assert
            _bookRepositoryMock.Verify(x => x.InsertAsync(book), Times.Once);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Book cannot have invalid isbn.")]
        public async Task AddInvalidBook()
        {
            //arrange
            _bookRepositoryMock.WithValidInsert();
            var book = GenerateInvalidBook();

            //act
            bool result = await _sut.AddBookAsync(book);

            //assert
            _bookRepositoryMock.Verify(X => X.InsertAsync(It.IsAny<Book>()), Times.Never);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Book cannot be null.")]
        public async Task AddNullBook()
        {
            //arrange
            _bookRepositoryMock.WithValidInsert();
            Book book = null;

            //act
            bool result = await _sut.AddBookAsync(book);

            //assert
            _bookRepositoryMock.Verify(x => x.InsertAsync(It.IsAny<Book>()), Times.Never);
            Assert.IsFalse(result);
        }
        #endregion

        #region UpdateBook

        [TestMethod]
        public async Task UpdateValidBook()
        {
            //arrange
            _bookRepositoryMock.WithValidUpdate();
            var book = GenerateValidBook();
            
            //act
            bool result = await _sut.UpdateBookAsync(book);
            
            //assert
            _bookRepositoryMock.Verify(x => x.UpdateAsync(book), Times.Once);
            Assert.IsTrue(result);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Book cannot have invalid isbn.")]
        public async Task UpdateInvalidBook()
        {
            //arrange
            _bookRepositoryMock.WithValidUpdate();
            var book = GenerateInvalidBook();

            //act
            bool result = await _sut.UpdateBookAsync(book);

            //assert
            _bookRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Book>()), Times.Never);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Book cannot be null.")]
        public async Task UpdateNullBook()
        {
            //arrange
            _bookRepositoryMock.WithValidUpdate();
            Book book = null;

            //act
            bool result = await _sut.UpdateBookAsync(book);

            //assert
            _bookRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Book>()), Times.Never);
            Assert.IsFalse(result);
        }

        #endregion

        #region DeleteBook

        [TestMethod]
        public async Task DeleteValidBook()
        {
            //arrange
            _bookRepositoryMock.WithValidDelete();
            var book = GenerateValidBook();

            //act
            bool result = await _sut.RemoveBookAsync(book);

            //assert
            _bookRepositoryMock.Verify(x => x.DeleteAsync(book), Times.Once);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Book cannot have invalid isbn.")]
        public async Task DeleteInvalidBook()
        {
            //arrange
            _bookRepositoryMock.WithValidDelete();
            var book = GenerateInvalidBook();

            //act
            bool result = await _sut.RemoveBookAsync(book);

            //assert
            _bookRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Book>()), Times.Never);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Book cannot be null.")]
        public async Task DeleteNullBook()
        {
            //arrange
            _bookRepositoryMock.WithValidDelete();
            Book book = null;

            //act
            bool result = await _sut.RemoveBookAsync(book);

            //assert
            _bookRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Book>()), Times.Never);
            Assert.IsFalse(result);
        }

        #endregion

        #region FindBookByIsbn
        [TestMethod]
        public async Task FindBookByValidIsbn()
        {
            //arrange
            _bookRepositoryMock.WithValidGetByIsbn();
            var isbn = GenerateValidIsbn13();

            //act
            var result = await _sut.FindBookByIsbnAsync(isbn);

            //assert
            _bookRepositoryMock.Verify(x => x.GetByIsbnAsync(isbn), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Isbn cannot be null, empty or invalid.")]
        public async Task FindBookByInvalidIsbn()
        {
            //arrange
            _bookRepositoryMock.WithValidGetByIsbn();
            var isbn = GenerateInvalidIsbn13();

            //act
            var result = await _sut.FindBookByIsbnAsync(isbn);

            //assert
            _bookRepositoryMock.Verify(x => x.GetByIsbnAsync(It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Isbn cannot be null, empty or invalid.")]
        public async Task FindNullBookByNullIsbn()
        {
            //arrange
            _bookRepositoryMock.WithValidGetByIsbn();
            string isbn = null;

            //act
            var result = await _sut.FindBookByIsbnAsync(isbn);

            //assert
            _bookRepositoryMock.Verify(x => x.GetByIsbnAsync(It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        #endregion

        #region FindBookByDescription

        [TestMethod]
        public async Task FindBookByValidDescription()
        {
            //arrange
            _bookRepositoryMock.WithValidGetByDescription();
            var description = GenerateValidDescription();

            //act
            var result = await _sut.FindBookByDescriptionAsync(description);

            //assert
            _bookRepositoryMock.Verify(x => x.GetByDescription(description), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Description cannot be null or empty")]
        public async Task FindBookByInvalidDescription()
        {
            //arrange
            _bookRepositoryMock.WithValidGetByDescription();
            var description = GenerateInvalidDescription();
            
            //act
            var result = await _sut.FindBookByDescriptionAsync(description);

            //assert
            _bookRepositoryMock.Verify(x => x.GetByDescription(It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Description cannot be null or empty")]
        public async Task FindNullBookByNullDescription()
        {
            //arrange
            _bookRepositoryMock.WithValidGetByDescription();
            string description = null;

            //act
            var result = await _sut.FindBookByDescriptionAsync(description);

            //assert
            _bookRepositoryMock.Verify(x => x.GetByDescription(It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        #endregion

        #region FindBookByPopularity
        [TestMethod]
        public async Task FindBookByValidPopularity()
        {
            //arrange
            _bookRepositoryMock.WithValidGetByLoans();
            var loans = 5;

            //act
            var result = await _sut.FindBookByLoansAsync(loans);

            //assert
            _bookRepositoryMock.Verify(x => x.GetByLoans(It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Loans cannot be null, empty or invalid")]
        public async Task FindBookByInvalidPopularity()
        {
            //arrange
            _bookRepositoryMock.WithValidGetByLoans();
            var loans = -1;

            //act
            var result = await _sut.FindBookByLoansAsync(loans);

            //assert
            _bookRepositoryMock.Verify(x => x.GetByLoans(It.IsAny<int>()), Times.Never);
            Assert.IsNull(result);
        }

        /*[TestMethod]
        [ExpectedException(typeof(ArgumentException), "Loans cannot be null, empty or invalid")]
        public async Task FindBookByNullPopularity()
        {
            _bookRepositoryMock.Setup(x => x.GetByLoans(It.IsAny<int>())).ReturnsAsync(GenerateValidBook());
            int? loans = null;
            var result = await _sut.FindBookByLoansAsync(loans);
            _bookRepositoryMock.Verify(x => x.GetByLoans(It.IsAny<int>()), Times.Never);
        }*/
        #endregion

        #region FindMostPopularBookOfaSpecificAuthor
        [TestMethod]
        public async Task FindBookByValidPopularityAndValidAuthor()
        {
            //arrange
            _bookRepositoryMock.WithValidGetByAuthorLoans();
            var loans = 5;
            var author = GenerateValidAuthor();

            //act
            var result = await _sut.FindPopularBookByAuthorAsync(loans, author);

            //assert
            _bookRepositoryMock.Verify(x => x.GetByAuthor(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Loans cannot be null, empty or invalid")]
        public async Task FindBookByInvalidPopularityAndValidAuthor()
        {
            //arrange
            _bookRepositoryMock.WithValidGetByAuthorLoans();
            var loans = -1;
            var author = GenerateValidAuthor();

            //act
            var result = await _sut.FindPopularBookByAuthorAsync(loans, author);

            //assert
            _bookRepositoryMock.Verify(x => x.GetByAuthor(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Author cannot be null, empty or invalid")]
        public async Task FindBookByValidPopularityAndInvalidAuthor()
        {
            //arrange
            _bookRepositoryMock.WithValidGetByAuthorLoans();
            var loans = 5;
            var author = GenerateInvalidAuthor();

            //act
            var result = await _sut.FindPopularBookByAuthorAsync(loans, author);

            //assert
            _bookRepositoryMock.Verify(x => x.GetByAuthor(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Loans and author cannot be null, empty or invalid")]
        public async Task FindBookByInvalidPopularityAndInvalidAuthor()
        {
            //arrange
            _bookRepositoryMock.WithValidGetByAuthorLoans();
            var loans = -1;
            var author = GenerateInvalidAuthor();

            //act
            var result = await _sut.FindPopularBookByAuthorAsync(loans, author);

            //assert
            _bookRepositoryMock.Verify(x => x.GetByAuthor(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Loans and author cannot be null, empty or invalid")]
        public async Task FindBookByInvalidPopularityAndNullAuthor()
        {
            //arrange
            _bookRepositoryMock.WithValidGetByAuthorLoans();
            var loans = -1;
            string author = null;

            //act
            var result = await _sut.FindPopularBookByAuthorAsync(loans, author);

            //assert
            _bookRepositoryMock.Verify(x => x.GetByAuthor(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        #endregion

        #region GetBookCount

        [TestMethod]
        public async Task GetBookCount_WithValidRepositoryBookCount()
        {
            //arrange
            _bookRepositoryMock.WithValidGetBookCount();

            //act
            var result = await _sut.GetBookCount();

            //assert
            _bookRepositoryMock.Verify(x => x.GetBookCount(), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Book Count cannot be negative")]
        public async Task GetBookCount_WithInvalidRepositoryBookCount()
        {
            _bookRepositoryMock.WithInvalidGetBookCount();

            await _sut.GetBookCount();
        }

        #endregion

        #region GetBookDescription

        [TestMethod]
        public async Task GetBookDescription_ValidIsbn10_HappyPath()
        {
            //arrange
            _bookRepositoryMock.WithValidGetDescription();
            var isbn = GenerateValidIsbn10();

            //act
            var result = await _sut.GetBookDescription(isbn);

            //assert
            _bookRepositoryMock.Verify(x => x.GetDescription(It.IsAny<string>()), Times.Once);
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetBookDescription_ValidIsbn13_HappyPath()
        {
            //arrange
            _bookRepositoryMock.WithValidGetDescription();
            var isbn = GenerateValidIsbn13();

            //act
            var result = await _sut.GetBookDescription(isbn);

            //assert
            _bookRepositoryMock.Verify(x => x.GetDescription(It.IsAny<string>()), Times.Once);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Description cannot be null or empty or invalid")]
        public async Task GetBookDescription_InvalidIsbn10_Fails()
        {
            //arrange
            _bookRepositoryMock.WithValidGetDescription();
            var isbn = GenerateInvalidIsbn10();

            //act
            var result = await _sut.GetBookDescription(isbn);

            //assert
            _bookRepositoryMock.Verify(x => x.GetDescription(It.IsAny<string>()), Times.Never);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "isbn cannot be null or empty or invalid")]
        public async Task GetBookDescription_InvalidIsbn13_Fails()
        {
            //arrange
            _bookRepositoryMock.WithValidGetDescription();
            var isbn = GenerateInvalidIsbn13();

            //act
            var result = await _sut.GetBookDescription(isbn);

            //assert
            _bookRepositoryMock.Verify(x => x.GetDescription(It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "isbn cannot be null or empty or invalid")]
        public async Task GetBookDescription_NullIsbn_Fails()
        {
            //arrange
            _bookRepositoryMock.WithValidGetDescription();

            //act
            var result = await _sut.GetBookDescription(null);

            //assert
            _bookRepositoryMock.Verify(x => x.GetDescription(It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        #endregion

        #region GetPagedBookCatalogModel

        [TestMethod]
        public async Task GetPagedBookCatalogModel_ValidIdAndSize()
        {
            int id = 1;
            int size = 10;
            _bookRepositoryMock.WithValidGetPagedBooks();

            var result = await _sut.GetPagedBookCatalogModel(id, size);

            _bookRepositoryMock.Verify(x => x.GetPagedBooks(It.IsAny<int>(), It.IsAny<int>()));
            Assert.AreEqual(size, result.Books.Count());
            Assert.AreEqual(id + 1, result.PageIndex);
                        //  ^^^^^^ Because we pass index that starts at 0 for arrays
                        // And the one in the model is readable one, that starts at 1
                        // that's why + 1
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Id cannot be negative")]
        public async Task GetPageBookCatalogModel_InvalidId_Fails()
        {
            var result = await _sut.GetPagedBookCatalogModel(-1, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Size cannot be <=0")]
        public async Task GetPagedBookCatalogModel_InvalidSize_Fails()
        {
            var result = await _sut.GetPagedBookCatalogModel(1, -10);
        }
        #endregion

        #region RentBooks

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "ssn cannot be invalid/empty when renting books")]
        public async Task RentBooks_InvalidSsn_Fails()
        {
            await _sut.RentBooks(GenerateInvalidSsn(), new List<string>() {GenerateValidIsbn13()});
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "isbn list cannot be empty when renting books")]
        public async Task RentBooks_EmptyIsbnList_Fails()
        {
            await _sut.RentBooks(GenerateValidSsn(), new List<string>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Isbn list cannot be null when renting books")]
        public async Task RentBooks_NullIsbnList_Fails()
        {
            await _sut.RentBooks(GenerateValidSsn(), null);
        }

        [TestMethod]
        public async Task RentBooks_MemberHasNotVerifiedRole_ResultModelUnsuccessful()
        {
            _memberRepositoryMock.WithSuccesfulGetRoleNameCannotRent();
            _memberRepositoryMock.WithSuccesfulFindBySsn();

            var result = await _sut.RentBooks(GenerateValidSsn(), new List<string>() {GenerateValidIsbn13()});

            _memberRepositoryMock.Verify(x => x.FindBySsn(It.IsAny<string>()));
            _memberRepositoryMock.Verify(x => x.GetRoleName(It.IsAny<Member>()));

            Assert.IsFalse(result.IsSuccesful);
        }
        [TestMethod]
        public async Task RentBooks_BookNotLoanable_ResultModelUnsuccessful()
        {
            _memberRepositoryMock.WithSuccesfulGetRoleNameCanRent();
            _memberRepositoryMock.WithSuccesfulFindBySsn();
            _orderRepositoryMock.WithValidInsertWithIdReturn();
            _bookRepositoryMock.WithValidGetByIsbn_BookNotLoanable();

            var result = await _sut.RentBooks(GenerateValidSsn(), new List<string>() {GenerateValidIsbn13()});

            _memberRepositoryMock.Verify(x => x.FindBySsn(It.IsAny<string>()));
            _memberRepositoryMock.Verify(x => x.GetRoleName(It.IsAny<Member>()));
            _orderRepositoryMock.Verify(x => x.InsertAsyncWithIdReturn(It.IsAny<Order>()));
            _bookRepositoryMock.Verify(x => x.GetByIsbnAsync(It.IsAny<string>()));

            Assert.IsFalse(result.IsSuccesful);
        }

        [TestMethod]
        public async Task RentBooks_NoCopiesAvailable_ResultModelUnsuccessful()
        {
            _memberRepositoryMock.WithSuccesfulGetRoleNameCanRent();
            _memberRepositoryMock.WithSuccesfulFindBySsn();
            _orderRepositoryMock.WithValidInsertWithIdReturn();
            _bookRepositoryMock.WithValidGetByIsbn();
            _bookCopyRepositoryMock.WithEmptyFindByIsbn();

            var result = await _sut.RentBooks(GenerateValidSsn(), new List<string>() { GenerateValidIsbn13() });

            _memberRepositoryMock.Verify(x => x.FindBySsn(It.IsAny<string>()));
            _memberRepositoryMock.Verify(x => x.GetRoleName(It.IsAny<Member>()));
            _orderRepositoryMock.Verify(x => x.InsertAsyncWithIdReturn(It.IsAny<Order>()));
            _bookRepositoryMock.Verify(x => x.GetByIsbnAsync(It.IsAny<string>()));
            _bookCopyRepositoryMock.Verify(x => x.GetByIsbnAsync(It.IsAny<string>()));

            Assert.IsFalse(result.IsSuccesful);
        }

        [TestMethod]
        public async Task RentBoks_EverythingValid_ResultModelSuccessful()
        {
            _memberRepositoryMock.WithSuccesfulGetRoleNameCanRent();
            _memberRepositoryMock.WithSuccesfulFindBySsn();
            _orderRepositoryMock.WithValidInsertWithIdReturn();
            _bookRepositoryMock.WithValidGetByIsbn();
            _bookCopyRepositoryMock.WithValidFindByIsbn();
            _orderLineRepositoryMock.WithValidInsert();
            _bookCopyRepositoryMock.WithValidUpdate();

            var result = await _sut.RentBooks(GenerateValidSsn(), new List<string>() { GenerateValidIsbn13() });

            _memberRepositoryMock.Verify(x => x.FindBySsn(It.IsAny<string>()));
            _memberRepositoryMock.Verify(x => x.GetRoleName(It.IsAny<Member>()));
            _orderRepositoryMock.Verify(x => x.InsertAsyncWithIdReturn(It.IsAny<Order>()));
            _bookRepositoryMock.Verify(x => x.GetByIsbnAsync(It.IsAny<string>()));
            _bookCopyRepositoryMock.Verify(x => x.GetByIsbnAsync(It.IsAny<string>()));
            _orderLineRepositoryMock.Verify(x=>x.InsertAsync(It.IsAny<OrderLine>()));
            _bookCopyRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<BookCopy>()));

            Assert.IsTrue(result.IsSuccesful);
        }

        #endregion

        #region GetBookCatalogItemModelByIsbn

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Isbn cannot be invalid")]
        public async Task GetBookCatalogItemModelByIsbn_InvalidIsbn_Fails()
        {
            await _sut.GetBookCatalogItemModelByIsbn(GenerateInvalidIsbn13());
        }

        [TestMethod]
        public async Task GetBookCatalogItemModelByIsbn_ValidIsbn()
        {
            _bookRepositoryMock.WithValidGetByIsbn();

            var result = await _sut.GetBookCatalogItemModelByIsbn(GenerateValidIsbn10());

            _bookRepositoryMock.Verify(x => x.GetByIsbnAsync(It.IsAny<string>()));
            Assert.IsNotNull(result);
        }

        #endregion

        #region GetBookCatalogItemModelSByIsbnS

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Isbns cannot be null")]
        public async Task GetBookCatalogItemModelSByIsbnS_NullIsbns_Fails()
        {
            await _sut.GetBookCatalogItemModelsByIsbns(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Isbns cannot be empty")]
        public async Task GetBookCatalogItemModelSByIsbnS_InvalidIsbn_Fails()
        {
            await _sut.GetBookCatalogItemModelsByIsbns(new string[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Isbns cannot be empty")]
        public async Task GetBookCatalogItemModelsByIsbns_NonEmptyListWithInvalidIsbn()
        {
            string[] isbns = new string[1] { GenerateInvalidIsbn10() };

            await _sut.GetBookCatalogItemModelsByIsbns(isbns);
        }

        [TestMethod]
        public async Task GetBookCatalogItemModelsByIsbns_NonEmptyListWithValidIsbn()
        {
            _bookRepositoryMock.WithValidGetByIsbn();
            string[] isbns = new string[1] { GenerateValidIsbn10() };

            var result = await _sut.GetBookCatalogItemModelsByIsbns(isbns);

            _bookRepositoryMock.Verify(x => x.GetByIsbnAsync(It.IsAny<string>()));
            Assert.IsTrue(result.Any());
        }

        #endregion
    }
}