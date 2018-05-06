using DataConnection.Facades;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class ChatController : ApiController
    {
        [HttpGet]
        public List<Chat> GetChatsForUser(int userid)
        {
           return ChatFacade.GetInstance().GetChatsForUser(userid);
        }

        //asp net doesn't support arrays as URL parameters, which is why we are parsing a string instead.
        [HttpGet]
        public bool AddChattoUser(string members, int adminid, string title)
        {
            string[] memberssplit = members.Split(new char[] { ';' },StringSplitOptions.RemoveEmptyEntries);
            List<int> membersintlist = new List<int>();
            foreach(string s in memberssplit)
            {
                membersintlist.Add(int.Parse(s));
            }
            return ChatFacade.GetInstance().AddChatToUser(membersintlist.ToArray(), title, adminid);
        }

        [HttpGet]
        public List<UserAccount> GetMembersForChat(int chatid)
        {
            return ChatFacade.GetInstance().GetMembersForChat(chatid);
        }

        [HttpGet]
        public List<Chat> CheckForNewChats(int lastchatid, int userid)
        {
            return ChatFacade.GetInstance().CheckforNewChats(lastchatid,userid);
        }

        [HttpGet]
        public bool DeleteChat(int deletechatid)
        {
            return ChatFacade.GetInstance().DeleteChat(deletechatid);
        }

        [HttpGet]
        public bool UserIsAdmin(int chatid, int userid)
        {
            return ChatFacade.GetInstance().UserIsAdmin(chatid, userid);
        }
    }
}