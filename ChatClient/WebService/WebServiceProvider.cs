using Domain;
using Domain.DBClasses;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.WebService
{
    public class WebServiceProvider
    {
        public const string url = "http://localhost:52273/";
        public static WebServiceProvider instance;
        public RestClient client;

        public WebServiceProvider()
        {
            client = new RestClient(url); 
        }

        public static WebServiceProvider getInstance()
        {
            if(instance == null)
            {
                instance = new WebServiceProvider();
            }
            return instance;
        }
        #region UserAccount Functions for UserAccount
        public UserAccount Login(string name, string password)
        {
            var request = new RestRequest("/api/UserAccount/Login");
            request.AddParameter("name", name);
            request.AddParameter("password", password);
            IRestResponse<UserAccount> response = client.Execute<UserAccount>(request);
            return response.Data;

        }

        public List<UserAccount> GetUsersForIds(List<int> ids)
        {
            var request = new RestRequest("/api/UserAccount/GetUsersForIds");
            request.AddParameter("ids", ids);
            var response = client.Execute<List<UserAccount>>(request);
            return response.Data;
        }

        public bool Register(UserAccount user)
        {
            var request = new RestRequest("/api/UserAccount/Register");
            request.AddParameter("username", user.UserName);
            request.AddParameter("password", user.Password);
            request.AddParameter("firstname", user.FirstName);
            request.AddParameter("lastname", user.LastName);
            IRestResponse<bool> response = client.Execute<bool>(request);
            return response.Data;
        }

        public UserAccount ChangePassword(string username, string password)
        {
            var request = new RestRequest("/api/UserAccount/ChangePassword");
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            var response = client.Execute<UserAccount>(request);
            return response.Data;
        }


        public UserAccount GetUserForName(string username)
        {
            var request = new RestRequest("/api/UserAccount/GetUserForName");
            request.AddParameter("username", username);
            IRestResponse<UserAccount> response = client.Execute<UserAccount>(request);
            return response.Data;
        }
        #endregion


        #region Chat Functions for Chat
        public List<Chat> GetChatsForUser(int userid)
        {
            var request = new RestRequest("/api/Chat/GetChatsForUser");
            request.AddParameter("userid", userid);
            var response = client.Execute<List<Chat>>(request);
            return response.Data;
        }

        public bool AddChatToUser(int[] members, int adminid, string title)
        {
            var request = new RestRequest("api/Chat/AddChattoUser");
            //Pass a string because asp net doesn't support arrays as URL parameters
            string membersstring = "";
            foreach(int i in members)
            {
                membersstring += i + ";";
            }
            request.AddParameter("members", membersstring);
            request.AddParameter("adminid", adminid);
            request.AddParameter("title", title);
            var response = client.Execute<bool>(request);
            return response.Data;
        }

        public List<UserAccount> GetMembersForChat(int chatid)
        {
            var request = new RestRequest("api/Chat/GetMembersForChat");
            request.AddParameter("chatid", chatid);
            var response = client.Execute<List<UserAccount>>(request);
            return response.Data;
        }

        #endregion

        #region Message Functions for Message

        public int AddMessage(int chatid, string message, int senderid)
        {
            var request = new RestRequest("api/Message/AddMessage");
            request.AddParameter("chatid", chatid);
            request.AddParameter("message", message);
            request.AddParameter("senderid", senderid);
            var response = client.Execute<int>(request);
            return response.Data;
        }

        public List<Chat_Message> GetMessagesForChat(int chatid)
        {
            var request = new RestRequest("api/Message/GetMessagesForChat");
            request.AddParameter("chatid", chatid);
            var response = client.Execute<List<Chat_Message>>(request);
            return response.Data;
        }

        #endregion

    }
}
