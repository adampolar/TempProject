using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;

namespace Util
{
    public class WebServiceProxy<T> : IDisposable
    {
        public T Service { get; private set; }

        private ChannelFactory<T> _serviceProxy;

        public WebServiceProxy(string configName)
        {
            _serviceProxy = new ChannelFactory<T>(configName);
            _serviceProxy.Open();
            Service = _serviceProxy.CreateChannel();
        }



        public void Dispose()
        {
            _serviceProxy.Close();
        }
    }
}