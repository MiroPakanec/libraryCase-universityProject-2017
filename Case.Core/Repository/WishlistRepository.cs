using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Repository.Interface;
using Case.Core.Mapper;
using DataAccess.EntityMaps;
using DataAccess.Queries.Wishlist;
using DataAccess.EntityMaps.Wishlist;

namespace Case.Core.Repository
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly IWishlistAccess _access;
        private readonly IMapper<WishlistBook> _mapper;

        public WishlistRepository(IWishlistAccess access, IMapper<WishlistBook> mapper)
        {
            _access = access;
            _mapper = mapper;
        }

        public async Task<bool> InsertAsync(WishlistBook item)
        {
            if (item == null)
            {
                throw new NullReferenceException("WishlistBook cannot be null");
            }

            if (item.Id < 0)
            {
                throw new Exception("WishlistBook id cannot be negative");
            }

            var wishlistBookMap = _mapper.Map(item);

            if (item == null)
            {
                throw new NullReferenceException("WishlistBookMap cannot be null");
            }

            return await _access.InsertOneAsync(wishlistBookMap as IWishlistMap);
        }
        
        public async Task<WishlistBook> GetAsync(int id)
        {
            if (id < 0)
            {
                throw new NullReferenceException("Id cannot be less than 0");
            }

            var wishlistBookMap = await _access.SelectWithIdAsync(id);

            if(wishlistBookMap == null)
            {
                return null;
            }

            return _mapper.Unmap(wishlistBookMap);
        }

        public async Task<bool> UpdateAsync(WishlistBook item)
        {
            if (item == null)
            {
                throw new NullReferenceException("WishlistBook cannot be null");
            }

            var wishlistBookMap = _mapper.Map(item);

            if (wishlistBookMap == null)
            {
                throw new NullReferenceException("WishlistBookMap cannot be null");
            }

            return await _access.UpdateOneAsync(wishlistBookMap as IWishlistMap);
        }

        public async Task<bool> DeleteAsync(WishlistBook item)
        {
            if (item == null)
            {
                throw new NullReferenceException("WishlistBook cannot be null");
            }

            var wishlistBookMap = _mapper.Map(item);

            if (wishlistBookMap == null)
            {
                throw new NullReferenceException("WishlistBookMap cannot be null");
            }

            return await _access.DeleteOneAsync(wishlistBookMap as IWishlistMap); 
        }

        public async Task<bool> InsertOrUpdateAsync(WishlistBook item)
        {
            if (item == null)
            {
                throw new NullReferenceException("WishlistBook cannot be null");
            }

            var wishlistBookMap = _mapper.Map(item);

            if(wishlistBookMap == null)
            {
                throw new NullReferenceException("WishlistBookMap cannot be null");
            }

            return await _access.InsertOrUpdateAsync(wishlistBookMap);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id < 0)
            {
                throw new Exception("Id cannot be less than 0");
            }

            return await _access.DeleteWithIdAsync(id);
        }

        public async Task<WishlistBook> FindByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new Exception("Id cannot be less than 0");
            }

            var wishlistBookMap = await _access.SelectWithIdAsync(id);

            if (wishlistBookMap == null)
            {
                return null;
            }

            return _mapper.Unmap(wishlistBookMap);
        }
    }
}