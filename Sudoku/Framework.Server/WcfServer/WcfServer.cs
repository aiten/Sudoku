using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Framework.Server.Logic;

namespace Framework.Server.WcfServer
{
    public abstract class WcfServer
    {
        NetTcpBinding _binding;
        List<ServiceHost> _svh;

        public int Port { get; set; }

        public abstract void RegisterAssemblies();

        public bool IsRunning
        {
            get { return _svh != null; }
        }

        private ServiceHost Resister(Type TM, Type TI)
        {
            ServiceHost svh = new ServiceHost(TM);
            svh.AddServiceEndpoint(TI, _binding, "net.tcp://localhost:"+Port+"/" + TI.Name);
            svh.Open();
            return svh;
        }

        public void Start()
        {
            _svh = new List<ServiceHost>();
            _binding = new NetTcpBinding();

            RegisterAssemblies();
        }

        public void Stop()
        {
            foreach (ServiceHost svh in _svh)
                svh.Close();

            _svh = null;
            _binding = null;
        }


        protected void RegisterAssembly(Assembly ass)
        {
            foreach (Type t in ass.GetTypes())
            {
                if (t.IsClass)
                {
                    ServiceAttribute[] atts =
                        t.GetCustomAttributes(typeof(ServiceAttribute), true) as ServiceAttribute[];

                    if (atts != null && atts.Length > 0)
                    {
                        if (atts[0].Service)
                            _svh.Add(this.Resister(t, t.GetInterfaces()[0]));
                    }
                }
            }
        }

        protected static bool IsManager(MethodInfo prop)
        {
            ServiceAttribute[] atts = prop.GetCustomAttributes(typeof(ServiceAttribute), true) as ServiceAttribute[];

            if (atts != null && atts.Length > 0)
                return true;
            else
                return false;
        }
    }
}
