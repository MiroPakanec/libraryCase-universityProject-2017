//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Case.Core.Entity;
//using Case.Core.Mapper;
//using Case.Core.Mapper.Book;
//using Case.Core.Model;
//using Case.Core.Model.Extensions;
//using Case.Core.Repository;
//using Case.Test.TestUtils;
//using DataAccess.EntityMaps.Book;
//using DataAccess.Queries.Book;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Case.IntegrationTest.Repository
//{
//    [TestClass]
//    public class BookRepositoryTests
//    {
//        private BookRepository _sut;
//        private IMapper<Book> _mapper;
//        private IMapper<BookCatalogFilter> _filterMapper;
//        private IDualMapper<Book, CatalogueItemMap> _bookCatalogueMapper;
//        private IBookAccess _access;

//        [TestInitialize]
//        public void SetUp()
//        {
//            _mapper = new BookMapper();
//            _filterMapper = new CatalogueFilterMapper();
//            _bookCatalogueMapper = new CatalogueItemMapper();
//            _access = new BookAccess();

//            _sut = new BookRepository(_access, _mapper, _filterMapper, _bookCatalogueMapper);
//        }

//        [TestCleanup]
//        public void TearDown()
//        {

//        }

//        [TestMethod]
//        public async Task InsertBook_ValidBookEmptyFilter_HappyPath()
//        {
//            //arrange
//            const int pageId = 0;
//            const int pageSize = 5;
//            var filter = new BookCatalogFilter();

//            //act
//            var result = await _sut.GetPagedBooks(pageId, pageSize, filter);

//            //assert
//            Assert.IsNotNull(result);
//            Assert.IsTrue(result.Count(b => true).Equals(pageSize));
//        }

//        //TC-I-1
//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException))]
//        public async Task InsertBook_TCI1_ExceptionalCase()
//        {
//            //arrange
//            const int pageId = -1;
//            const int pageSize = 1;

//            var filter = new BookCatalogFilter()
//                .WithLoanability(false)
//                .WithAuthor(string.Empty)
//                .WithSubject(GenerateRandomString(15))
//                .WithTitle(string.Empty)
//                .WithIsbn(GenerateRandomString(15))
//                .WithStartDate(DateTime.MinValue)
//                .WithEndDate(DateTime.Now);

//            //act
//            await _sut.GetPagedBooks(pageId, pageSize, filter);
//        }

//        //TC-I-2
//        [TestMethod]
//        public async Task InsertBook_TCI2_HappyPath()
//        {
//            //arrange
//            const int pageId = 0;
//            const int pageSize = 1;

//            var filter = new BookCatalogFilter()
//                .WithLoanability(true)
//                .WithAuthor(GenerateRandomString(31))
//                .WithSubject(GenerateRandomString(31))
//                .WithTitle(GenerateRandomString(101))
//                .WithIsbn(GenerateRandomString(21))
//                .WithStartDate(DateTime.MaxValue)
//                .WithEndDate(DateTime.MaxValue);

//            //act
//            var result = await _sut.GetPagedBooks(pageId, pageSize, filter);

//            //assert
//            Assert.IsNotNull(result);
//            Assert.IsTrue(result.Count(b => true).Equals(0));
//        }

//        //TC-I-3
//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException))]
//        public async Task InsertBook_TCI3_ExceptionalCase()
//        {
//            //arrange
//            const int pageId = 0;
//            const int pageSize = 0;

//            var filter = new BookCatalogFilter()
//                .WithLoanability(true)
//                .WithAuthor(string.Empty)
//                .WithSubject(string.Empty)
//                .WithTitle(GenerateRandomString(15))
//                .WithIsbn(GenerateRandomString(15))
//                .WithStartDate(DateTime.MinValue)
//                .WithEndDate(DateTime.MaxValue);

//            //act
//            await _sut.GetPagedBooks(pageId, pageSize, filter);
//        }

//        //TC-I-4
//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException))]
//        public async Task InsertBook_TCI4_ExceptionalCase()
//        {
//            //arrange
//            const int pageId = 0;
//            const int pageSize = 0;

//            var filter = new BookCatalogFilter()
//                .WithLoanability(false)
//                .WithAuthor(GenerateRandomString(15))
//                .WithSubject(GenerateRandomString(15))
//                .WithTitle(GenerateRandomString(101))
//                .WithIsbn(GenerateRandomString(21))
//                .WithStartDate(DateTime.Now)
//                .WithEndDate(DateTime.Now);

//            //act
//            await _sut.GetPagedBooks(pageId, pageSize, filter);
//        }

//        //TC-I-5
//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException))]
//        public async Task InsertBook_TCI5_ExceptionalCase()
//        {
//            //arrange
//            const int pageId = 0;
//            const int pageSize = 0;

//            var filter = new BookCatalogFilter()
//                .WithLoanability(true)
//                .WithAuthor(GenerateRandomString(31))
//                .WithSubject(GenerateRandomString(15))
//                .WithTitle(string.Empty)
//                .WithIsbn(string.Empty)
//                .WithStartDate(DateTime.MaxValue)
//                .WithEndDate(DateTime.MinValue);

//            //act
//            await _sut.GetPagedBooks(pageId, pageSize, filter);
//        }

//        //TC-I-6
//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException))]
//        public async Task InsertBook_TCI6_ExceptionalCase()
//        {
//            //arrange
//            const int pageId = -1;
//            const int pageSize = 0;

//            var filter = new BookCatalogFilter()
//                .WithLoanability(false)
//                .WithAuthor(GenerateRandomString(31))
//                .WithSubject(GenerateRandomString(31))
//                .WithTitle(GenerateRandomString(15))
//                .WithIsbn(GenerateRandomString(15))
//                .WithStartDate(DateTime.MaxValue)
//                .WithEndDate(DateTime.Now);

//            //act
//            await _sut.GetPagedBooks(pageId, pageSize, filter);
//        }

//        //TC-I-7
//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException))]
//        public async Task InsertBook_TCI7_ExceptionalCase()
//        {
//            //arrange
//            const int pageId = -1;
//            const int pageSize = 1;

//            var filter = new BookCatalogFilter()
//                .WithLoanability(false)
//                .WithAuthor(string.Empty)
//                .WithSubject(GenerateRandomString(31))
//                .WithTitle(GenerateRandomString(101))
//                .WithIsbn(string.Empty)
//                .WithStartDate(DateTime.MinValue)
//                .WithEndDate(DateTime.MinValue);

//            //act
//            await _sut.GetPagedBooks(pageId, pageSize, filter);
//        }

//        //TC-I-8
//        [TestMethod]
//        public async Task InsertBook_TCI8_HappyPath()
//        {
//            //arrange
//            const int pageId = 0;
//            const int pageSize = 1;

//            var filter = new BookCatalogFilter()
//                .WithLoanability(false)
//                .WithAuthor(GenerateRandomString(15))
//                .WithSubject(string.Empty)
//                .WithTitle(string.Empty)
//                .WithIsbn(GenerateRandomString(15))
//                .WithStartDate(DateTime.Now)
//                .WithEndDate(DateTime.MaxValue);

//            //act
//            var result = await _sut.GetPagedBooks(pageId, pageSize, filter);

//            //assert
//            Assert.IsNotNull(result);
//            Assert.IsTrue(result.Count(b => true).Equals(0));
//        }

//        //TC-I-9
//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException))]  
//        public async Task InsertBook_TCI9_ExceptionalCase()
//        {
//            //arrange
//            const int pageId = -1;
//            const int pageSize = 0;

//            var filter = new BookCatalogFilter()
//                .WithLoanability(true)
//                .WithAuthor(GenerateRandomString(15))
//                .WithSubject(string.Empty)
//                .WithTitle(GenerateRandomString(15))
//                .WithIsbn(GenerateRandomString(21))
//                .WithStartDate(DateTime.MinValue)
//                .WithEndDate(DateTime.MaxValue);

//            //act
//            await _sut.GetPagedBooks(pageId, pageSize, filter);
//        }

//        //TC-I-10
//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException))] 
//        public async Task InsertBook_TCI10_ExcptionalCase()
//        {
//            //arrange
//            const int pageId = -1;
//            const int pageSize = 1;

//            var filter = new BookCatalogFilter()
//                .WithLoanability(true)
//                .WithAuthor(GenerateRandomString(15))
//                .WithSubject(string.Empty)
//                .WithTitle(GenerateRandomString(101))
//                .WithIsbn(GenerateRandomString(15))
//                .WithStartDate(DateTime.Now)
//                .WithEndDate(DateTime.MaxValue);

//            //act
//            await _sut.GetPagedBooks(pageId, pageSize, filter);
//        }

//        //TC-I-11
//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException))]
//        public async Task InsertBook_TCI11_ExcptionalCase()
//        {
//            //arrange
//            const int pageId = -1;
//            const int pageSize = 0;

//            var filter = new BookCatalogFilter()
//                .WithLoanability(false)
//                .WithAuthor(string.Empty)
//                .WithSubject(GenerateRandomString(15))
//                .WithTitle(string.Empty)
//                .WithIsbn(GenerateRandomString(21))
//                .WithStartDate(DateTime.MaxValue)
//                .WithEndDate(DateTime.MinValue);

//            //act
//            await _sut.GetPagedBooks(pageId, pageSize, filter);
//        }

//        //TC-I-12
//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException))]
//        public async Task InsertBook_TCI12_ExcptionalCase()
//        {
//            //arrange
//            const int pageId = -1;
//            const int pageSize = 0;

//            var filter = new BookCatalogFilter()
//                .WithLoanability(false)
//                .WithAuthor(GenerateRandomString(31))
//                .WithSubject(GenerateRandomString(15))
//                .WithTitle(GenerateRandomString(15))
//                .WithIsbn(GenerateRandomString(15))
//                .WithStartDate(DateTime.Now)
//                .WithEndDate(DateTime.MinValue);

//            //act
//            await _sut.GetPagedBooks(pageId, pageSize, filter);
//        }

//        //TC-I-13
//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException))]
//        public async Task InsertBook_TCI13_ExcptionalCase()
//        {
//            //arrange
//            const int pageId = -1;
//            const int pageSize = 1;

//            var filter = new BookCatalogFilter()
//                .WithLoanability(false)
//                .WithAuthor(GenerateRandomString(31))
//                .WithSubject(string.Empty)
//                .WithTitle(GenerateRandomString(15))
//                .WithIsbn(string.Empty)
//                .WithStartDate(DateTime.MinValue)
//                .WithEndDate(DateTime.Now);

//            //act
//            await _sut.GetPagedBooks(pageId, pageSize, filter);
//        }

//        //TC-I-14
//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException))]
//        public async Task InsertBook_TCI14_ExcptionalCase()
//        {
//            //arrange
//            const int pageId = -1;
//            const int pageSize = 1;

//            var filter = new BookCatalogFilter()
//                .WithLoanability(true)
//                .WithAuthor(GenerateRandomString(15))
//                .WithSubject(GenerateRandomString(31))
//                .WithTitle(string.Empty)
//                .WithIsbn(string.Empty)
//                .WithStartDate(DateTime.Now)
//                .WithEndDate(DateTime.MaxValue);

//            //act
//            await _sut.GetPagedBooks(pageId, pageSize, filter);
//        }


//        //TC-I-15
//        [TestMethod]
//        public async Task InsertBook_TCI15_HappyPath()
//        {
//            //arrange
//            const int pageId = 0;
//            const int pageSize = 1;

//            var filter = new BookCatalogFilter()
//                .WithLoanability(false)
//                .WithAuthor(string.Empty)
//                .WithSubject(GenerateRandomString(31))
//                .WithTitle(GenerateRandomString(15))
//                .WithIsbn(GenerateRandomString(21))
//                .WithStartDate(DateTime.Now)
//                .WithEndDate(DateTime.MinValue);

//            //act
//            var result = await _sut.GetPagedBooks(pageId, pageSize, filter);

//            //assert
//            Assert.IsNotNull(result);
//            Assert.IsTrue(result.Count(b => true).Equals(0));
//        }

//        #region Helpers

//        public static string GenerateRandomString(int length)
//        {
//            var random = new Random();

//            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
//            return new string(Enumerable.Repeat(chars, length)
//              .Select(s => s[random.Next(s.Length)]).ToArray());
//        }

//        #endregion
//    }
//}
