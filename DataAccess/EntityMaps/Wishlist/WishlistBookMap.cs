using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityMaps.Wishlist
{
    public class WishlistMap : IWishlistMap
    {
        public int Id { get; set; }
        public string ReasonToAcquire { get; set; }
    }
}
