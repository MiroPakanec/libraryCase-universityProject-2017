using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Case.Core.Entity;
using Case.Core.Facade.Interfaces;
using Case.Core.Model;

namespace Case.Controllers
{
    public class BookCopyController : ApiController
    {
        private readonly IBookCopyFacade _facade;

        public BookCopyController(IBookCopyFacade facade)
        {
            _facade = facade;
        }

        public async Task<BookCopy> Get(int id/*For default routing, if this id, then the path is /api/book/{id}, if its anything else - /api/book?isbn={value}*/)
        {
            return await _facade.GetBookCopyById(id);
        }

        public async Task<bool> Post([FromBody] BookCopy copy)
        {
            return await _facade.UpdateBookCopyAsync(copy);
        }

        public async Task<bool> Delete(string isbnOrId)
        {
            if (isbnOrId.Contains("-"))
            {
                //ISBN
                return await _facade.RemoveBookCopyAsync(isbnOrId);
            }
            //Id
            int id = Int32.Parse(isbnOrId);
            return await _facade.RemoveBookCopyAsync(id);
        }
    }
}
