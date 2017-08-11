using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Utils;
using Case.SystemTest.Setup;
using Dapper;
using DataAccess.EntityMaps.Book;
using DataAccess.EntityMaps.BookCopy;
using DataAccess.EntityMaps.Member;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Case.SystemTest.TestStepLibrary.Database.Table
{
    internal class Book : TableBase
    {
        internal VerificationTestStep ExistsWithAvailableCopy(string isbn)
        {
            return new VerificationTestStep(driver =>
            {
                const string query = "Select * from dbo.Book_copy where [BookIsbn] = @BookIsbn";

                using (var con = new SqlConnection(ConnectionString))
                {
                    var result = con.Query<BookCopyMap>(query, new {BookIsbn = isbn}).First();
                    Assert.IsNotNull(result, $"Book copy with isbn {isbn} was not found in the database.");
                    Assert.IsTrue(result.Available,
                        $"Book copy with isbn {isbn} is not available.");
                }
            });
        }

        internal VerificationTestStep CopyIsNotAvailable(string isbn)
        {
            return new VerificationTestStep(driver =>
            {
                const string query = "Select * from dbo.Book_copy where [BookIsbn] = @BookIsbn";

                using (var con = new SqlConnection(ConnectionString))
                {
                    var result = con.Query<BookCopyMap>(query, new { BookIsbn = isbn }).First();
                    Assert.IsNotNull(result, $"Book copy with isbn {isbn} was not found in the database.");
                    Assert.IsFalse(result.Available,
                        $"Book copy with isbn {isbn} should be unavailable.");
                }
            });
        }

        internal ActionTestStep CreateBookWithBookCopy(string isbn)
        {
            return new ActionTestStep(driver =>
            {
                const string query =
                    "INSERT INTO dbo.Book ([Isbn], [Author], [Binding], [Can-loan], [Description], [Edition], [Language], [Subject], [Title]) " +
                    "VALUES (@Isbn, 'Author', 'Binding', 'true', 'Description', 'Edition', 'Language', 'Subject', 'Title');" +
                    "INSERT INTO dbo.Book_copy([Book-isbn], [Available]) " +
                    "VALUES (@Isbn, 'true'); ";
                using (var con = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        con.Execute(query, new { Isbn = isbn, BookIsbn = isbn });
                    }
                    catch (Exception e)
                    {                        
                        throw e;
                    }
                }
            });
        }

        internal ActionTestStep RemoveBookAndCopy(string isbn)
        {
            return new ActionTestStep(driver =>
            {
                const string query =
                    "DELETE FROM dbo.Book_copy WHERE [BookIsbn] = @BookIsbn;" +
                    "Delete From dbo.Book WHERE [Isbn] = @Isbn";
                using (var con = new SqlConnection(ConnectionString))
                {
                    con.Execute(query, new {BookIsbn = isbn, Isbn = isbn});
                }
            });
        }

        internal VerificationTestStep BookAndCopyDoesNotExist(string isbn)
        {
            return new VerificationTestStep(driver =>
            {
                const string query = "Select * from dbo.Book where [Isbn] = @Isbn";
                using (var con = new SqlConnection(ConnectionString))
                {
                    var result = con.Query<BookMap>(query, new {Isbn = isbn});
                    Assert.IsTrue(result == null || !result.Any());
                }
            });
        }

    }
}
