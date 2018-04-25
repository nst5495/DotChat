using Domain;
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
    }
}
