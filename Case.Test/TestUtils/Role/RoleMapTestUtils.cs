using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Member;

namespace Case.Test.TestUtils.Role
{
    public class RoleMapTestUtils
    {
        internal static IMap GenerateValidRoleMap()
        {
            return new RoleMap()
            {
                Id = "1",
                Name = "Member"
            };
        }
    }
}