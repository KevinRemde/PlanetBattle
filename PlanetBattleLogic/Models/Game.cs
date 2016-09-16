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
            this.Activities = new Collection<Activity>();
        }
        public Universe Universe { get; set; }
        public ICollection<Round> Rounds { get; set; }
        public ICollection<Player> Players { get; set; }
        public int NextShipId { get; set; }
        public bool ActiveGame { get; set; }
        public Collection<Activity> Activities { get; set; }

        internal void LogActivity(string logText)
        {
            var activity = new Activity()
                { DateTime = DateTime.Now, Group = "001", Text = logText };
            Activities.Add(activity);

        }


    }
}
