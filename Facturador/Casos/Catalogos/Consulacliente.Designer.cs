namespace Facturador
{
    partial class Consulacliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Consulacliente));
            this.BT_menuconsultalciente = new System.Windows.Forms.Button();
            this.BT_salirconsultacliente = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_nombre_consultac = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_contactocc_cd = new System.Windows.Forms.TextBox();
            this.BT_buscarclientec = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnclientecc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnNombrecc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDireccion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnContactocc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCasos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BT_nuevocliente = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BT_menuconsultalciente
            // 
            this.BT_menuconsultalciente.Location = new System.Drawing.Point(12, 12);
            this.BT_menuconsultalciente.Name = "BT_menuconsultalciente";
            this.BT_menuconsultalciente.Size = new System.Drawing.Size(75, 23);
            this.BT_menuconsultalciente.TabIndex = 0;
            this.BT_menuconsultalciente.Text = "Menú";
            this.BT_menuconsultalciente.UseVisualStyleBackColor = true;
            this.BT_menuconsultalciente.Click += new System.EventHandler(this.BT_menuconsultalciente_Click);
            // 
            // BT_salirconsultacliente
            // 
            this.BT_salirconsultacliente.Location = new System.Drawing.Point(905, 12);
            this.BT_salirconsultacliente.Name = "BT_salirconsultacliente";
            this.BT_salirconsultacliente.Size = new System.Drawing.Size(75, 23);
            this.BT_salirconsultacliente.TabIndex = 1;
            this.BT_salirconsultacliente.Text = "Regresar";
            this.BT_salirconsultacliente.UseVisualStyleBackColor = true;
            this.BT_salirconsultacliente.Click += new System.EventHandler(this.BT_salirconsultacliente_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(413, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Consulta Cliente";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(37, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Cliente";
            // 
            // TB_nombre_consultac
            // 
            this.TB_nombre_consultac.Location = new System.Drawing.Point(93, 67);
            this.TB_nombre_consultac.Name = "TB_nombre_consultac";
            this.TB_nombre_consultac.Size = new System.Drawing.Size(329, 20);
            this.TB_nombre_consultac.TabIndex = 7;
            this.TB_nombre_consultac.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_nombre_consultac_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Contacto";
            // 
            // TB_contactocc_cd
            // 
            this.TB_contactocc_cd.Location = new System.Drawing.Point(93, 128);
            this.TB_contactocc_cd.Name = "TB_contactocc_cd";
            this.TB_contactocc_cd.Size = new System.Drawing.Size(329, 20);
            this.TB_contactocc_cd.TabIndex = 9;
            this.TB_contactocc_cd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_contactocc_cd_KeyDown);
            // 
            // BT_buscarclientec
            // 
            this.BT_buscarclientec.Location = new System.Drawing.Point(492, 101);
            this.BT_buscarclientec.Name = "BT_buscarclientec";
            this.BT_buscarclientec.Size = new System.Drawing.Size(137, 23);
            this.BT_buscarclientec.TabIndex = 10;
            this.BT_buscarclientec.Text = "Buscar";
            this.BT_buscarclientec.UseVisualStyleBackColor = true;
            this.BT_buscarclientec.Click += new System.EventHandler(this.BT_buscarclientec_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnclientecc,
            this.columnNombrecc,
            this.columnDireccion,
            this.columnContactocc,
            this.columnCasos});
            this.listView1.HideSelection = false;
            this.listView1.HoverSelection = true;
            this.listView1.Location = new System.Drawing.Point(22, 195);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(948, 260);
            this.listView1.TabIndex = 11;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // columnclientecc
            // 
            this.columnclientecc.Text = "Cliente";
            this.columnclientecc.Width = 73;
            // 
            // columnNombrecc
            // 
            this.columnNombrecc.Text = "Nombre";
            this.columnNombrecc.Width = 258;
            // 
            // columnDireccion
            // 
            this.columnDireccion.Text = "Dirección";
            this.columnDireccion.Width = 375;
            // 
            // columnContactocc
            // 
            this.columnContactocc.Text = "Contacto";
            this.columnContactocc.Width = 160;
            // 
            // columnCasos
            // 
            this.columnCasos.Text = "Casos";
            this.columnCasos.Width = 75;
            // 
            // BT_nuevocliente
            // 
            this.BT_nuevocliente.Location = new System.Drawing.Point(726, 101);
            this.BT_nuevocliente.Name = "BT_nuevocliente";
            this.BT_nuevocliente.Size = new System.Drawing.Size(137, 23);
            this.BT_nuevocliente.TabIndex = 12;
            this.BT_nuevocliente.Text = "Nuevo Cliente";
            this.BT_nuevocliente.UseVisualStyleBackColor = true;
            this.BT_nuevocliente.Click += new System.EventHandler(this.BT_nuevocliente_Click);
            // 
            // Consulacliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 498);
            this.ControlBox = false;
            this.Controls.Add(this.BT_nuevocliente);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.BT_buscarclientec);
            this.Controls.Add(this.TB_contactocc_cd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TB_nombre_consultac);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BT_salirconsultacliente);
            this.Controls.Add(this.BT_menuconsultalciente);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Consulacliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta Cliente";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BT_menuconsultalciente;
        private System.Windows.Forms.Button BT_salirconsultacliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_nombre_consultac;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_contactocc_cd;
        private System.Windows.Forms.Button BT_buscarclientec;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnclientecc;
        private System.Windows.Forms.ColumnHeader columnNombrecc;
        private System.Windows.Forms.ColumnHeader columnDireccion;
        private System.Windows.Forms.ColumnHeader columnContactocc;
        private System.Windows.Forms.ColumnHeader columnCasos;
        private System.Windows.Forms.Button BT_nuevocliente;
    }
}