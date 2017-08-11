using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Book;
using DataAccess.Helpers;

namespace DataAccess.Queries.Book
{
    public class BookAccess : IBookAccess
    {
        private static readonly string ConString = Utilities.ConnectionStrings.Local;

        public async Task<bool> InsertOneAsync(IMap book)
        {
            const string query =
                "INSERT INTO dbo.Book ([Isbn], [Author], [Binding], [Can-Loan], [Description], [Edition], [Language], [Subject], [Title]) " +
                "VALUES (@Isbn, @Author, @Binding, @CanLoan, @Description, @Edition, @Language, @Subject, @Title) ";
            
            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.ExecuteAsync(query, AllParameters(book as IBookMap));
                    return result == 1;
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }
        
        public async Task<bool> DeleteOneAsync(IMap map)
        {
            const string query = "Delete from dbo.Book Where Isbn = @Isbn";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.ExecuteAsync(query, PrimaryParameters(map as IBookMap));
                    return result == 1;
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<int> Count()
        {
            const string query = "Select Count([Isbn]) from [Book]";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    return await con.ExecuteScalarAsync<int>(query);
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<bool> UpdateOneAsync(IMap book)
        {
            const string query =
                "Update dbo.Book SET [Author] = @Author, [Binding] = @Binding, [Can-Loan] = @CanLoan, [Title] = @Title, " +
                "[Description] = @Description, [Edition] = @Edition, [Language] = @Language, [Subject] = @Subject WHERE Isbn = @Isbn";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.ExecuteAsync(query, AllParameters(book as IBookMap));
                    return result == 1;
                }
                catch (Exception)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<bool> InsertOrUpdateAsync(IMap book)
        {
            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result =
                        await
                            con.ExecuteAsync("[spInsertUpdateBook]", AllParameters(book as IBookMap),
                                commandType: CommandType.StoredProcedure);

                    return result == 1;
                }
                catch (Exception)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<IEnumerable<IMap>> SelectAllAsync()
        {
            const string query = "Select * from dbo.Book";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.QueryAsync<BookMap>(query);
                    return result.ToList();
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<IMap> SelectWithIdAsync(string id)
        {
            const string query = "Select * from dbo.Book Where [Isbn] = @Isbn";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.QueryAsync<BookMap>(query, new { Isbn = id });
                    return result.First();
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<string> SelectDescriptionWithId(string id)
        {
            const string query = "Select Description from dbo.Book Where [Isbn] = @Isbn";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.QueryAsync<BookMap>(query, new { Isbn = id });
                    return result.First().Description;
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<IEnumerable<CatalogueItemMap>> SearchCatalogue(int pageIndex, int pageSize, IMap bookCatalogueFilter)
        {
            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    return
                        await
                            con.QueryAsync<CatalogueItemMap>("[spSearchBookCatalogue]",
                                CatalogueFilterParameters(pageIndex, pageSize, bookCatalogueFilter as ICatalogueFilterMap),
                                        commandType: CommandType.StoredProcedure);
                }
                catch(Exception e)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<bool> DeleteWithIdAsync(string id)
        {
            const string query = "Delete from dbo.Book Where Isbn = @Isbn";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.ExecuteAsync(query, new { Isbn = id});
                    return result == 1;
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<IEnumerable<IMap>> SelectWithDescription(string desc)
        {
            const string query = "Select * from dbo.Book where [Description] like @Desc";
            var description = $"%{desc}%";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.QueryAsync<BookMap>(query, new { Desc = description });
                    return result.ToList();
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        #region Helpers

        private static object AllParameters(IBookMap book)
        {
            return new
            {
                book.ISBN,
                book.Author,
                book.Binding,
                CanLoan = book.Loanable,
                book.Description,
                book.Edition,
                book.Language,
                book.Subject,
                book.Title
            };
        }

        private static object PrimaryParameters(IBookMap book)
        {
            return new
            {
                book.ISBN,
            };
        }

        private static object CatalogueFilterParameters(int pageIndex, int pageSize, ICatalogueFilterMap filter)
        {
            return new
            {
                pageIndex,
                pageSize,
                filter.CanLoan,
                filter.Author,
                filter.Subject,
                filter.Title,
                filter.Isbn,
                filter.StartDate,
                filter.EndDate
            };
        }

        #endregion

    }
}
