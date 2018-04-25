using Domain;
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

        public UserAccount Login(string name, string password)
        {
            var request = new RestRequest("/api/UserAccount/Login");
            request.AddParameter("name", name);
            request.AddParameter("password", password);
            IRestResponse<UserAccount> response = client.Execute<UserAccount>(request);
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

        


    }
}
