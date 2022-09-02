using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturador.plazos_forms
{
    public partial class subtipodocplazos : Form
    {
        funcionesdicss funcionesgenerales = new funcionesdicss();
        public subtipodocplazos()
        {
            InitializeComponent();
            cargartiposplazos();

            //cargamos la información de los combobox
            //ComboboxItem cbItme1 = new ComboboxItem();
            //cbItme1.Value = 1;
            //cbItme1.Text = "Habilitado";
            //ComboboxItem cbItme2 = new ComboboxItem();
            //cbItme2.Value = 0;
            //cbItme2.Text = "Inhabilitado";

            //cbHabilitado.Items.Add(cbItme1);
            //cbHabilitado.Items.Add(cbItme2);

            //seleccionamos las opciones de grupos
            conect con_grupo = new conect();
            String squery_grupo = "select * from tipodocumento;";
            MySqlDataReader resp_grupo = con_grupo.getdatareader(squery_grupo);
            while (resp_grupo.Read())
            {
                cbTipodocumento.Items.Add(funcionesgenerales.validareader("TipoDocumentoDescrip", "TipoDocumentoId", resp_grupo));
            }
            resp_grupo.Close();
            con_grupo.Cerrarconexion();

            //seleccionamos las opciones de grupoplazo
            conect con_grupo_plazo = new conect();
            String squery_grupo_plazo = "SELECT * FROM grupoplazo;";
            MySqlDataReader resp_grupo_plazo = con_grupo_plazo.getdatareader(squery_grupo_plazo);
            while (resp_grupo_plazo.Read())
            {
                cbgrupo_plazo.Items.Add(funcionesgenerales.validareader("GrupoPlazoDescripcion", "GrupoPlazoId", resp_grupo_plazo));
            }
            resp_grupo_plazo.Close();
            con_grupo_plazo.Cerrarconexion();

            //seleccionamos las opciones de grupo
            conect con_grupo_ = new conect();
            String squery_grupo_ = "select * from grupo;";
            MySqlDataReader resp_grupo_ = con_grupo_.getdatareader(squery_grupo_);
            while (resp_grupo_.Read())
            {
                cbGrupoCasos.Items.Add(funcionesgenerales.validareader("GrupoDescripcion", "GrupoId", resp_grupo_));
            }
            resp_grupo_.Close();
            con_grupo_.Cerrarconexion();


            //seleccionamos las opciones de Tiposolicitud
            conect con_tiosol = new conect();
            String squery_tiosol_ = "select * from tiposolicitud;";
            MySqlDataReader resp_tiosol = con_tiosol.getdatareader(squery_tiosol_);
            while (resp_tiosol.Read())
            {
                cbTiposolicitud.Items.Add(funcionesgenerales.validareader("TipoSolicitudDescrip", "TipoSolicitudId", resp_tiosol));
            }
            resp_tiosol.Close();
            con_tiosol.Cerrarconexion();

            //seleccionamos las opciones de Tiposolicitud
            conect con_subtiposol = new conect();
            String squery_subtiposol = "select * from subtiposolicitud;";
            MySqlDataReader resp_subtiposol = con_subtiposol.getdatareader(squery_subtiposol);
            while (resp_subtiposol.Read())
            {
                cbSubtiposolicitud.Items.Add(funcionesgenerales.validareader("SubTipoSolicitudDescripcion", "SubTipoSolicitudId", resp_subtiposol));
            }
            resp_subtiposol.Close();
            con_subtiposol.Cerrarconexion();

            //seleccionamos las opciones de casodiseñoclasific
            conect con_casodiseno = new conect();
            String squery_casodiseno = "select * from textodiseno;";
            MySqlDataReader resp_casodiseno = con_casodiseno.getdatareader(squery_casodiseno);
            while (resp_casodiseno.Read())
            {
                ComboboxItem sCasodiseno = new ComboboxItem();
                sCasodiseno.Value = funcionesgenerales.validareader("TextoDisenoTitulo", "CasoDisenoClasificacion", resp_casodiseno).Value;
                sCasodiseno.Text = funcionesgenerales.validareader("TextoDisenoTitulo", "CasoDisenoClasificacion", resp_casodiseno).Value+"-"+funcionesgenerales.validareader("TextoDisenoTitulo", "CasoDisenoClasificacion", resp_casodiseno).Text;
                cbCalsodiseno.Items.Add(sCasodiseno);
            }
            resp_casodiseno.Close();
            con_casodiseno.Cerrarconexion();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //agregar tipoplazo
            try
            {
                ComboboxItem sSubtipodocumentoidcb = (cbSubtipodocumentoid.SelectedItem as ComboboxItem);
                ComboboxItem scbTipoplazo = (cbTipo_plazo.SelectedItem as ComboboxItem);
                //los nuevos filtros agregados
                ComboboxItem ccbgrupo = (cbGrupoCasos.SelectedItem as ComboboxItem);
                ComboboxItem ccbTiposolicitud = (cbTiposolicitud.SelectedItem as ComboboxItem);
                ComboboxItem ccbSubtiposolicitud = (cbSubtiposolicitud.SelectedItem as ComboboxItem);
                ComboboxItem ccbCalsodiseno = (cbCalsodiseno.SelectedItem as ComboboxItem);


                String sccGrupo = "", sccbTiposolicitud = "", sccbSubtiposolicitud="", sccbCalsodiseno="", sDiasprorroga="", sMEsesprorroga="";
                if (ccbgrupo is null)
                {
                    sccGrupo = "null";
                }
                else {
                    sccGrupo = ccbgrupo.Value.ToString();
                }

                if (ccbTiposolicitud is null)
                {
                    sccbTiposolicitud = "null";
                }
                else {
                    sccbTiposolicitud = ccbTiposolicitud.Value.ToString();
                }

                if (ccbSubtiposolicitud is null)
                {
                    sccbSubtiposolicitud = "null";
                }
                else {
                    sccbSubtiposolicitud = ccbSubtiposolicitud.Value.ToString();
                }

                if (ccbCalsodiseno is null)
                {
                    sccbCalsodiseno = "null";
                }
                else {
                    sccbCalsodiseno = ccbCalsodiseno.Value.ToString();
                }

                if (tbDiasprorroga.Text.Trim() == "")
                {
                    sDiasprorroga = "0";
                }
                else {
                    sDiasprorroga = tbDiasprorroga.Text;
                }

                if (tbMesesprorroga.Text.Trim() == "")
                {
                    sMEsesprorroga = "0";
                }
                else {
                    sMEsesprorroga = tbMesesprorroga.Text;
                }

                if (!(sSubtipodocumentoidcb is null) && !(scbTipoplazo is null) && tbDias.Text!="" && tbMeses.Text!="")
                {
                    //cargamos los tipoplazos existentes
                    //validamos el checkbox para saber si es aviso y no poner fecha notificación
                    String sValorAviso = "";
                    if (chAviso.Checked)
                    {
                        sValorAviso = "1";
                    }
                    else {
                        sValorAviso = "0";
                    }
                    conect con_plazos = new conect();
                    String squery_plazos = " INSERT INTO `plazos_de_oficio` " +
                                            " ( " +
                                            " `SubTipoDocumentoId`, " +
                                            " `TipoPlazoId`, " +
                                            " `SubTipoDocumentoPlazoDias`, " +
                                            " `SubTipoDocumentoPlazoMeses`, " +
                                            " `aviso`, " +
                                            " `grupo`, " +
                                            " `TipoSolicitudId`, " +
                                            " `SubTipoSolicitudId`, " +
                                            " `CasoDisenoClasificacion`, " +
                                            " `diasprorroga`, " +
                                            " `mesesprorroga` " +
                                            " ) " +
                                            " VALUES " +
                                            " ( " +
                                            " '"+ sSubtipodocumentoidcb .Value+ "', " +
                                            " '"+ scbTipoplazo.Value+ "', " +
                                            " '"+ tbDias.Text+ "', " +
                                            " '"+ tbMeses.Text+ "', " +
                                            " '" + sValorAviso + "', " +
                                            " " + sccGrupo + ", " +//grupo
                                            " " + sccbTiposolicitud + ", " +//Tiposolicitudid
                                            " " + sccbSubtiposolicitud + ", " +//Subtiposolicitudid
                                            " " + sccbCalsodiseno + ", " +//Casodisenoclasific
                                            " " + sDiasprorroga + ", " +//dias prorroga
                                            " " + sMEsesprorroga + " " +//mesesprorroga
                                            " ); ";
                    MySqlDataReader resp_plazos = con_plazos.getdatareader(squery_plazos);
                    if (resp_plazos.RecordsAffected > 0)
                    {
                        MessageBox.Show("Tipo plazo agregado correctamente.");
                        cbSubtipodocumentoid.Text = "";
                        cbTipo_plazo.Text = "";
                        cbTipodocumento.Text = "";
                        cbgrupo_plazo.Text = "";
                        tbDias.Text = "";
                        tbMeses.Text = "";
                        chAviso.Checked = false;
                        cargartiposplazos();

                    }
                    else
                    {
                        MessageBox.Show("Verifica la información.");
                    }

                    resp_plazos.Close();
                    con_plazos.Cerrarconexion();
                }
                else
                {
                    MessageBox.Show("Debe llenar todos los datos");
                }
            }
            catch (Exception exs)
            {
                MessageBox.Show("Debe llenar todos los datos");
            }
        }
        public void cargartiposplazos()
        {
            try
            {
                //cargamos los tipoplazos existentes
                conect con_plazos = new conect();
                String squery_plazos = " SELECT  " +
                                        " plazos_de_oficio.plazos_de_oficioid, " +
                                        " tipodocumento.TipoDocumentoDescrip As Tipo_documento, " +
                                        " subtipodocumento.SubTipoDocumentoId As SubTipoDocumentoId, " +
                                        " subtipodocumento.SubTipoDocumentoDescrip As Documento, " +
                                        " tipoplazo.TipoPlazoId As TipoPlazoId, " +
                                        " tipoplazo.TipoPlazoDescrip As Tipo_Plazo, " +
                                        " grupoplazo.GrupoPlazoDescripcion As GrupoPlazoDescripcion, " +
                                        " subtipodocumento.SubTipoDocumentoIndAct As habilitado, " +
                                        " plazos_de_oficio.SubTipoDocumentoPlazoMeses As Meses_plazo, " +
                                        " plazos_de_oficio.SubTipoDocumentoPlazoDias As dias_plazo, " +
                                        " plazos_de_oficio.aviso As aviso, " +
                                        " plazos_de_oficio.grupo As grupo, " +
                                        " grupo.GrupoDescripcion As GrupoDescripcion, " +
                                        
                                        " plazos_de_oficio.TipoSolicitudId As TipoSolicitudId, " +
                                        " tiposolicitud.TipoSolicitudDescrip As TipoSolicitudDescrip," +
                                        " plazos_de_oficio.SubTipoSolicitudId As SubTipoSolicitudId, " +
                                        " subtiposolicitud.SubTipoSolicitudDescripcion As SubTipoSolicitudDescripcion," +
                                        " plazos_de_oficio.CasoDisenoClasificacion As CasoDisenoClasificacion, " +
                                        " textodiseno.TextoDisenoTitulo As TextoDisenoTitulo," +
                                        " plazos_de_oficio.diasprorroga As diasprorroga, " +
                                        " plazos_de_oficio.mesesprorroga As mesesprorroga " +
                                        " FROM " +
                                        " plazos_de_oficio " +
                                        " LEFT JOIN " +
                                        " subtipodocumento ON plazos_de_oficio.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId " +
                                        " LEFT JOIN  " +
                                        " tipoplazo ON plazos_de_oficio.TipoPlazoId = TipoPlazo.TipoPlazoId " +
                                        " left join grupoplazo on grupoplazo.GrupoPlazoId = tipoplazo.GrupoPlazoId " +
                                        " left join  " +
                                        " tipodocumento ON subtipodocumento.TipoDocumentoId = tipodocumento.TipoDocumentoId" +
                                        " left join " +
                                        " tiposolicitud ON plazos_de_oficio.TipoSolicitudId = tiposolicitud.TipoSolicitudId" +
                                        " left join" +
                                        " subtiposolicitud on plazos_de_oficio.SubTipoSolicitudId = SubTipoSolicitud.SubTipoSolicitudId" +
                                        " left join " +
                                        " textodiseno on plazos_de_oficio.CasoDisenoClasificacion = textodiseno.CasoDisenoClasificacion" +
                                        " left join " +
                                        " grupo on plazos_de_oficio.grupo = grupo.GrupoId group by plazos_de_oficio.plazos_de_oficioid;";

                MySqlDataReader resp_plazos = con_plazos.getdatareader(squery_plazos);
                dgvTioplazos.Rows.Clear();
                while (resp_plazos.Read())
                {
                    String sPlazohabilitado = "Inhabilitado";
                    if (funcionesgenerales.validareader("habilitado", "habilitado", resp_plazos).Text == "1")
                        sPlazohabilitado = "habilitado";
                        dgvTioplazos.Rows.Add(int.Parse(funcionesgenerales.validareader("plazos_de_oficioid", "plazos_de_oficioid", resp_plazos).Text),
                                        funcionesgenerales.validareader("Tipo_documento", "Tipo_documento", resp_plazos).Text,
                                        int.Parse(funcionesgenerales.validareader("SubTipoDocumentoId", "SubTipoDocumentoId", resp_plazos).Text), 
                                        funcionesgenerales.validareader("Documento", "Documento", resp_plazos).Text,
                                        funcionesgenerales.validareader("GrupoPlazoDescripcion", "GrupoPlazoDescripcion", resp_plazos).Text,
                                        int.Parse(funcionesgenerales.validareader("TipoPlazoId", "TipoPlazoId", resp_plazos).Text),
                                        funcionesgenerales.validareader("Tipo_Plazo", "Tipo_Plazo", resp_plazos).Text,
                                        funcionesgenerales.validareader("dias_plazo", "dias_plazo", resp_plazos).Text,
                                        funcionesgenerales.validareader("Meses_plazo", "Meses_plazo", resp_plazos).Text,
                                        funcionesgenerales.validareader("aviso", "aviso", resp_plazos).Text,

                                        funcionesgenerales.validareader("GrupoDescripcion", "GrupoDescripcion", resp_plazos).Text,
                                        funcionesgenerales.validareader("TipoSolicitudId", "TipoSolicitudId", resp_plazos).Text,
                                        funcionesgenerales.validareader("TipoSolicitudDescrip", "TipoSolicitudDescrip", resp_plazos).Text,//descrip
                                        funcionesgenerales.validareader("SubTipoSolicitudId", "SubTipoSolicitudId", resp_plazos).Text,
                                        funcionesgenerales.validareader("SubTipoSolicitudDescripcion", "SubTipoSolicitudDescripcion", resp_plazos).Text,//descrip
                                        funcionesgenerales.validareader("TextoDisenoTitulo", "TextoDisenoTitulo", resp_plazos).Text,
                                        funcionesgenerales.validareader("diasprorroga", "diasprorroga", resp_plazos).Text,
                                        funcionesgenerales.validareader("mesesprorroga", "mesesprorroga", resp_plazos).Text

                        );
                }
                resp_plazos.Close();
                con_plazos.Cerrarconexion();
            }
            catch (Exception ecs)
            {
            }

        }

        private void cbGrupo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cbTipo_plazo.Text = "";
                
                ComboboxItem cGrupoplazo_Seleccionado = (cbgrupo_plazo.SelectedItem as ComboboxItem);
                if (cGrupoplazo_Seleccionado != null)
                {
                    //seleccionamos las opciones de grupos
                    conect con_grupo_plazo = new conect();
                    String squery_grupo_plazo = "Select * from tipoplazo where GrupoPlazoId = " + cGrupoplazo_Seleccionado.Value + ";";// and Grupoid = " + cGrupoplazo_Seleccionado.Value + ";";
                    MySqlDataReader resp_grupo_plazo = con_grupo_plazo.getdatareader(squery_grupo_plazo);
                    cbTipo_plazo.Enabled = true;
                    cbTipo_plazo.Items.Clear();
                    while (resp_grupo_plazo.Read())
                    {
                        cbTipo_plazo.Items.Add(funcionesgenerales.validareader("TipoPlazoDescrip", "TipoPlazoId", resp_grupo_plazo));
                    }
                    resp_grupo_plazo.Close();
                    con_grupo_plazo.Cerrarconexion();
                }
                else
                {
                    cbTipo_plazo.Enabled = false;
                    cbTipo_plazo.Items.Clear();
                }

                //FIN seleccionamos las opciones de grupos
                //MessageBox.Show(cSeleccionado.Text);
            }
            catch (Exception ex)
            {
                new filelog("plazos.cs <--", ex.ToString());
            }
        }

        private void cbgrupo_plazo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cbTipo_plazo.Text = "";
                
                ComboboxItem cGrupoplazo_Seleccionado = (cbgrupo_plazo.SelectedItem as ComboboxItem);
                if (cGrupoplazo_Seleccionado != null)
                {
                    //seleccionamos las opciones de grupos
                    conect con_grupo_plazo = new conect();
                    String squery_grupo_plazo = "Select * from tipoplazo where GrupoPlazoId = " + cGrupoplazo_Seleccionado.Value + ";";// and Grupoid = " + cGrupoplazo_Seleccionado.Value + ";";
                    MySqlDataReader resp_grupo_plazo = con_grupo_plazo.getdatareader(squery_grupo_plazo);
                    cbTipo_plazo.Enabled = true;
                    cbTipo_plazo.Items.Clear();
                    while (resp_grupo_plazo.Read())
                    {
                        cbTipo_plazo.Items.Add(funcionesgenerales.validareader("TipoPlazoDescrip", "TipoPlazoId", resp_grupo_plazo));
                    }
                    resp_grupo_plazo.Close();
                    con_grupo_plazo.Cerrarconexion();
                }
                else
                {
                    cbTipo_plazo.Enabled = false;
                    cbTipo_plazo.Items.Clear();
                }

                //FIN seleccionamos las opciones de grupos
                //MessageBox.Show(cSeleccionado.Text);
            }
            catch (Exception ex)
            {
                new filelog("plazos.cs <--", ex.ToString());
            }
        }

        private void cbTipodocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem cbTipodocumentovar = (cbTipodocumento.SelectedItem as ComboboxItem);
                if (cbTipodocumentovar != null)
                {
                    //seleccionamos las opciones de grupos
                    conect con_grupo_plazo = new conect();
                    String squery_grupo_plazo = "select * from subtipodocumento where TipoDocumentoId = " + cbTipodocumentovar.Value + ";";// and Grupoid = " + cGrupoplazo_Seleccionado.Value + ";";
                    MySqlDataReader resp_grupo_plazo = con_grupo_plazo.getdatareader(squery_grupo_plazo);

                    cbSubtipodocumentoid.Items.Clear();
                    while (resp_grupo_plazo.Read())
                    {
                        cbSubtipodocumentoid.Items.Add(funcionesgenerales.validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_grupo_plazo));
                    }
                    resp_grupo_plazo.Close();
                    con_grupo_plazo.Cerrarconexion();
                }

                //FIN seleccionamos las opciones de grupos
                //MessageBox.Show(cSeleccionado.Text);
            }
            catch (Exception ex)
            {
                new filelog("genera documento plazo <--", ex.ToString());
            }
        }

        private void dgvTioplazos_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //obtenemos los valores del front para poder modificarlos
                if (dgvTioplazos.SelectedRows.Count > 0)
                {
                    
                    tbTipoplazoid.Text = dgvTioplazos.SelectedRows[0].Cells[0].Value.ToString();//id
                    cbTipodocumento.Text = dgvTioplazos.SelectedRows[0].Cells[1].Value.ToString();
                    cbSubtipodocumentoid.Text = dgvTioplazos.SelectedRows[0].Cells[3].Value.ToString();
                    cbgrupo_plazo.Text = dgvTioplazos.SelectedRows[0].Cells[4].Value.ToString();
                    cbTipo_plazo.Text = dgvTioplazos.SelectedRows[0].Cells[6].Value.ToString();

                    //llenamos los combobox en orden para el comportamiento del change

                    cbGrupoCasos.Text = dgvTioplazos.SelectedRows[0].Cells[10].Value.ToString();
                    cbTiposolicitud.Text = dgvTioplazos.SelectedRows[0].Cells[12].Value.ToString();
                    cbSubtiposolicitud.Text = dgvTioplazos.SelectedRows[0].Cells[14].Value.ToString();
                    cbCalsodiseno.Text = dgvTioplazos.SelectedRows[0].Cells[15].Value.ToString();
                    tbDiasprorroga.Text = dgvTioplazos.SelectedRows[0].Cells[16].Value.ToString();
                    tbMesesprorroga.Text = dgvTioplazos.SelectedRows[0].Cells[17].Value.ToString();

                    tbDias.Text = dgvTioplazos.SelectedRows[0].Cells[7].Value.ToString();
                    tbMeses.Text = dgvTioplazos.SelectedRows[0].Cells[8].Value.ToString();

                    //despues de captar los datos debemos hacer el update habilitar botones
                    btnModificar.Enabled = true;
                    btnLimpiar.Enabled = true;
                    btnAgregar.Enabled = false;
                    if (dgvTioplazos.SelectedRows[0].Cells[9].Value.ToString() == "1")
                    {
                        chAviso.Checked = true;
                    }
                    else {
                        chAviso.Checked = false;
                    }
                }
            }
            catch (Exception exs)
            {

            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //una vez seleccionado el registro del datagridview se habailitara este boton para que podamos hacer el update con el dato del id
            try
            {
                if (tbTipoplazoid.Text.Trim()!="" 
                    && !(cbSubtipodocumentoid.SelectedItem is null)
                    && !(cbTipo_plazo.SelectedItem is null)){
                    //cargamos los tipoplazos existentes

                    //query update 
                    String schAviso = "";
                    if (chAviso.Checked) {
                        schAviso = "1";
                    }
                    else {
                        schAviso = "0";
                    }

                    //cargamos los ultimos filtros agregados
                    ComboboxItem ccbgrupo = (cbGrupoCasos.SelectedItem as ComboboxItem);
                    ComboboxItem ccbTiposolicitud = (cbTiposolicitud.SelectedItem as ComboboxItem);
                    ComboboxItem ccbSubtiposolicitud = (cbSubtiposolicitud.SelectedItem as ComboboxItem);
                    ComboboxItem ccbCalsodiseno = (cbCalsodiseno.SelectedItem as ComboboxItem);


                    String sccGrupo = "", sccbTiposolicitud = "", sccbSubtiposolicitud = "", sccbCalsodiseno = "", sDiasprorroga = "", sMEsesprorroga = "";
                    if (ccbgrupo is null)
                    {
                        sccGrupo = "null";
                    }
                    else {
                        sccGrupo = ccbgrupo.Value.ToString();
                    }

                    if (ccbTiposolicitud is null)
                    {
                        sccbTiposolicitud = "null";
                    }
                    else
                    {
                        sccbTiposolicitud = ccbTiposolicitud.Value.ToString();
                    }

                    if (ccbSubtiposolicitud is null)
                    {
                        sccbSubtiposolicitud = "null";
                    }
                    else
                    {
                        sccbSubtiposolicitud = ccbSubtiposolicitud.Value.ToString();
                    }

                    if (ccbCalsodiseno is null)
                    {
                        sccbCalsodiseno = "null";
                    }
                    else
                    {
                        sccbCalsodiseno = ccbCalsodiseno.Value.ToString();
                    }

                    if (tbDiasprorroga.Text.Trim() == "")
                    {
                        sDiasprorroga = "0";
                    }
                    else
                    {
                        sDiasprorroga = tbDiasprorroga.Text;
                    }

                    if (tbMesesprorroga.Text.Trim() == "")
                    {
                        sMEsesprorroga = "0";
                    }
                    else
                    {
                        sMEsesprorroga = tbMesesprorroga.Text;
                    }


                    conect con_plazos = new conect();
                    String squery_plazos = " UPDATE `plazos_de_oficio`" +
                                            " SET" +
                                            " `TipoPlazoId` = '" + (cbTipo_plazo.SelectedItem as ComboboxItem).Value + "'," +
                                            " `SubTipoDocumentoId` = '" + (cbSubtipodocumentoid.SelectedItem as ComboboxItem).Value + "'," +
                                            " `SubTipoDocumentoPlazoDias` = '" + tbDias.Text + "'," +
                                            " `SubTipoDocumentoPlazoMeses` = '" + tbMeses.Text + "'," +
                                            " `aviso` = '" + schAviso + "', " +
                                            " `grupo` = " + sccGrupo + ", " +
                                            " `TipoSolicitudId` = " + sccbTiposolicitud + ", " +
                                            " `SubTipoSolicitudId` = " + sccbSubtiposolicitud + ", " +
                                            " `CasoDisenoClasificacion` = " + sccbCalsodiseno + ", " +
                                            " `diasprorroga` = " + sDiasprorroga + ", " +
                                            " `mesesprorroga` = " + sMEsesprorroga + " " +
                                            " WHERE `plazos_de_oficioid` = " + tbTipoplazoid.Text + ";";

                    MySqlDataReader resp_plazos = con_plazos.getdatareader(squery_plazos);
                    if (resp_plazos.RecordsAffected > 0)
                    {
                        MessageBox.Show("Tipo plazo modificado correctamente.");
                        
                        tbTipoplazoid.Text = "";
                        cbSubtipodocumentoid.Text = "";
                        cbTipo_plazo.Text = "";
                        tbDias.Text = "";
                        tbMeses.Text = "";

                        //nuevos filtros agregados
                        cbGrupoCasos.Text = "";
                        cbTiposolicitud.Text = "";
                        cbSubtiposolicitud.Text = "";
                        cbCalsodiseno.Text = "";
                        tbDiasprorroga.Text = "";
                        tbMesesprorroga.Text = "";

                        chAviso.Checked = false;

                        cargartiposplazos();
                        btnAgregar.Enabled = true;
                        btnModificar.Enabled = false;
                        btnLimpiar.Enabled = false;

                    }
                    else
                    {
                        MessageBox.Show("Verifica la información.");
                    }
                    resp_plazos.Close();
                    con_plazos.Cerrarconexion();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar todos los campos para poder modificar.");
                }
            }
            catch (Exception exs)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //obtenemos los valores del front para poder modificarlos
                if (dgvTioplazos.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Seguro que desea eliminar el Registro con descripción: \n\n" +
                                                        dgvTioplazos.SelectedRows[0].Cells[3].Value.ToString() +" Genera -->"+ 
                                                        dgvTioplazos.SelectedRows[0].Cells[5].Value.ToString()+
                                                        "?", "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result.Equals(DialogResult.OK))
                    {
                        conect con_plazos = new conect();
                        String squery_plazos = " delete from `plazos_de_oficio`" +
                                                " WHERE `plazos_de_oficioid` = '" + dgvTioplazos.SelectedRows[0].Cells[0].Value.ToString() + "';";

                        MySqlDataReader resp_plazos = con_plazos.getdatareader(squery_plazos);
                        if (resp_plazos.RecordsAffected > 0)
                        {
                            MessageBox.Show("Comportamiento eliminado correctamente.");
                            cargartiposplazos();
                            limpiar();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un registro de la lista.");
                }
            }
            catch (Exception exs)
            {
                new filelog("", "" + exs.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        public void limpiar() {
            try
            {
                tbTipoplazoid.Text = "";
                cbSubtipodocumentoid.Text = "";
                cbTipo_plazo.Text = "";
                cbTipodocumento.Text = "";
                cbgrupo_plazo.Text = "";

                cbGrupoCasos.Text = "";
                cbTiposolicitud.Text = "";
                cbSubtiposolicitud.Text = "";
                cbCalsodiseno.Text = "";

                tbDiasprorroga.Text = "";
                tbMesesprorroga.Text = "";

                tbDias.Text = "";
                tbMeses.Text = "";
                btnLimpiar.Enabled = false;
                btnModificar.Enabled = false;
                btnAgregar.Enabled = true;
            }
            catch (Exception exs)
            {
            }
        }

        private void dgvTioplazos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbGrupoCasos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //buscamos las tiposolicitud dependiento el grupo
            try {
                //seleccionamos las opciones de Tiposolicitud
                conect con_tiosol = new conect();
                String squery_tiosol_ = "select * from tiposolicitud where TipoSolicitudGrupo = " + (cbGrupoCasos.SelectedItem as ComboboxItem).Value;
                MySqlDataReader resp_tiosol = con_tiosol.getdatareader(squery_tiosol_);
                cbTiposolicitud.Items.Clear();
                while (resp_tiosol.Read())
                {
                    cbTiposolicitud.Items.Add(funcionesgenerales.validareader("TipoSolicitudDescrip", "TipoSolicitudId", resp_tiosol));
                }
                resp_tiosol.Close();
                con_tiosol.Cerrarconexion();
            }
            catch (Exception exs) {
                new filelog("Error", "al cargar tiposolicitud");
            }
        }

        private void cbTiposolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //seleccionamos las opciones de Tiposolicitud
                conect con_subtiposol = new conect();
                String squery_subtiposol = "select * from subtiposolicitud where TipoSolicitudId =" + (cbTiposolicitud.SelectedItem as ComboboxItem).Value; ;
                MySqlDataReader resp_subtiposol = con_subtiposol.getdatareader(squery_subtiposol);
                cbSubtiposolicitud.Items.Clear();
                while (resp_subtiposol.Read())
                {
                    cbSubtiposolicitud.Items.Add(funcionesgenerales.validareader("SubTipoSolicitudDescripcion", "SubTipoSolicitudId", resp_subtiposol));
                }
                resp_subtiposol.Close();
                con_subtiposol.Cerrarconexion();
            }
            catch (Exception exs)
            {
                new filelog("Error", "al cargar tiposolicitud");
            }
        }
    }
}
