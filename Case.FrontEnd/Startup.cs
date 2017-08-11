using System.Web.Mvc;
using System.Web.Security;
using Case.Core.Entity;
using Case.Core.Facade;
using Case.Core.Utils;
using Case.FrontEnd.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using Owin;
using Roles = Case.Core.Utils.Roles;

[assembly: OwinStartupAttribute(typeof(Case.FrontEnd.Startup))]
namespace Case.FrontEnd
{
    public partial class Startup
    {
        //private static readonly IUnityContainer _container = UnityHelpers
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //CreateRoles();
            
        }

        
    }
}
