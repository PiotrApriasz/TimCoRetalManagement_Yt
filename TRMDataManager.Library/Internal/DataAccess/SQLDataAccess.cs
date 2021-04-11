using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace TRMDataManager.Library.Internal.DataAccess
{
    //This is internal because we don't want to use this class in other projects in solution
    internal class SQLDataAccess
    {
        /// <summary>
        /// Get connection string based on matching name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetConnectionString(string name)
        {
            //Go to the web.config or app config, gets the conection string with the matching name
            //and returns that connection string
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        /// <summary>
        /// Load data from the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="storeProcedure"></param>
        /// <param name="parameters"></param>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public List<T> LoadData<T, U>(string storeProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            //Makes connection to database
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                //Returns set of rows
                List<T> rows = connection.Query<T>(storeProcedure, parameters,
                        commandType: CommandType.StoredProcedure)
                        .ToList();

                return rows;
            }
        }

        /// <summary>
        /// Save data to the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storeProcedure"></param>
        /// <param name="parameters"></param>
        /// <param name="connectionStringName"></param>
        public void SaveData<T>(string storeProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            //Makes connection to database
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storeProcedure, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
