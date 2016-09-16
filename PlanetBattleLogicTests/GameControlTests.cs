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
            var game = new Game();
            game.Universe = new Universe();
            var player = new Player("Bill");
            game.Players.Add(player);
            var startPlanet = new Planet("Planet B");
            startPlanet.Owner = player;
            startPlanet.Location = new Coordinates(10, 10);
            GameControl.CreateAndAddShips(startPlanet, 1, game);
            var destPlanet = new Planet("PlanetDest");
            destPlanet.Location = new Coordinates(10, 100);
            int numberOfShipsToMove = 5;
            GameControl.ExecuteTurn(player, startPlanet, destPlanet, numberOfShipsToMove, game);

            var expectedShipCount = GameControl.GetInitialShipCount() - numberOfShipsToMove;
            Assert.AreEqual(expectedShipCount, startPlanet.Ships.Count);

        }

        [TestMethod()]
        public void CreateAndAddShipsTest()
        {
            var universe = new Universe();
            var game = new Game();
            game.Universe = universe;
            var player = new Player("Steve");
            game.Players.Add(player);
            var planet = new Planet("TestPlanet");
            planet.Owner = player;
            GameControl.CreateAndAddShips(planet, 1, game);
            int intitialShipCount = GameControl.GetInitialShipCount();
            Assert.AreEqual(intitialShipCount, planet.Ships.Count);
            Assert.AreEqual
                (
                player.Name,
                planet.Ships.FirstOrDefault().Owner.Name
                );
        }

        [TestMethod()]
        public void GetShipsNotOnAnyPlanetTest()
        {
            var game = new Game();
            game.Universe = new Universe();

            // Setup: 2 ships on each planet
            var planet1 = new Planet("Planet1");
            planet1.Location = new Coordinates(10, 10);
            var player1 = new Player("P1");
            var ship11 = new Ship(1, player1, planet1.Location);
            ship11.CurrentPlanet = planet1;
            player1.Ships.Add(ship11);
            var ship12 = new Ship(2, player1, planet1.Location);
            ship12.CurrentPlanet = planet1;
            player1.Ships.Add(ship12);
            game.Players.Add(player1);
            game.Universe.Planets.Add(planet1);

            var planet2 = new Planet("Planet2");
            planet2.Location = new Coordinates(100, 100);
            var player2 = new Player("P2");
            var ship21 = new Ship(3, player2, planet2.Location);
            ship21.CurrentPlanet = planet2;
            player2.Ships.Add(ship21);
            var ship22 = new Ship(4, player2, planet2.Location);
            ship22.CurrentPlanet = planet2;
            player2.Ships.Add(ship22);
            game.Players.Add(player2);
            game.Universe.Planets.Add(planet2);

            var ships = GameControl.GetShipsNotOnAnyPlanet(game);

            var expectedCount = 0;
            Assert.AreEqual(expectedCount, ships.Count);
        }

        [TestMethod()]
        public void GetShipsNotOnAnyPlanetAfterMoveTest()
        {
            var game = new Game();
            game.Universe = new Universe();

            // Setup: 2 ships on each planet
            var planet1 = new Planet("Planet1");
            planet1.Location = new Coordinates(10, 10);
            var player1 = new Player("P1");
            var ship11 = new Ship(1, player1, planet1.Location);
            ship11.CurrentPlanet = planet1;
            player1.Ships.Add(ship11);
            var ship12 = new Ship(2, player1, planet1.Location);
            ship12.CurrentPlanet = planet1;
            player1.Ships.Add(ship12);
            game.Players.Add(player1);
            game.Universe.Planets.Add(planet1);

            var planet2 = new Planet("Planet2");
            planet2.Location = new Coordinates(100, 100);
            var player2 = new Player("P2");
            var ship21 = new Ship(3, player2, planet2.Location);
            ship21.CurrentPlanet = planet2;
            player2.Ships.Add(ship21);
            var ship22 = new Ship(4, player2, planet2.Location);
            ship22.CurrentPlanet = planet2;
            player2.Ships.Add(ship22);
            game.Players.Add(player2);
            game.Universe.Planets.Add(planet2);

            ship11.Move(20, planet2);
            ship22.Move(20, planet1);

            var ships = GameControl.GetShipsNotOnAnyPlanet(game);

            var expectedCount = 2;
            Assert.AreEqual(expectedCount, ships.Count);
        }
    }
}