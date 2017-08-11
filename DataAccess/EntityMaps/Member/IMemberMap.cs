using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityMaps.Member
{
    public interface IMemberMap : IMap
    {
        string Ssn { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string RoleId { get; set; }
        string Campus { get; set; }
        DateTime MembershipExpiration { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
