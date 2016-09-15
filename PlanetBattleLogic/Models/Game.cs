using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetBattleLogic.Models
{
    public class Game
    {
        public Game()
        {
            this.Players = new Collection<Player>();
        }
        public Universe Universe { get; set; }
        public ICollection<Round> Rounds { get; set; }
        public ICollection<Player> Players { get; set; }
        public int NextShipId { get; set; }


    }
}
