using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;

namespace Case.Core.Repository.Interface
{
    public interface IBookCopyRepository : IBaseRepository<BookCopy>
    {
        Task<List<BookCopy>> GetByIsbnAsync(string isbn);
        Task<BookCopy> GetByIdAsync(int id);
        Task<IEnumerable<BookCopy>> GetAllAsync();
        Task<bool> DeleteWithIsbnAsync(string isbn);
    }
}
