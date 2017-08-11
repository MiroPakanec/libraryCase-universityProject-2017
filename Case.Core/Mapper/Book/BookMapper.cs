using System;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Book;

namespace Case.Core.Mapper.Book
{
    public class BookMapper : IMapper<Entity.Book>
    {
        public IMap Map(Entity.Book item)
        {
            return new BookMap()
            {
                ISBN = item.Isbn,
                Title = item.Title,
                Description = item.Description,
                Subject = item.Subject,
                Language = item.Language,
                Binding = item.Binding,
                Edition = item.Edition,
                Author = item.Author,
                Loanable = item.Loanable
            };
        }
        

        Entity.Book IMapper<Entity.Book>.Unmap(IMap map)
        {
            var bookMap = map as IBookMap;

            if (bookMap == null)
            {
                throw new Exception("Unable to unmap a book.");
            }

            return new Entity.Book
            {
                Isbn = bookMap.ISBN,
                Title = bookMap.Title,
                Description = bookMap.Description,
                Subject = bookMap.Subject,
                Language = bookMap.Language,
                Binding = bookMap.Binding,
                Edition = bookMap.Edition.ToString(),
                Author = bookMap.Author,
                Loanable = bookMap.Loanable
            };
        }
    }
}
