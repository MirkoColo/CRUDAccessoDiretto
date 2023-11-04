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
            this.SuspendLayout();
            // 
            // LISTA
            // 
            this.LISTA.FormattingEnabled = true;
            this.LISTA.ItemHeight = 16;
            this.LISTA.Location = new System.Drawing.Point(795, 119);
            this.LISTA.Margin = new System.Windows.Forms.Padding(4);
            this.LISTA.Name = "LISTA";
            this.LISTA.Size = new System.Drawing.Size(725, 516);
            this.LISTA.TabIndex = 0;
            this.LISTA.SelectedIndexChanged += new System.EventHandler(this.LISTA_SelectedIndexChanged);
            // 
            // NOME
            // 
            this.NOME.Location = new System.Drawing.Point(220, 191);
            this.NOME.Margin = new System.Windows.Forms.Padding(4);
            this.NOME.Name = "NOME";
            this.NOME.Size = new System.Drawing.Size(193, 22);
            this.NOME.TabIndex = 1;
            // 
            // PREZZO
            // 
            this.PREZZO.Location = new System.Drawing.Point(220, 256);
            this.PREZZO.Margin = new System.Windows.Forms.Padding(4);
            this.PREZZO.Name = "PREZZO";
            this.PREZZO.Size = new System.Drawing.Size(193, 22);
            this.PREZZO.TabIndex = 2;
            // 
            // No
            // 
            this.No.AutoSize = true;
            this.No.Location = new System.Drawing.Point(219, 171);
            this.No.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.No.Name = "No";
            this.No.Size = new System.Drawing.Size(44, 16);
            this.No.TabIndex = 3;
            this.No.Text = "Nome";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 236);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Prezzo";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // UPDATE
            // 
            this.UPDATE.Location = new System.Drawing.Point(452, 315);
            this.UPDATE.Margin = new System.Windows.Forms.Padding(4);
            this.UPDATE.Name = "UPDATE";
            this.UPDATE.Size = new System.Drawing.Size(195, 60);
            this.UPDATE.TabIndex = 6;
            this.UPDATE.Text = "UPDATE";
            this.UPDATE.UseVisualStyleBackColor = true;
            this.UPDATE.Click += new System.EventHandler(this.UPDATE_Click);
            // 
            // DELETE
            // 
            this.DELETE.Location = new System.Drawing.Point(452, 412);
            this.DELETE.Margin = new System.Windows.Forms.Padding(4);
            this.DELETE.Name = "DELETE";
            this.DELETE.Size = new System.Drawing.Size(195, 60);
            this.DELETE.TabIndex = 7;
            this.DELETE.Text = "DELETE";
            this.DELETE.UseVisualStyleBackColor = true;
            this.DELETE.Click += new System.EventHandler(this.DELETE_Click);
            // 
            // CREATE
            // 
            this.CREATE.Location = new System.Drawing.Point(452, 214);
            this.CREATE.Margin = new System.Windows.Forms.Padding(4);
            this.CREATE.Name = "CREATE";
            this.CREATE.Size = new System.Drawing.Size(195, 60);
            this.CREATE.TabIndex = 8;
            this.CREATE.Text = "CREATE";
            this.CREATE.UseVisualStyleBackColor = true;
            this.CREATE.Click += new System.EventHandler(this.CREATE_Click);
            // 
            // QUANTITA
            // 
            this.QUANTITA.Location = new System.Drawing.Point(248, 412);
            this.QUANTITA.Name = "QUANTITA";
            this.QUANTITA.Size = new System.Drawing.Size(132, 60);
            this.QUANTITA.TabIndex = 9;
            this.QUANTITA.Text = "Quantità";
            this.QUANTITA.UseVisualStyleBackColor = true;
            this.QUANTITA.Click += new System.EventHandler(this.button1_Click);
            // 
            // piuQ
            // 
            this.piuQ.Location = new System.Drawing.Point(248, 478);
            this.piuQ.Name = "piuQ";
            this.piuQ.Size = new System.Drawing.Size(55, 37);
            this.piuQ.TabIndex = 10;
            this.piuQ.Text = "+";
            this.piuQ.UseVisualStyleBackColor = true;
            this.piuQ.Click += new System.EventHandler(this.piuQ_Click);
            // 
            // menoQ
            // 
            this.menoQ.Location = new System.Drawing.Point(323, 478);
            this.menoQ.Name = "menoQ";
            this.menoQ.Size = new System.Drawing.Size(57, 37);
            this.menoQ.TabIndex = 11;
            this.menoQ.Text = "-";
            this.menoQ.UseVisualStyleBackColor = true;
            this.menoQ.Click += new System.EventHandler(this.menoQ_Click);
            // 
            // ConfermaUp
            // 
            this.ConfermaUp.Location = new System.Drawing.Point(222, 315);
            this.ConfermaUp.Margin = new System.Windows.Forms.Padding(4);
            this.ConfermaUp.Name = "ConfermaUp";
            this.ConfermaUp.Size = new System.Drawing.Size(195, 60);
            this.ConfermaUp.TabIndex = 12;
            this.ConfermaUp.Text = "Conferma Update";
            this.ConfermaUp.UseVisualStyleBackColor = true;
            this.ConfermaUp.Click += new System.EventHandler(this.ConfermaUp_Click);
            // 
            // ESCI
            // 
            this.ESCI.Location = new System.Drawing.Point(502, 583);
            this.ESCI.Name = "ESCI";
            this.ESCI.Size = new System.Drawing.Size(97, 52);
            this.ESCI.TabIndex = 13;
            this.ESCI.Text = "ESCI";
            this.ESCI.UseVisualStyleBackColor = true;
            this.ESCI.Click += new System.EventHandler(this.ESCI_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1744, 807);
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
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
    }
}

