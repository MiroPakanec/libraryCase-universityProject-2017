using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core.Utils;
using Case.SystemTest.Setup;
using Dapper;
using DataAccess.EntityMaps.Member;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Case.SystemTest.TestStepLibrary.Database.Table
{
    internal class Member : TableBase
    {
        internal VerificationTestStep ExistsWithValidRole(string ssn)
        {
            return new VerificationTestStep(driver =>
            {
                const string query = "Select * from dbo.Member where [Ssn] = @Ssn";

                using (var con = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        var result = con.Query<MemberMap>(query, new {Ssn = ssn}).First();
                        Assert.IsNotNull(result, $"Member with Ssn {ssn} was not found in the database.");
                        Assert.AreEqual("2", result.RoleId.ToString(),
                            $"Member with Ssn {ssn} does not have an expected role.");
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            });
        }

        internal VerificationTestStep DoesNotExist(string ssn)
        {
            return new VerificationTestStep(driver =>
            {
                const string query = "Select * from dbo.Member where [Ssn] = @Ssn";

                using (var con = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        var result = con.Query<MemberMap>(query, new {Ssn = ssn});
                        Assert.IsTrue(result.Any() == false);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            });
        }

        internal ActionTestStep Remove(string ssn)
        {
            return new ActionTestStep(driver =>
            {
                const string query = "Delete from dbo.Member where [Ssn] = @Ssn";

                using (var con = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        con.Execute(query, new {Ssn = ssn});
                    }
                    catch (Exception e)
                    {
                        throw e; // ??? Why re-throw?
                    }
                }
            });
        }

        internal ActionTestStep AssignToVerifiedRole(string ssn)
        {
            return new ActionTestStep(driver =>
            {
                const string query =
                    "Update dbo.Member Set [RoleId] = (Select [Id] from dbo.Role where [Name] = @Name) where Ssn = @Ssn";
                using (var con = new SqlConnection(ConnectionString))
                {
                    con.Execute(query, new {Name = Roles.VerifiedMember, Ssn = ssn});
                }
            });
        }
    }
}
