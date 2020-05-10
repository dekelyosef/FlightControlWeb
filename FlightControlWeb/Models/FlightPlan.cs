using FlightControlWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlWeb.Models
{
    public class FlightPlan
    {
        public string Id { get; set; }
        public int Passengers { get; set; }
        public string CompanyName { get; set; }
        // initial location
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string DateTime { get; set; }
        // the all path
        public List<Segment> Segments { get; set; }
    }
}