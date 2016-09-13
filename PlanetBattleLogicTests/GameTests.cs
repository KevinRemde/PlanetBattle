using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlanetBattleLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetBattleLogic.Models;

namespace PlanetBattleLogic.Tests
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        public void FightBattleTest()
        {
            var planet = new Planet();
            var ship1 = new Ship();
            var ship2 = new Ship();
            
            var winningShip = GameControl.FightBattle(ship1, ship2);

            Assert.IsInstanceOfType(winningShip, typeof(Ship));
        }
    }
}