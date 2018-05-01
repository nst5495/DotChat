using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConnection
{
    public class UserFacade
    {
        public static UserFacade Instance;

        private DataConnectionManager Connection;

        private UserFacade(DataConnectionManager manager)
        {
            Connection = manager;
        }

        public UserAccount GetUser(string username, string userpassword)
        {
            return Connection.GetConnection().Query<UserAccount>("SELECT * FROM useraccount WHERE useraccount_username = @name AND useraccount_password = @password", new { name = username, password = userpassword }).FirstOrDefault();
        }

        public UserAccount GetUserForName(string username)
        {
            return Connection.GetConnection().Query<UserAccount>("SELECT * FROM useraccount WHERE useraccount_username = @name", new { name = username}).FirstOrDefault();
        }

        public List<UserAccount> GetUsersForIds(List<int> ids)
        {
            List<UserAccount> uas = new List<UserAccount>();
            foreach(int i in ids)
            {
                uas.Add(Connection.GetConnection().Query<UserAccount>("SELECT * FROM useraccount WHERE useraccount_id = @id", new { id = i }).FirstOrDefault());
            }
            return uas;
        }

        public static UserFacade GetInstance()
        {
            if(Instance == null)
            {
                Instance = new UserFacade(new DataConnectionManager());
            }

            return Instance;
        }

        public bool AddUser(UserAccount user)
        {
            if(IsDuplicateUsername(user.UserName))
            {
                
                return false;
            }
            IDbConnection con = Connection.GetConnection();
            con.Execute("INSERT INTO useraccount " +
                "(useraccount_firstname,useraccount_lastname,useraccount_username,useraccount_password) VALUES(@firstname,@lastname,@username,@password)",
                new { firstname = user.FirstName, lastname = user.LastName, username = user.UserName, password = user.Password });
            return true;
        }

        public bool IsDuplicateUsername(string username)
        {
            UserAccount user = Connection.GetConnection().Query<UserAccount>("SELECT * FROM useraccount WHERE useraccount_username = @name", new { name = username }).SingleOrDefault();
            return user != null;
        }

        public UserAccount ChangePassword(string username, string password)
        {
            Connection.GetConnection().Execute("UPDATE useraccount SET useraccount_password = @password WHERE useraccount_username = @username",
                new { password = password, username = username });
            return GetUser(username, password);
        }
        
    }
}
