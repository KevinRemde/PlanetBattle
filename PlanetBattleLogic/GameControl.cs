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
        public static void SetupGame()
        {
            var universe = new Universe(100, 100);
            universe.Planets = CreateAndAddPlanets();
            universe.PositionPlanets();
            var game = new Game();
            game.Universe = universe;
            game.Players = CreateAndAddPlayers();
            int nextId = 1;
            foreach (Player player in game.Players)
            {
                CreateAndAddShips(player.HomePlanet, nextId);
            }
            AssignPlayersToPlanets(universe.Planets, game.Players);
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
            var planetD = new Planet("Planet D");
            var planetX = new Planet("Planet X");
            var planetK = new Planet("Planet K");

            var planets = new Collection<Planet>();
            planets.Add(planetD);
            planets.Add(planetK);
            planets.Add(planetX);
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
            var ships = new Collection<Ship>();
            for (int i = startID; i <= 20; i++)
            {
                var ship = new Ship(i, planet.Owner, planet.Location);
                ships.Add(ship);
            }

            return ships;

        }

        public static void AssignPlayersToPlanets(ICollection<Planet> planets, ICollection<Player> players)
        {
            players.FirstOrDefault().HomePlanet = planets.FirstOrDefault();
            players.LastOrDefault().HomePlanet = planets.LastOrDefault();
        }

    }
}
