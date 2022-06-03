using System;

namespace lab.WebApi19Sample.Utility
{
    public static class ConnectHelper
    {
        public static ConnectionConfig WithHadoopHdfsUser(string serverUrl, string serverRelativeUrl)
        {
            return new ConnectionConfig(
                server: new Uri(serverUrl),
                serverRelativeUrl: serverRelativeUrl,
                userName: "hue",
                password: "");
        }

        public static ConnectionConfig WithElasticSearchUser(string serverUrl)
        {
            return new ConnectionConfig(
                server: new Uri(serverUrl),
                serverRelativeUrl: "",
                userName: "",
                password: "");
        }
    }
}
