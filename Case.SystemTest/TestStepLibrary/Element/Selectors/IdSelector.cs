using OpenQA.Selenium;

namespace Case.SystemTest.TestStepLibrary.Element.Selectors
{
    internal class IdSelector : Selector
    {
        public IdSelector(string selectorValue) : base(selectorValue)
        {
        }

        protected override IWebElement FindElement(IWebDriver driver)
        {
            return driver.FindElement(By.Id(SelectorValue));
        }
    }
}
