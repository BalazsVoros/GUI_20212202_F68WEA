﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NIKTOPIA.Logic.GameLogic;

namespace NIKTOPIA.Logic
{
    internal interface IGameControl
    {
        void Move(Directions direction);
        void ShowOptionsForMining();
        void Interact(int v);
        void RevertFromMining();
        void ShowOptionsForPlacing();
        void RevertFromPlacing();
    }
}
