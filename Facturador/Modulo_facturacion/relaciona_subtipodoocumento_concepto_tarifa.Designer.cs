namespace Facturador.Modulo_facturacion
{
    partial class relaciona_subtipodoocumento_concepto_tarifa
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbEscritos_subtipodocumento = new System.Windows.Forms.ComboBox();
            this.cb_conceptoratifa = new System.Windows.Forms.ComboBox();
            this.dgv_relacionsubtipoconceptotarifa = new System.Windows.Forms.DataGridView();
            this.relacion_subtipodocumento_tarifaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_subtipodocumentoid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_tarifa_concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubTipoDocumentoDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_relacionsubtipoconceptotarifa)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Selecciona un Escrito:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Selecciona el concepto de la tarifa a sugerir";
            // 
            // cbEscritos_subtipodocumento
            // 
            this.cbEscritos_subtipodocumento.FormattingEnabled = true;
            this.cbEscritos_subtipodocumento.Location = new System.Drawing.Point(32, 39);
            this.cbEscritos_subtipodocumento.Name = "cbEscritos_subtipodocumento";
            this.cbEscritos_subtipodocumento.Size = new System.Drawing.Size(1203, 21);
            this.cbEscritos_subtipodocumento.TabIndex = 2;
            // 
            // cb_conceptoratifa
            // 
            this.cb_conceptoratifa.FormattingEnabled = true;
            this.cb_conceptoratifa.Location = new System.Drawing.Point(32, 103);
            this.cb_conceptoratifa.Name = "cb_conceptoratifa";
            this.cb_conceptoratifa.Size = new System.Drawing.Size(1202, 21);
            this.cb_conceptoratifa.TabIndex = 3;
            // 
            // dgv_relacionsubtipoconceptotarifa
            // 
            this.dgv_relacionsubtipoconceptotarifa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_relacionsubtipoconceptotarifa.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.relacion_subtipodocumento_tarifaid,
            this.id_subtipodocumentoid,
            this.id_tarifa_concepto,
            this.SubTipoDocumentoDescrip,
            this.concepto});
            this.dgv_relacionsubtipoconceptotarifa.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv_relacionsubtipoconceptotarifa.Location = new System.Drawing.Point(0, 323);
            this.dgv_relacionsubtipoconceptotarifa.Name = "dgv_relacionsubtipoconceptotarifa";
            this.dgv_relacionsubtipoconceptotarifa.Size = new System.Drawing.Size(1411, 331);
            this.dgv_relacionsubtipoconceptotarifa.TabIndex = 4;
            // 
            // relacion_subtipodocumento_tarifaid
            // 
            this.relacion_subtipodocumento_tarifaid.HeaderText = "relacion_subtipodocumento_tarifaid";
            this.relacion_subtipodocumento_tarifaid.Name = "relacion_subtipodocumento_tarifaid";
            this.relacion_subtipodocumento_tarifaid.Visible = false;
            // 
            // id_subtipodocumentoid
            // 
            this.id_subtipodocumentoid.HeaderText = "id_subtipodocumentoid";
            this.id_subtipodocumentoid.Name = "id_subtipodocumentoid";
            this.id_subtipodocumentoid.Visible = false;
            // 
            // id_tarifa_concepto
            // 
            this.id_tarifa_concepto.HeaderText = "id_tarifa_concepto";
            this.id_tarifa_concepto.Name = "id_tarifa_concepto";
            this.id_tarifa_concepto.Visible = false;
            // 
            // SubTipoDocumentoDescrip
            // 
            this.SubTipoDocumentoDescrip.HeaderText = "Escrito";
            this.SubTipoDocumentoDescrip.Name = "SubTipoDocumentoDescrip";
            this.SubTipoDocumentoDescrip.Width = 700;
            // 
            // concepto
            // 
            this.concepto.HeaderText = "Tarifa sugerida";
            this.concepto.Name = "concepto";
            this.concepto.Width = 700;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1201, 141);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(198, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Agregar Concepto de tarifa a Escrito";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1324, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Salir";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1201, 294);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(210, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Eliminar Seleccionados";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // relaciona_subtipodoocumento_concepto_tarifa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1411, 654);
            this.ControlBox = false;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgv_relacionsubtipoconceptotarifa);
            this.Controls.Add(this.cb_conceptoratifa);
            this.Controls.Add(this.cbEscritos_subtipodocumento);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "relaciona_subtipodoocumento_concepto_tarifa";
            this.Text = "Relaciona Escrito con tarifa a sugerir";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_relacionsubtipoconceptotarifa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbEscritos_subtipodocumento;
        private System.Windows.Forms.ComboBox cb_conceptoratifa;
        private System.Windows.Forms.DataGridView dgv_relacionsubtipoconceptotarifa;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn relacion_subtipodocumento_tarifaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_subtipodocumentoid;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_tarifa_concepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubTipoDocumentoDescrip;
        private System.Windows.Forms.DataGridViewTextBoxColumn concepto;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}