using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Member;

namespace Case.Test.TestUtils
{
    internal static class MemberMapTestUtils
    {
        internal static IMap GenerateValidMemberMap()
        {
            return new MemberMap
            {
                Ssn = "000-00-0000",
                Campus = "Sofiendalsvej",
                MembershipExpiration = DateTime.Now,
                FirstName = "Bob",
                LastName = "Bobonson"
            };
        }
    }
}
