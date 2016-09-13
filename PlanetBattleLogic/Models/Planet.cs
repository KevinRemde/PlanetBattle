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

        public ICollection<Ship> Ships { get; set; }
        public Coordinates Location { get; set; }

    }
}
