using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(SimpleServer.Startup))]

namespace SimpleServer
{
    using Topshelf;
    using TopShelf.Owin;

    class Program
    {
        static void Main(string[] args)
        {
            //como consola
            //var server = new ApiContainer();
            //server.Start();
            //Console.WriteLine("Servidor esta levantado....");
            //Console.ReadKey();
            //server.Stop();

            //como servicio
            HostFactory.Run(host =>
            {
                host.Service<ApiContainer>(container =>
                {
                    container.ConstructUsing(() => new ApiContainer());
                    container.WhenStarted((svc, hostControl) => svc.Start(hostControl));
                    container.WhenStopped((svc, hostControl) => svc.Stop(hostControl));
                    container.WhenShutdown((svc, hostControl) => svc.Stop(hostControl));
                    container.OwinEndpoint(app =>
                    {
                        app.ConfigureAppBuilder(appBuilder => appBuilder.New());
                    });
                });

                host.SetDescription("Este es un ejemplo de un SelfHosted WebAPI como servicio usando TopShelf");
                host.SetDisplayName("SimpleApi");
                host.SetServiceName("SimpleApi");
            });
        }
    }
}
