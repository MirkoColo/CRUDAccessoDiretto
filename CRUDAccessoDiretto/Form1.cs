using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDAccessoDiretto
{
    public partial class Form1 : Form
    {
        public struct Prodotto
        {
            public string nome;
            public float prezzo;
            public int quantita;
        }
        public Prodotto[] p;
        public int dim;
        public int quanti = 1;
        public int dimCodProdotto;
        bool Confermadelete = false;
        bool Confermaupdate = false;
        bool ConfermaQuantita = false;
        public int[] CodProdotto;
        public string Nome = "@"; public string Prezzo = "@"; public int Quantita = 0;  // campi per record vuoto
        public int size = 64;  // lunghezza (30+30+4) del record PREFISSATA
        public int NumeroRecord;
        public string riga;
        public byte[] strInByte;
        public Form1()
        {
            InitializeComponent();
            dim = 0;
            dimCodProdotto = 0;
            p = new Prodotto[dim];
            CodProdotto = new int[dimCodProdotto];
            DELETE.Enabled = false;
            UPDATE.Enabled = false;
            QUANTITA.Enabled = false;
            piuQ.Enabled = false;
            menoQ.Enabled = false;
            ConfermaUpdate.Enabled = false;
            CreaFile();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void CREATE_Click(object sender, EventArgs e)
        {
            bool controllo = ControlloInserimento(NOME.Text, PREZZO.Text);

            if (NOME.Text == "" && PREZZO.Text == "")
            {
                MessageBox.Show("Inserire il nome e il prezzo del prodotto");
            }
            else if (NOME.Text == "")
            {
                MessageBox.Show("Inserire il nome del prodotto");
            }
            else if (PREZZO.Text == "")
            {
                MessageBox.Show("Inserire il prezzo del prodotto");
            }
            else if (controllo == true)
            {
                //if (ControlloQuantita(NOME.Text, float.Parse(PREZZO.Text)) == false)
                Array.Resize(ref p, p.Length + 1);
                p[dim].nome = NOME.Text;
                p[dim].prezzo = float.Parse(PREZZO.Text);
                p[dim].quantita = 1;
                dim++;
                //MessageBox.Show($"Il prodotto {NOME.Text} al prezzo di {PREZZO.Text} euro è stato aggiunto al carrello");
                NOME.Text = "";
                PREZZO.Text = "";
                AggiornaLista();
                UPDATE.Enabled = true;
                DELETE.Enabled = true;
                QUANTITA.Enabled = true;
            }
            else
            {
                MessageBox.Show("Il prodotto non è stato inserito");
                NOME.Text = "";
                PREZZO.Text = "";
            }
        }

        private void UPDATE_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Selezionare il prodotto da cancellare e poi Inserire il nome e il prezzo del nuovo prodotto");
            Confermaupdate = true;
            CREATE.Enabled = false;
            UPDATE.Enabled = false;
            DELETE.Enabled = false; 
            ConfermaUpdate.Enabled = true;
        }

        private void ConfermaUpdate_Click(object sender, EventArgs e)
        {
            bool controllo = ControlloInserimento(NOME.Text, PREZZO.Text);

            if (NOME.Text == "" && PREZZO.Text == "")
            {
                MessageBox.Show("Inserire il nome e il prezzo del nuovo prodotto");
            }
            else if (NOME.Text == "")
            {
                MessageBox.Show("Inserire il nome del nuovo prodotto");
            }
            else if (PREZZO.Text == "")
            {
                MessageBox.Show("Inserire il prezzo del nuovo prodotto");
            }
            else if (controllo == true)
            {
                int indiceLista = LISTA.SelectedIndex;
                p[indiceLista].nome = NOME.Text;
                p[indiceLista].prezzo = float.Parse(PREZZO.Text);
                AggiornaLista();
                ConfermaUpdate.Enabled = false;
                NOME.Text = "";
                PREZZO.Text = "";
                CREATE.Enabled = true;
                UPDATE.Enabled = true;
                DELETE.Enabled = true;
            }
            else
            {
                MessageBox.Show("Il prodotto non è stato inserito");
                NOME.Text = "";
                PREZZO.Text = "";
            }
        }

        private void DELETE_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Selezionare il prodotto da cancellare");
            Confermadelete = true;
            CREATE.Enabled = false;
            UPDATE.Enabled = false;
            QUANTITA.Enabled = false;
            ConfermaUpdate.Enabled = false;
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Selezionare il prodotto di cui si vuole cambiare la quantità");
            ConfermaQuantita = true;
            CREATE.Enabled = false;
            UPDATE.Enabled = false;
            DELETE.Enabled = false;
        }
        private void piuQ_Click(object sender, EventArgs e)
        {
            p[LISTA.SelectedIndex].quantita += 1;
            AggiornaLista();
            piuQ.Enabled = false;
            menoQ.Enabled = false;
            CREATE.Enabled = true;
            UPDATE.Enabled = true;
            DELETE.Enabled = true;
        }

        private void menoQ_Click(object sender, EventArgs e)
        {
            if (p[LISTA.SelectedIndex].quantita != 1)
            {
                p[LISTA.SelectedIndex].quantita--;
                AggiornaLista();              
            }
            menoQ.Enabled = false;
            piuQ.Enabled = false;
            CREATE.Enabled = true;
            UPDATE.Enabled = true;
            DELETE.Enabled = true;
        }

        private void LISTA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Confermadelete == true)
            {
                Confermadelete = false;
                for (int i = LISTA.SelectedIndex; i < p.Length - 1; i++)
                {
                    p[i].nome = p[i + 1].nome;
                    p[i].prezzo = p[i + 1].prezzo;
                }
                Array.Resize(ref p, p.Length - 1);
                dim--;
                AggiornaLista();
                if (dim == 0)
                {
                    UPDATE.Enabled = false;
                    DELETE.Enabled = false;
                    CREATE.Enabled = true;
                }
                else
                {
                    CREATE.Enabled = true;
                    UPDATE.Enabled = true;
                    DELETE.Enabled = true;
                }
            }
            //UPDATE
            else if (Confermaupdate == true)
            {
                ConfermaUpdate.Enabled = true;
                UPDATE.Enabled = false;
            }
            else if (ConfermaQuantita == true)
            {
                ConfermaQuantita = false;
                piuQ.Enabled = true;
                menoQ.Enabled = true;
            }
        }
        //FUNZIONI DI SERVIZIO

        public void AggiornaLista() 
        {
            LISTA.Items.Clear();
            for (int i = 0; i < dim; i++)
            {
                LISTA.Items.Add($"{p[i].nome};{p[i].prezzo}euro;{p[i].quantita}");
            }
        }
        public bool ControlloInserimento(string nome, string prezzo)
        {
            bool uscitaPrezzo = false;
            if (prezzo.All(char.IsNumber))
            {
                uscitaPrezzo = true;
            }
            return uscitaPrezzo;
        }

        

        /*
        public bool ControlloQuantita(string nome, float prezzo)
        {
            bool esiste = false;

            for(int i = 0; i < dim; i++)
            {
                if (p[i].nome == nome && p[i].prezzo == prezzo)
                {
                    
                    p[i].quantita++;
                    esiste = true;
                    break;
                }
            }
            return esiste;
        }
        */

        public void CreaFile()
        {
            FileStream f_in_out = new FileStream("Carrello.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryWriter f_out = new BinaryWriter(f_in_out);
            riga = Nome.PadRight(30) + Prezzo.PadRight(30) + (Quantita.ToString()).PadRight(4);
            strInByte = Encoding.Default.GetBytes(riga);
            for (int i = 1; i <= 100; i++)
                f_out.Write(strInByte);  
        }
    }
}
