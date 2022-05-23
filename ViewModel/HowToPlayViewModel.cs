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
    class HowToPlayViewModel
    {
        public ICommand GoBackCommand { get; set; }

        public HowToPlayViewModel()
        {
            GoBackCommand = new RelayCommand(() => GoBack());
        }

        private static void GoBack()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new MainWindowViewModel();
        }
    }
}
