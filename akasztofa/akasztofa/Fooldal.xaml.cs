using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
            public int BNy { get; set; }
            public int BV { get; set; }
            public int MNy { get; set; }
            public int MV { get; set; }
            public int INy { get; set; }
            public int IV { get; set; }
            public Jatekos(string nev)
            {//melyik témakörben nyert, nincs talalat
                Nev = nev;
                BNy = 0;
                BV = 0;
                MNy = 0;
                MV = 0;
                INy = 0;
                IV = 0;
            }
            public Jatekos(string sor, bool torol)
            {
                string[] adatok = sor.Split(';');
                Nev = adatok[0];
                BNy = Convert.ToInt32(adatok[1]);
                BV = Convert.ToInt32(adatok[2]);
                MNy = Convert.ToInt32(adatok[3]);
                MV = Convert.ToInt32(adatok[4]);
                INy = Convert.ToInt32(adatok[5]);
                IV = Convert.ToInt32(adatok[6]);
            }
            public string Sorra()
            {
                return $"{Nev};{BNy};{BV};{MNy};{MV};{INy};{IV}";
            }
            public string[] Eredmenyek()
            {
                string[] eredmenyek = new string[3];
                eredmenyek[0] = $"Biológia témakörben nyert: {BNy}, vesztett {BV} játékot.";
                eredmenyek[1] = $"Matematika témakörben nyert: {MNy}, vesztett {MV} játékot.";
                eredmenyek[2] = $"Informatika témakörben nyert: {INy}, vesztett {IV} játékot.";
                return eredmenyek;
            }
        }
        class Jatek
        {

            public Jatekos Jatekos { get; private set; }
            private List<Jatekos> jatekosok = new List<Jatekos>();
            private List<Szo> szavak = new List<Szo>();
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
            public bool Eredmeny(string megfejtes)
            {
                bool nyert = megfejtes == Szo.Alak;
                int x = 0;
                if (Tema == 'b') x = nyert ? Jatekos.BNy++ : Jatekos.BV++;
                else if (Tema == 'm') x = nyert ? Jatekos.MNy++ : Jatekos.MV++;
                else x = nyert ? Jatekos.INy++ : Jatekos.IV++;
                return nyert;
            }
            public void JatekosokMentese()
            {
                List<string> sorok = new List<string>();
                foreach (Jatekos jatekos in jatekosok) sorok.Add(jatekos.Sorra());
                File.WriteAllLines("jatekosok.txt", sorok);
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

        private void Nev_ok(object sender, RoutedEventArgs e)
        {
            //jatekosnevekod = jatekos_nev.Text;
            //jatekosnevekodvan = true;


        }


        private void jatekos_betolt()
{
    Jatek jatek = new Jatek();
    Jatekos jatekos = jatek.Jatekos;

    List<Szo> szavak = new List<Szo>();
    foreach (string sor in File.ReadAllLines("szavak.txt"))
        szavak.Add(new Szo(sor));

    char tema = Mainoldal.gamemode;

    List<Szo> helyesSzavak = szavak.Where(szo => szo.Tema == tema).ToList();

    if (helyesSzavak.Count > 0)
    {
        Random random = new Random();
        int randomSzoIndex = random.Next(0, helyesSzavak.Count - 1);

        Szo kitalalnivalo = helyesSzavak[randomSzoIndex];
        kitalalndoszo = Convert.ToString(kitalalnivalo.Alak).Trim();
        ide = kitalalnivalo.Alak;
        string rejtettSzo = new string('*', kitalalnivalo.Alak.Length);

                talalnivalo.Text = rejtettSzo + '\n' + kitalalnivalo.Alak;
            }
}


        private void BetuVizsgalat(char betu)
        {
            if (eddigitippek.Contains(betu))
            {
                MessageBox.Show($"Már próbáltad a(z) {betu} betűt!");
                return;
            }
            else
            {
                eddigitippek.Add(betu);

                if (ide.Contains(betu))
                {
                    MessageBox.Show($"A(z) [{betu}] betű a szó része!");
                    UpdateDisplayedWord(betu);
                    tippekszama++;
                }
                else
                {
                    MessageBox.Show("A betű nem a szó része.");
                    tippekszama++;
                }
            }
        }
        private void UpdateDisplayedWord(char betu)
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
            string szova = szavastipp_tbox.Text.Trim();
            if (szova == kitalalndoszo)
            {
                szavastipp_tbox.Text = "Kitaláltad a szót!";
                szavastipp_tbox.IsEnabled = false;
            }
            else
            {
                szavastipp_tbox.Text = "Ez sajnos nem sikerült!";
                szavastipp_tbox.IsEnabled = false;
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

            asdasd.Content = ($"Eddig tippelt betűk: {string.Join(" ", eddigitippek)}");
        }
    }
}
