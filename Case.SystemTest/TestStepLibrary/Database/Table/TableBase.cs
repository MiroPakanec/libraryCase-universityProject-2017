using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.SystemTest.TestStepLibrary.Database.Table
{
    internal class TableBase
    {
        protected string ConnectionString
        {
            get
            {
                var settings = ConfigurationManager.ConnectionStrings["Local"];
                return settings.ConnectionString;
            }
        }
    }
}
