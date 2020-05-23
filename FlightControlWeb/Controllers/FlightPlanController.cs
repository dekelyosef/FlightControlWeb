﻿using System;
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
        public async Task<ActionResult<IEnumerable<FlightPlan>>> GetFlightPlans()
        {
            return await context.FlightPlans.Include(x => x.InitialLocation)
                .Include(x => x.Segments).ToListAsync();
        }


        // GET: api/FlightPlan/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<FlightPlan>> GetFlightPlan(string id)
        {
            List<Segment> segments = new List<Segment>();
            var flightPlan = await context.FlightPlans.Include(x => x.InitialLocation)
                .Include(x => x.Segments).Where(x => String.Equals(id, x.Id)).FirstOrDefaultAsync();
            if (flightPlan != null)
            {
                return flightPlan;
            }
            try
            {
                var server = context.externalServersFlights[id];
                if (server == null)
                {
                    return NotFound();
                }
                string path = server.ServerURL + "api/FlightPlan/" + id;
                flightPlan = FlightPlansManager.GetExternalFlightPlans<FlightPlan>(path);
                if (flightPlan == null)
                {
                    return NotFound();
                }
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return flightPlan;
        }


        // POST: api/FlightPlan
        [HttpPost]
        public async Task<ActionResult<FlightPlan>> PostFlightPlan(FlightPlan flightPlan)
        {
            flightPlan.Id = Data.FlightId.GetRandomId();
            flightPlan.InitialLocation.Id = flightPlan.Id;
            foreach (var segment in flightPlan.Segments)
            {
                segment.Id = flightPlan.Id;
            }
            context.FlightPlans.Add(flightPlan);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return await context.FlightPlans.FindAsync(flightPlan.Id);
        }
    }
}
