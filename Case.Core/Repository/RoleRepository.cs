using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Repository.Interface;
using DataAccess.Queries.Member;
using Case.Core.Mapper;
using DataAccess.EntityMaps;
using DataAccess.Queries.Role;

namespace Case.Core.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IMemberAccess _access;
        private readonly IRoleAccess _roleAccess;
        private readonly IMapper<Role> _mapper;

        public RoleRepository(IMemberAccess access, IRoleAccess roleAccess, IMapper<Role> mapper)
        {
            _access = access;
            _roleAccess = roleAccess;
            _mapper = mapper;
        }

        public Task<bool> InsertAsync(Role item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Role cannot be null");
            }
            
            var roleMap = _mapper.Map(item);

            if (roleMap == null)
            {
                throw new NullReferenceException("Rolemap cannot be null");
            }

            return _roleAccess.InsertOneAsync(roleMap);
        }

        [Obsolete("You should use FindRoleNameById() instead")]
        public Task<Role> GetAsync(int id)
        {
            throw new Exception("Obsolete, use findById instead");
        }

        public Task<bool> UpdateAsync(Role item)
        {
            if (String.IsNullOrEmpty(item.Name))
            {
                throw new NullReferenceException("Role cannot be null");
            }

            var roleMap = _mapper.Map(item);

            if (roleMap == null)
            {
                throw new NullReferenceException("Rolemap cannot be null");
            }

            return _roleAccess.UpdateOneAsync(roleMap);
        }

        public Task<bool> DeleteAsync(Role item)
        {
            if (String.IsNullOrEmpty(item.Name))
            {
                throw new NullReferenceException("Role cannot be null");
            }

            var roleMap = _mapper.Map(item);

            if (roleMap == null)
            {
                throw new NullReferenceException("RoleMap cannot be null");
            }

            return _roleAccess.DeleteOneAsync(roleMap);
        }

        public Task<bool> InsertOrUpdateAsync(Role item)
        {
            if (String.IsNullOrEmpty(item.Name))
            {
                throw new NullReferenceException("Role cannot be null");
            }

            var roleMap = _mapper.Map(item);

            if (roleMap == null)
            {
                throw new NullReferenceException("Rolemap cannot be null");
            }

            return _roleAccess.InsertOrUpdateAsync(roleMap);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id < 0)
            {
                throw new Exception("Role Id cannot be less than 0");
            }

            var roleMap = await _access.FindMemberRoleByRoleId(id);

            if (roleMap == null)
            {
                 throw new Exception("member doesn't exist"); //what if the role doesn't exist?
            }

            return await _roleAccess.DeleteOneAsync(roleMap);
        }

        public async Task<Role> FindById(int id)
        {
            if (id < 0)
            {
                throw new Exception("Id cannot be invalid");
            }

            var roleMap = await _access.FindMemberRoleByRoleId(id);

            if (roleMap == null)
            {
                return new Role();
            }

            var role = _mapper.Unmap(roleMap);

            if (role == null)
            {
                throw new NullReferenceException("role cannot be null");
            }

            return role;
        }

        public async Task<IEnumerable<Role>> FindByName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new NullReferenceException("Role name cannot be invalid");
            }

            var roleMap = await _access.FindMemberRoleByName(name);

            if (roleMap == null)
            {
                return new List<Role>();
                //throw new NullReferenceException("roleMap cannot be null");
            }

            var role = _mapper.Unmap(roleMap);

            if (role == null)
            {
                throw new NullReferenceException("role cannot be null");
            }

            return new List<Role>() {role};
        }

        public async Task<string> FindRoleName(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new NullReferenceException("Role id cannot be invalid");
            }

            var roleName = await _access.FindMemberRoleName(id);

            if (roleName == null)
            {
                return null;
            }

            return roleName;
        }
    }
}

