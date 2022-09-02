namespace Facturador
{
    partial class tipoplazo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tipoplazo));
            this.cbgrupo_plazo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_agregar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tipoplazoid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipoplazo_cat_plazos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columTarea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnGrupoPlazoId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnGrupo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.cbGrupo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbTarea = new System.Windows.Forms.ComboBox();
            this.tbDescripcionplazo = new System.Windows.Forms.TextBox();
            this.btnGuardarcambios = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.iRowscount = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbgrupo_plazo
            // 
            this.cbgrupo_plazo.FormattingEnabled = true;
            this.cbgrupo_plazo.Location = new System.Drawing.Point(657, 14);
            this.cbgrupo_plazo.Name = "cbgrupo_plazo";
            this.cbgrupo_plazo.Size = new System.Drawing.Size(332, 21);
            this.cbgrupo_plazo.TabIndex = 2;
            this.cbgrupo_plazo.Text = " ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Descripción";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(586, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Grupo Plazo";
            // 
            // btn_agregar
            // 
            this.btn_agregar.Location = new System.Drawing.Point(812, 86);
            this.btn_agregar.Name = "btn_agregar";
            this.btn_agregar.Size = new System.Drawing.Size(75, 23);
            this.btn_agregar.TabIndex = 5;
            this.btn_agregar.Text = "Agregar";
            this.btn_agregar.UseVisualStyleBackColor = true;
            this.btn_agregar.Click += new System.EventHandler(this.btn_agregar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tipoplazoid,
            this.tipoplazo_cat_plazos,
            this.columTarea,
            this.columnGrupoPlazoId,
            this.columnGrupo});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 173);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1149, 483);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // tipoplazoid
            // 
            this.tipoplazoid.HeaderText = "tipoplazoid";
            this.tipoplazoid.Name = "tipoplazoid";
            // 
            // tipoplazo_cat_plazos
            // 
            this.tipoplazo_cat_plazos.HeaderText = "Tipo plazo";
            this.tipoplazo_cat_plazos.Name = "tipoplazo_cat_plazos";
            // 
            // columTarea
            // 
            this.columTarea.HeaderText = "Tarea";
            this.columTarea.Name = "columTarea";
            // 
            // columnGrupoPlazoId
            // 
            this.columnGrupoPlazoId.HeaderText = "Grupo Plazo";
            this.columnGrupoPlazoId.Name = "columnGrupoPlazoId";
            // 
            // columnGrupo
            // 
            this.columnGrupo.HeaderText = "Grupo";
            this.columnGrupo.Name = "columnGrupo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(615, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Grupo";
            // 
            // cbGrupo
            // 
            this.cbGrupo.FormattingEnabled = true;
            this.cbGrupo.Location = new System.Drawing.Point(657, 53);
            this.cbGrupo.Name = "cbGrupo";
            this.cbGrupo.Size = new System.Drawing.Size(239, 21);
            this.cbGrupo.TabIndex = 4;
            this.cbGrupo.Text = " ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Tarea";
            // 
            // cbTarea
            // 
            this.cbTarea.FormattingEnabled = true;
            this.cbTarea.Location = new System.Drawing.Point(114, 58);
            this.cbTarea.Name = "cbTarea";
            this.cbTarea.Size = new System.Drawing.Size(369, 21);
            this.cbTarea.TabIndex = 3;
            this.cbTarea.Text = " ";
            // 
            // tbDescripcionplazo
            // 
            this.tbDescripcionplazo.Location = new System.Drawing.Point(114, 14);
            this.tbDescripcionplazo.Name = "tbDescripcionplazo";
            this.tbDescripcionplazo.Size = new System.Drawing.Size(369, 20);
            this.tbDescripcionplazo.TabIndex = 1;
            // 
            // btnGuardarcambios
            // 
            this.btnGuardarcambios.Enabled = false;
            this.btnGuardarcambios.Location = new System.Drawing.Point(893, 86);
            this.btnGuardarcambios.Name = "btnGuardarcambios";
            this.btnGuardarcambios.Size = new System.Drawing.Size(106, 23);
            this.btnGuardarcambios.TabIndex = 6;
            this.btnGuardarcambios.Text = "Guardar Cambios";
            this.btnGuardarcambios.UseVisualStyleBackColor = true;
            this.btnGuardarcambios.Click += new System.EventHandler(this.btnGuardarcambios_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1062, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "Salir";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // iRowscount
            // 
            this.iRowscount.Location = new System.Drawing.Point(69, 139);
            this.iRowscount.Name = "iRowscount";
            this.iRowscount.ReadOnly = true;
            this.iRowscount.Size = new System.Drawing.Size(46, 20);
            this.iRowscount.TabIndex = 12;
            this.iRowscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Location = new System.Drawing.Point(893, 115);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(106, 23);
            this.btnCancelar.TabIndex = 13;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Registros";
            // 
            // btnEliminar
            // 
            this.btnEliminar.Enabled = false;
            this.btnEliminar.Location = new System.Drawing.Point(893, 144);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(106, 23);
            this.btnEliminar.TabIndex = 15;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // tipoplazo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 656);
            this.ControlBox = false;
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.iRowscount);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnGuardarcambios);
            this.Controls.Add(this.tbDescripcionplazo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbTarea);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbGrupo);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_agregar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbgrupo_plazo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "tipoplazo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tipo plazo";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbgrupo_plazo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_agregar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbGrupo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbTarea;
        private System.Windows.Forms.TextBox tbDescripcionplazo;
        private System.Windows.Forms.Button btnGuardarcambios;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox iRowscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipoplazoid;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipoplazo_cat_plazos;
        private System.Windows.Forms.DataGridViewTextBoxColumn columTarea;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnGrupoPlazoId;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnGrupo;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEliminar;
    }
}