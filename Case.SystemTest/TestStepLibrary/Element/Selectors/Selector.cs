using Case.SystemTest.Setup;
using Case.SystemTest.TestStepLibrary.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Case.SystemTest.TestStepLibrary.Element.Selectors
{
    internal abstract class Selector
    {
        protected string SelectorValue { get; set; }

        internal Selector(string selectorValue)
        {
            SelectorValue = selectorValue;
        }

        protected abstract IWebElement FindElement(IWebDriver driver);

        internal virtual ActionTestStep EnterValue(string value)
        {
            return new ActionTestStep(driver =>
            {
                FindElement(driver).SendKeys(value);
            });
        }

        internal virtual ActionTestStep Click()
        {
            return new ActionTestStep(driver =>
            {
                FindElement(driver).Click();
            });
        }

        internal virtual VerificationTestStep HasValue(string value)
        {
            return new VerificationTestStep(driver =>
            {
                var actualValue = FindElement(driver).GetAttribute(Attributes.Value);
                Assert.IsTrue(actualValue.Equals(value), "Element value is not equal to the expected value.");
            });
        }
    }
}
