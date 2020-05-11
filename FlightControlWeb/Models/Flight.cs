using FlightControlWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlWeb.Models
{
    public class Flight
    {
        string id;
        double longitude;
        double latitude;
        int passengers;
        string companyName;
        string dateTime;
        bool isExternal;

        public Flight() { }

        public Flight(double lon, double lat, int passengersNum, string company,
            string time, bool external)
        {
            Id = FlightId.GetRandomId();
            Longitude = lon;
            Latitude = lat;
            Passengers = passengersNum;
            CompanyName = company;
            DateTime = time;
            IsExternal = external;
        }

        public Flight(string flightId, double lon, double lat, int passengersNum, string company,
            string time, bool external)
        {
            Id = flightId;
            Longitude = lon;
            Latitude = lat;
            Passengers = passengersNum;
            CompanyName = company;
            DateTime = time;
            IsExternal = external;
        }

        public string Id
        {
            get { return this.id; }
            set
            {
                if (!this.id.Equals(value))
                {
                    this.id = value;
                }
            }
        }

        public string CompanyName
        {
            get { return this.companyName; }
            set
            {
                if (!this.companyName.Equals(value))
                {
                    this.companyName = value;
                }
            }
        }

        public double Longitude
        {
            get { return this.longitude; }
            set
            {
                if (this.longitude != value)
                {
                    this.longitude = value;
                }
            }
        }

        public double Latitude
        {
            get { return this.latitude; }
            set
            {
                if (this.latitude != value)
                {
                    this.latitude = value;
                }
            }
        }

        public int Passengers
        {
            get { return this.passengers; }
            set
            {
                if (this.passengers != value)
                {
                    this.passengers = value;
                }
            }
        }

        public string DateTime
        {
            get { return this.dateTime; }
            set
            {
                if (!this.dateTime.Equals(value))
                {
                    this.dateTime = value;
                }
            }
        }
        public bool IsExternal
        {
            get { return this.isExternal; }
            set
            {
                if (isExternal != value)
                {
                    this.isExternal = value;
                }
            }
        }
    }
}