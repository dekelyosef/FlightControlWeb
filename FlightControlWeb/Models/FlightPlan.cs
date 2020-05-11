using FlightControlWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlWeb.Models
{
    public class FlightPlan
    {
        string id;
        int passengers;
        string companyName;
        // initial location
        double longitude;
        double latitude;
        string dateTime;
        // the all path
        Segment[] segment;

        public FlightPlan(double lon, double lat, int passengersNum, string company,
            string time, Segment[] seg)
        {
            Id = FlightId.GetRandomId();
            Passengers = passengersNum;
            CompanyName = company;
            // initial lovation
            Longitude = lon;
            Latitude = lat;
            DateTime = time;
            // the all path
            Segment = seg;

            // create a new flight to this flightPlan
            new Flight(Id, lon, lat, passengersNum, company, time, true);
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

        public Segment[] Segment
        {
            get { return this.segment; }
            set
            {
                if (this.segment != value)
                {
                    this.segment = value;
                }
            }
        }
    }
}