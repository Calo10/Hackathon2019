using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace GFP.Helper
{
    public class DbConnectionHelper
    {
        public enum ConnType
        {
            Sql,
            MySql
        }

        public static IDbConnection GetOpenConnection(string connectionString, ConnType type)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new Exception(string.Format("Failed to get {0} connection, connectionString is null or empty.", type));

            IDbConnection connection;
            switch (type)
            {
                case ConnType.MySql:
                    connection = new MySqlConnection(connectionString);
                    break;
                case ConnType.Sql:
                default:
                    connection = null;
                    break;
            }
            connection.Open();
            return connection;
        }

        public async static Task<IEnumerable<dynamic>> QueryAsync(string connectionString, ConnType type, string query, object parmObject = null, bool isStoredProc = false)
        {
            using (var conn = GetOpenConnection(connectionString, type))
            {
                return isStoredProc ?
                    await conn.QueryAsync<dynamic>(query, parmObject, commandType: CommandType.StoredProcedure) :
                    await conn.QueryAsync<dynamic>(query, parmObject);
            }
        }

        public static IEnumerable<dynamic> Query(string connectionString, ConnType type, string query, object parmObject = null, bool isStoredProc = false)
        {
            return QueryAsync(connectionString, type, query, parmObject, isStoredProc).Result;
        }

        public async static Task<int> ExecuteAsync(string connectionString, ConnType type, string query, object parmObject, bool isStoredProc = false)
        {
            using (var conn = GetOpenConnection(connectionString, type))
            {
                return isStoredProc ?
                    await conn.ExecuteAsync(query, parmObject, commandType: CommandType.StoredProcedure) :
                    await conn.ExecuteAsync(query, parmObject);
            }
        }

        public static int Execute(string connectionString, ConnType type, string query, object parmObject, bool isStoredProc = false)
        {
            return ExecuteAsync(connectionString, type, query, parmObject, isStoredProc).Result;
        }

        public async static Task<List<IEnumerable<dynamic>>> QueryMultipleAsync(string connectionString, ConnType type, string query, object parmObject = null)
        {
            using (var conn = GetOpenConnection(connectionString, type))
            {
                using (var multi = await conn.QueryMultipleAsync(query, parmObject))
                {
                    var result = new List<IEnumerable<dynamic>>();
                    while (!multi.IsConsumed)
                    {
                        result.Add(multi.Read<dynamic>());
                    }
                    return result;
                }
            }
        }

        public static List<IEnumerable<dynamic>> QueryMultiple(string connectionString, ConnType type, string query, object parmObject = null)
        {
            return QueryMultipleAsync(connectionString, type, query, parmObject).Result;
        }

        public async static Task<int> BulkTransaction(string connectionString, ConnType type, string query, List<object> bulkList, bool isStoredProc = false)
        {
            var affected = 0;

            using (var conn = GetOpenConnection(connectionString, type))
            {
                using (var transaction = conn.BeginTransaction())
                {

                    try
                    {
                        foreach (var item in bulkList)
                        {
                            affected = +await conn.ExecuteAsync(query, item, transaction, null, CommandType.StoredProcedure);
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }


                }
            }
            return affected;
        }
    }
}
