using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Case.Core.Entity;
using Case.Core.Facade.Interfaces;
using Case.Core.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Case.Providers;
using Case.Results;

namespace Case.Controllers
{
    [RoutePrefix("Account")]
    public class AccountController : ApiController
    {
        ///////////////////////////////////////////////////
        // Login/Logout are not needed most likely
        // Since we are using token based authorization
        // Which is kind of logging in
        ///////////////////////////////////////////////////
        
         
        private readonly IMemberFacade _facade;

        public AccountController(IMemberFacade facade)
        {
            _facade = facade;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var member = new Member() {};// TODO Later when fields added

            var result = await _facade.RegisterMember(member);

            if (!result)
            {
                // Creating user in db failed, panic and return the best error known
                return InternalServerError();
            }

            return Ok();
        }
    }
}
;