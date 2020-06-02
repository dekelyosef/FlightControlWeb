using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
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
        public InitialLocation(string id, double lon, double lat, DateTime date)
        {
            Id = id;
            Longitude = lon;
            Latitude = lat;
            DateTime = date;
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



        [Newtonsoft.Json.JsonConverter(typeof(DateFormatConverter), "yyyy-MM-ddTHH:mm:ssZ")]
        [JsonProperty("date_time")]
        [JsonPropertyName("date_time")]
        public DateTime DateTime
        {
            get { return this.dateTime; }
            set { this.dateTime = value; }
        }
    }
}