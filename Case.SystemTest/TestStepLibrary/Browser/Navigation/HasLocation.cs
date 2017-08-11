using Case.SystemTest.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Case.SystemTest.TestStepLibrary.Browser.Navigation
{
    internal class HasLocation
    {
        public VerificationTestStep Register
        {
            get
            {
                return new VerificationTestStep(driver =>
                {
                    const string url = "http://localhost:60764/Account/Register";

                    Assert.IsNotNull(driver.Url);
                    Assert.AreEqual(url, driver.Url);
                });
            }
        }

        public VerificationTestStep Home
        {
            get
            {
                return new VerificationTestStep(driver =>
                {
                    const string url = "http://localhost:60764/";

                    Assert.IsNotNull(driver.Url);
                    Assert.AreEqual(url, driver.Url);
                });
            }
        }
        public VerificationTestStep BookIndex
        {
            get
            {
                return new VerificationTestStep(driver =>
                {
                    const string url = "http://localhost:60764/book";

                    Assert.IsNotNull(driver.Url);
                    Assert.AreEqual(url, driver.Url);
                });
            }
        }

        public VerificationTestStep BookCart
        {
            get
            {
                return new VerificationTestStep(driver =>
                {
                    const string url = "http://localhost:60764/book/cart";

                    Assert.IsNotNull(driver.Url);
                    Assert.AreEqual(url, driver.Url);
                });
            }
        }
    }
}
