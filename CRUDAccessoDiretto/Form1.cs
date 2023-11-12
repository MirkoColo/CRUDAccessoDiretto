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
        public int codProd = 1;
        public int indiceCanc = -1;
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
            if (File.Exists("Carrello.dat") == false)
            {
                CreaFile();
                
            }
            else
            {
                CaricaFile();
            }
            piuQ.Enabled = false;
            menoQ.Enabled = false;
            ConfermaUp.Enabled = false;
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

                    RicercaCanc();
                    FileStream f_in_out = new FileStream("Carrello.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    BinaryWriter f_out = new BinaryWriter(f_in_out);
                    if (indiceCanc == -1)
                    {
                        Array.Resize(ref p, p.Length + 1);
                        p[dim].nome = NOME.Text;
                        p[dim].CodProdotto = codProd;
                        f_out.BaseStream.Seek((p[dim].CodProdotto - 1) * size, SeekOrigin.Begin);
                        codProd++;
                        dim++;
                    }
                    else
                    {
                        for(int i = 0; i < dim; i++)
                        {
                            MessageBox.Show(p[i].nome);
                        }
                        MessageBox.Show(indiceCanc.ToString());
                        p[indiceCanc].nome = NOME.Text;
                        f_out.BaseStream.Seek((indiceCanc) * size, SeekOrigin.Begin);
                        indiceCanc = -1;
                    }                   
                    riga = NOME.Text.PadRight(30) + PREZZO.Text.PadRight(30) + Convert.ToString(Quantita + 1).PadRight(2) + Convert.ToString(Cancellato).PadRight(2);
                    strInByte = Encoding.Default.GetBytes(riga);                
                    f_out.Write(strInByte);
                    f_out.Close();
                    f_in_out.Close();
                    Nome = "@"; Prezzo = 0; Quantita = 0; Cancellato = 0;                 
                    NOME.Text = "";
                    PREZZO.Text = "";
                    AggiornaLista();
                    //OrdinamentoAlfabetico();
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
                CREATE.Enabled = false;
                DELETE.Enabled = false;
                QUANTITA.Enabled = false;
                RECUPERA.Enabled = false;
            }
        }

        private void ConfermaUp_Click(object sender, EventArgs e)
        {
            bool controllo = ControlloInserimento(NOME.Text, PREZZO.Text);
            bool prodottoEsiste = ProdottoEsiste(NOME.Text);
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
            else if (controllo == true && prodottoEsiste == false)
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
                ConfermaUp.Enabled = false;
                CREATE.Enabled = true;
                DELETE.Enabled = true;
                QUANTITA.Enabled = true;
                RECUPERA.Enabled = true;
                indice = -1;
                //OrdinamentoAlfabetico();
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
                    FileStream f_in_out = new FileStream("Carrello.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    BinaryWriter f_out = new BinaryWriter(f_in_out);

                    if (cancellazione == "f")
                    {
                        
                        p[indice].nome = "@";
                        Nome = "@";
                        Prezzo = 0;
                        Quantita = 0;
                        riga = Nome.PadRight(30) + Prezzo.ToString().PadRight(30) + Convert.ToString(Quantita).PadRight(2) + Convert.ToString(Cancellato).PadRight(2);
                        strInByte = Encoding.Default.GetBytes(riga);

                        f_out.BaseStream.Seek((p[indice].CodProdotto - 1) * size, 0);
                        f_out.Write(strInByte);

                        f_out.Close();
                        f_in_out.Close();
                        indice = -1;
                        AggiornaLista();

                    }
                    if (cancellazione == "l")
                    {

                        string a = "1".PadRight(2);
                        strInByte = Encoding.Default.GetBytes(a);

                        f_out.BaseStream.Seek((p[indice].CodProdotto - 1) * size + 62, SeekOrigin.Begin);
                        f_out.Write(strInByte);

                        f_out.Close();
                        f_in_out.Close();
                        indice = -1;
                        AggiornaLista();

                    }
                }
            }
            
        }


        private void button1_Click(object sender, EventArgs e) //QUANTITA BUTTON
        {
            InputQ();
            if (indice == -1)
            {
                MessageBox.Show("Il prodotto di cui si vuole cambiare la quantità non è stato trovato");
            }
            else
            {
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
            QUANTITA.Enabled = true;
            indice = -1;
        }

        private void RECUPERA_Click(object sender, EventArgs e)
        {
            string message, title, defaultValue;
            message = "Inserire il nome del prodotto che si vuole recuperare(";
            title = "Input Recupero";
            defaultValue = "";

            FileStream f_in_out = new FileStream("Carrello.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryReader f_in = new BinaryReader(f_in_out);
            byte[] br;
            string[] ar = new string[0];
            int l = 0;
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
                if (Cancellato == 1)
                {
                    Array.Resize(ref ar, ar.Length + 1);
                    ar[l] = Nome.Trim();
                    l++;
                    message += Nome.Trim() + ";";
                }
            }
            message += ")";
            if (l == 0)
            {
                MessageBox.Show("Non ci sono prodotti da recuperare");
            }
            else
            {
                string prodotto = Interaction.InputBox(message, title, defaultValue);
                int ind = -1;
                for (int i = 0; i < l; i++)
                {
                    if (ar[i] == prodotto)
                    {
                        ind = i;
                        break;
                    }
                }
                if (ind == -1)
                {
                    MessageBox.Show("Non è stato trovato il prodotto da recuperare");
                }
                else
                {
                    bool uscita = false;
                    BinaryWriter f_out = new BinaryWriter(f_in_out);
                    f_in.BaseStream.Seek(0, SeekOrigin.Begin);
                    while (uscita == false)
                    {

                        br = f_in.ReadBytes(30);
                        Nome = Encoding.ASCII.GetString(br, 0, br.Length);
                        br = f_in.ReadBytes(30);
                        Prezzo = int.Parse(Encoding.ASCII.GetString(br, 0, br.Length));
                        br = f_in.ReadBytes(2);
                        Quantita = int.Parse(Encoding.ASCII.GetString(br, 0, br.Length));
                        if (Nome.Trim() == prodotto)
                        {
                            string a = "0".PadRight(2);
                            strInByte = Encoding.Default.GetBytes(a);
                            f_out.Write(strInByte);
                            uscita = true;
                        }
                        else
                        {
                            br = f_in.ReadBytes(2);
                            Cancellato = int.Parse(Encoding.ASCII.GetString(br, 0, br.Length));
                        }
                    }
                    f_out.Close();

                }
            }
            f_in.Close();
            f_in_out.Close();
            AggiornaLista();
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
                    LISTA.Items.Add($"{Nome.Trim()};{Prezzo.ToString().Trim()}euro;{Quantita.ToString().Trim()}");
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
                if (nome[i] == '@')
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

            FileStream f_in_out = new FileStream("Carrello.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryReader f_in = new BinaryReader(f_in_out);
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
                if (p[i].nome == prodotto)
                {
                    indice = i;
                    if (Cancellato == 1)
                    {
                        indice = -1;
                    }
                    Cancellato = 0;
                }
            }

            f_in.Close();
            f_in_out.Close();
        }

        public void InputQ()
        {
            string message, title, defaultValue;
            message = "Inserire il nome del prodotto di cui si vuole cambiare la quantità";
            title = "Input Quantità";
            defaultValue = "";
            string prodotto = Interaction.InputBox(message, title, defaultValue);
            FileStream f_in_out = new FileStream("Carrello.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryReader f_in = new BinaryReader(f_in_out);
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
                if (p[i].nome == prodotto)
                {
                    indice = i;
                    if (Cancellato == 1)
                    {
                        indice = -1;
                    }
                    Cancellato = 0;
                }
            }


            f_in.Close();
            f_in_out.Close();

        }

        public void InputU()
        {
            string message, title, defaultValue;
            message = "Inserire il nome del prodotto che si vuole cambiare";
            title = "Input Update";
            defaultValue = "";
            string prodotto = Interaction.InputBox(message, title, defaultValue);

            FileStream f_in_out = new FileStream("Carrello.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryReader f_in = new BinaryReader(f_in_out);
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
                if (p[i].nome == prodotto)
                {
                    indice = i;                  
                    if (Cancellato == 1)
                    {
                        indice = -1;
                    }
                    Cancellato = 0;
                }
            }

            
            f_in.Close();
            f_in_out.Close();
        }

        public void CaricaFile()
        {
            StreamReader sr = new StreamReader(@"struct.txt");
            string line = sr.ReadLine();
           while(line != null)
            {
                Array.Resize(ref p, p.Length + 1);
                string[] arrSplit = Split(line);
                p[dim].nome = arrSplit[0];
                p[dim].CodProdotto = int.Parse(arrSplit[1]);
                codProd++;
                dim++;
                line = sr.ReadLine();
            }


            //OrdinamentoAlfabetico();
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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            string message, title, defaultValue;
            message = "Vuoi cancellare i prodotti?(si,no)";
            title = "Input scelta";
            defaultValue = "";
            bool uscita = false;
            while (uscita == false)
            {
                string scelta = Interaction.InputBox(message, title, defaultValue).ToLower();
                if (scelta == "si")
                {
                    StreamWriter sw = new StreamWriter("struct.txt");
                    sw.Close();
                    CreaFile();
                    uscita = true;
                }
                else if (scelta == "no")
                {
                    StreamWriter sw = new StreamWriter("struct.txt");
                    for (int i = 0; i < dim; i++)
                    {
                        sw.WriteLine(p[i].nome + ";" + p[i].CodProdotto);
                    }
                    sw.Close();
                    uscita = true;
                    
                }
            }

        }

        public void RicercaCanc()
        {

            for(int i = 0; i < dim; i++)
            {
                if (p[i].nome == "@")
                {
                    indiceCanc = i;
                    break;
                }
            }
        }
        
        /*
        public void OrdinamentoAlfabetico()
        {
            for(int i = 0; i < dim; i++)
            {
                for(int j = i + 1; j < dim; j++)
                {
                    if (p[i].nome.CompareTo(p[j].nome) > 0)
                    {
                        string a = p[i].nome;
                        p[i].nome = p[j].nome;
                        p[j].nome = a;
                        int b = p[i].CodProdotto;
                        p[i].CodProdotto = p[j].CodProdotto;
                        p[j].CodProdotto = b;

                    }
                }
            }
        }
        */

        

        static string[] Split(string stringa)
        {

            string[] array = new string[2];
            string frase = "";
            int p = 0;
            for (int i = 0; i < stringa.Length; i++)
            {
                if (stringa[i] == ';')
                {
                    array[p] = frase;
                    p++;
                    frase = "";
                }
                else
                {
                    frase += stringa[i];
                }

                if (i == stringa.Length - 1)
                {
                    array[p] = frase;
                }
            }
            return array;
        }
        /*
        public void RicercaBinaria(string prodotto)
        {
            int s = 0;
            int r = dim;
            bool uscita = false;

            while(s <= r && uscita == false)
            {
                int centro = (s + r) / 2;
                int trovato = string.Compare(p[centro].nome, prodotto);

                if(trovato == 0)
                {
                    indice = centro;
                    uscita = true;
                }
                else if(trovato < 0)
                {
                    s = centro + 1;
                }
                else
                {
                    r = centro - 1;
                }
            }

        }
        */
    }    
}
