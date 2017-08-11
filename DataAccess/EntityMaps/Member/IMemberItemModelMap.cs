using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityMaps.Member
{
    public interface IMemberItemModelMap : IMap
    {
        string Ssn { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
