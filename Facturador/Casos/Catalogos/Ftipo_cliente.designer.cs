namespace Facturador
{
    partial class Ftipo_cliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ftipo_cliente));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_tipocliente = new System.Windows.Forms.TextBox();
            this.Bagregar = new System.Windows.Forms.Button();
            this.Bmodificar = new System.Windows.Forms.Button();
            this.Beliminar = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnnombretipocliente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BT_menutipocliente = new System.Windows.Forms.Button();
            this.BT_salirtipocliente = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(133, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tipo de cliente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tipo de cliente";
            // 
            // TB_tipocliente
            // 
            this.TB_tipocliente.Location = new System.Drawing.Point(116, 77);
            this.TB_tipocliente.Name = "TB_tipocliente";
            this.TB_tipocliente.Size = new System.Drawing.Size(249, 20);
            this.TB_tipocliente.TabIndex = 1;
            // 
            // Bagregar
            // 
            this.Bagregar.Location = new System.Drawing.Point(15, 121);
            this.Bagregar.Name = "Bagregar";
            this.Bagregar.Size = new System.Drawing.Size(100, 23);
            this.Bagregar.TabIndex = 2;
            this.Bagregar.Text = "Agregar";
            this.Bagregar.UseVisualStyleBackColor = true;
            this.Bagregar.Click += new System.EventHandler(this.Bagregar_Click);
            // 
            // Bmodificar
            // 
            this.Bmodificar.Location = new System.Drawing.Point(137, 121);
            this.Bmodificar.Name = "Bmodificar";
            this.Bmodificar.Size = new System.Drawing.Size(100, 23);
            this.Bmodificar.TabIndex = 3;
            this.Bmodificar.Text = "Modificar";
            this.Bmodificar.UseVisualStyleBackColor = true;
            // 
            // Beliminar
            // 
            this.Beliminar.Location = new System.Drawing.Point(265, 121);
            this.Beliminar.Name = "Beliminar";
            this.Beliminar.Size = new System.Drawing.Size(100, 23);
            this.Beliminar.TabIndex = 4;
            this.Beliminar.Text = "Eliminar";
            this.Beliminar.UseVisualStyleBackColor = true;
            this.Beliminar.Click += new System.EventHandler(this.Beliminar_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnnombretipocliente});
            this.listView1.Location = new System.Drawing.Point(15, 161);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(350, 132);
            this.listView1.TabIndex = 38;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnnombretipocliente
            // 
            this.columnnombretipocliente.Text = "Tipo cliente";
            this.columnnombretipocliente.Width = 345;
            // 
            // BT_menutipocliente
            // 
            this.BT_menutipocliente.Location = new System.Drawing.Point(12, 12);
            this.BT_menutipocliente.Name = "BT_menutipocliente";
            this.BT_menutipocliente.Size = new System.Drawing.Size(75, 23);
            this.BT_menutipocliente.TabIndex = 5;
            this.BT_menutipocliente.Text = "Menú";
            this.BT_menutipocliente.UseVisualStyleBackColor = true;
            this.BT_menutipocliente.Click += new System.EventHandler(this.BT_menutipocliente_Click);
            // 
            // BT_salirtipocliente
            // 
            this.BT_salirtipocliente.Location = new System.Drawing.Point(294, 12);
            this.BT_salirtipocliente.Name = "BT_salirtipocliente";
            this.BT_salirtipocliente.Size = new System.Drawing.Size(75, 23);
            this.BT_salirtipocliente.TabIndex = 6;
            this.BT_salirtipocliente.Text = "Salir";
            this.BT_salirtipocliente.UseVisualStyleBackColor = true;
            this.BT_salirtipocliente.Click += new System.EventHandler(this.BT_salirtipocliente_Click);
            // 
            // Ftipo_cliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 300);
            this.ControlBox = false;
            this.Controls.Add(this.BT_salirtipocliente);
            this.Controls.Add(this.BT_menutipocliente);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.Beliminar);
            this.Controls.Add(this.Bmodificar);
            this.Controls.Add(this.Bagregar);
            this.Controls.Add(this.TB_tipocliente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Ftipo_cliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tipo de cliente";
            this.Load += new System.EventHandler(this.Ftipo_cliente_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_tipocliente;
        private System.Windows.Forms.Button Bagregar;
        private System.Windows.Forms.Button Bmodificar;
        private System.Windows.Forms.Button Beliminar;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnnombretipocliente;
        private System.Windows.Forms.Button BT_menutipocliente;
        private System.Windows.Forms.Button BT_salirtipocliente;
    }
}