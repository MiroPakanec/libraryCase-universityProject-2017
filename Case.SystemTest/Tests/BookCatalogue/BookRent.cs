using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using Case.SystemTest.Setup;
using Case.SystemTest.Setup.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Case.SystemTest.Tests.BookCatalogue
{
    [TestClass]
    public class BookRent : SeleniumTest
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

        [TestMethod]
        public void RegisterAndRentBook_ValidInput_HappyPath()
        {
            const string ssn = "111-00-0000";
            const string password = "MyPassword1.";
            const string confirmPassword = "MyPassword1.";
            const string isbn = "978-7-11-125544-5";

            //Action: Navigate browser to registration page
            _testCase.AddAction(TestStep.Browser.GoToLocation.Register);

            _testCase.AddAction(TestStep.Database.Book.CreateBookWithBookCopy(isbn));

            //Verififcation: Verify that browser is on the registration page.
            _testCase.VerifyThat(TestStep.Browser.HasLocation.Register);

            //Action: Find element with Name Ssn and set its value.
            _testCase.AddAction(TestStep.OnPage.Register.Element.WithName("Ssn").EnterValue(ssn));

            //Verification: Verify that element with Name Ssn has expected value.
            _testCase.VerifyThat(TestStep.OnPage.Register.Element.WithName("Ssn").HasValue(ssn));

            //Action: Find element with Name Password and set its value.
            _testCase.AddAction(TestStep.OnPage.Register.Element.WithName("Password").EnterValue(password));

            //Verification: Verify that element with Name Pasword has expected value.
            _testCase.VerifyThat(TestStep.OnPage.Register.Element.WithName("Password").HasValue(password));

            //Action: Find element with Name ConfirmPassword and set its value.
            _testCase.AddAction(
                TestStep.OnPage.Register.Element.WithName("ConfirmPassword").EnterValue(confirmPassword));

            //Verification: Verify that element with Name Pasword has expected value.
            _testCase.VerifyThat(TestStep.OnPage.Register.Element.WithName("ConfirmPassword")
                .HasValue(confirmPassword));

            //Action: Click on the submit button
            _testCase.AddAction(TestStep.OnPage.Register.Element.WithId("submit-register").Click());

            //Action: Wait 5 seconds
            _testCase.AddAction(TestStep.Driver.Sleep(TimeSpan.FromSeconds(5)));

            _testCase.AddAction(TestStep.Browser.GoToLocation.BookIndex);

            _testCase.VerifyThat(TestStep.Browser.HasLocation.BookIndex);

            _testCase.AddAction(TestStep.Driver.Sleep(TimeSpan.FromSeconds(5)));

            _testCase.AddAction(TestStep.OnPage.BookIndex.Element.WithClass("add_to_cart").Click());

            _testCase.AddAction(TestStep.Driver.Sleep(TimeSpan.FromSeconds(1)));

            _testCase.AddAction(TestStep.Browser.GoToLocation.BookCart);

            _testCase.AddAction(TestStep.Driver.Sleep(TimeSpan.FromSeconds(5)));

            _testCase.AddAction(TestStep.OnPage.BookCart.Element.WithId("rent_btn").Click());

            _testCase.AddAction(TestStep.Driver.Sleep(TimeSpan.FromSeconds(5)));

            _testCase.VerifyThat(TestStep.Database.Book.CopyIsNotAvailable(isbn));

            _testCase.AddAction(TestStep.Database.Order.CleanOrderForSsn(ssn));

            _testCase.VerifyThat(TestStep.Database.Order.MemberDoesntHaveOrders(ssn));

            _testCase.AddAction(TestStep.Database.Member.Remove(ssn));

            //Verification: Verify that member does no longer exist in the database.
            _testCase.VerifyThat(TestStep.Database.Member.DoesNotExist(ssn));

            _testCase.AddAction(TestStep.Database.Book.RemoveBookAndCopy(isbn));

            _testCase.VerifyThat(TestStep.Database.Book.BookAndCopyDoesNotExist(isbn));

            _testCase.Execute();
        }
    }
}
