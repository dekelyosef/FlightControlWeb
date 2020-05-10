﻿using System;
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
        private readonly IFlightsManager<Flight> flightsManager = new FlightsManager();

        // GET: api/Flights
        [HttpGet]
        public IEnumerable<Flight> Get()
        {
            return flightsManager.GetAllFlights();
        }

        // GET: api/Flights/5
       // [HttpGet("{id}", Name = "Get")]
        public Flight Get(string id)
        {
            return flightsManager.GetFlight(id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            flightsManager.DeleteFlight(id);
        }
    }
}