using Case.SystemTest.Setup;

namespace Case.SystemTest.TestStepLibrary.Browser.Navigation
{
    public class GoToLocation
    {
        public ActionTestStep Register
        {
            get
            {
                return new ActionTestStep(driver =>
                {
                    const string url = "http://localhost:60764/Account/Register";
                    driver.Navigate().GoToUrl(url);
                });
            }
        }

        public ActionTestStep BookIndex
        {
            get
            {
                return new ActionTestStep(driver =>
                {
                    const string url = "http://localhost:60764/Book";
                    driver.Navigate().GoToUrl(url);
                });
            }
        }

        public ActionTestStep BookCart
        {
            get
            {
                return new ActionTestStep(driver =>
                {
                    const string url = "http://localhost:60764/Book/cart";
                    driver.Navigate().GoToUrl(url);
                });
            }
        }
    }
}   
