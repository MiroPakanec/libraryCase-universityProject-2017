using DataAccess.EntityMaps.Member;
using DataAccess.Queries.Member;
using Moq;

namespace Case.Test.Repository.Extensions.MemberRepository
{
    public static class MemberAccessMockExtensions
    {
        public static Mock<IMemberAccess> WithInsertResult(this Mock<IMemberAccess> mock, bool result)
        {
            mock.Setup(m => m.InsertOneAsync(It.IsAny<IMemberMap>())).ReturnsAsync(result);
            return mock;
        }

        public static Mock<IMemberAccess> WithFindBySsnAndPasswordResult(this Mock<IMemberAccess> mock, IMemberMap memberMap)
        {
            mock.Setup(m => m.FindBySsnAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(memberMap);
            return mock;
        }

        public static Mock<IMemberAccess> WithFindBySsnResult(this Mock<IMemberAccess> mock, IMemberMap memberMap)
        {
            mock.Setup(m => m.FindBySsn(It.IsAny<string>())).ReturnsAsync(memberMap);
            return mock;
        }

        public static Mock<IMemberAccess> WithFindByUsernameResult(this Mock<IMemberAccess> mock, IMemberMap memberMap)
        {
            mock.Setup(m => m.FindByUsername(It.IsAny<string>())).ReturnsAsync(memberMap);
            return mock;
        }
    }
}
