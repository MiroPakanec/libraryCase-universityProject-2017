using Case.Core.Entity;
using Case.Core.Mapper;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Member;
using DataAccess.EntityMaps.Wishlist;
using Moq;

namespace Case.Test.Repository.Extensions.RoleRepository
{
    public static class RoleMapperMockExtensions
    {
        public static Mock<IMapper<Role>> WithMap(this Mock<IMapper<Role>> mock, IMap roleMap)
        {
            mock.Setup(m => m.Map(It.IsAny<Role>())).Returns(roleMap);
            return mock;
        }

        public static Mock<IMapper<Role>> WithUnmap(this Mock<IMapper<Role>> mock, Role role)
        {
            mock.Setup(m => m.Unmap(It.IsAny<RoleMap>())).Returns(role);
            return mock;
        }
    }
}