using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    interface IObjectsManager<T>
    {
        IEnumerable<T> GetAllObjects();
        T GetObject(string id);
        void AddObject(T obj);
        void UpdateObject(T newObj);
        void DeleteObject(string id);
    }
}