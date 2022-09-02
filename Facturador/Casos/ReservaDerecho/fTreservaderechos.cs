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

namespace Facturador
{
    public partial class fTreservaderechos : Form
    {
        public Form1 loguin;
        public captura fCapuraform;
        public String sCasoId;
        public Consutlacaso buscarclienteform;
        funcionesdicss objfuncionesdicss = new funcionesdicss();
        public fTreservaderechos(Form1 fLoguin, captura fcaptura, Consutlacaso buscarcliente, String CasoId)
        {
            loguin = fLoguin;
            fCapuraform = fcaptura;
            sCasoId = CasoId;
            buscarclienteform = buscarcliente;
            InitializeComponent();
            lCasoID.Text = CasoId;
        }

        public void carga_documentos_IMPI(string sCasoiddom, string gSTipoSolicitudId) {
            try
            {
                dGV_docimentos_IMPI.Rows.Clear();
                //lvdocumentosimpi.Items.Clear();
                //documentosimpi
                conect con2 = new conect();
                String squeryadocumentos = " SELECT " +
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
                while (resp_docimpi.Read())
                {
                    //lvdocumentosimpi 
                    String sMes = objfuncionesdicss.validareader("diasfiff", "casoid", resp_docimpi).Text;
                    int iMes = Int32.Parse(sMes) / 30;

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
                                dRows.Cells[11].Value = objfuncionesdicss.validareader("Estatus_doc", "casoid", resp_docimpi).Text;// "";//Estatus  Estatus_doc

                                dRows.Cells[12].Value = objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text;
                                dRows.Cells[13].Value = "";
                                dRows.Cells[14].Value = "";
                                dRows.Cells[15].Value = "";
                                dRows.Cells[16].Value = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[17].Value = objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi).Text;

                            }
                            break;
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
                                dRows.Cells[8].Value = objfuncionesdicss.validareader("DocumentoFecha", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[9].Value = "";
                                dRows.Cells[10].Value = objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text;
                                dRows.Cells[11].Value = objfuncionesdicss.validareader("Estatus_doc", "casoid", resp_docimpi).Text;// "";//Estatus  Estatus_doc

                                dRows.Cells[12].Value = objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text;
                                dRows.Cells[13].Value = "";
                                dRows.Cells[14].Value = "";
                                dRows.Cells[15].Value = "";
                                dRows.Cells[16].Value = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[17].Value = objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi, true).Text;
                                //Subtipodocumentoidultimoescrito = objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi).Text;

                            }
                            break;
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
                            }
                            break;
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
                                dRows.Cells[11].Value = objfuncionesdicss.validareader("Estatus_doc", "casoid", resp_docimpi).Text;// "";//Estatus  Estatus_doc

                                dRows.Cells[12].Value = objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text;
                                dRows.Cells[13].Value = "";
                                dRows.Cells[14].Value = "";
                                dRows.Cells[15].Value = "";
                                dRows.Cells[16].Value = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                            }
                            break;
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
                            }
                            break;
                        case "Recordatorio":
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
                            }
                            break;
                        default:
                            {
                                items = new ListViewItem("Tipo de documento no considerado");//link
                                items.SubItems.Add(objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);//tipo
                                dRows.Cells[0].Value = "Tipo de documento no considerado";
                                dRows.Cells[1].Value = objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text;
                            }
                            break;
                    }/*por ahora sólo consideraremos 5 tipos de documentos mencionados arriba*/

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

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        public void consultaplazo_nuevos()
        {
            try
            {
                conect con_tcon_edocs = new conect();
                String sConsultaplazos = "";// select * from plazo_general_vista " +
                //                         "where casoid = " + sCasoId +
                //                         " and TipoSolicitudId = " + gSTipoSolicitudId;
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
                    //dRows.Cells[8].Value = sValidafechavacia(sFecha_notificacion_impi);
                    //dRows.Cells[9].Value = sValidafechavacia(sFecha_Vencimiento_regular_impi);
                    ////dRows.Cells[10].Value = sValidafechavacia(sFecha_vencimiento_3m_impi);
                    //dRows.Cells[10].Value = sValidafechavacia(sFecha_vencimiento_4m_impi);
                    //dRows.Cells[11].Value = sValidafechavacia(sFecha_atendio_plazo_impi);
                    //dRows.Cells[12].Value = satendio_plazoimpi;
                    //dRows.Cells[13].Value = sDoc_atendio;
                    //dRows.Cells[14].Value = sMotivo_cancelacion_plazo_impi;
                    //dRows.Cells[15].Value = sValidafechavacia(sFecha_cancelacion_plazo_impi);

                    dRows.Cells[16].Value = sUsuariocancelo;
                    dRows.Cells[17].Value = sFecha_atendio_plazo_impi_sistema;
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

        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }
    }
}
