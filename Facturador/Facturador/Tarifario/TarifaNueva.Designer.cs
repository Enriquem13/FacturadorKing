namespace Facturador.Facturador.Tarifario
{
    partial class TarifaNueva
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
            this.labelNombre = new System.Windows.Forms.Label();
            this.textNombre = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.labelSeparador = new System.Windows.Forms.Label();
            this.listBoxTarifas = new System.Windows.Forms.ListBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelNombre
            // 
            this.labelNombre.AutoSize = true;
            this.labelNombre.Location = new System.Drawing.Point(11, 15);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(73, 13);
            this.labelNombre.TabIndex = 0;
            this.labelNombre.Text = "Nombre tarifa:";
            // 
            // textNombre
            // 
            this.textNombre.Location = new System.Drawing.Point(90, 12);
            this.textNombre.Name = "textNombre";
            this.textNombre.Size = new System.Drawing.Size(158, 20);
            this.textNombre.TabIndex = 1;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(254, 10);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 2;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // labelSeparador
            // 
            this.labelSeparador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparador.Location = new System.Drawing.Point(12, 46);
            this.labelSeparador.Name = "labelSeparador";
            this.labelSeparador.Size = new System.Drawing.Size(360, 2);
            this.labelSeparador.TabIndex = 3;
            // 
            // listBoxTarifas
            // 
            this.listBoxTarifas.FormattingEnabled = true;
            this.listBoxTarifas.Location = new System.Drawing.Point(8, 63);
            this.listBoxTarifas.Name = "listBoxTarifas";
            this.listBoxTarifas.Size = new System.Drawing.Size(364, 186);
            this.listBoxTarifas.TabIndex = 4;
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(297, 255);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 5;
            this.btnSalir.Text = "&Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // TarifaNueva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 291);
            this.ControlBox = false;
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.listBoxTarifas);
            this.Controls.Add(this.labelSeparador);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.textNombre);
            this.Controls.Add(this.labelNombre);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 330);
            this.MinimumSize = new System.Drawing.Size(400, 330);
            this.Name = "TarifaNueva";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Agregar nueva tarifa con nombre";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.TextBox textNombre;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label labelSeparador;
        private System.Windows.Forms.ListBox listBoxTarifas;
        private System.Windows.Forms.Button btnSalir;
    }
}