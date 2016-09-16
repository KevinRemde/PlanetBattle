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
            var endPlanetText = "";            
            var startLocation = new Coordinates(this.Location.X, this.Location.Y);
            var startPlanetText = "";
            if (this.CurrentPlanet != null)
            {
                startPlanetText = string.Format(" ({0})", CurrentPlanet.Name);
            }

            if (destinationPlanet == null)
            {
                // No destination set.
                return;
            }

            if (destinationPlanet.Location == this.Location) 
            {
                // Already arrived at destination
                this.CurrentPlanet = destinationPlanet;
                endPlanetText = string.Format(" ({0})", this.CurrentPlanet.Name);
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
                endPlanetText = string.Format(" ({0})", this.CurrentPlanet.Name);
                destinationPlanet.Ships.Add(this);

                var logText = string.Format
                    (
                    "Ship {0} moved from [{1:##0.0}, {2:##0.0}]{3} to [{4:##0.0}, {5:##0.0}]{6}",
                    this.Id, 
                    startLocation.X, 
                    startLocation.Y, 
                    startPlanetText,
                    this.Location.X, 
                    this.Location.Y,
                    endPlanetText
                    );
                destinationPlanet.Game.LogActivity(logText);
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

            var logText1 = string.Format
                (
                    "Ship {0} moved from [{1:##0.0}, {2:##0.0}]{3} to [{4:##0.0}, {5:##0.0}]{6}",
                    this.Id,
                    startLocation.X,
                    startLocation.Y,
                    startPlanetText,
                    this.Location.X,
                    this.Location.Y,
                    endPlanetText
                );
            destinationPlanet.Game.LogActivity(logText1);
        }

        private double GetDistanceToDestination()
        {
            double distance = Utilities.DistanceToDestination
                (this.Location, this.Destination.Location);
            return distance;
        }
    }
}
