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
                    gameControl.ShowOptionsForMining();
                    break;
                case Key.N:
                    gameControl.RevertFromMining();
                    break;
                case Key.D1:
                    gameControl.Mine(1);
                    break;
                case Key.D2:
                    gameControl.Mine(2);
                    break;
                case Key.D3:
                    gameControl.Mine(3);
                    break;
                case Key.D4:
                    gameControl.Mine(4);
                    break;
                case Key.D5:
                    gameControl.Mine(5);
                    break;
                case Key.D6:
                    gameControl.Mine(6);
                    break;
                case Key.D7:
                    gameControl.Mine(7);
                    break;
                case Key.D8:
                    gameControl.Mine(8);
                    break;
            }
        }

        //public void KeyUp(Key key)
        //{
        //    if (key == Key.M )
        //    {
        //       gameControl.RevertFromMining();
        //    }
        //}
    }
}
