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
using Microsoft.VisualBasic;

namespace CRUDAccessoDiretto
{
    public partial class Form1 : Form
    {
        public struct Prodotto
        {
            public string nome;
            public int quantita;
            public int CodProdotto;
            public bool cancellato;
        }
        public Prodotto[] p;
        public int dim;
        public int dim1 = 0;
        public int codProd = 1;
        bool Confermadelete = false;
        bool Confermaupdate = false;
        bool ConfermaQuantita = false;
        public string cancellazione = ""; //f-fisica ; l-logica
        public int indiceCanc = -1;
        public int[] CodProdotto;
        public string Nome = "@"; public int Prezzo = 0; public int Quantita = 0; public int Cancellato = 0;  // campi per record vuoto
        public int size = 64;  // lunghezza (30+30+4) del record PREFISSATA
        public int NumeroRecord;
        public string riga;
        public byte[] strInByte;
        
        public Form1()
        {
            InitializeComponent();
            dim = 0;
            p = new Prodotto[dim];
            CodProdotto = new int[dim];
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
                Array.Resize(ref p, p.Length + 1);
                p[dim].nome = NOME.Text;
                p[dim].quantita = 1;
                p[dim].CodProdotto = codProd;
                p[dim].cancellato = false;

                FileStream f_in_out = new FileStream("Carrello.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryWriter f_out = new BinaryWriter(f_in_out);

                riga = NOME.Text.PadRight(30) + PREZZO.Text.PadRight(30) + Convert.ToString(Quantita + 1).PadRight(2) + Convert.ToString(Cancellato).PadRight(2);
                strInByte = Encoding.Default.GetBytes(riga);
                f_out.BaseStream.Seek((p[dim].CodProdotto - 1) * size, SeekOrigin.Begin);
                f_out.Write(strInByte);

                f_out.Close();
                f_in_out.Close();

                Nome = "@"; Prezzo = 0; Quantita = 0; Cancellato = 0;
                codProd++;
                dim++;
                dim1++;
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

            InputDelete();
            if (indiceCanc == -1)
            {
                MessageBox.Show("Il prodotto da cancellare non è stato trovato");
            }
            else
            {
                InputCancellazione();
                if (cancellazione == "l" || cancellazione == "f")
                {
                    Confermadelete = true;
                    CREATE.Enabled = false;
                    UPDATE.Enabled = false;
                    QUANTITA.Enabled = false;
                    ConfermaUpdate.Enabled = false;
                    dim1--;
                    FileStream f_in_out = new FileStream("Carrello.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    BinaryWriter f_out = new BinaryWriter(f_in_out);

                    if (cancellazione == "f")
                    {
                        p[indiceCanc].nome = "";
                        p[indiceCanc].cancellato = true;
                        riga = Nome.PadRight(30) + Prezzo.ToString().PadRight(30) + Convert.ToString(Quantita).PadRight(2) + Convert.ToString(Cancellato).PadRight(2);
                        strInByte = Encoding.Default.GetBytes(riga);

                        f_out.BaseStream.Seek((p[indiceCanc].CodProdotto - 1) * size, 0);
                        f_out.Write(strInByte);

                        f_out.Close();
                        f_in_out.Close();
                        indiceCanc = -1;
                        AggiornaLista();
                    }
                    if (cancellazione == "l")
                    {
                        p[indiceCanc].cancellato = true;

                        string a = "1".PadRight(2);
                        strInByte = Encoding.Default.GetBytes(a);

                        f_out.BaseStream.Seek((p[indiceCanc].CodProdotto - 1) * size + 62, SeekOrigin.Begin);
                        f_out.Write(strInByte);

                        f_out.Close();
                        f_in_out.Close();

                        AggiornaLista();

                    }
                    if (dim1 <= 0)
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
            }
            
        }



            private void button1_Click(object sender, EventArgs e) //QUANTITA BUTTON
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
                       
            //UPDATE
            if (Confermaupdate == true)
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
            FileStream f_in_out = new FileStream("Carrello.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryReader f_in = new BinaryReader(f_in_out);
            LISTA.Items.Clear();
            riga = Nome.PadRight(30) + Prezzo.ToString().PadRight(30) + Convert.ToString(Quantita).PadRight(2) + Convert.ToString(Cancellato).PadRight(2);
            byte[] br;
            f_in.BaseStream.Seek(0, SeekOrigin.Begin);

            for (int i = 0; i < dim; i++)
            {       
                br = f_in.ReadBytes(30);
                Nome = Encoding.ASCII.GetString(br, 0, br.Length);
                br = f_in.ReadBytes(30);
                Prezzo = int.Parse(Encoding.ASCII.GetString(br, 0, br.Length));
                br = f_in.ReadBytes(2);
                Quantita = int.Parse(Encoding.ASCII.GetString(br, 0, br.Length));
                br = f_in.ReadBytes(2);
                Cancellato = int.Parse(Encoding.ASCII.GetString(br, 0, br.Length));
                if (Nome[0] != '@' && Cancellato != 1)
                {                         
                    LISTA.Items.Add($"{Nome.Trim()};{Prezzo.ToString().Trim()}euro;{Quantita.ToString().Trim()};{Cancellato.ToString().Trim()}");
                }               
            }
            Nome = "@"; Prezzo = 0; Quantita = 0; Cancellato = 0;
            f_in.Close();
            f_in_out.Close();
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
            riga = Nome.PadRight(30) + Prezzo.ToString().PadRight(30) + Convert.ToString(Quantita).PadRight(2) + Convert.ToString(Cancellato).PadRight(2); ;
            strInByte = Encoding.Default.GetBytes(riga);
            for (int i = 1; i <= 100; i++)
                f_out.Write(strInByte);  

            f_out.Close();
            f_in_out.Close();
        }

        //GESTIONE INPUT CON VISUAL BASIC
        public string InputCancellazione()
        {
            string message //ISTRUZIONI FORNITE ALL'UTENTE
                , title, //TITOLO MESSAGGIO
                defaultValue; //MESSAGGIO DI BASE
            bool controllo = false;
            message = "Inserire f per la cancellazione fisica o l per la cancellazione logica";
            title = "Input Cancellazione";
            defaultValue = "";
            do
            {
                cancellazione = Interaction.InputBox(message, title, defaultValue);
                if (cancellazione == "")
                {
                    controllo = true;
                }
                else if (cancellazione == "l")
                {
                    cancellazione = "l";
                    controllo = true;
                }
                else if (cancellazione == "f")
                {
                    cancellazione = "f";
                    controllo = true;
                }
                else
                {
                    MessageBox.Show(message);
                }
            } while (controllo == false);


            return cancellazione;
        }

        public void InputDelete()
        {
            string message, title, defaultValue; 
            message = "Inserire il nome del prodotto da cancellare";
            title = "Input Delete";
            defaultValue = "";
            string prodotto = Interaction.InputBox(message, title, defaultValue); 

            for(int i = 0; i < dim; i++)
            {
                if (p[i].nome == prodotto)
                {
                    indiceCanc = i;
                }
            }
        }


    }    
}
