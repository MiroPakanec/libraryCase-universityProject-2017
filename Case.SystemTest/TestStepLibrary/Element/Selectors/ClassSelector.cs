using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.SystemTest.Setup;
using Case.SystemTest.TestStepLibrary.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Case.SystemTest.TestStepLibrary.Element.Selectors
{
   internal class ClassSelector : Selector
    {
        public ClassSelector(string selectorValue) : base(selectorValue)
        {
        }

       protected override IWebElement FindElement(IWebDriver driver)
       {
           return driver.FindElement(By.ClassName(SelectorValue));
       }
    }
}
