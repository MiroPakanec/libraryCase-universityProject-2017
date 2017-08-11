using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Member;

namespace Case.Core.Mapper.Role
{
    public class RoleMapper : IMapper<Entity.Role>
    {
        public IMap Map(Entity.Role item)
        {
            return new RoleMap()
            {
                Id = item.Id,
                Name = item.Name
            };
        }

        public Entity.Role Unmap(IMap map)
        {
            var roleMap = map as IRoleMap;

            if (roleMap == null)
            {
                throw new Exception("Unable to unmap a role.");
            }

            return new Entity.Role()
            {
                Name = roleMap.Name,
                Id = roleMap.Id
            };
        }
    }
}
