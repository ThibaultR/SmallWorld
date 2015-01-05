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

namespace WpfApplication
{
    /// <summary>
    /// Logique d'interaction pour UnitBox.xaml
    /// </summary>
    public partial class UnitBox : UserControl
    {
        Unit unit;
        


        public UnitBox(Unit u)
        {
            this.unit = u;
            InitializeComponent();

            String typeUnitString = "";
            Type typeUnit = unit.GetType();
            if (typeUnit == typeof(UnitElf)) { typeUnitString = "Elf"; }
            if (typeUnit == typeof(UnitDwarf)) { typeUnitString = "Dwarf"; }
            if (typeUnit == typeof(UnitOrc)) { typeUnitString = "Orc"; }

            UnitType.Tag = "Unité " + typeUnitString;
            UnitAttack.Tag = "Attack : " + Unit.ATTACK;
            UnitDefence.Tag = "Defence : " + Unit.DEFENCE;
            UnitHealth.Tag = "Health : " + unit.healthPoint;
            UnitMvt.Tag = "Movement : " + unit.movementPoint;
            UnitPos.Tag = "Position : (" + unit.coordinate.x + ", " + unit.coordinate.y + ")";


            
            MapWindow parentWindow = (Application.Current.MainWindow as MapWindow);
            bool isOnSelectedTile = parentWindow.listHexa.IndexOf(parentWindow.selectedPolygon) == unit.coordinate.x + unit.coordinate.y * parentWindow.game.map.strategy.size;

            if (!parentWindow.game.isMovementPossible(unit))
            {
                UnitTired.Visibility = System.Windows.Visibility.Visible;
            }
            else if (parentWindow.game.getCurrentPlayer().units.Contains(unit) && isOnSelectedTile)
            {
                border.Margin = new Thickness(10, 2, 50, 2);
            }

            if (!unit.isAlive) //TODO try with binding
            {
                border.Opacity = 0.5;
                UnitTired.Visibility = System.Windows.Visibility.Collapsed;
                UnitAlive.Visibility = System.Windows.Visibility.Visible;
            }

        }
    }
}
