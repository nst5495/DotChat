using Dapper.FluentMap.Mapping;
using Domain.DBClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConnection.Mappers
{
    public class Chat_MessageMapper : EntityMap<Chat_Message>
    {
        public Chat_MessageMapper()
        {
            Map(x => x.Id).ToColumn("chat_messages_chat_id", false);
            Map(x => x.Message).ToColumn("chat_messages_message", false);
            Map(x => x.Senderid).ToColumn("chat_messages_sender_id", false);
            Map(x => x.Timestamp).ToColumn("chat_messages_timestamp", false);
        }
    }
}
