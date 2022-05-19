﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIKTOPIA.Logic
{
   public class GameLogic : IGameModel, IGameControl
    {
        public enum GameItem 
        {
            player, dirt, rock, space, bedrock , grass , dirtrock , pillar, mossyStone , gate, gold, coal, caveWall, emerald, platform, blueOre, goldPillar , bluePillar
        }

        public enum Directions
        { 
            up, down, left, right
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

        public void Move(Directions direction)
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
                        i--;
                    }
                    break;
                case Directions.down:
                    if (i + 1 < GameMatrix.GetLength(0))
                    {
                        i++;
                    }
                    break;
                case Directions.left:
                    if (j - 1 >= 0)
                    {
                        j--;
                    }
                    break;
                case Directions.right:
                    if (j + 1 < GameMatrix.GetLength(1))
                    {
                        j++;
                    }
                    break;
                default:
                    break;
            }

            if (GameMatrix[i,j] == GameItem.space)
            {
                GameMatrix[i, j] = GameItem.player;
                GameMatrix[old_i, old_j] = GameItem.space;
            }
           
        }

        private int[] GetPositionOfPlayer()
        {
            for (int i = 0; i < GameMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < GameMatrix.GetLength(1); j++)
                {
                    if (GameMatrix[i,j] == GameItem.player)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return new int[] { -1, -1 };
        }
    }
}
