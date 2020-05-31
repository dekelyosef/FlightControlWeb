using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightControlWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlightControlWeb.Models;
using FlightControlWeb.Data;

namespace FlightControlWeb.Controllers.Tests
{
    [TestClass()]
    public class FlightPlanControllerTests
    {
        private FlightsDbContext flightsDbContext;
        private FlightPlanController flightPlanController;


        /**
         * Stub - checks set controllers in configure services
         **/
        [TestMethod()]
        public void FlightPlanControllerTest()
        {
            try
            {
                // checks the exception
                Startup startUp = new Startup(null);
                Assert.Fail("Did Not Catch Exception");
            }
            catch (Exception)
            {
            }
        }


        /**
         * Set new database and Add new flight plan test
         **/
        [TestMethod()]
        public async Task ConfigureServicesAddFlightPlanTest()
        {
            // mocks
            var data = new DbContextOptionsBuilder<FlightsDbContext>();
            data.UseInMemoryDatabase("FlightPlanDb");
            flightsDbContext = new FlightsDbContext(data.Options);
            flightPlanController = new FlightPlanController(flightsDbContext);
            await SetnewFlightPlanToDbTest();
        }


        /**
         * Add flight plan to database and test if set succssefully
         **/
        public async Task SetnewFlightPlanToDbTest()
        {
            // arrange
            FlightPlan flight = AddNewFlightPlan();
            // add new flight plan to the data base for testing the controller
            flightsDbContext.FlightPlans.Add(flight);
            // save changes
            flightsDbContext.SaveChanges();

            // act
            // finds in the data base the flight plan for the test
            var flightPlan = await flightsDbContext.FlightPlans.FindAsync("ABcd1234");

            // assert
            Assert.AreEqual("ABcd1234", flightPlan.Id);
            Assert.AreEqual("ABcd1234", flightPlan.InitialLocation.Id);
            Assert.AreEqual(222, flightPlan.Passengers);
            Assert.AreEqual("El-Al", flightPlan.CompanyName);
        }


        /**
         * Creates new flight plan
         **/
        private FlightPlan AddNewFlightPlan()
        {
            // set initial location
            InitialLocation newLocation = AddNewLocation();
            Segment segment = new Segment("ABcd1234", 32, 32, 650);

            FlightPlan newFlightPlan = new FlightPlan
            {
                Id = "ABcd1234",
                Passengers = 222,
                CompanyName = "El-Al",
                InitialLocation = newLocation
            };

            return newFlightPlan;
        }


        /**
         * Creates new initial location
         **/
        private InitialLocation AddNewLocation()
        {
            DateTime time = DateTime.Now;
            InitialLocation newLocation = new InitialLocation("ABcd1234", 30, 30, time);
            return newLocation;
        }

    }
}