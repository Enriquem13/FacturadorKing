namespace Facturador
{
    partial class CapturaSolicitud
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CapturaSolicitud));
            this.label7 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.rtObservacion = new System.Windows.Forms.RichTextBox();
            this.tbDocumentofecharecepcion = new System.Windows.Forms.TextBox();
            this.cbEsritos = new System.Windows.Forms.ComboBox();
            this.tbEstatus = new System.Windows.Forms.TextBox();
            this.rtbtitulo = new System.Windows.Forms.RichTextBox();
            this.tbFolio = new System.Windows.Forms.TextBox();
            this.tbPaisRegistro = new System.Windows.Forms.TextBox();
            this.tbCodigo = new System.Windows.Forms.TextBox();
            this.tbTipo = new System.Windows.Forms.TextBox();
            this.tbCasonum = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFilename = new System.Windows.Forms.TextBox();
            this.tbExpediente__ = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(237, 309);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 13);
            this.label7.TabIndex = 95;
            this.label7.Text = "Seleccione Solicitud";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(43, 351);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(121, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "Cargar Archivo";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(225, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(43, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "Buscar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1089, 531);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 90;
            this.button2.Text = "Guardar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1089, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 89;
            this.button1.Text = "Salir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rtObservacion
            // 
            this.rtObservacion.Location = new System.Drawing.Point(163, 437);
            this.rtObservacion.Name = "rtObservacion";
            this.rtObservacion.Size = new System.Drawing.Size(733, 98);
            this.rtObservacion.TabIndex = 7;
            this.rtObservacion.Text = "";
            // 
            // tbDocumentofecharecepcion
            // 
            this.tbDocumentofecharecepcion.Location = new System.Drawing.Point(496, 99);
            this.tbDocumentofecharecepcion.Name = "tbDocumentofecharecepcion";
            this.tbDocumentofecharecepcion.Size = new System.Drawing.Size(100, 20);
            this.tbDocumentofecharecepcion.TabIndex = 2;
            this.tbDocumentofecharecepcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDocumentofecharecepcion_KeyPress);
            this.tbDocumentofecharecepcion.Validating += new System.ComponentModel.CancelEventHandler(this.tbDocumentofecharecepcion_Validating);
            // 
            // cbEsritos
            // 
            this.cbEsritos.FormattingEnabled = true;
            this.cbEsritos.Location = new System.Drawing.Point(364, 306);
            this.cbEsritos.Name = "cbEsritos";
            this.cbEsritos.Size = new System.Drawing.Size(404, 21);
            this.cbEsritos.TabIndex = 5;
            this.cbEsritos.SelectedIndexChanged += new System.EventHandler(this.cbEsritos_SelectedIndexChanged);
            // 
            // tbEstatus
            // 
            this.tbEstatus.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tbEstatus.Location = new System.Drawing.Point(364, 231);
            this.tbEstatus.Name = "tbEstatus";
            this.tbEstatus.ReadOnly = true;
            this.tbEstatus.Size = new System.Drawing.Size(348, 20);
            this.tbEstatus.TabIndex = 84;
            // 
            // rtbtitulo
            // 
            this.rtbtitulo.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.rtbtitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbtitulo.Location = new System.Drawing.Point(681, 40);
            this.rtbtitulo.Name = "rtbtitulo";
            this.rtbtitulo.ReadOnly = true;
            this.rtbtitulo.Size = new System.Drawing.Size(405, 102);
            this.rtbtitulo.TabIndex = 83;
            this.rtbtitulo.Text = "";
            // 
            // tbFolio
            // 
            this.tbFolio.Location = new System.Drawing.Point(428, 159);
            this.tbFolio.Name = "tbFolio";
            this.tbFolio.Size = new System.Drawing.Size(247, 20);
            this.tbFolio.TabIndex = 4;
            // 
            // tbPaisRegistro
            // 
            this.tbPaisRegistro.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tbPaisRegistro.Location = new System.Drawing.Point(455, 52);
            this.tbPaisRegistro.Name = "tbPaisRegistro";
            this.tbPaisRegistro.ReadOnly = true;
            this.tbPaisRegistro.Size = new System.Drawing.Size(156, 20);
            this.tbPaisRegistro.TabIndex = 81;
            // 
            // tbCodigo
            // 
            this.tbCodigo.Location = new System.Drawing.Point(123, 159);
            this.tbCodigo.Name = "tbCodigo";
            this.tbCodigo.Size = new System.Drawing.Size(188, 20);
            this.tbCodigo.TabIndex = 3;
            this.tbCodigo.TextChanged += new System.EventHandler(this.tbCodigo_TextChanged);
            // 
            // tbTipo
            // 
            this.tbTipo.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tbTipo.Location = new System.Drawing.Point(388, 19);
            this.tbTipo.Name = "tbTipo";
            this.tbTipo.ReadOnly = true;
            this.tbTipo.Size = new System.Drawing.Size(247, 20);
            this.tbTipo.TabIndex = 78;
            // 
            // tbCasonum
            // 
            this.tbCasonum.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tbCasonum.Location = new System.Drawing.Point(135, 19);
            this.tbCasonum.Name = "tbCasonum";
            this.tbCasonum.ReadOnly = true;
            this.tbCasonum.Size = new System.Drawing.Size(69, 20);
            this.tbCasonum.TabIndex = 77;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(298, 231);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 13);
            this.label13.TabIndex = 76;
            this.label13.Text = "Estatus";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(370, 166);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 74;
            this.label11.Text = "Folio";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(343, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 72;
            this.label9.Text = "Tipo";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(69, 437);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 71;
            this.label8.Text = "Observación";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(370, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 13);
            this.label6.TabIndex = 70;
            this.label6.Text = "Fecha sello  de solicitud";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 67;
            this.label3.Text = "Código de Barras";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 66;
            this.label2.Text = "Expediente";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 65;
            this.label1.Text = "Caso";
            // 
            // tbFilename
            // 
            this.tbFilename.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tbFilename.Location = new System.Drawing.Point(204, 354);
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.ReadOnly = true;
            this.tbFilename.Size = new System.Drawing.Size(577, 20);
            this.tbFilename.TabIndex = 96;
            // 
            // tbExpediente__
            // 
            this.tbExpediente__.Location = new System.Drawing.Point(123, 103);
            this.tbExpediente__.Name = "tbExpediente__";
            this.tbExpediente__.Size = new System.Drawing.Size(196, 20);
            this.tbExpediente__.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(513, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 12);
            this.label4.TabIndex = 99;
            this.label4.Text = "día / mes  /Año";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(361, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 100;
            this.label5.Text = "País de Registro";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(840, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 101;
            this.label10.Text = "Título";
            // 
            // CapturaSolicitud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1175, 566);
            this.ControlBox = false;
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbExpediente__);
            this.Controls.Add(this.tbFilename);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rtObservacion);
            this.Controls.Add(this.tbDocumentofecharecepcion);
            this.Controls.Add(this.cbEsritos);
            this.Controls.Add(this.tbEstatus);
            this.Controls.Add(this.rtbtitulo);
            this.Controls.Add(this.tbFolio);
            this.Controls.Add(this.tbPaisRegistro);
            this.Controls.Add(this.tbCodigo);
            this.Controls.Add(this.tbTipo);
            this.Controls.Add(this.tbCasonum);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CapturaSolicitud";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Captura de Solicitud";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox rtObservacion;
        private System.Windows.Forms.TextBox tbDocumentofecharecepcion;
        private System.Windows.Forms.ComboBox cbEsritos;
        private System.Windows.Forms.TextBox tbEstatus;
        private System.Windows.Forms.RichTextBox rtbtitulo;
        private System.Windows.Forms.TextBox tbFolio;
        private System.Windows.Forms.TextBox tbPaisRegistro;
        private System.Windows.Forms.TextBox tbCodigo;
        private System.Windows.Forms.TextBox tbTipo;
        private System.Windows.Forms.TextBox tbCasonum;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFilename;
        private System.Windows.Forms.TextBox tbExpediente__;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
    }
}