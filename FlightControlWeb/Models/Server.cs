using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class Server
    {
        string serverId;
        string uRL;

        public Server() { }

        public Server(string id, string uRLStr)
        {
            ServerId = id;
            URL = uRLStr;
        }

        public string ServerId
        {
            get { return this.serverId; }
            set
            {
                if (!this.serverId.Equals(value))
                {
                    this.serverId = value;
                }
            }
        }

        public string URL
        {
            get { return this.uRL; }
            set
            {
                if (!this.uRL.Equals(value))
                {
                    this.uRL = value;
                }
            }
        }
    }
}
