using Case.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Core.Repository.Interface
{
    public interface IWishlistRepository : IBaseRepository<WishlistBook>
    {
        Task<WishlistBook> FindByIdAsync(int id);
    }
}
