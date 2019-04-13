using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ODZ_TSPP.DB
{
    public class DBUtils
    {
        public static MySqlConnection GetDBConnection(string host, string username, string database, string password)
        {
            String connString = $"server={host};user={username};database={database};password={password}";

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
    }
}
