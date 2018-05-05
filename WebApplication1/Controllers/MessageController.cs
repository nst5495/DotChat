using DataConnection.Facades;
using Domain.DBClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class MessageController : ApiController
    {
        [HttpGet]
        public int AddMessage(int chatid, string message, int senderid)
        {
            return MessageFacade.getInstance().AddMessage(chatid, message, senderid);
        }

        [HttpGet]
        public List<Chat_Message> GetMessagesForChat(int chatid)
        {
            return MessageFacade.getInstance().GetMessagesForChat(chatid);
        }

        [HttpGet]
        public List<Chat_Message> CheckForNewMessages(DateTime timesince, int chatid)
        {
            return MessageFacade.getInstance().CheckForNewMessages(timesince, chatid);
        }
    }
}