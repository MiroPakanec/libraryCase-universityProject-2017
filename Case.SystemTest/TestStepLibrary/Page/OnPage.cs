using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.SystemTest.TestStepLibrary.Page.Pages;
using Case.SystemTest.TestStepLibrary.Page.Pages.Interfaces;

namespace Case.SystemTest.TestStepLibrary.Page
{
    internal class OnPage
    {
        internal BasePage Register => new PageRegister();
        internal BasePage Home => new PageHome();
        internal BasePage BookIndex => new PageBookIndex();
        internal BasePage BookCart => new PageBookCart();
    }
}
