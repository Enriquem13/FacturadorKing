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
        public int ipGrupo = 0;
        public agregaplazo(String sCasoid, String sTiposolicitudid, String sCasonumero, String sUsuarioid, int iGrupo)
        {//deberemos recibir el casoid, tiposolicitudid, casonumero, usuarioidformloguin
            //cone estos datos debe obtener 
            InitializeComponent();
            sgCasoid = sCasoid;
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
                    cbItme.Value = funcionesgenerales.validareader("Plazosid", "Plazosid", resp_plazos).ToString();
                    cbItme.Text = funcionesgenerales.validareader("Tipo_plazo_IMPI", "Tipo_plazo_IMPI", resp_plazos).ToString()+ " Doc: "+ funcionesgenerales.validareader("Documento", "Documento", resp_plazos).ToString();
                    cbPlazosdelcaso.Items.Add(cbItme);
                }
                resp_plazos.Close();
                con_plazos.Cerrarconexion();


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
            try
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
            }
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
                if (cbTipo_plazo.SelectedItem is null || cbEstadosplazos.SelectedItem is null || tbFechadVencimineto.Text == "" || tbCasonumero.Text == "")
                { //validmos los campos obligtorios para agregar un plazodetalle
                    MessageBox.Show("Debe llenar los campos obligatorios pra poder agregar un plazo.");
                    return;
                }
                //una vez validado que todos los campos estén llenos entonces pasamos a tomarlos para poder insertarlos
                String sPlazsoid = "", sDocumentoid = "", sTipoplzadoid = "", sEstatusid = "", sFechavencimiento = "", sFechaatendio = "", sAnualidadid = "", sFechanotificacion ="";
                sTipoplzadoid = (cbTipo_plazo.SelectedItem as ComboboxItem).Value.ToString();
                sEstatusid = (cbEstadosplazos.SelectedItem as ComboboxItem).Value.ToString();
                sFechavencimiento = tbFechadVencimineto.Text;
                sFechaatendio = tbFechaatendioplazo.Text;
                sAnualidadid = tbAnualidadid.Text;
                sFechanotificacion = tbFechaNotificacion.Text;

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

                sFechanotificacion = sValidafechaparainsert(sFechanotificacion);
                sFechavencimiento = sValidafechaparainsert(sFechavencimiento);
                sFechaatendio = sValidafechaparainsert(sFechaatendio);

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
                                                        " " + sFechaatendio + ", " +
                                                        " " + sAnualidadid + " " +
                                                        " ); ";
                MySqlDataReader resp_grupo_plazo = con_grupo_plazo.getdatareader(squery_grupo_plazo);
                resp_grupo_plazo.Read();
                if (resp_grupo_plazo.RecordsAffected > 0)
                {
                    MessageBox.Show("Plazo agreado correctamente.");
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                resp_grupo_plazo.Close();
                con_grupo_plazo.Cerrarconexion();
            }catch (Exception exs){
                new filelog("excepcion en insertar plazodetalleid", " mensaje: "+exs.Message);
                sInsertrespuesta = "cancel";
            }
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
    }
}
