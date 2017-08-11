using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityMaps.Member
{
    public class MemberMap : IMemberMap
    {
        public virtual string Ssn { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string RoleId { get; set; }
        public virtual string Campus { get; set; }
        public virtual DateTime MembershipExpiration { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
    }
}
