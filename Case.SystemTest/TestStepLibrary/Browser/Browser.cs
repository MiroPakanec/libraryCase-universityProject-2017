using Case.SystemTest.TestStepLibrary.Browser.Navigation;

namespace Case.SystemTest.TestStepLibrary.Browser
{
    internal class Browser
    {
        internal GoToLocation GoToLocation => new GoToLocation();
        internal HasLocation HasLocation => new HasLocation();
    }
}
