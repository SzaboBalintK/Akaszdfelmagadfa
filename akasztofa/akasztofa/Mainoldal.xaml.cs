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

namespace akasztofa
{
    /// <summary>
    /// Interaction logic for Mainoldal.xaml
    /// </summary>
    public partial class Mainoldal : Page
    {
        static public string nev;
        static public bool bioszb = false;
        static public bool infob = false;
        static public bool matekb = false;
        public Mainoldal()
        {
            InitializeComponent();
            biosz.Background = Brushes.WhiteSmoke;
            info.Background = Brushes.WhiteSmoke;
            matek.Background = Brushes.WhiteSmoke;
        }

        private void tovabb_button(object sender, RoutedEventArgs e)
        {
            nev = jatekos_nev.Text;
            //Mainoldal mainoldal = new Mainoldal();
            Fooldal fooldalpage = new Fooldal();
            NavigationService.Navigate(fooldalpage);
        }

        private void boiosz_button(object sender, RoutedEventArgs e)
        {
            bioszb = true;
            infob = false;
            matekb = false;
            biosz.Background = Brushes.LightBlue;
            info.Background = Brushes.WhiteSmoke;
            matek.Background = Brushes.WhiteSmoke;

        }

        private void info_button(object sender, RoutedEventArgs e)
        {
            bioszb = false;
            infob = true;
            matekb = false;
            biosz.Background = Brushes.WhiteSmoke;
            info.Background = Brushes.LightBlue;
            matek.Background = Brushes.WhiteSmoke;
        }

        private void matek_button(object sender, RoutedEventArgs e)
        {
            bioszb = false;
            infob = false;
            matekb = true;
            biosz.Background = Brushes.WhiteSmoke;
            info.Background = Brushes.WhiteSmoke;
            matek.Background = Brushes.LightBlue;
        }
    }
}
