using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace WpfApplication
{
    /// <summary>
    /// Logique d'interaction pour StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        PeopleCollection people1Collec;
        PeopleCollection people2Collec;
        MapCollection mapCollection;

        public StartWindow()
        {
            this.WindowState = WindowState.Maximized;
            InitializeComponent();

            mapCollection = new MapCollection();
            mapBox.DataContext = mapCollection;
            mapBox.SelectedItem = "Demo";

            people1Collec = new PeopleCollection();
            people1Box.DataContext = people1Collec;
            people1Box.SelectedItem = "Elf";

            people2Collec = new PeopleCollection();
            people2Box.DataContext = people2Collec;
            people2Box.SelectedItem = "Dwarf";
        }

        private void OnChangeMap(object sender, SelectionChangedEventArgs e)
        {
            mapCollection.selected = (string)mapBox.SelectedItem;
        }

        private void OnChangePeople1(object sender, SelectionChangedEventArgs e)
        {
            string value = (string)people1Box.SelectedItem;
            people1Collec.selected = value;
            people2Collec.Remove(value);
        }

        private void OnChangePeople2(object sender, SelectionChangedEventArgs e)
        {
            string value = (string)people2Box.SelectedItem;
            people2Collec.selected = value;
            people2Collec.Remove(value);
        }

        private void OnClickLauncher(object sender, RoutedEventArgs e)
        {
            string name1 = name1Box.Text;
            string name2 = name2Box.Text;

        }

        private void OnClickEnd(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }

    class PeopleCollection : ObservableCollection<string>
    {
        public string selected
        {
            get;
            set;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public PeopleCollection()
        {
            Add("Elf");
            Add("Dwarf");
            Add("Orc");
        }


    }

    class MapCollection : ObservableCollection<string>
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
            Add("Normal");
            Add("Small");
        }
    }

}
