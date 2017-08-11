using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Case.Core.Entity
{
    public class Role : IRole<string>
    {
        // Needed for identity
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
