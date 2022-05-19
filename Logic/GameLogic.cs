using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace NIKTOPIA.Logic
{
    public class GameLogic : IGameModel, IGameControl
    {
        private bool isRightPressed = false;
        private bool isLeftPressed = false;
        private bool isMoving = false;

        public enum GameItem
        {
            player, dirt, rock, space, bedrock , grass , dirtrock , pillar, mossyStone , gate, gold, coal, caveWall, emerald, platform, blueOre, goldPillar , bluePillar
        }

        public enum Directions
        {
            up, down, left, right, jump
        }

        public GameItem[,] GameMatrix { get; set; }

        public GameLogic()
        {
            string[] lines = File.ReadAllLines(Path.Combine("Resources", "gamematrix_level4.txt"));
            GameMatrix = new GameItem[int.Parse(lines[1]), int.Parse(lines[0])];
            for (int i = 0; i < GameMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < GameMatrix.GetLength(1); j++)
                {
                    char character = lines[i + 2][j];
                    GameMatrix[i, j] = ConvertToEnum(character);
                }
            }
        }
        private GameItem ConvertToEnum(char v)
        {
            switch (v)
            {
                case 'p': return GameItem.player;
                case 'd': return GameItem.dirt;
                case 'g': return GameItem.grass;
                case '-': return GameItem.dirtrock;
                case 'r': return GameItem.rock;
                case 's': return GameItem.space;
                case 'b': return GameItem.bedrock;
                case 't': return GameItem.pillar;
                case 'm': return GameItem.mossyStone;
                case 'i': return GameItem.gate;
                case 'w': return GameItem.caveWall;
                case 'c': return GameItem.coal;
                case 'o': return GameItem.gold;
                case 'x': return GameItem.platform;
                case 'z': return GameItem.blueOre;
                case 'e': return GameItem.emerald;
                case 'v': return GameItem.goldPillar;
                case 'j': return GameItem.bluePillar;
                default:
                    return GameItem.space;
            }
        }

        public async void Move(Directions direction)
        {
            var coordinates = GetPositionOfPlayer();
            int i = coordinates[0];
            int j = coordinates[1];
            int old_i = i;
            int old_j = j;
            switch (direction)
            {
                case Directions.up:
                    if (i - 1 >= 0)
                    {
                        isMoving = true;
                        if ( GameMatrix[i, j - 1] != GameItem.space && j - 1 >= 0)
                        {
                            i--;
                            j--;
                        }
                        else if ( GameMatrix[i, j + 1] != GameItem.space && j + 1 < GameMatrix.GetLength(1))
                        {
                            i--;
                            j++;
                        }
                        if (GameMatrix[i, j] == GameItem.space)
                        {
                            GameMatrix[i, j] = GameItem.player;
                            GameMatrix[old_i, old_j] = GameItem.space;

                            if (GameMatrix[i + 1, j] == GameItem.space)
                            {
                                await Task.Delay(40);

                                while (GameMatrix[i + 1, j] == GameItem.space)
                                {
                                    i++;
                                    GameMatrix[i, j] = GameItem.player;
                                    GameMatrix[i - 1, j] = GameItem.space;
                                    await Task.Delay(40);
                                }
                            }
                        }
                        isMoving = false;
                    }
                    break;
                case Directions.down:
                    if (i + 1 < GameMatrix.GetLength(0))
                    {
                        isMoving = true;
                        if ( GameMatrix[i + 1, j - 1] == GameItem.space && j - 1 >= 0)
                        {
                            i++;
                            j--;
                        }
                        else if ( GameMatrix[i + 1, j + 1] == GameItem.space && j + 1 < GameMatrix.GetLength(1))
                        {
                            i++;
                            j++;
                        }
                        if (GameMatrix[i, j] == GameItem.space)
                        {
                            GameMatrix[i, j] = GameItem.player;
                            GameMatrix[old_i, old_j] = GameItem.space;

                            if (GameMatrix[i + 1, j] == GameItem.space)
                            {
                                await Task.Delay(40);

                                while (GameMatrix[i + 1, j] == GameItem.space)
                                {
                                    i++;
                                    GameMatrix[i, j] = GameItem.player;
                                    GameMatrix[i - 1, j] = GameItem.space;
                                    await Task.Delay(40);
                                }
                            }
                        }
                        isMoving = false;
                    }
                    break;
                case Directions.left:
                    if (j - 1 >= 0)
                    {
                        ClearForLeft(i, j);

                        isMoving = true;
                        j--;
                       isLeftPressed = true;
                        if (GameMatrix[i, j] == GameItem.space)
                        {
                            ClearForLeft(i, j);

                            GameMatrix[i, j] = GameItem.player;
                            GameMatrix[old_i, old_j] = GameItem.space;

                            if (GameMatrix[i + 1, j] == GameItem.space)
                            {

                                await Task.Delay(40);

                                ClearForLeft(i, j);

                                while (GameMatrix[i + 1, j] == GameItem.space)
                                {
                                    i++;
                                    GameMatrix[i, j] = GameItem.player;
                                    GameMatrix[i - 1, j] = GameItem.space;
                                    await Task.Delay(40);
                                }
                                ClearForLeft(i, j);
                            }
                        }
                        isLeftPressed = false;
                        isMoving = false;
                    }
                    break;
                case Directions.right:
                    if (j + 1 < GameMatrix.GetLength(1))
                    {
                        ClearForRight(i, j);

                        isMoving = true;
                        j++;
                        isRightPressed = true;
                        if (GameMatrix[i, j] == GameItem.space)
                        {
                            ClearForRight(i, j);

                            GameMatrix[i, j] = GameItem.player;
                            GameMatrix[old_i, old_j] = GameItem.space;

                            if (GameMatrix[i + 1, j] == GameItem.space)
                            {

                                await Task.Delay(40);

                               ClearForRight(i, j);

                                while (GameMatrix[i + 1, j] == GameItem.space)
                                {
                                    i++;
                                    GameMatrix[i, j] = GameItem.player;
                                    GameMatrix[i - 1, j] = GameItem.space;
                                    await Task.Delay(40);
                                }
                                ClearForRight(i, j);
                            }
                        }
                        isRightPressed = false;
                        isMoving = false;
                    }
                    break;
                case Directions.jump:
                    if (i - 3 > 0)
                    {
                        ClearForRight(i, j);
                        ClearForLeft(i, j);

                        int k = 0;
                        while (k < 3 && isMoving == false)
                        {
                            i--;
                            GameMatrix[i, j] = GameItem.player;
                            GameMatrix[i + 1, j] = GameItem.space;
                            await Task.Delay(40);
                            k++;
                        }

                        if (isRightPressed || isLeftPressed)
                        {

                        }
                        if (GameMatrix[i+1, j] == GameItem.space && isMoving == false)
                        {
                            GameMatrix[i, j] = GameItem.player;
                            GameMatrix[old_i, old_j] = GameItem.space;

                            if (GameMatrix[i + 1, j] == GameItem.space)
                            {
                                await Task.Delay(40);

                                while (GameMatrix[i + 1, j] == GameItem.space)
                                {
                                    i++;
                                    GameMatrix[i, j] = GameItem.player;
                                    GameMatrix[i - 1, j] = GameItem.space;
                                    await Task.Delay(40);
                                }
                            }
                        }
                    }
                    else
                    {
                        ClearForRight(i, j);
                        ClearForLeft(i, j);
                        int l = Math.Abs(0 - i);
                        int k = 0;
                        while (k < l && isMoving == false)
                        {
                            i--;
                            GameMatrix[i, j] = GameItem.player;
                            GameMatrix[i + 1, j] = GameItem.space;
                            await Task.Delay(40);
                            k++;
                        }
                        if (GameMatrix[i, j] == GameItem.space)
                        {
                            GameMatrix[i, j] = GameItem.player;
                            GameMatrix[old_i, old_j] = GameItem.space;

                            if (GameMatrix[i + 1, j] == GameItem.space)
                            {
                                await Task.Delay(40);

                                while (GameMatrix[i + 1, j] == GameItem.space)
                                {
                                    i++;
                                    GameMatrix[i, j] = GameItem.player;
                                    GameMatrix[i - 1, j] = GameItem.space;
                                    await Task.Delay(40);
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void Mine() 
        {
            var coordinates = GetPositionOfPlayer();
            int i = coordinates[0];
            int j = coordinates[1];

            int DistanceFromLeftBorder = GetDistanceFromLeftBorder(j);
            int DistanceFromRightBorder = GetDistanceFromRightBorder(j);
            int DistanceFromTopBorder = GetDistanceFromTopBorder(i);
            int DistanceFromBottomBorder = GetDistanceFromBottomBorder(i);

            if (GetDistanceFromLeftBorder(j) >= 2)
            {

            }
            else if (GetDistanceFromLeftBorder(j) >= 1)
            {

            }
        }

        private int GetDistanceFromLeftBorder(int j)
        {
            return Math.Abs(0 - j);
        }
        private int GetDistanceFromRightBorder(int j)
        {
            return GameMatrix.GetLength(1) - j;
        }
        private int GetDistanceFromTopBorder(int i)
        {
            return Math.Abs(0 - i);
        }
        private int GetDistanceFromBottomBorder(int i)
        {
            return GameMatrix.GetLength(0) - i;
        }

        private int[] GetPositionOfPlayer()
        {
            for (int i = 0; i < GameMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < GameMatrix.GetLength(1); j++)
                {
                    if (GameMatrix[i, j] == GameItem.player)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return new int[] { -1, -1 };
        }

        private void ClearForLeft(int x, int y)
        {
            for (int i = 0; i < GameMatrix.GetLength(0); i++)
            {
                for (int j = y + 1; j < GameMatrix.GetLength(1); j++)
                {
                    if (GameMatrix[i, j] == GameItem.player)
                    {
                        GameMatrix[i, j] = GameItem.space;
                    }
                }
            }
        }
        private void ClearForRight(int x, int y)
        {
            for (int i = 0; i < GameMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (GameMatrix[i, j] == GameItem.player)
                    {
                        GameMatrix[i, j] = GameItem.space;

                    }
                }
            }
        }
    }

}
