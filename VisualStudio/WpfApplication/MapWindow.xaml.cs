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
        private int NbUnitP1;
        private int NbUnitP2;

        public MapWindow(Game g)
        {
            game = g;
            
            InitializeComponent();

            playerOneName.Tag = this.game.playerOne.name;
            playerTwoName.Tag = this.game.playerTwo.name;
            playerOnePopulation.Tag = "Population : " + this.game.playerOne.populationType;
            playerTwoPopulation.Tag = "Population : " + this.game.playerTwo.populationType;
            playerOneScore.Tag = "Score : " + this.game.playerOne.currentScore;
            playerTwoScore.Tag = "Score : " + this.game.playerTwo.currentScore;
            NbUnitP1 = this.game.playerOne.nbUnitAlive();
            NbUnitP2 = this.game.playerOne.nbUnitAlive();
            playerOneNbUnit.Tag = "Unit : " + this.NbUnitP1;
            playerTwoNbUnit.Tag = "Unit : " + this.NbUnitP2;

            MapView mv = new MapView(this.game);
            //mv.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            //mv.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            myScrollViewer.Content = mv;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NbUnitP1 = this.game.playerOne.nbUnitAlive();
            NbUnitP2 = this.game.playerOne.nbUnitAlive();
        }



        private void ClickStartNewGame(object sender, RoutedEventArgs e)
        {
            MessageBoxResult newGameBox= MessageBox.Show("Souhaitez-vous enregistrer la partie en cours ?",
                                                            "New Game",
                                                            MessageBoxButton.YesNo);
            if (newGameBox == MessageBoxResult.Yes)
            {
                //TODO Ne fonctionne pas : this.SaveGame();
            }
            else if (newGameBox == MessageBoxResult.No)
            {
                StartWindow createNewGame = new StartWindow();
                createNewGame.OnClickNewGame(null, null);

                createNewGame.Show();
                this.Close();
            }
            
        }

        private void ClickOpen(object sender, RoutedEventArgs e)
        {
            //TODO avec builderSavedGame
        }

        private void ClickSave(object sender, RoutedEventArgs e)
        {
            //TODO a revoir
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
            //TODO a verifier
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
