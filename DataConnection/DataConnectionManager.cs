using MySql.Data.MySqlClient;
using System.Data;

namespace DataConnection
{
    public class DataConnectionManager
    {
        private MySqlConnection connection;
        public IDbConnection GetConnection()
        {
            if(connection == null)
            {
                connection = new MySqlConnection($"Server={Properties.Resources.Servername};Database={Properties.Resources.DBName};Uid={Properties.Resources.Username};Pwd={Properties.Resources.Password};Ssl Mode=none;");
            }
            return connection;
        }
    }
}