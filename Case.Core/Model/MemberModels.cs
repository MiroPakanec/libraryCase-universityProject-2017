using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;

namespace Case.Core.Model
{
    public class VerifyMemberModel
    {
        public IEnumerable<VerifyMemberItem> Members;
        public bool IsBackAvailable { get; set; }
        public bool IsNextAvailable { get; set; }
        public int PageIndex { get; set; }
    }
    public class VerifyMemberItem
    {
        public string Ssn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
