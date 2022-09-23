using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;
using Facturador.plazos_forms;
using System.Diagnostics;
using SpreadsheetLight;
using Facturador.Casos.Documentos;
using Facturador.Casos.Patentes;


namespace Facturador
{
    public partial class fTcontencioso : Form
    {
        public Form1 loguin;
        public captura captura;
        public Image obj = null;
        //20220310 Fsalazar
        public captura fCapuraform;
        //20220310 Fsalazar

        public String Fecha = "";
        public bContencioso consultacaso;
        public String sCasoId;

        public bMarcas buscarclienteform;

        public Boolean bBanderaadelanteatras;
        public int icontadorbusqueda = 0;
        public int iIndiceids_global = 0;
        public String[] sArrayids;
        //public String = sTipogrupoglobal;

        //20220303 Fsalazar tipo de Solicitud
        public String gSTipoSolicitudId = "";
        public String gSclienteid = "";
        public String gSContactoid = "";
        //20220303 Fsalazar fin de modificación

        //20220309 Fsalazar carpeta para los documentos electronicos
        public String gSCasoNumero = "";
        //public String sCarpetadocumentos = "DigitalizadoPatentes\\documentosimpi";
        //public String sCarpetadocumentos = "DigitalizadoContencioso\\edocs";
        //public String sCarpetadocumentos = "DigitalizadoContencioso\\edocs";
        public String sCarpetadocumentos = "Edocs\\Contencioso";
        //20220309 Fsalazar fin de modificación

        //20220328FSV 
        funcionesdicss objfuncionesdicss = new funcionesdicss();
        view_caso_contencioso objcontenciosoctualview = null;

        public String Subtipodocumentoidultimoescrito;


        public bool bClienteduedate_update = false;
        public bool bFechapresentacion_update = false;
        public bool bFechaResolucion_update = false;

        public bool tbinteresado_update = false;
        public bool bExpediente_update = false;
        public bool bActor_update = false;
        public bool bDemandado_update = false;
        public bool bAutoridadDemandada_update = false;
        public bool bTerceroInteresado_update = false;
        public bool bInstancia_update = false;
        public bool bExpControvertido_update = false;
        public bool bClase_update = false;


        public bool bTitulo_update = false;
        public bool btituloidiomaoriginal_update = false;
        public bool bEncargadoExterno_update = false;
        public bool bMotivo_update = false;
        public bool bObservaciones_update = false;
        public bool Fecha_Pendiente_update = false;
        public bool seguimiento_update = false;
        public bool bFechaConcesion_update = false;
        public String NombreUsuario;
        public String IdUsuario;
        //public bool bTituloIngles_update = false;

        //20220328FSV


            public fTcontencioso(Form1 loguinp, captura cap, bContencioso consul, String sCasoIdcaso)
        {
            loguin = loguinp;
            cap = captura;
            //sTipogrupoglobal = consul.sGTipocaso;
            consultacaso = consul;
            sCasoId = sCasoIdcaso;
            NombreUsuario = loguin.sUsername;
            IdUsuario = loguin.sId;
            InitializeComponent();
  
            conect con = new conect();
            String sIds = "select count(*) as numpatentes from caso_contencioso";
            MySqlDataReader resp_numids = con.getdatareader(sIds);
            resp_numids.Read();
            String sNumerodeids = validareader("numpatentes", "numpatentes", resp_numids).Text;
            resp_numids.Close();
            int iNumerogrupoids = System.Convert.ToInt32(sNumerodeids);
            sArrayids = new String[iNumerogrupoids];


            String sGetids = "select * from caso_contencioso";
            MySqlDataReader resp_getids = con.getdatareader(sGetids);
            int iIndiceids = 0;
            while (resp_getids.Read())
            {
                sArrayids[iIndiceids] = validareader("Casoid", "Casoid", resp_getids).Text;
                iIndiceids++;
            }
            resp_getids.Close();
            con.Cerrarconexion();
            //iIndiceids_global = Array.IndexOf(sArrayids, sCasoIdcaso);
            //20220327GSV No sabemos porque viene desactivado, pero lo reactivamos
            iIndiceids_global = Array.IndexOf(sArrayids, sCasoIdcaso);
            //20220327FSV Reactivar
            comboBox1.Items.Add("ajustar y mover");
            comboBox1.Items.Add("mover");
            comboBox1.Items.Add("ajustar");
            //20220309 Llenamos la sección Edocs
            //consultamos los tipos de documentos para el combo edocs
            try
            {
                conect con_tcon_edocs = new conect();
                String sTipoEdocsquery = "select *  from tipodocumentoelectronico;";
                MySqlDataReader resp_tedocs = con_tcon_edocs.getdatareader(sTipoEdocsquery);
                cb_tipodocelect.Items.Clear();
                while (resp_tedocs.Read())
                {
                    cb_tipodocelect.Items.Add(validareader("TipoDocumentoElectronicoDescrip", "TipoDocumentoElectronicoId", resp_tedocs));
                }
                resp_tedocs.Close();
                con_tcon_edocs.Cerrarconexion();
            }
            catch (Exception Ex)
            {
                new filelog(loguin.sId, "linea 340: " + Ex.ToString());
            }


            generadom(sCasoId);

        }

        private string controlsInfoStr;
        private void button10_Click(object sender, EventArgs e)
        {
            controlsInfoStr = cursor.GetSizeAndPositionOfControlsToString(this);
        }
        private void button4_Click(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    cursor.WorkType = cursor.MoveOrResize.Resize;
                    break;
                case 1:
                    cursor.WorkType = cursor.MoveOrResize.Move;
                    break;
                case 2:
                    cursor.WorkType = cursor.MoveOrResize.MoveAndResize;

                    break;
            }
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            cursor.Init(dGV_docimentos_IMPI);
            comboBox1.SelectedIndex = 0;
 
        }
  


            public void limpiarcontenido()
        {

            tbTituloHeader.Text = "";
            tbCasoHeader.Text = "";
            tbCasoIdHeader.Text = "";
            tbExpedienteHeader.Text = "";
            tbEstatusHeader.Text = "";
            tbTipo.Text = "";
            tbEstatusfactura.Text = "";

            tbClienteHeader.Text = "";
            tbTitularHeader.Text = "";
            tbReferenciaHeader.Text = "";
            tbPaisHeader.Text = "";
            tbResponsableHeader.Text = "";
            tbAcciones.Text = "";
            Fecha_Pendiente.Text = "";

            tbContactoHeader.Text = "";
            rtCorreocontacto.Text = "";
            tbCorresponsalHeader.Text = "";
            tbContactoCorresponsalHeader.Text = "";
            richTextBox4.Text = "";


            //LImpiar las fechas
            tbInstitucion.Text = "";
            tbClientduedate.Text = "";
            tbFechapresentacion.Text = "";
            tbFecharesolucion.Text = "";

            //LImpiar Frame
            tbExpediente.Text = "";
            tbActor.Text = "";
            tbDemandado.Text = "";
            tbAutoridadDemandada.Text = "";
            tbTerceroInteresado.Text = "";
            tbInstancia.Text = "";
            tbExpControvertido.Text = "";
            tbClase.Text = "";
            ID_Seguimiento.Text = "";
            //Datos Generales
            rtTitulo.Text = "";
            rtTituloidionaoriginal.Text = "";
            tbEncargadoimpi.Text = "";
            rtbMotivo.Text = "";
            rtbObservaciones.Text = "";


        }
        public void generadom(String sCasoidgenera)
        {
            try
            {
                //20220329FSV Limpiamos campos editables
                resetvariable();
                //20220329FSV Fin de modificacion

                limpiarcontenido();
                sCasoId = sCasoidgenera;
                //lCasoID.Text = sCasoidgenera;
                tbCasoIdHeader.Text = sCasoidgenera;
                conect con = new conect();
                /*progressBar1.Show();
                progressBar1.Value = 0;*/
                if (loguin.sUsuarioCodigo == "1" || loguin.sUsuarioCodigo == "3")
                {
                    bAgregarplazo.Visible = true;
                }

                this.Enabled = false;
                int icontgeneradom = 0;
                conect conexcontencioso = new conect();
                String sQuery = "SELECT CasoId, " +
                                " TipoSolicitudId," +//subtiposolicitudoid
                                " TipoContenciosoId," +//subtiposolicitudoid
                                " CasoTituloespanol," +
                                " CasoTituloIngles," + //20220325FSV agregamos el titulo en ingles
                                " IdiomaId, " +
                                //no se utiliza realmente" DATE_FORMAT(CasoFechaConcesion , '%d-%m-%Y') as  CasoFechaConcesion, " + 
                                //" DATE_FORMAT(CasoFechaLegal , '%d-%m-%Y') as  CasoFechaLegal,    "+
                                //" DATE_FORMAT(CasoFechaDivulgacionPrevia , '%d-%m-%Y') as  CasoFechaDivulgacionPrevia,  "+
                                " DATE_FORMAT(CasoFechaRecepcion , '%d-%m-%Y') as  CasoFechaRecepcion,   " +
                                //" DATE_FORMAT(CasoFechaVigencia , '%d-%m-%Y') as  CasoFechaVigencia,    "+
                                " DATE_FORMAT(Fecha_Pendiente , '%d-%m-%Y') as  Fecha_Pendiente,    " +
                                " CasoNumeroExpedienteLargo,    CasoNumero,    ResponsableId," +
                                "    TipoMarcaId,  " +
                                " DATE_FORMAT(CasoFechaAlta , '%d-%m-%Y') as  CasoFechaAlta, " +
                                "    CasoTipoCaptura,   " +
                                "   Get_Interesados_tiposol(Casoid, TipoSolicitudId) as NombreUtilInt, " +
                                " DATE_FORMAT(CasoFechaFilingSistema , '%d-%m-%Y') as  CasoFechaFilingSistema,   " +
                                " DATE_FORMAT(CasoFechaFilingCliente , '%d-%m-%Y') as  CasoFechaFilingCliente,    " +
                                " DATE_FORMAT(CasoFechaCartaCliente , '%d-%m-%Y') as  CasoFechaCartaCliente,   " +
                                //" DATE_FORMAT(CasoFechaPresentacion , '%d-%m-%Y') as  CasoFechaPresentacion,   " +
                                " EstatusCasoId,   " +
                                " UsuarioId,    " +
                                " PaisId,    " +
                                " CasoEncargadoExterno, " +
                                " DATE_FORMAT(CasoFechaResolucion , '%d-%m-%Y') as CasoFechaResolucion, SentidoResolucionId, ParteRepresentadaId, " +
                                //    " DATE_FORMAT(CasoFechaPruebaUsoSig , '%d-%m-%Y') as  CasoFechaPruebaUsoSig,   "+
                                " CasoMotivo,    " +
                                " CasoObservaciones,    " +
                                " Pendiente,    " +
                                " Actor,    " +
                                " Demandado,    " +
                                " AutoridadDemandada,    " +
                                " TerceroInteresado,    " +
                                " Instancia,    " +
                                " ExpControvertido,    " +
                                " Clase,    " +
                                " ID_Seguimiento    " +
                                //" CasoNumConcedida    " +
                                "FROM    caso_contencioso " +
                                //debe buscar por el folio exacto
                                //"WHERE   caso_contencioso.CasoId  LIKE '%" + sCasoidgenera + "%'";
                                "WHERE   caso_contencioso.CasoId  = '" + sCasoidgenera + "'";
                //debe buscar por el folio exacto

                MySqlDataReader respuestastring3 = conexcontencioso.getdatareader(sQuery);

                while (respuestastring3.Read())
                {
                    //Variables Globales
                    gSCasoNumero = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                    gSTipoSolicitudId = validareader("TipoSolicitudId", "CasoId", respuestastring3).Text;

                    String ID_Seguimiento_V = validareader("ID_Seguimiento", "CasoId", respuestastring3).Text;
                    String CasoIds = validareader("CasoId", "CasoId", respuestastring3).Text;
                    //Datos del Encabezado
                    tbTituloHeader.Text = validareader("CasoTituloespanol", "CasoId", respuestastring3).Text;
                    tbCasoHeader.Text = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                    tbExpedienteHeader.Text = validareader  ("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                    tbTitularHeader.Text = validareader("NombreUtilInt", "CasoId", respuestastring3).Text;

                    //Buscaremos el status del interesado para saber si es o no vigilado
                    String QueryInteresadoUpdate = "Select * from casointeresado where CasoId= '" + CasoIds + "' AND TipoSolicitudId ='" + gSTipoSolicitudId + "';";
                    MySqlDataReader interesadorespuesta = conexcontencioso.getdatareader(QueryInteresadoUpdate);
                    String InteresadoID = "";
                    String EstatusInteresado = "";
                    if (interesadorespuesta != null)
                    {
                        while (interesadorespuesta.Read())
                        {
                            InteresadoID = validareader("InteresadoId", "CasoInteresadoId", interesadorespuesta).Text;
                        }
                        String QueryestatusUpdate = "Select * from seguimiento_interesado where GrupoId=3 and InteresadoId=" + InteresadoID;
                        MySqlDataReader estatusrespuesta = conexcontencioso.getdatareader(QueryestatusUpdate);
                        if (estatusrespuesta != null) { 
                        while (estatusrespuesta.Read())
                        {
                            EstatusInteresado = validareader("GrupoId", "InteresadoId", estatusrespuesta).Text;
                        }
                        
                    }
                        estatusrespuesta.Close();
                        //Actualizamos segun el cambio en la tabla interesado si es que lo hay
                        if (EstatusInteresado == "3")
                        {
                            conect coninteresado = new conect();
                            String seguimientoupdt = "UPDATE `caso_contencioso` SET `ID_Seguimiento` =4 " +
                                //" WHERE `caso_contencioso`.`CasoId` = " + sCasoId + " AND `caso_contencioso`.`TipoSolicitudId` = " + sTiposolicitudGlobal;
                                " WHERE `caso_contencioso`.`CasoTitular` = '" + tbTitularHeader.Text + "' ";
                            MySqlDataReader resp_seguimiento = coninteresado.getdatareader(seguimientoupdt);
                            resp_seguimiento.Read();

                            if (resp_seguimiento == null)
                            {
                                MessageBox.Show("Error al hacer el update. " + seguimientoupdt);
                            }
                            else
                            {
                                MessageBox.Show("Se Cambio el estatus de todos los casos contenciosos con el  mismo interesado" + tbTitularHeader.Text + "a No vigilado.Contacte al administrador");
                            }

                            resp_seguimiento.Close();
                            coninteresado.Cerrarconexion();
                        }
                        if (EstatusInteresado == "" || EstatusInteresado == "0")
                        {
                            if (ID_Seguimiento_V == "4")
                            {
                                conect coninteresado = new conect();
                                String seguimientoupdt = "UPDATE `caso_contencioso` SET `ID_Seguimiento` =1 " +
                                    //" WHERE `caso_contencioso`.`CasoId` = " + sCasoId + " AND `caso_contencioso`.`TipoSolicitudId` = " + sTiposolicitudGlobal;
                                    " WHERE `caso_contencioso`.`CasoTitular` = '" + tbTitularHeader.Text + "' ";
                                MySqlDataReader resp_seguimiento = coninteresado.getdatareader(seguimientoupdt);
                                resp_seguimiento.Read();

                                if (resp_seguimiento == null)
                                {
                                    MessageBox.Show("Error al hacer el update. " + seguimientoupdt);
                                }
                                else
                                {
                                    MessageBox.Show("Se Cambio el estatus de todos los casos contenciosos con el  mismo interesado" + tbTitularHeader.Text + "a Vigilado dudas Contacte al administrador");
                                }

                                resp_seguimiento.Close();
                                coninteresado.Cerrarconexion();
                            }
                        }
                    }

                    interesadorespuesta.Close();


                    //Nuevos Campos
                    if (validareader("Fecha_Pendiente", "CasoId", respuestastring3).Text != "00-00-0000")
                    {
                        Fecha_Pendiente.Text = validareader("Fecha_Pendiente", "CasoId", respuestastring3).Text;

                    }
                    ID_Seguimiento.Text = validareader("ID_Seguimiento", "CasoId", respuestastring3).Text;
                    // Nuevos Campos
                    //Traemos las Fechas

                    if (validareader("CasoFechaAlta", "CasoId", respuestastring3).Text != "00-00-0000")
                    {
                        tbInstitucion.Text = validareader("CasoFechaAlta", "CasoId", respuestastring3).Text;
                    }
                    if (validareader("CasoFechaFilingCliente", "CasoId", respuestastring3).Text != "00-00-0000")
                    {
                        tbClientduedate.Text = validareader("CasoFechaFilingCliente", "CasoId", respuestastring3).Text;
                    }
                    if (validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text != "00-00-0000")
                    {
                        tbFechapresentacion.Text = validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text;
                    }
                    if (validareader("CasoFechaResolucion", "CasoId", respuestastring3).Text != "00-00-0000")
                    {
                        tbFecharesolucion.Text = validareader("CasoFechaResolucion", "CasoId", respuestastring3).Text;
                    }


                    //Llenamos campos del Frame
                    tbExpediente.Text = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                    tbActor.Text = validareader("Actor", "CasoId", respuestastring3).Text;
                    tbDemandado.Text = validareader("Demandado", "CasoId", respuestastring3).Text;
                    tbAutoridadDemandada.Text = validareader("AutoridadDemandada", "CasoId", respuestastring3).Text;
                    tbTerceroInteresado.Text = validareader("TerceroInteresado", "CasoId", respuestastring3).Text;
                    tbInstancia.Text = validareader("Instancia", "CasoId", respuestastring3).Text;
                    tbExpControvertido.Text = validareader("ExpControvertido", "CasoId", respuestastring3).Text;
                    tbClase.Text = validareader("Clase", "CasoId", respuestastring3).Text;

                    tbPendientesread.Text = validareader("Pendiente", "CasoId", respuestastring3).Text;
                    tbPendienteswrite.Text = validareader("Pendiente", "CasoId", respuestastring3).Text;


                    //Seccion Datos Generales
                    rtTitulo.Text = validareader("CasoTituloespanol", "CasoId", respuestastring3).Text;
                    rtTituloidionaoriginal.Text = validareader("CasoTituloIngles", "CasoId", respuestastring3).Text;
                    tbEncargadoimpi.Text = validareader("CasoEncargadoExterno", "CasoId", respuestastring3).Text;
                    rtbMotivo.Text = validareader("CasoMotivo", "CasoId", respuestastring3).Text;
                    rtbObservaciones.Text = validareader("CasoObservaciones", "CasoId", respuestastring3).Text;

                    //Idiomas de Cartas
                    agregamosidiomas(validareader("IdiomaId", "CasoId", respuestastring3).Text);

                    //Desconocidos
                    tbParte.Text = validareader("ParteRepresentadaId", "CasoId", respuestastring3).Text;
                    tbSentido.Text = validareader("SentidoResolucionId", "CasoId", respuestastring3).Text;


                    //Estatus caso
                    conect conexestatuscaso = new conect();
                    String sEstatuscasoid = validareader("EstatusCasoId", "CasoId", respuestastring3).Text;
                    if (sEstatuscasoid != "")
                    {
                        MySqlDataReader respuestaEstatus = conexestatuscaso.getdatareader("select * from estatuscaso where EstatusCasoId = " + sEstatuscasoid);
                        while (respuestaEstatus.Read())
                        {
                            tbEstatusHeader.Text = validareader("EstatusCasoDescrip", "EstatusCasoId", respuestaEstatus).Text;
                        }
                        respuestaEstatus.Close();
                        conexestatuscaso.Cerrarconexion();
                    }



                    //Segumiiento
                    String sSeguimiento = validareader("ID_Seguimiento", "CasoId", respuestastring3).Text;
                    if (sSeguimiento != "")
                    {
                        conect con_subtiposolicitud = new conect();
                        String sQuerysubtiposolicitud = "select * from C_seguimiento where ID_Seguimiento = " + sSeguimiento;
                        MySqlDataReader respuestasubTiposol = con_subtiposolicitud.getdatareader(sQuerysubtiposolicitud);
                        while (respuestasubTiposol.Read())
                        {
                            ID_Seguimiento.Text = validareader("Nombre", "ID_Seguimiento", respuestasubTiposol).Text;
                        }
                        respuestasubTiposol.Close();
                        con_subtiposolicitud.Cerrarconexion();
                    }
                    ID_Seguimiento.Items.Clear();
                    conect con_subtiposolicitud2 = new conect();
                    String query222 = "select * from C_seguimiento";
                    MySqlDataReader respuestastringtdm23 = con_subtiposolicitud2.getdatareader(query222);
                    while (respuestastringtdm23.Read())
                    {
                        ID_Seguimiento.Items.Add(validareader("Nombre", "ID_Seguimiento", respuestastringtdm23));
                    }
                    respuestastringtdm23.Close();
                    con_subtiposolicitud2.Cerrarconexion();







                    //Tipo solicitud ID
                    conect conextiposol = new conect();
                    String sTipoSolicitudId = validareader("TipoSolicitudId", "CasoId", respuestastring3).Text;
                    if (sTipoSolicitudId != "")
                    {
                        MySqlDataReader respuestaTiposolic = conextiposol.getdatareader("select * from tiposolicitud where TipoSolicitudId = " + sTipoSolicitudId);
                        while (respuestaTiposolic.Read())
                        {
                            tbTipo.Text = validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestaTiposolic).Text;
                        }
                        respuestaTiposolic.Close();
                        conextiposol.Cerrarconexion();
                    }


                    //Traemos el Cliente
                    conect conexcostumer = new conect();
                    String sClienteid = "";
                    String respuestaCliente = ("Select * from casocliente, cliente where casocliente.CasoId = "
                                                                                    + validareader("CasoId", "CasoId", respuestastring3).Text
                                                                                    + " and TipoSolicitudId ="
                                                                                    + validareader("TipoSolicitudId", "CasoId", respuestastring3).Text
                                                                                    + " and cliente.clienteid =  casocliente.clienteid;");
                    MySqlDataReader respuestaClientes = conexcostumer.getdatareader(respuestaCliente);
                    while (respuestaClientes.Read())
                    {
                        //lCliente.Text = validareader("NombreUtilClient", "ClienteId", respuestaCliente).Text;
                        tbClienteHeader.Text = validareader("NombreUtilClient", "ClienteId", respuestaClientes).Text;
                        sClienteid = validareader("ClienteId", "ClienteId", respuestaClientes).Text;
                        gSContactoid = validareader("ContactoId", "ContactoId", respuestaClientes).Text;

                        //20220303 Fsalazar mandamos el numero de cliente
                        gSclienteid = sClienteid;
                        //20220303 Fsalazar Fin de modificación
                    }
                    respuestaClientes.Close();
                    conexcostumer.Cerrarconexion();
                    //consultamos al contacto del cliente


                    //Traemos la Referencia
                    conect conexreference = new conect();
                    String sReferencia = validareader("CasoId", "CasoId", respuestastring3).Text;
                    if (sReferencia != "")
                    {
                        MySqlDataReader respuestaReferencia = conexreference.getdatareader("select * from referencia where CasoId = " + sReferencia);
                        while (respuestaReferencia.Read())
                        {
                            //lReferencia.Text = validareader("ReferenciaNombre", "referenciaid", respuestaReferencia).Text;
                            tbReferenciaHeader.Text = validareader("ReferenciaNombre", "referenciaid", respuestaReferencia).Text;
                        }
                        respuestaReferencia.Close();
                        conexreference.Cerrarconexion();
                    }



                    //Traemos la Descripción del Pais
                    conect conexcountry = new conect();
                    String sIdpais = validareader("PaisId", "CasoId", respuestastring3).Text;
                    if (sIdpais != "")
                    {
                        MySqlDataReader respuestaPais = conexcountry.getdatareader("select * from pais where PaisId = " + sIdpais);
                        while (respuestaPais.Read())
                        {
                            //lPais.Text = validareader("PaisNombre", "PaisId", respuestaPais).Text;
                            tbPaisHeader.Text = validareader("PaisNombre", "PaisId", respuestaPais).Text;
                        }
                        respuestaPais.Close();
                        conexcountry.Cerrarconexion();
                    }


                    //Traemos el Responsable
                    conect conexresponsable = new conect();
                    String idUsuario = validareader("UsuarioId", "CasoId", respuestastring3).Text;
                    if (idUsuario != "")
                    {
                        MySqlDataReader respuestaUser = conexresponsable.getdatareader("select * from usuario where UsuarioId = " + idUsuario);
                        while (respuestaUser.Read())
                        {
                            //lResponsable.Text = validareader("UsuarioNombre", "UsuarioId", respuestaUser).Text;
                            tbResponsableHeader.Text = validareader("UsuarioNombre", "UsuarioId", respuestaUser).Text;
                        }
                        respuestaUser.Close();
                        conexresponsable.Cerrarconexion();
                    }

                    ///








                    // consultamos la marca logo
                    try
                    {
                        configuracionfiles objfile = new configuracionfiles();
                        objfile.configuracionfilesinicio();
                        String sFileexist = objfile.sFileupload + @"\logos_marcas\0" + sCasoId + @"\0" + sCasoId + ".gif";
                        if (File.Exists(sFileexist))
                        {
                            //aqui buscamos el logo si existe en la carpeta y lo insertamos
                            //y preguntamos si ya existe en la base para agregarlo
                            int icount = 0;

                            conect con_imglogo = new conect();
                            String simglogo = "select count(*) As num from imagen_logo where casoid = " + sCasoId + "  " + ";";
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
                                /*
                                  String sDatetime = DateTime.Now.ToString("ddMMyyyyHHmmss").ToString();

                                  String sRutaInsert = objfile.sFileupload + @"\logos_marcas\0" + sCasoId + @"\0" + sCasoId + "_" + sDatetime + ".gif";
                                  System.IO.File.Copy(sFileexist, sRutaInsert, true);
                                  System.IO.File.Delete(sFileexist);
                                  conect con_insert_imglogo = new conect();
                                  String simglogo_insert = "INSERT INTO `imagen_logo`(`RutaOpo`,`casoid`,`TipoSolicitudId`,`fecha`)VALUES('" + sRutaInsert.Replace("\\", "\\\\") + "','" + sCasoId + "','" + gSTipoSolicitudId + "',now());" + ";";
                                  MySqlDataReader resp_insert_imglogo = con_insert_imglogo.getdatareader(simglogo_insert);
                                  if (resp_insert_imglogo.RecordsAffected > 0)
                                  {//quiere decir que hicimos el insert correctamente
                                      obj = Image.FromFile(sRutaInsert);
                                      pbDimage.Image = obj;
                                  }
                                  resp_insert_imglogo.Close();
                                  con_insert_imglogo.Cerrarconexion();*/
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
                    }
                    catch (Exception exs)
                    {
                        new filelog("Al cargar logo", " :" + exs.Message);
                    }

                    ////////
                    //Tipo solicitud ID
                    conect conexsubtiposol = new conect();
                    String sSubTipoSolicitudId = validareader("TipoContenciosoId", "CasoId", respuestastring3).Text;
                    if (sSubTipoSolicitudId != "")
                    {
                        MySqlDataReader respuestasubTiposol = conexsubtiposol.getdatareader("select * from tipocontencioso where TipoContenciosoId = " + sSubTipoSolicitudId);
                        while (respuestasubTiposol.Read())
                        {
                            tbAcciones.Text = validareader("DecripLitigioEsp", "TipoContenciosoId", respuestasubTiposol).Text;
                        }
                        respuestasubTiposol.Close();
                        conexsubtiposol.Cerrarconexion();
                    }


                    //Traemos el Contacto
                    conect conexcontacto = new conect();
                    if (gSContactoid != "")
                    {
                        MySqlDataReader respuestaContacto = conexcontacto.getdatareader("select * from contacto where ContactoId = " + gSContactoid);
                        while (respuestaContacto.Read())
                        {
                            //lContacto.Text = validareader("ContactoNombre", "ContactoId", respuestaContacto).Text;
                            tbContactoHeader.Text = validareader("ContactoNombre", "ContactoId", respuestaContacto).Text;
                            rtCorreocontacto.Text = validareader("ContactoEmail", "ContactoId", respuestaContacto).Text;
                            //20220303 Fsalazar mandamos el numero de cliente
                            gSContactoid = validareader("ContactoId", "ContactoId", respuestaContacto).Text;
                            //20220303 Fsalazar Fin de modificación
                        }
                        respuestaContacto.Close();
                        conexcontacto.Cerrarconexion();
                    }


                    //20220311FSV Agregamos los datos del corresponsal
                    conect conexcorresponsal = new conect();
                    //20220603FSV Cambiamos la consulta
                    //String sQryCorresponsal = "SELECT caso_contencioso.CasoId, " +
                    //                            "cliente.NombreUtilClient AS ClienteCorresponsal,   " +
                    //                            "DAMEALCONTACTOCASO(casocorresponsal.ContactoId) AS ContactoCorresponsal,   " +
                    //                            "cliente.ClienteEmail AS CorreoCorresponsal " +
                    //                            "FROM caso_contencioso, casocorresponsal, cliente " +
                    //                            "WHERE caso_contencioso.CasoId = casocorresponsal.CasoId " +
                    //                            "AND casocorresponsal.ClienteId = cliente.ClienteId " +
                    //                            "AND casocorresponsal.CasoId  = " + sCasoidgenera +
                    //                            " AND casocorresponsal.TipoSolicitudId  = '" + gSTipoSolicitudId + "';";
                    String sQryCorresponsal = "SELECT caco.CasoId, " +
                                                "cli.NombreUtilClient as ClienteCorresponsal,   " +
                                                "cont.ContactoNombre as ContactoCorresponsal,   " +
                                                "cont.ContactoEmail as CorreoCorresponsal " +
                                                "FROM casocorresponsal caco " +
                                                "LEFT OUTER JOIN cliente cli ON caco.ClienteId = cli.ClienteId " +
                                                "LEFT OUTER JOIN contacto cont ON caco.ContactoId = cont.Contactoid " +
                                                "WHERE caco.CasoId  = " + sCasoidgenera +
                                                " AND caco.TipoSolicitudId = '" + gSTipoSolicitudId + "';";
                    //20220603FSV Fin de Modificación
                    MySqlDataReader respuestaCorresponsal = conexcorresponsal.getdatareader(sQryCorresponsal);
                    while (respuestaCorresponsal.Read())
                    {
                        tbCorresponsalHeader.Text = validareader("ClienteCorresponsal", "CasoId", respuestaCorresponsal).Text;
                        tbContactoCorresponsalHeader.Text = validareader("ContactoCorresponsal", "CasoId", respuestaCorresponsal).Text;
                        richTextBox4.Text = validareader("CorreoCorresponsal", "CasoId", respuestaCorresponsal).Text;
                    }
                    respuestaCorresponsal.Close();
                    conexcorresponsal.Cerrarconexion();
                    //20220311FSV Fin de Modificación



                    //20220311FSV Agregamos el titular o interesado
                    conect conextitular = new conect();
                    String sQryTitular = "SELECT caso_contencioso.CasoId, " +
                                                "interesado.NombreUtilInt AS InteresadoNombre   " +
                                                "FROM caso_contencioso, casointeresado, interesado " +
                                                "WHERE caso_contencioso.CasoId = casointeresado.CasoId " +
                                                "AND casointeresado.InteresadoId = interesado.InteresadoId " +
                                                "AND casointeresado.CasoId  = " + sCasoidgenera +
                                                " AND casointeresado.TipoSolicitudId  = '" + gSTipoSolicitudId + "';";
                    MySqlDataReader respuestaTitular = conextitular.getdatareader(sQryTitular);
                    while (respuestaTitular.Read())
                    {
                        tbTitularHeader.Text = validareader("InteresadoNombre", "CasoId", respuestaTitular).Text;
                    }
                    respuestaTitular.Close();
                    conextitular.Cerrarconexion();
                    //20220311FSV Fin de Titular o interesado


                    //Consultamos los idiomas
                    try
                    {
                        conect con_idiomas2 = new conect();
                        String sIdiomas2 = "select * from idioma";
                        MySqlDataReader resp_idioma2 = con_idiomas2.getdatareader(sIdiomas2);
                        while (resp_idioma2.Read())
                        {
                            ComboboxItem prueba = objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2);
                            cbIdioma.Items.Add(objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2));//consultar idioma  
                            //20220602FSV Agregamos el idioma del escrito
                            cbidiomaescrito.Items.Add(objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2));//consultar idioma

                        }
                        resp_idioma2.Close();
                        con_idiomas2.Cerrarconexion();

                        String sIdiomaidcaso = objfuncionesdicss.validareader("IdiomaId", "CasoId", respuestastring3).Text;

                        if (sIdiomaidcaso != "")
                        {
                            conect con_idioma = new conect();
                            String sIdioma = "select * from idioma where IdiomaId = " + sIdiomaidcaso;
                            MySqlDataReader resp_idioma = con_idioma.getdatareader(sIdioma);
                            String sIdiomadelcaso = "";
                            while (resp_idioma.Read())
                            {
                                sIdiomadelcaso = objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma).Text;
                            }
                            cbIdioma.Text = sIdiomadelcaso;

                            //20220602FSV Idioma del caso para los escritos
                            cbidiomaescrito.Text = sIdiomadelcaso;
                            //20220602FSV Fin de modificacion

                            resp_idioma.Close();
                            con_idioma.Cerrarconexion();
                        }
                    }
                    catch (Exception ExsIdioma)
                    {
                        new filelog("Consultamos idiomas linea 498", ExsIdioma.StackTrace.ToString());
                    }
                    //Fin de Idiomas



                    //20220328FSV Llenamos la seccion 
                    carga_documentos_IMPI(sCasoId, gSTipoSolicitudId);
                    //20220328FSV


                    //consultamos los plazos
                    consultaplazo_nuevos();



                    //20220406 Carga Facturas+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    try
                    {//Consultamos las facturas
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
                    catch (Exception Exsfac)
                    {
                        new filelog("conulta facturas linea 444", Exsfac.StackTrace.ToString());
                    }
                    //20220406FSV Fin Carga Facturas++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



                    icontgeneradom++;

                }

                conexcontencioso.Cerrarconexion();
                respuestastring3.Close();

                this.Enabled = true;

                consultamosdocumentoselectronicos();
                consultacartasyescritos(3, cbCartas);//cartas
                consultacartasyescritos(2, cbEscritos);//escritos
                                                       //cargamos los oficios que estan dentro de este caso para poder seleccionar uno y tomar su información
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
                                        " 	relaciondocumento.casoid = " + sCasoId +
                                        "     and relaciondocumento.TipoSolicitudId = " + gSTipoSolicitudId +
                                        "     and subtipodocumento.TipoDocumentoId in(1 ,2) " +
                                        "     and relaciondocumento.DocumentoId = documento.DocumentoId " +
                                        "     and documento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId; ";
                MySqlDataReader respuesta_ofiesc = con_ofiesc.getdatareader(sQuery_ofiesc);
                while (respuesta_ofiesc.Read())
                {
                    cbOficiosEscritos.Items.Add(validareader("SubTipoDocumentoDescrip", "DocumentoId", respuesta_ofiesc));
                }
                respuesta_ofiesc.Close();
                con_ofiesc.Cerrarconexion();
                //cbOficiosEscritos

            }
            catch (Exception E)
            {
                MessageBox.Show("No se ecnontraron más casos");
                this.Enabled = true;
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

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        public void carga_documentos_IMPI(string sCasoiddom, string gSTipoSolicitudId)
        {
            try
            {
                dGV_docimentos_IMPI.Rows.Clear();
                //20220328FSV Desactivar esto que no se usa
                //lvdocumentosimpi.Items.Clear();
                //20220328FSV Esto no se usa
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
                    String sMes = validareader("diasfiff", "casoid", resp_docimpi).Text;
                    int iMes = Int32.Parse(sMes) / 30;

                    /*Validamos primero que tipo de documento vamos a mostrar*/
                    /*Puede ser solicitud, Escrito, Oficio, Titulo, Email ...*/
                    ListViewItem items = new ListViewItem("");
                    //dGV_docimentos_IMPI
                    DataGridViewRow dRows = (DataGridViewRow)dGV_docimentos_IMPI.Rows[0].Clone();
                    //DataGridViewRow dRows = (DataGridViewRow)dgPlazos.Rows[0].Clone();


                    switch (validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text)
                    {

                        case "Solicitud":
                            {
                                dRows.Cells[0].Value = objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text;
                                dRows.Cells[1].Value = validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text;
                                dRows.Cells[2].Value = validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text;
                                dRows.Cells[3].Value = validareader("DocumentoFolio", "casoid", resp_docimpi).Text;
                                dRows.Cells[4].Value = "";
                                dRows.Cells[5].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text;
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

                            }
                            break;
                        case "Escrito":
                            {
                                dRows.Cells[0].Value = objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text;
                                dRows.Cells[1].Value = objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text;
                                dRows.Cells[2].Value = objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text;
                                dRows.Cells[3].Value = objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text;
                                dRows.Cells[4].Value = "";
                                dRows.Cells[5].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text;
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
                                String sSubtipodocumentoid = objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi).Text;
                                if (objfuncionesdicss.validareader("SubTipoDocumentoId", "casoid", resp_docimpi).Text == "115")//Validamos que no sea cita a pago
                                {
                                    //20220328FSV Este objeto no existe
                                    //tbFechacitaapago.Text = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                                    //20220328FSV Este objeto no existe
                                }
                                dRows.Cells[0].Value = objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text;
                                dRows.Cells[1].Value = objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text;
                                dRows.Cells[2].Value = objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text;
                                dRows.Cells[3].Value = objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text;
                                dRows.Cells[4].Value = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[5].Value = sFechavigencia_;
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
                                dRows.Cells[0].Value = objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text;
                                dRows.Cells[1].Value = objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text;
                                dRows.Cells[2].Value = objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text;
                                dRows.Cells[3].Value = objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text;
                                dRows.Cells[4].Value = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[5].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[6].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento4meses", "casoid", resp_docimpi, true).Text;
                                dRows.Cells[7].Value = "" + iMes;
                                dRows.Cells[8].Value = "";
                                dRows.Cells[9].Value = "";
                                dRows.Cells[10].Value = objfuncionesdicss.validareader("subtipodocumento", "casoid", resp_docimpi).Text;
                                dRows.Cells[11].Value = "";
                                dRows.Cells[12].Value = objfuncionesdicss.validareader("DocumentoObservacion", "casoid", resp_docimpi).Text;
                                dRows.Cells[13].Value = "";
                                dRows.Cells[14].Value = "";
                                dRows.Cells[15].Value = "";
                                dRows.Cells[16].Value = objfuncionesdicss.validareader("DocumentoFechaRecepcion", "casoid", resp_docimpi, true).Text;
                            }
                            break;
                        case "E-MAIL":
                            {
                                dRows.Cells[0].Value = objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text;
                                dRows.Cells[1].Value = objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text;
                                dRows.Cells[2].Value = objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text;
                                dRows.Cells[3].Value = objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text;
                                dRows.Cells[4].Value = "";
                                dRows.Cells[5].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text;
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
                                dRows.Cells[0].Value = objfuncionesdicss.validareader("RelacionDocumentoLink", "casoid", resp_docimpi).Text;
                                dRows.Cells[1].Value = objfuncionesdicss.validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text;
                                dRows.Cells[2].Value = objfuncionesdicss.validareader("DocumentoCodigoBarras", "casoid", resp_docimpi).Text;
                                dRows.Cells[3].Value = objfuncionesdicss.validareader("DocumentoFolio", "casoid", resp_docimpi).Text;
                                dRows.Cells[4].Value = "";
                                dRows.Cells[5].Value = objfuncionesdicss.validareader("DocumentoFechaVencimiento", "casoid", resp_docimpi, true).Text;
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
                                items.SubItems.Add(validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text);//tipo
                                dRows.Cells[0].Value = "Tipo de documento no considerado";
                                dRows.Cells[1].Value = validareader("TipoDocumentoDescrip", "casoid", resp_docimpi).Text;
                            }
                            break;


                    }/*por ahora sólo consideraremos 5 tipos de documentos mencionados arriba*/
                    String sDocumentosid = validareader("Documentoid", "Documentoid", resp_docimpi).Text;
                    dRows.Cells[19].Value = sDocumentosid;

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
                    icontardocumentototales++;
                    //lvdocumentosimpi.Items.Add(items);
                }
                resp_docimpi.Close();
                con2.Cerrarconexion();
                textBox19.Text = "" + icontardocumentototales;
            }
            catch (Exception Ex)
            {
                new filelog("cargando documentos IMPI patentes", "linea 2268:" + Ex.Message);
            }



        }


        public void upcatescampos()
        {

            try
            {


                String sUpdateset = "";
                String sUpdatesetI = "";

                //if (rtTitulo_update || btituloidiomaoriginal_update || bExpediente_update )
                //if (bTitulo_update || btituloidiomaoriginal_update || bEncargadoExterno_update || bMotivo_update || bObservaciones_update || bClienteduedate_update || bFechapresentacion_update || bFechaResolucion_update || bExpediente_update || bActor_update || bDemandado_update || bAutoridadDemandada_update || bTerceroInteresado_update || bInstancia_update || bExpControvertido_update || bClase_update)
                if (true)
                {


                    //1.- Filtros de Fechas
                    if (bClienteduedate_update)
                    {
                        sUpdateset += ", CasoFechaFilingCliente = STR_TO_DATE('" + tbClientduedate.Text + "', '%d-%m-%Y')";
                    }
                    if (bFechapresentacion_update)
                    {
                        sUpdateset += ", CasoFechaRecepcion =  STR_TO_DATE('" + tbFechapresentacion.Text + "', '%d-%m-%Y')";
                    }
                    if (bFechaResolucion_update)
                    {
                        sUpdateset += ", CasoFechaResolucion = STR_TO_DATE('" + tbFecharesolucion.Text + "', '%d-%m-%Y')";
                    }


                    //2.- Campos NUevos
                    if (bExpediente_update)
                    {
                        sUpdateset += ", CasoNumeroExpedienteLargo = '" + tbExpediente.Text + "'";
                    }


                    if (tbinteresado_update)
                    {
                        sUpdatesetI += ", InteresadoNombre = '" + tbTitularHeader.Text + "'";
                    }
                    if (bActor_update)
                    {
                        //sUpdateset += ", Actor = '" + tbActor.Text + "'";
                        string sCampoActor = tbActor.Text;
                        sCampoActor = sCampoActor.Replace("'", "''");
                        sUpdateset += ", Actor = '" + sCampoActor + "'";
                        sCampoActor = "";
                    }
                    if (bDemandado_update)
                    {
                        //sUpdateset += ", Demandado = '" + tbDemandado.Text + "'";
                        string sCampoDemandado = tbDemandado.Text;
                        sCampoDemandado = sCampoDemandado.Replace("'", "''");
                        sUpdateset += ", Demandado = '" + sCampoDemandado + "'";
                        sCampoDemandado = "";
                    }
                    if (bAutoridadDemandada_update)
                    {
                        //sUpdateset += ", AutoridadDemandada = '" + tbAutoridadDemandada.Text + "'";
                        string sCampoAutoridad = tbAutoridadDemandada.Text;
                        sCampoAutoridad = sCampoAutoridad.Replace("'", "''");
                        sUpdateset += ", AutoridadDemandada = '" + sCampoAutoridad + "'";
                        sCampoAutoridad = "";
                    }
                    if (bTerceroInteresado_update)
                    {
                        //sUpdateset += ", TerceroInteresado = '" + tbTerceroInteresado.Text + "'";
                        string sCampoTercero = tbTerceroInteresado.Text;
                        sCampoTercero = sCampoTercero.Replace("'", "''");
                        sUpdateset += ", TerceroInteresado = '" + sCampoTercero + "'";
                        sCampoTercero = "";
                    }
                    if (bInstancia_update)
                    {
                        //sUpdateset += ", Instancia = '" + tbInstancia.Text + "'";
                        string sCampoInstancia = tbInstancia.Text;
                        sCampoInstancia = sCampoInstancia.Replace("'", "''");
                        sUpdateset += ", Instancia = '" + sCampoInstancia + "'";
                        sCampoInstancia = "";
                    }
                    if (bExpControvertido_update)
                    {
                        //sUpdateset += ", ExpControvertido = '" + tbExpControvertido.Text + "'";
                        string sCampoExpCon = tbExpControvertido.Text;
                        sCampoExpCon = sCampoExpCon.Replace("'", "''");
                        sUpdateset += ", ExpControvertido = '" + sCampoExpCon + "'";
                        sCampoExpCon = "";
                    }
                    if (bClase_update)
                    {
                        sUpdateset += ", Clase = '" + tbClase.Text + "'";
                    }

                    //3.- Seccion Datos Generales
                    if (btituloidiomaoriginal_update)
                    {
                        sUpdateset += ", CasoTituloingles = '" + rtTituloidionaoriginal.Text + "'";
                    }
                    if (bEncargadoExterno_update)
                    {
                        sUpdateset += ", CasoEncargadoExterno = '" + tbEncargadoimpi.Text + "'";
                    }
                    if (bMotivo_update)
                    {
                        sUpdateset += ", casomotivo = '" + rtbMotivo.Text + "'";
                    }
                    if (bObservaciones_update)
                    {
                        sUpdateset += ", CasoObservaciones = '" + rtbObservaciones.Text + "'";
                    }
                    if (seguimiento_update)
                    {
                        if (ID_Seguimiento.Text == "Vigilando individual")
                        {

                            sUpdateset += ", ID_Seguimiento = '" + "1" + "'";
                        }
                        if (ID_Seguimiento.Text == "No Seguir individual")
                        {

                            sUpdateset += ", ID_Seguimiento = '" + "2" + "'";
                        }
                        if (ID_Seguimiento.Text == "No Seguir x Cliente")
                        {

                            sUpdateset += ", ID_Seguimiento = '" + "3" + "'";
                        }
                        if (ID_Seguimiento.Text == "No Seguir x interesado")
                        {

                            sUpdateset += ", ID_Seguimiento = '" + "4" + "'";
                        }

                    }

                    sUpdateset += ", Pendiente = '" + tbPendienteswrite.Text.Trim() + "'";


                    /*Campos nuevos 2022-07-27*/
                    if (Fecha_Pendiente_update)
                    {
                        conect conexF_Pendiente = new conect();
                        String ssQuery = "SELECT  DATE_FORMAT(Fecha_Pendiente , '%d-%m-%Y') as  Fecha_Pendiente,CasoId" +

                                        " FROM    caso_contencioso " +

                                        "WHERE   caso_contencioso.CasoId  = '" + sCasoId + "'";

                        MySqlDataReader respuestafecha = conexF_Pendiente.getdatareader(ssQuery);

                        while (respuestafecha.Read())
                        {

                            Fecha = validareader("Fecha_Pendiente", "CasoId", respuestafecha).Text;//UsuarioCodigo

                            if (Fecha == "")
                            {

                                sUpdateset += ", Fecha_Pendiente = STR_TO_DATE('" + Fecha_Pendiente.Text + "', '%d-%m-%Y')";

                            }
                            if (Fecha == null)
                            {



                            }
                            if (Fecha != "" || Fecha != "")
                            {
                                if (loguin.sUsuarioIndAdmin == "1")
                                {
                                    sUpdateset += ", Fecha_Pendiente = STR_TO_DATE('" + Fecha_Pendiente.Text + "', '%d-%m-%Y')";
                                }
                                else
                                {
                                    MessageBox.Show("No puede realizar el cambio no eres administrador");
                                }
                            }

                        }
                    }


                    /*2022-07-27*/
                    /*   conect cons = new conect();

                       String sQryTitular = "SELECT InteresadoID, " +

                                                  "FROM interesado " +

                                                  " where InteresadoNombre= '" + tbExpediente.Text + "';";
                       MySqlDataReader respuestaTitular = cons.getdatareader(sQryTitular);


                       String sInteresadoId = validareader("InteresadoId", "InteresadoId", respuestaTitular).Text;
                       String sInteresado = sInteresadoId;
                       /////

                       String sidInteresado = "UPDATE `casointeresado` SET `InteresadoId` = '" + sInteresado + "where CasoInteresadoId=" + sCasoId;
                       MySqlDataReader resp_Interesado = cons.getdatareader(sidInteresado);
                       resp_Interesado.Read();


                       resp_Interesado.Read();

                       if (resp_Interesado == null)
                       {
                           MessageBox.Show("Error al hacer el update. " + sidInteresado);
                       }
                       else
                       {
                           MessageBox.Show("Cambios guardados correctamente.");

                           resp_Interesado.Close();
                           cons.Cerrarconexion();
                       }*/
                    conect con = new conect();
                    String sIdspatentes = "UPDATE `caso_contencioso` SET `CasoTituloespanol` = '" + rtTitulo.Text + "' " + sUpdateset +
                        //" WHERE `caso_contencioso`.`CasoId` = " + sCasoId + " AND `caso_contencioso`.`TipoSolicitudId` = " + sTiposolicitudGlobal;
                        " WHERE `caso_contencioso`.`CasoId` = " + sCasoId + " AND `caso_contencioso`.`TipoSolicitudId` = " + gSTipoSolicitudId;


                    MySqlDataReader resp_numpatentes = con.getdatareader(sIdspatentes);
                    resp_numpatentes.Read();

                    if (resp_numpatentes == null)
                    {
                        MessageBox.Show("Error al hacer el update. " + sIdspatentes);
                    }
                    else
                    {
                        MessageBox.Show("Cambios guardados correctamente.");
                    }

                    resp_numpatentes.Close();
                    con.Cerrarconexion();
                }
                else
                {
                    MessageBox.Show("Cambios guardados correctamente.");
                }




            }
            catch (Exception E)
            {
                MessageBox.Show("Error al intentar guardar cambios.");
                new filelog(loguin.sId, "liena 4286: " + E.ToString());
            }
        }



        public void resetvariable()
        {
            //bTituloIngles_update = false;

            bClienteduedate_update = false;
            bFechapresentacion_update = false;
            bFechaResolucion_update = false;

            bExpediente_update = false;
            bActor_update = false;
            bDemandado_update = false;
            bAutoridadDemandada_update = false;
            bTerceroInteresado_update = false;
            bInstancia_update = false;
            bExpControvertido_update = false;
            bClase_update = false;

            bTitulo_update = false;
            btituloidiomaoriginal_update = false;
            bEncargadoExterno_update = false;
            bMotivo_update = false;
            bObservaciones_update = false;

            cbIdioma.Items.Clear();
            cbidiomaescrito.Items.Clear();

        }




        private void button39_Click_1(object sender, EventArgs e)
        {
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
        /* catch (Exception E)
         {
             new filelog(loguin.sId, "linea 4044: " + E.ToString());
         }*/




        private void button38_Click(object sender, EventArgs e)
        {
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

        private void button2_Click(object sender, EventArgs e)
        {
            //20220318FSV Regresar al form de búsqueda
            //consultacaso.Focus();
            consultacaso.Show();
            this.Close();
        }

        private void button28_Click(object sender, EventArgs e)
        {

            //Generamos las Cartas
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
                    objcontenciosoctualview = new view_caso_contencioso(sCasoId, gSTipoSolicitudId, sIdidiomaescritos, sDocumentoid);
                }
                else
                {
                    objcontenciosoctualview = new view_caso_contencioso(sCasoId, gSTipoSolicitudId, sIdidiomaescritos);
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
                        if (sCartanombreENfile == "")
                        {
                            MessageBox.Show("No existe la carta para éste idioma (EN)");
                            return;
                        }
                        generacarta_contencioso objcarta = new generacarta_contencioso(sCartanombreENfile, valuecob, objcontenciosoctualview);
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
                            generacarta_contencioso objcarta = new generacarta_contencioso(sCartanombreESPfile, valuecob, objcontenciosoctualview);
                        }
                        else
                        {
                            MessageBox.Show("Debe asignar un Idioma al cliente");
                        }
                    }
                }
                respuesta_carta.Close();
                con_carta.Cerrarconexion();
            }
            catch (Exception ex)
            {
                new filelog("al generar las cartas en contencioso", "" + ex.Message);
            }
        }

        private void fTcontencioso_FormClosing(object sender, FormClosingEventArgs e)
        {
            consultacaso.Show();
        }

        private void label19_MouseMove(object sender, MouseEventArgs e)
        {
            //label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(93)))), ((int)(((byte)(166)))));
            label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(181)))), ((int)(((byte)(1)))));
        }

        private void label19_MouseLeave(object sender, EventArgs e)
        {
            label19.BackColor = Color.Yellow;
        }

        private void label19_DoubleClick(object sender, EventArgs e)
        {

            //creamos una ventana en la que podamos buscar al cliente y asignarlo al caso
            //20220324FSV Cambiamos etiqueta por cuadro de texto solo lectura
            buscarcliente bForm = new buscarcliente(sCasoId, gSTipoSolicitudId, tbContactoHeader.Text, tbClienteHeader.Text, loguin);
            //20220324FSV Fin de Modificación
            if (bForm.ShowDialog() == DialogResult.OK)
            {
                tbClienteHeader.Text = bForm.sClienteidtext;
                tbContactoHeader.Text = bForm.sContactoidtext;
                gSclienteid = bForm.sClienteidvalue;
                rtCorreocontacto.Text = bForm.rtCorreocontacto_pass;
            }
        }

        private void label21_MouseMove(object sender, MouseEventArgs e)
        {
            label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(181)))), ((int)(((byte)(1)))));
        }

        private void label21_MouseLeave(object sender, EventArgs e)
        {
            label21.BackColor = Color.Yellow;
        }

        private void label21_DoubleClick(object sender, EventArgs e)
        {
            //20220324FSV Cambiamos etiqueta por cuadro de texto solo lectura
            addContacto objnuevocontacto = new addContacto(gSclienteid, tbClienteHeader.Text, gSContactoid, tbContactoHeader.Text, sCasoId, gSTipoSolicitudId);
            //20220324FSV Fin de Modificacion
            if (objnuevocontacto.ShowDialog() == DialogResult.OK)
            {
                //lContacto.Text = objnuevocontacto.SgContactotext;
                tbContactoHeader.Text = objnuevocontacto.SgContactotext;
                rtCorreocontacto.Text = objnuevocontacto.SgContactocorreos;
            }
        }

        private void tbEstatus_TextChanged(object sender, EventArgs e)
        {

        }

        private void label27_MouseMove(object sender, MouseEventArgs e)
        {
            label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(181)))), ((int)(((byte)(1)))));
        }

        private void label27_MouseLeave(object sender, EventArgs e)
        {
            label27.BackColor = Color.Yellow;
        }

        private void label8_MouseMove(object sender, MouseEventArgs e)
        {
            label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(181)))), ((int)(((byte)(1)))));
        }

        private void label8_MouseLeave(object sender, EventArgs e)
        {
            label8.BackColor = Color.Yellow;
        }

        private void label27_DoubleClick(object sender, EventArgs e)
        {

            conect con = new conect();

            updatePais updateestatus = new updatePais();
            if (updateestatus.ShowDialog() == DialogResult.OK)
            {
                String value = updateestatus.sValueestatus;//hacemos el update con este valor y ponemos el Texto en el form
                String texti = updateestatus.sTextoestatus;//hacemos el update con este valor y ponemos el Texto en el form
                conect con_4 = new conect();
                String updateestatuscaso = "UPDATE `caso_contencioso` SET `PaisId` = '" + value + "' WHERE (`CasoId` = '" + sCasoId + "');";
                String sIdPais = value;
                MySqlDataReader resp_updatecaso = con_4.getdatareader(updateestatuscaso);
                if (resp_updatecaso != null)
                {
                    //lPais.Text = texti;
                    tbPaisHeader.Text = texti;
                }
                resp_updatecaso.Close();
                con_4.Cerrarconexion();
            }

        }

        private void label8_DoubleClick(object sender, EventArgs e)
        {
            updateEstatus updateestatus = new updateEstatus("3");
            if (updateestatus.ShowDialog() == DialogResult.OK)
            {
                String value = updateestatus.sValueestatus;//hacemos el update con este valor y ponemos el Texto en el form
                String texti = updateestatus.sTextoestatus;//hacemos el update con este valor y ponemos el Texto en el form
                conect con_4 = new conect();
                String updateestatuscaso = "UPDATE `caso_contencioso` SET `EstatusCasoId` = '" + value + "' WHERE (`CasoId` = '" + sCasoId + "');";
                MySqlDataReader resp_updatecaso = con_4.getdatareader(updateestatuscaso);
                if (resp_updatecaso != null)
                {
                    tbEstatusHeader.Text = texti;
                }
                resp_updatecaso.Close();
                con_4.Cerrarconexion();
            }
        }

        private void label36_MouseLeave(object sender, EventArgs e)
        {
            label36.BackColor = Color.Yellow;
        }

        private void label36_MouseMove(object sender, MouseEventArgs e)
        {
            label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(181)))), ((int)(((byte)(1)))));
        }

        private void label36_DoubleClick(object sender, EventArgs e)
        {
            fResponsableupdate updateResponsable = new fResponsableupdate("1");
            if (updateResponsable.ShowDialog() == DialogResult.OK)
            {
                String value = updateResponsable.sValueResponsable;
                String texti = updateResponsable.sTextoResponsable;
                conect con_4 = new conect();
                String updateresponsableidcaso = "UPDATE `caso_contencioso` SET `ResponsableId` = '" + value + "', `UsuarioId` = '" + value +
                                                "' WHERE (`CasoId` = '" + sCasoId + "' and TipoSolicitudId ='" + gSTipoSolicitudId + "');";
                MySqlDataReader resp_updateResp = con_4.getdatareader(updateresponsableidcaso);
                if (resp_updateResp != null)
                {
                    //lResponsable.Text = texti;
                    tbResponsableHeader.Text = texti;
                }
                resp_updateResp.Close();
                con_4.Cerrarconexion();
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            cb_tipodocelect.Text = "";
            textBox37.Text = "";
            textBox38.Text = "";
        }

        private void button26_Click(object sender, EventArgs e)
        {


            String sLog = "";
            try
            {
                if (cb_tipodocelect.Text.Trim() == "")
                {
                    MessageBox.Show("Debe seleccionar un tipo de documento antes de agregar.");
                    return;
                }

                if (textBox37.Text.Trim() == "")
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

                DialogResult result1 = MessageBox.Show("Se agregará el documento: " + filePath + " \n Tipo:" + cb_tipodocelect.Text + "\n Descripción:" + textBox37.Text, "Confirmación.", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    //crear carpeta de cada caso 
                    //sCarpetadocumentos
                    string textoNormalizado = sNamefile.Normalize(NormalizationForm.FormD);
                    //coincide todo lo que no sean letras y números ascii o espacio
                    //y lo reemplazamos por una cadena vacía.Regex reg = new Regex("[^a-zA-Z0-9 ]");
                    Regex reg = new Regex("[^a-zA-Z0-9 ]");
                    string textoSinAcentos = reg.Replace(textoNormalizado, "");

                    //20220602FSV Quitamos la rebundancia de la carpeta edocs
                    //string ruta = obj.sFileupload + "\\" + sCarpetadocumentos + "\\" + gSCasoNumero + "\\edocs";
                    //string ruta = obj.sFileupload + "\\" + sCarpetadocumentos + "\\" + gSCasoNumero;

                    String[] sfilesss = obj.sFileupload.Split('\\');

                    string ruta = "\\\\" + sfilesss[2] + "\\" + sfilesss[3] + "\\" + sCarpetadocumentos + "\\" + gSCasoNumero;// + "\\edocs";
                    String sfilePath_2 = @ruta + "\\" + gSCasoNumero + " " + tbExpediente.Text.Replace("/", "") + " " + sNamefile;
                    //TextBox38. = sfilePath_2;
                    textBox38.Text = sfilePath_2;
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
                                //" '" + tb_descripdocelec.Text.Trim() + "', " +
                                " '" + textBox37.Text.Trim() + "', " +
                                //" '" + gSCasoId + "', " +
                                " '" + sCasoId + "', " +
                                //" '" + sTiposolicitudGlobal + "', " +
                                " '" + gSTipoSolicitudId + "', " +
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
                            //tb_filename.Text = "";
                            textBox38.Text = "";
                            //tb_descripdocelec.Text = "";
                            textBox37.Text = "";
                            MessageBox.Show("El Documento se agregó correctamente.");
                            consultamosdocumentoselectronicos();
                        }
                        else
                        {
                            MessageBox.Show("Error al intentar agregar el documento,  la ruta ó el nombre del archivo.");
                        }
                    }
                    else
                    {
                        sLog = sQueryinsert;
                        MessageBox.Show("Error al intentar guardar el documento \n Query:" + sQueryinsert);
                        new filelog(loguin.sId, "linea 4010: " + sQueryinsert);
                    }
                    con_insertdocelect.Cerrarconexion();
                }
            }
            catch (Exception E)
            {
                new filelog(loguin.sId, "linea 3854: " + E.ToString() + " query " + sLog);
            }



        }


        private void consultamosdocumentoselectronicos()
        {
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
                dgDocumentoselectronicos.Rows.Clear();
                int count_docelect = 0;
                while (resp_tedocs.Read())
                {
                    String sDocumentoElectronicoId = validareader("DocumentoElectronicoId", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sDocumentoElectronicoDate = validareader("DocumentoElectronicoDate", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sDocumentoElectronicoDescrip = validareader("DocumentoElectronicoDescrip", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sCasoId_doc = validareader("CasoId", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sClienteId = validareader("ClienteId", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sUsuarioId = validareader("UsuarioId", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sTipoDocumentoElectronicoId = validareader("TipoDocumentoElectronicoId", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sDocumentoElectronicoFilename = validareader("DocumentoElectronicoFilename", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sInteresadoId = validareader("InteresadoId", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sTipoDocumentoElectronicoDescrip = validareader("TipoDocumentoElectronicoDescrip", "DocumentoElectronicoId", resp_tedocs).Text;

                    String sUsuarioNombre = validareader("UsuarioNombre", "DocumentoElectronicoId", resp_tedocs).Text;
                    String sUsuarioPaterno = validareader("UsuarioPaterno", "DocumentoElectronicoId", resp_tedocs).Text;

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

                textBox39.Text = "" + count_docelect;
                resp_tedocs.Close();
                con_tcon_edocs.Cerrarconexion();
            }
            catch (Exception Ex)
            {
                new filelog(loguin.sId, "linea 3504:" + Ex.ToString());
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Fatenderplazo obj = new Fatenderplazo(sCasoId, gSTipoSolicitudId, loguin, gSCasoNumero);
            obj.ShowDialog();
            generadom(sCasoId);
        }

        private void button44_Click(object sender, EventArgs e)
        {
            //CapturaSolicitud addescrito = new CapturaSolicitud(fCapuraform, loguin, "1", sCasoId);
            CapturaSolicitud addescrito = new CapturaSolicitud(fCapuraform, loguin, "3", sCasoId);
            if (addescrito.ShowDialog() == DialogResult.OK)
            {
                generadom(sCasoId);
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            //CapturaEscritos addescrito = new CapturaEscritos(fCapuraform, loguin, "1", sCasoId);
            CapturaEscritos addescrito = new CapturaEscritos(fCapuraform, loguin, "3", sCasoId);
            if (addescrito.ShowDialog() == DialogResult.OK)
            {
                generadom(sCasoId);
            }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            try
            {
                CapturadeOficios addoficio = new CapturadeOficios(fCapuraform, loguin, "3", sCasoId, tbFecharesolucion.Text);
                if (addoficio.ShowDialog() == DialogResult.OK)
                {
                    generadom(sCasoId);
                }
            }
            catch (Exception ex)
            {
                new filelog("linea: 4212", "error:" + ex.Message);
            }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            //Capturadetitulo addtitulo = new Capturadetitulo(fCapuraform, loguin, "1", sCasoId);
            Capturadetitulo addtitulo = new Capturadetitulo(fCapuraform, loguin, "3", sCasoId);
            if (addtitulo.ShowDialog() == DialogResult.OK)
            {
                generadom(sCasoId);
            }

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void tbExpediente_ModifiedChanged(object sender, EventArgs e)
        {
            bExpediente_update = true;
        }

        private void rtTitulo_ModifiedChanged(object sender, EventArgs e)
        {
            bTitulo_update = true;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string message = "Se actualizarán los datos del Caso Contencioso ¿Desea Continuar?";
            string caption = "Consulta Caso Contencioso";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                upcatescampos();
                resetvariable();
            }
        }

        private void tbFechapresentacion_ModifiedChanged(object sender, EventArgs e)
        {

        }

        private void tbFechapresentacion_Validating(object sender, CancelEventArgs e)
        {

        }

        private void tbFechapresentacion_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbClientduedate_ModifiedChanged(object sender, EventArgs e)
        {
            bClienteduedate_update = true;
        }

        private void tbClientduedate_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbClientduedate);
        }

        private void tbClientduedate_KeyPress(object sender, KeyPressEventArgs e)
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






        private void tbFecharesolucion_KeyPress(object sender, KeyPressEventArgs e)
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
            if (tbFecharesolucion.Text.Length == 2)
            {
                tbFecharesolucion.Text = tbFecharesolucion.Text + "-";
                tbFecharesolucion.SelectionStart = tbFecharesolucion.Text.Length;
            }
            if (tbFecharesolucion.Text.Length == 5)
            {
                tbFecharesolucion.Text = tbFecharesolucion.Text + "-";
                tbFecharesolucion.SelectionStart = tbFecharesolucion.Text.Length;
            }
        }

        private void Fecha_Pendiente_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(Fecha_Pendiente);
        }

        private void tbFecharesolucion_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFecharesolucion);
        }

        private void rtTituloidionaoriginal_ModifiedChanged(object sender, EventArgs e)
        {
            btituloidiomaoriginal_update = true;
        }

        private void tbEncargadoimpi_ModifiedChanged(object sender, EventArgs e)
        {
            bEncargadoExterno_update = true;
        }

        private void rtbObservaciones_ModifiedChanged(object sender, EventArgs e)
        {
            bObservaciones_update = true;
        }

        private void seguimiento_ModifiedChanged(object sender, EventArgs e)
        {
            seguimiento_update = true;
        }
        private void tbFecharesolucion_ModifiedChanged(object sender, EventArgs e)
        {
            bFechaResolucion_update = true;
        }

        private void tbinteresado(object sender, EventArgs e)
        {
            tbinteresado_update = true;
        }

        private void tbFechapresentacion_KeyPress_1(object sender, KeyPressEventArgs e)
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

            if (tbFechapresentacion.Text.Length == 2)
            {
                //20200601FSV Corrección en formato de fecha
                //tbFechapresentacion.Text = tbClientduedate.Text + "-";
                tbFechapresentacion.Text = tbFechapresentacion.Text + "-";
                //20220601FSV Fin de corrección
                tbFechapresentacion.SelectionStart = tbFechapresentacion.Text.Length;
            }
            if (tbFechapresentacion.Text.Length == 5)
            {
                tbFechapresentacion.Text = tbFechapresentacion.Text + "-";
                tbFechapresentacion.SelectionStart = tbFechapresentacion.Text.Length;
            }
        }
        private void Fecha_Pendiente_ModifiedChanged_1(object sender, EventArgs e)
        {
            Fecha_Pendiente_update = true;
        }

        private void tbFechapresentacion_ModifiedChanged_1(object sender, EventArgs e)
        {
            bFechapresentacion_update = true;
        }

        private void tbFechapresentacion_Validating_1(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechapresentacion);
        }

        private void label29_MouseLeave(object sender, EventArgs e)
        {
            label29.BackColor = Color.Yellow;
        }

        private void label29_MouseMove(object sender, MouseEventArgs e)
        {
            label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(181)))), ((int)(((byte)(1)))));
        }

        private void label29_DoubleClick(object sender, EventArgs e)
        {
            fTitularupdateCO actualizarTitular = new fTitularupdateCO("1");
            if (actualizarTitular.ShowDialog() == DialogResult.OK)
            {
                String value = actualizarTitular.sValueTitular;
                String texti = actualizarTitular.sTextoTitular;
                conect con_4 = new conect();
                //String actualizarTitularidcaso = "UPDATE `caso_consulta` SET `ResponsableId` = '" + value + "', `UsuarioId` = '" + value +
                //                                "' WHERE (`CasoId` = '" + sCasoId + "' and TipoSolicitudId ='" + gSTipoSolicitudId + "');";
                string query = "SELECT CasoInteresadoId,CasoId FROM casointeresado WHERE CasoId= '" + sCasoId + "' and TipoSolicitudId ='" + gSTipoSolicitudId + "';";
                MySqlDataReader respuestastringid = con_4.getdatareader(query);
                String stt = "";
                while (respuestastringid.Read())
                {

                    stt = validareader("CasoInteresadoId", "CasoId", respuestastringid).Value.ToString();


                }
                respuestastringid.Close();
                if (stt == "")
                {
                    String insertar = "insert into casointeresado (CasoInteresadoId,InteresadoId,CasoId,TipoSolicitudId,CasoInteresadoSecuencia,TipoRelacionId,DireccionId) values(null, '" + value + "'," +
" '" + sCasoId + "', '" + gSTipoSolicitudId + "', '1','1','0');";
                    MySqlDataReader insertarcaso = con_4.getdatareader(insertar);
                }


                String actualizarTitularidcaso = "UPDATE casointeresado SET InteresadoID = '" + value + "'" +
                                                   " WHERE (`CasoId` = '" + sCasoId + "' and TipoSolicitudId ='" + gSTipoSolicitudId + "');";

                MySqlDataReader resp_updateResp = con_4.getdatareader(actualizarTitularidcaso);

                if (resp_updateResp != null)
                {
                    //lResponsable.Text = texti;
                    //tbResponsableHeader.Text = texti;
                    tbTitularHeader.Text = texti;
                }
                resp_updateResp.Close();
                con_4.Cerrarconexion();
            }
        }

        private void fTcontencioso_Load(object sender, EventArgs e)
        {

        }

        private void tbFechapresentacion_Leave(object sender, EventArgs e)
        {
            tbFechapresentacion.Text = tbFechapresentacion.Text.Replace("/", "-").Replace(".", "-");
        }

        private void tbClientduedate_Leave(object sender, EventArgs e)
        {
            tbClientduedate.Text = tbClientduedate.Text.Replace("/", "-").Replace(".", "-");
        }

        private void Fecha_Pendiente_Leave(object sender, EventArgs e)
        {
            Fecha_Pendiente.Text = Fecha_Pendiente.Text.Replace("/", "-").Replace(".", "-");
        }
        private void tbFecharesolucion_Leave(object sender, EventArgs e)
        {
            tbFecharesolucion.Text = tbFecharesolucion.Text.Replace("/", "-").Replace(".", "-");
        }

        private void label33_MouseLeave(object sender, EventArgs e)
        {
            label33.BackColor = Color.Yellow;
        }

        private void label33_MouseMove(object sender, MouseEventArgs e)
        {
            label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(181)))), ((int)(((byte)(1)))));
        }

        private void label33_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            fCorresponsalupdateCO bForm = new fCorresponsalupdateCO(sCasoId, gSTipoSolicitudId, tbContactoCorresponsalHeader.Text, tbCorresponsalHeader.Text, loguin);
            if (bForm.ShowDialog() == DialogResult.OK)
            {
                tbCorresponsalHeader.Text = bForm.sClienteidtext;
                tbContactoCorresponsalHeader.Text = bForm.sContactoidtext;
                gSclienteid = bForm.sClienteidvalue;
                richTextBox4.Text = bForm.rtCorreocontacto_pass;
            }
        }

        private void bgdatosg_Enter(object sender, EventArgs e)
        {

        }

        private void tbActor_ModifiedChanged(object sender, EventArgs e)
        {
            bActor_update = true;
        }

        private void tbDemandado_ModifiedChanged(object sender, EventArgs e)
        {
            bDemandado_update = true;
        }

        private void tbAutoridadDemandada_ModifiedChanged(object sender, EventArgs e)
        {
            bAutoridadDemandada_update = true;
        }

        private void tbTerceroInteresado_MarginChanged(object sender, EventArgs e)
        {
            //bTerceroInteresado_update = true;
        }

        private void tbInstancia_ModifiedChanged(object sender, EventArgs e)
        {
            bInstancia_update = true;
        }

        private void tbExpControvertido_ModifiedChanged(object sender, EventArgs e)
        {
            bExpControvertido_update = true;
        }

        private void tbClase_ModifiedChanged(object sender, EventArgs e)
        {
            bClase_update = true;
        }

        private void rtbMotivo_ModifiedChanged(object sender, EventArgs e)
        {
            bMotivo_update = true;
        }

        private void tbTerceroInteresado_ModifiedChanged(object sender, EventArgs e)
        {
            bTerceroInteresado_update = true;
        }

        private void bAgregarplazo_Click(object sender, EventArgs e)
        {
            //AQUÍ abriremos una ventana para capturar los datos del plazo nuevo fecha estatus plazo  relacionado si es que existe etc ..
            try
            {
                agregaplazo obj = new agregaplazo(sCasoId, gSTipoSolicitudId, gSCasoNumero, loguin.sId, 3);//loguin.sId es el usuario de la sesion
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

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                generaexcel(dgPlazos);
            }
            catch (Exception E)
            {
                Console.Write("Se canceló la exportación");

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

                            //20220524FSV Eliminamos los retornos de carro que producen error en el excel
                            sValor = sValor.Replace("\0", " ");
                            //20220524FSV Fin eliminar los retornos de carro
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

        private void button50_Click(object sender, EventArgs e)
        {
            Fatenderplazo obj = new Fatenderplazo(sCasoId, gSTipoSolicitudId, loguin, gSCasoNumero);
            obj.ShowDialog();
            generadom(sCasoId);
        }

        private void button14_Click(object sender, EventArgs e)
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


        public void consultaplazo_nuevos()
        {
            try
            {
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
                    dRows.Cells[8].Value = sValidafechavacia(sFecha_notificacion_impi);
                    dRows.Cells[9].Value = sValidafechavacia(sFecha_Vencimiento_regular_impi);
                    //dRows.Cells[10].Value = sValidafechavacia(sFecha_vencimiento_3m_impi);
                    dRows.Cells[10].Value = sValidafechavacia(sFecha_vencimiento_4m_impi);
                    dRows.Cells[11].Value = sValidafechavacia(sFecha_atendio_plazo_impi);
                    dRows.Cells[12].Value = satendio_plazoimpi;
                    dRows.Cells[13].Value = sDoc_atendio;
                    dRows.Cells[14].Value = sMotivo_cancelacion_plazo_impi;
                    dRows.Cells[15].Value = sValidafechavacia(sFecha_cancelacion_plazo_impi);

                    dRows.Cells[16].Value = sUsuariocancelo;
                    dRows.Cells[17].Value = sFecha_atendio_plazo_impi_sistema;

                    dgPlazos.Rows.Add(dRows);


                }
                con_tcon_edocs.Cerrarconexion();
                resp_tedocs.Close();

            }
            catch (Exception Ex)
            {
                new filelog("plazos_patentes: ", "linea 2386: Error: " + Ex.Message);
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


        public void consultacartasyescritos(int idTipodoc, ComboBox cbTipodoc)
        {
            try
            {
                // 2 Escrito
                // 3 Carta
                // 5 formato
                conect con_4 = new conect();

                String sQuery = " SELECT  " +
                                "     * " +
                                " FROM " +
                                "     subtipodocumento, " +
                                "     gruposubtipodocumento " +
                                " WHERE " +
                                "     gruposubtipodocumento.GrupoId = 3 " +//contencioso
                                "         AND gruposubtipodocumento.SubtipodocumentoId = subtipodocumento.SubtipodocumentoId " +
                                "         AND TipoDocumentoId = " + idTipodoc + " " +//carta o escrito
                                "         AND SubTipoDocumentoIndAct = 1 " +
                                "         AND (SubTipoDocumentoTemplateEspanol != '' " +
                                "         OR SubTipoDocumentoTemplateIngles != ''); ";//"SELECT * FROM `clasificadornizavigente` ORDER BY CasoProductosClase ASC";
                MySqlDataReader respuestastringclass = con_4.getdatareader(sQuery);
                cbTipodoc.Items.Clear();
                while (respuestastringclass.Read())
                {
                    //String sNombredescrip = validareader("SubTipoDocumentoId", "SubTipoDocumentoDescrip", respuestastringclass).Text;
                    //String sId = validareader("SubTipoDocumentoId", "SubTipoDocumentoDescrip", respuestastringclass).Value.ToString();
                    cbTipodoc.Items.Add(validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", respuestastringclass));
                }
                respuestastringclass.Close();
                con_4.Cerrarconexion();
            }
            catch (Exception exs)
            {
                new filelog("", "" + exs.Message);
            }
        }


        public void agregamosidiomas(String idIdiomaactual)
        {
            try
            {
                //Agregamos el idioma
                conect con_idiomas2 = new conect();
                String sIdiomas2 = "select * from idioma";// where IdiomaId <> " + objfuncionesdicss.validareader("idiomaId", "CasoId", respuestastring3).Text;
                MySqlDataReader resp_idioma2 = con_idiomas2.getdatareader(sIdiomas2);
                while (resp_idioma2.Read())
                {
                    ComboboxItem prueba = objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2);
                    //cbDIdioma.Items.Add(objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2));//consultar idioma
                    //cbIdiomaCliente.Items.Add(objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2));//consultar idioma
                    cbIdiomacarta.Items.Add(objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2));//consultar escrito
                                                                                                                           //cbidiomaescrito.Items.Add(objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2));//consultar escrito
                                                                                                                           //cbIdioma.Text = objfuncionesdicss.validareader("IdiomaDescripcion", "CasoId", respuestastring3).Text;//consultar idioma
                }
                resp_idioma2.Close();
                con_idiomas2.Cerrarconexion();


                conect con_idioma = new conect();
                String sIdioma = "select * from idioma where IdiomaId = " + idIdiomaactual;
                MySqlDataReader resp_idioma = con_idioma.getdatareader(sIdioma);
                String sIdiomadelcaso = "";
                while (resp_idioma.Read())
                {
                    sIdiomadelcaso = objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma).Text;
                }

                //cbDIdioma.Text = sIdiomadelcaso;
                //cbIdiomaCliente.Text = sIdiomadelcaso;
                cbIdiomacarta.Text = sIdiomadelcaso;
                //cbidiomaescrito.Text = sIdiomadelcaso;



                resp_idioma.Close();
                con_idioma.Cerrarconexion();
            }
            catch (Exception exs)
            {
                new filelog("agregamosidiomas", "" + exs.Message);
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
                else
                {
                    MessageBox.Show("Seleccione el idioma del escrito.");
                    return;
                }


                if (cbEscritos.SelectedItem == null)
                {
                    return;
                }


                if (cbOficiosEscritos.SelectedItem != null)
                {
                    String sDocumentoid = (cbOficiosEscritos.SelectedItem as ComboboxItem).Value.ToString();
                    objcontenciosoctualview = new view_caso_contencioso(sCasoId, gSTipoSolicitudId, sIdidiomaescritos, sDocumentoid);
                }
                else
                {
                    objcontenciosoctualview = new view_caso_contencioso(sCasoId, gSTipoSolicitudId, sIdidiomaescritos);
                }





                //generacarta_pat objcarta = null;
                generacarta_contencioso objcarta = null;
                //fin de modificacion

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
                        //objcarta = new generacarta_pat(sCartanombreENfile, valuecob, objpatentectualview);
                        objcarta = new generacarta_contencioso(sCartanombreENfile, valuecob, objcontenciosoctualview);
                        //generacarta_contencioso objcarta = new generacarta_contencioso(sCartanombreENfile, valuecob, objcontenciosoctualview);
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
                            //objcarta = new generacarta_pat(sCartanombreESPfile, valuecob, objpatentectualview);
                            objcarta = new generacarta_contencioso(sCartanombreESPfile, valuecob, objcontenciosoctualview);
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

        private void button1_Click(object sender, EventArgs e)
        {

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

        private void button15_Click(object sender, EventArgs e)
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
                    resp_dPlazos.Close();
                    con_.Cerrarconexion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe seleccionar un registro");
                new filelog("Error eliminar documento", " Error: " + ex.StackTrace);

            }
        }

        private void tbPendienteswrite_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tbPendientesread.Text = tbPendienteswrite.Text;
            }
            catch (Exception exs)
            {
                new filelog("", "" + exs.Message);
            }

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dGV_docimentos_IMPI_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void Form1_Load(object sender, DataGridViewCellMouseEventArgs e)
        {
            cursor.Init(tabPage6);
            cursor.WorkType = cursor.MoveOrResize.MoveAndResize;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            controlsInfoStr = cursor.GetSizeAndPositionOfControlsToString(this);
            String control ="";
            conect usuario = new conect();
            String queryusuario = "Select * from ccursor where IdUsuario= '" + IdUsuario + "' AND NombreUsuario ='" + NombreUsuario + "';";
            MySqlDataReader cursorusuario = usuario.getdatareader(queryusuario);
            while (cursorusuario.Read())
            {
               control = (objfuncionesdicss.validareader("IdCursor", "IdCursor", cursorusuario).Text);
            }
               
            if (control == null || control =="")
            {

                conect q_cursor = new conect();
                String query_cursor = " INSERT INTO `CCursor`  (`IdCursor`,`NCursor`,`IdUsuario`,`NombreUsuario`) VALUES(null,'" + controlsInfoStr + "','" + IdUsuario + "','" + NombreUsuario + "')";
                MySqlDataReader readerCursor = q_cursor.getdatareader(query_cursor);
                if (readerCursor.RecordsAffected == 1)
                {
                    MessageBox.Show("Se guardo la configuracion.");
                }
                readerCursor.Close();
                q_cursor.Cerrarconexion();

            }
            else
            {
                conect q_cursor = new conect();
                String query_cursor = " UPDATE `CCursor`  SET `NCursor`='" + controlsInfoStr + "' where `IdUsuario`='"+ IdUsuario + "' AND `NombreUsuario`='" + NombreUsuario + "'";
                MySqlDataReader readerCursor = q_cursor.getdatareader(query_cursor);
                if (readerCursor.RecordsAffected == 1)
                {
                    MessageBox.Show("Se actualizo la configuracion.");
                }
                readerCursor.Close();
                q_cursor.Cerrarconexion();
            }
            cursorusuario.Close();
            usuario.Cerrarconexion();
            }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                String control = "";
                conect usuario = new conect();
                String queryusuario = "Select * from ccursor where IdUsuario= '" + IdUsuario + "' AND NombreUsuario ='" + NombreUsuario + "';";
                MySqlDataReader cursorusuario = usuario.getdatareader(queryusuario);
                while (cursorusuario.Read())
                {
                    control = (objfuncionesdicss.validareader("NCursor", "IdCursor", cursorusuario).Text);
                }

                if (control != "")
                {
                    cursor.SetSizeAndPositionOfControlsFromString(this, control);
                    MessageBox.Show("Se cargo la configuracion.");
                }
                else
                {
                    MessageBox.Show("No tiene ninguna configuracion guardada.");
                }
            }
            catch{

            }

           /* if (!string.IsNullOrWhiteSpace(controlsInfoStr))
            {
                cursor.SetSizeAndPositionOfControlsFromString(this, controlsInfoStr);
            }*/
        }
    }
}
