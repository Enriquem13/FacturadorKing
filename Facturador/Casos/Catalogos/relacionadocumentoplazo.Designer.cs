namespace Facturador
{
    partial class relacionadocumentoplazo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(relacionadocumentoplazo));
            this.cbTipodocumento = new System.Windows.Forms.ComboBox();
            this.cbSubtipodocumento = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgTipoplazosgenra = new System.Windows.Forms.DataGridView();
            this.lbSubtipodocumento = new System.Windows.Forms.Label();
            this.gbGrupo = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbTipoplazo = new System.Windows.Forms.ComboBox();
            this.cbGrupoPlazo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_eliminarrelacionplazo = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Salir = new System.Windows.Forms.Button();
            this.btn_consultar = new System.Windows.Forms.Button();
            this.tbDocumento = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgTipoplazoid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTipoplazo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgTipoplazosgenra)).BeginInit();
            this.gbGrupo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbTipodocumento
            // 
            this.cbTipodocumento.FormattingEnabled = true;
            this.cbTipodocumento.Location = new System.Drawing.Point(21, 58);
            this.cbTipodocumento.Name = "cbTipodocumento";
            this.cbTipodocumento.Size = new System.Drawing.Size(309, 21);
            this.cbTipodocumento.TabIndex = 0;
            this.cbTipodocumento.SelectedIndexChanged += new System.EventHandler(this.cbTipodocumento_SelectedIndexChanged);
            // 
            // cbSubtipodocumento
            // 
            this.cbSubtipodocumento.FormattingEnabled = true;
            this.cbSubtipodocumento.Location = new System.Drawing.Point(21, 122);
            this.cbSubtipodocumento.Name = "cbSubtipodocumento";
            this.cbSubtipodocumento.Size = new System.Drawing.Size(309, 21);
            this.cbSubtipodocumento.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tipo Documento";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tipo Documento";
            // 
            // dgTipoplazosgenra
            // 
            this.dgTipoplazosgenra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTipoplazosgenra.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgTipoplazoid,
            this.dgTipoplazo});
            this.dgTipoplazosgenra.Location = new System.Drawing.Point(59, 359);
            this.dgTipoplazosgenra.Name = "dgTipoplazosgenra";
            this.dgTipoplazosgenra.Size = new System.Drawing.Size(557, 173);
            this.dgTipoplazosgenra.TabIndex = 4;
            // 
            // lbSubtipodocumento
            // 
            this.lbSubtipodocumento.AutoSize = true;
            this.lbSubtipodocumento.Location = new System.Drawing.Point(56, 336);
            this.lbSubtipodocumento.Name = "lbSubtipodocumento";
            this.lbSubtipodocumento.Size = new System.Drawing.Size(75, 13);
            this.lbSubtipodocumento.TabIndex = 5;
            this.lbSubtipodocumento.Text = "El documento:";
            // 
            // gbGrupo
            // 
            this.gbGrupo.Controls.Add(this.btn_consultar);
            this.gbGrupo.Controls.Add(this.cbSubtipodocumento);
            this.gbGrupo.Controls.Add(this.cbTipodocumento);
            this.gbGrupo.Controls.Add(this.label1);
            this.gbGrupo.Controls.Add(this.label2);
            this.gbGrupo.Location = new System.Drawing.Point(56, 63);
            this.gbGrupo.Name = "gbGrupo";
            this.gbGrupo.Size = new System.Drawing.Size(560, 247);
            this.gbGrupo.TabIndex = 6;
            this.gbGrupo.TabStop = false;
            this.gbGrupo.Text = "Documento";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cbTipoplazo);
            this.groupBox1.Controls.Add(this.cbGrupoPlazo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(785, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(391, 247);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Plazo a Generar";
            // 
            // cbTipoplazo
            // 
            this.cbTipoplazo.FormattingEnabled = true;
            this.cbTipoplazo.Location = new System.Drawing.Point(41, 119);
            this.cbTipoplazo.Name = "cbTipoplazo";
            this.cbTipoplazo.Size = new System.Drawing.Size(309, 21);
            this.cbTipoplazo.TabIndex = 5;
            // 
            // cbGrupoPlazo
            // 
            this.cbGrupoPlazo.FormattingEnabled = true;
            this.cbGrupoPlazo.Location = new System.Drawing.Point(41, 55);
            this.cbGrupoPlazo.Name = "cbGrupoPlazo";
            this.cbGrupoPlazo.Size = new System.Drawing.Size(309, 21);
            this.cbGrupoPlazo.TabIndex = 4;
            this.cbGrupoPlazo.SelectedIndexChanged += new System.EventHandler(this.cbGrupoPlazo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Grupo Plazo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Tipo Plazo";
            // 
            // btn_eliminarrelacionplazo
            // 
            this.btn_eliminarrelacionplazo.Location = new System.Drawing.Point(633, 359);
            this.btn_eliminarrelacionplazo.Name = "btn_eliminarrelacionplazo";
            this.btn_eliminarrelacionplazo.Size = new System.Drawing.Size(120, 32);
            this.btn_eliminarrelacionplazo.TabIndex = 8;
            this.btn_eliminarrelacionplazo.Text = "Eliminar tipo plazo";
            this.btn_eliminarrelacionplazo.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(44, 202);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Agregar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Salir
            // 
            this.btn_Salir.Location = new System.Drawing.Point(1158, 12);
            this.btn_Salir.Name = "btn_Salir";
            this.btn_Salir.Size = new System.Drawing.Size(75, 23);
            this.btn_Salir.TabIndex = 9;
            this.btn_Salir.Text = "Salir";
            this.btn_Salir.UseVisualStyleBackColor = true;
            this.btn_Salir.Click += new System.EventHandler(this.btn_Salir_Click);
            // 
            // btn_consultar
            // 
            this.btn_consultar.Location = new System.Drawing.Point(463, 202);
            this.btn_consultar.Name = "btn_consultar";
            this.btn_consultar.Size = new System.Drawing.Size(75, 23);
            this.btn_consultar.TabIndex = 4;
            this.btn_consultar.Text = "Consultar";
            this.btn_consultar.UseVisualStyleBackColor = true;
            this.btn_consultar.Click += new System.EventHandler(this.btn_consultar_Click);
            // 
            // tbDocumento
            // 
            this.tbDocumento.Location = new System.Drawing.Point(137, 333);
            this.tbDocumento.Name = "tbDocumento";
            this.tbDocumento.ReadOnly = true;
            this.tbDocumento.Size = new System.Drawing.Size(329, 20);
            this.tbDocumento.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(472, 336);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(144, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Genera los siguientes plazos:";
            // 
            // dgTipoplazoid
            // 
            this.dgTipoplazoid.HeaderText = "Tipoplazoid";
            this.dgTipoplazoid.Name = "dgTipoplazoid";
            this.dgTipoplazoid.Visible = false;
            // 
            // dgTipoplazo
            // 
            this.dgTipoplazo.HeaderText = "Tipo plazo";
            this.dgTipoplazo.Name = "dgTipoplazo";
            this.dgTipoplazo.Width = 200;
            // 
            // relacionadocumentoplazo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 583);
            this.ControlBox = false;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbDocumento);
            this.Controls.Add(this.btn_Salir);
            this.Controls.Add(this.btn_eliminarrelacionplazo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbGrupo);
            this.Controls.Add(this.lbSubtipodocumento);
            this.Controls.Add(this.dgTipoplazosgenra);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "relacionadocumentoplazo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Relaciona Documento Con Plazo";
            ((System.ComponentModel.ISupportInitialize)(this.dgTipoplazosgenra)).EndInit();
            this.gbGrupo.ResumeLayout(false);
            this.gbGrupo.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbTipodocumento;
        private System.Windows.Forms.ComboBox cbSubtipodocumento;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgTipoplazosgenra;
        private System.Windows.Forms.Label lbSubtipodocumento;
        private System.Windows.Forms.GroupBox gbGrupo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbTipoplazo;
        private System.Windows.Forms.ComboBox cbGrupoPlazo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_eliminarrelacionplazo;
        private System.Windows.Forms.Button btn_Salir;
        private System.Windows.Forms.Button btn_consultar;
        private System.Windows.Forms.TextBox tbDocumento;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTipoplazoid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTipoplazo;
    }
}