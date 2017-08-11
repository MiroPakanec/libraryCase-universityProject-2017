using Case.Core.Entity;

namespace Case.Test.TestUtils.Wishlist
{
    public static class WishlistBookTestUtils
    {
        public static WishlistBook GenerateValidWishlistBook()
        {
            return new WishlistBook()
            {
                Id = 1,
                Reason = "Because I want to"
            };
        }
        
        public static WishlistBook GenerateInvalidWishlistBook()
        {
            return new WishlistBook()
            {
                Id = -1,
                Reason = "Because I want to"
            };
        }
    }
}
