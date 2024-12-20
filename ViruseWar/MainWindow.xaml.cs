﻿using System;
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

using System.Linq;

namespace ViruseWar
{
    // our window
    public partial class MainWindow : Window
    {
        // the default constant is static
        private const int m_dimension = 10;
        private bool move_first = true;
        private static readonly MyButton[,] ButtonsField = new MyButton[m_dimension, m_dimension];
        private DB db = DB.Instance;

        public MainWindow()
        {
            // For Binding, we can delete this
            DataContext = this;
            
            // Create objects on field
            InitializeComponent();

            GiveUp.BorderBrush = GiveUp.Foreground = MyColors.ChooseColor[Player.FIRST];
            for (int i = 0; i < m_dimension; ++i)
            {
                StackPanel stackPanel = new()
                {
                    Orientation = Orientation.Horizontal
                };
                for (int j = 0; j < m_dimension; ++j)
                {
                    MyButton button = new(i, j)
                    {
                        BorderBrush = Brushes.Black,
                        Background = MyColors.ChooseColor[Player.EMPTY],
                        Height = 50,
                        Width = 50,

                        FontWeight = FontWeights.Bold,
                        FontSize = 30
                    };
                    stackPanel.Children.Add(button);
                    button.Click += ButtonClick;
                    ButtonsField[i, j] = button;
                }
                MainPanel.Children.Add(stackPanel);
            }
            ButtonsField[0, m_dimension - 1].Foreground = MyColors.ChooseColor[Player.SECOND];
            ButtonsField[0, m_dimension - 1].Content = "O";
            ButtonsField[m_dimension - 1, 0].Foreground = MyColors.ChooseColor[Player.FIRST];
            ButtonsField[m_dimension - 1, 0].Content = "X";
            RedrawField();
        }
        private static void RedrawField()
        {
            var m_field = FieldLogic.Instance;
            for (int i = 0; i < m_dimension; ++i)
                for (int j = 0; j < m_dimension; ++j)
                {
                    if (m_field[i, j] == Player.FIRST || m_field[i, j] == Player.SECOND)
                    {
                        ButtonsField[i, j].Background = MyColors.ChooseColor[Player.EMPTY];

                        ButtonsField[i, j].Foreground = MyColors.ChooseColor[m_field[i, j]];

                        ButtonsField[i, j].Content = (m_field[i, j] == Player.FIRST) ? "X" : "O";
                        continue;
                    }
                    ButtonsField[i, j].Background = MyColors.ChooseColor[m_field[i, j]];
                    ButtonsField[i, j].Content = "";
                }

        }
        private static int NumOfCalls = 0;
        // Methods
        // sender - returns the pressed button
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is not MyButton button)
                throw new NullReferenceException();

            Player recolor = FieldLogic.CheckCell(button.Row, button.Col, move_first);
            if (recolor == Player.EMPTY)
                return;

            if (++NumOfCalls >= 3) {
                NumOfCalls = 0;
                move_first = !move_first;
                PlayerTurn.Text = move_first ? "Player 1's turn" : "Player 2's turn";
                GiveUp.BorderBrush = GiveUp.Foreground =
                move_first ? MyColors.ChooseColor[Player.FIRST] : MyColors.ChooseColor[Player.SECOND];

            }

            if (recolor == Player.FIRST || recolor == Player.SECOND) {
                button.Foreground = MyColors.ChooseColor[recolor];
                button.Content = (recolor == Player.FIRST) ? "X" : "O";
            } else {
                button.Background = MyColors.ChooseColor[recolor];
                button.Content = "";
            }
        }

        private static readonly Caretaker caretaker = new();
        // Key Interaction
        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            // Saves data
            if (e.Key == Key.S)
            {
                InputBox dialog = new();
                if (dialog.ShowDialog() == false)
                    return;

                string? saveName = dialog.SaveName;
                if (saveName == null)
                {
                    MessageBox.Show("Something went wrong. Name doesn't save.");
                    return;
                }
                db.AddGameToSlot(saveName + (move_first ? "1" : "2"), FieldLogic.Serialize());

                if (caretaker.Backup(move_first))
                    MessageBox.Show(move_first ? $"Player 1's successfully saved with name {saveName}" : $"Player 2's successfully saved with name {saveName}");
                else
                    MessageBox.Show(move_first ? "The 1st player can no longer save" : "The player can no longer save");
            }

            // Restores saved data
            if (e.Key == Key.R)
            {
                if (caretaker.RestoreField(move_first))
                    RedrawField();
                else
                    MessageBox.Show("You don't have a saves");
            }
        }

        private void ButtonGiveUp(object sender, RoutedEventArgs e)
        {
            GiveUpWindow GUwindow = new(move_first);
            Close();
            GUwindow.Show();
        }
    }
}