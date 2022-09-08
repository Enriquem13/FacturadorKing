using Facturador.Casos.Documentos;
using Facturador.Casos.Oposicion;
using Facturador.plazos_forms;
using MySql.Data.MySqlClient;
using SpreadsheetLight;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Facturador
{
    public partial class fTmarcas : Form
    {
        public Form1 loguin;
        public captura fCapuraform;
        public String sCasoId;
        public String gSTipoSolicitudId = "";
        public String gsTipomarca = "";
        public bMarcas buscarclienteform;
        public Boolean bBanderaadelanteatras;
        public int icontadorbusqueda = 0;
        public int iIndiceids_global = 0;
        public String[] sArrayids;
        public String sTipogrupoglobal = "";
        public String sTipoSolicitudId = "";
        public String curp_1 = "";
        public String nombre_1 = "";
        public String appl1_1 = "";
        public String appl2_1 = "";
        public String telefono_1 = "";
        public String nacionalidad_1 = "";
        public String rfc_cambintermed2 = "";
        public String rasonsoc_cambint2 = "";
        public String sDireccionCalle = "";
        public String sDireccionNumExt = "";
        public String sDireccionNumInt = "";
        public String sDireccionColonia = "";
        public String sDireccionCP = "";
        public String sPaisId = "";
        public String gSCasoNumero = "";
        public String gsIdioma = "";
        //caso_marcas objmarcaactual = null;
        view_caso_marcas objmarcaactualview = null;
        private string sIdprioridadseleccionada;

        public String gSclienteid = "";
        public String gSContactoid = "";
        public String[] paises = new String[250];
        public String[] paisesclave = new String[250];
        public String sUsuarioparadocs = "";
        //public String sCarpetadocumentos = "DigitalizadoPatentes\\documentosimpi";
        public String sCarpetadocumentos = "Edocs\\Marcas";

        funcionesdicss objfuncionesdicss = new funcionesdicss();
        //public String sTipoSolicitudId { get; set; }

        public fTmarcas(Form1 fLoguin, captura fcaptura, bMarcas buscarmarcas, String CasoId)
        {
            loguin = fLoguin;
            fCapuraform = fcaptura;
            sCasoId = CasoId;
            buscarclienteform = buscarmarcas;
            sTipogrupoglobal = buscarmarcas.sGTipocaso;
            sUsuarioparadocs = loguin.sUsuarioparadocs;

            InitializeComponent();
            objfuncionesdicss.activaaviso(tbAvisoprueba);

            lvPlazos.FullRowSelect = true;
            lvPlazos.GridLines = true;

            //empieza caso_marcas
            conect con_uno = new conect();
            String sIds = "select count(*) as numpatentes from caso_marcas";
            MySqlDataReader resp_numids = con_uno.getdatareader(sIds);
            resp_numids.Read();
            String sNumerodeids = validareader("numpatentes", "numpatentes", resp_numids).Text;
            resp_numids.Close();
            con_uno.Cerrarconexion();
            int iNumerogrupoids = System.Convert.ToInt32(sNumerodeids);
            sArrayids = new String[iNumerogrupoids];

            conect con_dos = new conect();
            String sGetids = "select * from caso_marcas";
            MySqlDataReader resp_getids = con_dos.getdatareader(sGetids);
            int iIndiceids = 0;
            while (resp_getids.Read())
            {
                sArrayids[iIndiceids] = validareader("Casoid", "Casoid", resp_getids).Text;
                iIndiceids++;
            }
            resp_getids.Close();
            con_dos.Cerrarconexion();
            
            iIndiceids_global = Array.IndexOf(sArrayids, CasoId);
            //terminaciclo caso_marcas

            //generadom(sCasoId);
            conect con_tres = new conect();
            String sQueryclases = "SELECT * FROM `clasificadornizavigente` ORDER BY CasoProductosClase ASC";
            MySqlDataReader respuestastringclases = con_tres.getdatareader(sQueryclases);
            while (respuestastringclases.Read())
            {
                cbClasemarca.Items.Add(validareader("CasoProductosClase", "ClasificadorNizaDescripcion", respuestastringclases));
            }
            respuestastringclases.Close();
            con_tres.Cerrarconexion();

            conect con_4 = new conect();

            String sQuery = " SELECT  " +
                            "     * " +
                            " FROM " +
                            "     subtipodocumento, " +
                            "     gruposubtipodocumento " +
                            " WHERE " +
                            "     gruposubtipodocumento.GrupoId = 2 " +//MARCAS
                            "         AND gruposubtipodocumento.SubtipodocumentoId = subtipodocumento.SubtipodocumentoId " +
                            "         AND TipoDocumentoId = 3 " +
                            "         AND SubTipoDocumentoIndAct = 1 " +
                            "         AND (SubTipoDocumentoTemplateEspanol != '' " +
                            "         OR SubTipoDocumentoTemplateIngles != ''); ";//"SELECT * FROM `clasificadornizavigente` ORDER BY CasoProductosClase ASC";
            MySqlDataReader respuestastringclass = con_4.getdatareader(sQuery);
            cbCartas.Items.Clear();
            while (respuestastringclass.Read())
            {
                //String sNombredescrip = validareader("SubTipoDocumentoId", "SubTipoDocumentoDescrip", respuestastringclass).Text;
                //String sId = validareader("SubTipoDocumentoId", "SubTipoDocumentoDescrip", respuestastringclass).Value.ToString();
                cbCartas.Items.Add(validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", respuestastringclass));
            }
            respuestastringclass.Close();
            con_4.Cerrarconexion();

            //Consultar Escritos
            cbDocEscritos.Items.Clear();
            conect con_escritos = new conect();
            String sQuery_escritos = " SELECT  " +
                            "     * " +
                            " FROM " +
                            "     subtipodocumento, " +
                            "     gruposubtipodocumento " +
                            " WHERE " +
                            "     gruposubtipodocumento.GrupoId = 2 " +//MARCAS
                            "         AND gruposubtipodocumento.SubtipodocumentoId = subtipodocumento.SubtipodocumentoId " +
                            "         AND TipoDocumentoId = 2 " +
                            "         AND SubTipoDocumentoIndAct = 1 " +
                            "         AND (SubTipoDocumentoTemplateEspanol != '' " +
                            "         OR SubTipoDocumentoTemplateIngles != ''); ";//"SELECT * FROM `clasificadornizavigente` ORDER BY CasoProductosClase ASC";
            MySqlDataReader respuestastring_escritos = con_escritos.getdatareader(sQuery_escritos);
            while (respuestastring_escritos.Read())
            {
                //String sNombredescrip = validareader("SubTipoDocumentoId", "SubTipoDocumentoDescrip", respuestastringclass).Text;
                //String sId = validareader("SubTipoDocumentoId", "SubTipoDocumentoDescrip", respuestastringclass).Value.ToString();
                cbDocEscritos.Items.Add(validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", respuestastring_escritos));
            }
            respuestastring_escritos.Close();
            con_escritos.Cerrarconexion();

try
            {
                cbPoder.Items.Clear();
                conect con_3_poder = new conect();
                String sQueryescritosdisponibles_poder = "SELECT " +
                                                           "     * " +
                                                           " FROM " +
                                                           "    estatuscasosubtipodocumento, " +
                                                           "    subtipodocumento " +
                                                           " WHERE " +
                                                           //"     estatuscasosubtipodocumento.Estatuscasoid = " + gSEstatusCasoId + "  " +
                                                           "         estatuscasosubtipodocumento.GrupoId = 2" +// +sGrop +//El grupo falta validarlo con una variable dependiendo el tipo del caso 
                                                           "         AND subtipodocumento.SubTipoDocumentoId = estatuscasosubtipodocumento.SubTipoDocumentoId " +
                                                           "         AND subtipodocumento.TipoDocumentoId = 11 " +//poder
                                                           "         AND subtipodocumento.SubTipoDocumentoIndAct = 1 " +
                                                           //"         AND subtipodocumento.SubTipoDocumentoId in (186,205,206,210,1028,1034,1041,1043,1050,1058,1068,1070,1073,1097,1110,1116,1118,1123,1125,1126)" +
                                                           "         group by estatuscasosubtipodocumento.SubTipoDocumentoId order by subtipodocumento.SubTipoDocumentoDescrip;";
                MySqlDataReader resp_escritos_pod = con_3_poder.getdatareader(sQueryescritosdisponibles_poder);
                while (resp_escritos_pod.Read())
                {
                    String sCartas = objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoId", resp_escritos_pod).Text;
                    if (sCartas != "")
                    {
                        cbPoder.Items.Add(objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoId", resp_escritos_pod));//Estatus
                    }

                }
                resp_escritos_pod.Close();
                con_3_poder.Cerrarconexion();
            }
            catch (Exception exs)
            {
                new filelog("Poder: ", exs.Message);
            }


            try
            {
                cbCesiones.Items.Clear();
                conect con_3_Cesion = new conect();
                String sQueryescritosdisponibles_poder = "SELECT " +
                                                           "     * " +
                                                           " FROM " +
                                                           "    estatuscasosubtipodocumento, " +
                                                           "    subtipodocumento " +
                                                           " WHERE " +
                                                           //"     estatuscasosubtipodocumento.Estatuscasoid = " + gSEstatusCasoId + "  " +
                                                           "         estatuscasosubtipodocumento.GrupoId = 2" +// +sGrop +//El grupo falta validarlo con una variable dependiendo el tipo del caso 
                                                           "         AND subtipodocumento.SubTipoDocumentoId = estatuscasosubtipodocumento.SubTipoDocumentoId " +
                                                           "         AND subtipodocumento.TipoDocumentoId = 12 " +//Cesiones
                                                           "         AND subtipodocumento.SubTipoDocumentoIndAct = 1 " +
                                                           //"         AND subtipodocumento.SubTipoDocumentoId in (186,205,206,210,1028,1034,1041,1043,1050,1058,1068,1070,1073,1097,1110,1116,1118,1123,1125,1126)" +
                                                           "         group by estatuscasosubtipodocumento.SubTipoDocumentoId order by subtipodocumento.SubTipoDocumentoDescrip;";
                MySqlDataReader resp_escritos_Ces = con_3_Cesion.getdatareader(sQueryescritosdisponibles_poder);
                while (resp_escritos_Ces.Read())
                {
                    String sCartas = objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoId", resp_escritos_Ces).Text;
                    if (sCartas != "")
                    {
                        cbCesiones.Items.Add(objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoId", resp_escritos_Ces));//Estatus
                    }
                }

                resp_escritos_Ces.Close();
                con_3_Cesion.Cerrarconexion();
            }
            catch (Exception exs)
            {
                new filelog("Cesiones: ", exs.Message);
            }

            /*string[] ubicacion = Directory.GetFiles(@"C:\Pclientes\Cartas");//<--aqui va la ruta de la carpeta donde estan los documentos
            for (int i = 0; i < ubicacion.Length; i++)
            {
                cbCartas.Items.Add(Path.GetFileName(ubicacion[i]));//combobox el que mostrara todos los nombres
            }*/

            ComboboxItem combouno = new ComboboxItem();//IMPI-00-002_B.docx
            combouno.Text = "Solicitud de Renovación y Declaración de Uso Real y Efectivo de Signos Distintivos";
            combouno.Value = 1;//IMPI_00_002.doc

            ComboboxItem combodos = new ComboboxItem();
            combodos.Text = "Solicitud de Inscripción de Transmisión de Derechos";
            combodos.Value = 2;//	IMPI-00-003-A.doc

            ComboboxItem combotres = new ComboboxItem();
            combotres.Text = "Solicitud de Inscripción de Licencia de Uso o Franquicia";
            combotres.Value = 3;//	IMPI-00-004.doc

            ComboboxItem combocuatro = new ComboboxItem();
            combocuatro.Text = "Solicitud de Toma de Nota de Cambio de Domicilio";
            combocuatro.Value = 4;//IMPI-00-005.doc

            ComboboxItem combocinco = new ComboboxItem();
            combocinco.Text = "Solicitud de Inscripción de Transformación de Régimen Jurídico o Cambio de Nombre, Denominación o Razón Social";
            combocinco.Value = 5;//IMPI-00-006.doc

            ComboboxItem comboseis = new ComboboxItem();
            comboseis.Text = "Solicitud de Autorización de Uso de Denominación de Origen o Indicación Geográfica Nacional";
            comboseis.Value = 6;//IMPI-00-007.doc

            ComboboxItem combosiete = new ComboboxItem();
            combosiete.Text = "Solicitud de Inscripción del Convenio por el que se Permite el Uso de una Denominación de Origen o Indicación Geográfica Nacional";
            combosiete.Value = 7;//IMPI-00-008.doc

            ComboboxItem comboocho = new ComboboxItem();
            comboocho.Text = "Declaración de Uso Real y Efectivo de Signos Distintivos";
            comboocho.Value = 8;//IMPI-00-014_1.docx



            CB_formatoscc.Items.Add(combouno);
            CB_formatoscc.Items.Add(combodos);
            CB_formatoscc.Items.Add(combotres);
            CB_formatoscc.Items.Add(combocuatro);
            CB_formatoscc.Items.Add(combocinco);
            CB_formatoscc.Items.Add(comboseis);
            CB_formatoscc.Items.Add(combosiete);
            CB_formatoscc.Items.Add(comboocho);
                

            //lvDocumentosmarcas
            lvDocumentosmarcas.View = View.Details;
            lvDocumentosmarcas.AllowColumnReorder = true;
            //lvDocumentosmarcas.CheckBoxes = true;
            lvDocumentosmarcas.FullRowSelect = true;
            lvDocumentosmarcas.GridLines = true;
            
            
            //documentosimpi
           
            generadom(CasoId);
            
        }

        public int progresint = 0;
        public void limpiarcontenido() {
            tbDExpediente.Text = "";
            tbDNumeroReg.Text = "";
            //tbDFechalegal.Text = "";
            tbDfecharecepcion.Text = "";
            tbDfecharecepcion_plazos.Text = "";
            tbDFechaconcesion.Text = "";
            //tbDFechaprobo.Text = "";
            tbDFechacarta.Text = "";
            tbDFechainiciouso.Text = "";
            tbDFechavigencia.Text = "";
            tbDSigpruebauso.Text = "";
            rtbDDenominacion.Text = "";
            tbSubtipo.Text = "";
            Ley.Text = "";
            //tbEstatus.Text = "";
            tblTitular.Text = "";
            tbCasoNumero.Text = "";
            tblRefencia.Text = "";
            lResponsable.Text = "";
            tblContacto.Text = "";
            rtCorreocontacto.Text = "";
            tblCliente.Text = "";
            tbl_pais.Text = "";
            tbCasoNumero.Text = "";
            tbExpediente.Text = "";
            tbRegistro.Text = "";
            tbExpediente.Text = "";
            cbClasemarca.Text = "";
            tbclase.Text = "";
            gsIdioma = "";
            cbIdiomaCliente.Items.Clear();
            cbIdiomacarta.Items.Clear();
            cbidiomaescrito.Items.Clear();
            cbIdiomadoc.Items.Clear();
            cbDIdioma.Items.Clear();
            //lvProductos.Items.Clear();
            dGVProductos.Rows.Clear();
            dgVProductosheader.Rows.Clear();
            cbDNoseausado.Checked = false;
            dgview_facturas.Rows.Clear();
            //cbOficiosEscritos.Items.Clear();
            cbOficiosEscritos.Items.Clear();
            rtbDProductossidiomaorig.Text = "";
            cbDTipomarca.Text = "";
            tblCorresponsal.Text = "";
            tblCotaccorresponsal.Text = "";
            richTextBox4.Text = "";

        }
        public Image obj = null;


        public String validacombobox(ComboBox combo)
        {
            if (combo.SelectedItem != null)
            {
                return (combo.SelectedItem as ComboboxItem).Value.ToString();
            }
            else
            {
                return "";
            }
        }

        public void generadom(String sCasoidgenera) {
            try {
                if (validaversion(loguin.sVersion))
                {
                    return;
                }
                limpiarcontenido();
                sCasoId = sCasoidgenera;

                
                cbTiposolprio.Items.Clear();
                ComboboxItem comboTipodos = new ComboboxItem();
                comboTipodos.Text = "PCT";
                comboTipodos.Value = "1";
                cbTiposolprio.Items.Add(comboTipodos);

                ComboboxItem comboTipotres = new ComboboxItem();
                comboTipotres.Text = "París";
                comboTipotres.Value = "2";
                cbTiposolprio.Items.Add(comboTipotres);

                if (loguin.sUsuarioCodigo == "1" || loguin.sUsuarioCodigo == "3")
                {
                    bAgregarplazo.Visible = true;
                }

                tbCasoid.Text = sCasoidgenera;
                conect con_casosmarcas = new conect();
                progressBar1.Show();
                progressBar1.Value = 0;
                this.Enabled = false;
                int icontgeneradom = 0;
                String sQuery = "SELECT " +
                                      "    CasoId," +
                                      "    TipoSolicitudId," +
                                      "    SubTipoSolicitudId," +
                                      "    CasoTituloespanol," +
                                      "    CasoTituloingles," +
                                      "    Get_IdiomaCliente(CasoId, TipoSolicitudId) As IdiomaId," +
                                      "    DATE_FORMAT(CasoFechaConcesion , '%d-%m-%Y') as  CasoFechaConcesion," +
                                      "    DATE_FORMAT(CasoFechaLegal , '%d-%m-%Y') as  CasoFechaLegal," +
                                      //"    DATE_FORMAT(CasoFechaDivulgacionPrevia , '%d-%m-%Y') as  CasoFechaDivulgacionPrevia," +
                                      "    DATE_FORMAT(CasoFechaRecepcion , '%d-%m-%Y') as  CasoFechaRecepcion," +
                                      "    DATE_FORMAT(CasoFechaVigencia , '%d-%m-%Y') as  CasoFechaVigencia," +
                                    
                                      "    CasoNumeroExpedienteLargo," +
                                      "    CasoNumero," +
                                      "    ID_Ley," +
                                      "    ResponsableId," +
                                      "    TipoMarcaId," +
                                      "    CasoLeyendaNoReservable," +
                                      "    DATE_FORMAT(CasoFechaAlta , '%d-%m-%Y') as  CasoFechaAlta," +
                                      "    CasoTipoCaptura," +
                                      "    CasoTitular," +
                                      "    DATE_FORMAT(CasoFechaFilingSistema , '%d-%m-%Y') as  CasoFechaFilingSistema," +
                                      "    DATE_FORMAT(CasoFechaFilingCliente , '%d-%m-%Y') as  CasoFechaFilingCliente," +
                                      "    DATE_FORMAT(CasoFechaCartaCliente , '%d-%m-%Y') as  CasoFechaCartaCliente," +
                                      "   Get_Interesados_tiposol(Casoid, TipoSolicitudId) as NombreUtilInt, " +
                                      "    EstatusCasoId," +
                                      "    UsuarioId," +
                                      "    PaisId," +
                                      "    numregistrointernacional," +
                                      "    productoidiomageneral," +//productoidiomageneral
                                      "    DATE_FORMAT(Fecharegistrointernacional , '%d-%m-%Y') as Fecharegistrointernacional," +
                                      " Get_Paisclave_marcas(Casoid) as PaisClave, " +
                                      " Get_Referencia(Casoid, TipoSolicitudId) as referencia, " +
                                      //"    DATE_FORMAT(CasoFechaPruebaUsoSig , '%d-%m-%Y') as  CasoFechaPruebaUsoSig," +
                                      "    CasoNumConcedida," +
                                      //"    DATE_FORMAT(CasoFechaprobouso , '%d-%m-%Y') as  CasoFechaprobouso," +
                                      "    DATE_FORMAT(CasoFechaDeclaUso , '%d-%m-%Y') as  CasoFechaDeclaUso," +
                                      "    DATE_FORMAT(CasoFechainiciouso , '%d-%m-%Y') as  CasoFechainiciouso" +

                                      " FROM" +
                                      //" casointeresado" +
                                      //" LEFT JOIN" +
                                      //" interesado ON casointeresado.InteresadoId = interesado.InteresadoID" +
                                      //" left JOIN " +
                                      //" tiporelacion ON casointeresado.TipoRelacionId = tiporelacion.TipoRelacionId " +
                                      //" LEFT join" +
                                      //" direccion ON direccion.InteresadoID = interesado.InteresadoID" +
                                      //" where casointeresado.CasoId = '" + sCasoId + "'" +
                                      //" AND casointeresado.TipoSolicitudId = '" + gSTipoSolicitudId + "'" +
                                      //" GROUP BY interesado.InteresadoID order by casointeresado.CasoInteresadoId;";
                                      "    caso_marcas" +
                                      " WHERE " +
                                      //" caso.TipoSolicitudId = tiposolicitud.TipoSolicitudId" +
                                      "        caso_marcas.CasoId = '" + sCasoidgenera + "'";
                MySqlDataReader respuestastring3 = con_casosmarcas.getdatareader(sQuery);
                progressBar1.Value = 10;
                while (respuestastring3.Read())
                {
                    gSTipoSolicitudId = validareader("TipoSolicitudId", "CasoId", respuestastring3).Text;
                    tbCasoid.Text = validareader("CasoId", "CasoId", respuestastring3).Text;
                    //objmarcaactual = new caso_marcas(tbCasoid.Text, gSTipoSolicitudId);
                    gsIdioma = objfuncionesdicss.validareader("IdiomaId", "CasoId", respuestastring3).Text;
                    gsTipomarca = objfuncionesdicss.validareader("TipoMarcaId", "CasoId", respuestastring3).Text;

// Ley.Text = objfuncionesdicss.validareader("Id_Ley", "CasoId", respuestastring3).Text;
                    tbCasoNumero.Text = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                    tbExpediente.Text = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                    tbRegistro.Text = validareader("CasoNumConcedida", "CasoId", respuestastring3).Text;
                    tbExpediente.Text = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                    //Consultamos el pais si existe el paisid
                    tbl_pais.Text = validareader("PaisClave", "CasoId", respuestastring3).Text;
                    gSCasoNumero = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                    tbNumeroregistrointernacional.Text= validareader("numregistrointernacional", "numregistrointernacional", respuestastring3).Text;
                    

                    if (validareader("Fecharegistrointernacional", "Fecharegistrointernacional", respuestastring3).Text == "00-00-0000")
                    {
                        tbFechaRegistrointernacional.Text = "";
                    }
                    else {
                        tbFechaRegistrointernacional.Text = validareader("Fecharegistrointernacional", "Fecharegistrointernacional", respuestastring3).Text;
                    }

                    rtbDProductossidiomaorig.Text = validareader("productoidiomageneral", "productoidiomageneral", respuestastring3).Text;
                    tbEstatusfactura.Text = "";
                    //consultamos las facturas disponibles si es que las hay
                    conect_facturas con_facturas = new conect_facturas();
                    String squery_facturas = " SELECT  " +
                                            "     f.fac_pdf , " +
                                            "     f.fac_id 'Invoice #/ Factura no.', " +
                                            "     CONVERT( fac_fecha , CHAR) 'Date of Issue/ Fecha Emision', " +
                                            "     SUBSTRING(CONVERT( fac_fechapago , CHAR), " +
                                            "         1, " +
                                            "         10) 'Fecha Pago', " +
                                            "     DATEDIFF(IF(fac_fechaPago IS NULL, " +
                                            "                 NOW(), " +
                                            "                 fac_fechapago), " +
                                            "             fac_fecha) 'Days past due/ Dias sin pagar', " +
                                            "     Estatus 'Payment Status/ Status pago', " +
                                            "     Folio_feps 'Folio Feps', " +
                                            "     FORMAT(FLOOR(fac_courierexpenses) + FLOOR((SELECT  "+
                                            "                         SUM(FLOOR(sf_derechos / fac_tc)) " +
                                            "                     FROM " +
                                            "                         servicio_factura sf " +
                                            "                     WHERE " +
                                            "                         sf.fac_id = f.fac_id)) + FLOOR(fac_otrosgastos) + (SELECT  " +
                                            "                 SUM(sf_honorarios) " +
                                            "             FROM " +
                                            "                 servicio_factura sf " +
                                            "             WHERE " +
                                            "                 sf.fac_id = f.fac_id) + FLOOR(fac_costotraduccion), " +
                                            "         2) 'Total (MER)', " +
                                            "      " +
                                            "     GET_NUM_SERVICIOS(f.fac_id) AS Numero_de_servicios, " +
                                            "     GET_SERVICIOS_ROW_UNO(f.fac_id) AS Servicio_uno, " +
                                            "     GET_SERVICIOS_ROW(f.fac_id, 1, 2) AS Servicio_dos, " +
                                            "     GET_SERVICIOS_ROW(f.fac_id, 3, 10) AS Servicio_tres " +
                                            " FROM " +
                                            "     factura f " +
                                            "         INNER JOIN " +
                                            "     cliente c ON (f.cli_id = c.cli_id) " +
                                                " WHERE fac_nuestrareferencia LIKE '% " + tbCasoNumero.Text + "%'; ";

                    MySqlDataReader respuestafacturas= con_facturas.getdatareader(squery_facturas);
                    tbEstatusfactura.BackColor = Control.DefaultBackColor;
                    String sEstatus = "Aún no factura";
                    int iFacturas = 0;
                    bool bFacturaadeudo = false;
                    while (respuestafacturas.Read())
                    {
                        
                        dgview_facturas.Rows.Add(validareader("fac_pdf", "fac_pdf", respuestafacturas).Text,
                                                validareader("Invoice #/ Factura no.", "Invoice #/ Factura no.", respuestafacturas).Text,
                                                validareader("Date of Issue/ Fecha Emision", "Date of Issue/ Fecha Emision", respuestafacturas).Text,
                                                validareader("Fecha Pago", "Fecha Pago", respuestafacturas).Text,
                                                validareader("Days past due/ Dias sin pagar", "Days past due/ Dias sin pagar", respuestafacturas).Text,
                                                validareader("Payment Status/ Status pago", "Payment Status/ Status pago", respuestafacturas).Text,
                                                validareader("Folio Feps", "Folio Feps", respuestafacturas).Text,
                                                validareader("Total (MER)", "Total (MER)", respuestafacturas).Text,
                                                validareader("Numero_de_servicios", "Numero_de_servicios", respuestafacturas).Text,
                                                validareader("Servicio_uno", "Servicio_uno", respuestafacturas).Text,
                                                validareader("Servicio_dos", "Servicio_dos", respuestafacturas).Text,
                                                validareader("Servicio_tres", "Servicio_tres", respuestafacturas).Text);

                        String svalor = validareader("Payment Status/ Status pago", "Payment Status/ Status pago", respuestafacturas).Text;
                        //if (validareader("Payment Status/ Status pago", "Payment Status/ Status pago", respuestafacturas).Text=="Sin pagar") {
                        //    sEstatus = "Factura pendiente";
                        //}
                        if (validareader("Payment Status/ Status pago", "Payment Status/ Status pago", respuestafacturas).Text == "Sin pagar")
                        {
                            sEstatus = "Con adeudo";
                            bFacturaadeudo = true;
                            tbEstatusfactura.BackColor = Color.LightCoral;
                        }
                        iFacturas++;
                    }
                    if (!bFacturaadeudo && iFacturas > 0)
                    { //si no tiene adeudo y tiene más de una factura va al corriente
                        sEstatus = "Al corriente";
                        tbEstatusfactura.BackColor = Color.LightGreen;
                    }
                    tbEstatusfactura.Text = sEstatus;

                    respuestafacturas.Close();
                    con_facturas.Cerrarconexion();


                    //20220502 Agregamos las oposiciones
                    consultamosoposiciones(tbCasoid.Text);
                    //20220502 fin de oposiciones

                    //tipodedocumentos

                    //consultamos los tipos de documentos para edocs
                    try
                    {
                        conect con_tcon_edocs = new conect();
                        String sTipoEdocsquery = "select *  from tipodocumentoelectronico;";
                        MySqlDataReader resp_tedocs = con_tcon_edocs.getdatareader(sTipoEdocsquery);
                        cb_tipodocelect.Items.Clear();
                        while (resp_tedocs.Read())
                        {
                            cb_tipodocelect.Items.Add(objfuncionesdicss.validareader("TipoDocumentoElectronicoDescrip", "TipoDocumentoElectronicoId", resp_tedocs));//consultar idioma
                        }
                        resp_tedocs.Close();
                        con_tcon_edocs.Cerrarconexion();
                    }
                    catch (Exception Ex)
                    {
                        new filelog(loguin.sId, Ex.ToString());
                    }

                    //cbOficiosEscritos
                    cbOficiosEscritos.Items.Clear();
                    conect con_ofiesc = new conect();
                    String sQuery_ofiesc = " SELECT  " +
                                            "     documento.DocumentoId, " +
                                            "     subtipodocumento.SubTipoDocumentoDescrip " +
                                            " FROM " +
                                            "     documento, " +
                                            "     subtipodocumento, " +
                                            "     relaciondocumento " +
                                            " where  " +
                                            " 	relaciondocumento.casoid = " + tbCasoid.Text +
                                            "     and relaciondocumento.TipoSolicitudId = " + gSTipoSolicitudId +
                                            "     and subtipodocumento.TipoDocumentoId in(1 ,2) " +
                                            "     and relaciondocumento.DocumentoId = documento.DocumentoId " +
                                            "     and documento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId; ";
                    MySqlDataReader respuesta_ofiesc = con_ofiesc.getdatareader(sQuery_ofiesc);
                    while (respuesta_ofiesc.Read())
                    {
                        //String sNombredescrip = validareader("SubTipoDocumentoId", "SubTipoDocumentoDescrip", respuestastringclass).Text;
                        //String sId = validareader("SubTipoDocumentoId", "SubTipoDocumentoDescrip", respuestastringclass).Value.ToString();
                        cbOficiosEscritos.Items.Add(validareader("SubTipoDocumentoDescrip", "DocumentoId", respuesta_ofiesc));
                    }
                    respuesta_ofiesc.Close();
                    con_ofiesc.Cerrarconexion();

                    //Consultamos oficios para generar Escritos
                    //cbOficiosparaescritos.Items.Clear();
                    conect con_ofiparaescrito = new conect();
                    String sQuery_ofiparaescrito = " SELECT  " +
                                            "     documento.DocumentoId, " +
                                            "     subtipodocumento.SubTipoDocumentoDescrip " +
                                            " FROM " +
                                            "     documento, " +
                                            "     subtipodocumento, " +
                                            "     relaciondocumento " +
                                            " where  " +
                                            " 	relaciondocumento.casoid = " + tbCasoid.Text +
                                            "     and relaciondocumento.TipoSolicitudId = " + gSTipoSolicitudId +
                                            "     and subtipodocumento.TipoDocumentoId in(1) " +
                                            "     and relaciondocumento.DocumentoId = documento.DocumentoId " +
                                            "     and documento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId; ";
                    MySqlDataReader respuesta_ofiparaescrito = con_ofiparaescrito.getdatareader(sQuery_ofiparaescrito);
                    while (respuesta_ofiparaescrito.Read())
                    {
                        //String sNombredescrip = validareader("SubTipoDocumentoId", "SubTipoDocumentoDescrip", respuestastringclass).Text;
                        //String sId = validareader("SubTipoDocumentoId", "SubTipoDocumentoDescrip", respuestastringclass).Value.ToString();
                        cbOficiosparaescritos.Items.Add(validareader("SubTipoDocumentoDescrip", "DocumentoId", respuesta_ofiparaescrito));
                    }
                    respuesta_ofiparaescrito.Close();
                    con_ofiparaescrito.Cerrarconexion();



                    //consultamos al cliente
                    conect con_cliente = new conect();
                    String squerycliente = "Select NombreUtilClient,cliente.ClienteId,ContactoId from casocliente, cliente where " +
                        " casocliente.CasoId = " + validareader("CasoId", "CasoId", respuestastring3).Text +
                        " and casocliente.TipoSolicitudId = " + gSTipoSolicitudId +
                        " and cliente.clienteid =  casocliente.clienteid;";
                    String sClienteid = "";
                    MySqlDataReader respuestaCliente = con_cliente.getdatareader(squerycliente);
                    while (respuestaCliente.Read())
                    {
                        tblCliente.Text = validareader("NombreUtilClient", "ClienteId", respuestaCliente).Text;
                        sClienteid = validareader("ClienteId", "ClienteId", respuestaCliente).Text;
                        gSContactoid = validareader("ContactoId", "ContactoId", respuestaCliente).Text;
                        gSclienteid = sClienteid;
                    }
                    respuestaCliente.Close();
                    con_cliente.Cerrarconexion();

                    //consultamos al contacto del cliente
                    if (sClienteid != "")
                    {
                        conect con_detalle_cliente = new conect();
                        MySqlDataReader respuestaContacto = con_detalle_cliente.getdatareader("select * from contacto where ContactoId = " + gSContactoid);
                        while (respuestaContacto.Read())
                        {
                            tblContacto.Text = validareader("ContactoNombre", "ContactoId", respuestaContacto).Text;
                            rtCorreocontacto.Text = validareader("ContactoEmail", "ContactoId", respuestaContacto).Text;
                            gSContactoid = validareader("contactoid", "contactoid", respuestaContacto).Text;//consultar idioma
                        }
                        respuestaContacto.Close();
                        con_detalle_cliente.Cerrarconexion();
                    }
                
                    String idUsuario = validareader("UsuarioId", "CasoId", respuestastring3).Text;
                    if(idUsuario != ""){
                        conect con_usuario = new conect();
                        MySqlDataReader respuestaUser = con_usuario.getdatareader("select * from usuario where UsuarioId = " + idUsuario);
                        while (respuestaUser.Read())
                        {
                            //lResponsable.Text = validareader("UsuarioNombre", "UsuarioId", respuestaUser).Text;
                            tblResponsable.Text = validareader("UsuarioNombre", "UsuarioId", respuestaUser).Text;

                        }
                        respuestaUser.Close();
                        con_usuario.Cerrarconexion();
                    }
                    tblRefencia.Text = validareader("referencia", "CasoId", respuestastring3).Text;
                    tblTitular.Text = validareader("NombreUtilInt", "CasoId", respuestastring3).Text;
                    tbCasoNumero.Text = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                

                    //Datos Generales
                    //Estatus caso
                    
                    String sEstatuscasoid = validareader("EstatusCasoId", "CasoId", respuestastring3).Text;
                    if (sEstatuscasoid != "")
                    {
                        conect con_estatus = new conect();
                        MySqlDataReader respuestaEstatus = con_estatus.getdatareader("select * from estatuscaso where EstatusCasoId = " + sEstatuscasoid);
                        while (respuestaEstatus.Read())
                        {
                            //tbEstatus.Text = validareader("EstatusCasoDescrip", "EstatusCasoId", respuestaEstatus).Text;
                            tbEstatus_header.Text = validareader("EstatusCasoDescrip", "EstatusCasoId", respuestaEstatus).Text;
                        }
                        respuestaEstatus.Close();
                        con_estatus.Cerrarconexion();
                    }

                    //Tipo solicitud ID
                    sTipoSolicitudId = validareader("TipoSolicitudId", "CasoId", respuestastring3).Text;
                    if (sTipoSolicitudId != "")
                    {
                        conect con_tiposolicitud = new conect();
                        MySqlDataReader respuestaTiposolic = con_tiposolicitud.getdatareader("select * from tiposolicitud where  TipoSolicitudId = " + sTipoSolicitudId);
                        while (respuestaTiposolic.Read())
                        {
                            tbDtipo.Text = validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestaTiposolic).Text;
                        }
                        respuestaTiposolic.Close();
                        con_tiposolicitud.Cerrarconexion();
                    }
                    tbDtipo.Items.Clear();
                    conect con_tiposolicitud2 = new conect();
                    String query22 = "select * from tiposolicitud where  TipoSolicitudGrupo =" + "2";
                    MySqlDataReader respuestastringtdm2 = con_tiposolicitud2.getdatareader(query22);
                    while (respuestastringtdm2.Read())
                    {
                        tbDtipo.Items.Add(validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestastringtdm2));
                    }
                    respuestastringtdm2.Close();
                    con_tiposolicitud2.Cerrarconexion();

                    //Tipo solicitud ID
                    String sLey = validareader("Id_Ley", "CasoId", respuestastring3).Text;
                    if (sLey != "")
                    {
                        conect con_subtiposolicitud = new conect();
                        String sQuerysubtiposolicitud = "select * from Ley where IdGrupo=2 and  Id_Ley = " + sLey;
                        MySqlDataReader respuestasubTiposol = con_subtiposolicitud.getdatareader(sQuerysubtiposolicitud);
                        while (respuestasubTiposol.Read())
                        {
                            Ley.Text = validareader("Nombre", "ID_Ley", respuestasubTiposol).Text;
                        }
                        respuestasubTiposol.Close();
                        con_subtiposolicitud.Cerrarconexion();
                    }
                    Ley.Items.Clear();
                    conect con_subtiposolicitud2 = new conect();
                    String query222 = "select * from Ley where IdGrupo=2";
                    MySqlDataReader respuestastringtdm23 = con_subtiposolicitud2.getdatareader(query222);
                    while (respuestastringtdm23.Read())
                    {
                        Ley.Items.Add(validareader("Nombre", "ID_Ley", respuestastringtdm23));
                    }
                    respuestastringtdm23.Close();
                    con_subtiposolicitud2.Cerrarconexion();
                    //tbclase.Text = "";
                    //Datos Generales Marca
                    tbDExpediente.Text = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                    tbDNumeroReg.Text = validareader("CasoNumConcedida", "CasoId", respuestastring3).Text;

                    //tbDFechalegal.Text = validafechasvacias(validareader("CasoFechaLegal", "CasoId", respuestastring3).Text);
                    tbDfecharecepcion.Text = validafechasvacias(validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text);
                    
                    tbDFechaconcesion.Text = validafechasvacias(validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text);
                    //tbDFechaprobo.Text = validafechasvacias(validareader("CasoFechaprobouso", "CasoId", respuestastring3).Text);
                    tbDFechacarta.Text = validafechasvacias(validareader("CasoFechaCartaCliente", "CasoId", respuestastring3).Text);
                    tbDFechainiciouso.Text = validafechasvacias(validareader("CasoFechainiciouso", "CasoId", respuestastring3).Text);
                    tbDFechavigencia.Text = validafechasvacias(validareader("CasoFechaVigencia", "CasoId", respuestastring3).Text);
                    if (tbDFechainiciouso.Text != "")
                    {
                        cbDNoseausado.Checked = true;
                    }
                    else {
                        cbDNoseausado.Checked = false;
                    }

                    tbDSigpruebauso.Text = validafechasvacias(validareader("CasoFechaDeclaUso", "CasoId", respuestastring3).Text);
                    //tbEstatus.Text = validareader("EstatusCasoId", "CasoId", respuestastring3).Text;
                    //dependiendo el idioma ponemos 

                    rtbDDenominacion.Text = validareader("CasoTituloingles", "CasoId", respuestastring3).Text;
                    rtbDDenominacion_general.Text = validareader("CasoTituloingles", "CasoId", respuestastring3).Text;


                    String sTipoMarcaId = validareader("TipoMarcaId", "CasoId", respuestastring3).Text;
                    if (sTipoMarcaId != "")
                    {
                        conect con_tipomarca = new conect();
                        MySqlDataReader respuestasubTipomarca = con_tipomarca.getdatareader("select * from tipomarca where TipoMarcaId = " + sTipoMarcaId);
                        while (respuestasubTipomarca.Read())
                        {
                            cbDTipomarca.Text = validareader("TipoMarcaDescrip", "TipoMarcaId", respuestasubTipomarca).Text;
                        }
                        respuestasubTipomarca.Close();
                        con_tipomarca.Cerrarconexion();
                    }
                    icontgeneradom++;
                    icontadorbusqueda = 0;

                    //Agregamos el idioma
                    conect con_idiomas2 = new conect();
                    String sIdiomas2 = "select * from idioma";// where IdiomaId <> " + objfuncionesdicss.validareader("idiomaId", "CasoId", respuestastring3).Text;
                    MySqlDataReader resp_idioma2 = con_idiomas2.getdatareader(sIdiomas2);
                    while (resp_idioma2.Read())
                    {
                        ComboboxItem prueba = objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2);
                        cbDIdioma.Items.Add(objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2));//consultar idioma
                        cbIdiomaCliente.Items.Add(objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2));//consultar idioma
                        cbIdiomacarta.Items.Add(objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2));//consultar escrito
                        cbidiomaescrito.Items.Add(objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2));//consultar escrito
                        cbIdiomadoc.Items.Add(objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2));//consultar poderes y cesiones
                        //cbIdioma.Text = objfuncionesdicss.validareader("IdiomaDescripcion", "CasoId", respuestastring3).Text;//consultar idioma
                    }
                    resp_idioma2.Close();
                    con_idiomas2.Cerrarconexion();


                    conect con_idioma = new conect();

                    String sIdioma = "select * from idioma where IdiomaId = " + objfuncionesdicss.validareader("IdiomaId", "CasoId", respuestastring3).Text;
                    MySqlDataReader resp_idioma = con_idioma.getdatareader(sIdioma);
                    String sIdiomadelcaso = "";
                    while (resp_idioma.Read())
                    {
                        sIdiomadelcaso = objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma).Text;
                    }
                    cbDIdioma.Text = sIdiomadelcaso;
                    cbIdiomaCliente.Text = sIdiomadelcaso;
                    cbIdiomacarta.Text = sIdiomadelcaso;
                    cbidiomaescrito.Text = sIdiomadelcaso;
                    cbIdiomadoc.Text = sIdiomadelcaso;


                    resp_idioma.Close();
                    con_idioma.Cerrarconexion();

                    //cargamos el catalogo de idiomas para prioridades
                    //buscamos el catálogo de paises 
                    conect con_paises = new conect();
                    String sPaisese = "select * from pais"; ;
                    MySqlDataReader resp_paises = con_paises.getdatareader(sPaisese);
                    cbNombrepais.Items.Clear();
                    while (resp_paises.Read())
                    {
                        cbNombrepais.Items.Add(objfuncionesdicss.validareader("PaisNombre", "PaisId", resp_paises));
                        paises[Convert.ToInt32(objfuncionesdicss.validareader("PaisClave", "PaisId", resp_paises).Value.ToString())] = objfuncionesdicss.validareader("PaisClave", "PaisId", resp_paises).Text;
                        paisesclave[Convert.ToInt32(objfuncionesdicss.validareader("PaisClave", "PaisId", resp_paises).Value.ToString())] = objfuncionesdicss.validareader("PaisNombre", "PaisId", resp_paises).Text;
                        //lPais_texbox.Text = objfuncionesdicss.validareader("PaisNombre", "CasoId", respuestastring3).Text;//consultar pais
                    }
                    resp_paises.Close();
                    con_paises.Cerrarconexion();


                }
                respuestastring3.Close();
                con_casosmarcas.Cerrarconexion();

                //validamos el botón para agregar logo en la marca si es mixta o diseño si debe llevar logo id 2 y 3
                //gsTipomarca
                if (gsTipomarca == "2" || gsTipomarca == "3" || gsTipomarca == "5" || gsTipomarca == "6" || gsTipomarca == "7")
                {
                    button37.Visible = true;
                    button20.Visible = true;
                }
                else {
                    button37.Visible = false;
                    button20.Visible = false;
                }


                // consultamos la marca logo
                try {
                    configuracionfiles objfile = new configuracionfiles();
                    objfile.configuracionfilesinicio();
                    String sFileexist = objfile.sFileupload + @"\logos_marcas\0" + sCasoId + @"\0" + sCasoId + ".gif";
                    if (File.Exists(sFileexist))
                    {
                        //aqui buscamos el logo si existe en la carpeta y lo insertamos
                        //y preguntamos si ya existe en la base para agregarlo
                        int icount = 0;

                        conect con_imglogo = new conect();
                        String simglogo = "select count(*) As num from imagen_logo where casoid = " + sCasoId + " and TipoSolicitudId = " + gSTipoSolicitudId + ";";
                        MySqlDataReader resp_imglogo = con_imglogo.getdatareader(simglogo);
                        while (resp_imglogo.Read())
                        {
                            icount = int.Parse(objfuncionesdicss.validareader("num", "num", resp_imglogo).Text);// objfuncionesdicss.validareader("num", "num", resp_imglogo));//consultar idioma
                        }
                        resp_imglogo.Close();
                        con_imglogo.Cerrarconexion();

                        if (icount == 0)
                        { //quiere decir que no está agregado en la base y hay que agregarlo y mostrar posteriomente con la nueva ruta y nombre
                          //INSERT

                            String sDatetime = DateTime.Now.ToString("ddMMyyyyHHmmss").ToString();

                            String sRutaInsert = objfile.sFileupload + @"\logos_marcas\0" + sCasoId + @"\0" + sCasoId + "_" + sDatetime + ".gif";
                            System.IO.File.Copy(sFileexist, sRutaInsert, true);
                            System.IO.File.Delete(sFileexist);
                            conect con_insert_imglogo = new conect();
                            String simglogo_insert = "INSERT INTO `imagen_logo`(`ruta`,`casoid`,`TipoSolicitudId`,`fecha`)VALUES('" + sRutaInsert.Replace("\\", "\\\\") + "','" + sCasoId + "','" + gSTipoSolicitudId + "',now());" + ";";
                            MySqlDataReader resp_insert_imglogo = con_insert_imglogo.getdatareader(simglogo_insert);
                            if (resp_insert_imglogo.RecordsAffected > 0)
                            {//quiere decir que hicimos el insert correctamente
                                obj = Image.FromFile(sRutaInsert);
                                pbDimage.Image = obj;
                            }
                            resp_insert_imglogo.Close();
                            con_insert_imglogo.Cerrarconexion();
                        }
                        else
                        {//si tiene más de uno lo consultamos y lo colocamos en el picturebox

                            String simglogo_consulta = "select * from imagen_logo where casoid = " + sCasoId + " and TipoSolicitudId = " + gSTipoSolicitudId + " order by fecha desc limit 1;";
                            conect con_consul_imglogo = new conect();
                            MySqlDataReader resp_consul_imglogo = con_consul_imglogo.getdatareader(simglogo_consulta);
                            if (resp_consul_imglogo.Read())
                            {//quiere decir que hicimos el insert correctamente
                                String sRutaactual = objfuncionesdicss.validareader("ruta", "ruta", resp_consul_imglogo).Text;
                                obj = Image.FromFile(sRutaactual); ;//
                                pbDimage.Image = obj;
                            }
                            resp_consul_imglogo.Close();
                            con_consul_imglogo.Cerrarconexion();
                        }
                        //obj.Dispose();
                    }
                    else
                    {
                        int icount = 0;

                        conect con_imglogo = new conect();
                        String simglogo = "select count(*) As num from imagen_logo where casoid = " + sCasoId + " and TipoSolicitudId = " + gSTipoSolicitudId + ";";
                        MySqlDataReader resp_imglogo = con_imglogo.getdatareader(simglogo);
                        while (resp_imglogo.Read())
                        {

                            icount = int.Parse(objfuncionesdicss.validareader("num", "num", resp_imglogo).Text);// objfuncionesdicss.validareader("num", "num", resp_imglogo));//consultar idioma
                        }
                        resp_imglogo.Close();
                        con_imglogo.Cerrarconexion();

                        if (icount > 0)
                        {//si la consulta arrojo algun resultado colocamos esa ruta
                            String simglogo_consulta = "select * from imagen_logo where casoid = " + sCasoId + " and TipoSolicitudId = " + gSTipoSolicitudId + " order by fecha desc limit 1;";
                            conect con_consul_imglogo = new conect();
                            MySqlDataReader resp_consul_imglogo = con_consul_imglogo.getdatareader(simglogo_consulta);
                            while (resp_consul_imglogo.Read())
                            {//quiere decir que hicimos el insert correctamente
                                String sRutaactual = objfuncionesdicss.validareader("ruta", "ruta", resp_consul_imglogo).Text;
                                obj = Image.FromFile(sRutaactual); ;//
                                pbDimage.Image = obj;
                            }
                            resp_consul_imglogo.Close();
                            con_consul_imglogo.Cerrarconexion();
                        }
                        else
                        {//si no tiene resultados entonces no hay imagen para ésta Marca
                            pbDimage.Image = null;
                        }
                    }
                } catch (Exception exs) {
                    new filelog("Al cargar logo", " :"+exs.Message);
                }
                

                //Agregamos el dato de marca
                String sMarcasdescription = cargarproductos();
                ///////////////////////////// CODIGO EOSF
                conect conectinteresados = new conect();
                String kweryinteresados = "SELECT "+
                                               " casointeresado.CasoId, "+
                                               " interesado.InteresadoID, " +
                                               "     interesado.InteresadoCurp, " +
                                               "     interesado.InteresadoApPaterno, " +
                                               "     interesado.InteresadoApMaterno, " +
                                               "     interesado.InteresadoRFC, " +
                                               "     direccion.DireccionCalle, " +
                                               "     direccion.DireccionNumExt, " +
                                               "     direccion.DireccionNumInt, " +
                                               "     direccion.DireccionColonia, " +
                                               "     direccion.DireccionCP, " +
                                               "     direccion.DireccionEstado, " +
                                               "     interesado.NombreUtilInt, " +
                                               // interesado.InteresadoNombre
                                               " interesado.InteresadoTipoPersonaSAT, " +
                                               " CONCAT(COALESCE(interesado.NombreUtilInt, ''),  ' ', " +
                                               "         COALESCE(interesado.InteresadoApPaterno, ''), ' ', "+
                                               "         COALESCE(interesado.InteresadoApMaterno, '')) AS interesadonombrecompleto, "+
                                               " CONCAT(COALESCE(direccion.DireccionCalle, ''),  ' ', "+
                                               "         COALESCE(direccion.DireccionNumExt, ''), ' ', "+
                                               "         COALESCE(direccion.DireccionNumInt, ''), ' ', "+
                                               "         COALESCE(direccion.DireccionColonia, ''),' ', "+
                                               "         COALESCE(direccion.DireccionPoblacion, ''), ' ', "+
                                               "         COALESCE(direccion.DireccionEstado, ''), ' ', "+
                                               "         COALESCE(direccion.DireccionCP, '')) AS direccioncompleta, "+
                                               " DAMELANACIONALIDAD(interesado.PaisId) AS nacionalidad, "+
                                               " interesado.PaisId, "+
                                               " tiporelacion.TipoRelacionDescrip, "+
                                               " interesado.InteresadoPoder, "+
                                               " interesado.InteresadoRGP "+
                                           " FROM "+
                                           //    " casointeresado, "+
                                           //    " interesado, "+
                                           //    " direccion, "+
                                           //    " tiporelacion "+
                                           //" WHERE "+
                                               " casointeresado" +
                                               " LEFT JOIN" +
                                               " interesado ON casointeresado.InteresadoId = interesado.InteresadoID" +
                                               " left JOIN " +
                                               " tiporelacion ON casointeresado.TipoRelacionId = tiporelacion.TipoRelacionId " +
                                               " LEFT join" +
                                               " direccion ON direccion.InteresadoID = interesado.InteresadoID" +
                                               " where casointeresado.CasoId = '" + sCasoId + "'" +
                                               " AND casointeresado.TipoSolicitudId = '" + gSTipoSolicitudId + "'" +
                                               " GROUP BY interesado.InteresadoID order by casointeresado.CasoInteresadoId;";
                //" casointeresado.CasoId =  " + sCasoId +
                //                                   " AND interesado.InteresadoID = casointeresado.InteresadoId "+
                //                                   " AND tiporelacion.TipoRelacionId = casointeresado.TipoRelacionId "+
                //                                   " AND interesado.InteresadoID = direccion.InteresadoId "+
                //                           " GROUP BY interesado.InteresadoID;";
                MySqlDataReader respuestainteresados = conectinteresados.getdatareader(kweryinteresados);
                lvinteresados.Items.Clear();

                if (respuestainteresados != null)
                {
                    String TIPOPERSONA = "";
                    int INTERESADOS = 0;
                    while (respuestainteresados.Read())
                    {
                        switch (validareader("InteresadoTipoPersonaSAT", "InteresadoID", respuestainteresados).Text)
                        {
                            case "FN":
                                TIPOPERSONA = "Física Nacional";
                                break;
                            case "FE":
                                TIPOPERSONA = "Física Extranjera";
                                break;
                            case "MN":
                                TIPOPERSONA = "Moral Nacional";
                                break;
                            case "ME":
                                TIPOPERSONA = "Moral Extranjera";
                                break;

                        }
                        if (INTERESADOS == 0)
                        {
                            curp_1 = validareader("InteresadoCurp", "InteresadoID", respuestainteresados).Text;
                            nombre_1 = validareader("NombreUtilInt", "InteresadoID", respuestainteresados).Text;
                            appl1_1 = validareader("InteresadoApPaterno", "InteresadoID", respuestainteresados).Text;
                            appl2_1 = validareader("InteresadoApMaterno", "InteresadoID", respuestainteresados).Text;

                            nacionalidad_1 = validareader("nacionalidad", "InteresadoID", respuestainteresados).Text;

                            rfc_cambintermed2 = validareader("InteresadoRFC", "InteresadoID", respuestainteresados).Text;
                            rasonsoc_cambint2 = validareader("interesadonombrecompleto", "InteresadoID", respuestainteresados).Text;

                            /*consultamos la direccion del primer interesado*/
                            sDireccionCalle = validareader("DireccionCalle", "InteresadoID", respuestainteresados).Text;
                            sDireccionNumExt = validareader("DireccionNumExt", "InteresadoID", respuestainteresados).Text;
                            sDireccionNumInt = validareader("DireccionNumInt", "InteresadoID", respuestainteresados).Text;
                            sDireccionColonia = validareader("DireccionColonia", "InteresadoID", respuestainteresados).Text;
                            sDireccionCP = validareader("DireccionCP", "InteresadoID", respuestainteresados).Text;
                            sPaisId = validareader("PaisId", "InteresadoID", respuestainteresados).Text;

                        }

                        ListViewItem listinteresados = new ListViewItem(validareader("TipoRelacionDescrip", "InteresadoID", respuestainteresados).Text);
                        listinteresados.SubItems.Add(validareader("InteresadoID", "InteresadoID", respuestainteresados).Text);
                        listinteresados.SubItems.Add(validareader("interesadonombrecompleto", "InteresadoID", respuestainteresados).Text);
                        listinteresados.SubItems.Add(validareader("nacionalidad", "InteresadoID", respuestainteresados).Text);
                        listinteresados.SubItems.Add(validareader("direccioncompleta", "InteresadoID", respuestainteresados).Text);
                        listinteresados.SubItems.Add(validareader("InteresadoPoder", "InteresadoID", respuestainteresados).Text);
                        listinteresados.SubItems.Add(validareader("InteresadoRGP", "InteresadoID", respuestainteresados).Text);
                        listinteresados.SubItems.Add(TIPOPERSONA);
                        int residuo = INTERESADOS % 2;
                        if (residuo == 0)
                        {
                            listinteresados.BackColor = Color.LightGray;
                        }
                        else
                        {
                            listinteresados.BackColor = Color.Azure;
                        }
                        lvinteresados.Items.Add(listinteresados);
                        lvinteresados.FullRowSelect = true;
                        INTERESADOS++;
                    }
                    respuestainteresados.Close();
                    conectinteresados.Cerrarconexion();

                    if (sMarcasdescription.Length > 1)
                    {
                        tbclase.Text = sMarcasdescription.Substring(0, sMarcasdescription.Length - 1);
                        cbClasemarca.Text = sMarcasdescription.Substring(0, sMarcasdescription.Length - 1);
                    }
                    else
                    {
                        tbclase.Text = "";
                    }


                    if (icontgeneradom == 0)
                    {
                        //generadom();
                        int iCasoid = 0;
                        if (bBanderaadelanteatras)
                        {//sumamos
                            iCasoid = System.Convert.ToInt32(sCasoId) + 1;
                            icontadorbusqueda++;
                            if (icontadorbusqueda > 5)
                            {
                                MessageBox.Show("No hay mas casos");
                                iCasoid = System.Convert.ToInt32(sCasoId) - icontadorbusqueda;
                            }
                        }
                        else
                        {//restamos
                            iCasoid = System.Convert.ToInt32(sCasoId) - 1;
                        }
                        generadom(iCasoid + "");
                        //MessageBox.Show("Caso vacio");
                    }
                    cbDTipomarca.Items.Clear();
                    conect con_tipomarcas = new conect();
                    String query = "Select * from tipomarca where TipoMarcaIndAct = 1 order by TipoMarcaDescrip;";
                    MySqlDataReader respuestastringtdm = con_tipomarcas.getdatareader(query);
                    while (respuestastringtdm.Read())
                    {
                        cbDTipomarca.Items.Add(validareader("TipoMarcaDescrip", "TipoMarcaId", respuestastringtdm));
                    }
                    respuestastringtdm.Close();
                    con_tipomarcas.Cerrarconexion();
                    
                    //cargamos el dato de ley
                    try{
                        String sIdLey = validareader("ID_Ley", "ID_Ley", respuestastring3).Text;
                        if (sIdLey == "")
                            sIdLey = "0";
                        conect con_ley = new conect();
                        String query_ley = "select * from Ley where IdGrupo=2 and Id_Ley =" + sIdLey;
                        MySqlDataReader respuestastringt_ley = con_ley.getdatareader(query_ley);
                        while (respuestastringt_ley.Read())
                        {
                            String sLeyactual = validareader("Nombre", "ID_Ley", respuestastringt_ley).Text;
                            Ley.Text = sLeyactual;
                            //Ley.Items.Add(validareader("Nombre", "ID_Ley", respuestastringt_ley));
                        }
                        respuestastringt_ley.Close();
                        con_ley.Cerrarconexion();
                    }catch (Exception exs){
                        new filelog("", "" + exs.Message);
                    }

                    
                    conect con_prioridad = new conect();
                    String sQueryprioridades = " SELECT  " +
                                    "     * " +
                                    " FROM " +
                                    "     prioridad, " +
                                    "     pais, " +
                                    "     tipoprioridad " +
                                    " WHERE " +
                                    " prioridad.CasoId = '" + sCasoId + "' " +
                                    " AND prioridad.TipoSolicitudId = " + gSTipoSolicitudId +
                                    "     AND prioridad.PaisID = pais.PaisId " +
                                    "     AND prioridad.TipoPrioridadId = tipoprioridad.TipoPrioridadId order by prioridad.TipoPrioridadId, prioridad.PrioridadFecha desc;";
                    MySqlDataReader respuestastringprio = con_prioridad.getdatareader(sQueryprioridades);
                    lvPrioridades.Items.Clear();
                    int iCount = 0;
                    while (respuestastringprio.Read())
                    {
                        ListViewItem listintprior = new ListViewItem(objfuncionesdicss.validareader("PrioridadId", "PrioridadId", respuestastringprio).Text);
                        listintprior.SubItems.Add(objfuncionesdicss.validareader("PrioridadNumero", "PrioridadId", respuestastringprio).Text);
                        listintprior.SubItems.Add(objfuncionesdicss.validafechavacia(objfuncionesdicss.validareader("PrioridadFecha", "PrioridadId", respuestastringprio).Text));
                        listintprior.SubItems.Add(objfuncionesdicss.validareader("PaisClave", "PrioridadId", respuestastringprio).Text);
                        listintprior.SubItems.Add(objfuncionesdicss.validareader("PaisNombre", "PrioridadId", respuestastringprio).Text);
                        listintprior.SubItems.Add(objfuncionesdicss.validareader("TipoPrioridadDescripcion", "PrioridadId", respuestastringprio).Text);
                        lvPrioridades.Items.Add(listintprior);
                        lvPrioridades.FullRowSelect = true;
                        iCount++;
                    }
                    respuestastringprio.Close();
                    con_prioridad.Cerrarconexion();

                    //cargamos_documentoimpi();
                    cargamos_documentoimpi_datagridview(sCasoidgenera, gSTipoSolicitudId);
                    /*Comentamos los documentos impi*/
                    
                    //lvDocumentosmarcas.Items.Clear();
                    ////documentosimpi
                    //conect con2 = new conect();
                    //String squeryadocumentos = "select " +
                    //                            " documento.DocumentoCodigoBarras, " +
                    //                            " documento.SubTipoDocumentoId, " +
                    //                            " documento.DocumentoFecha, " +
                    //                            " documento.DocumentoFolio, " +
                    //                            " documento.DocumentoFechaRecepcion, " +
                    //                            " documento.DocumentoFechaVencimiento, " +
                    //                            " documento.DocumentoFechaCaptura, " +
                    //                            " documento.DocumentoFechaEscaneo, " +
                    //                            " documento.DocumentoObservacion, " +
                    //                            " documento.DocumentoIdRef, " +
                    //                            " documento.UsuarioId, " +
                    //                            " relaciondocumento.RelacionDocumentoLink, " +
                    //                            " relaciondocumento.casoid, " +
                    //                            " Get_subtipodocumento(documento.SubTipoDocumentoId) as subtipodocumento, " +//Get_tipodocumento
                    //                            " Get_tipodocumento(documento.SubTipoDocumentoId) as TipoDocumentoDescrip, " +//Get_tipodocumento
                    //                            " Get_Usuario(documento.UsuarioId) as Nombreusuario" +
                    //                            " from documento, relaciondocumento" +
                    //                            " where documento.DocumentoId = relaciondocumento.DocumentoId AND relaciondocumento.CasoId = " + sCasoidgenera + ";";
                    //MySqlDataReader resp_docimpi = con2.getdatareader(squeryadocumentos);
                    //while (resp_docimpi.Read())
                    //{
                    //    //lvdocumentosimpi 
                    //    ListViewItem items = new ListViewItem(validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text);
                    //    items.SubItems.Add(validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);
                    //    items.SubItems.Add(validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text);
                    //    items.SubItems.Add(validareader("DocumentoFolio", "casoid", resp_docimpi).Text);
                    //    items.SubItems.Add(validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi).Text);//validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi).Text);
                    //    items.SubItems.Add(validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi).Text);//validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi).Text);
                    //    items.SubItems.Add(validareader("DocumentoFecha", "casoid", resp_docimpi).Text);
                    //    items.SubItems.Add(validareader("DocumentoFechaCaptura", "casoid", resp_docimpi).Text);
                    //    items.SubItems.Add(validareader("subtipodocumento", "casoid", resp_docimpi).Text);

                    //    items.SubItems.Add("");
                    //    items.SubItems.Add("");
                    //    items.SubItems.Add("");

                    //    items.SubItems.Add(validareader("DocumentoObservacion", "casoid", resp_docimpi).Text);
                    //    items.SubItems.Add("");
                    //    lvDocumentosmarcas.Items.Add(items);
                    //}
                    //resp_docimpi.Close();
                    //con2.Cerrarconexion();
                }


                /*Calculamos y mostramos los plazos*/
                //try
                //{
                //    //conect con2_plazos = new conect();
                //    //String squeryplazos = "SELECT * FROM plazo where CasoId = " + sCasoiddom + ";";
                //    /*cabios*/
                //    String sConsultaplazos = " SELECT  " +
                //                                    "     c.CasoNumero, " +
                //                                    "     c.casoId, " +
                //                                    "     cl.NombreUtilClient, " +
                //                                    "     c.CasoTitular, " +
                //                                    "     d.DocumentoFechaRecepcion, " +
                //                                    "     e.EstatusCasoDescrip, " +
                //                                    "     d.DocumentoId, " +
                //                                    "     ep.EstatusPlazoDescrip, " +
                //                                    "     st.SubTipoDocumentoDescrip, " +
                //                                    "     d.DocumentoCodigoBarras, " +
                //                                    "     d.SubTipoDocumentoId, " +
                //                                    "     tp.TipoPlazoDescrip, " +
                //                                    "     tp.TipoPlazoId," +
                //                                    "     Get_Tipodesolicitud(c.casoid) as figura," +
                //                                    "     DAMEELUSUARIO(d.UsuarioId) AS usuario_capturo, " +
                //                                    "     Dameelusuario(p.UsuarioId) as usuario_responsable, " +
                //                                    "     GetNombrecontactobyclienteid(cc.ClienteId) as contactosdetblCliente," +
                //                                    "     Get_referenciasclientefromcasoidtiposolid(c.casoId, c.TipoSolicitudId) as referenciascliente," +
                //                                    "     c.CasoDenominacion," +
                //                                    "     c.CasoNumeroExpedienteLargo," +
                //                                    "     Get_Interesados(c.casoId) as interesados," +
                //                                    "     e.estatuscasoid," +
                //                                    "     tp.TipoTareaId," +
                //                                    "     tptar.TipoTareaDescrip," +
                //                                    "     tp.Grupoid, " +
                //                                    "     TIMESTAMPDIFF(DAY, d.DocumentoFechaRecepcion, now()) as diferenciafecha, " +
                //                                    "     TIMESTAMPDIFF(DAY, d.DocumentoFechaRecepcion, p.PlazoFechaAtencion) as diferenciafechaatendido, " +
                //                                    "     P.* " +
                //                                    " FROM " +
                //                                    "     documento d " +
                //                                    "         JOIN " +
                //                                    "     subtipodocumento st ON st.SubTipoDocumentoId = d.SubTipoDocumentoId " +
                //                                    "         JOIN " +
                //                                    "     relaciondocumento rd ON rd.DocumentoId = d.DocumentoId " +
                //                                    "         JOIN " +
                //                                    "     caso c ON c.CasoId = rd.CasoId " +
                //                                    "         JOIN " +
                //                                    "     casocliente cc ON cc.CasoId = c.CasoId " +
                //                                    "         JOIN " +
                //                                    "     cliente cl ON cl.ClienteId = cc.ClienteId " +
                //                                    "         JOIN " +
                //                                    "     estatuscaso e ON e.estatuscasoid = c.estatuscasoid " +
                //                                    "         JOIN " +
                //                                    "     plazo p ON p.CasoId = c.CasoId " +
                //                                    "         JOIN " +
                //                                    "     estatusplazo ep ON ep.EstatusPlazoId = p.EstatusPlazoId " +
                //                                    "         JOIN " +
                //                                    "     tipoplazo tp ON tp.TipoPlazoId = p.TipoPlazoId " +
                //                                    "         JOIN " +
                //                                    "     tipotarea tptar ON tptar.TipoTareaId = tp.TipoTareaId " +
                //                                    " WHERE p.casoid = " + sCasoId + " group by p.plazoid";
                //    //"limit 500";
                //    //contamos los plazos
                //    int iNum = 0;
                //    String[,] sArray; //= new String[iNum, 29];
                //    //Fin contamos los plazos
                //    conect conin_plazos = new conect();
                //    MySqlDataReader respuestastrig_plazos = conin_plazos.getdatareader(sConsultaplazos);
                //    int iRows = 0;
                //    /*using (loadinprocess form = new loadinprocess(consultamoslosplazos))
                //    {
                //        form.ShowDialog();
                //    }*/
                //    while (respuestastrig_plazos.Read())
                //    {
                //        //String[] saRow = new String[28];
                //        String sNumplazos = objfuncionesdicss.validareader("numplazos", "CasoId", respuestastrig_plazos).Text;
                //        /*iNum = Int32.Parse(sNumplazos);
                //        sArray = new String[iNum, 29];*/
                //        String sCasoNumero = objfuncionesdicss.validareader("CasoNumero", "CasoId", respuestastrig_plazos).Text;
                //        String scasoId = objfuncionesdicss.validareader("casoId", "CasoId", respuestastrig_plazos).Text;
                //        String sNombreUtilClient = objfuncionesdicss.validareader("NombreUtilClient", "CasoId", respuestastrig_plazos).Text;
                //        String sCasoTitular = objfuncionesdicss.validareader("CasoTitular", "CasoId", respuestastrig_plazos).Text;
                //        String sDocumentoFechaRecepcion = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "CasoId", respuestastrig_plazos).Text.Substring(0, 10);
                //        String sEstatusCasoDescrip = objfuncionesdicss.validareader("EstatusCasoDescrip", "CasoId", respuestastrig_plazos).Text;
                //        String sDocumentoId = objfuncionesdicss.validareader("DocumentoId", "CasoId", respuestastrig_plazos).Text;
                //        String sDocumentoCodigoBarras = objfuncionesdicss.validareader("DocumentoCodigoBarras", "CasoId", respuestastrig_plazos).Text;
                //        String sSubTipoDocumentoDescrip = objfuncionesdicss.validareader("SubTipoDocumentoDescrip", "CasoId", respuestastrig_plazos).Text;
                //        String sEstatusPlazoDescrip = objfuncionesdicss.validareader("EstatusPlazoDescrip", "CasoId", respuestastrig_plazos).Text;
                //        String sSubTipoDocumentoId = objfuncionesdicss.validareader("SubTipoDocumentoId", "CasoId", respuestastrig_plazos).Text;
                //        String sTipoPlazoDescrip = objfuncionesdicss.validareader("TipoPlazoDescrip", "CasoId", respuestastrig_plazos).Text;
                //        String sTipoPlazoId = objfuncionesdicss.validareader("TipoPlazoId", "CasoId", respuestastrig_plazos).Text;
                //        String sPlazoFecha = objfuncionesdicss.validareader("PlazoFecha", "CasoId", respuestastrig_plazos).Text.Substring(0, 10);
                //        String sfigura = objfuncionesdicss.validareader("figura", "CasoId", respuestastrig_plazos).Text;
                //        String susuario_capturo = objfuncionesdicss.validareader("usuario_capturo", "CasoId", respuestastrig_plazos).Text;
                //        String susuario_responsable = objfuncionesdicss.validareader("usuario_responsable", "CasoId", respuestastrig_plazos).Text;
                //        String scontactosdetblCliente = objfuncionesdicss.validareader("contactosdetblCliente", "CasoId", respuestastrig_plazos).Text;
                //        String sreferenciascliente = objfuncionesdicss.validareader("referenciascliente", "CasoId", respuestastrig_plazos).Text;
                //        String sCasoDenominacion = objfuncionesdicss.validareader("CasoDenominacion", "CasoId", respuestastrig_plazos).Text;
                //        String sCasoNumeroExpedienteLargo = objfuncionesdicss.validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastrig_plazos).Text;
                //        String sinteresados = objfuncionesdicss.validareader("interesados", "CasoId", respuestastrig_plazos).Text;
                //        String sestatuscasoid = objfuncionesdicss.validareader("estatuscasoid", "CasoId", respuestastrig_plazos).Text;
                //        String sTipoTareaId = objfuncionesdicss.validareader("TipoTareaId", "CasoId", respuestastrig_plazos).Text;
                //        String sGrupoid = objfuncionesdicss.validareader("Grupoid", "CasoId", respuestastrig_plazos).Text;
                //        String sdiferenciafecha = objfuncionesdicss.validareader("diferenciafecha", "CasoId", respuestastrig_plazos).Text;
                //        String sPlazoMotivoCancelacion = objfuncionesdicss.validareader("PlazoMotivoCancelacion", "CasoId", respuestastrig_plazos).Text;
                //        //String susuario_cancelo = objfuncionesdicss.validareader("usuario_cancelo", "CasoId", respuestastrig_plazos).Text;
                //        String sPlazoFechaAtencion = objfuncionesdicss.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", respuestastrig_plazos).Text;
                //        String splazoid = objfuncionesdicss.validareader("plazoid", "plazoid", respuestastrig_plazos).Text;
                //        String sTipoSolicitudId = objfuncionesdicss.validareader("TipoSolicitudId", "plazoid", respuestastrig_plazos).Text;
                //        String sTipoTareaDescrip = objfuncionesdicss.validareader("TipoTareaDescrip", "plazoid", respuestastrig_plazos).Text;
                //        String sPlazoFechaProrroga = objfuncionesdicss.validareader("PlazoFechaProrroga", "plazoid", respuestastrig_plazos).Text;
                //        String sUsuarioIdCancelo = objfuncionesdicss.validareader("UsuarioIdCancelo", "plazoid", respuestastrig_plazos).Text;
                //        String sdiferenciafechaatendido = objfuncionesdicss.validareader("diferenciafechaatendido", "plazoid", respuestastrig_plazos).Text;
                //        //diferenciafechaatendido

                //        ListViewItem items = new ListViewItem(splazoid);//plazoid
                //        items.SubItems.Add(susuario_capturo);//capturo
                //        items.SubItems.Add(sTipoPlazoDescrip);//tipo plazo
                //        items.SubItems.Add(sTipoTareaDescrip);//descripcion tarea
                //        items.SubItems.Add(sSubTipoDocumentoDescrip);
                //        items.SubItems.Add(sDocumentoFechaRecepcion);
                //        items.SubItems.Add(sPlazoFecha);
                //        items.SubItems.Add(sPlazoFechaAtencion);
                //        items.SubItems.Add("aviso cliente");
                //        items.SubItems.Add(sEstatusPlazoDescrip);
                //        if (sdiferenciafechaatendido != "")
                //        {
                //            int Mesatendido = Int32.Parse(sdiferenciafechaatendido) / 30;
                //            items.SubItems.Add(Mesatendido + "");
                //        }
                //        else
                //        {
                //            int Mesatendido = Int32.Parse(sdiferenciafecha) / 30;
                //            items.SubItems.Add(Mesatendido + "");
                //        }

                //        items.SubItems.Add(sPlazoFecha);
                //        items.SubItems.Add(sPlazoFechaAtencion);
                //        items.SubItems.Add(sPlazoFechaProrroga);
                //        items.SubItems.Add("");
                //        items.SubItems.Add(sPlazoMotivoCancelacion);
                //        lvPlazos.Items.Add(items);

                //        //    ListViewItem items = new ListViewItem(objfuncionesdicss.validareader("plazoid", "plazoid", resp_plazos).Text);//plazoid
                //        //    items.SubItems.Add(objfuncionesdicss.validareader("usuarioIdAtendio", "plazoid", resp_plazos).Text);//capturo
                //        //    items.SubItems.Add(objfuncionesdicss.validareader("TipoPlazoId", "plazoid", resp_plazos).Text);//tipoplazo
                //        //    items.SubItems.Add(objfuncionesdicss.validareader("TipoPlazoId", "plazoid", resp_plazos).Text);//descripcion tarea
                //        //    items.SubItems.Add(objfuncionesdicss.validareader("DocumentoId", "plazoid", resp_plazos).Text);//Documento
                //        //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFechaAtencion", "plazoid", resp_plazos).Text);//Notificado en
                //        //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFecha", "plazoid", resp_plazos).Text);//Vencimiento original
                //        //    items.SubItems.Add("2 meses");//Fecha escrito
                //        //    items.SubItems.Add("3 meses");//Fecha escrito
                //        //    items.SubItems.Add("mes");//mes
                //        //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFechaAtencion", "plazoid", resp_plazos).Text);//Fecha escrito
                //        //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFechaProrroga", "plazoid", resp_plazos).Text);//Aviso cliente
                //        //    items.SubItems.Add(objfuncionesdicss.validareader("EstatusPlazoId", "plazoid", resp_plazos).Text);//Estatus 
                //        //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFechaProrroga", "plazoid", resp_plazos).Text);//plazo final
                //        //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFechaAtencion", "plazoid", resp_plazos).Text);//Fecha atención
                //        //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFechaProrroga", "plazoid", resp_plazos).Text);//Fecha prorroga
                //        //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoMotivoCancelacion", "plazoid", resp_plazos).Text);//prorrogó o canceló
                //        //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoMotivoCancelacion", "plazoid", resp_plazos).Text);//Motivo cancelación
                //        //    lvPlazos.Items.Add(items);
                //        //    //lvdocumentosimpi.Items.Add(items);


                //    }
                //    /*fin de cambios*/
                //}
                //catch (Exception E)
                //{
                //    MessageBox.Show("Revisar plazos");
                //}
                consultamosdocumentoselectronicos();

                progressBar1.Value = 90;
                //respuestastring3.Close();
                progressBar1.Value = 100;
                progressBar1.Hide();
                this.Enabled = true;
                consultaplazo_nuevos();
                consultareferencias();
                cargarcorresponsal();
            }
            catch(Exception E){
                new filelog(loguin.sId, E.ToString());            
                progressBar1.Value = 100;
                progressBar1.Hide();
                this.Enabled = true;
            }
            
            
        }

        //public void cargarcorresponsal() {
        //    //try {
        //    //    conect conexcorresponsal = new conect();
        //    //    String sQryCorresponsal = "SELECT caso_contencioso.CasoId, " +
        //    //                                "cliente.NombreUtilClient AS ClienteCorresponsal,   " +
        //    //                                "DAMEALCONTACTOCASO(casocorresponsal.ContactoId) AS ContactoCorresponsal,   " +
        //    //                                "cliente.ClienteEmail AS CorreoCorresponsal " +
        //    //                                "FROM caso_contencioso, casocorresponsal, cliente " +
        //    //                                "WHERE caso_contencioso.CasoId = casocorresponsal.CasoId " +
        //    //                                "AND casocorresponsal.ClienteId = cliente.ClienteId " +
        //    //                                "AND casocorresponsal.CasoId  = " + sCasoId +
        //    //                                " AND casocorresponsal.TipoSolicitudId  = '" + gSTipoSolicitudId + "';";
        //    //    MySqlDataReader respuestaCorresponsal = conexcorresponsal.getdatareader(sQryCorresponsal);
        //    //    while (respuestaCorresponsal.Read())
        //    //    {
        //    //        tblCorresponsal.Text = validareader("ClienteCorresponsal", "CasoId", respuestaCorresponsal).Text;
        //    //        tblCotaccorresponsal.Text = validareader("ContactoCorresponsal", "CasoId", respuestaCorresponsal).Text;
        //    //        richTextBox4.Text = validareader("CorreoCorresponsal", "CasoId", respuestaCorresponsal).Text;
        //    //    }
        //    //    respuestaCorresponsal.Close();
        //    //    conexcorresponsal.Cerrarconexion();
        //    //}
        //    //catch (Exception exs) { 

        //    //}
        //    try
        //    {
        //        conect conexcorresponsal = new conect();
        //        String sQryCorresponsal = " SELECT  " +
        //                                    "     * " +
        //                                    " FROM " +
        //                                    "     casocorresponsal " +
        //                                    "         LEFT JOIN " +
        //                                    "     cliente ON casocorresponsal.ClienteId = cliente.ClienteId " +
        //                                    " 		LEFT JOIN  " +
        //                                    " 	contacto ON casocorresponsal.ContactoId = contacto.ContactoId " +
        //                                    " WHERE  " +
        //                                    " 		casocorresponsal.CasoId = " + sCasoId +
        //                                    "         AND casocorresponsal.TipoSolicitudId = '" + gSTipoSolicitudId + "';";
        //        //"SELECT caso_patente.CasoId, " +
        //        //                        "cliente.NombreUtilClient AS ClienteCorresponsal,   " +
        //        //                        "DAMEALCONTACTOCASO(casocorresponsal.ContactoId) AS ContactoCorresponsal,   " +
        //        //                        "cliente.ClienteEmail AS CorreoCorresponsal " +
        //        //                        "FROM caso_patente, casocorresponsal, cliente " +
        //        //                        "WHERE caso_patente.CasoId = casocorresponsal.CasoId " +
        //        //                        "AND casocorresponsal.ClienteId = cliente.ClienteId " +
        //        //                        "AND casocorresponsal.CasoId  = " + sCasoId +
        //        //                        " AND casocorresponsal.TipoSolicitudId  = '" + gSTipoSolicitudId + "';";
        //        MySqlDataReader respuestaCorresponsal = conexcorresponsal.getdatareader(sQryCorresponsal);
        //        while (respuestaCorresponsal.Read())
        //        {
        //            tblCorresponsal.Text = objfuncionesdicss.validareader("NombreUtilClient", "CasoId", respuestaCorresponsal).Text;
        //            tblCotaccorresponsal.Text = objfuncionesdicss.validareader("ContactoNombre", "CasoId", respuestaCorresponsal).Text;
        //            richTextBox4.Text = objfuncionesdicss.validareader("ContactoEmail", "CasoId", respuestaCorresponsal).Text;
        //        }
        //        respuestaCorresponsal.Close();
        //        conexcorresponsal.Cerrarconexion();
        //    }
        //    catch (Exception exs)
        //    {

        //    }
        //}

        public void cargarcorresponsal()
        {
            try
            {
                conect conexcorresponsal = new conect();
                String sQryCorresponsal = " SELECT  " +
                                            "     * " +
                                            " FROM " +
                                            "     casocorresponsal " +
                                            "         LEFT JOIN " +
                                            "     cliente ON casocorresponsal.ClienteId = cliente.ClienteId " +
                                            " 		LEFT JOIN  " +
                                            " 	contacto ON casocorresponsal.ContactoId = contacto.ContactoId " +
                                            " WHERE  " +
                                            " 		casocorresponsal.CasoId = " + sCasoId +
                                            "         AND casocorresponsal.TipoSolicitudId = '" + gSTipoSolicitudId + "';";
                //"SELECT caso_patente.CasoId, " +
                //                        "cliente.NombreUtilClient AS ClienteCorresponsal,   " +
                //                        "DAMEALCONTACTOCASO(casocorresponsal.ContactoId) AS ContactoCorresponsal,   " +
                //                        "cliente.ClienteEmail AS CorreoCorresponsal " +
                //                        "FROM caso_patente, casocorresponsal, cliente " +
                //                        "WHERE caso_patente.CasoId = casocorresponsal.CasoId " +
                //                        "AND casocorresponsal.ClienteId = cliente.ClienteId " +
                //                        "AND casocorresponsal.CasoId  = " + sCasoId +
                //                        " AND casocorresponsal.TipoSolicitudId  = '" + gSTipoSolicitudId + "';";
                MySqlDataReader respuestaCorresponsal = conexcorresponsal.getdatareader(sQryCorresponsal);
                while (respuestaCorresponsal.Read())
                {
                    tblCorresponsal.Text = objfuncionesdicss.validareader("NombreUtilClient", "CasoId", respuestaCorresponsal).Text;
                    tblCotaccorresponsal.Text = objfuncionesdicss.validareader("ContactoNombre", "CasoId", respuestaCorresponsal).Text;
                    richTextBox4.Text = objfuncionesdicss.validareader("ContactoEmail", "CasoId", respuestaCorresponsal).Text;
                }
                respuestaCorresponsal.Close();
                conexcorresponsal.Cerrarconexion();
            }
            catch (Exception exs)
            {

            }
        }

        private String cargarproductos()
        {
            try
            {
                String sMarcasdescription = "";
                conect con_casoproductos = new conect();
                String sQueryclasemarcas = "SELECT * FROM `casoproductos` WHERE casoid =" + tbCasoid.Text;
                MySqlDataReader respuestastring = con_casoproductos.getdatareader(sQueryclasemarcas);
                int iContador = 0;
                //lvProductos.Items.Clear();
                dGVProductos.Rows.Clear();
                dgVProductosheader.Rows.Clear();
                while (respuestastring.Read())
                {
                    sMarcasdescription += validareader("CasoProductosClase", "CasoProductosId", respuestastring).Text + ",";
                    //ListViewItem listview = new ListViewItem(validareader("CasoProductosId", "CasoProductosId", respuestastring).Text);
                    //listview.SubItems.Add(validareader("CasoProductosClase", "CasoProductosId", respuestastring).Text);
                    //listview.SubItems.Add(validareader("CasoProductosDescripcion", "CasoProductosId", respuestastring).Text.Replace("\n", ""));
                    //lvProductos.Items.Add(listview);

                    dGVProductos.Rows.Add(
                        validareader("CasoProductosId", "CasoProductosId", respuestastring).Text,
                        validareader("CasoProductosClase", "CasoProductosId", respuestastring).Text,
                        validareader("CasoProductosDescripcion", "CasoProductosId", respuestastring).Text.Replace("\n", "")
                        );
                    dgVProductosheader.Rows.Add(
                        validareader("CasoProductosId", "CasoProductosId", respuestastring).Text,
                        validareader("CasoProductosClase", "CasoProductosId", respuestastring).Text,
                        validareader("CasoProductosDescripcion", "CasoProductosId", respuestastring).Text.Replace("\n", "")
                        );

                    iContador++;
                }
                tbNumprod.Text = iContador + "";
                respuestastring.Close();
                con_casoproductos.Cerrarconexion();
                return sMarcasdescription;
            }catch(Exception Ex){
                return "";
            }
        }

        public ComboboxItem validareader(String campoText, String campoValue, MySqlDataReader mresultado)
        {
            try
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
            }catch(Exception E){
                ComboboxItem cItemresult = new ComboboxItem();
                cItemresult.Text = "";
                cItemresult.Value = "";
                return cItemresult;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //loguin.Close();
            //fCapuraform.Close();
            buscarclienteform.Close();
            this.Close();
        }

        private void button39_Click(object sender, EventArgs e)
        {
            //bBanderaadelanteatras = false;
            //int iCasoid = System.Convert.ToInt32(sCasoId) - 1;
            //generadom(iCasoid+"");

            try
            {
                iIndiceids_global = iIndiceids_global - 1;
                generadom(sArrayids[iIndiceids_global] + "");
            }
            catch (Exception E)
            {
                iIndiceids_global = sArrayids.Length - 1;
                generadom(sArrayids[iIndiceids_global] + "");

            }
            
        }

        private void button38_Click(object sender, EventArgs e)
        {
            //bBanderaadelanteatras = true;
            //int iCasoid = System.Convert.ToInt32(sCasoId) + 1;
            //generadom(iCasoid + "");

            try
            {
                iIndiceids_global = iIndiceids_global + 1;
                generadom(sArrayids[iIndiceids_global] + "");
            }
            catch (Exception E)
            {
                iIndiceids_global = 0;
                generadom(sArrayids[iIndiceids_global] + "");
            }
            
        }

        private void cbClasemarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                rtbdescripcionclase.Text = (cbClasemarca.SelectedItem as ComboboxItem).Value.ToString();
                rtDescripciondelproducto.Text = "";
                if ((cbClasemarca.SelectedItem as ComboboxItem).Text != "")
                {

                    int iClase = int.Parse((cbClasemarca.SelectedItem as ComboboxItem).Text);
                    if (iClase > 34)//son servicios
                    {
                        tabPage2.Text = "Servicios";
                        lDescrip.Text = "Escriba los servicios que quiera cargar al caso:";
                        lProductosdelcaso.Text = "Descripción de Servicios entregados al IMPI";
                        label16.Text = "Servicios idioma \noriginal";
                    }
                    else
                    { //son productos
                        tabPage2.Text = "Productos";
                        lDescrip.Text = "Escriba los productos que quiera cargar al caso:";
                        lProductosdelcaso.Text = "Descripción de Productos entregados al IMPI:";
                        label16.Text = "Productos idioma \noriginal";
                    }
                }
                //(combGrupos.SelectedItem as ComboboxItem).Value.ToString();
            }catch(Exception Ex){
            }
            
        }

        private void button33_Click(object sender, EventArgs e)
        {
            try
            {
                if ((CB_formatoscc.SelectedItem as ComboboxItem).Value != null)
                {
                    String sNamefile = "";
                    switch (System.Convert.ToInt32((CB_formatoscc.SelectedItem as ComboboxItem).Value))
                    {
                        case 1:
                            {   //IMPI_00_002.doc 100%
                                //Solicitud de Renovación y Declaración de Uso Real y Efectivo de Signos Distintivos
                                renovacionydeclaacion obj = new renovacionydeclaacion(loguin, sCasoId, this);
                                obj.Show();
                        } break;
                        case 2://	IMPI-00-003.docx 100%
                            { // Solicitud de Inscripción de Transmisión de Derechos  IMPI-00-003.docx
                                Formato_03 obj = new Formato_03(loguin, sCasoId, this);
                                obj.Show();
                        } break;
                        case 3: //	IMPI-00-004.doc  100%
                            { //Solicitud de Inscripción de Licencia de Uso o Franquicia
                                FLicenciaFranquicia obj = new FLicenciaFranquicia(loguin, sCasoId, this);
                                obj.Show();
                        } break;
                        case 4://Solicitud de Toma de Nota de Cambio de Domicilio 100% revisar las opciones
                            { //IMPI-00-005.doc
                                FNotadeCambioDomicilio obj = new FNotadeCambioDomicilio(loguin, sCasoId, this);
                                obj.Show();
                        } break;
                        case 5://	IMPI-00-006.doc 100%
                            {//Solicitud de Inscripción de Transformación de Régimen Jurídico o Cambio de Nombre, Denominación o Razón Social
                                FCambioReginemNombreDenominacionRazon_2 obj = new FCambioReginemNombreDenominacionRazon_2(loguin, sCasoId, this);
                                obj.Show();
                        } break;
                        case 6: //	IMPI-00-007.doc 100% 
                            { //Solicitud de Autorización de Uso de Denominación de Origen o Indicación Geográfica Nacional
                                FAutorizacionDenomicaciondeOrigen_2 obj = new FAutorizacionDenomicaciondeOrigen_2(loguin, sCasoId, this);
                                obj.Show();
                            } break;
                        case 7:
                            {//IMPI-00-008.doc 
                            //Solicitud de Inscripción del Convenio por el que se Permite el Uso de una Denominación de Origen o Indicación Geográfica Nacional
                                FInscripcionConvenioDOrigen obj = new FInscripcionConvenioDOrigen(loguin, sCasoId, this);
                                obj.Show();
                            
                        } break;
                        case 8:
                            {//IMPI-00-014_1.docx
                            //Declaración de Uso Real y Efectivo de Signos Distintivos
                                FDeclaracionrealyefectivo obj = new FDeclaracionrealyefectivo(loguin, sCasoId, this);
                                obj.Show();

                        } break;
                        //
                    }
                }
            }
            catch (Exception E)
            {
                new filelog(loguin.sId, E.ToString());
            }
            //Create Document
            //generadocs prueba = new generadocs();
            //prueba.generadocss();  
            
        }

        private void button31_Click(object sender, EventArgs e)
        {
            try {
                String sIdidiomaescritos = "";
                if ((cbIdiomacarta.SelectedItem as ComboboxItem) != null)
                { //idioma seleccionado para la carta
                    sIdidiomaescritos = (cbIdiomacarta.SelectedItem as ComboboxItem).Value.ToString();
                }
                else
                {
                    MessageBox.Show("Seleccione un idioma.");
                    return;
                }


                if (cbCartas.SelectedItem==null) {
                    return;
                }

                //cargamos los datos

                if (cbOficiosEscritos.SelectedItem != null)
                {
                    String sDocumentoid = (cbOficiosEscritos.SelectedItem as ComboboxItem).Value.ToString();
                    objmarcaactualview = new view_caso_marcas(tbCasoid.Text, gSTipoSolicitudId, sIdidiomaescritos, sDocumentoid);
                }
                else {
                    objmarcaactualview = new view_caso_marcas(tbCasoid.Text, gSTipoSolicitudId, sIdidiomaescritos);
                }
                
                String valuecob = (cbCartas.SelectedItem as ComboboxItem).Value.ToString();
                String sCartanombreESPfile = "";
                String sCartanombreENfile = "";
                conect con_carta = new conect();
                String sQuery_carta = " SELECT  " +
                                        "     * " +
                                        " FROM " +
                                        "     subtipodocumento " +
                                        " WHERE " +
                                        " SubtipodocumentoId = " + valuecob;
                MySqlDataReader respuesta_carta = con_carta.getdatareader(sQuery_carta);
                while (respuesta_carta.Read())
                {
                    sCartanombreESPfile = validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoTemplateEspanol", respuesta_carta).Text;
                    sCartanombreENfile = validareader("SubTipoDocumentoTemplateIngles", "SubTipoDocumentoTemplateIngles", respuesta_carta).Text;
                    //Aquí podemos validar el idioma y si existe la plantilla
                    //generacarta objcarta = new generacarta(sCartanombreESPfile, valuecob, objmarcaactual);
                    if (sIdidiomaescritos == "1")
                    {//Ingles
                        if (sCartanombreENfile=="") {
                            MessageBox.Show("No existe la carta para éste idioma (EN)");
                            return;
                        }
                        String[] sValorusuario = new string[2];
                        sValorusuario[0] = "idUsuariosistema";
                        sValorusuario[1] = sUsuarioparadocs;
                        objmarcaactualview.sValorescampos.Add(sValorusuario);
                        generacarta objcarta = new generacarta(sCartanombreENfile, valuecob, objmarcaactualview);
                    }
                    else {
                        if (sIdidiomaescritos == "2")
                        {
                            if (sCartanombreESPfile == "")
                            {
                                MessageBox.Show("No existe la carta para éste idioma (ES)");
                                return;
                            }
                            String[] sValorusuario = new string[2];
                            sValorusuario[0] = "idUsuariosistema";
                            sValorusuario[1] = sUsuarioparadocs;
                            generacarta objcarta = new generacarta(sCartanombreESPfile, valuecob, objmarcaactualview);
                        }
                        else {
                            MessageBox.Show("Debe asignar un Idioma al cliente");
                        }
                    
                    }
                    

                }
                respuesta_carta.Close();
                con_carta.Cerrarconexion();
            }
            catch (Exception ex) { 
            }
            //String valorcarta = cbCartas.Text;
            //if (cbCartas.SelectedItem == null)
            //{
            //    MessageBox.Show("Debes Seleccionar un tiposolicitd");
            //}
            //else
            //{
            //    generacartas prueba = new generacartas();
            //    //valuecob = (CB_tiposolicitudgeneracartas.SelectedItem as ComboboxItem).Value.ToString();//numero de tiposolicitud
            //    prueba.generacartass(sCasoId, sTipogrupoglobal, valorcarta);//casoId, tiposolicitud, nombre carta
            //    MessageBox.Show("Se ah generado Correctamente");

            //}
                //generadocs prueba = new generadocs();
                //prueba.generadocss(sCasoId, "3");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            buscarclienteform.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*Aquí debemos hacer el update para guardar la información modificada*/
            try {
                String cambioidioma = "";
                String sTipomarca = "0";// (cbDTipomarca.SelectedItem as ComboboxItem).Value.ToString();
                if (!(cbDTipomarca.SelectedItem is null)) {
                    sTipomarca = (cbDTipomarca.SelectedItem as ComboboxItem).Value.ToString();
                }
                


                if (cbDIdioma.SelectedItem!=null) {
                    cambioidioma = " IdiomaId = '" + (cbDIdioma.SelectedItem as ComboboxItem).Value+"',";
                }
                String sUpdtaecaso_mascas = " UPDATE caso_marcas SET  " +
                                            " CasoNumeroExpedienteLargo = '" + tbDExpediente.Text + "', " +
                                            cambioidioma +
                                            " CasoNumConcedida = '" + tbDNumeroReg.Text + "', " +
                                            " productoidiomageneral = '" + rtbDProductossidiomaorig.Text + "', " +
                                            " TipoMarcaId = '" + sTipomarca + "', " +
                                            " ID_Ley = '" + Ley + "', " +
                                            //productoidiomageneral

                                            " numregistrointernacional = '" + tbNumeroregistrointernacional.Text + "', " +
                                            " Fecharegistrointernacional = DATE(STR_TO_DATE('" + tbFechaRegistrointernacional.Text + "', '%d-%m-%Y')), " +

                                            " CasoFechaRecepcion = DATE(STR_TO_DATE('" + tbDfecharecepcion.Text + "', '%d-%m-%Y')), " +
                                            " CasoFechaCartaCliente = DATE(STR_TO_DATE('" + tbDFechacarta.Text + "', '%d-%m-%Y')), " +
                                            " CasoFechaConcesion = DATE(STR_TO_DATE('" + tbDFechaconcesion.Text + "', '%d-%m-%Y')), " +
                                            " CasoFechaVigencia = DATE(STR_TO_DATE('" + tbDFechavigencia.Text + "', '%d-%m-%Y')), " +
                                            " CasoFechainiciouso = DATE(STR_TO_DATE('" + tbDFechainiciouso.Text + "', '%d-%m-%Y')), " +
                                            //" CasoFechaprobouso = DATE(STR_TO_DATE('" + tbDSigpruebauso.Text + "', '%d-%m-%Y')), " +
                                            " CasoFechaDeclaUso = DATE(STR_TO_DATE('" + tbDSigpruebauso.Text + "', '%d-%m-%Y')), " +
                                            " CasoTituloingles = '" + rtbDDenominacion.Text + "' " +
                                            " WHERE CasoId = '"+sCasoId+"' AND TipoSolicitudId = '"+gSTipoSolicitudId+"'; ";
                conect con1 = new conect();
                MySqlDataReader resp_query = con1.getdatareader(sUpdtaecaso_mascas);
                if (resp_query.RecordsAffected > 0)
                {
                    MessageBox.Show("Se modificó correctamente.");
                    generadom(sCasoId);
                }
                resp_query.Close();
                con1.Cerrarconexion();
                /*Los Datos que podemos modificar son los siguientes:
                 *
                    TipoSolicitudId
                    SubTipoSolicitudId
                    CasoTituloingles
                    CasoTituloespanol
                    IdiomaId
                    CasoFechaConcesion
                    CasoFechaLegal
                    CasoFechaDivulgacionPrevia
                    CasoFechaRecepcion
                    CasoFechaVigencia
                    CasoNumeroConcedida
                    CasoNumeroExpedienteLargo
                    CasoNumero
                    ResponsableId
                    TipoMarcaId
                    CasoLeyendaNoReservable
                    CasoFechaAlta
                    CasoTipoCaptura
                    CasoTitular
                    CasoFechaFilingSistema
                    CasoFechaFilingCliente
                    CasoFechaCartaCliente
                    EstatusCasoId
                    UsuarioId
                    PaisId
                    CasoFechaPruebaUsoSig
                    CasoNumConcedida
                    CasoFechaprobouso
                    CasoFechainiciouso
                 *
                 * 
                 */
                //conect con_4 = new conect();
                //String updateestatuscaso = "UPDATE `caso_marcas` SET `EstatusCasoId` = '" + value + "' WHERE (`CasoId` = '" + sCasoId + "' and TipoSolicitudId = " + gSTipoSolicitudId + ");";
                //MySqlDataReader resp_updatecaso = con_4.getdatareader(updateestatuscaso);
                //if (resp_updatecaso != null)
                //{
                //    tbEstatus.Text = texti;
                //    tbEstatus_header.Text = texti;
                //}
                //resp_updatecaso.Close();
                //con_4.Cerrarconexion();
            }catch(Exception Ex){
                new filelog("linea: 1000  UPDATE caso_marcas ", ": " + Ex.Message);
            }
        }

        private void button43_Click(object sender, EventArgs e)
        {

        }

        private void button42_Click(object sender, EventArgs e)
        {

        }

        private void button41_Click(object sender, EventArgs e)
        {

        }

        private void button44_Click(object sender, EventArgs e)
        {
            CapturaSolicitud addescrito = new CapturaSolicitud(fCapuraform, loguin, "2", sCasoId);
            if (addescrito.ShowDialog() == DialogResult.OK)
            {
                generadom(sCasoId);
                //tbEstatus.Text = addescrito.sEstatusnuevodepuesdensert;
                //tbEstatus_header.Text = addescrito.sEstatusnuevodepuesdensert;
            }
        }

        private void button41_Click_1(object sender, EventArgs e)
        {
            CapturaEscritos addescrito = new CapturaEscritos(fCapuraform, loguin, "2", sCasoId);
            if (addescrito.ShowDialog() == DialogResult.OK)
            {
                generadom(sCasoId);
                //tbEstatus.Text = addescrito.sEstatusnuevodepuesdensert;
                //tbEstatus_header.Text = addescrito.sEstatusnuevodepuesdensert;
            }
        }

        private void button42_Click_1(object sender, EventArgs e)
        {
            CapturadeOficios addoficio = new CapturadeOficios(fCapuraform, loguin, "2", sCasoId, "");
            if (addoficio.ShowDialog() == DialogResult.OK)
            {
                generadom(sCasoId);

            }
        }

        private void button43_Click_1(object sender, EventArgs e)
        {
            Capturadetitulo addtitulo = new Capturadetitulo(fCapuraform, loguin, "2", sCasoId);
            if (addtitulo.ShowDialog() == DialogResult.OK)
            {
                generadom(sCasoId);
                //tbEstatus.Text = addescrito.sEstatusnuevodepuesdensert;
                //tbEstatus_header.Text = addescrito.sEstatusnuevodepuesdensert;
            }
        }

        private void tbEstatus_DoubleClick(object sender, EventArgs e)
        {
            try{
                updateEstatus updateestatus = new updateEstatus("2");
                if (updateestatus.ShowDialog() == DialogResult.OK)
                {
                    String value = updateestatus.sValueestatus;//hacemos el update con este valor y ponemos el Texto en el form
                    String texti = updateestatus.sTextoestatus;//hacemos el update con este valor y ponemos el Texto en el form
                    conect con_4 = new conect();
                    String updateestatuscaso = "UPDATE `caso_marcas` SET `EstatusCasoId` = '" + value + "' WHERE (`CasoId` = '" + sCasoId + "' and TipoSolicitudId = " + gSTipoSolicitudId + ");";
                    MySqlDataReader resp_updatecaso = con_4.getdatareader(updateestatuscaso);
                    if (resp_updatecaso != null)
                    {
                        //tbEstatus.Text = texti;
                        tbEstatus_header.Text = texti;
                    }
                    resp_updatecaso.Close();
                    con_4.Cerrarconexion();
                }
            }catch(Exception Ex){
                new filelog("linea", "Error: "+Ex.Message);
            }
        }

        private void tbEstatus_header_DoubleClick(object sender, EventArgs e)
        {
            try {
                updateEstatus updateestatus = new updateEstatus("2");
                if (updateestatus.ShowDialog() == DialogResult.OK)
                {
                    String value = updateestatus.sValueestatus;//hacemos el update con este valor y ponemos el Texto en el form
                    String texti = updateestatus.sTextoestatus;//hacemos el update con este valor y ponemos el Texto en el form
                    conect con_4 = new conect();
                    String updateestatuscaso = "UPDATE `caso_marcas` SET `EstatusCasoId` = '" + value + "' WHERE (`CasoId` = '" + sCasoId + "' and TipoSolicitudId = " + gSTipoSolicitudId + ");";
                    MySqlDataReader resp_updatecaso = con_4.getdatareader(updateestatuscaso);
                    if (resp_updatecaso != null)
                    {
                        //tbEstatus.Text = texti;
                        tbEstatus_header.Text = texti;
                    }
                    resp_updatecaso.Close();
                    con_4.Cerrarconexion();
                }
            }catch(Exception Ex){
                new filelog("linea", "Error: "+Ex.Message);
            }
            
        }

        private void button37_Click(object sender, EventArgs e)
        {
            /*
             * Cargamos la imagen 
             * 
             */
            try
            {
                configuracionfiles objfile = new configuracionfiles();
                objfile.configuracionfilesinicio();
                String sFileexist = objfile.sFileupload + @"\logos_marcas\0" + sCasoId;// + @"\0" + sCasoId + ".gif";
                if (!File.Exists(sFileexist))
                {
                    System.IO.Directory.CreateDirectory(sFileexist);
                }
                //else {
                //    pbDimage.Image = null;
                //    var myFile = File.Create(sFileexist);
                //    myFile.Close();
                //}
                openFileDialog1.Filter = "Image Files (JPG,PNG,GIF)|*.JPG;*.PNG;*.GIF";
                openFileDialog1.ShowDialog();
                
                string filename = System.IO.Path.GetFileName(openFileDialog1.FileName);
                if (filename == "openFileDialog1")
                {
                    MessageBox.Show("Seleccione un archivo Válido.");
                }else{
                    //we already define our connection globaly. We are just calling the object of connection.
                    //con.Open();
                    //SqlCommand cmd = new SqlCommand("insert into doc (document)values('\\Document\\" + filename + "')", con);

                    //Path que habre por 
                    //string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
                    //cerramos el archivo para poder modificarlo
                    
                    //insertamos a imagen 

                    String sDatetime = DateTime.Now.ToString("ddMMyyyyHHmmss").ToString();

                    String sRutaInsert = objfile.sFileupload + @"\logos_marcas\0" + sCasoId + @"\0" + sCasoId + "_" + sDatetime + ".gif";
                    System.IO.File.Copy(openFileDialog1.FileName, sRutaInsert, true);
                    conect con_insert_imglogo = new conect();
                    String simglogo_insert = "INSERT INTO `imagen_logo`(`ruta`,`casoid`,`TipoSolicitudId`,`fecha`)VALUES('" + sRutaInsert.Replace("\\", "\\\\") + "','" + sCasoId + "','" + gSTipoSolicitudId + "',now());" + ";";
                    MySqlDataReader resp_insert_imglogo = con_insert_imglogo.getdatareader(simglogo_insert);
                    if (resp_insert_imglogo.RecordsAffected > 0)
                    {//quiere decir que hicimos el insert correctamente
                        obj = Image.FromFile(sRutaInsert.Replace("\\", "\\\\"));
                        pbDimage.Image = obj;
                    }
                    else
                    {//no se pudo cargar la imagen
                        MessageBox.Show("No se pudo cargar la imagen.");

                    }
                    resp_insert_imglogo.Close();
                    con_insert_imglogo.Cerrarconexion();
                    //fin insertar imagen

                    //copiamos el archivo para cambiar
                    //System.IO.File.Copy(openFileDialog1.FileName, sFileexist + @"\0" + sCasoId + ".gif", true);
                    //pbDimage.Image = Image.FromFile(sFileexist + @"\0" + sCasoId + ".gif");
                    MessageBox.Show("Imagen Cargada correctamente.");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button13_Click(object sender, EventArgs e)
        {
            // funcion para ver el detalle de un interesado
            try
            {
                int casoid = Int32.Parse(sCasoId);

                if (lvinteresados.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Debes Seleccionar un interesado");
                }
                else
                {
                    String idinteresado = lvinteresados.SelectedItems[0].SubItems[1].Text;//id interesado 
                    FInteresadoDetalle detalleinterno = new FInteresadoDetalle(idinteresado, loguin, fCapuraform, 1, casoid);
                    detalleinterno.ShowDialog();

                    actualizatablainteresado();
                }
            }
            catch (Exception E)
            {
                //escribimos en log

            }
        }
        public void actualizatablainteresado()
        {
            //
            try
            {
                lvinteresados.Items.Clear();
                conect conectinteresados = new conect();
                String kweryinteresados = "SELECT " +
                                               " casointeresado.CasoId, " +
                                               " interesado.InteresadoID, " +
                                               " interesado.InteresadoTipoPersonaSAT, " +
                                               " CONCAT(COALESCE(interesado.NombreUtilInt, ''),  ' ', " +
                                               "         COALESCE(interesado.InteresadoApPaterno, ''), ' ', " +
                                               "         COALESCE(interesado.InteresadoApMaterno, '')) AS interesadonombrecompleto, " +
                                               " CONCAT(COALESCE(direccion.DireccionCalle, ''),  ' ', " +
                                               "         COALESCE(direccion.DireccionNumExt, ''), ' ', " +
                                               "         COALESCE(direccion.DireccionNumInt, ''), ' ', " +
                                               "         COALESCE(direccion.DireccionColonia, ''),' ', " +
                                               "         COALESCE(direccion.DireccionPoblacion, ''), ' ', " +
                                               "         COALESCE(direccion.DireccionEstado, ''), ' ', " +
                                               "         COALESCE(direccion.DireccionCP, '')) AS direccioncompleta, " +
                                               " DAMELANACIONALIDAD(interesado.PaisId) AS nacionalidad, " +
                                               " interesado.PaisId, " +
                                               " tiporelacion.TipoRelacionDescrip, " +
                                               " interesado.InteresadoPoder, " +
                                               " interesado.InteresadoRGP " +
                                           " FROM " +
                                           " casointeresado" +
                                               " LEFT JOIN" +
                                               " interesado ON casointeresado.InteresadoId = interesado.InteresadoID" +
                                               " left JOIN " +
                                               " tiporelacion ON casointeresado.TipoRelacionId = tiporelacion.TipoRelacionId " +
                                               " LEFT join" +
                                               " direccion ON direccion.InteresadoID = interesado.InteresadoID" +
                                            " where casointeresado.CasoId = '" + sCasoId + "'" +
                                            " AND casointeresado.TipoSolicitudId = '" + gSTipoSolicitudId + "'" +
                                            " GROUP BY interesado.InteresadoID order by casointeresado.CasoInteresadoId;";
                //    " casointeresado, " +
                //    " interesado, " +
                //    " direccion, " +
                //    " tiporelacion " +
                //" WHERE " +
                //    " casointeresado.CasoId =  " + sCasoId +
                //        " AND interesado.InteresadoID = casointeresado.InteresadoId " +
                //        " AND tiporelacion.TipoRelacionId = casointeresado.TipoRelacionId " +
                //        " AND interesado.InteresadoID = direccion.InteresadoId " +
                //" GROUP BY interesado.InteresadoID; ";
                MySqlDataReader respuestainteresados = conectinteresados.getdatareader(kweryinteresados);
                lvinteresados.Items.Clear();

                if (respuestainteresados != null)
                {
                    String TIPOPERSONA = "";
                    int INTERESADOS = 0;
                    while (respuestainteresados.Read())
                    {
                        switch (validareader("InteresadoTipoPersonaSAT", "InteresadoID", respuestainteresados).Text)
                        {
                            case "FN":
                                TIPOPERSONA = "Física Nacional";
                                break;
                            case "FE":
                                TIPOPERSONA = "Física Extranjera";
                                break;
                            case "MN":
                                TIPOPERSONA = "Moral Nacional";
                                break;
                            case "ME":
                                TIPOPERSONA = "Moral Extranjera";
                                break;

                        }

                        ListViewItem listinteresados = new ListViewItem(validareader("TipoRelacionDescrip", "InteresadoID", respuestainteresados).Text);
                        listinteresados.SubItems.Add(validareader("InteresadoID", "InteresadoID", respuestainteresados).Text);
                        listinteresados.SubItems.Add(validareader("interesadonombrecompleto", "InteresadoID", respuestainteresados).Text);
                        listinteresados.SubItems.Add(validareader("nacionalidad", "InteresadoID", respuestainteresados).Text);
                        listinteresados.SubItems.Add(validareader("direccioncompleta", "InteresadoID", respuestainteresados).Text);
                        listinteresados.SubItems.Add(validareader("InteresadoPoder", "InteresadoID", respuestainteresados).Text);
                        listinteresados.SubItems.Add(validareader("InteresadoRGP", "InteresadoID", respuestainteresados).Text);
                        listinteresados.SubItems.Add(TIPOPERSONA);
                        int residuo = INTERESADOS % 2;
                        if (residuo == 0)
                        {
                            listinteresados.BackColor = Color.LightGray;
                        }
                        else
                        {
                            listinteresados.BackColor = Color.Azure;
                        }
                        lvinteresados.Items.Add(listinteresados);
                        lvinteresados.FullRowSelect = true;
                        INTERESADOS++;
                    }
                    respuestainteresados.Close();
                    conectinteresados.Cerrarconexion();
                }

            }
            catch (Exception E)
            {
                //escribimos en log




            }
        }

        private void button40_Click(object sender, EventArgs e)
        {
            try
            {
                BuscaInteresadoCaso busquedainteresadocaso = new BuscaInteresadoCaso(sCasoId, sTipoSolicitudId);
                busquedainteresadocaso.ShowDialog();
                actualizatablainteresado();
            }
            catch (Exception E)
            {
                //escribimos en log

            }
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            try
            {
                int casoid = Int32.Parse(sCasoId);
                int tiposolicitud = Int32.Parse(sTipoSolicitudId);
                Finteresado finteresado = new Finteresado(loguin, fCapuraform, casoid, tiposolicitud);
                finteresado.ShowDialog();
                actualizatablainteresado();
            }
            catch (Exception E)
            {
                //escribimos en log

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (lvinteresados.SelectedItems.Count == 0)
            {
                MessageBox.Show("Debes seleccionar un interesado");
            }
            else
            {
                String sInteresadoid = lvinteresados.SelectedItems[0].SubItems[1].Text;//id interesado
                String sInteresadoNomnbre = lvinteresados.SelectedItems[0].SubItems[2].Text;//id interesado


                conect conectnumcasos = new conect();
                String kwerynumcasos = "SELECT COUNT(*) FROM casointeresado WHERE casointeresado.InteresadoId = " + sInteresadoid + "  group by CasoId;";
                MySqlDataReader respuestanumcasos = conectnumcasos.getdatareader(kwerynumcasos);
                if (respuestanumcasos != null)
                {
                    int contador = 0;
                    while (respuestanumcasos.Read())
                    {

                        contador++;
                    }
                    respuestanumcasos.Close();
                    conectnumcasos.Cerrarconexion();

                    if (contador > 0)
                    {//
                        var confirmResult2 = MessageBox.Show("¿Seguro que desea ELIMINAR este interesado " + sInteresadoNomnbre + " ?", "Eliminar Contacto", MessageBoxButtons.YesNo);
                        if (confirmResult2 == DialogResult.Yes)
                        {
                            conect conectcasointeresado = new conect();
                            String kweryconect = "DELETE FROM casointeresado WHERE CasoId =  " + sCasoId + " AND InteresadoId = " + sInteresadoid + ";";
                            MySqlDataReader respuesta_deletecasointeresado = conectcasointeresado.getdatareader(kweryconect);
                            if (respuesta_deletecasointeresado == null)
                            {
                                MessageBox.Show("No se pudo eliminar casointeresado");
                            }
                            else
                            {
                                MessageBox.Show("Contacto " + sInteresadoNomnbre + " eliminado correctamente");
                                respuesta_deletecasointeresado.Close();
                                conectcasointeresado.Cerrarconexion();
                            }
                        }
                        actualizatablainteresado();

                    }
                    else
                    {
                        var confirmResult = MessageBox.Show("¿Seguro que desea ELIMINAR este interesado " + sInteresadoNomnbre + " ?", "Eliminar Contacto", MessageBoxButtons.YesNo);
                        if (confirmResult == DialogResult.Yes)
                        {
                            conect conectdeleteinteresado = new conect();
                            String kwerydeleteinteresado = "DELETE FROM interesado WHERE interesado.InteresadoID =  " + sInteresadoid + ";";
                            MySqlDataReader respuesta_delete = conectdeleteinteresado.getdatareader(kwerydeleteinteresado);

                            if (respuesta_delete == null)
                            {
                                MessageBox.Show("No se pudo eliminar este contacto");
                            }
                            else
                            {
                                conect conectcasointeresado = new conect();
                                String kweryconect = "DELETE FROM casointeresado WHERE CasoId =  " + sCasoId + " AND InteresadoId = " + sInteresadoid + ";";
                                MySqlDataReader respuesta_deletecasointeresado = conectcasointeresado.getdatareader(kweryconect);
                                if (respuesta_deletecasointeresado == null)
                                {
                                    MessageBox.Show("No se pudo eliminar casointeresado");
                                }
                                else
                                {
                                    MessageBox.Show("Contacto " + sInteresadoNomnbre + " eliminado correctamente");
                                    respuesta_deletecasointeresado.Close();
                                    conectcasointeresado.Cerrarconexion();
                                }

                                MessageBox.Show("Contacto " + sInteresadoNomnbre + " eliminado correctamente");
                                respuesta_delete.Close();
                                conectdeleteinteresado.Cerrarconexion();
                                actualizatablainteresado();

                                conect conecdeletedireccioninteresado = new conect();
                                String kwerydeleteinteresadodireccion = "DELETE FROM direccion WHERE direccion.InteresadoId = " + sInteresadoid + ";";
                                MySqlDataReader respuesta_deletedireccion = conecdeletedireccioninteresado.getdatareader(kwerydeleteinteresadodireccion);
                                if (respuesta_delete == null)
                                {
                                    MessageBox.Show("No se pudo eliminar la direccion asociada a este interesado");
                                }
                                else
                                {
                                        respuesta_deletedireccion.Close();
                                        conecdeletedireccioninteresado.Cerrarconexion();

                                    //   MessageBox.Show("Direccion asociada a este interesado borrada correctamente.");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            actualizatablainteresado();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            /*
             Debemos agregar los productos a la tabla  y luego al list view
             * 
             */

            try {
                String sDescripcion_prod = rtDescripciondelproducto.Text;
                if (cbClasemarca.SelectedItem is Nullable || sDescripcion_prod == "") {
                    MessageBox.Show("Debe seleccionar una clase y agregar una descripción");
                    return;
                }
                String sClase = (cbClasemarca.SelectedItem as ComboboxItem).Text;

                
                conect con_tcon_edocs = new conect();
                String sConsultaplazos = " INSERT INTO `casoproductos` " +
                                        " (`CasoProductosId`, " +
                                        " `CasoProductosDescripcion`, " +
                                        " `CasoProductosClase`, " +
                                        " `CasoId`, " +
                                        " `TipoSolicitudId`) " +
                                        " VALUES " +
                                        " (null , " +
                                        " '"+ sDescripcion_prod + "' , " +
                                        " '"+ sClase + "' , " +
                                        " '"+ sCasoId + "' , " +
                                        " '"+ gSTipoSolicitudId + "' ); ";

                MySqlDataReader resp_tedocs = con_tcon_edocs.getdatareader(sConsultaplazos);
                //si hay un renglon afectado entonces agregamos al list view
                if (resp_tedocs.RecordsAffected>0) {
                    //Consultamos para obtener el id del producto en casoproducto
                    conect con_caso_prod = new conect();
                    String sConsult_prodcutos = "select * from `casoproductos` order by CasoProductosId desc limit 1;";
                    MySqlDataReader res_select = con_caso_prod.getdatareader(sConsult_prodcutos);
                    res_select.Read();
                    String scasoidprod = validareader("CasoProductosId", "CasoProductosId", res_select).Text;
                    //ListViewItem itenm = new ListViewItem(scasoidprod);
                    //itenm.SubItems.Add(sClase);
                    //itenm.SubItems.Add(sDescripcion_prod);

                    //lvProductos.Items.Add(itenm);
                    dGVProductos.Rows.Add(scasoidprod, sClase, sDescripcion_prod);
                    dgVProductosheader.Rows.Add(scasoidprod, sClase, sDescripcion_prod);

                    rtDescripciondelproducto.Text = "";
                    if (tbclase.Text != "")
                    {
                        tbclase.Text = tbclase.Text + ", " + sClase;
                    }
                    else {
                        tbclase.Text = sClase;
                    }
                    res_select.Close();
                    con_caso_prod.Cerrarconexion();
                }
                resp_tedocs.Close();
                con_tcon_edocs.Cerrarconexion();
            }
            catch(Exception Ex){

            }

        }

        private void lvinteresados_DoubleClick(object sender, EventArgs e)
        {
            // funcion para ver el detalle de un interesado
            //try
            //{
            //    int casoid = Int32.Parse(sCasoId);

            //    if (lvinteresados.SelectedItems.Count == 0)
            //    {
            //        MessageBox.Show("Debes Seleccionar un interesado");
            //    }
            //    else
            //    {
            //        String idinteresado = lvinteresados.SelectedItems[0].SubItems[1].Text;//id interesado 
            //        int iTiposolicitudid = Int32.Parse(gSTipoSolicitudId);
            //        FInteresadoDetalle detalleinterno = new FInteresadoDetalle(idinteresado, loguin, fCapuraform, 1, casoid, iTiposolicitudid);
            //        detalleinterno.ShowDialog();

            //        actualizatablainteresado();
            //    }
            //}
            //catch (Exception E)
            //{
            //    //escribimos en log

            //}

            try
            {
                if (lvinteresados.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Debe Seleccionar un Titular o Inventor.");
                    return;
                }
                String sInteresadoid = lvinteresados.SelectedItems[0].SubItems[1].Text;//interesadoid
                String sRelacion = lvinteresados.SelectedItems[0].SubItems[0].Text;//relacion
                String sDomicilio = lvinteresados.SelectedItems[0].SubItems[4].Text;//domicilio
                
                FSelectdireccionint obj = new FSelectdireccionint(sInteresadoid, sCasoId, tbCasoNumero.Text, sRelacion, sDomicilio, gSTipoSolicitudId);
                obj.ShowDialog();
                actualizatablainteresado();
                //actualizainformaciondeinteresado();
            }
            catch (Exception Ex)
            {

            }


        }

        private void lvDocumentosmarcas_DoubleClick(object sender, EventArgs e)
        {
            string sRutaarchivo = "";
            try
            {
                sRutaarchivo = lvDocumentosmarcas.SelectedItems[0].SubItems[0].Text;
                sRutaarchivo = sRutaarchivo.Replace("\\\\", "\\");
                String rutaabrir = "";
                if (sRutaarchivo.Contains("J"))
                {
                    rutaabrir = sRutaarchivo;
                }
                else
                {
                    rutaabrir = "\\" + sRutaarchivo;
                }
                Process.Start(rutaabrir);
            }
            catch (Exception E)
            {
                MessageBox.Show("Conflicto al buscar el archivo en: " + sRutaarchivo);
                new filelog(loguin.sId, E.ToString());
            }
        }

        private void lvProductos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void consultaplazo_nuevos()
        {
            try
            {
                conect con_tcon_edocs = new conect();
                String sConsultaplazos = " select * from plazo_general_vista " +
                                         "where casoid = " + sCasoId +
                                         " and TipoSolicitudId = " + gSTipoSolicitudId + " " +
                                         "";
                MySqlDataReader resp_tedocs = con_tcon_edocs.getdatareader(sConsultaplazos);
                dgPlazos.Rows.Clear();
                String sPlazodistinto = "";
                bool sbandera = true;
                Color[] cColorrow = { Color.Aqua, Color.LightBlue, Color.Aqua, Color.LightBlue, Color.Magenta };
                int xz = 0;
                while (resp_tedocs.Read())
                {
                    String sidPlazo_general = objfuncionesdicss.validareader("Plazosid", "Plazosid", resp_tedocs).Text;
                    String sPlazos_detalleid = objfuncionesdicss.validareader("Plazos_detalleid", "Plazosid", resp_tedocs).Text;
                    /*String sCasoId = objfuncionesdicss.validareader("CasoId", "CasoId", resp_tedocs).Text;
                    String sTipoSolicitudId = objfuncionesdicss.validareader("TipoSolicitudId", "TipoSolicitudId", resp_tedocs).Text;*/
                    String sCapturo = objfuncionesdicss.validareader("Capturo", "Capturo", resp_tedocs).Text;
                    String sDocumento = objfuncionesdicss.validareader("Documento", "Documento", resp_tedocs).Text;
                    String sTipo_plazo_IMPI = objfuncionesdicss.validareader("Tipo_plazo_IMPI", "Tipo_plazo_IMPI", resp_tedocs).Text;
                    String sEstatus_plazo_impi = objfuncionesdicss.validareader("Estatus_plazo_impi", "Estatus_plazo_impi", resp_tedocs).Text;
                    String sFecha_notificacion_impi = objfuncionesdicss.validareader("Fecha_notificacion_impi", "Fecha_notificacion_impi", resp_tedocs).Text;
                    String sFecha_Vencimiento_regular_impi = objfuncionesdicss.validareader("Fecha_Vencimiento_regular_impi", "Fecha_Vencimiento_regular_impi", resp_tedocs).Text;
                    String sFecha_vencimiento_3m_impi = objfuncionesdicss.validareader("Fecha_vencimiento_3m_impi", "Fecha_vencimiento_3m_impi", resp_tedocs).Text;
                    String sFecha_vencimiento_4m_impi = objfuncionesdicss.validareader("Fecha_vencimiento_4m_impi", "Fecha_vencimiento_4m_impi", resp_tedocs).Text;
                    String sFecha_atendio_plazo_impi = objfuncionesdicss.validareader("Fecha_atendio_plazo_impi", "Fecha_atendio_plazo_impi", resp_tedocs).Text;
                    String sDocumento_atenio_impi = objfuncionesdicss.validareader("Documento_atenio_impi", "Fecha_atendio_plazo_impi", resp_tedocs).Text;
                    String satendio_plazoimpi = objfuncionesdicss.validareader("atendio_plazoimpi", "atendio_plazoimpi", resp_tedocs).Text;
                    String sMotivo_cancelacion_plazo_impi = objfuncionesdicss.validareader("Motivo_cancelacion_plazo_impi", "Motivo_cancelacion_plazo_impi", resp_tedocs).Text;
                    String sFecha_cancelacion_plazo_impi = objfuncionesdicss.validareader("Fecha_cancelacion_plazo_impi", "Fecha_cancelacion_plazo_impi", resp_tedocs).Text;
                    String sUsuariocancelo = objfuncionesdicss.validareader("Usuariocancelo", "Usuariocancelo", resp_tedocs).Text;
                    String sAviso_cliente = objfuncionesdicss.validareader("Aviso_cliente", "Aviso_cliente", resp_tedocs).Text;
                    String sEstatusplazo_cliente = objfuncionesdicss.validareader("Estatusplazo_cliente", "Estatusplazo_cliente", resp_tedocs).Text;
                    String sFecha_plazo_cliente = objfuncionesdicss.validareader("Fecha_plazo_cliente", "Fecha_plazo_cliente", resp_tedocs).Text;
                    String sFecha_vencimiento_cliente = objfuncionesdicss.validareader("Fecha_vencimiento_cliente", "Fecha_vencimiento_cliente", resp_tedocs).Text;
                    String sFecha_atendio_aviso_cliente = objfuncionesdicss.validareader("Fecha_atendio_aviso_cliente", "Fecha_atendio_aviso_cliente", resp_tedocs).Text;
                    String sMotivo_cancelacion_aviso_cliente = objfuncionesdicss.validareader("Motivo_cancelacion_aviso_cliente", "Motivo_cancelacion_aviso_cliente", resp_tedocs).Text;
                    String sFecha_cancelacion_aviso_cliente = objfuncionesdicss.validareader("Fecha_cancelacion_aviso_cliente", "Fecha_cancelacion_aviso_cliente", resp_tedocs).Text;
                    String sDoc_atendio = objfuncionesdicss.validareader("Doc_atendio", "Doc_atendio", resp_tedocs).Text;//Doc_atendio

                    String sMes = objfuncionesdicss.validareader("Mes", "Mes", resp_tedocs).Text;//Doc_atendio

                    DataGridViewRow dRows = (DataGridViewRow)dgPlazos.Rows[0].Clone();
                    if (sbandera)
                    {
                        sPlazodistinto = sidPlazo_general;
                        sbandera = false;
                    }
                    if (sPlazodistinto != sidPlazo_general)
                    {
                        sbandera = true;
                        xz++;
                    }
                    if (xz > 3)
                    {
                        xz = 0;
                    }

                    if (sEstatus_plazo_impi == "Con instrucciones")
                    {
                        dRows.Cells[7].Style.BackColor = Color.Magenta;
                    }
                    else
                    {
                        dRows.Cells[7].Style.BackColor = Color.LightGreen;
                    }

                    dRows.DefaultCellStyle.BackColor = cColorrow[xz];
                    if (sEstatus_plazo_impi == "Pendiente")
                    {
                        dRows.Cells[7].Style.BackColor = Color.LightCoral;
                    }
                    else
                    {
                     //   dRows.Cells[7].Style.BackColor = Color.LightGreen;
                    }

                    dRows.Cells[0].Value = sidPlazo_general;
                    dRows.Cells[1].Value = sPlazos_detalleid;
                    dRows.Cells[2].Value = sCapturo;
                    dRows.Cells[3].Value = sDocumento;
                    dRows.Cells[4].Value = sDocumento;
                    dRows.Cells[5].Value = sTipo_plazo_IMPI;
                    dRows.Cells[6].Value = sTipo_plazo_IMPI;
                    dRows.Cells[7].Value = sEstatus_plazo_impi;
                    dRows.Cells[8].Value = sMes;
                    dRows.Cells[9].Value = sValidafechavacia(sFecha_notificacion_impi);
                    dRows.Cells[10].Value = sValidafechavacia(sFecha_Vencimiento_regular_impi);
                    dRows.Cells[11].Value = sValidafechavacia(sFecha_vencimiento_3m_impi);
                    dRows.Cells[12].Value = sValidafechavacia(sFecha_vencimiento_4m_impi);
                    dRows.Cells[13].Value = sValidafechavacia(sFecha_atendio_plazo_impi);
                    dRows.Cells[14].Value = satendio_plazoimpi;
                    dRows.Cells[15].Value = sDoc_atendio;
                    dRows.Cells[16].Value = sMotivo_cancelacion_plazo_impi;
                    dRows.Cells[17].Value = sValidafechavacia(sFecha_cancelacion_plazo_impi);
                    dRows.Cells[18].Value = sUsuariocancelo;
                    /*dRows.SetValues(sidPlazo_general,
                                        sCapturo,
                                        sDocumento,
                                        sDocumento,
                                        sTipo_plazo_IMPI,
                                        sTipo_plazo_IMPI,
                                        sEstatus_plazo_impi,
                                        sValidafechavacia(sFecha_notificacion_impi),
                                        sValidafechavacia(sFecha_Vencimiento_regular_impi),
                                        sValidafechavacia(sFecha_vencimiento_3m_impi),
                                        sValidafechavacia(sFecha_vencimiento_4m_impi),
                                        sValidafechavacia(sFecha_atendio_plazo_impi),
                                        satendio_plazoimpi,
                                        sDoc_atendio,
                                        sMotivo_cancelacion_plazo_impi,
                                        sValidafechavacia(sFecha_cancelacion_plazo_impi),
                                        sUsuariocancelo);*/
                    dgPlazos.Rows.Add(dRows);


                }
                con_tcon_edocs.Cerrarconexion();
                resp_tedocs.Close();

            }
            catch (Exception Ex)
            {
                new filelog("plazos_patentes: ", "Error: " + Ex.Message);
            }
        }
        public String sValidafechavacia(String sFecha_cancelacion_aviso_cliente)
        {
            if (sFecha_cancelacion_aviso_cliente == "0000/00/00" || sFecha_cancelacion_aviso_cliente == "")
            {
                return "";
            }
            else
            {
                DateTime sFecha = DateTime.ParseExact(sFecha_cancelacion_aviso_cliente, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                return sFecha.ToString("dd/MM/yyyy");
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try {
                Fatenderplazo obj = new Fatenderplazo(sCasoId, gSTipoSolicitudId, loguin, gSCasoNumero);
                obj.ShowDialog();
                generadom(sCasoId);
            }catch(Exception Ex){
                new filelog("linea: 1595", "Error: "+Ex.Message);
            }
            
        }

        private void button45_Click(object sender, EventArgs e)
        {
            try
            {

                String sPlazosdetalleid = dgPlazos.SelectedRows[0].Cells[1].Value.ToString();
                String sFechavigencia = dgPlazos.SelectedRows[0].Cells[9].Value.ToString();
                String sTipodeplazo = dgPlazos.SelectedRows[0].Cells[6].Value.ToString();


                String sEstatusplazo = dgPlazos.SelectedRows[0].Cells[7].Value.ToString();
                //7
                if (sPlazosdetalleid != "")
                {
                    if (sEstatusplazo == "Pendiente")
                    {
                        atenderplazo atiende = new atenderplazo(sPlazosdetalleid, sFechavigencia, sTipodeplazo, loguin);
                        atiende.ShowDialog();
                        consultaplazo_nuevos();
                    }
                    else
                    {
                        MessageBox.Show("El plazo está atendido.");
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un plazo para atender.");
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Debe seleccionar un plazo para atender.");
            }
        }

        private void button46_Click(object sender, EventArgs e)
        {
            try
            {
                Fatenderplazo obj = new Fatenderplazo(sCasoId, gSTipoSolicitudId, loguin, gSCasoNumero);
                obj.ShowDialog();
                generadom(sCasoId);
            }
            catch (Exception Ex)
            {
                new filelog("linea: 1595", "Error: " + Ex.Message);
            }
        }

        private void tbEstatus_header_TextChanged(object sender, EventArgs e)
        {

        }
        public String validafechasvacias(String sFecha)
        {
            String resultado = "";
            try
            {
                if (sFecha == "0000-00-00" || sFecha == "0000/00/00" || sFecha == "00-00-0000")
                {
                    resultado = "";
                }
                else
                {
                    resultado = sFecha;
                }
            }
            catch (Exception Ex)
            {
                resultado = "";
            }
            return resultado;
        }
        public void cargamos_documentoimpi_datagridview(string sCasoiddom, string gSTipoSolicitudId)
        {
            try
            {
                dGV_docimentos_IMPI.Rows.Clear();
                lvdocumentosimpi.Items.Clear();
                //documentosimpi
                conect con2 = new conect();
                String squeryadocumentos = " SELECT " +
                                            "     documento.DocumentoCodigoBarras," +
                                            "     documento.SubTipoDocumentoId," +
                                            " documento.DocumentoId,  "+

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
                                            "         AND relaciondocumento.CasoId = " + sCasoiddom + " " +
                                            "         AND relaciondocumento.TipoSolicitudId = " + gSTipoSolicitudId + "" +
                                            "         AND SubTipoDocumento.SubTipoDocumentoId = documento.SubTipoDocumentoId;";
                MySqlDataReader resp_docimpi = con2.getdatareader(squeryadocumentos);
                int iCountpar = 0;
                while (resp_docimpi.Read())
                {
                    //lvdocumentosimpi 
                    String sMes = objfuncionesdicss.validareader("diasfiff", "casoid", resp_docimpi).Text;
                    int iMes = 0;
                    try {
                        if (sMes != "")
                        {
                            iMes = Int32.Parse(sMes) / 30;
                        }
                    }catch (Exception exs) {
                        new filelog("Error: al calcular el mes ", ""+exs.Message);
                    }
                    
                    

                    /*Validamos primero que tipo de documento vamos a mostrar*/
                    /*Puede ser solicitud, Escrito, Oficio, Titulo, Email ...*/
                    ListViewItem items = new ListViewItem("");
                    //dGV_docimentos_IMPI
                    DataGridViewRow dRows = (DataGridViewRow)dGV_docimentos_IMPI.Rows[0].Clone();
                    //DataGridViewRow dRows = (DataGridViewRow)dgPlazos.Rows[0].Clone();
                    switch (objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text)
                    {

                        case "Solicitud":
                            {
                                //dRows.Cells[0].Value =
                                //items = new ListViewItem(objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text);//link
                                //items.SubItems.Add(objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);//tipo
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text);//codgbarras
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text);//folio
                                //items.SubItems.Add("");//fechanotificacion
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text);//vencimiento
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text);//vencimiento
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text);//vencimiento
                                //items.SubItems.Add("" + iMes);//mes
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFecha", "casoid", resp_docimpi, true).Text);//Fechaselloimpi
                                //items.SubItems.Add("");//fecha escrito
                                //items.SubItems.Add(objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text);//Documento
                                //items.SubItems.Add("");//Estatus
                                //items.SubItems.Add("");//Plazo final
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text);//observacion
                                //items.SubItems.Add("");//Aviso Cliente
                                //items.SubItems.Add("");//Motivo cancelacion
                                //items.SubItems.Add("");//Usuario prorroga
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text);//Fecha Firma
                                //items.SubItems.Add(objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi).Text);//SubTipoDocumentoId

                                dRows.Cells[0].Value = objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text;
                                dRows.Cells[1].Value = objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text;
                                dRows.Cells[2].Value = objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text;
                                dRows.Cells[3].Value = objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text;
                                dRows.Cells[4].Value = "";
                                dRows.Cells[5].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[6].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[7].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[8].Value = "" + iMes;
                                dRows.Cells[9].Value = objfuncionesdicss.validareader("DocumentoFecha", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[10].Value = "";
                                dRows.Cells[11].Value = objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text;
                                dRows.Cells[12].Value = "";
                                dRows.Cells[13].Value = "";
                                dRows.Cells[14].Value = objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text;
                                dRows.Cells[15].Value = "";
                                dRows.Cells[16].Value = "";
                                dRows.Cells[17].Value = "";
                                dRows.Cells[18].Value = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[19].Value = objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi).Text;
                                

                            } break;
                        case "Escrito":
                            {
                                //items = new ListViewItem(objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text);//link
                                //items.SubItems.Add(objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);//tipo
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text);//codgbarras
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text);//folio
                                //items.SubItems.Add("");//fechanotificacion
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text);//vencimiento
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text);//vencimiento
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text);//vencimiento
                                //items.SubItems.Add("" + iMes);//mes
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFecha", "casoid", resp_docimpi, true).Text);//Fechaselloimpi
                                //items.SubItems.Add("");//fecha escrito
                                //items.SubItems.Add(objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text);//Documento
                                //items.SubItems.Add("");//Estatus
                                //items.SubItems.Add("");//Plazo final
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text);//observacion
                                //items.SubItems.Add("");//Aviso Cliente
                                //items.SubItems.Add("");//Motivo cancelacion
                                //items.SubItems.Add("");//Usuario prorroga
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text);//Fecha Firma
                                //items.SubItems.Add(objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi, true).Text);//SubTipoDocumentoId


                                dRows.Cells[0].Value = objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text;
                                dRows.Cells[1].Value = objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text;
                                dRows.Cells[2].Value = objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text;
                                dRows.Cells[3].Value = objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text;
                                dRows.Cells[4].Value = "";
                                dRows.Cells[5].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[6].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[7].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[8].Value = "" + iMes;
                                dRows.Cells[9].Value = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[10].Value = "";
                                dRows.Cells[11].Value = objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text;
                                dRows.Cells[12].Value = "";
                                dRows.Cells[13].Value = "";
                                dRows.Cells[14].Value = objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text;
                                dRows.Cells[15].Value = "";
                                dRows.Cells[16].Value = "";
                                dRows.Cells[17].Value = "";
                                dRows.Cells[18].Value = objfuncionesdicss.validareader("DocumentoFecha", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[19].Value = objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi, true).Text;
                                //Subtipodocumentoidultimoescrito = objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi).Text;

                            } break;
                        case "Oficio":
                            {
                                String sFechavigencia_ = "";
                                String sFechavigencia3meses = "";
                                String sFechavigencia4meses = "";
                                if (objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi).Text != "100")//Validamos que no sea SATISFECHO FORMA
                                {
                                    sFechavigencia_ = objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text;
                                    if (objfuncionesdicss.validareader("SubTipoDocumentoIndProrrogable", "casoid", resp_docimpi).Text == "1")
                                    {//SubTipoDocumentoIndProrrogable
                                        sFechavigencia3meses = objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text;
                                        sFechavigencia4meses = objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text;

                                    }
                                }
                                //items = new ListViewItem(objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text);//link
                                //items.SubItems.Add(objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);//tipo
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text);//codgbarras
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text);//folio
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text);//fechanotificacion
                                //tbFechacitaapago.Text = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                                //items.SubItems.Add(sFechavigencia_);//);//vencimiento
                                //items.SubItems.Add(sFechavigencia3meses);//vencimiento 3 meses
                                //items.SubItems.Add(sFechavigencia4meses);//vencimiento 4 meses
                                //items.SubItems.Add("" + iMes);//mes
                                //items.SubItems.Add("");//Fechaselloimpi
                                //items.SubItems.Add("");//fecha escrito
                                //items.SubItems.Add(objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text);//Documento
                                //items.SubItems.Add("");//Estatus
                                //items.SubItems.Add("");//Plazo final
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text);//observacion
                                //items.SubItems.Add("");//Aviso Cliente
                                //items.SubItems.Add("");//Motivo cancelacion
                                //items.SubItems.Add("");//Usuario prorroga
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFecha", "casoid", resp_docimpi, true).Text);//Fecha Firma

                                dRows.Cells[0].Value = objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text;
                                dRows.Cells[1].Value = objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text;
                                dRows.Cells[2].Value = objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text;
                                dRows.Cells[3].Value = objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text;
                                dRows.Cells[4].Value = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[5].Value = sFechavigencia_;
                                dRows.Cells[6].Value = sFechavigencia3meses;
                                dRows.Cells[7].Value = sFechavigencia4meses;
                                dRows.Cells[8].Value = "" + iMes;
                                dRows.Cells[9].Value = "";
                                dRows.Cells[10].Value = "";
                                dRows.Cells[11].Value = objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text;
                                dRows.Cells[12].Value = "";
                                dRows.Cells[13].Value = "";
                                dRows.Cells[14].Value = objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text;
                                dRows.Cells[15].Value = "";
                                dRows.Cells[16].Value = "";
                                dRows.Cells[17].Value = "";
                                dRows.Cells[18].Value = objfuncionesdicss.validareader("DocumentoFecha", "casoid", resp_docimpi, true).Text;
                            } break;
                        case "Título":
                            {
                                //items = new ListViewItem(objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text);//link
                                //items.SubItems.Add(objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);//tipo
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text);//codgbarras
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text);//folio
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text);//fechanotificacion
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text);//vencimiento
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text);//vencimiento
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text);//vencimiento
                                //items.SubItems.Add("" + iMes);//mes
                                //items.SubItems.Add("");//Fechaselloimpi
                                //items.SubItems.Add("");//fecha escrito
                                //items.SubItems.Add(objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text);//Documento
                                //items.SubItems.Add("");//Estatus
                                //items.SubItems.Add("");//Plazo final
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text);//observacion
                                //items.SubItems.Add("");//Aviso Cliente
                                //items.SubItems.Add("");//Motivo cancelacion
                                //items.SubItems.Add("");//Usuario prorroga
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text);//Fecha Firma

                                dRows.Cells[0].Value = objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text;
                                dRows.Cells[1].Value = objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text;
                                dRows.Cells[2].Value = objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text;
                                dRows.Cells[3].Value = objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text;
                                dRows.Cells[4].Value = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[5].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[6].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[7].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[8].Value = "" + iMes;
                                dRows.Cells[9].Value = "";
                                dRows.Cells[10].Value = "";
                                dRows.Cells[11].Value = objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text;
                                dRows.Cells[12].Value = "";
                                dRows.Cells[13].Value = "";
                                dRows.Cells[14].Value = objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text;
                                dRows.Cells[15].Value = "";
                                dRows.Cells[16].Value = "";
                                dRows.Cells[17].Value = "";
                                dRows.Cells[18].Value = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                            } break;
                        case "E-MAIL":
                            {
                                //items = new ListViewItem(objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text);//link
                                //items.SubItems.Add(objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);//tipo
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text);//codgbarras
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text);//folio
                                //items.SubItems.Add("");//fechanotificacion
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text);//vencimiento
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text);//vencimiento
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text);//vencimiento
                                //items.SubItems.Add("" + iMes);//mes
                                //items.SubItems.Add("");//Fechaselloimpi
                                //items.SubItems.Add("");//fecha escrito
                                //items.SubItems.Add(objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text);//Documento
                                //items.SubItems.Add("");//Estatus
                                //items.SubItems.Add("");//Plazo final
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text);//observacion
                                //items.SubItems.Add("");//Aviso Cliente
                                //items.SubItems.Add("");//Motivo cancelacion
                                //items.SubItems.Add("");//Usuario prorroga
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text);//Fecha Firma

                                dRows.Cells[0].Value = objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text;
                                dRows.Cells[1].Value = objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text;
                                dRows.Cells[2].Value = objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text;
                                dRows.Cells[3].Value = objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text;
                                dRows.Cells[4].Value = "";
                                dRows.Cells[5].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[6].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[7].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[8].Value = "" + iMes;
                                dRows.Cells[9].Value = "";
                                dRows.Cells[10].Value = "";
                                dRows.Cells[11].Value = objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text;
                                dRows.Cells[12].Value = "";
                                dRows.Cells[13].Value = "";
                                dRows.Cells[14].Value = objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text;
                                dRows.Cells[15].Value = "";
                                dRows.Cells[16].Value = "";
                                dRows.Cells[17].Value = "";
                                dRows.Cells[18].Value = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                            } break;
                        case "Robot":
                            {
                                //items = new ListViewItem(objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text);//link
                                //items.SubItems.Add(objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);//tipo
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text);//codgbarras
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text);//folio
                                //items.SubItems.Add("");//fechanotificacion
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text);//vencimiento
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text);//vencimiento
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text);//vencimiento
                                //items.SubItems.Add("" + iMes);//mes
                                //items.SubItems.Add("");//Fechaselloimpi
                                //items.SubItems.Add("");//fecha escrito
                                //items.SubItems.Add(objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text);//Documento
                                //items.SubItems.Add("");//Estatus
                                //items.SubItems.Add("");//Plazo final
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text);//observacion
                                //items.SubItems.Add("");//Aviso Cliente
                                //items.SubItems.Add("");//Motivo cancelacion
                                //items.SubItems.Add("");//Usuario prorroga
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text);//Fecha Firma

                                dRows.Cells[0].Value = objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text;
                                dRows.Cells[1].Value = objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text;
                                dRows.Cells[2].Value = objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text;
                                dRows.Cells[3].Value = objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text;
                                dRows.Cells[4].Value = "";
                                dRows.Cells[5].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[6].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[7].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[8].Value = "" + iMes;
                                dRows.Cells[9].Value = "";
                                dRows.Cells[10].Value = "";
                                dRows.Cells[11].Value = objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text;
                                dRows.Cells[12].Value = "";
                                dRows.Cells[13].Value = "";
                                dRows.Cells[14].Value = objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text;
                                dRows.Cells[15].Value = "";
                                dRows.Cells[16].Value = "";
                                dRows.Cells[17].Value = "";
                                dRows.Cells[18].Value = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                            } break;
                        default:
                            {
                                items = new ListViewItem("Tipo de documento no considerado");//link
                                items.SubItems.Add(objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);//tipo
                                dRows.Cells[0].Value = "Tipo de documento no considerado";
                                dRows.Cells[1].Value = objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text;
                            } break;
                    }/*por ahora sólo consideraremos 5 tipos de documentos mencionados arriba*/
                    String sDocumentosid = objfuncionesdicss.validareader("Documentoid", "Documentoid", resp_docimpi).Text;
                    dRows.Cells[21].Value = sDocumentosid;

                    if (iCountpar % 2 == 0)
                    {
                        items.BackColor = Color.White;
                        dRows.DefaultCellStyle.BackColor = Color.White;
                    }
                    else
                    {
                        items.BackColor = Color.LightBlue;
                        dRows.DefaultCellStyle.BackColor = Color.LightBlue;
                    }
                    iCountpar++;
                    dGV_docimentos_IMPI.Rows.Add(dRows);
                    //lvdocumentosimpi.Items.Add(items);
                }
                resp_docimpi.Close();
                con2.Cerrarconexion();
            }
            catch (Exception Ex)
            {
                new filelog("cargando documentos IMPI patentes", ":" + Ex.Message);
            }
        }
        public void cargamos_documentoimpi() {
            lvdocumentosimpi.Items.Clear();
            //documentosimpi
            conect con2 = new conect();
            String squeryadocumentos = //"select "+
                //                            " documento.DocumentoCodigoBarras, " +
                //                            " documento.SubTipoDocumentoId, " +
                //                            " DATE_FORMAT(documento.DocumentoFecha , '%d-%m-%Y') as  DocumentoFecha, " +
                //                            " documento.DocumentoFolio, " +
                //                            " DATE_FORMAT(documento.DocumentoFechaRecepcion , '%d-%m-%Y') as  DocumentoFechaRecepcion, " +
                //                            " DATE_FORMAT(documento.DocumentoFechaVencimiento, '%d-%m-%Y') AS DocumentoFechaVencimiento, " +
                //                            " DATE_FORMAT((documento.DocumentoFechaVencimiento + INTERVAL 1 MONTH), '%d-%m-%Y') AS DocumentoFechaVencimiento3meses, " +
                //                            " DATE_FORMAT((documento.DocumentoFechaVencimiento + INTERVAL 2 MONTH), '%d-%m-%Y') AS DocumentoFechaVencimiento4meses, " +
                //                            " DATEDIFF(curdate(), documento.DocumentoFechaRecepcion) as  diasfiff, " +

            //                            " DATE_FORMAT(documento.DocumentoFechaCaptura , '%d-%m-%Y') as  DocumentoFechaCaptura, " +
                //                            " DATE_FORMAT(documento.DocumentoFechaEscaneo , '%d-%m-%Y') as  DocumentoFechaEscaneo, " +
                //                            " documento.DocumentoObservacion, " +
                //                            " documento.DocumentoIdRef, " +
                //                            " documento.UsuarioId, " +
                //                            " relaciondocumento.RelacionDocumentoLink, " +
                //                            " relaciondocumento.casoid, " +
                //                            " Get_subtipodocumento(documento.SubTipoDocumentoId) as subtipodocumento, " +//Get_tipodocumento
                //                            " Get_tipodocumento(documento.SubTipoDocumentoId) as TipoDocumentoDescrip, " +//Get_tipodocumento
                //                            " Get_Usuario(documento.UsuarioId) as Nombreusuario" +
                //                            " from documento, relaciondocumento"+
                //                            " where documento.DocumentoId = relaciondocumento.DocumentoId AND relaciondocumento.CasoId = " + sCasoiddom + ";";
            " SELECT " +
            "     documento.DocumentoCodigoBarras," +
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
            "         AND relaciondocumento.CasoId = " + sCasoId + " " +
            "         AND relaciondocumento.TipoSolicitudId = " + gSTipoSolicitudId + "" +
            "         AND SubTipoDocumento.SubTipoDocumentoId = documento.SubTipoDocumentoId;";
        
            MySqlDataReader resp_docimpi = con2.getdatareader(squeryadocumentos);
            int iCountpar = 0;
            while (resp_docimpi.Read())
            {
                //lvdocumentosimpi 
                String sMes = objfuncionesdicss.validareader("diasfiff", "casoid", resp_docimpi).Text;
                int iMes = Int32.Parse(sMes) / 30;
                /*Validamos primero que tipo de documento vamos a mostrar*/
                /*Puede ser solicitud, Escrito, Oficio, Titulo, Email ...*/
                ListViewItem items = new ListViewItem("");
                switch (objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text)
                {
                    case "Solicitud":
                        {
                            items = new ListViewItem(objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text);//link
                            items.SubItems.Add(objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);//tipo
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text);//codgbarras
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text);//folio
                            items.SubItems.Add("");//fechanotificacion
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text);//vencimiento
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text);//vencimiento
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text);//vencimiento
                            items.SubItems.Add("" + iMes);//mes
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFecha", "casoid", resp_docimpi, true).Text);//Fechaselloimpi
                            items.SubItems.Add("");//fecha escrito
                            items.SubItems.Add(objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text);//Documento
                            items.SubItems.Add("");//Estatus
                            items.SubItems.Add("");//Plazo final
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text);//observacion
                            items.SubItems.Add("");//Aviso Cliente
                            items.SubItems.Add("");//Motivo cancelacion
                            items.SubItems.Add("");//Usuario prorroga
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text);//Fecha Firma
                            items.SubItems.Add(objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi).Text);//SubTipoDocumentoId
                        } break;
                    case "Escrito":
                        {
                            items = new ListViewItem(objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text);//link
                            items.SubItems.Add(objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);//tipo
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text);//codgbarras
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text);//folio
                            items.SubItems.Add("");//fechanotificacion
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text);//vencimiento
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text);//vencimiento
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text);//vencimiento
                            items.SubItems.Add("" + iMes);//mes
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFecha", "casoid", resp_docimpi, true).Text);//Fechaselloimpi
                            items.SubItems.Add("");//fecha escrito
                            items.SubItems.Add(objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text);//Documento
                            items.SubItems.Add("");//Estatus
                            items.SubItems.Add("");//Plazo final
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text);//observacion
                            items.SubItems.Add("");//Aviso Cliente
                            items.SubItems.Add("");//Motivo cancelacion
                            items.SubItems.Add("");//Usuario prorroga
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text);//Fecha Firma
                            items.SubItems.Add(objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi, true).Text);//SubTipoDocumentoId
                            //Subtipodocumentoidultimoescrito = objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi).Text;

                        } break;
                    case "Oficio":
                        {
                            String sFechavigencia_ = "";
                            String sFechavigencia3meses = "";
                            String sFechavigencia4meses = "";
                            if (objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi).Text != "100")//Validamos que no sea SATISFECHO FORMA
                            {
                                sFechavigencia_ = objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text;
                                if (objfuncionesdicss.validareader("SubTipoDocumentoIndProrrogable", "casoid", resp_docimpi).Text == "1")
                                {//SubTipoDocumentoIndProrrogable
                                    sFechavigencia3meses = objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text;
                                    sFechavigencia4meses = objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text;

                                }
                            }
                            items = new ListViewItem(objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text);//link
                            items.SubItems.Add(objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);//tipo
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text);//codgbarras
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text);//folio
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text);//fechanotificacion
                            //tbFechacitaapago.Text = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                            items.SubItems.Add(sFechavigencia_);//);//vencimiento
                            items.SubItems.Add(sFechavigencia3meses);//vencimiento 3 meses
                            items.SubItems.Add(sFechavigencia4meses);//vencimiento 4 meses
                            items.SubItems.Add("" + iMes);//mes
                            items.SubItems.Add("");//Fechaselloimpi
                            items.SubItems.Add("");//fecha escrito
                            items.SubItems.Add(objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text);//Documento
                            items.SubItems.Add("");//Estatus
                            items.SubItems.Add("");//Plazo final
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text);//observacion
                            items.SubItems.Add("");//Aviso Cliente
                            items.SubItems.Add("");//Motivo cancelacion
                            items.SubItems.Add("");//Usuario prorroga
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFecha", "casoid", resp_docimpi, true).Text);//Fecha Firma
                        } break;
                    case "Título":
                        {
                            items = new ListViewItem(objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text);//link
                            items.SubItems.Add(objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);//tipo
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text);//codgbarras
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text);//folio
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text);//fechanotificacion
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text);//vencimiento
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text);//vencimiento
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text);//vencimiento
                            items.SubItems.Add("" + iMes);//mes
                            items.SubItems.Add("");//Fechaselloimpi
                            items.SubItems.Add("");//fecha escrito
                            items.SubItems.Add(objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text);//Documento
                            items.SubItems.Add("");//Estatus
                            items.SubItems.Add("");//Plazo final
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text);//observacion
                            items.SubItems.Add("");//Aviso Cliente
                            items.SubItems.Add("");//Motivo cancelacion
                            items.SubItems.Add("");//Usuario prorroga
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text);//Fecha Firma
                        } break;
                    case "E-MAIL":
                        {
                            items = new ListViewItem(objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text);//link
                            items.SubItems.Add(objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);//tipo
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text);//codgbarras
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text);//folio
                            items.SubItems.Add("");//fechanotificacion
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text);//vencimiento
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text);//vencimiento
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text);//vencimiento
                            items.SubItems.Add("" + iMes);//mes
                            items.SubItems.Add("");//Fechaselloimpi
                            items.SubItems.Add("");//fecha escrito
                            items.SubItems.Add(objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text);//Documento
                            items.SubItems.Add("");//Estatus
                            items.SubItems.Add("");//Plazo final
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text);//observacion
                            items.SubItems.Add("");//Aviso Cliente
                            items.SubItems.Add("");//Motivo cancelacion
                            items.SubItems.Add("");//Usuario prorroga
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text);//Fecha Firma
                        } break;
                    case "Recordatorio":
                        {
                            items = new ListViewItem(objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text);//link
                            items.SubItems.Add(objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);//tipo
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text);//codgbarras
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text);//folio
                            items.SubItems.Add("");//fechanotificacion
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text);//vencimiento
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text);//vencimiento
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text);//vencimiento
                            items.SubItems.Add("" + iMes);//mes
                            items.SubItems.Add("");//Fechaselloimpi
                            items.SubItems.Add("");//fecha escrito
                            items.SubItems.Add(objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text);//Documento
                            items.SubItems.Add("");//Estatus
                            items.SubItems.Add("");//Plazo final
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text);//observacion
                            items.SubItems.Add("");//Aviso Cliente
                            items.SubItems.Add("");//Motivo cancelacion
                            items.SubItems.Add("");//Usuario prorroga
                            items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text);//Fecha Firma
                        } break;
                    default:
                        {
                            items = new ListViewItem("Tipo de documento no considerado");//link
                            items.SubItems.Add(objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);//tipo
                        } break;
                }/*por ahora sólo consideraremos 5 tipos de documentos mencionados arriba*/

                if (iCountpar % 2 == 0)
                {
                    items.BackColor = Color.White;
                }
                else
                {
                    items.BackColor = Color.LightBlue;
                }
                iCountpar++;
                lvdocumentosimpi.Items.Add(items);

            }
            resp_docimpi.Close();
            con2.Cerrarconexion();
        }

        private void lvdocumentosimpi_DoubleClick(object sender, EventArgs e)
        {
            string sRutaarchivo = "";
            try
            {
                sRutaarchivo = lvdocumentosimpi.SelectedItems[0].SubItems[0].Text;
                sRutaarchivo = sRutaarchivo.Replace("\\\\", "\\");
                String rutaabrir = "";
                if (sRutaarchivo.Contains("J"))
                {
                    rutaabrir = sRutaarchivo;
                }else{
                    rutaabrir = "\\" + sRutaarchivo;
                }
                Process.Start(rutaabrir);
            }
            catch (Exception E)
            {
                MessageBox.Show("Conflicto al buscar el archivo en: " + sRutaarchivo);
                new filelog(loguin.sId, "line:2870" + E.Message);
            }
        }

        private void dGV_docimentos_IMPI_DoubleClick(object sender, EventArgs e)
        {
            string sRutaarchivo = "";
            try
            {
                sRutaarchivo = dGV_docimentos_IMPI.SelectedRows[0].Cells[0].Value.ToString(); //lvdocumentosimpi.SelectedItems[0].SubItems[0].Text;
                sRutaarchivo = sRutaarchivo.Replace("\\\\", "\\");
                String rutaabrir = "";
                if (sRutaarchivo.Contains("J"))
                {
                    rutaabrir = sRutaarchivo;
                }
                else
                {
                    rutaabrir = "\\" + sRutaarchivo;
                }
                Process.Start(rutaabrir);
            }
            catch (Exception E)
            {
                MessageBox.Show("Conflicto al buscar el archivo en: " + sRutaarchivo);
                new filelog(loguin.sId, "line:2870" + E.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try {
                DialogResult result = MessageBox.Show("¿Seguro que desea eliminar el renglón seleccionado?", "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK))
                {
                    //Do something
                    if (dGVProductos.SelectedRows is null) {
                        MessageBox.Show("Debe seleccionar un producto para eliminar.");
                        return;
                    }
                    //if (lvProductos.SelectedItems is Nullable) {
                    //    MessageBox.Show("Debe seleccionar un producto para eliminar.");
                    //    return;
                    //}
                    //Hacemos el delete del valor 0 del item seleccionado , ya que ese valor tiene el id en la tabla casoproductos
                    String sCasoproductoid = dGVProductos.SelectedRows[0].Cells[0].Value.ToString();
                    //String sCasoproductoid = lvProductos.SelectedItems[0].Text;//casoproductoid
                    String sQuerydelete = "Delete from casoproductos where CasoProductosId = " + sCasoproductoid;
                    conect con_del = new conect();
                    MySqlDataReader resp_del = con_del.getdatareader(sQuerydelete);
                    if (resp_del.RecordsAffected >0)
                    {
                        //lvProductos.g
                        //lvProductos.Items.Remove();
                        //MessageBox.Show(lvProductos.SelectedIndices[0].ToString());
                        //lvProductos.Items.RemoveAt(lvProductos.SelectedIndices[0]);
                        cargarproductos();
                        rtDescripciondelproducto.Text = "";
                    }
                    resp_del.Close();
                    con_del.Cerrarconexion();
                }
                
            }
            catch (Exception Ex){
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            rtDescripciondelproducto.Text = "";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbTiporeferencia.Text != "" && tb_referencia.Text != "")
                {
                    String sValue = (cbTiporeferencia.SelectedItem as ComboboxItem).Value + "";
                    String sReferencia = tb_referencia.Text;
                    conect conectcasointeresado = new conect();
                    String query_insert = "INSERT INTO `referencia`(`ReferenciaId`,`CasoId`,`TipoSolicitudId`,`TipoReferenciaId`,`ReferenciaNombre`)"+
                                          "VALUES(null,'" + sCasoId + "', '"+gSTipoSolicitudId+"','" + sValue + "','" + sReferencia + "')";
                    MySqlDataReader respuesta_deletecasointeresado = conectcasointeresado.getdatareader(query_insert);
                    if (respuesta_deletecasointeresado.RecordsAffected == 1)
                    {
                        MessageBox.Show("Referencia agregada correctamente");
                        consultareferencias();
                    }
                    cbTiporeferencia.Text = "";
                    tb_referencia.Text = "";
                    respuesta_deletecasointeresado.Close();
                    conectcasointeresado.Cerrarconexion();
                }
            }
            catch (Exception eX)
            {
                new filelog("ver edocs", "Error: " + eX.Message);
                MessageBox.Show(eX.Message);
            }
        }
        public void consultareferencias()
        {
            //lvReferencias
            try
            {
                conect con_tcon_edocs = new conect();
                String sTipoEdocsquery = "select * from " +
                                        " referencia, tiporeferencia " +
                                        " where " +
                                        " referencia.TipoReferenciaId = tiporeferencia.TipoReferenciaId" +
                                        " AND referencia.CasoId =" + sCasoId +
                                        " AND TipoSolicitudId = " + gSTipoSolicitudId + ";";
                MySqlDataReader resp_tedocs = con_tcon_edocs.getdatareader(sTipoEdocsquery);
                lvReferencias.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
                lvReferencias.Items.Clear();
                int count_docelect = 0;
                while (resp_tedocs.Read())
                {
                    String sTipoReferenciaDescrip = objfuncionesdicss.validareader("TipoReferenciaDescrip", "TipoReferenciaId", resp_tedocs).Text;
                    String sReferenciaNombre = objfuncionesdicss.validareader("ReferenciaNombre", "TipoReferenciaId", resp_tedocs).Text;

                    ListViewItem itemslist = new ListViewItem(sTipoReferenciaDescrip);
                    itemslist.SubItems.Add(sReferenciaNombre);
                    itemslist.SubItems.Add(objfuncionesdicss.validareader("ReferenciaId", "ReferenciaId", resp_tedocs).Text);//ReferenciaId

                    lvReferencias.Items.Add(itemslist);
                    count_docelect++;
                }

                tb_contdocelect.Text = "" + count_docelect;
                resp_tedocs.Close();
                con_tcon_edocs.Cerrarconexion();
            }
            catch (Exception Ex)
            {
                new filelog(loguin.sId, Ex.ToString());
            }

            try
            {
                conect con_tcon_edocs = new conect();
                String sTipoEdocsquery = " select * from " +
                                        " tiporeferencia ;";
                MySqlDataReader resp_tedocs = con_tcon_edocs.getdatareader(sTipoEdocsquery);
                cbTiporeferencia.Items.Clear();
                while (resp_tedocs.Read())
                {
                    cbTiporeferencia.Items.Add(objfuncionesdicss.validareader("TipoReferenciaDescrip", "TipoReferenciaId", resp_tedocs));
                }
                resp_tedocs.Close();
                con_tcon_edocs.Cerrarconexion();
            }
            catch (Exception Ex)
            {
                new filelog(loguin.sId, Ex.ToString());
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
            try
            {
                String sReferencia = lvReferencias.SelectedItems[0].SubItems[1].Text;
                var confirmResult2 = MessageBox.Show("¿Seguro que desea ELIMINAR la referencia seleccionada?");
                if (confirmResult2 == DialogResult.OK)
                {
                    conect con_tcon_edocs = new conect();
                    String sTipoEdocsquery = "delete from referencia where ReferenciaNombre = '" + sReferencia + "' "+
                                            "and TipoSolicitudId = '" + gSTipoSolicitudId + "' " +
                                            "and CasoId = '" + sCasoId + "' ";
                    MySqlDataReader resp_tedocs = con_tcon_edocs.getdatareader(sTipoEdocsquery);
                    cbTiporeferencia.Items.Clear();
                    while (resp_tedocs.Read())
                    {
                        cbTiporeferencia.Items.Add(objfuncionesdicss.validareader("TipoReferenciaDescrip", "TipoReferenciaId", resp_tedocs));
                    }
                    resp_tedocs.Close();
                    con_tcon_edocs.Cerrarconexion();
                    consultareferencias();
                    cargarproductos();
                }
                
            }
            catch (Exception Ex)
            {
                new filelog(loguin.sId, Ex.ToString());
            }
        }

        private void lvProductos_DoubleClick(object sender, EventArgs e)
        {
            
        }

        public string sgidproducto { get; set; }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try {
                //si existe la variable sgidproducto debemos permitir guardar el cambio 
                if (sgidproducto!="")//Se está editando un producto
                {
                    conect con_tcon_edocs = new conect();
                    String sTipoEdocsquery = "UPDATE `casoproductos` SET `CasoProductosDescripcion`='" + rtDescripciondelproducto.Text + "', CasoProductosClase = '" + cbClasemarca.Text + "' WHERE `CasoProductosId`='" + sgidproducto + "';";
                    MySqlDataReader resp_tedocs = con_tcon_edocs.getdatareader(sTipoEdocsquery);
                    cbTiporeferencia.Items.Clear();
                    if (resp_tedocs.RecordsAffected > 0)
                    {
                        MessageBox.Show("Camnbio realizado correctamente.");
                        rtDescripciondelproducto.Text = "";
                        sgidproducto = "";
                        cargarproductos();
                        btModificaciones.Enabled = false;
                    }
                    else {
                        MessageBox.Show("Seleccione un producto para modificar o corregir.");
                    }
                    resp_tedocs.Close();
                    con_tcon_edocs.Cerrarconexion();
                    
                }
            }catch(Exception ex){

            }
        }

        private void label58_Click(object sender, EventArgs e)
        {

        }

        private void cbClasemarca_TextChanged(object sender, EventArgs e)
        {
            try { 
                String sClase = cbClasemarca.Text;
                cbClasemarca.SelectedIndex = int.Parse(sClase) -1;
            
            }catch(Exception ex){

            }
        }

        private void tbEstatus_header_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void fTmarcas_FormClosing(object sender, FormClosingEventArgs e)
        {
            buscarclienteform.Show();
        }

        private void tbDfecharecepcion_TextChanged(object sender, EventArgs e)
        {
            tbDfecharecepcion_plazos.Text = tbDfecharecepcion.Text;
        }

        private void tbDFechavigencia_TextChanged(object sender, EventArgs e)
        {
            tbDFechavigencia_plazos.Text = tbDFechavigencia.Text;
        }

        private void tbDSigpruebauso_TextChanged(object sender, EventArgs e)
        {
            tbDSigpruebauso_plazos.Text = tbDSigpruebauso.Text;
        }

        private void rtbDDenominacion_TextChanged(object sender, EventArgs e)
        {
            rtbDDenominacion_general.Text = rtbDDenominacion.Text;
        }

        private void rtbDDenominacion_general_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbDfecharecepcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacamposfecha(tbDfecharecepcion, e);
            //tbDfecharecepcion.Text = tbDfecharecepcion.Text.Replace("/", "-").Replace(".", "-");
        }
        public void validacamposfecha(TextBox tbElement, KeyPressEventArgs e) {
            try
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


                if (tbElement.Text.Length == 2)
                {
                    tbElement.Text = tbElement.Text + "-";
                    tbElement.SelectionStart = tbElement.Text.Length;

                }
                if (tbElement.Text.Length == 5)
                {
                    tbElement.Text = tbElement.Text + "-";
                    tbElement.SelectionStart = tbElement.Text.Length;
                }
            }
            catch (Exception Ex)
            {
                new filelog("validavaloresfecha", Ex.Message);
            }
        }

        private void tbDFechaconcesion_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacamposfecha(tbDFechaconcesion, e);
        }

        private void tbDFechavigencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacamposfecha(tbDFechavigencia, e);
        }

        private void tbDFechainiciouso_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacamposfecha(tbDFechainiciouso, e);
        }

        private void tbDSigpruebauso_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacamposfecha(tbDSigpruebauso, e);
        }

        private void tbDFechacarta_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacamposfecha(tbDFechacarta, e);
        }

        private void tbDFechacarta_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbDFechacarta);
        }

        private void tbDfecharecepcion_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbDfecharecepcion);
        }

        private void tbDFechaconcesion_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbDFechaconcesion);
        }

        private void tbDFechavigencia_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbDFechavigencia);
        }

        private void tbDFechainiciouso_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbDFechainiciouso);
        }

        private void tbDSigpruebauso_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbDSigpruebauso);
        }

        private void cbIdiomaCliente_TextChanged(object sender, EventArgs e)
        {
            //para cambiar el idioma hacemos un update

        }

        private void fTmarcas_Load(object sender, EventArgs e)
        {

        }

        private void dgview_facturas_DoubleClick(object sender, EventArgs e)
        {
            string sRutaarchivo = "";
            try
            {
                //dGV_docimentos_IMPI.Rows[0].Cells[0].Value;
                sRutaarchivo = dgview_facturas.SelectedRows[0].Cells[0].Value.ToString();
                sRutaarchivo = sRutaarchivo.Replace("\\\\", "\\");
                String rutaabrir = "";
                if (sRutaarchivo.Contains("J"))
                {
                    rutaabrir = sRutaarchivo;
                }
                else
                {
                    rutaabrir = "\\" + sRutaarchivo;
                }
                Process.Start(rutaabrir);
            }
            catch (Exception E)
            {
                MessageBox.Show("No se encuentra el archivo: " + sRutaarchivo);
                new filelog(loguin.sId, "line:6059" + E.Message);
            }
        }

        private void tblCliente_DoubleClick(object sender, EventArgs e)
        {
            //creamos una ventana en la que podamos buscar al cliente y asignarlo al caso
            buscarcliente bForm = new buscarcliente(sCasoId, gSTipoSolicitudId, tblContacto.Text, tblCliente.Text,loguin);
            if (bForm.ShowDialog() == DialogResult.OK)
            {
                tblCliente.Text = bForm.sClienteidtext;
                tblContacto.Text = bForm.sContactoidtext;
                gSclienteid = bForm.sClienteidvalue;
                rtCorreocontacto.Text = bForm.rtCorreocontacto_pass;
            }
        }

        private void label19_DoubleClick(object sender, EventArgs e)
        {
            buscarcliente bForm = new buscarcliente(sCasoId, gSTipoSolicitudId, tblContacto.Text, tblCliente.Text,loguin);
            if (bForm.ShowDialog() == DialogResult.OK)
            {
                tblCliente.Text = bForm.sClienteidtext;
                tblContacto.Text = bForm.sContactoidtext;
                gSclienteid = bForm.sClienteidvalue;
                rtCorreocontacto.Text = bForm.rtCorreocontacto_pass;
            }
        }

        private void tblContacto_DoubleClick(object sender, EventArgs e)
        {
            addContacto objnuevocontacto = new addContacto(gSclienteid, tblCliente.Text, gSContactoid, tblContacto.Text, sCasoId, gSTipoSolicitudId);
            if (objnuevocontacto.ShowDialog() == DialogResult.OK)
            {
                tblContacto.Text = objnuevocontacto.SgContactotext;
                rtCorreocontacto.Text = objnuevocontacto.SgContactocorreos;

            }
        }

        private void label21_DoubleClick(object sender, EventArgs e)
        {
            addContacto objnuevocontacto = new addContacto(gSclienteid, tblCliente.Text, gSContactoid, tblContacto.Text, sCasoId, gSTipoSolicitudId);
            if (objnuevocontacto.ShowDialog() == DialogResult.OK)
            {
                tblContacto.Text = objnuevocontacto.SgContactotext;
                rtCorreocontacto.Text = objnuevocontacto.SgContactocorreos;

            }
        }

        private void label36_DoubleClick(object sender, EventArgs e)
        {
            fResponsableupdate updateResponsable = new fResponsableupdate("1");
            if (updateResponsable.ShowDialog() == DialogResult.OK)
            {
                String value = updateResponsable.sValueResponsable;//hacemos el update con este valor y ponemos el Texto en el form
                String texti = updateResponsable.sTextoResponsable;//hacemos el update con este valor y ponemos el Texto en el form
                conect con_4 = new conect();
                String updateresponsableidcaso = "UPDATE `caso_marcas` SET `ResponsableId` = '" + value + "', `UsuarioId` = '" + value +
                                                "' WHERE (`CasoId` = '" + sCasoId + "' and TipoSolicitudId ='" + gSTipoSolicitudId + "');";
                MySqlDataReader resp_updateResp = con_4.getdatareader(updateresponsableidcaso);
                if (resp_updateResp != null)
                {
                    tblResponsable.Text = texti;
                }

                resp_updateResp.Close();
                con_4.Cerrarconexion();
            }
        }

        private void tblResponsable_DoubleClick(object sender, EventArgs e)
        {
            fResponsableupdate updateResponsable = new fResponsableupdate("1");
            if (updateResponsable.ShowDialog() == DialogResult.OK)
            {
                String value = updateResponsable.sValueResponsable;//hacemos el update con este valor y ponemos el Texto en el form
                String texti = updateResponsable.sTextoResponsable;//hacemos el update con este valor y ponemos el Texto en el form
                conect con_4 = new conect();
                String updateresponsableidcaso = "UPDATE `caso_patente` SET `ResponsableId` = '" + value + "', `UsuarioId` = '" + value +
                                                "' WHERE (`CasoId` = '" + sCasoId + "' and TipoSolicitudId ='" + gSTipoSolicitudId + "');";
                MySqlDataReader resp_updateResp = con_4.getdatareader(updateresponsableidcaso);
                if (resp_updateResp != null)
                {
                    tblResponsable.Text = texti;
                }

                resp_updateResp.Close();
                con_4.Cerrarconexion();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                //if (sIdprioridadseleccionada == "")
                //{
                    if (tbNumeroprio.Text != "" && tbfechaprio.Text != "" && cbNombrepais.Text != "" && cbTiposolprio.Text != "")
                    {
                        //validafechacorecta
                        if (objfuncionesdicss.validafechacorecta(tbfechaprio.Text, "dd-MM-yyyy", "yyyy'/'MM'/'dd") != "")
                        {
                            if (tbNumeroprio.Text.Substring(0, 3).Equals("PCT"))
                            {
                                if (tbfechaprio.Text.Substring(6, 4).Equals(tbNumeroprio.Text.Substring(6, 4)))
                                {
                                    bool bCasoprioridades = false;
                                    //obtenemos el id del pais
                                    int iIdpais = 0;
                                    for (int y = 0; y < paisesclave.Length; y++)
                                    {
                                        if (paises[y] == tbCvepais.Text)
                                        {
                                            iIdpais = y;
                                        }
                                    }
                                    int iIdtipoprioridad = 0;
                                    if ("PCT" == cbTiposolprio.Text)
                                    { iIdtipoprioridad = 1; }
                                    else { iIdtipoprioridad = 2; }


                                    //aqui hacemos el insert
                                    conect conect_prio = new conect();
                                    String sInsertprioridades = " INSERT INTO `prioridad`" +
                                                                " (`PrioridadId`," +
                                                                " `CasoId`," +
                                                                " `TipoSolicitudId`," +
                                                                " `PrioridadNumero`," +
                                                                " `PrioridadFecha`," +
                                                                " `PaisID`," +
                                                                " `TipoPrioridadId`)" +
                                                                " VALUES" +
                                                                " (null," +
                                                                " '" + sCasoId + "'," +
                                                                " '" + gSTipoSolicitudId + "'," +
                                                                " '" + tbNumeroprio.Text + "'," +
                                                                " '" + objfuncionesdicss.validafechacorecta(tbfechaprio.Text, "dd-MM-yyyy", "yyyy'-'MM'-'dd") + "' ," +// +tbfechaprio.Text + "', " +
                                                                " '" + iIdpais + "'," +
                                                                " '" + iIdtipoprioridad + "');";
                                    MySqlDataReader resp_insertprioridades = conect_prio.getdatareader(sInsertprioridades);
                                    if (resp_insertprioridades.RecordsAffected == 1)
                                    {
                                        bCasoprioridades = true;
                                        generadom(sCasoId);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Revise quye los datos estén correctos");
                                    }
                                    resp_insertprioridades.Close();
                                    conect_prio.Cerrarconexion();
                                }
                                else
                                {
                                    //MessageBox.Show(tbfechaprio.Text.Substring(6, 4) + " vs " + tbNumeroprio.Text.Substring(6, 4));
                                    MessageBox.Show("La fecha de prioridad no coincide con el número de prioridad");
                                }
                            }
                            else
                            {
                                bool bCasoprioridades = false;
                                //obtenemos el id del pais
                                int iIdpais = 0;
                                for (int y = 0; y < paisesclave.Length; y++)
                                {
                                    if (paises[y] == tbCvepais.Text)
                                    {
                                        iIdpais = y;
                                    }
                                }
                                int iIdtipoprioridad = 0;
                                if ("PCT" == cbTiposolprio.Text)
                                { iIdtipoprioridad = 1; }
                                else { iIdtipoprioridad = 2; }


                                //aqui hacemos el insert
                                conect conect_prio = new conect();
                                String sInsertprioridades = " INSERT INTO `prioridad`" +
                                                            " (`PrioridadId`," +
                                                            " `CasoId`," +
                                                            " `TipoSolicitudId`," +
                                                            " `PrioridadNumero`," +
                                                            " `PrioridadFecha`," +
                                                            " `PaisID`," +
                                                            " `TipoPrioridadId`)" +
                                                            " VALUES" +
                                                            " (null," +
                                                            " '" + sCasoId + "'," +
                                                            " '" + gSTipoSolicitudId + "'," +
                                                            " '" + tbNumeroprio.Text + "'," +
                                                            " '" + objfuncionesdicss.validafechacorecta(tbfechaprio.Text, "dd-MM-yyyy", "yyyy'-'MM'-'dd") + "' ," +// +tbfechaprio.Text + "', " +
                                                            " '" + iIdpais + "'," +
                                                            " '" + iIdtipoprioridad + "');";
                                MySqlDataReader resp_insertprioridades = conect_prio.getdatareader(sInsertprioridades);
                                if (resp_insertprioridades.RecordsAffected == 1)
                                {
                                    bCasoprioridades = true;
                                    generadom(sCasoId);
                                }
                                else
                                {
                                    MessageBox.Show("Revise quye los datos estén correctos");
                                }
                                resp_insertprioridades.Close();
                                conect_prio.Cerrarconexion();
                                /*
                                ListViewItem lPrioridades = new ListViewItem(tbNumeroprio.Text);
                                lPrioridades.SubItems.Add(tbfechaprio.Text);
                                lPrioridades.SubItems.Add(cbNombrepais.Text);
                                lPrioridades.SubItems.Add(cbTiposolprio.Text);
                                lvPrioridades.Items.Add(lPrioridades);

                                tbNumeroprio.Text = "";
                                tbfechaprio.Text = "";
                                cbNombrepais.Text = "";
                                cbTiposolprio.Text = "";
                                tbCvepais.Text = "";*/
                                //MessageBox.Show(tbfechaprio.Text.Substring(0, 3));
                            }
                            button4_Click_1(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("La fecha de la prioridad que intenta agregar es incorrecta.");
                            tbfechaprio.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe completar los campos para agregar una prioridad");
                    }
                // }
                // else
                // {
                //     MessageBox.Show("Debe limpiar antes de agregar un nuevo registro.");
                // }
            }
            catch (Exception E)
            {
                new filelog(loguin.sId, E.ToString());
            }
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            try
            {
                if (sIdprioridadseleccionada != "")
                {

                    String sNumeroprioridadselected = tbNumeroprio.Text;
                    String sFechaprioridad = tbfechaprio.Text;
                    String sCalvepaisselected = tbCvepais.Text;
                    String sNombrepaisselected = cbNombrepais.Text;
                    String sTiposolselected = cbTiposolprio.Text;
                    String sPaisoid = "";
                    String sTiprioridadid = "";
                    if (objfuncionesdicss.validafechacorecta(sFechaprioridad, "dd-MM-yyyy", "yyyy'/'MM'/'dd") == "")
                    {
                        MessageBox.Show("La fecha de la prioridad es incorrecta!");
                        return;
                    }
                    conect con_2 = new conect();
                    String sGetpais = "select * from pais where PaisClave = '" + sCalvepaisselected + "'";
                    MySqlDataReader resp_getpais = con_2.getdatareader(sGetpais);
                    int iIndiceids = 0;
                    while (resp_getpais.Read())
                    {
                        sPaisoid = objfuncionesdicss.validareader("PaisId", "PaisId", resp_getpais).Text;
                    }
                    resp_getpais.Close();
                    con_2.Cerrarconexion();

                    //buscamos el tipoprioridadid
                    conect con_3 = new conect();
                    String sGetipoprioridadid = "select * from tipoprioridad where TipoPrioridadDescripcion = '" + sTiposolselected + "';";
                    MySqlDataReader resp_getipoprioridadid = con_3.getdatareader(sGetipoprioridadid);

                    while (resp_getipoprioridadid.Read())
                    {
                        sTiprioridadid = objfuncionesdicss.validareader("tipoprioridadid", "tipoprioridadid", resp_getipoprioridadid).Text;
                    }
                    resp_getipoprioridadid.Close();
                    con_3.Cerrarconexion();

                    //modificacion de prioridades
                    conect con_4 = new conect();
                    String sUpdateprioridad = "UPDATE `prioridad`" +
                                                "SET" +
                                                "`PrioridadNumero` = '" + sNumeroprioridadselected + "'," +
                                                "`PaisID` =  '" + sPaisoid + "'," +
                                                "`PrioridadFecha` = '" + objfuncionesdicss.validafechacorecta(sFechaprioridad, "dd-MM-yyyy", "yyyy'/'MM'/'dd") + "'," +
                                                "`TipoPrioridadId` = '" + sTiprioridadid + "'" +
                                                "WHERE `PrioridadId` = '" + sIdprioridadseleccionada + "'";
                    MySqlDataReader resp_updatecaso = con_4.getdatareader(sUpdateprioridad);
                    if (resp_updatecaso != null)
                    {
                        //ListViewItem itemview = new ListViewItem(sIdprioridadseleccionada);
                        //itemview.SubItems.Add(sNumeroprioridadselected);
                        //itemview.SubItems.Add(sFechaprioridad);
                        //itemview.SubItems.Add(sCalvepaisselected);
                        //itemview.SubItems.Add(sNombrepaisselected);
                        //itemview.SubItems.Add(sTiposolselected);
                        //lvPrioridades.Items.Add(itemview);
                        //sIdprioridadseleccionada = "";
                        generadom(sCasoId);
                        tbNumeroprio.Text = "";
                        tbfechaprio.Text = "";
                        tbCvepais.Text = "";
                        cbNombrepais.Text = "";
                        cbTiposolprio.Text = "";
                        button13.Enabled = false;
                    }
                    resp_updatecaso.Close();
                    con_4.Cerrarconexion();

                }
                else
                {
                    MessageBox.Show("Debe seleccionar una prioridad de la lista.");
                }
            }
            catch (Exception E)
            {
                new filelog(loguin.sId, E.ToString());
            }
        }

        private void bEliminarprioridades_Click(object sender, EventArgs e)
        {
            try
            {
                sIdprioridadseleccionada = lvPrioridades.SelectedItems[0].SubItems[0].Text;
                if (sIdprioridadseleccionada != "")
                {
                    String sNumeroprioridadselected = lvPrioridades.SelectedItems[0].SubItems[1].Text;
                    String sFechaprioridad = lvPrioridades.SelectedItems[0].SubItems[2].Text;
                    String sCalvepaisselected = lvPrioridades.SelectedItems[0].SubItems[3].Text;
                    String sNombrepaisselected = lvPrioridades.SelectedItems[0].SubItems[4].Text;
                    String sTiposolselected = lvPrioridades.SelectedItems[0].SubItems[5].Text;
                    String sPaisoid = "";
                    conect con_2 = new conect();
                    String sGetpais = "delete from prioridad where prioridadid = '" + sIdprioridadseleccionada + "'";
                    var confirmResult = MessageBox.Show("¿Seguro que desea borrar la prioridad Número:" + sNumeroprioridadselected + "?", "Confirmación para eliminar Prioridad",
                                         MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        MySqlDataReader resp_getpais = con_2.getdatareader(sGetpais);
                        generadom(sCasoId);
                        resp_getpais.Close();
                        con_2.Cerrarconexion();
                    }
                    else
                    {
                        sIdprioridadseleccionada = "";
                    }

                }
            }
            catch (Exception E)
            {
                new filelog(loguin.sId, E.ToString());
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            tbNumeroprio.Text = "";
            tbfechaprio.Text = "";
            tbCvepais.Text = "";
            cbNombrepais.Text = "";
            cbTiposolprio.Text = "";
            sIdprioridadseleccionada = "";
            button13.Enabled = false;
        }

        private void cbNombrepais_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int iValuepais = Convert.ToInt32((cbNombrepais.SelectedItem as ComboboxItem).Value.ToString());
                tbCvepais.Text = paises[iValuepais];
            }
            catch (Exception E)
            {
                new filelog(loguin.sId, E.ToString());
            }
        }

        private void lvPrioridades_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                button13.Enabled = true;
                sIdprioridadseleccionada = lvPrioridades.SelectedItems[0].SubItems[0].Text;
                String sNumeroprioridadselected = lvPrioridades.SelectedItems[0].SubItems[1].Text;
                String sFechaprioridad = lvPrioridades.SelectedItems[0].SubItems[2].Text;
                String sCalvepaisselected = lvPrioridades.SelectedItems[0].SubItems[3].Text;
                String sNombrepaisselected = lvPrioridades.SelectedItems[0].SubItems[4].Text;
                String sTiposolselected = lvPrioridades.SelectedItems[0].SubItems[5].Text;

                tbNumeroprio.Text = sNumeroprioridadselected;
                tbfechaprio.Text = objfuncionesdicss.validafechacorecta(sFechaprioridad, "dd/MM/yyyy", "dd'-'MM'-'yyyy");
                tbCvepais.Text = sCalvepaisselected;
                cbNombrepais.Text = sNombrepaisselected;
                cbTiposolprio.Text = sTiposolselected;

                for (int y = 0; y < lvPrioridades.Items.Count; y++)
                {//cada vuelta es un renglon

                    Console.WriteLine(lvPrioridades.Items[y].SubItems[0].Text);//cada subitem es la columna
                    Console.WriteLine(lvPrioridades.Items[y].SubItems[2].Text);
                    Console.WriteLine(lvPrioridades.Items[y].SubItems[3].Text);
                    Console.WriteLine(lvPrioridades.Items[y].SubItems[4].Text);
                    Console.WriteLine(lvPrioridades.Items[y].SubItems[5].Text);
                    if (lvPrioridades.Items[y].SubItems[5].Text == "PCT")
                    {
                        Console.WriteLine("Ya hay un PCT");
                    }
                }

            }
            catch (Exception E)
            {
                new filelog(loguin.sId, E.ToString());
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            /*
             * Generamos los documentos tipos escritos
             */
            try {
                String sIdidiomaescritos = "";
                if ((cbidiomaescrito.SelectedItem as ComboboxItem) != null)
                { //idioma seleccionado para la carta
                    sIdidiomaescritos = (cbidiomaescrito.SelectedItem as ComboboxItem).Value.ToString();
                }
                else
                {
                    MessageBox.Show("Seleccione el idioma del escrito.");
                    return;
                }


                if (cbDocEscritos.SelectedItem == null)
                {
                    return;
                }

                //cargamos los datos

                if (cbOficiosparaescritos.SelectedItem != null)
                {
                    String sDocumentoid = (cbOficiosparaescritos.SelectedItem as ComboboxItem).Value.ToString();
                    objmarcaactualview = new view_caso_marcas(tbCasoid.Text, gSTipoSolicitudId, sIdidiomaescritos, sDocumentoid);
                }
                else
                {
                    objmarcaactualview = new view_caso_marcas(tbCasoid.Text, gSTipoSolicitudId, sIdidiomaescritos);
                }

                generacarta objcarta = null;
                String valuecob = (cbDocEscritos.SelectedItem as ComboboxItem).Value.ToString();
                String sCartanombreESPfile = "";
                String sCartanombreENfile = "";
                conect con_carta = new conect();
                String sQuery_carta = " SELECT  " +
                                        "     * " +
                                        " FROM " +
                                        "     subtipodocumento " +
                                        " WHERE " +
                                        " SubtipodocumentoId = " + valuecob;
                MySqlDataReader respuesta_carta = con_carta.getdatareader(sQuery_carta);
                while (respuesta_carta.Read())
                {
                    sCartanombreESPfile = validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoTemplateEspanol", respuesta_carta).Text;
                    sCartanombreENfile = validareader("SubTipoDocumentoTemplateIngles", "SubTipoDocumentoTemplateIngles", respuesta_carta).Text;
                    //Aquí podemos validar el idioma y si existe la plantilla
                    //generacarta objcarta = new generacarta(sCartanombreESPfile, valuecob, objmarcaactual);
                    if (sIdidiomaescritos == "1")
                    {//Ingles
                        if (sCartanombreENfile == "")
                        {
                            MessageBox.Show("No existe la carta para éste idioma (EN)");
                            return;
                        }
                        String[] sValorusuario = new string[2];
                        sValorusuario[0] = "idUsuariosistema";
                        sValorusuario[1] = sUsuarioparadocs;
                        objmarcaactualview.sValorescampos.Add(sValorusuario);
                        objcarta = new generacarta(sCartanombreENfile, valuecob, objmarcaactualview);
                    }
                    else
                    {
                        if (sIdidiomaescritos == "2")
                        {
                            if (sCartanombreESPfile == "")
                            {
                                MessageBox.Show("No existe la carta para éste idioma (ES)");
                                return;
                            }
                            String[] sValorusuario = new string[2];
                            sValorusuario[0] = "idUsuariosistema";
                            sValorusuario[1] = sUsuarioparadocs;
                            objmarcaactualview.sValorescampos.Add(sValorusuario);
                            objcarta = new generacarta(sCartanombreESPfile, valuecob, objmarcaactualview);
                        }
                        else
                        {
                            MessageBox.Show("Debe asignar un Idioma al cliente");
                        }

                    }
                    if (objcarta.sMensajeerror!="") {
                        MessageBox.Show(objcarta.sMensajeerror);
                    }

                }
                respuesta_carta.Close();
                con_carta.Cerrarconexion();
            }
            catch (Exception ex)
            {
            }
        }

        private void lv_documentelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                if (cb_tipodocelect.Text.Trim() == "")
                {
                    MessageBox.Show("Debe seleccionar un tipo de documento antes de agregar.");
                    return;
                }

                if (tb_descripdocelec.Text.Trim() == "")
                {
                    MessageBox.Show("Debe agregar una descripción antes de agregar el documento.");
                    return;
                }
                var fileContent = string.Empty;
                var filePath = string.Empty;
                String sNamefile = "";
                String[] aName;

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    String ruta_documentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    openFileDialog.InitialDirectory = ruta_documentos;
                    //openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    //openFileDialog.FilterIndex = 2;
                    //openFileDialog.RestoreDirectory = true;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filePath = openFileDialog.FileName;
                        aName = filePath.Split('\\');
                        sNamefile = aName[aName.Length - 1];
                        //Read the contents of the file into a stream
                        var fileStream = openFileDialog.OpenFile();
                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            fileContent = reader.ReadToEnd();
                        }
                    }
                }
                configuracionfiles obj = new configuracionfiles();
                obj.configuracionfilesinicio();

                DialogResult result1 = MessageBox.Show("Se agregará el documento: " + filePath + " \n Tipo:" + cb_tipodocelect.Text + "\n Descripción:" + tb_descripdocelec.Text, "Confirmación.", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    //crear carpeta de cada caso 
                    //sCarpetadocumentos
                    string textoNormalizado = sNamefile.Normalize(NormalizationForm.FormD);
                    //coincide todo lo que no sean letras y números ascii o espacio
                    //y lo reemplazamos por una cadena vacía.Regex reg = new Regex("[^a-zA-Z0-9 ]");
                    Regex reg = new Regex("[^a-zA-Z0-9 ]");
                    string textoSinAcentos = reg.Replace(textoNormalizado, "");

                    String[] sfilesss = obj.sFileupload.Split('\\');
                    string ruta = "\\\\" + sfilesss[2] + "\\" + sfilesss[3] + "\\" + sCarpetadocumentos + "\\" + gSCasoNumero;// + "\\edocs";

                    //string ruta = obj.sFileupload + "\\" + sCarpetadocumentos + "\\" + gSCasoNumero + "\\edocs";
                    String sfilePath_2 = @ruta + "\\" + gSCasoNumero + " " + tbExpediente.Text.Replace("/", "") + " " + sNamefile;
                    tb_filename.Text = sfilePath_2;
                    // To copy a folder's contents to a new location:
                    // Create a new target folder, if necessary.
                    if (!System.IO.Directory.Exists(ruta))
                    {
                        System.IO.Directory.CreateDirectory(ruta);
                    }

                    //copiamos el archivo a las carpetas:
                    System.IO.File.Copy(filePath, sfilePath_2, true);
                    //MessageBox.Show("Se agregó correctamente el documento.");
                    DateTime date = DateTime.Now;
                    string dateFormatted = date.ToString("yyyy-MM-dd HH:mm:ss");
                    conect con_insertdocelect = new conect();
                    String sQueryinsert = " INSERT INTO `documentoelectronico` " +
                                " (`DocumentoElectronicoId`, " +
                                " `DocumentoElectronicoDate`, " +
                                " `DocumentoElectronicoDescrip`, " +
                                " `CasoId`, " +
                                " `TipoSolicitudId`, " +
                                " `UsuarioId`, " +
                                " `TipoDocumentoElectronicoId`, " +
                                " `DocumentoElectronicoFilename` " +
                                " ) " +
                                " VALUES " +
                                " (NULL, " +
                                " '" + dateFormatted + "', " +
                                " '" + tb_descripdocelec.Text.Trim() + "', " +
                                " '" +sCasoId + "', " +
                                " '" +sTipoSolicitudId + "', " +
                                " '" + loguin.sId + "', " +
                                " '" + (cb_tipodocelect.SelectedItem as ComboboxItem).Value + "', " +
                                " '" + @sfilePath_2.Replace("\\", "\\\\") + "'); ";
                    MySqlDataReader respuestastringinteresados = con_insertdocelect.getdatareader(sQueryinsert);
                    if (respuestastringinteresados != null)
                    {
                        respuestastringinteresados.Close();
                        if (respuestastringinteresados.RecordsAffected == 1)
                        {
                            cb_tipodocelect.Text = "";
                            tb_filename.Text = "";
                            tb_descripdocelec.Text = "";
                            MessageBox.Show("El Documento se agregó correctamente.");
                            consultamosdocumentoselectronicos();
                        }
                        else
                        {
                            MessageBox.Show("Error al intentar agregar el documento, verifique la ruta ó el nombre del archivo."+ sQueryinsert);
                        }
                    }
                    else {
                        MessageBox.Show("Error al intentar agregar el documento, verifique la ruta ó el nombre del archivo." + sQueryinsert);
                        new filelog(loguin.sId, " Linea 4043: Error:"+ sQueryinsert);
                    }
                    con_insertdocelect.Cerrarconexion();
                }
            }catch (Exception E){
                new filelog(loguin.sId, E.ToString());
            }
        }

        private void consultamosdocumentoselectronicos()
        {
            //consultamos los edocss existentes para éste caso
            try
            {
                conect con_tcon_edocs = new conect();
                String sTipoEdocsquery = " SELECT  " +
                                        " usuario.UsuarioNombre, " +
                                        " usuario.UsuarioPaterno, " +
                                        " documentoelectronico.*, " +
                                        " tipodocumentoelectronico.TipoDocumentoElectronicoDescrip " +
                                        " FROM " +
                                        " documentoelectronico, " +
                                        " tipodocumentoelectronico, " +
                                        " usuario " +
                                        " where  " +
                                        " documentoelectronico.TipoDocumentoElectronicoId = tipodocumentoelectronico.TipoDocumentoElectronicoId " +
                                        " AND usuario.UsuarioId = documentoelectronico.UsuarioId " +
                                        " AND documentoelectronico.CasoId = " + sCasoId + " and Tiposolicitudid = '"+sTipoSolicitudId+"';";
                MySqlDataReader resp_tedocs = con_tcon_edocs.getdatareader(sTipoEdocsquery);
                //lv_documentelect.Items.Clear();
                dgDocumentoselectronicos.Rows.Clear();
                int count_docelect = 0;
                while (resp_tedocs.Read())
                {
                    //DocumentoElectronicoId
                    //DocumentoElectronicoDate
                    //DocumentoElectronicoDescrip
                    //CasoId
                    //ClienteId
                    //UsuarioId
                    //TipoDocumentoElectronicoId
                    //DocumentoElectronicoFilename
                    //InteresadoId
                    //TipoDocumentoElectronicoDescrip
                    String sDocumentoElectronicoId = objfuncionesdicss.validareader("DocumentoElectronicoId", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sDocumentoElectronicoDate = objfuncionesdicss.validareader("DocumentoElectronicoDate", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sDocumentoElectronicoDescrip = objfuncionesdicss.validareader("DocumentoElectronicoDescrip", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sCasoId_doc = objfuncionesdicss.validareader("CasoId", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sClienteId = objfuncionesdicss.validareader("ClienteId", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sUsuarioId = objfuncionesdicss.validareader("UsuarioId", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sTipoDocumentoElectronicoId = objfuncionesdicss.validareader("TipoDocumentoElectronicoId", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sDocumentoElectronicoFilename = @objfuncionesdicss.validareader("DocumentoElectronicoFilename", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sInteresadoId = objfuncionesdicss.validareader("InteresadoId", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sTipoDocumentoElectronicoDescrip = objfuncionesdicss.validareader("TipoDocumentoElectronicoDescrip", "DocumentoElectronicoId", resp_tedocs).Text;

                    String sUsuarioNombre = objfuncionesdicss.validareader("UsuarioNombre", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sUsuarioPaterno = objfuncionesdicss.validareader("UsuarioPaterno", "DocumentoElectronicoId", resp_tedocs).Text;


                    //ListViewItem itemslist = new ListViewItem(sDocumentoElectronicoDate);
                    //itemslist.SubItems.Add(sUsuarioNombre + " " + sUsuarioPaterno);
                    //itemslist.SubItems.Add(sTipoDocumentoElectronicoDescrip);
                    //itemslist.SubItems.Add(sDocumentoElectronicoFilename);
                    //itemslist.SubItems.Add(sDocumentoElectronicoDescrip);
                    dgDocumentoselectronicos.Rows.Add(sDocumentoElectronicoId,
                                                    sDocumentoElectronicoDate,
                                                    sUsuarioNombre + " " + sUsuarioPaterno,
                                                    sTipoDocumentoElectronicoDescrip,
                                                    sDocumentoElectronicoDescrip,
                                                    sDocumentoElectronicoFilename);


                    //lv_documentelect.Items.Add(itemslist);
                    count_docelect++;
                }

                tb_contdocelect.Text = "" + count_docelect;
                tb_contdocelect_.Text = "" + count_docelect;
                resp_tedocs.Close();
                con_tcon_edocs.Cerrarconexion();
            }
            catch (Exception Ex)
            {
                new filelog(loguin.sId, Ex.ToString());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cb_tipodocelect.Text = "";
            tb_filename.Text = "";
            tb_descripdocelec.Text = "";
        }

        private void tabcFDatosgenerales_Resize(object sender, EventArgs e)
        {
            //dgPlazos.Location = new Point(this.dgPlazos.Location.X, 75);
            //dgPlazos.Size = new Size(dgPlazos.Width, this.Height - 710);//390+320 = 710

            //dGV_docimentos_IMPI.Location = new Point(this.dGV_docimentos_IMPI.Location.X, 75);
            //dGV_docimentos_IMPI.Size = new Size(dGV_docimentos_IMPI.Width, this.Height - 765); //445 +320 = 765

            //dgview_facturas.Location = new Point(this.dgview_facturas.Location.X, 75);
            //dgview_facturas.Size = new Size(dgview_facturas.Width, this.Height - 710);//390+320 = 710
        }



        private void fTmarcas_Resize(object sender, EventArgs e)
        {
            //tabcFDatosgenerales.Location = new Point(this.tabcFDatosgenerales.Location.X, 75);
            //tabcFDatosgenerales.Size = new Size(tabcFDatosgenerales.Width, this.Height - 600);//280 + 320 = 600 osea sumamos 320
        }

        private void label19_MouseLeave(object sender, EventArgs e)
        {
            label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(253)))), ((int)(((byte)(149)))));//255, 253, 149
        }

        private void label19_MouseMove(object sender, MouseEventArgs e)
        {
            label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(214)))), ((int)(((byte)(02)))));//218, 214, 0
        }   

        private void label21_MouseLeave(object sender, EventArgs e)
        {
            label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(253)))), ((int)(((byte)(149)))));//255, 253, 149
        }

        private void label21_MouseMove(object sender, MouseEventArgs e)
        {
            label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(214)))), ((int)(((byte)(02)))));//218, 214, 0
        }

        private void label36_MouseLeave(object sender, EventArgs e)
        {
            label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(253)))), ((int)(((byte)(149)))));//255, 253, 149
        }

        private void label36_MouseMove(object sender, MouseEventArgs e)
        {
            label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(214)))), ((int)(((byte)(02)))));//218, 214, 0
        }

        private void label61_MouseLeave(object sender, EventArgs e)
        {
            label61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(253)))), ((int)(((byte)(149)))));//255, 253, 149
        }

        private void label61_MouseMove(object sender, MouseEventArgs e)
        {
            label61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(214)))), ((int)(((byte)(02)))));//218, 214, 0
        }

        private void label27_DoubleClick(object sender, EventArgs e)
        {
            updatePais updateestatus = new updatePais();
            if (updateestatus.ShowDialog() == DialogResult.OK)
            {
                String value = updateestatus.sValueestatus;//hacemos el update con este valor y ponemos el Texto en el form
                String texti = updateestatus.sTextoestatus;//hacemos el update con este valor y ponemos el Texto en el form
                conect con_4 = new conect();
                String updateestatuscaso = "UPDATE `caso_marcas` SET `PaisId` = '" + value + "' WHERE (`CasoId` = '" + sCasoId + "');";
                MySqlDataReader resp_updatecaso = con_4.getdatareader(updateestatuscaso);
                if (resp_updatecaso != null)
                {
                    tbl_pais.Text = texti;
                }

            }
        }

        private void label27_MouseLeave(object sender, EventArgs e)
        {
            label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(253)))), ((int)(((byte)(149)))));//255, 253, 149
        }

        private void label27_MouseMove(object sender, MouseEventArgs e)
        {
            label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(214)))), ((int)(((byte)(02)))));//218, 214, 0
        }

        private void tblRefencia_TextChanged(object sender, EventArgs e)
        {

        }

        private void lv_documentelect_DoubleClick(object sender, EventArgs e)
        {
            //String sRuta = "";
            //try
            //{
            //    if (lv_documentelect.SelectedItems != null)
            //    {
            //        sRuta = lv_documentelect.SelectedItems[0].SubItems[3].Text;
            //        Process.Start(sRuta);
            //        MessageBox.Show("Ruta: " + sRuta);
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    new filelog("ver edocs", "Error: " + Ex.Message);
            //    MessageBox.Show(Ex.Message + " " + sRuta);
            //}
        }

        private void lvinteresados_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbFechaRegistrointernacional_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechaRegistrointernacional);
        }

        private void tbFechaRegistrointernacional_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacamposfecha(tbFechaRegistrointernacional, e);
        }

        private void dGV_docimentos_IMPI_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public bool validaversion(String sVersion)
        {

            bool breinicia = false;
            return breinicia;
         
        }

        private void bAgregarplazo_Click(object sender, EventArgs e)
        {
            //AQUÍ abriremos una ventana para capturar los datos del plazo nuevo fecha estatus plazo  relacionado si es que existe etc ..
            try
            {
                agregaplazo obj = new agregaplazo(sCasoId, gSTipoSolicitudId, tbCasoNumero.Text, loguin.sId, 2);//loguin.sId es el usuario de la sesion
                if (obj.ShowDialog() == DialogResult.OK)
                {
                    generadom(sCasoId);
                }
            }
            catch (Exception exs)
            {
                new filelog("WARNING casos king: ", " excepcion al agregar plazo manual " + exs.Message);
            }
        }
        private void instrucciones(object sender, EventArgs e)
        {
            /*Seleccionamos un plazo y se elimina, sólo para el administrador*/
            try
            {
                String sPlazodetalleid = dgPlazos.SelectedRows[0].Cells[1].Value.ToString();
                String sPlazoid = dgPlazos.SelectedRows[0].Cells[0].Value.ToString();
                if (dgPlazos.SelectedRows == null)
                {
                    MessageBox.Show("Debe seleccionar un plazo para eliminar");
                    return;
                }
                DialogResult result = MessageBox.Show("¿Seguro que desea cambiar el estatus del plazo " + sPlazoid + " con Descripción: \"" + dgPlazos.SelectedRows[0].Cells[6].Value.ToString() + "\" ?", "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK))
                {
                    conect con_ = new conect();
                    String sQuerydeleteplazo = "update plazos_detalle set Estatus_plazoid=11 where plazos_detalleid = " + sPlazodetalleid;
                    MySqlDataReader resp_dPlazos = con_.getdatareader(sQuerydeleteplazo);
                    if (resp_dPlazos != null)
                    {
                        resp_dPlazos.Close();
                        MessageBox.Show("Plazo Modificado");
                        consultaplazo_nuevos();
                    }
                    con_.Cerrarconexion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe seleccionar un registro");
                new filelog("Error eliminar plazos", " Error: " + ex.StackTrace);

            }
        }
        private void button16_Click(object sender, EventArgs e)
        {
            /*Seleccionamos un plazo y se elimina, sólo para el administrador*/
            try
            {
                String sPlazodetalleid = dgPlazos.SelectedRows[0].Cells[1].Value.ToString();
                String sPlazoid = dgPlazos.SelectedRows[0].Cells[0].Value.ToString();
                if (dgPlazos.SelectedRows == null)
                {
                    MessageBox.Show("Debe seleccionar un plazo para eliminar");
                    return;
                }
                DialogResult result = MessageBox.Show("¿Seguro que desea eliminar el plazo " + sPlazoid + " con Descripción: \"" + dgPlazos.SelectedRows[0].Cells[6].Value.ToString() + "\" ?", "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK))
                {
                    conect con_ = new conect();
                    String sQuerydeleteplazo = "delete from plazos_detalle where plazos_detalleid = " + sPlazodetalleid;
                    MySqlDataReader resp_dPlazos = con_.getdatareader(sQuerydeleteplazo);
                    if (resp_dPlazos != null)
                    {
                        resp_dPlazos.Close();
                        MessageBox.Show("Plazo eliminado");
                        consultaplazo_nuevos();
                    }
                    con_.Cerrarconexion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe seleccionar un registro");
                new filelog("Error eliminar plazos", " Error: " + ex.StackTrace);

            }
        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {
            /*Seleccionamos un Documento y se elimina, sólo para el administrador*/
            try
            {
                //2 codigo de barras
                //3 folio 
                //10 Documento 
                //19 documentoid

                String scodigodebarras = dGV_docimentos_IMPI.SelectedRows[0].Cells[2].Value.ToString();
                String sFoliodoc = dGV_docimentos_IMPI.SelectedRows[0].Cells[3].Value.ToString();
                String sDescripciondoc = dGV_docimentos_IMPI.SelectedRows[0].Cells[11].Value.ToString();
                String sdocimentoid = dGV_docimentos_IMPI.SelectedRows[0].Cells[21].Value.ToString();

                String sPlazodetalleid = dGV_docimentos_IMPI.SelectedRows[0].Cells[1].Value.ToString();
                String sPlazoid = dGV_docimentos_IMPI.SelectedRows[0].Cells[0].Value.ToString();
                if (dGV_docimentos_IMPI.SelectedRows == null)
                {
                    MessageBox.Show("Debe seleccionar un documento para eliminar");
                    return;
                }
                DialogResult result = MessageBox.Show("¿Seguro que desea eliminar el Documento \n con codigo de barras: \"" + scodigodebarras + "\"\n" +
                                     " con Folio: \"" + sFoliodoc + "\"\n" +
                                     " con Descripción: \"" + sDescripciondoc + "\"\n" +
                                     "\" ?", "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK))
                {
                    conect con_ = new conect();
                    String sQuerydeleteplazo = "delete from documento where documentoid = " + sdocimentoid;
                    MySqlDataReader resp_dPlazos = con_.getdatareader(sQuerydeleteplazo);
                    if (resp_dPlazos != null)
                    {
                        resp_dPlazos.Close();
                        MessageBox.Show("Documento eliminado");
                        cargamos_documentoimpi_datagridview(sCasoId, gSTipoSolicitudId);
                    }
                    con_.Cerrarconexion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe seleccionar un registro");
                new filelog("Error eliminar documento", " Error: " + ex.StackTrace);

            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                generaexcel(dGV_docimentos_IMPI);

            }
            catch (Exception E)
            {
                Console.Write("Se canceló la exportación");
                new filelog(loguin.sId, E.ToString());
                MessageBox.Show("" + E.Message);

            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                generaexcel(dgPlazos);

            }
            catch (Exception E)
            {
                Console.Write("Se canceló la exportación");
                new filelog(loguin.sId, E.ToString());
                MessageBox.Show("" + E.Message);

            }
        }
        //tabcontrolall.Location = new Point(this.tabcontrolall.Location.X, 75);
        //tabcontrolall.Size = new Size(tabcontrolall.Width, this.Height - 260);

        public void generaexcel(DataGridView tabla)
        {
            try
            {

                SLDocument obj = new SLDocument();
                //20220425FSV Formateamos fechas
                SLStyle estilofechas = obj.CreateStyle();
                estilofechas.FormatCode = "dd/mm/yyyy";
                //20220425FSV Fin de Formato de Fechas

                //agregamos el nombre de las columnas
                int ic = 1;
                foreach (DataGridViewColumn column in tabla.Columns)
                {
                    String svalorheader = column.HeaderText.ToString();
                    obj.SetCellValue(1, ic, svalorheader);
                    ic++;
                }

                //agregamos el contenido de la tabla
                int ir = 2;
                foreach (DataGridViewRow row in tabla.Rows)
                {
                    ic = 1;
                    foreach (DataGridViewColumn column in tabla.Columns)
                    {
                        //String sValor = "";
                        //if (!(row.Cells[ic - 1].Value is null))
                        //{
                        //    String sFormat = row.Cells[ic - 1].FormattedValueType.Name;
                        //    sValor = row.Cells[ic - 1].Value.ToString();
                        //}

                        //if (sValor == "01/01/0001 12:00:00 a. m.")
                        //{
                        //    sValor = "";
                        //}
                        //obj.SetCellValue(ir, ic, sValor);

                        //20220425FSV Cambio para insertar fechas formateadas
                        String sValor = "";
                        String sFormat = "";
                        if (!(row.Cells[ic - 1].Value is null))
                        {
                            sFormat = row.Cells[ic - 1].InheritedStyle.Format.ToString();
                            sValor = row.Cells[ic - 1].Value.ToString();
                        }

                        if (sValor == "01/01/0001 12:00:00 a. m." || sValor == "01/01/0001")
                        {
                            sValor = "";
                        }


                        if (sFormat == "d" && sValor != "01/01/0001 12:00:00 a. m." && sValor != "")
                        {
                            //para insertar un date debemos converitrlo primero
                            DateTime dValorfecha = DateTime.Parse(sValor);

                            if (dValorfecha.ToString("dd/MM/yyyy") == "01/01/0001")//si el formato de la fecha es minimo agregamos texto vacio
                            {
                                obj.SetCellValue(ir, ic, "");
                            }
                            else
                            {
                                //20220425FSV Aplicamos el formato definido
                                obj.SetCellStyle(ir, ic, estilofechas);
                                //20220425 Fin de Formato
                                obj.SetCellValue(ir, ic, dValorfecha, "dd/MM/yyyy");
                            }
                        }
                        else
                        {
                            if (sValor == "01/01/0001")
                            {
                                sValor = "";
                            }
                            obj.SetCellValue(ir, ic, sValor);
                        }
                        //20220425FSV Fin Cambio





                        ic++;
                    }
                    ir++;
                }
                //generamos la ruta
                String fechalog = DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss");
                String ruta = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\casosking\\";

                //guardamos el archivo
                obj.SaveAs(ruta + "excel_patentes" + fechalog + ".xlsx");
                //abrirmos el archivo
                Process.Start(ruta + "excel_patentes" + fechalog + ".xlsx");

            }
            catch (Exception exs)
            {
                new filelog("error al generar excel ", " :" + exs.Message);
                MessageBox.Show(exs.Message);

            }
        }

        private void label33_MouseLeave(object sender, EventArgs e)
        {
            label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(253)))), ((int)(((byte)(149)))));//255, 253, 149
        }

        private void label33_MouseMove(object sender, MouseEventArgs e)
        {
            label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(214)))), ((int)(((byte)(02)))));//218, 214, 0
        }

        private void label33_DoubleClick(object sender, EventArgs e)
        {
            fBuscarcorresponsal bForm = new fBuscarcorresponsal(sCasoId, gSTipoSolicitudId, tblCotaccorresponsal.Text, tblCorresponsal.Text,loguin);
            if (bForm.ShowDialog() == DialogResult.OK)
            {
                tblCorresponsal.Text = bForm.sClienteidtext;
                tblCotaccorresponsal.Text = bForm.sContactoidtext;
                gSclienteid = bForm.sClienteidvalue;
                richTextBox4.Text = bForm.rtCorreocontacto_pass;
            }
        }

        private void tbDFechacarta_MouseLeave(object sender, EventArgs e)
        {
            tbDFechacarta.Text = tbDFechacarta.Text.Replace("/", "-").Replace(".", "-");
        }

        private void tbDfecharecepcion_MouseLeave(object sender, EventArgs e)
        {
            tbDfecharecepcion.Text = tbDfecharecepcion.Text.Replace("/", "-").Replace(".", "-");
        }

        private void tbDFechainiciouso_MouseLeave(object sender, EventArgs e)
        {
            tbDFechainiciouso.Text = tbDFechainiciouso.Text.Replace("/", "-").Replace(".", "-");
        }

        private void tbDFechaconcesion_MouseLeave(object sender, EventArgs e)
        {
            tbDFechaconcesion.Text = tbDFechaconcesion.Text.Replace("/", "-").Replace(".", "-");
        }

        private void tbDFechavigencia_MouseLeave(object sender, EventArgs e)
        {
            tbDFechavigencia.Text = tbDFechavigencia.Text.Replace("/", "-").Replace(".", "-");
        }

        private void tbDSigpruebauso_MouseLeave(object sender, EventArgs e)
        {
            tbDSigpruebauso.Text = tbDSigpruebauso.Text.Replace("/", "-").Replace(".", "-");
        }

        private void tbfechaprio_MouseLeave(object sender, EventArgs e)
        {
            tbfechaprio.Text = tbfechaprio.Text.Replace("/", "-").Replace(".", "-");
        }

        private void dGVProductos_DoubleClick(object sender, EventArgs e)
        {
            //doble click para seleccionar el producto existente
            try
            {
                if (dGVProductos.SelectedRows is null)
                {
                    MessageBox.Show("Debe seleccionar un producto para modificar");
                }
                else
                {

                    sgidproducto = dGVProductos.SelectedRows[0].Cells[0].Value.ToString();
                    String sClase = dGVProductos.SelectedRows[0].Cells[1].Value.ToString();
                    String sDescripion = dGVProductos.SelectedRows[0].Cells[2].Value.ToString();
                    //cbClasemarca.SelectedValue = int.Parse(sClase);
                    cbClasemarca.SelectedIndex = int.Parse(sClase) - 1;
                    rtDescripciondelproducto.Text = sDescripion;
                    btModificaciones.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            /*Aquí debemos hacer el update para guardar la información modificada*/
            try
            {
                String cambioidioma = "";
                String sTipomarca = "";// (cbDTipomarca.SelectedItem as ComboboxItem).Value.ToString();
                String stipo = "";
                String cLey = "";
                String ccbDTipomarca = "";
                //if (!(cbDTipomarca.SelectedItem is null))
                String squerytipomarca = "";
                String squerytipo = "";
                //if (cbDTipomarca.Text != "")
                if (cbDTipomarca.SelectedItem != null)
                {// si hacemos el cambio
                    sTipomarca = (cbDTipomarca.SelectedItem as ComboboxItem).Value.ToString();
                    squerytipomarca = " TipoMarcaId = '" + sTipomarca + "', ";
                }
                else {
                    //no hacemos el cambio
                    squerytipomarca = "";
                    
                }
                


                // if (cbDTipomarca.SelectedItem != null)
                // {
                //     ccbDTipomarca = " SubTipoSolicitudId = '" + (cbDTipomarca.SelectedItem as ComboboxItem).Value + "',";
                // }





                    if (Ley.SelectedItem != null)
                {
                    cLey = " ID_Ley = '" + (Ley.SelectedItem as ComboboxItem).Value + "',";
                }
                if (cbDIdioma.SelectedItem != null)
                {
                    cambioidioma = " IdiomaId = '" + (cbDIdioma.SelectedItem as ComboboxItem).Value + "',";
                }

                if (tbDtipo.SelectedItem != null)
                {
                    stipo = " TipoSolicitudId = '" + (tbDtipo.SelectedItem as ComboboxItem).Value + "',";
                }
                String stipos = validacombobox(tbDtipo);


                String sUpdtaecaso_mascas = " UPDATE caso_marcas SET  " +
                                            " CasoNumeroExpedienteLargo = '" + tbDExpediente.Text + "', " +
                                            cambioidioma +
                                            " CasoNumConcedida = '" + tbDNumeroReg.Text + "', " +
                                            " productoidiomageneral = '" + rtbDProductossidiomaorig.Text + "', " +
                                             cLey +
                                            squerytipomarca +
                                             stipo +
                                             //" TipoMarcaId = '" + sTipomarca + "', " +
                                             //" TipoSolicitudId = '" + gSTipoSolicitudId + "', " +
                                            " numregistrointernacional = '" + tbNumeroregistrointernacional.Text + "', " +
                                            " Fecharegistrointernacional = DATE(STR_TO_DATE('" + tbFechaRegistrointernacional.Text + "', '%d-%m-%Y')), " +
                                            " CasoFechaRecepcion = DATE(STR_TO_DATE('" + tbDfecharecepcion.Text + "', '%d-%m-%Y')), " +
                                            " CasoFechaCartaCliente = DATE(STR_TO_DATE('" + tbDFechacarta.Text + "', '%d-%m-%Y')), " +
                                            " CasoFechaConcesion = DATE(STR_TO_DATE('" + tbDFechaconcesion.Text + "', '%d-%m-%Y')), " +
                                            " CasoFechaVigencia = DATE(STR_TO_DATE('" + tbDFechavigencia.Text + "', '%d-%m-%Y')), " +
                                            " CasoFechainiciouso = DATE(STR_TO_DATE('" + tbDFechainiciouso.Text + "', '%d-%m-%Y')), " +
                                            //" CasoFechaprobouso = DATE(STR_TO_DATE('" + tbDSigpruebauso.Text + "', '%d-%m-%Y')), " +
                                            " CasoFechaDeclaUso = DATE(STR_TO_DATE('" + tbDSigpruebauso.Text + "', '%d-%m-%Y')), " +
                                            " CasoTituloingles = '" + rtbDDenominacion.Text + "' " +
                                            " WHERE CasoId = '" + sCasoId + "' AND TipoSolicitudId = '" + gSTipoSolicitudId + "'; ";

                conect con1 = new conect();
                MySqlDataReader resp_query = con1.getdatareader(sUpdtaecaso_mascas);
                if (resp_query.RecordsAffected > 0)
                {
                    MessageBox.Show("Se modificó correctamente.");
                    generadom(sCasoId);
                }
                resp_query.Close();
                con1.Cerrarconexion();
                /*Los Datos que podemos modificar son los siguientes:
                 *
                    TipoSolicitudId
                    SubTipoSolicitudId
                    CasoTituloingles
                    CasoTituloespanol
                    IdiomaId
                    CasoFechaConcesion
                    CasoFechaLegal
                    CasoFechaDivulgacionPrevia
                    CasoFechaRecepcion
                    CasoFechaVigencia
                    CasoNumeroConcedida
                    CasoNumeroExpedienteLargo
                    CasoNumero
                    ResponsableId
                    TipoMarcaId
                    CasoLeyendaNoReservable
                    CasoFechaAlta
                    CasoTipoCaptura
                    CasoTitular
                    CasoFechaFilingSistema
                    CasoFechaFilingCliente
                    CasoFechaCartaCliente
                    EstatusCasoId
                    UsuarioId
                    PaisId
                    CasoFechaPruebaUsoSig
                    CasoNumConcedida
                    CasoFechaprobouso
                    CasoFechainiciouso
                 *
                 * 
                 */
                //conect con_4 = new conect();
                //String updateestatuscaso = "UPDATE `caso_marcas` SET `EstatusCasoId` = '" + value + "' WHERE (`CasoId` = '" + sCasoId + "' and TipoSolicitudId = " + gSTipoSolicitudId + ");";
                //MySqlDataReader resp_updatecaso = con_4.getdatareader(updateestatuscaso);
                //if (resp_updatecaso != null)
                //{
                //    tbEstatus.Text = texti;
                //    tbEstatus_header.Text = texti;
                //}
                //resp_updatecaso.Close();
                //con_4.Cerrarconexion();
            }
            catch (Exception Ex)
            {
                new filelog("linea: 1000  UPDATE caso_marcas ", ": " + Ex.Message);
            }
        }

        private void tbFechaRegistrointernacional_MouseLeave(object sender, EventArgs e)
        {
            tbFechaRegistrointernacional.Text = tbFechaRegistrointernacional.Text.Replace("/", "-").Replace(".", "-");
        }

        private void tbDfecharecepcion_KeyUp(object sender, KeyEventArgs e)
        {
            tbDfecharecepcion.Text = tbDfecharecepcion.Text.Replace("/", "-").Replace(".", "-");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string sCasonumero = tbCasoNumero.Text;
            int iCasoidmarca = Int32.Parse(tbCasoid.Text);
            string sMarcaDefendida = rtbDDenominacion_general.Text;

            string message = "Se creará un nuevo Caso de Oposición para el Caso Número: " + sCasonumero + ", Marca: " + sMarcaDefendida + " ¿Esta seguro de continuar?";
            string caption = "Caso Oposiciones";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                // Invocamos el formulario para casos nuevos
                //CasoNuevoOposicion objConsulta = new CasoNuevoOposicion(5, fCapuraform, loguin);
                CasoNuevoOposicion objConsulta = new CasoNuevoOposicion(5, fCapuraform, loguin, iCasoidmarca);
                //20220516 Al invocar Oposiciones cambiamos de seccion, por lo tanto se requiere cerrar marcas
                //buscarclienteform.Close();
                //this.Hide();
                this.Close();
                buscarclienteform.Close();
                //20220516 Si invocamos oposicion desde marcas cerramos form de busqueda
                objConsulta.Show();


            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            string sCasonumero = tbCasoNumero.Text;
            int iCasoidmarca = Int32.Parse(tbCasoid.Text);
            string sMarcaDefendida = rtbDDenominacion_general.Text;

            string message = "Se creará un nuevo Caso de Oposición para el Caso Número: " + sCasonumero + ", Marca: " + sMarcaDefendida + " ¿Esta seguro de continuar?";
            string caption = "Caso Oposiciones";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                // Invocamos el formulario para casos nuevos
                //CasoNuevoOposicion objConsulta = new CasoNuevoOposicion(5, fCapuraform, loguin);
                Caso_Nuevo_Defensor objConsulta = new   Caso_Nuevo_Defensor(5, fCapuraform, loguin, iCasoidmarca);
                //20220516 Al invocar Oposiciones cambiamos de seccion, por lo tanto se requiere cerrar marcas
                //buscarclienteform.Close();
                //this.Hide();
                this.Close();
                buscarclienteform.Close();
                //20220516 Si invocamos oposicion desde marcas cerramos form de busqueda
                objConsulta.Show();


            }
        }
        private void consultamosoposiciones(String sCasoMarcaGenera)
        {
            String sCasoMarca = sCasoMarcaGenera;

            dgViewOposiciones.Rows.Clear();
            conect con_oposiciones = new conect();
            String squery_oposiciones = " SELECT  " +
                                        " CasoId, " +
                                        " CasoNUmero, " +
                                        " MarcaImitadora, " +
                                        " Clase, " +
                                        " NombreImitador, " +
                                        " CasoNumeroExpedienteLargo, " +
                                        " DATE_FORMAT(FecPublicacionImitadora , '%d-%m-%Y') as  FecPublicacionImitadora, " +
                                        " DATE_FORMAT(FecPresentacionImitadora , '%d-%m-%Y') as  FecPresentacionImitadora, " +
                                        " DATE_FORMAT(FecPresentacionOpocision , '%d-%m-%Y') as  FecPresentacionOpocision " +
                                        " FROM caso_oposicion " +
                                        " WHERE tiposolicitudidoriginal in (7,8,9) and casoidoriginal = '" + sCasoMarca + "';";
            MySqlDataReader respuestaoposiciones = con_oposiciones.getdatareader(squery_oposiciones);

            int iOposiciones = 0;
            while (respuestaoposiciones.Read())
            {
                DataGridViewRow dRows = (DataGridViewRow)dgViewOposiciones.Rows[0].Clone();
                try
                {
                    dRows.Cells[0].Value = validareader("CasoId", "CasoId", respuestaoposiciones).Text;
                    dRows.Cells[1].Value = validareader("CasoNUmero", "CasoId", respuestaoposiciones).Text;
                    dRows.Cells[2].Value = validareader("MarcaImitadora", "CasoId", respuestaoposiciones).Text;
                    dRows.Cells[3].Value = validareader("Clase", "CasoId", respuestaoposiciones).Text;
                    dRows.Cells[4].Value = validareader("NombreImitador", "CasoId", respuestaoposiciones).Text;
                    dRows.Cells[5].Value = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestaoposiciones).Text;

                    dRows.Cells[6].Value = validareader("FecPresentacionImitadora", "CasoId", respuestaoposiciones).Text;
                    dRows.Cells[7].Value = validareader("FecPublicacionImitadora", "CasoId", respuestaoposiciones).Text;
                    
                    dRows.Cells[8].Value = validareader("FecPublicacionImitadora", "CasoId", respuestaoposiciones).Text;//plazo



                    dRows.Cells[9].Value = validareader("FecPresentacionOpocision", "CasoId", respuestaoposiciones).Text;
                    dRows.Cells[10].Value = validareader("FecPublicacionOposicion", "CasoId", respuestaoposiciones).Text;

                    dRows.Cells[11].Value = validareader("FecPresentacionOpocision", "CasoId", respuestaoposiciones).Text;
                }
                catch (Exception exs)
                {
                    new filelog("linea 4797", exs.StackTrace);
                }
                dgViewOposiciones.Rows.Add(dRows);
                iOposiciones++;
            }
            respuestaoposiciones.Close();
            con_oposiciones.Cerrarconexion();
        }

        private void dgViewOposiciones_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgViewOposiciones.SelectedRows != null)
                {

                    string sCasonumero = tbCasoNumero.Text;
                    string sMarcaDefendida = rtbDDenominacion_general.Text;

                   
                    //frmBuscaroposiciones = new bOposicion(14, fCapuraform, loguin);
                    String sCasoIdcaso = dgViewOposiciones.SelectedRows[0].Cells[0].Value.ToString();
                    dgViewOposiciones.Rows.Clear();
                    //fToposiciones objoposicion = new fToposiciones(fLoguin, captura, this, sCasoIdcaso);
                    //fToposiciones objoposicion = new fToposiciones(loguin, fCapuraform,this, sCasoIdcaso);
                    //Temporal parametro formulario
                    fToposiciones objoposicion = new fToposiciones(loguin, fCapuraform, sCasoIdcaso);
                    objoposicion.Show();
                    //20220516 Cambio de módulo cerramos marcas
                    //this.Hide();
                    //this.Close();
                    //buscarclienteform.Close();
                    //20220516 fin cambio de módulo
                        
                    

                }
                else
                {
                    MessageBox.Show("Debe seleccionar un caso.");
                }
            }
            catch (Exception exs)
            {
                new filelog("Mensaje:", " :" + exs.Message);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            try {
                conect con_insert_imglogo = new conect();
                String simglogo_insert = "delete from `imagen_logo` where casoid = "+ sCasoId + " and TipoSolicitudId = " + gSTipoSolicitudId + ";";
                MySqlDataReader resp_insert_imglogo = con_insert_imglogo.getdatareader(simglogo_insert);
                if (resp_insert_imglogo.RecordsAffected > 0)
                {//quiere decir que hicimos el insert correctamente
                    
                    pbDimage.Image = null;
                }
                else
                {//no se pudo cargar la imagen
                    MessageBox.Show("No se encontró la imagen.");

                }
                resp_insert_imglogo.Close();
                con_insert_imglogo.Cerrarconexion();
            }
            catch (Exception exs) {
                //MessageBox.Show("Error al intentar borra la imagen");
                new filelog("", " M: "+exs);
            }
            
        }

        private void button29_Click(object sender, EventArgs e)
        {
            /*Seleccionamos un Documento electronico y se elimina, sólo para el administrador*/
            try
            {
                String sDocumentoelectronicoid = dgDocumentoselectronicos.SelectedRows[0].Cells[0].Value.ToString();
                String sTipodocumento = dgDocumentoselectronicos.SelectedRows[0].Cells[3].Value.ToString();
                String sDescripciondoc = dgDocumentoselectronicos.SelectedRows[0].Cells[4].Value.ToString();
                //String sdocimentoid = dGV_docimentos_IMPI.SelectedRows[0].Cells[19].Value.ToString();

                String sPlazodetalleid = dgDocumentoselectronicos.SelectedRows[0].Cells[1].Value.ToString();
                String sPlazoid = dgDocumentoselectronicos.SelectedRows[0].Cells[0].Value.ToString();
                if (dgDocumentoselectronicos.SelectedRows == null)
                {
                    MessageBox.Show("Debe seleccionar un documento para eliminar");
                    return;
                }
                DialogResult result = MessageBox.Show("¿Seguro que desea eliminar el Documento " + sTipodocumento + " \n con descripción: \"" + sDescripciondoc + "\"\n",
                                        "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information); ;
                if (result.Equals(DialogResult.OK))
                {
                    conect con_ = new conect();
                    String sQuerydeleteplazo = "delete from documentoelectronico where DocumentoElectronicoId = " + sDocumentoelectronicoid;
                    MySqlDataReader resp_dPlazos = con_.getdatareader(sQuerydeleteplazo);
                    if (resp_dPlazos != null)
                    {
                        resp_dPlazos.Close();
                        MessageBox.Show("Documento eliminado");
                        consultamosdocumentoselectronicos();
                    }
                    con_.Cerrarconexion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe seleccionar un registro");
                new filelog("Error eliminar documento", " Error: " + ex.StackTrace);

            }
        }

        private void dgDocumentoselectronicos_DoubleClick(object sender, EventArgs e)
        {
            //String sRuta = "";
            //try
            //{
            //    if (lv_documentelect.SelectedItems != null)
            //    {
            //        sRuta = lv_documentelect.SelectedItems[0].SubItems[3].Text;
            //        Process.Start(sRuta);
            //        MessageBox.Show("Ruta: " + sRuta);
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    new filelog("ver edocs", "Error: " + Ex.Message);
            //    MessageBox.Show(Ex.Message + " " + sRuta);
            //}
            String sRuta = "";
            try
            {
                if (dgDocumentoselectronicos.SelectedRows != null)
                {
                    //sRuta = lv_documentelect.SelectedItems[0].SubItems[3].Text;
                    sRuta = dgDocumentoselectronicos.SelectedRows[0].Cells[5].Value.ToString();
                    Process.Start(sRuta);
                    //MessageBox.Show("Ruta: " + sRuta);
                }
            }
            catch (Exception Ex)
            {
                new filelog("ver edocs", "Error: " + Ex.Message);
                MessageBox.Show(Ex.Message + " " + sRuta);
            }
        }

        private void btnGenerarcesion_Click(object sender, EventArgs e)
        {
            
        }

        private void btnGenerarpoder_Click(object sender, EventArgs e)
        {
            
        }

        private void btnGenerarpoder_Click_1(object sender, EventArgs e)
        {
            try
            {
                String sIdidiomaescritos = "";
                if ((cbIdiomadoc.SelectedItem as ComboboxItem) != null)
                { //idioma seleccionado para la carta
                    sIdidiomaescritos = (cbIdiomadoc.SelectedItem as ComboboxItem).Value.ToString();
                }
                else
                {
                    MessageBox.Show("Seleccione el idioma del escrito.");
                    return;
                }


                if (cbPoder.SelectedItem == null)
                {
                    return;
                }

                //cargamos los datos


                objmarcaactualview = new view_caso_marcas(tbCasoid.Text, gSTipoSolicitudId, sIdidiomaescritos);


                generacarta objcarta = null;
                String valuecob = (cbPoder.SelectedItem as ComboboxItem).Value.ToString();
                String sCartanombreESPfile = "";
                String sCartanombreENfile = "";
                conect con_carta = new conect();
                String sQuery_carta = " SELECT  " +
                                        "     * " +
                                        " FROM " +
                                        "     subtipodocumento " +
                                        " WHERE " +
                                        " SubtipodocumentoId = " + valuecob;
                MySqlDataReader respuesta_carta = con_carta.getdatareader(sQuery_carta);
                while (respuesta_carta.Read())
                {
                    sCartanombreESPfile = validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoTemplateEspanol", respuesta_carta).Text;
                    sCartanombreENfile = validareader("SubTipoDocumentoTemplateIngles", "SubTipoDocumentoTemplateIngles", respuesta_carta).Text;
                    //Aquí podemos validar el idioma y si existe la plantilla
                    //generacarta objcarta = new generacarta(sCartanombreESPfile, valuecob, objmarcaactual);
                    if (sIdidiomaescritos == "1")
                    {//Ingles
                        if (sCartanombreENfile == "")
                        {
                            MessageBox.Show("No existe la carta para éste idioma (EN)");
                            return;
                        }
                        String[] sValorusuario = new string[2];
                        sValorusuario[0] = "idUsuariosistema";
                        sValorusuario[1] = sUsuarioparadocs;
                        objmarcaactualview.sValorescampos.Add(sValorusuario);
                        objcarta = new generacarta(sCartanombreENfile, valuecob, objmarcaactualview);
                    }
                    else
                    {
                        if (sIdidiomaescritos == "2")
                        {
                            if (sCartanombreESPfile == "")
                            {
                                MessageBox.Show("No existe la carta para éste idioma (ES)");
                                return;
                            }
                            String[] sValorusuario = new string[2];
                            sValorusuario[0] = "idUsuariosistema";
                            sValorusuario[1] = sUsuarioparadocs;
                            objmarcaactualview.sValorescampos.Add(sValorusuario);
                            objcarta = new generacarta(sCartanombreESPfile, valuecob, objmarcaactualview);
                        }
                        else
                        {
                            MessageBox.Show("Debe asignar un Idioma al cliente");
                        }

                    }
                    if (objcarta.sMensajeerror != "")
                    {
                        MessageBox.Show(objcarta.sMensajeerror);
                    }

                }
                respuesta_carta.Close();
                con_carta.Cerrarconexion();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnGenerarcesion_Click_1(object sender, EventArgs e)
        {
            try
            {
                String sIdidiomaescritos = "";
                if ((cbIdiomadoc.SelectedItem as ComboboxItem) != null)
                { //idioma seleccionado para la carta
                    sIdidiomaescritos = (cbIdiomadoc.SelectedItem as ComboboxItem).Value.ToString();
                }
                else
                {
                    MessageBox.Show("Seleccione el idioma del escrito.");
                    return;
                }


                if (cbCesiones.SelectedItem == null)
                {
                    return;
                }

                //cargamos los datos
                objmarcaactualview = new view_caso_marcas(tbCasoid.Text, gSTipoSolicitudId, sIdidiomaescritos);

                generacarta objcarta = null;
                String valuecob = (cbCesiones.SelectedItem as ComboboxItem).Value.ToString();
                String sCartanombreESPfile = "";
                String sCartanombreENfile = "";
                conect con_carta = new conect();
                String sQuery_carta = " SELECT  " +
                                        "     * " +
                                        " FROM " +
                                        "     subtipodocumento " +
                                        " WHERE " +
                                        " SubtipodocumentoId = " + valuecob;
                MySqlDataReader respuesta_carta = con_carta.getdatareader(sQuery_carta);
                while (respuesta_carta.Read())
                {
                    sCartanombreESPfile = validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoTemplateEspanol", respuesta_carta).Text;
                    sCartanombreENfile = validareader("SubTipoDocumentoTemplateIngles", "SubTipoDocumentoTemplateIngles", respuesta_carta).Text;
                    //Aquí podemos validar el idioma y si existe la plantilla
                    //generacarta objcarta = new generacarta(sCartanombreESPfile, valuecob, objmarcaactual);
                    if (sIdidiomaescritos == "1")
                    {//Ingles
                        if (sCartanombreENfile == "")
                        {
                            MessageBox.Show("No existe la carta para éste idioma (EN)");
                            return;
                        }
                        String[] sValorusuario = new string[2];
                        sValorusuario[0] = "idUsuariosistema";
                        sValorusuario[1] = sUsuarioparadocs;
                        objmarcaactualview.sValorescampos.Add(sValorusuario);
                        objcarta = new generacarta(sCartanombreENfile, valuecob, objmarcaactualview);
                    }
                    else
                    {
                        if (sIdidiomaescritos == "2")
                        {
                            if (sCartanombreESPfile == "")
                            {
                                MessageBox.Show("No existe la carta para éste idioma (ES)");
                                return;
                            }
                            String[] sValorusuario = new string[2];
                            sValorusuario[0] = "idUsuariosistema";
                            sValorusuario[1] = sUsuarioparadocs;
                            objmarcaactualview.sValorescampos.Add(sValorusuario);
                            objcarta = new generacarta(sCartanombreESPfile, valuecob, objmarcaactualview);
                        }
                        else
                        {
                            MessageBox.Show("Debe asignar un Idioma al cliente");
                        }

                    }
                    if (objcarta.sMensajeerror != "")
                    {
                        MessageBox.Show(objcarta.sMensajeerror);
                    }

                }
                respuesta_carta.Close();
                con_carta.Cerrarconexion();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
