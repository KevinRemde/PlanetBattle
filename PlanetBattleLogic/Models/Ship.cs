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
        public Planet CurrentPlanet { get; set; } 

        public void Move (double unitsToMove, Planet destinationPlanet)
        {
            if (destinationPlanet == null)
            {
                // No destination set.
                return;
            }

            if (destinationPlanet.Location == this.Location) 
            {
                // Already arrived at destination
                return;
            }

            this.Destination = destinationPlanet;

            double distanceToDestination = this.GetDistanceToDestination();
            if (distanceToDestination <= unitsToMove)
            {
                // Move is farther than destination planet.
                // Stop at planet
                this.Location = destinationPlanet.Location;
                this.Destination = null;
                this.CurrentPlanet = destinationPlanet;
                destinationPlanet.Ships.Add(this);
                return;
            }

            Coordinates destination = destinationPlanet.Location;

            double angle = Utilities.GetAngleOfLineBetweenTwoPoints
                (
                this.Location,
                destinationPlanet.Location
                );
            Coordinates newLocation = Utilities.GetDestinationLocation(this.Location, angle, unitsToMove);
            this.Location = newLocation;
            this.CurrentPlanet = null;
        }

        private double GetDistanceToDestination()
        {
            double distance = Utilities.DistanceToDestination
                (this.Location, this.Destination.Location);
            return distance;
        }
    }
}
