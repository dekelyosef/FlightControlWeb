using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlWeb.Data
{
    public class Segment
    {
        public int Longitude { get; set; }
        public int Latitude { get; set; }
        public string TimespanSeconds { get; set; }
    }
}