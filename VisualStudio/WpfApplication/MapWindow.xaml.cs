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
        private List<Polygon> listHexa;
        private Polygon selectedPolygon;

        public MapWindow(Game g)
        {

            Application.Current.MainWindow = this;
            game = g;
            listHexa = new List<Polygon>();

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
            myCanvas.Children.Add(mv);

            int TAILLE = this.game.map.strategy.size;
            double d = Hexagon.w /2 * Math.Tan(30 * Math.PI / 180);
            for (int i = 0; i < TAILLE; i++)
            {
                for (int j = 0; j < TAILLE; j++)
                {
                    double posx = i * Hexagon.w;
                    double posy = j * (Hexagon.h - d)+1;
                    if (j % 2 == 1)
                    {
                        posx += Hexagon.w / 2;
                    }
                    Hexagon h = new Hexagon(posx, posy);
                    h.polygon.MouseEnter += new MouseEventHandler(mouseEnterHandler);
                    h.polygon.MouseLeave += new MouseEventHandler(mouseLeaveHandler);
                    h.polygon.MouseLeftButtonDown += new MouseButtonEventHandler(mouseLeftClickHandler);
                    listHexa.Add(h.polygon);
                    myCanvas.Children.Add(h.polygon);
                }
            }

            showUnit();
        }

        private void mouseEnterHandler(object sender, MouseEventArgs e)
        {
            var polygon = sender as Polygon;
            if (polygon != this.selectedPolygon)
            {
                polygon.StrokeThickness = 4;
                polygon.Stroke = Brushes.White;
                polygon.SetValue(Canvas.ZIndexProperty, 50);
            }
        }

        private void mouseLeaveHandler(object sender, MouseEventArgs e)
        {
            var polygon = sender as Polygon;
            if (polygon != this.selectedPolygon)
            {
                polygon.StrokeThickness = 2;
                polygon.Stroke = Brushes.Black;
                polygon.SetValue(Canvas.ZIndexProperty, 10);
            }
        }

        private void mouseLeftClickHandler(object sender, MouseButtonEventArgs e)
        {
            foreach(Polygon p in this.listHexa){
                p.StrokeThickness = 2;
                p.Stroke = Brushes.Black;
                p.SetValue(Canvas.ZIndexProperty, 10);
            }

            var polygon = sender as Polygon;
            this.selectedPolygon = polygon;
            polygon.StrokeThickness = 4;
            polygon.Stroke = Brushes.Red;
            polygon.SetValue(Canvas.ZIndexProperty, 60);

            //showUnit();

            //MessageBox.Show("Machin : "+ this.listHexa.IndexOf(polygon));
        }

        private void showUnit()
        {
            Player currentPlayer;
            if (this.game.playerOne.playing) { currentPlayer = this.game.playerOne; }
            else { currentPlayer = this.game.playerTwo; }


            panelUnit.Children.Clear();

            ListBox listUnit = new ListBox();

            List<UnitBox> listUnitBox = new List<UnitBox>();
            foreach (Unit u in currentPlayer.units) {
                UnitBox ub = new UnitBox(u);
                //listUnitBox.Add(ub);
                panelUnit.Children.Add(ub);
            }

            //listUnit.ItemsSource = listUnitBox;
            //panelUnit.Children.Add(listUnit);
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

        }
    }
}
