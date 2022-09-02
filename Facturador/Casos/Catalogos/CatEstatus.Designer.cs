namespace Facturador
{
    partial class CatEstatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CatEstatus));
            this.label1 = new System.Windows.Forms.Label();
            this.tb_estatusespano = new System.Windows.Forms.TextBox();
            this.tb_estatusingles = new System.Windows.Forms.TextBox();
            this.cb_seguimiento = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dgv_estatuscaso = new System.Windows.Forms.DataGridView();
            this.estatuscasoid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estatuscasodescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estatuscasodescripingles = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.habilitado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btn_salir = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_estatuscaso)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Descripción del estatus:";
            // 
            // tb_estatusespano
            // 
            this.tb_estatusespano.Location = new System.Drawing.Point(48, 103);
            this.tb_estatusespano.Name = "tb_estatusespano";
            this.tb_estatusespano.Size = new System.Drawing.Size(183, 20);
            this.tb_estatusespano.TabIndex = 1;
            // 
            // tb_estatusingles
            // 
            this.tb_estatusingles.Location = new System.Drawing.Point(48, 191);
            this.tb_estatusingles.Name = "tb_estatusingles";
            this.tb_estatusingles.Size = new System.Drawing.Size(183, 20);
            this.tb_estatusingles.TabIndex = 2;
            // 
            // cb_seguimiento
            // 
            this.cb_seguimiento.FormattingEnabled = true;
            this.cb_seguimiento.Location = new System.Drawing.Point(297, 103);
            this.cb_seguimiento.Name = "cb_seguimiento";
            this.cb_seguimiento.Size = new System.Drawing.Size(134, 21);
            this.cb_seguimiento.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Descripción del estatus (Inglés):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(294, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Dar seguimiento:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(356, 191);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Agregar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgv_estatuscaso
            // 
            this.dgv_estatuscaso.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_estatuscaso.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.estatuscasoid,
            this.estatuscasodescrip,
            this.estatuscasodescripingles,
            this.habilitado});
            this.dgv_estatuscaso.Dock = System.Windows.Forms.DockStyle.Right;
            this.dgv_estatuscaso.Location = new System.Drawing.Point(492, 0);
            this.dgv_estatuscaso.Name = "dgv_estatuscaso";
            this.dgv_estatuscaso.Size = new System.Drawing.Size(779, 417);
            this.dgv_estatuscaso.TabIndex = 7;
            this.dgv_estatuscaso.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_estatuscaso_RowHeaderMouseDoubleClick);
            // 
            // estatuscasoid
            // 
            this.estatuscasoid.HeaderText = "estatuscasoid";
            this.estatuscasoid.Name = "estatuscasoid";
            this.estatuscasoid.Visible = false;
            // 
            // estatuscasodescrip
            // 
            this.estatuscasodescrip.HeaderText = "Estatus Caso Descripción";
            this.estatuscasodescrip.Name = "estatuscasodescrip";
            this.estatuscasodescrip.Width = 300;
            // 
            // estatuscasodescripingles
            // 
            this.estatuscasodescripingles.HeaderText = "Estatus Caso Descripción Inglés";
            this.estatuscasodescripingles.Name = "estatuscasodescripingles";
            this.estatuscasodescripingles.Width = 300;
            // 
            // habilitado
            // 
            this.habilitado.HeaderText = "Dar seguimiento";
            this.habilitado.Name = "habilitado";
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(356, 292);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Modificar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(356, 321);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "Eliminar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_salir
            // 
            this.btn_salir.Location = new System.Drawing.Point(411, 12);
            this.btn_salir.Name = "btn_salir";
            this.btn_salir.Size = new System.Drawing.Size(75, 23);
            this.btn_salir.TabIndex = 10;
            this.btn_salir.Text = "Salir";
            this.btn_salir.UseVisualStyleBackColor = true;
            this.btn_salir.Click += new System.EventHandler(this.btn_salir_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(356, 221);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 11;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // CatEstatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 417);
            this.ControlBox = false;
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_salir);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dgv_estatuscaso);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cb_seguimiento);
            this.Controls.Add(this.tb_estatusingles);
            this.Controls.Add(this.tb_estatusespano);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CatEstatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Catálogo de Estatus";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_estatuscaso)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_estatusespano;
        private System.Windows.Forms.TextBox tb_estatusingles;
        private System.Windows.Forms.ComboBox cb_seguimiento;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgv_estatuscaso;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btn_salir;
        private System.Windows.Forms.DataGridViewTextBoxColumn estatuscasoid;
        private System.Windows.Forms.DataGridViewTextBoxColumn estatuscasodescrip;
        private System.Windows.Forms.DataGridViewTextBoxColumn estatuscasodescripingles;
        private System.Windows.Forms.DataGridViewTextBoxColumn habilitado;
        private System.Windows.Forms.Button btn_cancelar;
    }
}