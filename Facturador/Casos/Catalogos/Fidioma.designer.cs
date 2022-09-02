namespace Facturador
{
    partial class Fidioma
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fidioma));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_nombreidioma = new System.Windows.Forms.TextBox();
            this.TB_claveidoma = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Bagregar = new System.Windows.Forms.Button();
            this.Bmodificar = new System.Windows.Forms.Button();
            this.Beliminar = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnnombreidioma = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnclaveidioma = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BT_menuidioma = new System.Windows.Forms.Button();
            this.BT_saliridioma = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(168, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Idioma";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Idioma";
            // 
            // TB_nombreidioma
            // 
            this.TB_nombreidioma.Location = new System.Drawing.Point(86, 104);
            this.TB_nombreidioma.Name = "TB_nombreidioma";
            this.TB_nombreidioma.Size = new System.Drawing.Size(250, 20);
            this.TB_nombreidioma.TabIndex = 5;
            // 
            // TB_claveidoma
            // 
            this.TB_claveidoma.Location = new System.Drawing.Point(86, 162);
            this.TB_claveidoma.Name = "TB_claveidoma";
            this.TB_claveidoma.Size = new System.Drawing.Size(250, 20);
            this.TB_claveidoma.TabIndex = 41;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(41, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 42;
            this.label3.Text = "Clave";
            // 
            // Bagregar
            // 
            this.Bagregar.Location = new System.Drawing.Point(19, 210);
            this.Bagregar.Name = "Bagregar";
            this.Bagregar.Size = new System.Drawing.Size(100, 23);
            this.Bagregar.TabIndex = 46;
            this.Bagregar.Text = "Agregar";
            this.Bagregar.UseVisualStyleBackColor = true;
            this.Bagregar.Click += new System.EventHandler(this.Bagregar_Click);
            // 
            // Bmodificar
            // 
            this.Bmodificar.Location = new System.Drawing.Point(146, 210);
            this.Bmodificar.Name = "Bmodificar";
            this.Bmodificar.Size = new System.Drawing.Size(100, 23);
            this.Bmodificar.TabIndex = 47;
            this.Bmodificar.Text = "Modificar";
            this.Bmodificar.UseVisualStyleBackColor = true;
            this.Bmodificar.Click += new System.EventHandler(this.Bmodificar_Click);
            // 
            // Beliminar
            // 
            this.Beliminar.Location = new System.Drawing.Point(275, 210);
            this.Beliminar.Name = "Beliminar";
            this.Beliminar.Size = new System.Drawing.Size(100, 23);
            this.Beliminar.TabIndex = 48;
            this.Beliminar.Text = "Eliminar";
            this.Beliminar.UseVisualStyleBackColor = true;
            this.Beliminar.Click += new System.EventHandler(this.Beliminar_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnnombreidioma,
            this.columnclaveidioma});
            this.listView1.Location = new System.Drawing.Point(12, 248);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(363, 183);
            this.listView1.TabIndex = 49;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnnombreidioma
            // 
            this.columnnombreidioma.Text = "Nombre Idioma";
            this.columnnombreidioma.Width = 232;
            // 
            // columnclaveidioma
            // 
            this.columnclaveidioma.Text = "Clave";
            this.columnclaveidioma.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnclaveidioma.Width = 129;
            // 
            // BT_menuidioma
            // 
            this.BT_menuidioma.Location = new System.Drawing.Point(12, 12);
            this.BT_menuidioma.Name = "BT_menuidioma";
            this.BT_menuidioma.Size = new System.Drawing.Size(75, 23);
            this.BT_menuidioma.TabIndex = 50;
            this.BT_menuidioma.Text = "Menú";
            this.BT_menuidioma.UseVisualStyleBackColor = true;
            this.BT_menuidioma.Click += new System.EventHandler(this.BT_menuidioma_Click);
            // 
            // BT_saliridioma
            // 
            this.BT_saliridioma.Location = new System.Drawing.Point(300, 12);
            this.BT_saliridioma.Name = "BT_saliridioma";
            this.BT_saliridioma.Size = new System.Drawing.Size(75, 23);
            this.BT_saliridioma.TabIndex = 51;
            this.BT_saliridioma.Text = "Salir";
            this.BT_saliridioma.UseVisualStyleBackColor = true;
            this.BT_saliridioma.Click += new System.EventHandler(this.BT_saliridioma_Click);
            // 
            // Fidioma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 443);
            this.ControlBox = false;
            this.Controls.Add(this.BT_saliridioma);
            this.Controls.Add(this.BT_menuidioma);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.Beliminar);
            this.Controls.Add(this.Bmodificar);
            this.Controls.Add(this.Bagregar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TB_claveidoma);
            this.Controls.Add(this.TB_nombreidioma);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Fidioma";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Idioma";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_nombreidioma;
        private System.Windows.Forms.TextBox TB_claveidoma;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Bagregar;
        private System.Windows.Forms.Button Bmodificar;
        private System.Windows.Forms.Button Beliminar;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button BT_menuidioma;
        private System.Windows.Forms.Button BT_saliridioma;
        private System.Windows.Forms.ColumnHeader columnnombreidioma;
        private System.Windows.Forms.ColumnHeader columnclaveidioma;
    }
}