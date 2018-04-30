using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.DBClasses;

namespace DataConnection.Facades
{
    public class ChatFacade
    {
        public static ChatFacade instance;
        private DataConnectionManager connection;
       
        public static ChatFacade GetInstance()
        {
            if(instance == null)
            {
                instance = new ChatFacade(new DataConnectionManager());
            }
            return instance;
        }

        private ChatFacade(DataConnectionManager mgr)
        {
            connection = mgr;
        }

        public List<Chat> GetChatsForUser(int userid)
        {
            return connection.GetConnection().Query<Chat>("SELECT chat_id, chat_title FROM chat_member cm RIGHT JOIN chat ch ON cm.chat_member_chat_id = ch.chat_id  WHERE chat_member_user_id = @id", new { id = userid }).ToList();
        }

        public bool AddChatToUser(List<int> userids, string title, int adminid)
        {
            connection.GetConnection().Execute("INSERT INTO chat (chat_title) VALUES (@title)", new { title = title });
            int id = connection.GetConnection().Query<Chat>("SELECT * FROM chat WHERE chat_title = @title", new { title = title }).FirstOrDefault().Id;
            foreach(int i in userids)
            {
                connection.GetConnection().Execute("INSERT INTO chat_members (chat_member_chat_id, chat_member_user_id, chat_member_user_is_admin) VALUES (@chatid,@userid,0)", new { chatid = id, userid = i });
            }
            connection.GetConnection().Execute("INSERT INTO chat_members(chat_member_chat_id,chat_member_user_id,chat_member_user_is_admin) VALUES (@chatid,@userid,1)", new { chatid = id, userid = adminid });
            return true;

        }
    }
}
