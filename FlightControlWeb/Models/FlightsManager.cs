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
                Longitude = 37.244, DateTime = "2020-12-26T23:56:21Z", IsExternal = false },
            new Flight { Id = "EFGH6789", CompanyName = "SwissAir", Passengers = 216, Latitude = 31.12,
                Longitude = 33.244, DateTime = "2020-12-27T23:00:21Z", IsExternal = false }
        };

        public FlightsManager() { }

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
            flight.IsExternal = newFlight.IsExternal;
        }

        public List<Flight> GetActiveFlights(string relativeTo)
        {
            DateTime relateTime = getDateTime(relativeTo);
            List<Flight> activeFlights = new List<Flight>();

            foreach (Flight flight in flightsList)
            {
                DateTime flightTime = getDateTime(relativeTo);
                if (DateTime.Compare(relateTime, flightTime) <= 0)
                {
                    activeFlights.Add(flight);
                }
            }
            return activeFlights;
        }

        public DateTime getDateTime(string relativeTo)
        {
            int year, mounth, day, hour, minute, second;
            // convert to dateTime
            year = Int32.Parse(relativeTo.Substring(0, 4));
            mounth = Int32.Parse(relativeTo.Substring(5, 2));
            day = Int32.Parse(relativeTo.Substring(8, 2));
            hour = Int32.Parse(relativeTo.Substring(11, 2));
            minute = Int32.Parse(relativeTo.Substring(14, 2));
            second = Int32.Parse(relativeTo.Substring(17, 2));

            return new DateTime(year, mounth, day, hour, minute, second);
        }
    }
}