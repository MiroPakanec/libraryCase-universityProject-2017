using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Case.Core.Facade.Interfaces;
using Case.Core.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Case.FrontEnd.Controllers
{
    [RoutePrefix("book")]
    public class BookController : Controller
    {
        private readonly IBookFacade _facade;
        private MemberManager _memberManager;

        public BookController(IBookFacade facade)
        {
            _facade = facade;
        }

        [Route("{page?}")]
        public async Task<ActionResult> Index(int? page, int? pageSize = 10)
        {
            var pageIndex = (page ?? 1) - 1;

            var bookModel = await _facade.GetPagedBookCatalogModel(pageIndex, pageSize.Value);

            return View(bookModel);
        }


        [Route("{isbn}/description")]
        public async Task<ActionResult> Description(string isbn)
        {
            ViewBag.description = await _facade.GetBookDescription(isbn);
            return View();
        }
        [Authorize(Roles = Roles.VerifiedMember + "," + Roles.Teacher)]
        [Route("cart")]
        public async Task<ActionResult> Cart(string isbns=null)
        {
            if (isbns == null)
            {
                return View("CartNoModel");
            }
            else
            {
                var model = await _facade.GetBookCatalogItemModelsByIsbns(isbns.Split(','));
                return View("CartWithModel", model);
            }
           // return View(isbns == null ? "CartNoModel" : "CartWithModel");
        }

        [Authorize(Roles = Roles.VerifiedMember + "," + Roles.Teacher)]
        [Route("rent/{isbns}")]
        public async Task<ActionResult> Rent(string isbns)
        {
            var isbnList = isbns.Split(',');
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var result = await _facade.RentBooks(user.Ssn, isbnList);
            return View(result);
        }

        public MemberManager UserManager => _memberManager ?? HttpContext.GetOwinContext().GetUserManager<MemberManager>();
    }
}