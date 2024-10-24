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

namespace ViruseWar
{
    public partial class SavedGamesWindow : Window
    {
        private DB db = DB.Instance;

        public SavedGamesWindow()
        {
            InitializeComponent();
            LoadSavedGames();
        }

        private void LoadSavedGames()
        {
            var games = db.GetGamesName();
            if (games == null)
                return;

            foreach (var game in games)
            {
                GameSelection.Items.Add(game);
            }
        }

        private void LoadGame(object sender, RoutedEventArgs e)
        {
            var selectedGame = GameSelection.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedGame))
            {
                MessageBox.Show("Please select a game.");
                return;
            }

            var game = db.GetGameByName(selectedGame);
            if (game == null)
            {
                MessageBox.Show("Failed to load the game.");
            }
            else
            {
                FieldLogic.Restore(game);
                DialogResult = true;
                Close();
            }
        }

        private void DeleteGame(object sender, RoutedEventArgs e)
        {
            var selectedGame = GameSelection.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedGame))
            {
                MessageBox.Show("Please select a game.");
            }
            else if (!db.DeleteGameByName(selectedGame))
            {
                MessageBox.Show("Game doesn't exist and can't be deleted.");
            }
            else
            {
                GameSelection.Items.Remove(selectedGame);
            }
        }
    }
}
