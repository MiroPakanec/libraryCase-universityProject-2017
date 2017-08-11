using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Book;

namespace DataAccess.Queries.Book
{
    public interface IBookAccess : IQueryable
    {
        Task<bool> DeleteWithIdAsync(string id);

        Task<IEnumerable<IMap>> SelectWithDescription(string desc);
        Task<IMap> SelectWithIdAsync(string id);
        Task<string> SelectDescriptionWithId(string id);
        Task<IEnumerable<CatalogueItemMap>> SearchCatalogue(int pageIndex, int pageSize, IMap bookCatalogueFilter);
    }
}
