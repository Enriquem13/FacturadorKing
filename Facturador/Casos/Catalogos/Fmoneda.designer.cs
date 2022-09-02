namespace Facturador
{
    partial class Fmoneda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fmoneda));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_nombremoneda = new System.Windows.Forms.TextBox();
            this.Bagregar = new System.Windows.Forms.Button();
            this.Bmodificar = new System.Windows.Forms.Button();
            this.Beliminar = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnnombremoneda = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnsimblo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnprefijo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TB_simbolomoneda = new System.Windows.Forms.TextBox();
            this.TB_descripcionmoneda = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BT_menumoneda = new System.Windows.Forms.Button();
            this.BT_salirmoneda = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(157, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Moneda";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Moneda";
            // 
            // TB_nombremoneda
            // 
            this.TB_nombremoneda.Location = new System.Drawing.Point(94, 80);
            this.TB_nombremoneda.Name = "TB_nombremoneda";
            this.TB_nombremoneda.Size = new System.Drawing.Size(262, 20);
            this.TB_nombremoneda.TabIndex = 1;
            // 
            // Bagregar
            // 
            this.Bagregar.Location = new System.Drawing.Point(12, 232);
            this.Bagregar.Name = "Bagregar";
            this.Bagregar.Size = new System.Drawing.Size(100, 23);
            this.Bagregar.TabIndex = 4;
            this.Bagregar.Text = "Agregar";
            this.Bagregar.UseVisualStyleBackColor = true;
            this.Bagregar.Click += new System.EventHandler(this.Bagregar_Click);
            // 
            // Bmodificar
            // 
            this.Bmodificar.Location = new System.Drawing.Point(142, 232);
            this.Bmodificar.Name = "Bmodificar";
            this.Bmodificar.Size = new System.Drawing.Size(100, 23);
            this.Bmodificar.TabIndex = 5;
            this.Bmodificar.Text = "Modificar";
            this.Bmodificar.UseVisualStyleBackColor = true;
            // 
            // Beliminar
            // 
            this.Beliminar.Location = new System.Drawing.Point(270, 232);
            this.Beliminar.Name = "Beliminar";
            this.Beliminar.Size = new System.Drawing.Size(100, 23);
            this.Beliminar.TabIndex = 6;
            this.Beliminar.Text = "Eliminar";
            this.Beliminar.UseVisualStyleBackColor = true;
            this.Beliminar.Click += new System.EventHandler(this.Beliminar_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnnombremoneda,
            this.columnsimblo,
            this.columnprefijo});
            this.listView1.Location = new System.Drawing.Point(12, 293);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(358, 132);
            this.listView1.TabIndex = 39;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnnombremoneda
            // 
            this.columnnombremoneda.Text = "Moneda";
            this.columnnombremoneda.Width = 141;
            // 
            // columnsimblo
            // 
            this.columnsimblo.Text = "Simbolo";
            this.columnsimblo.Width = 108;
            // 
            // columnprefijo
            // 
            this.columnprefijo.Text = "Prefijo";
            this.columnprefijo.Width = 104;
            // 
            // TB_simbolomoneda
            // 
            this.TB_simbolomoneda.Location = new System.Drawing.Point(94, 127);
            this.TB_simbolomoneda.Name = "TB_simbolomoneda";
            this.TB_simbolomoneda.Size = new System.Drawing.Size(262, 20);
            this.TB_simbolomoneda.TabIndex = 2;
            // 
            // TB_descripcionmoneda
            // 
            this.TB_descripcionmoneda.Location = new System.Drawing.Point(94, 172);
            this.TB_descripcionmoneda.Name = "TB_descripcionmoneda";
            this.TB_descripcionmoneda.ShortcutsEnabled = false;
            this.TB_descripcionmoneda.Size = new System.Drawing.Size(262, 20);
            this.TB_descripcionmoneda.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(35, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 42;
            this.label3.Text = "Símbolo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(42, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 43;
            this.label4.Text = "Subfijo";
            // 
            // BT_menumoneda
            // 
            this.BT_menumoneda.Location = new System.Drawing.Point(12, 12);
            this.BT_menumoneda.Name = "BT_menumoneda";
            this.BT_menumoneda.Size = new System.Drawing.Size(75, 23);
            this.BT_menumoneda.TabIndex = 7;
            this.BT_menumoneda.Text = "Menú";
            this.BT_menumoneda.UseVisualStyleBackColor = true;
            this.BT_menumoneda.Click += new System.EventHandler(this.BT_menumoneda_Click);
            // 
            // BT_salirmoneda
            // 
            this.BT_salirmoneda.Location = new System.Drawing.Point(295, 12);
            this.BT_salirmoneda.Name = "BT_salirmoneda";
            this.BT_salirmoneda.Size = new System.Drawing.Size(75, 23);
            this.BT_salirmoneda.TabIndex = 7;
            this.BT_salirmoneda.Text = "Salir";
            this.BT_salirmoneda.UseVisualStyleBackColor = true;
            this.BT_salirmoneda.Click += new System.EventHandler(this.BT_salirmoneda_Click);
            // 
            // Fmoneda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 437);
            this.ControlBox = false;
            this.Controls.Add(this.BT_salirmoneda);
            this.Controls.Add(this.BT_menumoneda);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TB_descripcionmoneda);
            this.Controls.Add(this.TB_simbolomoneda);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.Beliminar);
            this.Controls.Add(this.Bmodificar);
            this.Controls.Add(this.Bagregar);
            this.Controls.Add(this.TB_nombremoneda);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Fmoneda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Moneda";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_nombremoneda;
        private System.Windows.Forms.Button Bagregar;
        private System.Windows.Forms.Button Bmodificar;
        private System.Windows.Forms.Button Beliminar;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox TB_simbolomoneda;
        private System.Windows.Forms.TextBox TB_descripcionmoneda;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BT_menumoneda;
        private System.Windows.Forms.Button BT_salirmoneda;
        private System.Windows.Forms.ColumnHeader columnnombremoneda;
        private System.Windows.Forms.ColumnHeader columnsimblo;
        private System.Windows.Forms.ColumnHeader columnprefijo;
    }
}