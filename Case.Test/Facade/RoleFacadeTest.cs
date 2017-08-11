using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Facade;
using Case.Core.Repository.Interface;
using Case.Core.Utils;
using Case.Test.Facade.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using static Case.Test.TestUtils.Role.RoleTestUtils;

namespace Case.Test.Facade
{
    [TestClass]
    public class RoleFacadeTest
    {
        private RoleFacade _sut;
        private IRoleRepository _roleRepository;
        private Mock<IRoleRepository> _roleRepositoryMock;

        [TestInitialize]
        public void Setup()
        {
            _roleRepositoryMock = new Mock<IRoleRepository>();
            _roleRepository = _roleRepositoryMock.Object;
            _sut = new RoleFacade(_roleRepository);
        }

        #region Create

        [TestMethod]
        public async Task ValidRoleShouldBeCreated()
        {
            _roleRepositoryMock.WithValidInsert();
            var role = GenerateValidRole();
            await _sut.CreateAsync(role);
            _roleRepositoryMock.Verify(x => x.InsertAsync(role), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Cannot create null role")]
        public async Task NullRoleShouldNotBeCreated()
        {
            _roleRepositoryMock.WithValidInsert();
            Role role = null;
            await _sut.CreateAsync(role);
            _roleRepositoryMock.Verify(x => x.InsertAsync(It.IsAny<Role>()), Times.Never);
        }

        #endregion

        #region Update

        [TestMethod]
        public async Task ValidRoleShouldBeUpdated()
        {
            _roleRepositoryMock.WithValidUpdate();
            var role = GenerateValidRole();
            await _sut.UpdateAsync(role);
            _roleRepositoryMock.Verify(x => x.UpdateAsync(role), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Cannot update null role")]
        public async Task NullRoleShouldNotBeUpdated()
        {
            _roleRepositoryMock.WithValidUpdate();
            Role role = null;
            await _sut.UpdateAsync(role);
            _roleRepositoryMock.Verify(x => x.UpdateAsync(role), Times.Never);
        }

        #endregion

        #region Delete

        [TestMethod]
        public async Task ValidRoleShouldBeDeleted()
        {
            _roleRepositoryMock.WithValidDelete();
            var role = GenerateValidRole();
            await _sut.DeleteAsync(role);
            _roleRepositoryMock.Verify(x => x.DeleteAsync(role), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Cannot delete null role")]
        public async Task NullRoleShouldNotBeDeleted()
        {
            _roleRepositoryMock.WithValidDelete();
            Role role = null;
            await _sut.DeleteAsync(role);
            _roleRepositoryMock.Verify(x => x.DeleteAsync(role), Times.Never);
        }

        #endregion

        #region FindById

        [TestMethod]
        public async Task ValidIdShouldReturnValidRole()
        {
            _roleRepositoryMock.WithValidFindByStringId();
            string id = "1234"; //Needs updating
            var result = await _sut.FindByIdAsync(id);
            _roleRepositoryMock.Verify(x => x.FindById(Int32.Parse(id)), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Cannot find role with null id")]
        public async Task NullIdShouldNotReturnRole()
        {
            _roleRepositoryMock.WithValidFindByStringId();
            string id = null;
            var result = await _sut.FindByIdAsync(id);
            _roleRepositoryMock.Verify(x => x.FindById(Int32.Parse(id)), Times.Never);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Cannot find role with negative id")]
        public async Task NegativeIdShouldFail()
        {
            _roleRepositoryMock.WithValidFindByStringId();
            string id = "-2";
            var result = await _sut.FindByIdAsync(id);
            _roleRepositoryMock.Verify(x => x.FindById(Int32.Parse(id)), Times.Never);
            Assert.IsNull(result);
        }

        #endregion

        #region FindByName

        [TestMethod]
        public async Task ValidNameShouldReturnValidRole()
        {
            _roleRepositoryMock.WithValidFindByName();
            var name = Roles.NotVerifiedMember;
            var result = await _sut.FindByNameAsync(name);
            _roleRepositoryMock.Verify(x => x.FindByName(name), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Cannot find role with null name")]
        public async Task NullNameShouldNotReturnRole()
        {
            _roleRepositoryMock.WithValidFindByName();
            string name = null;
            var result = await _sut.FindByNameAsync(name);
            _roleRepositoryMock.Verify(x => x.FindByName(name), Times.Never);
        }

        #endregion
    }
}
