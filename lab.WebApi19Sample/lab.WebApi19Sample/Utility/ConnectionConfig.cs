using System;

namespace lab.WebApi19Sample.Utility
{
    public class ConnectionConfig
    {
        public ConnectionConfig(Uri server, string serverRelativeUrl, string userName, string password)
        {
            Server = server;
            ServerRelativeUrl = serverRelativeUrl;
            UserName = userName;
            Password = password;
        }

        public Uri Server { get; private set; }

        public string ServerRelativeUrl { get; private set; }

        public string UserName { get; private set; }

        public string Password { get; private set; }
    }
}
