using System;
using System.Linq;
using System.Threading.Tasks;
using Case.Core.Model;
using Case.Core.Model.Extensions;
using Case.IntegrationTest.TestUtils;
using DataAccess.EntityMaps.Book;
using DataAccess.EntityMaps.Book.Extensions;
using DataAccess.Queries.Book;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Case.IntegrationTest.DataAccess
{
    [TestClass]
    public class BookAccessTests
    {
        private BookAccess _sut;

        [TestInitialize]
        public void SetUp()
        {
            _sut = new BookAccess();
        }

        [TestCleanup]
        public void TearDown()
        {
            _sut = null;
        }

        //TC-I-1
        [TestMethod]
        public async Task SearchCatalogue_TCI1_HappyPath()
        {
            //arrange
            const int pageId = 0;
            const int pageSize = 5;

            var filter = new CatalogueFilterMap()
                .WithLoanability(false)
                .WithAuthor(string.Empty)
                .WithSubject(GenerateDummyString(31))
                .WithTitle(GenerateDummyString(101))
                .WithIsbn("0439139602")
                .WithStartDate(new DateTime(2000, 1, 1))
                .WithEndDate(new DateTime(2020, 1, 1));

            //act
            var result = await _sut.SearchCatalogue(pageId, pageSize, filter);

            //asser
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count(b => true) == 0);
        }

        //TC-I-2
        [TestMethod]
        public async Task SearchCatalogue_TCI2_HappyPath()
        {
            //arrange
            const int pageId = 0;
            const int pageSize = 5;

            var filter = new CatalogueFilterMap()
                .WithLoanability(false)
                .WithAuthor(string.Empty)
                .WithSubject(string.Empty)
                .WithTitle(GenerateDummyString(101))
                .WithIsbn(GenerateDummyString(21))
                .WithStartDate(DateTime.MaxValue)
                .WithEndDate(DateTime.MaxValue);

            //act
            var result = await _sut.SearchCatalogue(pageId, pageSize, filter);

            //asser
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count(b => true) == 0);
        }

        //TC-I-3
        [TestMethod]
        public async Task SearchCatalogue_TCI3_HappyPath()
        {
            //arrange
            const int pageId = 0;
            const int pageSize = 5;

            var filter = new CatalogueFilterMap()
                .WithLoanability(true)
                .WithAuthor("J.K. Rowling")
                .WithSubject(GenerateDummyString(31))
                .WithTitle(string.Empty)
                .WithIsbn(string.Empty)
                .WithStartDate(DateTime.MinValue)
                .WithEndDate(DateTime.MaxValue);

            //act
            var result = await _sut.SearchCatalogue(pageId, pageSize, filter);

            //asser
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count(b => true) == 0);
        }

        //TC-I-4
        [TestMethod]
        public async Task SearchCatalogue_TCI4_HappyPath()
        {
            //arrange
            const int pageId = 0;
            const int pageSize = 5;

            var filter = new CatalogueFilterMap()
                .WithLoanability(true)
                .WithAuthor("J.K. Rowling")
                .WithSubject("Fiction")
                .WithTitle("Harry Potter")
                .WithIsbn(string.Empty)
                .WithStartDate(new DateTime(2000, 1, 1))
                .WithEndDate(DateTime.MinValue);

            //act
            var result = await _sut.SearchCatalogue(pageId, pageSize, filter);

            //asser
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count(b => true) == 5);
        }

        //TC-I-5
        [TestMethod]
        public async Task SearchCatalogue_TCI5_HappyPath()
        {
            //arrange
            const int pageId = 0;
            const int pageSize = 5;

            var filter = new CatalogueFilterMap()
                .WithLoanability(true)
                .WithAuthor(GenerateDummyString(31))
                .WithSubject(string.Empty)
                .WithTitle(GenerateDummyString(101))
                .WithIsbn("0439139602")
                .WithStartDate(DateTime.MinValue)
                .WithEndDate(DateTime.MinValue);

            //act
            var result = await _sut.SearchCatalogue(pageId, pageSize, filter);

            //asser
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count(b => true) == 0);
        }

        //TC-I-6
        [TestMethod]
        public async Task SearchCatalogue_TCI6_HappyPath()
        {
            //arrange
            const int pageId = 0;
            const int pageSize = 5;

            var filter = new CatalogueFilterMap()
                .WithLoanability(true)
                .WithAuthor(GenerateDummyString(31))
                .WithSubject("Fiction")
                .WithTitle(string.Empty)
                .WithIsbn(GenerateDummyString(21))
                .WithStartDate(DateTime.MaxValue)
                .WithEndDate(new DateTime(2000, 1, 1));

            //act
            var result = await _sut.SearchCatalogue(pageId, pageSize, filter);

            //asser
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count(b => true) == 0);
        }

        //TC-I-7
        [TestMethod]
        public async Task SearchCatalogue_TCI7_HappyPath()
        {
            //arrange
            const int pageId = 0;
            const int pageSize = 5;

            var filter = new CatalogueFilterMap()
                .WithLoanability(false)
                .WithAuthor("J.K. Rownling")
                .WithSubject(string.Empty)
                .WithTitle(string.Empty)
                .WithIsbn(string.Empty)
                .WithStartDate(DateTime.MaxValue)
                .WithEndDate(DateTime.MinValue);

            //act
            var result = await _sut.SearchCatalogue(pageId, pageSize, filter);

            //asser
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count(b => true) == 0);
        }

        //TC-I-8
        [TestMethod]
        public async Task SearchCatalogue_TCI8_HappyPath()
        {
            //arrange
            const int pageId = 0;
            const int pageSize = 5;

            var filter = new CatalogueFilterMap()
                .WithLoanability(false)
                .WithAuthor(string.Empty)
                .WithSubject("Nonfiction")
                .WithTitle(string.Empty)
                .WithIsbn("0439139610")
                .WithStartDate(DateTime.MinValue)
                .WithEndDate(new DateTime(2030, 1, 1));

            //act
            var result = await _sut.SearchCatalogue(pageId, pageSize, filter);

            //asser
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count(b => true) == 1);
        }

        //TC-I-9
        [TestMethod]
        public async Task SearchCatalogue_TCI9_HappyPath()
        {
            //arrange
            const int pageId = 0;
            const int pageSize = 5;

            var filter = new CatalogueFilterMap()
                .WithLoanability(false)
                .WithAuthor("J.K. Rowling")
                .WithSubject("Fiction")
                .WithTitle("Harry Potter")
                .WithIsbn(GenerateDummyString(21))
                .WithStartDate(DateTime.MinValue)
                .WithEndDate(DateTime.MaxValue);

            //act
            var result = await _sut.SearchCatalogue(pageId, pageSize, filter);

            //asser
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count(b => true) == 0);
        }

        //TC-I-10
        [TestMethod]
        public async Task SearchCatalogue_TCI10_HappyPath()
        {
            //arrange
            const int pageId = 0;
            const int pageSize = 5;

            var filter = new CatalogueFilterMap()
                .WithLoanability(false)
                .WithAuthor("J.K. Rowling")
                .WithSubject(string.Empty)
                .WithTitle(GenerateDummyString(101))
                .WithIsbn(string.Empty)
                .WithStartDate(new DateTime(2000, 1, 1))
                .WithEndDate(new DateTime(2030, 1, 1));

            //act
            var result = await _sut.SearchCatalogue(pageId, pageSize, filter);

            //asser
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count(b => true) == 0);
        }


        //TC-I-11
        [TestMethod]
        public async Task SearchCatalogue_TCI11_HappyPath()
        {
            //arrange
            const int pageId = 0;
            const int pageSize = 5;

            var filter = new CatalogueFilterMap()
                .WithLoanability(true)
                .WithAuthor(string.Empty)
                .WithSubject(GenerateDummyString(31))
                .WithTitle(GenerateDummyString(101))
                .WithIsbn(GenerateDummyString(21))
                .WithStartDate(DateTime.MaxValue)
                .WithEndDate(DateTime.MinValue);

            //act
            var result = await _sut.SearchCatalogue(pageId, pageSize, filter);

            //asser
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count(b => true) == 0);
        }

        //TC-I-12
        [TestMethod]
        public async Task SearchCatalogue_TCI12_HappyPath()
        {
            //arrange
            const int pageId = 0;
            const int pageSize = 5;

            var filter = new CatalogueFilterMap()
                .WithLoanability(false)
                .WithAuthor("Simon Sinek")
                .WithSubject(string.Empty)
                .WithTitle("Leaders eat last")
                .WithIsbn("0439139610")
                .WithStartDate(DateTime.MaxValue)
                .WithEndDate(DateTime.MaxValue);

            //act
            var result = await _sut.SearchCatalogue(pageId, pageSize, filter);

            //asser
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count(b => true) == 1);
        }

        //TC-I-13
        [TestMethod]
        public async Task SearchCatalogue_TCI13_HappyPath()
        {
            //arrange
            const int pageId = 0;
            const int pageSize = 5;

            var filter = new CatalogueFilterMap()
                .WithLoanability(false)
                .WithAuthor(string.Empty)
                .WithSubject(GenerateDummyString(31))
                .WithTitle(string.Empty)
                .WithIsbn(GenerateDummyString(21))
                .WithStartDate(new DateTime(2000, 1, 1))
                .WithEndDate(DateTime.MaxValue);

            //act
            var result = await _sut.SearchCatalogue(pageId, pageSize, filter);

            //asser
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count(b => true) == 0);
        }

        //TC-I-14
        [TestMethod]
        public async Task SearchCatalogue_TCI14_HappyPath()
        {
            //arrange
            const int pageId = 0;
            const int pageSize = 5;

            var filter = new CatalogueFilterMap()
                .WithLoanability(true)
                .WithAuthor("J.K. Rowling")
                .WithSubject("Fiction")
                .WithTitle("Harry Potter")
                .WithIsbn(string.Empty)
                .WithStartDate(DateTime.MinValue)
                .WithEndDate(DateTime.MaxValue);

            //act
            var result = await _sut.SearchCatalogue(pageId, pageSize, filter);

            //asser
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count(b => true) == 5);
        }

        //TC-I-15
        [TestMethod]
        public async Task SearchCatalogue_TCI15_HappyPath()
        {
            //arrange
            const int pageId = 1;
            const int pageSize = 5;

            var filter = new CatalogueFilterMap()
                .WithLoanability(true)
                .WithAuthor("J.K. Rowling")
                .WithSubject("Fiction")
                .WithTitle("Harry Potter")
                .WithIsbn(string.Empty)
                .WithStartDate(DateTime.MinValue)
                .WithEndDate(DateTime.MaxValue);

            //act
            var result = await _sut.SearchCatalogue(pageId, pageSize, filter);

            //asser
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count(b => true) == 2);
        }

        //TC-I-16
        [TestMethod]
        [ExpectedException(typeof(Exception), "Unexpected Database Error")]
        public async Task SearchCatalogue_TCI16_ExceptionalCase()
        {
            //arrange
            const int pageId = -1;
            const int pageSize = 5;

            var filter = new CatalogueFilterMap()
                .WithLoanability(true)
                .WithAuthor("J.K. Rowling")
                .WithSubject("Fiction")
                .WithTitle("Harry Potter")
                .WithIsbn(string.Empty)
                .WithStartDate(DateTime.MinValue)
                .WithEndDate(DateTime.MaxValue);

            //act
            await _sut.SearchCatalogue(pageId, pageSize, filter);
        }

        //TC-I-17
        [TestMethod]
        [ExpectedException(typeof(Exception), "Unexpected Database Error")]
        public async Task SearchCatalogue_TCI17_ExceptionalCase()
        {
            //arrange
            const int pageId = 0;
            const int pageSize = 0;

            var filter = new CatalogueFilterMap()
                .WithLoanability(true)
                .WithAuthor("J.K. Rowling")
                .WithSubject("Fiction")
                .WithTitle("Harry Potter")
                .WithIsbn(string.Empty)
                .WithStartDate(DateTime.MinValue)
                .WithEndDate(DateTime.MaxValue);

            //act
            await _sut.SearchCatalogue(pageId, pageSize, filter);
        }

        //TC-I-18
        [TestMethod]
        public async Task SelectWithDescription_TCI18_HappyPath()
        {
            //arrange
            string description = GenerateDummyString(20);

            //act
            var result = await _sut.SelectWithDescription(description);

            //assert
            Assert.IsNotNull(result);
        }

        //TC-I-19
        [TestMethod]
        public async Task SelectWithDescription_TCI19_EmptyString()
        {
            //arrange
            string description = "";

            //act
            await _sut.SelectWithDescription(description);
        }

        //TC-I-20
        [TestMethod]
        public async Task SelectWithDescription_TCI20_NullString()
        {
            //arrange
            string description = null;

            //act
            await _sut.SelectWithDescription(description);
        }

        //TC-I-21
        [TestMethod]
        public async Task SelectAllAsync_TCI21_HappyPath()
        {
            //act
            var result = await _sut.SelectAllAsync();

            //assert
            Assert.IsNotNull(result);
        }

        //TC-I-22
        [TestMethod]
        public async Task Count_TCI22_HappyPath()
        {
            //act
            var result = await _sut.Count();

            //assert
            Assert.AreNotEqual(result, 0);
        }

        //TC-I-23
        [TestMethod]
        public async Task SelectDescriptionWithId_TCI23_HappyPath()
        {
            //arrange
            string id = "0439139600";

            //act
            var result = await _sut.SelectDescriptionWithId(id);

            //assert
            Assert.IsNotNull(result);
        }

        //TC-I-24
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task SelectDescriptionWithId_TCI24_EmptyId()
        {
            //arrange
            string id = "";

            //act
            var result = await _sut.SelectDescriptionWithId(id);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-25
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task SelectDescriptionWithId_TCI25_NullId()
        {
            //arrange
            string id = null;

            //act
            var result = await _sut.SelectDescriptionWithId(id);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-26
        [TestMethod]
        public async Task InsertOneAsync_TCI26_HappyPath()
        {
            //arrange
            var book = BookMapTestUtils.GenerateValidBookMap();
            

            //act
            var result = await _sut.InsertOneAsync(book);

            //assert
            Assert.IsTrue(result);
        }

        //TC-I-27
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task InsertOneAsync_TCI27_InvalidBookMap()
        {
            //arrange
            var book = BookMapTestUtils.GenerateValidBookMap();
            
            //act
            var result = await _sut.InsertOneAsync(book);

            //assert
            Assert.IsFalse(result);
        }

        //TC-I-28
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task InsertOneAsync_TCI28_NullBookMap()
        {
            //act
            var result = await _sut.InsertOneAsync(null);

            //assert
            Assert.IsFalse(result);
        }

        //TC-I-29
        [TestMethod]
        public async Task DeleteOneAsync_TCI29_HappyPath()
        {
            //arrange
            var book = BookMapTestUtils.GenerateValidBookMap();

            //act
            var result = await _sut.DeleteOneAsync(book);

            //assert
            Assert.IsTrue(result);
        }

        //TC-I-30
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task DeleteOneAsync_TCI30_InvalidBookMap()
        {
            //arrange
            var book = BookMapTestUtils.GenerateValidBookMap();

            //act
            var result = await _sut.DeleteOneAsync(book);

            //assert
            Assert.IsFalse(result);
        }

        //TC-I-31
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task DeleteOneAsync_TCI31_NullBookMap()
        {
            //act
            var result = await _sut.DeleteOneAsync(null);

            //assert
            Assert.IsFalse(result);
        }

        //TC-I-32
        [TestMethod]
        public async Task UpdateOneAsync_TCI32_HappyPath()
        {
            //arrange
            var book = BookMapTestUtils.GenerateValidBookMap();

            //act
            var result = await _sut.UpdateOneAsync(book);

            //assert
            Assert.IsTrue(result);
        }

        //TC-I-33
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task UpdateOneAsync_TCI33_InvalidBookMap()
        {
            //arrange
            var book = BookMapTestUtils.GenerateValidBookMap();

            //act
            var result = await _sut.UpdateOneAsync(book);

            //assert
            Assert.IsFalse(result);
        }

        //TC-I-34
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task DeleteOneAsync_TCI34_NullBookMap()
        {
            //act
            var result = await _sut.UpdateOneAsync(null);

            //assert
            Assert.IsFalse(result);
        }

        //TC-I-35
        [TestMethod]
        public async Task InsertOrUpdateAsync_TCI35_HappyPath()
        {
            //arrange
            var book = BookMapTestUtils.GenerateValidBookMap();

            //act
            var result = await _sut.InsertOrUpdateAsync(book);

            //assert
            Assert.IsTrue(result);
        }

        //TC-I-36
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task InsertOrUpdateAsync_TCI36_InvalidBookMap()
        {
            //arrange
            var book = BookMapTestUtils.GenerateValidBookMap();

            //act
            var result = await _sut.InsertOrUpdateAsync(book);

            //assert
            Assert.IsFalse(result);
        }

        //TC-I-37
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task InsertOrUpdateAsync_TCI37_NullBookMap()
        {
            //act
            var result = await _sut.InsertOrUpdateAsync(null);

            //assert
            Assert.IsFalse(result);
        }

        //TC-I-38
        [TestMethod]
        public async Task SelectWithId_TCI38_HappyPath()
        {
            //arrange
            string bookId = "0439139600";

            //act
            var result = await _sut.SelectWithIdAsync(bookId);

            //assert
            Assert.IsNotNull(result);
        }

        //TC-I-39
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task SelectWithId_TCI39_EmptyBookId()
        {
            //arrange
            var bookId = "";

            //act
            var result = await _sut.SelectWithIdAsync(bookId);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-40
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task SelectWithId_TCI40_NullBookId()
        {
            //act
            var result = await _sut.SelectWithIdAsync(null);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-41
        [TestMethod]
        [ExpectedException(typeof(Exception), "Relation with book copy")]
        public async Task DeleteWithId_TCI41_HappyPath()
        {
            //arrange
            string bookId = "0439139600";

            //act
            var result = await _sut.DeleteWithIdAsync(bookId);

            //assert
            Assert.IsNotNull(result);
        }

        //TC-I-42
        [TestMethod]
        public async Task DeleteWithId_TCI42_EmptyBookId()
        {
            //arrange
            var bookId = "";

            //act
            var result = await _sut.DeleteWithIdAsync(bookId);

            //assert
            Assert.IsNotNull(result);
        }

        //TC-I-43
        [TestMethod]
        public async Task DeleteWithId_TCI43_NullBookId()
        {
            //act
            var result = await _sut.DeleteWithIdAsync(null);

            //assert
            Assert.IsNotNull(result);
        }

        #region Helpers

        public static string GenerateDummyString(int length)
        {
            var random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion
    }
}
