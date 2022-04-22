using System;
using System.Collections.Generic;
using System.Linq;
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
