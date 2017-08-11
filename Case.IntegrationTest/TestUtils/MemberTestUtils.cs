using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;

namespace Case.IntegrationTest.TestUtils
{
    public static class MemberTestUtils
    {
        public static Member GenerateValidMember()
        {
            return new Member()
            {
                Ssn = "000-00-0000",
                Campus = "Sofiendalsvej",
                MembershipExpiration = DateTime.Now,
                FirstName = "Bob",
                LastName = "Bobonson",
                Password = "MyPassword1.",
                RoleId = "1"
            };
        }

        public static Member GenerateInvalidMember()
        {
            return new Member()
            {
                Ssn = "chr-no-alwd",
                Campus = "Doesn't matter",
                MembershipExpiration = DateTime.MinValue,
                FirstName = "Doesn't",
                LastName = "Matter",
                Password = " ",
                RoleId = "1"
            };
        }

        public static string GenerateValidSsn()
        {
            return "000-00-0000";
        }

        public static string GenerateInvalidSsn()
        {
            return "00a0-b0-a00a";
        }
    }
}
