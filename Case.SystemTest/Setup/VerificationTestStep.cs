using System;
using Case.SystemTest.Setup.Interfaces;
using OpenQA.Selenium;

namespace Case.SystemTest.Setup
{
    internal class VerificationTestStep : ITestStep
    {
        private readonly Action<IWebDriver> _action;

        public VerificationTestStep(Action<IWebDriver> action)
        {
            _action = action;
        }

        public void Execute(IWebDriver driver)
        {
            _action(driver);
        }
    }
}
