using System;
using Case.Core.Model;
using DataAccess.EntityMaps.Book;

namespace Case.Core.Mapper.Book
{
    public class CatalogueItemMapper : IDualMapper<Entity.Book, CatalogueItemMap>
    {
        public CatalogueItemMap Map(Entity.Book item)
        {
            throw new NotImplementedException();
        }

        public Entity.Book Unmap(CatalogueItemMap map)
        {
            return new Entity.Book
            {
                Author = map.Author,
                Isbn = map.Isbn,
                Title = map.Title
            };
        }
    }
}
