using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Model;

namespace Case.Core.Facade.Interfaces
{
    public interface IBookCopyFacade
    {
        Task<bool> AddBookCopyAsync(BookCopy item);
        Task<bool> UpdateBookCopyAsync(BookCopy item);
        Task<bool> RemoveBookCopyAsync(BookCopy item);
        Task<bool> RemoveBookCopyAsync(int id);
        Task<bool> RemoveBookCopyAsync(string isbn);
        Task<List<BookCopy>> GetBookCopiesFromIsbn(string isbn);
        Task<BookCopy> GetBookCopyById(int id);
    }
}
