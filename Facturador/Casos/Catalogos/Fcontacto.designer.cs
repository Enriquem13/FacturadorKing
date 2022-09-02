namespace Facturador
{
    partial class Fcontacto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fcontacto));
            this.label2 = new System.Windows.Forms.Label();
            this.TB_nombre_contacto = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.Bagregar = new System.Windows.Forms.Button();
            this.Bmodificar = new System.Windows.Forms.Button();
            this.Beliminar = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TB_sexo = new System.Windows.Forms.TextBox();
            this.TB_comentarioscontacto = new System.Windows.Forms.TextBox();
            this.TB_puestocontacto = new System.Windows.Forms.TextBox();
            this.TB_telefonocontacto = new System.Windows.Forms.TextBox();
            this.TB_nicknamecontacto = new System.Windows.Forms.TextBox();
            this.TB_correocontacto = new System.Windows.Forms.TextBox();
            this.TB_areacontacto = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.BT_menucontacto = new System.Windows.Forms.Button();
            this.BT_salircontacto = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nombre";
            // 
            // TB_nombre_contacto
            // 
            this.TB_nombre_contacto.Location = new System.Drawing.Point(89, 72);
            this.TB_nombre_contacto.Name = "TB_nombre_contacto";
            this.TB_nombre_contacto.Size = new System.Drawing.Size(262, 20);
            this.TB_nombre_contacto.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Cliente #";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(430, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Nickname";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(48, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Sexo";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(436, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Teléfono";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(50, 148);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Área";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(447, 148);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Puesto";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(39, 192);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Correo";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(417, 192);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Comentarios";
            // 
            // Bagregar
            // 
            this.Bagregar.Location = new System.Drawing.Point(89, 226);
            this.Bagregar.Name = "Bagregar";
            this.Bagregar.Size = new System.Drawing.Size(100, 23);
            this.Bagregar.TabIndex = 9;
            this.Bagregar.Text = "Agregar";
            this.Bagregar.UseVisualStyleBackColor = true;
            this.Bagregar.Click += new System.EventHandler(this.Bagregar_Click);
            // 
            // Bmodificar
            // 
            this.Bmodificar.Location = new System.Drawing.Point(359, 226);
            this.Bmodificar.Name = "Bmodificar";
            this.Bmodificar.Size = new System.Drawing.Size(100, 23);
            this.Bmodificar.TabIndex = 10;
            this.Bmodificar.Text = "Modificar";
            this.Bmodificar.UseVisualStyleBackColor = true;
            this.Bmodificar.Click += new System.EventHandler(this.Bmodificar_Click);
            // 
            // Beliminar
            // 
            this.Beliminar.Location = new System.Drawing.Point(661, 226);
            this.Beliminar.Name = "Beliminar";
            this.Beliminar.Size = new System.Drawing.Size(100, 23);
            this.Beliminar.TabIndex = 11;
            this.Beliminar.Text = "Eliminar";
            this.Beliminar.UseVisualStyleBackColor = true;
            this.Beliminar.Click += new System.EventHandler(this.Beliminar_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(89, 36);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(79, 20);
            this.textBox3.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TB_sexo);
            this.groupBox1.Controls.Add(this.TB_comentarioscontacto);
            this.groupBox1.Controls.Add(this.TB_puestocontacto);
            this.groupBox1.Controls.Add(this.TB_telefonocontacto);
            this.groupBox1.Controls.Add(this.TB_nicknamecontacto);
            this.groupBox1.Controls.Add(this.TB_correocontacto);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.Beliminar);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.TB_areacontacto);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.Bmodificar);
            this.groupBox1.Controls.Add(this.Bagregar);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TB_nombre_contacto);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(825, 274);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contacto";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // TB_sexo
            // 
            this.TB_sexo.Location = new System.Drawing.Point(89, 105);
            this.TB_sexo.Name = "TB_sexo";
            this.TB_sexo.Size = new System.Drawing.Size(262, 20);
            this.TB_sexo.TabIndex = 2;
            // 
            // TB_comentarioscontacto
            // 
            this.TB_comentarioscontacto.Location = new System.Drawing.Point(499, 189);
            this.TB_comentarioscontacto.Name = "TB_comentarioscontacto";
            this.TB_comentarioscontacto.Size = new System.Drawing.Size(262, 20);
            this.TB_comentarioscontacto.TabIndex = 8;
            // 
            // TB_puestocontacto
            // 
            this.TB_puestocontacto.Location = new System.Drawing.Point(499, 145);
            this.TB_puestocontacto.Name = "TB_puestocontacto";
            this.TB_puestocontacto.Size = new System.Drawing.Size(262, 20);
            this.TB_puestocontacto.TabIndex = 7;
            // 
            // TB_telefonocontacto
            // 
            this.TB_telefonocontacto.Location = new System.Drawing.Point(499, 104);
            this.TB_telefonocontacto.Name = "TB_telefonocontacto";
            this.TB_telefonocontacto.Size = new System.Drawing.Size(262, 20);
            this.TB_telefonocontacto.TabIndex = 6;
            // 
            // TB_nicknamecontacto
            // 
            this.TB_nicknamecontacto.Location = new System.Drawing.Point(499, 72);
            this.TB_nicknamecontacto.Name = "TB_nicknamecontacto";
            this.TB_nicknamecontacto.Size = new System.Drawing.Size(262, 20);
            this.TB_nicknamecontacto.TabIndex = 5;
            // 
            // TB_correocontacto
            // 
            this.TB_correocontacto.Location = new System.Drawing.Point(89, 189);
            this.TB_correocontacto.Name = "TB_correocontacto";
            this.TB_correocontacto.Size = new System.Drawing.Size(262, 20);
            this.TB_correocontacto.TabIndex = 4;
            // 
            // TB_areacontacto
            // 
            this.TB_areacontacto.Location = new System.Drawing.Point(89, 148);
            this.TB_areacontacto.Name = "TB_areacontacto";
            this.TB_areacontacto.Size = new System.Drawing.Size(262, 20);
            this.TB_areacontacto.TabIndex = 3;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 327);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(825, 161);
            this.listView1.TabIndex = 51;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // BT_menucontacto
            // 
            this.BT_menucontacto.Location = new System.Drawing.Point(12, 6);
            this.BT_menucontacto.Name = "BT_menucontacto";
            this.BT_menucontacto.Size = new System.Drawing.Size(75, 23);
            this.BT_menucontacto.TabIndex = 52;
            this.BT_menucontacto.Text = "Menú";
            this.BT_menucontacto.UseVisualStyleBackColor = true;
            this.BT_menucontacto.Click += new System.EventHandler(this.BT_menucontacto_Click);
            // 
            // BT_salircontacto
            // 
            this.BT_salircontacto.Location = new System.Drawing.Point(762, 6);
            this.BT_salircontacto.Name = "BT_salircontacto";
            this.BT_salircontacto.Size = new System.Drawing.Size(75, 23);
            this.BT_salircontacto.TabIndex = 53;
            this.BT_salircontacto.Text = "Salir";
            this.BT_salircontacto.UseVisualStyleBackColor = true;
            this.BT_salircontacto.Click += new System.EventHandler(this.BT_salircontacto_Click);
            // 
            // Fcontacto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 500);
            this.ControlBox = false;
            this.Controls.Add(this.BT_salircontacto);
            this.Controls.Add(this.BT_menucontacto);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Fcontacto";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Contacto";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_nombre_contacto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button Bagregar;
        private System.Windows.Forms.Button Bmodificar;
        private System.Windows.Forms.Button Beliminar;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox TB_comentarioscontacto;
        private System.Windows.Forms.TextBox TB_puestocontacto;
        private System.Windows.Forms.TextBox TB_telefonocontacto;
        private System.Windows.Forms.TextBox TB_nicknamecontacto;
        private System.Windows.Forms.TextBox TB_correocontacto;
        private System.Windows.Forms.TextBox TB_areacontacto;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BT_menucontacto;
        private System.Windows.Forms.Button BT_salircontacto;
        private System.Windows.Forms.TextBox TB_sexo;
    }
}