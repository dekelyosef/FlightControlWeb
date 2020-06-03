using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using FlightControlWeb.Data;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightPlanController : ControllerBase
    {
        private readonly FlightsDbContext context;


        /**
         * Constructor
         **/
        public FlightPlanController(FlightsDbContext c)
        {
            this.context = c;
        }


        // GET: api/FlightPlan
        [HttpGet]
        public async Task<ActionResult<FlightPlan>> GetFlightPlanWithoudId()
        {
            return await Task.FromResult(BadRequest("Please set Id"));
        }


        // GET: api/FlightPlan/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<FlightPlan>> GetFlightPlan(string id)
        {
            List<Segment> segments = new List<Segment>();
            var flightPlan =
                await context.FlightPlans.Include(x => x.InitialLocation).Include(x => x.Segments)
                .Where(x => String.Equals(id, x.Id)).FirstOrDefaultAsync();
            if (flightPlan != null)
            {
                return Ok(flightPlan);
            }
            try
            {
                var server = FlightsDbContext.externalServersFlights[id];
                if (server == null)
                {
                    return NotFound();
                }
                string path = server.ServerURL + "/api/FlightPlan/" + id;
                //flightPlan = FlightPlansManager.GetExternalFlightPlans<FlightPlan>(path);
                //flightPlan = FlightPlansManager.GetExternalFlightPlans(path);
                flightPlan = FlightPlansManager.GetExternalFlightPlan(path);
                if (flightPlan == null)
                {
                    return NotFound();
                }
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return Ok(flightPlan);
        }


        // POST: api/FlightPlan
        [HttpPost]
        public async Task<ActionResult<FlightPlan>> PostFlightPlan(FlightPlan flightPlan)
        {
            if (!FlightPlansManager.IsValid(flightPlan))
            {
                return BadRequest("Flight plan isn't valid");
            }
            context.FlightPlans.Add(flightPlan);
            flightPlan.Id = Data.FlightId.GetRandomId();
            context.FlightPlans.Add(flightPlan);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return CreatedAtAction("GetFlightPlan", new { id = flightPlan.Id }, flightPlan);
        }
    }
}
