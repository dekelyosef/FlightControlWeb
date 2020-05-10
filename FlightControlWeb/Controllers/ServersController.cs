using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {

        private readonly IObjectsManager<Server> serversManager = new ServersManager();

        // GET: api/Servers
        [HttpGet]
        public IEnumerable<Server> Get()
        {
            return serversManager.GetAllObjects();
        }

        // GET: api/Servers/5
        [HttpGet("{id}", Name = "Get")]
        public Server Get(string id)
        {
            return serversManager.GetObject(id);
        }

        // POST: api/Servers
        [HttpPost]
        //public void Post([FromBody] string value)
        public void Post(Server server)
        {
            serversManager.AddObject(server);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            serversManager.DeleteObject(id);
        }
    }
}
