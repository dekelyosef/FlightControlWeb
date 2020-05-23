using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class Server
    {
        string serverId;
        string serverURL;


        /**
         * Constructor
         **/
        public Server() { }


        /**
         * Constructor with given parameters
         **/
        public Server(string id, string uRL)
        {
            ServerId = id;
            ServerURL = uRL;
        }


        [JsonPropertyName("ServerId")]
        public string ServerId
        {
            get { return this.serverId; }
            set { this.serverId = value; }
        }


        [JsonPropertyName("ServerURL")]
        public string ServerURL
        {
            get { return this.serverURL; }
            set { this.serverURL = value; }
        }
    }
}
