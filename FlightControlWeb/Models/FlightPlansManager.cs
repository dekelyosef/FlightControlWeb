using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using FlightControlWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;


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
            }
            else if (segment.Latitude < -90 || segment.Latitude > 90)
            {
                return false;
            }
            else if (segment.TimespanSeconds < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /**
         * Get all active external flights
         **/
        public static FlightPlan GetExternalFlightPlan(string path)
        {
            // get the json string
            string jsonStr = GetFlightJson(String.Format(path));
            if (jsonStr == null)
            {
                return default;
            }
            return GetFlightsFromExternalServer(jsonStr);
        }


        public static Segment convertSegment(JToken segment)
        {
            return segment.ToObject<Segment>();
        }

        /**
         * Get all active external flights json
         **/
        public static FlightPlan GetFlightsFromExternalServer(string jsonStr)
        {
            // handle differences
            JObject json = JObject.Parse(jsonStr);
            string name = (string)json["company_name"];
            int passengers = (int)json["passengers"];
            Array segs = json["segments"].ToArray();

            List<Segment> list = new List<Segment>();

            foreach(JToken j in segs)
            {
                list.Add(convertSegment(j));
            }

            JToken jLoc = json["initial_location"];
            double longi = (double)jLoc["longitude"];
            double lati = (double)jLoc["latitude"];
            DateTime time = (DateTime)jLoc["date_time"];
            //DateTime dt = Convert.ToDateTime(time);

            InitialLocation location = new InitialLocation();
            location.Longitude = longi;
            location.Latitude = lati;
            location.DateTime = time;


            FlightPlan p = new FlightPlan(passengers, name, location, list);

            return p;
        }


        /**
         * Get json from given URL path
         **/
        public static string GetFlightJson(string serverPath)
        {
            HttpWebRequest r = (HttpWebRequest)WebRequest.Create(serverPath);
            using (HttpWebResponse re = (HttpWebResponse)r.GetResponse())
            {
                using (Stream s = re.GetResponseStream())
                {
                    using (StreamReader read = new StreamReader(s))
                    {
                        return read.ReadToEnd();
                    }
                }

            }



            //WebRequest request = WebRequest.Create((String.Format(serverPath)));
            //request.Method = "GET";
            //// gets a response from the external server
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //// creates a stream object from external API response
            //string jsonStr = "";
            //using (Stream stream = response.GetResponseStream())
            //{
            //    StreamReader reader = new StreamReader(stream);
            //    jsonStr = reader.ReadToEnd();
            //    reader.Close();
            //}
            //if (jsonStr == "")
            //{
            //    return null;
            //}
            //return jsonStr;
        }







        /**
         * Get external flightPlans
         **/
        /*        public static FlightPlan GetExternalFlightPlans(string url)
                {
                    WebRequest request = WebRequest.Create((String.Format(url)));
                    request.Method = "GET";
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    // get data
                    string jsonStr = "";
                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream);
                        jsonStr = reader.ReadToEnd();
                        reader.Close();
                    }
                    var deserialSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        }
                    };
                    if (jsonStr == "")
                    {
                        return default;
                    }
                    var flightPlan = JsonConvert.DeserializeObject<FlightPlan>(jsonStr, deserialSettings);
                    //return data;
                   // T flightPlan = JsonConvert.DeserializeObject<T>(data);
                    return flightPlan;
                }*/
    }
}