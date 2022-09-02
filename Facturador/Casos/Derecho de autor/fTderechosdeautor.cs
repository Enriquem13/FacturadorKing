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
    public partial class fTderechosdeautor : Form
    {
        public Form1 loguin;
        public captura fCapuraform;
        public String sCasoId;
        public bDerechoautor buscarclienteform;
        public int iIndiceids_global = 0;
        public String[] sArrayids;
        public fTderechosdeautor(Form1 fLoguin, captura fcaptura, bDerechoautor buscarcliente, String CasoId, String TipoSolicitudId)
        {
            InitializeComponent();
            loguin = fLoguin;
            fCapuraform = fcaptura;
            sCasoId = CasoId;
            buscarclienteform = buscarcliente;
            //lCasoID.Text = CasoId;
            conect con = new conect();
            String sIds = "select count(*) as numpatentes from caso_registrodeobra";
            MySqlDataReader resp_numids = con.getdatareader(sIds);
            resp_numids.Read();
            String sNumerodeids = validareader("numpatentes", "numpatentes", resp_numids).Text;
            resp_numids.Close();
            int iNumerogrupoids = System.Convert.ToInt32(sNumerodeids);
            sArrayids = new String[iNumerogrupoids];

            String sGetids = "select * from caso_registrodeobra";
            MySqlDataReader resp_getids = con.getdatareader(sGetids);
            int iIndiceids = 0;
            while (resp_getids.Read())
            {
                sArrayids[iIndiceids] = validareader("Casoid", "Casoid", resp_getids).Text;
                iIndiceids++;
            }
            resp_getids.Close();
            iIndiceids_global = Array.IndexOf(sArrayids, CasoId);
            generadom(sCasoId);
            //insertadatos();
        }

        public void insertadatos() {
            conect con = new conect();
            String querycontencioso = "    SELECT " +
                                    " CasoId, " +
                                    " TipoSolicitudId, " +
                                    " SubTipoSolicitudId, " +
                                    " TipoPctId, " +
                                    " CasoDenominacion, " +
                                    " CasoTitulo, " +
                                    " IdiomaId, " +
                                    "DATE_FORMAT(CasoFechaPruebaUso , '%Y-%m-%d') as   CasoFechaPruebaUso, " +
                                    "DATE_FORMAT(CasoFechaRenovacion , '%Y-%m-%d') as   CasoFechaRenovacion, " +
                                    "DATE_FORMAT(CasoEstatusFechaPruebaUso , '%Y-%m-%d') as   CasoEstatusFechaPruebaUso, " +
                                    "DATE_FORMAT(CasoEstatusFechaRenovacion , '%Y-%m-%d') as   CasoEstatusFechaRenovacion, " +
                                    " CasoEstatusTipoPruebaUso, " +
                                    " CasoEstatusIdPruebaUso, " +
                                    " CasoEstatusTipoRenovacion, " +
                                    " CasoEstatusIDRenovacion, " +
                                    "DATE_FORMAT(CasoFechaConcesion , '%Y-%m-%d') as   CasoFechaConcesion, " +
                                    "DATE_FORMAT(CasoFechaLegal , '%Y-%m-%d') as   CasoFechaLegal, " +
                                    "DATE_FORMAT(CasoFechaPresentacion , '%Y-%m-%d') as   CasoFechaPresentacion, " +
                                    "DATE_FORMAT(CasoFechaDivulgacionPrevia , '%Y-%m-%d') as   CasoFechaDivulgacionPrevia, " +
                                    "DATE_FORMAT(CasoFechaRecepcion , '%Y-%m-%d') as   CasoFechaRecepcion, " +
                                    "DATE_FORMAT(CasoFechaVigencia , '%Y-%m-%d') as   CasoFechaVigencia, " +
                                    " CasoNumConcedida, " +
                                    " CasoNumExpediente, " +
                                    " CasoNumeroExpedienteLargo, " +
                                    " CasoNumero, " +
                                    " ResponsableId, " +
                                    " TipoMarcaId, " +
                                    " CasoReferenciaPrevia, " +
                                    " CasoLeyendaNoReservable, " +
                                    " CasoReservaColor, " +
                                    " CasoComecializado, " +
                                    " CasoRelacionRegistro, " +
                                    " CasoCantidad, " +
                                    " CasoTipoRelacion, " +
                                    " CasoPoder, " +
                                    " CasoCartaPoder, " +
                                    " CasoImporte, " +
                                    "DATE_FORMAT(CasoFechaAlta , '%Y-%m-%d') as   CasoFechaAlta, " +
                                    "DATE_FORMAT(CasoFechaUltMod , '%Y-%m-%d') as   CasoFechaUltMod, " +
                                    " CasoTipoCaptura, " +
                                    " CasoMarcaColectiva, " +
                                    " CasoNumeroConcedida, " +
                                    " CasoNumeroExpediente, " +
                                    " CasoCatalogoTipoZonaGeografic, " +
                                    " CasoCatalogoIDZonaGeografica, " +
                                    " CasoMarcaComunitaria, " +
                                    " CasoTitular, " +
                                    "DATE_FORMAT(CasoFechaFilingSistema , '%Y-%m-%d') as   CasoFechaFilingSistema, " +
                                    "DATE_FORMAT(CasoFechaFilingCliente , '%Y-%m-%d') as   CasoFechaFilingCliente, " +
                                    "DATE_FORMAT(CasoFechaCartaCliente , '%Y-%m-%d') as   CasoFechaCartaCliente, " +
                                    " CasoWizPlazoTipo, " +
                                    " CasoIdRelacionado, " +
                                    " EstatusCasoId, " +
                                    " OrigenId, " +
                                    " UsuarioId, " +
                                    " TipoSolicitudDivId, " +
                                    " CasoNumeroExpedienteLargoDiv, " +
                                    "DATE_FORMAT(CasoFechaPresentacionDiv , '%Y-%m-%d') as   CasoFechaPresentacionDiv, " +
                                    " AreaImpiId, " +
                                    " CasoIndPph, " +
                                    "DATE_FORMAT(CasoFechaInternacional , '%Y-%m-%d') as   CasoFechaInternacional, " +
                                    " PaisId, " +
                                    "DATE_FORMAT(CasoFechaPruebaUsoSig , '%Y-%m-%d') as   CasoFechaPruebaUsoSig, " +
                                    "DATE_FORMAT(CasoFechaPublicacionSolicitud , '%Y-%m-%d') as   CasoFechaPublicacionSolicitud, " +
                                    " CasoNumRegistroInt, " +
                                    " CasoIndNoUso, " +
                                    " TipoReservaId, " +
                                    " SubTipoReservaId, " +
                                    " ParteRepresentadaId, " +
                                    " SentidoResolucionId, " +
                                    "DATE_FORMAT(CasoFechaResolucion , '%Y-%m-%d') as   CasoFechaResolucion, " +
                                    " CasoEncargadoExterno, " +
                                    " CasoEncargado, " +
                                    " CasoComentario, " +
                                    " CasoDisenoClasificacion "+               
                                    " FROM " +
                                    "     caso " +
                                    " WHERE " +
                                    "      tiposolicitudid = 15 or tiposolicitudid = 16; ";
            MySqlDataReader respuestastring3 = con.getdatareader(querycontencioso);
            while (respuestastring3.Read())
            {
                String sQuerycontencioso = " INSERT INTO `caso_registrodeobra` (`CasoId`, `TipoSolicitudId`, `CasoDenominacion`, `CasoTitulo`, `IdiomaId`, `CasoFechaLegal`, `CasoFechaRecepcion`, `CasoNumConcedida`, `CasoNumeroExpedienteLargo`, `CasoNumero`, `ResponsableId`, `TipoMarcaId`, `CasoTipoCaptura`, `CasoTitular`, `EstatusCasoId`, `PaisId`) VALUES " +
                                            " ('" + validareader("CasoId", "CasoId", respuestastring3).Text + "', " +
                                            " '" + validareader("TipoSolicitudId", "CasoId", respuestastring3).Text + "', " +
                                            " '" + validareader("CasoDenominacion", "CasoId", respuestastring3).Text + "', " +
                                            " '" + validareader("CasoTitulo", "CasoId", respuestastring3).Text + "', " +
                                            " '" + validareader("IdiomaId", "CasoId", respuestastring3).Text + "', " +
                                            " '" + validareader("CasoFechaLegal", "CasoId", respuestastring3).Text + "', " +
                                            " '" + validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text + "', " +
                                            " '" + validareader("CasoNumConcedida", "CasoId", respuestastring3).Text + "', " +
                                            " '" + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text + "', " +
                                            " '" + validareader("CasoNumero", "CasoId", respuestastring3).Text + "', " +
                                            " '" + validareader("ResponsableId", "CasoId", respuestastring3).Text + "', " +
                                            " '" + validareader("TipoMarcaId", "CasoId", respuestastring3).Text + "', " +
                                            " '" + validareader("CasoTipoCaptura", "CasoId", respuestastring3).Text + "', " +
                                            " '" + validareader("CasoTitular", "CasoId", respuestastring3).Text + "', " +
                                            " '" + validareader("EstatusCasoId", "CasoId", respuestastring3).Text + "', " +
                                            " '" + validareader("PaisId", "CasoId", respuestastring3).Text + "'" +
                                            ");";
                MySqlDataReader respuestastring4 = con.getdatareader(sQuerycontencioso);
            }
        }
        public void generadom(String sCasoidgenera)
        {
            caso_derechoautor caso_derecho = new caso_derechoautor(sCasoidgenera, "15");
            tbCasoNumero.Text = caso_derecho.CasoNumero;
            lCasoID.Text = caso_derecho.CasoId;
            lExpediente.Text = caso_derecho.CasoNumeroExpedienteLargo;
            lRegistro.Text = "";
            lPais.Text = "";
            lCliente.Text = "";
            lTitular.Text = "";
            lReferencia.Text = "";
            lCorresponsal.Text = "";
            lCotaccorresponsal.Text = "";
            lContacto.Text = "";
            rtCorreocontacto.Text = "";
            lResponsable.Text = "";
            rtCorreocorresponsal.Text = "";
            tbEstatus.Text = caso_derecho.EstatusCasoId;
            tbExpediente.Text = caso_derecho.CasoNumeroExpedienteLargo;
            tbRegistro.Text = "";
            tbTipo.Text = "";
            tbFechadepresentacion.Text = caso_derecho.CasoFechaRecepcion;
            tbFecharegistro.Text = caso_derecho.CasoFechaConcesion;
            tbFechacarta.Text = caso_derecho.CasoFechaCartaCliente;
            tbFehcainicio.Text = caso_derecho.CasoFechaLegal;
            tbFechavigencia.Text = caso_derecho.CasoFechaVigencia;
            cbTipoobra.Text = "";
            rtbTituloobra.Text = caso_derecho.CasoTituloespanol;
            rtSintesis.Text = caso_derecho.CasoTituloingles;

            //sCasoId = sCasoidgenera;
            //lCasoID.Text = sCasoidgenera;
            //conect con = new conect();
            //progressBar1.Show();
            //progressBar1.Value = 0;
            //this.Enabled = false;
            //String sQuery = "Select * from caso_registroobra_view where CasoId = " + sCasoidgenera + " ;";
            //                //" CasoId, " +
            //                //" TipoSolicitudId, " +
            //                ////" CasoDenominacion, " +
            //                ////" CasoTitulo, " +
            //                //" IdiomaId, " +
            //                //" DATE_FORMAT(CasoFechaLegal , '%d-%m-%Y') as CasoFechaLegal, " +
            //                //" DATE_FORMAT(CasoFechaRecepcion , '%d-%m-%Y') as CasoFechaRecepcion, " +
            //                //" CasoNumConcedida, " +
            //                //" CasoNumeroExpedienteLargo, " +
            //                //" CasoNumero, " +
            //                //" ResponsableId, " +
            //                //" TipoMarcaId, " +
            //                //" CasoTipoCaptura, " +
            //                //" CasoTitular, " +
            //                //" EstatusCasoId, " +
            //                //" PaisId, " +
            //                //" DATE_FORMAT(CasoFechaVigencia , '%d-%m-%Y') as CasoFechaVigencia," +
            //                //" DATE_FORMAT(CasoFechaCartaCliente , '%d-%m-%Y') as CasoFechaCartaCliente, " +
            //                //" UsuarioId "+
            //                //" FROM" +
            //                //"    caso_registrodeobra" +
            //                //" WHERE " +
            //                //"        caso_registrodeobra.CasoId = '" + sCasoidgenera + "'";
            //MySqlDataReader respuestastring3 = con.getdatareader(sQuery);
            //progressBar1.Value = 10;
            //while (respuestastring3.Read())
            //{
            //    tbCasoNumero.Text = validareader("CasoNumero", "CasoId", respuestastring3).Text;
            //    lExpediente.Text = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
            //    lRegistro.Text = validareader("CasoNumConcedida", "CasoId", respuestastring3).Text;
            //    lExpediente.Text = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
            //    String sIdpais = validareader("PaisId", "CasoId", respuestastring3).Text;
            //    if (sIdpais != "")
            //    {
            //        MySqlDataReader respuestaPais = con.getdatareader("select * from pais where PaisId = " + sIdpais);
            //        while (respuestaPais.Read())
            //        {
            //            lPais.Text = validareader("PaisNombre", "PaisId", respuestaPais).Text;
            //        }
            //        respuestaPais.Close();
            //    }
            //    //consultamos al cliente
            //    String sClienteid = "";
            //    MySqlDataReader respuestaCliente = con.getdatareader("Select * from casocliente, cliente where casocliente.CasoId = " + validareader("CasoId", "CasoId", respuestastring3).Text + " and cliente.clienteid =  casocliente.clienteid;");
            //    while (respuestaCliente.Read())
            //    {
            //        lCliente.Text = validareader("ClienteNombre", "ClienteId", respuestaCliente).Text;
            //        sClienteid = validareader("ClienteId", "ClienteId", respuestaCliente).Text;
            //    }
            //    respuestaCliente.Close();

            //    //consultamos al contacto del cliente
            //    if (sClienteid != "")
            //    {
            //        MySqlDataReader respuestaContacto = con.getdatareader("select * from contacto where Clienteid = " + sClienteid);
            //        while (respuestaContacto.Read())
            //        {
            //            lContacto.Text = validareader("ContactoNombre", "ContactoId", respuestaContacto).Text;
            //            rtCorreocontacto.Text = validareader("ContactoEmail", "ContactoId", respuestaContacto).Text;
            //        }
            //        respuestaContacto.Close();
            //    }

            //    String idUsuario = validareader("UsuarioId", "CasoId", respuestastring3).Text;
            //    if (idUsuario != "")
            //    {
            //        MySqlDataReader respuestaUser = con.getdatareader("select * from usuario where UsuarioId = " + idUsuario);
            //        while (respuestaUser.Read())
            //        {
            //            lResponsable.Text = validareader("UsuarioNombre", "UsuarioId", respuestaUser).Text;

            //        }
            //        respuestaUser.Close();
            //    }

            //    String sReferencia = validareader("CasoId", "CasoId", respuestastring3).Text;
            //    if (sReferencia != "")
            //    {

            //        MySqlDataReader respuestaReferencia = con.getdatareader("select * from referencia where CasoId = " + sReferencia);
            //        while (respuestaReferencia.Read())
            //        {
            //            lReferencia.Text = validareader("ReferenciaNombre", "referenciaid", respuestaReferencia).Text;
            //        }
            //        respuestaReferencia.Close();
            //    }

            //    lTitular.Text = validareader("CasoTitular", "CasoId", respuestastring3).Text;
            //    tbCasoNumero.Text = validareader("CasoNumero", "CasoId", respuestastring3).Text;
            //    String sEstatuscasoid = validareader("EstatusCasoId", "CasoId", respuestastring3).Text;
            //    if (sEstatuscasoid != "")
            //    {
            //        MySqlDataReader respuestaEstatus = con.getdatareader("select * from estatuscaso where EstatusCasoId = " + sEstatuscasoid);
            //        while (respuestaEstatus.Read())
            //        {
            //            tbEstatus.Text = validareader("EstatusCasoDescrip", "EstatusCasoId", respuestaEstatus).Text;
            //        }
            //        respuestaEstatus.Close();
            //    }

            //    //Tipo solicitud ID
            //    String sTipoSolicitudId = validareader("TipoSolicitudId", "CasoId", respuestastring3).Text;
            //    if (sTipoSolicitudId != "")
            //    {
            //        MySqlDataReader respuestaTiposolic = con.getdatareader("select * from tiposolicitud where TipoSolicitudId = " + sTipoSolicitudId);
            //        while (respuestaTiposolic.Read())
            //        {

            //            tbTipo.Text = validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestaTiposolic).Text;
            //        }
            //        respuestaTiposolic.Close();
            //    }

            //    tbExpediente.Text = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
            //    tbRegistro.Text = validareader("CasoNumConcedida", "CasoId", respuestastring3).Text;
            //    tbFechacarta.Text = validareader("CasoFechaCartaCliente", "CasoId", respuestastring3).Text;
            //    tbFechadepresentacion.Text = "00-00-0000";
            //    tbFehcainicio.Text = validareader("CasoFechaVigencia", "CasoId", respuestastring3).Text;
            //    tbFechavigencia.Text = validareader("CasoFechaVigencia", "CasoId", respuestastring3).Text;

            //    //tbDFerecharecepcion.Text = validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text;
            //    //tbDFechaconcesion.Text = validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text;
            //    //tbDFechaprobo.Text = validareader("CasoFechaprobouso", "CasoId", respuestastring3).Text;
            //    //tbDFechacarta.Text = validareader("CasoFechaCartaCliente", "CasoId", respuestastring3).Text;
            //    //tbDFechainiciouso.Text = validareader("CasoFechainiciouso", "CasoId", respuestastring3).Text;
            //    //tbDFechavigencia.Text = validareader("CasoFechaVigencia", "CasoId", respuestastring3).Text;
            //    //tbDSigpruebauso.Text = validareader("CasoFechaPruebaUsoSig", "CasoId", respuestastring3).Text;
            //    //tbEstatus.Text = validareader("EstatusCasoId", "CasoId", respuestastring3).Text;

            //    //dependiendo el idioma ponemos 
            //    rtbTituloobra.Text = validareader("CasoDenominacion", "CasoId", respuestastring3).Text;
            //}

            ////Estatus caso
            //progressBar1.Value = 90;
            //respuestastring3.Close();
            //progressBar1.Value = 100;
            //progressBar1.Hide();
            //this.Enabled = true;
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

        private void button2_Click(object sender, EventArgs e)
        {
            buscarclienteform.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loguin.Close();
            fCapuraform.Close();
            buscarclienteform.Close();
            this.Close();
        }

        private void button38_Click(object sender, EventArgs e)
        {
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

        private void button39_Click(object sender, EventArgs e)
        {
            //int iCasoid = System.Convert.ToInt32(sCasoId) - 1;
            //generadom(iCasoid + "");
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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void fTderechosdeautor_FormClosing(object sender, FormClosingEventArgs e)
        {
            buscarclienteform.Show();
        }
    }
}
