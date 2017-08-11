using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityMaps.Wishlist
{
    public interface IWishlistMap : IMap
    {
        int Id { get; set; }
        string ReasonToAcquire { get; set; }
    }
}
