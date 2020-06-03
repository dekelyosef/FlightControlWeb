using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using FlightControlWeb.Data;
using Newtonsoft.Json;
using System.Data;
using System.Text.Json.Serialization;

namespace FlightControlWeb
{
    public class FlightsDbContext : DbContext
    {
        public FlightsDbContext(DbContextOptions<FlightsDbContext> options) : base(options)
        {
        }

        public DbSet<FlightPlan> FlightPlans { get; set; }
        public DbSet<Server> Servers { get; set; }

        // list of flightPlan id with the external server that the flight came from
        public static Dictionary<string, Server> externalServersFlights =
            new Dictionary<string, Server>();
    }
}