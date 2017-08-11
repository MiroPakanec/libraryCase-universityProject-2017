using System;
using Case.SystemTest.Setup.Interfaces;
using OpenQA.Selenium;

namespace Case.SystemTest.Setup
{
    public class ActionTestStep : ITestStep
    {
        private readonly Action<IWebDriver> _action;

        public ActionTestStep(Action<IWebDriver> action)
        {
            _action = action;
        }

        public void Execute(IWebDriver driver)
        {
            _action(driver);
        }
    }
}
