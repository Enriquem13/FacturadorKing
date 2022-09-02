
namespace Facturador.serviciocorreoking
{
    partial class servcorreo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(servcorreo));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbCorreodepruebas = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_Aoficial_patentes = new System.Windows.Forms.CheckBox();
            this.cb_sol_patentes = new System.Windows.Forms.CheckBox();
            this.cb_Aoficial_marcas = new System.Windows.Forms.CheckBox();
            this.cb_sol_marcas = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(538, 377);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.tbCorreodepruebas);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(530, 351);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Configuración del servicio";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(437, 325);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "05 / 07 / 2022";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 470);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "                 ";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(425, 15);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 45);
            this.button2.TabIndex = 7;
            this.button2.Text = "Enviar prueba de correo";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Correo de prueba:";
            // 
            // tbCorreodepruebas
            // 
            this.tbCorreodepruebas.Location = new System.Drawing.Point(16, 40);
            this.tbCorreodepruebas.Name = "tbCorreodepruebas";
            this.tbCorreodepruebas.Size = new System.Drawing.Size(354, 20);
            this.tbCorreodepruebas.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightGray;
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cb_Aoficial_patentes);
            this.groupBox1.Controls.Add(this.cb_sol_patentes);
            this.groupBox1.Controls.Add(this.cb_Aoficial_marcas);
            this.groupBox1.Controls.Add(this.cb_sol_marcas);
            this.groupBox1.Location = new System.Drawing.Point(16, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(498, 223);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Servicios disponibles:";
            // 
            // cb_Aoficial_patentes
            // 
            this.cb_Aoficial_patentes.AutoSize = true;
            this.cb_Aoficial_patentes.Checked = true;
            this.cb_Aoficial_patentes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Aoficial_patentes.Location = new System.Drawing.Point(286, 116);
            this.cb_Aoficial_patentes.Name = "cb_Aoficial_patentes";
            this.cb_Aoficial_patentes.Size = new System.Drawing.Size(171, 17);
            this.cb_Aoficial_patentes.TabIndex = 3;
            this.cb_Aoficial_patentes.Text = "Plazos Patente (Acción Oficial)";
            this.cb_Aoficial_patentes.UseVisualStyleBackColor = true;
            // 
            // cb_sol_patentes
            // 
            this.cb_sol_patentes.AutoSize = true;
            this.cb_sol_patentes.Checked = true;
            this.cb_sol_patentes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_sol_patentes.Location = new System.Drawing.Point(286, 49);
            this.cb_sol_patentes.Name = "cb_sol_patentes";
            this.cb_sol_patentes.Size = new System.Drawing.Size(132, 17);
            this.cb_sol_patentes.TabIndex = 2;
            this.cb_sol_patentes.Text = "Solicitudes de Patente";
            this.cb_sol_patentes.UseVisualStyleBackColor = true;
            // 
            // cb_Aoficial_marcas
            // 
            this.cb_Aoficial_marcas.AutoSize = true;
            this.cb_Aoficial_marcas.Checked = true;
            this.cb_Aoficial_marcas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Aoficial_marcas.Location = new System.Drawing.Point(38, 116);
            this.cb_Aoficial_marcas.Name = "cb_Aoficial_marcas";
            this.cb_Aoficial_marcas.Size = new System.Drawing.Size(164, 17);
            this.cb_Aoficial_marcas.TabIndex = 1;
            this.cb_Aoficial_marcas.Text = "Plazos Marca (Acción Oficial)";
            this.cb_Aoficial_marcas.UseVisualStyleBackColor = true;
            // 
            // cb_sol_marcas
            // 
            this.cb_sol_marcas.AutoSize = true;
            this.cb_sol_marcas.Checked = true;
            this.cb_sol_marcas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_sol_marcas.Location = new System.Drawing.Point(38, 49);
            this.cb_sol_marcas.Name = "cb_sol_marcas";
            this.cb_sol_marcas.Size = new System.Drawing.Size(125, 17);
            this.cb_sol_marcas.TabIndex = 0;
            this.cb_sol_marcas.Text = "Solicitudes de Marca";
            this.cb_sol_marcas.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(309, 178);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Enviar seleccionados";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // servcorreo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 397);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "servcorreo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Correo de plazos";
            this.Load += new System.EventHandler(this.servcorreo_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbCorreodepruebas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cb_Aoficial_patentes;
        private System.Windows.Forms.CheckBox cb_sol_patentes;
        private System.Windows.Forms.CheckBox cb_Aoficial_marcas;
        private System.Windows.Forms.CheckBox cb_sol_marcas;
        private System.Windows.Forms.Button button1;
    }
}