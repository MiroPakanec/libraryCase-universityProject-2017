using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Case.Core.Entity;
using Case.Core.Facade.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Case
{
    public class MemberManager : UserManager<Member>
    {
        public MemberManager(IUserStore<Member> store) : base(store)
        {
        }

        public static MemberManager Create(IdentityFactoryOptions<MemberManager> options, IOwinContext context)
        {
            var store = (IUserStore<Member>) GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserStore<Member>));
            var manager = new MemberManager(store);
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<Member>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = false;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<Member>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    public class MemberSignInManager : SignInManager<Member, string>
    {
        public MemberSignInManager(MemberManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(Member user)
        {
            return user.GenerateUserIdentityAsync((MemberManager)UserManager);
        }

        public static MemberSignInManager Create(IdentityFactoryOptions<MemberSignInManager> options, IOwinContext context)
        {
            return new MemberSignInManager(context.GetUserManager<MemberManager>(), context.Authentication);
        }
    }
}