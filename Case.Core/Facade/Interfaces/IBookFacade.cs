using System.Collections.Generic;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Model;

namespace Case.Core.Facade.Interfaces
{
    public interface IBookFacade
    {
        Task<bool> AddBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book book);
        Task<bool> RemoveBookAsync(Book book);
        Task<Book> FindBookByIsbnAsync(string isbn);
        Task<Book> FindBookByDescriptionAsync(string description);
        Task<string> GetBookDescription(string isbn);
        Task<BookCatalogModel> GetPagedBookCatalogModel(int pageId, int pageSize);
        Task<int> GetBookCount();
        Task<RentAttemptModel> RentBooks(string ssn, IEnumerable<string> isbns);
        Task<BookCatalogItem> GetBookCatalogItemModelByIsbn(string isbn);
        Task<IEnumerable<BookCatalogItem>> GetBookCatalogItemModelsByIsbns(string[] isbns);
    }
}