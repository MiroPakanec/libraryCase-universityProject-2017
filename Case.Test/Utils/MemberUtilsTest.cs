using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Case.Test.TestUtils.MemberTestUtils;

namespace Case.Test.Utils
{
    [TestClass]
    public class MemberUtilsTest
    {
        #region ValidateSsn
        [TestMethod]
        public void ValidateValidSsnShouldReturnTrue()
        {
            var ssn = GenerateValidSsn();
            var result = MemberUtils.ValidateSsn(ssn);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateInvalidSsnShouldReturnFalse()
        {
            var ssn = GenerateInvalidSsn();
            var result = MemberUtils.ValidateSsn(ssn);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateEmptySsnShouldReturnFalse()
        {
            var ssn = string.Empty;
            var result = MemberUtils.ValidateSsn(ssn);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateNullSsnShouldReturnFalse()
        {
            string ssn = null;
            var result = MemberUtils.ValidateSsn(ssn);
            Assert.IsFalse(result);
        }
        #endregion

        #region ValidatePassword

        [TestMethod]
        public void ValidateValidPasswordShouldReturnTrue()
        {
            var password = "password";
            var result = MemberUtils.ValidatePassword(password);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateEmptyPasswordShouldReturnTrue()
        {
            var password = string.Empty;
            var result = MemberUtils.ValidatePassword(password);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateNullPasswordShouldReturnTrue()
        {
            string password = null;
            var result = MemberUtils.ValidatePassword(password);
            Assert.IsFalse(result);
        }

        #endregion

        #region ValidateMember

        [TestMethod]
        public void ValidateValidMemberShouldReturnTrue()
        {
            var member = GenerateValidMember();
            MemberUtils.ValidateMember(member);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Member cannot be invalid")]
        public void ValidateInvalidMemberShouldReturnFalse()
        {
            var member = GenerateInvalidMember();
            MemberUtils.ValidateMember(member);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Member cannot be null")]
        public void ValidateNullMemberShouldReturnFalse()
        {
            Member member = null;
            MemberUtils.ValidateMember(member);
        }

        #endregion
    }
}
