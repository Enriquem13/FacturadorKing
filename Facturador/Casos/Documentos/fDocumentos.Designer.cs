
namespace Facturador.Casos.Documentos
{
    partial class fDocumentos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fDocumentos));
            this.cbGrupo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbHabilitado = new System.Windows.Forms.ComboBox();
            this.cbGrupoplazo = new System.Windows.Forms.ComboBox();
            this.btnModificar = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tbDescripcion = new System.Windows.Forms.TextBox();
            this.tbTipoplazoid = new System.Windows.Forms.TextBox();
            this.Habilitado = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvTioplazos = new System.Windows.Forms.DataGridView();
            this.DocumentoId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentoCodigoBarras = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubTipoDocumentoId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoDocumentoDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubTipoDocumentoDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentoFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentoFolio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentoFechaRecepcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentoFechaVencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentoFechaCaptura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentoFechaEscaneo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentoObservacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UsuarioId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompaniaMensajeriaId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentoFechaEnvio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentoNumeroGuia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentoFechaEntrega = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.foliodocumentocontesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigobarrasdoccontesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usuarioIdPreparo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.casoid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoSolicitudId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RelacionDocumentoLink = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTioplazos)).BeginInit();
            this.SuspendLayout();
            // 
            // cbGrupo
            // 
            this.cbGrupo.FormattingEnabled = true;
            this.cbGrupo.Location = new System.Drawing.Point(844, 112);
            this.cbGrupo.Name = "cbGrupo";
            this.cbGrupo.Size = new System.Drawing.Size(144, 21);
            this.cbGrupo.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(847, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Grupo";
            // 
            // cbHabilitado
            // 
            this.cbHabilitado.FormattingEnabled = true;
            this.cbHabilitado.Location = new System.Drawing.Point(844, 41);
            this.cbHabilitado.Name = "cbHabilitado";
            this.cbHabilitado.Size = new System.Drawing.Size(144, 21);
            this.cbHabilitado.TabIndex = 27;
            // 
            // cbGrupoplazo
            // 
            this.cbGrupoplazo.FormattingEnabled = true;
            this.cbGrupoplazo.Location = new System.Drawing.Point(443, 40);
            this.cbGrupoplazo.Name = "cbGrupoplazo";
            this.cbGrupoplazo.Size = new System.Drawing.Size(121, 21);
            this.cbGrupoplazo.TabIndex = 26;
            // 
            // btnModificar
            // 
            this.btnModificar.Enabled = false;
            this.btnModificar.Location = new System.Drawing.Point(1186, 74);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(75, 23);
            this.btnModificar.TabIndex = 25;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1182, 125);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 35);
            this.button2.TabIndex = 24;
            this.button2.Text = "Eliminar Seleccionado";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbDescripcion
            // 
            this.tbDescripcion.Location = new System.Drawing.Point(124, 42);
            this.tbDescripcion.Name = "tbDescripcion";
            this.tbDescripcion.Size = new System.Drawing.Size(222, 20);
            this.tbDescripcion.TabIndex = 23;
            // 
            // tbTipoplazoid
            // 
            this.tbTipoplazoid.Location = new System.Drawing.Point(31, 41);
            this.tbTipoplazoid.Name = "tbTipoplazoid";
            this.tbTipoplazoid.ReadOnly = true;
            this.tbTipoplazoid.Size = new System.Drawing.Size(64, 20);
            this.tbTipoplazoid.TabIndex = 22;
            this.tbTipoplazoid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Habilitado
            // 
            this.Habilitado.AutoSize = true;
            this.Habilitado.Location = new System.Drawing.Point(842, 15);
            this.Habilitado.Name = "Habilitado";
            this.Habilitado.Size = new System.Drawing.Size(48, 13);
            this.Habilitado.TabIndex = 21;
            this.Habilitado.Text = "Habilitdo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(440, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Grupo plazo";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(1186, 20);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 19;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Tipoplazoid";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Descripción del plazo";
            // 
            // dgvTioplazos
            // 
            this.dgvTioplazos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTioplazos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DocumentoId,
            this.DocumentoCodigoBarras,
            this.SubTipoDocumentoId,
            this.TipoDocumentoDescrip,
            this.SubTipoDocumentoDescrip,
            this.DocumentoFecha,
            this.DocumentoFolio,
            this.DocumentoFechaRecepcion,
            this.DocumentoFechaVencimiento,
            this.DocumentoFechaCaptura,
            this.DocumentoFechaEscaneo,
            this.DocumentoObservacion,
            this.UsuarioId,
            this.CompaniaMensajeriaId,
            this.DocumentoFechaEnvio,
            this.DocumentoNumeroGuia,
            this.DocumentoFechaEntrega,
            this.foliodocumentocontesto,
            this.codigobarrasdoccontesto,
            this.usuarioIdPreparo,
            this.casoid,
            this.TipoSolicitudId,
            this.RelacionDocumentoLink});
            this.dgvTioplazos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvTioplazos.Location = new System.Drawing.Point(0, 205);
            this.dgvTioplazos.Name = "dgvTioplazos";
            this.dgvTioplazos.Size = new System.Drawing.Size(1275, 382);
            this.dgvTioplazos.TabIndex = 16;
            this.dgvTioplazos.DoubleClick += new System.EventHandler(this.dgvTioplazos_DoubleClick);
            // 
            // DocumentoId
            // 
            this.DocumentoId.HeaderText = "DocumentoId";
            this.DocumentoId.Name = "DocumentoId";
            // 
            // DocumentoCodigoBarras
            // 
            this.DocumentoCodigoBarras.HeaderText = "DocumentoCodigoBarras";
            this.DocumentoCodigoBarras.Name = "DocumentoCodigoBarras";
            // 
            // SubTipoDocumentoId
            // 
            this.SubTipoDocumentoId.HeaderText = "SubTipoDocumentoId";
            this.SubTipoDocumentoId.Name = "SubTipoDocumentoId";
            // 
            // TipoDocumentoDescrip
            // 
            this.TipoDocumentoDescrip.HeaderText = "TipoDocumentoDescrip";
            this.TipoDocumentoDescrip.Name = "TipoDocumentoDescrip";
            // 
            // SubTipoDocumentoDescrip
            // 
            this.SubTipoDocumentoDescrip.HeaderText = "SubTipoDocumentoDescrip";
            this.SubTipoDocumentoDescrip.Name = "SubTipoDocumentoDescrip";
            // 
            // DocumentoFecha
            // 
            this.DocumentoFecha.HeaderText = "DocumentoFecha";
            this.DocumentoFecha.Name = "DocumentoFecha";
            // 
            // DocumentoFolio
            // 
            this.DocumentoFolio.HeaderText = "DocumentoFolio";
            this.DocumentoFolio.Name = "DocumentoFolio";
            // 
            // DocumentoFechaRecepcion
            // 
            this.DocumentoFechaRecepcion.HeaderText = "DocumentoFechaRecepcion";
            this.DocumentoFechaRecepcion.Name = "DocumentoFechaRecepcion";
            // 
            // DocumentoFechaVencimiento
            // 
            this.DocumentoFechaVencimiento.HeaderText = "DocumentoFechaVencimiento";
            this.DocumentoFechaVencimiento.Name = "DocumentoFechaVencimiento";
            // 
            // DocumentoFechaCaptura
            // 
            this.DocumentoFechaCaptura.HeaderText = "DocumentoFechaCaptura";
            this.DocumentoFechaCaptura.Name = "DocumentoFechaCaptura";
            // 
            // DocumentoFechaEscaneo
            // 
            this.DocumentoFechaEscaneo.HeaderText = "DocumentoFechaEscaneo";
            this.DocumentoFechaEscaneo.Name = "DocumentoFechaEscaneo";
            // 
            // DocumentoObservacion
            // 
            this.DocumentoObservacion.HeaderText = "DocumentoObservacion";
            this.DocumentoObservacion.Name = "DocumentoObservacion";
            // 
            // UsuarioId
            // 
            this.UsuarioId.HeaderText = "UsuarioId";
            this.UsuarioId.Name = "UsuarioId";
            // 
            // CompaniaMensajeriaId
            // 
            this.CompaniaMensajeriaId.HeaderText = "CompaniaMensajeriaId";
            this.CompaniaMensajeriaId.Name = "CompaniaMensajeriaId";
            // 
            // DocumentoFechaEnvio
            // 
            this.DocumentoFechaEnvio.HeaderText = "DocumentoFechaEnvio";
            this.DocumentoFechaEnvio.Name = "DocumentoFechaEnvio";
            // 
            // DocumentoNumeroGuia
            // 
            this.DocumentoNumeroGuia.HeaderText = "DocumentoNumeroGuia";
            this.DocumentoNumeroGuia.Name = "DocumentoNumeroGuia";
            // 
            // DocumentoFechaEntrega
            // 
            this.DocumentoFechaEntrega.HeaderText = "DocumentoFechaEntrega";
            this.DocumentoFechaEntrega.Name = "DocumentoFechaEntrega";
            // 
            // foliodocumentocontesto
            // 
            this.foliodocumentocontesto.HeaderText = "foliodocumentocontesto";
            this.foliodocumentocontesto.Name = "foliodocumentocontesto";
            // 
            // codigobarrasdoccontesto
            // 
            this.codigobarrasdoccontesto.HeaderText = "codigobarrasdoccontesto";
            this.codigobarrasdoccontesto.Name = "codigobarrasdoccontesto";
            // 
            // usuarioIdPreparo
            // 
            this.usuarioIdPreparo.HeaderText = "usuarioIdPreparo";
            this.usuarioIdPreparo.Name = "usuarioIdPreparo";
            // 
            // casoid
            // 
            this.casoid.HeaderText = "casoid";
            this.casoid.Name = "casoid";
            // 
            // TipoSolicitudId
            // 
            this.TipoSolicitudId.HeaderText = "TipoSolicitudId";
            this.TipoSolicitudId.Name = "TipoSolicitudId";
            // 
            // RelacionDocumentoLink
            // 
            this.RelacionDocumentoLink.HeaderText = "RelacionDocumentoLink";
            this.RelacionDocumentoLink.Name = "RelacionDocumentoLink";
            // 
            // fDocumentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1275, 587);
            this.Controls.Add(this.cbGrupo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbHabilitado);
            this.Controls.Add(this.cbGrupoplazo);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbDescripcion);
            this.Controls.Add(this.tbTipoplazoid);
            this.Controls.Add(this.Habilitado);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvTioplazos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fDocumentos";
            this.Text = "Subtipo documentos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTioplazos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbGrupo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbHabilitado;
        private System.Windows.Forms.ComboBox cbGrupoplazo;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbDescripcion;
        private System.Windows.Forms.TextBox tbTipoplazoid;
        private System.Windows.Forms.Label Habilitado;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvTioplazos;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentoId;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentoCodigoBarras;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubTipoDocumentoId;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoDocumentoDescrip;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubTipoDocumentoDescrip;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentoFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentoFolio;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentoFechaRecepcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentoFechaVencimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentoFechaCaptura;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentoFechaEscaneo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentoObservacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsuarioId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompaniaMensajeriaId;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentoFechaEnvio;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentoNumeroGuia;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentoFechaEntrega;
        private System.Windows.Forms.DataGridViewTextBoxColumn foliodocumentocontesto;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigobarrasdoccontesto;
        private System.Windows.Forms.DataGridViewTextBoxColumn usuarioIdPreparo;
        private System.Windows.Forms.DataGridViewTextBoxColumn casoid;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoSolicitudId;
        private System.Windows.Forms.DataGridViewTextBoxColumn RelacionDocumentoLink;
    }
}