using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        private string saveFile;

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
            MessageBoxResult newGameBox= MessageBox.Show("Souhaitez-vous enregistrer la partie en cours ?",
                                                            "New Game",
                                                            MessageBoxButton.YesNo);
            if (newGameBox == MessageBoxResult.Yes)
            {
                //Ne fonctionne pas : this.SaveGame();
            }
            else if (newGameBox == MessageBoxResult.No)
            {
                StartWindow createNewGame = new StartWindow();
                createNewGame.builderGrid.Visibility = System.Windows.Visibility.Hidden;
                createNewGame.newGameGrid.Visibility = System.Windows.Visibility.Visible;
                createNewGame.Show();
                this.Close();
            }
            
        }

        private void ClickOpen(object sender, RoutedEventArgs e)
        {

        }

        private void ClickSave(object sender, RoutedEventArgs e)
        {
            //Solution Chaigno, à revoir (mais j'en avais marre, mes trucs ne fonctionnaient pas)
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".sav";
            dlg.Filter = "Saved game (*.sav)|*.sav|All files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                this.saveFile = dlg.FileName;
                this.SaveGame();
            }
        }

        private void ClickExit(object sender, RoutedEventArgs e)
        {
            MessageBoxResult exitBox= MessageBox.Show("Vous nous quittez déjà ?","Exit",MessageBoxButton.YesNo);
            if (exitBox == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void SaveGame()
        {
            if (this.saveFile == null)
            {

            }
            else
            {
                Stream stream = File.Open(this.saveFile, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this.game);
                stream.Close();
            }
        }
    }
}
