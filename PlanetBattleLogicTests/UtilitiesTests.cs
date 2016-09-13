using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlanetBattleLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetBattleLogic.Models;

namespace PlanetBattleLogic.Tests
{
    [TestClass()]
    public class UtilitiesTests
    {
        [TestMethod()]
        public void DistanceToDestinationTest1()
        {
            var start = new Coordinates(10, 20);
            var end = new Coordinates(10, 30);
            Double distance = Utilities.DistanceToDestination(start, end);
            Assert.AreEqual(10, distance);
        }

        [TestMethod()]
        public void DistanceToDestinationTest2()
        {
            var start = new Coordinates(10, 10);
            var end = new Coordinates(40, 50);
            Double distance = Utilities.DistanceToDestination(start, end);
            double expectedDistance = 50;
            Assert.AreEqual(expectedDistance, distance);
        }

        [TestMethod()]
        public void GetAngleOfLineBetweenTwoPointsTest()
        {
            var start = new Coordinates(10, 10);
            var end = new Coordinates(40, 50);
            double angle = Utilities.GetAngleOfLineBetweenTwoPoints(start, end);
            double expectedAngle = 53.13;
            Assert.AreEqual(Math.Floor(expectedAngle), Math.Floor(angle));
        }

        [TestMethod()]
        public void GetDestinationLocationTest()
        {
            double angle = 53.13;
            double distance = 50;
            Coordinates startLocation = new Coordinates(10, 10);
            Coordinates newLocation = Utilities.GetDestinationLocation(startLocation, angle, distance);

            double expectedX = 40;
            double expectedY = 50;
            double actualX = Math.Round(newLocation.X, 2);
            double actualY = Math.Round(newLocation.Y, 2);

            Assert.AreEqual(expectedX, actualX);
            Assert.AreEqual(expectedY, actualY);
            
        }
    }
}