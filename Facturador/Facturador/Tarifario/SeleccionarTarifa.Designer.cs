namespace Facturador.Facturador.Tarifario
{
    partial class SeleccionarTarifa
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
            this.groupBoxTipo = new System.Windows.Forms.GroupBox();
            this.radioCaso = new System.Windows.Forms.RadioButton();
            this.radioInteresado = new System.Windows.Forms.RadioButton();
            this.radioCliente = new System.Windows.Forms.RadioButton();
            this.groupDatosTipo = new System.Windows.Forms.GroupBox();
            this.panelCaso = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.textReferencia = new System.Windows.Forms.TextBox();
            this.labelReferencia = new System.Windows.Forms.Label();
            this.textTarifaCaso = new System.Windows.Forms.TextBox();
            this.labelTarifaCaso = new System.Windows.Forms.Label();
            this.textMonedaCaso = new System.Windows.Forms.TextBox();
            this.textNoCaso = new System.Windows.Forms.TextBox();
            this.labelNoCaso = new System.Windows.Forms.Label();
            this.textTituloCaso = new System.Windows.Forms.TextBox();
            this.labelTituloCaso = new System.Windows.Forms.Label();
            this.labelMonedaCaso = new System.Windows.Forms.Label();
            this.panelInteresado = new System.Windows.Forms.Panel();
            this.separadorInteresado = new System.Windows.Forms.Label();
            this.textNombreCorto = new System.Windows.Forms.TextBox();
            this.labelNombreCorto = new System.Windows.Forms.Label();
            this.textTarifaInteresado = new System.Windows.Forms.TextBox();
            this.labelTarifaInteresado = new System.Windows.Forms.Label();
            this.textMonedaInteresado = new System.Windows.Forms.TextBox();
            this.textNoInteresado = new System.Windows.Forms.TextBox();
            this.labelNoInteresado = new System.Windows.Forms.Label();
            this.textNombreInteresado = new System.Windows.Forms.TextBox();
            this.labelNombreInteresado = new System.Windows.Forms.Label();
            this.labelMonedaInteresado = new System.Windows.Forms.Label();
            this.panelCliente = new System.Windows.Forms.Panel();
            this.textTarifaCliente = new System.Windows.Forms.TextBox();
            this.labelTarifaCliente = new System.Windows.Forms.Label();
            this.textMonedaCliente = new System.Windows.Forms.TextBox();
            this.textNoCliente = new System.Windows.Forms.TextBox();
            this.labelNoCliente = new System.Windows.Forms.Label();
            this.textNombreCliente = new System.Windows.Forms.TextBox();
            this.labelNombreCliente = new System.Windows.Forms.Label();
            this.labelMonedaCliente = new System.Windows.Forms.Label();
            this.groupBusqueda = new System.Windows.Forms.GroupBox();
            this.comboGrupos = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.textNumero = new System.Windows.Forms.TextBox();
            this.labelNumero = new System.Windows.Forms.Label();
            this.textNombre = new System.Windows.Forms.TextBox();
            this.labelNombre = new System.Windows.Forms.Label();
            this.groupTarifa = new System.Windows.Forms.GroupBox();
            this.btnAsignarTarifa = new System.Windows.Forms.Button();
            this.listBoxTarifas = new System.Windows.Forms.ListBox();
            this.groupBoxTipo.SuspendLayout();
            this.groupDatosTipo.SuspendLayout();
            this.panelCaso.SuspendLayout();
            this.panelInteresado.SuspendLayout();
            this.panelCliente.SuspendLayout();
            this.groupBusqueda.SuspendLayout();
            this.groupTarifa.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxTipo
            // 
            this.groupBoxTipo.Controls.Add(this.radioCaso);
            this.groupBoxTipo.Controls.Add(this.radioInteresado);
            this.groupBoxTipo.Controls.Add(this.radioCliente);
            this.groupBoxTipo.Location = new System.Drawing.Point(12, 12);
            this.groupBoxTipo.Name = "groupBoxTipo";
            this.groupBoxTipo.Size = new System.Drawing.Size(239, 75);
            this.groupBoxTipo.TabIndex = 0;
            this.groupBoxTipo.TabStop = false;
            this.groupBoxTipo.Text = "Seleccione a quien se va a asignar la tarifa";
            // 
            // radioCaso
            // 
            this.radioCaso.AutoSize = true;
            this.radioCaso.Location = new System.Drawing.Point(168, 33);
            this.radioCaso.Name = "radioCaso";
            this.radioCaso.Size = new System.Drawing.Size(49, 17);
            this.radioCaso.TabIndex = 2;
            this.radioCaso.Text = "C&aso";
            this.radioCaso.UseVisualStyleBackColor = true;
            this.radioCaso.CheckedChanged += new System.EventHandler(this.radioTipo_CheckedChanged);
            // 
            // radioInteresado
            // 
            this.radioInteresado.AutoSize = true;
            this.radioInteresado.Location = new System.Drawing.Point(87, 33);
            this.radioInteresado.Name = "radioInteresado";
            this.radioInteresado.Size = new System.Drawing.Size(75, 17);
            this.radioInteresado.TabIndex = 1;
            this.radioInteresado.Text = "&Interesado";
            this.radioInteresado.UseVisualStyleBackColor = true;
            this.radioInteresado.CheckedChanged += new System.EventHandler(this.radioTipo_CheckedChanged);
            // 
            // radioCliente
            // 
            this.radioCliente.AutoSize = true;
            this.radioCliente.Checked = true;
            this.radioCliente.Location = new System.Drawing.Point(24, 33);
            this.radioCliente.Name = "radioCliente";
            this.radioCliente.Size = new System.Drawing.Size(57, 17);
            this.radioCliente.TabIndex = 0;
            this.radioCliente.TabStop = true;
            this.radioCliente.Text = "&Cliente";
            this.radioCliente.UseVisualStyleBackColor = true;
            this.radioCliente.CheckedChanged += new System.EventHandler(this.radioTipo_CheckedChanged);
            // 
            // groupDatosTipo
            // 
            this.groupDatosTipo.Controls.Add(this.panelCaso);
            this.groupDatosTipo.Controls.Add(this.panelInteresado);
            this.groupDatosTipo.Controls.Add(this.panelCliente);
            this.groupDatosTipo.Location = new System.Drawing.Point(12, 93);
            this.groupDatosTipo.Name = "groupDatosTipo";
            this.groupDatosTipo.Size = new System.Drawing.Size(444, 246);
            this.groupDatosTipo.TabIndex = 2;
            this.groupDatosTipo.TabStop = false;
            // 
            // panelCaso
            // 
            this.panelCaso.Controls.Add(this.label1);
            this.panelCaso.Controls.Add(this.textReferencia);
            this.panelCaso.Controls.Add(this.labelReferencia);
            this.panelCaso.Controls.Add(this.textTarifaCaso);
            this.panelCaso.Controls.Add(this.labelTarifaCaso);
            this.panelCaso.Controls.Add(this.textMonedaCaso);
            this.panelCaso.Controls.Add(this.textNoCaso);
            this.panelCaso.Controls.Add(this.labelNoCaso);
            this.panelCaso.Controls.Add(this.textTituloCaso);
            this.panelCaso.Controls.Add(this.labelTituloCaso);
            this.panelCaso.Controls.Add(this.labelMonedaCaso);
            this.panelCaso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCaso.Location = new System.Drawing.Point(3, 16);
            this.panelCaso.Name = "panelCaso";
            this.panelCaso.Size = new System.Drawing.Size(438, 227);
            this.panelCaso.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(15, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 2);
            this.label1.TabIndex = 10;
            // 
            // textReferencia
            // 
            this.textReferencia.Location = new System.Drawing.Point(114, 108);
            this.textReferencia.Name = "textReferencia";
            this.textReferencia.ReadOnly = true;
            this.textReferencia.Size = new System.Drawing.Size(180, 20);
            this.textReferencia.TabIndex = 9;
            // 
            // labelReferencia
            // 
            this.labelReferencia.AutoSize = true;
            this.labelReferencia.Location = new System.Drawing.Point(46, 112);
            this.labelReferencia.Name = "labelReferencia";
            this.labelReferencia.Size = new System.Drawing.Size(62, 13);
            this.labelReferencia.TabIndex = 8;
            this.labelReferencia.Text = "Referencia:";
            // 
            // textTarifaCaso
            // 
            this.textTarifaCaso.Location = new System.Drawing.Point(114, 175);
            this.textTarifaCaso.Name = "textTarifaCaso";
            this.textTarifaCaso.ReadOnly = true;
            this.textTarifaCaso.Size = new System.Drawing.Size(90, 20);
            this.textTarifaCaso.TabIndex = 7;
            // 
            // labelTarifaCaso
            // 
            this.labelTarifaCaso.AutoSize = true;
            this.labelTarifaCaso.Location = new System.Drawing.Point(71, 178);
            this.labelTarifaCaso.Name = "labelTarifaCaso";
            this.labelTarifaCaso.Size = new System.Drawing.Size(37, 13);
            this.labelTarifaCaso.TabIndex = 6;
            this.labelTarifaCaso.Text = "Tarifa:";
            // 
            // textMonedaCaso
            // 
            this.textMonedaCaso.Location = new System.Drawing.Point(114, 149);
            this.textMonedaCaso.Name = "textMonedaCaso";
            this.textMonedaCaso.ReadOnly = true;
            this.textMonedaCaso.Size = new System.Drawing.Size(90, 20);
            this.textMonedaCaso.TabIndex = 5;
            // 
            // textNoCaso
            // 
            this.textNoCaso.Location = new System.Drawing.Point(114, 8);
            this.textNoCaso.Name = "textNoCaso";
            this.textNoCaso.ReadOnly = true;
            this.textNoCaso.Size = new System.Drawing.Size(90, 20);
            this.textNoCaso.TabIndex = 1;
            // 
            // labelNoCaso
            // 
            this.labelNoCaso.AutoSize = true;
            this.labelNoCaso.Location = new System.Drawing.Point(19, 11);
            this.labelNoCaso.Name = "labelNoCaso";
            this.labelNoCaso.Size = new System.Drawing.Size(89, 13);
            this.labelNoCaso.TabIndex = 0;
            this.labelNoCaso.Text = "&Número de Caso:";
            this.labelNoCaso.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textTituloCaso
            // 
            this.textTituloCaso.Location = new System.Drawing.Point(114, 34);
            this.textTituloCaso.Multiline = true;
            this.textTituloCaso.Name = "textTituloCaso";
            this.textTituloCaso.ReadOnly = true;
            this.textTituloCaso.Size = new System.Drawing.Size(293, 68);
            this.textTituloCaso.TabIndex = 3;
            // 
            // labelTituloCaso
            // 
            this.labelTituloCaso.AutoSize = true;
            this.labelTituloCaso.Location = new System.Drawing.Point(26, 37);
            this.labelTituloCaso.Name = "labelTituloCaso";
            this.labelTituloCaso.Size = new System.Drawing.Size(82, 13);
            this.labelTituloCaso.TabIndex = 2;
            this.labelTituloCaso.Text = "&Título del Caso:";
            this.labelTituloCaso.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelMonedaCaso
            // 
            this.labelMonedaCaso.AutoSize = true;
            this.labelMonedaCaso.Location = new System.Drawing.Point(59, 152);
            this.labelMonedaCaso.Name = "labelMonedaCaso";
            this.labelMonedaCaso.Size = new System.Drawing.Size(49, 13);
            this.labelMonedaCaso.TabIndex = 4;
            this.labelMonedaCaso.Text = "Moneda:";
            // 
            // panelInteresado
            // 
            this.panelInteresado.Controls.Add(this.separadorInteresado);
            this.panelInteresado.Controls.Add(this.textNombreCorto);
            this.panelInteresado.Controls.Add(this.labelNombreCorto);
            this.panelInteresado.Controls.Add(this.textTarifaInteresado);
            this.panelInteresado.Controls.Add(this.labelTarifaInteresado);
            this.panelInteresado.Controls.Add(this.textMonedaInteresado);
            this.panelInteresado.Controls.Add(this.textNoInteresado);
            this.panelInteresado.Controls.Add(this.labelNoInteresado);
            this.panelInteresado.Controls.Add(this.textNombreInteresado);
            this.panelInteresado.Controls.Add(this.labelNombreInteresado);
            this.panelInteresado.Controls.Add(this.labelMonedaInteresado);
            this.panelInteresado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInteresado.Location = new System.Drawing.Point(3, 16);
            this.panelInteresado.Name = "panelInteresado";
            this.panelInteresado.Size = new System.Drawing.Size(438, 227);
            this.panelInteresado.TabIndex = 8;
            this.panelInteresado.Visible = false;
            // 
            // separadorInteresado
            // 
            this.separadorInteresado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separadorInteresado.Location = new System.Drawing.Point(15, 89);
            this.separadorInteresado.Name = "separadorInteresado";
            this.separadorInteresado.Size = new System.Drawing.Size(400, 2);
            this.separadorInteresado.TabIndex = 10;
            // 
            // textNombreCorto
            // 
            this.textNombreCorto.Location = new System.Drawing.Point(114, 59);
            this.textNombreCorto.Name = "textNombreCorto";
            this.textNombreCorto.ReadOnly = true;
            this.textNombreCorto.Size = new System.Drawing.Size(90, 20);
            this.textNombreCorto.TabIndex = 9;
            // 
            // labelNombreCorto
            // 
            this.labelNombreCorto.AutoSize = true;
            this.labelNombreCorto.Location = new System.Drawing.Point(34, 63);
            this.labelNombreCorto.Name = "labelNombreCorto";
            this.labelNombreCorto.Size = new System.Drawing.Size(74, 13);
            this.labelNombreCorto.TabIndex = 8;
            this.labelNombreCorto.Text = "Nombre corto:";
            // 
            // textTarifaInteresado
            // 
            this.textTarifaInteresado.Location = new System.Drawing.Point(114, 126);
            this.textTarifaInteresado.Name = "textTarifaInteresado";
            this.textTarifaInteresado.ReadOnly = true;
            this.textTarifaInteresado.Size = new System.Drawing.Size(90, 20);
            this.textTarifaInteresado.TabIndex = 7;
            // 
            // labelTarifaInteresado
            // 
            this.labelTarifaInteresado.AutoSize = true;
            this.labelTarifaInteresado.Location = new System.Drawing.Point(71, 129);
            this.labelTarifaInteresado.Name = "labelTarifaInteresado";
            this.labelTarifaInteresado.Size = new System.Drawing.Size(37, 13);
            this.labelTarifaInteresado.TabIndex = 6;
            this.labelTarifaInteresado.Text = "Tarifa:";
            // 
            // textMonedaInteresado
            // 
            this.textMonedaInteresado.Location = new System.Drawing.Point(114, 100);
            this.textMonedaInteresado.Name = "textMonedaInteresado";
            this.textMonedaInteresado.ReadOnly = true;
            this.textMonedaInteresado.Size = new System.Drawing.Size(90, 20);
            this.textMonedaInteresado.TabIndex = 5;
            // 
            // textNoInteresado
            // 
            this.textNoInteresado.Location = new System.Drawing.Point(114, 8);
            this.textNoInteresado.Name = "textNoInteresado";
            this.textNoInteresado.ReadOnly = true;
            this.textNoInteresado.Size = new System.Drawing.Size(90, 20);
            this.textNoInteresado.TabIndex = 1;
            // 
            // labelNoInteresado
            // 
            this.labelNoInteresado.AutoSize = true;
            this.labelNoInteresado.Location = new System.Drawing.Point(28, 11);
            this.labelNoInteresado.Name = "labelNoInteresado";
            this.labelNoInteresado.Size = new System.Drawing.Size(80, 13);
            this.labelNoInteresado.TabIndex = 0;
            this.labelNoInteresado.Text = "&No. Interesado:";
            this.labelNoInteresado.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textNombreInteresado
            // 
            this.textNombreInteresado.Location = new System.Drawing.Point(114, 34);
            this.textNombreInteresado.Name = "textNombreInteresado";
            this.textNombreInteresado.ReadOnly = true;
            this.textNombreInteresado.Size = new System.Drawing.Size(293, 20);
            this.textNombreInteresado.TabIndex = 3;
            // 
            // labelNombreInteresado
            // 
            this.labelNombreInteresado.AutoSize = true;
            this.labelNombreInteresado.Location = new System.Drawing.Point(9, 37);
            this.labelNombreInteresado.Name = "labelNombreInteresado";
            this.labelNombreInteresado.Size = new System.Drawing.Size(99, 13);
            this.labelNombreInteresado.TabIndex = 2;
            this.labelNombreInteresado.Text = "N&ombre interesado:";
            this.labelNombreInteresado.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelMonedaInteresado
            // 
            this.labelMonedaInteresado.AutoSize = true;
            this.labelMonedaInteresado.Location = new System.Drawing.Point(59, 103);
            this.labelMonedaInteresado.Name = "labelMonedaInteresado";
            this.labelMonedaInteresado.Size = new System.Drawing.Size(49, 13);
            this.labelMonedaInteresado.TabIndex = 4;
            this.labelMonedaInteresado.Text = "Moneda:";
            // 
            // panelCliente
            // 
            this.panelCliente.Controls.Add(this.textTarifaCliente);
            this.panelCliente.Controls.Add(this.labelTarifaCliente);
            this.panelCliente.Controls.Add(this.textMonedaCliente);
            this.panelCliente.Controls.Add(this.textNoCliente);
            this.panelCliente.Controls.Add(this.labelNoCliente);
            this.panelCliente.Controls.Add(this.textNombreCliente);
            this.panelCliente.Controls.Add(this.labelNombreCliente);
            this.panelCliente.Controls.Add(this.labelMonedaCliente);
            this.panelCliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCliente.Location = new System.Drawing.Point(3, 16);
            this.panelCliente.Name = "panelCliente";
            this.panelCliente.Size = new System.Drawing.Size(438, 227);
            this.panelCliente.TabIndex = 0;
            this.panelCliente.Visible = false;
            // 
            // textTarifaCliente
            // 
            this.textTarifaCliente.Location = new System.Drawing.Point(96, 86);
            this.textTarifaCliente.Name = "textTarifaCliente";
            this.textTarifaCliente.ReadOnly = true;
            this.textTarifaCliente.Size = new System.Drawing.Size(90, 20);
            this.textTarifaCliente.TabIndex = 7;
            // 
            // labelTarifaCliente
            // 
            this.labelTarifaCliente.AutoSize = true;
            this.labelTarifaCliente.Location = new System.Drawing.Point(53, 89);
            this.labelTarifaCliente.Name = "labelTarifaCliente";
            this.labelTarifaCliente.Size = new System.Drawing.Size(37, 13);
            this.labelTarifaCliente.TabIndex = 6;
            this.labelTarifaCliente.Text = "Tarifa:";
            // 
            // textMonedaCliente
            // 
            this.textMonedaCliente.Location = new System.Drawing.Point(96, 60);
            this.textMonedaCliente.Name = "textMonedaCliente";
            this.textMonedaCliente.ReadOnly = true;
            this.textMonedaCliente.Size = new System.Drawing.Size(90, 20);
            this.textMonedaCliente.TabIndex = 5;
            // 
            // textNoCliente
            // 
            this.textNoCliente.Location = new System.Drawing.Point(96, 8);
            this.textNoCliente.Name = "textNoCliente";
            this.textNoCliente.ReadOnly = true;
            this.textNoCliente.Size = new System.Drawing.Size(90, 20);
            this.textNoCliente.TabIndex = 1;
            // 
            // labelNoCliente
            // 
            this.labelNoCliente.AutoSize = true;
            this.labelNoCliente.Location = new System.Drawing.Point(28, 11);
            this.labelNoCliente.Name = "labelNoCliente";
            this.labelNoCliente.Size = new System.Drawing.Size(62, 13);
            this.labelNoCliente.TabIndex = 0;
            this.labelNoCliente.Text = "&No. Cliente:";
            this.labelNoCliente.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textNombreCliente
            // 
            this.textNombreCliente.Location = new System.Drawing.Point(96, 34);
            this.textNombreCliente.Name = "textNombreCliente";
            this.textNombreCliente.ReadOnly = true;
            this.textNombreCliente.Size = new System.Drawing.Size(311, 20);
            this.textNombreCliente.TabIndex = 3;
            // 
            // labelNombreCliente
            // 
            this.labelNombreCliente.AutoSize = true;
            this.labelNombreCliente.Location = new System.Drawing.Point(9, 37);
            this.labelNombreCliente.Name = "labelNombreCliente";
            this.labelNombreCliente.Size = new System.Drawing.Size(81, 13);
            this.labelNombreCliente.TabIndex = 2;
            this.labelNombreCliente.Text = "N&ombre cliente:";
            this.labelNombreCliente.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelMonedaCliente
            // 
            this.labelMonedaCliente.AutoSize = true;
            this.labelMonedaCliente.Location = new System.Drawing.Point(41, 63);
            this.labelMonedaCliente.Name = "labelMonedaCliente";
            this.labelMonedaCliente.Size = new System.Drawing.Size(49, 13);
            this.labelMonedaCliente.TabIndex = 4;
            this.labelMonedaCliente.Text = "Moneda:";
            // 
            // groupBusqueda
            // 
            this.groupBusqueda.Controls.Add(this.comboGrupos);
            this.groupBusqueda.Controls.Add(this.btnBuscar);
            this.groupBusqueda.Controls.Add(this.textNumero);
            this.groupBusqueda.Controls.Add(this.labelNumero);
            this.groupBusqueda.Controls.Add(this.textNombre);
            this.groupBusqueda.Controls.Add(this.labelNombre);
            this.groupBusqueda.Location = new System.Drawing.Point(257, 12);
            this.groupBusqueda.Name = "groupBusqueda";
            this.groupBusqueda.Size = new System.Drawing.Size(509, 75);
            this.groupBusqueda.TabIndex = 1;
            this.groupBusqueda.TabStop = false;
            this.groupBusqueda.Text = "Búsqueda";
            // 
            // comboGrupos
            // 
            this.comboGrupos.FormattingEnabled = true;
            this.comboGrupos.Location = new System.Drawing.Point(109, 45);
            this.comboGrupos.Name = "comboGrupos";
            this.comboGrupos.Size = new System.Drawing.Size(155, 21);
            this.comboGrupos.TabIndex = 5;
            this.comboGrupos.Visible = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(426, 43);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "&Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // textNumero
            // 
            this.textNumero.Location = new System.Drawing.Point(109, 19);
            this.textNumero.Name = "textNumero";
            this.textNumero.Size = new System.Drawing.Size(90, 20);
            this.textNumero.TabIndex = 1;
            // 
            // labelNumero
            // 
            this.labelNumero.Location = new System.Drawing.Point(3, 22);
            this.labelNumero.Name = "labelNumero";
            this.labelNumero.Size = new System.Drawing.Size(100, 13);
            this.labelNumero.TabIndex = 0;
            this.labelNumero.Text = "&Número:";
            this.labelNumero.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textNombre
            // 
            this.textNombre.Location = new System.Drawing.Point(109, 45);
            this.textNombre.Name = "textNombre";
            this.textNombre.Size = new System.Drawing.Size(311, 20);
            this.textNombre.TabIndex = 3;
            // 
            // labelNombre
            // 
            this.labelNombre.Location = new System.Drawing.Point(3, 48);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(100, 13);
            this.labelNombre.TabIndex = 2;
            this.labelNombre.Text = "N&ombre:";
            this.labelNombre.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupTarifa
            // 
            this.groupTarifa.Controls.Add(this.btnAsignarTarifa);
            this.groupTarifa.Controls.Add(this.listBoxTarifas);
            this.groupTarifa.Location = new System.Drawing.Point(462, 93);
            this.groupTarifa.Name = "groupTarifa";
            this.groupTarifa.Size = new System.Drawing.Size(304, 246);
            this.groupTarifa.TabIndex = 3;
            this.groupTarifa.TabStop = false;
            this.groupTarifa.Text = "Tarifas";
            // 
            // btnAsignarTarifa
            // 
            this.btnAsignarTarifa.Location = new System.Drawing.Point(6, 214);
            this.btnAsignarTarifa.Name = "btnAsignarTarifa";
            this.btnAsignarTarifa.Size = new System.Drawing.Size(75, 23);
            this.btnAsignarTarifa.TabIndex = 1;
            this.btnAsignarTarifa.Text = "Asignar";
            this.btnAsignarTarifa.UseVisualStyleBackColor = true;
            this.btnAsignarTarifa.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // listBoxTarifas
            // 
            this.listBoxTarifas.FormattingEnabled = true;
            this.listBoxTarifas.Location = new System.Drawing.Point(6, 21);
            this.listBoxTarifas.Name = "listBoxTarifas";
            this.listBoxTarifas.Size = new System.Drawing.Size(292, 186);
            this.listBoxTarifas.TabIndex = 0;
            // 
            // SeleccionarTarifa
            // 
            this.AcceptButton = this.btnBuscar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.groupTarifa);
            this.Controls.Add(this.groupBusqueda);
            this.Controls.Add(this.groupDatosTipo);
            this.Controls.Add(this.groupBoxTipo);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 400);
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "SeleccionarTarifa";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Seleccionar tarifa por cliente / interesado / caso";
            this.groupBoxTipo.ResumeLayout(false);
            this.groupBoxTipo.PerformLayout();
            this.groupDatosTipo.ResumeLayout(false);
            this.panelCaso.ResumeLayout(false);
            this.panelCaso.PerformLayout();
            this.panelInteresado.ResumeLayout(false);
            this.panelInteresado.PerformLayout();
            this.panelCliente.ResumeLayout(false);
            this.panelCliente.PerformLayout();
            this.groupBusqueda.ResumeLayout(false);
            this.groupBusqueda.PerformLayout();
            this.groupTarifa.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxTipo;
        private System.Windows.Forms.RadioButton radioCliente;
        private System.Windows.Forms.RadioButton radioInteresado;
        private System.Windows.Forms.RadioButton radioCaso;
        private System.Windows.Forms.GroupBox groupDatosTipo;
        private System.Windows.Forms.Panel panelCliente;
        private System.Windows.Forms.Label labelMonedaCliente;
        private System.Windows.Forms.GroupBox groupBusqueda;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox textNumero;
        private System.Windows.Forms.Label labelNumero;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.TextBox textNoCliente;
        private System.Windows.Forms.Label labelNoCliente;
        private System.Windows.Forms.TextBox textNombreCliente;
        private System.Windows.Forms.Label labelNombreCliente;
        private System.Windows.Forms.TextBox textMonedaCliente;
        private System.Windows.Forms.TextBox textTarifaCliente;
        private System.Windows.Forms.Label labelTarifaCliente;
        private System.Windows.Forms.GroupBox groupTarifa;
        private System.Windows.Forms.Button btnAsignarTarifa;
        private System.Windows.Forms.ListBox listBoxTarifas;
        private System.Windows.Forms.Panel panelInteresado;
        private System.Windows.Forms.TextBox textTarifaInteresado;
        private System.Windows.Forms.Label labelTarifaInteresado;
        private System.Windows.Forms.TextBox textMonedaInteresado;
        private System.Windows.Forms.TextBox textNoInteresado;
        private System.Windows.Forms.Label labelNoInteresado;
        private System.Windows.Forms.TextBox textNombreInteresado;
        private System.Windows.Forms.Label labelNombreInteresado;
        private System.Windows.Forms.Label labelMonedaInteresado;
        private System.Windows.Forms.TextBox textNombreCorto;
        private System.Windows.Forms.Label labelNombreCorto;
        private System.Windows.Forms.Label separadorInteresado;
        private System.Windows.Forms.ComboBox comboGrupos;
        private System.Windows.Forms.TextBox textNombre;
        private System.Windows.Forms.Panel panelCaso;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textReferencia;
        private System.Windows.Forms.Label labelReferencia;
        private System.Windows.Forms.TextBox textTarifaCaso;
        private System.Windows.Forms.Label labelTarifaCaso;
        private System.Windows.Forms.TextBox textMonedaCaso;
        private System.Windows.Forms.TextBox textNoCaso;
        private System.Windows.Forms.Label labelNoCaso;
        private System.Windows.Forms.TextBox textTituloCaso;
        private System.Windows.Forms.Label labelTituloCaso;
        private System.Windows.Forms.Label labelMonedaCaso;
    }
}