using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using Xunit;
using FluentAssertions;

namespace SimpleServer.Test
{
    public class TasksControllerTests
    {
        private HttpClient GetClient()
        {
            var urlBase = new Uri("http://localhost:9090");
            var config = new HttpSelfHostConfiguration(urlBase);
            var server = new HttpSelfHostServer(config);
            new SimpleServer.Bootstrap().Configure(config);
            var client = new HttpClient(server);

            try
            {
                client.BaseAddress = urlBase;
                return client;
            }
            catch (Exception)
            {
                client.Dispose();
                throw;
            }
        }

        [Fact]
        public async Task ShouldReturnOk() {
            using (var client = GetClient())
            {
                var getRequest = await client.GetStringAsync("api/tasks?name=Gente");
                getRequest.Should().Be("\"Hola Gente\"");
            }
        }

        [Fact]
        public void PostShouldReturnOkResponse()
        {
            using (var client = GetClient())
            {
                var clientToPost = new
                {
                    Type = "Internal"
                };

                var response = client.PostAsJsonAsync("api/tasks", clientToPost).Result;
                
            }
        }
    }
}
