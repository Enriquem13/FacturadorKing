namespace Facturador
{
    partial class BuscaInteresadoCaso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuscaInteresadoCaso));
            this.lbPadre = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_nombre_consultac = new System.Windows.Forms.TextBox();
            this.BT_buscarclientec = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CB_tipoderelacion = new System.Windows.Forms.ComboBox();
            this.BT_asociaracaso = new System.Windows.Forms.Button();
            this.textBox_selected = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbPadre
            // 
            this.lbPadre.AutoSize = true;
            this.lbPadre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPadre.Location = new System.Drawing.Point(175, 14);
            this.lbPadre.Name = "lbPadre";
            this.lbPadre.Size = new System.Drawing.Size(260, 16);
            this.lbPadre.TabIndex = 72;
            this.lbPadre.Text = "Agregar un nuevo solicitante o titular";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(9, 9);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(81, 29);
            this.button3.TabIndex = 73;
            this.button3.Text = "Salir";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(98, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 74;
            this.label3.Text = "Nombre:";
            // 
            // TB_nombre_consultac
            // 
            this.TB_nombre_consultac.Location = new System.Drawing.Point(158, 112);
            this.TB_nombre_consultac.Name = "TB_nombre_consultac";
            this.TB_nombre_consultac.Size = new System.Drawing.Size(329, 20);
            this.TB_nombre_consultac.TabIndex = 75;
            this.TB_nombre_consultac.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_nombre_consultac_KeyDown);
            // 
            // BT_buscarclientec
            // 
            this.BT_buscarclientec.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BT_buscarclientec.BackgroundImage")));
            this.BT_buscarclientec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BT_buscarclientec.Location = new System.Drawing.Point(491, 109);
            this.BT_buscarclientec.Name = "BT_buscarclientec";
            this.BT_buscarclientec.Size = new System.Drawing.Size(27, 26);
            this.BT_buscarclientec.TabIndex = 76;
            this.BT_buscarclientec.UseVisualStyleBackColor = true;
            this.BT_buscarclientec.Click += new System.EventHandler(this.BT_buscarclientec_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.HideSelection = false;
            this.listView1.HoverSelection = true;
            this.listView1.Location = new System.Drawing.Point(12, 204);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(593, 233);
            this.listView1.TabIndex = 77;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "N° Interesado";
            this.columnHeader1.Width = 79;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Nombre Solicitante o Titular";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 314;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(84, 179);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(434, 13);
            this.label10.TabIndex = 78;
            this.label10.Text = "Para agregar un nuevo interesado al caso,  debes seleccionarlo y elegir un tipo d" +
    "e relación";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(42, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 79;
            this.label1.Text = "Tipo de relacción:";
            // 
            // CB_tipoderelacion
            // 
            this.CB_tipoderelacion.FormattingEnabled = true;
            this.CB_tipoderelacion.Location = new System.Drawing.Point(158, 72);
            this.CB_tipoderelacion.Name = "CB_tipoderelacion";
            this.CB_tipoderelacion.Size = new System.Drawing.Size(329, 21);
            this.CB_tipoderelacion.TabIndex = 80;
            this.CB_tipoderelacion.SelectedIndexChanged += new System.EventHandler(this.CB_tipoderelacion_SelectedIndexChanged);
            // 
            // BT_asociaracaso
            // 
            this.BT_asociaracaso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BT_asociaracaso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_asociaracaso.Image = ((System.Drawing.Image)(resources.GetObject("BT_asociaracaso.Image")));
            this.BT_asociaracaso.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BT_asociaracaso.Location = new System.Drawing.Point(12, 442);
            this.BT_asociaracaso.Name = "BT_asociaracaso";
            this.BT_asociaracaso.Size = new System.Drawing.Size(91, 32);
            this.BT_asociaracaso.TabIndex = 81;
            this.BT_asociaracaso.Text = "Asociar";
            this.BT_asociaracaso.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BT_asociaracaso.UseVisualStyleBackColor = true;
            this.BT_asociaracaso.Click += new System.EventHandler(this.BT_asociaracaso_Click);
            // 
            // textBox_selected
            // 
            this.textBox_selected.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_selected.Location = new System.Drawing.Point(113, 447);
            this.textBox_selected.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_selected.Name = "textBox_selected";
            this.textBox_selected.ReadOnly = true;
            this.textBox_selected.Size = new System.Drawing.Size(492, 23);
            this.textBox_selected.TabIndex = 82;
            // 
            // BuscaInteresadoCaso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 484);
            this.ControlBox = false;
            this.Controls.Add(this.textBox_selected);
            this.Controls.Add(this.BT_asociaracaso);
            this.Controls.Add(this.CB_tipoderelacion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.BT_buscarclientec);
            this.Controls.Add(this.TB_nombre_consultac);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lbPadre);
            this.Name = "BuscaInteresadoCaso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar un Solicitante o Titular al Caso";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbPadre;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_nombre_consultac;
        private System.Windows.Forms.Button BT_buscarclientec;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CB_tipoderelacion;
        private System.Windows.Forms.Button BT_asociaracaso;
        private System.Windows.Forms.TextBox textBox_selected;
    }
}