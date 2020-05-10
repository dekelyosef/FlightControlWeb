using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlightControlWeb.Data;

namespace FlightControlWeb.Models
{
    public class FlightPlansManager : IFlightsManager<FlightPlan>
    {
        private static readonly List<FlightPlan> flightPlansList = new List<FlightPlan>()
        {
            new FlightPlan { }
        };

        public void AddFlight(FlightPlan flightPlan)
        {
            flightPlan.Id = Id.GetRandomId();
            flightPlansList.Add(flightPlan);
        }

        public void DeleteFlight(string id)
        {
            FlightPlan flightPlan = flightPlansList.Where(x => x.Id == id).FirstOrDefault();
            if (flightPlan == null)
            {
                throw new Exception("FlightPlan not found");
            }
            flightPlansList.Remove(flightPlan);
        }

        public IEnumerable<FlightPlan> GetAllFlights()
        {
            return flightPlansList;
        }

        public FlightPlan GetFlight(string id)
        {
            FlightPlan flightPlan = flightPlansList.Where(x => x.Id == id).FirstOrDefault();
            if (flightPlan == null)
            {
                throw new Exception("FlightPlan not found");
            }
            return flightPlan;
        }

        public void UpdateFlight(FlightPlan flightPlan)
        {
            FlightPlan newFlight = flightPlansList.Where(x => x.Id == flightPlan.Id).FirstOrDefault();
            if (newFlight == null)
            {
                throw new Exception("FlightPlan not found");
            }
            newFlight.Passengers = flightPlan.Passengers;
            newFlight.CompanyName = flightPlan.CompanyName;
            // initial location
            newFlight.Latitude = flightPlan.Latitude;
            newFlight.Longitude = flightPlan.Longitude;
            newFlight.DateTime = flightPlan.DateTime;
            // path
            newFlight.Segments = flightPlan.Segments;
        }
    }
}