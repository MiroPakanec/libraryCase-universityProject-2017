using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Member;
using DataAccess.Helpers;

namespace DataAccess.Queries.Role
{
    public class RoleAccess : IRoleAccess
    {
        private static readonly string ConString = Utilities.ConnectionStrings.Local;
        public async Task<bool> InsertOneAsync(IMap map)
        {
            const string query =
                "INSERT INTO dbo.Role ([Id], [Name]) " +
                "VALUES (@Id, @Name) ";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.ExecuteAsync(query, AllParameters(map as IRoleMap));
                    return result == 1;
                }
                catch(Exception e)
                {
                    throw new Exception("Unexpected Database Error");
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

        private static object AllParameters(IRoleMap role)
        {
            return new
            {
               role.Id,
               role.Name
            };
        }
    }
}
