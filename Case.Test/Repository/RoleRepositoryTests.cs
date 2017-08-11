using System;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Mapper;
using Case.Core.Repository;
using Case.Test.Repository.Extensions.RoleRepository;
using Case.Test.TestUtils.Role;
using DataAccess.EntityMaps;
using DataAccess.Queries.Member;
using DataAccess.Queries.Role;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Case.Test.Repository
{
    [TestClass]
    public class RoleRepositoryTests
    {
        private RoleRepository _sut;
        private Mock<IMemberAccess> _access;
        private Mock<IMapper<Role>> _mapper;
        private Mock<IRoleAccess> _rAccess;

        [TestInitialize]
        public void SetUp()
        {
            _access = new Mock<IMemberAccess>();
            _mapper = new Mock<IMapper<Role>>();
            _rAccess = new Mock<IRoleAccess>();
        }

        [TestCleanup]
        public void TearDown()
        {
            _sut = null;
            _mapper = null;
            _access = null;
        }

        #region InsertRole

        [TestMethod]
        public async Task InsertOneAsyncTest_ValidRole_HappyPath()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());
            _access.WithValidInsert(true);

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            var role = RoleTestUtils.GenerateValidRole();

            //act
            await _sut.InsertAsync(role);

            //assert
            _rAccess.Verify(x => x.InsertOneAsync(It.IsAny<IMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Role cannot be null.")]
        public async Task InsertOneAsyncTest_NullRole_Fails()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());
            _access.WithValidInsert(true);

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            //act
            await _sut.InsertAsync(null);

            //assert
            _rAccess.Verify(x => x.InsertOneAsync(It.IsAny<IMap>()), Times.Never);
        }

        #endregion

        #region GetAsync

        [TestMethod]
        [ExpectedException(typeof(Exception), "You should use FindRoleNameById() instead")]
        public async Task GetAsyncTest_ValidRole_Fails()
        {
            //arrange
            var role = RoleTestUtils.GenerateValidRole();

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            //act
            await _sut.GetAsync(Int32.Parse(role.Id));
        }

        #endregion

        #region UpdateAsync

        [TestMethod]
        public async Task UpdateAsyncTest_ValidRole_HappyPath()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            //act
            await _sut.UpdateAsync(RoleTestUtils.GenerateValidRole());

            //assert
            _rAccess.Verify(x => x.UpdateOneAsync(It.IsAny<IMap>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Role cannot be null")]
        public async Task UpdateAsyncTest_InvalidRole_Fails()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            //act
            await _sut.UpdateAsync(RoleTestUtils.GenerateInvalidRole());

            //assert
            _rAccess.Verify(x => x.UpdateOneAsync(It.IsAny<IMap>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Role cannot be null")]
        public async Task UpdateAsyncTest_NullRole_Fails()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            //act
            await _sut.UpdateAsync(null);

            //assert
            _rAccess.Verify(x => x.UpdateOneAsync(It.IsAny<IMap>()), Times.Never());
        }

        #endregion

        #region DeleteAsync

        [TestMethod]
        public async Task DeleteAsyncTest_ValidRole_HappyPath()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            //act
            await _sut.DeleteAsync(RoleTestUtils.GenerateValidRole());

            //assert
            _rAccess.Verify(x => x.DeleteOneAsync(It.IsAny<IMap>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Role cannot be null")]
        public async Task DeleteAsyncTest_InvalidRole_Fails()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            //act
            await _sut.DeleteAsync(RoleTestUtils.GenerateInvalidRole());

            //assert
            _rAccess.Verify(x => x.DeleteOneAsync(It.IsAny<IMap>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Role cannot be null")]
        public async Task DeleteAsyncTest_NullRole_Fails()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            //act
            await _sut.DeleteAsync(null);

            //assert
            _rAccess.Verify(x => x.DeleteOneAsync(It.IsAny<IMap>()), Times.Never());
        }

        #endregion

        #region InsertOrUpdateAsync

        [TestMethod]
        public async Task InsertOrUpdateAsyncTest_ValidRole_HappyPath()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            //act
            await _sut.InsertOrUpdateAsync(RoleTestUtils.GenerateValidRole());

            //assert
            _rAccess.Verify(x => x.InsertOrUpdateAsync(It.IsAny<IMap>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Role cannot be null")]
        public async Task InsertOrUpdateAsyncTest_InvalidRole_Fails()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            //act
            await _sut.InsertOrUpdateAsync(RoleTestUtils.GenerateInvalidRole());

            //assert
            _rAccess.Verify(x => x.InsertOrUpdateAsync(It.IsAny<IMap>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Role cannot be null")]
        public async Task InsertOrUpdateAsyncTest_NullRole_Fails()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            //act
            await _sut.InsertOrUpdateAsync(null);

            //assert
            _rAccess.Verify(x => x.InsertOrUpdateAsync(It.IsAny<IMap>()), Times.Never());
        }

        #endregion

        #region DeleteByIdAsync

        [TestMethod]
        [ExpectedException(typeof(Exception), "Member doesn't exist")]
        public async Task DeleteByIdAsyncTest_ValidRole_HappyPath()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            var role = RoleTestUtils.GenerateValidRole();

            //act
            await _sut.DeleteAsync(Int32.Parse(role.Id));

            //assert
            _rAccess.Verify(x => x.DeleteOneAsync(It.IsAny<IMap>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Role cannot be null")]
        public async Task DeleteByIdAsyncTest_InvalidRole_Fails()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            //act
            await _sut.DeleteAsync(RoleTestUtils.GenerateInvalidRole());

            //assert
            _rAccess.Verify(x => x.DeleteOneAsync(It.IsAny<IMap>()), Times.Never());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Role id cannot be invalid")]
        public async Task DeleteByIdAsyncTest_NullRole_Fails()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            //act
            await _sut.DeleteAsync(-1);

            //assert
            _rAccess.Verify(x => x.DeleteOneAsync(It.IsAny<IMap>()), Times.Never());
        }

        #endregion

        #region FindById

        [TestMethod]
        public async Task FindByIdAsyncTest_ValidRole_HappyPath()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            var role = RoleTestUtils.GenerateValidRole();

            //act
           var result = await _sut.FindById(Int32.Parse(role.Id));

            //assert
            _access.Verify(x => x.FindMemberRoleByRoleId(It.IsAny<int>()), Times.Once());
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Id cannot be invalid")]
        public async Task FindByIdAsyncTest_InvalidRole_Fails()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            var role = RoleTestUtils.GenerateInvalidRole();

            //act
            await _sut.FindById(Int32.Parse(role.Id));

            //assert
            _access.Verify(x => x.FindMemberRoleByRoleId(It.IsAny<int>()), Times.Never());
        }

        #endregion

        #region FindByName

        [TestMethod]
        public async Task FindByNameAsyncTest_ValidName_HappyPath()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            //act
            await _sut.FindByName(RoleTestUtils.GenerateValidRole().Name);

            //assert
            _access.Verify(x => x.FindMemberRoleByName(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Role name cannot be invalid")]
        public async Task FindByNameAsyncTest_InvalidName_Fails()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            //act
            await _sut.FindByName(RoleTestUtils.GenerateInvalidRole().Name);

            //assert
            _access.Verify(x => x.FindMemberRoleByName(It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Role name cannot be null")]
        public async Task FindByNameAsyncTest_NullName_Fails()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            //act
            await _sut.FindByName(null);

            //assert
            _access.Verify(x => x.FindMemberRoleByName(It.IsAny<string>()), Times.Once());
        }

        #endregion

        #region FindRoleNameAsync

        [TestMethod]
        public async Task FindRoleNameAsyncTest_ValidRole_HappyPath()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            //act
            await _sut.FindRoleName(RoleTestUtils.GenerateValidRole().Id);
        }

        [TestMethod]
        public async Task FindRoleNameAsyncTest_InvalidRole_Passes()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            //act
            await _sut.FindRoleName(RoleTestUtils.GenerateInvalidRole().Id);

            //assert
            _access.Verify(x => x.FindMemberRoleByRoleId(It.IsAny<int>()), Times.Never());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "RoleName cannot be null")]
        public async Task FindRoleNameAsyncTest_NullRole_Fails()
        {
            //arrange
            _mapper.WithMap(RoleMapTestUtils.GenerateValidRoleMap());

            _sut = new RoleRepository(_access.Object, _rAccess.Object, _mapper.Object);

            //act
            await _sut.FindRoleName(null);

            //assert
            _access.Verify(x => x.FindMemberRoleByRoleId(It.IsAny<int>()), Times.Never());
        }

        #endregion
    }
}
