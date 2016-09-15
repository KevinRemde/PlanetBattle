﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetBattleLogic.Models;
using PlanetBattleLogic;
using System.Collections.ObjectModel;

namespace PlanetBattleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = GameControl.SetupGame();
            PrintBoardStatus(game);

            // TODO: Replace this loop with 
            //          while more than one player has ships
            for (int i = 0; i < 5; i++)
            {
                foreach (Player player in game.Players)
                {
                    ProcessTurn(player, game);
                }

            }

            Console.WriteLine();
            Console.WriteLine("Game over!");
            Console.WriteLine("Press ENTER to Exit game!");

            Console.ReadLine();
        }

        public static void ProcessTurn(Player player, Game game)
        {
            Console.WriteLine();
            Console.WriteLine("{0}: It is your move", player.Name);
            Console.WriteLine("You have ships on the following planets:");
            foreach (Planet planet in game.Universe.Planets
                .Where(p => p.Owner?.Name == player.Name))
            {
                Console.WriteLine("{0}) {1}", planet.Id, planet.Name);
            }
            Console.WriteLine("From which planet do you want to launch ships?");
            Console.WriteLine("Enter an ID and press ENTER");
            string selectedIdString = Console.ReadLine();
            int selectedId = Convert.ToInt32(selectedIdString);
            Planet selectedStartPlanet = game.Universe.Planets
                .Where(p => p.Id == selectedId).FirstOrDefault();
            Console.WriteLine("You selected {0}", selectedStartPlanet.Name);
            Console.WriteLine();

            Console.WriteLine("How many ships would you like to launch from {0}?", selectedStartPlanet.Name);
            string numberOfShipsAsString = Console.ReadLine();
            int numberOfShips = Convert.ToInt32(numberOfShipsAsString);
            Console.WriteLine("You elected to launch {0} ships from {1}.", numberOfShips, selectedStartPlanet.Name);
            Console.WriteLine();
            Console.WriteLine("Toward which planet would you like to send these ships?");
            Console.WriteLine("Enter an ID and press ENTER");
            foreach (Planet planet in game.Universe.Planets
                .Where(p => p.Id != selectedId))
            {
                Console.WriteLine("{0}) {1}", planet.Id, planet.Name);
            }
            string selectedDestIdAsString = Console.ReadLine();
            int selectedDestId = Convert.ToInt32(selectedDestIdAsString);
            Planet selectedDestPlanet = game.Universe.Planets
                .Where(p => p.Id == selectedDestId).FirstOrDefault();
            Console.WriteLine("You elected to launch {0} ships from {1} toward {2}.", numberOfShips, selectedStartPlanet.Name, selectedDestPlanet.Name);

            GameControl.ExecuteTurn(player, selectedStartPlanet, selectedDestPlanet, numberOfShips, game);

            Console.WriteLine("=======================================");
            Console.WriteLine("Here is the current status of the game:");
            PrintBoardStatus(game);

            FightAllBattles(game);
            // Loop through ships. Any landed on planet?
            // If so, fight battles with ships on planet


            Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxx");
        }

        private static void FightAllBattles(Game game)
        {
            ICollection<Player> players = game.Players; 
            foreach (Player player in players)
            {
                ICollection<Ship> ships = player.Ships;
                foreach(Ship ship in ships)
                {
                    // Which ships are on a planet?
                    if (ShipIsOnAPlanet(ship))
                    {
                        // Are ships of any other owners on this planet?
                        //ICollection<Ship> OpponentShips = GetOpponentShipsOnPlanet(player, planet);

                    }
                }
            }
        }

        private static ICollection<Ship> GetOpponentShipsOnPlanet(Player player, object planet)
        {
            // TODO: Get Opponent ships on Planet
            var ships = new Collection<Ship>();
            return ships;
        }

        private static bool ShipIsOnAPlanet(Ship ship)
        {
            // TODO: Implement this
            return false;
            //throw new NotImplementedException();
        }

        private static void PrintBoardStatus(Game game)
        {
            PrintAllPlayers(game.Players);
            Universe universe = game.Universe;
            Console.WriteLine();
            PrintAllPlanets(universe.Planets); 

        }

        private static void PrintAllPlayers(ICollection<Player> players)
        {
            Console.WriteLine("=======");
            Console.WriteLine("Players");
            Console.WriteLine("=======");
            foreach (Player player in players)
            {
                Console.WriteLine(player.Name);
            }
        }

        private static void PrintAllPlanets(ICollection<Planet> planets)
        {
            Console.WriteLine("=======");
            Console.WriteLine("Planets");
            Console.WriteLine("=======");
            foreach (Planet planet in planets)
            {
                Console.WriteLine("Planet: {0}", planet.Name);
                Console.WriteLine("Owner: {0}", planet.Owner?.Name ?? "None");
                Console.WriteLine("Location: {0}, {1}", planet.Location.X, planet.Location.Y);
                Console.WriteLine("-----");
                Console.WriteLine("Ships");
                Console.WriteLine("-----");
                foreach (Ship ship in planet.Ships)
                {
                    Console.WriteLine("    Ship Id: {0}; Owner: {1}", ship.Id, ship.Owner.Name);
                }
                Console.WriteLine("-----------");
            }
        }
    }
}
