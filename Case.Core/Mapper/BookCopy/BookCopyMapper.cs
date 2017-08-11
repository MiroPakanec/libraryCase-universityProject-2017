using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Book;
using DataAccess.EntityMaps.BookCopy;

namespace Case.Core.Mapper.BookCopy
{
    public class BookCopyMapper : IMapper<Entity.BookCopy>
    {
        public IMap Map(Entity.BookCopy item)
        {
            return new BookCopyMap()
            {
                Id = item.Id,
                BookIsbn = item.Isbn,
                Available = item.Available
            };
        }

        public Entity.BookCopy Unmap(IMap map)
        {
            var bookCopyMap = map as IBookCopyMap;

            if (bookCopyMap == null)
            {
                throw new Exception("Unable to unmap a book copy.");
            }

            return new Entity.BookCopy()
            {
                Id = bookCopyMap.Id,
                Isbn = bookCopyMap.BookIsbn,
                Available = bookCopyMap.Available
            };
        }
    }
}
