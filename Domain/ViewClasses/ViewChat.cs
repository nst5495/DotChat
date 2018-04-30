using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewClasses
{
    public class ViewChat
    {
        public List<UserAccount> Members;
        public string Title;

        
        public override String ToString()
        {
            return Title;
        }
    }
}
