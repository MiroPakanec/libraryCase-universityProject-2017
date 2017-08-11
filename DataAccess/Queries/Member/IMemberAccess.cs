using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EntityMaps.Member;

namespace DataAccess.Queries.Member
{
    public interface IMemberAccess : IQueryable
    {
        Task<IMemberMap> FindBySsnAndPasswordAsync(string ssn, string password);
        Task<IMemberMap> FindBySsn(string ssn);
        Task<IMemberMap> FindByUsername(string username);
        Task<string> FindMemberRoleName(string roleId);
        Task<bool> IsMemberInRole(IMemberMap member, string roleName);
        Task<bool> AddMemberToRole(IMemberMap member, string roleName);
        Task<bool> RemoveMemberRole(IMemberMap member, string roleName);
        Task<IRoleMap> FindMemberRoleByRoleId(int id);
        Task<IRoleMap> FindMemberRoleByName(string roleName);
        Task<bool> SetPasswordHash(string ssn, string passwordHash);
        Task<string> GetPasswordHash(string ssn);
        Task<IEnumerable<IMemberItemModelMap>> FindMemberModelsByRoleName(string roleName);
        Task<int> GetMemberInRoleCount(string roleName);
    }
}
