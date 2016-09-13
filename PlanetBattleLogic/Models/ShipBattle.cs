using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetBattleLogic.Models
{
    public class ShipBattle
    {
        public ShipBattle(Ship ship1, Ship ship2)
        {
            this.FirstShip = ship1;
            this.SecondShip = ship2;
        }

        public Ship FirstShip { get; set; }
        public Ship SecondShip { get; set; }
    }
}
