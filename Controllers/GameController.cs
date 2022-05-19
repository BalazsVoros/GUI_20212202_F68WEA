using NIKTOPIA.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NIKTOPIA.Controllers
{
   internal class GameController
    {
        IGameControl gameControl;

        public GameController(IGameControl gameControl)
        {
            this.gameControl = gameControl;
        }

        public void KeyPressed(Key key)
        {
            switch (key)
            {
                case Key.W:
                    gameControl.Move(GameLogic.Directions.up);
                    break;
                case Key.S:
                    gameControl.Move(GameLogic.Directions.down);
                    break;
                case Key.A:
                    gameControl.Move(GameLogic.Directions.left);
                    break;
                case Key.D:
                    gameControl.Move(GameLogic.Directions.right);
                    break;
                case Key.Space:
                    gameControl.Move(GameLogic.Directions.jump); 
                    break;
                case Key.M:
                    gameControl.Mine();
                    break;
            }
        }
    }
}
