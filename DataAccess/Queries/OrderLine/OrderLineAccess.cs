using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.OrderLine;
using DataAccess.Helpers;

namespace DataAccess.Queries.OrderLine
{
    public class OrderLineAccess : IOrderLineAccess
    {
        private static readonly string ConString = Utilities.ConnectionStrings.Local;
        public async Task<bool> InsertOneAsync(IMap map)
        {
            var query = "Insert Into dbo.Order_Line ([OrderId], [BookCopyId], [Returned], [Extended])" +
                        "Values(@OrderId, @BookCopyId, @Returned, @Extended) ";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.ExecuteAsync(query, AllParameters(map as IOrderLineMap));
                    return result == 1;
                }
                catch (Exception e)
                {
                    throw new Exception("Unexpected Database error");
                }
            }
        }

        public Task<bool> UpdateOneAsync(IMap map)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrUpdateAsync(IMap map)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOneAsync(IMap map)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IMap>> SelectAllAsync()
        {
            throw new NotImplementedException();
        }
        private static object AllParameters(IOrderLineMap order)
        {
            return new
            {
               order.OrderId,
               order.BookCopyId,
               order.Returned,
               order.Extended
            };
        }
    }
}
