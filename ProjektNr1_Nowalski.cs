using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QQQP_ProjektNr1_Nowalski
{
    public partial class ProjektNr1_Nowalski : Form
    {
        const short anMargines = 20;
        public ProjektNr1_Nowalski() // konstruktor
        {
            InitializeComponent();
            //ustawienie strony kokpit jako otwartej zakładki
            tabControlZakladki.SelectedTab = tabPageKokpit;

            //ustalenie rozmiarów formularza
            //ustalenie położenia lewego górnego narożnika formularza
            this.Location = new Point( (Screen.PrimaryScreen.Bounds.X + anMargines), 
                                       (Screen.PrimaryScreen.Bounds.Y) + anMargines);
            //ustalenie szerokości i wysokości formularza
            this.Width = (int)(Screen.PrimaryScreen.Bounds.Width * 0.80F);
            this.Height = (int)(Screen.PrimaryScreen.Bounds.Height * 0.80F);
            //wymuszenie ustawień formularza wegług naszych rozmiarów
            this.StartPosition = FormStartPosition.Manual;
        }

        //Delkaracja pomocniczej tablicy oisującej stan aktywności zakładek
        bool[] anStanStronZakladki = { true, false, false };
        //Deklaracje zmiennych referencyjnych kontrolek dodawanych do formularza w czasie działania programu
        DataGridView andgvMacierzA;
        DataGridView andgvMacierzB;
        DataGridView andgvMacierzC;
        ushort anLiczbaWierszyMacierzy, anLiczbaKolumnMacierzy;
        int anDolnaGranicaPrzedzialu = 10;
        int anGornaGranicaPrzedzialu = 100;
        double anDolnaGranicaPrzedzialuB = 200.00;
        double anGornaGranicaPrzedzialuB = 800.00;
        //Deklaracje zmiennych referencyjnych do egzemplarzy klasy Macierz
        anMacierz anA;
        anMacierz anB;
        anMacierz anC;
        //Deklaracja zmiennych referencyjnych do egzemplarzy klasy Zespolona
        anZespolona anz1;
        anZespolona anz2;
        anZespolona anwynik;

        double anCzescRzeczywistaZ1, anCzescRzeczywistaZ2, anCzescUrojonaZ1, anCzescUrojonaZ2;
        int anPotegaz1, anPotegaz2;
        private void ProjektNr1_Nowalski_Load(object sender, EventArgs e)
        {

        }
        
        private void tabControlZakladki_Selecting(object sender, TabControlCancelEventArgs e)
        {
            //identyfikacja wybranej (kliknięciem) zakładki
            if (e.TabPage == tabControlZakladki.TabPages[0])
            {
                //sprawdzenie czy jest dozwolone przejście na zakładkę kokpit
                if (anStanStronZakladki[0])
                {
                    e.Cancel = false;
                    tabControlZakladki.SelectedTab = tabPageKokpit;
                }
                else
                    e.Cancel = true;
            }
            if (e.TabPage == tabControlZakladki.TabPages[1])
            {
                if (anStanStronZakladki[1] == true)
                {
                    e.Cancel = false;
                    tabControlZakladki.SelectedTab = tabPageMacierze;
                }
                else
                    e.Cancel = true;
                }

            if (e.TabPage == tabControlZakladki.TabPages[2])
            {
                if (anStanStronZakladki[2] == true)
                {
                    e.Cancel = false;
                    tabControlZakladki.SelectedTab = tabPageLiczbyZespolone;
                }
                else
                    e.Cancel = true;
            }
                
        }
        

        private void btnDzialaniaNaMacierzachPowrotDoKokpitu_Click(object sender, EventArgs e)
        {
            //zmiana stanu zakładki tabPageMacierze
            anStanStronZakladki[1] = false;
            anStanStronZakladki[0] = true;
            tabControlZakladki.SelectedTab = tabPageKokpit;

        }

        private void btnPowrotDoKokpitu2_Click(object sender, EventArgs e)
        {
            //zmiana stanu zakładki tabPageMacierze
            anStanStronZakladki[2] = false;
            anStanStronZakladki[0] = true;
            tabControlZakladki.SelectedTab = tabPageKokpit;
        }

        private void btnGenerujElementyMacierzyA_Click(object sender, EventArgs e)
        {
            //deklaracja i utworzenie egzemplarza generatora liczb losowych
            Random anrnd = new Random();
            //losowanie wartości elementów macierzy A i wpisywanie ich do kontrolki dgvMacierzA do odp. pozycji

            for(ushort ani = 0; ani < andgvMacierzA.Rows.Count; ani++)
                for (ushort anj = 0; anj < andgvMacierzA.Columns.Count; anj++)
                {
                    andgvMacierzA.Rows[ani].Cells[anj].Value = anrnd.Next(anDolnaGranicaPrzedzialu, anGornaGranicaPrzedzialu);
                    
                }
            //brak aktywności przycisku losowania 
            btnGenerujElementyMacierzyA.Enabled = false;
            
            
        }

        private void btnAkceptacjaWarMacierzaA_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            //sprawdzenie, czy zostały wpisane wszystkie wartośći elementów macierzy A
            for (ushort ani = 0; ani < andgvMacierzA.Rows.Count; ani++)
                for (ushort anj = 0; anj < andgvMacierzA.Columns.Count; anj++)
                    if (andgvMacierzA.Rows[ani].Cells[anj].Value is null)
                    {
                        //komurka [i, j] jest pusta
                        errorProvider1.SetError(btnAkceptacjaWarMacierzaA, "ERROR - niektóre komurki macierzy A są puste!!!");
                        return;
                    }
                //Ustawić tryb ReadOnly dla kontrolki dgvMacierzA
                andgvMacierzA.ReadOnly = true;
            //brak aktywności ddla btn akceptacjaA
            btnAkceptacjaWarMacierzaA.Enabled = false;
            btnUwtorzKontrolkeA.Enabled = false;
            if (btnAkceptacjaWarMacierzaB.Enabled == false && btnUtwórzkontrolkęDataGridViewdlamacierzyB.Enabled == false)
            {
                btnSumaMacierzyAiB.Enabled = true;
                btnRoznicaAB.Enabled = true;
                btnRoznicaBA.Enabled = true;
                btnIloczynMacierzyAiB.Enabled = true;
            }

        }
        private void btnUtwórzkontrolkęDataGridViewdlamacierzyB_Click(object sender, EventArgs e)
        {
            //deklaracje zmiennych dla przechowania rozmiaru macierzy
            ushort anLiczbaWierszy, anLiczbaKolumn;
            //zgasze kontrolki error 
            errorProvider1.Dispose();
            //pobranie liczby wierszy
            if (!ushort.TryParse(txtLiczbaWierszy.Text, out anLiczbaWierszy) || txtLiczbaWierszy.Text == "0")
            {
                errorProvider1.SetError(txtLiczbaWierszy, "ERROR: wystąpił niedozwolony znak w zapisie liczby wierszy");
                return;
            }
            //pobranie liczby kolumn
            if (!ushort.TryParse(txtLiczbaKolumn.Text, out anLiczbaKolumn) || txtLiczbaKolumn.Text == "0")
            {
                errorProvider1.SetError(txtLiczbaKolumn, "ERROR: wystąpił niedozwolony znak w zapisie liczby kolumn");
                return;
            }
            //Utworzenie egzemplarza kontrolki DataGridView dla macierzy B
            andgvMacierzB = new DataGridView();
            //sformatowanie kontrolki dgvMacierzA
            ushort anodstęp = 10;
            ushort anSzerokośćKolukny = 70; //kontrolki dgv
            ushort anWyskokośćWiersza = 25; //kontrolki dgv
            andgvMacierzB.Location = new Point(groupBox2.Location.X + groupBox2.Width + anodstęp,
                                             andgvMacierzA.Location.Y + andgvMacierzA.Height + anodstęp);
            andgvMacierzB.Size = new Size((anIloscKolumn(anLiczbaKolumn) + 1) * anSzerokośćKolukny, (anIloscWierszy(anLiczbaWierszy) + 1) * anWyskokośćWiersza);
            //Wpisanie liczby kolumn do kontrolki dgv
            andgvMacierzB.ColumnCount = anLiczbaKolumn; ///dlaczego nie +1?
            //odsłonięcie wiersza nagłówkowego dla kolumn
            andgvMacierzB.ColumnHeadersVisible = true;
            //umożliwienie wpisywanie i odczytu danyczh w kontrolce dgv
            andgvMacierzB.ReadOnly = false;
            //uniemożliwienie dodawania nowych wierszy
            andgvMacierzB.AllowUserToAddRows = false;
            //ustawienie trybu AutoSize dla wiersza nagłówkowego kolumn 
            andgvMacierzB.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            //uniemożliwienie multiSelekt komurce w kontrolce
            andgvMacierzB.MultiSelect = false;//?
            //ustalenie fomatu dla nagłówka kolumn kontrolki dgvMacierzA
            DataGridViewCellStyle anStylHeaderKolumn = new DataGridViewCellStyle();
            anStylHeaderKolumn.Font = new Font("Arial", 10, FontStyle.Bold);
            anStylHeaderKolumn.Alignment = DataGridViewContentAlignment.MiddleCenter;
            anStylHeaderKolumn.Format = "    ";

            //przypisanie StylHeaderKolumn do kontrolki dgvMacierzA
            andgvMacierzB.ColumnHeadersDefaultCellStyle = anStylHeaderKolumn;

            //ustalenie fomatu dla nagłówka wierszy kontrolki dgvMacierzA
            DataGridViewCellStyle anStylHeaderWierszy = new DataGridViewCellStyle();
            anStylHeaderWierszy.Font = new Font("Arial", 10, FontStyle.Bold);
            anStylHeaderWierszy.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //przypisanie StylHeaderWierszy do kontrolki dgvMacierzA
            andgvMacierzB.RowHeadersDefaultCellStyle = anStylHeaderWierszy;

            //wpisanie nazw (numerów) kolumn
            for (ushort ani = 0; ani < anLiczbaKolumn; ani++)
            {
                //wpisanie nazwy kolumny: (i)
                andgvMacierzB.Columns[ani].HeaderText = String.Format("({0})", ani + 1);
                andgvMacierzB.Columns[ani].Width = anSzerokośćKolukny;
                //wycentrowanie zapisów w kolumnie
                andgvMacierzB.Columns[ani].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            //dodanie kontrolki dgvMacierzA odpowiedzniej liczbt wierszy macierzyA
            for (ushort ani = 0; ani < anLiczbaWierszy; ani++)
            {
                andgvMacierzB.Rows.Add();
            }

            //wpisanie nazw (numerów) wierszy
            for (ushort ani = 0; ani < anLiczbaWierszy; ani++)
            {
                //wpisanie nazwy kolumny: (i)
                andgvMacierzB.Rows[ani].HeaderCell.Value = String.Format("({0})", ani + 1);

                //wycentrowanie zapisów w kolumnie
                andgvMacierzB.Rows[ani].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            //doddanie kontrolki dgvMacierzB do strony 1 zakładki
            tabControlZakladki.TabPages[1].Controls.Add(andgvMacierzB);

            //autosize dla kontrolki MacierzB
            andgvMacierzB.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            //ustawienie braku aktywności dla przycisku - przycisk utwurz
            btnDzialaniaNaMacierzachPowrotDoKokpitu.Enabled = false;

            //uaktywnienie kontrolek 
            btnUtwórzkontrolkęDataGridViewdlamacierzyB.Enabled = false;
            btnAkceptacjaWarMacierzaB.Enabled = true;
            btnWygenerujwartościB.Enabled = true;
            //to samo dla przycisków macierz b

        }

        private void btnWygenerujwartościB_Click(object sender, EventArgs e)
        {
            //deklaracja i utworzenie egzemplarza generatora liczb losowych
            Random anrnd = new Random();
            //losowanie wartości elementów macierzy B i wpisywanie ich do kontrolki dgvMacierzA do odp. pozycji

            for (ushort ani = 0; ani < andgvMacierzB.Rows.Count; ani++)
                for (ushort anj = 0; anj < andgvMacierzB.Columns.Count; anj++)
                {
                    andgvMacierzB.Rows[ani].Cells[anj].Value = string.Format("{0:F2}",anrnd.NextDouble() * (anGornaGranicaPrzedzialuB - anDolnaGranicaPrzedzialuB + anDolnaGranicaPrzedzialuB));
                }
            //brak aktywności przycisku losowania 
            btnWygenerujwartościB.Enabled = false;
            
        }

        private void btnUtworzKontrolkedgvDlaMacierzyB_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            //sprawdzenie, czy zostały wpisane wszystkie wartośći elementów macierzy A
            for (ushort ani = 0; ani < andgvMacierzB.Rows.Count; ani++)
                for (ushort anj = 0; anj < andgvMacierzB.Columns.Count; anj++)
                    if (andgvMacierzB.Rows[ani].Cells[anj].Value is null)
                    {
                        //komurka [i, j] jest pusta
                        errorProvider1.SetError(btnAkceptacjaWarMacierzaB, "ERROR - niektóre komurki macierzy A są puste!!!");
                        return;
                    }
            //Ustawić tryb ReadOnly dla kontrolki dgvMacierzA
            andgvMacierzB.ReadOnly = true;
            //brak aktywności ddla btn akceptacjaA
            btnAkceptacjaWarMacierzaB.Enabled = false;
            if (btnAkceptacjaWarMacierzaA.Enabled == false && btnUwtorzKontrolkeA.Enabled == false)
            {
                btnSumaMacierzyAiB.Enabled = true;
                btnRoznicaAB.Enabled = true;
                btnRoznicaBA.Enabled = true;
                btnIloczynMacierzyAiB.Enabled = true;
            }
        }

        private void btnSumaMacierzyAiB_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            if (btnAkceptacjaWarMacierzaA.Enabled || btnAkceptacjaWarMacierzaB.Enabled)
            {
                errorProvider1.SetError(btnSumaMacierzyAiB, "ERROR - brak zaakceptowania danych obu macierzy");
                return;
            }
            //sprawdzenie czy już została umieszczona kontrolka macierza C
            if (andgvMacierzC is null)
            {
                anUtworzKontrolkeDgvDlaMacierzyC();
            }
            //utworzenie egzemplarzy dla macierzy 
            anA = new anMacierz((ushort)andgvMacierzA.Rows.Count, (ushort)andgvMacierzA.Columns.Count);
            anB = new anMacierz((ushort)andgvMacierzB.Rows.Count, (ushort)andgvMacierzB.Columns.Count);
            anC = new anMacierz((ushort)andgvMacierzB.Rows.Count, (ushort)andgvMacierzB.Columns.Count);

            //należy przepisać wartośći elementów kontrolek: dgvA do macierzy A i dgvB do macierzy B
            anA.PrzepiszElementyDataGridViewDoMacierzy(andgvMacierzA);
            anB.PrzepiszElementyDataGridViewDoMacierzy(andgvMacierzB);

            anC = anA + anB;
            anC.anPrzepiszElementyMacierzyDoKontrolkiDataGridView(andgvMacierzC);

            btnSumaMacierzyAiB.Enabled = false;
        }

        private void btnKalkulatorObliczenNaMacierzach_Click(object sender, EventArgs e)
        {
            //zmaina zakładki
            anStanStronZakladki[0] = false;
            //zezwolenie na przejście do zakładki Działania na macierzach
            anStanStronZakladki[1] = true;
            //przejście do zakładki Działanie na macierzach
            tabControlZakladki.SelectedTab = tabPageMacierze;
        }

        private void btnDzialaniaNaMacierzach_Click(object sender, EventArgs e)
        {
            //zmaina zakładki
            anStanStronZakladki[0] = false;
            //zezwolenie na przejście do zakładki Działania na macierzach
            anStanStronZakladki[2] = true;
            //przejście do zakładki Działanie na macierzach
            tabControlZakladki.SelectedTab = tabPageLiczbyZespolone;
        }

        //Pilnowanie aby kontrolka DataGridView nie była za szeroka
        public ushort anIloscKolumn(ushort anLiczbaKolumn)
        {
            if (anLiczbaKolumn > 6)
            {
                return 6;
            }
         
            return anLiczbaKolumn;
        }

        private void btnSumaZ1iZ2_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();   
            if (btnZatwierdzLiczbeZ1.Enabled == true || btnZatwierdzLiczbeZ2.Enabled == true)
            {
                errorProvider1.SetError(btnSumaZ1iZ2, "Brak zatwierdzenia obu liczb!");
                return;
            }
            groupBox5.Visible = true;
            txtWynik.Visible = true;
            anwynik = anz1 + anz2;
            txtWynik.Text = anwynik.anWyswietlWynik(anwynik);
            btnSumaZ1iZ2.Enabled = false;
        }

        private void btnRoznicaZ1iZ2_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            if (btnZatwierdzLiczbeZ1.Enabled == true || btnZatwierdzLiczbeZ2.Enabled == true)
            {
                errorProvider1.SetError(btnRoznicaZ1iZ2, "Brak zatwierdzenia obu liczb!");
                return;
            }
            groupBox5.Visible = true;
            txtWynik.Visible = true;
            anwynik = anz1 - anz2;
            txtWynik.Text = anwynik.anWyswietlWynik(anwynik);
            btnRoznicaZ1iZ2.Enabled = false;
        }

        private void btnRoznicaZ2Z1_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            if (btnZatwierdzLiczbeZ1.Enabled == true || btnZatwierdzLiczbeZ2.Enabled == true)
            {
                errorProvider1.SetError(btnRoznicaZ2Z1, "Brak zatwierdzenia obu liczb!");
                return;
            }
            groupBox5.Visible = true;
            txtWynik.Visible = true;
            anwynik = anz2 - anz1;
            txtWynik.Text = anwynik.anWyswietlWynik(anwynik);
            btnRoznicaZ2Z1.Enabled = false;
        }

        string anUprosc(anZespolona anwynik)
        {
            string anw = "";

            anw += string.Format("{0}", anwynik[0, 0]);
            if (anwynik[1, 1] == anwynik[1, 2])
            {
                if ((anwynik[0, 1] + anwynik[0, 2]) > 0)
                {
                    anw += string.Format("+{0}^{1}", anwynik[0, 1] + anwynik[0, 2], anwynik[1, 1]);
                }
                else
                {
                    anw += string.Format("{0}^{1}", anwynik[0, 1] + anwynik[0, 2], anwynik[1, 1]);
                }
                if (anwynik[0, 3] > 0)
                {
                    anw += string.Format("+{0}^{1}", anwynik[0, 3], anwynik[1, 3]);
                }
                else
                {
                    anw += string.Format("{0}^{1}", anwynik[0, 3], anwynik[1, 3]);
                }
            }
            else
            {
                if (anwynik[0, 1] > 0)
                {
                    anw += string.Format("+{0}^{1}", anwynik[0, 1], anwynik[1, 1]);
                }
                else
                {
                    anw += string.Format("{0}^{1}", anwynik[0, 1], anwynik[1, 1]);
                }
                if (anwynik[0, 2] > 0)
                {
                    anw += string.Format("+{0}^{1}", anwynik[0, 2], anwynik[1, 2]);
                }
                else
                {
                    anw += string.Format("{0}^{1}", anwynik[0, 2], anwynik[1, 2]);
                }
                if (anwynik[0, 3] > 0)
                {
                    anw += string.Format("+{0}^{1}", anwynik[0, 3], anwynik[1, 3]);
                }
                else
                {
                    anw += string.Format("{0}^{1}", anwynik[0, 3], anwynik[1, 3]);
                }
            }

            return anw;
        }
        private void btnIloczynZ1Z2_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            if (btnZatwierdzLiczbeZ1.Enabled == true || btnZatwierdzLiczbeZ2.Enabled == true)
            {
                errorProvider1.SetError(btnIloczynZ1Z2, "Brak zatwierdzenia obu liczb!");
                return;
            }
            groupBox5.Visible = true;
            txtWynik.Visible = true;
            anwynik = anz1 * anz2; //funkcja upraszczająca i mnożąca
            
            txtWynik.Text = anUprosc(anwynik);
            btnIloczynZ1Z2.Enabled = false;
        }

        private void btnIloczynMacierzyAiB_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            if (btnAkceptacjaWarMacierzaA.Enabled || btnAkceptacjaWarMacierzaB.Enabled)
            {
                errorProvider1.SetError(btnSumaMacierzyAiB, "ERROR - brak zaakceptowania danych obu macierzy");
                return;
            }
            //sprawdzenie czy już została umieszczona kontrolka macierza C
            if (andgvMacierzC is null)
            {
                anUtworzKontrolkeDgvDlaMacierzyC();
            }
            //utworzenie egzemplarzy dla macierzy 
            anA = new anMacierz((ushort)andgvMacierzA.Rows.Count, (ushort)andgvMacierzA.Columns.Count);
            anB = new anMacierz((ushort)andgvMacierzB.Rows.Count, (ushort)andgvMacierzB.Columns.Count);
            anC = new anMacierz((ushort)andgvMacierzB.Rows.Count, (ushort)andgvMacierzB.Columns.Count);

            //należy przepisać wartośći elementów kontrolek: dgvA do macierzy A i dgvB do macierzy B
            anA.PrzepiszElementyDataGridViewDoMacierzy(andgvMacierzA);
            anB.PrzepiszElementyDataGridViewDoMacierzy(andgvMacierzB);

            anC = anA * anB;
            anC.anPrzepiszElementyMacierzyDoKontrolkiDataGridView(andgvMacierzC);

            btnIloczynMacierzyAiB.Enabled = false;
        }

        private void btnRoznicaAB_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            if (btnAkceptacjaWarMacierzaA.Enabled || btnAkceptacjaWarMacierzaB.Enabled)
            {
                errorProvider1.SetError(btnRoznicaAB, "ERROR - brak zaakceptowania danych obu macierzy");
                return;
            }
            //sprawdzenie czy już została umieszczona kontrolka macierza C
            if (andgvMacierzC is null)
            {
                anUtworzKontrolkeDgvDlaMacierzyC();
            }
            //utworzenie egzemplarzy dla macierzy 
            anA = new anMacierz((ushort)andgvMacierzA.Rows.Count, (ushort)andgvMacierzA.Columns.Count);
            anB = new anMacierz((ushort)andgvMacierzB.Rows.Count, (ushort)andgvMacierzB.Columns.Count);
            anC = new anMacierz((ushort)andgvMacierzB.Rows.Count, (ushort)andgvMacierzB.Columns.Count);

            //należy przepisać wartośći elementów kontrolek: dgvA do macierzy A i dgvB do macierzy B
            anA.PrzepiszElementyDataGridViewDoMacierzy(andgvMacierzA);
            anB.PrzepiszElementyDataGridViewDoMacierzy(andgvMacierzB);

            anC = anA - anB;
            anC.anPrzepiszElementyMacierzyDoKontrolkiDataGridView(andgvMacierzC);

            btnRoznicaAB.Enabled = false;
        }

        private void btnRoznicaBA_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            if (btnAkceptacjaWarMacierzaA.Enabled || btnAkceptacjaWarMacierzaB.Enabled)
            {
                errorProvider1.SetError(btnRoznicaBA, "ERROR - brak zaakceptowania danych obu macierzy");
                return;
            }
            //sprawdzenie czy już została umieszczona kontrolka macierza C
            if (andgvMacierzC is null)
            {
                anUtworzKontrolkeDgvDlaMacierzyC();
            }
            //utworzenie egzemplarzy dla macierzy 
            anA = new anMacierz((ushort)andgvMacierzA.Rows.Count, (ushort)andgvMacierzA.Columns.Count);
            anB = new anMacierz((ushort)andgvMacierzB.Rows.Count, (ushort)andgvMacierzB.Columns.Count);
            anC = new anMacierz((ushort)andgvMacierzB.Rows.Count, (ushort)andgvMacierzB.Columns.Count);

            //należy przepisać wartośći elementów kontrolek: dgvA do macierzy A i dgvB do macierzy B
            anA.PrzepiszElementyDataGridViewDoMacierzy(andgvMacierzA);
            anB.PrzepiszElementyDataGridViewDoMacierzy(andgvMacierzB);

            anC = anB - anA;
            anC.anPrzepiszElementyMacierzyDoKontrolkiDataGridView(andgvMacierzC);

            btnRoznicaBA.Enabled = false;
        }

        private void bntZatwierdzLiczbe_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            int Liczba;
            if (!int.TryParse(txtUrojonaZatwierdz.Text, out Liczba))
            {
                errorProvider1.SetError(txtUrojonaZatwierdz, "ERROR - Podana liczba musi być całkowita");
                return;
            }
            txtUrojonaZatwierdz.Enabled = false;
            btnZatwierdzLiczbe.Enabled = false;
        }

        private void btnSumaLiczbaZ1_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            if (btnZatwierdzLiczbe.Enabled)
            {
                errorProvider1.SetError(btnSumaLiczbaZ1, "ERROR - Liczba A musi być zaakceptowana");
                return;
            }
            if (btnZatwierdzLiczbeZ1.Enabled == true)
            {
                errorProvider1.SetError(btnSumaLiczbaZ1, "ERROR - Brak zatwierdzenia pierwszej liczby!");
                return;
            }
            groupBox5.Visible = true;
            txtWynik.Visible = true;
            btnSumaLiczbaZ1.Enabled = false;
            anwynik = anz1 + int.Parse(txtUrojonaZatwierdz.Text);
            txtWynik.Text = anwynik.anWyswietlWynikLiczba(anwynik);
        }

        private void btnRoznicaZ1Liczba_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            if (btnZatwierdzLiczbe.Enabled)
            {
                errorProvider1.SetError(btnRoznicaZ1Liczba, "ERROR - Liczba A musi być zaakceptowana");
                return;
            }
            if (btnZatwierdzLiczbeZ1.Enabled == true)
            {
                errorProvider1.SetError(btnRoznicaZ1Liczba, "ERROR - Brak zatwierdzenia pierwszej liczby!");
                return;
            }
            groupBox5.Visible = true;
            txtWynik.Visible = true;
            btnRoznicaZ1Liczba.Enabled = false;
            anwynik = anz1 - int.Parse(txtUrojonaZatwierdz.Text);
            txtWynik.Text = anwynik.anWyswietlWynikLiczba(anwynik);
        }

        private void btnIloczynz1Liczba_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            if (btnZatwierdzLiczbe.Enabled)
            {
                errorProvider1.SetError(btnIloczynz1Liczba, "ERROR - Liczba A musi być zaakceptowana");
                return;
            }
            if (btnZatwierdzLiczbeZ1.Enabled == true)
            {
                errorProvider1.SetError(btnIloczynz1Liczba, "ERROR - Brak zatwierdzenia pierwszej liczby!");
                return;
            }
            groupBox5.Visible = true;
            txtWynik.Visible = true;
            btnIloczynz1Liczba.Enabled = false;
            anwynik = anz1 * int.Parse(txtUrojonaZatwierdz.Text);
            txtWynik.Text = anwynik.anWyswietlWynikLiczba(anwynik);
        }

        private void btnPotegaZ1Liczba_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            if (btnZatwierdzLiczbe.Enabled)
            {
                errorProvider1.SetError(btnPotegaZ1Liczba, "ERROR - Liczba A musi być zaakceptowana");
                return;
            }
            if (btnZatwierdzLiczbeZ1.Enabled == true)
            {
                errorProvider1.SetError(btnPotegaZ1Liczba, "ERROR - Brak zatwierdzenia pierwszej liczby!");
                return;
            }
            groupBox5.Visible = true;
            txtWynik.Visible = true;
            btnPotegaZ1Liczba.Enabled = false;
            anwynik = anz1 ^ int.Parse(txtUrojonaZatwierdz.Text);
            txtWynik.Text = anwynik.anWyswietlWynikLiczba(anwynik);
        }


        //Pilnowanie aby kontrolka DataGridView nie była za wysoka
        public ushort anIloscWierszy(ushort anLiczbaWierszy)
        {
            if (anLiczbaWierszy > 6)
            {
                return 6;
            }
           
            return anLiczbaWierszy;
        }

        private void btnUwtorzKontrolkeA_Click(object sender, EventArgs e)
        {
            //deklaracje zmiennych dla przechowania rozmiaru macierzy
            ushort anLiczbaWierszy, anLiczbaKolumn;
            //zgasze kontrolki error 
            errorProvider1.Dispose();
            //pobranie liczby wierszy
            if (!ushort.TryParse(txtLiczbaWierszy.Text, out anLiczbaWierszy) || txtLiczbaWierszy.Text == "0")
            {
                errorProvider1.SetError(txtLiczbaWierszy, "ERROR: wystąpił niedozwolony znak w zapisie liczby wierszy");
                return;
            }
            //pobranie liczby kolumn
            if (!ushort.TryParse(txtLiczbaKolumn.Text, out anLiczbaKolumn) || txtLiczbaKolumn.Text == "0")
            {
                errorProvider1.SetError(txtLiczbaKolumn, "ERROR: wystąpił niedozwolony znak w zapisie liczby kolumn");
                return;
            }
            //Utworzenie egzemplarza kontrolki DataGridView dla macierzy A
            andgvMacierzA = new DataGridView();
            //sformatowanie kontrolki dgvMacierzA
            ushort anodstęp = 10;
            ushort anSzerokośćKolukny = 70; //kontrolki dgv
            ushort anWyskokośćWiersza = 25; //kontrolki dgv
            andgvMacierzA.Location = new Point(groupBox1.Location.X + groupBox1.Width + anodstęp,
                                             btnDzialaniaNaMacierzachPowrotDoKokpitu.Top);
            andgvMacierzA.Size = new Size((anIloscKolumn(anLiczbaKolumn) + 1) * anSzerokośćKolukny, (anIloscWierszy(anLiczbaWierszy) + 1) * anWyskokośćWiersza);
            //Wpisanie liczby kolumn do kontrolki dgv
            andgvMacierzA.ColumnCount = anLiczbaKolumn; ///dlaczego nie +1?
            //odsłonięcie wiersza nagłówkowego dla kolumn
            andgvMacierzA.ColumnHeadersVisible = true;
            //umożliwienie wpisywanie i odczytu danyczh w kontrolce dgv
            andgvMacierzA.ReadOnly = false;
            //uniemożliwienie dodawania nowych wierszy
            andgvMacierzA.AllowUserToAddRows = false;
            //ustawienie trybu AutoSize dla wiersza nagłówkowego kolumn 
            andgvMacierzA.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            //uniemożliwienie multiSelekt komurce w kontrolce
            andgvMacierzA.MultiSelect = false;//?
            //ustalenie fomatu dla nagłówka kolumn kontrolki dgvMacierzA
            DataGridViewCellStyle anStylHeaderKolumn = new DataGridViewCellStyle();
            anStylHeaderKolumn.Font = new Font("Arial", 10, FontStyle.Bold);
            anStylHeaderKolumn.Alignment = DataGridViewContentAlignment.MiddleCenter; //dlaczego nie działa???
            anStylHeaderKolumn.Format = "    ";

            //przypisanie StylHeaderKolumn do kontrolki dgvMacierzA
            andgvMacierzA.ColumnHeadersDefaultCellStyle = anStylHeaderKolumn;

            //ustalenie fomatu dla nagłówka wierszy kontrolki dgvMacierzA
            DataGridViewCellStyle anStylHeaderWierszy = new DataGridViewCellStyle();
            anStylHeaderWierszy.Font = new Font("Arial", 10, FontStyle.Bold);
            anStylHeaderWierszy.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            //przypisanie StylHeaderWierszy do kontrolki dgvMacierzA
            andgvMacierzA.RowHeadersDefaultCellStyle = anStylHeaderWierszy;

            //wpisanie nazw (numerów) kolumn
            for (ushort ani = 0; ani < anLiczbaKolumn; ani++)
            {
                //wpisanie nazwy kolumny: (i)
                andgvMacierzA.Columns[ani].HeaderText = String.Format("({0})", ani+1);
                andgvMacierzA.Columns[ani].Width = anSzerokośćKolukny;
                //wycentrowanie zapisów w kolumnie
                andgvMacierzA.Columns[ani].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
                //dodanie kontrolki dgvMacierzA odpowiedzniej liczbt wierszy macierzyA
                for (ushort ani = 0; ani < anLiczbaWierszy; ani++)
                {
                    andgvMacierzA.Rows.Add();
                }

                //wpisanie nazw (numerów) wierszy
                for (ushort ani = 0; ani < anLiczbaWierszy; ani++)
                {
                    //wpisanie nazwy kolumny: (i)
                    andgvMacierzA.Rows[ani].HeaderCell.Value = String.Format("({0})", ani + 1);

                //wycentrowanie zapisów w kolumnie
                andgvMacierzA.Rows[ani].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            //doddanie kontrolki dgvMacierzA do strony 1 zakładki
            tabControlZakladki.TabPages[1].Controls.Add(andgvMacierzA);

            //autosize dla kontrolki MacierzA
            andgvMacierzA.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            //ustawienie braku aktywności dla przycisku - przycisk utwurz
            btnDzialaniaNaMacierzachPowrotDoKokpitu.Enabled = false;

            //uaktywnienie kontrolek 
            btnGenerujElementyMacierzyA.Enabled = true;
            btnAkceptacjaWarMacierzaA.Enabled = true;
            btnUtwórzkontrolkęDataGridViewdlamacierzyB.Enabled = true;
            //to samo dla przycisków macierz b
            btnUwtorzKontrolkeA.Enabled = false;
            txtLiczbaKolumn.Enabled = false;
            txtLiczbaWierszy.Enabled = false;

        }

        public void anUtworzKontrolkeDgvDlaMacierzyC()
        {
            //deklaracje zmiennych dla przechowania rozmiaru macierzy
            ushort anLiczbaWierszy, anLiczbaKolumn;
            //zgasze kontrolki error 
            errorProvider1.Dispose();
            //pobranie liczby wierszy
            if (!ushort.TryParse(txtLiczbaWierszy.Text, out anLiczbaWierszy))
            {
                errorProvider1.SetError(txtLiczbaWierszy, "ERROR: wystąpił niedozwolony znak w zapisie liczby wierszy");
                return;
            }
            //pobranie liczby kolumn
            if (!ushort.TryParse(txtLiczbaKolumn.Text, out anLiczbaKolumn))
            {
                errorProvider1.SetError(txtLiczbaKolumn, "ERROR: wystąpił niedozwolony znak w zapisie liczby kolumn");
                return;
            }
            //Utworzenie egzemplarza kontrolki DataGridView dla macierzy A
            andgvMacierzC = new DataGridView();
            //sformatowanie kontrolki dgvMacierzA
            ushort anodstęp = 10;
            ushort anSzerokośćKolukny = 70; //kontrolki dgv
            ushort anWyskokośćWiersza = 25; //kontrolki dgv
            andgvMacierzC.Location = new Point(groupBox2.Location.X + groupBox2.Width + anodstęp,
                                             andgvMacierzB.Location.Y + andgvMacierzB.Height + anodstęp);
            andgvMacierzC.Size = new Size((anIloscKolumn(anLiczbaKolumn) + 1) * anSzerokośćKolukny, (anIloscWierszy(anLiczbaWierszy) + 1) * anWyskokośćWiersza);
            //Wpisanie liczby kolumn do kontrolki dgv
            andgvMacierzC.ColumnCount = anLiczbaKolumn; ///dlaczego nie +1?
            //odsłonięcie wiersza nagłówkowego dla kolumn
            andgvMacierzC.ColumnHeadersVisible = true;
            //umożliwienie wpisywanie i odczytu danyczh w kontrolce dgv
            andgvMacierzC.ReadOnly = false;
            //uniemożliwienie dodawania nowych wierszy
            andgvMacierzC.AllowUserToAddRows = false;
            //ustawienie trybu AutoSize dla wiersza nagłówkowego kolumn 
            andgvMacierzC.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            //uniemożliwienie multiSelekt komurce w kontrolce
            andgvMacierzC.MultiSelect = false;//?
            //ustalenie fomatu dla nagłówka kolumn kontrolki dgvMacierzA
            DataGridViewCellStyle anStylHeaderKolumn = new DataGridViewCellStyle();
            anStylHeaderKolumn.Font = new Font("Arial", 10, FontStyle.Bold);
            anStylHeaderKolumn.Alignment = DataGridViewContentAlignment.MiddleCenter;
            anStylHeaderKolumn.Format = "    ";

            //przypisanie StylHeaderKolumn do kontrolki dgvMacierzA
            andgvMacierzC.ColumnHeadersDefaultCellStyle = anStylHeaderKolumn;

            //ustalenie fomatu dla nagłówka wierszy kontrolki dgvMacierzA
            DataGridViewCellStyle anStylHeaderWierszy = new DataGridViewCellStyle();
            anStylHeaderWierszy.Font = new Font("Arial", 10, FontStyle.Bold);
            anStylHeaderWierszy.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //przypisanie StylHeaderWierszy do kontrolki dgvMacierzA
            andgvMacierzC.RowHeadersDefaultCellStyle = anStylHeaderWierszy;

            //wpisanie nazw (numerów) kolumn
            for (ushort ani = 0; ani < anLiczbaKolumn; ani++)
            {
                //wpisanie nazwy kolumny: (i)
                andgvMacierzC.Columns[ani].HeaderText = String.Format("({0})", ani + 1);
                andgvMacierzC.Columns[ani].Width = anSzerokośćKolukny;
                //wycentrowanie zapisów w kolumnie
                andgvMacierzC.Columns[ani].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            //dodanie kontrolki dgvMacierzA odpowiedzniej liczbt wierszy macierzyA
            for (ushort ani = 0; ani < anLiczbaWierszy; ani++)
            {
                andgvMacierzC.Rows.Add();
            }

            //wpisanie nazw (numerów) wierszy
            for (ushort ani = 0; ani < anLiczbaWierszy; ani++)
            {
                //wpisanie nazwy kolumny: (i)
                andgvMacierzC.Rows[ani].HeaderCell.Value = String.Format("({0})", ani + 1);

                //wycentrowanie zapisów w kolumnie
                andgvMacierzC.Rows[ani].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            //doddanie kontrolki dgvMacierzB do strony 1 zakładki
            tabControlZakladki.TabPages[1].Controls.Add(andgvMacierzC);

            //autosize dla kontrolki MacierzB
            andgvMacierzC.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            //ustawienie braku aktywności dla przycisku - przycisk utwurz
            btnDzialaniaNaMacierzachPowrotDoKokpitu.Enabled = false;

            //uaktywnienie kontrolek 
            
            //to samo dla przycisków macierz b
        }

        private void btnZatwierdzLiczbeZ1_Click(object sender, EventArgs e)
        {
            //Sprawdzenie poprawności danych 
            errorProvider1.Dispose();
            if (!double.TryParse(txtCzescRzeczywistaZ1.Text, out anCzescRzeczywistaZ1) || anCzescRzeczywistaZ1 == 0)
            {
                errorProvider1.SetError(txtCzescRzeczywistaZ1, "Podana wartość jest nieprawidłowa!");
                return;
            }
            if (!double.TryParse(txtCzescUrojonaZ1.Text, out anCzescUrojonaZ1) || anCzescUrojonaZ1 == 0)
            {
                errorProvider1.SetError(txtCzescUrojonaZ1, "Podana wartość jest nieprawidłowa!");
                return;
            }
            if (!int.TryParse(txtPotegaZ1.Text, out anPotegaz1) || anPotegaz1 == 0)
            {
                errorProvider1.SetError(txtPotegaZ1, "Podana wartość jest nieprawidłowa!");
                return;
            }

            //Zmiana właściwości Enabled kontrolek z danymi na false
            txtCzescRzeczywistaZ1.Enabled = false;
            txtCzescUrojonaZ1.Enabled = false;
            txtPotegaZ1.Enabled = false;

            //Wpisanie wzoru do kontrolki txtWzorZ1
            if(anCzescUrojonaZ1 > 0)
                txtWzorZ1.Text = String.Format("{0}+{1}i^{2}", anCzescRzeczywistaZ1, anCzescUrojonaZ1, anPotegaz1);
            else
                txtWzorZ1.Text = String.Format("{0}{1}i^{2}", anCzescRzeczywistaZ1, anCzescUrojonaZ1, anPotegaz1);

            //Wyłączenie kontrolki Sprawdzenia wzoru z1
            btnZatwierdzLiczbeZ1.Enabled = false;
            btnPowrotDoKokpitu2.Enabled = false;

            //Wpisanie liczby urojonej do tablicy
            anz1 = new anZespolona();
            //Nadanie wartośći obiektowi z1
            anz1[0, 0] = anCzescRzeczywistaZ1;
            anz1[0, 1] = anCzescUrojonaZ1;
            anz1[1, 1] = anPotegaz1;
        }
        private void btnZatwierdzLiczbeZ2_Click(object sender, EventArgs e)
        {
            //Sprawdzenie poprawności danych 
            errorProvider1.Dispose();

            if (!double.TryParse(txtCzescRzeczywistaZ2.Text, out anCzescRzeczywistaZ2) || anCzescRzeczywistaZ1 == 0)
            {
                errorProvider1.SetError(txtCzescRzeczywistaZ2, "Podana wartość jest nieprawidłowa!");
                return;
            }

            if (!double.TryParse(txtCzescUrojonaZ2.Text, out anCzescUrojonaZ2) || anCzescUrojonaZ2 == 0)
            {
                errorProvider1.SetError(txtCzescUrojonaZ2, "Podana wartość jest nieprawidłowa!");
                return;
            }
            if (!int.TryParse(txtPotegaZ2.Text, out anPotegaz2) || anPotegaz2 == 0)
            {
                errorProvider1.SetError(txtPotegaZ2, "Podana wartość jest nieprawidłowa!");
                return;
            }
            //Zmiana właściwości Enabled kontrolek z danymi na false
            txtCzescRzeczywistaZ2.Enabled = false;
            txtCzescUrojonaZ2.Enabled = false;
            txtPotegaZ2.Enabled = false;

            //Wpisanie wzoru do kontrolki txtWzorZ2
            if (anCzescUrojonaZ2 > 0)
                txtWzorZ2.Text = String.Format("{0}+{1}i^{2}", anCzescRzeczywistaZ2, anCzescUrojonaZ2, anPotegaz2);
            else
                txtWzorZ2.Text = String.Format("{0}{1}i^{2}", anCzescRzeczywistaZ2, anCzescUrojonaZ2, anPotegaz2);

            //Wyłączenie kontrolki Sprawdzenia wzoru z2
            btnZatwierdzLiczbeZ2.Enabled = false;
            btnPowrotDoKokpitu2.Enabled = false;
            //Wpisanie liczby urojonej do tablicy
            anz2 = new anZespolona();
            //Nadanie wartośći obiektowi z2
            anz2[0, 0] = anCzescRzeczywistaZ2;
            anz2[0, 1] = anCzescUrojonaZ2;
            anz2[1, 1] = anPotegaz2;
        }

    }
    //deklaracja klasy pomocniczej, która rozszerzy właściwości klasy Macierz
    static class anRozszerzenieWłaściwościKlasyMacierz //nie lepiej dodać property dla klasy Macierz??
    {
        public static void PrzepiszElementyDataGridViewDoMacierzy(this anMacierz anX, DataGridView andgvMacierzX)
        {
            //pobieranie wartośći elementów z kontrolki DataGridView i wpisanie do macierzy X
            for (ushort ani = 0; ani < anX.anLiczbaWierszy; ani++)
                for (ushort anj = 0; anj < anX.anLiczbaKolumn; anj++)
                    anX[ani, anj] = float.Parse((andgvMacierzX.Rows[ani].Cells[anj].Value).ToString());        
        }
        public static void anPrzepiszElementyMacierzyDoKontrolkiDataGridView(this anMacierz anX, DataGridView andgvMacierzX)
        {
            for (ushort ani = 0; ani < anX.anLiczbaWierszy; ani++)
                for (ushort anj = 0; anj < anX.anLiczbaKolumn; anj++)
                    andgvMacierzX.Rows[ani].Cells[anj].Value = string.Format("{0:F2}", anX[ani, anj]);
        }
    }
}
