using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Case.Core.Repository.Interface;
using Case.Core.Repository;
using Case.Core.Facade.Interfaces;
using Case.Core.Facade;
using Case.Core.Entity;
using Case.Core.Mapper;
using Case.Core.Mapper.Book;
using Case.Core.Mapper.BookCopy;
using Case.Core.Model;
using DataAccess.EntityMaps.Book;
using Case.Core.Mapper.Member;
using DataAccess.Queries.Book;
using DataAccess.Queries.BookCopy;
using DataAccess.Queries.Member;
using Microsoft.AspNet.Identity;
using Case.Core.Mapper.Role;
using Case.Core.Utils;
using Case.Core.Identity;
using DataAccess.Queries.Role;
using Case.Core.Mapper.Order;
using Case.Core.Mapper.OrderLine;
using DataAccess.Queries.Order;
using DataAccess.Queries.OrderLine;

namespace Case.FrontEnd
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            //Other
            container.RegisterType<IMapper<Book>, BookMapper>();
            container.RegisterType<IMapper<BookCopy>, BookCopyMapper>();
            container.RegisterType<IMapper<Member>, MemberMapper>();
            container.RegisterType<IMapper<Role>, RoleMapper>();
            container.RegisterType<IMapper<Order>, OrderMapper>();
            container.RegisterType<IMapper<OrderLine>, OrderLineMapper>();

            container.RegisterType<IMapper<BookCatalogFilter>, CatalogueFilterMapper>();
            container.RegisterType<IDualMapper<Book, CatalogueItemMap>, CatalogueItemMapper>();

            //DAL
            container.RegisterType<IBookAccess, BookAccess>();
            container.RegisterType<IBookCopyAccess, BookCopyAccess>();
            container.RegisterType<IMemberAccess, MemberAccess>();
            container.RegisterType<IRoleAccess, RoleAccess>();
            container.RegisterType<IOrderAccess, OrderAccess>();
            container.RegisterType<IOrderLineAccess, OrderLineAccess>();

            // repositories
            container.RegisterType<IBookCopyRepository, BookCopyRepository>();
            container.RegisterType<IBookRepository, BookRepository>();
            container.RegisterType<IMemberRepository, MemberRepository>();
            container.RegisterType<IRoleRepository, RoleRepository>();
            container.RegisterType<IOrderRepository, OrderRepository>();
            container.RegisterType<IOrderLineRepository, OrderLineRepository>();

            // Facades
            container.RegisterType<IBookCopyFacade, BookCopyFacade>();
            container.RegisterType<IBookFacade, BookFacade>();
            container.RegisterType<IMemberFacade, MemberFacade>();
            container.RegisterType<IRoleFacade, RoleFacade>();

            //Stores
            container.RegisterType<IUserStore<Member>, MemberFacade>();
            container.RegisterType<IRoleStore<Role>, RoleFacade>();

            var roleManagerInstance = new CustomRoleManager((IRoleStore<Role>) container.Resolve(typeof(IRoleStore<Role>)));
            container.RegisterInstance(typeof(RoleManager<Role>), roleManagerInstance);

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            CreateRoles(container);

        }

        private static void CreateRoles(UnityContainer container)
        {

            var roleManager = (RoleManager<Role>) container.Resolve(typeof(RoleManager<Role>));
            

           // if (!roleManager.RoleExists(Roles.Librarian))
            //{
                var role = new Role() { Name = Roles.Librarian };
                roleManager.Create(role);

            //}

            //if (!roleManager.RoleExists(Roles.NotVerifiedMember))
            //{
                role = new Role() { Name = Roles.NotVerifiedMember };
                roleManager.Create(role);
            //}

            role = new Role() {Name = Roles.VerifiedMember};
            roleManager.Create(role);

            role = new Role(){Name = Roles.Teacher};
            roleManager.Create(role);
        }
    }
}