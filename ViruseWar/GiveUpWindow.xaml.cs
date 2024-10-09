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
using System.Windows.Shapes;
using VWClassLibrary;

namespace ViruseWar
{
    public partial class GiveUpWindow : Window
    {
        public GiveUpWindow(bool move_first)
        {
            InitializeComponent();
            WhoseWin.Text = move_first ? "The second player won!" : "The first player won!";
            if (move_first) WhoseWin.Foreground = MyColors.ChooseColor[Player.SECOND];
        }
        private void NewGameClick(object sender, RoutedEventArgs e)
        {
            MainWindow MWindow = new MainWindow();
            Close();
            MWindow.Show();
        }
    }
}