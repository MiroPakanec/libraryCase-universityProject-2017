using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EntityMaps;

namespace Case.IntegrationTest.TestUtils
{
    public static class RoleTestUtils
    {
        public static Core.Entity.Role GenerateValidRole()
        {
            return new Core.Entity.Role()
            {
                Id = "1",
                Name = "Member"
            };
        }

        public static Core.Entity.Role GenerateInvalidRole()
        {
            return new Core.Entity.Role()
            {
                Id = "-1",
                Name = string.Empty
            };
        }
    }
}
