using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
namespace VWClassLibrary
{
    public enum Player
    {
        EMPTY,
        FIRST,
        SECOND,
        CAPTURED_FIRST,
        CAPTURED_SECOND,
    }
    public class MyColors
    {
        private static Brush first_color = Brushes.DarkViolet;
        private static Brush second_color = Brushes.Turquoise;
        public static Dictionary<Player, Brush> ChooseColor = new Dictionary<Player, Brush>()
        {
            { Player.EMPTY, Brushes.White },
            { Player.FIRST, first_color },
            { Player.SECOND, second_color },
            { Player.CAPTURED_FIRST, first_color },
            { Player.CAPTURED_SECOND, second_color },
        };
    }
}