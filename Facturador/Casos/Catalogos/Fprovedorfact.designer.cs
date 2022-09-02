namespace Facturador
{
    partial class Fprovedorfact
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fprovedorfact));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_nombre_proovedorfac = new System.Windows.Forms.TextBox();
            this.Bagregar = new System.Windows.Forms.Button();
            this.Bmodificar = new System.Windows.Forms.Button();
            this.Beliminar = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnproveedorfac = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnproveedorsitio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnproveedorobservaciones = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TB_sitio_provedorfac = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_observaciones_provfac = new System.Windows.Forms.TextBox();
            this.BT_menuproveedor = new System.Windows.Forms.Button();
            this.BT_salirproveedor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(68, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Proveedor de factura electronica";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Proveedor";
            // 
            // TB_nombre_proovedorfac
            // 
            this.TB_nombre_proovedorfac.Location = new System.Drawing.Point(106, 100);
            this.TB_nombre_proovedorfac.Name = "TB_nombre_proovedorfac";
            this.TB_nombre_proovedorfac.Size = new System.Drawing.Size(250, 20);
            this.TB_nombre_proovedorfac.TabIndex = 1;
            // 
            // Bagregar
            // 
            this.Bagregar.Location = new System.Drawing.Point(12, 224);
            this.Bagregar.Name = "Bagregar";
            this.Bagregar.Size = new System.Drawing.Size(100, 23);
            this.Bagregar.TabIndex = 36;
            this.Bagregar.Text = "Agregar";
            this.Bagregar.UseVisualStyleBackColor = true;
            this.Bagregar.Click += new System.EventHandler(this.Bagregar_Click);
            // 
            // Bmodificar
            // 
            this.Bmodificar.Location = new System.Drawing.Point(144, 224);
            this.Bmodificar.Name = "Bmodificar";
            this.Bmodificar.Size = new System.Drawing.Size(100, 23);
            this.Bmodificar.TabIndex = 37;
            this.Bmodificar.Text = "Modificar";
            this.Bmodificar.UseVisualStyleBackColor = true;
            // 
            // Beliminar
            // 
            this.Beliminar.Location = new System.Drawing.Point(269, 224);
            this.Beliminar.Name = "Beliminar";
            this.Beliminar.Size = new System.Drawing.Size(100, 23);
            this.Beliminar.TabIndex = 38;
            this.Beliminar.Text = "Eliminar";
            this.Beliminar.UseVisualStyleBackColor = true;
            this.Beliminar.Click += new System.EventHandler(this.Beliminar_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnproveedorfac,
            this.columnproveedorsitio,
            this.columnproveedorobservaciones});
            this.listView1.Location = new System.Drawing.Point(12, 287);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(357, 132);
            this.listView1.TabIndex = 39;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnproveedorfac
            // 
            this.columnproveedorfac.Text = "Proovedor";
            this.columnproveedorfac.Width = 87;
            // 
            // columnproveedorsitio
            // 
            this.columnproveedorsitio.Text = "Sitio Web";
            this.columnproveedorsitio.Width = 136;
            // 
            // columnproveedorobservaciones
            // 
            this.columnproveedorobservaciones.Text = "Observaciones";
            this.columnproveedorobservaciones.Width = 129;
            // 
            // TB_sitio_provedorfac
            // 
            this.TB_sitio_provedorfac.Location = new System.Drawing.Point(106, 140);
            this.TB_sitio_provedorfac.Name = "TB_sitio_provedorfac";
            this.TB_sitio_provedorfac.Size = new System.Drawing.Size(250, 20);
            this.TB_sitio_provedorfac.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(38, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Sitio Web";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 42;
            this.label4.Text = "Observaciones";
            // 
            // TB_observaciones_provfac
            // 
            this.TB_observaciones_provfac.Location = new System.Drawing.Point(106, 180);
            this.TB_observaciones_provfac.Name = "TB_observaciones_provfac";
            this.TB_observaciones_provfac.Size = new System.Drawing.Size(250, 20);
            this.TB_observaciones_provfac.TabIndex = 3;
            // 
            // BT_menuproveedor
            // 
            this.BT_menuproveedor.Location = new System.Drawing.Point(12, 12);
            this.BT_menuproveedor.Name = "BT_menuproveedor";
            this.BT_menuproveedor.Size = new System.Drawing.Size(75, 23);
            this.BT_menuproveedor.TabIndex = 43;
            this.BT_menuproveedor.Text = "Menú";
            this.BT_menuproveedor.UseVisualStyleBackColor = true;
            this.BT_menuproveedor.Click += new System.EventHandler(this.BT_menuproveedor_Click);
            // 
            // BT_salirproveedor
            // 
            this.BT_salirproveedor.Location = new System.Drawing.Point(294, 12);
            this.BT_salirproveedor.Name = "BT_salirproveedor";
            this.BT_salirproveedor.Size = new System.Drawing.Size(75, 23);
            this.BT_salirproveedor.TabIndex = 44;
            this.BT_salirproveedor.Text = "Salir";
            this.BT_salirproveedor.UseVisualStyleBackColor = true;
            this.BT_salirproveedor.Click += new System.EventHandler(this.BT_salirproveedor_Click);
            // 
            // Fprovedorfact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 440);
            this.ControlBox = false;
            this.Controls.Add(this.BT_salirproveedor);
            this.Controls.Add(this.BT_menuproveedor);
            this.Controls.Add(this.TB_observaciones_provfac);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TB_sitio_provedorfac);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.Beliminar);
            this.Controls.Add(this.Bmodificar);
            this.Controls.Add(this.Bagregar);
            this.Controls.Add(this.TB_nombre_proovedorfac);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Fprovedorfact";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proovedor factura electronica";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_nombre_proovedorfac;
        private System.Windows.Forms.Button Bagregar;
        private System.Windows.Forms.Button Bmodificar;
        private System.Windows.Forms.Button Beliminar;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox TB_sitio_provedorfac;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TB_observaciones_provfac;
        private System.Windows.Forms.ColumnHeader columnproveedorfac;
        private System.Windows.Forms.ColumnHeader columnproveedorsitio;
        private System.Windows.Forms.ColumnHeader columnproveedorobservaciones;
        private System.Windows.Forms.Button BT_menuproveedor;
        private System.Windows.Forms.Button BT_salirproveedor;
    }
}