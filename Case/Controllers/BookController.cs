using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Case.Core.Entity;
using Case.Core.Facade.Interfaces;
using System.Configuration;
using Case.Core.Model;

namespace Case.Controllers
{
    [System.Web.Http.RoutePrefix("books")]
    public class BookController : ApiController
    {
        private readonly IBookFacade _facade;

        public BookController(IBookFacade facade)
        {
            _facade = facade;
        }

        [Route("catalog/{pageId?}")]
        public async Task<BookCatalogModel> Catalog(int? pageId = 1, [FromUri] int? pageSize = 2)
        {
            var pageIndex = (pageId ?? 1) - 1;
            return await _facade.GetPagedBookCatalogModel(pageIndex, pageSize.Value);
        }

        [Authorize]
        [Route("{isbn}")]
        public async Task<Book> GetByIsbn(string isbn) 
        {
            return await _facade.FindBookByIsbnAsync(isbn);
        }

        public async Task<bool> Update(Book book)
        {
            return await _facade.UpdateBookAsync(book);
        }

        public async Task<bool> Delete(Book book)
        {
            return await _facade.RemoveBookAsync(book);
        }

        public async Task<Book> GetByDescription(string description)
        {
            return await _facade.FindBookByDescriptionAsync(description);
        }

        [Route("details/{isbn}")]
        public async Task<string> GetDescriptionByIsbn(string isbn )
        {
            return await _facade.GetBookDescription(isbn);
        }

        [Route("rent")]
        [HttpPost]
        public async Task<RentAttemptModel> Rent(RentRequestModel model)
        {
            return await _facade.RentBooks(model.Ssn, model.Isbns);
        }
    }

    public class RentRequestModel
    {
        public virtual string Ssn { get; set; }
        public virtual IEnumerable<string> Isbns { get; set; }
    }
}