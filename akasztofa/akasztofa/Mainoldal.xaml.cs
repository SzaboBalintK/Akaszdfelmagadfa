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
        static public bool mindenb = false;
        static public bool lenyugozes = false;
        public static char gamemode;
        public Mainoldal()
        {
            InitializeComponent();
            biosz.Background = Brushes.WhiteSmoke;
            info.Background = Brushes.WhiteSmoke;
            matek.Background = Brushes.WhiteSmoke;
            hibauzenet.Visibility = Visibility.Hidden;
        }

        private void tovabb_button(object sender, RoutedEventArgs e)
        {
            nev = jatekos_nev.Text.Trim();
            if (mindenb == true && !String.IsNullOrWhiteSpace(nev) && !nev.All(char.IsDigit))
            {
                //csak extra miatt van benn
                if (nev == "Tyborg" || nev == "Tibor" || nev == "Rózsás" || nev == "Terminátor" || nev == "tyborg" || nev == "tibor" || nev == "rózsás" || nev == "terminátor" || nev == "terminator" || nev == "rozsas" || nev == "Terminator" || nev == "Rozsas" )
                {
                    lenyugozes = true;
                }
                hibauzenet.Visibility = Visibility.Hidden;
                nev = jatekos_nev.Text.Trim();
                Fooldal fooldalpage = new Fooldal();
                NavigationService.Navigate(fooldalpage);
            }
            if (String.IsNullOrWhiteSpace(nev))
            {
                hibauzenet.Visibility = Visibility.Visible;
                hibauzenet.Content = "Kérem írjon be egy nevet!";
            }
            if(mindenb == false)
            {
                hibauzenet.Visibility = Visibility.Visible;
                hibauzenet.Content = "Kérem válasszon egy témát!"; 
            }
            if(mindenb == false && String.IsNullOrWhiteSpace(nev) && nev.All(char.IsDigit))
            {
                hibauzenet.Visibility = Visibility.Visible;
                hibauzenet.Content = "Kérem írjon be egy nevet és válasszon egy témát!";
            }
            if (nev.All(char.IsDigit) && !String.IsNullOrWhiteSpace(nev))
            {
                hibauzenet.Visibility = Visibility.Visible;
                hibauzenet.Content = "A név nem tartalmazhat csak számokat!";
            }
        }

        private void boiosz_button(object sender, RoutedEventArgs e)
        {
            mindenb = true;
            biosz.Background = Brushes.LightBlue;
            info.Background = Brushes.WhiteSmoke;
            matek.Background = Brushes.WhiteSmoke;
            mondatok_btn.Background = Brushes.WhiteSmoke;
            gamemode = 'b';

        }

        private void info_button(object sender, RoutedEventArgs e)
        {;
            mindenb = true;
            biosz.Background = Brushes.WhiteSmoke;
            info.Background = Brushes.LightBlue;
            matek.Background = Brushes.WhiteSmoke;
            mondatok_btn.Background = Brushes.WhiteSmoke;
            gamemode = 'i';
        }

        private void matek_button(object sender, RoutedEventArgs e)
        {
            mindenb = true;
            biosz.Background = Brushes.WhiteSmoke;
            info.Background = Brushes.WhiteSmoke;
            matek.Background = Brushes.LightBlue;
            mondatok_btn.Background = Brushes.WhiteSmoke;
            gamemode = 'm';
        }

        private void mondatok_button(object sender, RoutedEventArgs e)
        {
            mindenb = true;
            biosz.Background = Brushes.WhiteSmoke;
            info.Background = Brushes.WhiteSmoke;
            matek.Background = Brushes.WhiteSmoke;
            mondatok_btn.Background = Brushes.LightBlue;
            gamemode = 'k';
        }
    }
}
