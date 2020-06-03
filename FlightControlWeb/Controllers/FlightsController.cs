using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly FlightsDbContext context;


        /**
         * Constructor
         **/
        public FlightsController(FlightsDbContext c)
        {
            context = c;
        }


        // GET: api/Flights
        public async Task<ActionResult<List<Flight>>>
            GetFlights([FromQuery(Name = "relative_to")] DateTime relativeTo)
        {
            // convert the given current time to UTC
            DateTime time = TimeZoneInfo.ConvertTimeToUtc(relativeTo);
            FlightsDbContext.externalServersFlights.Clear();
            context.SaveChanges();
            List<Flight> externalFlights = new List<Flight>();
            List<FlightPlan> flightPlans = await context.FlightPlans.Include(x => x.Segments)
                .Include(x => x.InitialLocation).ToListAsync();
            // new list for all the present flights
            List<Flight> presentFlights = FlightsManager.GetPresentFlights(flightPlans, time);
            // if only internal flights are happening now
            if (!Request.QueryString.Value.Contains("sync_all"))
            {
                if (!presentFlights.Any())
                {
                    // if the list is empty
                    return NotFound();
                }
                return presentFlights;
            }
            // otherwise, get the external flights
            List<string> paths = context.Servers.Select(x => x.ServerURL).ToList();
            List<Flight> tmpFlights = new List<Flight>();
            foreach (string path in paths)
            {
                tmpFlights.AddRange(FlightsManager.GetExternalFlights(time, path));
                Server server = await context.Servers.Where
                    (x => String.Equals(path, x.ServerURL)).FirstOrDefaultAsync();
                externalFlights.AddRange(FlightsManager.SetFlightPlans(tmpFlights, server));
                // clear the tmp list of specific server
                tmpFlights.Clear();
            }
            presentFlights.AddRange(externalFlights);
            // return a list of all the relevant flights according to the relative time
            return presentFlights;
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FlightPlan>> DeleteFlight(string id)
        {
            FlightPlan flightPlan = await context.FlightPlans.FindAsync(id);
            if (flightPlan == null)
            {
                return NotFound();
            }
            context.FlightPlans.Remove(flightPlan);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return flightPlan;
        }
    }
}