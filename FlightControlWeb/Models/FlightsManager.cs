using FlightControlWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace FlightControlWeb.Models
{
    public class FlightsManager : ControllerBase
    {
        /**
         * Constructor
         **/
        public FlightsManager() { }


        /**
         * Get all present flights
         **/
        public static List<Flight> GetPresentFlights(List<FlightPlan> flightPlansList,
            DateTime relativeTo)
        {
            // new list
            List<Flight> presentFlights = new List<Flight>();
            foreach (FlightPlan flightPlan in flightPlansList)
            {
                // convert the given current time to UTC
                DateTime time = TimeZoneInfo.ConvertTimeToUtc(relativeTo);
                // checks if the flight happening now acording to the given time
                if (IsActive(flightPlan, time))
                {
                    Flight flight = new Flight(flightPlan);
                    // update the new location according to the time that pass
                    Tuple<double, double> newLocation = GetFlightLocation(flightPlan, time);
                    flight.Longitude = newLocation.Item1;
                    flight.Latitude = newLocation.Item2;
                    // add the flight to the present active fligts list
                    presentFlights.Add(flight);
                }
            }
            return presentFlights;
        }


        /**
         * Checks if flight is happening at the given time
         **/
        public static bool IsActive(FlightPlan flightPlan, DateTime time)
        {
            // if the departure didn't happen yet
            if (flightPlan.InitialLocation.DateTime.Ticks > time.Ticks)
            {
                return false;
            }
            else
            {
                double segmentsTime = 0;
                // calculate all the segments timespan of the flight
                foreach (Segment segment in flightPlan.Segments)
                {
                    segmentsTime += segment.TimespanSeconds;
                }
                // calculate the time of landing
                DateTime totalFlightTime =
                    flightPlan.InitialLocation.DateTime.AddSeconds(segmentsTime);
                // return true if the flight hasn't landed yet
                if (DateTime.Compare(totalFlightTime, time) > 0)
                {
                    return true;
                }
                return false;
            }
        }


        /**
         * Get updated flight location
         **/
        public static Tuple<double, double> GetFlightLocation(FlightPlan flightPlan, DateTime time)
        {
            double totalTime = (time - flightPlan.InitialLocation.DateTime).TotalSeconds;
            // calculates the seconds until arriving to current segment
            int index = CurrentSegmentIndex(flightPlan.Segments, totalTime);
            // get the initial location time
            DateTime timeFromTakeOff = flightPlan.InitialLocation.DateTime;
            for (int iter = 0; iter < index; iter++)
            {
                // update the initial location time
                timeFromTakeOff =
                    timeFromTakeOff.AddSeconds(flightPlan.Segments[iter].TimespanSeconds);
            }

            double leftTime = (time - timeFromTakeOff).TotalSeconds;
            return GetLocation(flightPlan, index, leftTime);
        }


        /**
         * Get the current segment according to the current flight time
         **/
        public static int CurrentSegmentIndex(List<Segment> segmentsList, double time)
        {
            int index = 0;
            foreach (Segment segment in segmentsList)
            {
                // checks if flight time is greater than the segment's timespan seconds
                if (time > segment.TimespanSeconds)
                {
                    // reduce segment's timespan seconds from flight time
                    time -= segment.TimespanSeconds;
                }
                else
                {
                    return index;
                }
                index++;
            }
            // in case of error return -1
            return -1;
        }


        /**
         * Get the current flight location using linear interpolation
         **/
        public static Tuple<double, double> GetLocation(FlightPlan flightPlan,
            int index, double leftTime)
        {
            Segment currentSegment;
            if (index == 0)
            {
                // initialize the first segment
                currentSegment = new Segment(flightPlan.Id, flightPlan.InitialLocation.Latitude,
                    flightPlan.InitialLocation.Longitude, 0);
            }
            else
            {
                currentSegment = flightPlan.Segments[index - 1];
            }
            Segment nextSegment = flightPlan.Segments[index];
            double partOfSegment = leftTime / nextSegment.TimespanSeconds;
            double x = LinearInterpolation(currentSegment.Longitude,
                currentSegment.Latitude, partOfSegment);
            double y = LinearInterpolation(nextSegment.Longitude,
                nextSegment.Latitude, partOfSegment);
            // return the updated location
            return Tuple.Create(x, y);
        }


        /**
         * Linear interpolation
         **/
        public static double LinearInterpolation(double var0, double var1, double partOfSegment)
        {
            return ((var1 - var0) * partOfSegment) + var0;
        }


        /**
         * Get all active external flights
         **/
        public static List<Flight> GetExternalFlights(DateTime time, string path)
        {
            // get the active flights from the current connected server
            string request =
                path + "api/Flights/?relative_to=" + time.ToString("yyyy-MM-ddTHH:mm:ssZ");
            // get the json string
            string jsonStr = GetFlightJson(String.Format(request));
            if (jsonStr == null)
            {
                return default;
            }
            return GetFlightsFromExternalServer(jsonStr);
        }


        /**
         * Get all active external flights json
         **/
        public static List<Flight> GetFlightsFromExternalServer(string jsonStr)
        {
            // handle differences
            JsonSerializerSettings dezerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
            return JsonConvert.DeserializeObject<List<Flight>>(jsonStr, dezerializerSettings);
        }


        /**
         * Get json from given URL path
         **/
        public static string GetFlightJson(string serverPath)
        {
            string jsonStr = "";
            WebRequest request = WebRequest.Create(String.Format(serverPath));
            request.Method = "GET";
            // gets a response from the external server
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // creates a stream object from external API response
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream);
                jsonStr = reader.ReadToEnd();
                reader.Close();
            }
            if (jsonStr == "")
            {
                return null;
            }
            return jsonStr;
        }
    }
}