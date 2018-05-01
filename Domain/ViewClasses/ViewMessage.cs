using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewClasses
{
    public class ViewMessage
    {
        public int Id { set; get; }
        public ViewChat Chat { set; get; }
        public UserAccount Sender { set; get; }
        public string Message { set; get; }
        public DateTime TimeStamp { set; get; }
        public int Senderid { set; get; }

        public override string ToString()
        {
            return Sender.UserName + ": " + Message + Environment.NewLine;
        }

    }
}
