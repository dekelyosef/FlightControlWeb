using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlWeb.Data
{
    public class Segment
    {
        double longitude;
        double latitude;
        double timespanSeconds;

        // counstructor
        public Segment(double lon, double lat, double time)
        {
            this.longitude = lon;
            this.latitude = lat;
            this.timespanSeconds = time;
        }

        public double Longitude
        {
            get { return this.longitude; }
            set
            {
                if (this.longitude != value)
                {
                    this.longitude = value;
                }
            }
        }

        public double Latitude
        {
            get { return this.latitude; }
            set
            {
                if (this.latitude != value)
                {
                    this.latitude = value;
                }
            }
        }

        public double TimespanSeconds
        {
            get { return this.timespanSeconds; }
            set
            {
                if (this.timespanSeconds != value)
                {
                    this.timespanSeconds = value;
                }
            }
        }
    }
}