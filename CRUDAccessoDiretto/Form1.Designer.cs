namespace CRUDAccessoDiretto
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.LISTA = new System.Windows.Forms.ListBox();
            this.NOME = new System.Windows.Forms.TextBox();
            this.PREZZO = new System.Windows.Forms.TextBox();
            this.No = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.UPDATE = new System.Windows.Forms.Button();
            this.DELETE = new System.Windows.Forms.Button();
            this.CREATE = new System.Windows.Forms.Button();
            this.QUANTITA = new System.Windows.Forms.Button();
            this.piuQ = new System.Windows.Forms.Button();
            this.menoQ = new System.Windows.Forms.Button();
            this.ConfermaUp = new System.Windows.Forms.Button();
            this.ESCI = new System.Windows.Forms.Button();
            this.VisualizzaFie = new System.Windows.Forms.Button();
            this.RECUPERA = new System.Windows.Forms.Button();
            this.TrovaProdotto = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LISTA
            // 
            this.LISTA.FormattingEnabled = true;
            this.LISTA.Location = new System.Drawing.Point(596, 97);
            this.LISTA.Name = "LISTA";
            this.LISTA.Size = new System.Drawing.Size(545, 420);
            this.LISTA.TabIndex = 0;
            this.LISTA.SelectedIndexChanged += new System.EventHandler(this.LISTA_SelectedIndexChanged);
            // 
            // NOME
            // 
            this.NOME.Location = new System.Drawing.Point(165, 155);
            this.NOME.Name = "NOME";
            this.NOME.Size = new System.Drawing.Size(146, 20);
            this.NOME.TabIndex = 1;
            // 
            // PREZZO
            // 
            this.PREZZO.Location = new System.Drawing.Point(165, 208);
            this.PREZZO.Name = "PREZZO";
            this.PREZZO.Size = new System.Drawing.Size(146, 20);
            this.PREZZO.TabIndex = 2;
            // 
            // No
            // 
            this.No.AutoSize = true;
            this.No.Location = new System.Drawing.Point(164, 139);
            this.No.Name = "No";
            this.No.Size = new System.Drawing.Size(35, 13);
            this.No.TabIndex = 3;
            this.No.Text = "Nome";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Prezzo";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // UPDATE
            // 
            this.UPDATE.Location = new System.Drawing.Point(339, 256);
            this.UPDATE.Name = "UPDATE";
            this.UPDATE.Size = new System.Drawing.Size(146, 49);
            this.UPDATE.TabIndex = 6;
            this.UPDATE.Text = "UPDATE";
            this.UPDATE.UseVisualStyleBackColor = true;
            this.UPDATE.Click += new System.EventHandler(this.UPDATE_Click);
            // 
            // DELETE
            // 
            this.DELETE.Location = new System.Drawing.Point(339, 335);
            this.DELETE.Name = "DELETE";
            this.DELETE.Size = new System.Drawing.Size(146, 49);
            this.DELETE.TabIndex = 7;
            this.DELETE.Text = "DELETE";
            this.DELETE.UseVisualStyleBackColor = true;
            this.DELETE.Click += new System.EventHandler(this.DELETE_Click);
            // 
            // CREATE
            // 
            this.CREATE.Location = new System.Drawing.Point(339, 174);
            this.CREATE.Name = "CREATE";
            this.CREATE.Size = new System.Drawing.Size(146, 49);
            this.CREATE.TabIndex = 8;
            this.CREATE.Text = "CREATE";
            this.CREATE.UseVisualStyleBackColor = true;
            this.CREATE.Click += new System.EventHandler(this.CREATE_Click);
            // 
            // QUANTITA
            // 
            this.QUANTITA.Location = new System.Drawing.Point(186, 335);
            this.QUANTITA.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.QUANTITA.Name = "QUANTITA";
            this.QUANTITA.Size = new System.Drawing.Size(99, 49);
            this.QUANTITA.TabIndex = 9;
            this.QUANTITA.Text = "Quantità";
            this.QUANTITA.UseVisualStyleBackColor = true;
            this.QUANTITA.Click += new System.EventHandler(this.button1_Click);
            // 
            // piuQ
            // 
            this.piuQ.Location = new System.Drawing.Point(186, 388);
            this.piuQ.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.piuQ.Name = "piuQ";
            this.piuQ.Size = new System.Drawing.Size(41, 30);
            this.piuQ.TabIndex = 10;
            this.piuQ.Text = "+";
            this.piuQ.UseVisualStyleBackColor = true;
            this.piuQ.Click += new System.EventHandler(this.piuQ_Click);
            // 
            // menoQ
            // 
            this.menoQ.Location = new System.Drawing.Point(242, 388);
            this.menoQ.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.menoQ.Name = "menoQ";
            this.menoQ.Size = new System.Drawing.Size(43, 30);
            this.menoQ.TabIndex = 11;
            this.menoQ.Text = "-";
            this.menoQ.UseVisualStyleBackColor = true;
            this.menoQ.Click += new System.EventHandler(this.menoQ_Click);
            // 
            // ConfermaUp
            // 
            this.ConfermaUp.Location = new System.Drawing.Point(166, 256);
            this.ConfermaUp.Name = "ConfermaUp";
            this.ConfermaUp.Size = new System.Drawing.Size(146, 49);
            this.ConfermaUp.TabIndex = 12;
            this.ConfermaUp.Text = "Conferma Update";
            this.ConfermaUp.UseVisualStyleBackColor = true;
            this.ConfermaUp.Click += new System.EventHandler(this.ConfermaUp_Click);
            // 
            // ESCI
            // 
            this.ESCI.Location = new System.Drawing.Point(375, 541);
            this.ESCI.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ESCI.Name = "ESCI";
            this.ESCI.Size = new System.Drawing.Size(73, 42);
            this.ESCI.TabIndex = 13;
            this.ESCI.Text = "ESCI";
            this.ESCI.UseVisualStyleBackColor = true;
            this.ESCI.Click += new System.EventHandler(this.ESCI_Click);
            // 
            // VisualizzaFie
            // 
            this.VisualizzaFie.Location = new System.Drawing.Point(339, 467);
            this.VisualizzaFie.Name = "VisualizzaFie";
            this.VisualizzaFie.Size = new System.Drawing.Size(146, 49);
            this.VisualizzaFie.TabIndex = 14;
            this.VisualizzaFie.Text = "Visualizza File";
            this.VisualizzaFie.UseVisualStyleBackColor = true;
            this.VisualizzaFie.Click += new System.EventHandler(this.VisualizzaFie_Click);
            // 
            // RECUPERA
            // 
            this.RECUPERA.Location = new System.Drawing.Point(339, 404);
            this.RECUPERA.Name = "RECUPERA";
            this.RECUPERA.Size = new System.Drawing.Size(146, 49);
            this.RECUPERA.TabIndex = 15;
            this.RECUPERA.Text = "Recupera";
            this.RECUPERA.UseVisualStyleBackColor = true;
            this.RECUPERA.Click += new System.EventHandler(this.RECUPERA_Click);
            // 
            // TrovaProdotto
            // 
            this.TrovaProdotto.Location = new System.Drawing.Point(165, 467);
            this.TrovaProdotto.Name = "TrovaProdotto";
            this.TrovaProdotto.Size = new System.Drawing.Size(146, 49);
            this.TrovaProdotto.TabIndex = 16;
            this.TrovaProdotto.Text = "Trova Prodotto";
            this.TrovaProdotto.UseVisualStyleBackColor = true;
            this.TrovaProdotto.Click += new System.EventHandler(this.TrovaProdotto_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1308, 656);
            this.Controls.Add(this.TrovaProdotto);
            this.Controls.Add(this.RECUPERA);
            this.Controls.Add(this.VisualizzaFie);
            this.Controls.Add(this.ESCI);
            this.Controls.Add(this.ConfermaUp);
            this.Controls.Add(this.menoQ);
            this.Controls.Add(this.piuQ);
            this.Controls.Add(this.QUANTITA);
            this.Controls.Add(this.CREATE);
            this.Controls.Add(this.DELETE);
            this.Controls.Add(this.UPDATE);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.No);
            this.Controls.Add(this.PREZZO);
            this.Controls.Add(this.NOME);
            this.Controls.Add(this.LISTA);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LISTA;
        private System.Windows.Forms.TextBox NOME;
        private System.Windows.Forms.TextBox PREZZO;
        private System.Windows.Forms.Label No;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button UPDATE;
        private System.Windows.Forms.Button DELETE;
        private System.Windows.Forms.Button CREATE;
        private System.Windows.Forms.Button QUANTITA;
        private System.Windows.Forms.Button piuQ;
        private System.Windows.Forms.Button menoQ;
        private System.Windows.Forms.Button ConfermaUp;
        private System.Windows.Forms.Button ESCI;
        private System.Windows.Forms.Button VisualizzaFie;
        private System.Windows.Forms.Button RECUPERA;
        private System.Windows.Forms.Button TrovaProdotto;
    }
}

