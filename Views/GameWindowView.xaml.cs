﻿using NIKTOPIA.Controllers;
using NIKTOPIA.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace NIKTOPIA.Views
{
    /// <summary>
    /// Interaction logic for GameWindowView.xaml
    /// </summary>
    public partial class GameWindowView : UserControl
    {
        GameController gameController;

        public GameWindowView()
        {
            InitializeComponent();
            GameLogic gameLogic = new GameLogic();
            display.SetupModel(gameLogic);
            gameController = new GameController(gameLogic);
            display.Size = new NIKTOPIA.Misc.Size(Application.Current.MainWindow.ActualWidth, Application.Current.MainWindow.ActualHeight);
            DispatcherTimer dispatcherTimer = new DispatcherTimer(); 
            dispatcherTimer.Tick += Timer_Tick; 
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(20);
            dispatcherTimer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            display.InvalidateVisual();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += Window_KeyDown;
            display.InvalidateVisual();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            gameController.KeyPressed(e.Key);
            display.InvalidateVisual();
        }

        //private void Window_KeyUp(object sender, KeyEventArgs e)
        //{
        //    gameController.KeyUp(e.Key);
        //}

    }
}
