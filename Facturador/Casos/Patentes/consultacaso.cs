using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using MailBee.Pop3Mail;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using Facturador.Casos.Documentos;
//using raiz;
using Facturador.plazos_forms;
using SpreadsheetLight;

namespace Facturador
{
    public partial class consultacaso : Form
    {
        public Form1 loguin;
        public captura fCapuraform;
        public String sCasoId;
        public Consutlacaso buscarclienteform;
        public String valuecob;
        public int iIndiceids_global = 0;
        public String[] sArrayids;
        public String sTiposolicitudGlobal = "";
        public String sTipogrupoglobal ="";
        public bool rtTitulo_update = false;
        public bool bTipo_update = false;
        public bool bExpediente_update = false;
        public bool bNumregistro_update = false;
        public bool bSubtipo_update = false;
        public bool bClienteduedate_update = false;
        public bool bFecharecimpi_update = false;
        public bool bFechaconcesion_update = false;
        public bool bCapitulo_update = false;
        public bool bPlazolegal_update = false;
        public bool bFechadivulgacion_update = false;
        public bool bFechavigencia_update = false;
        public bool bFechacarta_update = false;
        public bool bFechainternacional_update = false;
        public bool bFechapublicacion_update = false;
        public bool btituloidiomaoriginal_update = false;
        public bool bIdioma_update = false;
        public bool bAreaimpi_update = false;
        public bool bClasediesno_update = false;

        public bool bcasopatente = false;
        public bool bCasocliente = false;
        public bool bCasointeresado = false;
        public bool bCasoreferencias = false;

        public String gSCasoId = "";
        public String gSCasoNumero = "";
        public String gSTipoSolicitudId = "";
        public String gSSubTipoSolicitudId = "";
        public String gStipopctid = "";
        public String gSCasoNumeroExpedienteLargo = "";
        public String gSCasoTituloespanol = "";
        public String gSCasoTituloingles = "";
        public String gSCasoNumConcedida = "";
        public String gSPaisId = "";
        public String gSUsuarioId = "";
        public String gSEstatusCasoId = "";
        public String gSidiomaId = "";
        //para interesado y clientes
        public String gSclienteid = "";
        public String gSContactoid = "";
        public String gsDivicionalid = "";

        public String[] TipoRelacionId;
        public String[] InteresadoCurp;
        public String[] InteresadoNombre;
        public String[] InteresadoApPaterno;
        public String[] InteresadoApMaterno;
        public String[] nacionalidad;
        public String[] InteresadoRFC;
        public String[] sgDireccionCalle;
        public String[] sgDireccionNumExt;
        public String[] sgDireccionNumInt;
        public String[] sgDireccionColonia;
        public String[] sgDireccionCP;
        public String[] sgDireccionEstado;
        public String[] sgDireccionPoblacion;
        public String[] sgNombrepais;

        bool bPadredivicional = false;
        public bool bFechasupdate = false;
        private bool bSelectidiomachange;
        private string sIdprioridadseleccionada;
        public String[] paises = new String[250];
        public String[] paisesclave = new String[250];
        funcionesdicss objfuncionesdicss = new funcionesdicss();
        //public String sCarpetadocumentos = "DigitalizadoPatentes\\documentosimpi";
        public String sCarpetadocumentos = "Edocs\\Patentes";
        public String  Subtipodocumentoidultimoescrito;
        view_caso_patentes objpatentectualview = null;
        public String sanualidadesnuevas = "";
        public String sUsuarioparadocs = "";

        public consultacaso()
        {
            InitializeComponent();
        }


        //public static int IndexOf(Array array, object value, int startIndex);
        public consultacaso(Form1 fLoguin, captura fcaptura,Consutlacaso buscarcliente,String CasoId)
        {
            try {
                
                loguin = fLoguin;
                fCapuraform = fcaptura;
                sCasoId = CasoId;
                buscarclienteform = buscarcliente;
                sUsuarioparadocs = fLoguin.sUsuarioparadocs;
                InitializeComponent();


                objfuncionesdicss.activaaviso(tbAvisoprueba);
                lCasoID_texbox.Text = CasoId;
                sTipogrupoglobal = buscarcliente.sGTipocaso;

                lvdocumentosimpi.FullRowSelect= true;
                lvdocumentosimpi.GridLines = true;

                lvPlazos.FullRowSelect = true;
                lvPlazos.GridLines = true;
                button13.Enabled = false;
                sIdprioridadseleccionada = "";
                //lvdocumentosimpi.Sorting = SortOrder.Ascending;

                conect con = new conect();
                String sIdspatentes = "select count(*) as numpatentes from caso_patente";
                MySqlDataReader resp_numpatentes = con.getdatareader(sIdspatentes);
                resp_numpatentes.Read();
                String sNumerodepatentes = objfuncionesdicss.validareader("numpatentes", "numpatentes", resp_numpatentes).Text;
                resp_numpatentes.Close();
                con.Cerrarconexion();

                int iNumerogrupopatentes = System.Convert.ToInt32(sNumerodepatentes);
                sArrayids =new String[iNumerogrupopatentes];

                conect con_2 = new conect();
                String sGetids = "select * from caso_patente";
                MySqlDataReader resp_getidspatentes = con_2.getdatareader(sGetids);
                int iIndiceids = 0;
                while (resp_getidspatentes.Read())
                {
                    sArrayids[iIndiceids] = objfuncionesdicss.validareader("Casoid", "Casoid", resp_getidspatentes).Text;
                    iIndiceids++;
                }
                resp_getidspatentes.Close();
                con_2.Cerrarconexion();
            
                //Area impi
                conect con_3 = new conect();
                String squeryareaimpi = "select AreaImpiId, AreaImpiDescrip from areaimpi;";
                MySqlDataReader resp_areaimpi = con_3.getdatareader(squeryareaimpi);
                while (resp_areaimpi.Read())
                {
                    cbAreaimpi.Items.Add(objfuncionesdicss.validareader("AreaImpiDescrip", "AreaImpiId", resp_areaimpi));
                }
                resp_areaimpi.Close();
                con_3.Cerrarconexion();
                iIndiceids_global = Array.IndexOf(sArrayids, CasoId);
                /*
                
                //int iCasoid = sArrayids.IndexOf<String>(sArrayids, CasoId);
                string[] ubicacion = Directory.GetFiles(@"C:\Pclientes\Cartas");//<--aqui va la ruta de la carpeta donde estan los documentos
                for (int i = 0; i < ubicacion.Length; i++)
                {
                    cbCartas.Items.Add(Path.GetFileName(ubicacion[i]));//combobox el que mostrara todos los nombres
                }*/

                //iIndiceids_global = Array.IndexOf(sArrayids, CasoId);
                //int iCasoid = sArrayids.IndexOf<String>(sArrayids, CasoId);
                //String strRutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                //String[] ubicacions;
                //String[] nombre;
                //String sNombresfiles = "";
                //String sNombresfilesUbic = "";
                //string[] ubicacion = Directory.GetFiles(strRutaArchivo + @"\casosking\Cartas");//<--aqui va la ruta de la carpeta donde estan los documentos
                //for (int i = 0; i < ubicacion.Length; i++)
                //{
                //    cbCartas.Items.Add(Path.GetFileName(ubicacion[i]));//combobox el que mostrara todos los nombres
                //    ubicacions = Path.GetFileName(ubicacion[i]).Split('\\');
                //    nombre = ubicacions[ubicacions.Length - 1].Split('.');
                //    sNombresfilesUbic += ""+ Path.GetFileName(ubicacion[i]) + " \n";
                //    //sNombresfiles += "" + nombre[i] + " \n";
                //    //new filelog(loguin.sId, nombre[0]);
                //}

                //para los mails
                //if (loguin.sCorreousr != "" && loguin.sContrasenacorreo != "" && loguin.sUsuarioCodigo=="1")
                //{
                //    MailBee.Global.LicenseKey = "MN110-8B8932A44B8239779277420FE843-E158";
                //    Pop3 pop = new Pop3();
                //    try
                //    {
                //        String sServermail = "";
                //        bool b = loguin.sCorreousr.Contains("gmail");
                //        if (b)
                //        {//si el servidor es gmail entonces debe apuntar a uno diferente
                //            sServermail = "pop.gmail.com";
                //        }else{
                //            sServermail = "mail.dicss.com.mx";
                //        }
                //        pop.Connect(sServermail);
                //        pop.Login(loguin.sCorreousr, loguin.sContrasenacorreo);
                //        Console.WriteLine("Successfully logged in. __mail dicss ");
                //    }
                //    catch (MailBeePop3LoginNegativeResponseException ex)
                //    {
                //        Console.WriteLine("POP3 server replied with a negative response at login:" + ex.ToString());
                //    }

                //    //for (int y = correos.Length - 1; y >= 0; y--)
                //    //{
                //    //    Mesageemail = pop.DownloadEntireMessage(pop.GetMessageIndexFromUid(correos[y]));
                //    //    Console.WriteLine("Asunto: " + Mesageemail.Subject);
                //    //    Console.WriteLine("Mensaje: " + Mesageemail.BodyPlainText);
                //    //    Console.WriteLine(pop.GetMessageIndexFromUid(correos[y]));

                //    //    ListViewItem newitem = new ListViewItem(correos[y]);
                //    //    newitem.SubItems.Add(pop.GetMessageIndexFromUid(correos[y]).ToString());
                //    //    newitem.SubItems.Add(Mesageemail.Subject);
                //    //    newitem.SubItems.Add(Mesageemail.BodyPlainText);
                //    //    newitem.SubItems.Add("Adjutno");
                //    //    listView1.Items.Add(newitem);
                //    //}
                //}

                //for (int y = correos.Length - 1; y >= 0; y--)
                //{
                //    Mesageemail = pop.DownloadEntireMessage(pop.GetMessageIndexFromUid(correos[y]));
                //    Console.WriteLine("Asunto: " + Mesageemail.Subject);
                //    Console.WriteLine("Mensaje: " + Mesageemail.BodyPlainText);
                //    Console.WriteLine(pop.GetMessageIndexFromUid(correos[y]));

                //    ListViewItem newitem = new ListViewItem(correos[y]);
                //    newitem.SubItems.Add(pop.GetMessageIndexFromUid(correos[y]).ToString());
                //    newitem.SubItems.Add(Mesageemail.Subject);
                //    newitem.SubItems.Add(Mesageemail.BodyPlainText);
                //    newitem.SubItems.Add("Adjutno");
                //    listView1.Items.Add(newitem);
                //}

                ComboboxItem combouno = new ComboboxItem();//IMPI-00-002_B.docx
                combouno.Text = "Solicitud de Renovación y Declaración de Uso Real y Efectivo de Signos Distintivos";
                combouno.Value = 1;

                ComboboxItem combodos = new ComboboxItem();
                combodos.Text = "Solicitud de Inscripción de Transmisión de Derechos";
                combodos.Value = 2;

                ComboboxItem combotres = new ComboboxItem();
                combotres.Text = "Solicitud de Inscripción de Licencia de Uso o Franquicia";
                combotres.Value = 3;

                ComboboxItem combocuatro = new ComboboxItem();
                combocuatro.Text = "Solicitud de Toma de Nota de Cambio de Domicilio";
                combocuatro.Value = 4;

                ComboboxItem combocinco = new ComboboxItem();
                combocinco.Text = "Solicitud de Inscripción de Transformación de Régimen Jurídico o Cambio de Nombre, Denominación o Razón Social";
                combocinco.Value = 5;

                ComboboxItem comboseis = new ComboboxItem();
                comboseis.Text = "Solicitud de Autorización de Uso de Denominación de Origen o Indicación Geográfica Nacional";
                comboseis.Value = 6;

                ComboboxItem combosiete = new ComboboxItem();
                combosiete.Text = "Solicitud de Inscripción del Convenio por el que se Permite el Uso de una Denominación de Origen o Indicación Geográfica Nacional";
                combosiete.Value = 7;

                ComboboxItem comboocho = new ComboboxItem();
                comboocho.Text = "Declaración de Uso Real y Efectivo de Signos Distintivos";
                comboocho.Value = 8;

                   

                //cbFormatosnuevos.Items.Add(combouno);
                //cbFormatosnuevos.Items.Add(combodos);
                //cbFormatosnuevos.Items.Add(combotres);
                //cbFormatosnuevos.Items.Add(combocuatro);
                //cbFormatosnuevos.Items.Add(combocinco);
                //cbFormatosnuevos.Items.Add(comboseis);
                //cbFormatosnuevos.Items.Add(combosiete);
                //cbFormatosnuevos.Items.Add(comboocho);
                
                
                generadom(CasoId);
            }
            catch (Exception E)
            {
                new filelog(loguin.sId, "linea 292: "+E.ToString());
            }
        }

        public void resetvariable() {
            rtTitulo_update = false;            
            bTipo_update = false;
            bExpediente_update = false;
            bNumregistro_update = false;
            bSubtipo_update = false;
            bClienteduedate_update = false;
            bFecharecimpi_update = false;
            bFechaconcesion_update = false;
            bCapitulo_update = false;
            bPlazolegal_update = false;
            bFechadivulgacion_update = false;
            bFechavigencia_update = false;
            bFechacarta_update = false;
            bFechainternacional_update = false;
            bFechapublicacion_update = false;
            btituloidiomaoriginal_update = false;
            bAreaimpi_update = false;
            bClasediesno_update = false;
            tbNumeroprio.Text = "";
            tbfechaprio.Text = "";
            //tbCvepais.Text = "";

            cbNombrepais.Items.Clear();
            cbTiposolprio.Items.Clear();

            
            sIdprioridadseleccionada = "";
            tbRangoanupagadas.Text = "";
            tbultimoquinqueniopagado.Text = "";
            tbproximoquinqueniopagar.Text = "";
            tbfechaquinquenippago.Text = "";
            //CB_formatoscc.Items.Clear();
            listViewDivicional.Items.Clear();
            cbEscritos.Items.Clear();
            cbOficios.Items.Clear();
            //cbCartas.Items.Clear();
            lvPlazos.Items.Clear();
            cbIdioma.Items.Clear();
            cbidiomaescrito.Items.Clear();
            cbIdiomacarta.Items.Clear();
            tb_contdocelect.Text = "";
            lCliente_texbox_.Text = "";
            tblCorresponsal.Text = "";
            tblCotaccorresponsal.Text = "";
            richTextBox4.Text = "";
        }

        public void generadom (String sCasoiddom){
            if (validaversion(loguin.sVersion)) {
                return;
            }
            String Svariablelinea = "";//mensajes para debuguear las lineas
            try
            {
                resetvariable();
                lbPadre.Text = "Caso No Divisional";
                labelHijoPadre.Text = "Casos hijos:";
                button45.Enabled = true;
                lCasoID_texbox.Text = sCasoiddom;
                sCasoId = sCasoiddom;
                button40.Enabled = true;
                lvinteresados.Items.Clear();
                lvinventores.Items.Clear();
            
                progressBar1.Show();
                progressBar1.Value = 0;

                if (loguin.sUsuarioCodigo=="1" || loguin.sUsuarioCodigo == "3") {
                    bAgregarplazo.Visible = true;
                }
                //bAgregarplazo
                //conect con = new conect();
                //String kwery = "SELECT * FROM tiposolicitud";
                //MySqlDataReader respuestastring = con.getdatareader(kwery);

                //ComboboxItem cbitem_viejo = new ComboboxItem();
                //cbitem_viejo.Text = "Solicitud de Patente";
                //cbitem_viejo.Value = "99";
                //CB_formatoscc.Items.Add(cbitem_viejo);

                //ComboboxItem cbitem = new ComboboxItem();
                //cbitem.Text = "Solicitud de Patente (Nuevo Formato)";
                //cbitem.Value = "100";
                //this.Enabled = false;
                //progressBar1.Value = 10;
                //CB_formatoscc.Items.Add(cbitem);
                //while (respuestastring.Read())
                //{
                //    CB_formatoscc.Items.Add(objfuncionesdicss.validareader("TipoSolicitudDescripcion", "TipoSolicitudId", respuestastring));
                //}
                //respuestastring.Close();
                //con.Cerrarconexion();

                conect con_datoscaso = new conect();
                String sQuery = "SELECT "+
                                " CasoId, " +
                                " TipoSolicitudId, " +
                                " SubTipoSolicitudId, " +
                                " tipopctid, " +
                                " CasoNumero, " +
                                " CasoNumeroExpedienteLargo, " +
                                " CasoNumConcedida, " +
                                " CasoTitular, " +
                                " Get_Interesados_tiposol(CasoId, TipoSolicitudId) As interesados, " +
                                " CasoNumeroExpedienteLargo, " +
                                " CasoTituloespanol, " +
                                " CasoTituloingles, " +
                                " DATE_FORMAT(caso_patente.CasoFechaFilingCliente , '%d-%m-%Y') as CasoFechaFilingCliente, " +
                                " DATE_FORMAT(caso_patente.CasoFechaRecepcion , '%d-%m-%Y') as CasoFechaRecepcion, " +
                                //" TipoPctDescrip, " +
                                " DATE_FORMAT(caso_patente.CasoFechaLegal , '%d-%m-%Y') as CasoFechaLegal, " +
                                " DATE_FORMAT(caso_patente.CasoFechaDivulgacionPrevia , '%d-%m-%Y') as CasoFechaDivulgacionPrevia, " +
                                " DATE_FORMAT(caso_patente.CasoFechaCartaCliente , '%d-%m-%Y') as CasoFechaCartaCliente, " +
                                " DATE_FORMAT(caso_patente.CasoFechaInternacional , '%d-%m-%Y') as CasoFechaInternacional, " +
                                //" ContactoNombre, " +
                                //" ContactoEmail, " +
                                " CasoNumConcedida, " +
                                " PaisId, " +//EstatusCasoId
                                " UsuarioId, " +//"idiomaId"
                                " AreaImpiId, " +//"idiomaId"
                                " EstatusCasoId, " +//tipopctid
                                " Get_IdiomaCliente(CasoId, TipoSolicitudId) As idiomaId, " +
                                " DATE_FORMAT(caso_patente.CasoFechaConcesion , '%d-%m-%Y') as CasoFechaConcesion, " +
                                " DATE_FORMAT(caso_patente.CasoFechaVigencia , '%d-%m-%Y') as CasoFechaVigencia, " +
                                " DATE_FORMAT(caso_patente.CasoFechaPublicacionSolicitud , '%d-%m-%Y') as CasoFechaPublicacionSolicitud, " +
                                " CasoNumero, " +
                                " CasoDisenoClasificacion, " +
                                " Divicionalid" +
                                " FROM caso_patente WHERE caso_patente.CasoId = '" + sCasoiddom + "'";
                MySqlDataReader respuestastring3 = con_datoscaso.getdatareader(sQuery);
                int iContvalida = 0;
                while (respuestastring3.Read())
                {
                    gSCasoId = objfuncionesdicss.validareader("CasoId", "CasoId", respuestastring3).Text;
                    gSCasoNumero = objfuncionesdicss.validareader("CasoNumero", "CasoId", respuestastring3).Text;
                    gSTipoSolicitudId = objfuncionesdicss.validareader("TipoSolicitudId", "CasoId", respuestastring3).Text;
                    gSSubTipoSolicitudId = objfuncionesdicss.validareader("SubTipoSolicitudId", "CasoId", respuestastring3).Text;
                    gStipopctid = objfuncionesdicss.validareader("tipopctid", "CasoId", respuestastring3).Text;
                    gSCasoNumeroExpedienteLargo = objfuncionesdicss.validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                    gSCasoTituloespanol = objfuncionesdicss.validareader("CasoTituloespanol", "CasoId", respuestastring3).Text;
                    gSCasoTituloingles = objfuncionesdicss.validareader("CasoTituloingles", "CasoId", respuestastring3).Text;
                    gSCasoNumConcedida = objfuncionesdicss.validareader("CasoNumConcedida", "CasoId", respuestastring3).Text;
                    gSPaisId = objfuncionesdicss.validareader("PaisId", "CasoId", respuestastring3).Text;
                    gSUsuarioId = objfuncionesdicss.validareader("UsuarioId", "CasoId", respuestastring3).Text;
                    gSEstatusCasoId = objfuncionesdicss.validareader("EstatusCasoId", "CasoId", respuestastring3).Text;
                    gSidiomaId = objfuncionesdicss.validareader("idiomaId", "CasoId", respuestastring3).Text;
                    gsDivicionalid = objfuncionesdicss.validareader("Divicionalid", "CasoId", respuestastring3).Text;
                    //Tipo Solicitud
                    cbAreaimpi.Text = objfuncionesdicss.validareader("AreaImpiId", "CasoId", respuestastring3).Text;//cbAreaimpi
                    //2 Modelos de utilidada

                    //3Diseño Industrial (Modelo)

                    //4Diseño Industrial (Diseño)

                    //tbFechaRecimpi.Text

                    //Para Diseños


                    try {//Consultamos las facturas
                        dgview_facturas.Rows.Clear();
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
                                                "     FORMAT(FLOOR(fac_courierexpenses) + FLOOR((SELECT  " +
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
                                                    " WHERE fac_nuestrareferencia LIKE '%" + gSCasoNumero + "%'; ";

                        MySqlDataReader respuestafacturas = con_facturas.getdatareader(squery_facturas);
                        String sEstatus = "Aún no factura";
                        tbEstatusfactura.BackColor = Control.DefaultBackColor;
                        int iFacturas = 0;
                        bool bFacturaadeudo = false;
                        while (respuestafacturas.Read())
                        {
                            dgview_facturas.Rows.Add(objfuncionesdicss.validareader("fac_pdf", "fac_pdf", respuestafacturas).Text,
                                                    objfuncionesdicss.validareader("Invoice #/ Factura no.", "Invoice #/ Factura no.", respuestafacturas).Text,
                                                    objfuncionesdicss.validareader("Date of Issue/ Fecha Emision", "Date of Issue/ Fecha Emision", respuestafacturas).Text,
                                                    objfuncionesdicss.validareader("Fecha Pago", "Fecha Pago", respuestafacturas).Text,
                                                    objfuncionesdicss.validareader("Days past due/ Dias sin pagar", "Days past due/ Dias sin pagar", respuestafacturas).Text,
                                                    objfuncionesdicss.validareader("Payment Status/ Status pago", "Payment Status/ Status pago", respuestafacturas).Text,
                                                    objfuncionesdicss.validareader("Folio Feps", "Folio Feps", respuestafacturas).Text,
                                                    objfuncionesdicss.validareader("Total (MER)", "Total (MER)", respuestafacturas).Text,
                                                    objfuncionesdicss.validareader("Numero_de_servicios", "Numero_de_servicios", respuestafacturas).Text,
                                                    objfuncionesdicss.validareader("Servicio_uno", "Servicio_uno", respuestafacturas).Text,
                                                    objfuncionesdicss.validareader("Servicio_dos", "Servicio_dos", respuestafacturas).Text,
                                                    objfuncionesdicss.validareader("Servicio_tres", "Servicio_tres", respuestafacturas).Text);

                            //String svalor = objfuncionesdicss.validareader("Invoice #/ Factura no.", "ClienteId", respuestafacturas).Text;
                            String svalor = objfuncionesdicss.validareader("Payment Status/ Status pago", "Payment Status/ Status pago", respuestafacturas).Text;
                            if (objfuncionesdicss.validareader("Payment Status/ Status pago", "Payment Status/ Status pago", respuestafacturas).Text == "Sin pagar")
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
                    }
                    catch (Exception Exsfac) {
                        new filelog("conulta facturas linea 444", Exsfac.StackTrace.ToString());
                    }


                    try {
                        // para Cartas oficios y escritos
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
                                                " 	relaciondocumento.casoid = " + gSCasoId +
                                                "     and relaciondocumento.TipoSolicitudId = " + gSTipoSolicitudId +
                                                "     and subtipodocumento.TipoDocumentoId in(1, 2) " +
                                                "     and relaciondocumento.DocumentoId = documento.DocumentoId " +
                                                "     and documento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId; ";
                        MySqlDataReader respuesta_ofiesc = con_ofiesc.getdatareader(sQuery_ofiesc);
                        while (respuesta_ofiesc.Read())
                        {
                            String sNombredescrip = objfuncionesdicss.validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoDescrip", respuesta_ofiesc).Text;
                            String sId = objfuncionesdicss.validareader("DocumentoId", "DocumentoId", respuesta_ofiesc).Value.ToString();
                            cbOficiosEscritos.Items.Add(objfuncionesdicss.validareader("SubTipoDocumentoDescrip", "DocumentoId", respuesta_ofiesc));
                        }
                        respuesta_ofiesc.Close();
                        con_ofiesc.Cerrarconexion();


                        // para Escritos cargamos sólo los oficios
                        cbOficios.Items.Clear();
                        conect con_ofi = new conect();
                        String sQuery_ofi = " SELECT  " +
                                                "     documento.DocumentoId, " +
                                                "     subtipodocumento.SubTipoDocumentoDescrip " +
                                                " FROM " +
                                                "     documento, " +
                                                "     subtipodocumento, " +
                                                "     relaciondocumento " +
                                                " where  " +
                                                " 	relaciondocumento.casoid = " + gSCasoId +
                                                "     and relaciondocumento.TipoSolicitudId = " + gSTipoSolicitudId +
                                                "     and subtipodocumento.TipoDocumentoId in(1) " +
                                                "     and relaciondocumento.DocumentoId = documento.DocumentoId " +
                                                "     and documento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId; ";
                        MySqlDataReader respuesta_ofi = con_ofi.getdatareader(sQuery_ofi);
                        while (respuesta_ofi.Read())
                        {
                            String sNombredescrip = objfuncionesdicss.validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoDescrip", respuesta_ofi).Text;
                            String sId = objfuncionesdicss.validareader("DocumentoId", "DocumentoId", respuesta_ofi).Value.ToString();
                            cbOficios.Items.Add(objfuncionesdicss.validareader("SubTipoDocumentoDescrip", "DocumentoId", respuesta_ofi));
                        }
                        respuesta_ofi.Close();
                        con_ofi.Cerrarconexion();
                    }
                    catch (Exception Exscatrasofi) {
                        new filelog("consultamos catas oficios y escritos", Exscatrasofi.StackTrace.ToString());
                    }
                    



                    //anualidades antiguas
                    TabPage objtab = tab_anualidades;
                    //anualidades secuencia
                    TabPage objtab_ant = tab_anualidades_ant;

                    //Revisar acultar y mostrar las tabs

                    //DateTime dFechalimitenuevaley_disenos = DateTime.ParseExact("18-03-2020", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    //String sFechadePresentacion = objfuncionesdicss.validareader("CasoFechaRecepcion", "CasoFechaRecepcion", respuestastring3).Text;

                    //if (sFechadePresentacion == "00-00-0000")
                    //{
                    //    tabcontrolall.TabPages.Remove(objtab_ant);
                    //}
                    //else {

                    //    DateTime dsFechapresentaciong = DateTime.ParseExact(sFechadePresentacion, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    //    //Si la fecha de presentación es mayor a 18 de marzo de 2018 ocultamos las anualidades anteriores, si no , ocultamos las nuevas
                    //    if (dsFechapresentaciong >= dFechalimitenuevaley_disenos)
                    //    {
                    //        tabcontrolall.TabPages.Remove(objtab_ant);
                    //    }
                    //    else
                    //    {
                    //        tabcontrolall.TabPages.Remove(objtab);
                    //    }
                    //}

                    Svariablelinea = "Dentro del while linea 617";


                    tbCasodiseno.Text = objfuncionesdicss.validareader("CasoDisenoClasificacion", "CasoDisenoClasificacion", respuestastring3).Text;

                    
                    //gSTipoSolicitudId
                    //dganualidadesMD
                    //Leemos las dos tab de anualidades

                    


                    //tabcontrolall.TabPages.Remove();


                    // A PARTIR DE LA FECHA PRESENTACION SI ES MAYOR A 
                    // 13 DE MARZO DE 2018 DEBEN SER RENOVACIONES
                    // CON LA NUEVA PESTAÑA

                    /*
                     SELECT 
                        documento.*
                     FROM
                            documento,
                            relaciondocumento,
                            caso_patente
                     WHERE
	                        caso_patente.CasoNumero like '%7402%'
	                        and	(documento.SubTipoDocumentoId = 115
                            or documento.SubTipoDocumentoId = 116 
                            or documento.SubTipoDocumentoId = 1246)
                            and documento.DocumentoId = relaciondocumento.DocumentoId
                            and relaciondocumento.CasoId = caso_patente.CasoId
                            and relaciondocumento.TipoSolicitudId = caso_patente.TipoSolicitudId;
                     */

                    //13 marzo de 2018
                    bool bAnualidapestaña = false;
                    switch (gSTipoSolicitudId)
                    {
                        case "1":
                            {//PATENTES
                                bAnualidapestaña = true;
                            }
                            break;
                        case "2":{//Modelo
                            bAnualidapestaña=true;
                        }break;
                        case "3":{//Diseño Industrial
                            bAnualidapestaña=true;
                        
                        }break;
                        case "4":{//Diseño Industrial
                            bAnualidapestaña=true;
                        }break;
                        default:
                            bAnualidapestaña = true;
                            //tabcontrolall.TabPages.Remove(tab_anualidades);
                            //
                            //TabPage objtab = tab_anualidades;
                            //tabcontrolall.TabPages.Remove();
                            //tabcontrolall.TabPages.Insert(11, objtab);
                            break;
                    }
                    sanualidadesnuevas = "";

                    if (bAnualidapestaña)
                    {//validamos que sea Modelo o diseño
                        //tabcontrolall.TabPages.Remove(tab_anualidades_ant);
                        ////
                        //TabPage objtab = tab_anualidades;
                        //tabcontrolall.TabPages.Insert(11, objtab);
                        //tabcontrolall.TabPages.Remove(tab_anualidades);
                        
                        //cargamos sus anualidades
                        conect con_caso_anualidadesMD = new conect();
                        //String squeryanualidades = "select * from anialidades_md_nuevos where casoid = " + gSCasoId + " and TipoSolicitudId = " + gSTipoSolicitudId;
                        String squeryanualidades = "  SELECT " +
                                                    "     anialidades_md_nuevos.*," +
                                                    "     estatusanualidad.EstatusAnualidadDescrip, Get_anualidades_aPAGAResp_MODnuevos(" + gSCasoId + ", " + gSTipoSolicitudId + ") as anualidadesnuevas " +
                                                    " FROM" +
                                                    "     estatusanualidad," +
                                                    "     anialidades_md_nuevos" +
                                                    " WHERE" +
                                                    "     estatusanualidad.EstatusAnualidadId = anialidades_md_nuevos.estatusanualidad"+
                                                    " and casoid = " + gSCasoId + " and TipoSolicitudId = " + gSTipoSolicitudId;
                        
                        MySqlDataReader resp_anualidades_MD = con_caso_anualidadesMD.getdatareader(squeryanualidades);
                        dganualidadesMD.Rows.Clear();
                        bool bRangoanualidad = true;
                        while (resp_anualidades_MD.Read())
                        {
                            String Anialidades_MD_nuevosid = objfuncionesdicss.validareader("Anialidades_MD_nuevosid", "Anialidades_MD_nuevosid", resp_anualidades_MD).Text;//consultar Caso número padre
                            String sSecuencia = objfuncionesdicss.validareader("secuencia", "Anialidades_MD_nuevosid", resp_anualidades_MD).Text;
                            String sPeriodo = objfuncionesdicss.validareader("periodo", "Anialidades_MD_nuevosid", resp_anualidades_MD).Text;
                            String sFechaPgao = objfuncionesdicss.validareader("fecha_pago", "Anialidades_MD_nuevosid", resp_anualidades_MD).Text;
                            String sCasoid = objfuncionesdicss.validareader("casoid", "Anialidades_MD_nuevosid", resp_anualidades_MD).Text;
                            String sEstatusAnualidadDescrip = objfuncionesdicss.validareader("EstatusAnualidadDescrip", "Anialidades_MD_nuevosid", resp_anualidades_MD).Text;
                            String sfechalimite = objfuncionesdicss.validareader("fechalimite", "Anialidades_MD_nuevosid", resp_anualidades_MD).Text;
                            sanualidadesnuevas = objfuncionesdicss.validareader("anualidadesnuevas", "anualidadesnuevas", resp_anualidades_MD).Text;
                            if (sanualidadesnuevas != "")
                            { //tbFechaproximasanualidades
                                tbFechaproximasanualidades.Text = "" + sanualidadesnuevas;
                            }
                            else {
                                tbFechaproximasanualidades.Text = "";
                            }

                            if (sFechaPgao.Length>10)
                            {
                                sFechaPgao = sFechaPgao.Substring(0, 10);
                            }

                            if (sfechalimite.Length > 0)
                            {
                                sfechalimite = sfechalimite.Substring(0, 10);
                            }
                            DataGridViewRow dgRow = (DataGridViewRow)dganualidadesMD.Rows[0].Clone();//new DataGridViewRow();
                            dgRow.Cells[0].Value = Anialidades_MD_nuevosid;
                            dgRow.Cells[1].Value = sSecuencia;
                            dgRow.Cells[2].Value = sPeriodo;
                            dgRow.Cells[3].Value = sEstatusAnualidadDescrip;
                            dgRow.Cells[4].Value = sfechalimite;
                            dgRow.Cells[5].Value = sFechaPgao;
                            if (sEstatusAnualidadDescrip == "Pendiente")
                            {
                                if (bRangoanualidad)
                                {
                                    tb_Rangoanualidadesacubrir.Text = sPeriodo;
                                    tb_fechasigpago.Text = sfechalimite;
                                    bRangoanualidad = false;
                                }
                                dgRow.DefaultCellStyle.BackColor = Color.LightCoral; 
                            }else {
                                tb_cubiertasanualidades.Text = sPeriodo;
                                dgRow.DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                            }
                            dganualidadesMD.Rows.Add(dgRow);//Anialidades_MD_nuevosid, sSecuencia, sPeriodo, sEstatusAnualidadDescrip, sfechalimite, sFechaPgao);
                        }
                        resp_anualidades_MD.Close();
                        con_caso_anualidadesMD.Cerrarconexion();
                        //FIN cargamos sus anualidades
                        

                    }
                    else{
                        //tabcontrolall.TabPages.Remove(tab_anualidades_ant);
                    }

                    if (gsDivicionalid != "0")
                    {
                        String sNumerocaso = "";
                        conect con_caso_patente_div = new conect();
                        String sCasonumeropadre = "select * from caso_patente where casoid = " + gsDivicionalid;
                        MySqlDataReader resp_casonumeropadre = con_caso_patente_div.getdatareader(sCasonumeropadre);
                        while (resp_casonumeropadre.Read())
                        {
                            sNumerocaso = objfuncionesdicss.validareader("CasoNumero", "CasoId", resp_casonumeropadre).Text;//consultar Caso número padre
                            ListViewItem listinteresados = new ListViewItem(objfuncionesdicss.validareader("CasoId", "CasoId", resp_casonumeropadre).Text);
                            listinteresados.SubItems.Add(objfuncionesdicss.validareader("CasoNumero", "CasoId", resp_casonumeropadre).Text);
                            listinteresados.SubItems.Add(objfuncionesdicss.validareader("CasoTitular", "CasoId", resp_casonumeropadre).Text);
                            listinteresados.SubItems.Add(objfuncionesdicss.validareader("CasoNumeroExpedienteLargo", "CasoId", resp_casonumeropadre).Text);
                            listViewDivicional.Items.Add(listinteresados);
                            listViewDivicional.FullRowSelect = true;
                        }
                        resp_casonumeropadre.Close();
                        con_caso_patente_div.Cerrarconexion();
                        lbPadre.Text = "Caso Divisional hijo de: " + sNumerocaso;
                        labelHijoPadre.Text = "Caso Madre "+ sNumerocaso;
                        bAddpadre.Enabled = false;
                        button40.Enabled = false;
                        lbPadre.Show();
                    }
                
                    lCasoNumero_texbox.Text = objfuncionesdicss.validareader("CasoNumero", "CasoId", respuestastring3).Text;
                    lExpediente_texbox.Text = objfuncionesdicss.validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                    lRegistro_texbox.Text = objfuncionesdicss.validareader("CasoNumConcedida", "CasoId", respuestastring3).Text;
                    try {
                        String sIdiomaid = objfuncionesdicss.validareader("PaisId", "CasoId", respuestastring3).Text;
                        if (sIdiomaid!="") {
                            conect con_pais = new conect();
                            String sPais = "select * from pais where PaisId = " + sIdiomaid;
                            MySqlDataReader resp_pais = con_pais.getdatareader(sPais);
                            while (resp_pais.Read())
                            {
                                lPais_texbox.Text = objfuncionesdicss.validareader("PaisNombre", "PaisId", resp_pais).Text;//consultar pais
                                                                                                                           //lPais_texbox.Text = objfuncionesdicss.validareader("PaisNombre", "CasoId", respuestastring3).Text;//consultar pais
                            }
                            resp_pais.Close();
                            con_pais.Cerrarconexion();
                        }
                        
                    }
                    catch (Exception exs) {
                        new filelog("linea 776:", exs.Source);
                    }

                    try {//Consuyltamos los datos del cliente
                        conect con_cliente = new conect();
                        //String sclientdatos = "select * from cliente, casocliente where casocliente.casoid = " + objfuncionesdicss.validareader("CasoId", "CasoId", respuestastring3).Text + " and  casocliente.clienteid = cliente.clienteid";
                        String sclientdatos = "select * from cliente, casocliente where casocliente.casoid = " 
                            + objfuncionesdicss.validareader("CasoId", "CasoId", respuestastring3).Text + 
                            " and casocliente.TipoSolicitudId = " 
                            + objfuncionesdicss.validareader("TipoSolicitudId", "CasoId", respuestastring3).Text 
                            + " and  casocliente.clienteid = cliente.clienteid";
                        MySqlDataReader resp_datoscliente = con_cliente.getdatareader(sclientdatos);
                        while (resp_datoscliente.Read())
                        {
                            lCliente_texbox_.Text = objfuncionesdicss.validareader("NombreUtilClient", "CasoId", resp_datoscliente).Text;//consultar cliente
                            gSclienteid = objfuncionesdicss.validareader("clienteid", "CasoId", resp_datoscliente).Text;//consultar cliente
                        }
                        resp_datoscliente.Close();
                        con_cliente.Cerrarconexion();
                    }
                    catch (Exception exsCliente) {
                        new filelog("consultamos los datos del cliente", exsCliente.StackTrace.ToString());
                    }

                    try {
                        conect con_referencia = new conect();
                        String sReferencia = "select * from referencia where CasoId = " + objfuncionesdicss.validareader("CasoId", "CasoId", respuestastring3).Text + " and TipoSolicitudId= " + objfuncionesdicss.validareader("TipoSolicitudId", "TipoSolicitudId", respuestastring3).Text + " ; ";
                        MySqlDataReader resp_datosreferencia = con_referencia.getdatareader(sReferencia);
                        while (resp_datosreferencia.Read())
                        {
                            lReferencia_texbox.Text = objfuncionesdicss.validareader("ReferenciaNombre", "CasoId", resp_datosreferencia).Text;//consultar referencia
                        }
                        resp_datosreferencia.Close();
                        con_referencia.Cerrarconexion();
                    }
                    catch (Exception Exsreferencia) {
                        new filelog("Consultamos la referencia", Exsreferencia.StackTrace.ToString());
                    }

                    try {
                        lTitular_texbox.Text = objfuncionesdicss.validareader("interesados", "CasoId", respuestastring3).Text;
                        conect con_estatuscaso = new conect();
                        String sEstatuscaso = "select * from estatuscaso where EstatusCasoId = " + objfuncionesdicss.validareader("EstatusCasoId", "CasoId", respuestastring3).Text;
                        MySqlDataReader resp_estatuscaso = con_estatuscaso.getdatareader(sEstatuscaso);
                        while (resp_estatuscaso.Read())
                        {
                            //lReferencia_texbox.Text = objfuncionesdicss.validareader("ReferenciaNombre", "CasoId", resp_datosreferencia).Text;//consultar referencia
                            tbEstatus.Text = objfuncionesdicss.validareader("EstatusCasoDescrip", "EstatusCasoId", resp_estatuscaso).Text;//consultar estatus
                            tbEstatus_header.Text = objfuncionesdicss.validareader("EstatusCasoDescrip", "EstatusCasoId", resp_estatuscaso).Text;//consultar estatus
                        }
                        resp_estatuscaso.Close();
                        con_estatuscaso.Cerrarconexion();
                    }
                    catch (Exception exsEstatus) {
                        new filelog("Consultamos el Estatus linea 833", exsEstatus.StackTrace.ToString());
                    }


                    try {
                        conect con_tiposolicitud = new conect();
                        String sTiposolicitud = "select * from tiposolicitud where tiposolicitudId = " + objfuncionesdicss.validareader("tiposolicitudId", "CasoId", respuestastring3).Text;
                        MySqlDataReader resp_tiposolicitud = con_tiposolicitud.getdatareader(sTiposolicitud);
                        while (resp_tiposolicitud.Read())
                        {
                            tbTipo.Text = objfuncionesdicss.validareader("TipoSolicitudDescrip", "tiposolicitudId", resp_tiposolicitud).Text;//consultar Tiposolicitud
                            sTiposolicitudGlobal = objfuncionesdicss.validareader("tiposolicitudId", "tiposolicitudId", resp_tiposolicitud).Text;//consultar Tiposolicitud
                                                                                                                                                 //tbTipo.Text = objfuncionesdicss.validareader("TipoSolicitudDescrip", "CasoId", respuestastring3).Text;//consultar Tiposolicitud

                        }
                        resp_tiposolicitud.Close();
                        con_tiposolicitud.Cerrarconexion();
                    }
                    catch (Exception ExsTiposol) {
                        new filelog("Consultamos Tiposolicitud linea 851: ", ExsTiposol.StackTrace.ToString());
                    }

                    try {
                        tbExpediente.Text = objfuncionesdicss.validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                        rtTitulo.Text = objfuncionesdicss.validareader("CasoTituloespanol", "CasoId", respuestastring3).Text;
                        tbTituloheader.Text = objfuncionesdicss.validareader("CasoTituloespanol", "CasoId", respuestastring3).Text + "  " + objfuncionesdicss.validareader("CasoTituloingles", "CasoId", respuestastring3).Text;
                        rtTituloidionaoriginal.Text = objfuncionesdicss.validareader("CasoTituloingles", "CasoId", respuestastring3).Text;
                    }
                    catch (Exception ExsDatoscaso) {
                        new filelog("Consultamos datos del caso linea 873", ExsDatoscaso.StackTrace.ToString());
                    }

                    

                    //conect con_idiomas= new conect();
                    //String sIdiomas = "select * from Idioma";
                    //MySqlDataReader resp_idiomas = con_idiomas.getdatareader(sIdiomas);
                    //while (resp_idiomas.Read())
                    //{
                    //    cbIdioma.Items.Add(objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idiomas));//consultar idioma
                    //    //cbIdioma.Text = objfuncionesdicss.validareader("IdiomaDescripcion", "CasoId", respuestastring3).Text;//consultar idioma
                    //}
                    //resp_idiomas.Close();
                    //con_idiomas.Cerrarconexion();

                    //buscamos los tipos de prioridad 
                    /*conect con_tipoprioridadcat = new conect();
                    String sTipoprioridad = "select * from Idioma";
                    MySqlDataReader resp_tipoprioridad = con_tipoprioridadcat.getdatareader(sTipoprioridad);
                    while (resp_tipoprioridad.Read())
                    {
                        cbTiposolprio.Items.Add(objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_tipoprioridad));//consultar idioma
                    }
                    resp_tipoprioridad.Close();
                    con_tipoprioridadcat.Cerrarconexion();*/
                    ComboboxItem comboTipodos = new ComboboxItem();
                    comboTipodos.Text = "PCT";
                    comboTipodos.Value = "1";
                    cbTiposolprio.Items.Add(comboTipodos);

                    ComboboxItem comboTipotres = new ComboboxItem();
                    comboTipotres.Text = "París";
                    comboTipotres.Value = "2";
                    cbTiposolprio.Items.Add(comboTipotres);

                    try {
                        //buscamos el catálogo de paises 
                        conect con_paises = new conect();
                        String sPaisese = "select * from pais order by PaisNombre asc"; ;
                        MySqlDataReader resp_paises = con_paises.getdatareader(sPaisese);
                        while (resp_paises.Read())
                        {
                            cbNombrepais.Items.Add(objfuncionesdicss.validareader("PaisNombre", "PaisId", resp_paises));
                            cbCvpais.Items.Add(objfuncionesdicss.validareader("PaisClave", "PaisId", resp_paises));
                            paises[Convert.ToInt32(objfuncionesdicss.validareader("PaisClave", "PaisId", resp_paises).Value.ToString())] = objfuncionesdicss.validareader("PaisClave", "PaisId", resp_paises).Text;
                            paisesclave[Convert.ToInt32(objfuncionesdicss.validareader("PaisClave", "PaisId", resp_paises).Value.ToString())] = objfuncionesdicss.validareader("PaisNombre", "PaisId", resp_paises).Text;
                            //lPais_texbox.Text = objfuncionesdicss.validareader("PaisNombre", "CasoId", respuestastring3).Text;//consultar pais
                        }
                        resp_paises.Close();
                        con_paises.Cerrarconexion();
                    }
                    catch (Exception ExsPais) {
                        new filelog("Consultamos país linea 925", ExsPais.StackTrace.ToString()); 
                    }


                    try {
                        conect con_idiomas2 = new conect();
                        String sIdiomas2 = "select * from idioma";// where IdiomaId <> " + objfuncionesdicss.validareader("idiomaId", "CasoId", respuestastring3).Text;
                        MySqlDataReader resp_idioma2 = con_idiomas2.getdatareader(sIdiomas2);
                        while (resp_idioma2.Read())
                        {
                            ComboboxItem prueba = objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2);
                            cbIdioma.Items.Add(objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2));//consultar idioma
                            cbidiomaescrito.Items.Add(objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2));//consultar idioma
                            cbIdiomacarta.Items.Add(objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2));//consultar idioma
                                                                                                                                   //cbIdioma.Text = objfuncionesdicss.validareader("IdiomaDescripcion", "CasoId", respuestastring3).Text;//consultar idioma
                        }
                        resp_idioma2.Close();
                        con_idiomas2.Cerrarconexion();

                        String sIdiomaidcaso = objfuncionesdicss.validareader("IdiomaId", "CasoId", respuestastring3).Text;
                        Svariablelinea = "Consultamos el idioma";
                        if (sIdiomaidcaso!="") {
                            conect con_idioma = new conect();
                            String sIdioma = "select * from idioma where IdiomaId = " + sIdiomaidcaso;
                            MySqlDataReader resp_idioma = con_idioma.getdatareader(sIdioma);
                            String sIdiomadelcaso = "";
                            while (resp_idioma.Read())
                            {
                                sIdiomadelcaso = objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma).Text;
                                //ComboboxItem pruebados = (objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma));
                                //cbIdioma.Items.Add( objfuncionesdicss.validareader("IdiomaDescripcion", "IdiomaId", resp_idioma));//consultar idioma
                                //cbIdioma.Text = objfuncionesdicss.validareader("IdiomaDescripcion", "IdiomaId", resp_idioma).Text;
                                //cbIdioma.Text = objfuncionesdicss.validareader("IdiomaDescripcion", "IdiomaId", resp_idioma).Text;//consultar idioma
                            }
                            cbIdioma.Text = sIdiomadelcaso;
                            cbidiomaescrito.Text = sIdiomadelcaso;
                            cbIdiomacarta.Text = sIdiomadelcaso;
                            resp_idioma.Close();
                            con_idioma.Cerrarconexion();
                        }
                        Svariablelinea = "Despues de conusltar el idioma dentro del catch linea 968";

                    }
                    catch (Exception ExsIdioma) {
                        new filelog("Consultamos idiomas linea 932", ExsIdioma.StackTrace.ToString());
                    }

                    Svariablelinea = "Consultamos subtipodocumento linea 975";



                    try {
                        conect con_subtiposolicitud = new conect();
                        String ssubtiposolicitud = "select * from subtiposolicitud where TiposolicitudId = " + objfuncionesdicss.validareader("TiposolicitudId", "CasoId", respuestastring3).Text + " and SubTipoSolicitudId = " + objfuncionesdicss.validareader("SubTipoSolicitudId", "CasoId", respuestastring3).Text;
                        MySqlDataReader resp_subtiposolicitud = con_subtiposolicitud.getdatareader(ssubtiposolicitud);
                        while (resp_subtiposolicitud.Read())
                        {
                            tbSubtipo.Text = objfuncionesdicss.validareader("SubTipoSolicitudDescripcion", "TiposolicitudId", resp_subtiposolicitud).Text;//consultar idioma
                                                                                                                                                          //tbSubtipo.Text = objfuncionesdicss.validareader("SubTipoSolicitudDescripcion", "CasoId", respuestastring3).Text;//consultar subtiposolicitud
                        }
                        resp_subtiposolicitud.Close();
                        con_subtiposolicitud.Cerrarconexion();
                        Svariablelinea = "despues de Consultamos subtipodocumento linea 990";
                        /**/
                        tbClientduedate.Text = validafechavacia(objfuncionesdicss.validareader("CasoFechaFilingCliente", "CasoId", respuestastring3).Text);
                        tbFechaRecimpi.Text = validafechavacia(objfuncionesdicss.validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text);
                        tbFechaRecimpi_2.Text = validafechavacia(objfuncionesdicss.validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text);
                    } catch (Exception ExsSubtipo) {
                        new filelog("Consultamos subtiposolicitud linea 987", ExsSubtipo.StackTrace.ToString());
                    }

                    

                    //Consultando los tipos de prioridades
                    try {
                        conect con_tipoprioridad = new conect();
                        String sTipoct = "select * from tipopct where tipopctid = " + objfuncionesdicss.validareader("tipopctid", "CasoId", respuestastring3).Text;
                        MySqlDataReader resp_Tipopct = con_tipoprioridad.getdatareader(sTipoct);
                        while (resp_Tipopct.Read())
                        {
                            tbCapitulo.Text = objfuncionesdicss.validareader("TipoPctDescrip", "tipopctid", resp_Tipopct).Text;//consultar idioma
                                                                                                                               //tbCapitulo.Text = objfuncionesdicss.validareader("TipoPctDescrip", "CasoId", respuestastring3).Text;//tabla Tippct
                        }
                        resp_Tipopct.Close();
                        con_tipoprioridad.Cerrarconexion();
                    }
                    catch (Exception Ex) {
                        new filelog("Cnosultamos prioridades linea 1006", Ex.StackTrace.ToString());
                    }

                    Svariablelinea = "Cargamos la información del caso como plazolegal fecha divulgacion fecha carta linea 1012";
                    tbPlazolegal.Text = validafechavacia(objfuncionesdicss.validareader("CasoFechaLegal", "CasoId", respuestastring3).Text);
                    tbFechadivulgacion.Text = validafechavacia(objfuncionesdicss.validareader("CasoFechaDivulgacionPrevia", "CasoId", respuestastring3).Text);
                    tbFechacarta.Text = validafechavacia(objfuncionesdicss.validareader("CasoFechaCartaCliente", "CasoId", respuestastring3).Text);
                    tbFechainternacional.Text = validafechavacia(objfuncionesdicss.validareader("CasoFechaInternacional", "CasoId", respuestastring3).Text);
                    tbFechainternacional_2.Text = validafechavacia(objfuncionesdicss.validareader("CasoFechaInternacional", "CasoId", respuestastring3).Text);

                    conect con_contacto = new conect();
                    String sdatoscontacto = "select contacto.ContactoNombre, contacto.ContactoEmail, contacto.contactoid "+
                        " from contacto, casocliente, caso_patente "+ 
                        " where contacto.contactoid = casocliente.contactoid "+
                        " AND caso_patente.casoid = casocliente.casoid "+
                        " and casocliente.casoid = " + objfuncionesdicss.validareader("CasoId", "CasoId", respuestastring3).Text + " "+
                        " and casocliente.TipoSolicitudId =  "+ objfuncionesdicss.validareader("TipoSolicitudId", "CasoId", respuestastring3).Text;
                    MySqlDataReader resp_sdatoscontacto = con_contacto.getdatareader(sdatoscontacto);
                    while (resp_sdatoscontacto.Read())
                    {
                        lContacto_texbox.Text = objfuncionesdicss.validareader("ContactoNombre", "ContactoNombre", resp_sdatoscontacto).Text;//consultar idioma
                        rtCorreocontacto.Text = objfuncionesdicss.validareader("ContactoEmail", "ContactoEmail", resp_sdatoscontacto).Text;//consultar idioma
                        gSContactoid = objfuncionesdicss.validareader("contactoid", "contactoid", resp_sdatoscontacto).Text;//consultar idioma
                        //contacto.contactoid
                        //lContacto_texbox.Text = objfuncionesdicss.validareader("ContactoNombre", "CasoId", respuestastring3).Text;//tabla contacto
                        //lContacto_texbox.Text = objfuncionesdicss.validareader("ContactoNombre", "CasoId", respuestastring3).Text;//tabla contacto
                        //rtCorreocontacto.Text = objfuncionesdicss.validareader("ContactoEmail", "CasoId", respuestastring3).Text;//tabla contatco
                        //rtCorreocontacto.Text = objfuncionesdicss.validareader("ContactoEmail", "CasoId", respuestastring3).Text;//tabla contatco
                    }
                    resp_sdatoscontacto.Close();
                    con_contacto.Cerrarconexion();

                    conect con_responsable = new conect();
                    String sdatos_responsable = "select * from usuario where UsuarioId =" + objfuncionesdicss.validareader("UsuarioId", "UsuarioId", respuestastring3).Text;
                    MySqlDataReader resp_sdatosresponsable = con_responsable.getdatareader(sdatos_responsable);
                    while (resp_sdatosresponsable.Read())
                    {
                        //UsuarioPaterno
                        String sNombreresponsable = objfuncionesdicss.validareader("UsuarioNombre", "UsuarioNombre", resp_sdatosresponsable).Text;
                        String sPaternoresponsable = objfuncionesdicss.validareader("UsuarioPaterno", "UsuarioPaterno", resp_sdatosresponsable).Text;
                        tbResponsable.Text = sNombreresponsable + " " + sPaternoresponsable;
                        //lbresponsable.Text = sNombreresponsable + " " + sPaternoresponsable;
                    }
                    resp_sdatosresponsable.Close();
                    con_responsable.Cerrarconexion();
                    Svariablelinea = "Consultamos los usuarios o la información del responsable linea 1049";


                    tbRegistro.Text = objfuncionesdicss.validareader("CasoNumConcedida", "CasoId", respuestastring3).Text;
                    tbFechaconcesion.Text = validafechavacia(objfuncionesdicss.validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text);//fechaconsesion 
                    tbFechaconcesion_2.Text = validafechavacia(objfuncionesdicss.validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text);//fechaconsesion 

                    tbFechavigencia.Text = validafechavacia(objfuncionesdicss.validareader("CasoFechaVigencia", "CasoId", respuestastring3).Text);
                    tbFechavigencia__2.Text = validafechavacia(objfuncionesdicss.validareader("CasoFechaVigencia", "CasoId", respuestastring3).Text);
                    tbFechapublicacion.Text = validafechavacia(objfuncionesdicss.validareader("CasoFechaPublicacionSolicitud", "CasoId", respuestastring3).Text);

                    lCasoNumero_texbox.Text = objfuncionesdicss.validareader("CasoNumero", "CasoId", respuestastring3).Text;


                    if (tbFechainternacional.Text != "")
                    {
                        tbFechalegalanualidades.Text = tbFechainternacional.Text;
                    }
                    else {
                        tbFechalegalanualidades.Text = tbFechaRecimpi.Text;
                    }
                    iContvalida++;

                    Svariablelinea = "Asignamos los valores de la consulta a los texbox linea 1072";
                }
                respuestastring3.Close();
                con_datoscaso.Cerrarconexion();
                /*Calculamos y mostramos los plazos*/
                try { 
                //conect con2_plazos = new conect();
                //String squeryplazos = "SELECT * FROM plazo where CasoId = " + sCasoiddom + ";";
                /*cabios*/
                String sConsultaplazos = " SELECT  " +
                                                "     c.CasoNumero, " +
                                                "     c.casoId, " +
                                                "     cl.NombreUtilClient, " +
                                                "     c.CasoTitular, " +
                                                "     d.DocumentoFechaRecepcion, " +
                                                "     e.EstatusCasoDescrip, " +
                                                "     d.DocumentoId, " +
                                                "     ep.EstatusPlazoDescrip, " +
                                                "     st.SubTipoDocumentoDescrip, " +
                                                "     d.DocumentoCodigoBarras, " +
                                                "     d.SubTipoDocumentoId, " +
                                                "     tp.TipoPlazoDescrip, " +
                                                "     tp.TipoPlazoId," +
                                                "     Get_Tipodesolicitud(c.casoid) as figura," +
                                                "     DAMEELUSUARIO(d.UsuarioId) AS usuario_capturo, " +
                                                "     Dameelusuario(p.UsuarioId) as usuario_responsable, " +
                                                "     GetNombrecontactobyclienteid(cc.ClienteId) as contactosdelcliente," +
                                                "     Get_referenciasclientefromcasoidtiposolid(c.casoId, c.TipoSolicitudId) as referenciascliente," +
                                                "     c.CasoDenominacion," +
                                                "     c.CasoNumeroExpedienteLargo," +
                                                "     Get_Interesados_tiposol(c.casoId, c.TipoSolicitudId) as interesados," +
                                                "     e.estatuscasoid," +
                                                "     tp.TipoTareaId," +
                                                "     tptar.TipoTareaDescrip," +
                                                "     tp.Grupoid, " +
                                                "     TIMESTAMPDIFF(DAY, d.DocumentoFechaRecepcion, now()) as diferenciafecha, " +
                                                "     TIMESTAMPDIFF(DAY, d.DocumentoFechaRecepcion, p.PlazoFechaAtencion) as diferenciafechaatendido, " +
                                                "     P.* " +
                                                " FROM " +
                                                "     documento d " +
                                                "         JOIN " +
                                                "     subtipodocumento st ON st.SubTipoDocumentoId = d.SubTipoDocumentoId " +
                                                "         JOIN " +
                                                "     relaciondocumento rd ON rd.DocumentoId = d.DocumentoId " +
                                                "         JOIN " +
                                                "     caso c ON c.CasoId = rd.CasoId " +
                                                "         JOIN " +
                                                "     casocliente cc ON cc.CasoId = c.CasoId " +
                                                "         JOIN " +
                                                "     cliente cl ON cl.ClienteId = cc.ClienteId " +
                                                "         JOIN " +
                                                "     estatuscaso e ON e.estatuscasoid = c.estatuscasoid " +
                                                "         JOIN " +
                                                "     plazo p ON p.CasoId = c.CasoId " +
                                                "         JOIN " +
                                                "     estatusplazo ep ON ep.EstatusPlazoId = p.EstatusPlazoId " +
                                                "         JOIN " +
                                                "     tipoplazo tp ON tp.TipoPlazoId = p.TipoPlazoId " +
                                                "         JOIN " +
                                                "     tipotarea tptar ON tptar.TipoTareaId = tp.TipoTareaId " +
                                                " WHERE p.casoid = " + sCasoiddom +" group by p.plazoid";
                //"limit 500";
                //contamos los plazos
                int iNum = 0;
                String [,]sArray; //= new String[iNum, 29];
                //Fin contamos los plazos
                conect conin_plazos = new conect();
                //MySqlDataReader respuestastrig_plazos = conin_plazos.getdatareader(sConsultaplazos);
                int iRows = 0;
                    /*using (loadinprocess form = new loadinprocess(consultamoslosplazos))
                    {
                        form.ShowDialog();
                    }*/
                    //while (respuestastrig_plazos.Read())
                    //{
                    //    //String[] saRow = new String[28];
                    //    String sNumplazos = objfuncionesdicss.validareader("numplazos", "CasoId", respuestastrig_plazos).Text;
                    //    /*iNum = Int32.Parse(sNumplazos);
                    //    sArray = new String[iNum, 29];*/
                    //    String sCasoNumero = objfuncionesdicss.validareader("CasoNumero", "CasoId", respuestastrig_plazos).Text;
                    //    String scasoId = objfuncionesdicss.validareader("casoId", "CasoId", respuestastrig_plazos).Text;
                    //    String sNombreUtilClient = objfuncionesdicss.validareader("NombreUtilClient", "CasoId", respuestastrig_plazos).Text;
                    //    String sCasoTitular = objfuncionesdicss.validareader("CasoTitular", "CasoId", respuestastrig_plazos).Text;
                    //    String sDocumentoFechaRecepcion = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "CasoId", respuestastrig_plazos).Text.Substring(0, 10);
                    //    String sEstatusCasoDescrip = objfuncionesdicss.validareader("EstatusCasoDescrip", "CasoId", respuestastrig_plazos).Text;
                    //    String sDocumentoId = objfuncionesdicss.validareader("DocumentoId", "CasoId", respuestastrig_plazos).Text;
                    //    String sDocumentoCodigoBarras = objfuncionesdicss.validareader("DocumentoCodigoBarras", "CasoId", respuestastrig_plazos).Text;
                    //    String sSubTipoDocumentoDescrip = objfuncionesdicss.validareader("SubTipoDocumentoDescrip", "CasoId", respuestastrig_plazos).Text;
                    //    String sEstatusPlazoDescrip = objfuncionesdicss.validareader("EstatusPlazoDescrip", "CasoId", respuestastrig_plazos).Text;
                    //    String sSubTipoDocumentoId = objfuncionesdicss.validareader("SubTipoDocumentoId", "CasoId", respuestastrig_plazos).Text;
                    //    String sTipoPlazoDescrip = objfuncionesdicss.validareader("TipoPlazoDescrip", "CasoId", respuestastrig_plazos).Text;
                    //    String sTipoPlazoId = objfuncionesdicss.validareader("TipoPlazoId", "CasoId", respuestastrig_plazos).Text;
                    //    String sPlazoFecha = objfuncionesdicss.validareader("PlazoFecha", "CasoId", respuestastrig_plazos).Text.Substring(0,10);
                    //    String sfigura = objfuncionesdicss.validareader("figura", "CasoId", respuestastrig_plazos).Text;
                    //    String susuario_capturo = objfuncionesdicss.validareader("usuario_capturo", "CasoId", respuestastrig_plazos).Text;
                    //    String susuario_responsable = objfuncionesdicss.validareader("usuario_responsable", "CasoId", respuestastrig_plazos).Text;
                    //    String scontactosdelcliente = objfuncionesdicss.validareader("contactosdelcliente", "CasoId", respuestastrig_plazos).Text;
                    //    String sreferenciascliente = objfuncionesdicss.validareader("referenciascliente", "CasoId", respuestastrig_plazos).Text;
                    //    String sCasoDenominacion = objfuncionesdicss.validareader("CasoDenominacion", "CasoId", respuestastrig_plazos).Text;
                    //    String sCasoNumeroExpedienteLargo = objfuncionesdicss.validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastrig_plazos).Text;
                    //    String sinteresados = objfuncionesdicss.validareader("interesados", "CasoId", respuestastrig_plazos).Text;
                    //    String sestatuscasoid = objfuncionesdicss.validareader("estatuscasoid", "CasoId", respuestastrig_plazos).Text;
                    //    String sTipoTareaId = objfuncionesdicss.validareader("TipoTareaId", "CasoId", respuestastrig_plazos).Text;
                    //    String sGrupoid = objfuncionesdicss.validareader("Grupoid", "CasoId", respuestastrig_plazos).Text;
                    //    String sdiferenciafecha = objfuncionesdicss.validareader("diferenciafecha", "CasoId", respuestastrig_plazos).Text;
                    //    String sPlazoMotivoCancelacion = objfuncionesdicss.validareader("PlazoMotivoCancelacion", "CasoId", respuestastrig_plazos).Text;
                    //    //String susuario_cancelo = objfuncionesdicss.validareader("usuario_cancelo", "CasoId", respuestastrig_plazos).Text;
                    //    String sPlazoFechaAtencion = objfuncionesdicss.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", respuestastrig_plazos).Text;
                    //    String splazoid = objfuncionesdicss.validareader("plazoid", "plazoid", respuestastrig_plazos).Text;
                    //    String sTipoSolicitudId = objfuncionesdicss.validareader("TipoSolicitudId", "plazoid", respuestastrig_plazos).Text;
                    //    String sTipoTareaDescrip = objfuncionesdicss.validareader("TipoTareaDescrip", "plazoid", respuestastrig_plazos).Text;
                    //    String sPlazoFechaProrroga = objfuncionesdicss.validareader("PlazoFechaProrroga", "plazoid", respuestastrig_plazos).Text;
                    //    String sUsuarioIdCancelo = objfuncionesdicss.validareader("UsuarioIdCancelo", "plazoid", respuestastrig_plazos).Text;
                    //    String sdiferenciafechaatendido = objfuncionesdicss.validareader("diferenciafechaatendido", "plazoid", respuestastrig_plazos).Text;
                    //    //diferenciafechaatendido

                    //    ListViewItem items = new ListViewItem(splazoid);//plazoid
                    //    items.SubItems.Add(susuario_capturo);//capturo
                    //    items.SubItems.Add(sTipoPlazoDescrip);//tipo plazo
                    //    items.SubItems.Add(sTipoTareaDescrip);//descripcion tarea
                    //    items.SubItems.Add(sSubTipoDocumentoDescrip);
                    //    items.SubItems.Add(sDocumentoFechaRecepcion);
                    //    items.SubItems.Add(sPlazoFecha);
                    //    items.SubItems.Add(sPlazoFechaAtencion);
                    //    items.SubItems.Add("aviso cliente");
                    //    items.SubItems.Add(sEstatusPlazoDescrip);
                    //    if (sdiferenciafechaatendido!="")
                    //    {
                    //        int Mesatendido = Int32.Parse(sdiferenciafechaatendido) / 30;
                    //        items.SubItems.Add(Mesatendido+"");
                    //    }else{
                    //        int Mesatendido = Int32.Parse(sdiferenciafecha) / 30;
                    //        items.SubItems.Add(Mesatendido + "");
                    //    }

                    //    items.SubItems.Add(sPlazoFecha);
                    //    items.SubItems.Add(sPlazoFechaAtencion);
                    //    items.SubItems.Add(sPlazoFechaProrroga);
                    //    items.SubItems.Add("");
                    //    items.SubItems.Add(sPlazoMotivoCancelacion);
                    //    lvPlazos.Items.Add(items);

                    //    ListViewItem items = new ListViewItem(objfuncionesdicss.validareader("plazoid", "plazoid", resp_plazos).Text);//plazoid
                    //    items.SubItems.Add(objfuncionesdicss.validareader("usuarioIdAtendio", "plazoid", resp_plazos).Text);//capturo
                    //    items.SubItems.Add(objfuncionesdicss.validareader("TipoPlazoId", "plazoid", resp_plazos).Text);//tipoplazo
                    //    items.SubItems.Add(objfuncionesdicss.validareader("TipoPlazoId", "plazoid", resp_plazos).Text);//descripcion tarea
                    //    items.SubItems.Add(objfuncionesdicss.validareader("DocumentoId", "plazoid", resp_plazos).Text);//Documento
                    //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFechaAtencion", "plazoid", resp_plazos).Text);//Notificado en
                    //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFecha", "plazoid", resp_plazos).Text);//Vencimiento original
                    //    items.SubItems.Add("2 meses");//Fecha escrito
                    //    items.SubItems.Add("3 meses");//Fecha escrito
                    //    items.SubItems.Add("mes");//mes
                    //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFechaAtencion", "plazoid", resp_plazos).Text);//Fecha escrito
                    //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFechaProrroga", "plazoid", resp_plazos).Text);//Aviso cliente
                    //    items.SubItems.Add(objfuncionesdicss.validareader("EstatusPlazoId", "plazoid", resp_plazos).Text);//Estatus 
                    //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFechaProrroga", "plazoid", resp_plazos).Text);//plazo final
                    //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFechaAtencion", "plazoid", resp_plazos).Text);//Fecha atención
                    //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFechaProrroga", "plazoid", resp_plazos).Text);//Fecha prorroga
                    //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoMotivoCancelacion", "plazoid", resp_plazos).Text);//prorrogó o canceló
                    //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoMotivoCancelacion", "plazoid", resp_plazos).Text);//Motivo cancelación
                    //    lvPlazos.Items.Add(items);
                    //    //lvdocumentosimpi.Items.Add(items);


                    //}
                    /*fin de cambios*/
                    Svariablelinea = "Al buscar los plazos 1241";
                }
                catch (Exception E)
                {
                    MessageBox.Show("Revisar plazos");
                }
                Svariablelinea = "Salimos del while de la consulta de la información del caso y consultamos los plazos, linea 1243";
                //MySqlDataReader resp_plazos = con2_plazos.getdatareader(squeryplazos);
                //lvPlazos.Items.Clear();
                //while (resp_plazos.Read())
                //{
                //    //lvdocumentosimpi 
                //    //debemos consultar los datos de documento para saber los demás varoles
                //    ListViewItem items = new ListViewItem(objfuncionesdicss.validareader("plazoid", "plazoid", resp_plazos).Text);//plazoid
                //    items.SubItems.Add(objfuncionesdicss.validareader("usuarioIdAtendio", "plazoid", resp_plazos).Text);//capturo
                //    items.SubItems.Add(objfuncionesdicss.validareader("TipoPlazoId", "plazoid", resp_plazos).Text);//tipoplazo
                //    items.SubItems.Add(objfuncionesdicss.validareader("TipoPlazoId", "plazoid", resp_plazos).Text);//descripcion tarea
                //    items.SubItems.Add(objfuncionesdicss.validareader("DocumentoId", "plazoid", resp_plazos).Text);//Documento
                //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFechaAtencion", "plazoid", resp_plazos).Text);//Notificado en
                //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFecha", "plazoid", resp_plazos).Text);//Vencimiento original
                //    items.SubItems.Add("2 meses");//Fecha escrito
                //    items.SubItems.Add("3 meses");//Fecha escrito
                //    items.SubItems.Add("mes");//mes
                //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFechaAtencion", "plazoid", resp_plazos).Text);//Fecha escrito
                //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFechaProrroga", "plazoid", resp_plazos).Text);//Aviso cliente
                //    items.SubItems.Add(objfuncionesdicss.validareader("EstatusPlazoId", "plazoid", resp_plazos).Text);//Estatus 
                //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFechaProrroga", "plazoid", resp_plazos).Text);//plazo final
                //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFechaAtencion", "plazoid", resp_plazos).Text);//Fecha atención
                //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoFechaProrroga", "plazoid", resp_plazos).Text);//Fecha prorroga
                //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoMotivoCancelacion", "plazoid", resp_plazos).Text);//prorrogó o canceló
                //    items.SubItems.Add(objfuncionesdicss.validareader("PlazoMotivoCancelacion", "plazoid", resp_plazos).Text);//Motivo cancelación
                //    lvPlazos.Items.Add(items);
                //    //lvdocumentosimpi.Items.Add(items);
                //}
                //resp_plazos.Close();
                //con2_plazos.Cerrarconexion();
                /*Fin Calculamos y mostramos los plazos*/

                if (iContvalida==0)
                {
                    lCasoNumero_texbox.Text = "";
                    lExpediente_texbox.Text = "";
                    lRegistro_texbox.Text = "";
                    lPais_texbox.Text = "";
                    lCliente_texbox_.Text = "";
                    lTitular_texbox.Text = "";
                    lReferencia_texbox.Text = "";
                    tbEstatus.Text = "";
                    tbEstatus_header.Text = "";
                    tbTipo.Text = "";
                    tbExpediente.Text = "";
                    rtTitulo.Text = "";
                    rtTituloidionaoriginal.Text = "";
                    cbIdioma.Text = "";
                    cbidiomaescrito.Text = "";
                    cbIdiomacarta.Text= "";
                    tbSubtipo.Text = "";

                    tbClientduedate.Text = "";
                    tbFechaRecimpi.Text = "";
                    tbCapitulo.Text = "";
                    tbPlazolegal.Text = "";

                    tbFechadivulgacion.Text = "";
                    tbFechacarta.Text = "";
                    tbFechainternacional.Text = "";
                    lContacto_texbox.Text = "";
                    rtCorreocontacto.Text = "";
                    lbresponsable.Text = "";
                    tbRegistro.Text = "";
                    tbFechaconcesion.Text = "";
                    tbFechavigencia.Text = "";
                    tbFechapublicacion.Text = "";
                    lCasoNumero_texbox.Text = "";
                    /*lvPrioridades.Clear();
                    lvinteresados.Clear();*/
                    lvinteresados.Items.Clear();
                    lvPrioridades.Items.Clear();
                    //listViewDivicional.Items.Clear();
                }

                tb_numtit.Text = "0";
                tb_inv.Text = "0";
                tb_titinv.Text = "0";
                ////iniciamos la consulta del interesado
                //try {
                //    conect con_casointeresado_count = new conect();
                //    String sInteresados_count = " select count(*)as numtotaldeinteresados " +
                //                                " FROM " +
                //                                "     casointeresado, " +
                //                                "     interesado, " +
                //                                //"     direccion,  " +
                //                                "     tiporelacion " +
                //                                " WHERE " +
                //                                "     casointeresado.CasoId = '" + sCasoiddom + "'" +
                //                                " AND interesado.InteresadoID = casointeresado.InteresadoId " +
                //                                " AND tiporelacion.TipoRelacionId = casointeresado.TipoRelacionId " +
                //        //" AND interesado.InteresadoID = direccion.InteresadoId "+
                //                                "GROUP BY interesado.InteresadoID order by casointeresado.CasoInteresadoSecuencia; ";
                //    MySqlDataReader respuestastringinteresados_count = con_casointeresado_count.getdatareader(sInteresados_count);
                //    int iCount_tam = 0;
                //    while (respuestastringinteresados_count.Read())
                //    {
                //        iCount_tam++;
                //    }
                //    TipoRelacionId = new String[iCount_tam];
                //    InteresadoCurp = new String[iCount_tam];
                //    InteresadoNombre = new String[iCount_tam];
                //    InteresadoApPaterno = new String[iCount_tam];
                //    InteresadoApMaterno = new String[iCount_tam];
                //    nacionalidad = new String[iCount_tam];
                //    InteresadoRFC = new String[iCount_tam];

                //    sgDireccionCalle = new String[iCount_tam];
                //    sgDireccionNumExt = new String[iCount_tam];
                //    sgDireccionNumInt = new String[iCount_tam];
                //    sgDireccionColonia = new String[iCount_tam];
                //    sgDireccionCP = new String[iCount_tam];
                //    sgDireccionEstado = new String[iCount_tam];
                //    sgDireccionPoblacion = new String[iCount_tam];
                //    sgNombrepais = new String[iCount_tam];

                //    respuestastringinteresados_count.Close();
                //    con_casointeresado_count.Cerrarconexion();

                //    actualizatablainteresado();
                //    conect con_casointeresado = new conect();
                //    String sInteresados = " SELECT  " +
                //                        "     casointeresado.CasoId, " +
                //                        "     casointeresado.TipoRelacionId, " +
                //                        "     casointeresado.CasoInteresadoSecuencia, " +
                //                        "     interesado.InteresadoID, " +
                //                        "     interesado.InteresadoTipoPersonaSAT, " +
                //                        "     interesado.InteresadoNombre, " +
                //                        "     interesado.InteresadoApPaterno, " +
                //                        "     interesado.InteresadoApMaterno, " +
                //                        "     interesado.InteresadoRFC, " +
                //                        "     interesado.InteresadoCurp, " +
                //                        " CONCAT ( COALESCE(interesado.InteresadoNombre, ''  ), ' ', " +
                //                        " COALESCE(interesado.InteresadoApPaterno, ''), ' ', " +
                //                        " COALESCE(interesado.InteresadoApMaterno, '')) AS interesadonombrecompleto, " +


                //                        " CONCAT (  COALESCE(Dame_direccion_DireccionCalle(interesado.InteresadoID), ''  ), ' ',  " +
                //                        " COALESCE(Dame_direccion_DireccionNumExt(interesado.InteresadoID), ''), ' ', " +
                //                        " COALESCE(Dame_direccion_DireccionNumInt(interesado.InteresadoID), ''), ' ', " +
                //                        " COALESCE(Dame_direccion_DireccionColonia(interesado.InteresadoID), ''), ' ', " +
                //                        " COALESCE(Dame_direccion_DireccionPoblacion(interesado.InteresadoID), ''), ' ', " +
                //                        " COALESCE(Dame_direccion_DireccionEstado(interesado.InteresadoID), ''), ' ', " +
                //                        " COALESCE(Dame_direccion_DireccionCP(interesado.InteresadoID), '')) AS direccioncompleta, " +

                //                        "     Dame_direccion_DireccionCalle(interesado.InteresadoID) as DireccionCalle, " +
                //                        "     Dame_direccion_DireccionNumExt(interesado.InteresadoID) as DireccionNumExt, " +
                //                        "     Dame_direccion_DireccionNumInt(interesado.InteresadoID) as DireccionNumInt, " +
                //                        "     Dame_direccion_DireccionColonia(interesado.InteresadoID) as DireccionColonia, " +
                //                        "     Dame_direccion_DireccionCP(interesado.InteresadoID) as DireccionCP, " +
                //                        "     Dame_direccion_DireccionEstado(interesado.InteresadoID) as DireccionEstado, " +
                //                        "     Dame_direccion_DireccionPoblacion(interesado.InteresadoID) as DireccionPoblacion, " +


                //                        " Damelanacionalidad (interesado.PaisId )AS nacionalidad, " +//DameNombrePais
                //                        " DameNombrePais (Dame_direccion_PaisId(interesado.InteresadoID))AS Nombrepais, " +//DameNombrePais
                //                        "     interesado.PaisId, " +
                //                        "     tiporelacion.TipoRelacionDescrip, " +
                //                        "     interesado.InteresadoPoder, " +
                //                        "     interesado.InteresadoRGP " +
                //                        " FROM " +
                //                        "     casointeresado, " +
                //                        "     interesado, " +
                //        //"     caso,  " +
                //        //  "     pais,  " +
                //                        "     tiporelacion " +
                //                        " WHERE " +
                //                        "     casointeresado.CasoId = '" + sCasoiddom + "'" +
                //                        " AND interesado.InteresadoID = casointeresado.InteresadoId " +
                //                        " AND tiporelacion.TipoRelacionId = casointeresado.TipoRelacionId " +
                //        //"     AND casointeresado.CasoId = caso.CasoId " +
                //        // "     AND pais.PaisId = interesado.PaisId " +
                //                       // "     AND interesado.InteresadoID = direccion.InteresadoId "+
                //                        "GROUP BY interesado.InteresadoID order by casointeresado.CasoInteresadoSecuencia; ";
                //    MySqlDataReader respuestastringinteresados = con_casointeresado.getdatareader(sInteresados);
                //    lvinteresados.Items.Clear();
                //    int count = 0;
                //    String TIPOPERSONA = "";
                //    int iContadortitula = 0;
                //    int iContadorinventor = 0;
                //    int iContadorinventortitular = 0;

                //    while (respuestastringinteresados.Read())
                //    {

                //        TipoRelacionId[count] = objfuncionesdicss.validareader("TipoRelacionId", "InteresadoID", respuestastringinteresados).Text;
                //        InteresadoCurp[count] = objfuncionesdicss.validareader("InteresadoCurp", "InteresadoID", respuestastringinteresados).Text;
                //        InteresadoNombre[count] = objfuncionesdicss.validareader("InteresadoNombre", "InteresadoID", respuestastringinteresados).Text;
                //        InteresadoApPaterno[count] = objfuncionesdicss.validareader("InteresadoApPaterno", "InteresadoID", respuestastringinteresados).Text;
                //        InteresadoApMaterno[count] = objfuncionesdicss.validareader("InteresadoApMaterno", "InteresadoID", respuestastringinteresados).Text;
                //        nacionalidad[count] = objfuncionesdicss.validareader("nacionalidad", "InteresadoID", respuestastringinteresados).Text;
                //        InteresadoRFC[count] = objfuncionesdicss.validareader("InteresadoRFC", "InteresadoID", respuestastringinteresados).Text;

                //        sgDireccionCalle[count] = objfuncionesdicss.validareader("DireccionCalle", "InteresadoID", respuestastringinteresados).Text;
                //        sgDireccionNumExt[count] = objfuncionesdicss.validareader("DireccionNumExt", "InteresadoID", respuestastringinteresados).Text;
                //        sgDireccionNumInt[count] = objfuncionesdicss.validareader("DireccionNumInt", "InteresadoID", respuestastringinteresados).Text;
                //        sgDireccionColonia[count] = objfuncionesdicss.validareader("DireccionColonia", "InteresadoID", respuestastringinteresados).Text;
                //        sgDireccionCP[count] = objfuncionesdicss.validareader("DireccionCP", "InteresadoID", respuestastringinteresados).Text;
                //        sgDireccionEstado[count] = objfuncionesdicss.validareader("DireccionEstado", "InteresadoID", respuestastringinteresados).Text;
                //        sgDireccionPoblacion[count] = objfuncionesdicss.validareader("DireccionPoblacion", "InteresadoID", respuestastringinteresados).Text;


                //        sgNombrepais[count] = objfuncionesdicss.validareader("Nombrepais", "InteresadoID", respuestastringinteresados).Text;

                //        switch (objfuncionesdicss.validareader("InteresadoTipoPersonaSAT", "InteresadoID", respuestastringinteresados).Text)
                //        {
                //            case "FN":
                //                TIPOPERSONA = "Física Nacional";
                //                break;
                //            case "FE":
                //                TIPOPERSONA = "Física Extranjera";
                //                break;
                //            case "MN":
                //                TIPOPERSONA = "Moral Nacional";
                //                break;
                //            case "ME":
                //                TIPOPERSONA = "Moral Extranjera";
                //                break;
                //        }

                //        switch (objfuncionesdicss.validareader("TipoRelacionDescrip", "InteresadoID", respuestastringinteresados).Text)
                //        {
                //            case "Titular":
                //                {
                //                    iContadortitula++;
                //                } break;
                //            case "Inventor":
                //                {
                //                    iContadorinventor++;
                //                } break;
                //            case "Titular/Inventor":
                //                {
                //                    iContadorinventortitular++;
                //                } break;
                //        }
                //        ListViewItem listinteresados = new ListViewItem(objfuncionesdicss.validareader("TipoRelacionDescrip", "InteresadoID", respuestastringinteresados).Text);
                //        listinteresados.SubItems.Add(objfuncionesdicss.validareader("InteresadoID", "InteresadoID", respuestastringinteresados).Text);
                //        listinteresados.SubItems.Add(objfuncionesdicss.validareader("interesadonombrecompleto", "InteresadoID", respuestastringinteresados).Text);
                //        listinteresados.SubItems.Add(objfuncionesdicss.validareader("nacionalidad", "InteresadoID", respuestastringinteresados).Text);
                //        listinteresados.SubItems.Add(objfuncionesdicss.validareader("direccioncompleta", "InteresadoID", respuestastringinteresados).Text);
                //        listinteresados.SubItems.Add(objfuncionesdicss.validareader("InteresadoPoder", "InteresadoID", respuestastringinteresados).Text);
                //        listinteresados.SubItems.Add(objfuncionesdicss.validareader("InteresadoRGP", "InteresadoID", respuestastringinteresados).Text);
                //        listinteresados.SubItems.Add(TIPOPERSONA);
                //        int residuo = count % 2;
                //        if (residuo == 0)
                //        {
                //            listinteresados.BackColor = Color.LightGray;
                //        }
                //        else
                //        {
                //            listinteresados.BackColor = Color.Azure;
                //        }
                //        lvinteresados.Items.Add(listinteresados);
                //        lvinteresados.FullRowSelect = true;
                //        count++;

                //    }
                //    //cerrar conexion
                //    respuestastringinteresados.Close();
                //    con_casointeresado.Cerrarconexion();
                //    tb_numtit.Text = iContadortitula + "";
                //    tb_inv.Text = iContadorinventor + "";
                //    tb_titinv.Text = iContadorinventortitular + "";
                //}catch(Exception Ex){
                //    new filelog(loguin.sId,"Error en el interesado"+ Ex.ToString());
                //}
                Svariablelinea = "antes de ejecutar actualizainformaciondeinteresado linea 1509";
                actualizainformaciondeinteresado();
                actualizainformaciondeinvnetores();
                Svariablelinea = "después de ejecutar actualizainformaciondeinteresado linea 1511";
                ////aqui termina la dirección



                conect con_caso_patente = new conect();
                String sQuerydivicional = "select * from caso_patente where Divicionalid = " + sCasoiddom + ";";
                MySqlDataReader resp_div = con_caso_patente.getdatareader(sQuerydivicional);
                listViewDivicional.Items.Clear();
                while (resp_div.Read())
                {   //este es un caso divicional Padre
                    
                    ListViewItem listinteresados = new ListViewItem(objfuncionesdicss.validareader("CasoId", "CasoId", resp_div).Text);
                    listinteresados.SubItems.Add(objfuncionesdicss.validareader("CasoNumero", "CasoId", resp_div).Text);
                    listinteresados.SubItems.Add(objfuncionesdicss.validareader("CasoTitular", "CasoId", resp_div).Text);
                    listinteresados.SubItems.Add(objfuncionesdicss.validareader("CasoNumeroExpedienteLargo", "CasoId", resp_div).Text);
                    listViewDivicional.Items.Add(listinteresados);
                    listViewDivicional.FullRowSelect = true;
                    lbPadre.Text = "Caso Divisional Madre";
                    button40.Enabled = false;
                    
                    lbPadre.Show();
                    //button45.Enabled = false;
                    bPadredivicional = true;
                    bAddpadre.Enabled = false;
                }
                resp_div.Close();
                con_caso_patente.Cerrarconexion();
                Svariablelinea = "Despues de conultar divicional id liena 1551";
                conect con_prioridad = new conect();
                String sQueryprioridades = " SELECT  " +
                                "     * " +
                                " FROM " +
                                "     prioridad, " +
                                "     pais, " +
                                "     tipoprioridad " +
                                " WHERE " +
                                "prioridad.CasoId = '" + sCasoiddom + "' " +
                                "     AND prioridad.PaisID = pais.PaisId " +
                                "     AND prioridad.TipoPrioridadId = tipoprioridad.TipoPrioridadId order by prioridad.TipoPrioridadId, prioridad.PrioridadFecha desc;";
                MySqlDataReader respuestastringprio = con_prioridad.getdatareader(sQueryprioridades);
                lvPrioridades.Items.Clear();
                int iCount = 0;
                while (respuestastringprio.Read())
                {
                    ListViewItem listintprior = new ListViewItem(objfuncionesdicss.validareader("PrioridadId", "PrioridadId", respuestastringprio).Text);
                    listintprior.SubItems.Add(objfuncionesdicss.validareader("PrioridadNumero", "PrioridadId", respuestastringprio).Text);
                    listintprior.SubItems.Add(validafechavacia(objfuncionesdicss.validareader("PrioridadFecha", "PrioridadId", respuestastringprio).Text));
                    listintprior.SubItems.Add(objfuncionesdicss.validareader("PaisClave", "PrioridadId", respuestastringprio).Text);
                    listintprior.SubItems.Add(objfuncionesdicss.validareader("PaisNombre", "PrioridadId", respuestastringprio).Text);
                    listintprior.SubItems.Add(objfuncionesdicss.validareader("TipoPrioridadDescripcion", "PrioridadId", respuestastringprio).Text);
                    lvPrioridades.Items.Add(listintprior);
                    lvPrioridades.FullRowSelect = true;
                    iCount++;
                }
                respuestastringprio.Close();
                con_prioridad.Cerrarconexion();
                Svariablelinea = "Después de consultar las prioridades, linea 1568";
                //consultamos las anualidades
                try { 
                    conect con_anualidades = new conect();

                    String sConsutaanialidades = "select `AnualidadId`, `AnualidadSecuencia`, `AnualidadIndExe`, `AnualidadAno`, `AnualidadMes`,"+
                        " `AnualidadQuinquenio`, `EstatusAnualidadId`, `CasoId`, DATE_FORMAT(AnualidadFechaPago , '%d-%m-%Y') As AnualidadFechaPago, "+
                        "DATE_FORMAT(AnualidadFechaLimitePago , '%d-%m-%Y') As AnualidadFechaLimitePago, "+
                        "DATE_FORMAT(AnualidadFechaFinVigencia , '%d-%m-%Y') As AnualidadFechaFinVigencia, `AnualidadTipo`, Get_anualidades_aPAGAResp_2lalo(casoid) As anualidadesapagar " +
                        "from anualidad where casoid = " + sCasoiddom + " order by AnualidadSecuencia;";
                    MySqlDataReader respuesta_anualidades = con_anualidades.getdatareader(sConsutaanialidades);
                    lvAnualidades.Items.Clear();
                    String anualidadescubiertas_desde = "";
                    Boolean bDesde = true;
                    String anualidadescubiertas_hasta = "";
                    Boolean bHasta = true;

                    String ultimoquinqueniopagado = "";
                    String Proximosqiunqueniopagar = "";
                    String fechadelpagosigquinquenio = "";
                    Boolean bNohaypagos = true;
                    int iCalculaQuinquenio = 0;
                    int iQuinquenio = 0;
                    Boolean bEmpiezanquinquenios = true;
                    String sEstatusAnualidadDescrip = "";
                    int iContadoranualidades = 0;
                    String sAnualidadesapagar = "";
                    while (respuesta_anualidades.Read())
                    {

                        sAnualidadesapagar = objfuncionesdicss.validareader("anualidadesapagar", "anualidadesapagar", respuesta_anualidades).Text;
                        String sExenta = "No exenta";
                        ListViewItem listintprior = new ListViewItem(objfuncionesdicss.validareader("AnualidadId", "AnualidadId", respuesta_anualidades).Text);
                        String sValorexenta =objfuncionesdicss.validareader("AnualidadIndExe", "AnualidadId", respuesta_anualidades).Text;
                        if (sValorexenta == "1")
                        {
                            sExenta = "Exenta";
                        }

                        
                        
                        conect con_Estatus_anualidades = new conect();
                        String sEstatusanualidad = "select * from estatusanualidad where EstatusAnualidadIndAct = 1 and EstatusAnualidadId = " + objfuncionesdicss.validareader("EstatusAnualidadId", "AnualidadId", respuesta_anualidades).Text;
                        MySqlDataReader respuesta_EstatusAnualidad= con_Estatus_anualidades.getdatareader(sEstatusanualidad);
                        respuesta_EstatusAnualidad.Read();
                        sEstatusAnualidadDescrip = objfuncionesdicss.validareader("EstatusAnualidadDescrip", "EstatusAnualidadId", respuesta_EstatusAnualidad).Text;
                        respuesta_EstatusAnualidad.Close();
                        con_Estatus_anualidades.Cerrarconexion();

                        
                        //calculamos los quinquenios
                        if (sExenta != "exenta")
                        {
                            iCalculaQuinquenio++;
                            if(bEmpiezanquinquenios){
                                iQuinquenio = 1;
                                bEmpiezanquinquenios = false;
                            }

                            if (iCalculaQuinquenio == 6)
                            {
                                iQuinquenio++;
                                iCalculaQuinquenio = 1;
                            }
                        }
                        //fin calculamos los quinquenios
                        //Agregamos los valores al ListView
                        String AnualidadTipo = objfuncionesdicss.validareader("AnualidadTipo", "AnualidadId", respuesta_anualidades).Text;

                        String AnualidadSecuencia = objfuncionesdicss.validareader("AnualidadSecuencia", "AnualidadId", respuesta_anualidades).Text;
                        String AnualidadAno = objfuncionesdicss.validareader("AnualidadAno", "AnualidadId", respuesta_anualidades).Text;
                        String AnualidadMes = objfuncionesdicss.validareader("AnualidadMes", "AnualidadId", respuesta_anualidades).Text;
                        String AnualidadFechaLimitePago = objfuncionesdicss.validareader("AnualidadFechaLimitePago", "AnualidadId", respuesta_anualidades).Text;
                        String AnualidadFechaPago = objfuncionesdicss.validareader("AnualidadFechaPago", "AnualidadId", respuesta_anualidades).Text;
                        String AnualidadFechaFinVigencia = objfuncionesdicss.validareader("AnualidadFechaFinVigencia", "AnualidadId", respuesta_anualidades).Text;
                        String sAnualidadQuinquenio = objfuncionesdicss.validareader("AnualidadQuinquenio", "AnualidadId", respuesta_anualidades).Text;
                        if (AnualidadTipo=="3")
                        {
                            AnualidadTipo = "Agregada";
                        }
                        listintprior.SubItems.Add(sExenta);
                        listintprior.SubItems.Add(AnualidadTipo);
                        listintprior.SubItems.Add(AnualidadSecuencia);
                        listintprior.SubItems.Add(AnualidadAno);
                        listintprior.SubItems.Add(AnualidadMes);
                        listintprior.SubItems.Add(sAnualidadQuinquenio + "");
                        //listintprior.SubItems.Add(iQuinquenio + "");
                        listintprior.SubItems.Add(sEstatusAnualidadDescrip);
                        listintprior.SubItems.Add(validafechasvacias(AnualidadFechaLimitePago));
                        listintprior.SubItems.Add(validafechasvacias(AnualidadFechaPago));
                        listintprior.SubItems.Add(validafechasvacias(AnualidadFechaFinVigencia));
                        //Colocamos colores

                        int iAnualidadQuinquenio = int.Parse(sAnualidadQuinquenio);
                        //switch (iQuinquenio)
                        switch (iAnualidadQuinquenio)
                        {
                            case 0: { listintprior.BackColor = Color.LightGray; } break;
                            case 1: { listintprior.BackColor = Color.LightSkyBlue; } break;
                            case 2: { listintprior.BackColor = Color.LightSteelBlue; } break;
                            case 3: { listintprior.BackColor = Color.Plum; } break;
                            case 4: { listintprior.BackColor = Color.Pink; } break;
                            case 5: { listintprior.BackColor = Color.Orchid; } break;
                        }
                        if (sEstatusAnualidadDescrip.Contains("Pendiente"))
                        {
                            listintprior.BackColor = Color.Salmon;
                        }
                        //fin de terminar de colocar los colores.
                        lvAnualidades.Items.Add(listintprior);
                        //Fin listview

                        if (bDesde && objfuncionesdicss.validareader("EstatusAnualidadId", "AnualidadId", respuesta_anualidades).Text == "2")
                        {
                            anualidadescubiertas_desde = objfuncionesdicss.validareader("AnualidadSecuencia", "AnualidadId", respuesta_anualidades).Text;
                            bDesde = false;
                        }
                        //pendiente por el tipo 1 y el tipo 5
                        String sEstatudid = objfuncionesdicss.validareader("EstatusAnualidadId", "AnualidadId", respuesta_anualidades).Text;
                        if (bHasta && (objfuncionesdicss.validareader("EstatusAnualidadId", "AnualidadId", respuesta_anualidades).Text == "1" || objfuncionesdicss.validareader("EstatusAnualidadId", "AnualidadId", respuesta_anualidades).Text == "5")  && anualidadescubiertas_desde != "")
                        {
                            //ultimo quinquenio pagado
                            int iultimoquinqueniopagado = iQuinquenio - 1;
                            ultimoquinqueniopagado = iultimoquinqueniopagado +"";
                            //proximo quinuqenio a pagar
                            Proximosqiunqueniopagar = iQuinquenio+"";
                            //Fecha del proximo pago
                            fechadelpagosigquinquenio = objfuncionesdicss.validareader("AnualidadFechaLimitePago", "AnualidadId", respuesta_anualidades).Text;

                            int ianualidadescubiertas_desde = Int32.Parse(objfuncionesdicss.validareader("AnualidadSecuencia", "AnualidadId", respuesta_anualidades).Text) - 1;
                            anualidadescubiertas_hasta = ianualidadescubiertas_desde + "";
                            bHasta = false;
                        }
                        //es pendiente y no se ah pagado nada, por lo que ultimoquinqueniopagado es vacio.
                        if (objfuncionesdicss.validareader("EstatusAnualidadId", "AnualidadId", respuesta_anualidades).Text == "1" && anualidadescubiertas_desde=="" && bNohaypagos)
                        {
                            ultimoquinqueniopagado = "Ninguno";
                            /*if (objfuncionesdicss.validareader("AnualidadQuinquenio", "AnualidadId", respuesta_anualidades).Text != "")
                            {
                                Proximosqiunqueniopagar = objfuncionesdicss.validareader("AnualidadQuinquenio", "AnualidadId", respuesta_anualidades).Text;
                            }
                            else {
                                Proximosqiunqueniopagar = "1";
                            }*/
                            Proximosqiunqueniopagar = iQuinquenio + "";
                            fechadelpagosigquinquenio = objfuncionesdicss.validareader("AnualidadFechaLimitePago", "AnualidadId", respuesta_anualidades).Text;
                            bNohaypagos = false;
                        }
                        //quiere decir que se cubrieron todas las anualidades y debemos guardar el valor del último quinquenio
                        //y en Fecha, no hay más quinquenios por pagar
                        if (!bDesde && bNohaypagos && bHasta)
                        {
                            anualidadescubiertas_hasta = objfuncionesdicss.validareader("AnualidadSecuencia", "AnualidadId", respuesta_anualidades).Text;
                            ultimoquinqueniopagado = iQuinquenio + "";
                            Proximosqiunqueniopagar = "---";
                            fechadelpagosigquinquenio = "---";
                        }
                        iContadoranualidades++;
                        
                        
                    }
                    respuesta_anualidades.Close();
                    con_anualidades.Cerrarconexion();
                    tbnumanualidades.Text = iContadoranualidades+"";
                    if (anualidadescubiertas_desde != "" && anualidadescubiertas_hasta !="")
                    {
                        tbRangoanupagadas.Text = "Pagadas desde la anualidad " + anualidadescubiertas_desde + " a la " + anualidadescubiertas_hasta + ".";
                        tbultimoquinqueniopagado.Text = ultimoquinqueniopagado;
                        tbproximoquinqueniopagar.Text = Proximosqiunqueniopagar;
                        tbfechaquinquenippago.Text = fechadelpagosigquinquenio;
                    }
                    else {
                        if (!bNohaypagos)
                        {
                            tbRangoanupagadas.Text = "No hay anualidades Cubiertas/pagadas";
                            tbultimoquinqueniopagado.Text = ultimoquinqueniopagado;
                            tbproximoquinqueniopagar.Text = Proximosqiunqueniopagar;
                            tbfechaquinquenippago.Text = fechadelpagosigquinquenio;
                        }else {
                            if (anualidadescubiertas_desde!="")
                            {
                                tbRangoanupagadas.Text = tbRangoanupagadas.Text = "Pagadas desde la anualidad " + anualidadescubiertas_desde + " a la " + anualidadescubiertas_hasta + ".";
                                tbultimoquinqueniopagado.Text = ultimoquinqueniopagado;
                                tbproximoquinqueniopagar.Text = Proximosqiunqueniopagar;
                                tbfechaquinquenippago.Text = fechadelpagosigquinquenio;
                            }else{
                                tbRangoanupagadas.Text = "No hay anualidades por pagar.";
                                tbultimoquinqueniopagado.Text = "";
                                tbproximoquinqueniopagar.Text = "";
                                tbfechaquinquenippago.Text = "";
                            }
                            
                        }
                    }

                    if (sAnualidadesapagar != "" && sanualidadesnuevas == "")
                    {
                        tbFechaproximasanualidades.Text = "Proximas " + sAnualidadesapagar + "   en   " + tbfechaquinquenippago.Text + " ";
                    }
                    else {
                        if (sanualidadesnuevas == "") {
                            tbFechaproximasanualidades.Text = "";
                        }
                    }
                    

                }
                catch(Exception E){//calculo de anualidades
                    new filelog(loguin.sId, "linea 1690: "+E.ToString());
                }
                Svariablelinea = "Después de consultar las anualidades linea 1565";

                //consultamos la referencia
                //conect con_referencias = new conect();
                //String sQuery_referencia = " SELECT  * from referencia where casoid = "+sCasoId;
                //MySqlDataReader respuestas_referencias = con_referencias.getdatareader(sQuery_referencia);
                //lvPrioridades.Items.Clear();
                //int iCount_ref = 0;
                //while (respuestas_referencias.Read())
                //{
                //    //ListViewItem listintprior = new ListViewItem(objfuncionesdicss.validareader("PrioridadId", "PrioridadId", respuestastringprio).Text);
                //    //listintprior.SubItems.Add(objfuncionesdicss.validareader("PrioridadNumero", "PrioridadId", respuestastringprio).Text);
                //    //listintprior.SubItems.Add(validafechavacia(objfuncionesdicss.validareader("PrioridadFecha", "PrioridadId", respuestastringprio).Text));
                //    //listintprior.SubItems.Add(objfuncionesdicss.validareader("PaisClave", "PrioridadId", respuestastringprio).Text);
                //    //listintprior.SubItems.Add(objfuncionesdicss.validareader("PaisNombre", "PrioridadId", respuestastringprio).Text);
                //    //listintprior.SubItems.Add(objfuncionesdicss.validareader("TipoPrioridadDescripcion", "PrioridadId", respuestastringprio).Text);
                //    //lvPrioridades.Items.Add(listintprior);
                //    //lvPrioridades.FullRowSelect = true;
                //    //iCount_ref++;
                //}
                //respuestas_referencias.Close();
                //con_referencias.Cerrarconexion();
                //fin consulta referencia

                rtNumprio.Text = iCount + "";
                progressBar1.Value = 90;
                progressBar1.Value = 100;
                progressBar1.Hide();
                this.Enabled = true;


                //if (lCorresponsal.Text == "")
                //{
                //    //1254; 467
                //    label33.Hide();
                //    lCorresponsal.Hide();
                //    label38.Hide();
                //    lCotaccorresponsal.Hide();
                //    label40.Hide();
                //    richTextBox4.Hide();
                //    //tabdivicional.Location = new System.Drawing.Point(12, 170);
                //    //tabdivicional.Size = new System.Drawing.Size(1254, 467);
                //}
                /*Documentos IMPI */
                Svariablelinea = "Antes de consultar documentos impi";
                carga_documentos_IMPI(sCasoId, gSTipoSolicitudId);
                Svariablelinea = "Después de consultar documentos impi";
                //agregamsos las cartas
                cbCartas.Items.Clear();
                conect con_3_cartas = new conect();
                String sQueryescritosdisponibles = "SELECT " +
                                                           "     * " +
                                                           " FROM " +
                                                           "    estatuscasosubtipodocumento, " +
                                                           "    subtipodocumento " +
                                                           " WHERE " +
                                                           "     estatuscasosubtipodocumento.Estatuscasoid = " + gSEstatusCasoId + "  " +
                                                           "         AND estatuscasosubtipodocumento.GrupoId = 1" +// +sGrop +//El grupo falta validarlo con una variable dependiendo el tipo del caso 
                                                           "         AND subtipodocumento.SubTipoDocumentoId = estatuscasosubtipodocumento.SubTipoDocumentoId " +
                                                           "         AND subtipodocumento.TipoDocumentoId = 3 " +//carta
                                                           "         AND subtipodocumento.SubTipoDocumentoIndAct = 1 " +
                                                           //"         AND subtipodocumento.SubTipoDocumentoId in (186,205,206,210,1028,1034,1041,1043,1050,1058,1068,1070,1073,1097,1110,1116,1118,1123,1125,1126)" +
                                                           "         group by estatuscasosubtipodocumento.SubTipoDocumentoId order by subtipodocumento.SubTipoDocumentoDescrip;";
                //String sQueryescritosdisponibles = " SELECT  " +
                //            "     * " +
                //            " FROM " +
                //            "     subtipodocumento, " +
                //            "     gruposubtipodocumento " +
                //            " WHERE " +
                //            "     gruposubtipodocumento.GrupoId = 1 " +//MARCAS
                //            "         AND gruposubtipodocumento.SubtipodocumentoId = subtipodocumento.SubtipodocumentoId " +
                //            "         AND TipoDocumentoId = 3 " +
                //            "         AND SubTipoDocumentoIndAct = 1 " +
                //            "         AND (SubTipoDocumentoTemplateEspanol != '' " +
                //            "         OR SubTipoDocumentoTemplateIngles != ''); ";
                //String sQueryescritosdisponibless = "select * from estatuscasosubtipodocumento, subtipodocumento where estatuscasosubtipodocumento.EstatusCasoId = " + sEstatusidint + " and estatuscasosubtipodocumento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId and estatuscasosubtipodocumento.GrupoId =1 and subtipodocumento.TipoDocumentoId = 2 group by subtipodocumento.SubTipoDocumentoId;";
                //String sQueryescritosdisponibles = "select subtipodocumento.SubTipoDocumentoDescrip, estatuscasosubtipodocumentores.SubTipoDocumentoId from estatuscasosubtipodocumentores, subtipodocumento where estatuscasoid = " + tbEstatus.Text + " and estatuscasosubtipodocumentores.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId;";
                MySqlDataReader resp_escritos = con_3_cartas.getdatareader(sQueryescritosdisponibles);
                while (resp_escritos.Read())
                {
                    String sCartas = objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoId", resp_escritos).Text;
                    if (sCartas != "")
                    {
                        cbCartas.Items.Add(objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoId", resp_escritos));//Estatus
                    }

                }
                resp_escritos.Close();
                con_3_cartas.Cerrarconexion();

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
                                                               "     estatuscasosubtipodocumento.Estatuscasoid = " + gSEstatusCasoId + "  " +
                                                               "         AND estatuscasosubtipodocumento.GrupoId = 1" +// +sGrop +//El grupo falta validarlo con una variable dependiendo el tipo del caso 
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
                                                               "     estatuscasosubtipodocumento.Estatuscasoid = " + gSEstatusCasoId + "  " +
                                                               "         AND estatuscasosubtipodocumento.GrupoId = 1" +// +sGrop +//El grupo falta validarlo con una variable dependiendo el tipo del caso 
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

                Svariablelinea = "Después de consultar las cartas al combobox linea 1852";

                //agregamos los formatos
                //cbOficios.Items.Clear();
                //conect con_3_formatos = new conect();
                //String sQueryformatosdispo= "SELECT " +
                //                                           "     * " +
                //                                           " FROM " +
                //                                           "    estatuscasosubtipodocumento, " +
                //                                           "    subtipodocumento " +
                //                                           " WHERE " +
                //                                           "     estatuscasosubtipodocumento.Estatuscasoid = " + gSEstatusCasoId + "  " +
                //                                           "         AND estatuscasosubtipodocumento.GrupoId = 1" +// +sGrop +//El grupo falta validarlo con una variable dependiendo el tipo del caso 
                //                                           "         AND subtipodocumento.SubTipoDocumentoId = estatuscasosubtipodocumento.SubTipoDocumentoId " +
                //                                           "         AND subtipodocumento.TipoDocumentoId = 1 " +
                //                                           "         AND subtipodocumento.SubTipoDocumentoIndAct = 1 " +
                //                                           "         group by estatuscasosubtipodocumento.SubTipoDocumentoId order by subtipodocumento.SubTipoDocumentoDescrip;";
                ////String sQueryescritosdisponibless = "select * from estatuscasosubtipodocumento, subtipodocumento where estatuscasosubtipodocumento.EstatusCasoId = " + sEstatusidint + " and estatuscasosubtipodocumento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId and estatuscasosubtipodocumento.GrupoId =1 and subtipodocumento.TipoDocumentoId = 2 group by subtipodocumento.SubTipoDocumentoId;";
                ////String sQueryescritosdisponibles = "select subtipodocumento.SubTipoDocumentoDescrip, estatuscasosubtipodocumentores.SubTipoDocumentoId from estatuscasosubtipodocumentores, subtipodocumento where estatuscasoid = " + tbEstatus.Text + " and estatuscasosubtipodocumentores.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId;";
                //MySqlDataReader resp_formatos = con_3_formatos.getdatareader(sQueryformatosdispo);
                //while (resp_formatos.Read())
                //{
                //    String sOficiostext = objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoId", resp_formatos).Text;
                //    if (sOficiostext!="")
                //    {
                //        cbOficios.Items.Add(objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoId", resp_formatos));//Estatus
                //    }
                //}
                //resp_formatos.Close();
                //con_3_formatos.Cerrarconexion();

                //agregamos los escritos
                cbEscritos.Items.Clear();
                conect con_3_escritos = new conect();
                String sQuery_escritos = "SELECT " +
                                        "     * " +
                                        " FROM " +
                                        "    estatuscasosubtipodocumento, " +
                                        "    subtipodocumento " +
                                        " WHERE " +
                                        //"     estatuscasosubtipodocumento.Estatuscasoid = " + gSEstatusCasoId + "  " +
                                        "       estatuscasosubtipodocumento.GrupoId = 1" +// +sGrop +//El grupo falta validarlo con una variable dependiendo el tipo del caso 
                                        "       AND subtipodocumento.SubTipoDocumentoId = estatuscasosubtipodocumento.SubTipoDocumentoId " +
                                        "       AND subtipodocumento.TipoDocumentoId = 2 " +
                                        "       AND subtipodocumento.SubTipoDocumentoIndAct = 1 " +
                                        "       group by estatuscasosubtipodocumento.SubTipoDocumentoId order by subtipodocumento.SubTipoDocumentoDescrip;";
                //String sQueryescritosdisponibless = "select * from estatuscasosubtipodocumento, subtipodocumento where estatuscasosubtipodocumento.EstatusCasoId = " + sEstatusidint + " and estatuscasosubtipodocumento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId and estatuscasosubtipodocumento.GrupoId =1 and subtipodocumento.TipoDocumentoId = 2 group by subtipodocumento.SubTipoDocumentoId;";
                //String sQueryescritosdisponibles = "select subtipodocumento.SubTipoDocumentoDescrip, estatuscasosubtipodocumentores.SubTipoDocumentoId from estatuscasosubtipodocumentores, subtipodocumento where estatuscasoid = " + tbEstatus.Text + " and estatuscasosubtipodocumentores.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId;";
                MySqlDataReader resp_escritos_grid = con_3_escritos.getdatareader(sQuery_escritos);
                while (resp_escritos_grid.Read())
                {
                    cbEscritos.Items.Add(objfuncionesdicss.validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos_grid));//Estatus
                }
                resp_escritos_grid.Close();
                con_3_escritos.Cerrarconexion();
                Svariablelinea = "Después de agregar los escritos al combobox linea 1907";

                //carga formatos
                cargaformatos();

            }
            catch (Exception E){
                progressBar1.Value = 100;
                progressBar1.Hide();
                this.Enabled = true;
                new filelog(loguin.sId,"linea 1835: "+Svariablelinea+" "+ E.StackTrace.ToString());
            }
            
            //consultamos los tipos de documentos para edocs
            try {
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
            }catch(Exception Ex){
                new filelog(loguin.sId, "linea 1851: "+Ex.ToString());
            }

            consultamosdocumentoselectronicos();
            consultareferencias();
            consultaplazo_nuevos();
            //Consultamos el caso corresponsal
            cargarcorresponsal();
            if (tbCasodiseno.Text != "" && tbCasodiseno.Text != "0")
            {/* Consultamos la tabla de texto diseños para arrojar el mensaje */
                conect conect_aviso = new conect();
                String sAviso = "SELECT * FROM textodiseno where CasoDisenoClasificacion = '" + tbCasodiseno.Text + "';";
                MySqlDataReader resp_aviso = conect_aviso.getdatareader(sAviso);
                resp_aviso.Read();
                String TextoDisenoTitulo = objfuncionesdicss.validareader("TextoDisenoTitulo", "TextoDisenoTitulo", resp_aviso).Text;
                String TextoDisenoDescripcion = objfuncionesdicss.validareader("TextoDisenoDescripcion", "TextoDisenoDescripcion", resp_aviso).Text;
                richTextBoxDisenos.Text = TextoDisenoDescripcion;
                //MessageBox.Show(TextoDisenoDescripcion, TextoDisenoTitulo);
                resp_aviso.Close();
                conect_aviso.Cerrarconexion();
                //MessageBox.Show("");
            }
            else
            {
                richTextBoxDisenos.Hide();
                labeldescdiseno.Hide();
            }

            try {
                tbFecharecepcionAnulidadDise.Text = tbFechaRecimpi.Text;
                tbFechaRsgitroAnuDisen.Text = tbFechaconcesion.Text;
                tbFechaVigeniaAnuDisen.Text = tbFechavigencia.Text;
                if (tbFechainternacional.Text != "")
                {
                    tbFechaLegalAnulidadDise.Text = tbFechainternacional.Text;
                }
                else
                {
                    tbFechaLegalAnulidadDise.Text = tbPlazolegal.Text;
                }
            }
            catch (Exception ex) { 
                
            }
        }

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

        public void cargaformatos() {
            try {
                //agregamos los 
                if (cbFormatoscheck.Checked)
                {
                    cbFormatos.Items.Clear();
                    conect con_3_escritos = new conect();
                    String sQuery_escritos = " SELECT  " +
                                                        "     * " +
                                                        " FROM " +
                                                        "     subtipodocumento, " +
                                                        "     gruposubtipodocumento " +
                                                        " WHERE " +
                                                        "     gruposubtipodocumento.GrupoId = 1 " +
                                                        "         AND gruposubtipodocumento.SubtipodocumentoId = subtipodocumento.SubtipodocumentoId " +
                                                        "         AND TipoDocumentoId = 5 " +
                                                        "         AND SubTipoDocumentoIndAct = 1 " +
                                                        "         AND (SubTipoDocumentoTemplateEspanol != '' " +
                                                        "         OR SubTipoDocumentoTemplateIngles != ''); ";

                    MySqlDataReader resp_escritos_grid = con_3_escritos.getdatareader(sQuery_escritos);
                    while (resp_escritos_grid.Read())
                    {
                        cbFormatos.Items.Add(objfuncionesdicss.validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos_grid));//Estatus
                    }
                    resp_escritos_grid.Close();
                    con_3_escritos.Cerrarconexion();
                }else {
                    cbFormatos.Items.Clear();
                    conect con_3_escritos = new conect();
                    String sQuery_escritos = "SELECT " +
                                            "     * " +
                                            " FROM " +
                                            "    estatuscasosubtipodocumento, " +
                                            "    subtipodocumento " +
                                            " WHERE " +
                                            //"     estatuscasosubtipodocumento.Estatuscasoid = " + gSEstatusCasoId + "  " +
                                            "       estatuscasosubtipodocumento.GrupoId = 1" +// +sGrop +//El grupo falta validarlo con una variable dependiendo el tipo del caso 
                                            "       AND subtipodocumento.SubTipoDocumentoId = estatuscasosubtipodocumento.SubTipoDocumentoId " +
                                            "       AND subtipodocumento.TipoDocumentoId = 5 " +
                                            "       AND subtipodocumento.SubTipoDocumentoIndAct = 1 " +
                                            "       group by estatuscasosubtipodocumento.SubTipoDocumentoId order by subtipodocumento.SubTipoDocumentoDescrip;";
                    MySqlDataReader resp_escritos_grid = con_3_escritos.getdatareader(sQuery_escritos);
                    while (resp_escritos_grid.Read())
                    {
                        cbFormatos.Items.Add(objfuncionesdicss.validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos_grid));//Estatus
                    }
                    resp_escritos_grid.Close();
                    con_3_escritos.Cerrarconexion();

                }
            }catch (Exception exs) {
                new filelog("linea: 2055", exs.Message);
            }
            
        }


        public void carga_documentos_IMPI(string sCasoiddom, string gSTipoSolicitudId)
        {
            try {
                dGV_docimentos_IMPI.Rows.Clear();
                lvdocumentosimpi.Items.Clear();
                //documentosimpi
                conect con2 = new conect();
                String squeryadocumentos = " SELECT " +
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
                                            "         AND relaciondocumento.CasoId = " + sCasoiddom + " " +
                                            "         AND relaciondocumento.TipoSolicitudId = " + gSTipoSolicitudId + "" +
                                            "         AND SubTipoDocumento.SubTipoDocumentoId = documento.SubTipoDocumentoId;";
                MySqlDataReader resp_docimpi = con2.getdatareader(squeryadocumentos);
                int iCountpar = 0;
                int icontardocumentototales = 0;
                while (resp_docimpi.Read())
                {
                    //lvdocumentosimpi 
                    int iMes = 0;
                    try {
                        String sMes = objfuncionesdicss.validareader("diasfiff", "casoid", resp_docimpi).Text;
                        iMes = Int32.Parse(sMes) / 30;
                        iMes++;
                    }
                    catch (Exception exsm) {
                        iMes = 0;
                        new filelog("falla al calucular el mes", " "+exsm.Message);
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
                                ////items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text);//vencimiento
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
                                //dRows.Cells[6].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[6].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[7].Value = "" + iMes;
                                dRows.Cells[8].Value = objfuncionesdicss.validareader("DocumentoFecha", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[9].Value = "";
                                dRows.Cells[10].Value = objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text;
                                dRows.Cells[11].Value = "";// objfuncionesdicss.validareader("Estatus_doc", "casoid", resp_docimpi).Text;// "";//Estatus  Estatus_doc

                                dRows.Cells[12].Value = objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text;
                                dRows.Cells[13].Value = "";
                                dRows.Cells[14].Value = "";
                                dRows.Cells[15].Value = "";
                                dRows.Cells[16].Value = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[17].Value = objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi).Text;

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
                                //dRows.Cells[6].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[6].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[7].Value = "" + iMes;
                                dRows.Cells[8].Value = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;//
                                dRows.Cells[9].Value = "";
                                dRows.Cells[10].Value = objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text;
                                dRows.Cells[11].Value = "";// objfuncionesdicss.validareader("Estatus_doc", "casoid", resp_docimpi).Text;// "";//Estatus  Estatus_doc

                                dRows.Cells[12].Value = objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text;
                                dRows.Cells[13].Value = "";
                                dRows.Cells[14].Value = "";
                                dRows.Cells[15].Value = "";
                                dRows.Cells[16].Value = objfuncionesdicss.validareader("DocumentoFecha", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[17].Value = objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi, true).Text;
                                Subtipodocumentoidultimoescrito = objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi).Text;
                                

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

                                String sSubtipodocumentoid = objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi).Text;
                                if (objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi).Text == "115")//Validamos que no sea cita a pago
                                {
                                    tbFechacitaapago.Text = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                                }
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
                                //dRows.Cells[6].Value = sFechavigencia3meses;
                                dRows.Cells[6].Value = sFechavigencia4meses;
                                dRows.Cells[7].Value = "" + iMes;
                                dRows.Cells[8].Value = "";
                                dRows.Cells[9].Value = "";
                                dRows.Cells[10].Value = objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text;
                                dRows.Cells[11].Value = objfuncionesdicss.validareader("Estatus_doc", "casoid", resp_docimpi).Text;// "";//Estatus  Estatus_doc

                                dRows.Cells[12].Value = objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text;
                                dRows.Cells[13].Value = "";
                                dRows.Cells[14].Value = "";
                                dRows.Cells[15].Value = "";
                                dRows.Cells[16].Value = objfuncionesdicss.validareader("DocumentoFecha", "casoid", resp_docimpi, true).Text;
                            } break;
                        case "Título":
                            {
                                //items = new ListViewItem(objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text);//link
                                //items.SubItems.Add(objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);//tipo
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text);//codgbarras
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text);//folio
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text);//fechanotificacion
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text);//vencimiento
                                ////items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text);//vencimiento
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
                                //dRows.Cells[6].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[6].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[7].Value = "" + iMes;
                                dRows.Cells[8].Value = "";
                                dRows.Cells[9].Value = "";
                                dRows.Cells[10].Value = objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text;
                                dRows.Cells[11].Value = "";// objfuncionesdicss.validareader("Estatus_doc", "casoid", resp_docimpi).Text;// "";//Estatus  Estatus_doc

                                dRows.Cells[12].Value = objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text;
                                dRows.Cells[13].Value = "";
                                dRows.Cells[14].Value = "";
                                dRows.Cells[15].Value = "";
                                dRows.Cells[16].Value = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                            } break;
                        case "E-MAIL":
                            {
                                //items = new ListViewItem(objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text);//link
                                //items.SubItems.Add(objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);//tipo
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text);//codgbarras
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text);//folio
                                //items.SubItems.Add("");//fechanotificacion
                                //items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text);//vencimiento
                                ////items.SubItems.Add(objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text);//vencimiento
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
                                //dRows.Cells[6].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[6].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[7].Value = "" + iMes;
                                dRows.Cells[8].Value = "";
                                dRows.Cells[9].Value = "";
                                dRows.Cells[10].Value = objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text;
                                dRows.Cells[11].Value = objfuncionesdicss.validareader("Estatus_doc", "casoid", resp_docimpi).Text;// "";//Estatus  Estatus_doc

                                dRows.Cells[12].Value = objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text;
                                dRows.Cells[13].Value = "";
                                dRows.Cells[14].Value = "";
                                dRows.Cells[15].Value = "";
                                dRows.Cells[16].Value = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
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
                                //dRows.Cells[6].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento3meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[6].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[7].Value = "" + iMes;
                                dRows.Cells[8].Value = "";
                                dRows.Cells[9].Value = "";
                                dRows.Cells[10].Value = objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text;
                                dRows.Cells[11].Value = objfuncionesdicss.validareader("Estatus_doc", "casoid", resp_docimpi).Text;// "";//Estatus  Estatus_doc

                                dRows.Cells[12].Value = objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text;
                                dRows.Cells[13].Value = "";
                                dRows.Cells[14].Value = "";
                                dRows.Cells[15].Value = "";
                                dRows.Cells[16].Value = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                            } break;
                        default:
                            {
                                items = new ListViewItem("Tipo de documento no considerado");//link
                                items.SubItems.Add(objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);//tipo
                                dRows.Cells[0].Value = "Tipo de documento no considerado";
                                dRows.Cells[1].Value = objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text;
                            } break;
                            
                    }/*por ahora sólo consideraremos 5 tipos de documentos mencionados arriba*/
                    String sDocumentosid =objfuncionesdicss.validareader("Documentoid", "Documentoid", resp_docimpi).Text;
                    dRows.Cells[19].Value = sDocumentosid;

                    if (iCountpar % 2 == 0)
                    {
                        items.BackColor = Color.White;
                        dRows.DefaultCellStyle.BackColor = Color.White;
                    }else{
                        items.BackColor = Color.LightBlue;
                        dRows.DefaultCellStyle.BackColor = Color.LightBlue;
                    }
                    iCountpar++;
                    dGV_docimentos_IMPI.Rows.Add(dRows);
                    icontardocumentototales++;
                    //lvdocumentosimpi.Items.Add(items);
                }
                resp_docimpi.Close();
                con2.Cerrarconexion();
                textBox19.Text = ""+ icontardocumentototales;
            }catch(Exception Ex){
                new filelog("cargando documentos IMPI patentes", "linea 2268:"+Ex.Message);
            }
            
            
        }
        public void consultaplazo_nuevos() {
            try {
                conect con_tcon_edocs = new conect();
                String sConsultaplazos = " select * from plazo_general_vista " +
                                         "where casoid = " + sCasoId +
                                         " and TipoSolicitudId = " + gSTipoSolicitudId;
                MySqlDataReader resp_tedocs = con_tcon_edocs.getdatareader(sConsultaplazos);
                dgPlazos.Rows.Clear();
                String sPlazodistinto = "";
                bool sbandera = true;
                Color[] cColorrow = { Color.Aqua, Color.LightBlue, Color.Aqua, Color.LightBlue };
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

                    String sFecha_atendio_plazo_impi_sistema = objfuncionesdicss.validareader("Fecha_atendio_plazo_impi_sistema", "Fecha_atendio_plazo_impi_sistema", resp_tedocs).Text;
                    String sDocumento_atenio_impi = objfuncionesdicss.validareader("Documento_atenio_impi", "Fecha_atendio_plazo_impi", resp_tedocs).Text;//sistema
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
                    dRows.DefaultCellStyle.BackColor = cColorrow[xz];
                    if (sEstatus_plazo_impi == "Pendiente")
                    {
                        dRows.Cells[7].Style.BackColor = Color.LightCoral;
                    }
                    else
                    {
                        dRows.Cells[7].Style.BackColor = Color.LightGreen;
                    }

                    dRows.Cells[0].Value = sidPlazo_general;
                    dRows.Cells[1].Value = sPlazos_detalleid;
                    dRows.Cells[2].Value = sCapturo;
                    dRows.Cells[3].Value = sDocumento;
                    dRows.Cells[4].Value = sDocumento;
                    dRows.Cells[5].Value = sTipo_plazo_IMPI;
                    dRows.Cells[6].Value = sTipo_plazo_IMPI;
                    dRows.Cells[7].Value = sEstatus_plazo_impi;
                    dRows.Cells[8].Value = sMes;//Mes
                    dRows.Cells[9].Value = sValidafechavacia(sFecha_notificacion_impi);
                    dRows.Cells[10].Value = sValidafechavacia(sFecha_Vencimiento_regular_impi);
                    //dRows.Cells[10].Value = sValidafechavacia(sFecha_vencimiento_3m_impi);
                    dRows.Cells[11].Value = sValidafechavacia(sFecha_vencimiento_4m_impi);
                    dRows.Cells[12].Value = sValidafechavacia(sFecha_atendio_plazo_impi);
                    dRows.Cells[13].Value = satendio_plazoimpi;
                    dRows.Cells[14].Value = sDoc_atendio;
                    dRows.Cells[15].Value = sMotivo_cancelacion_plazo_impi;
                    dRows.Cells[16].Value = sValidafechavacia(sFecha_cancelacion_plazo_impi);

                    dRows.Cells[17].Value = sUsuariocancelo;
                    dRows.Cells[18].Value = sFecha_atendio_plazo_impi_sistema;
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

            }catch(Exception Ex){
                new filelog("plazos_patentes: " ,"linea 2386: Error: "+Ex.Message);
            }
        }

        public String sValidafechavacia(String sFecha_cancelacion_aviso_cliente) {
            if (sFecha_cancelacion_aviso_cliente == "0000/00/00" || sFecha_cancelacion_aviso_cliente == "")
            {
                return "";
            }else {
                DateTime sFecha = DateTime.ParseExact(sFecha_cancelacion_aviso_cliente, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                return sFecha.ToString("dd/MM/yyyy");
            }
        }
        //public void consultaplazo_nuevos() {
        //    try
        //    {
        //        //dgPlazos
        //        conect con_tcon_edocs = new conect();
        //        String sConsultaplazos = " select" +
        //                                " PlazoId," +
        //                                " TipoPlazoId," +
        //                                " Get_tipoplazo(TipoPlazoId) as tipoplazodescrip," +
        //                                " Get_grupoplazo(TipoPlazoId) as grupoplazodescrip," +//grupoplazo
        //                                " CasoId," +
        //                                " DocumentoId," +
        //                                " Get_Subtipodocumentodescrip_from_documentoid(DocumentoId) as Documento_descrip," +
        //                                " AnualidadId," +
        //                                " ClienteId," +
        //                                " PlazoMotivoCancelacion," +
        //                                " PlazoFecha," +
        //                                " PlazoFechaProrroga," +
        //                                " UsuarioId," +
        //                                " Get_Usuario(UsuarioId) as usuarionombre," +
        //                                " PlazoFechaAtencion," +
        //                                " EstatusPlazoId," +
        //                                " Get_Estatusplazodescrip(EstatusPlazoId) as Estatusplazodescrip," +
        //                                " UsuarioIdCancelo," +
        //                                " Get_Usuario(UsuarioIdCancelo) as usuarionombre_cancelo," +
        //                                " PlazoDescripcion," +
        //                                " PlazoIdRef," +
        //                                " usuarioIdAtendio," +
        //                                " TipoPlazoId_avisos," +
        //                                " Get_tipoplazo(TipoPlazoId_avisos) as tipoplazodescrip_avisos," +
        //                                " Get_grupoplazo(TipoPlazoId_avisos) as grupoplazodescrip_avisos," +//grupoplazo
        //                                " CasoId_avisos," +
        //                                " DocumentoId_avisos," +
        //                                " Get_Subtipodocumentodescrip_from_documentoid(DocumentoId_avisos) as documentodescrip_avisos," +
        //                                " AnualidadId_avisos," +
        //                                " ClienteId_avisos," +
        //                                " PlazoMotivoCancelacion_avisos," +
        //                                " PlazoFecha_avisos," +
        //                                " PlazoFechaProrroga_avisos," +
        //                                " UsuarioId_avisos," +
        //                                " Get_Usuario(UsuarioId_avisos) as usuarionombre_avisos," +

        //                                " PlazoFechaAtencion_avisos," +
        //                                " EstatusPlazoId_avisos," +
        //                                " Get_Estatusplazodescrip(EstatusPlazoId_avisos) as Estatusplazodescrip_avisos," +

        //                                " UsuarioIdCancelo_avisos," +
        //                                " Get_Usuario(UsuarioIdCancelo_avisos) as usuarionombre_cancelo_avisos," +

        //                                " PlazoDescripcion_avisos," +
        //                                " PlazoIdRef_avisos," +
        //                                " usuarioIdAtendio_avisos" +
        //                                " from " +
        //                                " plazos_king" +
        //                                " where casoid = " + sCasoId;
        //        /*String sConsultaplazos = "Select `PlazoId`, "+
        //                                "`TipoPlazoId`, "+
        //                                "`CasoId`, "+
        //                                "`DocumentoId`, " +
        //                                " Get_Subtipodocumentodescrip_from_documentoid(DocumentoId) as Documento_descrip, " +
        //                                "`AnualidadId`, "+
        //                                "`ClienteId`, "+
        //                                "`PlazoMotivoCancelacion`, "+
        //                                "`PlazoFecha`, "+
        //                                "`PlazoFechaProrroga`, "+
        //                                "`UsuarioId`, "+
        //                                "`PlazoFechaAtencion`, "+
        //                                "`EstatusPlazoId`, "+
        //                                "`UsuarioIdCancelo`, "+
        //                                "`PlazoDescripcion`, "+
        //                                "`PlazoIdRef`, "+
        //                                "`usuarioIdAtendio`, "+
        //                                "`TipoPlazoId_avisos`, "+
        //                                "`CasoId_avisos`, "+
        //                                "`DocumentoId_avisos`, "+
        //                                "`AnualidadId_avisos`, "+
        //                                "`ClienteId_avisos`, "+
        //                                "`PlazoMotivoCancelacion_avisos`, "+
        //                                "`PlazoFecha_avisos`, "+
        //                                "`PlazoFechaProrroga_avisos`, "+
        //                                "`UsuarioId_avisos`, "+
        //                                "`PlazoFechaAtencion_avisos`, "+
        //                                "`EstatusPlazoId_avisos`, "+
        //                                "`UsuarioIdCancelo_avisos`, "+
        //                                "`PlazoDescripcion_avisos`, "+
        //                                "`PlazoIdRef_avisos`, "+
        //                                "`usuarioIdAtendio_avisos` from plazos_king where casoid = "+sCasoId;*/
        //        //String sWueryplazos = "select * from  plazo where casoid = 13299;";
        //        //select * from  plazo where casoid = 38893;
        //        dgPlazos.Rows.Clear();
        //        MySqlDataReader resp_tedocs = con_tcon_edocs.getdatareader(sConsultaplazos);
        //        while (resp_tedocs.Read())
        //        {
        //            String sVPlazoId = objfuncionesdicss.validareader("PlazoId", "PlazoId", resp_tedocs).Text;
        //            String sVTipoPlazoId = objfuncionesdicss.validareader("TipoPlazoId", "TipoPlazoId", resp_tedocs).Text;
        //            String sVgrupoplazodescrip = objfuncionesdicss.validareader("grupoplazodescrip", "TipoPlazoId", resp_tedocs).Text;
        //            //grupoplazodescrip

        //            String sVtipoplazodescrip = objfuncionesdicss.validareader("tipoplazodescrip", "TipoPlazoId", resp_tedocs).Text;
        //            String sVCasoId = objfuncionesdicss.validareader("CasoId", "CasoId", resp_tedocs).Text;
        //            String sVDocumentoId = objfuncionesdicss.validareader("DocumentoId", "DocumentoId", resp_tedocs).Text;
        //            String sVDocumentoDescrip = objfuncionesdicss.validareader("Documento_descrip", "Documento_descrip", resp_tedocs).Text;
        //            String sVAnualidadId = objfuncionesdicss.validareader("AnualidadId", "AnualidadId", resp_tedocs).Text;
        //            String sVClienteId = objfuncionesdicss.validareader("ClienteId", "ClienteId", resp_tedocs).Text;
        //            String sVPlazoMotivoCancelacion = objfuncionesdicss.validareader("PlazoMotivoCancelacion", "PlazoMotivoCancelacion", resp_tedocs).Text;
        //            String sVPlazoFecha = objfuncionesdicss.validareader("PlazoFecha", "PlazoFecha", resp_tedocs).Text;
        //            String sVPlazoFechaProrroga = objfuncionesdicss.validareader("PlazoFechaProrroga", "PlazoFechaProrroga", resp_tedocs).Text;
        //            String sVUsuarioId = objfuncionesdicss.validareader("UsuarioId", "UsuarioId", resp_tedocs).Text;

        //            String sVusuarionombre = objfuncionesdicss.validareader("usuarionombre", "UsuarioId", resp_tedocs).Text;
        //            String sVPlazoFechaAtencion = objfuncionesdicss.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", resp_tedocs).Text;
        //            String sVEstatusPlazoId = objfuncionesdicss.validareader("EstatusPlazoId", "EstatusPlazoId", resp_tedocs).Text;

        //            String sVEstatusplazodescrip = objfuncionesdicss.validareader("Estatusplazodescrip", "EstatusPlazoId", resp_tedocs).Text;
        //            String sVUsuarioIdCancelo = objfuncionesdicss.validareader("UsuarioIdCancelo", "UsuarioIdCancelo", resp_tedocs).Text;
        //            String sVusuarionombre_cancelo = objfuncionesdicss.validareader("usuarionombre_cancelo", "UsuarioIdCancelo", resp_tedocs).Text;
                    
        //            String sVPlazoDescripcion = objfuncionesdicss.validareader("PlazoDescripcion", "PlazoDescripcion", resp_tedocs).Text;
        //            String sVPlazoIdRef = objfuncionesdicss.validareader("PlazoIdRef", "PlazoIdRef", resp_tedocs).Text;
        //            String sVusuarioIdAtendio = objfuncionesdicss.validareader("usuarioIdAtendio", "usuarioIdAtendio", resp_tedocs).Text;

        //            String TipoPlazoId_avisos = objfuncionesdicss.validareader("TipoPlazoId_avisos", "TipoPlazoId_avisos", resp_tedocs).Text;
        //            String tipoplazodescrip_avisos = objfuncionesdicss.validareader("tipoplazodescrip_avisos", "tipoplazodescrip_avisos", resp_tedocs).Text;

        //            String grupoplazodescrip_avisos = objfuncionesdicss.validareader("grupoplazodescrip_avisos", "grupoplazodescrip_avisos", resp_tedocs).Text;
        //            //tipoplazodescrip_avisos
        //            String CasoId_avisos = objfuncionesdicss.validareader("CasoId_avisos", "CasoId_avisos", resp_tedocs).Text;
        //            String DocumentoId_avisos = objfuncionesdicss.validareader("DocumentoId_avisos", "DocumentoId_avisos", resp_tedocs).Text;

        //            String documentodescrip_avisos = objfuncionesdicss.validareader("documentodescrip_avisos", "DocumentoId_avisos", resp_tedocs).Text;
        //            String AnualidadId_avisos = objfuncionesdicss.validareader("AnualidadId_avisos", "AnualidadId_avisos", resp_tedocs).Text;
        //            String ClienteId_avisos = objfuncionesdicss.validareader("ClienteId_avisos", "ClienteId_avisos", resp_tedocs).Text;
        //            String PlazoMotivoCancelacion_avisos = objfuncionesdicss.validareader("PlazoMotivoCancelacion_avisos", "PlazoMotivoCancelacion_avisos", resp_tedocs).Text;
        //            String PlazoFecha_avisos = objfuncionesdicss.validareader("PlazoFecha_avisos", "PlazoFecha_avisos", resp_tedocs).Text;
        //            String PlazoFechaProrroga_avisos = objfuncionesdicss.validareader("PlazoFechaProrroga_avisos", "PlazoFechaProrroga_avisos", resp_tedocs).Text;

        //            String UsuarioId_avisos = objfuncionesdicss.validareader("UsuarioId_avisos", "UsuarioId_avisos", resp_tedocs).Text;

        //            String usuarionombre_avisos = objfuncionesdicss.validareader("usuarionombre_avisos", "usuarionombre_avisos", resp_tedocs).Text;
        //            String PlazoFechaAtencion_avisos = objfuncionesdicss.validareader("PlazoFechaAtencion_avisos", "PlazoFechaAtencion_avisos", resp_tedocs).Text;
        //            String EstatusPlazoId_avisos = objfuncionesdicss.validareader("EstatusPlazoId_avisos", "EstatusPlazoId_avisos", resp_tedocs).Text;

        //            String Estatusplazodescrip_avisos = objfuncionesdicss.validareader("Estatusplazodescrip_avisos", "Estatusplazodescrip_avisos", resp_tedocs).Text;
        //            String UsuarioIdCancelo_avisos = objfuncionesdicss.validareader("UsuarioIdCancelo_avisos", "UsuarioIdCancelo_avisos", resp_tedocs).Text;
        //            //usuarionombre_cancelo_avisos
        //            String usuarionombre_cancelo_avisos = objfuncionesdicss.validareader("usuarionombre_cancelo_avisos", "UsuarioIdCancelo_avisos", resp_tedocs).Text;

        //            String PlazoDescripcion_avisos = objfuncionesdicss.validareader("PlazoDescripcion_avisos", "PlazoDescripcion_avisos", resp_tedocs).Text;
        //            String PlazoIdRef_avisos = objfuncionesdicss.validareader("PlazoIdRef_avisos", "PlazoIdRef_avisos", resp_tedocs).Text;
        //            String usuarioIdAtendio_avisos = objfuncionesdicss.validareader("usuarioIdAtendio_avisos", "usuarioIdAtendio_avisos", resp_tedocs).Text;

        //            //plazoid, documentoid, documentodescrip, grupoplazoid, tipoplazoid, tipoplazodescrip, estatusplazouno, fechaplazo, fechavencimientoplazouno, fechaprorrogaplazouno, fechaatendioplazouno, 
        //            //avisoalcliente, Estatusplazoaviso, fecheplazoaviso, fechavencimientoaviso, fechaatendioplazoaviso 

        //            dgPlazos.Rows.Add(sVPlazoId, sVDocumentoId, sVDocumentoDescrip, sVgrupoplazodescrip, sVTipoPlazoId, sVtipoplazodescrip, sVEstatusplazodescrip,
        //                sVPlazoFecha, sVPlazoFecha + "+ 2 meses", sVPlazoFechaProrroga, sVPlazoFechaAtencion, tipoplazodescrip_avisos, Estatusplazodescrip_avisos, PlazoFecha_avisos, 
        //                PlazoFecha_avisos+" + 2 meses", PlazoFechaAtencion_avisos);
        //        }
        //    }catch(Exception Ex){
        //        MessageBox.Show("Error: "+ Ex.Message);
        //    }
        //}
        public void consultaplazo_INSERT(){
            try { 
            
                //dgPlazos
                conect con_tcon_edocs = new conect();
                String sWueryplazos = "SELECT * FROM plazo order by CasoId, PlazoId asc;";
                //String sWueryplazos = "select * from  plazo where casoid = 13299;";
                //select * from  plazo where casoid = 38893;
                MySqlDataReader resp_tedocs = con_tcon_edocs.getdatareader(sWueryplazos);
                List<plazo> listadeplazos = new List<plazo>();
                int count_docelect = 0;
                plazo plazouno = new plazo();
                String sCasoidplazo = "27642";
                while (resp_tedocs.Read())
                {
                    plazouno = new plazo();
                    String sVPlazoId = objfuncionesdicss.validareader("PlazoId", "PlazoId", resp_tedocs).Text;
                    String sVTipoPlazoId = objfuncionesdicss.validareader("TipoPlazoId", "TipoPlazoId", resp_tedocs).Text;
                    String sVCasoId = objfuncionesdicss.validareader("CasoId", "CasoId", resp_tedocs).Text;
                    String sVDocumentoId = objfuncionesdicss.validareader("DocumentoId", "DocumentoId", resp_tedocs).Text;
                    String sVAnualidadId = objfuncionesdicss.validareader("AnualidadId", "AnualidadId", resp_tedocs).Text;
                    String sVClienteId = objfuncionesdicss.validareader("ClienteId", "ClienteId", resp_tedocs).Text;
                    String sVPlazoMotivoCancelacion = objfuncionesdicss.validareader("PlazoMotivoCancelacion", "PlazoMotivoCancelacion", resp_tedocs).Text;
                    String sVPlazoFecha = objfuncionesdicss.validareader("PlazoFecha", "PlazoFecha", resp_tedocs).Text;
                    String sVPlazoFechaProrroga = objfuncionesdicss.validareader("PlazoFechaProrroga", "PlazoFechaProrroga", resp_tedocs).Text;
                    String sVUsuarioId = objfuncionesdicss.validareader("UsuarioId", "UsuarioId", resp_tedocs).Text;
                    String sVPlazoFechaAtencion = objfuncionesdicss.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", resp_tedocs).Text;
                    String sVEstatusPlazoId = objfuncionesdicss.validareader("EstatusPlazoId", "EstatusPlazoId", resp_tedocs).Text;
                    String sVUsuarioIdCancelo = objfuncionesdicss.validareader("UsuarioIdCancelo", "UsuarioIdCancelo", resp_tedocs).Text;
                    String sVPlazoDescripcion = objfuncionesdicss.validareader("PlazoDescripcion", "PlazoDescripcion", resp_tedocs).Text;
                    String sVPlazoIdRef = objfuncionesdicss.validareader("PlazoIdRef", "PlazoIdRef", resp_tedocs).Text;
                    String sVusuarioIdAtendio = objfuncionesdicss.validareader("usuarioIdAtendio", "usuarioIdAtendio", resp_tedocs).Text;

                    if (sVCasoId == sCasoidplazo)//si es igual estamos llenando la lista por caso
                    {
                        
                        plazouno.PlazoId = objfuncionesdicss.validareader("PlazoId", "PlazoId", resp_tedocs).Text;
                        plazouno.TipoPlazoId = objfuncionesdicss.validareader("TipoPlazoId", "TipoPlazoId", resp_tedocs).Text;
                        plazouno.CasoId = objfuncionesdicss.validareader("CasoId", "CasoId", resp_tedocs).Text;
                        plazouno.DocumentoId = objfuncionesdicss.validareader("DocumentoId", "DocumentoId", resp_tedocs).Text;
                        plazouno.AnualidadId = objfuncionesdicss.validareader("AnualidadId", "AnualidadId", resp_tedocs).Text;
                        plazouno.ClienteId = objfuncionesdicss.validareader("ClienteId", "ClienteId", resp_tedocs).Text;
                        plazouno.PlazoMotivoCancelacion = objfuncionesdicss.validareader("PlazoMotivoCancelacion", "PlazoMotivoCancelacion", resp_tedocs).Text;
                        plazouno.PlazoFecha = objfuncionesdicss.validareader("PlazoFecha", "PlazoFecha", resp_tedocs).Text;
                        plazouno.PlazoFechaProrroga = objfuncionesdicss.validareader("PlazoFechaProrroga", "PlazoFechaProrroga", resp_tedocs).Text;
                        plazouno.UsuarioId = objfuncionesdicss.validareader("UsuarioId", "UsuarioId", resp_tedocs).Text;
                        plazouno.PlazoFechaAtencion = objfuncionesdicss.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", resp_tedocs).Text;
                        plazouno.EstatusPlazoId = objfuncionesdicss.validareader("EstatusPlazoId", "EstatusPlazoId", resp_tedocs).Text;
                        plazouno.UsuarioIdCancelo = objfuncionesdicss.validareader("UsuarioIdCancelo", "UsuarioIdCancelo", resp_tedocs).Text;
                        plazouno.PlazoDescripcion = objfuncionesdicss.validareader("PlazoDescripcion", "PlazoDescripcion", resp_tedocs).Text;
                        plazouno.PlazoIdRef = objfuncionesdicss.validareader("PlazoIdRef", "PlazoIdRef", resp_tedocs).Text;
                        plazouno.usuarioIdAtendio = objfuncionesdicss.validareader("usuarioIdAtendio", "usuarioIdAtendio", resp_tedocs).Text;
                        listadeplazos.Add(plazouno);
                        sCasoidplazo = sVCasoId;
                    }
                    else { //si no es igual entonces ejecutamos el insert y pasamos a llenar otra lista de plazos
                        //ejecutamos la funcion
                        insertaporcaso(listadeplazos);
                        //vacioamos la lista para poder llenarla para el siguiente caso
                        listadeplazos.Clear();
                        //llenamos el primer plazo del siguiente caso
                        plazouno.PlazoId = objfuncionesdicss.validareader("PlazoId", "PlazoId", resp_tedocs).Text;
                        plazouno.TipoPlazoId = objfuncionesdicss.validareader("TipoPlazoId", "TipoPlazoId", resp_tedocs).Text;
                        plazouno.CasoId = objfuncionesdicss.validareader("CasoId", "CasoId", resp_tedocs).Text;
                        plazouno.DocumentoId = objfuncionesdicss.validareader("DocumentoId", "DocumentoId", resp_tedocs).Text;
                        plazouno.AnualidadId = objfuncionesdicss.validareader("AnualidadId", "AnualidadId", resp_tedocs).Text;
                        plazouno.ClienteId = objfuncionesdicss.validareader("ClienteId", "ClienteId", resp_tedocs).Text;
                        plazouno.PlazoMotivoCancelacion = objfuncionesdicss.validareader("PlazoMotivoCancelacion", "PlazoMotivoCancelacion", resp_tedocs).Text;
                        plazouno.PlazoFecha = objfuncionesdicss.validareader("PlazoFecha", "PlazoFecha", resp_tedocs).Text;
                        plazouno.PlazoFechaProrroga = objfuncionesdicss.validareader("PlazoFechaProrroga", "PlazoFechaProrroga", resp_tedocs).Text;
                        plazouno.UsuarioId = objfuncionesdicss.validareader("UsuarioId", "UsuarioId", resp_tedocs).Text;
                        plazouno.PlazoFechaAtencion = objfuncionesdicss.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", resp_tedocs).Text;
                        plazouno.EstatusPlazoId = objfuncionesdicss.validareader("EstatusPlazoId", "EstatusPlazoId", resp_tedocs).Text;
                        plazouno.UsuarioIdCancelo = objfuncionesdicss.validareader("UsuarioIdCancelo", "UsuarioIdCancelo", resp_tedocs).Text;
                        plazouno.PlazoDescripcion = objfuncionesdicss.validareader("PlazoDescripcion", "PlazoDescripcion", resp_tedocs).Text;
                        plazouno.PlazoIdRef = objfuncionesdicss.validareader("PlazoIdRef", "PlazoIdRef", resp_tedocs).Text;
                        plazouno.usuarioIdAtendio = objfuncionesdicss.validareader("usuarioIdAtendio", "usuarioIdAtendio", resp_tedocs).Text;
                        sCasoidplazo = sVCasoId;
                        listadeplazos.Add(plazouno);
                    }
                    count_docelect++;
                }
                insertaporcaso(listadeplazos);
                resp_tedocs.Close();
                con_tcon_edocs.Cerrarconexion();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void insertaporcaso(List<plazo> listadeplazos)
        {
            String sFilelog = "";
            try {
                List<plazo> nuevalistadeplazos = new List<plazo>();
                bool sInsert = true;
                    for (int x = 0; x < listadeplazos.Count; x++)
                    {//aqui insertaremos y procesaremos las parejas de plazos por caso
                        sInsert = true;
                        int countsig = x + 1;
                        sFilelog += "countsig: " + countsig + "  tipoplazid: " + listadeplazos[x].TipoPlazoId;
                        if (countsig < listadeplazos.Count)
                        {
                            if (listadeplazos[x].TipoPlazoId == "2" && listadeplazos[x + 1].TipoPlazoId == "1")
                            {
                                listadeplazos[x].PlazoId_avisos = listadeplazos[x + 1].PlazoId;
                                listadeplazos[x].TipoPlazoId_avisos = listadeplazos[x + 1].TipoPlazoId;
                                listadeplazos[x].CasoId_avisos = listadeplazos[x + 1].CasoId;
                                listadeplazos[x].DocumentoId_avisos = listadeplazos[x + 1].DocumentoId;
                                listadeplazos[x].AnualidadId_avisos = listadeplazos[x + 1].AnualidadId;
                                listadeplazos[x].ClienteId_avisos = listadeplazos[x + 1].ClienteId;
                                listadeplazos[x].PlazoMotivoCancelacion_avisos = listadeplazos[x + 1].PlazoMotivoCancelacion;
                                listadeplazos[x].PlazoFecha_avisos = listadeplazos[x + 1].PlazoFecha;
                                listadeplazos[x].PlazoFechaProrroga_avisos = listadeplazos[x + 1].PlazoFechaProrroga;
                                listadeplazos[x].UsuarioId_avisos = listadeplazos[x + 1].UsuarioId;
                                listadeplazos[x].PlazoFechaAtencion_avisos = listadeplazos[x + 1].PlazoFechaAtencion;
                                listadeplazos[x].EstatusPlazoId_avisos = listadeplazos[x + 1].EstatusPlazoId;
                                listadeplazos[x].UsuarioIdCancelo_avisos = listadeplazos[x + 1].UsuarioIdCancelo;
                                listadeplazos[x].PlazoDescripcion_avisos = listadeplazos[x + 1].PlazoDescripcion;
                                listadeplazos[x].PlazoIdRef_avisos = listadeplazos[x + 1].PlazoIdRef;
                                listadeplazos[x].usuarioIdAtendio_avisos = listadeplazos[x + 1].usuarioIdAtendio;
                                insertpareja_dos(listadeplazos[x]);
                                x++;
                                sInsert = false;
                            }

                            if (listadeplazos[x].TipoPlazoId == "4" && listadeplazos[x + 1].TipoPlazoId == "22")
                            {
                                listadeplazos[x].PlazoId_avisos = listadeplazos[x + 1].PlazoId;
                                listadeplazos[x].TipoPlazoId_avisos = listadeplazos[x + 1].TipoPlazoId;
                                listadeplazos[x].CasoId_avisos = listadeplazos[x + 1].CasoId;
                                listadeplazos[x].DocumentoId_avisos = listadeplazos[x + 1].DocumentoId;
                                listadeplazos[x].AnualidadId_avisos = listadeplazos[x + 1].AnualidadId;
                                listadeplazos[x].ClienteId_avisos = listadeplazos[x + 1].ClienteId;
                                listadeplazos[x].PlazoMotivoCancelacion_avisos = listadeplazos[x + 1].PlazoMotivoCancelacion;
                                listadeplazos[x].PlazoFecha_avisos = listadeplazos[x + 1].PlazoFecha;
                                listadeplazos[x].PlazoFechaProrroga_avisos = listadeplazos[x + 1].PlazoFechaProrroga;
                                listadeplazos[x].UsuarioId_avisos = listadeplazos[x + 1].UsuarioId;
                                listadeplazos[x].PlazoFechaAtencion_avisos = listadeplazos[x + 1].PlazoFechaAtencion;
                                listadeplazos[x].EstatusPlazoId_avisos = listadeplazos[x + 1].EstatusPlazoId;
                                listadeplazos[x].UsuarioIdCancelo_avisos = listadeplazos[x + 1].UsuarioIdCancelo;
                                listadeplazos[x].PlazoDescripcion_avisos = listadeplazos[x + 1].PlazoDescripcion;
                                listadeplazos[x].PlazoIdRef_avisos = listadeplazos[x + 1].PlazoIdRef;
                                listadeplazos[x].usuarioIdAtendio_avisos = listadeplazos[x + 1].usuarioIdAtendio;
                                insertpareja_dos(listadeplazos[x]);
                                x++;
                                sInsert = false;

                            }


                            if (listadeplazos[x].TipoPlazoId == "6" && listadeplazos[x + 1].TipoPlazoId == "33")
                            {
                                listadeplazos[x].PlazoId_avisos = listadeplazos[x + 1].PlazoId;
                                listadeplazos[x].TipoPlazoId_avisos = listadeplazos[x + 1].TipoPlazoId;
                                listadeplazos[x].CasoId_avisos = listadeplazos[x + 1].CasoId;
                                listadeplazos[x].DocumentoId_avisos = listadeplazos[x + 1].DocumentoId;
                                listadeplazos[x].AnualidadId_avisos = listadeplazos[x + 1].AnualidadId;
                                listadeplazos[x].ClienteId_avisos = listadeplazos[x + 1].ClienteId;
                                listadeplazos[x].PlazoMotivoCancelacion_avisos = listadeplazos[x + 1].PlazoMotivoCancelacion;
                                listadeplazos[x].PlazoFecha_avisos = listadeplazos[x + 1].PlazoFecha;
                                listadeplazos[x].PlazoFechaProrroga_avisos = listadeplazos[x + 1].PlazoFechaProrroga;
                                listadeplazos[x].UsuarioId_avisos = listadeplazos[x + 1].UsuarioId;
                                listadeplazos[x].PlazoFechaAtencion_avisos = listadeplazos[x + 1].PlazoFechaAtencion;
                                listadeplazos[x].EstatusPlazoId_avisos = listadeplazos[x + 1].EstatusPlazoId;
                                listadeplazos[x].UsuarioIdCancelo_avisos = listadeplazos[x + 1].UsuarioIdCancelo;
                                listadeplazos[x].PlazoDescripcion_avisos = listadeplazos[x + 1].PlazoDescripcion;
                                listadeplazos[x].PlazoIdRef_avisos = listadeplazos[x + 1].PlazoIdRef;
                                listadeplazos[x].usuarioIdAtendio_avisos = listadeplazos[x + 1].usuarioIdAtendio;
                                insertpareja_dos(listadeplazos[x]);
                                x++;
                                sInsert = false;

                            }


                            if (listadeplazos[x].TipoPlazoId == "40" && listadeplazos[x + 1].TipoPlazoId == "41")
                            {
                                listadeplazos[x].PlazoId_avisos = listadeplazos[x + 1].PlazoId;
                                listadeplazos[x].TipoPlazoId_avisos = listadeplazos[x + 1].TipoPlazoId;
                                listadeplazos[x].CasoId_avisos = listadeplazos[x + 1].CasoId;
                                listadeplazos[x].DocumentoId_avisos = listadeplazos[x + 1].DocumentoId;
                                listadeplazos[x].AnualidadId_avisos = listadeplazos[x + 1].AnualidadId;
                                listadeplazos[x].ClienteId_avisos = listadeplazos[x + 1].ClienteId;
                                listadeplazos[x].PlazoMotivoCancelacion_avisos = listadeplazos[x + 1].PlazoMotivoCancelacion;
                                listadeplazos[x].PlazoFecha_avisos = listadeplazos[x + 1].PlazoFecha;
                                listadeplazos[x].PlazoFechaProrroga_avisos = listadeplazos[x + 1].PlazoFechaProrroga;
                                listadeplazos[x].UsuarioId_avisos = listadeplazos[x + 1].UsuarioId;
                                listadeplazos[x].PlazoFechaAtencion_avisos = listadeplazos[x + 1].PlazoFechaAtencion;
                                listadeplazos[x].EstatusPlazoId_avisos = listadeplazos[x + 1].EstatusPlazoId;
                                listadeplazos[x].UsuarioIdCancelo_avisos = listadeplazos[x + 1].UsuarioIdCancelo;
                                listadeplazos[x].PlazoDescripcion_avisos = listadeplazos[x + 1].PlazoDescripcion;
                                listadeplazos[x].PlazoIdRef_avisos = listadeplazos[x + 1].PlazoIdRef;
                                listadeplazos[x].usuarioIdAtendio_avisos = listadeplazos[x + 1].usuarioIdAtendio;
                                insertpareja_dos(listadeplazos[x]);
                                x++;
                                sInsert = false;

                            }

                            if (listadeplazos[x].TipoPlazoId == "20" && listadeplazos[x + 1].TipoPlazoId == "34")
                            {
                                listadeplazos[x].PlazoId_avisos = listadeplazos[x + 1].PlazoId;
                                listadeplazos[x].TipoPlazoId_avisos = listadeplazos[x + 1].TipoPlazoId;
                                listadeplazos[x].CasoId_avisos = listadeplazos[x + 1].CasoId;
                                listadeplazos[x].DocumentoId_avisos = listadeplazos[x + 1].DocumentoId;
                                listadeplazos[x].AnualidadId_avisos = listadeplazos[x + 1].AnualidadId;
                                listadeplazos[x].ClienteId_avisos = listadeplazos[x + 1].ClienteId;
                                listadeplazos[x].PlazoMotivoCancelacion_avisos = listadeplazos[x + 1].PlazoMotivoCancelacion;
                                listadeplazos[x].PlazoFecha_avisos = listadeplazos[x + 1].PlazoFecha;
                                listadeplazos[x].PlazoFechaProrroga_avisos = listadeplazos[x + 1].PlazoFechaProrroga;
                                listadeplazos[x].UsuarioId_avisos = listadeplazos[x + 1].UsuarioId;
                                listadeplazos[x].PlazoFechaAtencion_avisos = listadeplazos[x + 1].PlazoFechaAtencion;
                                listadeplazos[x].EstatusPlazoId_avisos = listadeplazos[x + 1].EstatusPlazoId;
                                listadeplazos[x].UsuarioIdCancelo_avisos = listadeplazos[x + 1].UsuarioIdCancelo;
                                listadeplazos[x].PlazoDescripcion_avisos = listadeplazos[x + 1].PlazoDescripcion;
                                listadeplazos[x].PlazoIdRef_avisos = listadeplazos[x + 1].PlazoIdRef;
                                listadeplazos[x].usuarioIdAtendio_avisos = listadeplazos[x + 1].usuarioIdAtendio;
                                insertpareja_dos(listadeplazos[x]);
                                x++;
                                sInsert = false;

                            }

                            if (listadeplazos[x].TipoPlazoId == "26" && listadeplazos[x + 1].TipoPlazoId == "36")
                            {
                                listadeplazos[x].PlazoId_avisos = listadeplazos[x + 1].PlazoId;
                                listadeplazos[x].TipoPlazoId_avisos = listadeplazos[x + 1].TipoPlazoId;
                                listadeplazos[x].CasoId_avisos = listadeplazos[x + 1].CasoId;
                                listadeplazos[x].DocumentoId_avisos = listadeplazos[x + 1].DocumentoId;
                                listadeplazos[x].AnualidadId_avisos = listadeplazos[x + 1].AnualidadId;
                                listadeplazos[x].ClienteId_avisos = listadeplazos[x + 1].ClienteId;
                                listadeplazos[x].PlazoMotivoCancelacion_avisos = listadeplazos[x + 1].PlazoMotivoCancelacion;
                                listadeplazos[x].PlazoFecha_avisos = listadeplazos[x + 1].PlazoFecha;
                                listadeplazos[x].PlazoFechaProrroga_avisos = listadeplazos[x + 1].PlazoFechaProrroga;
                                listadeplazos[x].UsuarioId_avisos = listadeplazos[x + 1].UsuarioId;
                                listadeplazos[x].PlazoFechaAtencion_avisos = listadeplazos[x + 1].PlazoFechaAtencion;
                                listadeplazos[x].EstatusPlazoId_avisos = listadeplazos[x + 1].EstatusPlazoId;
                                listadeplazos[x].UsuarioIdCancelo_avisos = listadeplazos[x + 1].UsuarioIdCancelo;
                                listadeplazos[x].PlazoDescripcion_avisos = listadeplazos[x + 1].PlazoDescripcion;
                                listadeplazos[x].PlazoIdRef_avisos = listadeplazos[x + 1].PlazoIdRef;
                                listadeplazos[x].usuarioIdAtendio_avisos = listadeplazos[x + 1].usuarioIdAtendio;
                                insertpareja_dos(listadeplazos[x]);
                                x++;
                                sInsert = false;

                            }
                            //sFilelog += "  tipoplazid: "+listadeplazos[x+1].TipoPlazoId;
                        }
                        
                        if (sInsert)
                        {//insertamos un plazo huerfano 
                            //hacemos el insert
                            conect conect_insert = new conect();
                            String sInsert_query = " INSERT INTO `plazos_king` " +
                                                " (`PlazoId`, " +
                                                " `TipoPlazoId`, " +
                                                " `CasoId`, " +
                                                " `DocumentoId`, " +
                                                " `AnualidadId`, " +
                                                " `ClienteId`, " +
                                                " `PlazoMotivoCancelacion`, " +
                                                " `PlazoFecha`, " +
                                                " `PlazoFechaProrroga`, " +
                                                " `UsuarioId`, " +
                                                " `PlazoFechaAtencion`, " +
                                                " `EstatusPlazoId`, " +
                                                " `UsuarioIdCancelo`, " +
                                                " `PlazoDescripcion`, " +
                                                " `PlazoIdRef`, " +
                                                " `usuarioIdAtendio`) " +
                                                " VALUES " +
                                                " ( " +
                                                " NULL, " +
                                                " '" + listadeplazos[x].TipoPlazoId + "', " +
                                                " '" + listadeplazos[x].CasoId + "', " +
                                                " '" + listadeplazos[x].DocumentoId + "', " +
                                                " '" + listadeplazos[x].AnualidadId + "', " +
                                                " '" + listadeplazos[x].ClienteId + "', " +
                                                " '" + listadeplazos[x].PlazoMotivoCancelacion + "', " +
                                                " '" + objfuncionesdicss.cambiodeformatofecha_yyyyMMdd(listadeplazos[x].PlazoFecha) + "', " +
                                                " '" + objfuncionesdicss.cambiodeformatofecha_yyyyMMdd(listadeplazos[x].PlazoFechaProrroga) + "', " +
                                                " '" + listadeplazos[x].UsuarioId + "', " +
                                                " '" + objfuncionesdicss.cambiodeformatofecha_yyyyMMdd(listadeplazos[x].PlazoFechaAtencion) + "', " +
                                                " '" + listadeplazos[x].EstatusPlazoId + "', " +
                                                " '" + listadeplazos[x].UsuarioIdCancelo + "', " +
                                                " '" + listadeplazos[x].PlazoDescripcion + "', " +
                                                " '" + listadeplazos[x].PlazoIdRef + "', " +
                                                " '" + listadeplazos[x].usuarioIdAtendio + "'); ";

                            sFilelog += "  insert huerfano: " + sInsert_query;
                            MySqlDataReader resp_insert = conect_insert.getdatareader(sInsert_query);
                            if (resp_insert.RecordsAffected == 1)
                            {

                            }
                            resp_insert.Close();
                            conect_insert.Cerrarconexion();
                        }


                    }
                }catch(Exception Ex){
                    //MessageBox.Show(Ex.Message);
                    new filelog("linea: 2867", sFilelog);
                }
        }

        private void insertpareja_dos(plazo plazouno)
        {
            try
            {
                //hacemos el insert
                conect conect_insert = new conect();
                String sInsert = " INSERT INTO `plazos_king` " +
                                    " (`PlazoId`, " +
                                    " `TipoPlazoId`, " +
                                    " `CasoId`, " +
                                    " `DocumentoId`, " +
                                    " `AnualidadId`, " +
                                    " `ClienteId`, " +
                                    " `PlazoMotivoCancelacion`, " +
                                    " `PlazoFecha`, " +
                                    " `PlazoFechaProrroga`, " +
                                    " `UsuarioId`, " +
                                    " `PlazoFechaAtencion`, " +
                                    " `EstatusPlazoId`, " +
                                    " `UsuarioIdCancelo`, " +
                                    " `PlazoDescripcion`, " +
                                    " `PlazoIdRef`, " +
                                    " `usuarioIdAtendio`, " +
                                    " `TipoPlazoId_avisos`, " +
                                    " `CasoId_avisos`, " +
                                    " `DocumentoId_avisos`, " +
                                    " `AnualidadId_avisos`, " +
                                    " `ClienteId_avisos`, " +
                                    " `PlazoMotivoCancelacion_avisos`, " +
                                    " `PlazoFecha_avisos`, " +
                                    " `PlazoFechaProrroga_avisos`, " +
                                    " `UsuarioId_avisos`, " +
                                    " `PlazoFechaAtencion_avisos`, " +
                                    " `EstatusPlazoId_avisos`, " +
                                    " `UsuarioIdCancelo_avisos`, " +
                                    " `PlazoDescripcion_avisos`, " +
                                    " `PlazoIdRef_avisos`, " +
                                    " `usuarioIdAtendio_avisos`) " +
                                    " VALUES " +
                                    " ( " +
                                    " NULL, " +
                                    " '" + plazouno.TipoPlazoId + "', " +
                                    " '" + plazouno.CasoId + "', " +
                                    " '" + plazouno.DocumentoId + "', " +
                                    " '" + plazouno.AnualidadId + "', " +
                                    " '" + plazouno.ClienteId + "', " +
                                    " '" + plazouno.PlazoMotivoCancelacion + "', " +
                                    " '" + objfuncionesdicss.cambiodeformatofecha_yyyyMMdd(plazouno.PlazoFecha) + "', " +
                                    " '" + objfuncionesdicss.cambiodeformatofecha_yyyyMMdd(plazouno.PlazoFechaProrroga) + "', " +
                                    " '" + plazouno.UsuarioId + "', " +
                                    " '" + objfuncionesdicss.cambiodeformatofecha_yyyyMMdd(plazouno.PlazoFechaAtencion) + "', " +
                                    " '" + plazouno.EstatusPlazoId + "', " +
                                    " '" + plazouno.UsuarioIdCancelo + "', " +
                                    " '" + plazouno.PlazoDescripcion + "', " +
                                    " '" + plazouno.PlazoIdRef + "', " +
                                    " '" + plazouno.usuarioIdAtendio + "', " +
                                    " '" + plazouno.TipoPlazoId_avisos + "', " +
                                    " '" + plazouno.CasoId_avisos + "', " +
                                    " '" + plazouno.DocumentoId_avisos + "', " +
                                    " '" + plazouno.AnualidadId_avisos + "', " +
                                    " '" + plazouno.ClienteId_avisos + "', " +
                                    " '" + plazouno.PlazoMotivoCancelacion_avisos + "', " +
                                    " '" + objfuncionesdicss.cambiodeformatofecha_yyyyMMdd(plazouno.PlazoFecha_avisos) + "', " +
                                    " '" + objfuncionesdicss.cambiodeformatofecha_yyyyMMdd(plazouno.PlazoFechaProrroga_avisos) + "', " +
                                    " '" + plazouno.UsuarioId_avisos + "', " +
                                    " '" + objfuncionesdicss.cambiodeformatofecha_yyyyMMdd(plazouno.PlazoFechaAtencion_avisos) + "', " +
                                    " '" + plazouno.EstatusPlazoId_avisos + "', " +
                                    " '" + plazouno.UsuarioIdCancelo_avisos + "', " +
                                    " '" + plazouno.PlazoDescripcion_avisos + "', " +
                                    " '" + plazouno.PlazoIdRef_avisos + "', " +
                                    " '" + plazouno.usuarioIdAtendio_avisos + "'); ";
                MySqlDataReader resp_insert = conect_insert.getdatareader(sInsert);
                if (resp_insert.RecordsAffected == 1)
                {

                }
                resp_insert.Close();
                conect_insert.Cerrarconexion();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error al inserta");
            }
        }


        public void insertaporcaso()
        {
            try {
                //for(int x=0; x < sPlazosporcaso.Count; x++){//aqui insertaremos y procesaremos las parejas de plazos por caso

                //}
                //plazo plazouno = new plazo();

                //switch (sVTipoPlazoId)
                //{
                //    case "2":
                //        {
                //            plazouno.PlazoId = objfuncionesdicss.validareader("PlazoId", "PlazoId", resp_tedocs).Text;
                //            plazouno.TipoPlazoId = objfuncionesdicss.validareader("TipoPlazoId", "TipoPlazoId", resp_tedocs).Text;
                //            plazouno.CasoId = objfuncionesdicss.validareader("CasoId", "CasoId", resp_tedocs).Text;
                //            plazouno.DocumentoId = objfuncionesdicss.validareader("DocumentoId", "DocumentoId", resp_tedocs).Text;
                //            plazouno.AnualidadId = objfuncionesdicss.validareader("AnualidadId", "AnualidadId", resp_tedocs).Text;
                //            plazouno.ClienteId = objfuncionesdicss.validareader("ClienteId", "ClienteId", resp_tedocs).Text;
                //            plazouno.PlazoMotivoCancelacion = objfuncionesdicss.validareader("PlazoMotivoCancelacion", "PlazoMotivoCancelacion", resp_tedocs).Text;
                //            plazouno.PlazoFecha = objfuncionesdicss.validareader("PlazoFecha", "PlazoFecha", resp_tedocs).Text;
                //            plazouno.PlazoFechaProrroga = objfuncionesdicss.validareader("PlazoFechaProrroga", "PlazoFechaProrroga", resp_tedocs).Text;
                //            plazouno.UsuarioId = objfuncionesdicss.validareader("UsuarioId", "UsuarioId", resp_tedocs).Text;
                //            plazouno.PlazoFechaAtencion = objfuncionesdicss.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", resp_tedocs).Text;
                //            plazouno.EstatusPlazoId = objfuncionesdicss.validareader("EstatusPlazoId", "EstatusPlazoId", resp_tedocs).Text;
                //            plazouno.UsuarioIdCancelo = objfuncionesdicss.validareader("UsuarioIdCancelo", "UsuarioIdCancelo", resp_tedocs).Text;
                //            plazouno.PlazoDescripcion = objfuncionesdicss.validareader("PlazoDescripcion", "PlazoDescripcion", resp_tedocs).Text;
                //            plazouno.PlazoIdRef = objfuncionesdicss.validareader("PlazoIdRef", "PlazoIdRef", resp_tedocs).Text;
                //            plazouno.usuarioIdAtendio = objfuncionesdicss.validareader("usuarioIdAtendio", "usuarioIdAtendio", resp_tedocs).Text;
                //        } break;
                //    case "4":
                //        {
                //            plazouno.PlazoId = objfuncionesdicss.validareader("PlazoId", "PlazoId", resp_tedocs).Text;
                //            plazouno.TipoPlazoId = objfuncionesdicss.validareader("TipoPlazoId", "TipoPlazoId", resp_tedocs).Text;
                //            plazouno.CasoId = objfuncionesdicss.validareader("CasoId", "CasoId", resp_tedocs).Text;
                //            plazouno.DocumentoId = objfuncionesdicss.validareader("DocumentoId", "DocumentoId", resp_tedocs).Text;
                //            plazouno.AnualidadId = objfuncionesdicss.validareader("AnualidadId", "AnualidadId", resp_tedocs).Text;
                //            plazouno.ClienteId = objfuncionesdicss.validareader("ClienteId", "ClienteId", resp_tedocs).Text;
                //            plazouno.PlazoMotivoCancelacion = objfuncionesdicss.validareader("PlazoMotivoCancelacion", "PlazoMotivoCancelacion", resp_tedocs).Text;
                //            plazouno.PlazoFecha = objfuncionesdicss.validareader("PlazoFecha", "PlazoFecha", resp_tedocs).Text;
                //            plazouno.PlazoFechaProrroga = objfuncionesdicss.validareader("PlazoFechaProrroga", "PlazoFechaProrroga", resp_tedocs).Text;
                //            plazouno.UsuarioId = objfuncionesdicss.validareader("UsuarioId", "UsuarioId", resp_tedocs).Text;
                //            plazouno.PlazoFechaAtencion = objfuncionesdicss.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", resp_tedocs).Text;
                //            plazouno.EstatusPlazoId = objfuncionesdicss.validareader("EstatusPlazoId", "EstatusPlazoId", resp_tedocs).Text;
                //            plazouno.UsuarioIdCancelo = objfuncionesdicss.validareader("UsuarioIdCancelo", "UsuarioIdCancelo", resp_tedocs).Text;
                //            plazouno.PlazoDescripcion = objfuncionesdicss.validareader("PlazoDescripcion", "PlazoDescripcion", resp_tedocs).Text;
                //            plazouno.PlazoIdRef = objfuncionesdicss.validareader("PlazoIdRef", "PlazoIdRef", resp_tedocs).Text;
                //            plazouno.usuarioIdAtendio = objfuncionesdicss.validareader("usuarioIdAtendio", "usuarioIdAtendio", resp_tedocs).Text;
                //        } break;
                //    case "6":
                //        {
                //            plazouno.PlazoId = objfuncionesdicss.validareader("PlazoId", "PlazoId", resp_tedocs).Text;
                //            plazouno.TipoPlazoId = objfuncionesdicss.validareader("TipoPlazoId", "TipoPlazoId", resp_tedocs).Text;
                //            plazouno.CasoId = objfuncionesdicss.validareader("CasoId", "CasoId", resp_tedocs).Text;
                //            plazouno.DocumentoId = objfuncionesdicss.validareader("DocumentoId", "DocumentoId", resp_tedocs).Text;
                //            plazouno.AnualidadId = objfuncionesdicss.validareader("AnualidadId", "AnualidadId", resp_tedocs).Text;
                //            plazouno.ClienteId = objfuncionesdicss.validareader("ClienteId", "ClienteId", resp_tedocs).Text;
                //            plazouno.PlazoMotivoCancelacion = objfuncionesdicss.validareader("PlazoMotivoCancelacion", "PlazoMotivoCancelacion", resp_tedocs).Text;
                //            plazouno.PlazoFecha = objfuncionesdicss.validareader("PlazoFecha", "PlazoFecha", resp_tedocs).Text;
                //            plazouno.PlazoFechaProrroga = objfuncionesdicss.validareader("PlazoFechaProrroga", "PlazoFechaProrroga", resp_tedocs).Text;
                //            plazouno.UsuarioId = objfuncionesdicss.validareader("UsuarioId", "UsuarioId", resp_tedocs).Text;
                //            plazouno.PlazoFechaAtencion = objfuncionesdicss.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", resp_tedocs).Text;
                //            plazouno.EstatusPlazoId = objfuncionesdicss.validareader("EstatusPlazoId", "EstatusPlazoId", resp_tedocs).Text;
                //            plazouno.UsuarioIdCancelo = objfuncionesdicss.validareader("UsuarioIdCancelo", "UsuarioIdCancelo", resp_tedocs).Text;
                //            plazouno.PlazoDescripcion = objfuncionesdicss.validareader("PlazoDescripcion", "PlazoDescripcion", resp_tedocs).Text;
                //            plazouno.PlazoIdRef = objfuncionesdicss.validareader("PlazoIdRef", "PlazoIdRef", resp_tedocs).Text;
                //            plazouno.usuarioIdAtendio = objfuncionesdicss.validareader("usuarioIdAtendio", "usuarioIdAtendio", resp_tedocs).Text;
                //        } break;
                //    case "40":
                //        {
                //            plazouno.PlazoId = objfuncionesdicss.validareader("PlazoId", "PlazoId", resp_tedocs).Text;
                //            plazouno.TipoPlazoId = objfuncionesdicss.validareader("TipoPlazoId", "TipoPlazoId", resp_tedocs).Text;
                //            plazouno.CasoId = objfuncionesdicss.validareader("CasoId", "CasoId", resp_tedocs).Text;
                //            plazouno.DocumentoId = objfuncionesdicss.validareader("DocumentoId", "DocumentoId", resp_tedocs).Text;
                //            plazouno.AnualidadId = objfuncionesdicss.validareader("AnualidadId", "AnualidadId", resp_tedocs).Text;
                //            plazouno.ClienteId = objfuncionesdicss.validareader("ClienteId", "ClienteId", resp_tedocs).Text;
                //            plazouno.PlazoMotivoCancelacion = objfuncionesdicss.validareader("PlazoMotivoCancelacion", "PlazoMotivoCancelacion", resp_tedocs).Text;
                //            plazouno.PlazoFecha = objfuncionesdicss.validareader("PlazoFecha", "PlazoFecha", resp_tedocs).Text;
                //            plazouno.PlazoFechaProrroga = objfuncionesdicss.validareader("PlazoFechaProrroga", "PlazoFechaProrroga", resp_tedocs).Text;
                //            plazouno.UsuarioId = objfuncionesdicss.validareader("UsuarioId", "UsuarioId", resp_tedocs).Text;
                //            plazouno.PlazoFechaAtencion = objfuncionesdicss.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", resp_tedocs).Text;
                //            plazouno.EstatusPlazoId = objfuncionesdicss.validareader("EstatusPlazoId", "EstatusPlazoId", resp_tedocs).Text;
                //            plazouno.UsuarioIdCancelo = objfuncionesdicss.validareader("UsuarioIdCancelo", "UsuarioIdCancelo", resp_tedocs).Text;
                //            plazouno.PlazoDescripcion = objfuncionesdicss.validareader("PlazoDescripcion", "PlazoDescripcion", resp_tedocs).Text;
                //            plazouno.PlazoIdRef = objfuncionesdicss.validareader("PlazoIdRef", "PlazoIdRef", resp_tedocs).Text;
                //            plazouno.usuarioIdAtendio = objfuncionesdicss.validareader("usuarioIdAtendio", "usuarioIdAtendio", resp_tedocs).Text;
                //        } break;
                //    case "20":
                //        {
                //            plazouno.PlazoId = objfuncionesdicss.validareader("PlazoId", "PlazoId", resp_tedocs).Text;
                //            plazouno.TipoPlazoId = objfuncionesdicss.validareader("TipoPlazoId", "TipoPlazoId", resp_tedocs).Text;
                //            plazouno.CasoId = objfuncionesdicss.validareader("CasoId", "CasoId", resp_tedocs).Text;
                //            plazouno.DocumentoId = objfuncionesdicss.validareader("DocumentoId", "DocumentoId", resp_tedocs).Text;
                //            plazouno.AnualidadId = objfuncionesdicss.validareader("AnualidadId", "AnualidadId", resp_tedocs).Text;
                //            plazouno.ClienteId = objfuncionesdicss.validareader("ClienteId", "ClienteId", resp_tedocs).Text;
                //            plazouno.PlazoMotivoCancelacion = objfuncionesdicss.validareader("PlazoMotivoCancelacion", "PlazoMotivoCancelacion", resp_tedocs).Text;
                //            plazouno.PlazoFecha = objfuncionesdicss.validareader("PlazoFecha", "PlazoFecha", resp_tedocs).Text;
                //            plazouno.PlazoFechaProrroga = objfuncionesdicss.validareader("PlazoFechaProrroga", "PlazoFechaProrroga", resp_tedocs).Text;
                //            plazouno.UsuarioId = objfuncionesdicss.validareader("UsuarioId", "UsuarioId", resp_tedocs).Text;
                //            plazouno.PlazoFechaAtencion = objfuncionesdicss.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", resp_tedocs).Text;
                //            plazouno.EstatusPlazoId = objfuncionesdicss.validareader("EstatusPlazoId", "EstatusPlazoId", resp_tedocs).Text;
                //            plazouno.UsuarioIdCancelo = objfuncionesdicss.validareader("UsuarioIdCancelo", "UsuarioIdCancelo", resp_tedocs).Text;
                //            plazouno.PlazoDescripcion = objfuncionesdicss.validareader("PlazoDescripcion", "PlazoDescripcion", resp_tedocs).Text;
                //            plazouno.PlazoIdRef = objfuncionesdicss.validareader("PlazoIdRef", "PlazoIdRef", resp_tedocs).Text;
                //            plazouno.usuarioIdAtendio = objfuncionesdicss.validareader("usuarioIdAtendio", "usuarioIdAtendio", resp_tedocs).Text;
                //        } break;
                //    case "26":
                //        {
                //            plazouno.PlazoId = objfuncionesdicss.validareader("PlazoId", "PlazoId", resp_tedocs).Text;
                //            plazouno.TipoPlazoId = objfuncionesdicss.validareader("TipoPlazoId", "TipoPlazoId", resp_tedocs).Text;
                //            plazouno.CasoId = objfuncionesdicss.validareader("CasoId", "CasoId", resp_tedocs).Text;
                //            plazouno.DocumentoId = objfuncionesdicss.validareader("DocumentoId", "DocumentoId", resp_tedocs).Text;
                //            plazouno.AnualidadId = objfuncionesdicss.validareader("AnualidadId", "AnualidadId", resp_tedocs).Text;
                //            plazouno.ClienteId = objfuncionesdicss.validareader("ClienteId", "ClienteId", resp_tedocs).Text;
                //            plazouno.PlazoMotivoCancelacion = objfuncionesdicss.validareader("PlazoMotivoCancelacion", "PlazoMotivoCancelacion", resp_tedocs).Text;
                //            plazouno.PlazoFecha = objfuncionesdicss.validareader("PlazoFecha", "PlazoFecha", resp_tedocs).Text;
                //            plazouno.PlazoFechaProrroga = objfuncionesdicss.validareader("PlazoFechaProrroga", "PlazoFechaProrroga", resp_tedocs).Text;
                //            plazouno.UsuarioId = objfuncionesdicss.validareader("UsuarioId", "UsuarioId", resp_tedocs).Text;
                //            plazouno.PlazoFechaAtencion = objfuncionesdicss.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", resp_tedocs).Text;
                //            plazouno.EstatusPlazoId = objfuncionesdicss.validareader("EstatusPlazoId", "EstatusPlazoId", resp_tedocs).Text;
                //            plazouno.UsuarioIdCancelo = objfuncionesdicss.validareader("UsuarioIdCancelo", "UsuarioIdCancelo", resp_tedocs).Text;
                //            plazouno.PlazoDescripcion = objfuncionesdicss.validareader("PlazoDescripcion", "PlazoDescripcion", resp_tedocs).Text;
                //            plazouno.PlazoIdRef = objfuncionesdicss.validareader("PlazoIdRef", "PlazoIdRef", resp_tedocs).Text;
                //            plazouno.usuarioIdAtendio = objfuncionesdicss.validareader("usuarioIdAtendio", "usuarioIdAtendio", resp_tedocs).Text;
                //        } break;

                //    case "1":
                //        {
                //            plazouno.PlazoId_avisos = objfuncionesdicss.validareader("PlazoId", "PlazoId", resp_tedocs).Text;
                //            plazouno.TipoPlazoId_avisos = objfuncionesdicss.validareader("TipoPlazoId", "TipoPlazoId", resp_tedocs).Text;
                //            plazouno.CasoId_avisos = objfuncionesdicss.validareader("CasoId", "CasoId", resp_tedocs).Text;
                //            plazouno.DocumentoId_avisos = objfuncionesdicss.validareader("DocumentoId", "DocumentoId", resp_tedocs).Text;
                //            plazouno.AnualidadId_avisos = objfuncionesdicss.validareader("AnualidadId", "AnualidadId", resp_tedocs).Text;
                //            plazouno.ClienteId_avisos = objfuncionesdicss.validareader("ClienteId", "ClienteId", resp_tedocs).Text;
                //            plazouno.PlazoMotivoCancelacion_avisos = objfuncionesdicss.validareader("PlazoMotivoCancelacion", "PlazoMotivoCancelacion", resp_tedocs).Text;
                //            plazouno.PlazoFecha_avisos = objfuncionesdicss.validareader("PlazoFecha", "PlazoFecha", resp_tedocs).Text;
                //            plazouno.PlazoFechaProrroga_avisos = objfuncionesdicss.validareader("PlazoFechaProrroga", "PlazoFechaProrroga", resp_tedocs).Text;
                //            plazouno.UsuarioId_avisos = objfuncionesdicss.validareader("UsuarioId", "UsuarioId", resp_tedocs).Text;
                //            plazouno.PlazoFechaAtencion_avisos = objfuncionesdicss.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", resp_tedocs).Text;
                //            plazouno.EstatusPlazoId_avisos = objfuncionesdicss.validareader("EstatusPlazoId", "EstatusPlazoId", resp_tedocs).Text;
                //            plazouno.UsuarioIdCancelo_avisos = objfuncionesdicss.validareader("UsuarioIdCancelo", "UsuarioIdCancelo", resp_tedocs).Text;
                //            plazouno.PlazoDescripcion_avisos = objfuncionesdicss.validareader("PlazoDescripcion", "PlazoDescripcion", resp_tedocs).Text;
                //            plazouno.PlazoIdRef_avisos = objfuncionesdicss.validareader("PlazoIdRef", "PlazoIdRef", resp_tedocs).Text;
                //            plazouno.usuarioIdAtendio_avisos = objfuncionesdicss.validareader("usuarioIdAtendio", "usuarioIdAtendio", resp_tedocs).Text;
                //        } break;
                //    case "22":
                //        {
                //            plazouno.PlazoId_avisos = objfuncionesdicss.validareader("PlazoId", "PlazoId", resp_tedocs).Text;
                //            plazouno.TipoPlazoId_avisos = objfuncionesdicss.validareader("TipoPlazoId", "TipoPlazoId", resp_tedocs).Text;
                //            plazouno.CasoId_avisos = objfuncionesdicss.validareader("CasoId", "CasoId", resp_tedocs).Text;
                //            plazouno.DocumentoId_avisos = objfuncionesdicss.validareader("DocumentoId", "DocumentoId", resp_tedocs).Text;
                //            plazouno.AnualidadId_avisos = objfuncionesdicss.validareader("AnualidadId", "AnualidadId", resp_tedocs).Text;
                //            plazouno.ClienteId_avisos = objfuncionesdicss.validareader("ClienteId", "ClienteId", resp_tedocs).Text;
                //            plazouno.PlazoMotivoCancelacion_avisos = objfuncionesdicss.validareader("PlazoMotivoCancelacion", "PlazoMotivoCancelacion", resp_tedocs).Text;
                //            plazouno.PlazoFecha_avisos = objfuncionesdicss.validareader("PlazoFecha", "PlazoFecha", resp_tedocs).Text;
                //            plazouno.PlazoFechaProrroga_avisos = objfuncionesdicss.validareader("PlazoFechaProrroga", "PlazoFechaProrroga", resp_tedocs).Text;
                //            plazouno.UsuarioId_avisos = objfuncionesdicss.validareader("UsuarioId", "UsuarioId", resp_tedocs).Text;
                //            plazouno.PlazoFechaAtencion_avisos = objfuncionesdicss.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", resp_tedocs).Text;
                //            plazouno.EstatusPlazoId_avisos = objfuncionesdicss.validareader("EstatusPlazoId", "EstatusPlazoId", resp_tedocs).Text;
                //            plazouno.UsuarioIdCancelo_avisos = objfuncionesdicss.validareader("UsuarioIdCancelo", "UsuarioIdCancelo", resp_tedocs).Text;
                //            plazouno.PlazoDescripcion_avisos = objfuncionesdicss.validareader("PlazoDescripcion", "PlazoDescripcion", resp_tedocs).Text;
                //            plazouno.PlazoIdRef_avisos = objfuncionesdicss.validareader("PlazoIdRef", "PlazoIdRef", resp_tedocs).Text;
                //            plazouno.usuarioIdAtendio_avisos = objfuncionesdicss.validareader("usuarioIdAtendio", "usuarioIdAtendio", resp_tedocs).Text;
                //        } break;
                //    case "33":
                //        {
                //            plazouno.PlazoId_avisos = objfuncionesdicss.validareader("PlazoId", "PlazoId", resp_tedocs).Text;
                //            plazouno.TipoPlazoId_avisos = objfuncionesdicss.validareader("TipoPlazoId", "TipoPlazoId", resp_tedocs).Text;
                //            plazouno.CasoId_avisos = objfuncionesdicss.validareader("CasoId", "CasoId", resp_tedocs).Text;
                //            plazouno.DocumentoId_avisos = objfuncionesdicss.validareader("DocumentoId", "DocumentoId", resp_tedocs).Text;
                //            plazouno.AnualidadId_avisos = objfuncionesdicss.validareader("AnualidadId", "AnualidadId", resp_tedocs).Text;
                //            plazouno.ClienteId_avisos = objfuncionesdicss.validareader("ClienteId", "ClienteId", resp_tedocs).Text;
                //            plazouno.PlazoMotivoCancelacion_avisos = objfuncionesdicss.validareader("PlazoMotivoCancelacion", "PlazoMotivoCancelacion", resp_tedocs).Text;
                //            plazouno.PlazoFecha_avisos = objfuncionesdicss.validareader("PlazoFecha", "PlazoFecha", resp_tedocs).Text;
                //            plazouno.PlazoFechaProrroga_avisos = objfuncionesdicss.validareader("PlazoFechaProrroga", "PlazoFechaProrroga", resp_tedocs).Text;
                //            plazouno.UsuarioId_avisos = objfuncionesdicss.validareader("UsuarioId", "UsuarioId", resp_tedocs).Text;
                //            plazouno.PlazoFechaAtencion_avisos = objfuncionesdicss.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", resp_tedocs).Text;
                //            plazouno.EstatusPlazoId_avisos = objfuncionesdicss.validareader("EstatusPlazoId", "EstatusPlazoId", resp_tedocs).Text;
                //            plazouno.UsuarioIdCancelo_avisos = objfuncionesdicss.validareader("UsuarioIdCancelo", "UsuarioIdCancelo", resp_tedocs).Text;
                //            plazouno.PlazoDescripcion_avisos = objfuncionesdicss.validareader("PlazoDescripcion", "PlazoDescripcion", resp_tedocs).Text;
                //            plazouno.PlazoIdRef_avisos = objfuncionesdicss.validareader("PlazoIdRef", "PlazoIdRef", resp_tedocs).Text;
                //            plazouno.usuarioIdAtendio_avisos = objfuncionesdicss.validareader("usuarioIdAtendio", "usuarioIdAtendio", resp_tedocs).Text;
                //        } break;
                //    case "41":
                //        {
                //            plazouno.PlazoId_avisos = objfuncionesdicss.validareader("PlazoId", "PlazoId", resp_tedocs).Text;
                //            plazouno.TipoPlazoId_avisos = objfuncionesdicss.validareader("TipoPlazoId", "TipoPlazoId", resp_tedocs).Text;
                //            plazouno.CasoId_avisos = objfuncionesdicss.validareader("CasoId", "CasoId", resp_tedocs).Text;
                //            plazouno.DocumentoId_avisos = objfuncionesdicss.validareader("DocumentoId", "DocumentoId", resp_tedocs).Text;
                //            plazouno.AnualidadId_avisos = objfuncionesdicss.validareader("AnualidadId", "AnualidadId", resp_tedocs).Text;
                //            plazouno.ClienteId_avisos = objfuncionesdicss.validareader("ClienteId", "ClienteId", resp_tedocs).Text;
                //            plazouno.PlazoMotivoCancelacion_avisos = objfuncionesdicss.validareader("PlazoMotivoCancelacion", "PlazoMotivoCancelacion", resp_tedocs).Text;
                //            plazouno.PlazoFecha_avisos = objfuncionesdicss.validareader("PlazoFecha", "PlazoFecha", resp_tedocs).Text;
                //            plazouno.PlazoFechaProrroga_avisos = objfuncionesdicss.validareader("PlazoFechaProrroga", "PlazoFechaProrroga", resp_tedocs).Text;
                //            plazouno.UsuarioId_avisos = objfuncionesdicss.validareader("UsuarioId", "UsuarioId", resp_tedocs).Text;
                //            plazouno.PlazoFechaAtencion_avisos = objfuncionesdicss.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", resp_tedocs).Text;
                //            plazouno.EstatusPlazoId_avisos = objfuncionesdicss.validareader("EstatusPlazoId", "EstatusPlazoId", resp_tedocs).Text;
                //            plazouno.UsuarioIdCancelo_avisos = objfuncionesdicss.validareader("UsuarioIdCancelo", "UsuarioIdCancelo", resp_tedocs).Text;
                //            plazouno.PlazoDescripcion_avisos = objfuncionesdicss.validareader("PlazoDescripcion", "PlazoDescripcion", resp_tedocs).Text;
                //            plazouno.PlazoIdRef_avisos = objfuncionesdicss.validareader("PlazoIdRef", "PlazoIdRef", resp_tedocs).Text;
                //            plazouno.usuarioIdAtendio_avisos = objfuncionesdicss.validareader("usuarioIdAtendio", "usuarioIdAtendio", resp_tedocs).Text;
                //        } break;
                //    case "34":
                //        {
                //            plazouno.PlazoId_avisos = objfuncionesdicss.validareader("PlazoId", "PlazoId", resp_tedocs).Text;
                //            plazouno.TipoPlazoId_avisos = objfuncionesdicss.validareader("TipoPlazoId", "TipoPlazoId", resp_tedocs).Text;
                //            plazouno.CasoId_avisos = objfuncionesdicss.validareader("CasoId", "CasoId", resp_tedocs).Text;
                //            plazouno.DocumentoId_avisos = objfuncionesdicss.validareader("DocumentoId", "DocumentoId", resp_tedocs).Text;
                //            plazouno.AnualidadId_avisos = objfuncionesdicss.validareader("AnualidadId", "AnualidadId", resp_tedocs).Text;
                //            plazouno.ClienteId_avisos = objfuncionesdicss.validareader("ClienteId", "ClienteId", resp_tedocs).Text;
                //            plazouno.PlazoMotivoCancelacion_avisos = objfuncionesdicss.validareader("PlazoMotivoCancelacion", "PlazoMotivoCancelacion", resp_tedocs).Text;
                //            plazouno.PlazoFecha_avisos = objfuncionesdicss.validareader("PlazoFecha", "PlazoFecha", resp_tedocs).Text;
                //            plazouno.PlazoFechaProrroga_avisos = objfuncionesdicss.validareader("PlazoFechaProrroga", "PlazoFechaProrroga", resp_tedocs).Text;
                //            plazouno.UsuarioId_avisos = objfuncionesdicss.validareader("UsuarioId", "UsuarioId", resp_tedocs).Text;
                //            plazouno.PlazoFechaAtencion_avisos = objfuncionesdicss.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", resp_tedocs).Text;
                //            plazouno.EstatusPlazoId_avisos = objfuncionesdicss.validareader("EstatusPlazoId", "EstatusPlazoId", resp_tedocs).Text;
                //            plazouno.UsuarioIdCancelo_avisos = objfuncionesdicss.validareader("UsuarioIdCancelo", "UsuarioIdCancelo", resp_tedocs).Text;
                //            plazouno.PlazoDescripcion_avisos = objfuncionesdicss.validareader("PlazoDescripcion", "PlazoDescripcion", resp_tedocs).Text;
                //            plazouno.PlazoIdRef_avisos = objfuncionesdicss.validareader("PlazoIdRef", "PlazoIdRef", resp_tedocs).Text;
                //            plazouno.usuarioIdAtendio_avisos = objfuncionesdicss.validareader("usuarioIdAtendio", "usuarioIdAtendio", resp_tedocs).Text;
                //        } break;
                //    case "36":
                //        {
                //            plazouno.PlazoId_avisos = objfuncionesdicss.validareader("PlazoId", "PlazoId", resp_tedocs).Text;
                //            plazouno.TipoPlazoId_avisos = objfuncionesdicss.validareader("TipoPlazoId", "TipoPlazoId", resp_tedocs).Text;
                //            plazouno.CasoId_avisos = objfuncionesdicss.validareader("CasoId", "CasoId", resp_tedocs).Text;
                //            plazouno.DocumentoId_avisos = objfuncionesdicss.validareader("DocumentoId", "DocumentoId", resp_tedocs).Text;
                //            plazouno.AnualidadId_avisos = objfuncionesdicss.validareader("AnualidadId", "AnualidadId", resp_tedocs).Text;
                //            plazouno.ClienteId_avisos = objfuncionesdicss.validareader("ClienteId", "ClienteId", resp_tedocs).Text;
                //            plazouno.PlazoMotivoCancelacion_avisos = objfuncionesdicss.validareader("PlazoMotivoCancelacion", "PlazoMotivoCancelacion", resp_tedocs).Text;
                //            plazouno.PlazoFecha_avisos = objfuncionesdicss.validareader("PlazoFecha", "PlazoFecha", resp_tedocs).Text;
                //            plazouno.PlazoFechaProrroga_avisos = objfuncionesdicss.validareader("PlazoFechaProrroga", "PlazoFechaProrroga", resp_tedocs).Text;
                //            plazouno.UsuarioId_avisos = objfuncionesdicss.validareader("UsuarioId", "UsuarioId", resp_tedocs).Text;
                //            plazouno.PlazoFechaAtencion_avisos = objfuncionesdicss.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", resp_tedocs).Text;
                //            plazouno.EstatusPlazoId_avisos = objfuncionesdicss.validareader("EstatusPlazoId", "EstatusPlazoId", resp_tedocs).Text;
                //            plazouno.UsuarioIdCancelo_avisos = objfuncionesdicss.validareader("UsuarioIdCancelo", "UsuarioIdCancelo", resp_tedocs).Text;
                //            plazouno.PlazoDescripcion_avisos = objfuncionesdicss.validareader("PlazoDescripcion", "PlazoDescripcion", resp_tedocs).Text;
                //            plazouno.PlazoIdRef_avisos = objfuncionesdicss.validareader("PlazoIdRef", "PlazoIdRef", resp_tedocs).Text;
                //            plazouno.usuarioIdAtendio_avisos = objfuncionesdicss.validareader("usuarioIdAtendio", "usuarioIdAtendio", resp_tedocs).Text;
                //        } break;
                //    case "vacio":
                //        {
                //        } break;
                //    default:
                //        {
                //            plazouno.PlazoId = objfuncionesdicss.validareader("PlazoId", "PlazoId", resp_tedocs).Text;
                //            plazouno.TipoPlazoId = objfuncionesdicss.validareader("TipoPlazoId", "TipoPlazoId", resp_tedocs).Text;
                //            plazouno.CasoId = objfuncionesdicss.validareader("CasoId", "CasoId", resp_tedocs).Text;
                //            plazouno.DocumentoId = objfuncionesdicss.validareader("DocumentoId", "DocumentoId", resp_tedocs).Text;
                //            plazouno.AnualidadId = objfuncionesdicss.validareader("AnualidadId", "AnualidadId", resp_tedocs).Text;
                //            plazouno.ClienteId = objfuncionesdicss.validareader("ClienteId", "ClienteId", resp_tedocs).Text;
                //            plazouno.PlazoMotivoCancelacion = objfuncionesdicss.validareader("PlazoMotivoCancelacion", "PlazoMotivoCancelacion", resp_tedocs).Text;
                //            plazouno.PlazoFecha = objfuncionesdicss.validareader("PlazoFecha", "PlazoFecha", resp_tedocs).Text;
                //            plazouno.PlazoFechaProrroga = objfuncionesdicss.validareader("PlazoFechaProrroga", "PlazoFechaProrroga", resp_tedocs).Text;
                //            plazouno.UsuarioId = objfuncionesdicss.validareader("UsuarioId", "UsuarioId", resp_tedocs).Text;
                //            plazouno.PlazoFechaAtencion = objfuncionesdicss.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", resp_tedocs).Text;
                //            plazouno.EstatusPlazoId = objfuncionesdicss.validareader("EstatusPlazoId", "EstatusPlazoId", resp_tedocs).Text;
                //            plazouno.UsuarioIdCancelo = objfuncionesdicss.validareader("UsuarioIdCancelo", "UsuarioIdCancelo", resp_tedocs).Text;
                //            plazouno.PlazoDescripcion = objfuncionesdicss.validareader("PlazoDescripcion", "PlazoDescripcion", resp_tedocs).Text;
                //            plazouno.PlazoIdRef = objfuncionesdicss.validareader("PlazoIdRef", "PlazoIdRef", resp_tedocs).Text;
                //            plazouno.usuarioIdAtendio = objfuncionesdicss.validareader("usuarioIdAtendio", "usuarioIdAtendio", resp_tedocs).Text;

                //            //hacemos el insert
                //            conect conect_insert = new conect();
                //            String sInsert = " INSERT INTO `plazos_king` " +
                //                                " (`PlazoId`, " +
                //                                " `TipoPlazoId`, " +
                //                                " `CasoId`, " +
                //                                " `DocumentoId`, " +
                //                                " `AnualidadId`, " +
                //                                " `ClienteId`, " +
                //                                " `PlazoMotivoCancelacion`, " +
                //                                " `PlazoFecha`, " +
                //                                " `PlazoFechaProrroga`, " +
                //                                " `UsuarioId`, " +
                //                                " `PlazoFechaAtencion`, " +
                //                                " `EstatusPlazoId`, " +
                //                                " `UsuarioIdCancelo`, " +
                //                                " `PlazoDescripcion`, " +
                //                                " `PlazoIdRef`, " +
                //                                " `usuarioIdAtendio`, " +
                //                                " `TipoPlazoId_avisos`, " +
                //                                " `CasoId_avisos`, " +
                //                                " `DocumentoId_avisos`, " +
                //                                " `AnualidadId_avisos`, " +
                //                                " `ClienteId_avisos`, " +
                //                                " `PlazoMotivoCancelacion_avisos`, " +
                //                                " `PlazoFecha_avisos`, " +
                //                                " `PlazoFechaProrroga_avisos`, " +
                //                                " `UsuarioId_avisos`, " +
                //                                " `PlazoFechaAtencion_avisos`, " +
                //                                " `EstatusPlazoId_avisos`, " +
                //                                " `UsuarioIdCancelo_avisos`, " +
                //                                " `PlazoDescripcion_avisos`, " +
                //                                " `PlazoIdRef_avisos`, " +
                //                                " `usuarioIdAtendio_avisos`) " +
                //                                " VALUES " +
                //                                " ( " +
                //                                " '" + plazouno.PlazoId + "', " +
                //                                " '" + plazouno.TipoPlazoId + "', " +
                //                                " '" + plazouno.CasoId + "', " +
                //                                " '" + plazouno.DocumentoId + "', " +
                //                                " '" + plazouno.AnualidadId + "', " +
                //                                " '" + plazouno.ClienteId + "', " +
                //                                " '" + plazouno.PlazoMotivoCancelacion + "', " +
                //                                " '" + objfuncionesdicss.cambiodeformatofecha_yyyyMMdd(plazouno.PlazoFecha) + "', " +
                //                                " '" + objfuncionesdicss.cambiodeformatofecha_yyyyMMdd(plazouno.PlazoFechaProrroga) + "', " +
                //                                " '" + plazouno.UsuarioId + "', " +
                //                                " '" + objfuncionesdicss.cambiodeformatofecha_yyyyMMdd(plazouno.PlazoFechaAtencion) + "', " +
                //                                " '" + plazouno.EstatusPlazoId + "', " +
                //                                " '" + plazouno.UsuarioIdCancelo + "', " +
                //                                " '" + plazouno.PlazoDescripcion + "', " +
                //                                " '" + plazouno.PlazoIdRef + "', " +
                //                                " '" + plazouno.usuarioIdAtendio + "', " +
                //                                " '', " +
                //                                " '', " +
                //                                " '', " +
                //                                " '', " +
                //                                " '', " +
                //                                " '', " +
                //                                " '', " +
                //                                " '', " +
                //                                " '', " +
                //                                " '', " +
                //                                " '', " +
                //                                " '', " +
                //                                " '', " +
                //                                " '', " +
                //                                " ''); ";
                //            MySqlDataReader resp_insert = conect_insert.getdatareader(sInsert);
                //            if (resp_insert.RecordsAffected == 1)
                //            {

                //            }
                //            resp_insert.Close();
                //            conect_insert.Cerrarconexion();
                //            plazouno = new plazo();
                //        } break;
                //}


                //if (plazouno.TipoPlazoId != null && plazouno.TipoPlazoId_avisos != null && plazouno.CasoId == plazouno.CasoId_avisos)
                //{
                //    //hacemos el insert
                //    conect conect_insert = new conect();
                //    String sInsert = " INSERT INTO `plazos_king` " +
                //                        " (`PlazoId`, " +
                //                        " `TipoPlazoId`, " +
                //                        " `CasoId`, " +
                //                        " `DocumentoId`, " +
                //                        " `AnualidadId`, " +
                //                        " `ClienteId`, " +
                //                        " `PlazoMotivoCancelacion`, " +
                //                        " `PlazoFecha`, " +
                //                        " `PlazoFechaProrroga`, " +
                //                        " `UsuarioId`, " +
                //                        " `PlazoFechaAtencion`, " +
                //                        " `EstatusPlazoId`, " +
                //                        " `UsuarioIdCancelo`, " +
                //                        " `PlazoDescripcion`, " +
                //                        " `PlazoIdRef`, " +
                //                        " `usuarioIdAtendio`, " +
                //                        " `TipoPlazoId_avisos`, " +
                //                        " `CasoId_avisos`, " +
                //                        " `DocumentoId_avisos`, " +
                //                        " `AnualidadId_avisos`, " +
                //                        " `ClienteId_avisos`, " +
                //                        " `PlazoMotivoCancelacion_avisos`, " +
                //                        " `PlazoFecha_avisos`, " +
                //                        " `PlazoFechaProrroga_avisos`, " +
                //                        " `UsuarioId_avisos`, " +
                //                        " `PlazoFechaAtencion_avisos`, " +
                //                        " `EstatusPlazoId_avisos`, " +
                //                        " `UsuarioIdCancelo_avisos`, " +
                //                        " `PlazoDescripcion_avisos`, " +
                //                        " `PlazoIdRef_avisos`, " +
                //                        " `usuarioIdAtendio_avisos`) " +
                //                        " VALUES " +
                //                        " ( " +
                //                        " '" + plazouno.PlazoId + "', " +
                //                        " '" + plazouno.TipoPlazoId + "', " +
                //                        " '" + plazouno.CasoId + "', " +
                //                        " '" + plazouno.DocumentoId + "', " +
                //                        " '" + plazouno.AnualidadId + "', " +
                //                        " '" + plazouno.ClienteId + "', " +
                //                        " '" + plazouno.PlazoMotivoCancelacion + "', " +
                //                        " '" + objfuncionesdicss.cambiodeformatofecha_yyyyMMdd(plazouno.PlazoFecha) + "', " +
                //                        " '" + objfuncionesdicss.cambiodeformatofecha_yyyyMMdd(plazouno.PlazoFechaProrroga) + "', " +
                //                        " '" + plazouno.UsuarioId + "', " +
                //                        " '" + objfuncionesdicss.cambiodeformatofecha_yyyyMMdd(plazouno.PlazoFechaAtencion) + "', " +
                //                        " '" + plazouno.EstatusPlazoId + "', " +
                //                        " '" + plazouno.UsuarioIdCancelo + "', " +
                //                        " '" + plazouno.PlazoDescripcion + "', " +
                //                        " '" + plazouno.PlazoIdRef + "', " +
                //                        " '" + plazouno.usuarioIdAtendio + "', " +
                //                        " '" + plazouno.TipoPlazoId_avisos + "', " +
                //                        " '" + plazouno.CasoId_avisos + "', " +
                //                        " '" + plazouno.DocumentoId_avisos + "', " +
                //                        " '" + plazouno.AnualidadId_avisos + "', " +
                //                        " '" + plazouno.ClienteId_avisos + "', " +
                //                        " '" + plazouno.PlazoMotivoCancelacion_avisos + "', " +
                //                        " '" + objfuncionesdicss.cambiodeformatofecha_yyyyMMdd(plazouno.PlazoFecha_avisos) + "', " +
                //                        " '" + objfuncionesdicss.cambiodeformatofecha_yyyyMMdd(plazouno.PlazoFechaProrroga_avisos) + "', " +
                //                        " '" + plazouno.UsuarioId_avisos + "', " +
                //                        " '" + objfuncionesdicss.cambiodeformatofecha_yyyyMMdd(plazouno.PlazoFechaAtencion_avisos) + "', " +
                //                        " '" + plazouno.EstatusPlazoId_avisos + "', " +
                //                        " '" + plazouno.UsuarioIdCancelo_avisos + "', " +
                //                        " '" + plazouno.PlazoDescripcion_avisos + "', " +
                //                        " '" + plazouno.PlazoIdRef_avisos + "', " +
                //                        " '" + plazouno.usuarioIdAtendio_avisos + "'); ";
                //    //MySqlDataReader resp_insert = conect_insert.getdatareader(sInsert);
                //    //if (resp_insert.RecordsAffected == 1)
                //    //{

                //    //}
                //    //resp_insert.Close();
                //    //conect_insert.Cerrarconexion();
                //    plazouno = new plazo();
                //}

                //count_docelect++;
            }catch(Exception Ex){
                MessageBox.Show(Ex.Message);
            }
        }

        public void consultareferencias() { 
            //lvReferencias
            try
            {
                conect con_tcon_edocs = new conect();
                String sTipoEdocsquery = " select * from " +
                                        " referencia, tiporeferencia " +
                                        " where " +
                                        " referencia.TipoReferenciaId = tiporeferencia.TipoReferenciaId" +
                                        " AND referencia.CasoId =" +sCasoId +
                                        " AND TipoSolicitudId = "+gSTipoSolicitudId +";";
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
                new filelog(loguin.sId, "linea 3416: "+Ex.ToString());
            }

            try {
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
                new filelog(loguin.sId, "linea 3434: "+Ex.ToString());
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
                                        " AND documentoelectronico.CasoId = " + sCasoId + " and Tiposolicitudid = '" + gSTipoSolicitudId + "';";
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


                    ListViewItem itemslist = new ListViewItem(sDocumentoElectronicoDate);
                    itemslist.SubItems.Add(sUsuarioNombre + " " + sUsuarioPaterno);
                    itemslist.SubItems.Add(sTipoDocumentoElectronicoDescrip);
                    itemslist.SubItems.Add(sDocumentoElectronicoDescrip);
                    itemslist.SubItems.Add(sDocumentoElectronicoFilename);

                    dgDocumentoselectronicos.Rows.Add(sDocumentoElectronicoId,
                        sDocumentoElectronicoDate,
                        sUsuarioNombre + " " + sUsuarioPaterno,
                        sTipoDocumentoElectronicoDescrip,
                        sDocumentoElectronicoDescrip,
                        sDocumentoElectronicoFilename
                        );
                    //lv_documentelect.Items.Add(itemslist);
                    count_docelect++;
                }

                tb_contdocelect.Text = "" + count_docelect;
                resp_tedocs.Close();
                con_tcon_edocs.Cerrarconexion();
            }
            catch (Exception Ex)
            {
                new filelog(loguin.sId, "linea 3504:"+Ex.ToString());
            }
        }

        public void actualizainformaciondeinteresado() {
            //iniciamos la consulta del interesado
            try
            {
                conect con_casointeresado_count = new conect();
                String sInteresados_count = /* " select count(*)as numtotaldeinteresados " +
                                            " FROM " +
                                            "     casointeresado, " +
                                            "     interesado, " +
                                            "     direccion,  " +
                                            "     tiporelacion " +
                                            " WHERE " +
                                            "     casointeresado.CasoId = '" + sCasoId + "'" +
                                            " AND interesado.InteresadoID = casointeresado.InteresadoId " +
                                            " AND tiporelacion.TipoRelacionId = casointeresado.TipoRelacionId " +
                                            " AND casointeresado.DireccionId = direccion.DireccionId" +
                                            " GROUP BY interesado.InteresadoID order by casointeresado.CasoInteresadoSecuencia; ";*/
                                            " SELECT " +
                                            " count(distinct (interesado.InteresadoID)) as numtotaldeinteresados " +
                                            " FROM " +
                                            " casointeresado " +
                                            " LEFT JOIN " +
                                            " interesado ON casointeresado.InteresadoId = interesado.InteresadoID " +
                                            " Left JOIN  " +
                                            " tiporelacion ON casointeresado.TipoRelacionId = tiporelacion.TipoRelacionId  " +
                                            " LEFT join " +
                                            " direccion ON direccion.InteresadoID = interesado.InteresadoID " +
                                            " where casointeresado.CasoId = '"+ sCasoId + "' "+
                                            " and casointeresado.TipoSolicitudId = '"+ gSTipoSolicitudId + "'"+
                                            " AND casointeresado.TipoRelacionId in(1, 3) " +
                                            " group by interesado.InteresadoID" +
                                            " order by casointeresado.CasoInteresadoSecuencia;";
                                            
                                            //"SELECT  " +
                                            //"    count(distinct (interesado.InteresadoID)) as numtotaldeinteresados " +
                                            //"FROM " +
                                            //"    casointeresado, " +
                                            //"    interesado, " +
                                            //"    tiporelacion, " +
                                            //"    direccion " +
                                            //"WHERE " +
                                            //"    casointeresado.CasoId = '" + sCasoId + "' " +
                                            //"    and casointeresado.TipoSolicitudId = '" + sTiposolicitudGlobal + "' " +
                                            //"    and interesado.InteresadoID = casointeresado.InteresadoId " +
                                            //"    and tiporelacion.TipoRelacionId = casointeresado.TipoRelacionId " +
                                            ////"    and direccion.direccionid = casointeresado.direccionid " +
                                            //"  AND direccion.InteresadoID = interesado.InteresadoID " +
                                            //"group by interesado.InteresadoID " +
                                            //"order by casointeresado.CasoInteresadoSecuencia; ";
                MySqlDataReader respuestastringinteresados_count = con_casointeresado_count.getdatareader(sInteresados_count);
                int iCount_tam = 0;
                while (respuestastringinteresados_count.Read())
                {
                    iCount_tam++;
                }
                if (iCount_tam==0)
                {
                    return;
                }
                TipoRelacionId = new String[iCount_tam];
                InteresadoCurp = new String[iCount_tam];
                InteresadoNombre = new String[iCount_tam];
                InteresadoApPaterno = new String[iCount_tam];
                InteresadoApMaterno = new String[iCount_tam];
                nacionalidad = new String[iCount_tam];
                InteresadoRFC = new String[iCount_tam];

                sgDireccionCalle = new String[iCount_tam];
                sgDireccionNumExt = new String[iCount_tam];
                sgDireccionNumInt = new String[iCount_tam];
                sgDireccionColonia = new String[iCount_tam];
                sgDireccionCP = new String[iCount_tam];
                sgDireccionEstado = new String[iCount_tam];
                sgDireccionPoblacion = new String[iCount_tam];
                sgNombrepais = new String[iCount_tam];

                respuestastringinteresados_count.Close();
                con_casointeresado_count.Cerrarconexion();

                //actualizatablainteresado();
                //xxx
                conect con_casointeresado = new conect();
                //String sInteresados = " SELECT  " +
                //                        " casointeresado.CasoId, " +
                //                        "     casointeresado.CasoInteresadoSecuencia, " +
                //                        "     interesado.InteresadoID, " +
                //                        "     interesado.InteresadoTipoPersonaSAT, " +
                //                        "     interesado.InteresadoNombre, " +
                //                        "     interesado.InteresadoApPaterno, " +
                //                        "     interesado.InteresadoApMaterno, " +
                //                        "     casointeresado.TipoRelacionId, " +
                //                        "     interesado.InteresadoRFC, " +
                //                        "     interesado.InteresadoCurp, " +
                //                        " CONCAT ( COALESCE(interesado.InteresadoNombre, ''  ), ' ', " +
                //                        " COALESCE(interesado.InteresadoApPaterno, ''), ' ', " +
                //                        " COALESCE(interesado.InteresadoApMaterno, '')) AS interesadonombrecompleto, " +
                //                        " CONCAT (  COALESCE(direccion.DireccionCalle, ''  ), ' ',  " +
                //                        " COALESCE(direccion.DireccionNumExt, ''), ' ', " +
                //                        " COALESCE(direccion.DireccionNumInt, ''), ' ', " +
                //                        " COALESCE(direccion.DireccionColonia, ''), ' ', " +
                //                        " COALESCE(direccion.DireccionPoblacion, ''), ' ', " +
                //                        " COALESCE(direccion.DireccionEstado, ''), ' ', " +
                //                        " COALESCE(direccion.DireccionCP, '')) AS direccioncompleta, " +
                //                        "     direccion.DireccionCalle, " +
                //                        "     direccion.DireccionNumExt, " +
                //                        "     direccion.DireccionNumInt, " +
                //                        "     direccion.DireccionColonia, " +
                //                        "     direccion.DireccionCP, " +
                //                        "     direccion.DireccionEstado, " +
                //                        "     direccion.DireccionPoblacion, " +
                //                        " Damelanacionalidad (interesado.PaisId )AS nacionalidad, " +
                //                        "     interesado.PaisId, " +
                //                        " DameNombrePais(interesado.PaisId) As Nombrepais, " +
                //                        "     tiporelacion.TipoRelacionDescrip, " +
                //                        "     interesado.InteresadoPoder, " +
                //    // "     pais.PaisNacionalidad, " +
                //                        "     interesado.InteresadoRGP " +
                //                        " FROM " +
                //                        "     casointeresado, " +
                //                        "     interesado, " +
                //                        "     direccion,  " +
                //    //"     caso,  " +
                //    //  "     pais,  " +
                //                        "     tiporelacion " +
                //                        " WHERE " +
                //                        "     casointeresado.CasoId = '" + sCasoId + "'" +
                //                        " AND casointeresado.TipoSolicitudId = '" + sTiposolicitudGlobal + "'" +
                //                        " AND interesado.InteresadoID = casointeresado.InteresadoId " +
                //                        " AND tiporelacion.TipoRelacionId = casointeresado.TipoRelacionId " +
                //    //"     AND casointeresado.CasoId = caso.CasoId " +
                //    // "     AND pais.PaisId = interesado.PaisId " +
                //                        " AND interesado.InteresadoID = direccion.InteresadoId" +
                //                        //" AND direccion.DireccionID = casointeresado.DireccionId " +
                //                        " GROUP BY interesado.InteresadoID order by casointeresado.CasoInteresadoId; ";
                String sInteresados = " SELECT" +
                                        " casointeresado.CasoId, " +
                                        " casointeresado.CasoInteresadoSecuencia, " +
                                        " interesado.InteresadoID, " +
                                        " interesado.InteresadoTipoPersonaSAT, " +
                                        " interesado.NombreUtilInt, " +
                                        " interesado.InteresadoApPaterno, " +
                                        " interesado.InteresadoApMaterno, " +
                                        " casointeresado.TipoRelacionId, " +
                                        " interesado.InteresadoRFC, " +
                                        " interesado.InteresadoCurp, " +

                                        " CONCAT (  COALESCE(direccion.DireccionCalle, ''  ), ' ',  " +
                                        " COALESCE(direccion.DireccionNumExt, ''), ' ', " +
                                        " COALESCE(direccion.DireccionNumInt, ''), ' ', " +
                                        " COALESCE(direccion.DireccionColonia, ''), ' ', " +
                                        " COALESCE(direccion.DireccionPoblacion, ''), ' ', " +
                                        " COALESCE(direccion.DireccionEstado, ''), ' ', " +
                                        " COALESCE(direccion.DireccionCP, '')) AS direccioncompleta, " +
                                        " direccion.DireccionCalle, " +
                                        " direccion.DireccionNumExt, " +
                                        " direccion.DireccionNumInt, " +
                                        " direccion.DireccionColonia, " +
                                        " direccion.DireccionCP, " +
                                        " direccion.DireccionEstado, " +
                                        " direccion.DireccionPoblacion, " +
                                        " Damelanacionalidad (interesado.PaisId )AS nacionalidad, " +
                                        " interesado.PaisId, " +
                                        " DameNombrePais(interesado.PaisId) As Nombrepais, " +
                                        " tiporelacion.TipoRelacionDescrip, " +
                                        " interesado.InteresadoPoder, " +
                                        " interesado.InteresadoRGP " +
                                        " FROM" +
                                        " casointeresado" +
                                        " LEFT JOIN" +
                                        " interesado ON casointeresado.InteresadoId = interesado.InteresadoID" +
                                        " left JOIN " +
                                        " tiporelacion ON casointeresado.TipoRelacionId = tiporelacion.TipoRelacionId " +
                                        " LEFT join" +
                                        " direccion ON direccion.DireccionId = casointeresado.DireccionId " +
                                        " where casointeresado.CasoId = '"+ sCasoId + "'"+
                                        " AND casointeresado.TipoSolicitudId = '" + gSTipoSolicitudId + "'" +
                                        " AND casointeresado.TipoRelacionId in(1, 3) "+
                                        " GROUP BY interesado.InteresadoID order by casointeresado.CasoInteresadoId;";
                MySqlDataReader respuestastringinteresados = con_casointeresado.getdatareader(sInteresados);
                lvinteresados.Items.Clear();
                int count = 0;
                String TIPOPERSONA = "";
                int iContadortitula = 0;
                int iContadorinventor = 0;
                int iContadorinventortitular = 0;

                while (respuestastringinteresados.Read())
                {

                    TipoRelacionId[count] = objfuncionesdicss.validareader("TipoRelacionId", "InteresadoID", respuestastringinteresados).Text;
                    InteresadoCurp[count] = objfuncionesdicss.validareader("InteresadoCurp", "InteresadoID", respuestastringinteresados).Text;
                    InteresadoNombre[count] = objfuncionesdicss.validareader("NombreUtilInt", "InteresadoID", respuestastringinteresados).Text;
                    InteresadoApPaterno[count] = objfuncionesdicss.validareader("InteresadoApPaterno", "InteresadoID", respuestastringinteresados).Text;
                    InteresadoApMaterno[count] = objfuncionesdicss.validareader("InteresadoApMaterno", "InteresadoID", respuestastringinteresados).Text;
                    nacionalidad[count] = objfuncionesdicss.validareader("nacionalidad", "InteresadoID", respuestastringinteresados).Text;
                    InteresadoRFC[count] = objfuncionesdicss.validareader("InteresadoRFC", "InteresadoID", respuestastringinteresados).Text;

                    sgDireccionCalle[count] = objfuncionesdicss.validareader("DireccionCalle", "InteresadoID", respuestastringinteresados).Text;
                    sgDireccionNumExt[count] = objfuncionesdicss.validareader("DireccionNumExt", "InteresadoID", respuestastringinteresados).Text;
                    sgDireccionNumInt[count] = objfuncionesdicss.validareader("DireccionNumInt", "InteresadoID", respuestastringinteresados).Text;
                    sgDireccionColonia[count] = objfuncionesdicss.validareader("DireccionColonia", "InteresadoID", respuestastringinteresados).Text;
                    sgDireccionCP[count] = objfuncionesdicss.validareader("DireccionCP", "InteresadoID", respuestastringinteresados).Text;
                    sgDireccionEstado[count] = objfuncionesdicss.validareader("DireccionEstado", "InteresadoID", respuestastringinteresados).Text;
                    sgDireccionPoblacion[count] = objfuncionesdicss.validareader("DireccionPoblacion", "InteresadoID", respuestastringinteresados).Text;

                    sgNombrepais[count] = objfuncionesdicss.validareader("Nombrepais", "InteresadoID", respuestastringinteresados).Text;
                    switch (objfuncionesdicss.validareader("InteresadoTipoPersonaSAT", "InteresadoID", respuestastringinteresados).Text)
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
                    //iContadortitula++;
                    switch (objfuncionesdicss.validareader("TipoRelacionDescrip", "InteresadoID", respuestastringinteresados).Text)
                    {
                        case "Titular":
                            {
                                iContadortitula++;
                            }
                            break;
                        case "Inventor":
                            {
                                iContadorinventor++;
                            }
                            break;
                        case "Titular/Inventor":
                            {
                                iContadorinventortitular++;
                            }
                            break;
                    }
                    ListViewItem listinteresados = new ListViewItem(objfuncionesdicss.validareader("TipoRelacionDescrip", "InteresadoID", respuestastringinteresados).Text);
                    listinteresados.SubItems.Add(objfuncionesdicss.validareader("InteresadoID", "InteresadoID", respuestastringinteresados).Text);
                    listinteresados.SubItems.Add(objfuncionesdicss.validareader("TipoRelacionDescrip", "InteresadoID", respuestastringinteresados).Text);//tipo de persona
                    listinteresados.SubItems.Add(objfuncionesdicss.validareader("NombreUtilInt", "InteresadoID", respuestastringinteresados).Text);
                    listinteresados.SubItems.Add(objfuncionesdicss.validareader("nacionalidad", "InteresadoID", respuestastringinteresados).Text);
                    listinteresados.SubItems.Add(objfuncionesdicss.validareader("direccioncompleta", "InteresadoID", respuestastringinteresados).Text);
                    listinteresados.SubItems.Add(objfuncionesdicss.validareader("InteresadoPoder", "InteresadoID", respuestastringinteresados).Text);
                    listinteresados.SubItems.Add(objfuncionesdicss.validareader("InteresadoRGP", "InteresadoID", respuestastringinteresados).Text);
                    listinteresados.SubItems.Add(TIPOPERSONA);
                    
                    int residuo = count % 2;
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
                    count++;

                }
                //cerrar conexion
                respuestastringinteresados.Close();
                con_casointeresado.Cerrarconexion();
                tb_numtit.Text = iContadortitula + "";
                //tb_inv.Text = iContadorinventor + "";
                //tb_titinv.Text = iContadorinventortitular + "";
            }
            catch (Exception Ex)
            {
                new filelog(loguin.sId, "linea 3714: Error en el interesado" + Ex.ToString());
            }
            //aqui termina la dirección
        }

        public void actualizainformaciondeinvnetores()
        {
            //iniciamos la consulta del interesado
            try
            {
                conect con_casointeresado_count = new conect();
                String sInteresados_count = 
                                            " SELECT " +
                                            " count(distinct (interesado.InteresadoID)) as numtotaldeinteresados " +
                                            " FROM " +
                                            " casointeresado " +
                                            " LEFT JOIN " +
                                            " interesado ON casointeresado.InteresadoId = interesado.InteresadoID " +
                                            " Left JOIN  " +
                                            " tiporelacion ON casointeresado.TipoRelacionId = tiporelacion.TipoRelacionId  " +
                                            " LEFT join " +
                                            " direccion ON direccion.InteresadoID = interesado.InteresadoID " +
                                            " where casointeresado.CasoId = '" + sCasoId + "' " +
                                            " and casointeresado.TipoSolicitudId = '" + gSTipoSolicitudId + "'" +
                                            " group by interesado.InteresadoID" +
                                            " order by casointeresado.CasoInteresadoSecuencia;";
                MySqlDataReader respuestastringinteresados_count = con_casointeresado_count.getdatareader(sInteresados_count);
                int iCount_tam = 0;
                while (respuestastringinteresados_count.Read())
                {
                    iCount_tam++;
                }
                if (iCount_tam == 0)
                {
                    return;
                }
                /*
                 * TipoRelacionId = new String[iCount_tam];
                InteresadoCurp = new String[iCount_tam];
                InteresadoNombre = new String[iCount_tam];
                InteresadoApPaterno = new String[iCount_tam];
                InteresadoApMaterno = new String[iCount_tam];
                nacionalidad = new String[iCount_tam];
                InteresadoRFC = new String[iCount_tam];

                sgDireccionCalle = new String[iCount_tam];
                sgDireccionNumExt = new String[iCount_tam];
                sgDireccionNumInt = new String[iCount_tam];
                sgDireccionColonia = new String[iCount_tam];
                sgDireccionCP = new String[iCount_tam];
                sgDireccionEstado = new String[iCount_tam];
                sgDireccionPoblacion = new String[iCount_tam];
                sgNombrepais = new String[iCount_tam];*/

                respuestastringinteresados_count.Close();
                con_casointeresado_count.Cerrarconexion();

                //actualizatablainteresado();
                //xxx
                conect con_casointeresado = new conect();
                String sInteresados = " SELECT" +
                                        " casoinventor.CasoId, " +
                                        " casoinventor.CasoInteresadoSecuencia, " +
                                        " inventor.InventorID, " +
                                        " inventor.InventorTipoPersonaSAT, " +
                                        " inventor.InventorNombre, " +
                                        " inventor.InventorApPaterno, " +
                                        " inventor.InventorApMaterno, " +
                                        " casoinventor.TipoRelacionId, " +
                                        " inventor.InventorRFC, " +
                                        " inventor.InventorCurp, " +
                                        " CONCAT ( COALESCE(inventor.InventorNombre, ''  ), ' ', " +
                                        " COALESCE(inventor.InventorApPaterno, ''), ' ', " +
                                        " COALESCE(inventor.InventorApMaterno, '')) AS interesadonombrecompleto, " +
                                        " CONCAT (  COALESCE(direccion.DireccionCalle, ''  ), ' ',  " +
                                        " COALESCE(direccion.DireccionNumExt, ''), ' ', " +
                                        " COALESCE(direccion.DireccionNumInt, ''), ' ', " +
                                        " COALESCE(direccion.DireccionColonia, ''), ' ', " +
                                        " COALESCE(direccion.DireccionPoblacion, ''), ' ', " +
                                        " COALESCE(direccion.DireccionEstado, ''), ' ', " +
                                        " COALESCE(direccion.DireccionCP, '')) AS direccioncompleta, " +
                                        " direccion.DireccionCalle, " +
                                        " direccion.DireccionNumExt, " +
                                        " direccion.DireccionNumInt, " +
                                        " direccion.DireccionColonia, " +
                                        " direccion.DireccionCP, " +
                                        " direccion.DireccionEstado, " +
                                        " direccion.DireccionPoblacion, " +
                                        " Damelanacionalidad (inventor.PaisId )AS nacionalidad, " +
                                        " inventor.PaisId, " +
                                        " DameNombrePais(inventor.PaisId) As Nombrepais, " +
                                        " tiporelacion.TipoRelacionDescrip, " +
                                        " inventor.InventorPoder, " +
                                        " inventor.InventorRGP " +
                                        " FROM" +
                                        " casoinventor " +
                                        " LEFT JOIN" +
                                        " inventor ON casoinventor.InventorID = inventor.InventorID" +
                                        " left JOIN " +
                                        " tiporelacion ON casoinventor.TipoRelacionId = tiporelacion.TipoRelacionId " +
                                        " LEFT join" +
                                        " direccion ON direccion.DireccionId = casoinventor.DireccionId " +
                                        " where casoinventor.CasoId = '" + sCasoId + "'" +
                                        " AND casoinventor.TipoSolicitudId = '" + gSTipoSolicitudId + "'" +
                                        " GROUP BY inventor.InventorID order by casoinventor.casoinventorid;";

                MySqlDataReader respuestastringinteresados = con_casointeresado.getdatareader(sInteresados);
                lvinventores.Items.Clear();
                int count = 0;
                String TIPOPERSONA = "";
                int iContadortitula = 0;
                int iContadorinventor = 0;
                int iContadorinventortitular = 0;

                while (respuestastringinteresados.Read())
                {

                    /*TipoRelacionId[count] = objfuncionesdicss.validareader("TipoRelacionId", "InteresadoID", respuestastringinteresados).Text;
                    InteresadoCurp[count] = objfuncionesdicss.validareader("InteresadoCurp", "InteresadoID", respuestastringinteresados).Text;
                    InteresadoNombre[count] = objfuncionesdicss.validareader("InteresadoNombre", "InteresadoID", respuestastringinteresados).Text;
                    InteresadoApPaterno[count] = objfuncionesdicss.validareader("InteresadoApPaterno", "InteresadoID", respuestastringinteresados).Text;
                    InteresadoApMaterno[count] = objfuncionesdicss.validareader("InteresadoApMaterno", "InteresadoID", respuestastringinteresados).Text;
                    nacionalidad[count] = objfuncionesdicss.validareader("nacionalidad", "InteresadoID", respuestastringinteresados).Text;
                    InteresadoRFC[count] = objfuncionesdicss.validareader("InteresadoRFC", "InteresadoID", respuestastringinteresados).Text;

                    sgDireccionCalle[count] = objfuncionesdicss.validareader("DireccionCalle", "InteresadoID", respuestastringinteresados).Text;
                    sgDireccionNumExt[count] = objfuncionesdicss.validareader("DireccionNumExt", "InteresadoID", respuestastringinteresados).Text;
                    sgDireccionNumInt[count] = objfuncionesdicss.validareader("DireccionNumInt", "InteresadoID", respuestastringinteresados).Text;
                    sgDireccionColonia[count] = objfuncionesdicss.validareader("DireccionColonia", "InteresadoID", respuestastringinteresados).Text;
                    sgDireccionCP[count] = objfuncionesdicss.validareader("DireccionCP", "InteresadoID", respuestastringinteresados).Text;
                    sgDireccionEstado[count] = objfuncionesdicss.validareader("DireccionEstado", "InteresadoID", respuestastringinteresados).Text;
                    sgDireccionPoblacion[count] = objfuncionesdicss.validareader("DireccionPoblacion", "InteresadoID", respuestastringinteresados).Text;

                    sgNombrepais[count] = objfuncionesdicss.validareader("Nombrepais", "InventorID", respuestastringinteresados).Text;*/
                    switch (objfuncionesdicss.validareader("InventorTipoPersonaSAT", "InventorID", respuestastringinteresados).Text)
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

                    switch (objfuncionesdicss.validareader("TipoRelacionDescrip", "InventorID", respuestastringinteresados).Text)
                    {
                        case "Titular":
                            {
                                iContadortitula++;
                            }
                            break;
                        case "Inventor":
                            {
                                iContadorinventor++;
                            }
                            break;
                        case "Titular/Inventor":
                            {
                                iContadorinventortitular++;
                            }
                            break;
                    }
                    ListViewItem listinteresados = new ListViewItem(objfuncionesdicss.validareader("TipoRelacionDescrip", "InventorID", respuestastringinteresados).Text);
                    listinteresados.SubItems.Add(objfuncionesdicss.validareader("InventorID", "InventorID", respuestastringinteresados).Text);
                    listinteresados.SubItems.Add(objfuncionesdicss.validareader("TipoRelacionDescrip", "InventorID", respuestastringinteresados).Text);//tipo de persona
                    listinteresados.SubItems.Add(objfuncionesdicss.validareader("interesadonombrecompleto", "InventorID", respuestastringinteresados).Text);
                    listinteresados.SubItems.Add(objfuncionesdicss.validareader("nacionalidad", "InventorID", respuestastringinteresados).Text);
                    listinteresados.SubItems.Add(objfuncionesdicss.validareader("direccioncompleta", "InventorID", respuestastringinteresados).Text);
                    listinteresados.SubItems.Add(objfuncionesdicss.validareader("InventorPoder", "InventorID", respuestastringinteresados).Text);
                    listinteresados.SubItems.Add(objfuncionesdicss.validareader("InventorRGP", "InventorID", respuestastringinteresados).Text);
                    listinteresados.SubItems.Add(TIPOPERSONA);

                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listinteresados.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listinteresados.BackColor = Color.Azure;
                    }
                    lvinventores.Items.Add(listinteresados);
                    lvinventores.FullRowSelect = true;
                    count++;

                }
                //cerrar conexion
                respuestastringinteresados.Close();
                con_casointeresado.Cerrarconexion();
                //tb_numtit.Text = iContadortitula + "";
                tb_inv.Text = iContadorinventor + "";
                //tb_titinv.Text = iContadorinventortitular + "";
            }
            catch (Exception Ex)
            {
                new filelog(loguin.sId, "linea 4302: Error en el interesado" + Ex.ToString());
            }
            //aqui termina la dirección
        }


        public String validafechavacia(String fecha){
            if (fecha != "")
            {
                if(fecha.Contains("0000")){
                    return fecha = "";
                }
                return fecha.Substring(0, 10);
            }
            else {
                return "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //loguin.Close();
            //fCapuraform.Close();
            buscarclienteform.Close();

            this.Close();
            //loguin.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //buscarclienteform.li
            //buscarclienteform.listViewCasos.Items.Clear();
            buscarclienteform.Show();
            this.Close();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            String sLog = "";
            try
            {
                if (cb_tipodocelect.Text.Trim()=="")
                {
                    MessageBox.Show("Debe seleccionar un tipo de documento antes de agregar.");
                    return;
                }

                if (tb_descripdocelec.Text.Trim()=="")
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
                                " '" + gSCasoId + "', " +
                                " '" + sTiposolicitudGlobal + "', " +
                                " '" + loguin.sId + "', " +
                                " '" + (cb_tipodocelect.SelectedItem as ComboboxItem).Value + "', " +
                                " '" + @sfilePath_2.Replace("\\", "\\\\") +"'); ";
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
                            MessageBox.Show("Error al intentar agregar el documento, ueque la ruta ó el nombre del archivo.");
                        }
                    }
                    else {
                        sLog = sQueryinsert;
                        MessageBox.Show("Error al intentar guardar el documento \n Query:"+ sQueryinsert);
                        new filelog(loguin.sId, "linea 4010: " + sQueryinsert);
                    }
                    con_insertdocelect.Cerrarconexion();
                }
            }catch(Exception E)
            {
                new filelog(loguin.sId, "linea 3854: "+E.ToString()+" query "+ sLog);
            }
        }
        private void ExportarDataGridViewExcel(DataGridView grd)
        {
            SaveFileDialog fichero = new SaveFileDialog();
            fichero.Filter = "Excel (*.xls)|*.xls";
            if (fichero.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application aplicacion;
                Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
                aplicacion = new Microsoft.Office.Interop.Excel.Application();
                libros_trabajo = aplicacion.Workbooks.Add();
                hoja_trabajo =
                    (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);
                //Recorremos el DataGridView rellenando la hoja de trabajo
                for (int i = 0; i < grd.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < grd.Columns.Count; j++)
                    {
                        hoja_trabajo.Cells[i + 1, j + 1] = grd.Rows[i].Cells[j].Value.ToString();
                    }
                }
                libros_trabajo.SaveAs(fichero.FileName,
                    Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                libros_trabajo.Close(true);
                aplicacion.Quit();
            }
        }

        public void exportexcel() {
            Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
            xla.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook wb = xla.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xla.ActiveSheet;
            int i = 1;
            int j = 1;

            foreach (ListViewItem comp in lvPlazos.Items)
            {
                ws.Cells[i, j] = comp.Text.ToString();
                //MessageBox.Show(comp.Text.ToString());
                foreach (ListViewItem.ListViewSubItem drv in comp.SubItems)
                {
                    ws.Cells[i, j] = drv.Text.ToString();
                    j++;
                }
                j = 1;
                i++;
            }
        }

        public void generadocumento(){
            solicitudpat2 obj = new solicitudpat2();
            obj.nuevasolicitud(sCasoId);
        }

        public void generadocumentodocs()
        {
            generadocs prueba = new generadocs();
            prueba.generadocss(sCasoId, valuecob);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            ////Create Document
            ////generadocs prueba = new generadocs();
            ////prueba.generadocss();  
            //try {
            //    if (CB_formatoscc.SelectedItem == null)
            //    {
            //        MessageBox.Show("Debes Seleccionar un formato");
            //    }
            //    else
            //    {
            //        if ((CB_formatoscc.SelectedItem as ComboboxItem).Value.ToString() != "100")
            //        {
                        
            //            using (load_documentos form = new load_documentos(generadocumento))
            //            {
            //                form.ShowDialog();
            //            }
            //        }
            //        else {
            //            Solicituddepatente2019 obj = new Solicituddepatente2019(loguin, sCasoId, this);
            //            obj.Show();
            //        }
            //        //if ((CB_formatoscc.SelectedItem as ComboboxItem).Value.ToString() != "100")
            //        //{
            //        //    valuecob = (CB_formatoscc.SelectedItem as ComboboxItem).Value.ToString();
            //        //    using (load_documentos form = new load_documentos(generadocumentodocs))
            //        //    {
            //        //        form.ShowDialog();
            //        //    }
            //        //}
            //        //else
            //        //{
            //        //    using (load_documentos form = new load_documentos(generadocumento))
            //        //    {
            //        //        form.ShowDialog();
            //        //    }
            //        //}
            //    }
            //}
            //catch (Exception E)
            //{
            //    new filelog(loguin.sId, E.ToString());
            //}
        }


        private void lvPrioridades_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            tbNumeroprio.Text = "";
            tbfechaprio.Text = "";
            //tbCvepais.Text = "";
            cbNombrepais.Text = "";
            cbCvpais.Text = "";
            cbTiposolprio.Text = "";
            sIdprioridadseleccionada = "";
            button13.Enabled = false;
        }

        private void button34_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            upcatescampos();
            resetvariable();
        }

        private void button38_Click(object sender, EventArgs e)
        {
            try { 
                if (rtTitulo_update || bTipo_update || bExpediente_update || bNumregistro_update || bSubtipo_update || bClienteduedate_update || bFecharecimpi_update || bFechaconcesion_update || bCapitulo_update || bPlazolegal_update || bFechadivulgacion_update || bFechavigencia_update || bFechacarta_update || bFechainternacional_update || bFechapublicacion_update || btituloidiomaoriginal_update || bAreaimpi_update || bClasediesno_update)
                {
                    DialogResult boton = MessageBox.Show("¿Desea guardar los cambios?", "Guardar cambios", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (boton == DialogResult.OK)
                    {
                        upcatescampos();
                    }
                }
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
            }catch (Exception E)
            {
                new filelog(loguin.sId, "linea 4016: "+E.ToString());
            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtTitulo_update || bTipo_update || bExpediente_update || bNumregistro_update || bSubtipo_update || bClienteduedate_update || bFecharecimpi_update || bFechaconcesion_update || bCapitulo_update || bPlazolegal_update || bFechadivulgacion_update || bFechavigencia_update || bFechacarta_update || bFechainternacional_update || bFechapublicacion_update || btituloidiomaoriginal_update || bAreaimpi_update || bClasediesno_update)
                {
                    DialogResult boton = MessageBox.Show("¿Desea guardar los cambios?", "Guardar cambios", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (boton == DialogResult.OK)
                    {
                        upcatescampos();
                    }
                }
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
            }catch (Exception E)
            {
                new filelog(loguin.sId, "linea 4044: "+E.ToString());
            }
            //consultacaso adelante = new consultacaso(loguin, fCapuraform, buscarclienteform, iCasoid + "");
            //adelante.Show();
            //this.Close();
            //this.ActiveMdiChild.Refresh();
        }

        private void rtTitulo_ModifiedChanged(object sender, EventArgs e)
        {
            //agregamos true a la cariable para guardar el cambio
            rtTitulo_update = true;
        }
        private void button28_Click(object sender, EventArgs e)
        {
            try
            {
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


                if (cbCartas.SelectedItem == null)
                {
                    return;
                }

                //cargamos los datos

                if (cbOficiosEscritos.SelectedItem != null)
                {
                    String sDocumentoid = (cbOficiosEscritos.SelectedItem as ComboboxItem).Value.ToString();
                    objpatentectualview = new view_caso_patentes(gSCasoId, gSTipoSolicitudId, sIdidiomaescritos, sDocumentoid);
                }
                else
                {
                    objpatentectualview = new view_caso_patentes(gSCasoId, gSTipoSolicitudId, sIdidiomaescritos);
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
                    sCartanombreESPfile = objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoTemplateEspanol", respuesta_carta).Text;
                    sCartanombreENfile = objfuncionesdicss.validareader("SubTipoDocumentoTemplateIngles", "SubTipoDocumentoTemplateIngles", respuesta_carta).Text;
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
                        objpatentectualview.sValorescampos.Add(sValorusuario);
                        generacarta_pat objcarta = new generacarta_pat(sCartanombreENfile, valuecob, objpatentectualview);
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
                            objpatentectualview.sValorescampos.Add(sValorusuario);
                            generacarta_pat objcarta = new generacarta_pat(sCartanombreESPfile, valuecob, objpatentectualview);
                        }
                        else
                        {
                            MessageBox.Show("Debe seleccionar un Idioma");
                        }

                    }


                }
                respuesta_carta.Close();
                con_carta.Cerrarconexion();
            }
            catch (Exception ex)
            {
                new filelog(loguin.sId, "linea 4141"+ex.ToString());
            }
            //try { 
            //    String valorcarta = cbCartas.Text;
            //    if (cbCartas.SelectedItem == null)
            //    {
            //        MessageBox.Show("Debes Seleccionar un tiposolicitd");
            //    }
            //    else
            //    {
            //        generacartas prueba = new generacartas();
            //        //valuecob = (CB_tiposolicitudgeneracartas.SelectedItem as ComboboxItem).Value.ToString();//numero de tiposolicitud
            //        prueba.generacartass(sCasoId, sTipogrupoglobal, valorcarta);//casoId, tiposolicitud, nombre carta
            //        MessageBox.Show("Se ah generado Correctamente");
            //    }
            //}catch (Exception E)
            //{
            //    MessageBox.Show("No se encontró el archivo.");
            //    new filelog(loguin.sId, E.ToString());
            //}
        }

        public void upcatescampos(){
            //if (bFechasupdate)
            //{
            //    conect con = new conect();
            //    String sIdspatentes = "UPDATE `caso_patente` SET `CasoTituloespanol` = '" + rtTitulo.Text + "' " + sUpdateset + " WHERE `caso_patente`.`CasoId` = " + sCasoId + " AND `caso_patente`.`TipoSolicitudId` = " + sTiposolicitudGlobal;
            //    MySqlDataReader resp_numpatentes = con.getdatareader(sIdspatentes);
            //    resp_numpatentes.Read();
            //    resp_numpatentes.Close();
            //    bFechasupdate = false;
            //}
            try {
                //if (bSelectidiomachange)
                //{
                //    if (cbIdioma.SelectedItem !=null) {
                //        String sValornuevo = (cbIdioma.SelectedItem as ComboboxItem).Value.ToString();
                //        if (!updatecampos(sValornuevo, "IdiomaId"))
                //        {
                //            MessageBox.Show("Error al hacer la modificación Verifique la conexión.");
                //        }
                //    }

                //}
                if (true || bPlazolegal || rtTitulo_update || bTipo_update || bExpediente_update || bNumregistro_update || bSubtipo_update || bClienteduedate_update || bFecharecimpi_update || bFechaconcesion_update || bCapitulo_update || bPlazolegal_update || bFechadivulgacion_update || bFechavigencia_update || bFechacarta_update || bFechainternacional_update || bFechapublicacion_update || btituloidiomaoriginal_update || bAreaimpi_update || bClasediesno_update)
                {
                    String sUpdateset = "";
                    //if (bTipo_update)
                    //{
                    //    sUpdateset += "";
                    //}
                    if (bExpediente_update)
                    {
                        sUpdateset += ", CasoNumeroExpedienteLargo = '" + tbExpediente.Text+"'";
                    }
                    if (bNumregistro_update)
                    {
                        sUpdateset += ", CasoNumConcedida = '" + tbRegistro.Text + "'";
                    }
                    if (bPlazolegal)
                    {
                        sUpdateset += ", CasoFechaLegal = '" + tbPlazolegal.Text + "'";
                    }
                    //if (bSubtipo_update)
                    //{
                    //    sUpdateset += "";
                    //}
                    if (bClienteduedate_update)
                    {
                        sUpdateset += ", CasoFechaFilingCliente = STR_TO_DATE('" + tbClientduedate.Text + "', '%d-%m-%Y')";// +tbClientduedate.Text + "'";
                    }
                    if (bFecharecimpi_update)
                    {
                        sUpdateset += ", CasoFechaRecepcion = STR_TO_DATE('" + tbFechaRecimpi.Text + "', '%d-%m-%Y')";// +tbFechaRecimpi.Text + "'";
                    }
                    if (bFechaconcesion_update)
                    {
                        sUpdateset += ", CasoFechaConcesion =  STR_TO_DATE('" + tbFechaconcesion.Text + "', '%d-%m-%Y')";// '" + tbFechaconcesion.Text + "'";
                    }
                    //if (bCapitulo_update)
                    //{
                    //    sUpdateset += "";
                    //}
                    //if (bPlazolegal_update)
                    //{
                    //    sUpdateset += "";
                    //}
                    if (bFechadivulgacion_update)
                    {
                        sUpdateset += ", CasoFechaDivulgacionPrevia = STR_TO_DATE('" + tbFechadivulgacion.Text + "', '%d-%m-%Y')";// ' '" + tbFechadivulgacion.Text + "'";
                    }
                    if (bFechavigencia_update)
                    {
                        sUpdateset += ", CasoFechaVigencia = STR_TO_DATE('" + tbFechavigencia.Text + "', '%d-%m-%Y')";//'"+tbFechavigencia.Text+"'";
                    }
                    if (bFechacarta_update)
                    {
                        sUpdateset += ", CasoFechaCartaCliente = STR_TO_DATE('" + tbFechacarta.Text + "', '%d-%m-%Y')";// '"+ tbFechacarta.Text +"'";
                    }
                    if (bFechainternacional_update)
                    {
                        sUpdateset += ", CasoFechaInternacional = STR_TO_DATE('" + tbFechainternacional.Text + "', '%d-%m-%Y')";// '"+ tbFechainternacional.Text +"'";
                    }
                    if (bFechapublicacion_update)
                    {
                        sUpdateset += ", CasoFechaPublicacionSolicitud = STR_TO_DATE('" + tbFechapublicacion.Text + "', '%d-%m-%Y')";// '" + tbFechapublicacion.Text + "'";
                    }
                    if (btituloidiomaoriginal_update)
                    {
                        sUpdateset += ", CasoTituloingles = '"+ rtTituloidionaoriginal.Text + "'";
                    }
                    //if (bIdioma_update) //pendiente
                    //{
                    //    sUpdateset += "";
                    //}
                    //if (bAreaimpi_update)//es un select
                    //{
                    //    sUpdateset += "";
                    //}
                    if (bClasediesno_update)
                    {
                        sUpdateset += "";
                    }
                    conect con = new conect();
                    String sIdspatentes = "UPDATE `caso_patente` SET `CasoTituloespanol` = '" + rtTitulo.Text + "', " +
                        " CasoTituloingles = '" + 
                        rtTituloidionaoriginal.Text + "' "+
                        sUpdateset + 
                        " WHERE `caso_patente`.`CasoId` = " + sCasoId + " AND `caso_patente`.`TipoSolicitudId` = " + sTiposolicitudGlobal;
                    MySqlDataReader resp_numpatentes = con.getdatareader(sIdspatentes);
                    resp_numpatentes.Read();
                    if (resp_numpatentes == null){
                        MessageBox.Show("Error al hacer el update. " + sIdspatentes);
                    }else {
                        MessageBox.Show("Cambios guardados correctamente.");
                    }
                
                    resp_numpatentes.Close();
                    con.Cerrarconexion();
                }
                else{
                    MessageBox.Show("Cambios guardados correctamente.");
                }
            }
            catch (Exception E)
            {
                MessageBox.Show("Error al intentar guardar cambios.");

                new filelog(loguin.sId, "liena 4286: "+E.ToString());
            }
        }

        //aquí agregaremos las modificaciones de los campos y hacer el uupdate

        private void rtTituloidionaoriginal_ModifiedChanged(object sender, EventArgs e)
        {

        }

        private void tbEstatus_TextChanged(object sender, EventArgs e)
        {
            //crear form para seleccionar los estatus disponibles
            //updateEstatus updateestatus = new updateEstatus();
            //if (updateestatus.ShowDialog() == DialogResult.OK)
            //{
            //    String value = updateestatus.sValueestatus;//hacemos el update con este valor y ponemos el Texto en el form
            //    String texti = updateestatus.sTextoestatus;//hacemos el update con este valor y ponemos el Texto en el form
            //    tbEstatus.Text = texti;

            //}
        }

        private void tbEstatus_DoubleClick(object sender, EventArgs e)
        {
            try{
                updateEstatus updateestatus = new updateEstatus("1");
                if (updateestatus.ShowDialog() == DialogResult.OK)
                {
                    String value = updateestatus.sValueestatus;//hacemos el update con este valor y ponemos el Texto en el form
                    String texti = updateestatus.sTextoestatus;//hacemos el update con este valor y ponemos el Texto en el form
                    conect con_4 = new conect();
                    String updateestatuscaso = "UPDATE `caso_patente` SET `EstatusCasoId` = '" + value + "' WHERE (`CasoId` = '" + sCasoId + "');";
                    MySqlDataReader resp_updatecaso = con_4.getdatareader(updateestatuscaso);
                    if (resp_updatecaso != null)
                    {
                        tbEstatus.Text = texti;
                        tbEstatus_header.Text = texti;
                    }
                    resp_updatecaso.Close();
                    con_4.Cerrarconexion();
                }
            }catch (Exception E)
            {
                new filelog(loguin.sId, "linea 4331:"+E.ToString());
            }
        }

        private void tbExpediente_ModifiedChanged(object sender, EventArgs e)
        {
            bExpediente_update = true;
        }

        private void cbAreaimpi_SelectedValueChanged(object sender, EventArgs e)
        {
            //MessageBox.Show((cbAreaimpi.SelectedItem as ComboboxItem).Value.ToString());
        }

        private void tbRegistro_ModifiedChanged(object sender, EventArgs e)
        {
            bNumregistro_update = true;
        }

        private void tbClientduedate_ModifiedChanged(object sender, EventArgs e)
        {
            bClienteduedate_update = true;
        }

        private void tbFechaRecimpi_ModifiedChanged(object sender, EventArgs e)
        {
            bFecharecimpi_update = true;
            //String sNuevovalor = tbFechaRecimpi.Text;
            //if (!updatecampos("DATE(STR_TO_DATE('" + sNuevovalor + "', '%m-%d-%Y'))", "CasoFechaRecepcion"))
            //{
            //    MessageBox.Show("Error al modificar, revise la conexión.");
            //}
        }

        private void tbFechaconcesion_ModifiedChanged(object sender, EventArgs e)
        {
            bFechaconcesion_update = true;
            
        }

        private void tbPlazolegal_ModifiedChanged(object sender, EventArgs e)
        {
            bPlazolegal = true;
        }

        private void tbFechadivulgacion_ModifiedChanged(object sender, EventArgs e)
        {
            bFechadivulgacion_update = true;
        }

        private void tbFechavigencia_ModifiedChanged(object sender, EventArgs e)
        {
            bFechavigencia_update = true;
        }

        private void tbFechacarta_ModifiedChanged(object sender, EventArgs e)
        {
            bFechacarta_update = true;
        }

        private void tbFechainternacional_ModifiedChanged(object sender, EventArgs e)
        {
            bFechainternacional_update = true;
        }

        private void tbFechapublicacion_ModifiedChanged(object sender, EventArgs e)
        {
            bFechapublicacion_update = true;
        }

        //Agregamos el evento modificar
        //private void tbExpediente_TextChanged(object sender, EventArgs e)
        //{
        //    bExpediente_update = true;
        //}

        //private void tbRegistro_TextChanged(object sender, EventArgs e)
        //{
        //    bNumregistro_update = true;
        //}

        //private void tbClientduedate_TextChanged(object sender, EventArgs e)
        //{
        //    bClienteduedate_update = true;
        //}

        //private void tbFechaRecimpi_TextChanged(object sender, EventArgs e)
        //{
        //    bFecharecimpi_update = true;
        //}

        //private void tbFechaconcesion_TextChanged(object sender, EventArgs e)
        //{
        //    bFechaconcesion_update = true;
        //}
        //private void tbFechadivulgacion_TextChanged(object sender, EventArgs e)
        //{
        //    bFechadivulgacion_update = true;
        //}

        //private void tbFechacarta_TextChanged(object sender, EventArgs e)
        //{
        //    bFechacarta_update = true;
        //}

        //private void tbFechainternacional_TextChanged(object sender, EventArgs e)
        //{
        //    bFechainternacional_update = true;
        //}

        //private void tbFechapublicacion_TextChanged(object sender, EventArgs e)
        //{
        //    bFechapublicacion_update = true;
        //}

        //private void rtTituloidionaoriginal_TextChanged(object sender, EventArgs e)
        //{
        //    btituloidiomaoriginal_update = true;
        //}

        private void cbIdioma_SelectedValueChanged(object sender, EventArgs e)
        {
            //hacemos el upodate del idioma
        }

        private void button40_Click(object sender, EventArgs e)
        {
            try { 
                if (cbNumdivicional.Text == "")
                {
                    MessageBox.Show("Debe seleccionar el número de casos divicionales a éste que desea agregar.");
                }
                else {
                    if (MessageBox.Show("¿Seguro que desea agregar "+cbNumdivicional.Text+" casos divicionales de éste caso?", "Agregar Casos Divicionales",MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        //MessageBox.Show("Registro eliminado");
                    }else{
                        DateTime Hoy = DateTime.Today;
                        string fecha_actual = Hoy.ToString("yyyy-MM-dd");
                    
                        int iNumcasodivicional = System.Convert.ToInt32(cbNumdivicional.Text);
                        String sCasosnumeros="";
                        for (int x = 0; x < iNumcasodivicional; x++)
                        {
                            conect con = new conect();
                            String sGetcasonumero = "select casoid, casonumero from caso_patente order by casoid desc limit 1;";
                            MySqlDataReader respuestastringcasonum = con.getdatareader(sGetcasonumero);
                            String sCasonumeronuevo = "";
                            while (respuestastringcasonum.Read())
                            {
                                sCasonumeronuevo = objfuncionesdicss.validareader("casonumero", "casoid", respuestastringcasonum).Text;
                            }
                            respuestastringcasonum.Close();
                            con.Cerrarconexion();
                            //para el caso de patente , no tenemos que hacer analisís de extención
                            int iValor = Int32.Parse(sCasonumeronuevo) + 1;
                            sCasonumeronuevo = iValor + "";

                            //gSCasoId
                            //gSCasoNumero
                            //gSTipoSolicitudId
                            //gStipopctid
                            //gSCasoNumeroExpedienteLargo
                            //gSCasoNumConcedida
                            //gSPaisId
                            //gSUsuarioId
                            //gSEstatusCasoId
                        
                            //if (sCasonumero.Length == 7)
                            //{
                            //    String extencion = sCasonumero.Substring(4, 3);
                            //    sCasonumero = sCasonumero.Substring(0, 4);
                            //    int iValor = Int32.Parse(sCasonumero) + 1;
                            //    sCasonumero = iValor + extencion;
                            //}
                            //else
                            //{
                            //    if (sCasonumero.Length == 4)
                            //    {
                            //        int iValor = Int32.Parse(sCasonumero) + 1;
                            //        sCasonumero = iValor + "";
                            //    }
                            //    else
                            //    {
                            //        MessageBox.Show("El último caso de éste tipo es: " + sCasonumero + " y no se reconoce el formato");
                            //    }
                            //}
                            String sEstatudID = "1"; // por default EstatuscasoID
                            //inserta caso_ patente
                            conect con_con2 = new conect();
                            String sQueryinsertpat = "INSERT INTO `caso_patente` (`CasoId`, " +
                                                    " `TipoSolicitudId`, " +
                                                    " `SubTipoSolicitudId`, " +
                                                    " `TipoPctId`, " +
                                                    " `CasoTituloespanol`, " +
                                                    " `CasoTituloingles`, " +
                                                    " `IdiomaId`, " +

                                                    " `CasoNumConcedida`, " +
                                                    " `CasoNumeroExpedienteLargo`, " +
                                                    " `CasoNumero`, " +
                                                    " `ResponsableId`, " +
                                                    
                                                    " `CasoTitular`, " +
                                                    " `EstatusCasoId`, " +
                                                    " `UsuarioId`, " +

                                                    " `CasoFechaFilingSistema`, " +
                                                    
                                                    " `CasoFechaCartaCliente`, " +
                                                    " `Divicionalid`)  " +
                                                    " VALUES " +
                                                    " (NULL, " +
                                                    " '" + gSTipoSolicitudId + "', " +
                                                    " '" + gSSubTipoSolicitudId + "', " +
                                                    " '" + gStipopctid + "', " +
                                                    " '" + gSCasoTituloespanol + "', " +
                                                    " '" + gSCasoTituloingles + "', " +
                                                    " '" + gSidiomaId + "', " +

                                                    " '" + gSCasoNumConcedida + "', " +
                                                    " '" + gSCasoNumeroExpedienteLargo + "', " +
                                                    " '" + sCasonumeronuevo + "', " +
                                                    " '" + gSUsuarioId + "', " +
                                                    
                                                    " '" + lTitular_texbox.Text + "', " +
                                                    " '" + gSEstatusCasoId + "', " +
                                                    " '" + gSUsuarioId + "', " +
                                                   

                                                    " '" + fecha_actual + "', " +
                                                    
                                                    " '" + fecha_actual + "', "+
                                                    " '" + gSCasoId + "'); ";
                            String sGetid = "SELECT * FROM `caso_patente` order by CasoId desc limit 1";
                            MySqlDataReader respuestastring = con_con2.getdatareader(sQueryinsertpat);
                            respuestastring.Close();
                            con_con2.Cerrarconexion();
                            conect con_con3 = new conect();
                            MySqlDataReader respuestastringid = con_con3.getdatareader(sGetid);
                        String sCasoid = "";
                        while (respuestastringid.Read())
                        {
                            sCasoid = objfuncionesdicss.validareader("CasoId", "CasoId", respuestastringid).Value.ToString();
                            //MessageBox.Show("El caso se agrego correctamente con CasoID: " + sCasoid + " Casonumero: " + sCasonumero);
                            bcasopatente = true;
                        }
                        respuestastringid.Close();
                        con_con3.Cerrarconexion();

                        conect con_con4 = new conect();
                        String insertacliente = "INSERT INTO `casocliente` " +
                                                "(`CasoClienteId`, " +
                                                "`ClienteId`, " +
                                                "`contactoid`, " +
                                                "`CasoId`, " +
                                                "`TipoSolicitudId`, " +
                                                "`CasoClienteIndCorres`) " +
                                                "VALUES " +
                                                "(NULL, " +
                                                gSclienteid + ", " +
                                                gSContactoid + ", " +
                                                sCasoid + ", " +
                                                sTiposolicitudGlobal + ", " +
                                                "0);";
                        MySqlDataReader respuestastringinsertclient = con_con4.getdatareader(insertacliente);
                        if (respuestastringinsertclient.RecordsAffected == 1)
                        {
                            bCasocliente = true;
                        }
                        respuestastringinsertclient.Close();
                        con_con4.Cerrarconexion();

                        conect con_con5 = new conect();
                        String sInteresados = " SELECT * " +
                                                //"     casointeresado.CasoId, " +
                                                //"     interesado.InteresadoID, " +
                                                //"     interesado.InteresadoNombre, " +
                                                //"     direccion.DireccionCalle, " +
                                                //"     direccion.DireccionNumExt, " +
                                                //"     direccion.DireccionNumInt, " +
                                                //"     direccion.DireccionColonia, " +
                                                //"     direccion.DireccionCP, " +
                                                //"     direccion.DireccionEstado, " +
                                                //"     interesado.PaisId, " +
                                                //"     tiporelacion.TipoRelacionDescrip, " +
                                                //"     interesado.InteresadoPoder, " +
                                                //"     casointeresado.TipoRelacionId, " +
                                                //"     pais.PaisNacionalidad, " +
                                                //"     interesado.InteresadoRGP " +
                                                " FROM " +
                                                "     casointeresado " +
                                                //"     , interesado, " +
                                                //"     direccion,  " +
                                                ////"     caso,  " +
                                                //"     pais,  " +
                                                //"     tiporelacion " +
                                                " WHERE " +
                                                "     casointeresado.CasoId = '" + gSCasoId + "'";
                                                //" AND interesado.InteresadoID = casointeresado.InteresadoId " +
                                                //"     AND tiporelacion.TipoRelacionId = casointeresado.TipoRelacionId " +
                                                ////"     AND casointeresado.CasoId = caso.CasoId " +
                                                //"     AND pais.PaisId = interesado.PaisId " +
                                                //"     AND interesado.InteresadoID = direccion.InteresadoId; ";

                        MySqlDataReader resp_interesados = con_con5.getdatareader(sInteresados);
                        while (resp_interesados.Read())
                        {
                            //if(objfuncionesdicss.validareader("TipoRelacionId", "CasoId", resp_interesados).Text == "1"){
                            //    //update el titular del nombre en la tabla caso_patente
                            //    String nombredetitularencasopatente = objfuncionesdicss.validareader("InteresadoNombre", "InteresadoID", resp_interesados).Text;

                            //    String sUpdatetitular = "UPDATE `caso_patente` SET `CasoTitular` = '" + lTitular_texbox.Text + "' WHERE `caso_patente`.`CasoId` = " + sCasoid + " AND `caso_patente`.`TipoSolicitudId` = " + sTiposolicitudGlobal + ";";
                            //    MySqlDataReader resp_updatetitular = con.getdatareader(sUpdatetitular);
                            //}
                            //ListViewItem listinteresados = new ListViewItem(objfuncionesdicss.validareader("TipoRelacionDescrip", "CasoId", resp_interesados).Text);
                            //listinteresados.SubItems.Add(objfuncionesdicss.validareader("InteresadoID", "InteresadoID", resp_interesados).Text);
                            //listinteresados.SubItems.Add(objfuncionesdicss.validareader("InteresadoNombre", "InteresadoID", resp_interesados).Text);
                            //listinteresados.SubItems.Add(objfuncionesdicss.validareader("PaisNacionalidad", "InteresadoID", resp_interesados).Text);
                            //listinteresados.SubItems.Add(objfuncionesdicss.validareader("DireccionColonia", "InteresadoID", resp_interesados).Text);
                            //listinteresados.SubItems.Add(objfuncionesdicss.validareader("InteresadoPoder", "InteresadoID", resp_interesados).Text);
                            //listinteresados.SubItems.Add(objfuncionesdicss.validareader("InteresadoRGP", "InteresadoID", resp_interesados).Text);
                            //lvinteresados.Items.Add(listinteresados);
                            //lvinteresados.FullRowSelect = true;
                            //sCasoid = objfuncionesdicss.validareader("CasoId", "CasoId", resp_interesados).Value.ToString();
                            ////MessageBox.Show("El caso se agrego correctamente con CasoID: " + sCasoid + " Casonumero: " + sCasonumero);
                            conect con_con6 = new conect();
                            String insertcasointeresado = "INSERT INTO `casointeresado` " +
                                                        "(`CasoInteresadoId`, " +
                                                        "`InteresadoId`, " +
                                                        "`CasoId`, " +
                                                        "`TipoSolicitudId`, " +
                                                        "`CasoInteresadoSecuencia`, " +
                                                        "`TipoRelacionId`, " +
                                                        "`DireccionId`) " +
                                                        "VALUES " +
                                                        "( " +
                                                        "NULL, " +
                                                        "'" + objfuncionesdicss.validareader("InteresadoID", "InteresadoID", resp_interesados).Text + "', " +
                                                        sCasoid + ", " +
                                                        sTiposolicitudGlobal + ", " +
                                                        "1," +
                                                        "'" + objfuncionesdicss.validareader("TipoRelacionId", "CasoId", resp_interesados).Text + "'," +
                                                        "null);";
                            MySqlDataReader respuestastringinscasoint = con_con6.getdatareader(insertcasointeresado);
                            if (respuestastringinscasoint.RecordsAffected == 1)
                            {
                                bCasointeresado = true;
                            }
                            respuestastringinscasoint.Close();
                            con_con6.Cerrarconexion();
                            bcasopatente = true;
                        }
                        resp_interesados.Close();
                        con_con5.Cerrarconexion();

                        conect con_con7 = new conect();
                        String insertreferencia = " INSERT INTO `referencia` " +
                                                    " (`ReferenciaId`, " +
                                                    " `CasoId`, " +
                                                    " `TipoSolicitudId`, " +
                                                    " `TipoReferenciaId`, " +
                                                    " `ReferenciaNombre`) " +
                                                    " VALUES (" +
                                                    " NULL, " +
                                                    sCasoid + ", " +
                                                    sTiposolicitudGlobal + ", " +
                                                    "1, " +
                                                    "'" + lReferencia_texbox.Text + "'); ";
                        MySqlDataReader respinsertreferencia = con_con7.getdatareader(insertreferencia);
                        if (respinsertreferencia.RecordsAffected == 1)
                        {
                            bCasoreferencias = true;
                        }
                        respinsertreferencia.Close();
                        con_con7.Cerrarconexion();


                        if(bcasopatente && bCasocliente  && bCasoreferencias){
                            sCasosnumeros += sCasonumeronuevo+"\n";
                        }
                            //listViewDivicional
                            ListViewItem listViewDivicional_item = new ListViewItem(sCasoid);
                            listViewDivicional_item.SubItems.Add(sCasonumeronuevo);
                            listViewDivicional_item.SubItems.Add(lTitular_texbox.Text);
                            listViewDivicional_item.SubItems.Add(gSCasoNumeroExpedienteLargo);
                            listViewDivicional.Items.Add(listViewDivicional_item);
                            listViewDivicional.FullRowSelect = true;
                            bPadredivicional = true;
                        }
                        lbPadre.Text = "Caso Divisional Madre";
                        button40.Enabled = false;
                        bAddpadre.Enabled = false;
                        lbPadre.Show();
                        MessageBox.Show("Se agregaron " + cbNumdivicional.Text + " casos divicionales correctamente. \n" + sCasosnumeros);
                    }
                }
            }
            catch (Exception E)
            {
                new filelog(loguin.sId, "linea 4731: "+E.ToString());
            }
        }

        private void listViewDivicional_DoubleClick(object sender, EventArgs e)
        {
            String sCasoid = listViewDivicional.SelectedItems[0].SubItems[0].Text;
            generadom(sCasoid);
        }

        private void button42_Click(object sender, EventArgs e)
        {   //diseños no ay internacionañes
            /*Aquí validaremos que la fecha exista, y tomaremos la fecha internacional si existe o si no la de presentación*/
            String sfechaRecimpi = tbFechaRecimpi.Text;
            String sFechaInternacional = tbFechainternacional.Text;
            String sFechaenviar = sfechaRecimpi;
            if (sFechaInternacional != "")
            {
                sFechaenviar = sFechaInternacional;
            }
            try{
                CapturadeOficios addoficio = new CapturadeOficios(fCapuraform, loguin, "1", sCasoId, sFechaenviar);
                if (addoficio.ShowDialog() == DialogResult.OK)
                {
                    generadom(sCasoId);
                }
            }catch(Exception ex){
                new filelog("linea: 4212", "error:"+ex.Message);
            }

        }

        private void button41_Click(object sender, EventArgs e)
        {

            CapturaEscritos addescrito = new CapturaEscritos(fCapuraform, loguin, "1", sCasoId);
            if (addescrito.ShowDialog() == DialogResult.OK)
            {
                generadom(sCasoId);
                //tbEstatus.Text = addescrito.sEstatusnuevodepuesdensert;
                //tbEstatus_header.Text = addescrito.sEstatusnuevodepuesdensert;
            }
            
        }

        private void tbEstatus_header_DoubleClick(object sender, EventArgs e)
        {
            updateEstatus updateestatus = new updateEstatus("1");
            if (updateestatus.ShowDialog() == DialogResult.OK)
            {
                String value = updateestatus.sValueestatus;//hacemos el update con este valor y ponemos el Texto en el form
                String texti = updateestatus.sTextoestatus;//hacemos el update con este valor y ponemos el Texto en el form
                conect con_4 = new conect();
                String updateestatuscaso = "UPDATE `caso_patente` SET `EstatusCasoId` = '" + value + "' WHERE (`CasoId` = '" + sCasoId + "');";
                MySqlDataReader resp_updatecaso = con_4.getdatareader(updateestatuscaso);
                if (resp_updatecaso != null)
                {
                    tbEstatus.Text = texti;
                    tbEstatus_header.Text = texti;
                }

            }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            Capturadetitulo addtitulo = new Capturadetitulo(fCapuraform, loguin, "1", sCasoId);
            if (addtitulo.ShowDialog() == DialogResult.OK)
            {
                generadom(sCasoId);
                //tbEstatus.Text = addescrito.sEstatusnuevodepuesdensert;
                //tbEstatus_header.Text = addescrito.sEstatusnuevodepuesdensert;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Fatenderplazo obj = new Fatenderplazo(sCasoId, sTiposolicitudGlobal, loguin, gSCasoNumero);
            obj.ShowDialog();
            generadom(sCasoId);
        }

        private void button44_Click(object sender, EventArgs e)
        {
            /*if(){//estatus diferente de 27
            }*/
            CapturaSolicitud addescrito = new CapturaSolicitud(fCapuraform, loguin, "1", sCasoId);
            if (addescrito.ShowDialog() == DialogResult.OK)
            {
                generadom(sCasoId);
                //tbEstatus.Text = addescrito.sEstatusnuevodepuesdensert;
                //tbEstatus_header.Text = addescrito.sEstatusnuevodepuesdensert;
            }
        }

        private void lvdocumentosimpi_DoubleClick(object sender, EventArgs e)
        {
            string sRutaarchivo = "";
            try {
                
                sRutaarchivo = lvdocumentosimpi.SelectedItems[0].SubItems[0].Text;
                sRutaarchivo = sRutaarchivo.Replace("\\\\", "\\");
                String rutaabrir = "";
                if (sRutaarchivo.Contains("J"))
                {
                    rutaabrir = sRutaarchivo;
                }else {
                    rutaabrir = "\\" + sRutaarchivo;
                }
                Process.Start(rutaabrir);
            }catch(Exception E){
                MessageBox.Show("Conflicto al buscar el archivo en: " + sRutaarchivo);
                //String ruta_log = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\casosking\\";
                //if (!Directory.Exists(ruta_log))
                //{
                //    System.IO.Directory.CreateDirectory(ruta_log);
                //}
                //String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                //StringBuilder sb_log = new StringBuilder();
                //sb_log.Append(fechalog + ":Usrid:" + loguin.sId + " Error:" + E + "\n");
                //System.IO.File.AppendAllText(ruta_log + "sistema_casosking.log", sb_log.ToString());
                //sb_log.Clear();
                new filelog(loguin.sId, "line:2870"+E.Message);
                

            }
            
        }

        private void button45_Click(object sender, EventArgs e)
        {
            //readmail obj = new readmail();
            //obj.readmailone();

            FGeneraCartas objsdf = new FGeneraCartas(loguin, fCapuraform);
            objsdf.Show();
        }

        private void tbClientduedate_TextChanged_1(object sender, EventArgs e)
        {

        }
        public bool updatecampos(String sNuevovalor, String sNombrecampo){
            try { 
                    conect con = new conect();
                    String sIdspatentes = "UPDATE caso_patente SET " + sNombrecampo + "= "+sNuevovalor+" WHERE `caso_patente`.`CasoId` = " + sCasoId + " AND `caso_patente`.`TipoSolicitudId` = " + sTiposolicitudGlobal;
                    MySqlDataReader resp_numpatentes = con.getdatareader(sIdspatentes);
                    resp_numpatentes.Read();
                    if (resp_numpatentes != null)
                    {
                        resp_numpatentes.Close();
                        con.Cerrarconexion();
                        return true;
                    }
                    else {
                        con.Cerrarconexion();
                        return false;                   
                    }
                }catch(Exception E){
                    MessageBox.Show("Ocurrió un Error. Revise el log para más detalles.");
                    String ruta = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\casosking\\";
                    if (!Directory.Exists(ruta))
                    {
                        System.IO.Directory.CreateDirectory(ruta);
                    }
                    String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    StringBuilder sb = new StringBuilder();
                    sb.Append(fechalog + ":userid:" + loguin.sId + " Error:" + E + "\n");
                    System.IO.File.AppendAllText(ruta + "sistema_casosking.log", sb.ToString());
                    sb.Clear();
                    return false;
                }
        }

        private void cbIdioma_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void tbClientduedate_LocationChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Termino de modificar y hacemos el update");
        }

        private void cbIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            bSelectidiomachange = true;
        }



        public bool bPlazolegal { get; set; }

        public MailBee.Mime.MailMessage Mesageemail { get; set; }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void lPais_texbox_DoubleClick(object sender, EventArgs e)
        {
            updatePais updateestatus = new updatePais();
            if (updateestatus.ShowDialog() == DialogResult.OK)
            {
                String value = updateestatus.sValueestatus;//hacemos el update con este valor y ponemos el Texto en el form
                String texti = updateestatus.sTextoestatus;//hacemos el update con este valor y ponemos el Texto en el form
                conect con_4 = new conect();
                String updateestatuscaso = "UPDATE `caso_patente` SET `PaisId` = '" + value + "' WHERE (`CasoId` = '" + sCasoId + "');";
                MySqlDataReader resp_updatecaso = con_4.getdatareader(updateestatuscaso);
                if (resp_updatecaso != null)
                {
                    lPais_texbox.Text = texti;
                }

            }
        }

        private void lvPrioridades_MouseClick(object sender, EventArgs e)
        {
            try {
                button13.Enabled = true;
                sIdprioridadseleccionada = lvPrioridades.SelectedItems[0].SubItems[0].Text;
                String sNumeroprioridadselected = lvPrioridades.SelectedItems[0].SubItems[1].Text;
                String sFechaprioridad = lvPrioridades.SelectedItems[0].SubItems[2].Text;
                String sCalvepaisselected = lvPrioridades.SelectedItems[0].SubItems[3].Text;
                String sNombrepaisselected = lvPrioridades.SelectedItems[0].SubItems[4].Text;
                String sTiposolselected = lvPrioridades.SelectedItems[0].SubItems[5].Text;

                tbNumeroprio.Text = sNumeroprioridadselected;
                tbfechaprio.Text = validafechacorecta(sFechaprioridad, "dd/MM/yyyy", "dd'-'MM'-'yyyy");
                //tbCvepais.Text = sCalvepaisselected;
                cbCvpais.Text = sCalvepaisselected;
                cbNombrepais.Text = sNombrepaisselected;
                cbTiposolprio.Text = sTiposolselected;

                for(int y =0; y<lvPrioridades.Items.Count; y++){//cada vuelta es un renglon
                    
                    Console.WriteLine(lvPrioridades.Items[y].SubItems[0].Text);//cada subitem es la columna
                    Console.WriteLine(lvPrioridades.Items[y].SubItems[2].Text);
                    Console.WriteLine(lvPrioridades.Items[y].SubItems[3].Text);
                    Console.WriteLine(lvPrioridades.Items[y].SubItems[4].Text);
                    Console.WriteLine(lvPrioridades.Items[y].SubItems[5].Text);
                    if (lvPrioridades.Items[y].SubItems[5].Text =="PCT") {
                        Console.WriteLine("Ya hay un PCT");
                    }
                }
                
            }catch(Exception E){
                new filelog(loguin.sId, "linea 4979: "+E.ToString());
            }
            
        }

        private String validafechacorectadiagonal(String Fechaentrada)
        {//verificamos que es una fecha valida y la convertimos a un formato date mysql
            String sFechasalida = "";
            try
            {
                sFechasalida = DateTime.ParseExact(Fechaentrada, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbDocumentofecharecepcion.Text;
            }
            catch (Exception E)
            {
                sFechasalida = "";
            }
            return sFechasalida;
        }
        private String validafechacorecta(String Fechaentrada, String sFormatoentrada, String sFechaSalida)
        {//verificamos que es una fecha valida y la convertimos a un formato date mysql
            String sFechasalida = "";
            try
            {
                sFechasalida = DateTime.ParseExact(Fechaentrada, sFormatoentrada, CultureInfo.InvariantCulture).ToString(sFechaSalida); //tbDocumentofecharecepcion.Text;
            }
            catch (Exception E)
            {
                sFechasalida = "";
            }
            return sFechasalida;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                if (sIdprioridadseleccionada != "")
                {

                    String sNumeroprioridadselected = tbNumeroprio.Text;
                    String sFechaprioridad = tbfechaprio.Text;
                    //String sCalvepaisselected = tbCvepais.Text;
                    String sCalvepaisselected = cbCvpais.Text;
                    
                    String sNombrepaisselected = cbNombrepais.Text;
                    String sTiposolselected = cbTiposolprio.Text;
                    String sPaisoid = "";
                    String sTiprioridadid = "";
                    if (validafechacorecta(sFechaprioridad, "dd-MM-yyyy", "yyyy'/'MM'/'dd") == "")
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
                                                "`PrioridadFecha` = '" + validafechacorecta(sFechaprioridad, "dd-MM-yyyy", "yyyy'/'MM'/'dd") + "'," +
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
                        generadom(gSCasoId);
                        tbNumeroprio.Text = "";
                        tbfechaprio.Text = "";
                        //tbCvepais.Text = "";
                        cbNombrepais.Text = "";
                        cbCvpais.Text = "";
                        cbTiposolprio.Text = "";
                        button13.Enabled = false;
                    }
                    resp_updatecaso.Close();
                    con_4.Cerrarconexion();
                    
                }
                else {
                    MessageBox.Show("Debe seleccionar una prioridad de la lista.");
                }
            }
            catch (Exception E)
            {
                new filelog(loguin.sId, "linea 5091"+E.ToString());
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            
        }

        private void tbClientduedate_KeyPress_1(object sender, KeyPressEventArgs e)
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


            if (tbClientduedate.Text.Length == 2)
            {
                tbClientduedate.Text = tbClientduedate.Text + "-";
                tbClientduedate.SelectionStart = tbClientduedate.Text.Length;

            }
            if (tbClientduedate.Text.Length == 5)
            {
                tbClientduedate.Text = tbClientduedate.Text + "-";
                tbClientduedate.SelectionStart = tbClientduedate.Text.Length;
            }

        }

        private void tbFechaRecimpi_KeyPress(object sender, KeyPressEventArgs e)
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


            if (tbFechaRecimpi.Text.Length == 2)
            {
                tbFechaRecimpi.Text = tbFechaRecimpi.Text + "-";
                tbFechaRecimpi.SelectionStart = tbFechaRecimpi.Text.Length;

            }
            if (tbFechaRecimpi.Text.Length == 5)
            {
                tbFechaRecimpi.Text = tbFechaRecimpi.Text + "-";
                tbFechaRecimpi.SelectionStart = tbFechaRecimpi.Text.Length;
            }
        }

        private void tbFechaconcesion_KeyPress(object sender, KeyPressEventArgs e)
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


            if (tbFechaconcesion.Text.Length == 2)
            {
                tbFechaconcesion.Text = tbFechaconcesion.Text + "-";
                tbFechaconcesion.SelectionStart = tbFechaconcesion.Text.Length;

            }
            if (tbFechaconcesion.Text.Length == 5)
            {
                tbFechaconcesion.Text = tbFechaconcesion.Text + "-";
                tbFechaconcesion.SelectionStart = tbFechaconcesion.Text.Length;
            }
        }

        private void tbPlazolegal_KeyPress(object sender, KeyPressEventArgs e)
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


            if (tbPlazolegal.Text.Length == 2)
            {
                tbPlazolegal.Text = tbPlazolegal.Text + "-";
                tbPlazolegal.SelectionStart = tbPlazolegal.Text.Length;

            }
            if (tbPlazolegal.Text.Length == 5)
            {
                tbPlazolegal.Text = tbPlazolegal.Text + "-";
                tbPlazolegal.SelectionStart = tbPlazolegal.Text.Length;
            }
        }

        private void tbFechadivulgacion_KeyPress(object sender, KeyPressEventArgs e)
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


            if (tbFechadivulgacion.Text.Length == 2)
            {
                tbFechadivulgacion.Text = tbFechadivulgacion.Text + "-";
                tbFechadivulgacion.SelectionStart = tbFechadivulgacion.Text.Length;

            }
            if (tbFechadivulgacion.Text.Length == 5)
            {
                tbFechadivulgacion.Text = tbFechadivulgacion.Text + "-";
                tbFechadivulgacion.SelectionStart = tbFechadivulgacion.Text.Length;
            }
        }

        private void tbFechacarta_KeyPress(object sender, KeyPressEventArgs e)
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


            if (tbFechacarta.Text.Length == 2)
            {
                tbFechacarta.Text = tbFechacarta.Text + "-";
                tbFechacarta.SelectionStart = tbFechacarta.Text.Length;

            }
            if (tbFechacarta.Text.Length == 5)
            {
                tbFechacarta.Text = tbFechacarta.Text + "-";
                tbFechacarta.SelectionStart = tbFechacarta.Text.Length;
            }
        }

        private void tbFechainternacional_KeyPress(object sender, KeyPressEventArgs e)
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


            if (tbFechainternacional.Text.Length == 2)
            {
                tbFechainternacional.Text = tbFechainternacional.Text + "-";
                tbFechainternacional.SelectionStart = tbFechainternacional.Text.Length;

            }
            if (tbFechainternacional.Text.Length == 5)
            {
                tbFechainternacional.Text = tbFechainternacional.Text + "-";
                tbFechainternacional.SelectionStart = tbFechainternacional.Text.Length;
            }
        }

        private void tbFechapublicacion_KeyPress(object sender, KeyPressEventArgs e)
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


            if (tbFechapublicacion.Text.Length == 2)
            {
                tbFechapublicacion.Text = tbFechapublicacion.Text + "-";
                tbFechapublicacion.SelectionStart = tbFechapublicacion.Text.Length;

            }
            if (tbFechapublicacion.Text.Length == 5)
            {
                tbFechapublicacion.Text = tbFechapublicacion.Text + "-";
                tbFechapublicacion.SelectionStart = tbFechapublicacion.Text.Length;
            }
        }

        private void tbfechaprio_KeyPress(object sender, KeyPressEventArgs e)
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

            if (tbfechaprio.Text.Length == 2)
            {
                tbfechaprio.Text = tbfechaprio.Text + "-";
                tbfechaprio.SelectionStart = tbfechaprio.Text.Length;

            }
            if (tbfechaprio.Text.Length == 5)
            {
                tbfechaprio.Text = tbfechaprio.Text + "-";
                tbfechaprio.SelectionStart = tbfechaprio.Text.Length;
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
                        generadom(gSCasoId);
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
                new filelog(""+ loguin.sId, "linea 5430" + E.Message);
                //new filelog(":"+loguin.sId, "linea 5430"+E.Message);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                if (sIdprioridadseleccionada == "")
                {
                    if (tbNumeroprio.Text != "" && tbfechaprio.Text != "" && cbNombrepais.Text != "" && cbTiposolprio.Text != "")
                    {
                        if (validafechacorecta(tbfechaprio.Text, "dd-MM-yyyy", "yyyy'/'MM'/'dd") != "")
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
                                        if (paises[y] == cbCvpais.Text)
                                        {
                                            iIdpais = y;
                                        }
                                    }
                                    int iIdtipoprioridad = 0;
                                    if ("PCT" == cbTiposolprio.Text)
                                    { 
                                        iIdtipoprioridad = 1; 
                                    }else{ 
                                        iIdtipoprioridad = 2; 
                                    }

                                    //aqui hacemos el insert
                                    conect conect_prio = new conect();
                                    String sInsertprioridades = " INSERT INTO `prioridad`" +
                                                                " (`PrioridadId`," +
                                                                " `CasoId`," +
                                                                " `CasoId`," +
                                                                " `PrioridadNumero`," +
                                                                " `PrioridadFecha`," +
                                                                " `PaisID`," +
                                                                " `TipoPrioridadId`)" +
                                                                " VALUES" +
                                                                " (null," +
                                                                " '" + gSCasoId + "'," +
                                                                " '" + gSTipoSolicitudId + "'," +
                                                                " '" + tbNumeroprio.Text + "'," +
                                                                " '" + validafechacorecta(tbfechaprio.Text, "dd-MM-yyyy", "yyyy'-'MM'-'dd") + "' ," +// +tbfechaprio.Text + "', " +
                                                                " '" + iIdpais + "'," +
                                                                " '" + iIdtipoprioridad + "');";
                                    MySqlDataReader resp_insertprioridades = conect_prio.getdatareader(sInsertprioridades);
                                    if (resp_insertprioridades.RecordsAffected == 1)
                                    {
                                        bCasoprioridades = true;
                                        generadom(gSCasoId);
                                    }
                                    else {
                                        MessageBox.Show("Revise quye los datos estén correctos");
                                    }
                                    resp_insertprioridades.Close();
                                    conect_prio.Cerrarconexion();

                                    //ListViewItem lPrioridades = new ListViewItem(tbNumeroprio.Text);
                                    //lPrioridades.SubItems.Add(tbfechaprio.Text);
                                    //lPrioridades.SubItems.Add(cbNombrepais.Text);
                                    //lPrioridades.SubItems.Add(cbTiposolprio.Text);
                                    //lvPrioridades.Items.Add(lPrioridades);

                                    //tbNumeroprio.Text = "";
                                    //tbfechaprio.Text = "";
                                    //cbNombrepais.Text = "";
                                    //cbTiposolprio.Text = "";
                                    //tbCvepais.Text = "";
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
                                    if (paises[y] == cbCvpais.Text)
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
                                                            " '" + gSCasoId + "'," +
                                                            " '" + gSTipoSolicitudId + "'," +
                                                            " '" + tbNumeroprio.Text + "'," +
                                                            " '" + validafechacorecta(tbfechaprio.Text, "dd-MM-yyyy", "yyyy'-'MM'-'dd") + "' ," +// +tbfechaprio.Text + "', " +
                                                            " '" + iIdpais + "'," +
                                                            " '" + iIdtipoprioridad + "');";
                                MySqlDataReader resp_insertprioridades = conect_prio.getdatareader(sInsertprioridades);
                                if (resp_insertprioridades.RecordsAffected == 1)
                                {
                                    bCasoprioridades = true;
                                    generadom(gSCasoId);
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
                }
                else {
                    MessageBox.Show("Debe limpiar antes de agregar un nuevo registro.");
                }
            }
            catch (Exception E)
            {
                new filelog(loguin.sId, E.ToString());
            }
        }

        private void label89_Click(object sender, EventArgs e)
        {

        }

        private void cbNombrepais_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                int iValuepais = Convert.ToInt32((cbNombrepais.SelectedItem as ComboboxItem).Value.ToString());
                //tbCvepais.Text = paises[iValuepais];
                cbCvpais.Text = paises[iValuepais];
            }
            catch(Exception E)
            {
                new filelog(loguin.sId, E.ToString());
            }
            
        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                var excelApp = new Excel.Application();
                excelApp.Visible = true;
                //Crea un nuevo libro
                excelApp.Workbooks.Add();
                //Crear una hoja
                Excel._Worksheet workSheet = excelApp.ActiveSheet;
                //En versiones anteriores de C# se requiere una conversión explícita
                //Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;
                //Estableciendo los encabezados de columna
                workSheet.Cells[3, "A"] = "Id";
                workSheet.Cells[3, "B"] = "Excenta";
                workSheet.Cells[3, "C"] = "Tipo";
                workSheet.Cells[3, "D"] = "Anualidad";
                workSheet.Cells[3, "E"] = "Año";
                workSheet.Cells[3, "F"] = "Mes";
                workSheet.Cells[3, "G"] = "Quiquenio";
                workSheet.Cells[3, "H"] = "Estatus";
                workSheet.Cells[3, "I"] = "Fecha límite";
                workSheet.Cells[3, "J"] = "Fecha Pagado";
                workSheet.Cells[3, "K"] = "Fecha fin Vigencia";
                

                workSheet.Cells[1, "A"] = "Anualidades para el caso: "+gSCasoId;
                workSheet.Range["A1", "F1"].Merge();
                workSheet.Range["A1", "F1"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                workSheet.Range["A1", "F1"].Font.Bold = true;
                workSheet.Range["A1", "F1"].Font.Size = 20;
                var row = 3;

                for (int i = 0; i < lvAnualidades.Items.Count; i++)
                {
                    workSheet.Cells[i + 4, "A"] = lvAnualidades.Items[i].SubItems[0].Text;
                    workSheet.Cells[i + 4, "B"] = lvAnualidades.Items[i].SubItems[1].Text;
                    workSheet.Cells[i + 4, "C"] = lvAnualidades.Items[i].SubItems[2].Text;
                    workSheet.Cells[i + 4, "D"] = lvAnualidades.Items[i].SubItems[3].Text;
                    workSheet.Cells[i + 4, "E"] = lvAnualidades.Items[i].SubItems[4].Text;
                    workSheet.Cells[i + 4, "F"] = lvAnualidades.Items[i].SubItems[5].Text;
                    workSheet.Cells[i + 4, "G"] = lvAnualidades.Items[i].SubItems[6].Text;
                    workSheet.Cells[i + 4, "H"] = lvAnualidades.Items[i].SubItems[7].Text;
                    workSheet.Cells[i + 4, "I"] = lvAnualidades.Items[i].SubItems[8].Text;
                    workSheet.Cells[i + 4, "J"] = lvAnualidades.Items[i].SubItems[9].Text;
                    workSheet.Cells[i + 4, "K"] = lvAnualidades.Items[i].SubItems[10].Text;
                    
                    //workSheet.Cells[i + 4, "AP"] = listView1.Items[i].SubItems[41].Text;
                    //workSheet.Cells[i + 4, "AQ"] = listView1.Items[i].SubItems[42].Text;
                    row = i;
                }
                //foreach (var acct in listView1.Items)
                //{
                //    row++;
                //    workSheet.Cells[row, "A"] = acct.;
                //    workSheet.Cells[row, "B"] = acct;
                //    workSheet.Cells[row, "C"] = acct;
                //}

                workSheet.Columns[1].AutoFit();
                workSheet.Columns[2].AutoFit();
                workSheet.Columns[3].AutoFit();

                //Aplicando un autoformato a la tabla
                workSheet.Range["A3", "k" + (row + 4)].AutoFormat(Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic2);
            }
            catch (Exception E)
            {
                Console.Write("Se canceló la exportación");
                new filelog(loguin.sId, E.ToString());
                
            }
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            //try{
            //    if((cbFormatosnuevos.SelectedItem as ComboboxItem).Value!=null){
            //        String sNamefile = "";
            //        switch (System.Convert.ToInt32((cbFormatosnuevos.SelectedItem as ComboboxItem).Value))
            //        {
            //            case 1:
            //                {
            //                    //renovacionydeclaacion obj = new renovacionydeclaacion(loguin, sCasoId, );
            //                    //obj.ShowDialog();
            //                } break;
            //            case 2: { } break;
            //            case 3: { } break;
            //            case 4: { } break;
            //            case 5: { } break;
            //            case 6: { } break;
            //            case 7: { } break;
          
            //        }
            //    }
                

            //}catch(Exception E){
            //    new filelog(loguin.sId, E.ToString());
            //}
            
            /*String sFormatonuevo = (cbFormatosnuevos.SelectedItem as ComboboxItem).Text;
            configuracionfiles conf = new configuracionfiles();
            conf.configuracionfilesinicio();
            String sPath = conf.sFileupload + "\\formatosconfigurables\\"+sFormatonuevo;

            String sruta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\casosking\" + "Nuevos_formatos\\IMPI-00-002_B_copia.docx";*/
            ////string[] readText = File.ReadAllLines(@"D:\CARTAP1.txt");
            //Word.Application application = new Word.Application();
            //Word.Document document = application.Documents.Open(sruta);
            //for (int i = 1; i <= document.Bookmarks.Count; i++)
            //{
            //    object objI = i;
            //    application.Visible = true;
            //    document.Bookmarks[document.Bookmarks.get_Item(ref objI).Name].Select();
            //    application.Selection.TypeText("" + i);
            //}
            //document.Save();
            //application.Quit();

        }

        private void button8_Click(object sender, EventArgs e)
        {

            try {
                if (lvinteresados.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Debe Seleccionar un Titular o Inventor.");
                    return;
                }
                String sInteresadoid = lvinteresados.SelectedItems[0].SubItems[1].Text;//interesadoid
                String sRelacion = lvinteresados.SelectedItems[0].SubItems[0].Text;//relacion
                String sDomicilio = lvinteresados.SelectedItems[0].SubItems[4].Text;//domicilio
                String sTiposolfalso = buscarclienteform.iTiposolicitud.ToString();
                FSelectdireccionint obj = new FSelectdireccionint(sInteresadoid, sCasoId, lCasoNumero_texbox.Text, sRelacion, sDomicilio, gSTipoSolicitudId);
                obj.ShowDialog();
                actualizainformaciondeinteresado();

            }catch(Exception Ex){
            
            }
            //// ver intersado
            //try
            //{
            //    int casoid = Int32.Parse(sCasoId);
            //    if (lvinteresados.SelectedItems.Count == 0)
            //    {
            //        MessageBox.Show("Debe Seleccionar un interesado");
            //    }
            //    else
            //    {
            //        String sInteresadoid = lvinteresados.SelectedItems[0].SubItems[1].Text;//id interesado 

            //        //InteresadoDetalleInterno detalleinterno = new InteresadoDetalleInterno(sInteresadoid);
            //        FInteresadoDetalle detalleinterno = new FInteresadoDetalle(sInteresadoid, loguin, fCapuraform, 1, casoid);
            //        detalleinterno.ShowDialog();
            //        generadom(sCasoId);
            //    }
            //}
            //catch (Exception E)
            //{
            //    //escribimos en log
            //    new filelog(loguin.sId, E.ToString());
            //}
        }

        private void button15_Click_2(object sender, EventArgs e)
        {
            try
            {
                BuscaInteresadoCaso busquedainteresadocaso = new BuscaInteresadoCaso(sCasoId, sTiposolicitudGlobal);
                busquedainteresadocaso.ShowDialog();
                generadom(sCasoId);
            }
            catch (Exception E)
            {
                new filelog(loguin.sId, E.ToString());
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                int casoid= Int32.Parse(sCasoId);
                int tiposolicitud = Int32.Parse(sTiposolicitudGlobal);
                Finteresado finteresado = new Finteresado(loguin, fCapuraform, casoid, tiposolicitud);
                finteresado.ShowDialog();
                generadom(sCasoId);
            }catch(Exception E){
                new filelog(loguin.sId, E.ToString());
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // boton elimimar
            try
            {

                if (lvinteresados.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Debes seleccionar un Titular o Inventor.");
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
                        {
                            // MessageBox.Show("El interesado esta asociado a otros casos, no se puede eliminar");

                            var confirmResult2 = MessageBox.Show("¿Seguro que desea ELIMINAR este interesado  " + sInteresadoNomnbre + " ?", "Eliminar Contacto", MessageBoxButtons.YesNo);
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
                                    respuesta_deletecasointeresado.Close();
                                    conectcasointeresado.Cerrarconexion();
                                }

                            }

                            generadom(sCasoId);
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
                                    MessageBox.Show("No se pudo eliminar este interesado");
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
                                        respuesta_deletecasointeresado.Close();
                                        conectcasointeresado.Cerrarconexion();
                                    }

                                    MessageBox.Show("Interesado " + sInteresadoNomnbre + " eliminado correctamente");
                                    respuesta_delete.Close();
                                    conectdeleteinteresado.Cerrarconexion();
                                    generadom(sCasoId);

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
                                        // MessageBox.Show("Direccion asociada a este interesado borrada correctamente.");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception E)
            {
                //escribimos en log falta el log de lalo
                new filelog(loguin.sId, E.ToString());
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            generadom(sCasoId);
        }


        
        //public void actualizatablainteresado()
        //{
        //    try
        //    {


        //        lvinteresados.Items.Clear();
        //        conect con = new conect();
        //        String sInteresados = " SELECT  " +
        //                                " casointeresado.CasoId, " +
        //                                " interesado.InteresadoID, " +
        //                                " interesado.InteresadoTipoPersonaSAT, " +
        //            // "     interesado.InteresadoNombre, " +
        //                                " CONCAT ( COALESCE(interesado.InteresadoNombre, ''  ), ' ', " +
        //                                " COALESCE(interesado.InteresadoApPaterno, ''), ' ', " +
        //                                " COALESCE(interesado.InteresadoApMaterno, '')) AS interesadonombrecompleto, " +
        //                                " CONCAT (  COALESCE(direccion.DireccionCalle, ''  ), ' ',  " +
        //                                " COALESCE(direccion.DireccionNumExt, ''), ' ', " +
        //                                " COALESCE(direccion.DireccionNumInt, ''), ' ', " +
        //                                " COALESCE(direccion.DireccionColonia, ''), ' ', " +
        //                                " COALESCE(direccion.DireccionPoblacion, ''), ' ', " +
        //                                " COALESCE(direccion.DireccionEstado, ''), ' ', " +
        //                                " COALESCE(direccion.DireccionCP, '')) AS direccioncompleta, " +
        //            //"     direccion.DireccionCalle, " +
        //            //"     direccion.DireccionNumExt, " +
        //            //"     direccion.DireccionNumInt, " +
        //            //"     direccion.DireccionColonia, " +
        //            // "     direccion.DireccionCP, " +
        //            //"     direccion.DireccionEstado, " +
        //            //"     direccion.DireccionPoblacion, " +
        //                                " Damelanacionalidad (interesado.PaisId )AS nacionalidad, " +
        //                                "     interesado.PaisId, " +
        //                                "     tiporelacion.TipoRelacionDescrip, " +
        //                                "     interesado.InteresadoPoder, " +
        //            // "     pais.PaisNacionalidad, " +
        //                                "     interesado.InteresadoRGP " +
        //                                " FROM " +
        //                                "     casointeresado, " +
        //                                "     interesado, " +
        //                                "     direccion,  " +
        //            //"     caso,  " +
        //            //  "     pais,  " +
        //                                "     tiporelacion " +
        //                                " WHERE " +
        //                                "     casointeresado.CasoId = '" + sCasoId + "'" +
        //                                " AND interesado.InteresadoID = casointeresado.InteresadoId " +
        //                                "     AND tiporelacion.TipoRelacionId = casointeresado.TipoRelacionId " +
        //            //"     AND casointeresado.CasoId = caso.CasoId " +
        //            // "     AND pais.PaisId = interesado.PaisId " +
        //                                "     AND interesado.InteresadoID = casointeresado.InteresadoId GROUP BY interesado.InteresadoID ; ";
        //        MySqlDataReader respuestastringinteresados = con.getdatareader(sInteresados);
        //        lvinteresados.Items.Clear();
        //        int count = 0;
        //        String TIPOPERSONA = "";
        //        while (respuestastringinteresados.Read())
        //        {
        //            switch (objfuncionesdicss.validareader("InteresadoTipoPersonaSAT", "InteresadoID", respuestastringinteresados).Text)
        //            {
        //                case "FN":
        //                    TIPOPERSONA = "Física Nacional";
        //                    break;
        //                case "FE":
        //                    TIPOPERSONA = "Física Extranjera";
        //                    break;
        //                case "MN":
        //                    TIPOPERSONA = "Moral Nacional";
        //                    break;
        //                case "ME":
        //                    TIPOPERSONA = "Moral Extranjera";
        //                    break;

        //            }
        //            ListViewItem listinteresados = new ListViewItem(objfuncionesdicss.validareader("TipoRelacionDescrip", "InteresadoID", respuestastringinteresados).Text);
        //            listinteresados.SubItems.Add(objfuncionesdicss.validareader("InteresadoID", "InteresadoID", respuestastringinteresados).Text);
        //            listinteresados.SubItems.Add(objfuncionesdicss.validareader("interesadonombrecompleto", "InteresadoID", respuestastringinteresados).Text);
        //            listinteresados.SubItems.Add(objfuncionesdicss.validareader("nacionalidad", "InteresadoID", respuestastringinteresados).Text);
        //            listinteresados.SubItems.Add(objfuncionesdicss.validareader("direccioncompleta", "InteresadoID", respuestastringinteresados).Text);
        //            listinteresados.SubItems.Add(objfuncionesdicss.validareader("InteresadoPoder", "InteresadoID", respuestastringinteresados).Text);
        //            listinteresados.SubItems.Add(objfuncionesdicss.validareader("InteresadoRGP", "InteresadoID", respuestastringinteresados).Text);
        //            listinteresados.SubItems.Add(TIPOPERSONA);
        //            int residuo = count % 2;
        //            if (residuo == 0)
        //            {
        //                listinteresados.BackColor = Color.LightGray;
        //            }
        //            else
        //            {
        //                listinteresados.BackColor = Color.Azure;
        //            }
        //            lvinteresados.Items.Add(listinteresados);
        //            lvinteresados.FullRowSelect = true;
        //            //lCasoNumero_texbox.Text = objfuncionesdicss.validareader("InteresadoID", "InteresadoID", respuestastringinteresados).Text;
        //            count++;
        //        }
        //        respuestastringinteresados.Close();
        //        con.Cerrarconexion();
        //        //cerrar conexion
        //    }catch(Exception E){
        //        new filelog(loguin.sId, E.ToString());
        //    }
        //}

        private void lbPadre_Click(object sender, EventArgs e)
        {
            //tabcontrolall.SelectedIndex = 18;
        }

        private void button45_Click_1(object sender, EventArgs e)
        {
            try
            {//para éste caso, éste sería un caso divicional hijo del que se está agregando en el textbox

                if (tbCasonumerohijo.Text.Trim() == "")
                {
                    MessageBox.Show("Debe escribir un caso para poder relacionarlo como caso Hijo.");
                    return;
                }

                //consultamos el casoid del caso número que se escriba
                conect con_caso_patente = new conect();
                String sQuerydivicional = "select * from caso_patente where casonumero like '%" + tbCasonumerohijo.Text.Trim() + "%';";// + sCasoId + ";";
                MySqlDataReader resp_div = con_caso_patente.getdatareader(sQuerydivicional);
                listViewDivicional.Items.Clear();
                int iCount = 0;
                String scasoidcasomadre = "";
                while (resp_div.Read())
                {   //este es un caso divicional Padre
                    iCount++;
                    scasoidcasomadre = objfuncionesdicss.validareader("CasoId", "CasoId", resp_div).Text;
                }
                resp_div.Close();
                con_caso_patente.Cerrarconexion();

                if (iCount == 1)
                { //encontramos el caso y  lo relacionamos
                    conect con_caso_patenteupdate = new conect();
                    String updatecasomadre = "Update caso_patente SET Divicionalid = '" + sCasoId + "' where casoid = '" + scasoidcasomadre + "';";
                    MySqlDataReader resp_divupdate = con_caso_patenteupdate.getdatareader(updatecasomadre);
                    if (resp_divupdate.RecordsAffected == 1)
                    { //se agrego el caso divicional padre
                        MessageBox.Show("Se agregó el caso divicional Hijo");
                        generadom(sCasoId);
                    }
                    resp_divupdate.Close();
                    con_caso_patenteupdate.Cerrarconexion();
                }
                else
                {
                    MessageBox.Show("No se encontró el caso " + sCasoId + " para relacionarlo como Caso Hijo");
                }
            }
            catch (Exception exs)
            {
                new filelog(" Agregar hijo divicional ", " : " + exs.StackTrace);
            }
        }

        private void bAddpadre_Click(object sender, EventArgs e)
        {
            try {//para éste caso, éste sería un caso divicional hijo del que se está agregando en el textbox

                if (tbCasonumeromadre.Text.Trim() =="") {
                    MessageBox.Show("Debe escribir un caso para poder relacionarlo como caso Madre.");
                    return;
                }

                //consultamos el casoid del caso número que se escriba
                conect con_caso_patente = new conect();
                String sQuerydivicional = "select * from caso_patente where casonumero like '%" + tbCasonumeromadre.Text.Trim() + "%';";// + sCasoId + ";";
                MySqlDataReader resp_div = con_caso_patente.getdatareader(sQuerydivicional);
                listViewDivicional.Items.Clear();
                int iCount = 0;
                String scasoidcasomadre = "";
                while (resp_div.Read())
                {   //este es un caso divicional Padre
                    iCount++;
                    scasoidcasomadre = objfuncionesdicss.validareader("CasoId", "CasoId", resp_div).Text;   
                }
                resp_div.Close();
                con_caso_patente.Cerrarconexion();

                if (iCount == 1)
                { //encontramos el caso y  lo relacionamos
                    conect con_caso_patenteupdate = new conect();
                    String updatecasomadre = "Update caso_patente SET Divicionalid = '" + scasoidcasomadre + "' where casoid = '" + sCasoId + "';";
                    MySqlDataReader resp_divupdate = con_caso_patenteupdate.getdatareader(updatecasomadre);
                    if (resp_divupdate.RecordsAffected == 1)
                    { //se agrego el caso divicional padre
                        MessageBox.Show("Se agregó el caso divicional Madre");
                        generadom(sCasoId);
                    }
                    resp_divupdate.Close();
                    con_caso_patenteupdate.Cerrarconexion();
                }
                else {
                    MessageBox.Show("No se encontró el caso " + sCasoId + " para relacionarlo como Caso Madre");
                }
            }catch (Exception exs){
                new filelog(" Agregar madre divicional ", " : "+exs.StackTrace);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Solicituddepatente2019 obj = new Solicituddepatente2019(loguin, sCasoId, this);
            obj.ShowDialog();
        }

        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buscarclienteform.Show();
            this.Close();
        }

        private void inicioToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            buscarclienteform.Show();
            this.Close();
        }

        private void consultacaso_Load(object sender, EventArgs e)
        {

        }

        private void tbEstatus_header_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try {
                String sPlazosdetalleid = dgPlazos.SelectedRows[0].Cells[1].Value.ToString();
                String sFechavigencia = dgPlazos.SelectedRows[0].Cells[10].Value.ToString();
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
                    }else{
                        MessageBox.Show("El plazo está atendido.");
                    }
                }else{
                    MessageBox.Show("Debe seleccionar un plazo para atender.");
                }
            }catch(Exception Ex){
                MessageBox.Show("Debe seleccionar un plazo para atender.");
            }
            
            
        }

        private void lvinteresados_DoubleClick(object sender, EventArgs e)
        {
            btn_verrelacion.PerformClick();
        }

        private void button46_Click(object sender, EventArgs e)
        {
            //// ver intersado
            try
            {
                int casoid = Int32.Parse(sCasoId);
                if (lvinteresados.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Debe Seleccionar un Titular o Inventor.");
                }
                else
                {
                    String sInteresadoid = lvinteresados.SelectedItems[0].SubItems[1].Text;//id interesado 

                    //InteresadoDetalleInterno detalleinterno = new InteresadoDetalleInterno(sInteresadoid);
                    FInteresadoDetalle detalleinterno = new FInteresadoDetalle(sInteresadoid, loguin, fCapuraform, 1, casoid);
                    detalleinterno.ShowDialog();
                    //generadom(sCasoId);
                    actualizainformaciondeinteresado();
                }
            }
            catch (Exception E)
            {
                //escribimos en log
                new filelog(loguin.sId, E.ToString());
            }
        }

        private void lv_documentelect_DoubleClick(object sender, EventArgs e)
        {
            //String sRuta = "";
            //try { 
            //    if (lv_documentelect.SelectedItems!=null)
            //    {
            //        sRuta = lv_documentelect.SelectedItems[0].SubItems[3].Text;
            //        Process.Start(sRuta);
            //        MessageBox.Show("Ruta: "+sRuta);
            //    }
            //    }
            //catch (Exception Ex)
            //{
            //    new filelog("ver edocs", "Error: " + Ex.Message);
            //    MessageBox.Show(Ex.Message+" "+ sRuta);
            //}
        }

        private void button27_Click(object sender, EventArgs e)
        {
            cb_tipodocelect.Text = "";
            tb_filename.Text = "";
            tb_descripdocelec.Text = "";
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            try {
                if (cbTiporeferencia.Text!="" && tb_referencia.Text!="")
                {
                    String sValue = (cbTiporeferencia.SelectedItem as ComboboxItem).Value +"";
                    String sReferencia = tb_referencia.Text;
                    conect conectcasointeresado = new conect();
                    String query_insert = "INSERT INTO `referencia`(`ReferenciaId`,"+
                                                        "`CasoId`,"+
                                                        "`TipoSolicitudId`,"+
                                                        "`TipoReferenciaId`,"+
                                                        "`ReferenciaNombre`)VALUES"+
                                                        "(null,'" + 
                                                        sCasoId + "', "+
                                                        "'"+gSTipoSolicitudId+"', "+
                                                        "'" + sValue + "',"+
                                                        "'" + sReferencia + "')";
                    MySqlDataReader respuesta_deletecasointeresado = conectcasointeresado.getdatareader(query_insert);
                    if (respuesta_deletecasointeresado.RecordsAffected==1)
                    {
                        MessageBox.Show("Referencia agregada correctamente");
                        consultareferencias();
                    }
                }
            }catch(Exception eX){
                new filelog("ver edocs", "Error: " + eX.Message);
                MessageBox.Show(eX.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try{
                String sReferenciaid = lvReferencias.SelectedItems[0].SubItems[2].Text;
                String schTiporeferencia = lvReferencias.SelectedItems[0].SubItems[1].Text;
                String schReferencia = lvReferencias.SelectedItems[0].SubItems[0].Text;

                var confirmResult = MessageBox.Show("¿Seguro que desea eliminar éste registro?\n " + schTiporeferencia + "\n " + schReferencia,
                                     "Confirmar eliminar referencia.",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    conect conectcasointeresado = new conect();
                    String query_insert = "delete from `referencia` where `ReferenciaId` = " + sReferenciaid + " ;";
                    MySqlDataReader respuesta_deletecasointeresado = conectcasointeresado.getdatareader(query_insert);
                    if (respuesta_deletecasointeresado.RecordsAffected == 1)
                    {
                        MessageBox.Show("Referencia eliminada correctamente");
                        consultareferencias();
                    }
                }
                else
                {
                    MessageBox.Show("Error al intentar eliminar");
                }
            }catch(Exception Ex){
                MessageBox.Show("Debe seleccionar una referencia");
            }
            

        }

        private void label85_Click(object sender, EventArgs e)
        {
            //consultaplazo();
            //con esata funciona migramos los plazos viejos a los nuevos
        }

        private void button47_Click(object sender, EventArgs e)
        {
            //Facturar_CFDI obj = new Facturar_CFDI();
            //obj.inicializacaso(this);
            //obj.ShowDialog();

            //creamos el login para poder usar el segundo proyecto

            //frmLogin frmLogin = new frmLogin();
            //frmLogin.Show();
            //frmLogin.txtUsuario.Text = "KING";
            //frmLogin.txtContrasenia.Text = "760224";
            //frmLogin.lblIdUsuario.Text = "1";

            //frmLogin.btnEntrar.Click();

            //frmFactura frmFactura = new frmFactura();
            //frmFactura.Show();
            ////se asignan datos para que pueda saber el sistema que usuario y en que datos está el caso
            //frmFactura.sIdusuario = gSUsuarioId;
            //frmFactura.lblIdUsuario.Text = gSUsuarioId;
            //frmFactura.sgTiposolicitud = gSTipoSolicitudId;
            //frmFactura.tbCasoid.Text = gSCasoId;
            

        }

        private void label19_DoubleClick(object sender, EventArgs e)//damos coble click para cambiar al cliente
        {
            //creamos una ventana en la que podamos buscar al cliente y asignarlo al caso
            buscarcliente bForm = new buscarcliente(sCasoId, gSTipoSolicitudId, lContacto_texbox.Text, lCliente_texbox_.Text);
            if (bForm.ShowDialog() == DialogResult.OK)
            {
                lCliente_texbox_.Text = bForm.sClienteidtext;
                lContacto_texbox.Text = bForm.sContactoidtext;
                gSclienteid = bForm.sClienteidvalue;
                rtCorreocontacto.Text = bForm.rtCorreocontacto_pass;
            }

        }

        private void label21_DoubleClick(object sender, EventArgs e)
        {
            addContacto objnuevocontacto = new addContacto(gSclienteid, lCliente_texbox_.Text, gSContactoid, lContacto_texbox.Text, sCasoId, gSTipoSolicitudId);
            if (objnuevocontacto.ShowDialog() == DialogResult.OK)
            {
                lContacto_texbox.Text = objnuevocontacto.SgContactotext;
                rtCorreocontacto.Text = objnuevocontacto.SgContactocorreos;

            }
        }

        private void button48_Click(object sender, EventArgs e)
        {
            try {
                /*Consultamos las anualidades para saber en cual vamos y 
                 * si ya están pagadas y si esámos 6 meses antes de la vigencia 
                 * para poder pagar la primera renovacion 
                 * 
                 * select * from anualidad where casoid = 39683 and TipoSolicitudId = 3 order by AnualidadSecuencia asc;
                 * 
                 * 
                 * Agregamos anualidades*/
                int iNumanualidades = 0;

                conect con_consultafecha_legal = new conect();
                String sQuery = "select * from caso_patente where `CasoId` = '" + sCasoId + "' and TipoSolicitudId = '" + gSTipoSolicitudId + "'";
                MySqlDataReader resp_consvig = con_consultafecha_legal.getdatareader(sQuery);
                resp_consvig.Read();
                String sCasoFechaLegal = objfuncionesdicss.validareader("CasoFechaLegal", "CasoFechaLegal", resp_consvig).Text;
                String sTipoSolicitudId = objfuncionesdicss.validareader("TipoSolicitudId", "TipoSolicitudId", resp_consvig).Text;
                String sCasoFechaRecepcion = objfuncionesdicss.validareader("CasoFechaRecepcion", "CasoFechaRecepcion", resp_consvig).Text;
                String sCasoFechaInternacional = objfuncionesdicss.validareader("CasoFechaInternacional", "CasoFechaInternacional", resp_consvig).Text;
                
                DateTime dCasoFechaRecepcion = Convert.ToDateTime(sCasoFechaRecepcion);

                resp_consvig.Close();
                con_consultafecha_legal.Cerrarconexion();


                conect con_consulta_anualidad = new conect();
                String sQuery_anualidad = "select * from anualidad where casoid = " + sCasoId + " and TipoSolicitudId = " + gSTipoSolicitudId + " order by AnualidadSecuencia desc limit 1;";
                MySqlDataReader resp_consvig_anualidad = con_consulta_anualidad.getdatareader(sQuery_anualidad);
                resp_consvig_anualidad.Read();

                String s_AnualidadSecuencia = objfuncionesdicss.validareader("AnualidadSecuencia", "AnualidadSecuencia", resp_consvig_anualidad).Text;
                String s_AnualidadIndExe = objfuncionesdicss.validareader("AnualidadIndExe", "AnualidadIndExe", resp_consvig_anualidad).Text;
                String s_AnualidadAno = objfuncionesdicss.validareader("AnualidadAno", "AnualidadAno", resp_consvig_anualidad).Text;
                String s_AnualidadMes = objfuncionesdicss.validareader("AnualidadMes", "AnualidadMes", resp_consvig_anualidad).Text;
                String s_AnualidadQuinquenio = objfuncionesdicss.validareader("AnualidadQuinquenio", "AnualidadQuinquenio", resp_consvig_anualidad).Text;
                String s_EstatusAnualidadId = objfuncionesdicss.validareader("EstatusAnualidadId", "EstatusAnualidadId", resp_consvig_anualidad).Text;
                String s_CasoId = objfuncionesdicss.validareader("CasoId", "CasoId", resp_consvig_anualidad).Text;
                String s_TipoSolicitudId = objfuncionesdicss.validareader("TipoSolicitudId", "TipoSolicitudId", resp_consvig_anualidad).Text;
                String s_AnualidadFechaLimitePago = objfuncionesdicss.validareader("AnualidadFechaLimitePago", "AnualidadFechaLimitePago", resp_consvig_anualidad).Text;
                String s_AnualidadTipo = objfuncionesdicss.validareader("AnualidadTipo", "AnualidadTipo", resp_consvig_anualidad).Text;

                resp_consvig_anualidad.Close();
                con_consulta_anualidad.Cerrarconexion();

                if (s_EstatusAnualidadId != "2" )//&& s_AnualidadTipo =="1")
                {
                    MessageBox.Show("La última anualidad no está pagada, debe pagarse antes de agregar nuevas anualidades");
                    return;
                }

                int iAnualidadsecuencia = 0;
                if (s_AnualidadSecuencia!="")
                {
                    iAnualidadsecuencia = int.Parse(s_AnualidadSecuencia);
                }
                if(sTipoSolicitudId =="2"){//Si es Model de utilidad es de 10 anualidades a 15 anualidades
                    if (iAnualidadsecuencia>10)
                    {
                        MessageBox.Show("El caso Tipo Modelo de Utilidad no puede tener más de 15 años.");
                        return;
                    }
                    iNumanualidades = 5;

                }

                if (sTipoSolicitudId == "3" || sTipoSolicitudId == "4")
                {//Si es Model de utilidad es de 15 anualidades a 25 anualidades
                    if (iAnualidadsecuencia > 20)
                    {
                        MessageBox.Show("El caso Tipo Dis. Industrial no puede tener más de 25 años.");
                        return;
                    }
                    iNumanualidades = 10;
                }

                int is_AnualidadSecuencia = Int16.Parse(s_AnualidadSecuencia);
                int is_AnualidadAno = Int16.Parse(s_AnualidadAno);
                int is_AnualidadQuinquenio = Int16.Parse(s_AnualidadQuinquenio);

                is_AnualidadSecuencia = is_AnualidadSecuencia + 1;
                is_AnualidadAno = is_AnualidadAno + 1;
                is_AnualidadQuinquenio = is_AnualidadQuinquenio + 1;
               
                String sFechacalculo = sCasoFechaInternacional;
                if (sCasoFechaInternacional == "" || sCasoFechaInternacional == "0000-00-00" || sCasoFechaInternacional== "0000/00/00")
                {
                    sFechacalculo = sCasoFechaRecepcion;
                }

                DateTime sFechaproximavigencia = DateTime.Parse(s_AnualidadFechaLimitePago);
                sFechaproximavigencia = sFechaproximavigencia.AddMonths(6);
                String sInsert_anualidad = "";
                bool sbandera = true;
                for (int x = 0; x < iNumanualidades; x++ )
                {
                    sInsert_anualidad += "INSERT INTO `anualidad` (`AnualidadSecuencia`,"+
                                                " `AnualidadIndExe`,"+
                                                "`AnualidadAno`, "+
                                                "`AnualidadMes`, "+
                                                "`AnualidadQuinquenio`, "+
                                                "`EstatusAnualidadId`, "+
                                                "`CasoId`, "+
                                                "`TipoSolicitudId`, "+
                                                "`AnualidadFechaLimitePago`, "+
                                                "`AnualidadTipo`) "+
                                                "VALUES ('" + is_AnualidadSecuencia 
                                                + "', '0', '" 
                                                + is_AnualidadAno + "', '" 
                                                + s_AnualidadMes + "', '" 
                                                + is_AnualidadQuinquenio +
                                                "', '1', '" 
                                                + sCasoId + "', '" 
                                                + gSTipoSolicitudId + "',"+
                                                " '" + sFechaproximavigencia.ToString("yyyy-MM-dd") 
                                                + "', '3');";

                    //INSERT INTO `anualidad` (`AnualidadSecuencia`, `AnualidadIndExe`, 
                    //`AnualidadAno`, `AnualidadMes`, `AnualidadQuinquenio`, `EstatusAnualidadId`, `CasoId`, `TipoSolicitudId`, 
                    //`AnualidadFechaPago`, `AnualidadFechaLimitePago`, `AnualidadTipo`) 
                    //VALUES ('16', '0', '2031', '12', '3', '1', '39683', '3', '', '2020-11-11', '3');
                    is_AnualidadAno++;
                    is_AnualidadSecuencia++;
                    if (x == 4 && sbandera) {
                        is_AnualidadQuinquenio++;
                        sFechaproximavigencia = sFechaproximavigencia.AddYears(5);
                        sbandera = false;
                    }
                }

                if (iNumanualidades == 10) {
                    dCasoFechaRecepcion = dCasoFechaRecepcion.AddYears(25);
                }
                else {
                    if (iNumanualidades == 5) {
                        dCasoFechaRecepcion = dCasoFechaRecepcion.AddYears(15);
                    }
                }

                conect con_consulta_update = new conect();
                String sUpdatevigencia = "Update caso_patente set " +
                    " CasoFechaVigencia =  '" + dCasoFechaRecepcion.ToString("yyyy-MM-dd") + "'" +
                    " where " +
                    " CasoId = " + sCasoId +
                    " and TipoSolicitudId =  '" + gSTipoSolicitudId + "'";
                MySqlDataReader resp_update = con_consulta_update.getdatareader(sUpdatevigencia);
                resp_update.Close();
                con_consulta_update.Cerrarconexion();


                conect con_consulta_insert = new conect();
                MySqlDataReader resp_Insert = con_consulta_insert.getdatareader(sInsert_anualidad);
                creaplazossubsecuentes();
                MessageBox.Show(resp_Insert.RecordsAffected+" Anualidades agregadas.");
                resp_Insert.Close();
                con_consulta_insert.Cerrarconexion();

                generadom(sCasoId);

            }catch(Exception Ex){

            }
            /*         capturaanualidades objcaptura = new capturaanualidades();
                objcaptura.ShowDialog();
                generaanualialidades(int.Parse(objcaptura.snumeroanualidades), objcaptura.sFechainicioanualidades, sCasoId, gSTipoSolicitudId);*/
            
        }
        public void creaplazossubsecuentes() {
            //creamos los plazos de las anualidades subsecuentes que se acaban agregar
            try {
                conect con_consulta_update = new conect();
                String squery = "CALL agrega_anualidades(" + sCasoId + ", " + sTiposolicitudGlobal + ", 1);";
                MySqlDataReader resp_update = con_consulta_update.getdatareader(squery);
                resp_update.Close();
                con_consulta_update.Cerrarconexion();
            }
            catch (Exception exs) {
                new filelog("linea 6433 ", " mensaje:"+exs.InnerException);
            }
        
        }
        public void generaanualialidades(int Num_anos, string sFechapresentacion, string sCasoid, string sTiposolicitud)
        {
            try {
                String DocumentoFecha = DateTime.ParseExact(sFechapresentacion, "yyyy/MM/dd", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbFechacalce.Text;// now();
                String fec_dia = DateTime.ParseExact(sFechapresentacion, "yyyy/MM/dd", CultureInfo.InvariantCulture).ToString("dd"); //tbFechacalce.Text;// now();
                String fec_mes = DateTime.ParseExact(sFechapresentacion, "yyyy/MM/dd", CultureInfo.InvariantCulture).ToString("MM"); //tbFechacalce.Text;// now();
                String fec_ano = DateTime.ParseExact(sFechapresentacion, "yyyy/MM/dd", CultureInfo.InvariantCulture).ToString("yyyy"); //tbFechacalce.Text;// now();
                String AnualidadIndExe = "";
                int sCountquinquenio = 0;
                int iNumquinquenio = 0;
                int sEstatus = 4;
                String sAnualidadfechapago = "";
                String AnualidadFechaLimitePago = "";
                String sQuerys = "";

                String date = DateTime.Now.Date.ToString();
                String Month = DateTime.Now.Month.ToString();
                String Year_actual = DateTime.Now.Year.ToString();
                int sAnoactual = Convert.ToInt32(Year_actual);
                int sAnofecha = Convert.ToInt32(fec_ano);
                bool bPrimer = true;

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
                        if (sCountquinquenio > 5)
                        {
                            //aumenta el quinquenio
                            iNumquinquenio++;
                            sCountquinquenio = 1;
                        }
                    }

                    if (iNumquinquenio > 1)
                    {
                        AnualidadFechaLimitePago = sAnofecha + "-" + fec_mes + "-" + fec_dia; //"";
                        sAnualidadfechapago = "";//sAnofecha + "-" + fec_mes + "-" + fec_dia;
                    }

                    /*Si es Diseño (Modelo de Útilidad todas son pendientes, no existen las excentas)*/
                    if (sTiposolicitud == "3")
                    {
                        AnualidadIndExe = "0";
                    }

                    if (sAnualidadfechapago =="") {
                        sAnualidadfechapago = "NULL";
                    }
                    //hacemos un insert a docuemtos y luego a relaciona docuemntos
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
                                                
                                                " '" + AnualidadFechaLimitePago + "'," +
                                                
                                                "'1');";
                    sQuerys += insertdocumento;
                    sAnofecha++;
                    if (sAnofecha >= sAnoactual)
                    {
                        sCountquinquenio++;
                    }

                }
                String total = sQuerys;
                conect con1 = new conect();
                MySqlDataReader resp_escritos = con1.getdatareader(sQuerys);
                if (resp_escritos.RecordsAffected > 0)
                {
                    MessageBox.Show("Se agregaron " + resp_escritos.RecordsAffected + " Anualidades.");
                }
                
            }
            catch (Exception Ex)
            {

            }
        }

        private void button49_Click(object sender, EventArgs e)
        {
            //si la fecha de presentacion es igual o mayor al 5 de noviembre debemos mostrar Nueva anualidades en los tipos de patentes Modelos de utilidad o Diseños industriales
            
            
            
            //tab_anualidades.ImageIndex = 6;
            //tabcontrolall.TabPages.Add(tab_anualidades);
            //tab_anualidades.Vi hide tab
        }

        public String validafechasvacias(String sFecha) {
            String resultado = "";
            try {
                if (sFecha == "0000-00-00" || sFecha == "0000/00/00" || sFecha == "00-00-0000")
                {
                    resultado = "";
                }else {
                    resultado = sFecha;                
                }
            }catch(Exception Ex){
                resultado = "";
            }
            return resultado;
        }

        private void button50_Click(object sender, EventArgs e)
        {
            Fatenderplazo obj = new Fatenderplazo(sCasoId, sTiposolicitudGlobal, loguin, gSCasoNumero);
            obj.ShowDialog();
            generadom(sCasoId);
        }


        private void dGV_docimentos_IMPI_DoubleClick(object sender, EventArgs e)
        {
            string sRutaarchivo = "";
            try
            {
                //dGV_docimentos_IMPI.Rows[0].Cells[0].Value;
                sRutaarchivo = dGV_docimentos_IMPI.SelectedRows[0].Cells[0].Value.ToString();
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
                new filelog(loguin.sId, "line:6059" + E.Message);
            }
        }

        private void tbResponsable_DoubleClick(object sender, EventArgs e)
        {
            fResponsableupdate updateResponsable = new fResponsableupdate("1");
            if (updateResponsable.ShowDialog() == DialogResult.OK)
            {
                String value = updateResponsable.sValueResponsable;//hacemos el update con este valor y ponemos el Texto en el form
                String texti = updateResponsable.sTextoResponsable;//hacemos el update con este valor y ponemos el Texto en el form
                conect con_4 = new conect();
                String updateresponsableidcaso = "UPDATE `caso_patente` SET `ResponsableId` = '" + value + "', `UsuarioId` = '" + value + "' WHERE (`CasoId` = '" + sCasoId + "');";
                MySqlDataReader resp_updateResp = con_4.getdatareader(updateresponsableidcaso);
                if (resp_updateResp != null)
                {
                    tbResponsable.Text = texti;
                }

                resp_updateResp.Close();
                con_4.Cerrarconexion();
            }
        }

        private void consultacaso_FormClosed(object sender, FormClosedEventArgs e)
        {
            try {
                buscarclienteform.Show();
            }
            catch (Exception exs) {
                new filelog("WARNING casos_king: al cerrar el caso abierto desde plazos_consulta", ""+exs.Message);
            }
            
            //MessageBox.Show("cerrar");
        }

        private void tbPlazolegal_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbPlazolegal);
        }

        private void tbClientduedate_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbClientduedate);
        }

        private void tbFechaRecimpi_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechaRecimpi);
            objfuncionesdicss.validafechnomayoralaactual(tbFechaRecimpi);
            //validacio , no puede ser  mayor a la fecha en que se este modificando éste dato
        }

        private void tbFechaconcesion_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechaconcesion);
        }

        private void tbFechadivulgacion_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechadivulgacion);
            objfuncionesdicss.validafechnomayoralaactual(tbFechadivulgacion);
        }

        private void tbFechavigencia_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechavigencia);
        }

        private void tbFechacarta_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechacarta);
        }

        private void tbFechainternacional_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechainternacional);
            objfuncionesdicss.validafechnomayoralaactual(tbFechainternacional);
        }

        private void tbFechapublicacion_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechapublicacion);
        }

        private void cbAreaimpi_SelectedIndexChanged(object sender, EventArgs e)
        {
            //guardamos los cambios del area impi
            try {
                conect con = new conect();
                String sIdspatentes = "UPDATE `caso_patente` SET `AreaImpiId` = '" + cbAreaimpi.Text + "' WHERE `caso_patente`.`CasoId` = " + sCasoId + " AND `caso_patente`.`TipoSolicitudId` = " + sTiposolicitudGlobal;
                MySqlDataReader resp_numpatentes = con.getdatareader(sIdspatentes);

                if (resp_numpatentes != null) {
                    resp_numpatentes.Read();
                    resp_numpatentes.Close();
                }
                con.Cerrarconexion();
            }
            catch (Exception ex) {
                //new filelog("", ":"+ex.StackTrace);
            }
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

        private void lContacto_texbox_DoubleClick(object sender, EventArgs e)
        {
            addContacto objnuevocontacto = new addContacto(gSclienteid, lCliente_texbox_.Text, gSContactoid, lContacto_texbox.Text, sCasoId, gSTipoSolicitudId);
            if (objnuevocontacto.ShowDialog() == DialogResult.OK)
            {
                lContacto_texbox.Text = objnuevocontacto.SgContactotext;
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
                String updateresponsableidcaso = "UPDATE `caso_patente` SET `ResponsableId` = '" + value + "', `UsuarioId` = '" + value + 
                                                "' WHERE (`CasoId` = '" + sCasoId + "' and TipoSolicitudId ='"+sTiposolicitudGlobal+"');";
                MySqlDataReader resp_updateResp = con_4.getdatareader(updateresponsableidcaso);
                if (resp_updateResp != null)
                {
                    tbResponsable.Text = texti;
                }

                resp_updateResp.Close();
                con_4.Cerrarconexion();
            }
        }

        private void lCliente_texbox__DoubleClick(object sender, EventArgs e)
        {
            //creamos una ventana en la que podamos buscar al cliente y asignarlo al caso
            buscarcliente bForm = new buscarcliente(sCasoId, gSTipoSolicitudId, lContacto_texbox.Text, lCliente_texbox_.Text);
            if (bForm.ShowDialog() == DialogResult.OK)
            {
                lCliente_texbox_.Text = bForm.sClienteidtext;
                lContacto_texbox.Text = bForm.sContactoidtext;
                gSclienteid = bForm.sClienteidvalue;
                rtCorreocontacto.Text = bForm.rtCorreocontacto_pass;
            }
        }
        //funcion repetido en un evento equivocado
        private void lContacto_texbox_TextChanged(object sender, EventArgs e)
        {
            addContacto objnuevocontacto = new addContacto(gSclienteid, lCliente_texbox_.Text, gSContactoid, lContacto_texbox.Text, sCasoId, gSTipoSolicitudId);
            if (objnuevocontacto.ShowDialog() == DialogResult.OK)
            {
                lContacto_texbox.Text = objnuevocontacto.SgContactotext;
                rtCorreocontacto.Text = objnuevocontacto.SgContactocorreos;

            }
        }

        private void label100_Click(object sender, EventArgs e)
        {

        }

        private void tbEstatusfactura_TextChanged(object sender, EventArgs e)
        {

        }

        private void button29_Click(object sender, EventArgs e)
        {
            /*
             * Generamos los documentos tipos escritos
             */
            
            
            try
            {
                
                String sIdidiomaescritos = "";
                if ((cbidiomaescrito.SelectedItem as ComboboxItem) != null)
                { //idioma seleccionado para la carta
                    sIdidiomaescritos = (cbidiomaescrito.SelectedItem as ComboboxItem).Value.ToString();
                }
                else {
                    MessageBox.Show("Seleccione el idioma del escrito.");
                    return;
                }


                if (cbEscritos.SelectedItem == null)
                {
                    return;
                }

                //cargamos los datos

                if (cbOficios.SelectedItem != null)
                {
                    String sDocumentoid = (cbOficios.SelectedItem as ComboboxItem).Value.ToString();
                    objpatentectualview = new view_caso_patentes(gSCasoId, gSTipoSolicitudId, sIdidiomaescritos, sDocumentoid);
                }
                else
                {
                    objpatentectualview = new view_caso_patentes(gSCasoId, gSTipoSolicitudId, sIdidiomaescritos);
                }

                generacarta_pat objcarta = null;
                String valuecob = (cbEscritos.SelectedItem as ComboboxItem).Value.ToString();
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
                    sCartanombreESPfile = objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoTemplateEspanol", respuesta_carta).Text;
                    sCartanombreENfile = objfuncionesdicss.validareader("SubTipoDocumentoTemplateIngles", "SubTipoDocumentoTemplateIngles", respuesta_carta).Text;
                    //Aquí podemos validar el idioma y si existe la plantilla
                    //generacarta objcarta = new generacarta(sCartanombreESPfile, valuecob, objmarcaactual);
                    if (sIdidiomaescritos == "1")
                    {//Ingles
                        if (sCartanombreENfile == "")
                        {
                            MessageBox.Show("No existe el escrito para éste idioma (EN)");
                            return;
                        }
                        String[] sValorusuario = new string[2];
                        sValorusuario[0] = "idUsuariosistema";
                        sValorusuario[1] = sUsuarioparadocs;
                        objpatentectualview.sValorescampos.Add(sValorusuario);
                        objcarta = new generacarta_pat(sCartanombreENfile, valuecob, objpatentectualview);
                    }
                    else
                    {
                        if (sIdidiomaescritos == "2")
                        {
                            if (sCartanombreESPfile == "")
                            {
                                MessageBox.Show("No existe el escrito para éste idioma (ES)");
                                return;
                            }                            
                            
                            String[] sValorusuario = new string[2];
                            sValorusuario[0] = "idUsuariosistema";
                            sValorusuario[1] = sUsuarioparadocs;
                            objpatentectualview.sValorescampos.Add(sValorusuario);
                            objcarta = new generacarta_pat(sCartanombreESPfile, valuecob, objpatentectualview);
                        }
                        else
                        {
                            MessageBox.Show("Debe seleccionar un Idioma");
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
        
        private void consultacaso_Resize(object sender, EventArgs e)
        {
            /*
            tabcontrolall.Location = new Point(this.tabcontrolall.Location.X, 75);
            tabcontrolall.Size = new Size(tabcontrolall.Width, this.Height - 260);*/
        }

        private void dGV_docimentos_IMPI_Resize(object sender, EventArgs e)
        {   /*
            dGV_docimentos_IMPI.Location = new Point(this.dGV_docimentos_IMPI.Location.X, 75);
            dGV_docimentos_IMPI.Size = new Size(dGV_docimentos_IMPI.Width, this.Height - 445);
            */
        }

        private void dgPlazos_Resize(object sender, EventArgs e)
        {
            /*
            dgPlazos.Location = new Point(this.dgPlazos.Location.X, 75);
            dgPlazos.Size = new Size(dgPlazos.Width, this.Height - 390);
            */
        }

        private void dgview_facturas_Resize(object sender, EventArgs e)
        {
            /*
            dgview_facturas.Location = new Point(this.dgview_facturas.Location.X, 75);
            dgview_facturas.Size = new Size(dgview_facturas.Width, this.Height - 390);
            */
        }


        private void label21_MouseLeave(object sender, EventArgs e)
        {
            label21.BackColor = Color.LightPink;
        }
        private void label21_MouseMove(object sender, MouseEventArgs e)
        {
            label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(93)))), ((int)(((byte)(166)))));
        }

        private void label36_MouseLeave(object sender, EventArgs e)
        {
            label36.BackColor = Color.LightPink;
        }

        private void label36_MouseMove(object sender, MouseEventArgs e)
        {
            label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(93)))), ((int)(((byte)(166)))));
        }

        private void label19_MouseLeave(object sender, EventArgs e)
        {
            label19.BackColor = Color.LightPink;
        }

        private void label19_MouseMove(object sender, MouseEventArgs e)
        {
            label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(93)))), ((int)(((byte)(166)))));
        }

        private void label27_MouseLeave(object sender, EventArgs e)
        {
            label27.BackColor = Color.LightPink;
        }

        private void label27_MouseMove(object sender, MouseEventArgs e)
        {
            label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(93)))), ((int)(((byte)(166)))));
        }

        private void label87_MouseLeave(object sender, EventArgs e)
        {
            label87.BackColor = Color.LightPink;
        }

        private void label87_MouseMove(object sender, MouseEventArgs e)
        {
            label87.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(93)))), ((int)(((byte)(166)))));
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            Solicituddepatente2019 obj = new Solicituddepatente2019(loguin, sCasoId, this);
            obj.ShowDialog();
        }

        private void button23_Click(object sender, EventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            try {
                if (checkBox4.Checked) {
                    //agregamsos las cartas
                    cbCartas.Items.Clear();
                    conect con_3_cartas = new conect();
                    //String sQueryescritosdisponibles = "SELECT " +
                    //                                           "     * " +
                    //                                           " FROM " +
                    //                                           "    estatuscasosubtipodocumento, " +
                    //                                           "    subtipodocumento " +
                    //                                           " WHERE " +
                    //                                           "     estatuscasosubtipodocumento.Estatuscasoid = " + gSEstatusCasoId + "  " +
                    //                                           "         AND estatuscasosubtipodocumento.GrupoId = 1" +// +sGrop +//El grupo falta validarlo con una variable dependiendo el tipo del caso 
                    //                                           "         AND subtipodocumento.SubTipoDocumentoId = estatuscasosubtipodocumento.SubTipoDocumentoId " +
                    //                                           "         AND subtipodocumento.TipoDocumentoId = 3 " +//carta
                    //                                           "         AND subtipodocumento.SubTipoDocumentoIndAct = 1 " +
                    //                                           //"         AND subtipodocumento.SubTipoDocumentoId in (186,205,206,210,1028,1034,1041,1043,1050,1058,1068,1070,1073,1097,1110,1116,1118,1123,1125,1126)" +
                    //                                           "         group by estatuscasosubtipodocumento.SubTipoDocumentoId order by subtipodocumento.SubTipoDocumentoDescrip;";
                    String sQueryescritosdisponibles = " SELECT  " +
                                                        "     * " +
                                                        " FROM " +
                                                        "     subtipodocumento, " +
                                                        "     gruposubtipodocumento " +
                                                        " WHERE " +
                                                        "     gruposubtipodocumento.GrupoId = 1 " +
                                                        "         AND gruposubtipodocumento.SubtipodocumentoId = subtipodocumento.SubtipodocumentoId " +
                                                        "         AND TipoDocumentoId = 3 " + // 3 es carta
                                                        "         AND SubTipoDocumentoIndAct = 1 " +
                                                        "         AND (SubTipoDocumentoTemplateEspanol != '' " +
                                                        "         OR SubTipoDocumentoTemplateIngles != ''); ";
                    //String sQueryescritosdisponibless = "select * from estatuscasosubtipodocumento, subtipodocumento where estatuscasosubtipodocumento.EstatusCasoId = " + sEstatusidint + " and estatuscasosubtipodocumento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId and estatuscasosubtipodocumento.GrupoId =1 and subtipodocumento.TipoDocumentoId = 2 group by subtipodocumento.SubTipoDocumentoId;";
                    //String sQueryescritosdisponibles = "select subtipodocumento.SubTipoDocumentoDescrip, estatuscasosubtipodocumentores.SubTipoDocumentoId from estatuscasosubtipodocumentores, subtipodocumento where estatuscasoid = " + tbEstatus.Text + " and estatuscasosubtipodocumentores.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId;";
                    MySqlDataReader resp_escritos = con_3_cartas.getdatareader(sQueryescritosdisponibles);
                    while (resp_escritos.Read())
                    {
                        String sCartas = objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoId", resp_escritos).Text;
                        if (sCartas != "")
                        {
                            cbCartas.Items.Add(objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoId", resp_escritos));//Estatus
                        }
                    }
                    resp_escritos.Close();
                    con_3_cartas.Cerrarconexion();

                } else {
                    


                    //agregamsos las cartas
                    cbCartas.Items.Clear();
                    conect con_3_cartas = new conect();
                    String sQueryescritosdisponibles = "SELECT " +
                                                               "     * " +
                                                               " FROM " +
                                                               "    estatuscasosubtipodocumento, " +
                                                               "    subtipodocumento " +
                                                               " WHERE " +
                                                               "     estatuscasosubtipodocumento.Estatuscasoid = " + gSEstatusCasoId + "  " +
                                                               "         AND estatuscasosubtipodocumento.GrupoId = 1" +// +sGrop +//El grupo falta validarlo con una variable dependiendo el tipo del caso 
                                                               "         AND subtipodocumento.SubTipoDocumentoId = estatuscasosubtipodocumento.SubTipoDocumentoId " +
                                                               "         AND subtipodocumento.TipoDocumentoId = 3 " +//3 es carta
                                                               "         AND subtipodocumento.SubTipoDocumentoIndAct = 1 " +
                                                               //"         AND subtipodocumento.SubTipoDocumentoId in (186,205,206,210,1028,1034,1041,1043,1050,1058,1068,1070,1073,1097,1110,1116,1118,1123,1125,1126)" +
                                                               "         group by estatuscasosubtipodocumento.SubTipoDocumentoId order by subtipodocumento.SubTipoDocumentoDescrip;";
                    //String sQueryescritosdisponibles = " SELECT  " +
                    //            "     * " +
                    //            " FROM " +
                    //            "     subtipodocumento, " +
                    //            "     gruposubtipodocumento " +
                    //            " WHERE " +
                    //            "     gruposubtipodocumento.GrupoId = 1 " +//MARCAS
                    //            "         AND gruposubtipodocumento.SubtipodocumentoId = subtipodocumento.SubtipodocumentoId " +
                    //            "         AND TipoDocumentoId = 3 " +
                    //            "         AND SubTipoDocumentoIndAct = 1 " +
                    //            "         AND (SubTipoDocumentoTemplateEspanol != '' " +
                    //            "         OR SubTipoDocumentoTemplateIngles != ''); ";
                    //String sQueryescritosdisponibless = "select * from estatuscasosubtipodocumento, subtipodocumento where estatuscasosubtipodocumento.EstatusCasoId = " + sEstatusidint + " and estatuscasosubtipodocumento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId and estatuscasosubtipodocumento.GrupoId =1 and subtipodocumento.TipoDocumentoId = 2 group by subtipodocumento.SubTipoDocumentoId;";
                    //String sQueryescritosdisponibles = "select subtipodocumento.SubTipoDocumentoDescrip, estatuscasosubtipodocumentores.SubTipoDocumentoId from estatuscasosubtipodocumentores, subtipodocumento where estatuscasoid = " + tbEstatus.Text + " and estatuscasosubtipodocumentores.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId;";
                    MySqlDataReader resp_escritos = con_3_cartas.getdatareader(sQueryescritosdisponibles);
                    while (resp_escritos.Read())
                    {
                        String sCartas = objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoId", resp_escritos).Text;
                        if (sCartas != "")
                        {
                            cbCartas.Items.Add(objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoId", resp_escritos));//Estatus
                        }

                    }
                    resp_escritos.Close();
                    con_3_cartas.Cerrarconexion();
                }
            }
            catch (Exception Exs) {
                new filelog("error linea 7090", Exs.Message);
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox6.Checked)
                {
                    //agregamsos las cartas
                    cbEscritos.Items.Clear();
                    conect con_3_cartas = new conect();
                    //String sQueryescritosdisponibles = "SELECT " +
                    //                                           "     * " +
                    //                                           " FROM " +
                    //                                           "    estatuscasosubtipodocumento, " +
                    //                                           "    subtipodocumento " +
                    //                                           " WHERE " +
                    //                                           "     estatuscasosubtipodocumento.Estatuscasoid = " + gSEstatusCasoId + "  " +
                    //                                           "         AND estatuscasosubtipodocumento.GrupoId = 1" +// +sGrop +//El grupo falta validarlo con una variable dependiendo el tipo del caso 
                    //                                           "         AND subtipodocumento.SubTipoDocumentoId = estatuscasosubtipodocumento.SubTipoDocumentoId " +
                    //                                           "         AND subtipodocumento.TipoDocumentoId = 3 " +//carta
                    //                                           "         AND subtipodocumento.SubTipoDocumentoIndAct = 1 " +
                    //                                           //"         AND subtipodocumento.SubTipoDocumentoId in (186,205,206,210,1028,1034,1041,1043,1050,1058,1068,1070,1073,1097,1110,1116,1118,1123,1125,1126)" +
                    //                                           "         group by estatuscasosubtipodocumento.SubTipoDocumentoId order by subtipodocumento.SubTipoDocumentoDescrip;";
                    String sQueryescritosdisponibles = " SELECT  " +
                                                        "     * " +
                                                        " FROM " +
                                                        "     subtipodocumento, " +
                                                        "     gruposubtipodocumento " +
                                                        " WHERE " +
                                                        "     gruposubtipodocumento.GrupoId = 1 " +
                                                        "         AND gruposubtipodocumento.SubtipodocumentoId = subtipodocumento.SubtipodocumentoId " +
                                                        "         AND TipoDocumentoId = 2 " +
                                                        "         AND SubTipoDocumentoIndAct = 1 " +
                                                        "         AND (SubTipoDocumentoTemplateEspanol != '' " +
                                                        "         OR SubTipoDocumentoTemplateIngles != ''); ";
                    //String sQueryescritosdisponibless = "select * from estatuscasosubtipodocumento, subtipodocumento where estatuscasosubtipodocumento.EstatusCasoId = " + sEstatusidint + " and estatuscasosubtipodocumento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId and estatuscasosubtipodocumento.GrupoId =1 and subtipodocumento.TipoDocumentoId = 2 group by subtipodocumento.SubTipoDocumentoId;";
                    //String sQueryescritosdisponibles = "select subtipodocumento.SubTipoDocumentoDescrip, estatuscasosubtipodocumentores.SubTipoDocumentoId from estatuscasosubtipodocumentores, subtipodocumento where estatuscasoid = " + tbEstatus.Text + " and estatuscasosubtipodocumentores.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId;";
                    MySqlDataReader resp_escritos = con_3_cartas.getdatareader(sQueryescritosdisponibles);
                    while (resp_escritos.Read())
                    {
                        String sCartas = objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoId", resp_escritos).Text;
                        if (sCartas != "")
                        {
                            cbEscritos.Items.Add(objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoId", resp_escritos));//Estatus
                        }
                    }
                    resp_escritos.Close();
                    con_3_cartas.Cerrarconexion();
                }else{
                    //agregamsos las cartas
                    cbEscritos.Items.Clear();
                    conect con_3_cartas = new conect();
                    String sQueryescritosdisponibles = "SELECT " +
                                                               "     * " +
                                                               " FROM " +
                                                               "    estatuscasosubtipodocumento, " +
                                                               "    subtipodocumento " +
                                                               " WHERE " +
                                                               "     estatuscasosubtipodocumento.Estatuscasoid = " + gSEstatusCasoId + "  " +
                                                               "         AND estatuscasosubtipodocumento.GrupoId = 1" +// +sGrop +//El grupo falta validarlo con una variable dependiendo el tipo del caso 
                                                               "         AND subtipodocumento.SubTipoDocumentoId = estatuscasosubtipodocumento.SubTipoDocumentoId " +
                                                               "         AND subtipodocumento.TipoDocumentoId = 2 " +//carta
                                                               "         AND subtipodocumento.SubTipoDocumentoIndAct = 1 " +
                                                               //"         AND subtipodocumento.SubTipoDocumentoId in (186,205,206,210,1028,1034,1041,1043,1050,1058,1068,1070,1073,1097,1110,1116,1118,1123,1125,1126)" +
                                                               "         group by estatuscasosubtipodocumento.SubTipoDocumentoId order by subtipodocumento.SubTipoDocumentoDescrip;";
                    //String sQueryescritosdisponibles = " SELECT  " +
                    //            "     * " +
                    //            " FROM " +
                    //            "     subtipodocumento, " +
                    //            "     gruposubtipodocumento " +
                    //            " WHERE " +
                    //            "     gruposubtipodocumento.GrupoId = 1 " +//MARCAS
                    //            "         AND gruposubtipodocumento.SubtipodocumentoId = subtipodocumento.SubtipodocumentoId " +
                    //            "         AND TipoDocumentoId = 3 " +
                    //            "         AND SubTipoDocumentoIndAct = 1 " +
                    //            "         AND (SubTipoDocumentoTemplateEspanol != '' " +
                    //            "         OR SubTipoDocumentoTemplateIngles != ''); ";
                    //String sQueryescritosdisponibless = "select * from estatuscasosubtipodocumento, subtipodocumento where estatuscasosubtipodocumento.EstatusCasoId = " + sEstatusidint + " and estatuscasosubtipodocumento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId and estatuscasosubtipodocumento.GrupoId =1 and subtipodocumento.TipoDocumentoId = 2 group by subtipodocumento.SubTipoDocumentoId;";
                    //String sQueryescritosdisponibles = "select subtipodocumento.SubTipoDocumentoDescrip, estatuscasosubtipodocumentores.SubTipoDocumentoId from estatuscasosubtipodocumentores, subtipodocumento where estatuscasoid = " + tbEstatus.Text + " and estatuscasosubtipodocumentores.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId;";
                    MySqlDataReader resp_escritos = con_3_cartas.getdatareader(sQueryescritosdisponibles);
                    while (resp_escritos.Read())
                    {
                        String sCartas = objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoId", resp_escritos).Text;
                        if (sCartas != "")
                        {
                            cbEscritos.Items.Add(objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoId", resp_escritos));//Estatus
                        }
                    }
                    resp_escritos.Close();
                    con_3_cartas.Cerrarconexion();
                }
            }catch (Exception Exs){
                new filelog("error linea 7090", Exs.Message);
            }
        }

        private void button18_Click(object sender, EventArgs e)
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
                String sDescripciondoc = dGV_docimentos_IMPI.SelectedRows[0].Cells[10].Value.ToString();
                String sdocimentoid = dGV_docimentos_IMPI.SelectedRows[0].Cells[19].Value.ToString();

                String sPlazodetalleid = dGV_docimentos_IMPI.SelectedRows[0].Cells[1].Value.ToString();
                String sPlazoid = dGV_docimentos_IMPI.SelectedRows[0].Cells[0].Value.ToString();
                if (dGV_docimentos_IMPI.SelectedRows == null)
                {
                    MessageBox.Show("Debe seleccionar un documento para eliminar");
                    return;
                }
                DialogResult result = MessageBox.Show("¿Seguro que desea eliminar el Documento \n con codigo de barras: \"" + scodigodebarras + "\"\n"+
                                     " con Folio: \"" + sFoliodoc + "\"\n"+
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
                        carga_documentos_IMPI(gSCasoId, gSTipoSolicitudId);
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

        private void tbCvepais_TextChanged(object sender, EventArgs e)
        {
            int iValuepais = Convert.ToInt32((cbCvpais.SelectedItem as ComboboxItem).Value.ToString());
            //(comboBoxPais.SelectedItem as ComboboxItem).Value = iValuepais;
            //textBoxcve.Text = paises[iValuepais];
            //comboBoxPais.Text = paisesclave[iValuepais];
        }

        private void cbCvpais_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                int iValuepais = Convert.ToInt32((cbCvpais.SelectedItem as ComboboxItem).Value.ToString());
                cbNombrepais.Text = paisesclave[iValuepais];
            }catch (Exception exs) {
                new filelog("linea 7468: ", " mensaje: "+exs.StackTrace);
            }
        }

        private void tbfechaprio_TextChanged(object sender, EventArgs e)//validamos que puedan pegar diferentes tipos de fechas
        {
            try {
                
                //pueden pegar fechas con los siguientes formatos 
                //ddmmyyyy
                //dd.mm.yyyy
                //dd/mm/yyyy
                //dd-mm-yyyy

                if (tbfechaprio.Text.Length == 8) { //validamos el primer formato ddmmyyyy
                    Boolean bsondigitos = true;
                    for (int x = 0; x < tbfechaprio.Text.Length; x++)
                    {
                        if (!Char.IsDigit(tbfechaprio.Text[x]))
                        {
                            bsondigitos = false;
                        }
                    }
                    if (bsondigitos) { //todos los caracateres osn numeros y son 8 por lo que supondremos que son ddmmyyyy
                        String sfechaconcarateres = tbfechaprio.Text.Substring(0,2)+"-"+tbfechaprio.Text.Substring(2, 2)+"-"+tbfechaprio.Text.Substring(4, 4);
                        tbfechaprio.Text = sfechaconcarateres;
                    }
                }
                String spuntouno = tbfechaprio.Text.Substring(2, 1);
                String spuntodos = tbfechaprio.Text.Substring(5, 1);

                if (tbfechaprio.Text.Length == 10 && spuntouno == "." && spuntodos == ".")//entonces validamos el segundo formato dd.mm.yyyy
                {
                    Boolean bsondigitos = true;
                    for (int x = 0; x < tbfechaprio.Text.Length; x++)
                    {
                        if ((!Char.IsDigit(tbfechaprio.Text[x])) && x!=2 && x!=5)
                        {
                            bsondigitos = false;
                        }
                    }
                    if(bsondigitos)
                    {
                        tbfechaprio.Text = tbfechaprio.Text.Replace('.', '-');
                    }
                }

           }catch (Exception exs) {
                new filelog("analizando la prioridad", " : " + exs.StackTrace);
            }
            


        }

        private void button23_Click_1(object sender, EventArgs e)
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
            String sRuta = "";
            try
            {
                
                if (dgDocumentoselectronicos.SelectedRows != null)
                {
                    sRuta = dgDocumentoselectronicos.SelectedRows[0].Cells[5].Value.ToString();//lv_documentelect.SelectedItems[0].SubItems[3].Text;
                    Process.Start(sRuta);
                    //MessageBox.Show("Ruta: " + sRuta);
                }
            }
            catch (Exception Ex)
            {
                new filelog("ver edocs", "Error: " + Ex.Message);
                //MessageBox.Show(Ex.Message + " " + sRuta);
            }
        }

        private void cbFormatoscheck_CheckedChanged(object sender, EventArgs e)
        {
            cargaformatos();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            /*
             * Generamos los documentos tipos Formatos
             */
            try
            {
                String sIdidiomaescritos = "";
                if ((cbidiomaescrito.SelectedItem as ComboboxItem) != null)
                { //idioma seleccionado para la carta
                    sIdidiomaescritos = (cbidiomaescrito.SelectedItem as ComboboxItem).Value.ToString();
                }else
                {
                    MessageBox.Show("Seleccione el idioma del formato o escrito.");
                    return;
                }


                if (cbFormatos.SelectedItem == null)
                {
                    return;
                }

                //cargamos los datos

                if (cbOficios.SelectedItem != null)
                {
                    String sDocumentoid = (cbOficios.SelectedItem as ComboboxItem).Value.ToString();
                    objpatentectualview = new view_caso_patentes(gSCasoId, gSTipoSolicitudId, sIdidiomaescritos, sDocumentoid);
                }
                else
                {
                    objpatentectualview = new view_caso_patentes(gSCasoId, gSTipoSolicitudId, sIdidiomaescritos);
                }

                generacarta_pat objcarta = null;
                String valuecob = (cbFormatos.SelectedItem as ComboboxItem).Value.ToString();
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
                    sCartanombreESPfile = objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoTemplateEspanol", respuesta_carta).Text;
                    sCartanombreENfile = objfuncionesdicss.validareader("SubTipoDocumentoTemplateIngles", "SubTipoDocumentoTemplateIngles", respuesta_carta).Text;
                    //Aquí podemos validar el idioma y si existe la plantilla
                    //generacarta objcarta = new generacarta(sCartanombreESPfile, valuecob, objmarcaactual);
                    if (sIdidiomaescritos == "1")
                    {//Ingles
                        if (sCartanombreENfile == "")
                        {
                            MessageBox.Show("No existe el escrito para éste idioma (EN)");
                            return;
                        }

                        String[] sValorusuario = new string[2];
                        sValorusuario[0] = "idUsuariosistema";
                        sValorusuario[1] = sUsuarioparadocs;
                        objpatentectualview.sValorescampos.Add(sValorusuario);
                        objcarta = new generacarta_pat(sCartanombreENfile, valuecob, objpatentectualview);
                    }
                    else
                    {
                        if (sIdidiomaescritos == "2")
                        {
                            if (sCartanombreESPfile == "")
                            {
                                MessageBox.Show("No existe el escrito para éste idioma (ES)");
                                return;
                            }

                            String[] sValorusuario = new string[2];
                            sValorusuario[0] = "idUsuariosistema";
                            sValorusuario[1] = sUsuarioparadocs;
                            objpatentectualview.sValorescampos.Add(sValorusuario);
                            objcarta = new generacarta_pat(sCartanombreESPfile, valuecob, objpatentectualview);
                        }
                        else
                        {
                            MessageBox.Show("Debe seleccionar un Idioma");
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

        private void bAgregarplazo_Click(object sender, EventArgs e)
        {
            //AQUÍ abriremos una ventana para capturar los datos del plazo nuevo fecha estatus plazo  relacionado si es que existe etc ..
            try {
                //loguin.sId es el usuario de la sesion
                agregaplazo obj = new agregaplazo(sCasoId, sTiposolicitudGlobal, gSCasoNumero, loguin.sId, 1);//pasamos el grupo
                if (obj.ShowDialog() == DialogResult.OK) { 
                    generadom(sCasoId);
                }
            }catch (Exception exs) {
                new filelog("WARNING casos king: ", " excepcion al agregar plazo manual "+exs.Message);
            }
        }

        public bool validaversion(String sVersion)
        {

            bool breinicia = false;
            try
            {
                conect con_filev = new conect();
                String sQuery = "SELECT * FROM act_version order by idact_version desc limit 1;";
                MySqlDataReader resp_consltv = con_filev.getdatareader(sQuery);
                String sIdversionact = "";
                String sFechaversion = "";
                while (resp_consltv.Read())
                {
                    sIdversionact = objfuncionesdicss.validareader("v_actual", "v_actual", resp_consltv).Text;
                    sFechaversion = objfuncionesdicss.validareader("fecha", "fecha", resp_consltv).Text;
                    if (sIdversionact != sVersion)
                    {
                        MessageBox.Show("Deben actualizar la versión de casos king");
                        breinicia = true;
                    }
                }
                con_filev.Cerrarconexion();
                resp_consltv.Close();

                //if (breinicia) {
                //    buscarclienteform.Show();
                //    this.Close();
                //}
                return breinicia;
            }
            catch (Exception exs)
            {
                return breinicia;
            }

        }

        private void button30_Click_1(object sender, EventArgs e)
        {
            // boton elimimar
            try
            {
                if (lvinteresados.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Debes seleccionar un Titular o Inventor.");
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
                        {
                            // MessageBox.Show("El interesado esta asociado a otros casos, no se puede eliminar");

                            var confirmResult2 = MessageBox.Show("¿Seguro que desea ELIMINAR este interesado  " + sInteresadoNomnbre + " ?", "Eliminar Contacto", MessageBoxButtons.YesNo);
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
                                    respuesta_deletecasointeresado.Close();
                                    conectcasointeresado.Cerrarconexion();
                                }

                            }

                            generadom(sCasoId);
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
                                    MessageBox.Show("No se pudo eliminar este interesado");
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
                                        respuesta_deletecasointeresado.Close();
                                        conectcasointeresado.Cerrarconexion();
                                    }

                                    MessageBox.Show("Interesado " + sInteresadoNomnbre + " eliminado correctamente");
                                    respuesta_delete.Close();
                                    conectdeleteinteresado.Cerrarconexion();
                                    generadom(sCasoId);

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
                                        // MessageBox.Show("Direccion asociada a este interesado borrada correctamente.");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception E)
            {
                //escribimos en log falta el log de lalo
                new filelog(loguin.sId, E.ToString());
            }
        }

        private void button17_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
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
                        //codigo original
                        //String sValor = "";
                        //if (!(row.Cells[ic - 1].Value is null))
                        //{
                        //   sValor = row.Cells[ic - 1].Value.ToString();
                        //}

                        //if (sValor == "01/01/0001 12:00:00 a. m.")
                        //{
                        //    sValor = "";
                        //}
                        //obj.SetCellValue(ir, ic, sValor);
                        //fin de codigo original


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

        private void dgPlazos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tbFechaRecimpi_Leave(object sender, EventArgs e)
        {
            tbFechaRecimpi.Text = tbFechaRecimpi.Text.Replace("/", "-").Replace(".", "-");
        }

        private void tbFechaconcesion_Leave(object sender, EventArgs e)
        {
            tbFechaconcesion.Text = tbFechaconcesion.Text.Replace("/", "-").Replace(".", "-");
        }

        private void tbFechadivulgacion_Leave(object sender, EventArgs e)
        {
            tbFechadivulgacion.Text = tbFechadivulgacion.Text.Replace("/", "-").Replace(".", "-");
        }

        private void tbFechavigencia_Leave(object sender, EventArgs e)
        {
            tbFechavigencia.Text = tbFechavigencia.Text.Replace("/", "-").Replace(".", "-");
        }

        private void tbFechacarta_Leave(object sender, EventArgs e)
        {
            tbFechacarta.Text = tbFechacarta.Text.Replace("/", "-").Replace(".", "-");
        }

        private void tbFechainternacional_Leave(object sender, EventArgs e)
        {
            tbFechainternacional.Text = tbFechainternacional.Text.Replace("/", "-").Replace(".", "-");
        }

        private void tbFechapublicacion_Leave(object sender, EventArgs e)
        {
            tbFechapublicacion.Text = tbFechapublicacion.Text.Replace("/", "-").Replace(".", "-");
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label33_DoubleClick(object sender, EventArgs e)
        {
            fBuscarcorresponsal bForm = new fBuscarcorresponsal(sCasoId, gSTipoSolicitudId, tblCotaccorresponsal.Text, tblCorresponsal.Text);
            if (bForm.ShowDialog() == DialogResult.OK)
            {
                tblCorresponsal.Text = bForm.sClienteidtext;
                tblCotaccorresponsal.Text = bForm.sContactoidtext;
                gSclienteid = bForm.sClienteidvalue;
                richTextBox4.Text = bForm.rtCorreocontacto_pass;
            }
        }

        private void label33_MouseLeave(object sender, EventArgs e)
        {
            label33.BackColor = Color.LightPink;
        }

        private void label33_MouseMove(object sender, MouseEventArgs e)
        {
            label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(93)))), ((int)(((byte)(166)))));
        }

        private void button31_Click(object sender, EventArgs e)
        {
            // boton elimimar
            try
            {

                if (lvinventores.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Debes seleccionar un Titular o Inventor.");
                }
                else
                {
                    String sInteresadoid = lvinventores.SelectedItems[0].SubItems[1].Text;//id interesado
                    String sInteresadoNomnbre = lvinventores.SelectedItems[0].SubItems[2].Text;//id interesado

                    conect conectnumcasos = new conect();
                    String kwerynumcasos = "SELECT COUNT(*) FROM casoinventor WHERE casoinventor.InventorId = " + sInteresadoid + "  group by CasoId;";
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
                        {
                            // MessageBox.Show("El interesado esta asociado a otros casos, no se puede eliminar");

                            var confirmResult2 = MessageBox.Show("¿Seguro que desea ELIMINAR este inventor  " + sInteresadoNomnbre + " ?", "Eliminar inventor", MessageBoxButtons.YesNo);
                            if (confirmResult2 == DialogResult.Yes)
                            {
                                conect conectcasointeresado = new conect();
                                String kweryconect = "DELETE FROM casoinventor WHERE CasoId =  " + sCasoId + " AND InventorId = " + sInteresadoid + ";";
                                MySqlDataReader respuesta_deletecasointeresado = conectcasointeresado.getdatareader(kweryconect);
                                if (respuesta_deletecasointeresado == null)
                                {
                                    MessageBox.Show("No se pudo eliminar casoinventor");
                                }
                                else
                                {
                                    respuesta_deletecasointeresado.Close();
                                    conectcasointeresado.Cerrarconexion();
                                }

                            }

                            generadom(sCasoId);
                        }
                        else
                        {
                            var confirmResult = MessageBox.Show("¿Seguro que desea ELIMINAR este inventor " + sInteresadoNomnbre + " ?", "Eliminar Contacto", MessageBoxButtons.YesNo);
                            if (confirmResult == DialogResult.Yes)
                            {
                                conect conectdeleteinteresado = new conect();
                                String kwerydeleteinteresado = "DELETE FROM inventor WHERE interesado.InventorId =  " + sInteresadoid + ";";
                                MySqlDataReader respuesta_delete = conectdeleteinteresado.getdatareader(kwerydeleteinteresado);

                                if (respuesta_delete == null)
                                {
                                    MessageBox.Show("No se pudo eliminar este interesado");
                                }
                                else
                                {
                                    conect conectcasointeresado = new conect();
                                    String kweryconect = "DELETE FROM casoinventor WHERE CasoId =  " + sCasoId + " AND TipoSolicitudId = '"+"' AND InventorId = " + sInteresadoid + ";";
                                    MySqlDataReader respuesta_deletecasointeresado = conectcasointeresado.getdatareader(kweryconect);
                                    if (respuesta_deletecasointeresado == null)
                                    {
                                        MessageBox.Show("No se pudo eliminar casoinventor");
                                    }
                                    else
                                    {
                                        respuesta_deletecasointeresado.Close();
                                        conectcasointeresado.Cerrarconexion();
                                    }

                                    MessageBox.Show("Inventor " + sInteresadoNomnbre + " eliminado correctamente");
                                    respuesta_delete.Close();
                                    conectdeleteinteresado.Cerrarconexion();
                                    generadom(sCasoId);

                                    //conect conecdeletedireccioninteresado = new conect();
                                    //String kwerydeleteinteresadodireccion = "DELETE FROM direccion WHERE direccion.InteresadoId = " + sInteresadoid + ";";
                                    //MySqlDataReader respuesta_deletedireccion = conecdeletedireccioninteresado.getdatareader(kwerydeleteinteresadodireccion);
                                    //if (respuesta_delete == null)
                                    //{
                                    //    MessageBox.Show("No se pudo eliminar la direccion asociada a este interesado");
                                    //}
                                    //else
                                    //{
                                    //    respuesta_deletedireccion.Close();
                                    //    conecdeletedireccioninteresado.Cerrarconexion();
                                    //}
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception E)
            {
                //escribimos en log falta el log de lalo
                new filelog(loguin.sId, E.ToString());
            }
        }

        private void tabPage13_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

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
                if ((cbIdiomacarta.SelectedItem as ComboboxItem) != null)
                { //idioma seleccionado para la carta
                    sIdidiomaescritos = (cbIdiomacarta.SelectedItem as ComboboxItem).Value.ToString();
                }
                else
                {
                    MessageBox.Show("Seleccione un idioma.");
                    return;
                }


                if (cbPoder.SelectedItem == null)
                {
                    return;
                }

                //cargamos los datos

                if (cbOficiosEscritos.SelectedItem != null)
                {
                    String sDocumentoid = (cbOficiosEscritos.SelectedItem as ComboboxItem).Value.ToString();
                    objpatentectualview = new view_caso_patentes(gSCasoId, gSTipoSolicitudId, sIdidiomaescritos, sDocumentoid);
                }
                else
                {
                    objpatentectualview = new view_caso_patentes(gSCasoId, gSTipoSolicitudId, sIdidiomaescritos);
                }

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
                    sCartanombreESPfile = objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoTemplateEspanol", respuesta_carta).Text;
                    sCartanombreENfile = objfuncionesdicss.validareader("SubTipoDocumentoTemplateIngles", "SubTipoDocumentoTemplateIngles", respuesta_carta).Text;
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
                        objpatentectualview.sValorescampos.Add(sValorusuario);
                        generacarta_pat objcarta = new generacarta_pat(sCartanombreENfile, valuecob, objpatentectualview);
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
                            objpatentectualview.sValorescampos.Add(sValorusuario);
                            generacarta_pat objcarta = new generacarta_pat(sCartanombreESPfile, valuecob, objpatentectualview);
                        }
                        else
                        {
                            MessageBox.Show("Debe seleccionar un Idioma");
                        }

                    }


                }
                respuesta_carta.Close();
                con_carta.Cerrarconexion();
            }
            catch (Exception ex)
            {
                new filelog(loguin.sId, "linea 4141" + ex.ToString());
            }
        }

        private void btnGenerarcesion_Click_1(object sender, EventArgs e)
        {
            try
            {
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


                if (cbCesiones.SelectedItem == null)
                {
                    return;
                }

                //cargamos los datos

                if (cbOficiosEscritos.SelectedItem != null)
                {
                    String sDocumentoid = (cbOficiosEscritos.SelectedItem as ComboboxItem).Value.ToString();
                    objpatentectualview = new view_caso_patentes(gSCasoId, gSTipoSolicitudId, sIdidiomaescritos, sDocumentoid);
                }
                else
                {
                    objpatentectualview = new view_caso_patentes(gSCasoId, gSTipoSolicitudId, sIdidiomaescritos);
                }

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
                    sCartanombreESPfile = objfuncionesdicss.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoTemplateEspanol", respuesta_carta).Text;
                    sCartanombreENfile = objfuncionesdicss.validareader("SubTipoDocumentoTemplateIngles", "SubTipoDocumentoTemplateIngles", respuesta_carta).Text;
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
                        objpatentectualview.sValorescampos.Add(sValorusuario);
                        generacarta_pat objcarta = new generacarta_pat(sCartanombreENfile, valuecob, objpatentectualview);
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
                            objpatentectualview.sValorescampos.Add(sValorusuario);
                            generacarta_pat objcarta = new generacarta_pat(sCartanombreESPfile, valuecob, objpatentectualview);
                        }
                        else
                        {
                            MessageBox.Show("Debe seleccionar un Idioma");
                        }

                    }


                }
                respuesta_carta.Close();
                con_carta.Cerrarconexion();
            }
            catch (Exception ex)
            {
                new filelog(loguin.sId, "linea 4141" + ex.ToString());
            }
        }
    }
}
