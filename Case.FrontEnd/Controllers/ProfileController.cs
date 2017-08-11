using Case.Core.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using Case.Core.Utils;

namespace Case.FrontEnd.Controllers
{
    // Shouldn't all this functionality just be in AccountController? It kind of overlaps.
    // + usually controller name and route are the same/similar
    [RoutePrefix("user")]
    public class ProfileController : Controller
    {
         private readonly MemberFacade _facade;

         public ProfileController(MemberFacade facade)
         {
             _facade = facade;
         }
         // Stop blindly copying shit.
         // GET: MvcBook
         public ActionResult Index()
         {
             return View();
         }

         [Route("{id}")]
         [Authorize] // Should this be allowed to access to anyone with any account? If not, specify the role.
         public async Task<ActionResult> GetUser(string id)
         {
             ViewBag.user = await _facade.FindByIdAsync(id);
             return View();
         }

         [Authorize(Roles = Roles.Librarian)]
         [Route("verify/{pageId?}")]
         public async Task<ActionResult> VerifyMembers(int? pageId, string returnUrl, int? pageSize = 10)
         {
             var pageIndex = (pageId ?? 1) - 1;

             var model = await _facade.FindWithRoleNamePagedModel(Roles.NotVerifiedMember, pageIndex, pageSize.Value);

             ViewBag.ReturnUrl = returnUrl;

             return View(model);
         }

        [Authorize(Roles = Roles.Librarian)]
        [Route("verifyMember/{ssn}")]
        public async Task<ActionResult> VerifyMember(string ssn, string returnUrl)
        {
            await _facade.VerifyMember(ssn);
            return RedirectToLocal(returnUrl);
           // TODO Maybe check whether that was successful or not and show error instead of redirect if it has failed
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("VerifyMembers", "Profile");
        }
    }
} 