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
    public class GameControlTests
    {
        [TestMethod()]
        public void SetupGameTest()
        {
            Game game = GameControl.SetupGame();
            Assert.IsTrue(game.Players.Count > 0);
            Assert.IsTrue(game.Universe.Planets.Count > 0);
            Assert.IsTrue(game.Universe.Height == 100);
            Assert.IsTrue(game.Universe.Width == 100);


        }

        [TestMethod()]
        public void ExecuteTurnTest()
        {
            var player = new Player("Bill");
            var startPlanet = new Planet("Planet B");
            startPlanet.Owner = player;
            startPlanet.Location = new Coordinates(10, 10);
            GameControl.CreateAndAddShips(startPlanet, 1);
            var destPlanet = new Planet("PlanetDest");
            destPlanet.Location = new Coordinates(10, 100);
            int numberOfShipsToMove = 5;
            GameControl.ExecuteTurn(player, startPlanet, destPlanet, numberOfShipsToMove);

            var expectedShipCount = GameControl.GetInitialShipCount() - numberOfShipsToMove;
            Assert.AreEqual(expectedShipCount, startPlanet.Ships.Count);

        }

        [TestMethod()]
        public void CreateAndAddShipsTest()
        {
            var player = new Player("Steve");
            var planet = new Planet("TestPlanet");
            planet.Owner = player;
            GameControl.CreateAndAddShips(planet, 1);
            int intitialShipCount = GameControl.GetInitialShipCount();
            Assert.AreEqual(intitialShipCount, planet.Ships.Count);
            Assert.AreEqual
                (
                player.Name,
                planet.Ships.FirstOrDefault().Owner.Name
                );
        }
    }
}