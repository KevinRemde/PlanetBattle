using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetBattleLogic.Models
{
    public class Player
    {
        public Player()
        {
            this.Ships = new Collection<Ship>();
        }
        public Player(string name)
        {
            this.Ships = new Collection<Ship>();
            this.Name = name;
        }
        public string Name { get; set; }
        public Planet HomePlanet { get; set; }
        public ICollection<Ship> Ships { get; set; }


    }
}
