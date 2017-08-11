using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.BookCopy;
using DataAccess.Helpers;

namespace DataAccess.Queries.BookCopy
{
    public class BookCopyAccess : IBookCopyAccess
    {
        private static readonly string ConString = Utilities.ConnectionStrings.Local;

        public async Task<bool> InsertOneAsync(IMap bookCopy)
        {
            const string query =
                "INSERT INTO dbo.Book_copy ([Id], [BookIsbn], [Available]) " +
                "VALUES (@Id, @BookIsbn, @Available) ";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.ExecuteAsync(query, ParameterizeAll(bookCopy as IBookCopyMap));
                    return result == 1;
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<bool> DeleteWithIdAsync(int id)
        {
            const string query = "Delete from dbo.Book_Copy Where Id = @Id";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.ExecuteAsync(query, new { Id = id });
                    return result == 1;
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<bool> DeleteWithIsbnAsync(string isbn)
        {
            const string query = "Delete from dbo.Book_Copy Where [BookIsbn] = @Isbn";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.ExecuteAsync(query, new { Isbn = isbn });
                    return result >= 1;
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<bool> UpdateOneAsync(IMap bookCopy)
        {
            const string query =
                "Update dbo.Book_Copy SET [BookIsbn] = @BookIsbn, [Available] = @Available WHERE Id = @Id";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.ExecuteAsync(query, ParameterizeAll(bookCopy as IBookCopyMap));
                    return result == 1;
                }
                catch (Exception e)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<bool> InsertOrUpdateAsync(IMap bookCopy)
        {
            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result =
                        await
                            con.ExecuteAsync("[spInsertUpdateBookCopy]", ParameterizeAll(bookCopy as IBookCopyMap),
                                commandType: CommandType.StoredProcedure);

                    return result == 1;
                }
                catch (Exception)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<bool> DeleteOneAsync(IMap map)
        {
            const string query = "Delete from dbo.Book_Copy Where [BookIsbn] = @Isbn";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.ExecuteAsync(query, ParameterizePrimary(map as IBookCopyMap));
                    return result >= 1;
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IMap>> GetByIsbnAsync(string isbn)
        {
            const string query = "Select * from dbo.Book_Copy where [BookIsbn] = @Isbn";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.QueryAsync<BookCopyMap>(query, new {Isbn = isbn});
                    return result.ToList();
                }
                catch(Exception e)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<IMap> GetByIdAsync(int id)
        {
            const string query = "Select * from dbo.Book_Copy where Id = @Id";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.QueryAsync<BookCopyMap>(query, new { Id = id });
                    return result.First();
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<IEnumerable<IMap>> SelectAllAsync()
        {
            const string query = "Select * from dbo.Book_Copy";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.QueryAsync<BookCopyMap>(query);
                    return result.ToList();
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        #region Helpers


        private static object ParameterizeAll(IBookCopyMap bookCopy)
        {
            return new
            {
                bookCopy.Id,
                bookCopy.BookIsbn,
                bookCopy.Available
            };
        }

        private static object ParameterizePrimary(IBookCopyMap bookCopy)
        {
            return new
            {
                bookCopy.Id
            };
        }

        #endregion
    }
}
