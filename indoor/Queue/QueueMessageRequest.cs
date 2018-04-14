using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
namespace indoor.Queue
{
    public class QueueMessageRequest
    {
        public string User
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string Endpoint
        {
            get;
            set;
        }

        public List<string> GetParameters
        {
            get;
            set;
        }

        public string JsonBodyContent
        {
            get;
            set;
        }

        public QueueMessageRequest(string User, string Password, string Endpoint, List<string> GetParameters, string JsonBodyContent)
        {
            this.User = User;
            this.Password = Password;
            this.Endpoint = Endpoint;
            this.GetParameters = GetParameters;
            this.JsonBodyContent = JsonBodyContent;
        }

        public QueueMessageRequest(string User, string Password, string Endpoint, string JsonBodyContent)
        {
            this.User = User;
            this.Password = Password;
            this.Endpoint = Endpoint;
            this.JsonBodyContent = JsonBodyContent;
        }

        public QueueMessageRequest(string User, string Password, string Endpoint, List<string> GetParameters)
        {
            this.User = User;
            this.Password = Password;
            this.Endpoint = Endpoint;
            this.GetParameters = GetParameters;
        }

        public QueueMessageRequest(string User, string Password, string Endpoint)
        {
            this.User = User;
            this.Password = Password;
            this.Endpoint = Endpoint;
        }
    }
}
