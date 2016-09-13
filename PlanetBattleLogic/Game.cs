using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetBattleLogic.Models;

namespace PlanetBattleLogic
{
    public static class Game
    {
        public ICollection<Player> Players { get; set; };

        public static void SetupGame()
        {

        }

        public static Ship FightBattle (Ship ship1, Ship ship2)
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

        public static ShipBattle MatchShipsForBattle (ICollection<Ship> ships)
        {
            if (ships.Count <=1)
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
    }
}
