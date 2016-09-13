using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanetBattleLogic.Models
{
    public class Universe
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public ICollection<Planet> Planets { get; set; }
    }
}