using Dapper.FluentMap.Mapping;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConnection.Mappers
{
    public class ChatMapper : EntityMap<Chat>
    {
        public ChatMapper()
        {
            Map(x => x.Id).ToColumn("chat_id", false);
            Map(x => x.Title).ToColumn("chat_title",false);
        }
    }
}
