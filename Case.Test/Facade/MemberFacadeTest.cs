using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Facade;
using Case.Core.Repository.Interface;
using Case.Core.Utils;
using Case.Test.TestUtils;
using Case.Test.TestUtils.Role;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using static Case.Test.TestUtils.MemberTestUtils;
using static Case.Test.Facade.Extensions.MemberRepositoryMockExtensions;

namespace Case.Test.Facade
{
    [TestClass]
    public class MemberFacadeTest
    {
        private MemberFacade _facade;
        private Mock<IMemberRepository> _repositoryMock;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<IMemberRepository>();
            _facade = new MemberFacade(_repositoryMock.Object);
        }

        #region Register

        [TestMethod]
        public async Task RegisterValidMemberShouldReturnTrue()
        {
            _repositoryMock.WithSuccesfulInsert();
            var member = GenerateValidMember();
            var result = await _facade.RegisterMember(member);
            _repositoryMock.Verify(x => x.InsertAsync(member), Times.Once);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task RegisterInSuccessfulMemberShouldReturnFalse()
        {
            _repositoryMock.WithSuccesfulInsert();
            var member = GenerateInvalidMember();
            var result = await _facade.RegisterMember(member);
            _repositoryMock.Verify(x => x.InsertAsync(member), Times.Never);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task RegisterNullMemberShouldReturnFalse()
        {
            _repositoryMock.WithSuccesfulInsert();
            Member member = null;
            var result = await _facade.RegisterMember(member);
            _repositoryMock.Verify(x => x.InsertAsync(It.IsAny<Member>()), Times.Never);
        }

        #endregion

        #region FindMember

        [TestMethod]
        public async Task FindMemberWithSuccesfulSsnValidPasswordShouldReturnValidMember()
        {
            _repositoryMock.WithSuccesfulFindBySsnAndPassword();
            var ssn = GenerateValidSsn();
            var password = "password";
            var result = await _facade.FindMember(ssn, password);
            _repositoryMock.Verify(x => x.FindBySsnAndPassword(ssn,password), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task FindMemberWithSuccesfulSsnInvalidPasswordShouldReturnNull()
        {
            _repositoryMock.WithSuccesfulFindBySsnAndPassword();
            var ssn = GenerateValidSsn();
            var password = string.Empty;
            var result = await _facade.FindMember(ssn, password);
            _repositoryMock.Verify(x => x.FindBySsnAndPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task FindMemberWithInvalidSsnValidPasswordShouldReturnNull()
        {
            _repositoryMock.WithSuccesfulFindBySsnAndPassword();
            var ssn = GenerateInvalidSsn();
            var password = "password";
            var result = await _facade.FindMember(ssn, password);
            _repositoryMock.Verify(x => x.FindBySsnAndPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task FindMemberWithInvalidSsnAndInValidPassowrdShouldReturnNull()
        {
            _repositoryMock.WithSuccesfulFindBySsnAndPassword();
            var ssn = GenerateInvalidSsn();
            var password = string.Empty;
            var result = await _facade.FindMember(ssn, password);
            _repositoryMock.Verify(x => x.FindBySsnAndPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task FindMemberWithSuccesfulSsnNullPasswordShouldReturnNull()
        {
            _repositoryMock.WithSuccesfulFindBySsnAndPassword();
            var ssn = GenerateValidSsn();
            string password = null;
            var result = await _facade.FindMember(ssn, password);
            _repositoryMock.Verify(x => x.FindBySsnAndPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task FindMemberWithNullSsnValidPassword()
        {
            _repositoryMock.WithSuccesfulFindBySsnAndPassword();
            string ssn = null;
            string password = "password";
            var result = await _facade.FindMember(ssn, password);
            _repositoryMock.Verify(x => x.FindBySsnAndPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }
        #endregion

        #region GetRoles

        [TestMethod]
        public async Task GetRoles_ValidMember_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccesfulGetRoleNameCannotRent();
            var member = MemberTestUtils.GenerateValidMember();

            //act
            var result = await _facade.GetRolesAsync(member);
            
            //assert
            _repositoryMock.Verify(x => x.GetRoleName(It.IsAny<Member>()), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "SSN cannot be invalid")]
        public async Task GetRoles_InvalidMember_Fails()
        {
            //arrange
            _repositoryMock.WithSuccesfulGetRoleNameCannotRent();
            var member = MemberTestUtils.GenerateInvalidMember();

            //act
            var result = await _facade.GetRolesAsync(member);

            //assert
            _repositoryMock.Verify(x => x.GetRoleName(It.IsAny<Member>()), Times.Never);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Member cannot be invalid")]
        public async Task GetRoles_NullMember_Fails()
        {
            //arrange
            _repositoryMock.WithSuccesfulGetRoleNameCannotRent();

            //act
            var result = await _facade.GetRolesAsync(null);

            //assert
            _repositoryMock.Verify(x => x.GetRoleName(It.IsAny<Member>()), Times.Never);
            Assert.IsNull(result);
        }

        #endregion

        #region GetMemberInRoleCount
        
        [TestMethod]
        public async Task GetMemberInRoleCount_ValidRole_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccessfulInRoleCount();
            var role = RoleTestUtils.GenerateValidRole();

            //act
            var result = await _facade.GetMemberInRoleCount(role.Name);

            //assert
            _repositoryMock.Verify(x => x.GetMemberInRoleCount(It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Role name cannot be invalid")]
        public async Task GetMemberInRoleCount_InvalidRole_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccessfulInRoleCount();
            var role = RoleTestUtils.GenerateInvalidRole();

            //act
            var result = await _facade.GetMemberInRoleCount(role.Name);

            //assert
            _repositoryMock.Verify(x => x.GetMemberInRoleCount(It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Role name cannot be null")]
        public async Task GetMemberInRoleCount_NullRole_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccessfulInRoleCount();

            //act
            var result = await _facade.GetMemberInRoleCount(null);

            //assert
            _repositoryMock.Verify(x => x.GetMemberInRoleCount(It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        #endregion

        #region IsInRole

        [TestMethod]
        public async Task IsMemberInRole_ValidMember_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccessfulIsInRole();
            var member = MemberTestUtils.GenerateValidMember();
            var role = RoleTestUtils.GenerateValidRole();

            //act
            var result = await _facade.IsInRoleAsync(member, role.Name);

            //assert
            _repositoryMock.Verify(x => x.IsInRole(It.IsAny<Member>(), It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Member or role cannot be invalid")]
        public async Task IsMemberInRole_InvalidMember_Fails()
        {
            //arrange
            _repositoryMock.WithSuccessfulIsInRole();
            var member = MemberTestUtils.GenerateInvalidMember();
            var role = RoleTestUtils.GenerateValidRole();

            //act
            var result = await _facade.IsInRoleAsync(member, role.Name);

            //assert
            _repositoryMock.Verify(x => x.IsInRole(It.IsAny<Member>(), It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Member or role cannot be invalid")]
        public async Task IsMemberInRole_NullMember_Fails()
        {
            //arrange
            _repositoryMock.WithSuccessfulIsInRole();
            var role = RoleTestUtils.GenerateValidRole();

            //act
            var result = await _facade.IsInRoleAsync(null, role.Name);

            //assert
            _repositoryMock.Verify(x => x.IsInRole(It.IsAny<Member>(), It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Member or role cannot be invalid")]
        public async Task IsMemberInRole_NullRole_Fails()
        {
            //arrange
            _repositoryMock.WithSuccessfulIsInRole();

            var member = MemberTestUtils.GenerateValidMember();

            //act
            var result = await _facade.IsInRoleAsync(member, null);

            //assert
            _repositoryMock.Verify(x => x.IsInRole(It.IsAny<Member>(), It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        #endregion

        #region UpdateAsync

        [TestMethod]
        public async Task UpdateAsync_ValidMember_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccesfulUpdate();
            var member = GenerateValidMember();

            //act
            await _facade.UpdateAsync(member);

            //assert
            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Member>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Member cannot be invalid")]
        public async Task UpdateAsync_InvalidMember_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccesfulUpdate();
            var member = GenerateInvalidMember();

            //act
            await _facade.UpdateAsync(member);

            //assert
            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Member>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Member cannot be null")]
        public async Task UpdateAsync_NullMember_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccesfulUpdate();

            //act
            await _facade.UpdateAsync(null);

            //assert
            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Member>()), Times.Never);
        }

        #endregion

        #region CreateAsync

        [TestMethod]
        public async Task CreateAsync_ValidMember_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccesfulInsert();
            var member = GenerateValidMember();

            //act
            await _facade.CreateAsync(member);

            //assert
            _repositoryMock.Verify(x => x.InsertAsync(It.IsAny<Member>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "member cannot be invalid")]
        public async Task CreateAsync_InvalidMember_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccesfulInsert();
            var member = GenerateInvalidMember();

            //act
            await _facade.CreateAsync(member);

            //assert
            _repositoryMock.Verify(x => x.InsertAsync(It.IsAny<Member>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Member cannot be null")]
        public async Task CreateAsync_NullMember_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccesfulInsert();

            //act
            await _facade.CreateAsync(null);

            //assert
            _repositoryMock.Verify(x => x.InsertAsync(It.IsAny<Member>()), Times.Never);
        }

        #endregion

        #region DeleteAsync

        [TestMethod]
        public async Task DeleteAsync_ValidMember_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccesfulDelete();
            var member = GenerateValidMember();

            //act
            await _facade.DeleteAsync(member);

            //assert
            _repositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Member>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Member cannot be invalid")]
        public async Task DeleteAsync_InvalidMember_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccesfulDelete();
            var member = GenerateInvalidMember();

            //act
            await _facade.DeleteAsync(member);

            //assert
            _repositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Member>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Member cannot be null")]
        public async Task DeleteAsync_NullMember_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccesfulDelete();

            //act
            await _facade.DeleteAsync(null);

            //assert
            _repositoryMock.Verify(x => x.InsertAsync(It.IsAny<Member>()), Times.Never);
        }

        #endregion

        #region FindWithRoleNamePagedModel

        [TestMethod]
        public async Task FindWithRoleNamePagedModel_ValidRole_HappyPath()
        {
            //arrange
            var role = RoleTestUtils.GenerateValidRole();
            _repositoryMock.WithSuccesfulFindRoleNamePagedModel(role.Name);

            //act
            var result = await _facade.FindWithRoleNamePagedModel(role.Name, 1, 2);

            //assert
            _repositoryMock.Verify(x => x.FindModelsWithRoleName(It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Role cannot be invalid")]
        public async Task FindWithRoleNamePagedModel_InvalidRole_Fails()
        {
            //arrange
            var role = RoleTestUtils.GenerateInvalidRole();
            _repositoryMock.WithSuccesfulFindRoleNamePagedModel(role.Name);

            //act
            var result = await _facade.FindWithRoleNamePagedModel(role.Name, 1, 2);

            //assert
            _repositoryMock.Verify(x => x.FindModelsWithRoleName(It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Role cannot be null")]
        public async Task FindWithRoleNamePagedModel_NullRole_Fails()
        {
            //arrange
            _repositoryMock.WithSuccesfulFindRoleNamePagedModel(null);

            //act
            var result = await _facade.FindWithRoleNamePagedModel(null, 1, 2);

            //assert
            _repositoryMock.Verify(x => x.FindModelsWithRoleName(It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid page number")]
        public async Task FindWithRoleNamePagedModel_InvalidPage_Fails()
        {
            //arrange
            var role = RoleTestUtils.GenerateValidRole();
            _repositoryMock.WithSuccesfulFindRoleNamePagedModel(role.Name);

            //act
            var result = await _facade.FindWithRoleNamePagedModel(role.Name, -2, 2);

            //assert
            _repositoryMock.Verify(x => x.FindModelsWithRoleName(It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalide page size")]
        public async Task FindWithRoleNamePagedModel_InvalidPageSize_Fails()
        {
            //arrange
            var role = RoleTestUtils.GenerateValidRole();
            _repositoryMock.WithSuccesfulFindRoleNamePagedModel(role.Name);

            //act
            var result = await _facade.FindWithRoleNamePagedModel(role.Name, 1, 0);

            //assert
            _repositoryMock.Verify(x => x.FindModelsWithRoleName(It.IsAny<string>()), Times.Never);
            Assert.IsNull(result);
        }

        #endregion

        #region VerifyMember

        [TestMethod]
        public async Task VerifyMember_ValidMember_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccessfulUpdateRole();
            var member = MemberTestUtils.GenerateValidMember();

            //act
            await _facade.VerifyMember(member.Ssn);

            //assert
            _repositoryMock.Verify(x => x.UpdateRole(It.IsAny<Member>(), Roles.VerifiedMember), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Member cannot be invalid")]
        public async Task VerifyMember_InvalidMember_Fails()
        {
            //arrange
            _repositoryMock.WithSuccessfulUpdateRole();
            var member = MemberTestUtils.GenerateInvalidMember();

            //act
            await _facade.VerifyMember(member.Ssn);

            //assert
            _repositoryMock.Verify(x => x.UpdateRole(It.IsAny<Member>(), Roles.VerifiedMember), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Member cannot be null")]
        public async Task VerifyMember_NullMember_Fails()
        {
            //arrange
            _repositoryMock.WithSuccessfulUpdateRole();

            //act
            await _facade.VerifyMember(null);

            //assert
            _repositoryMock.Verify(x => x.UpdateRole(It.IsAny<Member>(), Roles.VerifiedMember), Times.Never);
        }

        #endregion

        #region HasPasswordAsync

        [TestMethod]
        public async Task HasPasswordAsync_ValidMember_HappyPath()
        {
            //arrange
            _repositoryMock.withSuccesfulHasPassword();
            var member = MemberTestUtils.GenerateValidMember();

            //act
            await _facade.HasPasswordAsync(member);

            //assert
            _repositoryMock.Verify(x => x.GetMemberPasswordHash(It.IsAny<Member>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Member cannot be invalid")]
        public async Task HasPasswordAsync_InvalidMember_Fails()
        {
            //arrange
            _repositoryMock.withSuccesfulHasPassword();
            var member = MemberTestUtils.GenerateInvalidMember();

            //act
            await _facade.HasPasswordAsync(member);

            //assert
            _repositoryMock.Verify(x => x.GetMemberPasswordHash(It.IsAny<Member>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Member cannot be null")]
        public async Task HasPasswordAsync_NullMember_Fails()
        {
            //arrange
            _repositoryMock.withSuccesfulHasPassword();

            //act
            await _facade.HasPasswordAsync(null);

            //assert
            _repositoryMock.Verify(x => x.GetMemberPasswordHash(It.IsAny<Member>()), Times.Never);
        }

        #endregion

        #region FindByIdAsync

        [TestMethod]
        public async Task FindByIdAsyncTest_ValidId_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccesfulFindById();
            var member = MemberTestUtils.GenerateValidMember();

            //act
            await _facade.FindByIdAsync(member.Id);

            //assert
            _repositoryMock.Verify(x => x.FindById(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Id cannot be invalid")]
        public async Task FindByIdAsyncTest_InvalidId_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccesfulFindById();
            var member = MemberTestUtils.GenerateInvalidMember();
            member.Id = "";

            //act
            await _facade.FindByIdAsync(member.Id);

            //assert
            _repositoryMock.Verify(x => x.FindById(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Id cannot be invalid")]
        public async Task FindByIdAsyncTest_NullId_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccesfulFindById();

            //act
            await _facade.FindByIdAsync(null);

            //assert
            _repositoryMock.Verify(x => x.FindById(It.IsAny<string>()), Times.Never);
        }

        #endregion

        #region FindByNameAsync

        [TestMethod]
        public async Task FindByNameAsyncTest_ValidName_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccesfulFindByName();
            var member = MemberTestUtils.GenerateValidMember();

            //act
            await _facade.FindByNameAsync(member.UserName);

            //assert
            _repositoryMock.Verify(x => x.FindByUsername(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Id cannot be invalid")]
        public async Task FindByNameAsyncTest_InvalidName_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccesfulFindByName();
            var member = MemberTestUtils.GenerateInvalidMember();
            member.UserName = "";

            //act
            await _facade.FindByNameAsync(member.UserName);

            //assert
            _repositoryMock.Verify(x => x.FindByUsername(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Username cannot be invalid")]
        public async Task FindByNameAsyncTest_NullName_HappyPath()
        {
            //arrange
            _repositoryMock.WithSuccesfulFindByName();

            //act
            await _facade.FindByNameAsync(null);

            //assert
            _repositoryMock.Verify(x => x.FindByUsername(It.IsAny<string>()), Times.Never);
        }

        #endregion

        #region RemoveFromRoleAsync

        [TestMethod]
        public async Task RemoveFromRoleAsync_ValidMember_HappyPath()
        {
            //arrange
            _repositoryMock.RemoveFromRoleAsync();
            var member = MemberTestUtils.GenerateValidMember();
            var role = RoleTestUtils.GenerateValidRole();

            //act
            await _facade.RemoveFromRoleAsync(member, role.Name);

            //assert
            _repositoryMock.Verify(x => x.RemoveFromRole(It.IsAny<Member>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Member cannot be invalid")]
        public async Task RemoveFromRoleAsync_InvalidMember_HappyPath()
        {
            //arrange
            _repositoryMock.RemoveFromRoleAsync();
            var member = MemberTestUtils.GenerateInvalidMember();
            member.UserName = "";
            var role = RoleTestUtils.GenerateValidRole();

            //act
            await _facade.RemoveFromRoleAsync(member, role.Name);

            //assert
            _repositoryMock.Verify(x => x.RemoveFromRole(It.IsAny<Member>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Member cannot be invalid")]
        public async Task RemoveFromRoleAsync_NullMember_HappyPath()
        {
            //arrange
            _repositoryMock.RemoveFromRoleAsync();
            var role = RoleTestUtils.GenerateValidRole();

            //act
            await _facade.RemoveFromRoleAsync(null, role.Name);

            //assert
            _repositoryMock.Verify(x => x.RemoveFromRole(It.IsAny<Member>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Role cannot be invalid")]
        public async Task RemoveFromRoleAsync_InvalidRole_Fails()
        {
            //arrange
            _repositoryMock.RemoveFromRoleAsync();
            var member = MemberTestUtils.GenerateValidMember();
            var role = RoleTestUtils.GenerateInvalidRole();

            //act
            await _facade.RemoveFromRoleAsync(member, role.Name);

            //assert
            _repositoryMock.Verify(x => x.RemoveFromRole(It.IsAny<Member>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Role cannot be invalid")]
        public async Task RemoveFromRoleAsync_NullRole_Fails()
        {
            //arrange
            _repositoryMock.RemoveFromRoleAsync();
            var member = MemberTestUtils.GenerateValidMember();

            //act
            await _facade.RemoveFromRoleAsync(member, null);

            //assert
            _repositoryMock.Verify(x => x.RemoveFromRole(It.IsAny<Member>(), It.IsAny<string>()), Times.Never);
        }

        #endregion
    }
}