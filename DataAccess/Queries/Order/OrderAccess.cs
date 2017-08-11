using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Order;
using DataAccess.Helpers;

namespace DataAccess.Queries.Order
{
    public class OrderAccess : IOrderAccess
    {
        private static readonly string ConString = Utilities.ConnectionStrings.Local;
        public async Task<bool> InsertOneAsync(IMap map)
        {
            return (await InsertWithIdReturn(map as IOrderMap)) > 0;
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

        public async Task<int> InsertWithIdReturn(IMap orderMap)
        {
            const string query = // For testing, in db these arent implemented yet
                "INSERT INTO [Order] ( [MemberSsn], [Created]) " +
                "VALUES (@MemberSsn, @Created)" +
                "SELECT CAST(SCOPE_IDENTITY() as int)"; // This works for identity() only, but in our case, our PK is identity

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var map = orderMap as IOrderMap;
                    var result = (await con.QueryAsync<int>(query,
                        new {MemberSsn = map.MemberSsn, Created = map.Created.Date.ToString("yyyy-MM-dd HH:mm:ss")})).Single();
                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<IOrderMap> GetById(int id)
        {
            const string query = "Select [Id],[MemberSsn], [Created] From dbo.Order Where [Id] = @Id";
            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.QueryAsync<IOrderMap>(query, new {Id = id});
                    return result.FirstOrDefault();
                }
                catch (Exception e)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }
        private static object AllParameters(IOrderMap order)
        {
            var Created = order.Created.Date.ToString("yyyy-MM-dd HH:mm:ss");
            return new
            {
                order.Id,
                order.MemberSsn,
                Created
            };
        }

    }
}
