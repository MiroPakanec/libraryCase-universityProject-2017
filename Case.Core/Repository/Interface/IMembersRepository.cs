using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Model;

namespace Case.Core.Repository.Interface
{
    public interface IMemberRepository : IBaseRepository<Member>
    {
        Task<Member> FindBySsnAndPassword(string ssn, string password);
        Task<Member> FindById(string id);
        Task<Member> FindBySsn(string ssn);
        Task<Member> FindByUsername(string username);
        Task<IList<string>> GetRoles(Member memer);
        Task<string> GetRoleName(Member member);
        Task<bool> IsInRole(Member member, string roleName);
        Task AddToRole(Member member, string roleName);
        Task<bool> UpdateRole(Member member, string roleName);
        Task RemoveFromRole(Member member, string roleName);
        Task<IEnumerable<VerifyMemberItem>> FindModelsWithRoleName(string roleName);
        Task<int> GetMemberInRoleCount(string roleName);
        Task<bool> SetMemberPasswordHash(Member member, string passwordHash);
        Task<string> GetMemberPasswordHash(Member member);
    }
}
