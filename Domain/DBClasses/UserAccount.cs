using System;

namespace Domain
{
    public class UserAccount 
    {
        public int Id { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string StatusMessage { set; get; }
        public int Status { set; get; }
        public object UserIcon { set; get; }
    }
}
