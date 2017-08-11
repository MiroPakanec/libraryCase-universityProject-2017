using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Repository.Interface;
using Case.Test.TestUtils;
using Case.Test.TestUtils.Role;
using Moq;

namespace Case.Test.Facade.Extensions
{
    public static class RoleRepositoryMockExtensions
    {
        public static Mock<IRoleRepository> WithValidInsert(this Mock<IRoleRepository> repository)
        {
            repository.Setup(x => x.InsertAsync(It.IsAny<Role>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IRoleRepository> WithValidUpdate(this Mock<IRoleRepository> repository)
        {
            repository.Setup(x => x.UpdateAsync(It.IsAny<Role>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IRoleRepository> WithValidDelete(this Mock<IRoleRepository> repository)
        {
            repository.Setup(x => x.DeleteAsync(It.IsAny<Role>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IRoleRepository> WithValidGet(this Mock<IRoleRepository> repository)
        {
            repository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(RoleTestUtils.GenerateValidRole);
            return repository;
        }

        public static Mock<IRoleRepository> WithValidInsertOrUpdate(this Mock<IRoleRepository> repository)
        {
            repository.Setup(x => x.InsertOrUpdateAsync(It.IsAny<Role>())).ReturnsAsync(true);
            return repository;
        }

        public static Mock<IRoleRepository> WithValidFindByStringId(this Mock<IRoleRepository> repository)
        {
            repository.Setup(x => x.FindById(It.IsAny<int>())).ReturnsAsync(RoleTestUtils.GenerateValidRole);
            return repository;
        }

        public static Mock<IRoleRepository> WithValidFindByName(this Mock<IRoleRepository> repository)
        {
            repository.Setup(x => x.FindByName(It.IsAny<string>()))
                .ReturnsAsync(new List<Role>() {RoleTestUtils.GenerateValidRole()});
            return repository;
        }
    }
}
