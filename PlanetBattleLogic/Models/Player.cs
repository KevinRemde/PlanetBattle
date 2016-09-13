using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetBattleLogic.Models
{
    public class Player
    {
        public Player()
        {
        }
        public Player(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }
        public Planet HomePlanet { get; set; }
        public ICollection<Ship> Ships { get; set; }


    }
}
