using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Case.Core.Entity
{
    public class Member : IUser
    {
        public Member()
        {
            Id = Guid.NewGuid().ToString();
        }
        // Required for identity
        public string Id
        {
            get { return Ssn; }
            set
            {
                if (value != null) Ssn = value;
            }
        }

        public string UserName
        {
            get
            {
                return Ssn;
            }
            set { if(value != null) Ssn = value; }
        }

        public string Password { get; set; }

        public virtual string Ssn { get; set; }
        public virtual string Campus { get; set; }
        public virtual DateTime MembershipExpiration { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string RoleId { get; set; }
        //public virtual IEnumerable<Role> Roles { get; set; }


        //TODO Address, Phone, Role(s)

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Member> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
