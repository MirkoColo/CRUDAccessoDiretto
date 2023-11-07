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
using System.Diagnostics;
using System.Reflection;
using static CRUDAccessoDiretto.Form1;

namespace CRUDAccessoDiretto
{
    public partial class Form1 : Form
    {
        public struct Prodotto
        {
            public string nome;
            public int CodProdotto;
        }
        public Prodotto[] p;
        public int dim;
        public int dim1 = 0;
        public int codProd = 1;
        public string cancellazione = ""; //f-fisica ; l-logica
        public int indice = -1;
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
            DELETE.Enabled = false;
            UPDATE.Enabled = false;
            QUANTITA.Enabled = false;
            ConfermaUp.Enabled = false;
            piuQ.Enabled = false;
            menoQ.Enabled = false;
            if (File.Exists("Carrello.dat") == false)
            {
                CreaFile();
            }
            else
            {
                CaricaFile();
                UPDATE.Enabled = true;
                DELETE.Enabled = true;
                QUANTITA.Enabled = true;
            }

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
                bool prodottoEsiste = ProdottoEsiste(NOME.Text);

                if (prodottoEsiste == false)
                {
                    Array.Resize(ref p, p.Length + 1);
                    p[dim].nome = NOME.Text;
                    p[dim].CodProdotto = codProd;

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
            else
            {
                MessageBox.Show("Il prodotto non è stato inserito");
                NOME.Text = "";
                PREZZO.Text = "";
            }
        }

        private void UPDATE_Click(object sender, EventArgs e)
        {
            InputU();
            if (indice == -1)
            {
                MessageBox.Show("Il prodotto che si vuole cambiare non è stato trovato");
            }
            else
            {
                ConfermaUp.Enabled = true;
                QUANTITA.Enabled = false;
                CREATE.Enabled = false;
                UPDATE.Enabled = false;
                DELETE.Enabled = false;

            }
        }

        private void ConfermaUp_Click(object sender, EventArgs e)
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
                p[indice].nome = NOME.Text;
                FileStream f_in_out = new FileStream("Carrello.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryWriter f_out = new BinaryWriter(f_in_out);

                riga = NOME.Text.PadRight(30) + PREZZO.Text.ToString().PadRight(30);
                strInByte = Encoding.Default.GetBytes(riga);
                f_out.BaseStream.Seek((p[indice].CodProdotto - 1) * size, SeekOrigin.Begin);
                f_out.Write(strInByte);

                f_out.Close();
                f_in_out.Close();

                Nome = "@"; Prezzo = 0;

                AggiornaLista();
                NOME.Text = "";
                PREZZO.Text = "";
                CREATE.Enabled = true;
                UPDATE.Enabled = true;
                DELETE.Enabled = true;
                ConfermaUp.Enabled = false;
                QUANTITA.Enabled = true;
                indice = -1;
            }
            else
            {
                MessageBox.Show("Il prodotto non è stato inserito");
                NOME.Text = "";
                PREZZO.Text = "";
            }
        }

        private void ConfermaUpdate_Click(object sender, EventArgs e)
        {
           
        }

        private void DELETE_Click(object sender, EventArgs e)
        {
            InputDelete();
            if (indice == -1)
            {
                MessageBox.Show("Il prodotto da cancellare non è stato trovato");
            }
            else
            {
                InputCancellazione();
                if (cancellazione == "l" || cancellazione == "f")
                {
                    CREATE.Enabled = false;
                    UPDATE.Enabled = false;
                    dim1--;
                    FileStream f_in_out = new FileStream("Carrello.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    BinaryWriter f_out = new BinaryWriter(f_in_out);

                    if (cancellazione == "f")
                    {
                        
                        p[indice].nome = "";
                        Nome = "$";
                        riga = Nome.PadRight(30) + Prezzo.ToString().PadRight(30) + Convert.ToString(Quantita).PadRight(2) + Convert.ToString(Cancellato).PadRight(2);
                        strInByte = Encoding.Default.GetBytes(riga);

                        f_out.BaseStream.Seek((p[indice].CodProdotto - 1) * size, 0);
                        f_out.Write(strInByte);

                        f_out.Close();
                        f_in_out.Close();
                        indice = -1;
                        AggiornaLista();
                        Nome = "@";

                    }
                    if (cancellazione == "l")
                    {

                        string a = "1".PadRight(2);
                        strInByte = Encoding.Default.GetBytes(a);

                        f_out.BaseStream.Seek((p[indice].CodProdotto - 1) * size + 62, SeekOrigin.Begin);
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
                        QUANTITA.Enabled = false;
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
            InputQ();
            if (indice == -1 )
            {
                MessageBox.Show("Il prodotto di cui si vuole cambiare la quantità non è stato trovato");
            }
            else
            {
                QUANTITA.Enabled = false;
                CREATE.Enabled = false;
                UPDATE.Enabled = false;
                DELETE.Enabled = false;
                piuQ.Enabled = true;
                menoQ.Enabled = true;
            }
        }

        //SOMMA 1 ALLA QUANTITA DI UN PRODOTTO
        private void piuQ_Click(object sender, EventArgs e)
        {
            FileStream f_in_out = new FileStream("Carrello.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryWriter f_out = new BinaryWriter(f_in_out);
            BinaryReader f_in = new BinaryReader(f_in_out);
            byte[] br;
            f_in.BaseStream.Seek((p[indice].CodProdotto - 1) * size + 60, SeekOrigin.Begin);
            br = f_in.ReadBytes(2);
            Quantita = int.Parse(Encoding.ASCII.GetString(br, 0, br.Length)) + 1;
            strInByte = Encoding.Default.GetBytes(Quantita.ToString().PadRight(2));

            f_out.BaseStream.Seek((p[indice].CodProdotto - 1) * size + 60, SeekOrigin.Begin);
            f_out.Write(strInByte);

            f_in.Close();
            f_out.Close();
            f_in_out.Close();

            AggiornaLista();
            piuQ.Enabled = false;
            menoQ.Enabled = false;
            CREATE.Enabled = true;
            UPDATE.Enabled = true;
            DELETE.Enabled = true;
            QUANTITA.Enabled = true;
            indice = -1;
        }

        //SOTTRAE 1 ALLA QUANTITA DI UN PRODOTTO

        private void menoQ_Click(object sender, EventArgs e)
        {
            FileStream f_in_out = new FileStream("Carrello.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryReader f_in = new BinaryReader(f_in_out);
            BinaryWriter f_out = new BinaryWriter(f_in_out);
            byte[] br;
            f_in.BaseStream.Seek((p[indice].CodProdotto - 1) * size + 60, SeekOrigin.Begin);
            br = f_in.ReadBytes(2);       
            Quantita = int.Parse(Encoding.ASCII.GetString(br, 0, br.Length));
            if (Quantita > 1)
            {
                f_in.BaseStream.Seek((p[indice].CodProdotto - 1) * size + 60, SeekOrigin.Begin);
                strInByte = Encoding.Default.GetBytes((Quantita - 1).ToString().PadRight(2));              
                f_out.Write(strInByte);      
            }
            f_out.Close();
            f_in.Close();
            f_in_out.Close();
            AggiornaLista();
            piuQ.Enabled = false;
            menoQ.Enabled = false;
            CREATE.Enabled = true;
            UPDATE.Enabled = true;
            DELETE.Enabled = true;
            QUANTITA.Enabled = true;
            indice = -1;
        }

        private void LISTA_SelectedIndexChanged(object sender, EventArgs e)
        {
                       
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
                if (Nome[0] != '@' && Cancellato != 1 && Nome[0] != '$')
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
            bool dollaro = false; //non c'è il dollaro nel nome
            for(int i = 0; i < nome.Length; i++)
            {
                if (nome[i] == '$')
                {
                    dollaro = true;
                }
            }

            bool uscitaPrezzo = false;
            if (prezzo.All(char.IsNumber) && dollaro == false)
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

        //CREAZIONE DEL FILE DI BASE QUANDO VIENE AVVIATO IL PROGRAMMA
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
                cancellazione = Interaction.InputBox(message, title, defaultValue).ToLower();
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
                if (p[i].nome == prodotto && p[i].nome != "$")
                {
                    indice = i;
                }
            }
        }

        public void InputQ()
        {
            string message, title, defaultValue;
            message = "Inserire il nome del prodotto di cui si vuole cambiare la quantità";
            title = "Input Quantità";
            defaultValue = "";
            string prodotto = Interaction.InputBox(message, title, defaultValue);
            for (int i = 0; i < dim; i++)
            {
                if (p[i].nome == prodotto && p[i].nome != "$")
                {
                    indice = i;
                }
            }
        }

        public void InputU()
        {
            string message, title, defaultValue;
            message = "Inserire il nome del prodotto che si vuole cambiare";
            title = "Input Update";
            defaultValue = "";
            string prodotto = Interaction.InputBox(message, title, defaultValue);
            for (int i = 0; i < dim; i++)
            {
                if (p[i].nome == prodotto && p[i].nome != "$")
                {
                    indice = i;
                }
            }
        }

        public void CaricaFile()
        {
            //seek dim * size
            FileStream f_in_out = new FileStream("Carrello.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryReader f_in = new BinaryReader(f_in_out);
            byte[] br;
            bool controllo = false;
            f_in.BaseStream.Seek(0, SeekOrigin.Begin);

            while (controllo == false)
            {
                br = f_in.ReadBytes(30);
                Nome = Encoding.ASCII.GetString(br, 0, br.Length);
                br = f_in.ReadBytes(30);
                Prezzo = int.Parse(Encoding.ASCII.GetString(br, 0, br.Length));
                br = f_in.ReadBytes(2);
                Quantita = int.Parse(Encoding.ASCII.GetString(br, 0, br.Length));
                br = f_in.ReadBytes(2);
                Cancellato = int.Parse(Encoding.ASCII.GetString(br, 0, br.Length));
                if (Nome[0] == '@')
                {
                    controllo = true;
                    break;
                }
                else
                {
                    Array.Resize(ref p, p.Length + 1);
                    p[dim].nome = Nome.Trim();
                    p[dim].CodProdotto = codProd;
                    codProd++;
                    dim++;
                }
            }
            
            f_in.Close();
            f_in_out.Close();
            AggiornaLista();

        }

        public bool ProdottoEsiste(string nom)
        {
            bool uscita = false;
            for (int i = 0; i < dim; i++)
            {
                if (p[i].nome == nom)
                {
                    uscita = true;
                    break;
                }
            }
            return uscita;
        }
        private void ESCI_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //APRI FILE
        private void VisualizzaFie_Click(object sender, EventArgs e)
        {
            string percorso = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Carrello.dat");
            Process.Start(percorso);
        }
    }    
}
