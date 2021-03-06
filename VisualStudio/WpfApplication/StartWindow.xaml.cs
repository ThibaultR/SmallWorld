﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UML_SW;

namespace WpfApplication
{

    public partial class StartWindow : Window
    {
        public GameCreator gameCreator { get; set; }

        public StartWindow()
        {
            InitializeComponent();
            this.gameCreator = new GameCreator();
        }

        public void OnClickNewGame(object sender, RoutedEventArgs e)
        {
            this.Background = new ImageBrush(new BitmapImage(new Uri("textures/backgroundMap.png", UriKind.Relative)));
            Info.VerticalAlignment=System.Windows.VerticalAlignment.Center;

            MapCollection mapCollection = new MapCollection();
            mapBox.DataContext = mapCollection;
            mapBox.SelectedItem = "Demo";

            PopulationCollection populationCollection = new PopulationCollection();
            population1Box.DataContext = populationCollection;
            population1Box.SelectedItem = "Elf";
            population2Box.DataContext = populationCollection;
            population2Box.SelectedItem = "Dwarf";


            builderGrid.Visibility = System.Windows.Visibility.Hidden;
            newGameGrid.Visibility = System.Windows.Visibility.Visible;

        }

        private void OnClickLoadGame(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.DefaultExt = ".sw";
            ofdlg.Filter = "Small world (*.sw)|*.sw|All files (*.*)|*.*";
            Nullable<bool> result = ofdlg.ShowDialog();
            if (result == true)
            {
                this.gameCreator.builderGame = new BuilderSavedGame(ofdlg.FileName);
                this.gameCreator.builderGame.createGame();

                MapWindow mapW = new MapWindow(this.gameCreator.builderGame.game);
                mapW.Show();
                this.Close();
            }
        }

        private void OnChangePopulation1(object sender, SelectionChangedEventArgs e)
        {
            ImagePlayerOne.Tag = selectImageForPlayer((string)population1Box.SelectedItem);

            if ((string)population1Box.SelectedItem == (string)population2Box.SelectedItem)
            {
                errorPop1.Visibility = System.Windows.Visibility.Visible;
            }
            else {
                errorPop1.Visibility = System.Windows.Visibility.Collapsed;
                errorPop2.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void OnChangePopulation2(object sender, SelectionChangedEventArgs e)
        {
            ImagePlayerTwo.Tag = selectImageForPlayer((string)population2Box.SelectedItem);

            if ((string)population1Box.SelectedItem == (string)population2Box.SelectedItem)
            {
                errorPop2.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                errorPop1.Visibility = System.Windows.Visibility.Collapsed;
                errorPop2.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        public void TextBoxFocusHandler(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBoxFocusHandler;
        }

        private void OnClickLauncher(object sender, RoutedEventArgs e)
        {
            string name1 = name1Box.Text;
            string name2 = name2Box.Text;
            
            string population1String = (string) population1Box.SelectedItem;
            string population2String = (string) population2Box.SelectedItem;

            string mapTypeString = (string) mapBox.SelectedItem;

            if (name1 == "" || name2 == "" || population1String == "" || population2String == "" || mapTypeString == "")
            {
                MessageBox.Show("Il manque des informations pour créer la partie !");
            }
            else if (population1String == population2String)
            {
                MessageBox.Show("Vous devez choisir des peuples différents !");
            }
            else if (name1 == name2)
            {
                MessageBox.Show("Vous ne pouvez pas avoir le même nom !");
            }
            else
            {
                FactoryPopulation.populationType population1;
                Enum.TryParse<FactoryPopulation.populationType>(population1String, out population1);
                FactoryPopulation.populationType population2;
                Enum.TryParse<FactoryPopulation.populationType>(population2String, out population2);
                Map.mapType mapType;
                Enum.TryParse<Map.mapType>(mapTypeString, true, out mapType);

                this.gameCreator.builderGame = new BuilderNewGame(name1, name2, population1, population2, mapType);
                this.gameCreator.createGame();

                MapWindow mapW = new MapWindow(this.gameCreator.builderGame.game);
                mapW.Show();
                this.Close();
            }
        }

        public BitmapImage selectImageForPlayer(string c)
        {
            BitmapImage myBitmapImage = new BitmapImage();

            myBitmapImage.BeginInit();
            if (c == "Elf"){
                myBitmapImage.UriSource = new Uri("textures/population/elf_big.png", UriKind.Relative);
            }
            else if (c == "Dwarf")
            {
                myBitmapImage.UriSource = new Uri("textures/population/dwarf_big.png", UriKind.Relative);
            }
            else if (c == "Orc")
            {
                myBitmapImage.UriSource = new Uri("textures/population/orc_big.png", UriKind.Relative);
            }
            myBitmapImage.EndInit();

            return myBitmapImage;
        }


    }

    public class PopulationCollection : ObservableCollection<string>
    {
        public string selected
        {
            get;
            set;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public PopulationCollection()
        {
            Add("Elf");
            Add("Dwarf");
            Add("Orc");
        }

/*        public string SelectedPopulation()
        {
            return (string)this.selected;
        }*/
    }

    public class MapCollection : ObservableCollection<string>
    {
        public string selected
        {
            get;
            set;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public MapCollection()
        {
            Add("Demo");
            Add("Small");
            Add("Normal");
        }
    }

}
