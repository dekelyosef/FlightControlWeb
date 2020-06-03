using FlightControlWeb.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;

namespace FlightControlWeb.Models
{
    public class Flight
    {
        string id;
        double longitude;
        double latitude;
        int passengers;
        string companyName;
        DateTime dateTime;
        bool isExternal;


        /**
         * Constructor
         **/
        public Flight() { }
        

        /**
         * Constructor with given flightPlan
         **/
        public Flight(FlightPlan flightPlan)
        {
            Id = flightPlan.Id;
            Longitude = flightPlan.InitialLocation.Longitude;
            Latitude = flightPlan.InitialLocation.Latitude;
            Passengers = flightPlan.Passengers;
            CompanyName = flightPlan.CompanyName;
            DateTime = flightPlan.InitialLocation.DateTime;
            IsExternal = false;
        }


        /**
         * Constructor with given parameters
         **/
        public Flight(double lon, double lat, int passengersNum, string company,
            DateTime time, bool external)
        {
            Id = FlightId.GetRandomId();
            Longitude = lon;
            Latitude = lat;
            Passengers = passengersNum;
            CompanyName = company;
            DateTime = time;
            IsExternal = external;
        }


        /**
         * Constructor with given parameters
         **/
        public Flight(string flightId, double lon, double lat, int passengersNum, string company,
            DateTime time, bool external)
        {
            Id = flightId;
            Longitude = lon;
            Latitude = lat;
            Passengers = passengersNum;
            CompanyName = company;
            DateTime = time;
            IsExternal = external;
        }

        [JsonProperty("flight_id")]
        [JsonPropertyName("flight_id")]
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
            set { this.longitude = value; }
        }

        [JsonProperty("latitude")]
        [JsonPropertyName("latitude")]
        public double Latitude
        {
            get { return this.latitude; }
            set { this.latitude = value; }
        }

        [JsonProperty("passengers")]
        [JsonPropertyName("passengers")]
        public int Passengers
        {
            get { return this.passengers; }
            set { this.passengers = value; }
         
        }

        [JsonProperty("company_name")]
        [JsonPropertyName("company_name")]
        public string CompanyName
        {
            get { return this.companyName; }
            set { this.companyName = value; }
        }

        [JsonProperty("date_time")]
        [JsonPropertyName("date_time")]
        public DateTime DateTime
        {
            get { return this.dateTime; }
            set { this.dateTime = value; }
        }

        [JsonProperty("is_external")]
        [JsonPropertyName("is_external")]
        public bool IsExternal
        {
            get { return this.isExternal; }
            set { this.isExternal = value; }
        }
    }
}