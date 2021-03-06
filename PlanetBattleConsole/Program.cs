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

            while (game.ActiveGame)
            {
                foreach (Player player in game.Players)
                {
                    ProcessTurn(player, game, game.Universe.Ships);
                    int playersRemaining = CountPlayersWithShipsRemaining(game);
                    if (playersRemaining <=1)
                    {
                        game.ActiveGame = false;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Game over!");
            Console.WriteLine("Press ENTER to Exit game!");

            Console.ReadLine();
        }

        private static int CountPlayersWithShipsRemaining(Game game)
        {
            int playersWithShips = 0;
            foreach (Player player in game.Players)
            {
                if (player.Ships.Count() > 0)
                {
                    playersWithShips++;
                }
            }
            return playersWithShips;
        }

        public static void ProcessTurn(Player player, Game game, ICollection<Ship> ships)
        {
            Console.WriteLine();
            Console.WriteLine("{0}: It is your move", player.Name);
            Console.WriteLine("You have ships on the following planets:");
            foreach (Planet planet in game.Universe.Planets
                .Where(p => p.Owner?.Name == player.Name))
            {
                Console.WriteLine("{0}) {1}", planet.Id, planet.Name);
            }

            Planet selectedStartPlanet = PromptForStartPlanet(game);
            Console.WriteLine("You selected {0}", selectedStartPlanet.Name);
            Console.WriteLine();

            int numberOfShips = PromptForNumberOfShips();
            Console.WriteLine("You elected to launch {0} ships from {1}.", numberOfShips, selectedStartPlanet.Name);

            Console.WriteLine();

            Planet selectedDestPlanet = PromptForDestinationPlanet(game);

            Console.WriteLine("You elected to launch {0} ships from {1} toward {2}.", numberOfShips, selectedStartPlanet.Name, selectedDestPlanet.Name);

            GameControl.ExecuteTurn(player, selectedStartPlanet, selectedDestPlanet, numberOfShips, game);

            Console.WriteLine("******************************");
            Console.WriteLine("Here is the current status of the game:");
            PrintBoardStatus(game);

            FightAllBattles(game);

            Console.WriteLine();
            Console.WriteLine("******************************");
            Console.WriteLine("******************************");
            Console.WriteLine("******************************");
            Console.WriteLine();
        }

        private static Planet PromptForDestinationPlanet(Game game)
        {
            Planet selectedDestPlanet = null;
            bool validDestinationPlanetEntered = false;
            while (!validDestinationPlanetEntered)
            {
                Console.WriteLine("Toward which planet would you like to send these ships?");
                Console.WriteLine("Enter an ID and press ENTER");
                foreach (Planet planet in game.Universe.Planets)
                {
                    Console.WriteLine("{0}) {1}", planet.Id, planet.Name);
                }
                string selectedDestIdAsString = Console.ReadLine();
                int selectedDestId;
                if (int.TryParse(selectedDestIdAsString, out selectedDestId))
                {
                    selectedDestPlanet = game.Universe.Planets
                        .Where(p => p.Id == selectedDestId).FirstOrDefault();
                    if (selectedDestPlanet != null)
                    {
                        validDestinationPlanetEntered = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid planet");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid planet number");
                }
            }

            return selectedDestPlanet;
        }

        private static int PromptForNumberOfShips()
        {
            bool validNumberOfShipsEntered = false;
            int numberOfShips = 0;
            while (!validNumberOfShipsEntered)
            {
                Console.WriteLine("How many ships would you like to launch from this planet?");
                string numberOfShipsAsString = Console.ReadLine();
                if (int.TryParse(numberOfShipsAsString, out numberOfShips))
                {
                    validNumberOfShipsEntered = true;
                }
                else
                {
                    Console.WriteLine("Not a valid number.");
                }
            }
            return numberOfShips;
        }

        private static Planet PromptForStartPlanet(Game game)
        {
            Console.WriteLine("From which planet do you want to launch ships?");
            Console.WriteLine("Enter an ID and press ENTER");
            bool validPlanetIdEntered = false;
            Planet selectedStartPlanet = new Planet();
            while (!validPlanetIdEntered)
            {
                string selectedIdString = Console.ReadLine();

                //int selectedId = Convert.ToInt32(selectedIdString);
                int selectedId = 0;
                if (int.TryParse(selectedIdString, out selectedId))
                {
                    selectedStartPlanet = game.Universe.Planets
                        .Where(p => p.Id == selectedId).FirstOrDefault();
                    if (selectedStartPlanet != null)
                    {
                        validPlanetIdEntered = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid planet");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid planet number");
                }
            }

            return selectedStartPlanet;
        }

        private static void FightAllBattles(Game game)
        {
            foreach (Planet planet in game.Universe.Planets)
            {
                FightAllBattles(planet);
            }
        }

        private static void FightAllBattles (Planet planet)
        {
            if (planet.Ships.Count <= 1)
            {
                return;
            }
            while (true)
            {
                Ship ship1 = planet.Ships.First();
                if (ship1 == null)
                {
                    // No ships on planet
                    return;
                }

                // Pick another owner's ship on this planet
                Ship ship2 = planet.Ships.Where(s => s.Owner.Name != ship1.Owner.Name).FirstOrDefault();
                if (ship2 == null)
                {
                    // No opposing ships to battle
                    return;
                }

                // Ships fight each other; Destroy loser
                Ship winningShip = GameControl.FightBattle(ship1, ship2);
                if (winningShip.Id == ship1.Id)
                {
                    planet.Ships.Remove(ship2);
                    ship2 = null;
                }
                else
                {
                    planet.Ships.Remove(ship1);
                    ship1 = null;
                }
            }

        }

        private static void PrintBoardStatus(Game game)
        {
            PrintActivities(game, "001");

            PrintAllPlayers(game.Players);

            Console.WriteLine();

            Universe universe = game.Universe;
            PrintAllPlanets(universe.Planets);

            Console.WriteLine();

            PrintShipsBetweenPlanets(game);

        }

        private static void PrintActivities(Game game, string group)
        {
            Console.WriteLine("***************************");
            Console.WriteLine("Moves so far:");
            if (game.Activities.Where(a => a.Group == group).Count() == 0)
            {
                Console.WriteLine("    **none**");
            }
            foreach (Activity activity in game.Activities.Where(a => a.Group == group))
            {
                Console.Write("    ");
                Console.Write(activity.DateTime);
                Console.Write("  ");
                Console.WriteLine(activity.Text);
            }
        }

        private static void PrintShipsBetweenPlanets(Game game)
        {
            Console.WriteLine("=======================");
            Console.WriteLine("Ships betweeen planets:");
            Console.WriteLine("=======================");

            var ships = GameControl.GetShipsNotOnAnyPlanet(game);
            if (ships.Count() == 0)
            {
                Console.WriteLine("    **none**");
                return;
            }
            foreach (Ship ship in ships)
            {
                Console.WriteLine
                    (
                    "    Ship Id: {0}; Owner: {1}; Location: {2:##0.0}, {3:##0.0}", 
                    ship.Id, 
                    ship.Owner.Name, 
                    ship.Location.X, 
                    ship.Location.Y
                    );
            }
        }

        private static void PrintAllPlayers(ICollection<Player> players)
        {
            Console.WriteLine("=======");
            Console.WriteLine("Players");
            Console.WriteLine("=======");
            foreach (Player player in players)
            {
                Console.Write ("    ");
                Console.WriteLine(player.Name);
            }
            Console.WriteLine();
        }

        private static void PrintAllPlanets(ICollection<Planet> planets)
        {
            Console.WriteLine("=======");
            Console.WriteLine("Planets");
            Console.WriteLine("=======");
            Console.WriteLine();
            foreach (Planet planet in planets)
            {
                Console.WriteLine("  Planet: {0}", planet.Name);
                Console.WriteLine("  Owner: {0}", planet.Owner?.Name ?? "None");
                Console.WriteLine("  Location: {0:##0.0}, {1:##0.0}", planet.Location.X, planet.Location.Y);
                Console.WriteLine("  {0} Ships:", planet.Name);
                //Console.WriteLine("  -----");
                if (planet.Ships.Count() == 0)
                {
                    Console.WriteLine("    **none**");
                }
                foreach (Ship ship in planet.Ships)
                {
                    Console.WriteLine("    Ship Id: {0}; Owner: {1}", ship.Id, ship.Owner.Name);
                }
                Console.WriteLine("-----------");
            }
        }
    }
}
