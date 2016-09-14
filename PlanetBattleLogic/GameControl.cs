using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetBattleLogic.Models;

namespace PlanetBattleLogic
{
    public static class GameControl
    {
        public static int GetUnitsToMove()
        {
            return 50;
        }

        public static int GetInitialShipCount()
        {
            return 20;
        }

        public static Game SetupGame()
        {
            var universe = new Universe(100, 100);
            universe.Planets = CreateAndAddPlanets();
            universe.PositionPlanets();
            var game = new Game();
            game.Universe = universe;
            game.Players = CreateAndAddPlayers();
            int nextId = 1;
            AssignPlayersToPlanets(universe.Planets, game.Players);
            foreach (Player player in game.Players)
            {
                var shipsForThisPlayer = CreateAndAddShips(player.HomePlanet, nextId);
                //player.HomePlanet.Ships = shipsForThisPlayer;
            }

            return game;
        }

        public static void ExecuteTurn(Player player, Planet startPlanet, Planet destinationPlanet, int numberOfShips)
        {
            int unitsToMove = GetUnitsToMove();
            var playersShipsOnThisPlanet = startPlanet.Ships.Where(s => s.Owner == player);
            if (numberOfShips > playersShipsOnThisPlanet.Count())
            {
                numberOfShips = playersShipsOnThisPlanet.Count();
            }
            int shipsSentSoFar = 0;

            // Copy of planet's ships collection
            var copyShipsCollection = new Collection<Ship>();
            foreach (Ship ship in startPlanet.Ships)
            {
                copyShipsCollection.Add(ship);
            }

            foreach (Ship ship in startPlanet.Ships)
            {
                if (shipsSentSoFar >= numberOfShips)
                {
                    break;
                }
                ship.Move(unitsToMove, destinationPlanet);
                copyShipsCollection.Remove(ship);
                shipsSentSoFar++;
            }
            startPlanet.Ships = copyShipsCollection;
        }

        public static Ship FightBattle(Ship ship1, Ship ship2)
        {
            var r = new Random();
            int winnerIndex = r.Next(0, 1);

            Ship[] shipArray = new Ship[2] { ship1, ship2 };
            return shipArray[winnerIndex];
        }

        public static void BattleForPlanet(Planet planet)
        {
            ShipBattle battle = MatchShipsForBattle(planet.Ships);
            while (battle != null)
            {
                Ship winningShip = FightBattle(battle.FirstShip, battle.SecondShip);
                Ship losingShip = null;
                if (winningShip == battle.FirstShip)
                {
                    losingShip = battle.SecondShip;
                }
                else
                {
                    losingShip = battle.FirstShip;
                }

                planet.Ships.Remove(losingShip);

                battle = MatchShipsForBattle(planet.Ships);
            }

        }

        public static ShipBattle MatchShipsForBattle(ICollection<Ship> ships)
        {
            if (ships.Count <= 1)
            {
                return null;
            }
            Ship ship1 = ships.First();
            foreach (Ship ship2 in ships)
            {
                if (ship1.Owner != ship2.Owner)
                {
                    return new ShipBattle(ship1, ship2);
                }
            }
            return null;

        }

        public static ICollection<Player> CreateAndAddPlayers()
        {
            var player1 = new Player("David");
            var player2 = new Player("Kevin");
            var players = new Collection<Player>();
            players.Add(player1);
            players.Add(player2);
            return players;
        }

        public static ICollection<Planet> CreateAndAddPlanets()
        {
            var planetD = new Planet("Planet D") { Id = 1 };
            var planetX = new Planet("Planet X") { Id = 2 };
            var planetK = new Planet("Planet K") { Id = 3 };

            var planets = new Collection<Planet>();
            planets.Add(planetD);
            planets.Add(planetX);
            planets.Add(planetK);
            return planets;
        }

        public static ICollection<Round> CreateAndAddRound()
        {
            var rounds = new Collection<Round>();
            var round = new Round();
            rounds.Add(round);
            return rounds;
        }

        public static ICollection<Ship> CreateAndAddShips(Planet planet,int startID)
        {
            int initialShipCount = GetInitialShipCount();
            var ships = new Collection<Ship>();
            for (int i = startID; i <= initialShipCount; i++)
            {
                var ship = new Ship(i, planet.Owner, planet.Location);
                ships.Add(ship);
            }
            planet.Ships = ships;

            return ships;

        }

        public static void AssignPlayersToPlanets(ICollection<Planet> planets, ICollection<Player> players)
        {
            players.FirstOrDefault().HomePlanet = planets.FirstOrDefault();
            players.LastOrDefault().HomePlanet = planets.LastOrDefault();
            planets.FirstOrDefault().Owner = players.FirstOrDefault();
            planets.LastOrDefault().Owner = players.LastOrDefault();
        }
    }
}
