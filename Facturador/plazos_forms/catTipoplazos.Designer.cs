
namespace Facturador.plazos
{
    partial class catTipoplazos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(catTipoplazos));
            this.dgvTioplazos = new System.Windows.Forms.DataGridView();
            this.tipoplazoid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripciontipoplazo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Grupoplazoid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grupodesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.habilitadoid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Habilitado = new System.Windows.Forms.Label();
            this.tbTipoplazoid = new System.Windows.Forms.TextBox();
            this.tbDescripcion = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.cbGrupoplazo = new System.Windows.Forms.ComboBox();
            this.cbHabilitado = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbGrupo = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTioplazos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTioplazos
            // 
            this.dgvTioplazos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTioplazos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tipoplazoid,
            this.Descripciontipoplazo,
            this.Grupoplazoid,
            this.grupodesc,
            this.habilitadoid});
            this.dgvTioplazos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvTioplazos.Location = new System.Drawing.Point(0, 203);
            this.dgvTioplazos.Name = "dgvTioplazos";
            this.dgvTioplazos.Size = new System.Drawing.Size(838, 382);
            this.dgvTioplazos.TabIndex = 0;
            this.dgvTioplazos.DoubleClick += new System.EventHandler(this.dgvTioplazos_DoubleClick);
            // 
            // tipoplazoid
            // 
            this.tipoplazoid.HeaderText = "Tipoplazoid";
            this.tipoplazoid.Name = "tipoplazoid";
            // 
            // Descripciontipoplazo
            // 
            this.Descripciontipoplazo.HeaderText = "Descripción del plazo";
            this.Descripciontipoplazo.Name = "Descripciontipoplazo";
            this.Descripciontipoplazo.Width = 300;
            // 
            // Grupoplazoid
            // 
            this.Grupoplazoid.HeaderText = "Grupoplazo";
            this.Grupoplazoid.Name = "Grupoplazoid";
            // 
            // grupodesc
            // 
            this.grupodesc.HeaderText = "Grupo";
            this.grupodesc.Name = "grupodesc";
            // 
            // habilitadoid
            // 
            this.habilitadoid.HeaderText = "Habilitado";
            this.habilitadoid.Name = "habilitadoid";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Descripción del plazo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tipoplazoid";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(717, 57);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 3;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(369, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Grupo plazo";
            // 
            // Habilitado
            // 
            this.Habilitado.AutoSize = true;
            this.Habilitado.Location = new System.Drawing.Point(516, 31);
            this.Habilitado.Name = "Habilitado";
            this.Habilitado.Size = new System.Drawing.Size(48, 13);
            this.Habilitado.TabIndex = 5;
            this.Habilitado.Text = "Habilitdo";
            // 
            // tbTipoplazoid
            // 
            this.tbTipoplazoid.Location = new System.Drawing.Point(31, 55);
            this.tbTipoplazoid.Name = "tbTipoplazoid";
            this.tbTipoplazoid.ReadOnly = true;
            this.tbTipoplazoid.Size = new System.Drawing.Size(64, 20);
            this.tbTipoplazoid.TabIndex = 6;
            this.tbTipoplazoid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbDescripcion
            // 
            this.tbDescripcion.Location = new System.Drawing.Point(124, 56);
            this.tbDescripcion.Name = "tbDescripcion";
            this.tbDescripcion.Size = new System.Drawing.Size(222, 20);
            this.tbDescripcion.TabIndex = 7;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(713, 162);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 35);
            this.button2.TabIndex = 10;
            this.button2.Text = "Eliminar Seleccionado";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Enabled = false;
            this.btnModificar.Location = new System.Drawing.Point(717, 111);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(75, 23);
            this.btnModificar.TabIndex = 11;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // cbGrupoplazo
            // 
            this.cbGrupoplazo.FormattingEnabled = true;
            this.cbGrupoplazo.Location = new System.Drawing.Point(372, 57);
            this.cbGrupoplazo.Name = "cbGrupoplazo";
            this.cbGrupoplazo.Size = new System.Drawing.Size(121, 21);
            this.cbGrupoplazo.TabIndex = 12;
            // 
            // cbHabilitado
            // 
            this.cbHabilitado.FormattingEnabled = true;
            this.cbHabilitado.Location = new System.Drawing.Point(518, 57);
            this.cbHabilitado.Name = "cbHabilitado";
            this.cbHabilitado.Size = new System.Drawing.Size(144, 21);
            this.cbHabilitado.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(521, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Grupo";
            // 
            // cbGrupo
            // 
            this.cbGrupo.FormattingEnabled = true;
            this.cbGrupo.Location = new System.Drawing.Point(518, 128);
            this.cbGrupo.Name = "cbGrupo";
            this.cbGrupo.Size = new System.Drawing.Size(144, 21);
            this.cbGrupo.TabIndex = 15;
            // 
            // catTipoplazos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 585);
            this.Controls.Add(this.cbGrupo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbHabilitado);
            this.Controls.Add(this.cbGrupoplazo);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbDescripcion);
            this.Controls.Add(this.tbTipoplazoid);
            this.Controls.Add(this.Habilitado);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvTioplazos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "catTipoplazos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Catálogo de Tipo de plazos";
            this.Load += new System.EventHandler(this.catTipoplazos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTioplazos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTioplazos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Habilitado;
        private System.Windows.Forms.TextBox tbTipoplazoid;
        private System.Windows.Forms.TextBox tbDescripcion;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.ComboBox cbGrupoplazo;
        private System.Windows.Forms.ComboBox cbHabilitado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbGrupo;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipoplazoid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripciontipoplazo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Grupoplazoid;
        private System.Windows.Forms.DataGridViewTextBoxColumn grupodesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn habilitadoid;
    }
}