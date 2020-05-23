using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlWeb.Data
{
    public class InitialLocation
    {
        string id;
        double longitude;
        double latitude;
        DateTime dateTime;


        /**
         * Constructor
         **/
        public InitialLocation() { }


        /**
         * Constructor with given parameters
         **/
        public InitialLocation(double lon, double lat, DateTime date)
        {
            Longitude = lon;
            Latitude = lat;
            DateTime = date;
        }


        [System.Text.Json.Serialization.JsonIgnore]
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }


        [JsonProperty("longitude")]
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


        [JsonProperty("date_time")]
        public DateTime DateTime
        {
            get { return this.dateTime; }
            set { this.dateTime = value; }
        }
    }
}