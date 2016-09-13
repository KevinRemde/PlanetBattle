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
    public class GameControlTests
    {
        [TestMethod()]
        public void SetupGameTest()
        {
            Game game = GameControl.SetupGame();
            Assert.IsTrue(game.Players.Count > 0);
            Assert.IsTrue(game.Universe.Planets.Count > 0);
            Assert.IsTrue(game.Universe.Height == 100);
            Assert.IsTrue(game.Universe.Width  == 100);


        }
    }
}