using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace PlanetBattleLogic.Models
{
    public class Universe
    {
        public Universe()
        {
            this.Planets = new Collection<Planet>();
        }
        public Universe(int width, int height)
        {
            this.Planets = new Collection<Planet>();
            this.Width = width;
            this.Height = height;
        }
        public int Width { get; set; }
        public int Height { get; set; }

        public ICollection<Planet> Planets { get; set; }


        public void PositionPlanets()
        {
            var random = new Random();
            foreach(Planet p in this.Planets)
            {
                p.Location = new Coordinates();
                p.Location.X = random.Next(10, this.Width - 10);
                p.Location.Y = random.Next(10, this.Height - 10);

            }
        }
    }
}