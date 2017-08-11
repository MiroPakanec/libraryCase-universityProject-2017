using System;
using System.Threading.Tasks;
using Case.IntegrationTest.TestUtils;
using DataAccess.Queries.Member;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.EntityMaps.Member;

namespace Case.IntegrationTest.DataAccess
{
    [TestClass]
    public class MemberAccessTests
    {
        private MemberAccess _sut;
        private MemberMap _mapper;

        [TestInitialize]
        public void SetUp()
        {
            _sut = new MemberAccess();
            _mapper = new MemberMap();
        }

        [TestCleanup]
        public void TearDown()
        {
            _sut = null;
        }

        //TC-I-44
        [TestMethod]
        public async Task InsertOneAsync_TCI44_HappyPath()
        {
            //arrange
            var memberMap = MemberMapTestUtils.GenerateValidMemberMap();

            //act
            var result = await _sut.InsertOneAsync(memberMap);

            //assert
            Assert.IsTrue(result);
        }

        //TC-I-45
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task InsertOneAsync_TCI45_InvalidMap()
        {
            //arrange
            var memberMap = MemberMapTestUtils.GenerateValidMemberMap();

            //act
            var result = await _sut.InsertOneAsync(memberMap);

            //assert
            Assert.IsTrue(result);
        }

        //TC-I-46
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task InsertOneAsync_TCI46_NullMap()
        {
            //act
            var result = await _sut.InsertOneAsync(null);

            //assert
            Assert.IsTrue(result);
        }

        //TC-I-47
        [TestMethod]
        public async Task UpdateOneAsync_TCI47_HappyPath()
        {
            //arrange
            var memberMap = MemberMapTestUtils.GenerateValidMemberMap();

            //act
            var result = await _sut.UpdateOneAsync(memberMap);

            //assert
            Assert.IsTrue(result);
        }

        //TC-I-48
        [TestMethod]
        public async Task UpdateOneAsync_TCI48_InvalidMap()
        {
            //arrange
            var memberMap = MemberMapTestUtils.GenerateValidMemberMap();

            //act
            var result = await _sut.UpdateOneAsync(memberMap);

            //assert
            Assert.IsTrue(result);
        }

        //TC-I-49
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task UpdateOneAsync_TCI49_NullMap()
        {
            //act
            var result = await _sut.UpdateOneAsync(null);

            //assert
            Assert.IsTrue(result);
        }

        //TC-I-50
        [TestMethod]
        public async Task UpdateOneAsync_TCI50_HappyPath()
        {
            //arrange
            var memberMap = MemberMapTestUtils.GenerateValidMemberMap();

            //act
            var result = await _sut.UpdateOneAsync(memberMap);

            //assert
            Assert.IsTrue(result);
        }

        //TC-I-51
        [TestMethod]
        public async Task UpdateOneAsync_TCI51_InvalidMap()
        {
            //arrange
            var memberMap = MemberMapTestUtils.GenerateValidMemberMap();

            //act
            var result = await _sut.UpdateOneAsync(memberMap);

            //assert
            Assert.IsTrue(result);
        }

        //TC-I-52
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task UpdateOneAsync_TCI52_NullMap()
        {
            //act
            var result = await _sut.UpdateOneAsync(null);

            //assert
            Assert.IsTrue(result);
        }

        //TC-I-53
        [TestMethod]
        public async Task InsertOrUpdateAsync_TCI53_HappyPath()
        {
            //arrange
            var memberMap = MemberMapTestUtils.GenerateValidMemberMap();

            //act
            var result = await _sut.InsertOrUpdateAsync(memberMap);

            //assert
            Assert.IsTrue(result);
        }

        //TC-I-54
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task InsertOrUpdateAsync_TCI54_InvalidMap()
        {
            //arrange
            var memberMap = MemberMapTestUtils.GenerateValidMemberMap();

            //act
            var result = await _sut.InsertOrUpdateAsync(memberMap);

            //assert
            Assert.IsTrue(result);
        }

        //TC-I-55
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task InsertOrUpdateAsync_TCI55_NullMap()
        {
            //act
            var result = await _sut.InsertOrUpdateAsync(null);

            //assert
            Assert.IsTrue(result);
        }

        //TC-I-56
        [TestMethod]
        public async Task DeleteOneAsync_TCI56_HappyPath()
        {
            //arrange
            var memberMap = MemberMapTestUtils.GenerateValidMemberMap();

            //act
            var result = await _sut.DeleteOneAsync(memberMap);

            //assert
            Assert.IsFalse(result);;
        }

        //TC-I-57
        [TestMethod]
        public async Task DeleteOneAsync_TCI57_InvalidMap()
        {
            //arrange
            var memberMap = MemberMapTestUtils.GenerateValidMemberMap();

            //act
            var result = await _sut.DeleteOneAsync(memberMap);

            //assert
            Assert.IsFalse(result);
        }

        //TC-I-58
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task DeleteOneAsync_TCI58_NullMap()
        {
            //act
            var result = await _sut.DeleteOneAsync(null);

            //assert
            Assert.IsTrue(result);
        }

        //TC-I-59
        [TestMethod]
        public async Task SelectAllAsync_TCI59_HappyPath()
        {
            //act
            var result = await _sut.SelectAllAsync();

            //assert
            Assert.IsNotNull(result);
        }

        //TC-I-60
        [TestMethod]
        public async Task FindBySsnAndPasswordAsync_TCI60_HappyPath()
        {
            //arrange
            string ssn = MemberTestUtils.GenerateValidSsn();
            string password = MemberTestUtils.GenerateValidMember().Password;

            //act
            var result = await _sut.FindBySsnAndPasswordAsync(ssn, password);

            //assert
            Assert.IsNotNull(result);
        }

        //TC-I-61
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task FindBySsnAndPasswordAsync_TCI61_InvalidSsn()
        {
            //arrange
            string ssn = MemberTestUtils.GenerateInvalidSsn();
            string password = MemberTestUtils.GenerateValidMember().Password;

            //act
            var result = await _sut.FindBySsnAndPasswordAsync(ssn, password);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-62
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task FindBySsnAndPasswordAsync_TCI62_InvalidPassword()
        {
            //arrange
            string ssn = MemberTestUtils.GenerateValidSsn();
            string password = "";

            //act
            var result = await _sut.FindBySsnAndPasswordAsync(ssn, password);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-63
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task FindBySsnAndPasswordAsync_TCI63_NullSsn()
        {
            //arrange
            string password = MemberTestUtils.GenerateValidMember().Password;

            //act
            var result = await _sut.FindBySsnAndPasswordAsync(null, password);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-64
        [TestMethod]
        public async Task FindBySsn_TCI64_HappyPath()
        {
            //arrange
            string ssn = MemberTestUtils.GenerateValidSsn();

            //act
            var result = await _sut.FindBySsn(ssn);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-65
        [TestMethod]
        public async Task FindBySsn_TCI65_InvalidSsn()
        {
            //arrange
            string ssn = MemberTestUtils.GenerateValidSsn();

            //act
            var result = await _sut.FindBySsn(ssn);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-66
        [TestMethod]
        public async Task FindBySsn_TCI66_NullSsn()
        {
            //act
            var result = await _sut.FindBySsn(null);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-67
        [TestMethod]
        public async Task FindByUsername_TCI67_HappyPath()
        {
            //arrange
            string username = MemberTestUtils.GenerateValidMember().UserName;

            //act
            var result = await _sut.FindByUsername(username);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-68
        [TestMethod]
        public async Task FindByUsername_TCI68_InvalidUsername()
        {
            //arrange
            string username = "";

            //act
            var result = await _sut.FindByUsername(username);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-69
        [TestMethod]
        public async Task FindByUsername_TCI68_NullUsername()
        {
            //act
            var result = await _sut.FindByUsername(null);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-70
        [TestMethod]
        public async Task FindMemberRoleName_TCI70_ValidRoleName()
        {
            //arrange
            string role = "Member";

            //act
            var result = await _sut.FindMemberModelsByRoleName(role);

            //assert
            Assert.IsNotNull(result);
        }

        //TC-I-71
        [TestMethod]
        public async Task FindMemberRoleName_TCI71_InvalidRoleName()
        {
            //arrange
            string role = "";

            //act
            await _sut.FindMemberModelsByRoleName(role);
        }

        //TC-I-72
        [TestMethod]
        public async Task FindMemberRoleName_TCI72_NullRoleName()
        {
            //act
            await _sut.FindMemberModelsByRoleName(null);
        }

        //TC-I-73
        [TestMethod]
        public async Task IsMemberInRole_TCI73_HappyPath()
        {
            //arrange
            var memberMap = MemberMapTestUtils.GenerateValidMemberMap();
            
            string role = "Member";

            //act
            var result = await _sut.IsMemberInRole(memberMap as MemberMap, role);

            //assert
            Assert.IsNotNull(result);
        }

        //TC-I-74
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task IsMemberInRole_TCI74_InvalidMemberMap()
        {
            //arrange
            var memberMap = MemberMapTestUtils.GenerateValidMemberMap();

            string role = "Member";

            //act
            var result = await _sut.IsMemberInRole(memberMap as MemberMap, role);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-75
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task IsMemberInRole_TCI75_NullMemberMap()
        {
            //arrange
            string role = "Member";

            //act
            var result = await _sut.IsMemberInRole(null, role);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-76
        [TestMethod]
        public async Task AddMemberToRole_TCI76_HappyPath()
        {
            //arrange
            var memberMap = MemberMapTestUtils.GenerateValidMemberMap();
            string role = "Member";

            //act
            var result = await _sut.AddMemberToRole(memberMap as MemberMap, role);
            
            //assert
            Assert.IsNotNull(result);
        }

        //TC-I-77
        [TestMethod]
        public async Task AddMemberToRole_TCI77_InvalidMemberMap()
        {
            //arrange
            var memberMap = MemberMapTestUtils.GenerateValidMemberMap();
            string role = "Member";

            //act
            await _sut.AddMemberToRole(memberMap as MemberMap, role);
        }

        //TC-I-78
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task AddMemberToRole_TCI78_NullMemberMap()
        {
            //arrange
            string role = "Member";

            //act
            var result = await _sut.AddMemberToRole(null, role);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-79
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task FindMemberRoleName_TCI79_HappyPath()
        {
            //arrange
            string role = "Member";

            //act
            var result = await _sut.FindMemberRoleName(role);

            //assert
            Assert.IsNotNull(result);
        }

        //TC-I-80
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task FindMemberRoleName_TCI80_InvalidRoleName()
        {
            //arrange
            string role = "Member";

            //act
            var result = await _sut.FindMemberRoleName(role);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-81
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task FindMemberRoleName_TCI81_NullRoleName()
        {
            //arrange 
            string role = "Member";

            //act
            var result = await _sut.FindMemberRoleName(role);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-82
        [TestMethod]
        public async Task RemoveMemberFromRole_TCI82_HappyPath()
        {
            //arrange
            var memberMap = MemberMapTestUtils.GenerateValidMemberMap();
            string role = "Member";

            //act
            var result = await _sut.RemoveMemberRole(memberMap as MemberMap, role);

            //assert
            Assert.IsNotNull(result);
        }

        //TC-I-83
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task RemoveMemberFromRole_TCI83_InvalidRole()
        {
            //arrange
            var memberMap = MemberMapTestUtils.GenerateValidMemberMap();
            string role = "...";

            //act
            var result = await _sut.RemoveMemberRole(memberMap as MemberMap, role);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-84
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task RemoveMemberFromRole_TCI84_NullRole()
        {
            //arrange
            var memberMap = MemberMapTestUtils.GenerateValidMemberMap();
            string role = null;

            //act
            var result = await _sut.RemoveMemberRole(memberMap as MemberMap, role);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-85
        [TestMethod]
        public async Task FindMemberRoleByRoleId_TCI85_HappyPath()
        {
            //arrange
            int roleId = Int32.Parse(RoleTestUtils.GenerateValidRole().Id);

            //act
            var result = await _sut.FindMemberRoleByRoleId(roleId);

            //assert
            Assert.IsNotNull(result);
        }

        //TC-I-86
        [TestMethod]
        public async Task FindMemberRoleByRoleId_TCI86_InvalidRoleId()
        {
            //arrange
            int roleId = Int32.Parse(RoleTestUtils.GenerateInvalidRole().Id);

            //act
            var result = await _sut.FindMemberRoleByRoleId(roleId);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-87
        [TestMethod]
        public async Task FindMemberRoleByName_TCI87_HappyPath()
        {
            //arrange
            var roleName = RoleTestUtils.GenerateValidRole().Name;

            //act
            var result = await _sut.FindMemberRoleByName(roleName);

            //assert
            Assert.IsNotNull(result);
        }

        //TC-I-88
        [TestMethod]
        public async Task FindMemberRoleByName_TCI88_InvalidRoleName()
        {
            //arrange
            var roleName = RoleTestUtils.GenerateInvalidRole().Name;

            //act
            var result = await _sut.FindMemberRoleByName(roleName);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-89
        [TestMethod]
        public async Task FindMemberRoleByName_TCI89_NullRoleName()
        {
            //act
            var result = await _sut.FindMemberRoleByName(null);

            //assert
            Assert.IsNull(result);
        }

        //TC-I-90
        [TestMethod]
        public async Task GetMemberInRoleCount_TCI90_HappyPath()
        {
            //arrange
            var roleName = RoleTestUtils.GenerateValidRole().Name;

            //act
            var result = await _sut.GetMemberInRoleCount(roleName);

            //assert
            Assert.IsNotNull(result);
        }

        //TC-I-90
        [TestMethod]
        public async Task GetMemberInRoleCount_TCI91_InvalidRoleName()
        {
            //arrange
            var roleName = RoleTestUtils.GenerateInvalidRole().Name;

            //act
            var result = await _sut.GetMemberInRoleCount(roleName);

            //assert
            Assert.IsNotNull(result);
        }

        //TC-I-90
        [TestMethod]
        public async Task GetMemberInRoleCount_TCI92_NullRoleName()
        {
            //act
            var result = await _sut.GetMemberInRoleCount(null);

            //assert
            Assert.IsNotNull(result);
        }

    }
}