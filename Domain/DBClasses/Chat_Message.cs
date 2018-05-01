using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DBClasses
{
    public class Chat_Message
    {
        public int Id { set; get; }
        public int Senderid { set; get; }
        public int Chatid { set; get; }
        public string Message { set; get; }
        public DateTime Timestamp { set; get; }
    }
}
