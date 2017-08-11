using Case.Core.Entity;
using Case.Core.Mapper;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Member;
using Moq;

namespace Case.Test.Repository.Extensions.MemberRepository
{
    public static class MemberMapperMockExtensions
    {
        public static Mock<IMapper<Member>> WithMap(this Mock<IMapper<Member>> mock, IMap memberMap)
        {
            mock.Setup(m => m.Map(It.IsAny<Member>())).Returns(memberMap);
            return mock;
        }

        public static Mock<IMapper<Member>> WithUnmap(this Mock<IMapper<Member>> mock, Member member)
        {
            mock.Setup(m => m.Unmap(It.IsAny<MemberMap>())).Returns(member);
            return mock;
        }
    }
}
