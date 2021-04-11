using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDataManager.Library
{
    public class SQLDataAccess
    {
        public string GetConnectionString(string name)
        {
            //Go to the web.config or app config, gets the conection string with the matching name
            //and returns that connection string
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
