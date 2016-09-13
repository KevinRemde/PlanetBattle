using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlanetBattleLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetBattleLogic.Models.Tests
{
    [TestClass()]
    public class ShipTests
    {
        [TestMethod()]
        public void MoveTest()
        {
            var start = new Coordinates(10, 10);
            var ship = new Ship(1, new Player("Satya"), start);
            var destinationPlanet = new Planet
                ("Planet Microsoft");
            destinationPlanet.Location = new Coordinates(10, 100);
            ship.Move(50, destinationPlanet);

            var expectedX = 10;
            var expectedY = 60;
            Assert.IsTrue (Math.Abs (expectedX- ship.Location.X) < .001);
            Assert.IsTrue(Math.Abs(expectedY - ship.Location.Y) < .001);

        }
    }
}