using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Server.Logic
{
    [AttributeUsage(AttributeTargets.Class, Inherited=true)]
    public class ServiceAttribute : Attribute
    {
        public bool Service { get; private set; }

        public ServiceAttribute()
        {
            Service = true;
        }
    }
}
