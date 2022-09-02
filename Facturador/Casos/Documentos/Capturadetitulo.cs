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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturador
{
    public partial class Capturadetitulo : Form
    {
        public captura capform;
        public Form1 login;
        public String sGrupocaso;
        public String sTablaconsulta;
        public String sCasoidactual;
        public String sgTiposolicitudid;
        public String sEstatusidint;
        public String sCasoidgeneralGlabal = "0";
        public String sCapturatituloid;
        public bool sBAnderadesdecaso = false;
        public String sfilePath, sfilePath_2, sCarpetadocumentos, sCarpetacaso;
        public String sgSubtiposolicitudid = "", sgCasoDisenoClasificacion="";
        funcionesdicss objfuncionesdicss;
        public Capturadetitulo(captura capformulario, Form1 log, String sGrupo, String sCasoidgeneral)
        {
            try
            {
                login = log;
                sGrupocaso = sGrupo;
                capform = capformulario;
                objfuncionesdicss = new funcionesdicss();
                InitializeComponent();

                switch (sGrupocaso)
                {
                    case "1":
                        {
                            sTablaconsulta = "caso_patente";
                            this.Text = this.Text + " ( Grupo Patentes)";
                            this.BackColor = Color.Pink;
                            sCapturatituloid = "99";
                            sCarpetadocumentos = "Patentes";
                            gbMarcasfechavigencia.Visible = false;
                        } break;
                    case "2":
                        {
                            sTablaconsulta = "caso_marcas";
                            this.Text = this.Text + " ( Grupo Marcas)";
                            this.BackColor = Color.FromArgb(255, 255, 192);
                            sCapturatituloid = "1044";
                            sCarpetadocumentos = "Marcas";
                        } break;
                    case "3":
                        {
                            sTablaconsulta = "caso_contencioso";
                            this.Text = this.Text + " ( Grupo Contencioso)";
                            this.BackColor = Color.Yellow;
                            sCarpetadocumentos = "Casocontencioso";
                            gbMarcasfechavigencia.Visible = false;
                        } break;
                    case "4":
                        {
                            sTablaconsulta = "caso_consulta";
                            this.Text = this.Text + " ( Grupo Consulta)";
                            this.BackColor = SystemColors.Control;
                            sCarpetadocumentos = "Consulta";
                            gbMarcasfechavigencia.Visible = false;
                        } break;
                    case "5":
                        {
                            sTablaconsulta = "caso_oposicion";
                            this.Text = this.Text + " ( Grupo Oposicion a solicitudes)";
                            this.BackColor = Color.FromArgb(255, 192, 128);
                            sCarpetadocumentos = "Oposicion";
                            gbMarcasfechavigencia.Visible = false;
                        } break;
                    case "6":
                        {
                            sTablaconsulta = "";
                            this.Text = this.Text + " ( Grupo Variedades vegetales)";
                            this.BackColor = SystemColors.Control;
                            sCarpetadocumentos = "Variedadesveg";
                            gbMarcasfechavigencia.Visible = false;
                        } break;
                    case "7":
                        {
                            sTablaconsulta = "caso_registrodeobra";
                            this.Text = this.Text + " ( Grupo Derechos de autor)";
                            this.BackColor = Color.SkyBlue;
                            sCarpetadocumentos = "Registrodeobra";
                            gbMarcasfechavigencia.Visible = false;
                        } break;
                    case "8":
                        {
                            sTablaconsulta = "caso_reservadederechos";
                            this.Text = this.Text + " ( Grupo Reserva de derechos)";
                            this.BackColor = Color.LightGreen;
                            sCarpetadocumentos = "Reservadederechos";
                            gbMarcasfechavigencia.Visible = false;
                        } break;
                    default:
                        {
                            MessageBox.Show("Debe seleccionar un tipo correcto");
                        } break;
                }
                if (sCasoidgeneral != "0")
                {
                    button3.Enabled = false;
                    sCasoidgeneralGlabal = sCasoidgeneral;
                    cargacasoenform();
                    sBAnderadesdecaso = true;
                    sCarpetacaso = tbCasonum.Text;
                }
            }catch(Exception E){
                new filelog(login.sId, E.ToString());
            }
        }
        public ComboboxItem validareader(String campoText, String campoValue, MySqlDataReader mresultado)
        {
            ComboboxItem cItemresult = new ComboboxItem();
            try {
                if (!mresultado.IsDBNull(mresultado.GetOrdinal(campoText)))
                {
                    try {
                        cItemresult.Text = mresultado.GetString(mresultado.GetOrdinal(campoText));
                    }
                    catch (Exception exss) {
                        cItemresult.Text = mresultado.GetDateTime(mresultado.GetOrdinal(campoText)).ToString("yyyy-MM-dd");
                    }
                    
                }
                else
                {
                    cItemresult.Text = "";
                }

                if (!mresultado.IsDBNull(mresultado.GetOrdinal(campoValue)))
                {
                    try {
                        cItemresult.Value = mresultado.GetString(mresultado.GetOrdinal(campoValue));
                    } catch (Exception exsss) {
                        cItemresult.Value = mresultado.GetDateTime(mresultado.GetOrdinal(campoText)).ToString("yyyy-MM-dd");
                    }
                    
                }
                else
                {
                    cItemresult.Value = "";
                }
            }
            catch (Exception exs) { 
            }
            

            
            return cItemresult;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int isGrop = Int32.Parse(sGrupocaso);
            bCasoparadoc bCaso = new bCasoparadoc(isGrop, capform, login);
            if (bCaso.ShowDialog() == DialogResult.OK)
            {
                String sQueryconulta = "";
                try{
                    sCasoidgeneralGlabal = bCaso.sCasoid;
                    cargacasoenform();
                }catch (Exception E)
                {
                    new filelog(login.sId, E.ToString());
                }
            }
        }
        public void cargacasoenform()
        {
            try
            {
                conect con1 = new conect();
                String sQueryconulta = "SELECT * FROM " + sTablaconsulta + " where Casoid = " + sCasoidgeneralGlabal + "";
                MySqlDataReader respuestastring6 = con1.getdatareader(sQueryconulta);
                if (respuestastring6 != null)
                {
                    while (respuestastring6.Read())
                    {
                        tbCasonum.Text = validareader("CasoNumero", "Casoid", respuestastring6).Text;
                        sFechapresentacion = validareader("CasoFechaRecepcion", "Casoid", respuestastring6).Text;
                        tbFPresentaciosol.Text = sFechapresentacion.Substring(0,10);

                        String EstatusCasoId = validareader("EstatusCasoId", "Casoid", respuestastring6).Text;
                        String sConsultaestatus = "SELECT * FROM estatuscaso where EstatusCasoId = '" + EstatusCasoId + "' ";
                        sCasoidactual = validareader("Casoid", "Casoid", respuestastring6).Text;
                        sgTiposolicitudid = validareader("TipoSolicitudid", "Casoid", respuestastring6).Text;
                        sgSubtiposolicitudid = validareader("TipoSolicitudid", "Casoid", respuestastring6).Text;
                        if (sGrupocaso == "1")
                        {
                            sgCasoDisenoClasificacion = validareader("CasoDisenoClasificacion", "CasoDisenoClasificacion", respuestastring6).Text;
                        }
                        conect con2 = new conect();
                        MySqlDataReader resp_estatus = con2.getdatareader(sConsultaestatus);
                        while (resp_estatus.Read())
                        {
                            tbEstatus.Text = validareader("EstatusCasoDescrip", "EstatusCasoId", resp_estatus).Text;//Estatus
                            sEstatusidint = validareader("EstatusCasoId", "EstatusCasoId", resp_estatus).Text;//Estatus
                        }
                        resp_estatus.Close();
                        con2.Cerrarconexion();
                        String sQuerytiposolicitud = "select * from tiposolicitud where TipoSolicitudid = " + validareader("TipoSolicitudid", "Casoid", respuestastring6).Text;
                        conect con3 = new conect();
                        MySqlDataReader resp_tiposolicitud = con3.getdatareader(sQuerytiposolicitud);
                        while (resp_tiposolicitud.Read())
                        {
                            tbTipo.Text = validareader("TipoSolicitudDescrip", "TipoSolicitudid", resp_tiposolicitud).Text;//Estatus
                        }
                        resp_tiposolicitud.Close();
                        con3.Cerrarconexion();

                        cbEsritos.Items.Clear();
                        //1044
                        //99
                        /*Debemos agrgear los estatus y escritos disponibles segun las tablas y reaccionar dependiendo eso*/
                        //String sQueryescritosdisponibles = "SELECT " +
                        //                                   "     * " +
                        //                                   " FROM " +
                        //                                   "    estatuscasosubtipodocumento, " +
                        //                                   "    subtipodocumento " +
                        //                                   " WHERE " +
                        //                                   "     estatuscasosubtipodocumento.Estatuscasoid = " + sEstatusidint + "  " +
                        //                                   "         AND estatuscasosubtipodocumento.GrupoId = " + sGrupocaso +//El grupo falta validarlo con una variable dependiendo el tipo del caso 
                        //                                   "         AND subtipodocumento.SubTipoDocumentoId = estatuscasosubtipodocumento.SubTipoDocumentoId " +
                        //                                   "         AND subtipodocumento.TipoDocumentoId = 9 " +//en esta pantalla debe ser fijo el número 1 puesto que estamos en escritoa
                        //                                   "         AND subtipodocumento.SubTipoDocumentoIndAct = 1 " +// el valos uno debe ser fijo puesto que validamos que el documento esté activo
                        //                                   "         group by estatuscasosubtipodocumento.SubTipoDocumentoId order by subtipodocumento.SubTipoDocumentoDescrip;";

                        String sQueryescritosdisponibles = "select * from subtipodocumento where subtipodocumentoid = "+sCapturatituloid;
                        //String sQueryescritosdisponibless = "select * from estatuscasosubtipodocumento, subtipodocumento where estatuscasosubtipodocumento.EstatusCasoId = " + sEstatusidint + " and estatuscasosubtipodocumento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId and estatuscasosubtipodocumento.GrupoId =1 and subtipodocumento.TipoDocumentoId = 2 group by subtipodocumento.SubTipoDocumentoId;";
                        //String sQueryescritosdisponibles = "select subtipodocumento.SubTipoDocumentoDescrip, estatuscasosubtipodocumentores.SubTipoDocumentoId from estatuscasosubtipodocumentores, subtipodocumento where estatuscasoid = " + tbEstatus.Text + " and estatuscasosubtipodocumentores.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId;";
                        conect con4 = new conect();
                        MySqlDataReader resp_escritos = con4.getdatareader(sQueryescritosdisponibles);
                        while (resp_escritos.Read())
                        {
                            cbEsritos.Items.Add(validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos));//Estatus
                        }
                        resp_escritos.Close();
                        con4.Cerrarconexion();
                        //String sQueryresponsables = "select UsuarioName, UsuarioId from usuario;";
                        //MySqlDataReader resp_responsables = con1.getdatareader(sQueryresponsables);
                        //while (resp_responsables.Read())
                        //{
                        //    cbPreparo.Text = validareader("UsuarioName", "UsuarioId", resp_responsables).Text;//Estatus
                        //}
                        //resp_responsables.Close();
                        /*fin de ciclo escritos*/
                        //tbRegistro.Text = validareader("CasoNumConcedida", "Casoid", respuestastring6).Text;//registro
                        tbExpediente.Text = validareader("CasoNumeroExpedienteLargo", "Casoid", respuestastring6).Text;
                        rtbtitulo.Text = validareader("CasoTituloespanol", "Casoid", respuestastring6).Text + " /" + validareader("CasoTituloingles", "Casoid", respuestastring6).Text;
                    }
                }
                respuestastring6.Close();
                con1.Cerrarconexion();
            }
            catch (Exception E)
            {
                new filelog(login.sId, E.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {//aqui guardamos el título de marcas y debemos generar los plazos
            try
            {
                String stbFechacalce = tbFechanotificacion.Text;
                DateTime oDate = DateTime.Parse(stbFechacalce);

                DateTime sFechAc = DateTime.Now.Date;
                if (oDate > sFechAc) // Si la fecha indicada es menor o igual a la fecha actual
                {
                    MessageBox.Show("Le fecha no puede ser mayor a la fecha acutal.");
                    if (tbFechanotificacion.CanFocus)
                    {
                        tbFechanotificacion.Focus();
                    }
                    return;
                }

                if (tb_numconcedida.Text.Trim()=="") {
                    MessageBox.Show("El número de registro no puede estar vacío");
                    tb_numconcedida.Focus();
                    return;
                }

                if (cbEsritos.Text.Trim()=="") {
                    MessageBox.Show("Seleccione el tipo de documento.");
                    cbEsritos.Focus();
                    return;
                }

                if (tbFilename.Text.Trim() == "") {
                    MessageBox.Show("Debe seleccionar un archivo");
                    tbFilename.Focus();
                    return;
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Debe Seleccionar una Fecha correcta.");
                return;
            }

            try
            {
                String sfecharecepcion = tbDocumentofecharecepcion.Text;
                DateTime oDate = DateTime.Parse(sfecharecepcion);

                DateTime FechAc = DateTime.Now.Date;
                if (oDate > FechAc) // Si la fecha indicada es menor o igual a la fecha actual
                {
                    MessageBox.Show("Le fecha no puede ser mayor a la fecha acutal.");
                    if (tbDocumentofecharecepcion.CanFocus)
                    {
                        tbDocumentofecharecepcion.Focus();
                    }
                    return;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Debe Seleccionar una Fecha correcta.");
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
                    tbFilename.Text = "";
                    buttonfileupload.Focus();
                    return;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Archivo Repetido, Seleccione otro Documento ó cambie el nombre");
                tbFilename.Text = "";
                buttonfileupload.Focus();
                return;
            }

            try
            {
                this.Hide();
                confirmarnumerodecasodocs sConfirmanuermodecaso = new confirmarnumerodecasodocs();
                if (sConfirmanuermodecaso.ShowDialog() == DialogResult.OK)
                {
                    if (sConfirmanuermodecaso.sNumerocaso == tbCasonum.Text)
                    {

                        this.Show();
                        try
                        {
                            //debemos insertar lo que esta capturado pero dependioendo el valor insertado aremos un update que modifique el estatus según sea el docuemnto subido 
                            if (cbEsritos.SelectedItem != null && sCasoidactual != "")
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
                                    tbFilename.Text = "";
                                    buttonfileupload.Focus();
                                    return;
                                }
                                //fin movemos el archivo

                                String casonum = tbCasonum.Text;
                                String SubTipoDocumentoId = (cbEsritos.SelectedItem as ComboboxItem).Value.ToString();
                                String DocumentoCodigoBarras = tbCodigo.Text;
                                //DateTime fecha = DateTime.Now;

                                DateTime dDocumentoFecha = DateTime.ParseExact(tbFechanotificacion.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);//.ToString("yyyy'/'MM'/'dd"); //tbFechacalce.Text;// now();


                                String DocumentoFechaCaptura = DateTime.Now.ToString().Substring(0, 10).Replace('/', '.');// now();
                                String DocumentoFecha = DateTime.ParseExact(tbFechanotificacion.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbFechacalce.Text;// now();
                                String DocumentoFolio = tbFolio.Text;
                                String DocumentoFechaRecepcion = DateTime.ParseExact(tbDocumentofecharecepcion.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbDocumentofecharecepcion.Text;
                                String DocumentoObservacion = rtObservacion.Text;
                                String fechaactual = DateTime.Now.ToString("yyyy/MM/dd");
                                conect con1 = new conect();
                                //DocumentoFecha = DateTime.ParseExact(DocumentoFecha, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd");
                                //DocumentoFechaCaptura = DateTime.ParseExact(DocumentoFechaCaptura, "dd'.'MM'.'YYYY", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd");
                                //hacemos un insert a docuemtos y luego a relaciona docuemntos
                                String insertdocumento = " INSERT INTO `documento` " +
                                                        " (`DocumentoId`, " +
                                                        " `DocumentoCodigoBarras`, " +
                                                        " `SubTipoDocumentoId`, " +
                                                        " `DocumentoFecha`, " +
                                                        " `DocumentoFolio`, " +
                                                        " `DocumentoFechaRecepcion`, " +

                                                        " `DocumentoFechaCaptura`, " +

                                                        " `DocumentoObservacion`, " +
                                                        " `DocumentoIdRef`, " +
                                                        " `UsuarioId`, " +
                                                        " `CompaniaMensajeriaId`, " +

                                                        " `DocumentoNumeroGuia`, " +

                                                        " `CasoId`, " +
                                                        " `TipoSolicitudId`, " +
                                                        " `RelacionDocumentoLink`, " +

                                                        " `usuarioIdPreparo`) " +
                                                        " VALUES " +
                                                        " (null, " +
                                                        " '" + DocumentoCodigoBarras + "', " +
                                                        " '" + SubTipoDocumentoId + "', " +
                                                        " '" + DocumentoFecha + "', " +
                                                        " '" + DocumentoFolio + "', " +
                                                        " '" + DocumentoFechaRecepcion + ": ', " +

                                                        " '" + fechaactual + "', " +

                                                        " '" + DocumentoObservacion + "', " +
                                                        " 0, " +
                                                        " 1, " +
                                                        " 0, " +

                                                         " 'numeroguia', " +

                                                        " '" + sCasoidactual + "', " +
                                                        " '" + sgTiposolicitudid + "', " +
                                                        " '" + sfilePath_2.Replace("\\", "\\\\") + "', " +

                                                         " 0); ";
                                MySqlDataReader resp_escritos = con1.getdatareader(insertdocumento);
                                String sDocumentoid = "";
                                if (resp_escritos != null)
                                {
                                    resp_escritos.Close();
                                    con1.Cerrarconexion();
                                    conect con_2 = new conect();
                                    MySqlDataReader resp_docid = con_2.getdatareader("select DocumentoId from documento order by documentoid desc limit 1;");
                                    if (resp_docid != null)
                                    {
                                        resp_docid.Read();
                                        //File.Copy(sfilePath, sfilePath_2);
                                        String documentoid = validareader("DocumentoId", "DocumentoId", resp_docid).Text;
                                        sDocumentoid = documentoid;
                                        //Actualizamos los datos del caso 
                                        switch (sGrupocaso)
                                        {
                                            case "1":
                                                {
                                                    actcaso(DocumentoFecha, SubTipoDocumentoId, documentoid);
                                                }
                                                break;
                                            case "2":
                                                {
                                                    actcasomarcas(DocumentoFecha, SubTipoDocumentoId, documentoid);
                                                }
                                                break;
                                            case "3":
                                                {

                                                }
                                                break;
                                            case "4":
                                                {

                                                }
                                                break;
                                            case "5":
                                                {

                                                }
                                                break;
                                            case "6":
                                                {

                                                }
                                                break;
                                            case "7":
                                                {

                                                }
                                                break;
                                            case "8":
                                                {

                                                }
                                                break;
                                            default:
                                                {
                                                    MessageBox.Show("Debe seleccionar un tipo correcto");
                                                }
                                                break;
                                        }
                                        
                                        conect con1_inerttitulo = new conect();
                                        String insertrelaciondoc = " INSERT INTO `relaciondocumento` " +
                                                                    " (`RelacionDocumentoId`, " +
                                                                    " `DocumentoId`, " +
                                                                    " `CasoId`, " +
                                                                    " `TipoSolicitudId`, " +
                                                                    " `RelacionDocumentoLink`) " +
                                                                    " VALUES " +
                                                                    " (null, " +
                                                                    " '" + documentoid + "', " +
                                                                    " '" + sCasoidactual + "', " +
                                                                    " '" + sgTiposolicitudid + "', " +
                                                                    " '" + sfilePath_2.Replace("\\", "\\\\") + "');";
                                        MySqlDataReader esp_insertrelaciona = con1_inerttitulo.getdatareader(insertrelaciondoc);
                                        if (esp_insertrelaciona != null)
                                        {
                                            esp_insertrelaciona.Close();
                                        }
                                        con1_inerttitulo.Cerrarconexion();
                                    }
                                    resp_docid.Close();
                                    con_2.Cerrarconexion();
                                }

                                //agregamos la funcion para generar los plazos
                                generaplazosplazosdeoficio(SubTipoDocumentoId, sDocumentoid, dDocumentoFecha, dDocumentoFecha, login.sId);
                            }
                            else
                            {
                                MessageBox.Show("Debe seleccionar un caso y un escrito para poder guardar");
                            }
                        }
                        catch (Exception E)
                        {
                            MessageBox.Show("Error " + E.Message);
                            new filelog(login.sId, E.ToString());
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
            }catch(Exception E){
                MessageBox.Show("Ocurrió un Error. Revise el log para más detalles.");
                new filelog(login.sId, E.ToString());
            }
        }


        public String generaplazo()
        {
            String sResult = "";
            String sQuery_plazo_relacion_general = "";
            try
            {
                conect conect_plazoid_relacion = new conect();
                sQuery_plazo_relacion_general = " INSERT INTO `plazos` " +
                                                        " (`Plazosid`, " +
                                                        " `CasoId`, " +
                                                        " `TipoSolicitudId`, " +
                                                        " `Fecha_creacion`) " +
                                                        " VALUES " +
                                                        " (Plazosid, " +
                                                        " '" + sCasoidactual + "', " +
                                                        " '" + sgTiposolicitudid + "', " +
                                                        " now() " +
                                                        " ); ";
                MySqlDataReader resp_plazo_relacion = conect_plazoid_relacion.getdatareader(sQuery_plazo_relacion_general);

                if (resp_plazo_relacion.RecordsAffected > 0)
                { //se inserto correctamente
                    conect conect_plazos = new conect();
                    String sQuery_plazos = " select * from plazos order by Plazosid desC limit 1";
                    MySqlDataReader resp_plazos = conect_plazos.getdatareader(sQuery_plazos);
                    while (resp_plazos.Read())
                    {
                        sResult = validareader("Plazosid", "Plazosid", resp_plazos).Text;
                    }
                    resp_plazos.Close();
                    conect_plazos.Cerrarconexion();

                }
                resp_plazo_relacion.Close();
                conect_plazoid_relacion.Cerrarconexion();
            }
            catch (Exception exs)
            {
                new filelog("insert plazos query:" + sQuery_plazo_relacion_general, "" + exs.Message);
            }
            return sResult;
        }
        public void generaplazosplazosdeoficio(String subtipodocumentoid, String documentoid, DateTime plazofechaNotificacion, DateTime plazofecha, String usuarioid)
        {
            try
            {
                DateTime dFechaNoficiacionorig = plazofechaNotificacion;
                //consultamos los plazos que debe generar este documento
                conect conect_plazoid_relacion = new conect();
                String sQuery_plazo_relacion_general = " SELECT  " +
                                                        "     tipodocumento.TipoDocumentoDescrip, " +
                                                        "     plazos_de_oficio.aviso, " +
                                                        "     subtipodocumento.SubTipoDocumentoDescrip, " +
                                                        "     tipoplazos.TipoPlazoDescrip, " +
                                                        "     plazos_de_oficio.* " +
                                                        " FROM " +
                                                        "     plazos_de_oficio " +
                                                        "         LEFT JOIN " +
                                                        "     subtipodocumento ON plazos_de_oficio.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId " +
                                                        "         LEFT JOIN " +
                                                        "     tipoplazos ON plazos_de_oficio.TipoPlazoId = tipoplazos.tipoplazosid " +
                                                        "         LEFT JOIN " +
                                                        "     tipodocumento ON subtipodocumento.TipoDocumentoId = tipodocumento.TipoDocumentoId " +
                                                        " 		 " +
                                                        "     where plazos_de_oficio.SubTipoDocumentoId = " + subtipodocumentoid + ";";//
                MySqlDataReader resp_plazo_relacion = conect_plazoid_relacion.getdatareader(sQuery_plazo_relacion_general);
                int iCounplazos = 0;
                String sPlazosidparainsertdetalle = generaplazo();
                String sInsertdetalleplazosid = "";
                String sTipoplazoidanterior = "", tipoplazoid_impi = "";
                bool bConvinacionval = true;
                while (resp_plazo_relacion.Read())
                {
                    //consultamos primero las validaciones del plazo consultado
                    String sCasogrupo = validareader("grupo", "grupo", resp_plazo_relacion).Text;
                    String sTiposolicitud = validareader("TipoSolicitudId", "TipoSolicitudId", resp_plazo_relacion).Text;
                    String sSubtiposolicitud = validareader("SubTipoSolicitudId", "SubTipoSolicitudId", resp_plazo_relacion).Text;
                    String sCasodisenoclasif = validareader("CasoDisenoClasificacion", "CasoDisenoClasificacion", resp_plazo_relacion).Text;

                    String sdiasprorroga = validareader("diasprorroga", "diasprorroga", resp_plazo_relacion).Text;
                    String sMesesprorroga = validareader("mesesprorroga", "mesesprorroga", resp_plazo_relacion).Text;

                    if (sCasogrupo != "")
                    { //si existe el filtro de caso
                        if (sGrupocaso != sCasogrupo)
                        { //validamos que sea el mismo grupo
                            bConvinacionval = false;
                        }
                    }

                    if (sTiposolicitud != "")
                    {
                        if (sgTiposolicitudid != sTiposolicitud)
                        {
                            bConvinacionval = false;
                        }
                    }

                    if (sSubtiposolicitud != "")
                    {
                        if (sgSubtiposolicitudid != sSubtiposolicitud)
                        {
                            bConvinacionval = false;
                        }
                    }

                    if (sCasodisenoclasif != "")
                    {
                        if (sgCasoDisenoClasificacion != sCasodisenoclasif)
                        {
                            bConvinacionval = false;
                        }
                    }

                    if (bConvinacionval)
                    {//si pasa las validaciones entonces hace el calculo de generar el plazo
                        //calculamos cuanto sumaremos a la fecha 
                        String sSubTipoDocumentoPlazoDias = "", sSubTipoDocumentoPlazoMeses = "", sAviso = "";
                        int iSubTipoDocumentoPlazoDias = 0, iSubTipoDocumentoPlazoMeses = 0;
                        try
                        {
                            sSubTipoDocumentoPlazoDias = validareader("SubTipoDocumentoPlazoDias", "SubTipoDocumentoPlazoDias", resp_plazo_relacion).Text;
                            sSubTipoDocumentoPlazoMeses = validareader("SubTipoDocumentoPlazoMeses", "SubTipoDocumentoPlazoMeses", resp_plazo_relacion).Text;
                            if (sSubTipoDocumentoPlazoDias != "")
                            {
                                iSubTipoDocumentoPlazoDias = int.Parse(sSubTipoDocumentoPlazoDias);
                            }

                            if (sSubTipoDocumentoPlazoMeses != "")
                            {
                                iSubTipoDocumentoPlazoMeses = int.Parse(sSubTipoDocumentoPlazoMeses);
                            }
                        }
                        catch (Exception exs)
                        {
                            new filelog("", "");
                        }

                        plazofecha = plazofechaNotificacion.AddDays(iSubTipoDocumentoPlazoDias);
                        plazofecha = plazofecha.AddMonths(iSubTipoDocumentoPlazoMeses);

                        //validamos si es un aviso para quitar la fecha notificacion y para quita los plazos 3 y 4 meses
                        sAviso = sSubTipoDocumentoPlazoDias = validareader("aviso", "aviso", resp_plazo_relacion).Text;
                        String sFechanotific = "", sFechanotifictres = "", sFechanotificcuatro = "";
                        if (sAviso == "0")
                        { //quiere decir que es oficial y debe poner fehca notificacion
                            sFechanotific = "'" + dFechaNoficiacionorig.ToString("yyyy/MM/dd") + "'";
                            sFechanotifictres = "'" + plazofecha.AddMonths(1).ToString("yyyy/MM/dd") + "'";
                            sFechanotificcuatro = "'" + plazofecha.AddMonths(2).ToString("yyyy/MM/dd") + "'";
                        }
                        else
                        {
                            sFechanotific = "null";
                            sFechanotifictres = "null";
                            sFechanotificcuatro = "null";
                        }

                        //debemos hacer un insert con los primero datos recibidos
                        if (iCounplazos != 0)
                        {//si es el primer plazo generamos un plazosid
                         //consultamos si es pareja
                         //String tipoplazoid_impi_int = validareader("TipoPlazoId", "TipoPlazoId", resp_plazo_relacion).Text;
                            tipoplazoid_impi = validareader("TipoPlazoId", "TipoPlazoId", resp_plazo_relacion).Text;
                            if (!espareja(sTipoplazoidanterior, tipoplazoid_impi))//si no es pareja entonces asignamos un nuevo plazosid
                            {
                                sPlazosidparainsertdetalle = generaplazo();
                            }
                        }
                        else
                        {
                            tipoplazoid_impi = validareader("TipoPlazoId", "TipoPlazoId", resp_plazo_relacion).Text;
                            sTipoplazoidanterior = tipoplazoid_impi;
                        }

                        //aqui insertamos el valor del plazodetalle
                        conect conect_insertplazos = new conect();
                        sInsertdetalleplazosid = " INSERT INTO `plazos_detalle` " +
                                                        " ( `Plazosid`, " +
                                                        " `documentoid`, " +
                                                        " `usuario_creo_plazodetalle`, " +
                                                        " `Tipo_plazoid`, " +
                                                        " `Estatus_plazoid`, " +
                                                        " `Fecha_notificacion`, " +
                                                        " `Fecha_Vencimiento`, " +
                                                        " `Fecha_vencimiento_3m`, " +
                                                        " `Fecha_vencimiento_4m` " +
                                                        ") " +
                                                        " VALUES " +
                                                        " ( " +
                                                        " '" + sPlazosidparainsertdetalle + "' , " +
                                                        " '" + documentoid + "' , " +
                                                        " '" + usuarioid + "' , " +
                                                        " '" + tipoplazoid_impi + "' , " +
                                                        " '1' , " +
                                                        " " + sFechanotific + " , " +
                                                        " '" + plazofecha.ToString("yyyy/MM/dd") + "' , " +
                                                        " " + sFechanotifictres + " , " +
                                                        " " + sFechanotificcuatro + " " +
                                                        " ); ";
                        MySqlDataReader resp_insertplazodetalle = conect_insertplazos.getdatareader(sInsertdetalleplazosid);
                        if (resp_insertplazodetalle.RecordsAffected > 0)
                        {
                            new filelog("plazoingresao ", "plazosdetalleid: " + sPlazosidparainsertdetalle);
                        }
                        //falta completar los plazos
                        iCounplazos++;
                    }

                }
                resp_plazo_relacion.Close();
                conect_plazoid_relacion.Cerrarconexion();
            }
            catch (Exception exs)
            {
                new filelog("excepcion al ingresar ", " " + exs.Message);
            }
        }
        public bool espareja(String sTipoplazouno, String sTipoplazdosid)
        {
            bool bRespuesta = false;
            try
            {
                conect conect_plazoid_relacion = new conect();
                String sQuery_plazo_relacion_general = " select * from plazos_parejas where tipoplazoid = " + sTipoplazouno + " and tipoplazoidpareja = " + sTipoplazdosid + "; ";
                MySqlDataReader resp_plazo_relacion = conect_plazoid_relacion.getdatareader(sQuery_plazo_relacion_general);
                int icount = 0;
                while (resp_plazo_relacion.Read())
                {
                    icount++;
                }
                if (icount > 0)
                { //quiere decir que si son pareja
                    bRespuesta = true;
                }
                resp_plazo_relacion.Close();
                conect_plazoid_relacion.Cerrarconexion();
            }
            catch (Exception exs)
            {
                bRespuesta = false;
            }
            return bRespuesta;
        }

        public void actcaso(String DocumentoFecha, String SubTipoDocumentoId, String documentoid) {
            try
            {
                //aqui hacemos el update para cambiar el estatus y posteriormente agregar los plazos

                //SubTipoDocumentoId para obtener SubTipoDocumentoIndTipo
                conect con_5 = new conect();
                String sconsultaeindtipo = "select SubTipoDocumentoIndTipo from subtipodocumento where SubTipoDocumentoId = " + SubTipoDocumentoId;
                MySqlDataReader resp_consultaestipo = con_5.getdatareader(sconsultaeindtipo);
                resp_consultaestipo.Read();
                String sSubTipoDocumentoIndTipo = validareader("SubTipoDocumentoIndTipo", "SubTipoDocumentoIndTipo", resp_consultaestipo).Text;
                if (resp_consultaestipo != null)
                {

                    con_5.Cerrarconexion();
                    conect con_3 = new conect();
                    String sConsultaestatussiguiente = "select * from subtipodocumentoestatuscaso where SubTipoDocumentoIndTipo = " +
                                                        sSubTipoDocumentoIndTipo +
                                                        " and grupoid = " + sGrupocaso;
                    MySqlDataReader resp_consultaestatuscaso = con_3.getdatareader(sConsultaestatussiguiente);
                    resp_consultaestatuscaso.Read();
                    try
                    {
                        String sEstatusactual = validareader("EstatusCasoId", "EstatusCasoId", resp_consultaestatuscaso).Text;
                        con_3.Cerrarconexion();
                        resp_consultaestatuscaso.Close();
                        //Aqui debemos actualizar la fecha de vigencia
                        //para eso debemos preguntar cuantas anualidades tiene ese tipo ya sean secuencias o anualidades y a partir de ahí debemos sumar ese numero en años a la 
                        //fecha de presentación
                        conect con_fechas = new conect();
                        String squeryfechas = "select * from caso_patente where casoid = " + sCasoidactual + ";";//consultamos la fecha presentacion
                        MySqlDataReader resp_selectfechas = con_fechas.getdatareader(squeryfechas);
                        resp_selectfechas.Read();
                        String sCasoFechaInternacionalselect = validareader("CasoFechaInternacional", "CasoFechaInternacional", resp_selectfechas).Text;
                        String sCasoFechaRecepcionselect = validareader("CasoFechaRecepcion", "CasoFechaRecepcion", resp_selectfechas).Text;
                        

                        resp_selectfechas.Close();
                        con_fechas.Cerrarconexion();

                        //Consultamos las anualidades
                        conect con_numanualidades = new conect();

                        String squery_numanualidades = "select count(*) As numanualidad from anualidad where casoid = " + sCasoidactual + ";";//consultamos la fecha presentacion
                        MySqlDataReader resp__numanualidades = con_numanualidades.getdatareader(squery_numanualidades);
                        resp__numanualidades.Read();
                        String snumanualidad = validareader("numanualidad", "numanualidad", resp__numanualidades).Text;
                        
                        resp__numanualidades.Close();
                        con_numanualidades.Cerrarconexion();

                        //sonsultamos las anualidades secuancias 25 años em teoria 5 de 5 
                        conect con_numanualidades_sec = new conect();
                        String squery_numanualidades_sec = "select count(*) As numsec from anialidades_md_nuevos where casoid  = " + sCasoidactual + ";";//consultamos la fecha presentacion
                        MySqlDataReader resp__numanualidades_sec = con_numanualidades_sec.getdatareader(squery_numanualidades_sec);
                        resp__numanualidades_sec.Read();
                        String sCasoFechaInternacional_numanualidades = validareader("numsec", "numsec", resp__numanualidades_sec).Text;
                        
                        resp__numanualidades_sec.Close();
                        con_numanualidades_sec.Cerrarconexion();

                        resp_consultaestipo.Close();




                        //calculamos con la fecha internacional
                        DateTime sFechavigenciacalculada = DateTime.Now;
                        if (sCasoFechaInternacionalselect != "" && sCasoFechaInternacionalselect !=null)//calculamos con la fecha internacional si es que está capturada
                        {
                            sFechavigenciacalculada = DateTime.ParseExact(sCasoFechaInternacionalselect.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);//.ToString("yyyy-MM-dd");
                            sFechavigenciacalculada = sFechavigenciacalculada.AddYears(int.Parse(snumanualidad));
                        }
                        else {
                            if (sCasoFechaRecepcionselect!="") {//calculamos con la fecha presentacion ya que no está la fecha internacional
                                sFechavigenciacalculada = DateTime.ParseExact(sCasoFechaRecepcionselect.Substring(0,10), "dd/MM/yyyy", CultureInfo.InvariantCulture);//.ToString("yyyy-MM-dd");
                                sFechavigenciacalculada = sFechavigenciacalculada.AddYears(int.Parse(snumanualidad));
                            }
                        
                        }

                        if (int.Parse(snumanualidad)==0 && int.Parse(sCasoFechaInternacional_numanualidades) >0) {//calculamos de nuevo en caso de que sean tipos diseños nuevo y existan secuencias de anuualidades
                            if (sCasoFechaInternacionalselect != "" && sCasoFechaInternacionalselect != null)//diseños secuencias con fecha internacional
                            {
                                sFechavigenciacalculada = DateTime.ParseExact(sCasoFechaInternacionalselect.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);//.ToString("yyyy-MM-dd");
                                sFechavigenciacalculada = sFechavigenciacalculada.AddYears(int.Parse(sCasoFechaInternacional_numanualidades)*5);
                            }
                            else
                            {
                                if (sCasoFechaRecepcionselect != "")//diseños secuencias con fecha presentación
                                {
                                    sFechavigenciacalculada = DateTime.ParseExact(sCasoFechaRecepcionselect.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);//.ToString("yyyy-MM-dd");
                                    sFechavigenciacalculada = sFechavigenciacalculada.AddYears(int.Parse(sCasoFechaInternacional_numanualidades)*5);
                                }
                            }
                        }


                        String sFechavigencia = sFechavigenciacalculada.ToString("yyyy-MM-dd");//DateTime.ParseExact(tbFechavigencia.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                        if (sFechavigenciacalculada == DateTime.Today) {
                            MessageBox.Show("No se pudo calcular la fecha vigencia");
                            new filelog("linea 573 Capturadetitulo.cs ", "no se pudo calcular la fecha de Vigencia");
                            sFechavigencia = "0000-00-00";
                        }


                        //actualizamos los datos del caso en la db
                        conect con_4 = new conect();
                        //String sFechavigencia = sFechavigenciacalculada.ToString("yyyy-MM-dd");//DateTime.ParseExact(tbFechavigencia.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                        String sFecha_pruebauso = DateTime.ParseExact(tbFechadeclaracionuso.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                        String updateestatuscaso = "UPDATE `" + sTablaconsulta + "` SET `EstatusCasoId` = '" + sEstatusactual + "', CasoNumConcedida = '" + tb_numconcedida.Text + "', CasoFechaConcesion = '"
                            + DocumentoFecha + "', CasoFechaPruebaUsoSig='" + sFecha_pruebauso + "', CasoFechaVigencia = '" + sFechavigencia + "' WHERE (`CasoId` = '" + sCasoidactual + "');";
                        MySqlDataReader resp_updatecaso = con_4.getdatareader(updateestatuscaso);
                        if (resp_updatecaso != null)
                        {
                            MessageBox.Show("Documento agregado correctamente.");
                            DialogResult = DialogResult.OK;
                            con_4.Cerrarconexion();
                            resp_updatecaso.Close();
                            capform.Show();
                            this.Close();
                        }
                        
                        //Fin //actualizamos los datos del caso en la db
                    }
                    catch (Exception E)
                    {
                        //no hay cambio de estatus porque este documento no esta asignado para cambiar de estatus.
                        //DialogResult = DialogResult.OK;
                        new filelog("linea 601 Capturadetitulo.cs ", E.Message);
                        //MessageBox.Show("Documento agregado correctamente.");
                        if (sBAnderadesdecaso)
                        {
                            DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            capform.Show();
                            this.Close();
                        }
                    }
                }
                //Plazsos que se deben generar por grupo de caso
                if (SubTipoDocumentoId != "203")
                {
                    switch (sGrupocaso)
                    {
                        case "1":
                            {//plazos para patentes
                                DateTime sFechanotificacion = DateTime.Parse(tbFechanotificacion.Text);//new DateTime(tbFechanotificacion.Text,"");
                                //agregaplazo(sCasoidactual, sgTiposolicitudid, SubTipoDocumentoId, sFechanotificacion, documentoid);
                            }
                            break;
                        case "2":
                            {//plazos para marcas
                             //Al subir un título deben agregarse el plazo de Renovación y el plazo de declaración de Uso
                                DateTime sFechanotificacion = DateTime.Parse(tbFechanotificacion.Text);//new DateTime(tbFechanotificacion.Text,"");
                                                                                                       //agregaplazo(sCasoidactual, sgTiposolicitudid, SubTipoDocumentoId, sFechanotificacion, documentoid);

                                /*Además de agregar el plazo de reportar al cliente agregamos los que esten en la configuracion en la tabla
                                 SELECT * FROM plazop_por_subtipodocumentoid where habilitado = 1 and SubTipoDocumentoId = 'el evento actual';
                                 */

                                conect con_dos = new conect();
                                String sGetids = "SELECT * FROM plazop_por_subtipodocumentoid where habilitado = 1 and SubTipoDocumentoId = '" + SubTipoDocumentoId + "';";
                                MySqlDataReader resp_getids = con_dos.getdatareader(sGetids);
                                int iIndiceids = 0;
                                while (resp_getids.Read())
                                {
                                    //sArrayids[iIndiceids] = validareader("Casoid", "Casoid", resp_getids).Text;
                                    //iIndiceids++;

                                    //debemos mandar a llamar a una funcion que genere el plazo segun sea los parametros consultados
                                    //generaplazos
                                    String sTipoplazosid = validareader("tipoplazos", "tipoplazos", resp_getids).Text;

                                    String sdays = validareader("days", "days", resp_getids).Text;
                                    String smonths = validareader("months", "months", resp_getids).Text;
                                    String syears = validareader("years", "years", resp_getids).Text;

                                    String sFechacampo = validareader("fecha_nombre_campo_calcular", "fecha_nombre_campo_calcular", resp_getids).Text;
                                    /*calculamos la fecha de notificacion, fecha de plazo regular , fecha de plazo regular 3 meses, fecha de plazo regular 4 meses*/
                                    //solo para este caso es cafechavigencia, pero no siempre puede ser esa , puede ser valida la fecha del caso cualquiera pero debe estar 
                                    //en la tabla caso_marcas

                                    //CasoFechavigencia
                                    String sValorcampo = get_valorcampo(sFechacampo, "caso_marcas", " Where casoid =" + sCasoidactual + " and Tiposolicitudid = " + sgTiposolicitudid);
                                    DateTime sFechacalculado = DateTime.ParseExact(sValorcampo.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);//
                                    sFechacalculado = sFechacalculado.AddDays(int.Parse(sdays));
                                    sFechacalculado = sFechacalculado.AddMonths(int.Parse(smonths));
                                    sFechacalculado = sFechacalculado.AddYears(int.Parse(syears));
                                    //generaplazos(sCasoidactual, sgTiposolicitudid, documentoid, sTipoplazosid, "", sFechacalculado.ToString("yyyy/MM/dd"),"", "");
                                }
                                resp_getids.Close();
                                con_dos.Cerrarconexion();

                            }
                            break;
                        case "3": { } break;
                        case "4": { } break;
                        case "5": { } break;
                        case "6": { } break;
                        case "7": { } break;
                        case "8": { } break;
                        default:
                            {
                            }
                            break;
                    }
                }


            
            }
            catch (Exception Ex) { 
            }
        }

        public void actcasomarcas(String DocumentoFecha, String SubTipoDocumentoId, String documentoid)
        {
            try
            {
                //aqui hacemos el update para cambiar el estatus y posteriormente agregar los plazos

                //SubTipoDocumentoId para obtener SubTipoDocumentoIndTipo
                conect con_5 = new conect();
                String sconsultaeindtipo = "select SubTipoDocumentoIndTipo from subtipodocumento where SubTipoDocumentoId = " + SubTipoDocumentoId;
                MySqlDataReader resp_consultaestipo = con_5.getdatareader(sconsultaeindtipo);
                resp_consultaestipo.Read();
                String sSubTipoDocumentoIndTipo = validareader("SubTipoDocumentoIndTipo", "SubTipoDocumentoIndTipo", resp_consultaestipo).Text;
                if (resp_consultaestipo != null)
                {

                    con_5.Cerrarconexion();
                    conect con_3 = new conect();
                    String sConsultaestatussiguiente = "select * from subtipodocumentoestatuscaso where SubTipoDocumentoIndTipo = " +
                                                        sSubTipoDocumentoIndTipo +
                                                        " and grupoid = " + sGrupocaso;
                    MySqlDataReader resp_consultaestatuscaso = con_3.getdatareader(sConsultaestatussiguiente);
                    resp_consultaestatuscaso.Read();
                    try
                    {
                        String sEstatusactual = validareader("EstatusCasoId", "EstatusCasoId", resp_consultaestatuscaso).Text;
                        con_3.Cerrarconexion();
                        resp_consultaestatuscaso.Close();
                        //Aqui debemos actualizar la fecha de vigencia
                        //para eso debemos preguntar cuantas anualidades tiene ese tipo ya sean secuencias o anualidades y a partir de ahí debemos sumar ese numero en años a la 
                        //fecha de presentación
                        conect con_fechas = new conect();
                        String squeryfechas = "select * from caso_marcas where casoid = " + sCasoidactual + ";";//consultamos la fecha presentacion
                        MySqlDataReader resp_selectfechas = con_fechas.getdatareader(squeryfechas);
                        resp_selectfechas.Read();
                        String sCasoFechaInternacionalselect = validareader("CasoFechaInternacional", "CasoFechaInternacional", resp_selectfechas).Text;
                        String sCasoFechaRecepcionselect = validareader("CasoFechaRecepcion", "CasoFechaRecepcion", resp_selectfechas).Text;


                        resp_selectfechas.Close();
                        con_fechas.Cerrarconexion();
                        resp_consultaestipo.Close();

                        //calculamos con la fecha internacional
                        DateTime sFechavigenciacalculada = DateTime.ParseExact(tbFechavigencia.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        
                        String sFechavigencia = sFechavigenciacalculada.ToString("yyyy-MM-dd");//DateTime.ParseExact(tbFechavigencia.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                        


                        //actualizamos los datos del caso en la db
                        conect con_4 = new conect();
                        //String sFechavigencia = sFechavigenciacalculada.ToString("yyyy-MM-dd");//DateTime.ParseExact(tbFechavigencia.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                        String sFecha_pruebauso = DateTime.ParseExact(tbFechadeclaracionuso.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                        String updateestatuscaso = "UPDATE `" + sTablaconsulta + "` SET `EstatusCasoId` = '" + sEstatusactual + "', CasoNumConcedida = '" + tb_numconcedida.Text + "', CasoFechaConcesion = '"
                            + DocumentoFecha + "', CasoFechaDeclaUso='" + sFecha_pruebauso + "', CasoFechaVigencia = '" + sFechavigencia + "' WHERE (`CasoId` = '" + sCasoidactual + "');";
                        MySqlDataReader resp_updatecaso = con_4.getdatareader(updateestatuscaso);
                        if (resp_updatecaso != null)
                        {
                            MessageBox.Show("Documento agregado correctamente.");
                            DialogResult = DialogResult.OK;
                            con_4.Cerrarconexion();
                            resp_updatecaso.Close();
                            capform.Show();
                            this.Close();
                        }

                        //Fin //actualizamos los datos del caso en la db
                    }
                    catch (Exception E)
                    {
                        //no hay cambio de estatus porque este documento no esta asignado para cambiar de estatus.
                        //DialogResult = DialogResult.OK;
                        new filelog("linea 601 Capturadetitulo.cs ", E.Message);
                        //MessageBox.Show("Documento agregado correctamente.");
                        if (sBAnderadesdecaso)
                        {
                            DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            capform.Show();
                            this.Close();
                        }
                    }
                }
                //Plazsos que se deben generar por grupo de caso
                if (SubTipoDocumentoId != "203")
                {
                    switch (sGrupocaso)
                    {
                        case "1":
                            {//plazos para patentes
                                DateTime sFechanotificacion = DateTime.Parse(tbFechanotificacion.Text);//new DateTime(tbFechanotificacion.Text,"");
                                //agregaplazo(sCasoidactual, sgTiposolicitudid, SubTipoDocumentoId, sFechanotificacion, documentoid);
                            }
                            break;
                        case "2":
                            {//plazos para marcas
                             //Al subir un título deben agregarse el plazo de Renovación y el plazo de declaración de Uso
                                DateTime sFechanotificacion = DateTime.Parse(tbFechanotificacion.Text);//new DateTime(tbFechanotificacion.Text,"");
                                                                                                       //agregaplazo(sCasoidactual, sgTiposolicitudid, SubTipoDocumentoId, sFechanotificacion, documentoid);

                                /*Además de agregar el plazo de reportar al cliente agregamos los que esten en la configuracion en la tabla
                                 SELECT * FROM plazop_por_subtipodocumentoid where habilitado = 1 and SubTipoDocumentoId = 'el evento actual';
                                 */

                                conect con_dos = new conect();
                                String sGetids = "SELECT * FROM plazop_por_subtipodocumentoid where habilitado = 1 and SubTipoDocumentoId = '" + SubTipoDocumentoId + "';";
                                MySqlDataReader resp_getids = con_dos.getdatareader(sGetids);
                                int iIndiceids = 0;
                                while (resp_getids.Read())
                                {
                                    //sArrayids[iIndiceids] = validareader("Casoid", "Casoid", resp_getids).Text;
                                    //iIndiceids++;

                                    //debemos mandar a llamar a una funcion que genere el plazo segun sea los parametros consultados
                                    //generaplazos
                                    String sTipoplazosid = validareader("tipoplazos", "tipoplazos", resp_getids).Text;

                                    String sdays = validareader("days", "days", resp_getids).Text;
                                    String smonths = validareader("months", "months", resp_getids).Text;
                                    String syears = validareader("years", "years", resp_getids).Text;

                                    String sFechacampo = validareader("fecha_nombre_campo_calcular", "fecha_nombre_campo_calcular", resp_getids).Text;
                                    /*calculamos la fecha de notificacion, fecha de plazo regular , fecha de plazo regular 3 meses, fecha de plazo regular 4 meses*/
                                    //solo para este caso es cafechavigencia, pero no siempre puede ser esa , puede ser valida la fecha del caso cualquiera pero debe estar 
                                    //en la tabla caso_marcas

                                    //CasoFechavigencia
                                    String sValorcampo = get_valorcampo(sFechacampo, "caso_marcas", " Where casoid =" + sCasoidactual + " and Tiposolicitudid = " + sgTiposolicitudid);
                                    DateTime sFechacalculado = DateTime.ParseExact(sValorcampo.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);//
                                    sFechacalculado = sFechacalculado.AddDays(int.Parse(sdays));
                                    sFechacalculado = sFechacalculado.AddMonths(int.Parse(smonths));
                                    sFechacalculado = sFechacalculado.AddYears(int.Parse(syears));
                                    //generaplazos(sCasoidactual, sgTiposolicitudid, documentoid, sTipoplazosid, "", sFechacalculado.ToString("yyyy/MM/dd"),"", "");
                                }
                                resp_getids.Close();
                                con_dos.Cerrarconexion();

                            }
                            break;
                        case "3": { } break;
                        case "4": { } break;
                        case "5": { } break;
                        case "6": { } break;
                        case "7": { } break;
                        case "8": { } break;
                        default:
                            {
                            }
                            break;
                    }
                }



            }
            catch (Exception Ex)
            {
            }
        }

        public String get_valorcampo(String sCampotabla, String sNombretabla, String sCondicion) {
            try {
                String sCampovalor = "";
                String sQWhere = "";
                conect conect_plazosid_relacion = new conect();
                if (sCondicion!="")
                {
                    sQWhere = sCondicion;
                }

                String sQuery_plazos_relacion_general = "select " + sCampotabla + " from " + sNombretabla + " " + sQWhere;
                MySqlDataReader resp_plazos_relacion = conect_plazosid_relacion.getdatareader(sQuery_plazos_relacion_general);
                while (resp_plazos_relacion.Read())
                {
                    sCampovalor = validareader(sCampotabla, sCampotabla, resp_plazos_relacion).Text;
                }
                resp_plazos_relacion.Close();
                conect_plazosid_relacion.Cerrarconexion();
                return sCampovalor;
            }catch(Exception ex){
                return "";
            }
        }

        public String generaplazos(String sCasoid, String sComboTiposolicitud, String documentoid, String tipoplazoid_impi, String sFechanotificacion, String ssFechaplazoregular, String ssFechaplazoregular3meses, String ssFechaplazoregular4meses)
        {
            try {
                String sPlazosid = "";
                conect conect_plazosid = new conect();
                String sQuery_plazos = " INSERT INTO `plazos`(`Plazosid`,`CasoId`,`TipoSolicitudId`,`Fecha_creacion`)VALUES " +
                                                " (null, " +
                                                " '" + sCasoid + "', " +
                                                " '" + sComboTiposolicitud + "', " +
                                                " curdate()); ";

                MySqlDataReader resp_plazos = conect_plazosid.getdatareader(sQuery_plazos);
                if (resp_plazos.RecordsAffected == 1)
                {
                    conect conect_plazosid_relacion = new conect();
                    String sQuery_plazos_relacion_general = "select * from plazos order by  plazosid desc limit 1;";
                    MySqlDataReader resp_plazos_relacion = conect_plazosid_relacion.getdatareader(sQuery_plazos_relacion_general);
                    while (resp_plazos_relacion.Read())
                    {
                        sPlazosid = validareader("Plazosid", "Plazosid", resp_plazos_relacion).Text;
                    }
                    resp_plazos_relacion.Close();
                    conect_plazosid_relacion.Cerrarconexion();
                }
                resp_plazos.Close();
                conect_plazosid.Cerrarconexion();

                /*Una vez insertado el plazo general insertamos el detalle con el parametro pasado en la funcion qe debe ser el tipoplazo, plazosid*/
                conect conect_plazoid = new conect();
                String sQuery_plazo_general = " INSERT INTO `plazos_detalle` " +
                                                " (`Plazos_detalleid`, " +
                                                " `Plazosid`, " +
                                                " `documentoid`, " +
                                                " `usuario_creo_plazodetalle`, " +
                                                " `Tipo_plazoid`, " +
                                                " `Estatus_plazoid`, " +
                                                " `Fecha_notificacion`, " +
                                                " `Fecha_Vencimiento`, " +
                                                " `Fecha_vencimiento_3m`, " +
                                                " `Fecha_vencimiento_4m`) " +
                                                " VALUES (null," +
                                                " '" + sPlazosid + "', " +
                                                " '" + documentoid + "', " +
                                                " '" + login.sId + "', " +
                                                " '" + tipoplazoid_impi + "', " +
                                                " '1', " +
                                                " '" + sFechanotificacion + "', " +
                                                " '" + ssFechaplazoregular + "', " +
                                                " '" + ssFechaplazoregular3meses + "', " +
                                                " '" + ssFechaplazoregular4meses + "');";

                //MySqlDataReader resp_plazo = conect_plazoid.getdatareader(sQuery_plazo_general);
                //if (resp_plazo.RecordsAffected == 1)
                //{
                //    /*aqui validamos que se inserto el plazo*/
                //}
                //resp_plazo.Close();
                conect_plazoid.Cerrarconexion();
                return sPlazosid;
            }catch(Exception ex){
                return ex.Message;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbCasonum_KeyDown(object sender, KeyEventArgs e)
        {
            if (!sBAnderadesdecaso)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    button3_Click(sender, e);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
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
                    //openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        filePath = openFileDialog.FileName;
                        aName = filePath.Split('\\');
                        sNamefile = aName[aName.Length - 1];
                    }
                }
                configuracionfiles obj = new configuracionfiles();
                obj.configuracionfilesinicio();

                string ruta = obj.sFileupload + "\\" + sCarpetadocumentos + "\\" + sCarpetacaso;
                if (!Directory.Exists(ruta))//si no existe la carpeta la creamos
                {
                    Directory.CreateDirectory(ruta);
                }
                String sFechanow = DateTime.Now.ToString("yyyyMMddHHmmsss");
                sfilePath = filePath;
                sfilePath_2 = @ruta + "\\" + tbCasonum.Text + "_" + sFechanow +"_ "+ tbExpediente.Text.Replace("/", "") + "_" + cbEsritos.Text.Replace("/", "") + " " + sNamefile.Replace("/", "");
                sfilePath_2 = sfilePath_2.Replace('\t', ' ');
                tbFilename.Text = @"\\" + tbCasonum.Text + "_" + tbExpediente.Text.Replace("/", "") + "_" + cbEsritos.Text.Replace("/", "") + " " + sNamefile;
            }
            catch (Exception E)
            {
                new filelog(login.sId, E.ToString());
            }
        }

        private void tbFechanotificacion_KeyPress(object sender, KeyPressEventArgs e)
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

            if (tbFechanotificacion.Text.Length == 2)
            {
                tbFechanotificacion.Text = tbFechanotificacion.Text + "-";
                tbFechanotificacion.SelectionStart = tbFechanotificacion.Text.Length;

            }
            if (tbFechanotificacion.Text.Length == 5)
            {
                tbFechanotificacion.Text = tbFechanotificacion.Text + "-";
                tbFechanotificacion.SelectionStart = tbFechanotificacion.Text.Length;
            }
        }

        private void tbDocumentofecharecepcion_KeyPress(object sender, KeyPressEventArgs e)
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

            if (tbDocumentofecharecepcion.Text.Length == 2)
            {
                tbDocumentofecharecepcion.Text = tbDocumentofecharecepcion.Text + "-";
                tbDocumentofecharecepcion.SelectionStart = tbDocumentofecharecepcion.Text.Length;

            }
            if (tbDocumentofecharecepcion.Text.Length == 5)
            {
                tbDocumentofecharecepcion.Text = tbDocumentofecharecepcion.Text + "-";
                tbDocumentofecharecepcion.SelectionStart = tbDocumentofecharecepcion.Text.Length;
            }
        }

        private void tbCodigo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                String sCodigo = tbCodigo.Text;//validacion con por lo menos nueve en el tamaño del código en los oficios
                if (sCodigo.Length >= 9)
                {
                    String[] sSeparado = sCodigo.Split('/');
                    tbFolio.Text = sSeparado[sSeparado.Length - 1];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifique el código de barras");
            }
        }

        private void tbFechanotificacion_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechanotificacion);
        }

        private void tbDocumentofecharecepcion_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbDocumentofecharecepcion);
        }

        public void agregaplazo(String sCasoid, String sComboTiposolicitud, String subtipodocumentoid, DateTime dFecha_notificacion_impi, String documentoid)
        {
            try
            {
                String relacion_plazo_subtipodocumentoid = "";
                String tipoplazoid_impi = "";
                String tipoplazoid_avisocliente = "";
                String activo = "";

                String sSubTipoDocumentoIndPlazo = "";
                String sSubTipoDocumentoPlazoMeses = "";
                String sSubTipoDocumentoPlazoDias = "";

                /*Consultamos si genera plazo en la tabla Subtipodocumento y cuantos meses y dias se le suman*/
                //select * from subtipodocumento where subtipodocumentoid = 108;
                /*
                 * SubTipoDocumentoIndPlazo = 1 Indica si genera plazo,
                 * SubTipoDocumentoPlazoMeses = Número de meses que se le suma a la fecha de Notificacion
                 * SubTipoDocumentoPlazoDias = Número de días que se le suma a la fecha de Notificacion adicional a los mesese
                 
                 El resultado es Fecha_Vencimiento_regular_impi
                  
                 */


                /*
                 * Creamos el plazo en la tabla Plazo la relacion con casoid y tiposolicitudid 
                 */
                String sPlazosid = "";
                bool bBanderacreadetalleplazo = false;
                //conect conect_plazosid = new conect();
                //String sQuery_plazos = " INSERT INTO `plazos`(`Plazosid`,`CasoId`,`TipoSolicitudId`,`Fecha_creacion`)VALUES " +
                //                                " (null, " +
                //                                " '" + sCasoid + "', " +
                //                                " '" + sComboTiposolicitud + "', " +
                //                                " curdate()); ";

                //MySqlDataReader resp_plazos = conect_plazosid.getdatareader(sQuery_plazos);
                //if (resp_plazos.RecordsAffected == 1)
                //{
                //    conect conect_plazosid_relacion = new conect();
                //    String sQuery_plazos_relacion_general = "select * from plazos order by  plazosid desc limit 1;";
                //    MySqlDataReader resp_plazos_relacion = conect_plazosid_relacion.getdatareader(sQuery_plazos_relacion_general);
                //    while (resp_plazos_relacion.Read())
                //    {
                //        sPlazosid = validareader("Plazosid", "Plazosid", resp_plazos_relacion).Text;
                //    }
                //    resp_plazos_relacion.Close();
                //    conect_plazosid_relacion.Cerrarconexion();

                //    bBanderacreadetalleplazo = true;
                //    //MessageBox.Show("Se creó un plazo.");
                //    /*aqui validamos que se inserto el plazo*/
                //}
                //resp_plazos.Close();
                //conect_plazosid.Cerrarconexion();


                /* Validamos que tipo de plazo tiene el subtipodocumentoid generado anteriormente 
                 * en la tabla relacion_plazo_subtipodocumento, es quien nos 
                 * indica el plazo impi y el plazo aviso al cliente 
                 * relacionado al subtipodocumentoid
                 */
                int ibBanderacreadetalleplazo_contador = 0;
                if (bBanderacreadetalleplazo)
                {
                    
                    
                        relacion_plazo_subtipodocumentoid = "99";//validareader("subtipodocumentoid", "SubTipoDocumentoId", resp_plazo_relacion).Text;
                        tipoplazoid_impi = "23";
                        /*Validamos que si es satisfecho forma no genere el plazo de accion oficial*/
                        //if (!(tipoplazoid_impi == "4" && relacion_plazo_subtipodocumentoid == "100"))
                        if (false)
                        {
                            /**
                             Consultamos las fechas del plazo
                             */
                            conect conect_plazoid_activo = new conect();
                            String sQuery_plazo_relacion_activo = " select * from subtipodocumento where SubTipoDocumentoId = " + relacion_plazo_subtipodocumentoid;
                            MySqlDataReader resp_plazo_activo = conect_plazoid_activo.getdatareader(sQuery_plazo_relacion_activo);
                            while (resp_plazo_activo.Read())
                            {

                                sSubTipoDocumentoIndPlazo = validareader("SubTipoDocumentoIndPlazo", "SubTipoDocumentoId", resp_plazo_activo).Text;
                                sSubTipoDocumentoPlazoMeses = validareader("SubTipoDocumentoPlazoMeses", "SubTipoDocumentoId", resp_plazo_activo).Text;
                                sSubTipoDocumentoPlazoDias = validareader("SubTipoDocumentoPlazoDias", "SubTipoDocumentoId", resp_plazo_activo).Text;
                            }
                            resp_plazo_activo.Close();
                            conect_plazoid_activo.Cerrarconexion();


                            //tipoplazoid_avisocliente = validareader("tipoplazoid_avisocliente", "relacion_plazo_subtipodocumentoid", resp_plazo_relacion).Text;
                            activo = "1";//validareader("aviso", "aviso", resp_plazo_relacion).Text;

                            /*Calculamos las fechas a insertar en el plazo*/
                            DateTime sFechaplazoregular = dFecha_notificacion_impi;
                            try
                            {
                                sFechaplazoregular = sFechaplazoregular.AddMonths(Int16.Parse(sSubTipoDocumentoPlazoMeses));
                                sFechaplazoregular = sFechaplazoregular.AddDays(Int16.Parse(sSubTipoDocumentoPlazoDias));
                            }
                            catch (Exception Ex)
                            {

                            }
                            DateTime sFechaplazoregular3meses = sFechaplazoregular.AddMonths(1);
                            DateTime sFechaplazoregular4meses = sFechaplazoregular3meses.AddMonths(1);

                            /*Aqui vamos, solo falta insertar el plazo y llamar a ésta función*/

                            //.AddMonths(Int16.Parse());


                            /*aqui validamos que se inserto el plazo*/
                            /*
                             Necesitamos un tipoplazoid_impi y 
                             * Fecha_notificacion_impi  <-----
                             * Fecha_Vencimiento_regular_impi <-- Calculado
                             * Fecha_vencimiento_3m_impi <-- Calculado
                             * Fecha_vencimiento_4m_impi <-- Calculado
                             */
                            String sFechanotificacion = "'"+dFecha_notificacion_impi.ToString("yyyy-MM-dd")+"'";
                            String ssFechaplazoregular = sFechaplazoregular.ToString("yyyy-MM-dd");
                            String ssFechaplazoregular3meses = "'"+sFechaplazoregular3meses.ToString("yyyy-MM-dd")+"'";
                            String ssFechaplazoregular4meses = "'" + sFechaplazoregular4meses.ToString("yyyy-MM-dd")+"'";
                            if (activo == "1")//Nos indica que es un aviso y sólo aumenta un día a la fecha actual como vigencia
                            {//las demás fechas van vacias
                                sFechanotificacion = "NULL";
                                sFechaplazoregular = DateTime.Today;//asignamos la fecha actual
                                sFechaplazoregular = sFechaplazoregular.AddDays(1);
                                ssFechaplazoregular = sFechaplazoregular.ToString("yyyy-MM-dd");
                                ssFechaplazoregular3meses = "NULL";
                                ssFechaplazoregular4meses = "NULL";
                            }

                            conect conect_plazoid = new conect();
                            String sQuery_plazo_general = " INSERT INTO `plazos_detalle` " +
                                                            " (`Plazos_detalleid`, " +
                                                            " `Plazosid`, " +//
                                                            " `documentoid`, " +//documentoid
                                                            " `usuario_creo_plazodetalle`, " +
                                                            " `Tipo_plazoid`, " +
                                                            " `Estatus_plazoid`, " +
                                                            " `Fecha_notificacion`, " +
                                                            " `Fecha_Vencimiento`, " +
                                                            " `Fecha_vencimiento_3m`, " +
                                                            " `Fecha_vencimiento_4m`) " +
                                                            " VALUES (null," +
                                                            " '" + sPlazosid + "', " +
                                                            " '" + documentoid + "', " +
                                                            " '" + login.sId + "', " +
                                                            " '" + tipoplazoid_impi + "', " +
                                                            " '1', " +
                                                            " " + sFechanotificacion + ", " +
                                                            " '" + ssFechaplazoregular + "', " +
                                                            " " + ssFechaplazoregular3meses + ", " +
                                                            " " + ssFechaplazoregular4meses + ");";

                            //" '" + tipoplazoid_avisocliente + "', " +
                            //" '1', " +
                            //" '" + documentoid + "', " +
                            //" '" + login.sId + "');";

                            MySqlDataReader resp_plazo = conect_plazoid.getdatareader(sQuery_plazo_general);
                            if (resp_plazo.RecordsAffected == 1)
                            {
                                ibBanderacreadetalleplazo_contador++;
                                //MessageBox.Show("Se creó un plazo.");
                                /*aqui validamos que se inserto el plazo*/
                            }
                            resp_plazo.Close();
                            conect_plazoid.Cerrarconexion();
                        
                        // }//cerramos el if de SubTipoDocumentoIndPlazo
                    }
                    //resp_plazo_relacion.Close();
                    //conect_plazoid_relacion.Cerrarconexion();
                }//cerramos el if
                //MessageBox.Show("Se agregraron " + ibBanderacreadetalleplazo_contador + " de subPlazos");

            }
            catch (Exception Ex)
            {

            }
        }

        private void tbFechanotificacion_TextChanged(object sender, EventArgs e)
        {
            //si sabemos que es de tamaño 10 dd/mm/yyyy
            try
            {
                if (tbFechanotificacion.TextLength == 10) {
                    DateTime dDocumentoFecha = DateTime.ParseExact(tbFechanotificacion.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    DateTime dFechapresentacion = DateTime.ParseExact(sFechapresentacion.Substring(0,10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //tbFPresentaciosol.Text = sFechapresentacion;
                    DateTime dFEchalimite = DateTime.ParseExact("2020-11-05", "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    DateTime dFechavigencia;
                    DateTime dDeclaracionuso;
                    if (dFechapresentacion >= dFEchalimite)//validamos que sea mayor al 5 de noviembre de 2020
                    {
                       dFechavigencia = dDocumentoFecha.AddYears(10);//Este calcula la fecha de vigencia de nueva ley 
                    }else {
                        dFechavigencia = dFechapresentacion.AddYears(10);
                    }

                    dDeclaracionuso = dDocumentoFecha.AddYears(3);

                    tbFechavigencia.Text = dFechavigencia.ToString("dd-MM-yyyy");
                    //Fechavigencia = dFechavigencia.AddYears(3);
                    tbFechadeclaracionuso.Text = dDeclaracionuso.ToString("dd-MM-yyyy");
                    
                }
            }catch (Exception Ex) {
                MessageBox.Show("Error: "+Ex.Message);
            }
        }

        public string sFechapresentacion { get; set; }
    }
}
