using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Case.Core.Entity;
using Case.Core.Repository;

namespace Case.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var book = new Book
            {
                Isbn = "thisistest",
                Title = "test",
                Author = "test",
                Binding = "test",
                Loanable = true,
                Description = "test",
                Edition = "1",
                Language = "test",
                Subject = "test"
            };
            //var bookRepository = new BookRepository();
            
            //var result = bookRepository.InsertAsync(book);

            return View("Index");
        }
    }
}