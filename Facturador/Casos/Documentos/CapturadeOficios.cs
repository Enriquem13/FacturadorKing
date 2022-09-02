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
    public partial class CapturadeOficios : Form
    {
        public captura capform;
        public Form1 login;
        public String sGrupocaso;
        public String sTablaconsulta;
        public String sCasoidactual;
        public String sgTiposolicitudid;
        //public string sgSubTiposolicitud;
        public String sgSubtiposolicitudid;
        public String sEstatusidint;
        public String sCasoidgeneralGlabal = "0";
        public bool sBAnderadesdecaso = false;
        public String sfilePath, sfilePath_2, sCarpetadocumentos, sCarpetacaso;
        public String sFechapresentaciong = "";
        funcionesdicss objfuncionesdicss;
        public String sCasoDisenoClasificacion = "";
        public string sgCasoDisenoClasificacion="";

        public CapturadeOficios(captura capformulario, Form1 log, String sGrupo, String sCasoidgeneral, String sFechapresentacion)
        {
            login = log;
            sGrupocaso = sGrupo;
            capform = capformulario;
            sFechapresentaciong = sFechapresentacion;
            objfuncionesdicss = new funcionesdicss();
            InitializeComponent();
            try
            {
                switch (sGrupocaso)
                {
                    case "1":
                        {
                            sTablaconsulta = "caso_patente";
                            this.Text = this.Text + " ( Grupo Patentes)";
                            this.BackColor = Color.Pink;
                            sCarpetadocumentos = "Patentes";
                        } break;
                    case "2":
                        {
                            sTablaconsulta = "caso_marcas";
                            this.Text = this.Text + " ( Grupo Marcas)";
                            this.BackColor = Color.FromArgb(255, 255, 192);
                            sCarpetadocumentos = "Marcas";
                        } break;
                    case "3":
                        {
                            sTablaconsulta = "caso_contencioso";
                            this.Text = this.Text + " ( Grupo Contencioso)";
                            this.BackColor = Color.Yellow;
                            sCarpetadocumentos = "Casocontencioso";
                        } break;
                    case "4":
                        {
                            sTablaconsulta = "caso_consulta";
                            this.Text = this.Text + " ( Grupo Consulta)";
                            this.BackColor = SystemColors.Control;
                            sCarpetadocumentos = "Consulta";
                        } break;
                    case "5":
                        {
                            sTablaconsulta = "caso_oposicion";
                            this.Text = this.Text + " ( Grupo Oposicion a solicitudes)";
                            this.BackColor = Color.FromArgb(255, 192, 128);
                            sCarpetadocumentos = "Oposicion";
                        } break;
                    case "6":
                        {
                            sTablaconsulta = "";
                            this.Text = this.Text + " ( Grupo Variedades vegetales)";
                            this.BackColor = SystemColors.Control;
                            sCarpetadocumentos = "Variedadesveg";
                        } break;
                    case "7":
                        {
                            sTablaconsulta = "caso_registrodeobra";
                            this.Text = this.Text + " ( Grupo Derechos de autor)";
                            this.BackColor = Color.SkyBlue;
                            sCarpetadocumentos = "Registrodeobra";
                        } break;
                    case "8":
                        {
                            sTablaconsulta = "caso_reservadederechos";
                            this.Text = this.Text + " ( Grupo Reserva de derechos)";
                            this.BackColor = Color.LightGreen;
                            sCarpetadocumentos = "Reservadederechos";
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
                    sCasoDisenoClasificacion = "";
                    cargacasoenform();
                    sBAnderadesdecaso = true;
                    sCarpetacaso = tbCasonum.Text;
                }

                if (login.sUsuarioIndAdmin == "1")
                {
                    cbMostraroficios.Enabled = true;
                }
                else
                {
                    cbMostraroficios.Enabled = false;
                }
                
            }catch(Exception E){
                new filelog(login.sId, E.ToString());
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

        private void button1_Click(object sender, EventArgs e)
        {//botón de salir en captura oficios
            if (sBAnderadesdecaso)
            {
                this.Close();
            }
            else {
                capform.Show();
                this.Close();
            }
        }
        public void cargacasoenform() {
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
                        String EstatusCasoId = validareader("EstatusCasoId", "Casoid", respuestastring6).Text;
                        String sConsultaestatus = "SELECT * FROM estatuscaso where EstatusCasoId = '" + EstatusCasoId + "' ";
                        sCasoidactual = validareader("Casoid", "Casoid", respuestastring6).Text;
                        //CasoDisenoClasificacion
                        if (sTablaconsulta == "caso_patente") {
                            sCasoDisenoClasificacion = validareader("CasoDisenoClasificacion", "CasoDisenoClasificacion", respuestastring6).Text;
                        }

                        sgTiposolicitudid = validareader("TipoSolicitudid", "Casoid", respuestastring6).Text;

                        sgSubtiposolicitudid = validareader("SubTipoSolicitudId", "Casoid", respuestastring6).Text;
                        //consultamos el caso de que sea caso_patente del grupo 1 el tipo diseño clasificacion
                        if (sGrupocaso=="1") {
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
                        /*Debemos agrgear los estatus y escritos disponibles segun las tablas y reaccionar dependiendo eso*/
                        String sQueryescritosdisponibles = "SELECT " +//gruposubtipodocumento
                                                           "     * " +
                                                           " FROM " +
                                                           "    estatuscasosubtipodocumento, " +
                                                           "    subtipodocumento " +
                                                           " WHERE " +
                                                           "     estatuscasosubtipodocumento.Estatuscasoid = " + sEstatusidint + "  " +
                                                           "         AND estatuscasosubtipodocumento.GrupoId = " + sGrupocaso +//El grupo falta validarlo con una variable dependiendo el tipo del caso 
                                                           "         AND subtipodocumento.SubTipoDocumentoId = estatuscasosubtipodocumento.SubTipoDocumentoId " +
                                                           "         AND subtipodocumento.TipoDocumentoId = 1 " +//en esta pantalla debe ser fijo el número 1 puesto que estamos en escritoa
                                                           "         AND subtipodocumento.SubTipoDocumentoIndAct = 1 " +// el valos uno debe ser fijo puesto que validamos que el documento esté activo
                                                           "         group by estatuscasosubtipodocumento.SubTipoDocumentoId order by subtipodocumento.SubTipoDocumentoDescrip;";
                        //String sQueryescritosdisponibless = "select * from estatuscasosubtipodocumento, subtipodocumento where estatuscasosubtipodocumento.EstatusCasoId = " + sEstatusidint + " and estatuscasosubtipodocumento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId and estatuscasosubtipodocumento.GrupoId =1 and subtipodocumento.TipoDocumentoId = 2 group by subtipodocumento.SubTipoDocumentoId;";
                        //String sQueryescritosdisponibles = "select subtipodocumento.SubTipoDocumentoDescrip, estatuscasosubtipodocumentores.SubTipoDocumentoId from estatuscasosubtipodocumentores, subtipodocumento where estatuscasoid = " + tbEstatus.Text + " and estatuscasosubtipodocumentores.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId;";
                        conect con4 = new conect();
                        MySqlDataReader resp_escritos = con4.getdatareader(sQueryescritosdisponibles);
                        while (resp_escritos.Read())
                        {
                            String sIdescritp = validareader("SubTipoDocumentoId", "SubTipoDocumentoId", resp_escritos).Text;
                            cbEsritos.Items.Add(validareader_documentos("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos, sIdescritp));//Estatus
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
                MessageBox.Show("Warning: "+ E);
                new filelog(login.sId, E.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int isGrop = Int32.Parse(sGrupocaso);
            bCasoparadoc bCaso = new bCasoparadoc(isGrop, capform, login);
            if (bCaso.ShowDialog() == DialogResult.OK)
            { 
                String sQueryconulta = "";
                try
                {
                    sCasoidgeneralGlabal = bCaso.sCasoid;
                    cargacasoenform();
                //    conect con1 = new conect();
                //    sQueryconulta = "SELECT * FROM " + sTablaconsulta + " where Casoid = " + bCaso.sCasoid + "";
                //    MySqlDataReader respuestastring6 = con1.getdatareader(sQueryconulta);
                //    if (respuestastring6 != null)
                //    {
                //        while (respuestastring6.Read())
                //        {
                //            tbCasonum.Text = validareader("CasoNumero", "Casoid", respuestastring6).Text;
                //            String EstatusCasoId = validareader("EstatusCasoId", "Casoid", respuestastring6).Text;
                //            String sConsultaestatus = "SELECT * FROM estatuscaso where EstatusCasoId = '" + EstatusCasoId + "' ";
                //            sCasoidactual = validareader("Casoid", "Casoid", respuestastring6).Text;
                //            MySqlDataReader resp_estatus = con1.getdatareader(sConsultaestatus);
                //            while (resp_estatus.Read())
                //            {
                //                tbEstatus.Text = validareader("EstatusCasoDescrip", "EstatusCasoId", resp_estatus).Text;//Estatus
                //                sEstatusidint = validareader("EstatusCasoId", "EstatusCasoId", resp_estatus).Text;//Estatus
                //            }
                //            resp_estatus.Close();
                //            String sQuerytiposolicitud = "select * from tiposolicitud where TipoSolicitudid = " + validareader("TipoSolicitudid", "Casoid", respuestastring6).Text;
                //            MySqlDataReader resp_tiposolicitud = con1.getdatareader(sQuerytiposolicitud);
                //            while (resp_tiposolicitud.Read())
                //            {
                //                tbTipo.Text = validareader("TipoSolicitudDescrip", "TipoSolicitudid", resp_tiposolicitud).Text;//Estatus
                //            }
                //            resp_tiposolicitud.Close();
                //            cbEsritos.Items.Clear();
                //            /*Debemos agrgear los estatus y escritos disponibles segun las tablas y reaccionar dependiendo eso*/
                //            String sQueryescritosdisponibles = "SELECT " +
                //                                               "     * " +
                //                                               " FROM " +
                //                                               "    estatuscasosubtipodocumento, " +
                //                                               "    subtipodocumento " +
                //                                               " WHERE " +
                //                                               "     estatuscasosubtipodocumento.Estatuscasoid = " + sEstatusidint + "  " +
                //                                               "         AND estatuscasosubtipodocumento.GrupoId = " + sGrupocaso +//El grupo falta validarlo con una variable dependiendo el tipo del caso 
                //                                               "         AND subtipodocumento.SubTipoDocumentoId = estatuscasosubtipodocumento.SubTipoDocumentoId " +
                //                                               "         AND subtipodocumento.TipoDocumentoId = 1 " +//en esta pantalla debe ser fijo el número 1 puesto que estamos en escritoa
                //                                               "         AND subtipodocumento.SubTipoDocumentoIndAct = 1 " +// el valos uno debe ser fijo puesto que validamos que el documento esté activo
                //                                               "         group by estatuscasosubtipodocumento.SubTipoDocumentoId order by subtipodocumento.SubTipoDocumentoDescrip;";
                //            //String sQueryescritosdisponibless = "select * from estatuscasosubtipodocumento, subtipodocumento where estatuscasosubtipodocumento.EstatusCasoId = " + sEstatusidint + " and estatuscasosubtipodocumento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId and estatuscasosubtipodocumento.GrupoId =1 and subtipodocumento.TipoDocumentoId = 2 group by subtipodocumento.SubTipoDocumentoId;";
                //            //String sQueryescritosdisponibles = "select subtipodocumento.SubTipoDocumentoDescrip, estatuscasosubtipodocumentores.SubTipoDocumentoId from estatuscasosubtipodocumentores, subtipodocumento where estatuscasoid = " + tbEstatus.Text + " and estatuscasosubtipodocumentores.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId;";
                //            MySqlDataReader resp_escritos = con1.getdatareader(sQueryescritosdisponibles);
                //            while (resp_escritos.Read())
                //            {
                //                cbEsritos.Items.Add(validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos));//Estatus
                //            }
                //            resp_escritos.Close();
                //            //String sQueryresponsables = "select UsuarioName, UsuarioId from usuario;";
                //            //MySqlDataReader resp_responsables = con1.getdatareader(sQueryresponsables);
                //            //while (resp_responsables.Read())
                //            //{
                //            //    cbPreparo.Text = validareader("UsuarioName", "UsuarioId", resp_responsables).Text;//Estatus
                //            //}
                //            //resp_responsables.Close();
                //            /*fin de ciclo escritos*/
                //            tbRegistro.Text = validareader("CasoNumConcedida", "Casoid", respuestastring6).Text;//registro
                //            tbExpediente.Text = validareader("CasoNumeroExpedienteLargo", "Casoid", respuestastring6).Text;
                //            rtbtitulo.Text = validareader("CasoTituloespanol", "Casoid", respuestastring6).Text + " /" + validareader("CasoTituloingles", "Casoid", respuestastring6).Text;
                //        }
                //    }
                }
                catch (Exception E)
                {
                    new filelog(login.sId, E.ToString());
                }
            
            }
            //int isGrop = Int32.Parse(sGrop);
            //bCasoparadoc bCaso = new bCasoparadoc(isGrop, capform, login);
            //if (bCaso.ShowDialog() == DialogResult.OK)
            //{  }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tbCodigo.Text=="")
            {
                MessageBox.Show("Debe escanear el código de barras.");
                if (tbCodigo.CanFocus)
                {
                    tbCodigo.Focus();
                }
                return;
            }
            //cbEsritos

            if (cbEsritos.Text=="")
            {
                MessageBox.Show("Debe seleccionar un tipo de oficio antes de agregar");
                if (cbEsritos.CanFocus)
                {
                    cbEsritos.Focus();
                }
                return;
            }
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

            if (tbFilename.Text=="")
            {
                MessageBox.Show("Debe seleccionar un archivo antes de continuar.");
                if (tbFilename.CanFocus)
                {
                    tbFilename.Focus();
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
                    tbFilename.Text = "";
                    button4.Focus();
                    return;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Archivo Repetido, Seleccione otro Documento ó cambie el nombre");
                tbFilename.Text = "";
                button4.Focus();
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
                                    button4.Focus();
                                    return;
                                }
                                //fin movemos el archivo

                                String casonum = tbCasonum.Text;
                                String SubTipoDocumentoId = (cbEsritos.SelectedItem as ComboboxItem).Value.ToString();
                                String DocumentoCodigoBarras = tbCodigo.Text;
                                //DateTime fecha = DateTime.Now;
                                String DocumentoFechaCaptura = DateTime.Now.ToString().Substring(0, 10).Replace('/', '.');// now();
                                String DocumentoFecha = "";
                                try {
                                    /*este es fecha oficio*/
                                    DocumentoFecha = DateTime.ParseExact(tbDocumentofecharecepcion.Text.Replace('/', '-'), "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbFechacalce.Text;// now();
                                }catch(Exception E){//No hay fecha y de echo no debe haber
                                    DocumentoFecha = "";
                                }
                                

                                String DocumentoFolio = tbFolio.Text;
                                String DocumentoFechaRecepcion = DateTime.ParseExact(tbDocumentofecharecepcion.Text.Replace('/', '-'), "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbDocumentofecharecepcion.Text;
                                String DocumentoFechaNotificacion = DateTime.ParseExact(tbFechanotificacion.Text.Replace('/', '-'), "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbDocumentofecharecepcion.Text;
                                String stbFechanotificacion = DateTime.ParseExact(tbFechanotificacion.Text.Replace('/', '-'), "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbDocumentofecharecepcion.Text;
                                DateTime dFechanotificacion = DateTime.ParseExact(tbFechanotificacion.Text.Replace('/', '-'), "dd-MM-yyyy", CultureInfo.InvariantCulture);//.ToString("yyyy'/'MM'/'dd"); //tbDocumentofecharecepcion.Text;

                                String DocumentoObservacion = rtObservacion.Text;
                                String fechaactual = DateTime.Now.ToString("yyyy/MM/dd");

                                Boolean bAnualidadesexistentes = false;
                                try
                                {
                                    int iNum_ref = 0;
                                    conect con_ref = new conect();
                                    String sGetcasonumero_ref = "select count(*) As num_anualidades  " +
                                                                "from anualidad where CasoId = '" + sCasoidactual +
                                                                "' and TipoSolicitudId = '" + sgTiposolicitudid + "';";
                                    MySqlDataReader respuestastringcasonum_ref = con_ref.getdatareader(sGetcasonumero_ref);
                                    while (respuestastringcasonum_ref.Read())
                                    {
                                        iNum_ref = int.Parse(validareader("num_anualidades", "num_anualidades", respuestastringcasonum_ref).Text);
                                    }
                                    respuestastringcasonum_ref.Close();
                                    con_ref.Cerrarconexion();

                                    if (iNum_ref > 0)
                                    {
                                        //MessageBox.Show("No se pueden generar anualidades, ya existen anualidades para éste caso.");
                                        bAnualidadesexistentes = false;
                                    }
                                    else
                                    {
                                        bAnualidadesexistentes = true;
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    bAnualidadesexistentes = true;
                                }

                                String sFechavigenciaregular = DateTime.ParseExact(tbFechacalce.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd");
                                DateTime dFechavigenciaregular = DateTime.ParseExact(tbFechacalce.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);//.ToString("yyyy'/'MM'/'dd");
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
                                                        " `DocumentoFechaVencimiento`, " +
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
                                                        " '" + DocumentoFechaRecepcion + "', " +/*Fecha de Oficio*/
                                                        " '" + DocumentoFolio + "', " +
                                                        " '" + DocumentoFechaNotificacion + ": ', " +/*Fecha Notificacion*/
                                                        " '" + sFechavigenciaregular + "', " +
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

                                if (resp_escritos != null)
                                {
                                    resp_escritos.Close();
                                    con1.Cerrarconexion();
                                    //Fecha para la nueva ley a partir del 
                                    
                                    //5 de Noviembre de 2020
                                    
                                    DateTime dFechalimitenuevaley = DateTime.ParseExact("18-03-2020", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                    /*validamos el tipo de oficio para poder generar las anualidades en ceros con la fecha de notificacion del oficio mas 4 meses como fecha limite de pago*/
                                    /*Validamos el tipo de escrito para crear las anualidades dependiendo el tipo de patente*/
                                    //if ((SubTipoDocumentoId == "116" || SubTipoDocumentoId == "115") && bAnualidadesexistentes)
                                    //if ((SubTipoDocumentoId == "116" || SubTipoDocumentoId == "115"|| SubTipoDocumentoId == "1252" || SubTipoDocumentoId == "1248")   && bAnualidadesexistentes)
                                    if (SubTipoDocumentoId == "116" || SubTipoDocumentoId == "115" || SubTipoDocumentoId == "1252" || SubTipoDocumentoId == "1248" || SubTipoDocumentoId == "1246") 
                                    {//El tipo de Escrito validamos si es el pago de título con o sin figuras para generar las anualidades
                                            DateTime dDocumentoFechaNotificacion = DateTime.ParseExact(DocumentoFechaNotificacion, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                                        //DateTime fechamas4meses = dDocumentoFechaNotificacion.AddMonths(4);
                                        //DocumentoFechaNotificacion = fechamas4meses.ToString("yyyy/MM/dd");
                                        DocumentoFechaNotificacion = dDocumentoFechaNotificacion.ToString("yyyy/MM/dd");
                                        //dat.AddMonths(ctr)
                                        DateTime dsFechapresentaciong = DateTime.ParseExact(sFechapresentaciong, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                        //queda
                                        switch (sgTiposolicitudid)//Casos para generar anualidades
                                        {
                                            case "1"://Solicitud de Patentes
                                                {
                                                    generaanualialidades(20, DocumentoFechaNotificacion, sFechapresentaciong, sCasoidactual, sgTiposolicitudid);
                                                } break;
                                            case "2"://Modelo de utilidad
                                                {
                                                    DateTime dFechalimitenuevaley_Modelos = DateTime.ParseExact("05-11-2020", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                                    if (dsFechapresentaciong >= dFechalimitenuevaley_Modelos)/*5 DE Noviembre 2020*/
                                                    {
                                                        //int iAnualidadesinsert = anualidadesMD(dsFechapresentaciong.ToString("yyyy-MM-dd"), 10, sCasoidgeneralGlabal, sgTiposolicitudid);
                                                        generaanualialidades(15, DocumentoFechaNotificacion, sFechapresentaciong, sCasoidactual, sgTiposolicitudid); //se puede extender cada 5 años hasta los 25
                                                    }
                                                    else
                                                    {
                                                        generaanualialidades(10, DocumentoFechaNotificacion, sFechapresentaciong, sCasoidactual, sgTiposolicitudid); //se puede extender cada 5 años hasta los 25
                                                    }
                                                } break;
                                            case "3"://Diseño (Modelo)
                                                {//validamos que sean anualidades a parrtir del 26-04-2018
                                                    dFechalimitenuevaley = DateTime.ParseExact("26-04-2018", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                                    if (sCasoDisenoClasificacion == "5")
                                                    {
                                                        int iAnualidadesinsert = anualidadesMD(dsFechapresentaciong.ToString("yyyy-MM-dd"), stbFechanotificacion, 25, sCasoidgeneralGlabal, sgTiposolicitudid);
                                                    }
                                                    else {
                                                        if (dsFechapresentaciong >= dFechalimitenuevaley)
                                                        {
                                                            int iAnualidadesinsert = anualidadesMD(dsFechapresentaciong.ToString("yyyy-MM-dd"), stbFechanotificacion, 25, sCasoidgeneralGlabal, sgTiposolicitudid);
                                                        }
                                                        else
                                                        {
                                                            generaanualialidades(15, DocumentoFechaNotificacion, sFechapresentaciong, sCasoidactual, sgTiposolicitudid);//se puede extender cada 5 años hasta los 25
                                                        }
                                                    }
                                                }break;
                                            case "4"://Diseño Industrial (Dibujo)
                                                {//validamos que sean anualidades a parrtir del 5 de noviembre
                                                    dFechalimitenuevaley = DateTime.ParseExact("26-04-2018", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                                    //si es tipo 5 creamos 25 años sin importar la fecha, 
                                                    //ya que es una peticion del caso que se riga como los nuevos casos para este tipo
                                                    if (sCasoDisenoClasificacion == "5")
                                                    {
                                                        int iAnualidadesinsert = anualidadesMD(dsFechapresentaciong.ToString("yyyy-MM-dd"), stbFechanotificacion, 25, sCasoidgeneralGlabal, sgTiposolicitudid);
                                                    }
                                                    else {
                                                        if (dsFechapresentaciong >= dFechalimitenuevaley)
                                                        {
                                                            //MessageBox.Show("Se generan anualidades de la nueva ley 5 noviembre");
                                                            int iAnualidadesinsert = anualidadesMD(dsFechapresentaciong.ToString("yyyy-MM-dd"), stbFechanotificacion, 25, sCasoidgeneralGlabal, sgTiposolicitudid);
                                                        }
                                                        else
                                                        {
                                                            generaanualialidades(15, DocumentoFechaNotificacion, sFechapresentaciong, sCasoidactual, sgTiposolicitudid);//se puede extender cada 5 años hasta los 25
                                                        }
                                                    }

                                                    
                                                }break;
                                            case "5"://Registro de patente
                                                {

                                                } break;
                                            case "19"://Esquea trazado de circuito
                                                {
                                                    generaanualialidades(20, DocumentoFechaNotificacion, sFechapresentaciong, sCasoidactual, sgTiposolicitudid);
                                                } break;
                                        }
                                    }
                                    conect con_select = new conect();
                                    MySqlDataReader resp_docid = con_select.getdatareader("select DocumentoId from documento order by documentoid desc limit 1;");
                                    if (resp_docid != null)
                                    {
                                        resp_docid.Read();

                                        //File.Copy(sfilePath, sfilePath_2);
                                        String documentoid = validareader("DocumentoId", "DocumentoId", resp_docid).Text;
                                        resp_docid.Close();
                                        con_select.Cerrarconexion();

                                        //creamos el insert para agrear la relacion del documento
                                        conect con_insert_ = new conect();
                                        String insertrelaciondoc = @" INSERT INTO `relaciondocumento` " +
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
                                        MySqlDataReader esp_insertrelaciona = con_insert_.getdatareader(insertrelaciondoc);
                                        if (esp_insertrelaciona != null)
                                        {
                                            esp_insertrelaciona.Close();
                                            con_insert_.Cerrarconexion();
                                            //aqui hacemos el update para cambiar el estatus y posteriormente agregar los plazos
                                            //SubTipoDocumentoId para obtener SubTipoDocumentoIndTipo 
                                            conect con_5 = new conect();
                                            String sconsultaeindtipo = "select SubTipoDocumentoIndTipo, SubTipoDocumentoIndAct from subtipodocumento where SubTipoDocumentoId = " + SubTipoDocumentoId;
                                            MySqlDataReader resp_consultaestipo = con_5.getdatareader(sconsultaeindtipo);
                                            resp_consultaestipo.Read();
                                            String sSubTipoDocumentoIndTipo = validareader("SubTipoDocumentoIndTipo", "SubTipoDocumentoIndTipo", resp_consultaestipo).Text;
                                            String SubTipoDocumentoIndAct = validareader("SubTipoDocumentoIndAct", "SubTipoDocumentoIndTipo", resp_consultaestipo).Text;
                                            resp_consultaestipo.Close();
                                            con_5.Cerrarconexion();

                                            generaplazosplazosdeoficio(SubTipoDocumentoId, documentoid, dFechanotificacion, dFechavigenciaregular, login.sId);

                                            if (SubTipoDocumentoIndAct == "1")//si genera plazo 
                                            {
                                                //para generar un plazo necesitamos, el tipoplazo, casoid, documentoid, plazofecha, usuarioid, estatusplazoid
                                                //En éste momento del evento 
                                                //debemos preguntar si es que éste tipo de documento genera un plazo en la tabla
                                                //relacionplazosubtipodocumento, y si existe que pudieran ser varios plazos, los generamos

                                                //Primero hacemos el dispoaro de todos los plazos que pudieran generarse
                                                //conect conexionplazosconsulta = new conect();
                                                //String sConsultaplazosporsubtipodoc = "Select * from relacionplazosubtipodocumento where subtipodocumentoid = " + SubTipoDocumentoId;
                                                //MySqlDataReader sConsultaplazos = conexionplazosconsulta.getdatareader(sConsultaplazosporsubtipodoc);
                                                //while (sConsultaplazos.Read())
                                                //{


                                                //}
                                                //sConsultaplazos.Close();
                                                //conexionplazosconsulta.Cerrarconexion();

                                                //String sTipoplazo = "4";
                                                ////DateTime.Now.ToString("yyyyMMddHHmmss");                                                
                                                //String sPlazofecha = DateTime.Parse(tbFechacalce.Text).ToString("yyyy/MM/dd");
                                                ////DateTime dPlazofecha =
                                                //String sIdusuario = login.sId;
                                                //String sEstatuplazoid = "1";// es uno porque el plazo se crea con un estatus pendiente
                                                //generarplazo(sTipoplazo, sCasoidactual, documentoid, sPlazofecha, sIdusuario, sEstatuplazoid);

                                                //generarplazos(sCasoidactual, sDocumentoid);
                                                    /*
                                                     Aquí agregaremos el plazo
                                                     */
                                                /* Tenemos que validar el tipo de fecha a convertir */
                                                DateTime sFechanotificacion = DateTime.Parse(tbFechanotificacion.Text);//new DateTime(tbFechanotificacion.Text,"");
                                                //agregaplazo(sCasoidactual, sgTiposolicitudid, SubTipoDocumentoId, sFechanotificacion, documentoid); //cambio de comportamineto
                                                
                                            }
                                            if (sSubTipoDocumentoIndTipo != "")//Si hay cambio de estatus hacemos el plazo y el update
                                            {
                                                conect con_3 = new conect();
                                                String sConsultaestatussiguiente = "select * from subtipodocumentoestatuscaso where SubTipoDocumentoIndTipo = " +
                                                                                    sSubTipoDocumentoIndTipo +
                                                                                    " and grupoid = " + sGrupocaso;
                                                //validar que no cambie el estatus 
                                                MySqlDataReader resp_consultaestatuscaso = con_3.getdatareader(sConsultaestatussiguiente);
                                                resp_consultaestatuscaso.Read();
                                                try{
                                                    String sEstatusactual = validareader("EstatusCasoId", "EstatusCasoId", resp_consultaestatuscaso).Text;
                                                    resp_consultaestatuscaso.Close();
                                                    con_3.Cerrarconexion();

                                                    conect con_4 = new conect();
                                                    String updateestatuscaso = "UPDATE `" + sTablaconsulta + "` SET `EstatusCasoId` = '" + sEstatusactual + "' WHERE (`CasoId` = '" + sCasoidactual + "');";
                                                    MySqlDataReader resp_updatecaso = con_4.getdatareader(updateestatuscaso);
                                                    if (resp_updatecaso != null)
                                                    {
                                                        MessageBox.Show("Documento agregado correctamente.");
                                                        resp_updatecaso.Close();
                                                        con_4.Cerrarconexion();
                                                        DialogResult = DialogResult.OK;
                                                        capform.Show();
                                                        this.Close();
                                                    }
                                                    
                                                }catch (Exception E){
                                                    //no hay cambio de estatus porque este documento no esta asignado para cambiar de estatus.

                                                    //DialogResult = DialogResult.OK;
                                                    MessageBox.Show("Documento agregado correctamente.");
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
                                                //resp_consultaestatuscaso.Close();
                                                //con_3.Cerrarconexion();
                                            }
                                            else
                                            { //si no cambia estatus solo agregamos el documento y salimos
                                                MessageBox.Show("Documento agregado correctamente.");
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
                                    }
                                    //resp_docid.Close();
                                    //con_2.Cerrarconexion();
                                }
                                else {
                                    MessageBox.Show("No se pudo ingresar el documento.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Debe seleccionar un caso y un escrito para poder guardar");
                            }

                        }
                        catch (Exception E)
                        {
                            MessageBox.Show("Error: " + E.ToString());
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
        public String generaplazo(){
            String sResult = "";
            String sQuery_plazo_relacion_general = "";
            try {
                conect conect_plazoid_relacion = new conect();
                sQuery_plazo_relacion_general = " INSERT INTO `plazos` " +
                                                        " (`Plazosid`, " +
                                                        " `CasoId`, " +
                                                        " `TipoSolicitudId`, " +
                                                        " `Fecha_creacion`) " +
                                                        " VALUES " +
                                                        " (Plazosid, " +
                                                        " '"+ sCasoidactual + "', " +
                                                        " '"+ sgTiposolicitudid + "', " +
                                                        " now() " +
                                                        " ); ";
                MySqlDataReader resp_plazo_relacion = conect_plazoid_relacion.getdatareader(sQuery_plazo_relacion_general);
                
                if (resp_plazo_relacion.RecordsAffected>0) { //se inserto correctamente
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
            catch (Exception exs){
                new filelog("insert plazos query:"+ sQuery_plazo_relacion_general, ""+exs.Message);
            }
            return sResult;
        }
        public void generaplazosplazosdeoficio(String subtipodocumentoid, String documentoid, DateTime plazofechaNotificacion, DateTime plazofecha, String usuarioid) {
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
                String sTipoplazoidanterior = "" , tipoplazoid_impi="";
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

                    if (sCasogrupo!="") { //si existe el filtro de caso
                        if (sGrupocaso!= sCasogrupo) { //validamos que sea el mismo grupo
                            bConvinacionval = false;
                        }
                    }

                    if (sTiposolicitud!="") {
                        if (sgTiposolicitudid!=sTiposolicitud) {
                            bConvinacionval = false;
                        }
                    }

                    if (sSubtiposolicitud!="") {
                        if (sgSubtiposolicitudid != sSubtiposolicitud) {
                            bConvinacionval = false;
                        }
                    }

                    if (sCasodisenoclasif!="") {
                        if (sgCasoDisenoClasificacion!= sCasodisenoclasif) {
                            bConvinacionval = false;
                        }
                    }

                    if (bConvinacionval) {//si pasa las validaciones entonces hace el calculo de generar el plazo
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
                } catch (Exception exs) {
                    new filelog("excepcion al ingresar ", " "+exs.Message);
                }
        }
        public bool espareja(String sTipoplazouno, String sTipoplazdosid) {
            bool bRespuesta = false;
            try {
                conect conect_plazoid_relacion = new conect();
                String sQuery_plazo_relacion_general = " select * from plazos_parejas where tipoplazoid = "+ sTipoplazouno + " and tipoplazoidpareja = "+ sTipoplazdosid + "; ";
                MySqlDataReader resp_plazo_relacion = conect_plazoid_relacion.getdatareader(sQuery_plazo_relacion_general);
                int icount = 0;
                while (resp_plazo_relacion.Read())
                {
                    icount++;
                }
                if (icount>0) { //quiere decir que si son pareja
                    bRespuesta = true;
                }
                resp_plazo_relacion.Close();
                conect_plazoid_relacion.Cerrarconexion();
            } catch (Exception exs) {
                bRespuesta = false;
            }
            return bRespuesta;
        }

        public void agregaplazo(String sCasoid, String sComboTiposolicitud, String subtipodocumentoid, DateTime dFecha_notificacion_impi, String documentoid)
        {
            try{
                if (subtipodocumentoid!="1052") {
                    String relacion_plazo_subtipodocumentoid = "";
                    String tipoplazoid_impi = "";
                    String tipoplazoid_avisocliente = "";
                    String activo = "";

                    String sSubTipoDocumentoIndPlazo = "";
                    String sSubTipoDocumentoPlazoMeses = "";
                    String sSubTipoDocumentoPlazoDias = "";
                    String sSubTipoDocumentoIndProrrogable = "";

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

                        bBanderacreadetalleplazo = true;
                        //MessageBox.Show("Se creó un plazo.");
                        /*aqui validamos que se inserto el plazo*/
                    }
                    resp_plazos.Close();
                    conect_plazosid.Cerrarconexion();


                    /* Validamos que tipo de plazo tiene el subtipodocumentoid generado anteriormente 
                     * en la tabla relacion_plazo_subtipodocumento, es quien nos 
                     * indica el plazo impi y el plazo aviso al cliente 
                     * relacionado al subtipodocumentoid
                     */
                    int ibBanderacreadetalleplazo_contador = 0;
                    if (bBanderacreadetalleplazo)
                    {
                        //preguntamos por el plazo DIFERENTE DE AVISO para saber si se agrega aquí, 
                        //de ser un plazo de aviso al cliente debe sumarle un día a la fecha actual y esa sera la fecha vigencia
                        conect conect_plazoid_relacion = new conect();
                        //String sQuery_plazo_relacion_general = " select * from relacion_plazo_subtipodocumento where subtipodocumentoid = " + subtipodocumentoid;
                        String sQuery_plazo_relacion_general = "select * from plazos_de_oficio where subtipodocumentoid = " + subtipodocumentoid + " and TipoPlazoId != 33 and TipoPlazoId != 29;";
                        //" SELECT  " +
                        //"     * " +
                        //" FROM " +
                        //"     relacion_plazo_subtipodocumento, " +
                        //"     tipoplazos " +
                        //" WHERE " +
                        //"     relacion_plazo_subtipodocumento.tipoplazoid_impi = tipoplazos.tipoplazosid " +
                        ////" 	AND tipoplazos.aviso is null " +
                        ///
                        //" 	AND relacion_plazo_subtipodocumento.subtipodocumentoid = " + subtipodocumentoid +" ;";
                        MySqlDataReader resp_plazo_relacion = conect_plazoid_relacion.getdatareader(sQuery_plazo_relacion_general);
                        while (resp_plazo_relacion.Read())
                        {
                            relacion_plazo_subtipodocumentoid = validareader("subtipodocumentoid", "SubTipoDocumentoId", resp_plazo_relacion).Text;
                            tipoplazoid_impi = validareader("TipoPlazoId", "SubTipoDocumentoId", resp_plazo_relacion).Text;
                            /*Validamos que si es satisfecho forma no genere el plazo de accion oficial*/
                            if (!(tipoplazoid_impi == "4" && relacion_plazo_subtipodocumentoid == "100"))
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
                                    sSubTipoDocumentoIndProrrogable = validareader("SubTipoDocumentoIndProrrogable", "SubTipoDocumentoId", resp_plazo_activo).Text;
                                    /*SubTipoDocumentoIndProrrogable*/
                                }
                                resp_plazo_activo.Close();
                                conect_plazoid_activo.Cerrarconexion();


                                //tipoplazoid_avisocliente = validareader("tipoplazoid_avisocliente", "relacion_plazo_subtipodocumentoid", resp_plazo_relacion).Text;
                                activo = validareader("aviso", "aviso", resp_plazo_relacion).Text;

                                /*Calculamos las fechas a insertar en el plazo*/
                                DateTime sFechaplazoregular = dFecha_notificacion_impi;
                                try
                                {
                                    sFechaplazoregular = sFechaplazoregular.AddMonths(Int16.Parse(sSubTipoDocumentoPlazoMeses));
                                    sFechaplazoregular = sFechaplazoregular.AddDays(Int16.Parse(sSubTipoDocumentoPlazoDias));
                                }
                                catch (Exception Ex)
                                {
                                    new filelog("Error: genera plazo desde captura oficio linea 918", " Error:" + Ex.Message);
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
                                String sFechanotificacion = "'" + dFecha_notificacion_impi.ToString("yyyy-MM-dd") + "'";
                                String ssFechaplazoregular = sFechaplazoregular.ToString("yyyy-MM-dd");
                                String ssFechaplazoregular3meses = "NULL";
                                String ssFechaplazoregular4meses = "NULL";
                                if (sSubTipoDocumentoIndProrrogable == "1")
                                {
                                    ssFechaplazoregular3meses = "'" + sFechaplazoregular3meses.ToString("yyyy-MM-dd") + "'";
                                    ssFechaplazoregular4meses = "'" + sFechaplazoregular4meses.ToString("yyyy-MM-dd") + "'";
                                }

                                if (activo == "1")//Nos indica que es un aviso y sólo aumenta un día a la fecha actual como vigencia
                                {//las demás fechas van vacias
                                    sFechanotificacion = "NULL";
                                    sFechaplazoregular = DateTime.Today;//asignamos la fecha actual
                                    sFechaplazoregular = sFechaplazoregular.AddDays(1);
                                    ssFechaplazoregular = sFechaplazoregular.ToString("yyyy-MM-dd");
                                    ssFechaplazoregular3meses = "NULL";
                                    ssFechaplazoregular4meses = "NULL";
                                }

                                /***
                                 * Repite el plazo de repotar oficio
                                 * por lo que cuando es cita a pago SIN figuras 
                                 * lo cambiamos por el reportar anualidades subsecuentes
                                 * que es el plazo 33 y calculamos correctamente la fecha
                                 */

                                //if (subtipodocumentoid == "115" && tipoplazoid_impi == "22")
                                //{
                                //    //tipoplazoid_impi = "33";
                                //    break;
                                //}

                                if (tipoplazoid_impi == "33")//validamos si el plazo es de pago de anualidades subsecuentes
                                {
                                    /*
                                     * Éste aviso debe tener como fecha de vigencia  5 años después de la notificación y tomar el mes de la fecha presentación y al resultado restarle 
                                     * 3 meses para poder avisar al cliente con anterioridad al pago de las anualidades subsecuentes.
                                     * Se tomará los siguientes campos para tomar el mes
                                     * 1.- CasoFechaInternacional
                                     * 2.- CasoFechaLegal
                                     * 3.- CasoFechaRecepcion
                                     */
                                    String CasoFechaInternacional = "", CasoFechaLegal = "", CasoFechaRecepcion = "";
                                    conect conect_plazosid_relacion = new conect();
                                    String sQuery_plazos_relacion_general = "select * from caso_patente where casoid =" + sCasoid;// +" order by  plazosid desc limit 1;" + sCasoid;
                                    MySqlDataReader resp_plazos_relacion = conect_plazosid_relacion.getdatareader(sQuery_plazos_relacion_general);
                                    while (resp_plazos_relacion.Read())
                                    {
                                        try
                                        {
                                            CasoFechaInternacional = validareader("CasoFechaInternacional", "casoid", resp_plazos_relacion).Text;
                                            CasoFechaLegal = validareader("CasoFechaLegal", "casoid", resp_plazos_relacion).Text;
                                            CasoFechaRecepcion = validareader("CasoFechaRecepcion", "casoid", resp_plazos_relacion).Text;
                                        }
                                        catch (Exception Ex)
                                        {
                                        }

                                    }
                                    resp_plazos_relacion.Close();
                                    conect_plazosid_relacion.Cerrarconexion();
                                    String sFechamesaniversario = "";
                                    if (CasoFechaInternacional != "")
                                    {
                                        sFechamesaniversario = CasoFechaInternacional;
                                    }
                                    else
                                    {
                                        if (CasoFechaLegal != "")
                                        {
                                            sFechamesaniversario = CasoFechaLegal;
                                        }
                                        else
                                        {
                                            sFechamesaniversario = CasoFechaRecepcion;
                                        }
                                    }
                                    if (sFechamesaniversario != "")
                                    {
                                        int iMes = DateTime.Parse(sFechamesaniversario).Month;

                                        /* FIN Éste aviso debe tener como fecha de vigencia  5 años ...*/
                                        dFecha_notificacion_impi = dFecha_notificacion_impi.AddYears(5);
                                        int ianoVigencia = dFecha_notificacion_impi.Year;

                                        sFechaplazoregular = new DateTime(ianoVigencia, iMes, 1);
                                        sFechaplazoregular = sFechaplazoregular.AddMonths(-3);
                                        ssFechaplazoregular = sFechaplazoregular.ToString("yyyy-MM-dd");
                                        ssFechaplazoregular3meses = "NULL";
                                        ssFechaplazoregular4meses = "NULL";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No existen Fechas internacional, Legal o recepción, No se puede crear plazo de reportar plazo de anualidades subsecuentes.");
                                        return;
                                    }
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
                            }
                            // }//cerramos el if de SubTipoDocumentoIndPlazo
                        }
                        resp_plazo_relacion.Close();
                        conect_plazoid_relacion.Cerrarconexion();
                    }//cerramos el if
                    //MessageBox.Show("Se agregraron " + ibBanderacreadetalleplazo_contador + " de subPlazos");
                }


            }
            catch(Exception Ex){
                new filelog("Error: genera plazo desde captura oficio linea 988", " Error:"+Ex.Message);
            }
        }
        

        public void generaanualialidades(int Num_anos, string sFechanotificacion, string sFechapresentacion, string sCasoid, string sTiposolicitud)
        {
            try {
                String DocumentoFecha = DateTime.ParseExact(sFechapresentacion, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbFechacalce.Text;// now();
                //aqui tenemos el año del documento que se acaba de subir
                String fec_ano = DateTime.ParseExact(sFechapresentacion, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy");
                String fec_Mes_presentacion = DateTime.ParseExact(sFechapresentacion, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("MM");

                String fec_dia = DateTime.ParseExact(sFechanotificacion, "yyyy/MM/dd", CultureInfo.InvariantCulture).ToString("dd"); //tbFechacalce.Text;// now();
                String fec_mes = DateTime.ParseExact(sFechanotificacion, "yyyy/MM/dd", CultureInfo.InvariantCulture).ToString("MM"); //tbFechacalce.Text;// now();
                String DocumentoAño = DateTime.ParseExact(sFechanotificacion, "yyyy/MM/dd", CultureInfo.InvariantCulture).ToString("yyyy"); //tbFechacalce.Text;// now();
                //String fec_ano_notificacion = DateTime.ParseExact(sFechanotificacion, "yyyy/MM/dd", CultureInfo.InvariantCulture).ToString("yyyy"); //tbFechacalce.Text;// now();
                String AnualidadIndExe = "";
                int sCountquinquenio = 1;
                int iNumquinquenio = 0;
                int sEstatus = 4;
                String sAnualidadfechapago = "Null";
                String AnualidadFechaLimitePago = "";
                String sQuerys = "";

                String date = DateTime.Now.Date.ToString();
                String Month = DateTime.Now.Month.ToString();
                String Year_actual = DateTime.Now.Year.ToString();
                int sAnoactual = Convert.ToInt32(DocumentoAño);
                int sAnofecha = Convert.ToInt32(fec_ano);
                bool bPrimer = true;
                int quinquenios = 1;
                for (int x = 1; x <= Num_anos; x++)
                {
                    if (sAnofecha < sAnoactual)
                    {
                        AnualidadIndExe = "1";
                        sEstatus = 4;
                        AnualidadFechaLimitePago = sAnoactual + "-" + fec_mes + "-" + fec_dia;
                    }
                    else
                    {
                        if (bPrimer)
                        {//validamos el primer quinquenio
                            bPrimer = false;
                            iNumquinquenio = 1;
                        }

                        AnualidadIndExe = "0";//está bien
                        sEstatus = 1;//está bien
                        AnualidadFechaLimitePago = sAnoactual + "-" + fec_mes + "-" + fec_dia;
                        sAnualidadfechapago = "";// sAnoactual + "-" + fec_mes + "-" + fec_dia;
                    }

                    

                    /*Si es Diseño (Modelo de Útilidad todas son pendientes, no existen las excentas)*/
                    /*if (sTiposolicitud=="3")
                    {
                        AnualidadIndExe = "0";
                    }*/

                    /**
                     * *
                     * Contaremos 5 repeticiones a partir de que entre para calcular cada quinquenio
                     */
                    if (quinquenios>5)
                    {
                        iNumquinquenio++;
                        quinquenios = 1;
                    }

                    if (iNumquinquenio > 1)
                    {
                        //
                        AnualidadFechaLimitePago = sAnofecha + "-" + fec_Mes_presentacion + "-01";
                        //AnualidadFechaLimitePago = sAnofecha + "-" + fec_mes + "-" + fec_dia; //"";
                        sAnualidadfechapago = "";//sAnofecha + "-" + fec_mes + "-" + fec_dia;
                    }
                    //hacemos un insert a docuemtos y luego a relaciona docuemntos
                    if (sEstatus == 4)
                    {
                        AnualidadFechaLimitePago = "";
                    }
                    if (AnualidadFechaLimitePago != "" && iNumquinquenio == 1)//si es diferente de vacio debemos sumarle 4 meses
                    {//por lo que debemos convertirlo a fecha y 
                        //sumarle 2 meses para que sea la Fecha vencimiento Regular
                        //o le sumamos 4 cuando sea  la fecha limite de vencimiento
                        DateTime CalculoFechalimitedepago = DateTime.Parse(AnualidadFechaLimitePago);
                        CalculoFechalimitedepago = CalculoFechalimitedepago.AddMonths(2);
                        AnualidadFechaLimitePago = CalculoFechalimitedepago.ToString("yyyy'/'MM'/'dd");
                    }

                    if (AnualidadFechaLimitePago == "")
                    {
                        AnualidadFechaLimitePago = "null";
                    }
                    else {
                        AnualidadFechaLimitePago = "'"+AnualidadFechaLimitePago + "'";
                    }
                    String insertdocumento = "INSERT INTO `anualidad`" +
                                                "(`AnualidadId`," +
                                                "`AnualidadSecuencia`," +
                                                "`AnualidadIndExe`," +
                                                "`AnualidadAno`," +
                                                "`AnualidadMes`," +
                                                "`AnualidadQuinquenio`," +
                                                "`EstatusAnualidadId`," +
                                                "`CasoId`," +
                                                "`TipoSolicitudId`," +
                                                
                                                "`AnualidadFechaLimitePago`," +
                                                "`AnualidadTipo`)" +
                                                "VALUES" +
                                                "(null," +
                                                " " + x + "," +
                                                " " + AnualidadIndExe + "," +
                                                " " + sAnofecha + "," +
                                                " " + fec_mes + "," +
                                                " " + iNumquinquenio + "," +
                                                " " + sEstatus + "," +
                                                " '" + sCasoid + "'," +
                                                " '" + sTiposolicitud + "'," +
                                                
                                                " " + AnualidadFechaLimitePago + "," +
                                                "'1');";
                    
                    //if (sCountquinquenio > 5)
                    //{
                    //    //aumenta el quinquenio
                    //    iNumquinquenio++;
                    //    sCountquinquenio = 1;
                    //}
                    /*if (sAnofecha >= sAnoactual)
                    {
                        sCountquinquenio++;
                    }*/

                    if (!(sAnofecha < sAnoactual))
                    {
                        quinquenios++;
                    }
                    //if ((sAnofecha == sAnoactual) && x==1)
                    //{
                    //    quinquenios++;
                    //}

                    sQuerys += insertdocumento;
                    sAnofecha++;
                }/*Termina for de creacion de anualidades*/
                try
                {
                    String total = sQuerys;
                    conect con1 = new conect();
                    MySqlDataReader resp_escritos = con1.getdatareader(sQuerys);
                    if (resp_escritos.RecordsAffected > 0)
                    {
                        MessageBox.Show("Se agregaron " + resp_escritos.RecordsAffected + " Anualidades.");
                        /*Aquí debemos actualizar la fecha vigencia*/
                        //conect con_4_vig = new conect();
                        //DateTime sCasoFechavigeniacalc = DateTime.Parse(AnualidadFechaLimitePago.Replace('\'', ' ').Trim());
                        //sCasoFechavigeniacalc = sCasoFechavigeniacalc.AddYears(1);
                        //String updatevigenciacaso = "UPDATE `" + sTablaconsulta + "` SET `CasoFechaVigencia` = '" + sCasoFechavigeniacalc.ToString("yyyy-MM-dd") +
                        //    "' WHERE (`CasoId` = '" + sCasoid + "' and TipoSolicitudId = '" + sTiposolicitud + "' );";
                        //MySqlDataReader resp_updatevig = con_4_vig.getdatareader(updatevigenciacaso);
                        //resp_updatevig.Close();
                        //con_4_vig.Cerrarconexion();

                    }
                    resp_escritos.Close();
                    con1.Cerrarconexion();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Error al intentar agregar Anualidades :" + Ex.Message);
                    new filelog("1", Ex.Message);
                }

            }catch(Exception Ex){
                MessageBox.Show("Error al intentar agregar Anualidades :" + Ex.Message);
                new filelog("1", Ex.Message);
            }
            
            

        }
        public int anualidadesMD(String sFecha_presentacion, String sFecha_citaapago, int iNumerodeanualidades, String sCasoid, String sTiposolicitud) {
            try {
                //Para el primer pago que incluye las primero 5 anualidades y el titulo se toma la FECHA CITA A PAGO a pago más 2 Meses.
                //Para la fecha de los siguientes pagos se toma la FECHA DE VIGENCIA - 6 Meses.
                 //SELECT 
                 //       anialidades_md_nuevos.*,
                 //       estatusanualidad.EstatusAnualidadDescrip
                 //   FROM
                 //       estatusanualidad,
                 //       anialidades_md_nuevos
                 //   WHERE
                 //       estatusanualidad.EstatusAnualidadId = anialidades_md_nuevos.estatusanualidad;
                DateTime dFechacitapago = DateTime.ParseExact(sFecha_citaapago, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                dFechacitapago = dFechacitapago.AddMonths(2);
                String primerpago = dFechacitapago.ToString("yyyy-MM-dd");

                DateTime dFechapresentacion = DateTime.ParseExact(sFecha_presentacion, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime dsegudnopagoprimrenovacion = dFechapresentacion.AddYears(4);
                dsegudnopagoprimrenovacion = dsegudnopagoprimrenovacion.AddMonths(6);
                String sSegundopago = dsegudnopagoprimrenovacion.ToString("yyyy-MM-dd");

                DateTime dtercerpagoprimrenovacion = dFechapresentacion.AddYears(9);
                dtercerpagoprimrenovacion = dtercerpagoprimrenovacion.AddMonths(6);
                String stercerpago = dtercerpagoprimrenovacion.ToString("yyyy-MM-dd");

                DateTime dcuartoopagoprimrenovacion = dFechapresentacion.AddYears(14);
                dcuartoopagoprimrenovacion = dcuartoopagoprimrenovacion.AddMonths(6);
                String sCuartopago = dcuartoopagoprimrenovacion.ToString("yyyy-MM-dd");

                DateTime dquintopagoprimrenovacion = dFechapresentacion.AddYears(19);
                dquintopagoprimrenovacion = dquintopagoprimrenovacion.AddMonths(6);
                String sQuintopago = dquintopagoprimrenovacion.ToString("yyyy-MM-dd");

                

                String[] sfechas = { primerpago, sSegundopago, stercerpago, sCuartopago, sQuintopago};

                String[] sPeriodos = { "1 a 5  ( Concesión  )", "6 a 10  ( 1ra Renovación )", "11 a 15  ( 2da Renovación) ", "16 a 20  ( 3ra Renovación) ", "21 a 25  (4ta Renovación) " };
                String sQuerygeneraanualidades = " INSERT INTO `anialidades_md_nuevos` " +
                                            " (Anialidades_MD_nuevosid, `secuencia`, `periodo`, `fechalimite`, estatusanualidad, casoid, Tiposolicitudid)" +
                                            " VALUES ";
                                            
                for (int y = 0; y < (iNumerodeanualidades/ 5); y++){
                    int iSecuencia = y+1;
                    String sFecha = sfechas[y];
                    if (sFecha.Length>10)
                    {
                        sFecha = sFecha.Substring(0, 10);
                    }
                    sQuerygeneraanualidades += " (null, '" + iSecuencia + "', '" + sPeriodos[y] + "', '" + sFecha + "', '1', '" + sCasoid + "', '" + sTiposolicitud + "'),";
                }//quitamos la última como y le colocamos un ; para terminar el script
                sQuerygeneraanualidades = sQuerygeneraanualidades.Substring(0, sQuerygeneraanualidades.Length - 1) + ";";
                conect con1 = new conect();
                MySqlDataReader resp_anualidad_md = con1.getdatareader(sQuerygeneraanualidades);
                if(resp_anualidad_md.RecordsAffected > 0)
                {
                  MessageBox.Show("Se agregaron " + resp_anualidad_md.RecordsAffected + " periodos de pago de anualidades correctamente.");
                }
                resp_anualidad_md.Close();
                con1.Cerrarconexion();
                return 1;
            }catch(Exception Ex){
                new filelog("Error anualidades MD", ":"+Ex.Message);
                return 0;
            }
            
            
        }

        //private void generarplazo(string sTipoplazo, string sCasoidactual, string documentoid, string sPlazofecha, string sIdusuario, string sEstatuplazoid)
        //{

        //    try {

        //        conect con_insertplazo = new conect();
        //        String sQueryinsert = " INSERT INTO `plazo` " +
        //                                            " (`PlazoId`, " +
        //                                            " `TipoPlazoId`, " +
        //                                            " `CasoId`, " +
        //                                            " `DocumentoId`, " +
        //                                            " `AnualidadId`, " +
        //                                            " `ClienteId`, " +
        //                                            " `PlazoMotivoCancelacion`, " +
        //                                            " `PlazoFecha`, " +
        //                                            " `PlazoFechaProrroga`, " +
        //                                            " `UsuarioId`, " +
        //                                            " `PlazoFechaAtencion`, " +
        //                                            " `EstatusPlazoId`, " +
        //                                            " `UsuarioIdCancelo`, " +
        //                                            " `PlazoDescripcion`, " +
        //                                            " `PlazoIdRef`, " +
        //                                            " `usuarioIdAtendio`) " +
        //                                            " VALUES " +
        //                                            " (null, " +
        //                                            " '" + sTipoplazo + "', " +
        //                                            " '" + sCasoidactual + "', " +
        //                                            " '" + documentoid + "', " +
        //                                            " '', " +
        //                                            " '', " +
        //                                            " '', " +
        //                                            " '" + sPlazofecha + "', " +
        //                                            " '', " +
        //                                            " '" + sIdusuario + "', " +
        //                                            " '', " +
        //                                            " '" + sEstatuplazoid + "', " +
        //                                            " '', " +
        //                                            " '', " +
        //                                            " '', " +
        //                                            " ''); ";
        //        MySqlDataReader resp_insertplazo = con_insertplazo.getdatareader(sQueryinsert);
        //        if (resp_insertplazo != null)
        //        {
        //            new filelog(login.sId, "Se inertó plazo correctamente");
        //        }
        //        else {
        //            new filelog(login.sId, "Error al insertar plazo correctamente");
        //        }
        //    }catch(Exception Ex){
        //    }
        //}

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            //tbFechanotificacion
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
            //tbDocumentofecharecepcion
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

        private void tbFechacalce_KeyPress(object sender, KeyPressEventArgs e)
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

            if (tbFechacalce.Text.Length == 2)
            {
                tbFechacalce.Text = tbFechacalce.Text + "-";
                tbFechacalce.SelectionStart = tbFechacalce.Text.Length;
            }
            if (tbFechacalce.Text.Length == 5)
            {
                tbFechacalce.Text = tbFechacalce.Text + "-";
                tbFechacalce.SelectionStart = tbFechacalce.Text.Length;
            }
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try { 
                cbEsritos.Items.Clear();
                conect con1 = new conect();
                String sQueryallescritos = "";
                if (cbMostraroficios.Checked == true)
                {
                    sQueryallescritos = "SELECT " +
                                                "    * " +
                                                " FROM " +
                                                "    gruposubtipodocumento, " +
                                                "    subtipodocumento " +
                                                " WHERE " +
                                                "    gruposubtipodocumento.GrupoId = " + sGrupocaso + /*para patentes 1, para marcas 2*/
                                                "    AND subtipodocumento.SubTipoDocumentoId = gruposubtipodocumento.SubTipoDocumentoId " +
                                                "    AND subtipodocumento.TipoDocumentoId = 1 " +
                                                "    AND subtipodocumento.SubTipoDocumentoIndAct = 1 " +// el valos uno debe ser fijo puesto que validamos que el documento esté activo
                                                "    group by subtipodocumento.SubTipoDocumentoId;";
                    MySqlDataReader resp_escritos = con1.getdatareader(sQueryallescritos);
                    cbEsritos.Items.Clear();
                    while (resp_escritos.Read())
                    {
                        ComboboxItem obj = new ComboboxItem();
                        obj.Value = validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos).Value;
                        obj.Text = validareader("SubTipoDocumentoId", "SubTipoDocumentoId", resp_escritos).Text + "-" + validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos).Text;
                        cbEsritos.Items.Add(obj);//Estatus
                    }
                    resp_escritos.Close();
                }
                else
                {
                    if (sEstatusidint != "")
                    {
                        sQueryallescritos = "select * " +
                                            "from " +
                                            "estatuscasosubtipodocumento, " +
                                            "subtipodocumento " +
                                            "where " +
                                            "estatuscasosubtipodocumento.EstatusCasoId = " + sEstatusidint +
                                            " and estatuscasosubtipodocumento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId" +
                                            " and estatuscasosubtipodocumento.GrupoId = " + sGrupocaso +/*para patentes 1, para marcas 2*/
                                            " and subtipodocumento.TipoDocumentoId = 1 " +
                                            " and subtipodocumento.SubTipoDocumentoIndAct = 1 " +// el valos uno debe ser fijo puesto que validamos que el documento esté activo
                                            " group by subtipodocumento.SubTipoDocumentoId;";

                        MySqlDataReader resp_escritos = con1.getdatareader(sQueryallescritos);
                        cbEsritos.Items.Clear();
                        while (resp_escritos.Read())
                        {
                            ComboboxItem obj = new ComboboxItem();
                            obj.Value = validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos).Value;
                            obj.Text = validareader("SubTipoDocumentoId", "SubTipoDocumentoId", resp_escritos).Text + "-" + validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos).Text;
                            cbEsritos.Items.Add(obj);//Estatus
                        }
                        resp_escritos.Close();
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar un caso antes");
                    }


                }
                }catch(Exception E){
                    new filelog(login.sId, E.ToString());
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cbEsritos.Text == "")
            {
                MessageBox.Show("Debe seleccionar un tipo de oficio antes de agregar");
                if (cbEsritos.CanFocus)
                {
                    cbEsritos.Focus();
                }
                return;
            }
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
                sfilePath = filePath;

                
                string textoNormalizado = cbEsritos.Text.Normalize(NormalizationForm.FormD);
                //coincide todo lo que no sean letras y números ascii o espacio
                //y lo reemplazamos por una cadena vacía.Regex reg = new Regex("[^a-zA-Z0-9 ]");
                Regex reg = new Regex("[^a-zA-Z0-9 ]");
                string textoSinAcentos = reg.Replace(textoNormalizado, "");
                String sFechanow = DateTime.Now.ToString("yyyyMMddHHmmsss");

                sfilePath_2 = @ruta + "\\" + tbCasonum.Text + "_"+ sFechanow +"_" + tbExpediente.Text.Replace("/", "") + "_" + textoSinAcentos + " " + sNamefile;
                tbFilename.Text = @"\\" + tbCasonum.Text + "_" + tbExpediente.Text.Replace("/", "") + "_" + textoSinAcentos + " " + sNamefile;
            }
            catch (Exception E)
            {
                new filelog(login.sId, E.ToString());
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

        public ComboboxItem validareader_documentos(String campoText, String campoValue, MySqlDataReader mresultado, String id_documento)
        {
            ComboboxItem cItemresult = new ComboboxItem();

            if (!mresultado.IsDBNull(mresultado.GetOrdinal(campoText)))
            {
                cItemresult.Text = id_documento + " - " + mresultado.GetString(mresultado.GetOrdinal(campoText));
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

        private void tbCodigo_TextChanged(object sender, EventArgs e)
        {
            try {
                String sCodigo = tbCodigo.Text;//validacion con por lo menos nueve en el tamaño del código en los oficios
                if (sCodigo.Length >= 9)
                {
                    String[] sSeparado = sCodigo.Split('/');
                    tbFolio.Text = sSeparado[sSeparado.Length-1];
                }
            }catch(Exception ex){
                MessageBox.Show("Verifique el código de barras");
            }


        }

        private void tbFechanotificacion_TextChanged(object sender, EventArgs e)
        {
            try
            {

                String sfecharecepcion = tbFechanotificacion.Text;
                if (sfecharecepcion.Length == 10)
                {
                    //DateTime oDate = DateTime.Parse(sfecharecepcion);
                    /*consultamos la fecha del subtipo documento*/
                    conect con_3 = new conect();
                    String sQuery = "select * from subtipodocumento where subtipodocumentoid = " + (cbEsritos.SelectedItem as ComboboxItem).Value + ";";
                    MySqlDataReader resp_areaimpi = con_3.getdatareader(sQuery);
                    String sMes = "", sDias = "";
                    DateTime dFechasumuada = DateTime.Parse(sfecharecepcion);
                        while (resp_areaimpi.Read())
                    {
                        sMes = validareader("SubTipoDocumentoPlazoMeses", "SubTipoDocumentoId", resp_areaimpi).Text;
                        if (sMes != "")
                        {
                            dFechasumuada = dFechasumuada.AddMonths(Int32.Parse(sMes));
                        }

                        sDias = validareader("SubTipoDocumentoPlazoDias", "SubTipoDocumentoId", resp_areaimpi).Text;
                        if (sDias != "")
                        {
                            dFechasumuada = dFechasumuada.AddDays(Int32.Parse(sDias));
                        }

                        String sFechareult = dFechasumuada.ToString("dd-MM-yyyy");
                        if (sFechareult.Length > 10)
                        {
                            tbFechacalce.Text = sFechareult.Substring(0, 10);
                        }
                        else
                        {
                            tbFechacalce.Text = sFechareult;
                        }
                    }
                    resp_areaimpi.Close();
                    con_3.Cerrarconexion();
                    //tbFechacalce.Text = oDate.AddMonths(2).ToString();
                    //if (tbFechacalce.Text.Length > 10)
                    //{
                    //    tbFechacalce.Text = tbFechacalce.Text.Substring(0, 10);
                    //}

                }
            }
            catch (Exception Ex)
            {
                    MessageBox.Show("Seleccione un Tipo de Documento ó Verifique la Fecha de Notificación." + Ex.Message);
                tbFechanotificacion.Focus();
            }

        }

    }
}
