using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQQP_ProjektNr1_Nowalski
{
    internal class anMacierz
    {
        //deklaracje wewnętrzne (lokalne) klasy 
        private float[,] anmacierz;
        //deklaracja konstruktora klasy Macierz
        public anMacierz(ushort anLiczbaWierszy, ushort anLiczbaKolumn)
        {
            anmacierz = new float[anLiczbaWierszy, anLiczbaKolumn];
        }
        //deklaracja właściwości
        public ushort anLiczbaWierszy
        {
            get { return (ushort)anmacierz.GetLength(0); }
        }
        public ushort anLiczbaKolumn
        {
            get { return (ushort)anmacierz.GetLength(1); }
        }

        //deklaracja indeksator macierzy, który będzie umożliwiał dostęp do elementów macierzy w notacji matematycznej A[i,j]
        public float this[ushort anNrWiersza, ushort anNrKolumny]
        {
            set
            {
                anmacierz[anNrWiersza, anNrKolumny] = value;
            }
            get { return anmacierz[anNrWiersza, anNrKolumny]; }
        }

        //deklaracje metod i przeciążeń operatorów arytmetycznych: +, *, -, ...

        //przeciążenie operatora '+'
        public static anMacierz operator + (anMacierz ana, anMacierz anb)
        {
            //sprawdzenie warunku wejściowego
            if (ana.anLiczbaWierszy != anb.anLiczbaWierszy || ana.anLiczbaKolumn != anb.anLiczbaKolumn)
            {
                //sygnalizacja błędu-sygnalizacja wyjątku
                throw new ArgumentException("ERROR: wymiary macierzy nie spełniają warunku zgodności");
            }
                //deklaracja pomocnicza macierzy c dla przechowania wyników obliczeń
                anMacierz anC = new anMacierz(ana.anLiczbaWierszy, ana.anLiczbaKolumn);
                //rozmiary macierzy a i b są zgodne
                //sumowanie elementów macierzy a i b
                for (ushort  ani = 0; ani < ana.anLiczbaWierszy; ani++)
                    for (ushort anj = 0; anj < ana.anLiczbaKolumn; anj++)                    
                        anC.anmacierz[ani, anj] = ana.anmacierz[ani, anj] + anb.anmacierz[ani, anj];
                //zwrócenie wyniku obliczeń
                return anC; 
        }
        //przeciążenie operatora '-'
        public static anMacierz operator - (anMacierz ana, anMacierz anb)
        {
            //sprawdzenie warunku wejściowego
            if (ana.anLiczbaWierszy != anb.anLiczbaWierszy || ana.anLiczbaKolumn != anb.anLiczbaKolumn)
            {
                //sygnalizacja błędu-sygnalizacja wyjątku
                throw new ArgumentException("ERROR: wymiary macierzy nie spełniają warunku zgodności");
            }
            //deklaracja pomocnicza macierzy c dla przechowania wyników obliczeń
            anMacierz anC = new anMacierz(ana.anLiczbaWierszy, ana.anLiczbaKolumn);
            //rozmiary macierzy a i b są zgodne
            //sumowanie elementów macierzy a i b
            for (ushort ani = 0; ani < ana.anLiczbaWierszy; ani++)
                for (ushort anj = 0; anj < ana.anLiczbaKolumn; anj++)
                    anC.anmacierz[ani, anj] = ana.anmacierz[ani, anj] - anb.anmacierz[ani, anj];
            //zwrócenie wyniku obliczeń
            return anC;
        }
        //przeciążenie operatora '*'
        public static anMacierz operator * (anMacierz ana, anMacierz anb)
        {
            //sprawdzenie warunku dla wykonalności mnożenia macierzy
            if (ana.anLiczbaKolumn != anb.anLiczbaWierszy)
            {
                //sygnalizacja błędu-sygnalizacja wyjątku
                throw new System.ArgumentException("ERROR: nie zgodność rozmiarów macierzy");
            }
            anMacierz anC = new anMacierz(ana.anLiczbaWierszy, anb.anLiczbaKolumn);
            //wykonujemy mnożenie
            for(ushort ani = 0; ani < ana.anLiczbaWierszy; ani++)
                for(ushort anj = 0; anj < anb.anLiczbaKolumn; anj++)
                {
                    anC.anmacierz[ani, anj] = 0.0f;
                    //oblliczenie sumy iloczynów
                    for (ushort ank = 0; ank < anb.anLiczbaWierszy; ank++)
                        anC.anmacierz[ani, anj] += ana.anmacierz[ani, ank] * anb.anmacierz[ank, anj]; 
                }
            //zwrotne zwrócenie wyniku
            return anC;
        }
        public static anMacierz operator * (float anLiczba, anMacierz ana)
        {
            //pomocniczo deklarujemy macierz c
            anMacierz anC = new anMacierz(ana.anLiczbaWierszy, ana.anLiczbaKolumn);

            for(ushort ani = 0;ani<ana.anLiczbaWierszy;ani++)
                for (ushort anj = 0; anj < ana.anLiczbaKolumn; anj++)
                    anC.anmacierz[ani, anj] = anLiczba * ana.anmacierz[ani, anj];

            return anC;
        }
        
        public static bool operator == (anMacierz ana, anMacierz anb)
        {
            if (ana.anLiczbaWierszy != anb.anLiczbaWierszy || ana.anLiczbaKolumn != anb.anLiczbaKolumn)
            {
                //sygnalizacja błędu-sygnalizacja wyjątku
                throw new ArgumentException("ERROR: wymiary macierzy nie spełniają warunku zgodności");
            }
            for (ushort ani = 0; ani < ana.anLiczbaWierszy; ani++)
                for (ushort anj = 0; anj < ana.anLiczbaKolumn; anj++)
                    if (ana[ani, anj] != anb[ani, anj])
                    {
                        return false;
                    }
            return true;
        }
        public static bool operator != (anMacierz ana, anMacierz anb)
        {
            return!(ana == anb);
        }

        //napisanie metody Equals i GetHashCode
        public override bool Equals(object anobj)
        {
            //metoda Equals służy do porównywania stanu wskazanej w liście parametrów instancji
            //egzemplarza obiektu ze stanem  tzw obiektu na rzecz którego została wywołana

            //sprawdzenie parametru 'obj'
            if ((anobj is null) || !(anobj is anMacierz))
            {
                return false;
            }
            //deklaracja zmiennej referencyjnej Macierz
            anMacierz anm = (anMacierz)anobj;
            //sprawdzamy, czy elementy obiektu bieżącego (this.macierz) odpowiadają elementom obiektu 'obj'
            for (ushort ani = 0; ani < anm.anLiczbaWierszy; ani++)
                for (ushort anj = 0; anj < anm.anLiczbaKolumn; anj++)
                    if (this.anmacierz[ani, anj] != anm[ani, anj])
                    {
                        return false;
                    }
            return true;
        }
        public override int GetHashCode()
        {
            return this.anmacierz.GetHashCode();
        }
        // int x = 45;
        // ushort k = (ushort)x;
        //deklaracja operatora konwersji (rzutowania) dla klasy Macierzy
        public static explicit operator anMacierz (float anx)
        {
            //deklaracja macierzy pomocniczej dla przekazania wyniku
            anMacierz anc = new anMacierz(1, 1);
            // Wpisanie wartośći parametru 'x' do macierzy 'c'
            anc.anmacierz[0, 0] = anx;
            //zwrócenie wyniku
            return anc;
        }
    }
}
