namespace Facturador.Facturador
{
    partial class Actualizartarifas_excel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Actualizartarifas_excel));
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dataGridView_relaciontarifas = new System.Windows.Forms.DataGridView();
            this.id_relaciotarifa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titulo_excel_pesos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titulo_excel_doares = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titulo_excel_euros = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbGrupocaso = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBoxInteresado = new System.Windows.Forms.ComboBox();
            this.comboBoxClientes = new System.Windows.Forms.ComboBox();
            this.rb_caso = new System.Windows.Forms.RadioButton();
            this.rb_titular = new System.Windows.Forms.RadioButton();
            this.rb_cliente = new System.Windows.Forms.RadioButton();
            this.tbCasoid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.tb_euros = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_dolares = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_pesos = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_cargarconceptos = new System.Windows.Forms.Button();
            this.btneliminar = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_relaciontarifas)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(206, 300);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 27);
            this.button1.TabIndex = 0;
            this.button1.Text = "Cargar tarifas";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // dataGridView_relaciontarifas
            // 
            this.dataGridView_relaciontarifas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_relaciontarifas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_relaciotarifa,
            this.id_item,
            this.tipo,
            this.titulo_excel_pesos,
            this.titulo_excel_doares,
            this.titulo_excel_euros});
            this.dataGridView_relaciontarifas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView_relaciontarifas.Location = new System.Drawing.Point(0, 337);
            this.dataGridView_relaciontarifas.Name = "dataGridView_relaciontarifas";
            this.dataGridView_relaciontarifas.Size = new System.Drawing.Size(1251, 261);
            this.dataGridView_relaciontarifas.TabIndex = 1;
            // 
            // id_relaciotarifa
            // 
            this.id_relaciotarifa.HeaderText = "id_relaciontarifa";
            this.id_relaciotarifa.Name = "id_relaciotarifa";
            this.id_relaciotarifa.Visible = false;
            // 
            // id_item
            // 
            this.id_item.HeaderText = "Cliente / Interesado / Caso";
            this.id_item.Name = "id_item";
            this.id_item.Width = 300;
            // 
            // tipo
            // 
            this.tipo.HeaderText = "Tipo";
            this.tipo.Name = "tipo";
            // 
            // titulo_excel_pesos
            // 
            this.titulo_excel_pesos.HeaderText = "Titulo de Excel Pesos";
            this.titulo_excel_pesos.Name = "titulo_excel_pesos";
            this.titulo_excel_pesos.Width = 200;
            // 
            // titulo_excel_doares
            // 
            this.titulo_excel_doares.HeaderText = "Titulo de Excel Dolares";
            this.titulo_excel_doares.Name = "titulo_excel_doares";
            this.titulo_excel_doares.Width = 200;
            // 
            // titulo_excel_euros
            // 
            this.titulo_excel_euros.HeaderText = "Titulo de Excel Euros";
            this.titulo_excel_euros.Name = "titulo_excel_euros";
            this.titulo_excel_euros.Width = 200;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(485, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(302, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Relación y actualización de tarifas King";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbGrupocaso);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.comboBoxInteresado);
            this.groupBox1.Controls.Add(this.comboBoxClientes);
            this.groupBox1.Controls.Add(this.rb_caso);
            this.groupBox1.Controls.Add(this.rb_titular);
            this.groupBox1.Controls.Add(this.rb_cliente);
            this.groupBox1.Controls.Add(this.tbCasoid);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(27, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(557, 230);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleccióne una opción:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(230, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Grupo";
            // 
            // cbGrupocaso
            // 
            this.cbGrupocaso.FormattingEnabled = true;
            this.cbGrupocaso.Location = new System.Drawing.Point(230, 162);
            this.cbGrupocaso.Name = "cbGrupocaso";
            this.cbGrupocaso.Size = new System.Drawing.Size(205, 21);
            this.cbGrupocaso.TabIndex = 13;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(461, 160);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Buscar caso";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // comboBoxInteresado
            // 
            this.comboBoxInteresado.FormattingEnabled = true;
            this.comboBoxInteresado.Location = new System.Drawing.Point(57, 106);
            this.comboBoxInteresado.Name = "comboBoxInteresado";
            this.comboBoxInteresado.Size = new System.Drawing.Size(476, 21);
            this.comboBoxInteresado.TabIndex = 11;
            // 
            // comboBoxClientes
            // 
            this.comboBoxClientes.FormattingEnabled = true;
            this.comboBoxClientes.Location = new System.Drawing.Point(57, 49);
            this.comboBoxClientes.Name = "comboBoxClientes";
            this.comboBoxClientes.Size = new System.Drawing.Size(476, 21);
            this.comboBoxClientes.TabIndex = 10;
            // 
            // rb_caso
            // 
            this.rb_caso.AutoSize = true;
            this.rb_caso.Location = new System.Drawing.Point(21, 165);
            this.rb_caso.Name = "rb_caso";
            this.rb_caso.Size = new System.Drawing.Size(14, 13);
            this.rb_caso.TabIndex = 9;
            this.rb_caso.TabStop = true;
            this.rb_caso.UseVisualStyleBackColor = true;
            // 
            // rb_titular
            // 
            this.rb_titular.AutoSize = true;
            this.rb_titular.Location = new System.Drawing.Point(23, 109);
            this.rb_titular.Name = "rb_titular";
            this.rb_titular.Size = new System.Drawing.Size(14, 13);
            this.rb_titular.TabIndex = 8;
            this.rb_titular.TabStop = true;
            this.rb_titular.UseVisualStyleBackColor = true;
            // 
            // rb_cliente
            // 
            this.rb_cliente.AutoSize = true;
            this.rb_cliente.Location = new System.Drawing.Point(23, 52);
            this.rb_cliente.Name = "rb_cliente";
            this.rb_cliente.Size = new System.Drawing.Size(14, 13);
            this.rb_cliente.TabIndex = 7;
            this.rb_cliente.TabStop = true;
            this.rb_cliente.UseVisualStyleBackColor = true;
            // 
            // tbCasoid
            // 
            this.tbCasoid.Enabled = false;
            this.tbCasoid.Location = new System.Drawing.Point(55, 162);
            this.tbCasoid.Name = "tbCasoid";
            this.tbCasoid.Size = new System.Drawing.Size(145, 20);
            this.tbCasoid.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(55, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Casoid";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Titular";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Cliente";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.tb_euros);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tb_dolares);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tb_pesos);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(590, 58);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(595, 230);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Agregue los títulos de excel de cada moneda:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(514, 201);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Agregar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tb_euros
            // 
            this.tb_euros.Location = new System.Drawing.Point(32, 162);
            this.tb_euros.Name = "tb_euros";
            this.tb_euros.Size = new System.Drawing.Size(532, 20);
            this.tb_euros.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(29, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Título excel Euros €";
            // 
            // tb_dolares
            // 
            this.tb_dolares.Location = new System.Drawing.Point(32, 109);
            this.tb_dolares.Name = "tb_dolares";
            this.tb_dolares.Size = new System.Drawing.Size(532, 20);
            this.tb_dolares.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Título excel Dolares $US";
            // 
            // tb_pesos
            // 
            this.tb_pesos.Location = new System.Drawing.Point(32, 61);
            this.tb_pesos.Name = "tb_pesos";
            this.tb_pesos.Size = new System.Drawing.Size(532, 20);
            this.tb_pesos.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Título excel Pesos $";
            // 
            // btn_cargarconceptos
            // 
            this.btn_cargarconceptos.Location = new System.Drawing.Point(27, 304);
            this.btn_cargarconceptos.Name = "btn_cargarconceptos";
            this.btn_cargarconceptos.Size = new System.Drawing.Size(106, 23);
            this.btn_cargarconceptos.TabIndex = 6;
            this.btn_cargarconceptos.Text = "Cargar Conceptos";
            this.btn_cargarconceptos.UseVisualStyleBackColor = true;
            this.btn_cargarconceptos.Click += new System.EventHandler(this.btn_cargarconceptos_Click);
            // 
            // btneliminar
            // 
            this.btneliminar.Location = new System.Drawing.Point(1048, 308);
            this.btneliminar.Name = "btneliminar";
            this.btneliminar.Size = new System.Drawing.Size(137, 23);
            this.btneliminar.TabIndex = 7;
            this.btneliminar.Text = "Eliminar seleccionados";
            this.btneliminar.UseVisualStyleBackColor = true;
            this.btneliminar.Click += new System.EventHandler(this.btneliminar_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(233, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(351, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "Para cargar la tarifa Base se debe seleccionar el primer cliente que aparece en l" +
    "a lista.";
            // 
            // Actualizartarifas_excel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 598);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btneliminar);
            this.Controls.Add(this.btn_cargarconceptos);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_relaciontarifas);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Actualizartarifas_excel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Relación y actualización de tarifas";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_relaciontarifas)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dataGridView_relaciontarifas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbCasoid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rb_caso;
        private System.Windows.Forms.RadioButton rb_titular;
        private System.Windows.Forms.RadioButton rb_cliente;
        private System.Windows.Forms.ComboBox comboBoxInteresado;
        private System.Windows.Forms.ComboBox comboBoxClientes;
        private System.Windows.Forms.TextBox tb_euros;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_dolares;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_pesos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btn_cargarconceptos;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbGrupocaso;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_relaciotarifa;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_item;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn titulo_excel_pesos;
        private System.Windows.Forms.DataGridViewTextBoxColumn titulo_excel_doares;
        private System.Windows.Forms.DataGridViewTextBoxColumn titulo_excel_euros;
        private System.Windows.Forms.Button btneliminar;
        private System.Windows.Forms.Label label9;
    }
}