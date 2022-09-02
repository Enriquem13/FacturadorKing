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
    public partial class CapturaSolicitud : Form
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
        public String sgSubtiposolicitudid = "", sgCasoDisenoClasificacion="";

        public bool sBAnderadesdecaso = false;
        public String sfilePath, sfilePath_2, sCarpetadocumentos, sCarpetacaso;
        public String gsEstatuscasoid;
        private string sgPlazoid;
        funcionesdicss objfuncionesdicss;
        public CapturaSolicitud(captura capformulario, Form1 log, String sGrupo, String sCasoidgeneral)
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
                        sCapturatituloid = "118";//solicitud de patente
                        sCarpetadocumentos = "Patentes";
                    } break;
                case "2":
                    {
                        sTablaconsulta = "caso_marcas";
                        this.Text = this.Text + " ( Grupo Marcas)";
                        this.BackColor = Color.FromArgb(255, 255, 192);
                        sCapturatituloid = "1122"; //117, 1122
                        sCarpetadocumentos = "Marcas";
                    } break;
                case "3":
                    {
                        sTablaconsulta = "caso_contencioso";
                        this.Text = this.Text + " ( Grupo Contencioso)";
                        this.BackColor = Color.Yellow;
                        sCapturatituloid = "1122"; //117, 1122
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
                cargacasoenform();
                sBAnderadesdecaso = true;
                sCarpetacaso = tbCasonum.Text;
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
                }
                catch (Exception E)
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
                        String EstatusCasoId = validareader("EstatusCasoId", "Casoid", respuestastring6).Text;
                        gsEstatuscasoid = EstatusCasoId;
                        String sConsultaestatus = "SELECT * FROM estatuscaso where EstatusCasoId = '" + EstatusCasoId + "' ";
                        sCasoidactual = validareader("Casoid", "Casoid", respuestastring6).Text;
                        sgTiposolicitudid = validareader("TipoSolicitudid", "Casoid", respuestastring6).Text;
                        sgSubtiposolicitudid = validareader("SubTipoSolicitudId", "Casoid", respuestastring6).Text;
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

                        conect conpais = new conect();
                        MySqlDataReader resp_pais = conpais.getdatareader("select PaisClave, PaisId from pais where  PaisId = " + validareader("PaisId", "Casoid", respuestastring6).Text);
                        while (resp_pais.Read())
                        {
                            tbPaisRegistro.Text = validareader("PaisClave", "PaisId", resp_pais).Text;//Estatus
                        }
                        resp_pais.Close();
                        conpais.Cerrarconexion();

                        
                        //tbRegistro.Text = validareader("CasoNumConcedida", "Casoid", respuestastring6).Text;//registro
                        //tbExpediente.Text = validareader("CasoNumeroExpedienteLargo", "Casoid", respuestastring6).Text;
                        rtbtitulo.Text = validareader("CasoTituloespanol", "Casoid", respuestastring6).Text + " /" + validareader("CasoTituloingles", "Casoid", respuestastring6).Text;
                        /*Agregamos a consulta a losplazos solicitud*/
                        conect con_oficio = new conect();
                        String sQueryoficioanterior = " select " +
                                                        " plazos_detalle.plazosid," +
                                                        " plazos_detalle.Plazos_detalleid," +
                                                        " documento.DocumentoCodigoBarras," +
                                                        " subtipodocumento.SubTipoDocumentoDescrip" +
                                                        " from " +
                                                        " plazos," +
                                                        " plazos_detalle," +
                                                        " documento," +
                                                        " subtipodocumento," +
                                                        " tipoplazos" +
                                                        " where " +
                                                        " plazos.Plazosid = plazos_detalle.plazosid" +
                                                        " and plazos_detalle.documentoid = documento.DocumentoId" +
                                                        " and subtipodocumento.SubTipoDocumentoId = documento.SubTipoDocumentoId" +
                                                        " AND subtipodocumento.TipoDocumentoId = 1" +
                                                        " and plazos_detalle.Estatus_plazoid = 1" +
                                                        " and tipoplazos.tipoplazosid = plazos_detalle.Tipo_plazoid" +
                                                        " and tipoplazos.aviso is null" +
                                                        " and plazos.casoid = " + sCasoidgeneralGlabal +
                                                        " and plazos.TipoSolicitudId = " + sgTiposolicitudid +
                                                        " limit 1;";
                        MySqlDataReader resp_oficioanterior = con_oficio.getdatareader(sQueryoficioanterior);
                        while (resp_oficioanterior.Read())
                        {
                            ComboboxItem cbItemoficios = new ComboboxItem();
                            cbItemoficios.Text = validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoDescrip", resp_oficioanterior).Text + " - " + validareader("DocumentoCodigoBarras", "DocumentoCodigoBarras", resp_oficioanterior).Text;
                            cbItemoficios.Value = validareader("Plazos_detalleid", "Plazos_detalleid", resp_oficioanterior).Value;
                            sgPlazoid = validareader("plazosid", "plazosid", resp_oficioanterior).Text;
                            //cbOficio.Items.Add(cbItemoficios);//Estatus
                        }
                        resp_oficioanterior.Close();
                        con_oficio.Cerrarconexion();
                    }
                }
                respuestastring6.Close();
                con1.Cerrarconexion();
            }
            catch (Exception E)
            {
                MessageBox.Show("Warning: " + E);
                new filelog(login.sId, E.ToString());
            }
        }

        public void atenderplazoconescrito(object sIdplazodetalleid, string SubTipoDocumentoId, string documentoid, String sPlazosid)
        {
            try
            {
                DateTime dFechaactual = DateTime.Today;
                dFechaactual = dFechaactual.AddDays(1);
                String ssFechaplazoregular = dFechaactual.ToString("yyyy/MM/dd");
                String sPreparo = login.sId;//(cbPreparo.SelectedItem as ComboboxItem).Value;
                //creamos el subplazo del escrito , es un subplazo de aviso del escrito al cliente
                /*Validamos al subir la solicitud si es de marcas o de patentes para insertar que el plazo sea correcto*/
                String sTipoplazoreportarescrito = "29";
                if (SubTipoDocumentoId == "118")//para patentes
                {
                    sTipoplazoreportarescrito = "55";
                }
                else {
                    if (SubTipoDocumentoId == "1122")
                    { //para marcas
                        sTipoplazoreportarescrito = "29";
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
                                                
                                                " `Fecha_Vencimiento`) " +
                                                " VALUES (null," +
                                                " '" + sPlazosid + "', " +
                                                " '" + documentoid + "', " +
                                                " '" + sPreparo + "', " +
                                                " '"+ sTipoplazoreportarescrito + "', " +
                                                " '1', " +
                                                " '" + ssFechaplazoregular + "');";

                //" '" + tipoplazoid_avisocliente + "', " +
                //" '1', " +
                //" '" + documentoid + "', " +
                //" '" + login.sId + "');";
                String splazos_detalleid_ult = "";

                MySqlDataReader resp_plazo = conect_plazoid.getdatareader(sQuery_plazo_general);
                if (resp_plazo.RecordsAffected == 1)
                {

                    /*aqui validamos que se inserto el plazo*/
                    conect con1_select = new conect();
                    String sQueryallescritos_select = "select * from `plazos_detalle` order by plazos_detalleid desc limit 1;";
                    MySqlDataReader resp_escritos_select = con1_select.getdatareader(sQueryallescritos_select);
                    while (resp_escritos_select.Read())
                    {
                        splazos_detalleid_ult = validareader("plazos_detalleid", "plazos_detalleid", resp_escritos_select).Text;
                    }
                    resp_escritos_select.Close();
                    con1_select.Cerrarconexion();
                    /*Y consultamos el iddetalle del ultimo insertado */
                }
                resp_plazo.Close();
                conect_plazoid.Cerrarconexion();

                /*FIN creamos el subplazo del escrito , es un subplazo de aviso del escrito al cliente*/
                DateTime sFechaactual = DateTime.Today;
                conect con1 = new conect();
                String sQueryallescritos = " UPDATE `plazos_detalle` SET " +
                                            " `Atendio_Plazos_detalleid` = '" + splazos_detalleid_ult + "', " +
                                            " `Estatus_plazoid` = '2', " +
                                            " `Fecha_atendio_plazo` = '" + sFechaactual.ToString("yyyy/MM/dd") + "', " +
                                            " `Usuarioid_atendio_plazo` = '" + login.sId + "' " +
                                            " WHERE (`Plazos_detalleid` = '" + sIdplazodetalleid + "');";
                MySqlDataReader resp_escritos = con1.getdatareader(sQueryallescritos);
                //while (resp_escritos.Read())
                //{
                //    String sIdescritp = validareader("SubTipoDocumentoId", "SubTipoDocumentoId", resp_escritos).Text;
                //    cbEsritos.Items.Add(validareader_documentos("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos, sIdescritp));//Estatus
                //}
                resp_escritos.Close();
                con1.Cerrarconexion();
            }
            catch (Exception Ex)
            {

            }
        }
        public bool validanumexpediente() {
            bool bExpediente = false;
            try { 
                    int iNum_ref = 0;
                String sCasonumero = "";
                    conect con_ref = new conect();
                    String sGetcasonumero_ref = "select count(CasoNumeroExpedienteLargo) As Num_Expediente,"+
                                                " CasoNumero from caso_patente where CasoNumeroExpedienteLargo like '%" + tbExpediente__.Text + "%';";
                    MySqlDataReader respuestastringcasonum_ref = con_ref.getdatareader(sGetcasonumero_ref);
                    while (respuestastringcasonum_ref.Read())
                    {
                        iNum_ref = int.Parse(validareader("Num_Expediente", "Num_Expediente", respuestastringcasonum_ref).Text);
                        sCasonumero = validareader("CasoNumero", "CasoNumero", respuestastringcasonum_ref).Text;
                    }
                    respuestastringcasonum_ref.Close();
                    con_ref.Cerrarconexion();

                    if (iNum_ref > 0)
                    {
                        MessageBox.Show("El número de expediente ya existe en el caso: " + sCasonumero + ", Verifique, no puede duplicarse.");
                        bExpediente = true;
                    }
                    else {
                        bExpediente = false;
                    }
                        
            }catch(Exception Ex){
                bExpediente = false;
            }
            return bExpediente;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //validar que las fechas sean de 1950 a fecha actual y poner focus 
            //fecha a validar tbFechanotificacion
            //fechas  a validar tbDocumentofecharecepcion
            //if (this.DateTimePickerDesde.Value.CompareTo(this.DateTimePickerHasta.Value) == 1)
            //{ 
            //}
            if (tbExpediente__.Text == "")
            {
                if (tbExpediente__.CanFocus)
                {
                    tbExpediente__.Focus();
                }
                MessageBox.Show("Debe agregar un expediente.");
                return;
            }
            //tbDocumentofecharecepcion
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

            if (tbCodigo.Text=="")
            {
                MessageBox.Show("Debe caprutar el Código de barras.");
                if (tbCodigo.CanFocus)
                {
                    tbCodigo.Focus();
                }
                return;
            }
            
            
            if (cbEsritos.Text=="")
            {
                MessageBox.Show("Debe seleccionar un tipo de solicitud.");
                if (cbEsritos.CanFocus)
                {
                    cbEsritos.Focus();
                }
                return;
            }
            if (tbFilename.Text=="")
            {
                MessageBox.Show("Debe seleccionar un archivo para poder agregar la solicitud");
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
                                if(validanumexpediente()){
                                    return;
                                }


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
                                DateTime dFEchanotifica = DateTime.ParseExact(tbDocumentofecharecepcion.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                String DocumentoFecha = DateTime.ParseExact(tbDocumentofecharecepcion.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbFechacalce.Text;// now();
                                String DocumentoFolio = tbFolio.Text;
                                String DocumentoFechaRecepcion = DateTime.ParseExact(tbDocumentofecharecepcion.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbDocumentofecharecepcion.Text;
                                DateTime dDocumentoFechaRecepcion = DateTime.ParseExact(tbDocumentofecharecepcion.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
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

                                            String sFechaatencion = DateTime.Now.ToString("yyyy-MM-dd");
                                            conect con_plazos = new conect();
                                            //Usuarioid_atendio_plazo_impi
                                            String sUpdate_plazo_solicitud = " SELECT  " +
                                                                                "     * " +
                                                                                " FROM " +
                                                                                "     plazos, " +
                                                                                "     plazos_detalle " +
                                                                                " WHERE " +
                                                                                "     plazos.Plazosid = plazos_detalle.Plazosid " +
                                                                                "     and Estatus_plazoid = 1 " +//pendiente
                                                                                "     and Tipo_plazoid = 2 " +//plazo solicitud
                                                                                "     and CasoId = " +sCasoidgeneralGlabal +
                                                                                "     and TipoSolicitudId = " + sgTiposolicitudid + ";";

                                            MySqlDataReader resp_consulta_plazo = con_plazos.getdatareader(sUpdate_plazo_solicitud);
                                            String sPlazosidg = "";
                                            String sPlazosdetalleidg = "";
                                            while (resp_consulta_plazo.Read())
                                            {
                                                sPlazosidg = validareader("Plazosid", "Plazosid", resp_consulta_plazo).Text;
                                                sPlazosdetalleidg = validareader("plazos_detalleid", "plazos_detalleid", resp_consulta_plazo).Text;
                                            }

                                            resp_consulta_plazo.Close();
                                            con_plazos.Cerrarconexion();
                                            if (sPlazosidg!="") {
                                                atenderplazoconescrito(sPlazosdetalleidg, SubTipoDocumentoId, documentoid, sPlazosidg);
                                            }
                                            

                                            /*FIN Editamos el plazo que debe tener de presentar solicitus*/
                                            generaplazosplazosdeoficio(SubTipoDocumentoId, documentoid, dDocumentoFechaRecepcion, dFEchanotifica, login.sId);

                                            //SubTipoDocumentoId para obtener SubTipoDocumentoIndTipo

                                            conect con_5 = new conect();
                                            String sconsultaeindtipo = "select SubTipoDocumentoIndTipo from subtipodocumento where SubTipoDocumentoId = " + SubTipoDocumentoId;
                                            MySqlDataReader resp_consultaestipo = con_5.getdatareader(sconsultaeindtipo);
                                            resp_consultaestipo.Read();
                                            String sSubTipoDocumentoIndTipo = validareader("SubTipoDocumentoIndTipo", "SubTipoDocumentoIndTipo", resp_consultaestipo).Text;
                                            if (resp_consultaestipo != null)
                                            {
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
                                                    conect con_4 = new conect();
                                                    String updateestatuscaso = "UPDATE `" + sTablaconsulta + "` SET `EstatusCasoId` = '" + sEstatusactual + "' WHERE (`CasoId` = '" + sCasoidactual + "');";
                                                    MySqlDataReader resp_updatecaso = con_4.getdatareader(updateestatuscaso);
                                                    if (resp_updatecaso != null)
                                                    {
                                                        MessageBox.Show("Documento agregado correctamente.");
                                                        DialogResult = DialogResult.OK;
                                                        capform.Show();
                                                        this.Close();
                                                    }
                                                    //modificamos el expediente si es una solicitud
                                                    //if (gsEstatuscasoid == "1")
                                                    //{
                                                        conect con_51 = new conect();
                                                        String updateestatuscasoexp = "UPDATE `" + sTablaconsulta + "` SET `CasoNumeroExpedienteLargo` = '" + tbExpediente__.Text + "', CasoFechaRecepcion = '" + DocumentoFecha + "' WHERE (`CasoId` = '" + sCasoidactual + "' and Tiposolicitudid = '" + sgTiposolicitudid + "');";
                                                        MySqlDataReader resp_updatecasoexpediente = con_51.getdatareader(updateestatuscasoexp);
                                                        if (resp_updatecasoexpediente != null)
                                                        {
                                                            resp_updatecasoexpediente.Close();
                                                            con_51.Cerrarconexion();
                                                        }
                                                    //}
                                                }
                                                catch (Exception E)
                                                {
                                                    //no hay cambio de estatus porque este documento no esta asignado para cambiar de estatus.
                                                    //DialogResult = DialogResult.OK;
                                                    MessageBox.Show("Documento agregado correctamente.");
                                                    new filelog(login.sId, E.ToString());
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
                                                resp_consultaestipo.Close();
                                            }
                                            con_5.Cerrarconexion();
                                        }
                                    }
                                    resp_docid.Close();
                                    con_2.Cerrarconexion();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Debe seleccionar un caso y un escrito para poder guardar");
                            }
                        }
                        catch (Exception E)
                        {
                            MessageBox.Show("Debe ingresar Fechas correctas ó verifique el archivo sea el correcto.");
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
                MessageBox.Show("Ocurrió un error. Revise el log para más detalles.");
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
                            if (tipoplazoid_impi == "21")
                            {

                                sFechanotific = "null"; 
                                sFechanotificcuatro = "null";
                            }
                        }

                        //if (tipoplazoid_impi=="21") {
                        //    sFechanotificcuatro = "null";
                        //}

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

        public void insertplazos() { 
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
                sfilePath_2 = @ruta + "\\" + tbCasonum.Text + " _" + sFechanow +"_ "+ tbExpediente__.Text.Replace("/", "") + "_" + cbEsritos.Text.Replace("/", "") + " " + sNamefile.Replace("/", "");
                tbFilename.Text = @"\\" + tbCasonum.Text + "_" + tbExpediente__.Text.Replace("/", "") + "_" + cbEsritos.Text.Replace("/", "") + " " + sNamefile;
            }
            catch (Exception E)
            {
                new filelog(login.sId, E.ToString());
            }
        }

        private void tbDocumentofecharecepcion_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbDocumentofecharecepcion);
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

        //private void tbFechanotificacion_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (Char.IsDigit(e.KeyChar))
        //    {
        //        e.Handled = false;
        //    }
        //    else if (Char.IsControl(e.KeyChar))
        //    {
        //        e.Handled = false;
        //    }
        //    else if (Char.IsSeparator(e.KeyChar))
        //    {
        //        e.Handled = false;
        //    }
        //    else
        //    {
        //        e.Handled = true;
        //    }

        //    if (tbFechanotificacion.Text.Length == 2)
        //    {
        //        tbFechanotificacion.Text = tbFechanotificacion.Text + "-";
        //        tbFechanotificacion.SelectionStart = tbFechanotificacion.Text.Length;

        //    }
        //    if (tbFechanotificacion.Text.Length == 5)
        //    {
        //        tbFechanotificacion.Text = tbFechanotificacion.Text + "-";
        //        tbFechanotificacion.SelectionStart = tbFechanotificacion.Text.Length;
        //    }
        //}

        private void cbEsritos_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tbCodigo_TextChanged(object sender, EventArgs e)
        {
            tbFolio.Text = tbCodigo.Text;
        }
        
    }
}
