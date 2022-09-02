using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturador
{
    public partial class Fatenderplazo : Form
    {
        funcionesdicss objfuncionesdicss = new funcionesdicss();
        String sgCasoid = "", sgCasonumero = "";
        String sgTiposolicitudid = "";
        Form1 fLogin;
        public Fatenderplazo(String Casoid, String Tiposolicitudid, Form1 login, String sCasonumero) {
            sgCasoid = Casoid;
            sgCasonumero = sCasonumero;
            sgTiposolicitudid = Tiposolicitudid;
            fLogin = login;

            InitializeComponent();

            conect con_2 = new conect();
            String sQuery_2 = "select distinct malcomx from subtipodocumento where TipoDocumentoId = 6;";
            MySqlDataReader resp_areaimpi_2 = con_2.getdatareader(sQuery_2);
            while (resp_areaimpi_2.Read())
            {
                if(objfuncionesdicss.validareader("malcomx", "malcomx", resp_areaimpi_2).Text != "null"){
                    cbCorreotipo.Items.Add(objfuncionesdicss.validareader("malcomx", "malcomx", resp_areaimpi_2));
                }
            }
            resp_areaimpi_2.Close();
            con_2.Cerrarconexion();


            

            //cbPlazoatender
            conect con_4 = new conect();
            String sQuery_4 = " SELECT  " +
                                " 	plazos.casoid, " +
                                "   plazos.TipoSolicitudId, " +
                                " 	plazos_detalle.Plazosid, " +
                                " 	plazos_detalle.Plazos_detalleid, " +
                                "   tipoplazos.TipoPlazoDescrip, " +
                                "   GET_SUBTIPODOCUMENTODESCRIP_FROM_DOCUMENTOID(plazos_detalle.documentoid) As documentodescrip " +
                                " FROM " +
                                "     plazos, " +
                                "     plazos_detalle, " +
                                "     tipoplazos " +
                                " WHERE " +
                                "     plazos.Plazosid = plazos_detalle.Plazosid " +
                                "     and plazos_detalle.Tipo_plazoid = tipoplazos.tipoplazosid " +
                                "     and Estatus_plazoid = 1 " +/*Pendiente*/
                                "     and tipoplazos.aviso = 1 " +/*Plazos de aviso*/
                                "     and plazos.casoid = " + Casoid +
                                "     and plazos.TipoSolicitudId = " + Tiposolicitudid +" ;";
            MySqlDataReader resp_plazos = con_4.getdatareader(sQuery_4);
            while (resp_plazos.Read())
            {
                ComboboxItem plazos = new ComboboxItem();
                plazos.Value = objfuncionesdicss.validareader("Plazos_detalleid", "Plazos_detalleid", resp_plazos).Value;
                plazos.Text = objfuncionesdicss.validareader("Plazosid", "Plazosid", resp_plazos).Text + "-" +//concatenamos el plazosid para poder usarlo al atender el plazo
                              objfuncionesdicss.validareader("TipoPlazoDescrip", "TipoPlazoDescrip", resp_plazos).Text + "(" +
                              objfuncionesdicss.validareader("documentodescrip", "documentodescrip", resp_plazos).Text + ")";
                cbPlazoatender.Items.Add(plazos);
            }
            resp_plazos.Close();
            con_4.Cerrarconexion();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (tbFechacorreo.Text == "")
            {
                if (tbFechacorreo.CanFocus)
                {
                    tbFechacorreo.Focus();
                }
                MessageBox.Show("Debe agregar una fecha.");
                return;
            }
            if (cbTipocorreo.Text == "")
            {
                MessageBox.Show("Debe seleccionar un tipo de correo.");
                if (cbTipocorreo.CanFocus)
                {
                    cbTipocorreo.Focus();
                }
                return;
            }


            if (tbNombredelarchivo.Text == "")
            {
                MessageBox.Show("Debe seleccionar un archivo.");
                if (tbNombredelarchivo.CanFocus)
                {
                    tbNombredelarchivo.Focus();
                }
                return;
            }

            try
            {
                //Validamos que el archivo no exista
                //File.Copy(sfilePath, sfilePath_2);
                if (File.Exists(@sfilePath_2))
                {
                    //Console.WriteLine("The file exists.");
                    MessageBox.Show("Archivo Repetido, Seleccione otro Documento ó cambie el nombre");
                    tbNombredelarchivo.Text = "";
                    button1.Focus();
                    return;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Archivo Repetido, Seleccione otro Documento ó cambie el nombre");
                tbNombredelarchivo.Text = "";
                button1.Focus();
                return;
            }

            try
            {
                this.Hide();
                confirmarnumerodecasodocs sConfirmanuermodecaso = new confirmarnumerodecasodocs();
                if (sConfirmanuermodecaso.ShowDialog() == DialogResult.OK)
                {
                    if (sConfirmanuermodecaso.sNumerocaso == sgCasonumero)
                    {

                        this.Show();
                        try
                        {
                            //debemos insertar lo que esta capturado pero dependioendo el valor insertado aremos un update que modifique el estatus según sea el docuemnto subido 
                            if (cbTipocorreo.SelectedItem != null && sgCasoid != "")
                            {
                                //movemos el archivo
                                try
                                {
                                    //Validamos que el archivo no exista
                                    File.Copy(sfilePath, sfilePath_2);
                                }
                                catch (Exception Ex)
                                {
                                    MessageBox.Show("Archivo Repetido, Seleccione otro Documento ó cambie el nombre");
                                    tbNombredelarchivo.Text = "";
                                    button1.Focus();
                                    return;
                                }
                                //fin movemos el archivo

                                String SubTipoDocumentoId = (cbTipocorreo.SelectedItem as ComboboxItem).Value.ToString();
                                //String DocumentoCodigoBarras = tbCodigo.Text;
                                //DateTime fecha = DateTime.Now;
                                String DocumentoFechaCaptura = DateTime.Now.ToString().Substring(0, 10).Replace('/', '.');// now();
                                //String DocumentoFecha = DateTime.ParseExact(tbDocumentofecharecepcion.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbFechacalce.Text;// now();
                                //String DocumentoFolio = tbFolio.Text;
                                //String DocumentoFechaRecepcion = DateTime.ParseExact(tbDocumentofecharecepcion.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbDocumentofecharecepcion.Text;
                                String DocumentoObservacion = rtDescripcion.Text;//descripcion correo
                                
                                String fechaactual = DateTime.Now.ToString("yyyy/MM/dd");
                                conect con1 = new conect();
                                //DocumentoFecha = DateTime.ParseExact(DocumentoFecha, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd");
                                //DocumentoFechaCaptura = DateTime.ParseExact(DocumentoFechaCaptura, "dd'.'MM'.'YYYY", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd");
                                //hacemos un insert a docuemtos y luego a relaciona docuemntos
                                String insertdocumento = " INSERT INTO `documento` " +
                                                            " (`DocumentoId`, " +
                                                            " `SubTipoDocumentoId`, " +
                                                            " `DocumentoFecha`, " +
                                                            " `DocumentoFechaRecepcion`, " +
                                                            " `DocumentoFechaEscaneo`, " +
                                                            " `DocumentoObservacion`, " +
                                                            " `UsuarioId`, " +

                                                            " `CasoId`, " +
                                                            " `TipoSolicitudId`, " +
                                                            " `RelacionDocumentoLink`, " +

                                                            " `usuarioIdPreparo`) " +
                                                            " VALUES " +
                                                            " (NULL, " +
                                                            " '" + SubTipoDocumentoId + "', " +
                                                            " '" + fechaactual + "', " +
                                                            " '" + fechaactual + ": ', " +
                                                            " '" + fechaactual + "', " +
                                                            " '" + DocumentoObservacion + "', " +
                                                            " " + fLogin.sId + ", " +

                                                            " '" + sgCasoid + "', " +
                                                            " '" + sgTiposolicitudid + "', " +
                                                            " '" + sfilePath_2.Replace("\\", "\\\\") + "', " +

                                                            " " + fLogin.sId + "); ";
                                MySqlDataReader resp_escritos = con1.getdatareader(insertdocumento);
                                if (resp_escritos != null)
                                {
                                    resp_escritos.Close();
                                    con1.Cerrarconexion();
                                    conect con_2 = new conect();
                                    MySqlDataReader resp_docid = con_2.getdatareader("select DocumentoId from documento order by documentoid desc limit 1;");
                                    if (resp_docid != null)
                                    {
                                        resp_docid.Read();
                                        //sfilePath = tbNombredelarchivo.Text;
                                        //File.Copy(sfilePath, sfilePath_2);
                                        String documentoid = validareader("DocumentoId", "DocumentoId", resp_docid).Text;
                                        String insertrelaciondoc = " INSERT INTO `relaciondocumento` " +
                                                                    " (`RelacionDocumentoId`, " +
                                                                    " `DocumentoId`, " +
                                                                    " `CasoId`, " +
                                                                    " `TipoSolicitudId`, " +
                                                                    
                                                                    " `RelacionDocumentoLink`) " +
                                                                    " VALUES " +
                                                                    " (NULL, " +
                                                                    " '" + documentoid + "', " +
                                                                    " '" + sgCasoid + "', " +
                                                                    " '" + sgTiposolicitudid + "', " +
                                                                    " '" + sfilePath_2.Replace("\\", "\\\\") + "');";
                                        MySqlDataReader esp_insertrelaciona = con1.getdatareader(insertrelaciondoc);
                                        if (esp_insertrelaciona != null)
                                        {
                                            //aqui hacemos el update para cambiar el estatus y posteriormente agregar los plazos
                                            //Editamos el plazo que debe tener de presentar solicitus
                                            //String sFechaatencion = DateTime.Now.ToString("yyyy-MM-dd");
                                            //conect con_plazos = new conect();
                                            ////Usuarioid_atendio_plazo_impi
                                            //String sUpdate_plazo_solicitud = "Update plazo_general SET"+
                                            //                                " Usuarioid_atendio_plazo_impi = " + login.sId +" ,"+
                                            //                                " Estatus_plazoid_impi = 2 ," +
                                            //                                " Tipo_plazoid_aviso_cliente = 29 ," +
                                            //                                " Estatusid_plazo_cliente = 1 , " +
                                            //                                " Fecha_atendio_plazo_impi ='" + sFechaatencion + "" + 
                                            //                                "' WHERE casoid = " + sCasoidgeneralGlabal + 
                                            //                                " and TipoSolicitudId = " + sgTiposolicitudid + 
                                            //                                " and Tipo_plazoid_impi = 2 and Estatus_plazoid_impi = 1";
                                            //MySqlDataReader resp_consulta_plazo = con_plazos.getdatareader(sUpdate_plazo_solicitud);
                                            //resp_consulta_plazo.Read();
                                            //if (resp_consulta_plazo.RecordsAffected==1)
                                            //{
                                            //    MessageBox.Show("Se atendió el plazo de presentación de solicitud");
                                            //}

                                            //resp_consulta_plazo.Close();
                                            //con_plazos.Cerrarconexion();


                                            /*Consultamos por medio del casoid y tiposolicitudid el plazo de solicitud, tanto el general como el detalle*/

                                            //sCasoidgeneralGlabal
                                            //sgTiposolicitudid

                                            if (cbPlazoatender.SelectedItem != null && (rbSerecibieroninstrucciones.Checked || rbAtendido.Checked || rbCancelado.Checked || rbProrrogado.Checked))//seleccionó un plazo a atender
                                            {
                                                String[] Splazosid = (cbPlazoatender.SelectedItem as ComboboxItem).Text.Split('-');//en la posición 0 se encuentra el plazosid
                                                String sPlazosdetalleid = (cbPlazoatender.SelectedItem as ComboboxItem).Value.ToString();
                                                String sEstatus = "";
                                                String sCampofecha = "Fecha_atendio_plazo";
                                                String sAtendio = "Usuarioid_atendio_plazo";
                                                String sMotivocancelacion = "";
                                                DateTime sFechaactual = DateTime.Today;
                                                DateTime sFechaCorreoatendio = DateTime.Parse(tbFechacorreo.Text);//, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                                                if (rbSerecibieroninstrucciones.Checked)
                                                {
                                                    sEstatus = "11";
                                                }

                                                if (rbAtendido.Checked)
                                                {
                                                    sEstatus = "2";

                                                }

                                                if (rbCancelado.Checked)
                                                {
                                                    sEstatus = "4";
                                                    sCampofecha = "Fecha_cancelacion_plazo";
                                                    sAtendio = "usuario_cancelo";
                                                    sMotivocancelacion = " `Motivo_cancelacion_plazo` = '" + rtMotivocancelacion.Text + "', ";

                                                }

                                                if (rbProrrogado.Checked)
                                                {
                                                    sEstatus = "3";
                                                    sCampofecha = "Fecha_Vencimiento";
                                                    sFechaactual = DateTime.ParseExact(tbFechaprorroga.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);//.ToString("yyyy'/'MM'/'dd");
                                                }

                                                conect con_plazosdetalle = new conect();
                                                String sQuerycorreos = " UPDATE `plazos_detalle` SET " +
                                                                            " `Estatus_plazoid` = '" + sEstatus + "', " +
                                                                            " `" + sCampofecha + "` = '" + sFechaCorreoatendio.ToString("yyyy/MM/dd") + "', " +
                                                                            " `Fecha_atendio_plazo_sistema` = '" + sFechaactual.ToString("yyyy/MM/dd") + "', " +
                                                                            sMotivocancelacion +
                                                                            " `" + sAtendio + "` = '" + fLogin.sId + "' " +
                                                                            " WHERE (`Plazos_detalleid` = '" + sPlazosdetalleid + "');";
                                                MySqlDataReader resp_correos = con1.getdatareader(sQuerycorreos);
                                                //while (resp_escritos.Read())
                                                //{
                                                //    String sIdescritp = validareader("SubTipoDocumentoId", "SubTipoDocumentoId", resp_escritos).Text;
                                                //    cbEsritos.Items.Add(validareader_documentos("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos, sIdescritp));//Estatus
                                                //}
                                                resp_correos.Close();
                                                con_plazosdetalle.Cerrarconexion();
                                            }
                                            else {
                                                if (cbPlazoatender.SelectedItem != null)
                                                {
                                                    MessageBox.Show("Seleccione una opción para atender el plazo.");
                                                }
                                            }

                                            

                                            /*FIN Editamos el plazo que debe tener de presentar solicitus*/

                                        }
                                    }
                                    resp_docid.Close();
                                    con_2.Cerrarconexion();
                                    this.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Debe seleccionar un caso y un escrito para poder guardar");
                            }
                        }
                        catch (Exception E)
                        {
                            MessageBox.Show("Error:"+E.Message);
                            new filelog(fLogin.sId, E.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("El número de caso es incorrecto");
                        this.Show();
                    }
                }
                else
                {
                    this.Show();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show("Ocurrió un error. Revise el log para más detalles.");
                new filelog(fLogin.sId, E.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Salimos desde nuetro botón cerrar");
            this.Close();

        }

        private void tbFechacorreo_KeyPress(object sender, KeyPressEventArgs e)
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

            if (tbFechacorreo.Text.Length == 2)
            {
                tbFechacorreo.Text = tbFechacorreo.Text + "-";
                tbFechacorreo.SelectionStart = tbFechacorreo.Text.Length;

            }
            if (tbFechacorreo.Text.Length == 5)
            {
                tbFechacorreo.Text = tbFechacorreo.Text + "-";
                tbFechacorreo.SelectionStart = tbFechacorreo.Text.Length;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var fileContent = string.Empty;
                var filePath = string.Empty;
                String sNamefile = "";
                String[] aName;
                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.InitialDirectory = mdoc;
                    openFileDialog.Filter = "msg files (*.msg)|*.msg";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filePath = openFileDialog.FileName;
                        aName = filePath.Split('\\');
                        sNamefile = aName[aName.Length - 1];
                        //Read the contents of the file into a stream
                        /*var fileStream = openFileDialog.OpenFile();

                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            fileContent = reader.ReadToEnd();
                        }*/
                    }
                }
                //\\192.168.1.95\documentosserver\Patentes
                configuracionfiles obj = new configuracionfiles();
                obj.configuracionfilesinicio();
                String sCarpetadocumentos = "Correossubidos";
                //File.Copy(filePath, "\\\\" + obj.sServer + "\\documentosserver\\Patentes\\queonda.jpg");
                //string ruta = obj.sFileupload + "documentosserver\\" + sCarpetadocumentos;
                string ruta = obj.sFileupload + "\\" + sCarpetadocumentos;

                //if (!Directory.Exists(ruta))//si no existe la carpeta la creamos
                //{
                //    Directory.CreateDirectory(ruta);
                //}
                if (!Directory.Exists(ruta))//si no existe la carpeta la creamos
                {
                    Directory.CreateDirectory(ruta);
                }
                //crear carpeta de cada caso 
                //sCarpetadocumentos
                string textoNormalizado = cbTipocorreo.Text.Normalize(NormalizationForm.FormD);
                //coincide todo lo que no sean letras y números ascii o espacio
                //y lo reemplazamos por una cadena vacía.Regex reg = new Regex("[^a-zA-Z0-9 ]");
                Regex reg = new Regex("[^a-zA-Z0-9 ]");
                string textoSinAcentos = reg.Replace(textoNormalizado, "");
                if (sNamefile!="") {

                    String sFechanow = DateTime.Now.ToString("yyyyMMddHHmmsss");
                    sfilePath_2 = @ruta + "\\" + textoSinAcentos +" "+ sFechanow+" " + sNamefile;
                    tbNombredelarchivo.Text = @"\\" + textoSinAcentos + " " + sNamefile;
                }
                
                /*cambio a oficios*/
                sfilePath = filePath;
                
                
            }
            catch (Exception E)
            {
                new filelog("subir correo para atender caso", E.ToString());
            }
        }
        public ComboboxItem validareader(String campoText, String campoValue, MySqlDataReader mresultado)
        {
            ComboboxItem cItemresult = new ComboboxItem();

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
            return cItemresult;
        }

        public string sfilePath { get; set; }

        public string sfilePath_2 { get; set; }

        private void cbPlazoatender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbFechacorreo_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechacorreo);
        }

        private void Fatenderplazo_Load(object sender, EventArgs e)
        {

        }

        private void rtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbCorreotipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                if (cbCorreotipo.Text != "")
                {
                    cbTipocorreo.Items.Clear();
                    conect con_3 = new conect();
                    String sQuery = "select * from subtipodocumento where TipoDocumentoId = 6 and malcomx = '" + cbCorreotipo.Text + "';";
                    MySqlDataReader resp_areaimpi = con_3.getdatareader(sQuery);
                    while (resp_areaimpi.Read())
                    {
                        cbTipocorreo.Items.Add(objfuncionesdicss.validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_areaimpi));
                    }
                    resp_areaimpi.Close();
                    con_3.Cerrarconexion();
                }
            }
            catch (Exception ex) { 
            }
        }

        private void rbProrrogado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbProrrogado.Checked){
                tbFechaprorroga.Enabled = true;
            }else {
                tbFechaprorroga.Enabled = false;
            }
        }
    }
}
