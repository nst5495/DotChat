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

        public bool AddChatToUser(int[] userids, string title, int adminid)
        {
            //https://stackoverflow.com/questions/8270205/how-do-i-perform-an-insert-and-return-inserted-identity-with-dapper
            //@TODO : Make this actually work
            connection.GetConnection().Execute(@"INSERT INTO chat (chat_title)VALUES (@Title)", new { Title = title });
            int id = connection.GetConnection().QuerySingle<int>(@"SELECT LAST_INSERT_ID()");
            foreach(int i in userids)
            {
                connection.GetConnection().Execute("INSERT INTO chat_member (chat_member_chat_id, chat_member_user_id, chat_member_user_is_admin) VALUES (@chatid,@userid,0)", new { chatid = id, userid = i });
            }
            connection.GetConnection().Execute("INSERT INTO chat_member(chat_member_chat_id,chat_member_user_id,chat_member_user_is_admin) VALUES (@chatid,@userid,1)", new { chatid = id, userid = adminid });
            return true;

        }

        public Chat GetChat(int chatid)
        {
            return connection.GetConnection().Query<Chat>("SELECT * FROM chat WHERE chat_id == @id", new { id = chatid }).FirstOrDefault();
        }

        public List<UserAccount> GetMembersForChat(int chatid)
        {
            var members = connection.GetConnection().Query<Chat_Member>("SELECT * FROM chat_member WHERE chat_member_chat_id = @id", new { id = chatid });
            List<UserAccount> lac = new List<UserAccount>();
            foreach(Chat_Member c in members)
            {
                lac.Add(connection.GetConnection().Query<UserAccount>("SELECT * FROM useraccount WHERE useraccount_id = @id", new { id = c.Userid }).FirstOrDefault());
            }
            return lac;
        }

        public List<Chat> CheckforNewChats(int lastchatid, int userid)
        {
            List<Chat> chats = GetChatsForUser(userid);
            if(chats.Where(x=>x.Id > lastchatid).Count() > 0)
            {
                return chats.Where(x => x.Id > lastchatid).ToList();
            }
            return new List<Chat>();
        }

        public bool DeleteChat(int chatid)
        {
            List<UserAccount> members = GetMembersForChat(chatid);
            foreach(UserAccount m in members)
            {
                DeleteChatMember(chatid, m.Id);
            }
            connection.GetConnection().Execute("DELETE FROM chat WHERE chat_id = @cid", new { cid = chatid });
            return true;
        }

        private void DeleteChatMember(int chatid, int userid)
        {
            connection.GetConnection().Execute("DELETE FROM chat_member WHERE chat_member_user_id = @uid AND chat_member_chat_id = @cid",new { uid = userid, cid = chatid });
        }

        public bool UserIsAdmin(int chatid, int userid)
        {
            Chat_Member member =  connection.GetConnection().Query<Chat_Member>("SELECT * FROM chat_member WHERE chat_member_user_id = @uid AND chat_member_chat_id = @cid", new { uid = userid, cid = chatid }).FirstOrDefault();
            return member.Userisadmin == 1;
        }
    }
}
