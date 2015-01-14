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
        public Game game;
        private string saveFile;
        private int NbUnitP1;
        private int NbUnitP2;
        public double canvasHeight;
        public double canvasWidth;
        public List<Polygon> listHexa;
        public Polygon selectedPolygon;
        public List<Polygon> listHexaReachable;
        public bool newGame = false;

        public MapWindow(Game g)
        {
            Application.Current.MainWindow = this;
            game = g;
            int TAILLE = this.game.map.strategy.size;
            double d = Hexagon.w / 2 * Math.Tan(30 * Math.PI / 180);
            canvasHeight = (Hexagon.h - d) * TAILLE + d;
            canvasWidth = Hexagon.w * (TAILLE + 0.5);
            listHexa = new List<Polygon>();
            listHexaReachable = new List<Polygon>();

            InitializeComponent();
            
            // display Map
            myCanvas.Height = this.canvasHeight;
            myCanvas.Width = this.canvasWidth;
            MapView mv = new MapView(this.game);
            myCanvas.Children.Add(mv);

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


            //TO DO : Ne Fonctionne pas très bien 
            nbRoundsLeft.Tag = "Rounds left : " + (this.game.map.strategy.nbRounds - this.game.currentRoundNumber);
            imagePlayer.Source = selectImageForPlayer(this.game.getCurrentPlayer(), false);

            NbUnitP1 = this.game.playerOne.nbUnitAlive();
            NbUnitP2 = this.game.playerTwo.nbUnitAlive();
            showUnit();
            showUnitOnMap();
            showPlayer();
            selectEventSentence(-1);           
        }



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
                this.game.action();
                if (!this.game.isMovementPossible(this.game.currentSelectedUnit))
                {
                    this.game.currentSelectedUnit = null;
                    selectListReachable();
                }
            }

            showPolygon();
            showUnit();
            showUnitOnMap();
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
                newGame = true;
                this.Close();
                
            }
            //return false;
        }

        private void ClickOpen(object sender, RoutedEventArgs e)
        {
            //TODO avec builderSavedGame
        }

        private void ClickSave(object sender, RoutedEventArgs e)
        {
            //TODO a revoir
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".sw";
            dlg.Filter = "Small world (*.sw)|*.sw|All files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                this.saveFile = dlg.FileName;
                this.SaveGame();
            }
        }

        private void ClickHelp(object sender, RoutedEventArgs e)
        {

        }

        private void ClickSaveAs(object sender, RoutedEventArgs e)
        {

        }


        public bool AskSave()
        {
            MessageBoxResult exitBox = MessageBox.Show("Vous nous quittez déjà ? Souhaitez-vous enregistrer votre partie en cours avant de partir ?", "Exit", MessageBoxButton.YesNoCancel);

            if (exitBox == MessageBoxResult.Yes  || exitBox == MessageBoxResult.Cancel)
            {
                return true;
            }
            return false;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (newGame) {
               newGame = false;
               e.Cancel = false;
            }
            else if (AskSave() == true)
            {
                e.Cancel = true;
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


        private void endRoundClickHandler(object sender, RoutedEventArgs e)
        {
            this.game.endRound();

            if (this.game.isEnd()) {
                MessageBox.Show("The game has ended ! The Winner is " + this.game.getWinner().name + " with " + this.game.getWinner().currentScore + " points. Well Played !");
            }

            showUnit();
            imagePlayer.Source = selectImageForPlayer(this.game.getCurrentPlayer(), false);

            showPlayer();

            listHexaReachable.Clear();
            showPolygon();
            selectEventSentence(-1);
        }

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

        private BitmapImage selectImageForPlayer(Player p, bool isSmall){
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
                default:
                    break;
            }

            eventSentence.Content = str;
            
        }

        
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
