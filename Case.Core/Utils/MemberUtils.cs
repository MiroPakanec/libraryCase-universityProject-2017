using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Case.Core.Entity;

namespace Case.Core.Utils
{
    public static class MemberUtils
    {
        public static void ValidateMember(Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member), "Member is null");
            }
            
            if (!ValidateSsn(member.Ssn))
            {
                throw new ArgumentException("Invalid Ssn");
            }
        }

        public static bool ValidateSsn(string ssn)
        {
            if (string.IsNullOrWhiteSpace(ssn))
            {
                return false;
            }
            var regex = new Regex(@"^\d{3}-\d{2}-\d{4}$");
            return regex.IsMatch(ssn);
        }

        public static bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            return true;
        }
    }
}