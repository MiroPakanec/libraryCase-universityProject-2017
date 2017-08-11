using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataAccess.EntityMaps;
using DataAccess.Helpers;
using DataAccess.EntityMaps.Wishlist;

namespace DataAccess.Queries.Wishlist
{
    public class WishlistAccess : IWishlistAccess
    {
        private static readonly string ConString = Utilities.ConnectionStrings.Local;

        public async Task<bool> InsertOneAsync(IMap wishlistBook)
        {
            const string query =
                "INSERT INTO dbo.Wishlist ([Id], [ReasonToAcquire]) " +
                "VALUES (@Id, @Reason) ";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.ExecuteAsync(query, AllParameters(wishlistBook as IWishlistMap));
                    return result == 1;
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<bool> DeleteOneAsync(IMap wishlistBook)
        {
            const string query = "Delete from dbo.Wishlist Where Id = @Id";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.ExecuteAsync(query, PrimaryParameters(wishlistBook as IWishlistMap));
                    return result == 1;
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

        public async Task<bool> UpdateOneAsync(IMap wishlistBook)
        {
            const string query =
                "Update dbo.Wishlist SET [ReasonToAcquire] = @Reason WHERE Id = @Id";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.ExecuteAsync(query, AllParameters(wishlistBook as IWishlistMap));
                    return result == 1;
                }
                catch (Exception)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }


        //TODO: Stored procedure
        public async Task<bool> InsertOrUpdateAsync(IMap wishlistBook)
        {
            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result =
                        await
                            con.ExecuteAsync("[spInsertUpdateWishlistBook]", AllParameters(wishlistBook as IWishlistMap),
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
            const string query = "Select * from dbo.Wishlist";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.QueryAsync<WishlistMap>(query);
                    return result.ToList();
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<IMap> SelectWithIdAsync(int wishlistBookId)
        {
            const string query = "Select * from dbo.Wishlist Where [Id] = @Id";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.QueryAsync<WishlistMap>(query, new { Id = wishlistBookId });
                    return result.First();
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<bool> DeleteWithIdAsync(int wishlistBookId)
        {
            const string query = "Delete from dbo.Wishlist Where Id = @Id";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.ExecuteAsync(query, new { Id = wishlistBookId });
                    return result == 1;
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        #region Helpers

        private static object AllParameters(IWishlistMap book)
        {
            return new
            {
                book.Id,
                book.ReasonToAcquire,
            };
        }

        private static object PrimaryParameters(IWishlistMap book)
        {
            return new
            {
                book.Id,
            };
        }

        #endregion

    }
}
