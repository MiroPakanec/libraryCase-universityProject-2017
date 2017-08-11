using DataAccess.EntityMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Queries.Wishlist
{
    public interface IWishlistAccess : IQueryable
    {
        Task<IMap> SelectWithIdAsync(int wishlistBookId);
        Task<bool> DeleteWithIdAsync(int wishlistBookId);
    }
}
