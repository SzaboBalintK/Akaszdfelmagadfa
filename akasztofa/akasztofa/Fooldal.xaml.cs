﻿using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Fooldal.xaml
    /// </summary>
    public partial class Fooldal : Page
    {
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
            public char Tema { get; set; }
            private List<Szo> szavak = new List<Szo>();
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
            private void JatekosokBetoltese()
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
            private void SzavakBetoltese()
            {
                foreach (string sor in File.ReadAllLines("szavak.txt"))
                    szavak.Add(new Szo(sor));
            }
            public void SzoValasztas()
            {
                Random vgen = new Random();
                do Szo = szavak[vgen.Next(szavak.Count)]; while (Szo.Tema != Tema);
                Minta = new string('*', Szo.Alak.Length).ToCharArray();
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
        }
    }
}
