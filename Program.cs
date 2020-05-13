using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace _2018OktEmeltLezerloveszet
{
    class JatekosLovese
    {
        /*28,95;31,60
        Ricsi;26,99;33,00*/
        public string JatekosNeve;
        public double XKoor;
        public double YKoor;
        public int Sorszam;
        public double XKozep;
        public double YKozep;

        public JatekosLovese(string jatekosNeve, double xKoor, double yKoor, int sorszam, double xKozep, double yKozep)
        {
            JatekosNeve = jatekosNeve;
            XKoor = xKoor;
            YKoor = yKoor;
            Sorszam = sorszam;
            XKozep = xKozep;
            YKozep = yKozep;
        }

        public double Tavolsag()
        {
            double dx = Math.Pow(Convert.ToDouble(XKozep) - Convert.ToDouble(XKoor), 2);
            double dy = Math.Pow(Convert.ToDouble(YKozep) - Convert.ToDouble(YKoor), 2);
            return Math.Sqrt(dx + dy);
        }

        public double Pontszam()
        {
            double tav = (10 - Tavolsag());
            if (tav < 0)
                return 0;
            else
                return Math.Round(tav, 2);
        }
    }
    class Program
    {
        static List<JatekosLovese> Adat = new List<JatekosLovese>();
        static JatekosLovese jatekos;
        static SortedDictionary<string, int> adatok = new SortedDictionary<string, int>();
        static double maxAtlag;
        static string maxNev;
        static void Main(string[] args)
        {
            F2();
            F5();
            F7();
            F9();
            F10();
            F11();
            F12();
            F13();
            Console.ReadKey();
        }

        private static void F13() => Console.WriteLine($"13. feladat: A játék nyertese: {maxNev}");

        private static void F12()
        {
            Console.WriteLine("12. feladat: Átlagpontszámok:");
            SortedDictionary<string, double> atlagok = new SortedDictionary<string, double>();
            var Pontszamok = new SortedDictionary<string, double>();
            foreach (JatekosLovese jatekosok in Adat)
            {
                if (Pontszamok.ContainsKey(jatekosok.JatekosNeve))
                    Pontszamok[jatekosok.JatekosNeve] += jatekosok.Pontszam();
                else
                    Pontszamok[jatekosok.JatekosNeve] = jatekosok.Pontszam();
            }
            foreach (KeyValuePair<string, int> item in adatok)
            {
                if (!atlagok.ContainsKey(item.Key))
                {
                    double atlag = Pontszamok[item.Key] / item.Value;
                    atlagok.Add(item.Key, atlag);
                }
            }
            foreach (var jatekos in atlagok)
            {
                Console.WriteLine("\t" + jatekos.Key + " - " + jatekos.Value);

                if (maxAtlag < jatekos.Value)
                {
                    maxAtlag = jatekos.Value;
                    maxNev = jatekos.Key;
                }
            }
        }

        private static void F11()
        {
            for (int i = 0; i < Adat.Count; i++)
            {
                if (adatok.ContainsKey(Adat[i].JatekosNeve))
                    adatok[Adat[i].JatekosNeve]++;
                else
                    adatok[Adat[i].JatekosNeve] = 1;
            }
            Console.WriteLine("11. feladat: Lövések száma");
            foreach (var db in adatok)
            {
                Console.WriteLine($"\t{db.Key} - {db.Value} db");
            }
        }

        private static void F10() => Console.WriteLine($"10. feladat: Játékosok száma: {Adat.Select(x => x.JatekosNeve).Distinct().Count()}");

        private static void F9() => Console.WriteLine($"9. feladat: Nulla pontos lövések száma: {Adat.Where(x => x.Pontszam() == 0).Count()} db");

        private static void F7()
        {
            Console.WriteLine("7. feladat: Legpontosabb lövés");
            double min = Adat[0].Tavolsag();
            jatekos = Adat[0];
            for (int i = 0; i < Adat.Count; i++)
            {
                if (Adat[i].Tavolsag() < min)
                {
                    min = Adat[i].Tavolsag();
                    jatekos = Adat[i];
                }
            }
            Console.WriteLine($"\t{jatekos.Sorszam}; {jatekos.JatekosNeve}; x={jatekos.XKoor}; y={jatekos.YKoor}; távolság: {jatekos.Tavolsag()}");
        }

        private static void F5() => Console.WriteLine($"5. feladat: Lövések száma: {Adat.Count()} db");

        private static void F2()
        {
            StreamReader olvas = new StreamReader(@"lovesek.txt");
            string[] elsoSor = olvas.ReadLine().Split(';');
            double xKozep = Convert.ToDouble(elsoSor[0]);
            double yKozep = Convert.ToDouble(elsoSor[1]);
            int sorszam = 0;
            while (!olvas.EndOfStream)
            {
                string[] split = olvas.ReadLine().Split(';');
                string nev = split[0];
                double xkoor = Convert.ToDouble(split[1]);
                double ykoor = Convert.ToDouble(split[2]);
                sorszam++;
                jatekos = new JatekosLovese(nev, xkoor, ykoor, sorszam, xKozep, yKozep);
                Adat.Add(jatekos);
            }
            olvas.Close();
        }
    }
}
