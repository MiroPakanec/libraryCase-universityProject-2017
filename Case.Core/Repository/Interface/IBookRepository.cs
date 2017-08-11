using System.Collections.Generic;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Repository.Interface;

namespace Case.Core.Repository.Interface
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<Book> GetByIsbnAsync(string isbn);
        Task<Book> GetByDescription(string description);
        Task<string> GetDescription(string isbn);
        Task<IEnumerable<Book>> GetPagedBooks(int pageId, int pageSize);
        Task<int> GetBookCount();
        Task<Book> GetByLoans(int loans);
        Task<Book> GetByAuthor(int loans, string author);
    }
}