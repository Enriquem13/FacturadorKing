
namespace Facturador.plazos_forms
{
    partial class subtipodocplazos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(subtipodocplazos));
            this.cbSubtipodocumentoid = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbTipodocumento = new System.Windows.Forms.ComboBox();
            this.btnModificar = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tbTipoplazoid = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvTioplazos = new System.Windows.Forms.DataGridView();
            this.plazos_de_oficioid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo_documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idsubtipodocumentoid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Documentodescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grupoplazodescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipoplzoid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo_Plazo_descrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Meses_plazo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dias_plazo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avisoid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grupocaso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tiposolicitudid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tiposolicituddescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtiposolicitudid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subtiposolicituddescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.casodisenoclasific = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diasprorrogable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mesesprorrogable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbgrupo_plazo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbTipo_plazo = new System.Windows.Forms.ComboBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.tbDias = new System.Windows.Forms.TextBox();
            this.tbMeses = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chAviso = new System.Windows.Forms.CheckBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cbTiposolicitud = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbSubtiposolicitud = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbCalsodiseno = new System.Windows.Forms.ComboBox();
            this.cbGrupoCasos = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbMesesprorroga = new System.Windows.Forms.TextBox();
            this.tbDiasprorroga = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTioplazos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbSubtipodocumentoid
            // 
            this.cbSubtipodocumentoid.FormattingEnabled = true;
            this.cbSubtipodocumentoid.Location = new System.Drawing.Point(24, 131);
            this.cbSubtipodocumentoid.Name = "cbSubtipodocumentoid";
            this.cbSubtipodocumentoid.Size = new System.Drawing.Size(648, 21);
            this.cbSubtipodocumentoid.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 16);
            this.label4.TabIndex = 28;
            this.label4.Text = "Subtipodocumento";
            // 
            // cbTipodocumento
            // 
            this.cbTipodocumento.FormattingEnabled = true;
            this.cbTipodocumento.Location = new System.Drawing.Point(24, 80);
            this.cbTipodocumento.Name = "cbTipodocumento";
            this.cbTipodocumento.Size = new System.Drawing.Size(144, 21);
            this.cbTipodocumento.TabIndex = 26;
            this.cbTipodocumento.SelectedIndexChanged += new System.EventHandler(this.cbTipodocumento_SelectedIndexChanged);
            // 
            // btnModificar
            // 
            this.btnModificar.Enabled = false;
            this.btnModificar.Location = new System.Drawing.Point(1329, 59);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(75, 23);
            this.btnModificar.TabIndex = 25;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1329, 163);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 35);
            this.button2.TabIndex = 24;
            this.button2.Text = "Eliminar Seleccionado";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbTipoplazoid
            // 
            this.tbTipoplazoid.Location = new System.Drawing.Point(45, 22);
            this.tbTipoplazoid.Name = "tbTipoplazoid";
            this.tbTipoplazoid.ReadOnly = true;
            this.tbTipoplazoid.Size = new System.Drawing.Size(47, 20);
            this.tbTipoplazoid.TabIndex = 22;
            this.tbTipoplazoid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Tipo documento";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(1329, 27);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 19;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "ID";
            // 
            // dgvTioplazos
            // 
            this.dgvTioplazos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTioplazos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.plazos_de_oficioid,
            this.Tipo_documento,
            this.idsubtipodocumentoid,
            this.Documentodescrip,
            this.grupoplazodescrip,
            this.tipoplzoid,
            this.Tipo_Plazo_descrip,
            this.Meses_plazo,
            this.dias_plazo,
            this.avisoid,
            this.grupocaso,
            this.Tiposolicitudid,
            this.Tiposolicituddescrip,
            this.subtiposolicitudid,
            this.Subtiposolicituddescrip,
            this.casodisenoclasific,
            this.diasprorrogable,
            this.mesesprorrogable});
            this.dgvTioplazos.Location = new System.Drawing.Point(12, 357);
            this.dgvTioplazos.Name = "dgvTioplazos";
            this.dgvTioplazos.Size = new System.Drawing.Size(1757, 380);
            this.dgvTioplazos.TabIndex = 16;
            this.dgvTioplazos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTioplazos_CellContentClick);
            this.dgvTioplazos.DoubleClick += new System.EventHandler(this.dgvTioplazos_DoubleClick);
            // 
            // plazos_de_oficioid
            // 
            this.plazos_de_oficioid.HeaderText = "ID";
            this.plazos_de_oficioid.Name = "plazos_de_oficioid";
            this.plazos_de_oficioid.Width = 80;
            // 
            // Tipo_documento
            // 
            this.Tipo_documento.HeaderText = "Tipo documento";
            this.Tipo_documento.Name = "Tipo_documento";
            this.Tipo_documento.Width = 80;
            // 
            // idsubtipodocumentoid
            // 
            this.idsubtipodocumentoid.HeaderText = "Subtipodocumentoid";
            this.idsubtipodocumentoid.Name = "idsubtipodocumentoid";
            this.idsubtipodocumentoid.Width = 80;
            // 
            // Documentodescrip
            // 
            this.Documentodescrip.HeaderText = "Documento";
            this.Documentodescrip.Name = "Documentodescrip";
            this.Documentodescrip.Width = 350;
            // 
            // grupoplazodescrip
            // 
            this.grupoplazodescrip.HeaderText = "Grupo Plazo";
            this.grupoplazodescrip.Name = "grupoplazodescrip";
            this.grupoplazodescrip.Width = 80;
            // 
            // tipoplzoid
            // 
            this.tipoplzoid.HeaderText = "tipoplazoid";
            this.tipoplzoid.Name = "tipoplzoid";
            this.tipoplzoid.Width = 80;
            // 
            // Tipo_Plazo_descrip
            // 
            this.Tipo_Plazo_descrip.HeaderText = "Tipo Plazo";
            this.Tipo_Plazo_descrip.Name = "Tipo_Plazo_descrip";
            this.Tipo_Plazo_descrip.Width = 120;
            // 
            // Meses_plazo
            // 
            this.Meses_plazo.HeaderText = "Días plazo";
            this.Meses_plazo.Name = "Meses_plazo";
            this.Meses_plazo.Width = 50;
            // 
            // dias_plazo
            // 
            this.dias_plazo.HeaderText = "Meses plazo";
            this.dias_plazo.Name = "dias_plazo";
            this.dias_plazo.Width = 50;
            // 
            // avisoid
            // 
            this.avisoid.HeaderText = "Aviso";
            this.avisoid.Name = "avisoid";
            this.avisoid.Width = 50;
            // 
            // grupocaso
            // 
            this.grupocaso.HeaderText = "Grupo";
            this.grupocaso.Name = "grupocaso";
            // 
            // Tiposolicitudid
            // 
            this.Tiposolicitudid.HeaderText = "Tiposolicitudid";
            this.Tiposolicitudid.Name = "Tiposolicitudid";
            this.Tiposolicitudid.Visible = false;
            this.Tiposolicitudid.Width = 50;
            // 
            // Tiposolicituddescrip
            // 
            this.Tiposolicituddescrip.HeaderText = "Tiposolicitud";
            this.Tiposolicituddescrip.Name = "Tiposolicituddescrip";
            // 
            // subtiposolicitudid
            // 
            this.subtiposolicitudid.HeaderText = "Subtiposolicitudid";
            this.subtiposolicitudid.Name = "subtiposolicitudid";
            this.subtiposolicitudid.Visible = false;
            this.subtiposolicitudid.Width = 50;
            // 
            // Subtiposolicituddescrip
            // 
            this.Subtiposolicituddescrip.HeaderText = "Subtiposolicitud";
            this.Subtiposolicituddescrip.Name = "Subtiposolicituddescrip";
            // 
            // casodisenoclasific
            // 
            this.casodisenoclasific.HeaderText = "Caso diseño Calsificacion";
            this.casodisenoclasific.Name = "casodisenoclasific";
            // 
            // diasprorrogable
            // 
            this.diasprorrogable.HeaderText = "Días prorrogable";
            this.diasprorrogable.Name = "diasprorrogable";
            // 
            // mesesprorrogable
            // 
            this.mesesprorrogable.HeaderText = "Meses prorrogable";
            this.mesesprorrogable.Name = "mesesprorrogable";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Facturador.Properties.Resources._2191186;
            this.pictureBox1.Location = new System.Drawing.Point(678, 114);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(109, 84);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(824, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 38;
            this.label7.Text = "Grupo plazo";
            // 
            // cbgrupo_plazo
            // 
            this.cbgrupo_plazo.FormattingEnabled = true;
            this.cbgrupo_plazo.Location = new System.Drawing.Point(904, 71);
            this.cbgrupo_plazo.Name = "cbgrupo_plazo";
            this.cbgrupo_plazo.Size = new System.Drawing.Size(321, 21);
            this.cbgrupo_plazo.TabIndex = 37;
            this.cbgrupo_plazo.SelectedIndexChanged += new System.EventHandler(this.cbgrupo_plazo_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(816, 114);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 16);
            this.label8.TabIndex = 40;
            this.label8.Text = "Tipo plazo";
            // 
            // cbTipo_plazo
            // 
            this.cbTipo_plazo.Enabled = false;
            this.cbTipo_plazo.FormattingEnabled = true;
            this.cbTipo_plazo.Location = new System.Drawing.Point(904, 111);
            this.cbTipo_plazo.Name = "cbTipo_plazo";
            this.cbTipo_plazo.Size = new System.Drawing.Size(321, 21);
            this.cbTipo_plazo.TabIndex = 39;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(1329, 88);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 41;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbDias
            // 
            this.tbDias.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDias.Location = new System.Drawing.Point(903, 163);
            this.tbDias.Name = "tbDias";
            this.tbDias.Size = new System.Drawing.Size(67, 20);
            this.tbDias.TabIndex = 42;
            this.tbDias.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbMeses
            // 
            this.tbMeses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMeses.Location = new System.Drawing.Point(1062, 163);
            this.tbMeses.Name = "tbMeses";
            this.tbMeses.Size = new System.Drawing.Size(62, 20);
            this.tbMeses.TabIndex = 43;
            this.tbMeses.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(852, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Días";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1006, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 45;
            this.label5.Text = "Meses";
            // 
            // chAviso
            // 
            this.chAviso.AutoSize = true;
            this.chAviso.Location = new System.Drawing.Point(1173, 165);
            this.chAviso.Name = "chAviso";
            this.chAviso.Size = new System.Drawing.Size(52, 17);
            this.chAviso.TabIndex = 46;
            this.chAviso.Text = "Aviso";
            this.chAviso.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(45, 278);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(83, 13);
            this.label21.TabIndex = 50;
            this.label21.Text = "SubtipoSolicitud";
            // 
            // cbTiposolicitud
            // 
            this.cbTiposolicitud.FormattingEnabled = true;
            this.cbTiposolicitud.Location = new System.Drawing.Point(134, 231);
            this.cbTiposolicitud.Name = "cbTiposolicitud";
            this.cbTiposolicitud.Size = new System.Drawing.Size(249, 21);
            this.cbTiposolicitud.TabIndex = 48;
            this.cbTiposolicitud.SelectedIndexChanged += new System.EventHandler(this.cbTiposolicitud_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(21, 232);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 16);
            this.label6.TabIndex = 47;
            this.label6.Text = "Tipo de solicitud";
            // 
            // cbSubtiposolicitud
            // 
            this.cbSubtiposolicitud.FormattingEnabled = true;
            this.cbSubtiposolicitud.Location = new System.Drawing.Point(134, 278);
            this.cbSubtiposolicitud.Name = "cbSubtiposolicitud";
            this.cbSubtiposolicitud.Size = new System.Drawing.Size(249, 21);
            this.cbSubtiposolicitud.TabIndex = 49;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(58, 317);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 26);
            this.label9.TabIndex = 52;
            this.label9.Text = "Caso Diseño \r\nClasificación";
            // 
            // cbCalsodiseno
            // 
            this.cbCalsodiseno.FormattingEnabled = true;
            this.cbCalsodiseno.Location = new System.Drawing.Point(134, 322);
            this.cbCalsodiseno.Name = "cbCalsodiseno";
            this.cbCalsodiseno.Size = new System.Drawing.Size(406, 21);
            this.cbCalsodiseno.TabIndex = 51;
            // 
            // cbGrupoCasos
            // 
            this.cbGrupoCasos.FormattingEnabled = true;
            this.cbGrupoCasos.Location = new System.Drawing.Point(134, 192);
            this.cbGrupoCasos.Name = "cbGrupoCasos";
            this.cbGrupoCasos.Size = new System.Drawing.Size(249, 21);
            this.cbGrupoCasos.TabIndex = 54;
            this.cbGrupoCasos.SelectedIndexChanged += new System.EventHandler(this.cbGrupoCasos_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(82, 196);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 16);
            this.label10.TabIndex = 53;
            this.label10.Text = "Grupo";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.tbMesesprorroga);
            this.groupBox1.Controls.Add(this.tbDiasprorroga);
            this.groupBox1.Location = new System.Drawing.Point(827, 219);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 53);
            this.groupBox1.TabIndex = 55;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Prorrogable  ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(274, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 49;
            this.label11.Text = "Meses";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(120, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 13);
            this.label12.TabIndex = 48;
            this.label12.Text = "Días";
            // 
            // tbMesesprorroga
            // 
            this.tbMesesprorroga.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMesesprorroga.Location = new System.Drawing.Point(330, 19);
            this.tbMesesprorroga.Name = "tbMesesprorroga";
            this.tbMesesprorroga.Size = new System.Drawing.Size(62, 20);
            this.tbMesesprorroga.TabIndex = 47;
            this.tbMesesprorroga.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbDiasprorroga
            // 
            this.tbDiasprorroga.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDiasprorroga.Location = new System.Drawing.Point(171, 19);
            this.tbDiasprorroga.Name = "tbDiasprorroga";
            this.tbDiasprorroga.Size = new System.Drawing.Size(67, 20);
            this.tbDiasprorroga.TabIndex = 46;
            this.tbDiasprorroga.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // subtipodocplazos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1791, 749);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbGrupoCasos);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbCalsodiseno);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.cbTiposolicitud);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbSubtiposolicitud);
            this.Controls.Add(this.chAviso);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbMeses);
            this.Controls.Add(this.tbDias);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbgrupo_plazo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbTipo_plazo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cbSubtipodocumentoid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbTipodocumento);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbTipoplazoid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvTioplazos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "subtipodocplazos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Subtipo Documentos que generan plazos:";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTioplazos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSubtipodocumentoid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbTipodocumento;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbTipoplazoid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvTioplazos;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbgrupo_plazo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbTipo_plazo;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.TextBox tbDias;
        private System.Windows.Forms.TextBox tbMeses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chAviso;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cbTiposolicitud;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbSubtiposolicitud;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbCalsodiseno;
        private System.Windows.Forms.ComboBox cbGrupoCasos;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbMesesprorroga;
        private System.Windows.Forms.TextBox tbDiasprorroga;
        private System.Windows.Forms.DataGridViewTextBoxColumn plazos_de_oficioid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo_documento;
        private System.Windows.Forms.DataGridViewTextBoxColumn idsubtipodocumentoid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Documentodescrip;
        private System.Windows.Forms.DataGridViewTextBoxColumn grupoplazodescrip;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipoplzoid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo_Plazo_descrip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Meses_plazo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dias_plazo;
        private System.Windows.Forms.DataGridViewTextBoxColumn avisoid;
        private System.Windows.Forms.DataGridViewTextBoxColumn grupocaso;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tiposolicitudid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tiposolicituddescrip;
        private System.Windows.Forms.DataGridViewTextBoxColumn subtiposolicitudid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subtiposolicituddescrip;
        private System.Windows.Forms.DataGridViewTextBoxColumn casodisenoclasific;
        private System.Windows.Forms.DataGridViewTextBoxColumn diasprorrogable;
        private System.Windows.Forms.DataGridViewTextBoxColumn mesesprorrogable;
    }
}