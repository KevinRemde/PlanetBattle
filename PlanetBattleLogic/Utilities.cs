using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetBattleLogic.Models;

namespace PlanetBattleLogic
{
    public static class Utilities
    {
        public static double DistanceToDestination(Coordinates start, Coordinates end)
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

        public static Coordinates GetDestinationLocation(Coordinates startLocation, double angle, double distance)
        {
            angle = Math.PI * angle / 180.0;
            var newX = startLocation.X + (distance * Math.Cos(angle));
            var newY = startLocation.Y + (distance * Math.Sin(angle));
            var endLocation = new Coordinates(newX, newY);

            return endLocation;

        }
    }
}
