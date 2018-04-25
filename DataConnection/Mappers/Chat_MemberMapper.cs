using Dapper.FluentMap.Mapping;
using Domain.DBClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConnection.Mappers
{
    public class Chat_MemberMapper : EntityMap<Chat_Member>
    {
        public Chat_MemberMapper()
        {
            Map(x => x.Id).ToColumn("chat_member_id", false);
            Map(x => x.Chatid).ToColumn("chat_member_chat_id", false);
            Map(x => x.Userid).ToColumn("chat_member_user_id", false);
            Map(x => x.Userisadmin).ToColumn("chat_member_user_is_admin", false);
        }
    }
}
