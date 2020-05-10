using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlWeb.Models
{
    public class Flight
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Passengers { get; set; }
        public string DateTime { get; set; }
        public bool External { get; set; }
    }
}