using Dapper.FluentMap;
using DataConnection.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConnection
{
    public static class DapperConfiguration
    {
        public static void Map()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new UserAccountMapper());
            });
        }
    }
}
