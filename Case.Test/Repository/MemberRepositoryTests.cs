using System;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Mapper;
using Case.Core.Repository;
using Case.Test.Repository.Extensions.MemberRepository;
using Case.Test.TestUtils;
using Case.Test.TestUtils.Role;
using DataAccess.EntityMaps.Member;
using DataAccess.Queries.Member;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Case.Test.Repository
{
    [TestClass]
    public class MemberRepositoryTests
    {
        private MemberRepository _sut;
        private Mock<IMemberAccess> _access;
        private Mock<IMapper<Member>> _mapper;

        [TestInitialize]
        public void SetUp()
        {
            _mapper = new Mock<IMapper<Member>>();
            _access = new Mock<IMemberAccess>();
        }

        [TestCleanup]
        public void TearDown()
        {
            _sut = null;
            _mapper = null;
            _access = null;
        }

        [TestMethod]
        public async Task InsertAsyncTest_ValidMember_HappyPath()
        {
            //arrange
            _mapper.WithMap(MemberMapTestUtils.GenerateValidMemberMap());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.InsertAsync(MemberTestUtils.GenerateValidMember());

            //assert
            _access.Verify(m => m.InsertOneAsync(It.IsAny<MemberMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Member cannot be null.")]
        public async Task InsertAsyncTest_NullMember_ExceptionalCase()
        {
            //arrange
            _mapper.WithMap(MemberMapTestUtils.GenerateValidMemberMap());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.InsertAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Member map cannot be null.")]
        public async Task InsertAsyncTest_NullMemberMap_ExceptionalCase()
        {
            //arrange
            _mapper.WithMap(null);

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.InsertAsync(MemberTestUtils.GenerateValidMember());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Member cannot be selected with Id.")]
        public async Task GetAsync_ObsoleteMethodCall_ExceptionalCase()
        {
            //arrange
            const int dummyId = 0;
            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.GetAsync(dummyId);
        }

        [TestMethod]
        public async Task UpdateAsyncTest_ValidMember_HappyPath()
        {
            //arrange
            _mapper.WithMap(MemberMapTestUtils.GenerateValidMemberMap());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.UpdateAsync(MemberTestUtils.GenerateValidMember());

            //assert
            _access.Verify(m => m.UpdateOneAsync(It.IsAny<MemberMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Member cannot be null.")]
        public async Task UpdateAsyncTest_NullMember_ExceptionalCase()
        {
            //arrange
            _mapper.WithMap(MemberMapTestUtils.GenerateValidMemberMap());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.UpdateAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Member map cannot be null.")]
        public async Task UpdateAsyncTest_NullMemberMap_ExceptionalCase()
        {
            //arrange
            _mapper.WithMap(null);

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.UpdateAsync(MemberTestUtils.GenerateValidMember());
        }

        [TestMethod]
        public async Task DeleteAsyncTest_ValidMember_HappyPath()
        {
            //arrange
            _mapper.WithMap(MemberMapTestUtils.GenerateValidMemberMap());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.DeleteAsync(MemberTestUtils.GenerateValidMember());

            //assert
            _access.Verify(m => m.DeleteOneAsync(It.IsAny<MemberMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Member cannot be null.")]
        public async Task DeleteAsyncTest_NullMember_ExceptionalCase()
        {
            //arrange
            _mapper.WithMap(MemberMapTestUtils.GenerateValidMemberMap());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.DeleteAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Member map cannot be null.")]
        public async Task DeleteAsyncTest_NullMemberMap_ExceptionalCase()
        {
            //arrange
            _mapper.WithMap(null);

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.DeleteAsync(MemberTestUtils.GenerateValidMember());
        }

        [TestMethod]
        public async Task InsertOrUpdateTest_ValidMember_HappyPath()
        {
            //arrange
            _mapper.WithMap(MemberMapTestUtils.GenerateValidMemberMap());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.InsertOrUpdateAsync(MemberTestUtils.GenerateValidMember());

            //assert
            _access.Verify(m => m.InsertOrUpdateAsync(It.IsAny<MemberMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Member cannot be null.")]
        public async Task InsertOrUpdateTest_NullMember_ExceptionalCase()
        {
            //arrange
            _mapper.WithMap(MemberMapTestUtils.GenerateValidMemberMap());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.InsertOrUpdateAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Member map cannot be null.")]
        public async Task InsertOrUpdateTest_NullMemberMap_ExceptionalCase()
        {
            //arrange
            _mapper.WithMap(null);

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.InsertOrUpdateAsync(MemberTestUtils.GenerateValidMember());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Member cannot be deleted with id.")]
        public async Task DeleteAsyncTestWithId_ValidMember_HappyPath()
        {
            //arrange
            const int dummyId = 10;
            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.DeleteAsync(dummyId);
        }

        [TestMethod]
        public async Task FindBySsnAndPassword_ValidSsnAndPassword_HappyPath()
        {
            //arrange
            var validSsn = MemberTestUtils.GenerateValidSsn();
            var validPassword = MemberTestUtils.GenerateValidMember().Password;

            _access.WithFindBySsnAndPasswordResult(MemberMapTestUtils.GenerateValidMemberMap() as IMemberMap);
            _mapper.WithUnmap(MemberTestUtils.GenerateValidMember());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.FindBySsnAndPassword(validSsn, validPassword);

            //assert
            _access.Verify(m => m.FindBySsnAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Ssn cannot be null.")]
        public async Task FindBySsnAndPassword_NullSsn_ExceptionalCase()
        {
            //arrange
            const string nullSsn = null;
            var validPassword = MemberTestUtils.GenerateValidMember().Password;

            _access.WithFindBySsnAndPasswordResult(MemberMapTestUtils.GenerateValidMemberMap() as IMemberMap);
            _mapper.WithUnmap(MemberTestUtils.GenerateValidMember());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.FindBySsnAndPassword(nullSsn, validPassword);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Password cannot be null.")]
        public async Task FindBySsnAndPassword_NullPassword_ExceptionalCase()
        {
            //arrange
            var validSsn = MemberTestUtils.GenerateValidSsn();
            const string nullPassword = null;

            _access.WithFindBySsnAndPasswordResult(MemberMapTestUtils.GenerateValidMemberMap() as IMemberMap);
            _mapper.WithUnmap(MemberTestUtils.GenerateValidMember());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.FindBySsnAndPassword(validSsn, nullPassword);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Ssn is not valid.")]
        public async Task FindBySsnAndPassword_InvalidSsn_ExceptionalCase()
        {
            //arrange
            var invalidSsn = MemberTestUtils.GenerateInvalidSsn();
            var validPassword = MemberTestUtils.GenerateValidMember().Password;

            _access.WithFindBySsnAndPasswordResult(MemberMapTestUtils.GenerateValidMemberMap() as IMemberMap);
            _mapper.WithUnmap(MemberTestUtils.GenerateValidMember());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.FindBySsnAndPassword(invalidSsn, validPassword);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Password is not valid.")]
        public async Task FindBySsnAndPassword_InvalidPassword_ExceptionalCase()
        {
            //arrange
            var validSsn = MemberTestUtils.GenerateValidSsn();
            var invalidPassword = MemberTestUtils.GenerateInvalidMember().Password;

            _access.WithFindBySsnAndPasswordResult(MemberMapTestUtils.GenerateValidMemberMap() as IMemberMap);
            _mapper.WithUnmap(MemberTestUtils.GenerateValidMember());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.FindBySsnAndPassword(validSsn, invalidPassword);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Member map cannot be null.")]
        public async Task FindBySsnAndPassword_NullMemberMap_ExceptionalCase()
        {
            //arrange
            var validSsn = MemberTestUtils.GenerateValidSsn();
            var validPassword = MemberTestUtils.GenerateValidMember().Password;

            _access.WithFindBySsnAndPasswordResult(null);
            _mapper.WithUnmap(MemberTestUtils.GenerateValidMember());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.FindBySsnAndPassword(validSsn, validPassword);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Member cannot be null.")]
        public async Task FindBySsnAndPassword_NullMember_ExceptionalCase()
        {
            //arrange
            var validSsn = MemberTestUtils.GenerateValidSsn();
            var validPassword = MemberTestUtils.GenerateValidMember().Password;

            _access.WithFindBySsnAndPasswordResult(MemberMapTestUtils.GenerateValidMemberMap() as IMemberMap);
            _mapper.WithUnmap(null);

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.FindBySsnAndPassword(validSsn, validPassword);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Member cannot be selected with Id.")]
        public async Task FindById_ObsoleteMethodCall_ExceptionalCase()
        {
            //arrange
            const string dummyId = "dummyId";
            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.FindById(dummyId);
        }

        [TestMethod]
        public async Task FindBySsn_ValidSsn_HappyPath()
        {
            //arrange
            var validSsn = MemberTestUtils.GenerateValidSsn();

            _access.WithFindBySsnResult(MemberMapTestUtils.GenerateValidMemberMap() as IMemberMap);
            _mapper.WithUnmap(MemberTestUtils.GenerateValidMember());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.FindBySsn(validSsn);

            //assert
            _access.Verify(m => m.FindBySsn(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Ssn cannot be null.")]
        public async Task FindBySsn_NullSsn_ExceptionalCase()
        {
            //arrange
            const string nullSsn = null;

            _access.WithFindBySsnResult(MemberMapTestUtils.GenerateValidMemberMap() as IMemberMap);
            _mapper.WithUnmap(MemberTestUtils.GenerateValidMember());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.FindBySsn(nullSsn);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Ssn cannot be null.")]
        public async Task FindBySsn_InvalidSsn_ExceptionalCase()
        {
            //arrange
            var invalidSsn = MemberTestUtils.GenerateInvalidSsn();

            _access.WithFindBySsnResult(MemberMapTestUtils.GenerateValidMemberMap() as IMemberMap);
            _mapper.WithUnmap(MemberTestUtils.GenerateValidMember());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.FindBySsn(invalidSsn);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Member map cannot be null.")]
        public async Task FindBySsn_NullMemberMap_ExceptionalCase()
        {
            //arrange
            var validSsn = MemberTestUtils.GenerateValidSsn();

            _access.WithFindBySsnResult(null);
            _mapper.WithUnmap(MemberTestUtils.GenerateValidMember());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.FindBySsn(validSsn);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Member cannot be null.")]
        public async Task FindBySsn_NullMember_ExceptionalCase()
        {
            //arrange
            var validSsn = MemberTestUtils.GenerateValidSsn();

            _access.WithFindBySsnResult(MemberMapTestUtils.GenerateValidMemberMap() as IMemberMap);
            _mapper.WithUnmap(null);

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.FindBySsn(validSsn);
        }

        [TestMethod]
        public async Task FindByUsername_ValidUsername_HappyPath()
        {
            //arrange
            const string validUsername = "validUsername";

            _access.WithFindByUsernameResult(MemberMapTestUtils.GenerateValidMemberMap() as IMemberMap);
            _mapper.WithUnmap(MemberTestUtils.GenerateValidMember());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.FindByUsername(validUsername);

            //assert
            _access.Verify(m => m.FindByUsername(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Username cannot be null.")]
        public async Task FindByUsername_NullUsername_ExceptionalCase()
        {
            //arrange
            const string nullUsername = null;

            _access.WithFindByUsernameResult(MemberMapTestUtils.GenerateValidMemberMap() as IMemberMap);
            _mapper.WithUnmap(MemberTestUtils.GenerateValidMember());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.FindByUsername(nullUsername);
        }

        [TestMethod]
        public async Task FindByUsername_NullMemberMap_ExceptionalCase()
        {
            //arrange
            const string validUsername = "validUsername";

            _access.WithFindByUsernameResult(null);
            _mapper.WithUnmap(MemberTestUtils.GenerateValidMember());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.FindByUsername(validUsername);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Member cannot be null.")]
        public async Task FindByUsername_NullMember_ExceptionalCase()
        {
            //arrange
            const string validUsername = "validUsername";

            _access.WithFindByUsernameResult(MemberMapTestUtils.GenerateValidMemberMap() as IMemberMap);
            _mapper.WithUnmap(null);

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.FindByUsername(validUsername);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Multiple roles cannot be selected. Use GetRoleName instead.")]
        public async Task GetRoles_ObsoleteMethodCall_ExceptionalCase()
        {
            //arrange
            var dummyMember = new Member();
            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.GetRoles(dummyMember);
        }

        [TestMethod]
        public async Task IsInRole_ValidMemberAndRole_HappyPath()
        {
            //arrange
            const string validRole = "validRole";
            var validMember = MemberTestUtils.GenerateValidMember();

            _mapper.WithMap(MemberMapTestUtils.GenerateValidMemberMap());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.IsInRole(validMember, validRole);

            //assert
            _access.Verify(m => m.IsMemberInRole(It.IsAny<MemberMap>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Member cannot be null.")]
        public async Task IsInRole_NullMember_ExceptionalCase()
        {
            //arrange
            const string validRole = "validRole";

            _mapper.WithMap(MemberMapTestUtils.GenerateValidMemberMap());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.IsInRole(null, validRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Member is invalid.")]
        public async Task IsInRole_InvalidMember_ExceptionalCase()
        {
            //arrange
            const string validRole = "validRole";
            var invalidMember = MemberTestUtils.GenerateInvalidMember();

            _mapper.WithMap(MemberMapTestUtils.GenerateValidMemberMap());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.IsInRole(invalidMember, validRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Rolename is null.")]
        public async Task IsInRole_NullRoleName_ExceptionalCase()
        {
            //arrange
            const string nullRole = null;
            var validMember = MemberTestUtils.GenerateValidMember();

            _mapper.WithMap(MemberMapTestUtils.GenerateValidMemberMap());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.IsInRole(validMember, nullRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Rolename is not valid.")]
        public async Task IsInRole_InvalidRoleName_ExceptionalCase()
        {
            //arrange
            const string invalidRole = "";
            var validMember = MemberTestUtils.GenerateValidMember();

            _mapper.WithMap(MemberMapTestUtils.GenerateValidMemberMap());

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.IsInRole(validMember, invalidRole);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "member map cannot be null.")]
        public async Task IsInRole_NullMemberMap_ExceptionalCase()
        {
            //arrange
            const string validRole = "validRole";
            var validMember = MemberTestUtils.GenerateValidMember();

            _mapper.WithMap(null);

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.IsInRole(validMember, validRole);
        }

        #region GetRoleName

        [TestMethod]
        public async Task GetRoleName_ValidMember_HappyPath()
        {
            //arrange
            var member = MemberTestUtils.GenerateValidMember();

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.GetRoleName(member);

            //assert
            _access.Verify(x => x.FindMemberRoleName(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "member cannot be invalid.")]
        public async Task GetRoleName_InvalidMember_Fails()
        {
            //arrange
            var member = MemberTestUtils.GenerateInvalidMember();

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.GetRoleName(member);

            //assert
            _access.Verify(x => x.FindMemberRoleName(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "member cannot be null.")]
        public async Task GetRoleName_NullMember_Fails()
        {
            //arrange
            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.GetRoleName(null);

            //assert
            _access.Verify(x => x.FindMemberRoleName(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Role cannot be null.")]
        public async Task GetRoleName_NullRole_Fails()
        {
            //arrange
            _sut = new MemberRepository(_access.Object, _mapper.Object);
            var member = MemberTestUtils.GenerateValidMember();
            member.RoleId = "";

            //act
            await _sut.GetRoleName(member);

            //assert
            _access.Verify(x => x.FindMemberRoleName(It.IsAny<string>()), Times.Once());
        }

        #endregion

        #region UpdateRole

        [TestMethod]
        public async Task UpdateRole_ValidRole_HappyPath()
        {
            //arrange
            var member = MemberTestUtils.GenerateValidMember();
            var role = RoleTestUtils.GenerateValidRole();

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.UpdateRole(member, role.Name);

            //assert
            _access.Verify(x => x.AddMemberToRole(It.IsAny<IMemberMap>(), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Role cannot be invalid.")]
        public async Task UpdateRole_InvalidRole_Fails()
        {
            //arrange
            var member = MemberTestUtils.GenerateValidMember();
            var role = RoleTestUtils.GenerateInvalidRole();

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.UpdateRole(member, role.Name);

            //assert
            _access.Verify(x => x.AddMemberToRole(It.IsAny<IMemberMap>(), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "member cannot be null.")]
        public async Task UpdateRole_NullRole_Fails()
        {
            //arrange
            var member = MemberTestUtils.GenerateValidMember();

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.UpdateRole(member, null);

            //assert
            _access.Verify(x => x.AddMemberToRole(It.IsAny<IMemberMap>(), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Member cannot be invalid.")]
        public async Task UpdateRole_InvalidMember_Fails()
        {
            //arrange
            var member = MemberTestUtils.GenerateInvalidMember();
            var role = RoleTestUtils.GenerateValidRole();

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.UpdateRole(member, role.Name);

            //assert
            _access.Verify(x => x.AddMemberToRole(It.IsAny<IMemberMap>(), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "member cannot be null.")]
        public async Task UpdateRole_NullMember_Fails()
        {
            //arrange
            var role = RoleTestUtils.GenerateValidRole();

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.UpdateRole(null, role.Name);

            //assert
            _access.Verify(x => x.AddMemberToRole(It.IsAny<IMemberMap>(), It.IsAny<string>()), Times.Once());
        }

        #endregion

        #region RemoveFromRole

        [TestMethod]
        public async Task RemoveFromRole_ValidRole_HappyPath()
        {
            //arrange
            var member = MemberTestUtils.GenerateValidMember();
            var role = RoleTestUtils.GenerateValidRole();

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.RemoveFromRole(member, role.Name);

            //assert
            _access.Verify(x => x.RemoveMemberRole(It.IsAny<IMemberMap>(), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Role cannot be invalid.")]
        public async Task RemoveFromRole_InvalidRole_Fails()
        {
            //arrange
            var member = MemberTestUtils.GenerateValidMember();
            var role = RoleTestUtils.GenerateInvalidRole();

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.RemoveFromRole(member, role.Name);

            //assert
            _access.Verify(x => x.RemoveMemberRole(It.IsAny<IMemberMap>(), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "member cannot be null.")]
        public async Task RemoveFromRole_NullRole_Fails()
        {
            //arrange
            var member = MemberTestUtils.GenerateValidMember();

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.RemoveFromRole(member, null);

            //assert
            _access.Verify(x => x.RemoveMemberRole(It.IsAny<IMemberMap>(), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Member cannot be invalid.")]
        public async Task RemoveFromRole_InvalidMember_Fails()
        {
            //arrange
            var member = MemberTestUtils.GenerateInvalidMember();
            var role = RoleTestUtils.GenerateValidRole();

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.RemoveFromRole(member, role.Name);

            //assert
            _access.Verify(x => x.RemoveMemberRole(It.IsAny<IMemberMap>(), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "member cannot be null.")]
        public async Task RemoveFromRole_NullMember_Fails()
        {
            //arrange
            var role = RoleTestUtils.GenerateValidRole();

            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.RemoveFromRole(null, role.Name);

            //assert
            _access.Verify(x => x.RemoveMemberRole(It.IsAny<IMemberMap>(), It.IsAny<string>()), Times.Once());
        }

        #endregion

        #region GetMemberInRoleCount

        [TestMethod]
        public async Task GetMemberInRoleCountTest_ValidRoleName_HappyPath()
        {
            //arrange
            _sut = new MemberRepository(_access.Object, _mapper.Object);
            var role = RoleTestUtils.GenerateValidRole();

            //act
            await _sut.GetMemberInRoleCount(role.Name);

            //assert
            _access.Verify(x => x.GetMemberInRoleCount(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Rolename cannot be invalid")]
        public async Task GetMemberInRoleCountTest_InvalidRoleName_HappyPath()
        {
            //arrange
            _sut = new MemberRepository(_access.Object, _mapper.Object);
            var role = RoleTestUtils.GenerateInvalidRole();

            //act
            await _sut.GetMemberInRoleCount(role.Name);

            //assert
            _access.Verify(x => x.GetMemberInRoleCount(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Rolename cannot be invalid")]
        public async Task GetMemberInRoleCountTest_NullRoleName_HappyPath()
        {
            //arrange
            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.GetMemberInRoleCount(null);

            //assert
            _access.Verify(x => x.GetMemberInRoleCount(It.IsAny<string>()), Times.Never);
        }

        #endregion

        #region FindModelsWithRoleName

        [TestMethod]
        public async Task FindModelsWithRoleName_ValidRoleName_HappyPath()
        {
            //arrange
            _sut = new MemberRepository(_access.Object, _mapper.Object);
            var role = RoleTestUtils.GenerateValidRole();

            //act
            await _sut.FindModelsWithRoleName(role.Name);

            //assert
            _access.Verify(x => x.FindMemberModelsByRoleName(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Rolename cannot be invalid")]
        public async Task FindModelsWithRoleName_InvalidRoleName_HappyPath()
        {
            //arrange
            _sut = new MemberRepository(_access.Object, _mapper.Object);
            var role = RoleTestUtils.GenerateInvalidRole();

            //act
            await _sut.FindModelsWithRoleName(role.Name);

            //assert
            _access.Verify(x => x.FindMemberModelsByRoleName(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Rolename cannot be invalid")]
        public async Task FindModelsWithRoleName_NullRoleName_HappyPath()
        {
            //arrange
            _sut = new MemberRepository(_access.Object, _mapper.Object);

            //act
            await _sut.FindModelsWithRoleName(null);

            //assert
            _access.Verify(x => x.FindMemberModelsByRoleName(It.IsAny<string>()), Times.Never);
        }

        #endregion
    }
}
