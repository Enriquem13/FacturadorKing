namespace Facturador
{
    partial class Ftipoenvioinf
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ftipoenvioinf));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_nombre_tipoenvio = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_observaciones_tipoenvio = new System.Windows.Forms.TextBox();
            this.Bagregar = new System.Windows.Forms.Button();
            this.Bmodificar = new System.Windows.Forms.Button();
            this.Beliminar = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnnombreenvios = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columndescripcionenvio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BT_menuenvio = new System.Windows.Forms.Button();
            this.BT_salir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(77, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tipo de envio de facturas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tipo de envío";
            // 
            // TB_nombre_tipoenvio
            // 
            this.TB_nombre_tipoenvio.Location = new System.Drawing.Point(106, 92);
            this.TB_nombre_tipoenvio.Name = "TB_nombre_tipoenvio";
            this.TB_nombre_tipoenvio.Size = new System.Drawing.Size(262, 20);
            this.TB_nombre_tipoenvio.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 43;
            this.label4.Text = "Observaciones";
            // 
            // TB_observaciones_tipoenvio
            // 
            this.TB_observaciones_tipoenvio.Location = new System.Drawing.Point(105, 128);
            this.TB_observaciones_tipoenvio.Name = "TB_observaciones_tipoenvio";
            this.TB_observaciones_tipoenvio.Size = new System.Drawing.Size(262, 20);
            this.TB_observaciones_tipoenvio.TabIndex = 44;
            // 
            // Bagregar
            // 
            this.Bagregar.Location = new System.Drawing.Point(11, 167);
            this.Bagregar.Name = "Bagregar";
            this.Bagregar.Size = new System.Drawing.Size(100, 23);
            this.Bagregar.TabIndex = 45;
            this.Bagregar.Text = "Agregar";
            this.Bagregar.UseVisualStyleBackColor = true;
            this.Bagregar.Click += new System.EventHandler(this.Bagregar_Click);
            // 
            // Bmodificar
            // 
            this.Bmodificar.Location = new System.Drawing.Point(144, 167);
            this.Bmodificar.Name = "Bmodificar";
            this.Bmodificar.Size = new System.Drawing.Size(100, 23);
            this.Bmodificar.TabIndex = 46;
            this.Bmodificar.Text = "Modificar";
            this.Bmodificar.UseVisualStyleBackColor = true;
            // 
            // Beliminar
            // 
            this.Beliminar.Location = new System.Drawing.Point(267, 167);
            this.Beliminar.Name = "Beliminar";
            this.Beliminar.Size = new System.Drawing.Size(100, 23);
            this.Beliminar.TabIndex = 47;
            this.Beliminar.Text = "Eliminar";
            this.Beliminar.UseVisualStyleBackColor = true;
            this.Beliminar.Click += new System.EventHandler(this.Beliminar_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnnombreenvios,
            this.columndescripcionenvio});
            this.listView1.Location = new System.Drawing.Point(10, 203);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(357, 148);
            this.listView1.TabIndex = 48;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnnombreenvios
            // 
            this.columnnombreenvios.Text = "Nombre";
            this.columnnombreenvios.Width = 180;
            // 
            // columndescripcionenvio
            // 
            this.columndescripcionenvio.Text = "Observaciones";
            this.columndescripcionenvio.Width = 173;
            // 
            // BT_menuenvio
            // 
            this.BT_menuenvio.Location = new System.Drawing.Point(12, 12);
            this.BT_menuenvio.Name = "BT_menuenvio";
            this.BT_menuenvio.Size = new System.Drawing.Size(75, 23);
            this.BT_menuenvio.TabIndex = 49;
            this.BT_menuenvio.Text = "Menú";
            this.BT_menuenvio.UseVisualStyleBackColor = true;
            this.BT_menuenvio.Click += new System.EventHandler(this.BT_menuenvio_Click);
            // 
            // BT_salir
            // 
            this.BT_salir.Location = new System.Drawing.Point(293, 12);
            this.BT_salir.Name = "BT_salir";
            this.BT_salir.Size = new System.Drawing.Size(75, 23);
            this.BT_salir.TabIndex = 50;
            this.BT_salir.Text = "Salir";
            this.BT_salir.UseVisualStyleBackColor = true;
            this.BT_salir.Click += new System.EventHandler(this.BT_salir_Click);
            // 
            // Ftipoenvioinf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 363);
            this.ControlBox = false;
            this.Controls.Add(this.BT_salir);
            this.Controls.Add(this.BT_menuenvio);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.Beliminar);
            this.Controls.Add(this.Bmodificar);
            this.Controls.Add(this.Bagregar);
            this.Controls.Add(this.TB_observaciones_tipoenvio);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TB_nombre_tipoenvio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Ftipoenvioinf";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Envio de facturas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_nombre_tipoenvio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TB_observaciones_tipoenvio;
        private System.Windows.Forms.Button Bagregar;
        private System.Windows.Forms.Button Bmodificar;
        private System.Windows.Forms.Button Beliminar;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnnombreenvios;
        private System.Windows.Forms.ColumnHeader columndescripcionenvio;
        private System.Windows.Forms.Button BT_menuenvio;
        private System.Windows.Forms.Button BT_salir;
    }
}