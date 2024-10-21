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

namespace ViruseWar
{
    // Create 100 MyButtons
    public class MyButton(int row, int col) : Button()
    {
        public int Row { get; private set; } = row;
        public int Col { get; private set; } = col;
    }
}