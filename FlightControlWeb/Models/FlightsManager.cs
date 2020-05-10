using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlWeb.Models
{
    public class FlightsManager : IFlightsManager<Flight>
    {
        private static readonly List<Flight> flightsList = new List<Flight>()
        {
            new Flight { Id = "ABCD1234", CompanyName = "ElAl", Passengers = 230, Latitude = 34.12,
                Longitude = 37.244, DateTime = "2020-12-26T23:56:21Z", External = false },
            new Flight { Id = "EFGH6789", CompanyName = "SwissAir", Passengers = 216, Latitude = 31.12,
                Longitude = 33.244, DateTime = "2020-12-27T23:00:21Z", External = false }
        };

        public void AddFlight(Flight flight)
        {
            flightsList.Add(flight);
        }

        public void DeleteFlight(string id)
        {
            Flight flight = flightsList.Where(x => x.Id == id).FirstOrDefault();
            if (flight == null)
            {
                throw new Exception("Flight not found");
            }
            flightsList.Remove(flight);
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            return flightsList;
        }

        public Flight GetFlight(string id)
        {
            Flight flight = flightsList.Where(x => x.Id == id).FirstOrDefault();
            if (flight == null)
            {
                throw new Exception("Flight not found");
            }
            return flight;
        }

        public void UpdateFlight(Flight flight)
        {
            Flight newFlight = flightsList.Where(x => x.Id == flight.Id).FirstOrDefault();
            if (newFlight == null)
            {
                throw new Exception("Flight not found");
            }
            newFlight.CompanyName = flight.CompanyName;
            newFlight.Passengers = flight.Passengers;
            newFlight.Latitude = flight.Latitude;
            newFlight.Longitude = flight.Longitude;
            newFlight.DateTime = flight.DateTime;
            newFlight.External = flight.External;
        }
    }
}