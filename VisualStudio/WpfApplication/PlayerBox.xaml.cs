﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UML_SW;

namespace WpfApplication
{
    /// <summary>
    /// Logique d'interaction pour PlayerBox.xaml
    /// </summary>
    public partial class PlayerBox : UserControl
    {
        Player player;
        MapWindow pW;

        public PlayerBox(Player p)
        {
            player = p;
            pW = (Application.Current.MainWindow as MapWindow);

            InitializeComponent();

            playerName.Tag = this.player.name;
            playerPopulation.Tag = "Population : " + this.player.populationType;
            playerScore.Tag = "Score : " + this.player.currentScore;
            playerNbUnit.Tag = "Unit : " + this.player.nbUnitAlive();

            if (player == pW.game.getCurrentPlayer())
            {
                border.BorderThickness = new Thickness(4);
            }

            if (player == pW.game.playerOne)
            {
                border.BorderBrush = Brushes.Blue;
            }
            else {
                border.BorderBrush = Brushes.Green;
            }
        }
    }
}