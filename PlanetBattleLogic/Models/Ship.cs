using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetBattleLogic.Models
{
    public class Ship
    {
        public Ship()
        {
        }
        public Ship(int id, Player owner, Coordinates location)
        {
            Id = id;
            Owner = owner;
            Location = location;
        }
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

            var distanceToDestination = Utilities.DistanceToDestination(this.Location, destination);

            if (unitsToMove > distanceToDestination)
            {
                this.Location = this.Destination.Location;
            }
            else
            {
                double angle = Utilities.GetAngleOfLineBetweenTwoPoints(this.Location, this.Destination.Location);
                Coordinates newLocation = Utilities.GetDestinationLocation(this.Location, angle, unitsToMove);

                //var newX = this.Location.X + Math.Cos(angle);
                //var newY = this.Location.Y + Math.Sin(angle);
                //this.Location.X = newX;
                //this.Location.Y = newY;
            }


        }


    }
}
