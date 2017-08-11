using System;
using System.Collections.Generic;
using Case.SystemTest.Setup.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Case.SystemTest.Setup
{
    internal class TestCase : ITestCase
    {
        private readonly IList<ITestStep> _testSteps;
        private readonly IWebDriver _driver;

        private bool _wasExecuted;
        private bool _wasVerified;

        public TestCase()
        {
            _testSteps = new List<ITestStep>();
            _driver = new FirefoxDriver();

            _wasExecuted = false;
            _wasVerified = false;
        }

        public void AddAction(ActionTestStep step)
        {
            _testSteps.Add(step);
        }

        public void VerifyThat(VerificationTestStep step)
        {
            _testSteps.Add(step);
            _wasVerified = true;
        }

        public void Execute()
        {
            try
            {
                foreach (var step in _testSteps)
                {
                    step.Execute(_driver);
                }

                _wasExecuted = true;
            }
            catch
            {   
                Dispose();           
                throw;
            }
        }

        public void IsValid()
        {
            Assert.IsTrue(_wasExecuted, "Execution method was never called or an error interupted command execution.");
            Assert.IsTrue(_testSteps.Count > 0, "At least one test step has to be supplied.");
            Assert.IsTrue(_wasVerified, "At least one verification test step has to be specified.");
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
