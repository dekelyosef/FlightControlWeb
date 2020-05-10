using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightPlanController : ControllerBase
    {
        private readonly IFlightsManager<FlightPlan> flightPlansManager =
            new FlightPlansManager();

        // GET: api/FlightPlan
        [HttpGet]
        public IEnumerable<FlightPlan> Get()
        {
            return flightPlansManager.GetAllFlights();
        }

        // GET: api/FlightPlan/5
        //[HttpGet("{id}", Name = "Get")]
        public FlightPlan Get(string id)
        {
            return flightPlansManager.GetFlight(id);
        }

        // POST: api/FlightPlan
        [HttpPost]
        public void Post(FlightPlan flightPlan)
        {
            // flightPlansManager.GetFlight(flightPlan);
            flightPlansManager.AddFlight(flightPlan);
        }

        // DELETE: api/FlightPlan/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            flightPlansManager.DeleteFlight(id);
        }
    }
}
