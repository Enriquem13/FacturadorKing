namespace Facturador
{
    partial class bCasoparadoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(bCasoparadoc));
            this.label11 = new System.Windows.Forms.Label();
            this.tbClave = new System.Windows.Forms.TextBox();
            this.listViewCasos = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button4 = new System.Windows.Forms.Button();
            this.cbPais = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tbCasoid = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbDenominacion = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbreferencia = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbPrioridad = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbCliente = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbInteresado = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbregistro = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbexpediente = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCasonumero = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbTiposolicitud = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(395, 209);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 57;
            this.label11.Text = "Clave";
            // 
            // tbClave
            // 
            this.tbClave.Location = new System.Drawing.Point(448, 204);
            this.tbClave.Name = "tbClave";
            this.tbClave.Size = new System.Drawing.Size(46, 20);
            this.tbClave.TabIndex = 56;
            // 
            // listViewCasos
            // 
            this.listViewCasos.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewCasos.AllowColumnReorder = true;
            this.listViewCasos.AllowDrop = true;
            this.listViewCasos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader11});
            this.listViewCasos.LabelEdit = true;
            this.listViewCasos.Location = new System.Drawing.Point(12, 255);
            this.listViewCasos.Name = "listViewCasos";
            this.listViewCasos.Size = new System.Drawing.Size(932, 372);
            this.listViewCasos.TabIndex = 55;
            this.listViewCasos.UseCompatibleStateImageBehavior = false;
            this.listViewCasos.View = System.Windows.Forms.View.Details;
            this.listViewCasos.SelectedIndexChanged += new System.EventHandler(this.listViewCasos_SelectedIndexChanged);
            this.listViewCasos.DoubleClick += new System.EventHandler(this.listViewCasos_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "País";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "CasoId";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Caso";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Tipo";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Expediente";
            this.columnHeader5.Width = 123;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Registro";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Interesado";
            this.columnHeader7.Width = 101;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Título/denominación";
            this.columnHeader8.Width = 179;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Cliente";
            this.columnHeader9.Width = 94;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Referencia";
            this.columnHeader11.Width = 127;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 53;
            this.button4.Text = "Menu";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // cbPais
            // 
            this.cbPais.FormattingEnabled = true;
            this.cbPais.Location = new System.Drawing.Point(218, 203);
            this.cbPais.Name = "cbPais";
            this.cbPais.Size = new System.Drawing.Size(162, 21);
            this.cbPais.TabIndex = 52;
            this.cbPais.SelectedIndexChanged += new System.EventHandler(this.cbPais_SelectedIndexChanged);
            this.cbPais.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbTiposolicitud_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(161, 206);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 51;
            this.label10.Text = "País";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(853, 74);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 50;
            this.button3.Text = "wp";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(853, 47);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(75, 20);
            this.textBox10.TabIndex = 49;
            this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(853, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 47;
            this.button1.Text = "Consulta";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbCasoid
            // 
            this.tbCasoid.Location = new System.Drawing.Point(218, 172);
            this.tbCasoid.Name = "tbCasoid";
            this.tbCasoid.Size = new System.Drawing.Size(162, 20);
            this.tbCasoid.TabIndex = 46;
            this.tbCasoid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbTiposolicitud_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(150, 179);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 45;
            this.label9.Text = "CasoId";
            // 
            // tbDenominacion
            // 
            this.tbDenominacion.Location = new System.Drawing.Point(218, 110);
            this.tbDenominacion.Name = "tbDenominacion";
            this.tbDenominacion.Size = new System.Drawing.Size(162, 20);
            this.tbDenominacion.TabIndex = 44;
            this.tbDenominacion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbTiposolicitud_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(93, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 13);
            this.label8.TabIndex = 43;
            this.label8.Text = "Denominación Titulo";
            // 
            // tbreferencia
            // 
            this.tbreferencia.Location = new System.Drawing.Point(622, 48);
            this.tbreferencia.Name = "tbreferencia";
            this.tbreferencia.Size = new System.Drawing.Size(162, 20);
            this.tbreferencia.TabIndex = 42;
            this.tbreferencia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbTiposolicitud_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(536, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "Referencia";
            // 
            // tbPrioridad
            // 
            this.tbPrioridad.Location = new System.Drawing.Point(622, 107);
            this.tbPrioridad.Name = "tbPrioridad";
            this.tbPrioridad.Size = new System.Drawing.Size(162, 20);
            this.tbPrioridad.TabIndex = 40;
            this.tbPrioridad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbTiposolicitud_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(544, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 39;
            this.label6.Text = "Prioridad ";
            // 
            // tbCliente
            // 
            this.tbCliente.Location = new System.Drawing.Point(622, 17);
            this.tbCliente.Name = "tbCliente";
            this.tbCliente.Size = new System.Drawing.Size(162, 20);
            this.tbCliente.TabIndex = 38;
            this.tbCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbTiposolicitud_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(556, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Cliente";
            // 
            // tbInteresado
            // 
            this.tbInteresado.Location = new System.Drawing.Point(622, 79);
            this.tbInteresado.Name = "tbInteresado";
            this.tbInteresado.Size = new System.Drawing.Size(162, 20);
            this.tbInteresado.TabIndex = 36;
            this.tbInteresado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbTiposolicitud_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(536, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Interesado";
            // 
            // tbregistro
            // 
            this.tbregistro.Location = new System.Drawing.Point(218, 142);
            this.tbregistro.Name = "tbregistro";
            this.tbregistro.Size = new System.Drawing.Size(162, 20);
            this.tbregistro.TabIndex = 34;
            this.tbregistro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbTiposolicitud_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(144, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Registro";
            // 
            // tbexpediente
            // 
            this.tbexpediente.Location = new System.Drawing.Point(218, 79);
            this.tbexpediente.Name = "tbexpediente";
            this.tbexpediente.Size = new System.Drawing.Size(162, 20);
            this.tbexpediente.TabIndex = 32;
            this.tbexpediente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbTiposolicitud_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(137, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Expediente";
            // 
            // textBoxCasonumero
            // 
            this.textBoxCasonumero.Location = new System.Drawing.Point(218, 49);
            this.textBoxCasonumero.Name = "textBoxCasonumero";
            this.textBoxCasonumero.Size = new System.Drawing.Size(162, 20);
            this.textBoxCasonumero.TabIndex = 30;
            this.textBoxCasonumero.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbTiposolicitud_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(166, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Caso";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(556, 142);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 13);
            this.label12.TabIndex = 58;
            this.label12.Text = "Holder";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(622, 139);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 59;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbTiposolicitud_KeyDown);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(731, 139);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(53, 20);
            this.textBox2.TabIndex = 60;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(118, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 13);
            this.label13.TabIndex = 61;
            this.label13.Text = "Tipo de Solicitud";
            // 
            // cbTiposolicitud
            // 
            this.cbTiposolicitud.FormattingEnabled = true;
            this.cbTiposolicitud.Location = new System.Drawing.Point(218, 18);
            this.cbTiposolicitud.Name = "cbTiposolicitud";
            this.cbTiposolicitud.Size = new System.Drawing.Size(162, 21);
            this.cbTiposolicitud.TabIndex = 62;
            this.cbTiposolicitud.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbTiposolicitud_KeyDown);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(622, 176);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(162, 21);
            this.comboBox2.TabIndex = 63;
            this.comboBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbTiposolicitud_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(562, 181);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(33, 13);
            this.label14.TabIndex = 64;
            this.label14.Text = "Clase";
            // 
            // bCasoparadoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 639);
            this.ControlBox = false;
            this.Controls.Add(this.label14);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.cbTiposolicitud);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbClave);
            this.Controls.Add(this.listViewCasos);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.cbPais);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbCasoid);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbDenominacion);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbreferencia);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbPrioridad);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbCliente);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbInteresado);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbregistro);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbexpediente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxCasonumero);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "bCasoparadoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buscar caso";
            this.Load += new System.EventHandler(this.bMarcas_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbClave;
        public System.Windows.Forms.ListView listViewCasos;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox cbPais;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox10;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbCasoid;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbDenominacion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbreferencia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbPrioridad;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbCliente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbInteresado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbregistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbexpediente;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCasonumero;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbTiposolicitud;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label14;
    }
}