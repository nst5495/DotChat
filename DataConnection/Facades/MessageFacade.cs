﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.DBClasses;

namespace DataConnection.Facades
{
    public class MessageFacade
    {
        public static MessageFacade instance;
        private DataConnectionManager connection;

        public static MessageFacade getInstance()
        {
            if(instance == null)
            {
                instance = new MessageFacade(new DataConnectionManager());
            }
            return instance;
        }

        private MessageFacade(DataConnectionManager mgr)
        {
            connection = mgr;
        }

        //Returns id of inserted row
        public int AddMessage(int chatid, string message, int senderid)
        {
            connection.GetConnection().Execute("INSERT INTO chat_messages(chat_messages_chat_id,chat_messages_message,chat_messages_sender_id,chat_messages_timestamp) VALUES (@cid,@msg,@sid,@ts)", new { cid = chatid, msg = message, sid = senderid, ts = DateTime.Now });
            return connection.GetConnection().QuerySingle("SELECT LAST_INSERT_ID()");
        }

        public List<Chat_Message> GetMessagesForChat(int chatid)
        {
            return connection.GetConnection().Query<Chat_Message>("SELECT * FROM chat_messages WHERE chat_messages_chat_id = @cid", new { cid = chatid }).ToList();
        }
    }
}