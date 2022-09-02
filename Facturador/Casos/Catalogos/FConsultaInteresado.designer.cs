namespace Facturador
{
    partial class FConsultaInteresado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FConsultaInteresado));
            this.BT_menuci = new System.Windows.Forms.Button();
            this.BT_salirci = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_nombre_consultac = new System.Windows.Forms.TextBox();
            this.BT_buscarclientec = new System.Windows.Forms.Button();
            this.BT_nuevocliente = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // BT_menuci
            // 
            this.BT_menuci.Location = new System.Drawing.Point(12, 12);
            this.BT_menuci.Name = "BT_menuci";
            this.BT_menuci.Size = new System.Drawing.Size(75, 23);
            this.BT_menuci.TabIndex = 1;
            this.BT_menuci.Text = "Menú";
            this.BT_menuci.UseVisualStyleBackColor = true;
            this.BT_menuci.Click += new System.EventHandler(this.BT_menuconsultalciente_Click);
            // 
            // BT_salirci
            // 
            this.BT_salirci.Location = new System.Drawing.Point(793, 12);
            this.BT_salirci.Name = "BT_salirci";
            this.BT_salirci.Size = new System.Drawing.Size(75, 23);
            this.BT_salirci.TabIndex = 2;
            this.BT_salirci.Text = "Regresar";
            this.BT_salirci.UseVisualStyleBackColor = true;
            this.BT_salirci.Click += new System.EventHandler(this.BT_salirconsultacliente_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(339, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Consulta Interesado";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(48, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Interesado";
            // 
            // TB_nombre_consultac
            // 
            this.TB_nombre_consultac.Location = new System.Drawing.Point(131, 90);
            this.TB_nombre_consultac.Name = "TB_nombre_consultac";
            this.TB_nombre_consultac.Size = new System.Drawing.Size(329, 20);
            this.TB_nombre_consultac.TabIndex = 8;
            this.TB_nombre_consultac.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_nombre_consultac_KeyDown);
            // 
            // BT_buscarclientec
            // 
            this.BT_buscarclientec.Location = new System.Drawing.Point(490, 88);
            this.BT_buscarclientec.Name = "BT_buscarclientec";
            this.BT_buscarclientec.Size = new System.Drawing.Size(137, 23);
            this.BT_buscarclientec.TabIndex = 11;
            this.BT_buscarclientec.Text = "Buscar";
            this.BT_buscarclientec.UseVisualStyleBackColor = true;
            this.BT_buscarclientec.Click += new System.EventHandler(this.BT_buscarclientec_Click);
            // 
            // BT_nuevocliente
            // 
            this.BT_nuevocliente.Location = new System.Drawing.Point(686, 88);
            this.BT_nuevocliente.Name = "BT_nuevocliente";
            this.BT_nuevocliente.Size = new System.Drawing.Size(137, 23);
            this.BT_nuevocliente.TabIndex = 13;
            this.BT_nuevocliente.Text = "Nuevo Interesado";
            this.BT_nuevocliente.UseVisualStyleBackColor = true;
            this.BT_nuevocliente.Click += new System.EventHandler(this.BT_nuevocliente_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader5});
            this.listView1.HideSelection = false;
            this.listView1.HoverSelection = true;
            this.listView1.Location = new System.Drawing.Point(12, 173);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(856, 143);
            this.listView1.TabIndex = 14;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "N° Interesado";
            this.columnHeader1.Width = 98;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Interesado";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 561;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Nombre Corto";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 189;
            // 
            // FConsultaInteresado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 361);
            this.ControlBox = false;
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.BT_nuevocliente);
            this.Controls.Add(this.BT_buscarclientec);
            this.Controls.Add(this.TB_nombre_consultac);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BT_salirci);
            this.Controls.Add(this.BT_menuci);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FConsultaInteresado";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta Interesado";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BT_menuci;
        private System.Windows.Forms.Button BT_salirci;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_nombre_consultac;
        private System.Windows.Forms.Button BT_buscarclientec;
        private System.Windows.Forms.Button BT_nuevocliente;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}