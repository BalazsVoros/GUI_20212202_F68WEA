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
                double rectWidth = Size.Width /  gameModel.GameMatrix.GetLength(1);
                double rectHeight = Size.Height / gameModel.GameMatrix.GetLength(0);

                drawingContext.DrawRectangle(Brushes.Transparent, new Pen(Brushes.Transparent, 0),
                    new Rect(0, 0, Size.Width, Size.Height));

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
                            default:
                                break;
                        }

                        drawingContext.DrawRectangle(brush
                                    , new Pen(Brushes.Black, 0),
                                    new Rect(j * rectWidth, i * rectHeight, rectWidth, rectHeight)
                                    );
                        
                    }
                }
            }
        }
    }

    
}
