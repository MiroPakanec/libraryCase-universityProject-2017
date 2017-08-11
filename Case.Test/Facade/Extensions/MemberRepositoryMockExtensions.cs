using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Model;
using Case.Core.Repository.Interface;
using Case.Core.Utils;
using Case.Test.TestUtils;
using Moq;

namespace Case.Test.Facade.Extensions
{
    public static class MemberRepositoryMockExtensions
    {
        public static Mock<IMemberRepository> WithSuccesfulInsert(this Mock<IMemberRepository> repository)
        {
            repository.Setup(x => x.InsertAsync(It.IsAny<Member>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IMemberRepository> WithSuccesfulUpdate(this Mock<IMemberRepository> repository)
        {
            repository.Setup(x => x.UpdateAsync(It.IsAny<Member>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IMemberRepository> WithSuccesfulDelete(this Mock<IMemberRepository> repository)
        {
            repository.Setup(x => x.DeleteAsync(It.IsAny<Member>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IMemberRepository> WithSuccesfulGet(this Mock<IMemberRepository> repository)
        {
            repository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(MemberTestUtils.GenerateValidMember());
            return repository;
        }

        public static Mock<IMemberRepository> WithSuccesfulInsertOrUpdate(this Mock<IMemberRepository> repository)
        {
            repository.Setup(x => x.InsertOrUpdateAsync(It.IsAny<Member>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IMemberRepository> WithSuccesfulFindBySsnAndPassword(this Mock<IMemberRepository> repository)
        {
            repository.Setup(x => x.FindBySsnAndPassword(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(MemberTestUtils.GenerateValidMember);
            return repository;
        }

        public static Mock<IMemberRepository> WithSuccesfulGetRoles(this Mock<IMemberRepository> repository)
        {
            repository.Setup(x => x.GetRoles(It.IsAny<Member>()))
                .ReturnsAsync(new List<string>() {Roles.NotVerifiedMember});
            return repository;
        }

        public static Mock<IMemberRepository> WithSuccesfulGetRoleNameCannotRent(this Mock<IMemberRepository> repository)
        {
            repository.Setup(x => x.GetRoleName(It.IsAny<Member>())).ReturnsAsync(Roles.NotVerifiedMember);
            return repository;
        }

        public static Mock<IMemberRepository> WithSuccesfulGetRoleNameCanRent(this Mock<IMemberRepository> repository)
        {
            repository.Setup(x => x.GetRoleName(It.IsAny<Member>())).ReturnsAsync(Roles.VerifiedMember);
            return repository;
        }

        public static Mock<IMemberRepository> WithSuccessfulIsInRole(this Mock<IMemberRepository> repository)
        {
            repository.Setup(x => x.IsInRole(It.IsAny<Member>(), It.IsAny<string>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IMemberRepository> WithSuccessfulUpdateRole(this Mock<IMemberRepository> repository)
        {
            repository.Setup(x => x.UpdateRole(It.IsAny<Member>(), It.IsAny<string>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IMemberRepository> WithSuccessfulInRoleCount(this Mock<IMemberRepository> repository)
        {
            repository.Setup(x => x.GetMemberInRoleCount(It.IsAny<string>())).ReturnsAsync(It.IsAny<int>());
            return repository;
        }

        public static Mock<IMemberRepository> WithSuccesfulFindRoleNamePagedModel(
            this Mock<IMemberRepository> repository, string roleName)
        {
            repository.Setup(x => x.FindModelsWithRoleName(roleName)).Returns(It.IsAny<Task<IEnumerable<VerifyMemberItem>>>());
            return repository;
        }

        public static Mock<IMemberRepository> WithSuccesfulFindBySsn(this Mock<IMemberRepository> repository)
        {
            repository.Setup(x => x.FindBySsn(It.IsAny<string>())).ReturnsAsync(MemberTestUtils.GenerateValidMember());
            return repository;
        }

        public static Mock<IMemberRepository> withSuccesfulHasPassword(this Mock<IMemberRepository> repository)
        {
            repository.Setup(x => x.GetMemberPasswordHash(It.IsAny<Member>())).ReturnsAsync(It.IsAny<string>());
            return repository;
        }

        public static Mock<IMemberRepository> WithSuccesfulFindById(this Mock<IMemberRepository> repository)
        {
            repository.Setup(x => x.FindById(It.IsAny<string>())).ReturnsAsync(MemberTestUtils.GenerateValidMember());
            return repository;
        }

        public static Mock<IMemberRepository> WithSuccesfulFindByName(this Mock<IMemberRepository> repository)
        {
            repository.Setup(x => x.FindByUsername(It.IsAny<string>())).ReturnsAsync(MemberTestUtils.GenerateValidMember());
            return repository;
        }
        
        public static Mock<IMemberRepository> RemoveFromRoleAsync(this Mock<IMemberRepository> repository)
        {
            repository.Setup(x => x.RemoveFromRole(It.IsAny<Member>(), It.IsAny<string>()));
            return repository;
        }
        //TODO finish extension and unit tests for new code

    }
}
