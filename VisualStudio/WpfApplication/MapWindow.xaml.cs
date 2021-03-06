﻿using Microsoft.Win32;
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
        public Game game { get; set; }
        public string saveFile { get; set; }
        public double canvasHeight { get; set; }
        public double canvasWidth { get; set; }
        public List<Polygon> listHexa { get; set; }
        public Polygon selectedPolygon { get; set; }
        public List<Polygon> listHexaReachable { get; set; }

        public bool newGame = false;

        public MapWindow(Game g)
        {
            Application.Current.MainWindow = this;
            game = g;
            listHexa = new List<Polygon>();
            listHexaReachable = new List<Polygon>();

            InitializeComponent();

            displayCanvas();

            nbRoundsLeft.Tag = "Rounds left : " + (this.game.map.strategy.nbRounds - this.game.currentRoundNumber);
            selectEventSentence(-1);    

            showUnit();
            showUnitOnMap();
            showPlayer();       
        }


        //*********************************************************************************
        //*                           Begin Hexagon handler
        //*********************************************************************************
        private void mouseEnterHexaHandler(object sender, MouseEventArgs e)
        {
            var polygon = sender as Polygon;
            if (polygon != this.selectedPolygon)
            {
                polygon.StrokeThickness = 4;
                polygon.Stroke = Brushes.White;
                polygon.SetValue(Canvas.ZIndexProperty, 50);
            }
        }


        private void mouseLeaveHexaHandler(object sender, MouseEventArgs e)
        {
            var polygon = sender as Polygon;
            if (polygon != this.selectedPolygon)
            {
                polygon.StrokeThickness = 2;
                polygon.Stroke = Brushes.Black;
                polygon.SetValue(Canvas.ZIndexProperty, 10);
            }
            if (this.listHexaReachable.Contains(polygon)) {
                polygon.StrokeThickness = 3;
                polygon.Stroke = Brushes.GreenYellow;
                polygon.SetValue(Canvas.ZIndexProperty, 25);
            }
        }

        private void mouseLeftClickHexaHandler(object sender, MouseButtonEventArgs e)
        {
            var polygon = sender as Polygon;
            this.selectedPolygon = polygon;
            showPolygon();
            showUnit();
        }

        private void mouseRightClickHexaHandler(object sender, MouseButtonEventArgs e)
        {
            var polygon = sender as Polygon;
            int pos = this.listHexa.IndexOf(polygon);
            int x = pos % this.game.map.strategy.size;
            int y = pos / this.game.map.strategy.size;
            this.game.currentSelectedTileCoordinate = new Coordinate(x,y);
            if (this.game.currentSelectedUnit == null) { 
                //unité non selectionné
                selectEventSentence(0);
            }
            else if (!this.game.isTileReachable(this.game.currentSelectedUnit, this.game.currentSelectedTileCoordinate))
            {
                //tile non reachable
                selectEventSentence(1);
            }
            else if (!this.game.isMovementPossible(this.game.currentSelectedUnit))
            {
                //not enough movement
                selectEventSentence(2);
            }
            else
            {
                int sentence = this.game.action();
                selectEventSentence(sentence);
                if (!this.game.currentSelectedUnit.isAlive) {
                    selectEventSentence(22);
                }


                if (!this.game.isMovementPossible(this.game.currentSelectedUnit))
                {
                    this.game.currentSelectedUnit = null;
                    selectListReachable();
                }
            }
            showPlayer();
            showPolygon();
            showUnit();
            showUnitOnMap();

            if (this.game.isEnd()) 
            {
                endRoundClickHandler(null, null);
            }
        }
        //*********************************************************************************
        //*                           End Hexagon handler
        //*********************************************************************************


        //*********************************************************************************
        //*                           Begin Display Methods
        //*********************************************************************************
        public void displayCanvas() 
        {
            myCanvas.Children.Clear();

            int TAILLE = this.game.map.strategy.size;
            double d = Hexagon.w / 2 * Math.Tan(30 * Math.PI / 180);
            canvasHeight = (Hexagon.h - d) * TAILLE + d;
            canvasWidth = Hexagon.w * (TAILLE + 0.5);

            myCanvas.Height = this.canvasHeight;
            myCanvas.Width = this.canvasWidth;
            MapView mv = new MapView(this.game);
            myCanvas.Children.Add(mv);

            listHexa.Clear();
            for (int j = 0; j < TAILLE; j++)
            {
                for (int i = 0; i < TAILLE; i++)
                {
                    double posx = i * Hexagon.w;
                    double posy = j * (Hexagon.h - d);
                    if (j % 2 == 1)
                    {
                        posx += Hexagon.w / 2;
                    }
                    Hexagon h = new Hexagon(posx, posy);
                    h.polygon.MouseEnter += new MouseEventHandler(mouseEnterHexaHandler);
                    h.polygon.MouseLeave += new MouseEventHandler(mouseLeaveHexaHandler);
                    h.polygon.MouseLeftButtonDown += new MouseButtonEventHandler(mouseLeftClickHexaHandler);
                    h.polygon.MouseRightButtonDown += new MouseButtonEventHandler(mouseRightClickHexaHandler);
                    listHexa.Add(h.polygon);
                    myCanvas.Children.Add(h.polygon);
                }
            }
        }

        public void showPolygon()
        {
            foreach (Polygon p in listHexa) {
                if (p == selectedPolygon) {
                    p.StrokeThickness = 4;
                    p.Stroke = Brushes.Red;
                    p.SetValue(Canvas.ZIndexProperty, 60);
                }
                else if (listHexaReachable.Contains(p))
                {
                    p.StrokeThickness = 3;
                    p.Stroke = Brushes.GreenYellow;
                    p.SetValue(Canvas.ZIndexProperty, 25);
                }
                else {
                    p.StrokeThickness = 2;
                    p.Stroke = Brushes.Black;
                    p.SetValue(Canvas.ZIndexProperty, 10);
                }
            }
        }

        public void showUnit()
        {
            panelUnit.Children.Clear();
            int pos = this.listHexa.IndexOf(selectedPolygon);
            int x = pos % this.game.map.strategy.size;
            int y = pos / this.game.map.strategy.size;
            List<Unit> listEnemyUnits = this.game.selectEnemyUnitsOnCoordinates(new Coordinate(x, y));

            if (listEnemyUnits.Count == 0)
            {
                bool NoSelectedUnit = this.game.currentSelectedUnit == null ;
                foreach (Unit u in this.game.getCurrentPlayer().units)
                {
                    UnitBox ub = new UnitBox(u);
                    panelUnit.Children.Add(ub);
                }
            }
            else {
                foreach (Unit u in listEnemyUnits)
                {
                    UnitBox ub = new UnitBox(u);
                    panelUnit.Children.Add(ub);
                }
            }
        }

        public void showUnitOnMap()
        {
            List<Image> imgToRemove = new List<Image>();
            foreach (UIElement e in myCanvas.Children)
            {
                if (e.GetType() == typeof(Image))
                {
                    imgToRemove.Add((Image)e);
                }
            }

            foreach (Image i in imgToRemove) { 
                myCanvas.Children.Remove(i); 
            }

            double w = Hexagon.w;
            double h = Hexagon.h;

            List<Coordinate> coordinateList = new List<Coordinate>();
            foreach (Unit u in this.game.playerOne.units) {
                if (!coordinateList.Contains(u.coordinate) && u.isAlive) {
                    coordinateList.Add(u.coordinate);

                    double d = w / 2 * Math.Tan(30 * Math.PI / 180);

                    double posx = u.coordinate.x * w;
                    double posy = u.coordinate.y * (h - d);
                    if (u.coordinate.y % 2 == 1)
                    {
                        posx += w / 2;
                    }

                    Image imgP1 = new Image();
                    imgP1.Source = selectImageForPlayer(this.game.playerOne, true);
                    imgP1.Width = w;
                    imgP1.Height = h;
                    imgP1.SetValue(Canvas.ZIndexProperty, 9);
                    imgP1.SetValue(Canvas.LeftProperty, posx);
                    imgP1.SetValue(Canvas.TopProperty, posy);
                    myCanvas.Children.Add(imgP1);     
                }
            }

            foreach (Unit u2 in this.game.playerTwo.units)
            {
                if (!coordinateList.Contains(u2.coordinate) && u2.isAlive)
                {
                    coordinateList.Add(u2.coordinate);

                    double d = w / 2 * Math.Tan(30 * Math.PI / 180);

                    double posx = u2.coordinate.x * w;
                    double posy = u2.coordinate.y * (h - d);
                    if (u2.coordinate.y % 2 == 1)
                    {
                        posx += w / 2;
                    }

                    Image imgP2 = new Image();
                    imgP2.Source = selectImageForPlayer(this.game.playerTwo, true);
                    imgP2.Width = w;
                    imgP2.Height = h;
                    imgP2.SetValue(Canvas.ZIndexProperty, 9);
                    imgP2.SetValue(Canvas.LeftProperty, posx);
                    imgP2.SetValue(Canvas.TopProperty, posy);
                    myCanvas.Children.Add(imgP2);
                }
            }
        }

        public void showPlayer()
        {
            panelPlayer.Children.Clear();
            PlayerBox p1 = new PlayerBox(this.game.playerOne);
            panelPlayer.Children.Add(p1);
            PlayerBox p2 = new PlayerBox(this.game.playerTwo);
            panelPlayer.Children.Add(p2);
        }
        //*********************************************************************************
        //*                           End Display Methods
        //*********************************************************************************


        //*********************************************************************************
        //*                           Begin MenuButton Handler
        //*********************************************************************************
        private void ClickStartNewGame(object sender, RoutedEventArgs e)
        {
            MessageBoxResult newGameBox= MessageBox.Show("Do you want to save the current Game ?",
                                                            "New Game",
                                                            MessageBoxButton.YesNoCancel);
            if (newGameBox == MessageBoxResult.Yes)
            {
                ClickSave(sender,e);
            }
            else if (newGameBox == MessageBoxResult.No)
            {
                StartWindow createNewGame = new StartWindow();
                createNewGame.OnClickNewGame(sender, e);

                createNewGame.Show();
                newGame = true;
                this.Close();
            }
        }

        private void ClickOpen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.DefaultExt = ".sw";
            ofdlg.Filter = "Small world (*.sw)|*.sw|All files (*.*)|*.*";
            Nullable<bool> result = ofdlg.ShowDialog();
            if (result == true)
            {
                LoadGame(ofdlg.FileName);
            }
        }

        private void ClickSave(object sender, RoutedEventArgs e)
        {
            if (this.saveFile == null)
            {
                ClickSaveAs(sender, e);
            }
            else 
            {
                SaveGame();
            }
        }

        private void ClickSaveAs(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfdlg = new SaveFileDialog();
            sfdlg.DefaultExt = ".sw";
            sfdlg.Filter = "Small world (*.sw)|*.sw|All files (*.*)|*.*";
            Nullable<bool> result = sfdlg.ShowDialog();
            if (result == true)
            {
                this.saveFile = sfdlg.FileName;
                this.SaveGame();
            }
        }

        
        //*********************************************************************************
        //*                           End MenuButton Handler
        //*********************************************************************************


        //*********************************************************************************
        //*                           Begin Closing / Saving / Load
        //*********************************************************************************

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (newGame) {
               newGame = false;
               e.Cancel = false;
            }
            else
            {
                MessageBoxResult exitBox = MessageBox.Show("Do you want to save the current game before leaving ?", "Exit", MessageBoxButton.YesNoCancel);
                switch (exitBox) 
                { 
                    case MessageBoxResult.Yes:
                        ClickSave(null, null);
                        break;
                    case MessageBoxResult.No:
                        e.Cancel = false;
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                    default:
                        break;
                }
            }
        }


        private void SaveGame()
        {
            if (this.saveFile != null)
            {
                Stream stream = File.Open(this.saveFile, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this.game);
                stream.Close();
            }
            else
            {
                //TODO : error
            }
        }

        private void LoadGame(string openFile)
        {
            if (openFile != null)
            {
                BuilderSavedGame bsg = new BuilderSavedGame(openFile);
                bsg.createGame();
                this.game = bsg.game;
                this.listHexaReachable.Clear();
                this.selectedPolygon = null;

                displayCanvas();
                showPolygon();
                showUnit();
                showPlayer();
                showUnitOnMap();
                selectEventSentence(-1);
            }
            else
            {
                //TODO : error
            }
        }
        //*********************************************************************************
        //*                           End Closing / Saving /Load
        //*********************************************************************************

        private void endRoundClickHandler(object sender, RoutedEventArgs e)
        {
            if (!this.game.isEnd())
            { 
                this.game.endRound();
            }

            showPlayer();

            if (this.game.isEnd()) {
            string str = "";
                if (this.game.getWinner() == null)
                {
                    str = "The game has ended ! It's a tie, there is no Winner. Well Played !";
                    
                }
                else
                {
                    str = "The game has ended ! The Winner is " + this.game.getWinner().name + " with " + this.game.getWinner().currentScore + " points. Well Played !";
                }

                MessageBoxResult exitBox = MessageBox.Show(str+"\nWould you like to start a new game ?", "End of the Game", MessageBoxButton.YesNo);
                if (exitBox == MessageBoxResult.Yes)
                {
                    ClickStartNewGame(null, null);
                }
                return;
            }

            nbRoundsLeft.Tag = "Rounds left : " + ((int)(this.game.map.strategy.nbRounds - this.game.currentRoundNumber));


            showUnit();
            listHexaReachable.Clear();
            showPolygon();
            selectEventSentence(-1);
        }

        //*********************************************************************************
        //*                           Begin Selector
        //*********************************************************************************
        public unsafe void selectListReachable() {
            listHexaReachable.Clear();

            if(this.game.currentSelectedUnit == null){
                return;
            }

            int taille = this.game.map.strategy.size;
            bool* boolList = this.game.getBoolListReachable(this.game.currentSelectedUnit);
            for (int i = 0; i < taille * taille; i++)
            {
                if (boolList[i])
                {
                    Polygon p = this.listHexa.ElementAt<Polygon>(i);
                    this.listHexaReachable.Add(p);
                }
            }
        }

        public BitmapImage selectImageForPlayer(Player p, bool isSmall){
            BitmapImage myBitmapImage = new BitmapImage();

            myBitmapImage.BeginInit();
            switch (p.populationType)
            {
                case FactoryPopulation.populationType.Elf:
                    if (isSmall)
                    {
                        myBitmapImage.UriSource = new Uri("textures/population/elf.png", UriKind.Relative);
                    }
                    else
                    {
                        myBitmapImage.UriSource = new Uri("textures/population/elf_big.png", UriKind.Relative);
                    }
                    break;
                case FactoryPopulation.populationType.Dwarf: 
                    if (isSmall)
                    {
                        myBitmapImage.UriSource = new Uri("textures/population/dwarf.png", UriKind.Relative);
                    }
                    else
                    {
                        myBitmapImage.UriSource = new Uri("textures/population/dwarf_big.png", UriKind.Relative);
                    }
                    break;
                case FactoryPopulation.populationType.Orc:
                    if (isSmall)
                    {
                        myBitmapImage.UriSource = new Uri("textures/population/orc.png", UriKind.Relative);
                    }
                    else
                    {
                        myBitmapImage.UriSource = new Uri("textures/population/orc_big.png", UriKind.Relative);
                    }
                    break;
                default:
                    break;
            }
            myBitmapImage.EndInit();

            return myBitmapImage;
        }

        public BitmapImage selectImageForPlayer(Unit u, bool isSmall) { 
            Player p;
            if (u.GetType() == this.game.playerOne.units[0].GetType())
                p = this.game.playerOne;
            else
                p = this.game.playerTwo;

            return selectImageForPlayer(p, isSmall);
        }

        public void selectEventSentence(int n) {
            string str = this.game.getCurrentPlayer().name + ", it's your turn !";

            switch (n)
            {
                case 0:
                    str = "You must select an unit first.";
                    break;
                case 1:
                    str = "You must select a reachable tile.";
                    break;
                case 2:
                    str = "The selected unit is tired.";
                    break;
                case 21:
                    str = "Your unit moved to the selected tile.";
                    break;
                case 22:
                    str = "Your unit died in battle.";
                    break;
                case 23:
                    str = "You killed the enemy unit and moved.";
                    break;
                case 25:
                    str = "You attacked but there is still enemies on the tile.";
                    break;
                default:
                    break;
            }

            eventSentence.Text = str;
            
        }
        //*********************************************************************************
        //*                           End Selector
        //*********************************************************************************
        
    }


    public class Hexagon
    {

        public Polygon polygon;
        public const double h = 80;
        public const double w = 69; //TODO 

        public Hexagon(double x, double y)
        {
            polygon = new Polygon();
            polygon.Stroke = Brushes.Black;
            polygon.Fill = Brushes.Transparent;
            polygon.StrokeThickness = 2;

            // d + side + d = h
            double d = w /2 * Math.Tan(30 * Math.PI / 180);

            PointCollection pCollect = new PointCollection();
            Point p1 = new Point(w/2, 0);
            Point p2 = new Point(w, d);
            Point p3 = new Point(w, h-d);
            Point p4 = new Point(w/2, h);
            Point p5 = new Point(0, h-d);
            Point p6 = new Point(0, d);
            pCollect.Add(p1);
            pCollect.Add(p2);
            pCollect.Add(p3);
            pCollect.Add(p4);
            pCollect.Add(p5);
            pCollect.Add(p6);
            polygon.Points = pCollect;

            polygon.SetValue(Canvas.LeftProperty, x);
            polygon.SetValue(Canvas.TopProperty, y);
            polygon.SetValue(Canvas.ZIndexProperty, 10);

        }
    }
}
