using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    interface IFlightsManager<T>
    {
        IEnumerable<T> GetAllFlights();
        T GetFlight(string id);
        void AddFlight(T flight);
        void UpdateFlight(T flight);
        void DeleteFlight(string id);
    }
}