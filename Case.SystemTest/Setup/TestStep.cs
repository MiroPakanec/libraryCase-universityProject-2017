using Case.SystemTest.TestStepLibrary.Browser;
using Case.SystemTest.TestStepLibrary.Database;
using Case.SystemTest.TestStepLibrary.Page;
using Case.SystemTest.TestStepLibrary.Selenium;

namespace Case.SystemTest.Setup
{
    internal class TestStep
    {
        internal static Browser Browser => new Browser();
        internal static OnPage OnPage => new OnPage();
        internal static Driver Driver => new Driver();
        internal static Database Database => new Database();
    }
}
