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

            //String typeUnitString = "";
            //Type typeUnit = unit.GetType();
            //if (typeUnit == typeof(UnitElf)) { typeUnitString = "Elf"; }
            //if (typeUnit == typeof(UnitDwarf)) { typeUnitString = "Dwarf"; }
            //if (typeUnit == typeof(UnitOrc)) { typeUnitString = "Orc"; }
            //UnitType.Tag = "Unit " + typeUnitString;
            //if (pW.game.playerOne.units.Contains(unit)) { UnitType.Foreground = Brushes.Blue; }
            //else { UnitType.Foreground = Brushes.Green; }

            //UnitAttack.Tag = "Attack : " + Unit.ATTACK;
            //UnitDefence.Tag = "Defence : " + Unit.DEFENCE;
            //UnitHealth.Tag = "Health : " + unit.healthPoint;
            //UnitMvt.Tag = "Movement : " + unit.movementPoint;
            //UnitPos.Tag = "Position : (" + unit.coordinate.x + ", " + unit.coordinate.y + ")";

            Type typeUnit = unit.GetType();

            UnitType.Tag = pW.selectImageForPlayer(u, true);
            UnitAttack.Tag = Unit.ATTACK;
            UnitDefence.Tag = Unit.DEFENCE;
            UnitHealth.Tag = unit.healthPoint;
            UnitMvt.Tag = unit.movementPoint;
            UnitPos.Tag = "(" + unit.coordinate.x + ", " + unit.coordinate.y + ")";

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

        private void mouseLeftClickUnitHandler(object sender, MouseButtonEventArgs e)
        {
            // Change style of selectedUnit to normal
            foreach (UnitBox ub in pW.panelUnit.Children)
            {
                if (ub.unit == pW.game.currentSelectedUnit)
                {
                    ub.border.BorderThickness = new Thickness(2);
                    ub.border.BorderBrush = Brushes.Gray;
                }
            }
            
            // Change currentSelectedUnit and change style to selected
            var border = sender as Border;
            pW.game.currentSelectedUnit = unit;
            border.BorderThickness = new Thickness(4);
            border.BorderBrush = Brushes.Red;
            
            pW.selectListReachable();
            pW.showPolygon();
        }

    }
}
