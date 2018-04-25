using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConnection
{
    public static class Extensions
    {
        public static void AddParameter(this IDbCommand command, string name, DbType type, object value)
        {
            IDbDataParameter param = command.CreateParameter();
            param.ParameterName = name;
            param.DbType = type;
            param.Value = value;
            command.Parameters.Add(param);
            
            
        }
    }
}
