using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace jegkorcso
{
    class korcsolya
    {
        public string nev;
        public string orszag;
        public double techpont;
        public double komppont;
        public int levonas;
        public string oszpont;

        public korcsolya(string adatsor)
        {
            string[] adatsorelemek = adatsor.Split(';');
            this.nev = adatsorelemek[0];
            this.orszag = adatsorelemek[1];
            this.techpont = Convert.ToDouble(adatsorelemek[2].Replace('.', ','));
            this.komppont = Convert.ToDouble(adatsorelemek[3].Replace('.', ','));
            this.levonas = Convert.ToInt32(adatsorelemek[4]);
        }


    }
    class Program
    {



        static List<korcsolya> rovidprog = new List<korcsolya>();
        static List<korcsolya> dontolista = new List<korcsolya>();
        static void Main(string[] args)
        {
            StreamReader olv = new StreamReader("rovidprogram.csv");
            string fejl = olv.ReadLine();
            while (!olv.EndOfStream)
            {
                rovidprog.Add(new korcsolya(olv.ReadLine()));
            }
            olv.Close();
            StreamReader olv2 = new StreamReader("donto.csv");
            fejl = olv2.ReadLine();
            while (!olv2.EndOfStream)
            {
                dontolista.Add(new korcsolya(olv2.ReadLine()));

            }
            olv2.Close();
            // 2.feladat
            Console.WriteLine($"rovidprogramban {rovidprog.Count} indulo volt.");

            //3feledat
            bool bejutott = false;
            for (int i = 0; i < dontolista.Count; i++)
            {
                if (dontolista[i].orszag == "HUN")
                {
                    bejutott = true;
                }
            }
            if (bejutott == true)
            {
                Console.WriteLine("A magyarversenyző bejutott");
            }
            else
            {
                Console.WriteLine("A magyarversenyzo nem jutott be");
            }
            // 5 feladat
            Console.WriteLine("Kérem a versenyző nevét");
            string bekernev = Console.ReadLine();
            bool voleily = false;
            for (int i = 0; i < rovidprog.Count; i++)
            {
                if (bekernev == rovidprog[i].nev)
                {
                    voleily = true;
                }
            }
            if (voleily == false)
            {
                Console.WriteLine("Ilyen nevu indulo nem volt");
            }

            // 6 FELADAT
            double oszpont = osszpontszam(bekernev);
            Console.WriteLine($"A veresenyzo osszpontszama: {oszpont}");

            // 7 feladat
            List<string> orszaglista = new List<string>();
            for (int i = 0; i < dontolista.Count; i++)
            {
                bool szerep = false;
                for (int j = 0; j < orszaglista.Count; j++)
                {
                    if (dontolista[i].orszag == orszaglista[j])
                    {
                        szerep = true;
                    }

                }
                if (szerep == false)
                {
                    orszaglista.Add(dontolista[i].orszag);
                }
            }

            int[] orszagseged = new int[orszaglista.Count];
            for (int i = 0; i < dontolista.Count; i++)
            {
                for (int j = 0; j < orszaglista.Count; j++)
                {
                    if (dontolista[i].orszag == orszaglista[j])
                    {
                        orszagseged[j]++;
                    }
                }
            }
            for (int i = 0; i < orszagseged.Length; i++)
            {
                if (orszagseged[i] > 1)
                {
                    Console.WriteLine($"{orszaglista[i]}: {orszagseged[i]} versenyzo");
                }
            }

            // 8feladat
            StreamWriter ir = new StreamWriter("vegeredmegy.csv");
            for (int i = 0; i < dontolista.Count; i++)
            {
                korcsolya newkorcsolya = dontolista[i];
                newkorcsolya.oszpont = osszpontszam(dontolista[i].nev);
                dontolista[i] = newkorcsolya;
            }
            dontolista = dontolista.OrderBy(versenyzo => versenyzo.oszpont).ToList();
            dontolista.Reverse();
            int helyezes = 1;
            foreach ( korcsolya i in dontolista)
            {
                ir.WriteLine($"{helyezes};{i.nev};{i.orszag};{i.oszpont}");
                helyezes++;
            }
            ir.Close();
            Console.ReadKey();
        }

        static double osszpontszam(string nev) 
        {
            double osszp = 0;
            foreach (korcsolya i in rovidprog)
            {
                if (i.nev == nev)
                {
                    osszp += i.techpont + i.komppont - i.levonas;
                }
            }
            foreach ( korcsolya i in dontolista )
            {
                if (i.nev == nev)
                {
                    osszp += i.techpont + i.komppont - i.levonas;
                }
            }
            return osszp;
        }
    }
}
