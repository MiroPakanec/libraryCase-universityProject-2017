using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Member;
using DataAccess.Queries.Member;
using DataAccess.Queries.Role;
using Moq;

namespace Case.Test.Repository.Extensions.RoleRepository
{
    public static class RoleAccessMockExtensions
    {
        public static Mock<IMemberAccess> WithValidInsert(this Mock<IMemberAccess> mock, bool result)
        {
            mock.Setup(m => m.InsertOneAsync(It.IsAny<IRoleMap>())).ReturnsAsync(result);
            return mock;
        }

        public static Mock<IMemberAccess> WithValidUpdate(this Mock<IMemberAccess> mock, bool result)
        {
            mock.Setup(m => m.UpdateOneAsync(It.IsAny<IRoleMap>())).ReturnsAsync(result);
            return mock;
        }

        public static Mock<IMemberAccess> WithValidInsertUpdate(this Mock<IMemberAccess> mock, bool result)
        {
            mock.Setup(m => m.InsertOrUpdateAsync(It.IsAny<IRoleMap>())).ReturnsAsync(result);
            return mock;
        }

        public static Mock<IMemberAccess> WithValidDelete(this Mock<IMemberAccess> mock, bool result)
        {
            mock.Setup(m => m.DeleteOneAsync(It.IsAny<IRoleMap>())).ReturnsAsync(result);
            return mock;
        }

        public static Mock<IMemberAccess> WithValidFindByName(this Mock<IMemberAccess> mock, IRoleMap roleMap)
        {
            mock.Setup(m => m.FindMemberRoleByName(It.IsAny<string>())).ReturnsAsync(roleMap);
            return mock;
        }

        public static Mock<IMemberAccess> WithValidFindRoleByName(this Mock<IMemberAccess> mock, string roleName)
        {
            mock.Setup(m => m.FindMemberRoleName(It.IsAny<string>())).ReturnsAsync(roleName);
            return mock;
        }
    }
}