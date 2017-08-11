using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.EntityMaps;

namespace DataAccess.Queries.BookCopy
{
    public interface IBookCopyAccess : IQueryable
    {
        Task<bool> DeleteWithIdAsync(int id);
        Task<bool> DeleteWithIsbnAsync(string isbn);

        Task<IEnumerable<IMap>> GetByIsbnAsync(string isbn);
        Task<IMap> GetByIdAsync(int id);
    }
}
