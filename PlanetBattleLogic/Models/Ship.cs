using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetBattleLogic.Models
{
    public class Ship
    {
        public int Id { get; set; }
        public Player Owner { get; set; }
        public Coordinates Location { get; set; }

        public Planet Destination { get; set; }

        public void Move (double unitsToMove)
        {
            if (this.Destination == null)
            {
                // No destination set.
                return;
            }
            
            Coordinates destination = this.Destination.Location;
            if (destination == this.Location)
            {
                // Already arrived at destination
                return;
            }

            var distanceToDestination = DistanceToDestination(this.Location, destination);

            if (unitsToMove > distanceToDestination)
            {
                this.Location = this.Destination.Location;
            }
            else
            {
                double angle = GetAngleOfLineBetweenTwoPoints(this.Location, this.Destination.Location);

                var newX = this.Location.X + Math.Cos(angle);
                var newY = this.Location.Y + Math.Sin(angle);
                this.Location.X = newX;
                this.Location.Y = newY;
            }


        }

        private static double DistanceToDestination(Coordinates start, Coordinates end)
        {
            var xDiff = end.X - start.X;
            var yDiff = end.Y - start.Y;
            var diff = Math.Sqrt((xDiff * xDiff) + (yDiff * yDiff));

            return diff;
        }

        public static double GetAngleOfLineBetweenTwoPoints(Coordinates start, Coordinates end)
        {
            double xDiff = end.X - start.X;
            double yDiff = end.Y - start.Y;
            return Math.Atan2(yDiff, xDiff) * (180 / Math.PI);
        }
    }
}
