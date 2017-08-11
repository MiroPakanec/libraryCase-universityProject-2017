using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Case.Core.Entity;
using Case.Core.Facade.Interfaces;
using Case.Core.Model;
using Case.Core.Utils;
using Case.FrontEnd;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Case.Controllers
{
    [RoutePrefix("Account")]
    public class AccountController : Controller
    {
        private MemberManager _memberManager;
        private MemberSignInManager _memberSignInManager;
        private RoleManager<Role> _roleManager;

        public AccountController(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        [Route("Login")]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // TODO
            var result = await SignInManager.PasswordSignInAsync(model.Ssn, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    // Screw that -> return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        [HttpGet]
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("Register")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByNameAsync(Roles.NotVerifiedMember);
                var user = new Member { UserName = model.Ssn, Campus = "Sofiendalsvej", FirstName = "Bob", LastName = "Bobby", RoleId = role.Id };
                
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, Roles.NotVerifiedMember);
                    await SignInManager.PasswordSignInAsync(model.Ssn, model.Password, false, false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public MemberSignInManager SignInManager
        {
            get
            {
                return _memberSignInManager ?? HttpContext.GetOwinContext().Get<MemberSignInManager>();
            }
            private set
            {
                _memberSignInManager = value;
            }
        }

        public MemberManager UserManager
        {
            get
            {
                return _memberManager ?? HttpContext.GetOwinContext().GetUserManager<MemberManager>();
            }
            private set
            {
                _memberManager = value;
            }
        }

    }
}