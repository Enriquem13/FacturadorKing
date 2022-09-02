
namespace Facturador.plazos_forms
{
    partial class agregaplazo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(agregaplazo));
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbGrupo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbgrupo_plazo = new System.Windows.Forms.ComboBox();
            this.cbEstadosplazos = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbTipo_plazo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbPlazosdelcaso = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbFechaatendioplazo = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tbAnualidadid = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.lFechavencimiento = new System.Windows.Forms.Label();
            this.tbFechadVencimineto = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbCasonumero = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbDocumentos = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.tbFechaNotificacion = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(793, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 18);
            this.label6.TabIndex = 25;
            this.label6.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(442, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 18);
            this.label5.TabIndex = 24;
            this.label5.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Grupo";
            // 
            // cbGrupo
            // 
            this.cbGrupo.FormattingEnabled = true;
            this.cbGrupo.Location = new System.Drawing.Point(126, 78);
            this.cbGrupo.Name = "cbGrupo";
            this.cbGrupo.Size = new System.Drawing.Size(295, 21);
            this.cbGrupo.TabIndex = 16;
            this.cbGrupo.SelectedIndexChanged += new System.EventHandler(this.cbGrupo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Grupo plazo";
            // 
            // cbgrupo_plazo
            // 
            this.cbgrupo_plazo.FormattingEnabled = true;
            this.cbgrupo_plazo.Location = new System.Drawing.Point(127, 116);
            this.cbgrupo_plazo.Name = "cbgrupo_plazo";
            this.cbgrupo_plazo.Size = new System.Drawing.Size(295, 21);
            this.cbgrupo_plazo.TabIndex = 18;
            this.cbgrupo_plazo.SelectedIndexChanged += new System.EventHandler(this.cbgrupo_plazo_SelectedIndexChanged);
            // 
            // cbEstadosplazos
            // 
            this.cbEstadosplazos.FormattingEnabled = true;
            this.cbEstadosplazos.Location = new System.Drawing.Point(654, 73);
            this.cbEstadosplazos.Name = "cbEstadosplazos";
            this.cbEstadosplazos.Size = new System.Drawing.Size(121, 21);
            this.cbEstadosplazos.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Tipo plazo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(595, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Estado";
            // 
            // cbTipo_plazo
            // 
            this.cbTipo_plazo.Enabled = false;
            this.cbTipo_plazo.FormattingEnabled = true;
            this.cbTipo_plazo.Location = new System.Drawing.Point(126, 151);
            this.cbTipo_plazo.Name = "cbTipo_plazo";
            this.cbTipo_plazo.Size = new System.Drawing.Size(295, 21);
            this.cbTipo_plazo.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(442, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 18);
            this.label7.TabIndex = 26;
            this.label7.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(442, 151);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 18);
            this.label8.TabIndex = 27;
            this.label8.Text = "*";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(47, 276);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 29;
            this.label10.Text = "Plazos del caso:";
            // 
            // cbPlazosdelcaso
            // 
            this.cbPlazosdelcaso.FormattingEnabled = true;
            this.cbPlazosdelcaso.Location = new System.Drawing.Point(161, 273);
            this.cbPlazosdelcaso.Name = "cbPlazosdelcaso";
            this.cbPlazosdelcaso.Size = new System.Drawing.Size(534, 21);
            this.cbPlazosdelcaso.TabIndex = 28;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(44, 195);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 13);
            this.label11.TabIndex = 31;
            this.label11.Text = "Plazo relacionado:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.lFechavencimiento);
            this.groupBox1.Controls.Add(this.tbFechadVencimineto);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tbCasonumero);
            this.groupBox1.Controls.Add(this.cbGrupo);
            this.groupBox1.Controls.Add(this.cbTipo_plazo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbgrupo_plazo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbEstadosplazos);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(35, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(987, 225);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Campos obligatorios:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(748, 315);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(104, 13);
            this.label17.TabIndex = 37;
            this.label17.Text = "Fecha atendió Plazo";
            // 
            // tbFechaatendioplazo
            // 
            this.tbFechaatendioplazo.Location = new System.Drawing.Point(871, 312);
            this.tbFechaatendioplazo.Name = "tbFechaatendioplazo";
            this.tbFechaatendioplazo.Size = new System.Drawing.Size(121, 20);
            this.tbFechaatendioplazo.TabIndex = 36;
            this.tbFechaatendioplazo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbFechaatendioplazo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            this.tbFechaatendioplazo.Validating += new System.ComponentModel.CancelEventHandler(this.tbFechaatendioplazo_Validating);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(793, 115);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(14, 18);
            this.label15.TabIndex = 35;
            this.label15.Text = "*";
            // 
            // tbAnualidadid
            // 
            this.tbAnualidadid.Location = new System.Drawing.Point(871, 355);
            this.tbAnualidadid.Name = "tbAnualidadid";
            this.tbAnualidadid.Size = new System.Drawing.Size(121, 20);
            this.tbAnualidadid.TabIndex = 34;
            this.tbAnualidadid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(787, 358);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 13);
            this.label14.TabIndex = 33;
            this.label14.Text = "AnualidadID";
            // 
            // lFechavencimiento
            // 
            this.lFechavencimiento.AutoSize = true;
            this.lFechavencimiento.Location = new System.Drawing.Point(522, 120);
            this.lFechavencimiento.Name = "lFechavencimiento";
            this.lFechavencimiento.Size = new System.Drawing.Size(113, 13);
            this.lFechavencimiento.TabIndex = 32;
            this.lFechavencimiento.Text = "Fecha de Vencimiento";
            // 
            // tbFechadVencimineto
            // 
            this.tbFechadVencimineto.Location = new System.Drawing.Point(654, 117);
            this.tbFechadVencimineto.Name = "tbFechadVencimineto";
            this.tbFechadVencimineto.Size = new System.Drawing.Size(121, 20);
            this.tbFechadVencimineto.TabIndex = 31;
            this.tbFechadVencimineto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbFechadVencimineto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFechadVencimineto_KeyPress);
            this.tbFechadVencimineto.Validating += new System.ComponentModel.CancelEventHandler(this.tbFechadVencimineto_Validating);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(232, 41);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 18);
            this.label12.TabIndex = 30;
            this.label12.Text = "*";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 29;
            this.label9.Text = "Caso número";
            // 
            // tbCasonumero
            // 
            this.tbCasonumero.Location = new System.Drawing.Point(126, 39);
            this.tbCasonumero.Name = "tbCasonumero";
            this.tbCasonumero.ReadOnly = true;
            this.tbCasonumero.Size = new System.Drawing.Size(100, 20);
            this.tbCasonumero.TabIndex = 28;
            this.tbCasonumero.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(25, 323);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(113, 13);
            this.label13.TabIndex = 34;
            this.label13.Text = "Documentos del caso:";
            // 
            // cbDocumentos
            // 
            this.cbDocumentos.FormattingEnabled = true;
            this.cbDocumentos.Location = new System.Drawing.Point(161, 315);
            this.cbDocumentos.Name = "cbDocumentos";
            this.cbDocumentos.Size = new System.Drawing.Size(534, 21);
            this.cbDocumentos.TabIndex = 33;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(925, 403);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 31);
            this.button1.TabIndex = 35;
            this.button1.Text = "Agregar plazo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(756, 275);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(96, 13);
            this.label16.TabIndex = 38;
            this.label16.Text = "Fecha Notificación";
            // 
            // tbFechaNotificacion
            // 
            this.tbFechaNotificacion.Location = new System.Drawing.Point(871, 274);
            this.tbFechaNotificacion.Name = "tbFechaNotificacion";
            this.tbFechaNotificacion.Size = new System.Drawing.Size(121, 20);
            this.tbFechaNotificacion.TabIndex = 39;
            this.tbFechaNotificacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFechaNotificacion_KeyPress);
            this.tbFechaNotificacion.Validating += new System.ComponentModel.CancelEventHandler(this.tbFechaNotificacion_Validating);
            // 
            // agregaplazo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 455);
            this.Controls.Add(this.tbFechaNotificacion);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbFechaatendioplazo);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cbDocumentos);
            this.Controls.Add(this.tbAnualidadid);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbPlazosdelcaso);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "agregaplazo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Agregar plazo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.agregaplazo_FormClosing);
            this.Load += new System.EventHandler(this.agregaplazo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbGrupo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbgrupo_plazo;
        private System.Windows.Forms.ComboBox cbEstadosplazos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbTipo_plazo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbPlazosdelcaso;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbAnualidadid;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lFechavencimiento;
        private System.Windows.Forms.TextBox tbFechadVencimineto;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbCasonumero;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbDocumentos;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbFechaatendioplazo;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbFechaNotificacion;
    }
}