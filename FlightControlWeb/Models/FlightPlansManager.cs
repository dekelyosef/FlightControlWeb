using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Linq;
using System.Web;
using FlightControlWeb.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace FlightControlWeb.Models
{
    public class FlightPlansManager : ControllerBase
    {
        /**
         * Constructor
         **/
        public FlightPlansManager() { }


        /**
         * Checks if the flightPlan with valid parameters
         **/
        public static bool IsValid(FlightPlan flightPlan)
        {
            if (flightPlan.CompanyName == null || flightPlan.Passengers <= -1)
            {
                return false;
            }
            if (flightPlan.InitialLocation == null || !IsValidLocation(flightPlan.InitialLocation))
            {
                return false;
            }
            if (flightPlan.Segments == null || flightPlan.Segments.Count == 0)
            {
                return false;
            }
            if (!IsValidSegment(flightPlan.Segments))
            {
                return false;
            }
            return true;
        }


        /**
         * Checks if the flightPlan location with valid parameters
         **/
        public static bool IsValidLocation(InitialLocation location)
        {
            if (location.Longitude < -180 || location.Longitude > 180)
            {
                return false;
            }
            else if (location.Latitude < -90 || location.Latitude > 90)
            {
                return false;
            }
            else if (location.DateTime == DateTime.MinValue)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /**
         * Checks if the flightPlan Segments are valid
         **/
        public static bool IsValidSegment(List<Segment> segments)
        {
            foreach (Segment segment in segments)
            {
                if (!InRange(segment))
                {
                    return false;
                }
            }
            return true;
        }


        /**
         * Checks if the flightPlan segments are in range
         **/
        public static bool InRange(Segment segment)
        {
            if (segment.Longitude < -180 || segment.Longitude > 180)
            {
                return false;
            } else if (segment.Latitude < -90 || segment.Latitude > 90)
            {
                return false;
            } else if (segment.TimespanSeconds < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /**
         * Get external flightPlans
         **/
        public static T GetExternalFlightPlans<T>(string url)
        {
            string data = "";
            WebRequest request = WebRequest.Create(String.Format(url));
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // get data
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream);
                data = reader.ReadToEnd();
                reader.Close();
            }
            if (data == "")
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}