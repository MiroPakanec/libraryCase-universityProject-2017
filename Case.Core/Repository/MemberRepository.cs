using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Mapper;
using Case.Core.Model;
using Case.Core.Repository.Interface;
using Case.Core.Utils;
using DataAccess.EntityMaps.Member;
using DataAccess.Queries.Member;

namespace Case.Core.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly IMemberAccess _access;
        private readonly IMapper<Member> _mapper;

        public MemberRepository(IMemberAccess access, IMapper<Member> mapper)
        {
            _access = access;
            _mapper = mapper;
        }

        public Task<bool> InsertAsync(Member item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Member cannot be null.");
            }

            var memberMap = _mapper.Map(item);

            if (memberMap == null)
            {
                throw new NullReferenceException("Member map cannot be null.");
            }

            return _access.InsertOneAsync(memberMap);
        }

        [Obsolete("Use FindBySsn")]
        public Task<Member> GetAsync(int id)
        {
            throw new Exception("Member cannot be selected with Id.");
        }

        public Task<bool> UpdateAsync(Member item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Member cannot be null.");
            }

            var memberMap = _mapper.Map(item);

            if (memberMap == null)
            {
                throw new NullReferenceException("Member map cannot be null.");
            }

            return _access.UpdateOneAsync(memberMap);
        }

        public Task<bool> DeleteAsync(Member item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Member cannot be null.");
            }

            var memberMap = _mapper.Map(item);

            if (memberMap == null)
            {
                throw new NullReferenceException("Member map cannot be null.");
            }

            return _access.DeleteOneAsync(memberMap);
        }

        public Task<bool> InsertOrUpdateAsync(Member item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Member cannot be null.");
            }

            var memberMap = _mapper.Map(item);

            if (memberMap == null)
            {
                throw new NullReferenceException("Member map cannot be null.");
            }

            return _access.InsertOrUpdateAsync(memberMap);
        }

        [Obsolete("Use DeleteBySsn")]
        public Task<bool> DeleteAsync(int id)
        {
            throw new Exception("Member cannot be deleted with id.");
        }

        public async Task<Member> FindBySsnAndPassword(string ssn, string password)
        {
            if (ssn == null)
            {
                throw new ArgumentNullException($"Ssn cannot be null.");
            }

            if (MemberUtils.ValidateSsn(ssn) == false)
            {
                throw new ArgumentException("Ssn is not valid.");
            }

            if (password == null)
            {
                throw new ArgumentNullException($"Password cannot be null.");
            }

            if (MemberUtils.ValidatePassword(password) == false)
            {
                throw new ArgumentException("Password is not valid.");
            }

            var memberMap = await _access.FindBySsnAndPasswordAsync(ssn, password);

            if (memberMap == null)
            {
                throw new NullReferenceException("Member map cannot be null.");
            }

            var member = _mapper.Unmap(memberMap);

            if (member == null)
            {
                throw new NullReferenceException("Member cannot be null.");
            }

            return member;
        }

        public async Task<Member> FindById(string id)
        {
            return await FindBySsn(id);
        }

        public async Task<Member> FindBySsn(string ssn)
        {
            if (ssn == null)
            {
                throw new ArgumentNullException($"Ssn cannot be null.");
            }

            if (MemberUtils.ValidateSsn(ssn) == false)
            {
                throw new ArgumentException("Ssn is invalid.");
            }

            var memberMap = await _access.FindBySsn(ssn);

            if (memberMap == null)
            {
                throw new NullReferenceException("Member map cannot be null.");
            }

            var member = _mapper.Unmap(memberMap);

            if (member == null)
            {
                throw new NullReferenceException("Member cannot be null.");
            }

            return member;
        }

        public async Task<Member> FindByUsername(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException($"Username cannot be null.");
            }

            var memberMap = await _access.FindByUsername(username);

            if (memberMap == null)
            {
                return null; //throw new NullReferenceException("Member map cannot be null.");
            }

            var member = _mapper.Unmap(memberMap);

            if (member == null)
            {
                throw new NullReferenceException("Member map cannot be null.");
            }

            return member;
        }

        [Obsolete("Use GetRoleName")]
        public Task<IList<string>> GetRoles(Member member)
        {          
            throw new Exception("Multiple roles cannot be selected. Use GetRoleName instead.");
        }

        public async Task<string> GetRoleName(Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException($"Member cannot be null.");
            }

            if (string.IsNullOrEmpty(member.RoleId))
            {
                throw new NullReferenceException("Member role cannot be null.");
            }

            MemberUtils.ValidateMember(member);

            
           return await _access.FindMemberRoleName(member.RoleId);
        }

        public async Task<bool> IsInRole(Member member, string roleName)
        {
            if (member == null)
            {
                throw new ArgumentNullException($"Member cannot be null.");
            }

            MemberUtils.ValidateMember(member);

            if (roleName == null)
            {
                throw new ArgumentNullException($"Role cannot be null.");
            }

            if (roleName.Length <= 0)
            {
                throw new ArgumentException("Role is invalid.");
            }

            var memberMap = _mapper.Map(member);

            if (memberMap == null)
            {
                throw new NullReferenceException("Member map cannot be null.");
            }

            return await _access.IsMemberInRole(memberMap as IMemberMap, roleName);
        }

        [Obsolete("Use UpdateRole")]
        public async Task AddToRole(Member member, string roleName)
        {
            await UpdateRole(member, roleName);
        }

        public async Task<bool> UpdateRole(Member member, string roleName)
        {
            if (member == null)
            {
                throw new ArgumentNullException($"Member cannot be null.");
            }

            MemberUtils.ValidateMember(member);

            if (roleName == null)
            {
                throw new ArgumentNullException($"Role cannot be null.");
            }

            if (roleName.Length <= 0)
            {
                throw new ArgumentException("Role is invalid.");
            }

            var memberMap = _mapper.Map(member);

            if (memberMap == null)
            {
                throw new NullReferenceException("Member map cannot be null.");
            }

            return await _access.AddMemberToRole(memberMap as IMemberMap, roleName);
        }

        public Task RemoveFromRole(Member member, string roleName)
        {
            if (member == null)
            {
                throw new ArgumentNullException($"Member cannot be null.");
            }

            MemberUtils.ValidateMember(member);
            
            if (roleName == null)
            {
                throw new ArgumentNullException($"Role cannot be null.");
            }

            if (roleName.Length <= 0)
            {
                throw new ArgumentException("Role is invalid.");
            }

            var memberMap = _mapper.Map(member);

            if (memberMap == null)
            {
                throw new NullReferenceException("Member map cannot be null.");
            }

            return _access.RemoveMemberRole(memberMap as IMemberMap, roleName);
        }

        public async Task<IEnumerable<VerifyMemberItem>> FindModelsWithRoleName(string roleName)
        {
            if (!Roles.IsRole(roleName))
            {
                throw new ArgumentException("Invalid role");
            }

            var mapList = await _access.FindMemberModelsByRoleName(roleName);

            var result = new List<VerifyMemberItem>();
            // Not worth to create mapper when it'd be used only here
            foreach (var map in mapList)
            {
                result.Add(
                    new VerifyMemberItem()
                    {
                        Ssn = map.Ssn,
                        FirstName = map.FirstName,
                        LastName = map.LastName
                    });
            }
            return result;
        }

        public async Task<int> GetMemberInRoleCount(string roleName)
        {
            if (String.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Rolename cannot be invalid");
            }

            return await _access.GetMemberInRoleCount(roleName);
        }

        public async Task<bool> SetMemberPasswordHash(Member member, string passwordHash)
        {
            return await _access.SetPasswordHash(member.Ssn, passwordHash);
        }

        public async Task<string> GetMemberPasswordHash(Member member)
        {
            return await _access.GetPasswordHash(member.Ssn);
        }

        public Task<IEnumerable<Member>> FindWithRoleName(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
