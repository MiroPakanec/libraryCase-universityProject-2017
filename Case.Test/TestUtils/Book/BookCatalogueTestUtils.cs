using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EntityMaps.Book;

namespace Case.Test.TestUtils.Book
{
    internal static class BookCatalogueTestUtils
    {
        internal static IEnumerable<CatalogueItemMap> GetValidCatalogueSearchResult => new List<CatalogueItemMap>
        {
            GetValidCatalogueItemMap,
            GetValidCatalogueItemMap,
            GetValidCatalogueItemMap,
            GetValidCatalogueItemMap
        };

        internal static CatalogueItemMap GetValidCatalogueItemMap => new CatalogueItemMap
        {
            Author = BookTestUtils.GenerateInvalidAuthor(),
            Isbn = BookTestUtils.GenerateValidBook().Isbn,
            Title = BookTestUtils.GenerateValidBook().Title
        };
    }
}
