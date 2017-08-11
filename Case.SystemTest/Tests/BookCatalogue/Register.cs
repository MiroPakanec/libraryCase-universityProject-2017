using System;
using Case.SystemTest.Setup;
using Case.SystemTest.Setup.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace Case.SystemTest.Tests.BookCatalogue
{
    [TestClass]
    public class Register : SeleniumTest
    {
        private ITestCase _testCase;

        [TestInitialize]
        public void SetUp()
        {
           _testCase = new TestCase();
        }

        [TestCleanup]
        public void TearDown()
        {
            _testCase.IsValid();
            _testCase.Dispose();
        }

        /// <summary>
        /// TC-S-1
        /// Test that verifies member registration with valid ssn and password.
        /// </summary>
        [TestMethod()]
        public void Register_ValidInput_HappyPath()
        {
            const string ssn = "111-00-0003";
            const string password = "MyPassword1.";
            const string confirmPassword = "MyPassword1.";

            //Action: Navigate browser to registration page
            _testCase.AddAction(TestStep.Browser.GoToLocation.Register);

            //Verififcation: Verify that browser is on the registration page.
            _testCase.VerifyThat(TestStep.Browser.HasLocation.Register);

            _testCase.AddAction(TestStep.Driver.Sleep(TimeSpan.FromSeconds(10)));

            //Action: Find element with Name Ssn and set its value.
            _testCase.AddAction(TestStep.OnPage.Register.Element.WithName("Ssn").EnterValue(ssn));

            //Verification: Verify that element with Name Ssn has expected value.
            _testCase.VerifyThat(TestStep.OnPage.Register.Element.WithName("Ssn").HasValue(ssn));

            //Action: Find element with Name Password and set its value.
            _testCase.AddAction(TestStep.OnPage.Register.Element.WithName("Password").EnterValue(password));

            //Verification: Verify that element with Name Pasword has expected value.
            _testCase.VerifyThat(TestStep.OnPage.Register.Element.WithName("Password").HasValue(password));

            //Action: Find element with Name ConfirmPassword and set its value.
            _testCase.AddAction(TestStep.OnPage.Register.Element.WithName("ConfirmPassword").EnterValue(confirmPassword));

            //Verification: Verify that element with Name Pasword has expected value.
            _testCase.VerifyThat(TestStep.OnPage.Register.Element.WithName("ConfirmPassword").HasValue(confirmPassword));

            //Action: Click on the submit button
            _testCase.AddAction(TestStep.OnPage.Register.Element.WithId("submit-register").Click());

            //Action: Wait 5 seconds
            _testCase.AddAction(TestStep.Driver.Sleep(TimeSpan.FromSeconds(5)));

            //Verification: Verify that browser is on the home page.
            _testCase.VerifyThat(TestStep.Browser.HasLocation.Home);
            
            //Verification: Verify that member was registered.
            _testCase.VerifyThat(TestStep.Database.Member.ExistsWithValidRole(ssn));

            //Action: Remove member from database
            _testCase.AddAction(TestStep.Database.Member.Remove(ssn));

            //Verification: Verify that member does no longer exist in the database.
            _testCase.VerifyThat(TestStep.Database.Member.DoesNotExist(ssn));

            _testCase.Execute();
        }
    }   
}
    