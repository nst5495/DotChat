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
                config.AddMap(new Chat_MemberMapper());
                config.AddMap(new ChatMapper());
                config.AddMap(new Chat_MessageMapper());
                config.AddMap(new Contact_ListMapper());
            });
        }
    }
}
