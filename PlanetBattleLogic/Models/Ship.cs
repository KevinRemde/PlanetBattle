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

        public void Move (double unitsToMove, Planet destinationPlanet)
        {
            if (destinationPlanet== null)
            {
                // No destination set.
                return;
            }
            
            Coordinates destination = destinationPlanet.Location;
            if (destination == this.Location)
            {
                // Already arrived at destination
                return;
            }

            var distanceToDestination = Utilities.DistanceToDestination(this.Location, destination);

            if (unitsToMove > distanceToDestination)
            {
                this.Location = destinationPlanet.Location;
            }
            else
            {
                double angle = Utilities.GetAngleOfLineBetweenTwoPoints
                    (
                    this.Location, 
                    destinationPlanet.Location
                    );
                Coordinates newLocation = Utilities.GetDestinationLocation(this.Location, angle, unitsToMove);
                this.Location = newLocation;
            }
        }


    }
}
