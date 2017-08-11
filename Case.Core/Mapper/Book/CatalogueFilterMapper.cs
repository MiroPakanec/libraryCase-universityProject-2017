using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Model;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Book;
using DataAccess.EntityMaps.BookCopy;

namespace Case.Core.Mapper.Book
{
    public class CatalogueFilterMapper : IMapper<BookCatalogFilter>
    {
        public IMap Map(BookCatalogFilter item)
        {
            return new CatalogueFilterMap
            {
                Author = item.Author,
                CanLoan = item.Loanable,
                Isbn = item.Isbn,
                Subject = item.Subject,
                Title = item.Title,
                StartDate = item.PublishDateIntervalStart,
                EndDate = item.PublishDateIntervalEnd
            };
        }

        public BookCatalogFilter Unmap(IMap map)
        {
            throw new NotImplementedException();
        }
    }
}
