using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.SystemTest.Setup;
using Dapper;
using DataAccess.EntityMaps.Order;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Case.SystemTest.TestStepLibrary.Database.Table
{
    internal class Order : TableBase
    {
        internal ActionTestStep CleanOrderForSsn(string ssn)
        {
            return new ActionTestStep(driver =>
            {
                const string query = "DELETE FROM [dbo.Order_Line] WHERE [OrderId] = (Select [Id] from dbo.Order where [MemberSsn] = @Ssn); DELETE FROM [dbo.Order] WHERE where [MemberSsn] = @Ssn;";

                using (var con = new SqlConnection(ConnectionString))
                {
                    con.Execute(query, new {Ssn = ssn});
                }
            });
        }

        internal VerificationTestStep MemberDoesntHaveOrders(string ssn)
        {
            return new VerificationTestStep(driver =>
            {
                const string query = "SELECT * FROM [dbo.Order] WHERE [MemberSsn] = @Ssn";

                using (var con = new SqlConnection(ConnectionString))
                {
                    var result = con.Query<OrderMap>(query, new {Ssn = ssn});
                    Assert.IsTrue(result == null || !result.Any());
                }
            });
        }
    }
}
