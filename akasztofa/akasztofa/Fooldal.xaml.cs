using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//buzi felix
namespace akasztofa
{
    /// <summary>
    /// Interaction logic for Fooldal.xaml
    /// </summary>

    public partial class Fooldal : Page
    {
        static public string mainneve = Mainoldal.nev.Trim();
        public string nev;
        static public string teszt;
        static public string kitalalndoszo;
        static public string ide;
        public List<char> eddigitippek = new List<char>();
        public static int tippekszama;
        public static int maxhiba = 8;
        public static int jelenlegihiba = 0;
        public static int nyertb;
        public static int nyertm;
        public static int nyerti;
        public static int nyertk;
        public static int vesztettb;
        public static int vesztettm;
        public static int vesztetti;
        public static int vesztettk;
        public static bool igazevalasz = false;

        class Szo
        {
            public string Alak { get; set; }
            public char Tema { get; set; }
            public Szo(string sor)
            {
                string[] adatok = sor.Split(';');
                Alak = adatok[0];
                Tema = Convert.ToChar(adatok[1]);
            }
        }
        class Jatekos_sajat
        {
            public string Nev { get; set; }
            public int nyertb { get; set; }
            public int vesztettb { get; set; }
            public int nyertm { get; set; }
            public int vesztettm { get; set; }
            public int nyerti { get; set; }
            public int vesztetti { get; set; }
            public int nyertk { get; set; }
            public int vesztettk { get; set; }
            public Jatekos_sajat(string sor)
            {
                string[] adatok = sor.Split(';');
                Nev = adatok[0];
                nyertb = Convert.ToInt32(adatok[1]);
                vesztettb = Convert.ToInt32(adatok[2]);
                nyertm = Convert.ToInt32(adatok[3]);
                vesztettm = Convert.ToInt32(adatok[4]);
                nyerti = Convert.ToInt32(adatok[5]);
                vesztetti = Convert.ToInt32(adatok[6]);
                nyertk = Convert.ToInt32(adatok[7]);
                vesztettk = Convert.ToInt32(adatok[8]);
            }
        }
            public Fooldal()
        {
            InitializeComponent();
            jatekos_betolt();
            asdasd.Content = ($"Eddig tippelt betűk: {string.Join(" ", eddigitippek)}" + $"\nHibák száma: {jelenlegihiba} / {maxhiba}");
            var lista = eredmenyek_beolvasas(mainneve);
            eredmenyek.Text = atalakit(lista.First(), igazevalasz);
            //eredmenyek.Text =teszt;
        }
        private string atalakit(string sor, bool igaze)
        {
            if (igaze)
            {
                string[] adatok = sor.Split(';');
                nev = adatok[0];
                nyertb = Convert.ToInt32(adatok[1]);
                vesztettb = Convert.ToInt32(adatok[2]);
                nyertm = Convert.ToInt32(adatok[3]);
                vesztettm = Convert.ToInt32(adatok[4]);
                nyerti = Convert.ToInt32(adatok[5]);
                vesztetti = Convert.ToInt32(adatok[6]);
                nyertk = Convert.ToInt32(adatok[7]);
                vesztettk = Convert.ToInt32(adatok[8]);
                if (Mainoldal.lenyugozes)
                {
                    nev = "Rózsás Tibor";
                }
                return $"Név: {nev}\nBilógia W: {nyertb}, L: {vesztettb}; Matematika W: {nyertm}, L: {vesztettm}; Informatika W: {nyerti}, L: {vesztetti}; Közmondások W: {nyertk}, L: {vesztettk}";
            }
            else
            {
                string[] adatok = sor.Split(';');
                nev = adatok[0];
                if (Mainoldal.lenyugozes) 
                {
                    nev = "Rózsás Tibor";
                }
                return $"Még nincs eredménye a(z) {nev} nevű játékosnak!";
            }
        }

        private List<string> eredmenyek_beolvasas(string neve)
        {
            List<Jatekos_sajat> eredmenyek = new List<Jatekos_sajat>();
            List<string> eredmenyeksima = new List<string>();
            List<string> eredmenyekv = new List<string>();
            foreach (string sor in File.ReadAllLines("jatekosok.txt"))
            {
                //if (sor.Find(neve) && igazevalasz != true)
                //{
                    eredmenyek.Add(new Jatekos_sajat(sor));
                    eredmenyeksima.Add(sor);

                //}
            }
            var felhasznalo = eredmenyek.Find(x => x.Nev == neve);
            var felhasznalo1 = eredmenyek.FindIndex(x => x.Nev == neve);
            //teszt = eredmenyek.Where(x => x.Nev == neve).First().ToString();
            if (felhasznalo != null)
            {
                eredmenyekv.Add(eredmenyeksima[felhasznalo1].ToString());
                igazevalasz = true;
            }
               
            if (igazevalasz == false && felhasznalo == null)
            {
                eredmenyekv.Add(neve.ToString() + $";{nyertb};{vesztettb};{nyertm};{vesztettm};{nyerti};{vesztetti};{nyertk};{vesztettk}");
            }
            return eredmenyekv;
        }
        public void jatekosok_mentese_sajat(string jatekosneve, int a, int b, int c, int d, int e, int f, int g, int j)
        {
            List<Jatekos_sajat> eredmenyek = new List<Jatekos_sajat>();
            List<string> eredmenyeksima = new List<string>();
            //List<string> eredmenyekv = new List<string>();
            foreach (string sor in File.ReadAllLines("jatekosok.txt"))
            {
                /*if (!sor.Contains(jatekosneve))
                {
                    eredmenyek.Add(sor);
                }*/
                eredmenyek.Add(new Jatekos_sajat(sor));
                eredmenyeksima.Add(sor);
                
            }
            var felhasznalo = eredmenyek.Find(x => x.Nev == jatekosneve);
            var felhasznalo1 = eredmenyek.FindIndex(x => x.Nev == jatekosneve);
            if (felhasznalo != null)
            {
                eredmenyeksima[felhasznalo1] = ($"{jatekosneve};{a};{b};{c};{d};{e};{f};{g};{j}");
            }
            else
            {
                eredmenyeksima.Add($"{jatekosneve};{a};{b};{c};{d};{e};{f};{g};{j}");
            }
            //eredmenyekv.Add($"{jatekosneve};{a};{b};{c};{d};{e};{f};{g};{j}");
            File.WriteAllLines("jatekosok.txt", eredmenyeksima);
        }

        private void jatekos_betolt()
        {
            //Jatek jatek = new Jatek();
            //Jatekos jatekos = jatek.Jatekos;

            List<Szo> szavak = new List<Szo>();
            foreach (string sor in File.ReadAllLines("szavak.txt"))
                szavak.Add(new Szo(sor));

            char tema = Mainoldal.gamemode;

            List<Szo> helyesSzavak = szavak
                .Where(szo => szo.Tema == tema)
                .ToList();

            if (helyesSzavak.Count > 0)
            {
                Random random = new Random();
                int randomSzoIndex = random.Next(0, helyesSzavak.Count - 1);

                Szo kitalalnivalo = helyesSzavak[randomSzoIndex];
                kitalalndoszo = kitalalnivalo.Alak;
                ide = kitalalnivalo.Alak;
                string rejtettSzo = Nincsures(kitalalnivalo.Alak);

                talalnivalo.Text = rejtettSzo /*+ '\n' + kitalalnivalo.Alak*/;
            }
        }

        private string Nincsures(string word)
        {
            StringBuilder rejtettszo = new StringBuilder();

            foreach (char character in word)
            {
                if (char.IsWhiteSpace(character))
                {
                    rejtettszo.Append('\t');
                }
                if (character == ',')
                {
                    rejtettszo.Append(',');
                }
                else
                {
                    rejtettszo.Append('*');
                }
            }

            return rejtettszo.ToString();
        }

        private void BetuVizsgalat(char betu)
        {
            if (eddigitippek.Contains(betu) || eddigitippek.Contains(char.ToUpper(betu)) || eddigitippek.Contains(char.ToLower(betu)))
            {
                MessageBox.Show($"Már próbáltad a(z) {betu} betűt!");
                return;
            }
            else
            {
                eddigitippek.Add(betu);

                if (ide.Contains(betu) || ide.Contains(char.ToUpper(betu)) || ide.Contains(char.ToLower(betu)))
                {
                    MessageBox.Show($"A(z) [{betu}] betű a szó része!");
                    Frissites(betu);
                    Frissites(char.ToLower(betu));
                    Frissites(char.ToUpper(betu));
                }
                else
                {
                    MessageBox.Show("A betű nem a szó része.");
                    tippekszama++;
                    jelenlegihiba++;
                }
            }
            betu_tbox.Clear();
            
        }
        private void Frissites(char betu)
        {
            StringBuilder megjelenitettSzo = new StringBuilder(talalnivalo.Text);
            for (int i = 0; i < ide.Length; i++)
            {
                if (ide[i] == betu)
                {
                    megjelenitettSzo[i] = betu;
                }
            }
            talalnivalo.Text = megjelenitettSzo.ToString();
        }

        private void szo_hajo(object sender, RoutedEventArgs e)
        {
            char tema = Mainoldal.gamemode;
            string szova = szavastipp_tbox.Text;
            if (szova == kitalalndoszo)
            {
                //szavastipp_tbox.Text = "Kitaláltad a szót!";
                switch (tema)
                {
                    case 'm':
                    case 'i':
                    case 'b':
                        MessageBox.Show("Kitaláltad a szót!");
                        break;
                    case 'k':
                        MessageBox.Show("Kitaláltad a mondatot!");
                        break;
                }
                szavastipp_tbox.IsEnabled = false;
                szo_tipp.IsEnabled = false;
                betu_tbox.IsEnabled = false;
                betu_tipp.IsEnabled = false;
                go_back.IsEnabled = true;
                
                 //nyertb,vesztettb,nyertm,vesztettm,nyerti,vesztetti, nyertk, vesztettk
                switch (tema)
                {
                    case 'm':
                        nyertm++;
                        break;
                    case 'i':
                        nyerti++;
                        break;
                    case 'b':
                        nyertb++;
                        break;
                    case 'k':
                        nyertk++;
                        break;
                }
                jatekosok_mentese_sajat(mainneve, nyertb, vesztettb, nyertm, vesztettm, nyerti, vesztetti, nyertk, vesztettk);
                MessageBox.Show("Mentés kész!");
            }
            else
            {
                //szavastipp_tbox.Text = "Ez sajnos nem sikerült!";
                MessageBox.Show($"Ez sajnos nem sikerült!\nA szó: {kitalalndoszo} volt.");
                szavastipp_tbox.IsEnabled = false;
                szo_tipp.IsEnabled = false;
                betu_tbox.IsEnabled = false;
                betu_tipp.IsEnabled = false;
                go_back.IsEnabled = true;

                switch (tema)
                {
                    case 'm':
                        vesztettm++;
                        break;
                    case 'i':
                        vesztetti++;
                        break;
                    case 'b':
                        vesztettb++;
                        break;
                    case 'k':
                        vesztettk++;
                        break;
                }
                jatekosok_mentese_sajat(mainneve, nyertb, vesztettb, nyertm, vesztettm, nyerti, vesztetti, nyertk, vesztettk);
                MessageBox.Show("Mentés kész!");
            }
        }

        private void betu_hajo(object sender, RoutedEventArgs e)
        {
            char betutipp = Convert.ToChar(betu_tbox.Text.FirstOrDefault());

            if (char.IsLetter(betutipp))
            {
                BetuVizsgalat(betutipp);
            }
            else
            {
                MessageBox.Show("Nem talált! / Helytelen karakter.");
            }

            asdasd.Content = ($"Eddig tippelt betűk: {string.Join(" ", eddigitippek)}" + $"\nHibák száma: {jelenlegihiba} / {maxhiba}");

            switch (tippekszama)
            {
                case 1:
                    ImageSource imageSource = new BitmapImage(new Uri("/kepek/2.png", UriKind.RelativeOrAbsolute));
                    mrincredible.Source = imageSource;
                    break;
                case 2:
                    ImageSource imageSource2 = new BitmapImage(new Uri("/kepek/3.png", UriKind.RelativeOrAbsolute));
                    mrincredible.Source = imageSource2;
                    break;
                case 3:
                    ImageSource imageSource3 = new BitmapImage(new Uri("/kepek/4.png", UriKind.RelativeOrAbsolute));
                    mrincredible.Source = imageSource3;
                    break;
                case 4:
                    ImageSource imageSource4 = new BitmapImage(new Uri("/kepek/5.png", UriKind.RelativeOrAbsolute));
                    mrincredible.Source = imageSource4;
                    break;
                case 5:
                    ImageSource imageSource5 = new BitmapImage(new Uri("/kepek/6.png", UriKind.RelativeOrAbsolute));
                    mrincredible.Source = imageSource5;
                    break;
                case 6:
                    ImageSource imageSource6 = new BitmapImage(new Uri("/kepek/7.png", UriKind.RelativeOrAbsolute));
                    mrincredible.Source = imageSource6;
                    break;
                case 7:
                    ImageSource imageSource7 = new BitmapImage(new Uri("/kepek/8.png", UriKind.RelativeOrAbsolute));
                    mrincredible.Source = imageSource7;
                    break;
                case 8:
                    ImageSource imageSource8 = new BitmapImage(new Uri("/kepek/9.png", UriKind.RelativeOrAbsolute));
                    mrincredible.Source = imageSource8;
                betu_tbox.IsEnabled = false;
                betu_tipp.IsEnabled = false;
                betu_tbox.DataContext = tippekszama;
                asdasd.Content = ($"Nem tippelhetsz több betűt!\nKérlek írj be egy szót/mondatot!");
                    break;
            }
        }

        private void visszagomb(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            betu_tbox.Clear();
        }
    }
}
