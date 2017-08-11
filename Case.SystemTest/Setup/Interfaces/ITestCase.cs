using System;

namespace Case.SystemTest.Setup.Interfaces
{
    internal interface ITestCase : IDisposable
    {
        void AddAction(ActionTestStep step);
        void VerifyThat(VerificationTestStep step);
        void IsValid();
        void Execute();
    }
}
