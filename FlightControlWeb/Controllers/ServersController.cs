using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly FlightsDbContext context;


        /**
         * Constructor
         **/
        public ServersController(FlightsDbContext c)
        {
            context = c;
        }


        // GET: api/Servers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Server>>> GetServers()
        {
           return await context.Servers.ToListAsync();
        }


        // POST: api/Servers
        [HttpPost]
        public async Task<ActionResult<Server>> PostServer(Server server)
        {
            context.Servers.Add(server);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (context.Servers.Any(e => e.ServerId == server.ServerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return await context.Servers.FindAsync(server.ServerId);
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Server>> DeleteServer(string id)
        {
            var server = await context.Servers.FindAsync(id);
            if (server == null)
            {
                return NotFound();
            }
            context.Servers.Remove(server);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return server;
        }
    }
}