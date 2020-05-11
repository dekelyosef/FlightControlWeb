using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class ServersManager : IObjectsManager<Server>
    {
        private static readonly List<Server> serversList = new List<Server>()
        {
            new Server { ServerId = "1", URL = "www.server.com" }
        };


        public void AddObject(Server server)
        {
            serversList.Add(server);
        }

        public void DeleteObject(string id)
        {
            Server server = serversList.Where(x => x.ServerId == id).FirstOrDefault();
            if (server == null)
            {
                throw new Exception("Server not found");
            }
            serversList.Remove(server);
        }

        public IEnumerable<Server> GetAllObjects()
        {
            return serversList;
        }

        public Server GetObject(string id)
        {
            Server server = serversList.Where(x => x.ServerId == id).FirstOrDefault();
            if (server == null)
            {
                throw new Exception("Server not found");
            }
            return server;
        }

        public void UpdateObject(Server newServer)
        {
            Server server = serversList.Where(x => x.ServerId == newServer.ServerId).FirstOrDefault();
            if (server == null)
            {
                throw new Exception("Server not found");
            }
            server.ServerId = newServer.ServerId;
            server.URL = newServer.URL;
        }
    }
}
