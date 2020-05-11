using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlightControlWeb.Data;

namespace FlightControlWeb.Models
{
    public class FlightPlansManager : IObjectsManager<FlightPlan>
    {
        private static readonly List<FlightPlan> flightPlansList = new List<FlightPlan>()
        {
            new FlightPlan { }
        };

        public void AddObject(FlightPlan flightPlan)
        {
            flightPlan.Id = FlightId.GetRandomId();
            flightPlansList.Add(flightPlan);
        }

        public void DeleteObject(string id)
        {
            FlightPlan flightPlan = flightPlansList.Where(x => x.Id == id).FirstOrDefault();
            if (flightPlan == null)
            {
                throw new Exception("FlightPlan not found");
            }
            flightPlansList.Remove(flightPlan);
        }

        public IEnumerable<FlightPlan> GetAllObjects()
        {
            return flightPlansList;
        }

        public FlightPlan GetObject(string id)
        {
            FlightPlan flightPlan = flightPlansList.Where(x => x.Id == id).FirstOrDefault();
            if (flightPlan == null)
            {
                throw new Exception("FlightPlan not found");
            }
            return flightPlan;
        }

        public void UpdateObject(FlightPlan newFlightPlan)
        {
            FlightPlan flightPlan = flightPlansList.Where(x => x.Id == newFlightPlan.Id).FirstOrDefault();
            if (flightPlan == null)
            {
                throw new Exception("FlightPlan not found");
            }
            flightPlan.Passengers = newFlightPlan.Passengers;
            flightPlan.CompanyName = newFlightPlan.CompanyName;
            // initial location
            flightPlan.Latitude = newFlightPlan.Latitude;
            flightPlan.Longitude = newFlightPlan.Longitude;
            flightPlan.DateTime = newFlightPlan.DateTime;
            // path
            flightPlan.Segments = newFlightPlan.Segments;
        }
    }
}