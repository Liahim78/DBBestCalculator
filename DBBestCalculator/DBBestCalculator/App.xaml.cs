using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DBBestCalculator
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void OnStartup(Object sender, StartupEventArgs e)
        {
            Views.CalculatorView view = new Views.CalculatorView();
            view.DataContext = new ViewModels.CalculatorViewModel();
            view.Show();
        }
    }
}
