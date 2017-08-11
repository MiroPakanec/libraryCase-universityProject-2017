using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Member;

namespace Case.Core.Mapper.Member
{
    public class MemberMapper : IMapper<Entity.Member>
    {
        public IMap Map(Entity.Member member)
        {
            //Todo: missing role id and id

            return new MemberMap()
            {
                Ssn = member.Ssn,
                Username = member.UserName,
                Password = member.Password,
                Campus = member.Campus,
                FirstName = member.FirstName,
                LastName = member.LastName,
                MembershipExpiration = member.MembershipExpiration,
                RoleId = member.RoleId
            };
        }

        public Entity.Member Unmap(IMap map)
        {
            //Todo: missing role id and id

            var memberMap = map as IMemberMap;

            if (memberMap == null)
            {
                throw new Exception("Unable to unmap a member.");
            }

            return new Entity.Member
            {
                Ssn = memberMap.Ssn,
                Campus = memberMap.Campus,
                FirstName = memberMap.FirstName,
                LastName = memberMap.LastName,
                MembershipExpiration = memberMap.MembershipExpiration,
                Password = memberMap.Password,
                UserName = memberMap.Username,
                RoleId = memberMap.RoleId
            };
        }
    }
}
