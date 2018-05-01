using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewClasses
{
    public class ViewChat
    {
        public List<UserAccount> Members;
        public List<ViewMessage> Messages;
        public string Title;
        public int Id;

        
        public override String ToString()
        {
            return Title;
        }
    }
}
