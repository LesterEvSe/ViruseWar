using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

// TODO: Add Results table, Games table
namespace ViruseWar
{
    public partial class StartWindowRules : Window
    {
        public StartWindowRules()
        {
            InitializeComponent();
        }
        private void Hypertext(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "https://ru.wikipedia.org/wiki/%D0%92%D0%BE%D0%B9%D0%BD%D0%B0_%D0%B2%D0%B8%D1%80%D1%83%D1%81%D0%BE%D0%B2",
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(psi);
        }
        private void StartGame(object sender, RoutedEventArgs e)
        {
            MainWindow MWindow = new();
            Close();
            MWindow.Show();
        }
        private void LoadGameClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void ShowResultsClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}