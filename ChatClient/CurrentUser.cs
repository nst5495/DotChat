using ChatClient.WebService;
using Domain;
using Domain.DBClasses;
using Domain.ViewClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    public class CurrentUser
    {
        private static UserAccount loggedin;
        public static bool isloggedin;
        public static List<ViewChat> chats;

        public static UserAccount GetCurrentUser()
        {
           if(isloggedin)
           {
                return loggedin;
           }
           else
            {
                throw new Exception("User not logged in");
            }
        }

        public static void Login(UserAccount acc)
        {
            loggedin = acc;
            isloggedin = true;
        }

        public static void Init()
        {
            InitChats();
        }

        private static void InitChats()
        {
            chats = new List<ViewChat>();
            List<Chat> cs = WebServiceProvider.getInstance().GetChatsForUser(loggedin.Id);

            foreach(Chat c in cs)
            {
                List<Chat_Message> cms = WebServiceProvider.getInstance().GetMessagesForChat(c.Id);
                List<ViewMessage> vms = new List<ViewMessage>();
                foreach(Chat_Message cm in cms)
                {
                    ViewMessage vm = new ViewMessage
                    {
                        Id = cm.Id,
                        Message = cm.Message,
                        TimeStamp = cm.Timestamp,
                        Senderid = cm.Senderid
                    };
                    vms.Add(vm);
                }
                ViewChat vc = new ViewChat
                {
                    Title = c.Title,
                    Members = WebServiceProvider.getInstance().GetMembersForChat(c.Id),
                    Messages = vms,
                    Id = c.Id
                };
                //This probably breaks if someone leaves a group
                foreach(ViewMessage viewmsg in vc.Messages)
                {
                    viewmsg.Sender = vc.Members.Where(x => x.Id == viewmsg.Senderid).FirstOrDefault();
                }
                chats.Add(vc);
            }
        }
    }
}
