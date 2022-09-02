namespace Facturador
{
    partial class estatuspordocumento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(estatuspordocumento));
            this.cb_subtipodocumentoid = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_estatuscasodocument = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_estatusid = new System.Windows.Forms.ComboBox();
            this.btn_elimiarreg = new System.Windows.Forms.Button();
            this.btn_modificar = new System.Windows.Forms.Button();
            this.btn_agregarreñlacion = new System.Windows.Forms.Button();
            this.btn_salir_estatusdoc = new System.Windows.Forms.Button();
            this.id_estatusdocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_estatuscaso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtipodocumentoid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Documentodescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estatusdescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grupodescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_grupo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_tipodocumento = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_estatuscasodocument)).BeginInit();
            this.SuspendLayout();
            // 
            // cb_subtipodocumentoid
            // 
            this.cb_subtipodocumentoid.FormattingEnabled = true;
            this.cb_subtipodocumentoid.Location = new System.Drawing.Point(34, 96);
            this.cb_subtipodocumentoid.Name = "cb_subtipodocumentoid";
            this.cb_subtipodocumentoid.Size = new System.Drawing.Size(633, 21);
            this.cb_subtipodocumentoid.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Documentos:";
            // 
            // dgv_estatuscasodocument
            // 
            this.dgv_estatuscasodocument.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_estatuscasodocument.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_estatusdocumento,
            this.id_estatuscaso,
            this.subtipodocumentoid,
            this.Documentodescrip,
            this.estatusdescrip,
            this.grupodescrip});
            this.dgv_estatuscasodocument.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv_estatuscasodocument.Location = new System.Drawing.Point(0, 251);
            this.dgv_estatuscasodocument.Name = "dgv_estatuscasodocument";
            this.dgv_estatuscasodocument.Size = new System.Drawing.Size(959, 388);
            this.dgv_estatuscasodocument.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(361, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Estatus:";
            // 
            // cb_estatusid
            // 
            this.cb_estatusid.FormattingEnabled = true;
            this.cb_estatusid.Location = new System.Drawing.Point(364, 149);
            this.cb_estatusid.Name = "cb_estatusid";
            this.cb_estatusid.Size = new System.Drawing.Size(303, 21);
            this.cb_estatusid.TabIndex = 3;
            // 
            // btn_elimiarreg
            // 
            this.btn_elimiarreg.Location = new System.Drawing.Point(862, 144);
            this.btn_elimiarreg.Name = "btn_elimiarreg";
            this.btn_elimiarreg.Size = new System.Drawing.Size(75, 23);
            this.btn_elimiarreg.TabIndex = 5;
            this.btn_elimiarreg.Text = "Eliminar";
            this.btn_elimiarreg.UseVisualStyleBackColor = true;
            // 
            // btn_modificar
            // 
            this.btn_modificar.Location = new System.Drawing.Point(781, 144);
            this.btn_modificar.Name = "btn_modificar";
            this.btn_modificar.Size = new System.Drawing.Size(75, 23);
            this.btn_modificar.TabIndex = 6;
            this.btn_modificar.Text = "Modificar";
            this.btn_modificar.UseVisualStyleBackColor = true;
            this.btn_modificar.Click += new System.EventHandler(this.btn_modificar_Click);
            // 
            // btn_agregarreñlacion
            // 
            this.btn_agregarreñlacion.Location = new System.Drawing.Point(592, 186);
            this.btn_agregarreñlacion.Name = "btn_agregarreñlacion";
            this.btn_agregarreñlacion.Size = new System.Drawing.Size(75, 23);
            this.btn_agregarreñlacion.TabIndex = 7;
            this.btn_agregarreñlacion.Text = "Agregar";
            this.btn_agregarreñlacion.UseVisualStyleBackColor = true;
            this.btn_agregarreñlacion.Click += new System.EventHandler(this.btn_agregarreñlacion_Click);
            // 
            // btn_salir_estatusdoc
            // 
            this.btn_salir_estatusdoc.Location = new System.Drawing.Point(872, 12);
            this.btn_salir_estatusdoc.Name = "btn_salir_estatusdoc";
            this.btn_salir_estatusdoc.Size = new System.Drawing.Size(75, 23);
            this.btn_salir_estatusdoc.TabIndex = 8;
            this.btn_salir_estatusdoc.Text = "Salir";
            this.btn_salir_estatusdoc.UseVisualStyleBackColor = true;
            this.btn_salir_estatusdoc.Click += new System.EventHandler(this.btn_salir_estatusdoc_Click);
            // 
            // id_estatusdocumento
            // 
            this.id_estatusdocumento.HeaderText = "id_estatusdocumento";
            this.id_estatusdocumento.Name = "id_estatusdocumento";
            this.id_estatusdocumento.Visible = false;
            // 
            // id_estatuscaso
            // 
            this.id_estatuscaso.HeaderText = "estatuscasoid";
            this.id_estatuscaso.Name = "id_estatuscaso";
            this.id_estatuscaso.Visible = false;
            // 
            // subtipodocumentoid
            // 
            this.subtipodocumentoid.HeaderText = "subtipodocumentoid";
            this.subtipodocumentoid.Name = "subtipodocumentoid";
            this.subtipodocumentoid.Visible = false;
            // 
            // Documentodescrip
            // 
            this.Documentodescrip.HeaderText = "Documento";
            this.Documentodescrip.Name = "Documentodescrip";
            this.Documentodescrip.Width = 350;
            // 
            // estatusdescrip
            // 
            this.estatusdescrip.HeaderText = "Estatus Caso";
            this.estatusdescrip.Name = "estatusdescrip";
            this.estatusdescrip.Width = 350;
            // 
            // grupodescrip
            // 
            this.grupodescrip.HeaderText = "Grupo";
            this.grupodescrip.Name = "grupodescrip";
            this.grupodescrip.Width = 200;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Grupo:";
            // 
            // cb_grupo
            // 
            this.cb_grupo.FormattingEnabled = true;
            this.cb_grupo.Location = new System.Drawing.Point(37, 149);
            this.cb_grupo.Name = "cb_grupo";
            this.cb_grupo.Size = new System.Drawing.Size(192, 21);
            this.cb_grupo.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Tipo Documento";
            // 
            // cb_tipodocumento
            // 
            this.cb_tipodocumento.FormattingEnabled = true;
            this.cb_tipodocumento.Location = new System.Drawing.Point(34, 36);
            this.cb_tipodocumento.Name = "cb_tipodocumento";
            this.cb_tipodocumento.Size = new System.Drawing.Size(195, 21);
            this.cb_tipodocumento.TabIndex = 12;
            this.cb_tipodocumento.SelectedIndexChanged += new System.EventHandler(this.cb_tipodocumento_SelectedIndexChanged);
            // 
            // estatuspordocumento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 639);
            this.ControlBox = false;
            this.Controls.Add(this.cb_tipodocumento);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cb_grupo);
            this.Controls.Add(this.btn_salir_estatusdoc);
            this.Controls.Add(this.btn_agregarreñlacion);
            this.Controls.Add(this.btn_modificar);
            this.Controls.Add(this.btn_elimiarreg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cb_estatusid);
            this.Controls.Add(this.dgv_estatuscasodocument);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_subtipodocumentoid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "estatuspordocumento";
            this.Text = "Documentos Estatus";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_estatuscasodocument)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_subtipodocumentoid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_estatuscasodocument;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_estatusid;
        private System.Windows.Forms.Button btn_elimiarreg;
        private System.Windows.Forms.Button btn_modificar;
        private System.Windows.Forms.Button btn_agregarreñlacion;
        private System.Windows.Forms.Button btn_salir_estatusdoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_estatusdocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_estatuscaso;
        private System.Windows.Forms.DataGridViewTextBoxColumn subtipodocumentoid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Documentodescrip;
        private System.Windows.Forms.DataGridViewTextBoxColumn estatusdescrip;
        private System.Windows.Forms.DataGridViewTextBoxColumn grupodescrip;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_grupo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_tipodocumento;
    }
}