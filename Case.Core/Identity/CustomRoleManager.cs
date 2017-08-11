using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Microsoft.AspNet.Identity;

namespace Case.Core.Identity
{
    public class CustomRoleManager : RoleManager<Role>
    {
        public CustomRoleManager(IRoleStore<Role, string> store) : base(store)
        {
        }

        public override async  Task<IdentityResult> CreateAsync(Role role)
        {
            if (string.IsNullOrEmpty(role.Name))
            {
                throw new ArgumentException("Role name cannot be null or empty");
            }

            var exists = (await Store.FindByNameAsync(role.Name)) != null;
            if (exists)
            {
                return IdentityResult.Failed("Role already exists");
            }

            if (role.Id == null)
            {
                role.Id = Guid.NewGuid().ToString();
            }

            await Store.CreateAsync(role);
            return IdentityResult.Success;
        }
    }
}
