using FlightControlWeb.Data;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightControlWeb.Models
{
    public class FlightPlan
    {
        string id;
        int passengers;
        string companyName;
        InitialLocation initialLocation;
        // the all path
        List<Segment> segments;


        /**
         * Constructor
         **/
        public FlightPlan() { }


        /**
         * Constructor with given parameters
         **/
        public FlightPlan(int passengersNum, string company, InitialLocation init,
            List<Segment> seg)
        {
            Id = FlightId.GetRandomId();
            Passengers = passengersNum;
            CompanyName = company;
            InitialLocation = init;
            // the all path
            Segments = seg;
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [System.Text.Json.Serialization.JsonIgnore]
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
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


        [JsonProperty("initial_location")]
        [JsonPropertyName("initial_location")]
        public InitialLocation InitialLocation
        {
            get { return this.initialLocation; }
            set { this.initialLocation = value; }
        }


        [JsonProperty("segments")]
        [JsonPropertyName("segments")]
        public List<Segment> Segments
        {
            get { return this.segments; }
            set { this.segments = value; }
        }
    }
}