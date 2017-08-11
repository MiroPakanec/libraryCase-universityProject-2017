using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Facade.Interfaces;
using Case.Core.Model;
using Case.Core.Repository.Interface;
using Case.Core.Utils;
using Microsoft.AspNet.Identity;
using static Case.Core.Utils.MemberUtils;

namespace Case.Core.Facade
{
    public class MemberFacade : IMemberFacade, IUserStore<Member>, IUserPasswordStore<Member>, IUserLockoutStore<Member,string>, IUserTwoFactorStore<Member, string>, IUserRoleStore<Member>
    {
        private IMemberRepository _repository;

        public MemberFacade(IMemberRepository repository)
        {
            _repository = repository;
        }

        public async Task<Member> FindMember(string ssn, string password)
        {
            if (!ValidateSsn(ssn))
            {
                throw new ArgumentException("Invalid Ssn");
            }

            if(!ValidatePassword(password))
            {
                throw new ArgumentException("Invalid password");
            }

            return await _repository.FindBySsnAndPassword(ssn, password);
        }

        public async Task<bool> RegisterMember(Member member)
        {
            ValidateMember(member);

            return await _repository.InsertAsync(member);
        }

        public async Task<VerifyMemberModel> FindWithRoleNamePagedModel(string roleName, int page, int pageSize)
        {
            if (!Roles.IsRole(roleName))
            {
                throw new ArgumentException("Role with such name doesn't exist. This is NOT case sensitive.");
            }

            if (page < 0)
            {
                throw new ArgumentException("Page Id cannot be below zero");
            }

            if (pageSize < 1)
            {
                throw new ArgumentException("Page size lower than 1");
            }

            return new VerifyMemberModel()
            {
                Members = await _repository.FindModelsWithRoleName(roleName),
                IsBackAvailable = page > 0,
                IsNextAvailable = (page + 1) * pageSize < await GetMemberInRoleCount(roleName),
                PageIndex = page
            };
        }

        public async Task VerifyMember(string ssn)
        {
            var member = await _repository.FindBySsn(ssn);
            await AddToRoleAsync(member, Roles.VerifiedMember);
        }

        public async Task<int> GetMemberInRoleCount(string roleName)
        {
            if (!Roles.IsRole(roleName))
            {
                throw new ArgumentException("Role with such name doesn't exist. This is Not case sensitive.");
            }

            return await _repository.GetMemberInRoleCount(roleName);
        }

        public void Dispose()
        {
            
        }

        #region UserStore impl
        

        public async Task CreateAsync(Member user)
        {
            ValidateMember(user);
            await _repository.InsertAsync(user);
        }

        public async Task UpdateAsync(Member user)
        {
            ValidateMember(user);
            await _repository.UpdateAsync(user);
        }

        public async Task DeleteAsync(Member user)
        {
            ValidateMember(user);
            await _repository.DeleteAsync(user);
        }

        public async Task<Member> FindByIdAsync(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("id cannot be invalid");
            }

            return await _repository.FindById(userId);
        }

        public async Task<Member> FindByNameAsync(string userName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("userName cannot be invalid");
            }

            return await _repository.FindByUsername(userName);
        }

        #endregion

        #region Roles

        public async Task AddToRoleAsync(Member user, string roleName)
        {
            ValidateMember(user);
            await _repository.UpdateRole(user, roleName);
        }

        public async Task RemoveFromRoleAsync(Member user, string roleName)
        {
            ValidateMember(user);

            if (String.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("roleName cannot be null or invalid");
            }
            await _repository.RemoveFromRole(user, roleName);
        }

        public async Task<IList<string>> GetRolesAsync(Member user)
        {
            ValidateMember(user);
            return new List<string>()
            {
                await _repository.GetRoleName(user)
            };
        }

        public async Task<bool> IsInRoleAsync(Member user, string roleName)
        {
            ValidateMember(user);
            if (String.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("roleName cannot be invalid");
            }
            return await _repository.IsInRole(user, roleName);
        }

        #endregion


        public async Task SetPasswordHashAsync(Member user, string passwordHash)
        {
            await _repository.SetMemberPasswordHash(user, passwordHash);
        }

        public async Task<string> GetPasswordHashAsync(Member user)
        {
            return await _repository.GetMemberPasswordHash(user);
        }

        public async Task<bool> HasPasswordAsync(Member user)
        {
            ValidateMember(user);

            try
            {
                return !string.IsNullOrEmpty(await _repository.GetMemberPasswordHash(user));
            }
            catch
            {
                return false;
            }
        }

        #region Useless methods implemented from useless stores

        // Too lazy to explain, basically Email == ssn
        public async Task SetEmailAsync(Member user, string email) { }

        public async Task<string> GetEmailAsync(Member user)
        {
            return string.Empty;
        }

        public async Task<bool> GetEmailConfirmedAsync(Member user)
        {
            return true;
        }

        public async Task SetEmailConfirmedAsync(Member user, bool confirmed) {}

        public async Task<Member> FindByEmailAsync(string email)
        {
            return null;
        }

        public async Task<DateTimeOffset> GetLockoutEndDateAsync(Member user)
        {
            return DateTimeOffset.Now;
        }

        public async Task SetLockoutEndDateAsync(Member user, DateTimeOffset lockoutEnd) {}

        public async Task<int> IncrementAccessFailedCountAsync(Member user)
        {
            return 0;
        }

        public async Task ResetAccessFailedCountAsync(Member user) {}

        public async Task<int> GetAccessFailedCountAsync(Member user)
        {
            return 0;
        }

        public async Task<bool> GetLockoutEnabledAsync(Member user)
        {
            return false;
        }

        public async Task SetLockoutEnabledAsync(Member user, bool enabled) {}

        public async Task SetTwoFactorEnabledAsync(Member user, bool enabled)
        {
        }

        public async Task<bool> GetTwoFactorEnabledAsync(Member user)
        {
            return false;
        }

        #endregion
    }
}
