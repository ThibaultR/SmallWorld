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
        Thickness BorderMargin;


        public UnitBox(Unit unit)
        {

            MapWindow parentWindow = (Application.Current.MainWindow as MapWindow);
            if (parentWindow.game.isMovementPossible(unit))
            {
                BorderMargin = new Thickness(10, 2, 50, 2);
            }
            else
            {
                BorderMargin = new Thickness(50, 2, 10, 2);
            }

            InitializeComponent();


            UnitType.Tag = "Unité " + unit.GetType();
            UnitAttack.Tag = "Attack : " + Unit.ATTACK;
            UnitDefence.Tag = "Defence : " + Unit.DEFENCE;
            UnitHealth.Tag = "Health : " + unit.healthPoint;
            UnitMvt.Tag = "Movement : " + unit.movementPoint;
            UnitPos.Tag = "Position : (" + unit.coordinate.x + ", " + unit.coordinate.y + ")";
            border.Tag = BorderMargin;

            if (!unit.isAlive) //TODO try with binding
            {
                border.Opacity = 0.5;
                UnitAlive.Visibility = System.Windows.Visibility.Visible;
            }

        }
    }
}
