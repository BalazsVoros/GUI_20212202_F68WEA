using System;
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
        void Mine();
        void openInventory();
    }
}
