namespace Facturador
{
    partial class Conceptos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Conceptos));
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.combGrupos = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.comboEstatus = new System.Windows.Forms.ComboBox();
            this.conbobServicios = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.listvRelacioneconcept = new System.Windows.Forms.ListView();
            this.columnGrupodesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnEstatuscaso2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnConceptocargo2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1174, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Salir";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 21;
            this.button3.Text = "Menú";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // combGrupos
            // 
            this.combGrupos.FormattingEnabled = true;
            this.combGrupos.Location = new System.Drawing.Point(24, 121);
            this.combGrupos.Name = "combGrupos";
            this.combGrupos.Size = new System.Drawing.Size(236, 21);
            this.combGrupos.TabIndex = 23;
            this.combGrupos.SelectedIndexChanged += new System.EventHandler(this.combGrupos_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(19, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(141, 15);
            this.label6.TabIndex = 24;
            this.label6.Text = "Seleccione un Grupo";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(20, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(148, 15);
            this.label8.TabIndex = 25;
            this.label8.Text = "Seleccione un estatus";
            // 
            // comboEstatus
            // 
            this.comboEstatus.FormattingEnabled = true;
            this.comboEstatus.Location = new System.Drawing.Point(22, 193);
            this.comboEstatus.Name = "comboEstatus";
            this.comboEstatus.Size = new System.Drawing.Size(234, 21);
            this.comboEstatus.TabIndex = 26;
            // 
            // conbobServicios
            // 
            this.conbobServicios.FormattingEnabled = true;
            this.conbobServicios.Location = new System.Drawing.Point(22, 270);
            this.conbobServicios.Name = "conbobServicios";
            this.conbobServicios.Size = new System.Drawing.Size(908, 21);
            this.conbobServicios.TabIndex = 27;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(19, 243);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(202, 15);
            this.label9.TabIndex = 28;
            this.label9.Text = "Seleccione concepto de Cargo";
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(691, 203);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(130, 24);
            this.button4.TabIndex = 29;
            this.button4.Text = "Relacionar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(889, 131);
            this.button5.Name = "button5";
            this.button5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button5.Size = new System.Drawing.Size(130, 24);
            this.button5.TabIndex = 30;
            this.button5.Text = "Modificar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // listvRelacioneconcept
            // 
            this.listvRelacioneconcept.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listvRelacioneconcept.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnGrupodesc,
            this.columnEstatuscaso2,
            this.columnConceptocargo2});
            this.listvRelacioneconcept.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listvRelacioneconcept.Location = new System.Drawing.Point(12, 367);
            this.listvRelacioneconcept.Name = "listvRelacioneconcept";
            this.listvRelacioneconcept.Size = new System.Drawing.Size(1142, 213);
            this.listvRelacioneconcept.TabIndex = 31;
            this.listvRelacioneconcept.UseCompatibleStateImageBehavior = false;
            this.listvRelacioneconcept.View = System.Windows.Forms.View.Details;
            this.listvRelacioneconcept.SelectedIndexChanged += new System.EventHandler(this.listvRelacioneconcept_SelectedIndexChanged);
            // 
            // columnGrupodesc
            // 
            this.columnGrupodesc.Text = "Grupo ";
            this.columnGrupodesc.Width = 143;
            // 
            // columnEstatuscaso2
            // 
            this.columnEstatuscaso2.Text = "Estatus caso";
            this.columnEstatuscaso2.Width = 291;
            // 
            // columnConceptocargo2
            // 
            this.columnConceptocargo2.Text = "Concepto Cargo";
            this.columnConceptocargo2.Width = 689;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(21, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(800, 16);
            this.label10.TabIndex = 32;
            this.label10.Text = "Para sugerir los conceptos de cargo  al crear una factura es necesario relacionar" +
    " el estatus de los casos con los conceptos de cobro.";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(691, 121);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 45);
            this.button1.TabIndex = 33;
            this.button1.Text = "Guardar \r\nCambios";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Conceptos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 592);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listvRelacioneconcept);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.conbobServicios);
            this.Controls.Add(this.comboEstatus);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.combGrupos);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Conceptos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tarifas por concepto de cargo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox combGrupos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboEstatus;
        private System.Windows.Forms.ComboBox conbobServicios;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ListView listvRelacioneconcept;
        private System.Windows.Forms.ColumnHeader columnGrupodesc;
        private System.Windows.Forms.ColumnHeader columnEstatuscaso2;
        private System.Windows.Forms.ColumnHeader columnConceptocargo2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button1;
    }
}