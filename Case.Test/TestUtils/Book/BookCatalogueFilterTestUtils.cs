using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Model;
using Case.Core.Model.Extensions;
using DataAccess.EntityMaps.Book;

namespace Case.Test.TestUtils.Book
{
    internal static class BookCatalogueFilterTestUtils
    {

        internal static BookCatalogFilter GetValidCatalogueFilter => new BookCatalogFilter()
            .WithAuthor("Unk")
            .WithLoanability(false)
            .WithIsbn(string.Empty)
            .WithSubject("Ma")
            .WithTitle(string.Empty)
            .WithStartDate(DateTime.MinValue)
            .WithEndDate(DateTime.MaxValue);        

        internal static CatalogueFilterMap GetValidCatalogueFilterMap => new CatalogueFilterMap
        {
            Author = "Unk",
            CanLoan = false,
            Isbn = "",
            Subject = "Ma",
            Title = "",
            StartDate = DateTime.Now.AddYears(-50),
            EndDate = DateTime.Now.AddYears(50)
        };
    }
}
