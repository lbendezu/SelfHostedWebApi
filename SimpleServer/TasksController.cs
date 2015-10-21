using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleServer
{
    using System.Web.Http;

    [Authorize]
    public class TasksController : ApiController
    {
        public IHttpActionResult Get(string name)
        {
            return Ok("Hola " + name);
        }

        public IHttpActionResult Post(Client client) {
            if (client.Type == "External")
                return BadRequest("Only Internal Clients");
            if (client.Type != "Internal") return BadRequest("Invalid Arguments");

            return Ok();
        }
    }
}
