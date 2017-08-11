using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Wishlist;

namespace Case.Test.TestUtils.Wishlist
{
    internal static class WishlistMapTestUtils
    {
        internal static IMap GenerateValidWishlistBookMap()
        {
            return new WishlistMap()
            {
                Id = 1,
                ReasonToAcquire = "Because I want to"
            };
        }
    }
}
