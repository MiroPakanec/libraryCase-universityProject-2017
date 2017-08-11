using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Facade;
using Case.Core.Repository.Interface;
using Case.Test.Facade.Extensions;
using Case.Test.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using static Case.Test.TestUtils.BookCopyTestUtils;

namespace Case.Test.Facade
{
    [TestClass]
    public class BookCopyFacadeTest
    {
        private BookCopyFacade _facade;
        private IBookCopyRepository _bookCopyRepository;
        private Mock<IBookCopyRepository> _bookCopyRepositoryMock;

        [TestInitialize]
        public void Setup()
        {
            _bookCopyRepositoryMock = new Mock<IBookCopyRepository>();
            _bookCopyRepositoryMock
                .WithValidInsert()
                .WithValidDelete()
                .WithValidUpdate()
                .WithValidGet()
                .WithValidGetByIsbn()
                .WithValidInserOrUpdate();
            _bookCopyRepository = _bookCopyRepositoryMock.Object;
            _facade = new BookCopyFacade(_bookCopyRepository);
        }

        #region AddBookCopy

        [TestMethod]
        public async Task ValidBookCopyShouldBeAdded()
        {
            var bookCopy = GenerateValidBookCopy();
            bool result = await _facade.AddBookCopyAsync(bookCopy);
            _bookCopyRepositoryMock.Verify(x => x.InsertAsync(bookCopy), Times.Once);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Isbn cannot be invalid")]
        public async Task InvalidBookCopyShouldNotBeAdded()
        {
            var bookCopy = GenerateInvalidBookCopy();
            bool result = await _facade.AddBookCopyAsync(bookCopy);
            _bookCopyRepositoryMock.Verify(x => x.InsertAsync(It.IsAny<BookCopy>()), Times.Never);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "BookCopy cannot be null")]
        public async Task NullBookCopyShouldNotBeAdded()
        {
            BookCopy bookCopy = null;
            bool result = await _facade.AddBookCopyAsync(bookCopy);
            _bookCopyRepositoryMock.Verify(x => x.InsertAsync(It.IsAny<BookCopy>()), Times.Never);
            Assert.IsFalse(result);
        }

        #endregion

        #region UpdateBookCopy

        [TestMethod]
        public async Task ValidBookCopyShouldBeUpdated()
        {
            var bookCopy = GenerateValidBookCopy();
            bool result = await _facade.UpdateBookCopyAsync(bookCopy);
            _bookCopyRepositoryMock.Verify(x => x.InsertOrUpdateAsync(bookCopy), Times.Once);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Isbn cannot be invalid")]
        public async Task InvalidBookCopyShouldNotBeUpdated()
        {
            var bookCopy = GenerateInvalidBookCopy();
            bool result = await _facade.UpdateBookCopyAsync(bookCopy);
            _bookCopyRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<BookCopy>()), Times.Never);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "BookCopy cannot be null")]
        public async Task NullBookCopyShouldNotBeUpdated()
        {
            BookCopy bookCopy = null;
            bool result = await _facade.UpdateBookCopyAsync(bookCopy);
            _bookCopyRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<BookCopy>()), Times.Never);
            Assert.IsFalse(result);
        }

        #endregion

        #region RemoveBookCopy

        [TestMethod]
        public async Task ValidBookCopyShouldBeRemoved()
        {
            var bookCopy = GenerateValidBookCopy();
            bool result = await _facade.RemoveBookCopyAsync(bookCopy);
            _bookCopyRepositoryMock.Verify(x => x.DeleteAsync(bookCopy), Times.Once);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Isbn cannot be invalid")]
        public async Task InvalidBookCopyShouldNotBeRemoved()
        {
            var bookCopy = GenerateInvalidBookCopy();
            bool result = await _facade.RemoveBookCopyAsync(bookCopy);
            _bookCopyRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<BookCopy>()), Times.Never);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "BookCopy cannot be null")]
        public async Task NullBookCopyShouldNotBeRemoved()
        {
            BookCopy bookCopy = null;
            bool result = await _facade.RemoveBookCopyAsync(bookCopy);
            _bookCopyRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<BookCopy>()), Times.Never);
            Assert.IsFalse(result);
        }

        #endregion

        #region FindBookByIsbn
        [TestMethod]
        public async Task ValidIsbnShouldReturnValidBookCopy()
        {
            var isbn = GenerateValidIsbn13();
            var result = await _facade.GetBookCopiesFromIsbn(isbn);
            _bookCopyRepositoryMock.Verify(x => x.GetByIsbnAsync(isbn), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Isbn cannot be invalid")]
        public async Task InvalidIsbnShouldReturnNull()
        {
            var isbn = "isbn";
            var result = await _facade.GetBookCopiesFromIsbn(isbn);
            _bookCopyRepositoryMock.Verify(x => x.GetByIsbnAsync(It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Isbn cannot be null or invalid")]
        public async Task NullIsbnShouldReturnNull()
        {
            string isbn = null;
            var result = await _facade.GetBookCopiesFromIsbn(isbn);
            _bookCopyRepositoryMock.Verify(x => x.GetByIsbnAsync(It.IsAny<string>()), Times.Never);
        }

        #endregion
    }
}
