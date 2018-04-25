using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DBClasses
{
    public class Chat_Member
    {
        public int Id { set; get; }
        public int Userid { set; get; }
        public int Chatid { set; get; }
        public int Userisadmin { set; get; }
    }
}
