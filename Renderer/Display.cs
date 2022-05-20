using NIKTOPIA.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NIKTOPIA.Renderer
{
    public class Display : FrameworkElement
    {
        IGameModel gameModel;
        public NIKTOPIA.Misc.Size Size { get; set; }
        //public void Resize(NIKTOPIA.Misc.Size size)
        //{
        //    this.Size = size;
        //}

        public void SetupModel(IGameModel gameModel)
        {
            this.gameModel = gameModel;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (gameModel != null && Size.Width > 50 && Size.Height > 50)
            {
                double rectWidth = Size.Width / gameModel.GameMatrix.GetLength(1);
                double rectHeight = Size.Height / gameModel.GameMatrix.GetLength(0);

                drawingContext.DrawRectangle(Brushes.Transparent, new Pen(Brushes.Transparent, 0),
                    new Rect(0, 0, rectHeight, rectHeight));

                for (int i = 0; i < gameModel.GameMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < gameModel.GameMatrix.GetLength(1); j++)
                    {
                        ImageBrush brush = new ImageBrush();
                        switch (gameModel.GameMatrix[i, j])
                        {
                            case GameLogic.GameItem.player:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "character_tryout.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.dirt:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "dirt.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.grass:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "dirt_grass.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.dirtrock:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "stonedirt.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.rock:
                                brush = new ImageBrush
                                  (new BitmapImage(new Uri(Path.Combine("Resources", "stone.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.bedrock:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "bedrock.jpg"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.pillar:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "mossy_stone_2.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.mossyStone:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "mossy_stone.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.gate:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "gate.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.caveWall:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "cave_wall.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.coal:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "coal.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.gold:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "gold.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.platform:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "bossplatform.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.blueOre:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "bossore.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.emerald:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "emerald.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.goldPillar:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "goldpillar.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.bluePillar:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "bluepillar.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.n1:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "number1.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.n2:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "number2.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.n3:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "number3.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.n4:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "number4.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.n5:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "number5.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.n6:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "number6.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.n7:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "number7.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameLogic.GameItem.n8:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Resources", "number8.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            default:
                                break;
                        }

                        drawingContext.DrawRectangle(brush
                                    , new Pen(Brushes.Transparent, 0),
                                    new Rect(j * rectWidth, i * rectHeight, rectWidth, rectHeight)
                                    );
                        
                    }
                }
            }
        }
    }

    
}
