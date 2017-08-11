using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Book;
using DataAccess.EntityMaps.Member;
using DataAccess.Helpers;

namespace DataAccess.Queries.Member
{
    public class MemberAccess : IMemberAccess
    {
        private static readonly string ConString = Utilities.ConnectionStrings.Local;

        public async Task<bool> InsertOneAsync(IMap map)
        {
            const string query = // For testing, in db these arent implemented yet
                "INSERT INTO dbo.Member ([Ssn], "+ /*[Username],*/" [Password], [RoleId], [Campus], [Membership-expiration], [FirstName], [LastName]) " +
                "VALUES (@Ssn, "+ /*@Username,*/  "@Password, @RoleId, @Campus, @MembershipExpiration, @FirstName, @LastName) ";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.ExecuteAsync(query, AllParameters(map as IMemberMap));
                    return result == 1;
                }
                catch(Exception e)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<bool> UpdateOneAsync(IMap map)
        {
            const string query =
                "Update dbo.Member SET [Ssn] = @Ssn, [Password] = @Password, [RoleId] = @RoleId, [Campus] = @Campus, " + 
                "[Membership-expiration] = @MembershipExpiration, [FirstName] = @FirstName, [LastName] = @LastName " +
                "WHERE [Ssn] = @Ssn";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.ExecuteAsync(query, AllParameters(map as IMemberMap));
                    return result == 1;
                }
                catch (Exception e)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<bool> InsertOrUpdateAsync(IMap map)
        {
            //Todo: create stored procedure
            const string sp = "[]";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result =
                        await
                            con.ExecuteAsync(sp, AllParameters(map as IMemberMap),
                                commandType: CommandType.StoredProcedure);

                    return result == 1;
                }
                catch (Exception)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<bool> DeleteOneAsync(IMap map)
        {
            const string query =
                "Delete from Member where [Ssn] = @Ssn";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.ExecuteAsync(query, PrimaryParameter(map as IMemberMap));
                    return result == 1;
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IMap>> SelectAllAsync()
        {
            const string query = "Select * from dbo.Member";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.QueryAsync<MemberMap>(query);
                    return result.ToList();
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<IMemberMap> FindBySsnAndPasswordAsync(string ssn, string password)
        {
            using (var con = new SqlConnection(ConString))
            {
                //Todo: create stored procedure
                const string sp = "[]";

                try
                {
                    var result = await con.QueryAsync<MemberMap>(sp, LoginParameters(ssn, password), commandType: CommandType.StoredProcedure);
                    return result.First();
                }
                catch (Exception)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        // ->>>>>> This method failed when db has different namings than in mapping

        public async Task<IMemberMap> FindBySsn(string ssn)
        {
            const string query = "Select * from dbo.Member where [Ssn] = @Ssn";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.QueryAsync<MemberMap>(query, new {Ssn = ssn});
                    return result.FirstOrDefault();
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<IMemberMap> FindByUsername(string username)
        {
            // We use ssn as username for identity
            const string query = "Select * from dbo.Member where [Ssn] = @Username";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.QueryAsync<MemberMap>(query, new { Username = username });
                    return result.FirstOrDefault();
                }
                catch(Exception e)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<string> FindMemberRoleName(string roleId)
        {
            const string query = "Select [Name] from dbo.Role where [Id] = @RoleId";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.QueryAsync<string>(query, new { RoleId = roleId });
                    return result.First();
                }
                catch
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<bool> IsMemberInRole(IMemberMap member, string roleName)
        {
            using (var con = new SqlConnection(ConString))
            {
                //Todo: create stored procedure
                const string sp = "[]";

                try
                {
                    var result = await con.QueryAsync<bool>(sp, MemberRoleParameters(member, roleName), commandType: CommandType.StoredProcedure);
                    return result.First();
                }
                catch (Exception)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<bool> AddMemberToRole(IMemberMap member, string roleName)
        {
            using (var con = new SqlConnection(ConString))
            {
                //Todo: create stored procedure
                const string sp = "[]";
                //Since we don't have that yet, for now I will implement in usual way
                const string query =
                    "Update dbo.Member Set [RoleId] = (Select [Id] from dbo.Role where [Name] = @Name) where Ssn = @Ssn";

                try
                {
                    //var result = await con.QueryAsync<bool>(sp, MemberRoleParameters(member, roleName), commandType: CommandType.StoredProcedure);
                    var result = await con.ExecuteAsync(query, new {Name = roleName, Ssn = member.Ssn});
                    return result == 1;
                }
                catch (Exception)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<bool> RemoveMemberRole(IMemberMap member, string roleName)
        {
            using (var con = new SqlConnection(ConString))
            {
                //Todo: create stored procedure
                const string sp = "[]";

                try
                {
                    var result = await con.QueryAsync<bool>(sp, MemberRoleParameters(member, roleName), commandType: CommandType.StoredProcedure);
                    return result.First();
                }
                catch (Exception)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<IRoleMap> FindMemberRoleByRoleId(int id)
        {
            using (var con = new SqlConnection(ConString))
            {
                const string query = "Select [Name] from dbo.Role where [Id] = @RoleId";

                try
                {
                    var result = await con.QueryAsync<RoleMap>(query, new { RoleId = id });
                    return result.First();
                }
                catch (Exception)
                {
                    throw new Exception("Unexpected Database Error");
                }
            } 
        }

        public async Task<IRoleMap> FindMemberRoleByName(string name)
        {
            using (var con = new SqlConnection(ConString))
            {
                const string query = "Select [Id] from dbo.Role where [Name] = @name";

                try
                {
                    var result = await con.QueryAsync<RoleMap>(query, new { Name = name });
                    return result.FirstOrDefault();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public async Task<bool> SetPasswordHash(string ssn, string passwordHash)
        {
            const string query = "Update dbo.Member Set Password = @Password Where [Ssn] = @Ssn";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.ExecuteAsync(query, new {Password = passwordHash, Ssn = ssn});
                    return result == 1;
                }
                catch (Exception e)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<string> GetPasswordHash(string ssn)
        {
            using (var con = new SqlConnection(ConString))
            {
                const string query = "Select [Password] from dbo.Member where [Ssn] = @Ssn";

                try
                {
                    var result = await con.QueryAsync<MemberMap>(query, new {Ssn = ssn});
                    return result.First().Password;
                }
                catch (Exception e)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<IEnumerable<IMemberItemModelMap>> FindMemberModelsByRoleName(string roleName)
        {
            const string query = "Select [Ssn], [FirstName], [LastName] from dbo.Member where [RoleId] = (Select [Id] from dbo.Role where [Name] = @Name)";

            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = await con.QueryAsync<MemberItemModelMap>(query, new {Name = roleName});
                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        public async Task<int> GetMemberInRoleCount(string roleName)
        {
            const string query =
                "Select COUNT([Ssn]) from dbo.Member where [RoleId] = (Select [Id] from dbo.Role where [Name] = @Name)";
            using (var con = new SqlConnection(ConString))
            {
                try
                {
                    var result = (int)(await con.ExecuteScalarAsync(query, new {Name = roleName}));
                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Unexpected Database Error");
                }
            }
        }

        #region Helpers

        private static object AllParameters(IMemberMap member)
        {
            var MembershipExpiration = member.MembershipExpiration.Date.ToString("yyyy-MM-dd HH:mm:ss");
            return new
            {
                member.Ssn,
                //member.Username,
                member.Password,
                member.Campus,
                member.FirstName,
                member.LastName,
                MembershipExpiration,
                member.RoleId
            };
        }

        private static object PrimaryParameter(IMemberMap member)
        {
            return new
            {
                member.Ssn,
            };
        }

        private static object LoginParameters(string ssn, string password)
        {
            return new
            {
                ssn,
                password
            };
        }

        private static object MemberRoleParameters(IMemberMap member, string roleName)
        {
            return new
            {
                member.Ssn,
                member.Username,
                member.Password,
                member.Campus,
                member.FirstName,
                member.LastName,
                member.MembershipExpiration,
                member.RoleId,
                roleName
            };
        }

        #endregion
    }
}
