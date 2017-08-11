using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.SystemTest.TestStepLibrary.Database.Table;

namespace Case.SystemTest.TestStepLibrary.Database
{
    internal class Database
    {
        internal Member Member => new Member();

        internal Book Book => new Book();

        internal Order Order => new Order();
    }
}
