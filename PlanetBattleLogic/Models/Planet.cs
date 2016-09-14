using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetBattleLogic.Models
{
    public class Planet
    {
        public Planet()
        {
            Ships = new Collection<Ship>();
        }

        public Planet(string name)
        {
            Name = name;
            Ships = new Collection<Ship>();
        }
        public int Id { get; set; }
        public Player Owner { get; set; }
        public string Name { get; set; }
        public ICollection<Ship> Ships { get; set; }
        public Coordinates Location { get; set; }

    }
}
