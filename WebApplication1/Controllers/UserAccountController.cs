using DataConnection;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Services;

namespace WebApplication1.Controllers
{
    public class UserAccountController : ApiController
    {
        [HttpGet]
        public UserAccount Login(string name, string password)
        {
            return UserFacade.GetInstance().GetUser(name, password);
        }

        [HttpGet]
        public bool Register(string username, string password, string firstname, string lastname)
        {
            UserAccount user = new UserAccount();
            user.FirstName = firstname;
            user.LastName = lastname;
            user.UserName = username;
            user.Password = password;
            return UserFacade.GetInstance().AddUser(user);
        }

        [HttpGet]
        public UserAccount ChangePassword(string username, string password)
        {
            return UserFacade.GetInstance().ChangePassword(username, password);
        }

        [HttpGet]
        public UserAccount GetUserForName(string username)
        {
            return UserFacade.GetInstance().GetUserForName(username);
        }

        [HttpGet]
        public List<UserAccount> GetUsersForIds(List<int> ids)
        {
            return UserFacade.GetInstance().GetUsersForIds(ids);
        }

        [HttpGet]
        public bool UpdateStats(int userid, string status)
        {
            return UserFacade.GetInstance().UpdateStatus(userid, status);
        }
    }
}