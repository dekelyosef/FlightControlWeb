﻿using FlightControlWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace FlightControlWeb.Models
{
    public class FlightsManager : ControllerBase
    {
        /**
         * Constructor
         **/
        public FlightsManager() { }


        /**
         * Set server flightPlans
         **/
        public static List<Flight> SetFlightPlans(List<Flight> tmpExternalFlights, Server server)
        {
            List<Flight> externalFlights = new List<Flight>();

            foreach (Flight flight in tmpExternalFlights)
            {
                // update flight as external
                flight.IsExternal = true;
                FlightsDbContext.externalServersFlights[flight.Id] = server;
                // add to all external active flights list
                externalFlights.Add(flight);
            }
            return externalFlights;
        }


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
                // checks if the flight happening now acording to the given time
                if (IsActive(flightPlan, relativeTo))
                {
                    // update the new location according to the time that pass
                    Tuple<double, double> newLocation = GetFlightLocation(flightPlan, relativeTo);
                    if (newLocation == null)
                    {
                        continue;
                    }
                    Flight flight = new Flight(flightPlan);
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
        public static Tuple<double, double>
            GetFlightLocation(FlightPlan flightPlan, DateTime currentTime)
        {
            // calculates the seconds until arriving to current segment
            int index = CurrentSegmentIndex(flightPlan, currentTime);
            if (index == -1)
            {
                return null;
            }
            // get the initial location time
            DateTime timeFromTakeOff = flightPlan.InitialLocation.DateTime;
            for (int iter = 0; iter < index; iter++)
            {
                // update the initial location time
                timeFromTakeOff = timeFromTakeOff.AddSeconds
                    (flightPlan.Segments.ElementAt(index).TimespanSeconds);
            }

            return GetLocation(flightPlan, index, currentTime, timeFromTakeOff);
        }


        /**
         * Get the current segment according to the current flight time
         **/
        public static int CurrentSegmentIndex(FlightPlan flightPlan, DateTime currentTime)
        {
            List<Segment> segmentsList = flightPlan.Segments;
            DateTime takeOffTime = flightPlan.InitialLocation.DateTime;
            int index = -1;
            while (takeOffTime <= currentTime && (index+1) < segmentsList.Count)
            {
                index++;
                double segmentTimespan = segmentsList.ElementAt(index).TimespanSeconds;
                takeOffTime = takeOffTime.AddSeconds(segmentTimespan);
            }
            // if the flight didn't start or already finished
            if (takeOffTime < currentTime)
            {
                index = -1;
            }
            if (flightPlan.InitialLocation.DateTime > currentTime)
            {
                index = -1;
            }
            return index;
        }


        /**
         * Get the current flight location using linear interpolation
         **/
        public static Tuple<double, double> GetLocation(FlightPlan flightPlan,
            int index, DateTime currentTime, DateTime timeFromTakeOff)
        {
            Segment currentSegment;
            if (index == 0)
            {
                // initialize the first segment
                currentSegment = new Segment(flightPlan.Id, 
                    flightPlan.InitialLocation.Longitude, flightPlan.InitialLocation.Latitude,0);
            }
            else
            {
                currentSegment = flightPlan.Segments.ElementAt(index - 1);
            }
            // get the last position flight and the new position
            double startLon = currentSegment.Longitude;
            double endLon = flightPlan.Segments.ElementAt(index).Longitude;
            double startLat = currentSegment.Latitude;
            double endLat = flightPlan.Segments.ElementAt(index).Latitude;

            double longitude = LinearInterpolation(startLon, endLon, timeFromTakeOff,
                flightPlan.Segments.ElementAt(index).TimespanSeconds, currentTime);

            double latitude = LinearInterpolation(startLat, endLat, timeFromTakeOff,
                flightPlan.Segments.ElementAt(index).TimespanSeconds, currentTime);

            // return the updated location
            return Tuple.Create(longitude, latitude);
        }


        /**
         * Linear interpolation
         **/
        public static double LinearInterpolation(double start, double end,
            DateTime timeFromTakeOff, double timespan, DateTime currentTime)
        {
            double distance = end - start;
            double currentSeconds = currentTime.Subtract(timeFromTakeOff).TotalSeconds;
            // find the ratio
            double timePass = currentSeconds / timespan;
            return (start + (timePass * distance));
        }


        /**
         * Get all active external flights
         **/
        public static List<Flight> GetExternalFlights(DateTime time, string path)
        {
            // get the active flights from the current connected server
            string request =
                path + "/api/Flights/?relative_to=" + time.ToString("yyyy-MM-ddTHH:mm:ssZ");
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
            // new flights list
            List<Flight> flightsList = new List<Flight>();
            // get flights array from string
            JArray flightsArray = JArray.Parse(jsonStr);
            int index = 0;
            for (; index < flightsArray.Count; index++)
            {
                // set Flight plan
                flightsList.Add(flightsArray[index].ToObject<Flight>());
            }

            return flightsList;
        }


        /**
         * Get json from given URL path
         **/
        public static string GetFlightJson(string serverPath)
        {
            WebRequest request = WebRequest.Create((String.Format(serverPath)));
            request.Method = "GET";
            // gets a response from the external server
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // creates a stream object from external API response
            string jsonStr = "";
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