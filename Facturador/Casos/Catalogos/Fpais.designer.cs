namespace Facturador
{
    partial class Fpais
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fpais));
            this.label1 = new System.Windows.Forms.Label();
            this.BT_menupais = new System.Windows.Forms.Button();
            this.BT_salirpais = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_nombre_pais_esp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_clavepais = new System.Windows.Forms.TextBox();
            this.TB_nacionalidad_esp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_nombrepais_ing = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TB_inglesnacionalidad = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Bagregar = new System.Windows.Forms.Button();
            this.Bmodificar = new System.Windows.Forms.Button();
            this.Beliminar = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnclavepais = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnnombre_pais_esp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnacionaliddad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnationality = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(250, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "País";
            // 
            // BT_menupais
            // 
            this.BT_menupais.Location = new System.Drawing.Point(12, 12);
            this.BT_menupais.Name = "BT_menupais";
            this.BT_menupais.Size = new System.Drawing.Size(75, 23);
            this.BT_menupais.TabIndex = 9;
            this.BT_menupais.Text = "Menú";
            this.BT_menupais.UseVisualStyleBackColor = true;
            this.BT_menupais.Click += new System.EventHandler(this.BT_menupais_Click);
            // 
            // BT_salirpais
            // 
            this.BT_salirpais.Location = new System.Drawing.Point(436, 12);
            this.BT_salirpais.Name = "BT_salirpais";
            this.BT_salirpais.Size = new System.Drawing.Size(75, 23);
            this.BT_salirpais.TabIndex = 10;
            this.BT_salirpais.Text = "Salir";
            this.BT_salirpais.UseVisualStyleBackColor = true;
            this.BT_salirpais.Click += new System.EventHandler(this.BT_salirpais_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(134, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Nombre";
            // 
            // TB_nombre_pais_esp
            // 
            this.TB_nombre_pais_esp.Location = new System.Drawing.Point(190, 123);
            this.TB_nombre_pais_esp.Name = "TB_nombre_pais_esp";
            this.TB_nombre_pais_esp.Size = new System.Drawing.Size(250, 20);
            this.TB_nombre_pais_esp.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(145, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 48;
            this.label3.Text = "Clave";
            // 
            // TB_clavepais
            // 
            this.TB_clavepais.AcceptsReturn = true;
            this.TB_clavepais.Location = new System.Drawing.Point(190, 78);
            this.TB_clavepais.Name = "TB_clavepais";
            this.TB_clavepais.Size = new System.Drawing.Size(250, 20);
            this.TB_clavepais.TabIndex = 1;
            this.TB_clavepais.Text = "2 Letras";
            // 
            // TB_nacionalidad_esp
            // 
            this.TB_nacionalidad_esp.Location = new System.Drawing.Point(190, 161);
            this.TB_nacionalidad_esp.Name = "TB_nacionalidad_esp";
            this.TB_nacionalidad_esp.Size = new System.Drawing.Size(250, 20);
            this.TB_nacionalidad_esp.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(103, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 50;
            this.label4.Text = "Nacionalidad";
            // 
            // TB_nombrepais_ing
            // 
            this.TB_nombrepais_ing.Location = new System.Drawing.Point(190, 201);
            this.TB_nombrepais_ing.Name = "TB_nombrepais_ing";
            this.TB_nombrepais_ing.Size = new System.Drawing.Size(250, 20);
            this.TB_nombrepais_ing.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(78, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 52;
            this.label5.Text = "Nombre en Inglés";
            // 
            // TB_inglesnacionalidad
            // 
            this.TB_inglesnacionalidad.Location = new System.Drawing.Point(190, 241);
            this.TB_inglesnacionalidad.Name = "TB_inglesnacionalidad";
            this.TB_inglesnacionalidad.Size = new System.Drawing.Size(250, 20);
            this.TB_inglesnacionalidad.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(47, 244);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "Nacionalidad en Inglés";
            // 
            // Bagregar
            // 
            this.Bagregar.Location = new System.Drawing.Point(50, 302);
            this.Bagregar.Name = "Bagregar";
            this.Bagregar.Size = new System.Drawing.Size(100, 23);
            this.Bagregar.TabIndex = 6;
            this.Bagregar.Text = "Agregar";
            this.Bagregar.UseVisualStyleBackColor = true;
            this.Bagregar.Click += new System.EventHandler(this.Bagregar_Click);
            // 
            // Bmodificar
            // 
            this.Bmodificar.Location = new System.Drawing.Point(208, 302);
            this.Bmodificar.Name = "Bmodificar";
            this.Bmodificar.Size = new System.Drawing.Size(100, 23);
            this.Bmodificar.TabIndex = 7;
            this.Bmodificar.Text = "Modificar";
            this.Bmodificar.UseVisualStyleBackColor = true;
            // 
            // Beliminar
            // 
            this.Beliminar.Location = new System.Drawing.Point(369, 302);
            this.Beliminar.Name = "Beliminar";
            this.Beliminar.Size = new System.Drawing.Size(100, 23);
            this.Beliminar.TabIndex = 8;
            this.Beliminar.Text = "Eliminar";
            this.Beliminar.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnclavepais,
            this.columnnombre_pais_esp,
            this.columnacionaliddad,
            this.columnname,
            this.columnationality});
            this.listView1.Location = new System.Drawing.Point(12, 356);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(499, 153);
            this.listView1.TabIndex = 58;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnclavepais
            // 
            this.columnclavepais.Text = "Clave";
            this.columnclavepais.Width = 65;
            // 
            // columnnombre_pais_esp
            // 
            this.columnnombre_pais_esp.Text = "Nombre";
            this.columnnombre_pais_esp.Width = 92;
            // 
            // columnacionaliddad
            // 
            this.columnacionaliddad.Text = "Nacionalidad";
            this.columnacionaliddad.Width = 106;
            // 
            // columnname
            // 
            this.columnname.Text = "Name";
            this.columnname.Width = 112;
            // 
            // columnationality
            // 
            this.columnationality.Text = "Nationality";
            this.columnationality.Width = 116;
            // 
            // Fpais
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 521);
            this.ControlBox = false;
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.Beliminar);
            this.Controls.Add(this.Bmodificar);
            this.Controls.Add(this.Bagregar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TB_inglesnacionalidad);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TB_nombrepais_ing);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TB_nacionalidad_esp);
            this.Controls.Add(this.TB_clavepais);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TB_nombre_pais_esp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BT_salirpais);
            this.Controls.Add(this.BT_menupais);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Fpais";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "País";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BT_menupais;
        private System.Windows.Forms.Button BT_salirpais;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_nombre_pais_esp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_clavepais;
        private System.Windows.Forms.TextBox TB_nacionalidad_esp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TB_nombrepais_ing;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TB_inglesnacionalidad;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Bagregar;
        private System.Windows.Forms.Button Bmodificar;
        private System.Windows.Forms.Button Beliminar;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnclavepais;
        private System.Windows.Forms.ColumnHeader columnnombre_pais_esp;
        private System.Windows.Forms.ColumnHeader columnacionaliddad;
        private System.Windows.Forms.ColumnHeader columnname;
        private System.Windows.Forms.ColumnHeader columnationality;
    }
}