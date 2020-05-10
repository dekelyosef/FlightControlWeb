using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlWeb.Models
{
    public class FlightsManager : IObjectsManager<Flight>
    {
        private static readonly List<Flight> flightsList = new List<Flight>()
        {
            new Flight { Id = "ABCD1234", CompanyName = "ElAl", Passengers = 230, Latitude = 34.12,
                Longitude = 37.244, DateTime = "2020-12-26T23:56:21Z", External = false },
            new Flight { Id = "EFGH6789", CompanyName = "SwissAir", Passengers = 216, Latitude = 31.12,
                Longitude = 33.244, DateTime = "2020-12-27T23:00:21Z", External = false }
        };

        public void AddObject(Flight flight)
        {
            flightsList.Add(flight);
        }

        public void DeleteObject(string id)
        {
            Flight flight = flightsList.Where(x => x.Id == id).FirstOrDefault();
            if (flight == null)
            {
                throw new Exception("Flight not found");
            }
            flightsList.Remove(flight);
        }

        public IEnumerable<Flight> GetAllObjects()
        {
            return flightsList;
        }

        public Flight GetObject(string id)
        {
            Flight flight = flightsList.Where(x => x.Id == id).FirstOrDefault();
            if (flight == null)
            {
                throw new Exception("Flight not found");
            }
            return flight;
        }

        public void UpdateObject(Flight newFlight)
        {
            Flight flight = flightsList.Where(x => x.Id == newFlight.Id).FirstOrDefault();
            if (flight == null)
            {
                throw new Exception("Flight not found");
            }
            flight.CompanyName = newFlight.CompanyName;
            flight.Passengers = newFlight.Passengers;
            flight.Latitude = newFlight.Latitude;
            flight.Longitude = newFlight.Longitude;
            flight.DateTime = newFlight.DateTime;
            flight.External = newFlight.External;
        }
    }
}