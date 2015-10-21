using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Topshelf;

namespace SimpleServer
{
    public class ApiContainer : ServiceControl
    {
        private HttpSelfHostServer server;
        public ApiContainer()
        {
            var baseAddress = "http://localhost:8092";
            var config = new HttpSelfHostConfiguration(baseAddress);
            new Bootstrap().Configure(config);
            server = new HttpSelfHostServer(config);


            //using (WebApp.Start<Startup>(baseAddress))
            //{
            //    new Bootstrap().Configure(config);
            //}
        }

        public bool Start(HostControl control)
        {
            try
            {
                server.OpenAsync();                
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.InnerException.Message);
                //
                return false;
            }

        }

        public bool Stop(HostControl control)
        {
            server.CloseAsync();
            server.Dispose();
            return true;
        }
    }
}