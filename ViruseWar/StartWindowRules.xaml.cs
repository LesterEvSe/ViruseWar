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

namespace ViruseWar
{
    public partial class StartWindowRules : Window
    {
        public StartWindowRules() { InitializeComponent(); }
        private void Hypertxt(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://ru.wikipedia.org/wiki/%D0%92%D0%BE%D0%B9%D0%BD%D0%B0_%D0%B2%D0%B8%D1%80%D1%83%D1%81%D0%BE%D0%B2"));
        }
        private void StartGame(object sender, RoutedEventArgs e)
        {
            MainWindow MWindow = new MainWindow();
            Close();
            MWindow.Show();
        }
    }
}