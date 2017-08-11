using Case.Core.Entity;
using Case.Core.Mapper;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Wishlist;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Test.Repository.Extensions.WishlistBookRepository
{
    public static class WishlistMapperMockExtensions
    {
        public static Mock<IMapper<WishlistBook>> WithMap(this Mock<IMapper<WishlistBook>> mock, IMap wishlistBookMap)
        {
            mock.Setup(m => m.Map(It.IsAny<WishlistBook>())).Returns(wishlistBookMap);
            return mock;
        }

        public static Mock<IMapper<WishlistBook>> WithUnmap(this Mock<IMapper<WishlistBook>> mock, WishlistBook wishlistBook)
        {
            mock.Setup(m => m.Unmap(It.IsAny<WishlistMap>())).Returns(wishlistBook);
            return mock;
        }
    }
}
