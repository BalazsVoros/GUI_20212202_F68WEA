using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NIKTOPIA.ViewModel
{
    public class MainWindowViewModel
    {
        public ICommand NewGameCommand { get; set; }
        public ICommand ContinueGameCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        public MainWindowViewModel()
        {
            NewGameCommand = new RelayCommand(() => StartNewGame());
            ContinueGameCommand = new RelayCommand(() => ContinueGame());
            ExitCommand = new RelayCommand(() => Exit());
        }

        private static void Exit()
        {
            Window window = Application.Current.MainWindow;
            window.Close();
        }

        private static void ContinueGame()
        {
        }

        private static void StartNewGame()
        {
           // Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new GameWindowViewModel();
        }
    }
}