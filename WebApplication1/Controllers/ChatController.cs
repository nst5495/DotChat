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
    }
}