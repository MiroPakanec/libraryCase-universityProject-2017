using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Wishlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Core.Mapper.Wishlist
{
    public class WishlistBookMapper : IMapper<Entity.WishlistBook>
    {
        public IMap Map(Entity.WishlistBook item)
        {
            return new WishlistMap()
            {
                Id = item.Id,
                ReasonToAcquire = item.Reason
            };
        }

        Entity.WishlistBook IMapper<Entity.WishlistBook>.Unmap(IMap map)
        {
            var wishlistBookMap = map as IWishlistMap;

            if (wishlistBookMap == null)
            {
                throw new Exception("Unable to unmap a book for a wishlist.");
            }

            return new Entity.WishlistBook
            {
                Id = wishlistBookMap.Id,
                Reason = wishlistBookMap.ReasonToAcquire
            };
        }
    }
}
