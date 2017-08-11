using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Case.Test.TestUtils.BookCopyTestUtils;

namespace Case.Test.Utils
{
    [TestClass]
    public class BookUtilsTest
    {
        #region isbn13
        [TestMethod]
        public void ValidIsbn13ShouldReturnTrue()
        {
            var isbn = GenerateValidIsbn13();
            var result = BookUtils.VerifyIsbn(isbn);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InvalidIsbn13ShouldReturnFalse()
        {
            var isbn = GenerateInvalidIsbn13();
            var result = BookUtils.VerifyIsbn(isbn);
            Assert.IsFalse(result);
        }
        #endregion

        #region isbn10

        [TestMethod]
        public void ValidIsbn10ShouldReturnTrue()
        {
            var isbn = GenerateValidIsbn10();
            var result = BookUtils.VerifyIsbn(isbn);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InvalidIsbn10ShouldReturnFalse()
        {
            var isbn = GenerateInvalidIsbn10();
            var result = BookUtils.VerifyIsbn(isbn);
            Assert.IsFalse(result);
        }

        #endregion

        #region generalIsbn
        [TestMethod]
        public void NullIsbnShouldReturnFalse()
        {
            string isbn = null;
            var result = BookUtils.VerifyIsbn(isbn);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhitespaceIsbnShouldReturnFalse()
        {
            string isbn = "    ";
            var result = BookUtils.VerifyIsbn(isbn);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EmptyIsbnShouldReturnFalse()
        {
            string isbn = string.Empty;
            var result = BookUtils.VerifyIsbn(isbn);
            Assert.IsFalse(result);
        }
        #endregion
    }
}
