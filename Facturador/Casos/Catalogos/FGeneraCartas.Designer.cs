namespace Facturador
{
    partial class FGeneraCartas
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
            this.BT_menugeneracartas = new System.Windows.Forms.Button();
            this.BT_salirgeneracartas = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_Casoidgenerecartas = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CB_tiposolicitudgeneracartas = new System.Windows.Forms.ComboBox();
            this.CB_Cartageneracartas = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BT_menugeneracartas
            // 
            this.BT_menugeneracartas.Location = new System.Drawing.Point(12, 12);
            this.BT_menugeneracartas.Name = "BT_menugeneracartas";
            this.BT_menugeneracartas.Size = new System.Drawing.Size(75, 23);
            this.BT_menugeneracartas.TabIndex = 1;
            this.BT_menugeneracartas.Text = "Menú";
            this.BT_menugeneracartas.UseVisualStyleBackColor = true;
            this.BT_menugeneracartas.Click += new System.EventHandler(this.BT_menugeneracartas_Click);
            // 
            // BT_salirgeneracartas
            // 
            this.BT_salirgeneracartas.Location = new System.Drawing.Point(288, 12);
            this.BT_salirgeneracartas.Name = "BT_salirgeneracartas";
            this.BT_salirgeneracartas.Size = new System.Drawing.Size(75, 23);
            this.BT_salirgeneracartas.TabIndex = 6;
            this.BT_salirgeneracartas.Text = "Salir";
            this.BT_salirgeneracartas.UseVisualStyleBackColor = true;
            this.BT_salirgeneracartas.Click += new System.EventHandler(this.BT_salirgeneracartas_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(123, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Genera Cartas";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(57, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 51;
            this.label4.Text = "Caso Id";
            // 
            // TB_Casoidgenerecartas
            // 
            this.TB_Casoidgenerecartas.Location = new System.Drawing.Point(113, 72);
            this.TB_Casoidgenerecartas.Name = "TB_Casoidgenerecartas";
            this.TB_Casoidgenerecartas.Size = new System.Drawing.Size(250, 20);
            this.TB_Casoidgenerecartas.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Tipo Solicitud";
            // 
            // CB_tiposolicitudgeneracartas
            // 
            this.CB_tiposolicitudgeneracartas.FormattingEnabled = true;
            this.CB_tiposolicitudgeneracartas.Location = new System.Drawing.Point(113, 130);
            this.CB_tiposolicitudgeneracartas.Name = "CB_tiposolicitudgeneracartas";
            this.CB_tiposolicitudgeneracartas.Size = new System.Drawing.Size(250, 21);
            this.CB_tiposolicitudgeneracartas.TabIndex = 3;
            this.CB_tiposolicitudgeneracartas.Text = "Seleccione";
            // 
            // CB_Cartageneracartas
            // 
            this.CB_Cartageneracartas.FormattingEnabled = true;
            this.CB_Cartageneracartas.Location = new System.Drawing.Point(113, 194);
            this.CB_Cartageneracartas.Name = "CB_Cartageneracartas";
            this.CB_Cartageneracartas.Size = new System.Drawing.Size(250, 21);
            this.CB_Cartageneracartas.TabIndex = 4;
            this.CB_Cartageneracartas.Text = "Seleccione";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(70, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 56;
            this.label3.Text = "Carta";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(165, 252);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Generar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FGeneraCartas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 335);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CB_Cartageneracartas);
            this.Controls.Add(this.CB_tiposolicitudgeneracartas);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TB_Casoidgenerecartas);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BT_salirgeneracartas);
            this.Controls.Add(this.BT_menugeneracartas);
            this.Name = "FGeneraCartas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FGeneraCartas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BT_menugeneracartas;
        private System.Windows.Forms.Button BT_salirgeneracartas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TB_Casoidgenerecartas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CB_tiposolicitudgeneracartas;
        private System.Windows.Forms.ComboBox CB_Cartageneracartas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
    }
}