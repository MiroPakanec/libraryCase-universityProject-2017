using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Case.SystemTest.Setup;
using OpenQA.Selenium;

namespace Case.SystemTest.TestStepLibrary.Selenium
{
    internal class Driver
    {
        internal ActionTestStep Sleep(TimeSpan span)
        {
            return new ActionTestStep(driver =>
            {
                Thread.Sleep(span);
            });
        }
    }
}
