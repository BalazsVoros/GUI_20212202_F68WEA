using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static NIKTOPIA.Logic.GameLogic;

namespace NIKTOPIA.Logic
{
    public interface IGameModel 
    {
        GameItem[,] GameMatrix { get; set; }
    }
}
