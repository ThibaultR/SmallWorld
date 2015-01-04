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
using UML_SW;

namespace WpfApplication
{
    public partial class MapWindow : Window
    {
        private Game game;

        public MapWindow(Game g)
        {
            InitializeComponent();
            game = g;
            
            playerOneName.Tag = this.game.playerOne.name;
            playerTwoName.Tag = this.game.playerTwo.name;
            playerOnePopulation.Tag = "Population : " + this.game.playerOne.populationType;
            playerTwoPopulation.Tag = "Population : " + this.game.playerTwo.populationType;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ClickStartNewGame(object sender, RoutedEventArgs e)
        {

        }

        private void ClickOpen(object sender, RoutedEventArgs e)
        {

        }

        private void ClickSave(object sender, RoutedEventArgs e)
        {

        }

    }
}
