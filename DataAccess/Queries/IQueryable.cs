using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EntityMaps;

namespace DataAccess.Queries
{
    public interface IQueryable
    {
        Task<bool> InsertOneAsync(IMap map);
        Task<bool> UpdateOneAsync(IMap map);
        Task<bool> InsertOrUpdateAsync(IMap map);
        Task<bool> DeleteOneAsync(IMap map);

        Task<int> Count();

        Task<IEnumerable<IMap>> SelectAllAsync();
    }
}
