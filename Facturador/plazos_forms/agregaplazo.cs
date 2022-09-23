using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturador.plazos_forms
{
    public partial class agregaplazo : Form
    {
        funcionesdicss funcionesgenerales = new funcionesdicss();
        public String sgCasoid = "";
        public String sgTiposolicitudid = "";
        public String sgCasonumero = "";
        public String sgPlazosid = "";
        public String sgUsuarioid = "";
        public String sInsertrespuesta = "";
        public String ParejaId;
        public String IdGrupoPlazo;
        public String SubtipoSolicitud;
        public int ipGrupo = 0;
        public String Tipo;
        public agregaplazo(String sCasoid, String sTiposolicitudid, String sCasonumero, String sUsuarioid, int iGrupo)
        {//deberemos recibir el casoid, tiposolicitudid, casonumero, usuarioidformloguin
            //cone estos datos debe obtener 
            InitializeComponent();
            sgCasoid = sCasoid;
            //SubtipoSolicitud = gSSubTipoSolicitudId;
            sgTiposolicitudid = sTiposolicitudid;
            sgCasonumero = sCasonumero;
            sgUsuarioid = sUsuarioid;
            ipGrupo = iGrupo;

        }

        private void agregaplazo_Load(object sender, EventArgs e)
        {
            try{
                //inicializamos el número del caso 
                tbCasonumero.Text = sgCasonumero;
                //dgPlazos
                conect con_plazos = new conect();
                String squery_plazos = " select * from plazo_general_vista " +
                                         "where casoid = " + sgCasoid +
                                         " and TipoSolicitudId = " + sgTiposolicitudid;
                MySqlDataReader resp_plazos = con_plazos.getdatareader(squery_plazos);
                while (resp_plazos.Read())
                {
                    ComboboxItem cbItme = new ComboboxItem();
                    cbItme.Value =  funcionesgenerales.validareader("Plazosid", "Plazosid", resp_plazos).ToString();
                    cbItme.Text = sgCasoid + "-" + funcionesgenerales.validareader("Tipo_plazo_IMPI", "Tipo_plazo_IMPI", resp_plazos).ToString()+ " Doc: "+ funcionesgenerales.validareader("Documento", "Documento", resp_plazos).ToString();
                    cbPlazosdelcaso.Items.Add(cbItme);
                }
                resp_plazos.Close();
                con_plazos.Cerrarconexion();
                String srfecha = "4";
                rfecha.Text = srfecha;
                if (sgTiposolicitudid == "1" || sgTiposolicitudid == "2")
                {
                    conect cons = new conect();
                    String querys = "select * from tipoplazo where TipoPlazoId in (6,4)";

                    MySqlDataReader respuestastrings = cons.getdatareader(querys);
                    while (respuestastrings.Read())
                    {
                        cbTipo_plazo.Items.Add(validareader("TipoPlazoDescrip", "TipoPlazoId", respuestastrings));
                    }
                    respuestastrings.Close();
                    cons.Cerrarconexion();
                }
                else if (sgTiposolicitudid == "3" || sgTiposolicitudid == "4")
                {
                    //conect cons2 = new conect();
                    //String querys2 = "select * from tipoplazo where TipoPlazoId in (40,4)";
                    //MySqlDataReader respuestastrings2 = cons2.getdatareader(querys2);
                    //while (respuestastrings2.Read())
                    //{
                    //cbTipo_plazo.Items.Add(validareader("TipoPlazoDescrip", "TipoPlazoId", respuestastrings2));
                    //}
                    //respuestastrings2.Close();
                    //cons2.Cerrarconexion();
                    conect cons2 = new conect();
                    String querys2 = "select * from caso_patente where CasoId =" + sgCasoid;
                    MySqlDataReader respuestastrings2 = cons2.getdatareader(querys2);
                    while (respuestastrings2.Read())
                    {
                    Tipo = validareader("CasoDisenoClasificacion", "CasoId", respuestastrings2).Text;
                    }
                    respuestastrings2.Close();
                    cons2.Cerrarconexion();

                    if (Tipo =="5") {
                        conect cons = new conect();
                        String querys = "select * from tipoplazo where TipoPlazoId in (40)";

                        MySqlDataReader respuestastrings = cons.getdatareader(querys);
                        while (respuestastrings.Read())
                        {
                            cbTipo_plazo.Items.Add(validareader("TipoPlazoDescrip", "TipoPlazoId", respuestastrings));
                        }
                        respuestastrings.Close();
                        cons.Cerrarconexion();
                    }
                    else
                    {
                        conect cons10 = new conect();
                        String Resultado;
                        String FechaVencimientoPareja = "0000-00-00";
                        String querys10 = "select * from anualidad  where TipoSolicitudId in (3,4) and AnualidadSecuencia in(14,15) and AnualidadFechaPago ='" + FechaVencimientoPareja +"' and CasoID=" +sgCasoid;
                        MySqlDataReader respuestastrings10 = cons10.getdatareader(querys10);


                        if (respuestastrings10.Read()) { 
                            conect cons = new conect();
                            String querys = "select * from tipoplazo where TipoPlazoId in (4)";

                            MySqlDataReader respuestastrings = cons.getdatareader(querys);
                            while (respuestastrings.Read())
                            {
                                cbTipo_plazo.SelectedIndex = cbTipo_plazo.Items.Add(validareader("TipoPlazoDescrip", "TipoPlazoId", respuestastrings));
                            }
                            respuestastrings.Close();
                            cons.Cerrarconexion();
                        }
                        else
                        {
                            conect cons = new conect();
                            String querys = "select * from tipoplazo where TipoPlazoId in (40)";

                            MySqlDataReader respuestastrings = cons.getdatareader(querys);
                            while (respuestastrings.Read())
                            {
                                cbTipo_plazo.SelectedIndex = cbTipo_plazo.Items.Add(validareader("TipoPlazoDescrip", "TipoPlazoId", respuestastrings));
                            }
                            respuestastrings.Close();
                            cons.Cerrarconexion();
                        }
                        respuestastrings10.Close();
                        cons10.Cerrarconexion();
                    }
                }
                else
                {
                    conect cons = new conect();
                    String querys = "select * from tipoplazo where TipoPlazoId in (4)";

                    MySqlDataReader respuestastrings = cons.getdatareader(querys);
                    while (respuestastrings.Read())
                    {
                        cbTipo_plazo.Items.Add(validareader("TipoPlazoDescrip", "TipoPlazoId", respuestastrings));
                    }
                    respuestastrings.Close();
                    cons.Cerrarconexion();
                }

                //dgdocumentos
                conect con_docs = new conect();
                String squery_docs = " SELECT " +
                                            "     documento.DocumentoCodigoBarras," +
                                            "     documento.Documentoid," +
                                            "     documento.SubTipoDocumentoId," +
                                            "     DATE_FORMAT(documento.DocumentoFecha, '%d-%m-%Y') AS DocumentoFecha," +
                                            "     documento.DocumentoFolio," +
                                            "     DATE_FORMAT(documento.DocumentoFechaRecepcion," +
                                            "             '%d-%m-%Y') AS DocumentoFechaRecepcion," +
                                            "     DATE_FORMAT(documento.DocumentoFechaVencimiento," +
                                            "             '%d-%m-%Y') AS DocumentoFechaVencimiento," +
                                            "     DATE_FORMAT((documento.DocumentoFechaVencimiento + INTERVAL 1 MONTH)," +
                                            "             '%d-%m-%Y') AS DocumentoFechaVencimiento3meses," +
                                            "     DATE_FORMAT((documento.DocumentoFechaVencimiento + INTERVAL 2 MONTH)," +
                                            "             '%d-%m-%Y') AS DocumentoFechaVencimiento4meses," +
                                            "     DATEDIFF(CURDATE()," +
                                            "             documento.DocumentoFechaRecepcion) AS diasfiff," +
                                            "     DATE_FORMAT(documento.DocumentoFechaCaptura," +
                                            "             '%d-%m-%Y') AS DocumentoFechaCaptura," +
                                            "     DATE_FORMAT(documento.DocumentoFechaEscaneo," +
                                            "             '%d-%m-%Y') AS DocumentoFechaEscaneo," +
                                            "     Dame_estatus_docid(documento.documentoid) As Estatus_doc, " +
                                            "     documento.DocumentoObservacion," +
                                            "     documento.DocumentoIdRef," +
                                            "     documento.UsuarioId," +
                                            "     relaciondocumento.RelacionDocumentoLink," +
                                            "     relaciondocumento.casoid," +
                                            "     GET_SUBTIPODOCUMENTO(documento.SubTipoDocumentoId) AS subtipodocumento," +
                                            "     GET_TIPODOCUMENTO(documento.SubTipoDocumentoId) AS TipoDocumentoDescrip," +
                                            "     GET_USUARIO(documento.UsuarioId) AS Nombreusuario," +
                                            "     SubTipoDocumento.SubTipoDocumentoIndProrrogable" +
                                            " FROM" +
                                            "     documento," +
                                            "     relaciondocumento," +
                                            "     SubTipoDocumento" +
                                            " WHERE" +
                                            "     documento.DocumentoId = relaciondocumento.DocumentoId" +
                                            "         AND relaciondocumento.CasoId = " + sgCasoid + " " +
                                            "         AND relaciondocumento.TipoSolicitudId = " + sgTiposolicitudid + "" +
                                            "         AND SubTipoDocumento.SubTipoDocumentoId = documento.SubTipoDocumentoId;";
                MySqlDataReader resp_docs = con_docs.getdatareader(squery_docs);
                while (resp_docs.Read())
                {
                    ComboboxItem cbItme = new ComboboxItem();
                    cbItme.Value = funcionesgenerales.validareader("Documentoid", "Documentoid", resp_docs).ToString();
                    cbItme.Text = funcionesgenerales.validareader("DocumentoCodigoBarras", "DocumentoCodigoBarras", resp_docs).ToString() 
                        + " Doc: " + funcionesgenerales.validareader("TipoDocumentoDescrip", "TipoDocumentoDescrip", resp_docs).ToString()+
                        " / "+ funcionesgenerales.validareader("subtipodocumento", "subtipodocumento", resp_docs).ToString();
                    cbDocumentos.Items.Add(cbItme);
                }
                resp_docs.Close();
                con_docs.Cerrarconexion();


                //seleccionamos las opciones de grupos
                conect con_grupo = new conect();
                String squery_grupo = "select * from grupo where grupoid = "+ ipGrupo+";";
                MySqlDataReader resp_grupo = con_grupo.getdatareader(squery_grupo);
                String sGrupodescript = "";//
                while (resp_grupo.Read())
                {
                    cbGrupo.Items.Add(funcionesgenerales.validareader("GrupoDescripcion", "GrupoId", resp_grupo));
                    sGrupodescript = funcionesgenerales.validareader("GrupoDescripcion", "GrupoId", resp_grupo).Text;
                }
                resp_grupo.Close();
                con_grupo.Cerrarconexion();

                cbGrupo.Text = sGrupodescript;
                //FIN seleccionamos las opciones de grupos

                //seleccionamos las opciones de grupos
                //conect con_grupo_plazo = new conect();
                //String squery_grupo_plazo = "SELECT * FROM grupoplazo;";
                //MySqlDataReader resp_grupo_plazo = con_grupo_plazo.getdatareader(squery_grupo_plazo);
                ////GrupoId
                //while (resp_grupo_plazo.Read())
                //{
                //    cbgrupo_plazo.Items.Add(funcionesgenerales.validareader("GrupoPlazoDescripcion", "GrupoPlazoId", resp_grupo_plazo));
                //}
                //resp_grupo_plazo.Close();
                //con_grupo_plazo.Cerrarconexion();

                conect con_grupo_plazo = new conect();
                String squery_grupo_plazo = "Select * from grupoplazo where GrupoId = " + ipGrupo + ";";// and Grupoid = " + cGrupoplazo_Seleccionado.Value + ";";
                MySqlDataReader resp_grupo_plazo = con_grupo_plazo.getdatareader(squery_grupo_plazo);
                cbgrupo_plazo.Items.Clear();
                while (resp_grupo_plazo.Read())
                {
                    cbgrupo_plazo.Items.Add(funcionesgenerales.validareader("GrupoPlazoDescripcion", "GrupoPlazoId", resp_grupo_plazo));
                }
                resp_grupo_plazo.Close();
                con_grupo_plazo.Cerrarconexion();

                //String squery_grupo_plazo = "Select * from tipoplazo where GrupoPlazoId = " + cGrupoplazo_Seleccionado.Value + ";";// and Grupoid = " + (cbGrupo.SelectedItem as ComboboxItem).Value + ";";
                //FIN seleccionamos las opciones de grupos
                //
                //Seleccionamos las opciones para los estados posibles para plazos
                conect con_estatus_plazo = new conect();
                String squery_estatus_plazo = "select * from estatusplazo;";
                MySqlDataReader resp_estatus_plazo = con_estatus_plazo.getdatareader(squery_estatus_plazo);
                while (resp_estatus_plazo.Read())
                {
                    cbEstadosplazos.Items.Add(funcionesgenerales.validareader("EstatusPlazoDescrip", "EstatusPlazoId", resp_estatus_plazo));
                }
                resp_estatus_plazo.Close();
                con_estatus_plazo.Cerrarconexion();

                conect con_estatus_plazo2 = new conect();
                String squery_estatus_plazo2 = "select * from estatusplazo ";
                MySqlDataReader resp_estatus_plazo2 = con_estatus_plazo2.getdatareader(squery_estatus_plazo2);
                while (resp_estatus_plazo2.Read())
                {
                 EstatusIdPareja.Items.Add(funcionesgenerales.validareader("EstatusPlazoDescrip", "EstatusPlazoId", resp_estatus_plazo2));
                }
                resp_estatus_plazo2.Close();
                con_estatus_plazo2.Cerrarconexion();
            } catch (Exception exs) { 
            }
        }

        private void cbGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //cbTipo_plazo.Text = "";
                //ComboboxItem cGrupo_Seleccionado = (cbGrupo.SelectedItem as ComboboxItem);
                //ComboboxItem cGrupoplazo_Seleccionado = (cbgrupo_plazo.SelectedItem as ComboboxItem);
                //if (cGrupo_Seleccionado != null && cGrupoplazo_Seleccionado != null)
                //{
                //    //seleccionamos las opciones de grupos
                //    conect con_grupo_plazo = new conect();
                //    String squery_grupo_plazo = "Select * from tipoplazo where GrupoPlazoId = " + cGrupoplazo_Seleccionado.Value + ";";// and Grupoid = " + cGrupoplazo_Seleccionado.Value + ";";
                //    MySqlDataReader resp_grupo_plazo = con_grupo_plazo.getdatareader(squery_grupo_plazo);
                //    cbTipo_plazo.Enabled = true;
                //    cbTipo_plazo.Items.Clear();
                //    while (resp_grupo_plazo.Read())
                //    {
                //        cbTipo_plazo.Items.Add(funcionesgenerales.validareader("TipoPlazoDescrip", "TipoPlazoId", resp_grupo_plazo));
                //    }
                //    resp_grupo_plazo.Close();
                //    con_grupo_plazo.Cerrarconexion();
                //}
                //else
                //{
                //    cbTipo_plazo.Enabled = false;
                //    cbTipo_plazo.Items.Clear();
                //}x


                /*modificamos el combobox de grupoplazo*/
                //seleccionamos las opciones de grupos
                conect con_grupo_plazo = new conect();
                String squery_grupo_plazo = "Select * from grupoplazo where GrupoId = " + ipGrupo + ";";// and Grupoid = " + cGrupoplazo_Seleccionado.Value + ";";
                MySqlDataReader resp_grupo_plazo = con_grupo_plazo.getdatareader(squery_grupo_plazo);
                while (resp_grupo_plazo.Read())
                {
                    cbgrupo_plazo.Items.Add(funcionesgenerales.validareader("GrupoPlazoDescripcion", "GrupoPlazoId", resp_grupo_plazo));
                }
                resp_grupo_plazo.Close();
                con_grupo_plazo.Cerrarconexion();

                /**/
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
          /*  try
            {
                cbTipo_plazo.Text = "";
                ComboboxItem cGrupo_Seleccionado = (cbGrupo.SelectedItem as ComboboxItem);
                ComboboxItem cGrupoplazo_Seleccionado = (cbgrupo_plazo.SelectedItem as ComboboxItem);
                if (cGrupo_Seleccionado != null && cGrupoplazo_Seleccionado != null)
                {
                    //seleccionamos las opciones de grupos
                    conect con_grupo_plazo = new conect();
                    String squery_grupo_plazo = "Select * from tipoplazo where GrupoPlazoId = " + cGrupoplazo_Seleccionado.Value + ";";// and Grupoid = " + (cbGrupo.SelectedItem as ComboboxItem).Value + ";";
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
            }*/
        }

        private void agregaplazo_FormClosing(object sender, FormClosingEventArgs e)
        {
            //aqui cerramos
            sInsertrespuesta = "cancel";
        }

        private void tbFechadVencimineto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }


            if (tbFechadVencimineto.Text.Length == 2)
            {
                tbFechadVencimineto.Text = tbFechadVencimineto.Text + "-";
                tbFechadVencimineto.SelectionStart = tbFechadVencimineto.Text.Length;

            }
            if (tbFechadVencimineto.Text.Length == 5)
            {
                tbFechadVencimineto.Text = tbFechadVencimineto.Text + "-";
                tbFechadVencimineto.SelectionStart = tbFechadVencimineto.Text.Length;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }


            if (tbFechaatendioplazo.Text.Length == 2)
            {
                tbFechaatendioplazo.Text = tbFechaatendioplazo.Text + "-";
                tbFechaatendioplazo.SelectionStart = tbFechaatendioplazo.Text.Length;

            }
            if (tbFechaatendioplazo.Text.Length == 5)
            {
                tbFechaatendioplazo.Text = tbFechaatendioplazo.Text + "-";
                tbFechaatendioplazo.SelectionStart = tbFechaatendioplazo.Text.Length;
            }
        }

        private void tbFechadVencimineto_Validating(object sender, CancelEventArgs e)
        {
            funcionesgenerales.validafecha(tbFechadVencimineto);
        }

        private void tbFechaatendioplazo_Validating(object sender, CancelEventArgs e)
        {
            funcionesgenerales.validafecha(tbFechaatendioplazo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //aqui validaremos todos los datos para hacer el insert dentro del caso
            try
            {
                if (cbTipo_plazo.SelectedItem is null || cbEstadosplazos.SelectedItem is null || tbCasonumero.Text == "")
                { //validmos los campos obligtorios para agregar un plazodetalle
                    MessageBox.Show("Debe llenar los campos obligatorios para poder agregar un plazo.");
                    return;
                }
                if ((cbEstadosplazos.SelectedItem as ComboboxItem).Value.ToString() != "1")
                {
                    if (tbFechadVencimineto.Text == "" || FechaVencimientoPareja.Text == "")
                    {
                        MessageBox.Show("No puede tener el  Campo Fecha vacio, favor de colocar una fecha");
                        return;
                    }
                }
                if ((cbEstadosplazos.SelectedItem as ComboboxItem).Value.ToString() != (EstatusIdPareja.SelectedItem as ComboboxItem).Value.ToString())
                {
                    MessageBox.Show("El estado del plazo no puede ser diferente, por favor corrigelo");
                    return;
                }
                //una vez validado que todos los campos estén llenos entonces pasamos a tomarlos para poder insertarlos
                String sPlazsoid = "", sgrupopatentes="", sfechanotificacionpareja="", sFechaAtendioPlazoPareja="", sEstatusIdPareja = "", sFechavencimientoPareja ="", sTipoPlazoPareja ="", sDocumentoid = "", sTipoplzadoid = "", sEstatusid = "", sFechavencimiento = "", sFechaatendio = "", sAnualidadid = "", sFechanotificacion ="", sFechaAtendioPlazo = "", sFechaVencimientoPareja ="";

                sTipoplzadoid = (cbTipo_plazo.SelectedItem as ComboboxItem).Value.ToString();
                sEstatusid = (cbEstadosplazos.SelectedItem as ComboboxItem).Value.ToString();
                sEstatusIdPareja = (EstatusIdPareja.SelectedItem as ComboboxItem).Value.ToString();//Segundo group
                sFechavencimiento = tbFechadVencimineto.Text;
                sFechaatendio = tbFechaatendioplazo.Text;
                sAnualidadid = tbAnualidadid.Text;
                sFechanotificacion = tbFechaNotificacion.Text;
                sfechanotificacionpareja = fechanotificacionpareja.Text;
                sFechavencimientoPareja = FechaVencimientoPareja.Text;
                sFechaAtendioPlazo = FechaAtendioPlazo.Text;
                sFechaAtendioPlazoPareja = FechaAtendioPlazoPareja.Text;
                if (sFechaAtendioPlazoPareja != "" && sFechaAtendioPlazo !="")
                {
                    var fecha1 = DateTime.Parse(sFechaAtendioPlazoPareja);
                    var fecha2 = DateTime.Parse(sFechaAtendioPlazo);
                    if (fecha1 > DateTime.Today)
                    {
                        MessageBox.Show("No puede tener una fecha vencimiento Pareja mayor a la fecha actual");
                        return;
                    }
                    if (fecha2 > DateTime.Today)
                    {
                        MessageBox.Show("No puede tener una fecha Atendio Plazo mayor a la fecha actual");
                        return;
                    }
                }

                sTipoPlazoPareja = (TipoPlazoPareja.SelectedItem as ComboboxItem).Value.ToString();// Segundo group
                //sgrupopatentes = (grupopatentes.SelectedItem as ComboboxItem).Value.ToString();//Segundo group
                sFechaVencimientoPareja = FechaVencimientoPareja.Text;//Segundo group
                //Validamos que la fecha atendio Pareja sea menor o igual a la fecha actual

                //Validamos que la fecha atendio sea menor o igual a la fecha actual

                if (!(cbDocumentos.SelectedItem is null))
                {//si no es null entonces lo tomamos del combobox
                    sDocumentoid = (cbDocumentos.SelectedItem as ComboboxItem).Value.ToString();    
                }
                
                //el primer dato que validaremos es si está relacionado con otro plazo
                if (cbPlazosdelcaso.SelectedItem is null)
                {
                    //agregamos un insert relacionando el casoid y el tiposolicituid en la tabla plazos
                    //y tomamos el id plazosid para insertarlo en plazos_detalle
                    sPlazsoid = sInsertaplazosNuevo(sgCasoid, sgTiposolicitudid);
                }
                else
                {
                    //ya exste plazosid por lo que lo tomamos del comobobox y sólo insertamos en plazos_detalle
                    sPlazsoid = (cbPlazosdelcaso.SelectedItem as ComboboxItem).Value.ToString();
                }
                //validamos que los datos que son opcionales tengan un valor correcto como usuarios y fechas ademas de cambiar el formato de las fechas
                if (sDocumentoid == "")//validamos el documentoid
                {
                    sDocumentoid = "null";
                }
                else {
                    sDocumentoid = "'"+sDocumentoid+"'";
                }


                if (sAnualidadid == "")//validamos el documentoid
                {
                    sAnualidadid = "null";
                }
                else
                {
                    sAnualidadid = "'" + sAnualidadid + "'";
                }
                ////validamos el sFechavencimiento
                //if (sFechavencimiento == "")
                //{
                //    sFechavencimiento = "null";
                //}
                //else
                //{
                //    String sFechaformatocrrecto = DateTime.ParseExact(sFechavencimiento, "dd-MM-yyyy", 
                //                                    CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd");
                //    sFechavencimiento = "'" + sFechaformatocrrecto + "'";
                //}


                ////validamos el sFechaatendio
                //if (sFechaatendio == "")
                //{
                //    sFechaatendio = "null";
                //}
                //else
                //{
                //    String sFechaformatocrrecto = DateTime.ParseExact(sFechaatendio, "dd-MM-yyyy",
                //                                    CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd");
                //    sFechaatendio = "'" + sFechaformatocrrecto + "'";
                //}
                // Fechas del primer group by
                sFechanotificacion = sValidafechaparainsert(sFechanotificacion);
                sFechavencimiento = sValidafechaparainsert(sFechavencimiento);
                sFechaatendio = sValidafechaparainsert(sFechaatendio);
                sFechaAtendioPlazoPareja = sValidafechaparainsert(sFechaAtendioPlazoPareja);
                sFechaAtendioPlazo = sValidafechaparainsert(sFechaAtendioPlazo);
                sFechavencimientoPareja = sValidafechaparainsert(sFechavencimientoPareja);
                sfechanotificacionpareja = sValidafechaparainsert(sfechanotificacionpareja);
                //Fechas del segundo group by



                //hasta aquí ya tenemos el dato del plazosid con el que va relacionado y quien nos dice a que tipos de caso y caso pertenece
                //ahora insertaremos todos los datos disponibles en el formulario y el plazosid ya sea nuevo o relacionado con uno antiguo
                conect con_grupo_plazo = new conect();
                String squery_grupo_plazo = " INSERT INTO `plazos_detalle` " +
                                                        " (`Plazosid`, " +
                                                        " `documentoid`, " +
                                                        " `usuario_creo_plazodetalle`, " +
                                                        " `Tipo_plazoid`, " +
                                                        " `Estatus_plazoid`, " +
                                                        " `Fecha_notificacion`, " +
                                                        " `Fecha_Vencimiento`, " +
                                                        " `Fecha_atendio_plazo`, " +
                                                        " `AnualidadId`) " +
                                                        " VALUES " +
                                                        " ( " +
                                                        " '" + sPlazsoid + "', " +
                                                        " " + sDocumentoid + ", " +
                                                        " '" + sgUsuarioid + "', " +
                                                        " '" + sTipoplzadoid + "', " +
                                                        " '" + sEstatusid + "', " +
                                                        " " + sFechanotificacion + ", " +
                                                        " " + sFechavencimiento + ", " +
                                                        " " + sFechaAtendioPlazo + ", " +
                                                        " " + sAnualidadid + " " +
                                                        " ); ";
                MySqlDataReader resp_grupo_plazo = con_grupo_plazo.getdatareader(squery_grupo_plazo);
                resp_grupo_plazo.Read();
                if (resp_grupo_plazo.RecordsAffected > 0)
                {
                    MessageBox.Show(" Primer Plazo agreado correctamente.");
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                resp_grupo_plazo.Close();
                con_grupo_plazo.Cerrarconexion();
                conect con_grupo_plazo2 = new conect();
                String squery_grupo_plazo2 = " INSERT INTO `plazos_detalle` " +
                                                        " (`Plazosid`, " +
                                                        " `documentoid`, " +
                                                        " `usuario_creo_plazodetalle`, " +
                                                        " `Tipo_plazoid`, " +
                                                        " `Estatus_plazoid`, " +
                                                        " `Fecha_notificacion`, " +
                                                        " `Fecha_Vencimiento`, " +
                                                        " `Fecha_atendio_plazo`, " +
                                                        " `AnualidadId`) " +
                                                        " VALUES " +
                                                        " ( " +
                                                        " '" + sPlazsoid + "', " +
                                                        " " + sDocumentoid + ", " +
                                                        " '" + sgUsuarioid + "', " +
                                                        " '" + sTipoPlazoPareja + "', " +
                                                        " '" + sEstatusIdPareja + "', " +
                                                        " " + sfechanotificacionpareja + ", " +
                                                        " " + sFechavencimientoPareja + ", " +
                                                        " " + sFechaAtendioPlazoPareja + ", " +
                                                        " " + sAnualidadid + " " +
                                                        " ); ";
                MySqlDataReader resp_grupo_plazo2 = con_grupo_plazo2.getdatareader(squery_grupo_plazo2);
                resp_grupo_plazo2.Read();
                if (resp_grupo_plazo2.RecordsAffected > 0)
                {
                    MessageBox.Show(" Segundo Plazo agreado correctamente.");
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                resp_grupo_plazo2.Close();
                con_grupo_plazo2.Cerrarconexion();
            }
            catch (Exception exs){
                new filelog("excepcion en insertar plazodetalleid", " mensaje: "+exs.Message);
                sInsertrespuesta = "cancel";
            }
            //Empieza el insert para el segundo grupo de campos

        }

        public String sValidafechaparainsert(String sFechaddmmyyyy) {
            String sFechaatendio = "null";
            try {
                String sFechaformatocrrecto = DateTime.ParseExact(sFechaddmmyyyy, "dd-MM-yyyy",
                CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd");
                sFechaatendio = "'" + sFechaformatocrrecto + "'";
            }
            catch (Exception exs) { 

            }
            
            return sFechaatendio;
        }

        public String sInsertaplazosNuevo(String sCasoid, String sTiposolicitudid) {
            String sRespuestaplazosid = "";
            try {
                //seleccionamos las opciones de grupos
                conect con_grupo_plazo = new conect();
                String squery_grupo_plazo = "INSERT INTO `plazos`(`CasoId`,`TipoSolicitudId`,`Fecha_creacion`)VALUES('"+sgCasoid+"','"+sgTiposolicitudid+"',now());";
                MySqlDataReader resp_grupo_plazo = con_grupo_plazo.getdatareader(squery_grupo_plazo);
                resp_grupo_plazo.Read();
                if(resp_grupo_plazo.RecordsAffected > 0){ //si inserto correctamente entonces consultamos el ultimo plazo insertado para mandarlo comoretorno
                    conect con_grupo_plazo_consultaid = new conect();
                    String squery_grupo_plazo_consulta = "select * from plazos order by Plazosid desc limit 1;";
                    MySqlDataReader resp_grupo_plazo_consulta = con_grupo_plazo_consultaid.getdatareader(squery_grupo_plazo_consulta);
                    resp_grupo_plazo_consulta.Read();
                    sRespuestaplazosid = funcionesgenerales.validareader("Plazosid", "Plazosid", resp_grupo_plazo_consulta).Text;

                    resp_grupo_plazo_consulta.Close();
                    con_grupo_plazo_consultaid.Cerrarconexion();

                }
                resp_grupo_plazo.Close();
                con_grupo_plazo.Cerrarconexion();
            } catch (Exception exs) {
                sRespuestaplazosid = "";
            }
            return sRespuestaplazosid;


        }

        private void tbFechaNotificacion_Validating(object sender, CancelEventArgs e)
        {
            funcionesgenerales.validafecha(tbFechaNotificacion);
        }

        private void tbFechaNotificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }else{
                e.Handled = true;
            }


            if (tbFechaNotificacion.Text.Length == 2)
            {
                tbFechaNotificacion.Text = tbFechaNotificacion.Text + "-";
                tbFechaNotificacion.SelectionStart = tbFechaNotificacion.Text.Length;

            }
            if (tbFechaNotificacion.Text.Length == 5)
            {
                tbFechaNotificacion.Text = tbFechaNotificacion.Text + "-";
                tbFechaNotificacion.SelectionStart = tbFechaNotificacion.Text.Length;
            }
        }

        public ComboboxItem validareader(String campoText, String campoValue, MySqlDataReader mresultado)
        {
            ComboboxItem cItemresult = new ComboboxItem();
            try
            {

                if (!mresultado.IsDBNull(mresultado.GetOrdinal(campoText)))
                {
                    cItemresult.Text = mresultado.GetString(mresultado.GetOrdinal(campoText));
                }
                else
                {
                    cItemresult.Text = "";
                }

                if (!mresultado.IsDBNull(mresultado.GetOrdinal(campoValue)))
                {
                    cItemresult.Value = mresultado.GetString(mresultado.GetOrdinal(campoValue));
                }
                else
                {
                    cItemresult.Value = "";
                }
            }
            catch (Exception Ex)
            {
                cItemresult.Text = "";
                cItemresult.Value = "";
            }

            return cItemresult;
        }


        private void cbTipo_plazo_SelectedIndexChanged(object sender, EventArgs e)
        {

            TipoPlazoPareja.Items.Clear();

            String combotipoplazo = (cbTipo_plazo.SelectedItem as ComboboxItem).Value.ToString();
            conect cons2 = new conect();
            String querys2 = "select * from tipoplazo where TipoPlazoId=" + combotipoplazo;
            MySqlDataReader respuestastrings2 = cons2.getdatareader(querys2);
            while (respuestastrings2.Read())
            {
                IdGrupoPlazo = validareader("ParejaId", "TipoPlazoId", respuestastrings2).Text;
                //TipoPlazoPareja.SelectedIndex = TipoPlazoPareja.Items.Add(validareader("TipoPlazoDescrip", "TipoPlazoId", respuestastrings2));
            }
            respuestastrings2.Close();
            cons2.Cerrarconexion();
            if (IdGrupoPlazo =="22")
            {
                TipoPlazoPareja.Items.Clear();
                TipoPlazoPareja.Text = "";
                conect cons3 = new conect();

                String querys3 = "select * from tipoplazo where TipoPlazoId in(" + IdGrupoPlazo +",13)";
                MySqlDataReader respuestastrings3 = cons3.getdatareader(querys3);
                while (respuestastrings3.Read())
                {
                    
                    this.TipoPlazoPareja.Enabled = true;
                    TipoPlazoPareja.Items.Add(validareader("TipoPlazoDescrip", "TipoPlazoId", respuestastrings3));
                }
                respuestastrings3.Close();
                cons3.Cerrarconexion();
            }
            else {
                TipoPlazoPareja.Items.Clear();
                conect cons3 = new conect();
                String querys3 = "select * from tipoplazo where TipoPlazoId in(" + IdGrupoPlazo + ")";
                MySqlDataReader respuestastrings3 = cons3.getdatareader(querys3);
                while (respuestastrings3.Read())
                {
                    this.TipoPlazoPareja.Enabled = false;
                    TipoPlazoPareja.SelectedIndex = TipoPlazoPareja.Items.Add(validareader("TipoPlazoDescrip", "TipoPlazoId", respuestastrings3));
                }
                respuestastrings3.Close();
                cons3.Cerrarconexion();
            }
            if (combotipoplazo == "6" || combotipoplazo == "4")
            {
                conect con_estatus_plazo = new conect();
                String squery_estatus_plazo = "select * from estatusplazo where EstatusPlazoId=1";
                MySqlDataReader resp_estatus_plazo = con_estatus_plazo.getdatareader(squery_estatus_plazo);
                while (resp_estatus_plazo.Read())
                {
                
                    cbEstadosplazos.SelectedIndex = cbEstadosplazos.Items.Add(funcionesgenerales.validareader("EstatusPlazoDescrip", "EstatusPlazoId", resp_estatus_plazo));
                }
                resp_estatus_plazo.Close();
                con_estatus_plazo.Cerrarconexion();
                conect con_estatus_plazo2 = new conect();
                String squery_estatus_plazo2 = "select * from estatusplazo where EstatusPlazoId=1";
                MySqlDataReader resp_estatus_plazo2 = con_estatus_plazo2.getdatareader(squery_estatus_plazo2);
                while (resp_estatus_plazo2.Read())
                {
                    EstatusIdPareja.SelectedIndex = EstatusIdPareja.Items.Add(funcionesgenerales.validareader("EstatusPlazoDescrip", "EstatusPlazoId", resp_estatus_plazo2));
                }
                resp_estatus_plazo2.Close();
                con_estatus_plazo2.Cerrarconexion();
            }
        }

        private void tbFechadVencimineto_TextChanged(object sender, EventArgs e)
        {

            try {
                String sFechavencimiento = "", sFechaVencimientoPareja = "";
                int srfecha = 0;

                sFechavencimiento = tbFechadVencimineto.Text;
                srfecha = Convert.ToInt32(rfecha.Text);
                var Fecha1 = DateTime.Parse(sFechavencimiento).AddMonths(-srfecha);
                sFechaVencimientoPareja = Fecha1.ToString("dd-MM-yyyy"); //Segundo Group
                FechaVencimientoPareja.Text = sFechaVencimientoPareja;
            }
            catch
            {
                if (rfecha.Text == "")
                {
                    MessageBox.Show("Debe llenar el campo Meses a restar" + "");
                }
                
            }

        }

        private void cbEstadosplazos_SelectedIndexChanged(object sender, EventArgs e)
        {
            conect con_estatus_plazo2 = new conect();
            String squery_estatus_plazo2 = "select * from estatusplazo where EstatusPlazoId =" + (cbEstadosplazos.SelectedItem as ComboboxItem).Value.ToString();
            MySqlDataReader resp_estatus_plazo2 = con_estatus_plazo2.getdatareader(squery_estatus_plazo2);
            while (resp_estatus_plazo2.Read())
            {
                EstatusIdPareja.SelectedIndex = EstatusIdPareja.Items.Add(funcionesgenerales.validareader("EstatusPlazoDescrip", "EstatusPlazoId", resp_estatus_plazo2));
            }
            resp_estatus_plazo2.Close();
            con_estatus_plazo2.Cerrarconexion();
            if ((cbEstadosplazos.SelectedItem as ComboboxItem).Value.ToString() == "1" )
            {
                tbFechaNotificacion.Visible = false;
                label16.Visible = false;
                FechaAtendioPlazo.Visible = false;
                label20.Visible = false;
                label26.Visible = false;
                FechaAtendioPlazoPareja.Visible = false;
                cbPlazosdelcaso.Visible = false;
                cbDocumentos.Visible = false;
                label10.Visible = false;
                label13.Visible = false;
            }
            else
            {
                tbFechaNotificacion.Visible = true;
                label16.Visible = true;
                FechaAtendioPlazo.Visible = true;
                label20.Visible = true;
                label26.Visible = true;
                FechaAtendioPlazoPareja.Visible = true;
                cbPlazosdelcaso.Visible = true;
                cbDocumentos.Visible = true;
                label10.Visible = true;
                label13.Visible = true;
            }
        }

        private void EstatusIdPareja_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cbEstadosplazos.SelectedItem as ComboboxItem).Value.ToString() != (EstatusIdPareja.SelectedItem as ComboboxItem).Value.ToString())
            {
                MessageBox.Show("El estado del plazo no puede ser diferente, por favor corrigelo");
                return;
            }
        }

        private void tbFechadVencimineto_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                String sFechavencimiento = "", sFechaVencimientoPareja = "";
                int srfecha = 0;

                sFechavencimiento = tbFechadVencimineto.Text;
                srfecha = Convert.ToInt32(rfecha.Text);
                var Fecha1 = DateTime.Parse(sFechavencimiento).AddMonths(-srfecha);
                sFechaVencimientoPareja = Fecha1.ToString("dd-MM-yyyy"); //Segundo Group
                FechaVencimientoPareja.Text = sFechaVencimientoPareja;
            }
            catch
            {
                if (rfecha.Text == "")
                {
                    MessageBox.Show("Debe llenar el campo Meses a restar" + "");
                }

            }
        }

        private void fechanotificacionpareja_Validating(object sender, CancelEventArgs e)
        {
            funcionesgenerales.validafecha(fechanotificacionpareja);
        }
    }
}
