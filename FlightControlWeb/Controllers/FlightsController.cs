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
    public class FlightsController : ControllerBase
    {
        private readonly IObjectsManager<Flight> flightsManager = new FlightsManager();
        private FlightsManager manager;

        // GET: api/Flights
        [HttpGet]
        public IEnumerable<Flight> Get(string relativeTo)
        {
            string request = Request.QueryString.Value;
            bool notOnlyInternal = request.Contains("sync_all");
            IEnumerable<Flight> allActiveFlights = manager.GetActiveFlights(relativeTo);
            if (notOnlyInternal)
            {
                return allActiveFlights;
            }
            List<Flight> internalFlights = new List<Flight>();
            foreach (Flight flight in allActiveFlights)
            {
                if (!flight.External)
                {
                    internalFlights.Add(flight);
                }
            }
            return internalFlights;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            flightsManager.DeleteObject(id);
        }
    }
}