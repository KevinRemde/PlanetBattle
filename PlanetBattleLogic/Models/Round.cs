﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetBattleLogic.Models
{
    public class Round
    {
        public ICollection<PlayerTurn> PlayerTurns { get; set; }

    }
}