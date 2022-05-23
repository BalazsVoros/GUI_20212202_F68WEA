using NIKTOPIA.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private bool isMining = false;
        private bool isPlacing = false;

        public enum GameItem
        {
            player, dirt, rock, space, bedrock, grass, dirtrock, pillar, mossyStone, gate, gold, coal, caveWall, emerald, platform, blueOre, goldPillar, bluePillar, n1, n2, n3, n4, n5, n6, n7, n8, placingBlock
        }

        public enum Directions
        {
            up, down, left, right, jump
        }

        public GameItem[,] GameMatrix { get; set; }
        public GameItem[,] HelperMatrix { get; set; }

        public int BlockNumber { get; set; }
        public int BlocksMined { get; set; }
        public int BlocksPlaced { get; set; }

        Queue<string> levelsQueue;


        public GameLogic()
        {
            BlockNumber = 0;
            BlocksMined = 0;
            BlocksPlaced = 0;

            levelsQueue = new Queue<string>();

            var lvls = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Levels"), "*.txt");

            foreach (var item in lvls)
            {
                levelsQueue.Enqueue(item);
            }

            LoadNextLevel(levelsQueue.Dequeue());

            HelperMatrix = new GameItem[3, 3];
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
            RevertFromMining();

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
                        if (GameMatrix[i, j - 1] != GameItem.space && j - 1 >= 0)
                        {
                            i--;
                            j--;
                            ClearForLeft(i, j);
                        }
                        else if (GameMatrix[i, j + 1] != GameItem.space && j + 1 < GameMatrix.GetLength(1))
                        {
                            i--;
                            j++;
                            ClearForRight(i, j);
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
                        if (GameMatrix[i + 1, j - 1] == GameItem.space && j - 1 > 0)
                        {
                            i++;
                            j--;
                            ClearForLeft(i,j);
                        }
                        else if (GameMatrix[i + 1, j + 1] == GameItem.space && j + 1 < GameMatrix.GetLength(1))
                        {
                            i++;
                            j++;
                            ClearForRight(i, j);
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
                        while (k < 3 && isMoving == false && GameMatrix[i-1, j] == GameItem.space)
                        {
                            i--;
                            GameMatrix[i, j] = GameItem.player;
                            GameMatrix[i + 1, j] = GameItem.space;
                            await Task.Delay(40);
                            k++;
                        }

                        if (GameMatrix[i + 1, j] == GameItem.space && isMoving == false && !isLeftPressed && !isRightPressed)
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
        public void Interact(int number)
        {
            if (isMining)
            {
                var coordinates = GetPositionOfPlayer();
                int x = coordinates[0];
                int y = coordinates[1];

                switch (number)
                {
                    case 1:
                        GameMatrix[x - 1, y - 1] = GameItem.space;
                        BlockNumber++;
                        BlocksMined++;
                        CheckForGameEnd();
                        break;
                    case 2:
                        GameMatrix[x - 1, y] = GameItem.space;
                        BlockNumber++;
                        BlocksMined++;
                        CheckForGameEnd();
                        break;
                    case 3:
                        GameMatrix[x - 1, y + 1] = GameItem.space;
                        BlockNumber++;
                        BlocksMined++;
                        CheckForGameEnd();
                        break;
                    case 4:
                        GameMatrix[x, y + 1] = GameItem.space;
                        BlockNumber++;
                        BlocksMined++;
                        CheckForGameEnd();
                        break;
                    case 5:
                        GameMatrix[x + 1, y + 1] = GameItem.space;
                        BlockNumber++;
                        BlocksMined++;
                        CheckForGameEnd();
                        break;
                    case 6:
                        GameMatrix[x + 1, y] = GameItem.space;
                        BlockNumber++;
                        BlocksMined++;
                        CheckForGameEnd();
                        break;
                    case 7:
                        GameMatrix[x + 1, y - 1] = GameItem.space;
                        BlockNumber++;
                        BlocksMined++;
                        CheckForGameEnd();
                        break;
                    case 8:
                        GameMatrix[x, y - 1] = GameItem.space;
                        BlockNumber++;
                        BlocksMined++;
                        CheckForGameEnd();
                        break;
                    default:
                        break;
                }
            }
            else if (isPlacing && BlockNumber > 0)
            {
                var coordinates = GetPositionOfPlayer();
                int x = coordinates[0];
                int y = coordinates[1];

                switch (number)
                {
                    case 1:
                        GameMatrix[x - 1, y - 1] = GameItem.placingBlock;
                        BlockNumber--;
                        BlocksPlaced++;
                        CheckForGameEnd();
                        break;
                    case 2:
                        GameMatrix[x - 1, y] = GameItem.placingBlock;
                        BlockNumber--;
                        BlocksPlaced++;
                        CheckForGameEnd();
                        break;
                    case 3:
                        GameMatrix[x - 1, y + 1] = GameItem.placingBlock;
                        BlockNumber--;
                        BlocksPlaced++;
                        CheckForGameEnd();
                        break;
                    case 4:
                        GameMatrix[x, y + 1] = GameItem.placingBlock;
                        BlockNumber--;
                        BlocksPlaced++;
                        CheckForGameEnd();
                        break;
                    case 5:
                        GameMatrix[x + 1, y + 1] = GameItem.placingBlock;
                        BlockNumber--;
                        BlocksPlaced++;
                        CheckForGameEnd();
                        break;
                    case 6:
                        GameMatrix[x + 1, y] = GameItem.placingBlock;
                        BlockNumber--;
                        BlocksPlaced++;
                        CheckForGameEnd();
                        break;
                    case 7:
                        GameMatrix[x + 1, y - 1] = GameItem.placingBlock;
                        BlockNumber--;
                        BlocksPlaced++;
                        CheckForGameEnd();
                        break;
                    case 8:
                        GameMatrix[x, y - 1] = GameItem.placingBlock;
                        BlockNumber--;
                        BlocksPlaced++;
                        CheckForGameEnd();
                        break;
                    default:
                        break;
                }
            }
        }
        public void ShowOptionsForMining()
        {
            if (!isPlacing)
            {
                isMining = true;

                var coordinates = GetPositionOfPlayer();
                int x = coordinates[0];
                int y = coordinates[1];

                int k = 0;
                int l = 0;

                for (int i = x - 1; i <= x + 1; i++)
                {
                    l = 0;
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        HelperMatrix[k, l] = GameMatrix[i, j];
                        l++;
                    }
                    k++;
                }

                if (!CheckForLeftBorder(y))
                {
                    if (GameMatrix[x - 1, y - 1] != GameItem.space)
                    {
                        GameMatrix[x - 1, y - 1] = GameItem.n1;
                    }
                    if (GameMatrix[x, y - 1] != GameItem.space)
                    {
                        GameMatrix[x, y - 1] = GameItem.n8;
                    }
                    if (GameMatrix[x + 1, y - 1] != GameItem.space)
                    {
                        GameMatrix[x + 1, y - 1] = GameItem.n7;
                    }
                }
                if (!CheckForRightBorder(y))
                {
                    if (GameMatrix[x - 1, y + 1] != GameItem.space)
                    {
                        GameMatrix[x - 1, y + 1] = GameItem.n3;
                    }
                    if (GameMatrix[x, y + 1] != GameItem.space)
                    {
                        GameMatrix[x, y + 1] = GameItem.n4;
                    }
                    if (GameMatrix[x + 1, y + 1] != GameItem.space)
                    {
                        GameMatrix[x + 1, y + 1] = GameItem.n5;
                    }
                }
                if (!CheckForTopBorder(x))
                {
                    if (GameMatrix[x - 1, y - 1] != GameItem.space)
                    {
                        GameMatrix[x - 1, y - 1] = GameItem.n1;
                    }
                    if (GameMatrix[x - 1, y] != GameItem.space)
                    {
                        GameMatrix[x - 1, y] = GameItem.n2;
                    }
                    if (GameMatrix[x - 1, y + 1] != GameItem.space)
                    {
                        GameMatrix[x - 1, y + 1] = GameItem.n3;
                    }
                }
                if (!CheckForBottomBorder(x))
                {
                    if (GameMatrix[x + 1, y - 1] != GameItem.space)
                    {
                        GameMatrix[x + 1, y - 1] = GameItem.n7;
                    }
                    if (GameMatrix[x + 1, y] != GameItem.space)
                    {
                        GameMatrix[x + 1, y] = GameItem.n6;
                    }
                    if (GameMatrix[x + 1, y + 1] != GameItem.space)
                    {
                        GameMatrix[x + 1, y + 1] = GameItem.n5;
                    }
                }
            }

        }
        public async void RevertFromMining()
        {
            if (isMining)
            {
                var coordinates = GetPositionOfPlayer();
                int x = coordinates[0];
                int y = coordinates[1];

                int k = 0;
                int l = 0;

                for (int i = x - 1; i <= x + 1; i++)
                {
                    l = 0;
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if (GameMatrix[i, j] != GameItem.space && GameMatrix[i, j] != GameItem.player)
                        {
                            GameMatrix[i, j] = HelperMatrix[k, l];

                        }

                        l++;
                    }
                    k++;
                }

                isMining = false;

                while (GameMatrix[x + 1, y] == GameItem.space)
                {
                    x++;
                    GameMatrix[x, y] = GameItem.player;
                    GameMatrix[x - 1, y] = GameItem.space;
                    await Task.Delay(40);
                }
            }
        }

        public void ShowOptionsForPlacing()
        {
            if (!isMining)
            {
                isPlacing = true;

                var coordinates = GetPositionOfPlayer();
                int x = coordinates[0];
                int y = coordinates[1];

                int k = 0;
                int l = 0;

                for (int i = x - 1; i <= x + 1; i++)
                {
                    l = 0;
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        HelperMatrix[k, l] = GameMatrix[i, j];
                        l++;
                    }
                    k++;
                }

                if (!CheckForLeftBorder(y))
                {
                    if (GameMatrix[x - 1, y - 1] == GameItem.space)
                    {
                        GameMatrix[x - 1, y - 1] = GameItem.n1;
                    }
                    if (GameMatrix[x, y - 1] == GameItem.space)
                    {
                        GameMatrix[x, y - 1] = GameItem.n8;
                    }
                    if (GameMatrix[x + 1, y - 1] == GameItem.space)
                    {
                        GameMatrix[x + 1, y - 1] = GameItem.n7;
                    }
                }
                if (!CheckForRightBorder(y))
                {
                    if (GameMatrix[x - 1, y + 1] == GameItem.space)
                    {
                        GameMatrix[x - 1, y + 1] = GameItem.n3;
                    }
                    if (GameMatrix[x, y + 1] == GameItem.space)
                    {
                        GameMatrix[x, y + 1] = GameItem.n4;
                    }
                    if (GameMatrix[x + 1, y + 1] == GameItem.space)
                    {
                        GameMatrix[x + 1, y + 1] = GameItem.n5;
                    }
                }
                if (!CheckForTopBorder(x))
                {
                    if (GameMatrix[x - 1, y - 1] == GameItem.space)
                    {
                        GameMatrix[x - 1, y - 1] = GameItem.n1;
                    }
                    if (GameMatrix[x - 1, y] == GameItem.space)
                    {
                        GameMatrix[x - 1, y] = GameItem.n2;
                    }
                    if (GameMatrix[x - 1, y + 1] == GameItem.space)
                    {
                        GameMatrix[x - 1, y + 1] = GameItem.n3;
                    }
                }
                if (!CheckForBottomBorder(x))
                {
                    if (GameMatrix[x + 1, y - 1] == GameItem.space)
                    {
                        GameMatrix[x + 1, y - 1] = GameItem.n7;
                    }
                    if (GameMatrix[x + 1, y] == GameItem.space)
                    {
                        GameMatrix[x + 1, y] = GameItem.n6;
                    }
                    if (GameMatrix[x + 1, y + 1] == GameItem.space)
                    {
                        GameMatrix[x + 1, y + 1] = GameItem.n5;
                    }
                }
            }
        }
        public async void RevertFromPlacing()
        {
            if (isPlacing)
            {
                var coordinates = GetPositionOfPlayer();
                int x = coordinates[0];
                int y = coordinates[1];

                int k = 0;
                int l = 0;

                for (int i = x - 1; i <= x + 1; i++)
                {
                    l = 0;
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if (GameMatrix[i, j] != GameItem.placingBlock && GameMatrix[i, j] != GameItem.player)
                        {
                            GameMatrix[i, j] = HelperMatrix[k, l];

                        }

                        l++;
                    }
                    k++;
                }

                isPlacing = false;

                while (GameMatrix[x + 1, y] == GameItem.space)
                {
                    x++;
                    GameMatrix[x, y] = GameItem.player;
                    GameMatrix[x - 1, y] = GameItem.space;
                    await Task.Delay(40);
                }
            }
        }

        private bool CheckForLeftBorder(int j)
        {
            if (j == 0)
            {
                return true;
            }
            return false;
        }
        private bool CheckForRightBorder(int j)
        {
            if (j == GameMatrix.GetLength(1))
            {
                return true;
            }
            return false;
        }
        private bool CheckForTopBorder(int i)
        {
            if (i == 0)
            {
                return true;
            }
            return false;
        }
        private bool CheckForBottomBorder(int i)
        {
            if (i == GameMatrix.GetLength(0))
            {
                return true;
            }
            return false;
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

        private void LoadNextLevel(string path)
        {
            string[] lines = File.ReadAllLines(path);
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

        private void CheckForGameEnd()
        {
            if (BlocksMined >= 10 && BlocksPlaced >= 10)
            {
                RevertFromMining();
                RevertFromPlacing();

                BlocksPlaced = 0;
                BlocksMined = 0;

                if (levelsQueue.Count == 0)
                {
                    Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new MainWindowViewModel();
                }
                else
                {
                    LoadNextLevel(levelsQueue.Dequeue());

                }
            }
        }

    }

}
