using Case.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Core.Facade.Interfaces
{
    public interface IWishlistFacade
    {
        Task<bool> AddBookToWishlistAsync(WishlistBook item);
        Task<WishlistBook> GetWishlistBookAsync(int id);
        Task<bool> UpdateWishlistBookAsync(WishlistBook item);
        Task<bool> RemoveWishlistBookAsync(WishlistBook item);
        Task<bool> RemoveWishlistBookAsync(int id);
        Task<WishlistBook> FindWishlistBookById(int id);
    }
}
