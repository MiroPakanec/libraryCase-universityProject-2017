using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Model;

namespace Case.Core.Facade.Interfaces
{
    public interface IMemberFacade
    {
        Task<Member> FindMember(string ssn, string password);
        Task<bool> RegisterMember(Member member); // Probably change to a membermodel later
        Task<VerifyMemberModel> FindWithRoleNamePagedModel(string roleName, int page, int pageSize);
        Task VerifyMember(string ssn);
    }
}
