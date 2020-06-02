using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;

namespace FlightControlWeb.Data
{
    public class Segment
    {
        string id;
        double longitude;
        double latitude;
        double timespanSeconds;


        /**
         * Constructor
         **/
        public Segment() { }


        /**
         * Constructor with given parameters
         **/
        public Segment(string flightId, double lon, double lat, double time)
        {
            Id = flightId;
            Longitude = lon;
            Latitude = lat;
            TimespanSeconds = time;
        }


        [Key]
        [System.Text.Json.Serialization.JsonIgnore]
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }


        [JsonProperty("longitude")]
        [JsonPropertyName("longitude")]
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


        [JsonProperty("latitude")]
        [JsonPropertyName("latitude")]
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


        [JsonProperty("timespan_seconds")]
        [JsonPropertyName("timespan_seconds")]
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