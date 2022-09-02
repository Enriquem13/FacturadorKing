
namespace Facturador
{
    partial class consultaulticasos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(consultaulticasos));
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cbSelectGrupo = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tbLimitcasos = new System.Windows.Forms.TextBox();
            this.dgRowCasos = new System.Windows.Forms.DataGridView();
            this.pais = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Casoid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Casonumero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expediente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.registro_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.registro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tituloomarca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prioridad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgRowCasos)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(577, 53);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(75, 20);
            this.textBox10.TabIndex = 29;
            this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(577, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 28;
            this.button1.Text = "Consulta";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbSelectGrupo
            // 
            this.cbSelectGrupo.FormattingEnabled = true;
            this.cbSelectGrupo.Location = new System.Drawing.Point(81, 14);
            this.cbSelectGrupo.Name = "cbSelectGrupo";
            this.cbSelectGrupo.Size = new System.Drawing.Size(162, 21);
            this.cbSelectGrupo.TabIndex = 168;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(39, 17);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 13);
            this.label13.TabIndex = 167;
            this.label13.Text = "Grupo";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(259, 18);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(102, 13);
            this.label14.TabIndex = 170;
            this.label14.Text = "Consultar los últimos";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(409, 18);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(36, 13);
            this.label15.TabIndex = 172;
            this.label15.Text = "Casos";
            // 
            // tbLimitcasos
            // 
            this.tbLimitcasos.Location = new System.Drawing.Point(367, 15);
            this.tbLimitcasos.MaxLength = 3;
            this.tbLimitcasos.Name = "tbLimitcasos";
            this.tbLimitcasos.Size = new System.Drawing.Size(36, 20);
            this.tbLimitcasos.TabIndex = 171;
            this.tbLimitcasos.Text = "60";
            this.tbLimitcasos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dgRowCasos
            // 
            this.dgRowCasos.AllowUserToAddRows = false;
            this.dgRowCasos.AllowUserToDeleteRows = false;
            this.dgRowCasos.AllowUserToOrderColumns = true;
            this.dgRowCasos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRowCasos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pais,
            this.Casoid,
            this.Casonumero,
            this.tipo,
            this.expediente,
            this.registro_,
            this.registro,
            this.tituloomarca,
            this.cliente,
            this.prioridad,
            this.referencia});
            this.dgRowCasos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgRowCasos.Location = new System.Drawing.Point(0, 79);
            this.dgRowCasos.Name = "dgRowCasos";
            this.dgRowCasos.Size = new System.Drawing.Size(1143, 516);
            this.dgRowCasos.TabIndex = 173;
            // 
            // pais
            // 
            this.pais.HeaderText = "País";
            this.pais.Name = "pais";
            // 
            // Casoid
            // 
            this.Casoid.HeaderText = "Caso id";
            this.Casoid.Name = "Casoid";
            // 
            // Casonumero
            // 
            this.Casonumero.HeaderText = "Caso Número";
            this.Casonumero.Name = "Casonumero";
            // 
            // tipo
            // 
            this.tipo.HeaderText = "Tipo";
            this.tipo.Name = "tipo";
            // 
            // expediente
            // 
            this.expediente.HeaderText = "expediente";
            this.expediente.Name = "expediente";
            // 
            // registro_
            // 
            this.registro_.HeaderText = "Registro";
            this.registro_.Name = "registro_";
            // 
            // registro
            // 
            this.registro.HeaderText = "interesado";
            this.registro.Name = "registro";
            // 
            // tituloomarca
            // 
            this.tituloomarca.HeaderText = "Título/Denominación";
            this.tituloomarca.Name = "tituloomarca";
            // 
            // cliente
            // 
            this.cliente.HeaderText = "Cliente";
            this.cliente.Name = "cliente";
            // 
            // prioridad
            // 
            this.prioridad.HeaderText = "Prioridad";
            this.prioridad.Name = "prioridad";
            // 
            // referencia
            // 
            this.referencia.HeaderText = "Referencia";
            this.referencia.Name = "referencia";
            // 
            // consultaulticasos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 595);
            this.Controls.Add(this.dgRowCasos);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.tbLimitcasos);
            this.Controls.Add(this.cbSelectGrupo);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "consultaulticasos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Consultar últimos casos";
            this.ResizeEnd += new System.EventHandler(this.consultaulticasos_ResizeEnd);
            this.Resize += new System.EventHandler(this.consultaulticasos_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgRowCasos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox10;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbSelectGrupo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbLimitcasos;
        private System.Windows.Forms.DataGridView dgRowCasos;
        private System.Windows.Forms.DataGridViewTextBoxColumn pais;
        private System.Windows.Forms.DataGridViewTextBoxColumn Casoid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Casonumero;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn expediente;
        private System.Windows.Forms.DataGridViewTextBoxColumn registro_;
        private System.Windows.Forms.DataGridViewTextBoxColumn registro;
        private System.Windows.Forms.DataGridViewTextBoxColumn tituloomarca;
        private System.Windows.Forms.DataGridViewTextBoxColumn cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn prioridad;
        private System.Windows.Forms.DataGridViewTextBoxColumn referencia;
    }
}