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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace akasztofa
{
    /// <summary>
    /// Interaction logic for Fooldal.xaml
    /// </summary>

    public partial class Fooldal : Page
    {
        //static public string jatekosnevekod;
        //static public bool jatekosnevekodvan = false;
        //static public bool matekbool = false;
        //static public bool biologiabool = false;
        //static public bool informatikabool = false;
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
        private IEnumerable<Jatekos> jatekosok;

        class Szo
        {//itt jonnek be a szavak
            public string Alak { get; set; }
            public char Tema { get; set; }
            public Szo(string sor)
            {
                string[] adatok = sor.Split(';');
                Alak = adatok[0];
                Tema = Convert.ToChar(adatok[1]);
            }
        }
        class Jatekos
        {
            public string Nev { get; set; }
            public int nyertb { get; set; }
            public int vesztettb { get; set; }
            public int nyertm { get; set; }
            public int vesztettm { get; set; }
            public int nyerti { get; set; }
            public int vesztetti { get; set; }
            public Jatekos(string nev)
            {//melyik témakörben nyert, nincs talalat
                Nev = nev;
                nyertb = 0;
                vesztettb = 0;
                nyertm = 0;
                vesztettm = 0;
                nyerti = 0;
                vesztetti = 0;
            }
            public Jatekos(string sor, bool torol)
            {
                string[] adatok = sor.Split(';');
                Nev = adatok[0];
                nyertb = Convert.ToInt32(adatok[1]);
                vesztettb = Convert.ToInt32(adatok[2]);
                nyertm = Convert.ToInt32(adatok[3]);
                vesztettm = Convert.ToInt32(adatok[4]);
                nyerti = Convert.ToInt32(adatok[5]);
                vesztetti = Convert.ToInt32(adatok[6]);
            }

            public string Sorra()
            {
                return $"{Mainoldal.nev};{nyertb};{vesztettb};{nyertm};{vesztettm};{nyerti};{vesztetti}";
            }
            public string[] Eredmenyek()
            {
                string[] eredmenyek = new string[3];
                eredmenyek[0] = $"Biológia témakörben nyert: {nyertb}, vesztett {vesztettb} játékot.";
                eredmenyek[1] = $"Matematika témakörben nyert: {nyertm}, vesztett {vesztettm} játékot.";
                eredmenyek[2] = $"Informatika témakörben nyert: {nyerti}, vesztett {vesztetti} játékot.";
                return eredmenyek;
            }

        }
        class Jatek
        {

            public Jatekos Jatekos { get; private set; }
            private List<Szo> szavak = new List<Szo>();
            public List<Jatekos> jatekosok = new List<Jatekos>();
            public char Tema { get; set; }

            public Szo Szo { get; private set; }
            public char[] Minta { get; private set; }
            public int Kor { get; private set; }
            public int HibaSzam { get; set; }
            public Jatek()
            {
                JatekosokBetoltese();
                SzavakBetoltese();
                Kor = 1;
            }
            public void JatekosokBetoltese()
            {
                foreach (string sor in File.ReadAllLines("jatekosok.txt"))
                    jatekosok.Add(new Jatekos(sor, false));
            }
            public string[] JatekosNevek()
            {
                string[] jatekosnevek = new string[jatekosok.Count];
                int i = 0;
                foreach (Jatekos j in jatekosok) jatekosnevek[i++] = j.Nev;
                return jatekosnevek;
            }
            public void JatekosBelepese(string nev)
            {
                Jatekos = jatekosok.Find(x => x.Nev == nev);
                if (Jatekos == null)
                {
                    Jatekos = new Jatekos(nev);
                    jatekosok.Add(Jatekos);
                }
            }
            public void SzavakBetoltese()
            {
                /*foreach (string sor in File.ReadAllLines("szavak.txt"))
                    szavak.Add(new Szo(sor));*/

            }
            public void SzoValasztas()
            {
                Random vgen = new Random();
                do Szo = szavak[vgen.Next(szavak.Count)]; while (Szo.Tema != Tema);
                Minta = new string('*', Szo.Alak.Length).ToCharArray();
                MessageBox.Show(Convert.ToString(Minta));
            }
            public bool UjKor()
            {
                if (Kor < HibaSzam)
                {
                    Kor++;
                    return true;
                }
                else return false;
               
            }
            public bool BetuVizsgalat(char betu)
            {
                bool talalat = false;
                for (int i = 0; i < Szo.Alak.Length; i++)
                {
                    if (Szo.Alak[i] == betu)
                    {
                        Minta[i] = betu;
                        talalat = true;
                    }
                }
                return talalat;
            }
            

        }
        public Fooldal()
        {
            InitializeComponent();
            jatekos_betolt();
            Jatek jatek = new Jatek();
            eredmenyek.Text = "ide jonnek majd az eredmenyek";
            string mainoldalinev = Mainoldal.nev;
            jatek.JatekosBelepese(mainoldalinev);
            Jatekos jatekos = jatek.Jatekos;
            asdasd.Content = ($"Eddig tippelt betűk: {string.Join(" ", eddigitippek)}" + $"\nHibák száma: {jelenlegihiba} / {maxhiba}");
            if (jatekos != null)
            {
                eredmenyek.Text = string.Join(Environment.NewLine, jatekos.Eredmenyek().Take(3));
            }
            else
            {
                eredmenyek.Text = "Player not found.";
            }

            //jatek.JatekosokMentese();

        }




        private void jatekos_betolt()
        {
            Jatek jatek = new Jatek();
            Jatekos jatekos = jatek.Jatekos;

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
                kitalalndoszo = kitalalnivalo.Alak; // No need to convert to string
                ide = kitalalnivalo.Alak;
                string rejtettSzo = Nincsures(kitalalnivalo.Alak);

                talalnivalo.Text = rejtettSzo + '\n' + kitalalnivalo.Alak;
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
                else
                {
                    rejtettszo.Append('_');
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
            asdasd.Content = ($"Eddig tippelt betűk: {string.Join(" ", eddigitippek)}" + $"\nHibák száma: {jelenlegihiba} / {maxhiba}");
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
                JatekosokMentese();

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
            }
            else
            {
                //szavastipp_tbox.Text = "Ez sajnos nem sikerült!";
                MessageBox.Show("Ez sajnos nem sikerült!");
                szavastipp_tbox.IsEnabled = false;
                szo_tipp.IsEnabled = false;
                betu_tbox.IsEnabled = false;
                betu_tipp.IsEnabled = false;
                JatekosokMentese();

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
            }
        }

        //private bool nemrejtett = false;
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
                    break;
            }
        }
        public void JatekosokMentese()
        {
            List<string> sorok = new List<string>();
            foreach (Jatekos jatekos in jatekosok) sorok.Add(jatekos.Sorra());
            File.WriteAllLines("jatekosok.txt", sorok);
        }

        public string[] Eredmenyek()
        {
            string[] eredmenyek = new string[3];
            eredmenyek[0] = $"Biológia témakörben nyert: {nyertb}, vesztett {vesztettb} játékot.";
            eredmenyek[1] = $"Matematika témakörben nyert: {nyertm}, vesztett {vesztettm} játékot.";
            eredmenyek[2] = $"Informatika témakörben nyert: {nyerti}, vesztett {vesztetti} játékot.";
            return eredmenyek;
        }


    }
}
