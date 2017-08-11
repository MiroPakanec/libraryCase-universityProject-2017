using Microsoft.Practices.Unity;
using System.Web.Http;
using Case.Core.Repository.Interface;
using Case.Core.Facade;
using Case.Core.Facade.Interfaces;
using Case.Core.Repository;
using Unity.WebApi;
using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Case.Core.Entity;
using Case.Core.Entity;
using Case.Core.Mapper;
using Case.Core.Mapper.Book;
using Case.Core.Mapper.BookCopy;
using Case.Core.Mapper.Member;
using DataAccess.Queries.Book;
using DataAccess.Queries.BookCopy;
using DataAccess.Queries.Member;

namespace Case
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Maps
            container.RegisterType<IMapper<Book>, BookMapper>();
            container.RegisterType<IMapper<BookCopy>, BookCopyMapper>();
            container.RegisterType<IMapper<Member>, MemberMapper>();

            // Access
            container.RegisterType<IBookAccess, BookAccess>();
            container.RegisterType<IBookCopyAccess, BookCopyAccess>();
            container.RegisterType<IMemberAccess, MemberAccess>();

            // Repositories
            container.RegisterType<IBookCopyRepository, BookCopyRepository>();
            container.RegisterType<IBookRepository, BookRepository>();
            container.RegisterType<IMemberRepository, MemberRepository>();

            // Facades
            container.RegisterType<IBookCopyFacade, BookCopyFacade>();
            container.RegisterType<IBookFacade, BookFacade>();
            container.RegisterType<IMemberFacade, MemberFacade>();
            container.RegisterType<IUserStore<Member>, MemberFacade>();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}