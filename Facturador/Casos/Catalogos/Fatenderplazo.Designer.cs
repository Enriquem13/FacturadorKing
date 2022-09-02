namespace Facturador
{
    partial class Fatenderplazo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fatenderplazo));
            this.cbTipocorreo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFechacorreo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbNombredelarchivo = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.rtDescripcion = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbPlazoatender = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbFechaprorroga = new System.Windows.Forms.TextBox();
            this.rbProrrogado = new System.Windows.Forms.RadioButton();
            this.rbCancelado = new System.Windows.Forms.RadioButton();
            this.rbAtendido = new System.Windows.Forms.RadioButton();
            this.rbSerecibieroninstrucciones = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.rtMotivocancelacion = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnsalir = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cbCorreotipo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbTipocorreo
            // 
            this.cbTipocorreo.FormattingEnabled = true;
            this.cbTipocorreo.Location = new System.Drawing.Point(537, 25);
            this.cbTipocorreo.Name = "cbTipocorreo";
            this.cbTipocorreo.Size = new System.Drawing.Size(468, 21);
            this.cbTipocorreo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(449, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tipo Correo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fecha del correo";
            // 
            // tbFechacorreo
            // 
            this.tbFechacorreo.Location = new System.Drawing.Point(157, 73);
            this.tbFechacorreo.Name = "tbFechacorreo";
            this.tbFechacorreo.Size = new System.Drawing.Size(85, 20);
            this.tbFechacorreo.TabIndex = 3;
            this.tbFechacorreo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFechacorreo_KeyPress);
            this.tbFechacorreo.Validating += new System.ComponentModel.CancelEventHandler(this.tbFechacorreo_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nombre del archivo";
            // 
            // tbNombredelarchivo
            // 
            this.tbNombredelarchivo.Location = new System.Drawing.Point(157, 120);
            this.tbNombredelarchivo.Name = "tbNombredelarchivo";
            this.tbNombredelarchivo.ReadOnly = true;
            this.tbNombredelarchivo.Size = new System.Drawing.Size(681, 20);
            this.tbNombredelarchivo.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(873, 118);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Seleccionar correo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Descripción";
            // 
            // rtDescripcion
            // 
            this.rtDescripcion.Location = new System.Drawing.Point(157, 168);
            this.rtDescripcion.Name = "rtDescripcion";
            this.rtDescripcion.Size = new System.Drawing.Size(848, 153);
            this.rtDescripcion.TabIndex = 8;
            this.rtDescripcion.Text = "";
            this.rtDescripcion.TextChanged += new System.EventHandler(this.rtDescripcion_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 380);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Plazo a atender";
            // 
            // cbPlazoatender
            // 
            this.cbPlazoatender.FormattingEnabled = true;
            this.cbPlazoatender.Location = new System.Drawing.Point(121, 377);
            this.cbPlazoatender.Name = "cbPlazoatender";
            this.cbPlazoatender.Size = new System.Drawing.Size(669, 21);
            this.cbPlazoatender.TabIndex = 10;
            this.cbPlazoatender.SelectedIndexChanged += new System.EventHandler(this.cbPlazoatender_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbFechaprorroga);
            this.groupBox1.Controls.Add(this.rbProrrogado);
            this.groupBox1.Controls.Add(this.rbCancelado);
            this.groupBox1.Controls.Add(this.rbAtendido);
            this.groupBox1.Controls.Add(this.rbSerecibieroninstrucciones);
            this.groupBox1.Location = new System.Drawing.Point(121, 414);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(669, 141);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de atención";
            // 
            // tbFechaprorroga
            // 
            this.tbFechaprorroga.Enabled = false;
            this.tbFechaprorroga.Location = new System.Drawing.Point(482, 96);
            this.tbFechaprorroga.Name = "tbFechaprorroga";
            this.tbFechaprorroga.ReadOnly = true;
            this.tbFechaprorroga.Size = new System.Drawing.Size(162, 20);
            this.tbFechaprorroga.TabIndex = 4;
            // 
            // rbProrrogado
            // 
            this.rbProrrogado.AutoSize = true;
            this.rbProrrogado.Enabled = false;
            this.rbProrrogado.Location = new System.Drawing.Point(373, 96);
            this.rbProrrogado.Name = "rbProrrogado";
            this.rbProrrogado.Size = new System.Drawing.Size(77, 17);
            this.rbProrrogado.TabIndex = 3;
            this.rbProrrogado.TabStop = true;
            this.rbProrrogado.Text = "Prorrogado";
            this.rbProrrogado.UseVisualStyleBackColor = true;
            this.rbProrrogado.CheckedChanged += new System.EventHandler(this.rbProrrogado_CheckedChanged);
            // 
            // rbCancelado
            // 
            this.rbCancelado.AutoSize = true;
            this.rbCancelado.Location = new System.Drawing.Point(45, 96);
            this.rbCancelado.Name = "rbCancelado";
            this.rbCancelado.Size = new System.Drawing.Size(76, 17);
            this.rbCancelado.TabIndex = 2;
            this.rbCancelado.TabStop = true;
            this.rbCancelado.Text = "Cancelado";
            this.rbCancelado.UseVisualStyleBackColor = true;
            // 
            // rbAtendido
            // 
            this.rbAtendido.AutoSize = true;
            this.rbAtendido.Location = new System.Drawing.Point(373, 33);
            this.rbAtendido.Name = "rbAtendido";
            this.rbAtendido.Size = new System.Drawing.Size(67, 17);
            this.rbAtendido.TabIndex = 1;
            this.rbAtendido.TabStop = true;
            this.rbAtendido.Text = "Atendido";
            this.rbAtendido.UseVisualStyleBackColor = true;
            // 
            // rbSerecibieroninstrucciones
            // 
            this.rbSerecibieroninstrucciones.AutoSize = true;
            this.rbSerecibieroninstrucciones.Location = new System.Drawing.Point(45, 33);
            this.rbSerecibieroninstrucciones.Name = "rbSerecibieroninstrucciones";
            this.rbSerecibieroninstrucciones.Size = new System.Drawing.Size(152, 17);
            this.rbSerecibieroninstrucciones.TabIndex = 0;
            this.rbSerecibieroninstrucciones.TabStop = true;
            this.rbSerecibieroninstrucciones.Text = "Se recibieron instrucciones";
            this.rbSerecibieroninstrucciones.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(815, 414);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Motivo Cancelación";
            // 
            // rtMotivocancelacion
            // 
            this.rtMotivocancelacion.Location = new System.Drawing.Point(818, 451);
            this.rtMotivocancelacion.Name = "rtMotivocancelacion";
            this.rtMotivocancelacion.Size = new System.Drawing.Size(504, 104);
            this.rtMotivocancelacion.TabIndex = 13;
            this.rtMotivocancelacion.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1247, 587);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Guardar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnsalir
            // 
            this.btnsalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnsalir.Location = new System.Drawing.Point(1247, 12);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Size = new System.Drawing.Size(75, 23);
            this.btnsalir.TabIndex = 15;
            this.btnsalir.Text = "Salir";
            this.btnsalir.UseVisualStyleBackColor = true;
            this.btnsalir.Click += new System.EventHandler(this.button3_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(164, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 12);
            this.label7.TabIndex = 100;
            this.label7.Text = "día / mes  /Año";
            // 
            // cbCorreotipo
            // 
            this.cbCorreotipo.FormattingEnabled = true;
            this.cbCorreotipo.Location = new System.Drawing.Point(157, 24);
            this.cbCorreotipo.Name = "cbCorreotipo";
            this.cbCorreotipo.Size = new System.Drawing.Size(243, 21);
            this.cbCorreotipo.TabIndex = 101;
            this.cbCorreotipo.SelectedIndexChanged += new System.EventHandler(this.cbCorreotipo_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(65, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 102;
            this.label8.Text = "Quién envía";
            // 
            // Fatenderplazo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnsalir;
            this.ClientSize = new System.Drawing.Size(1334, 622);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbCorreotipo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnsalir);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.rtMotivocancelacion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbPlazoatender);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rtDescripcion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbNombredelarchivo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbFechacorreo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbTipocorreo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Fatenderplazo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Subir Correo";
            this.Load += new System.EventHandler(this.Fatenderplazo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbTipocorreo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFechacorreo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbNombredelarchivo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox rtDescripcion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbPlazoatender;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox rtMotivocancelacion;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbFechaprorroga;
        private System.Windows.Forms.RadioButton rbProrrogado;
        private System.Windows.Forms.RadioButton rbCancelado;
        private System.Windows.Forms.RadioButton rbAtendido;
        private System.Windows.Forms.RadioButton rbSerecibieroninstrucciones;
        private System.Windows.Forms.Button btnsalir;
        public System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbCorreotipo;
        private System.Windows.Forms.Label label8;
    }
}