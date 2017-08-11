using Case.Core.Entity;
using Case.Core.Facade.Interfaces;
using Case.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Core.Facade
{
    public class WishlistFacade : IWishlistFacade
    {
        private readonly IWishlistRepository _repository;

        public WishlistFacade(IWishlistRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddBookToWishlistAsync(WishlistBook item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Wishlist book cannot be null");
            }

            if (item.Id < 0)
            {
                throw new Exception("Wishlist book Id cannot be less than 0");
            }

            return await _repository.InsertAsync(item);
        }

        public async Task<WishlistBook> GetWishlistBookAsync(int id)
        {
            if (id < 0)
            {
                throw new Exception("Id cannot be less than 0");
            }

            return await _repository.GetAsync(id);
        }

        public async Task<bool> UpdateWishlistBookAsync(WishlistBook item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Wishlist book cannot be null");
            }

            if(item.Id < 0)
            {
                throw new Exception("Wishlist book Id cannot be less than 0");
            }

            return await _repository.UpdateAsync(item);
        }

        public async Task<bool> RemoveWishlistBookAsync(WishlistBook item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Wishlist book cannot be null");
            }

            if (item.Id < 0)
            {
                throw new Exception("Wishlist book id cannot be invalid");
            }

            return await _repository.DeleteAsync(item);
        }

        public async Task<bool> RemoveWishlistBookAsync(int id)
        {
            if (id < 0)
            {
                throw new Exception("Id cannot be less than 0");
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<WishlistBook> FindWishlistBookById(int id)
        {
            if (id < 0)
            {
                throw new Exception("Id cannot be less than 0");
            }

            return await _repository.FindByIdAsync(id);
        }
    }
}
