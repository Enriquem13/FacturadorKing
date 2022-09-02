namespace Facturador
{
    partial class tarifas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tarifas));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboGrupo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboEstatus = new System.Windows.Forms.ComboBox();
            this.comboConceptocargo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rAct = new System.Windows.Forms.RadioButton();
            this.rInactivo = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tCeuro = new System.Windows.Forms.TextBox();
            this.tCdolar = new System.Windows.Forms.TextBox();
            this.tCpeso = new System.Windows.Forms.TextBox();
            this.tDerechos = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.listviewtarifas = new System.Windows.Forms.ListView();
            this.cCliente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cConcepto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cDerechos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cCostop = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cCostod = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cCostoeu = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cActivo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(31, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Seleccione cliente";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(34, 88);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(199, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(34, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Seleccione Grupo";
            // 
            // comboGrupo
            // 
            this.comboGrupo.FormattingEnabled = true;
            this.comboGrupo.Location = new System.Drawing.Point(37, 173);
            this.comboGrupo.Name = "comboGrupo";
            this.comboGrupo.Size = new System.Drawing.Size(196, 21);
            this.comboGrupo.TabIndex = 3;
            this.comboGrupo.SelectedIndexChanged += new System.EventHandler(this.comboGrupo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(34, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Seleccione estatus";
            // 
            // comboEstatus
            // 
            this.comboEstatus.FormattingEnabled = true;
            this.comboEstatus.Location = new System.Drawing.Point(37, 272);
            this.comboEstatus.Name = "comboEstatus";
            this.comboEstatus.Size = new System.Drawing.Size(196, 21);
            this.comboEstatus.TabIndex = 5;
            this.comboEstatus.SelectedIndexChanged += new System.EventHandler(this.comboEstatus_SelectedIndexChanged);
            // 
            // comboConceptocargo
            // 
            this.comboConceptocargo.FormattingEnabled = true;
            this.comboConceptocargo.Location = new System.Drawing.Point(34, 359);
            this.comboConceptocargo.Name = "comboConceptocargo";
            this.comboConceptocargo.Size = new System.Drawing.Size(482, 21);
            this.comboConceptocargo.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(31, 331);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(202, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Seleccione concepto de Cargo";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(998, 352);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 28);
            this.button1.TabIndex = 8;
            this.button1.Text = "Agregar tarifas";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rAct);
            this.groupBox1.Controls.Add(this.rInactivo);
            this.groupBox1.Location = new System.Drawing.Point(970, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(167, 59);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Activo";
            // 
            // rAct
            // 
            this.rAct.AutoSize = true;
            this.rAct.Location = new System.Drawing.Point(21, 25);
            this.rAct.Name = "rAct";
            this.rAct.Size = new System.Drawing.Size(55, 17);
            this.rAct.TabIndex = 17;
            this.rAct.TabStop = true;
            this.rAct.Text = "Activo";
            this.rAct.UseVisualStyleBackColor = true;
            // 
            // rInactivo
            // 
            this.rInactivo.AutoSize = true;
            this.rInactivo.Location = new System.Drawing.Point(88, 25);
            this.rInactivo.Name = "rInactivo";
            this.rInactivo.Size = new System.Drawing.Size(63, 17);
            this.rInactivo.TabIndex = 18;
            this.rInactivo.TabStop = true;
            this.rInactivo.Text = "Inactivo";
            this.rInactivo.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(551, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 15);
            this.label7.TabIndex = 30;
            this.label7.Text = "Costo Dolar:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(413, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 29;
            this.label5.Text = "Costo peso:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(700, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 15);
            this.label6.TabIndex = 28;
            this.label6.Text = "Costo Euro:";
            // 
            // tCeuro
            // 
            this.tCeuro.Location = new System.Drawing.Point(703, 70);
            this.tCeuro.Name = "tCeuro";
            this.tCeuro.Size = new System.Drawing.Size(100, 20);
            this.tCeuro.TabIndex = 27;
            // 
            // tCdolar
            // 
            this.tCdolar.Location = new System.Drawing.Point(554, 70);
            this.tCdolar.Name = "tCdolar";
            this.tCdolar.Size = new System.Drawing.Size(100, 20);
            this.tCdolar.TabIndex = 26;
            // 
            // tCpeso
            // 
            this.tCpeso.Location = new System.Drawing.Point(416, 70);
            this.tCpeso.Name = "tCpeso";
            this.tCpeso.Size = new System.Drawing.Size(100, 20);
            this.tCpeso.TabIndex = 25;
            // 
            // tDerechos
            // 
            this.tDerechos.Location = new System.Drawing.Point(290, 70);
            this.tDerechos.Name = "tDerechos";
            this.tDerechos.Size = new System.Drawing.Size(100, 20);
            this.tDerechos.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(287, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 15);
            this.label8.TabIndex = 23;
            this.label8.Text = "Derechos:";
            // 
            // listviewtarifas
            // 
            this.listviewtarifas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cCliente,
            this.cConcepto,
            this.cDerechos,
            this.cCostop,
            this.cCostod,
            this.cCostoeu,
            this.cActivo});
            this.listviewtarifas.HoverSelection = true;
            this.listviewtarifas.Location = new System.Drawing.Point(37, 417);
            this.listviewtarifas.Name = "listviewtarifas";
            this.listviewtarifas.Size = new System.Drawing.Size(1157, 231);
            this.listviewtarifas.TabIndex = 32;
            this.listviewtarifas.UseCompatibleStateImageBehavior = false;
            this.listviewtarifas.View = System.Windows.Forms.View.Details;
            // 
            // cCliente
            // 
            this.cCliente.Text = "Cliente";
            this.cCliente.Width = 243;
            // 
            // cConcepto
            // 
            this.cConcepto.Text = "Concepto";
            this.cConcepto.Width = 465;
            // 
            // cDerechos
            // 
            this.cDerechos.Text = "Derechos";
            this.cDerechos.Width = 72;
            // 
            // cCostop
            // 
            this.cCostop.Text = "Costo Peso";
            this.cCostop.Width = 87;
            // 
            // cCostod
            // 
            this.cCostod.Text = "Costo Dolar";
            this.cCostod.Width = 96;
            // 
            // cCostoeu
            // 
            this.cCostoeu.Text = "Costo Euro";
            this.cCostoeu.Width = 92;
            // 
            // cActivo
            // 
            this.cActivo.Text = "Activo";
            this.cActivo.Width = 76;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1119, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 33;
            this.button2.Text = "Salir";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 34;
            this.button3.Text = "Menú";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tarifas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1206, 660);
            this.ControlBox = false;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listviewtarifas);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tCeuro);
            this.Controls.Add(this.tCdolar);
            this.Controls.Add(this.tCpeso);
            this.Controls.Add(this.tDerechos);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboConceptocargo);
            this.Controls.Add(this.comboEstatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboGrupo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "tarifas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tarifas clientes";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboGrupo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboEstatus;
        private System.Windows.Forms.ComboBox comboConceptocargo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rAct;
        private System.Windows.Forms.RadioButton rInactivo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tCeuro;
        private System.Windows.Forms.TextBox tCdolar;
        private System.Windows.Forms.TextBox tCpeso;
        private System.Windows.Forms.TextBox tDerechos;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView listviewtarifas;
        private System.Windows.Forms.ColumnHeader cCliente;
        private System.Windows.Forms.ColumnHeader cConcepto;
        private System.Windows.Forms.ColumnHeader cDerechos;
        private System.Windows.Forms.ColumnHeader cCostop;
        private System.Windows.Forms.ColumnHeader cCostod;
        private System.Windows.Forms.ColumnHeader cCostoeu;
        private System.Windows.Forms.ColumnHeader cActivo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}