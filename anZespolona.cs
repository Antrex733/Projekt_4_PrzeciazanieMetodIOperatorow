using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQQP_ProjektNr1_Nowalski
{
    internal class anZespolona
    {
        private double[,] anWyrazenia;

        public anZespolona()
        {
            anWyrazenia = new double[2, 4];
        }
        public double anIloscWyrazow
        {
            get { return anWyrazenia.GetLength(0); }
        }
        public double this[ushort anLiczbaWierszy, ushort anLiczbaWyrazow]
        {
            get
            {
                return anWyrazenia[anLiczbaWierszy, anLiczbaWyrazow];
            }
            set
            {
                anWyrazenia[anLiczbaWierszy, anLiczbaWyrazow] = value;
            }
        }
        public string anWyswietlWynik(anZespolona anwynik)
        {
            string annapis = "";

            if (anwynik[0, 1] > 0)
            {
                annapis = string.Format("{0}+{1}i^{2}", anwynik[0, 0], anwynik[0, 1], anwynik[1, 1]);
            }
            else if (anwynik[0, 1] < 0)
            {
                annapis = string.Format("{0}{1}i^{2}", anwynik[0, 0], anwynik[0, 1], anwynik[1, 1]);
            }
            if (anwynik[0, 2] > 0 && anwynik[1, 2] != 0)
            {
                annapis += string.Format("+{0}i^{1}", anwynik[0, 2], anwynik[1, 2]);
            }
            else if (anwynik[0, 2] < 0 && anwynik[1, 2] != 0)
            {
                annapis += string.Format("{0}i^{1}", anwynik[0, 2], anwynik[1, 2]);
            }
            return annapis;
        }
        public string anWyswietlWynikLiczba(anZespolona anwynik)
        {
            string annapis = "";

            if (anwynik[0, 1] > 0)
            {
                annapis = string.Format("{0}+{1}i^{2}", anwynik[0, 0], anwynik[0, 1], anwynik[1, 1]);
            }
            else if (anwynik[0, 1] < 0)
            {
                annapis = string.Format("{0}{1}i^{2}", anwynik[0, 0], anwynik[0, 1], anwynik[1, 1]);
            }
            
            return annapis;
        }
        public static anZespolona operator + (anZespolona anz1, anZespolona anz2)
        {
            anZespolona anwynik = new anZespolona();
            anwynik[0, 0] = anz1[0, 0] + anz2[0, 0];
            if (anz1[1, 1] == anz2[1, 1])
            {
                anwynik[0, 1] = anz1[0, 1] + anz2[0, 1];
                anwynik[1, 1] = anz1[1, 1];
            }
            else
            {
                anwynik[0, 1] = anz1[0, 1];
                anwynik[1, 1] = anz1[1, 1];
                anwynik[0, 2] = anz2[0, 1];
                anwynik[1, 2] = anz2[1, 1];
            }
            return anwynik;

        }
        public static anZespolona operator + (anZespolona anz1, int anliczba)
        {
            anZespolona anwynik = new anZespolona();
            anwynik[0, 0] = anz1[0, 0] + anliczba;
            anwynik[1, 0] = anz1[1, 0];
            anwynik[0, 1] = anz1[0, 1];
            anwynik[1, 1] = anz1[1, 1];

            return anwynik;

        }
        public static anZespolona operator - (anZespolona anz1, anZespolona anz2)
        {
            anZespolona anwynik = new anZespolona();
            anwynik[0, 0] = anz1[0, 0] - anz2[0, 0];
            if (anz1[1, 1] == anz2[1, 1])
            {
                anwynik[0, 1] = anz1[0, 1] - anz2[0, 1];
                anwynik[1, 1] = anz1[1, 1];
            }
            else
            {
                anwynik[0, 1] = anz1[0, 1];
                anwynik[1, 1] = anz1[1, 1];
                anwynik[0, 2] = -anz2[0, 1];
                anwynik[1, 2] = anz2[1, 1];
            }
            return anwynik;

        }
        public static anZespolona operator - (anZespolona anz1, int anliczba)
        {
            anZespolona anwynik = new anZespolona();
            anwynik[0, 0] = anz1[0, 0] - anliczba;
            anwynik[1, 0] = anz1[1, 0];
            anwynik[0, 1] = anz1[0, 1];
            anwynik[1, 1] = anz1[1, 1];

            return anwynik;

        }
        public static anZespolona operator * (anZespolona anz1, anZespolona anz2)
        {
            anZespolona anwynik = new anZespolona();

            anwynik[0, 0] = anz1[0, 0] * anz2[0, 0];

            anwynik[0, 1] = anz1[0, 0] * anz2[0, 1];
            anwynik[1, 1] = anz2[1, 1];

            anwynik[0, 2] = anz1[0, 1] * anz2[0, 0];
            anwynik[1, 2] = anz1[1, 1];

            anwynik[0, 3] = anz1[0, 1] * anz2[0, 1];
            anwynik[1, 3] = anz1[1, 1] + anz2[1, 1];

            return anwynik;

        }
        public static anZespolona operator * (anZespolona anz1, int anliczba)
        {
            anZespolona anwynik = new anZespolona();
            anwynik[0, 0] = anz1[0, 0] * anliczba;
            anwynik[1, 0] = anz1[1, 0];
            anwynik[0, 1] = anz1[0, 1];
            anwynik[1, 1] = anz1[1, 1];

            return anwynik;

        }
        public static anZespolona operator ^ (anZespolona anz1, int anliczba)
        {
            anZespolona anwynik = new anZespolona();
            anwynik[0, 0] = Math.Pow(anz1[0, 0], anliczba);
            anwynik[1, 0] = anz1[1, 0];
            anwynik[0, 1] = anz1[0, 1] * anliczba;
            anwynik[1, 1] = Math.Pow(anz1[1, 1], anliczba);

            return anwynik;

        }


    }

}
