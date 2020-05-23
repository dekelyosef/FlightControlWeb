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