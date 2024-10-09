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
    // Create 100 MyButtons
    public class MyButton : Button
    {
        public int row { get; private set; }
        public int col { get; private set; }

        public MyButton(int _row, int _col) : base()
        {
            row = _row;
            col = _col;
        }
    }
}