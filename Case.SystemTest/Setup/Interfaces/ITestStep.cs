using OpenQA.Selenium;

namespace Case.SystemTest.Setup.Interfaces
{
    internal interface ITestStep
    {
        void Execute(IWebDriver driver);
    }
}
