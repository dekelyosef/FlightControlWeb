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
        private readonly IObjectsManager<FlightPlan> flightPlansManager =
            new FlightPlansManager();

        // GET: api/FlightPlan
        [HttpGet]
        public IEnumerable<FlightPlan> Get()
        {
            return flightPlansManager.GetAllObjects();
        }

        // GET: api/FlightPlan/5
        //[HttpGet("{id}", Name = "Get")]
        public FlightPlan Get(string id)
        {
            return flightPlansManager.GetObject(id);
        }

        // POST: api/FlightPlan
        [HttpPost]
        public void Post(FlightPlan flightPlan)
        {
            flightPlansManager.AddObject(flightPlan);
        }

        // DELETE: api/FlightPlan/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            flightPlansManager.DeleteObject(id);
        }
    }
}
