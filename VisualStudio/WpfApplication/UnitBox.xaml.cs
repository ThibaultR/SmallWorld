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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UML_SW;
using Wrapper;

namespace WpfApplication
{
    /// <summary>
    /// Logique d'interaction pour UnitBox.xaml
    /// </summary>
    public partial class UnitBox : UserControl
    {
        Unit unit;
        MapWindow pW;


        public UnitBox(Unit u)
        {
            this.unit = u;
            pW = (Application.Current.MainWindow as MapWindow);
            InitializeComponent();

            String typeUnitString = "";
            Type typeUnit = unit.GetType();
            if (typeUnit == typeof(UnitElf)) { typeUnitString = "Elf"; }
            if (typeUnit == typeof(UnitDwarf)) { typeUnitString = "Dwarf"; }
            if (typeUnit == typeof(UnitOrc)) { typeUnitString = "Orc"; }
            UnitType.Tag = "Unité " + typeUnitString;
            if (pW.game.playerOne.units.Contains(unit)) { UnitType.Foreground = Brushes.Blue; }
            else { UnitType.Foreground = Brushes.Green; }

            UnitAttack.Tag = "Attack : " + Unit.ATTACK;
            UnitDefence.Tag = "Defence : " + Unit.DEFENCE;
            UnitHealth.Tag = "Health : " + unit.healthPoint;
            UnitMvt.Tag = "Movement : " + unit.movementPoint;
            UnitPos.Tag = "Position : (" + unit.coordinate.x + ", " + unit.coordinate.y + ")";

            bool isOnSelectedTile = pW.listHexa.IndexOf(pW.selectedPolygon) == unit.coordinate.x + unit.coordinate.y * pW.game.map.strategy.size;
            if (!pW.game.isMovementPossible(unit))
            {
                UnitTired.Visibility = System.Windows.Visibility.Visible;
            }
            else if (pW.game.getCurrentPlayer().units.Contains(unit) && isOnSelectedTile)
            {
                border.Margin = new Thickness(10, 2, 50, 2);
                border.MouseEnter += new MouseEventHandler(mouseEnterUnitHandler);
                border.MouseLeave += new MouseEventHandler(mouseLeaveUnitHandler);
                border.MouseLeftButtonDown += new MouseButtonEventHandler(mouseLeftClickUnitHandler);
            }

            if (unit == pW.game.currentSelectedUnit) {
                border.BorderThickness = new Thickness(4);
                border.BorderBrush = Brushes.Red;
            }

            if (!unit.isAlive) 
            {
                UnitTired.Visibility = System.Windows.Visibility.Collapsed;
                UnitAlive.Visibility = System.Windows.Visibility.Visible;
            }

            if (!unit.isAlive || pW.game.getEnemyPlayer().units.Contains(unit)) //TODO try with binding
            {
                border.Opacity = 0.5;
            }
        }

        private void mouseEnterUnitHandler(object sender, MouseEventArgs e)
        {
            var border = sender as Border;
            if (unit != pW.game.currentSelectedUnit)
            {
                border.BorderThickness = new Thickness(4);
                border.BorderBrush = Brushes.White;
            }
        }

        private void mouseLeaveUnitHandler(object sender, MouseEventArgs e)
        {
            var border = sender as Border;
            if (unit != pW.game.currentSelectedUnit)
            {
                border.BorderThickness = new Thickness(2);
                border.BorderBrush = Brushes.Gray;
            }
        }

        private unsafe void mouseLeftClickUnitHandler(object sender, MouseButtonEventArgs e)
        {
            foreach (UnitBox ub in pW.panelUnit.Children)
            {
                if(ub.unit == pW.game.currentSelectedUnit)
                ub.border.BorderThickness = new Thickness(2);
                ub.border.BorderBrush = Brushes.Gray;
            }

            
            int taille = pW.game.map.strategy.size;
            WrapperAlgo algoW = new WrapperAlgo();

            Unit currentUnit = pW.game.currentSelectedUnit;
            /*int[] fd = pW.game.map.convertMapToIntList().ToArray();
            bool* boolList = algoW.findPossibleMovement(taille, currentUnit.GetType() == typeof(UnitDwarf), currentUnit.coordinate.x, currentUnit.coordinate.y, fd);
            for (int i = 0; i < taille * taille; i++) {
                if (boolList[i]) {
                    Polygon p = pW.listHexa.ElementAt<Polygon>(i);
                    if (p != pW.selectedPolygon)
                    {
                        p.StrokeThickness = 2;
                        p.Stroke = Brushes.Black;
                        p.SetValue(Canvas.ZIndexProperty, 10);
                    }
                }
            }*/


            var border = sender as Border;
            pW.game.currentSelectedUnit = unit;
            border.BorderThickness = new Thickness(4);
            border.BorderBrush = Brushes.Red;

            /*boolList = algoW.findPossibleMovement(taille, unit.GetType() == typeof(UnitDwarf), unit.coordinate.x, unit.coordinate.y, fd);
            for (int i = 0; i < taille * taille; i++)
            {
                if (boolList[i])
                {
                    Polygon p = pW.listHexa.ElementAt<Polygon>(i);
                    if (p != pW.selectedPolygon)
                    {
                        p.StrokeThickness = 3;
                        p.Stroke = Brushes.GreenYellow;
                        p.SetValue(Canvas.ZIndexProperty, 25);
                    }
                }
            }*/
        }


    }
}
