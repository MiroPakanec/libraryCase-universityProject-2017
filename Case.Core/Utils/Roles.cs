using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;

namespace Case.Core.Utils
{
    public static class Roles
    {
        public const string NotVerifiedMember = "member";
        public const string VerifiedMember = "verified_member";
        public const string Librarian = "librarian";
        public const string Teacher = "teacher";

        public static bool IsRole(string roleName)
        {
            roleName = roleName.ToLower();

            return roleName.Equals(NotVerifiedMember) ||
                   roleName.Equals(Librarian) ||
                   roleName.Equals(Teacher) ||
                   roleName.Equals(VerifiedMember);
        }

        public static bool IsRole(Role role)
        {
            return IsRole(role.Name);
        }

        public static bool CanRent(Role role)
        {
            return CanRent(role.Name);
        }

        public static bool CanRent(string roleName)
        {
            return roleName.ToLower().Equals(VerifiedMember) ||
                   roleName.ToLower().Equals(Teacher);
        }
    }
}
