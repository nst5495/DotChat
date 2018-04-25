using MySql.Data.MySqlClient;
using System.Data;

namespace DataConnection
{
    public class DataConnectionManager
    {
        public IDbConnection GetConnection()
        {
           
            return new MySqlConnection($"Server={Properties.Resources.Servername};Database={Properties.Resources.DBName};Uid={Properties.Resources.Username};Pwd={Properties.Resources.Password};Ssl Mode=none;");
        }
    }
}