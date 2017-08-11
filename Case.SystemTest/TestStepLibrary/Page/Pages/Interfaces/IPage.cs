using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.SystemTest.TestStepLibrary.Element;

namespace Case.SystemTest.TestStepLibrary.Page.Pages.Interfaces
{
    internal interface IPage
    {
        BaseElement Element { get; }
    }
}
