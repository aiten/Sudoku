using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Framework.Client.WcfClient
{
    static public class WcfClient
    {
        static string _ip = @"localhost";
        static int _port = 32224;
        static string _binding = @"net.tcp:";

        static public int Port { get { return _port; } set { _port = value; }  }
        static public String IP { get { return _ip; } set { _ip = value; } }
        static public String Binding { get { return _binding; } set { _binding = value; } }

        static private string BuildConnectString(Type t)
        {
            return _binding + @"//" + _ip + ":" + _port.ToString() + "/" + t.Name;
        }

        public static T Create<T>()
        {
            ChannelFactory<T> scf;
            scf = new ChannelFactory<T>(new NetTcpBinding(), BuildConnectString(typeof(T)));

            return scf.CreateChannel();
        }
    }
}
