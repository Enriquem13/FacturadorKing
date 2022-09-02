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

namespace Facturador
{
    public partial class fTcontencioso_lalo : Form
    {
        public Form1 loguin;
        public captura captura;

        //20220310 Fsalazar
        public captura fCapuraform;
        //20220310 Fsalazar


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
        public String sCarpetadocumentos = "DigitalizadoContencioso\\edocs";
        //20220309 Fsalazar fin de modificación

        //20220328FSV 
        funcionesdicss objfuncionesdicss = new funcionesdicss();
        view_caso_contencioso objcontenciosoctualview = null;
        public String Subtipodocumentoidultimoescrito;
        

        public bool bExpediente_update = false;
        public bool bFechaConcesion_update = false;
        public bool bClienteduedate_update = false;
        public bool bFechaResolucion_update = false;
        //public bool bTituloIngles_update = false;
        public bool bTitulo_update = false;
        public bool btituloidiomaoriginal_update = false;
        public bool bEncargadoExterno_update = false;
        public bool bObservaciones_update = false;


        //20220328FSV

        public fTcontencioso_lalo(Form1 loguinp, captura cap, bContencioso consul, String sCasoIdcaso)
        {
            loguin = loguinp;
            cap = captura;
            //sTipogrupoglobal = consul.sGTipocaso;
            consultacaso = consul;
            sCasoId = sCasoIdcaso;
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
            //iIndiceids_global = Array.IndexOf(sArrayids, sCasoIdcaso);
            //20220327GSV No sabemos porque viene desactivado, pero lo reactivamos
            iIndiceids_global = Array.IndexOf(sArrayids, sCasoIdcaso);
            //20220327FSV Reactivar


            //string[] ubicacion = Directory.GetFiles(@"C:\Pclientes\Cartas");//<--aqui va la ruta de la carpeta donde estan los documentos

            //for (int i = 0; i < ubicacion.Length; i++)
            //{
            //    cbCartas.Items.Add(Path.GetFileName(ubicacion[i]));//combobox el que mostrara todos los nombres

            //}
            //terminaciclo caso_contencioso
            generadom(sCasoId);
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
        public void limpiarcontenido()
        {
            tbExpediente.Text = "";
            /*tbDNumeroReg.Text = "";
            tbDFechalegal.Text = "";
            tbDfecharecepcion.Text = "";
            tbDFechaconcesion.Text = "";
            tbDFechaprobo.Text = "";
            tbDFechacarta.Text = "";
            tbDFechainiciouso.Text = "";
            tbDFechavigencia.Text = "";
            tbDSigpruebauso.Text = "";
            rtbDDenominacion.Text = "";
            tbSubtipo.Text = "";
            tbDtipo.Text = "";
            tbEstatus.Text = "";*/
            //lTitular.Text = "";
            tbTitularHeader.Text = "";
            tbTituloHeader.Text = "";
            //tbCasoNumero.Text = "";
            tbCasoHeader.Text = "";
            tbCasoIdHeader.Text = "";
            //lReferencia.Text = "";
            tbReferenciaHeader.Text = "";
            tbRegistroHeader.Text = "";
            //lResponsable.Text = "";
            tbResponsableHeader.Text = "";
            //lContacto.Text = "";
            tbContactoHeader.Text = "";
            rtCorreocontacto.Text = "";
            //lCliente.Text = "";
            tbClienteHeader.Text = "";
            //lPais.Text = "";
            tbPaisHeader.Text = "";
            //tbCasoNumero.Text = "";
            tbExpedienteHeader.Text = "";
            lRegistro.Text = "";
            tbRegistroHeader.Text = "";
            tbContactoCorresponsalHeader.Text = "";
            tbCorresponsalHeader.Text = "";

            //20220329FSV Limpiamos objetos
            rtTitulo.Text = "";
            rtTituloidionaoriginal.Text = "";
            tbEncargadoimpi.Text = "";
            rtbObservaciones.Text = "";
            //20220329FVS Fin de modificacion

            /*cbClasemarca.Text = "";
            tbclase.Text = "";
            lvProductos.Items.Clear();*/
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
                String sQuery = "SELECT CasoId, "+
                                " TipoSolicitudId," +//subtiposolicitudoid
                                " SubTipoSolicitudId," +//subtiposolicitudoid
                                " CasoTituloespanol," +
                                " CasoTituloIngles," + //20220325FSV agregamos el titulo en ingles
                                " IdiomaId, " + 
                                " DATE_FORMAT(CasoFechaConcesion , '%d-%m-%Y') as  CasoFechaConcesion, "+
                                " DATE_FORMAT(CasoFechaLegal , '%d-%m-%Y') as  CasoFechaLegal,    "+
                                " DATE_FORMAT(CasoFechaDivulgacionPrevia , '%d-%m-%Y') as  CasoFechaDivulgacionPrevia,  "+
                                " DATE_FORMAT(CasoFechaRecepcion , '%d-%m-%Y') as  CasoFechaRecepcion,   "+
                                " DATE_FORMAT(CasoFechaVigencia , '%d-%m-%Y') as  CasoFechaVigencia,    "+
                                " CasoNumeroExpedienteLargo,    CasoNumero,    ResponsableId,"+
                                "    TipoMarcaId,  "+
                                " DATE_FORMAT(CasoFechaAlta , '%d-%m-%Y') as  CasoFechaAlta, "+
                                "    CasoTipoCaptura,   "+
                                " CasoTitular,    "+
                                " DATE_FORMAT(CasoFechaFilingSistema , '%d-%m-%Y') as  CasoFechaFilingSistema,   "+
                                " DATE_FORMAT(CasoFechaFilingCliente , '%d-%m-%Y') as  CasoFechaFilingCliente,    "+
                                " DATE_FORMAT(CasoFechaCartaCliente , '%d-%m-%Y') as  CasoFechaCartaCliente,   "+
                                " DATE_FORMAT(CasoFechaPresentacion , '%d-%m-%Y') as  CasoFechaPresentacion,   " +
                                " EstatusCasoId,   "+
                                " UsuarioId,    "+
                                " PaisId,    "+
                                " CasoEncargadoExterno, "+
                                " DATE_FORMAT(CasoFechaResolucion , '%d-%m-%Y') as CasoFechaResolucion, SentidoResolucionId, ParteRepresentadaId, " +
                                " DATE_FORMAT(CasoFechaPruebaUsoSig , '%d-%m-%Y') as  CasoFechaPruebaUsoSig,   "+
                                //20220329FSV Agregamos el campo de observacion
                                " CasoObservaciones,    " +
                                //20220322FSV Fin de modificaccion
                                " CasoNumConcedida    FROM    caso_contencioso " +
                                "WHERE   caso_contencioso.CasoId  LIKE '%" + sCasoidgenera + "%'";
                MySqlDataReader respuestastring3 = con.getdatareader(sQuery);
                //progressBar1.Value = 10;
                while (respuestastring3.Read())
                {
                    tbTituloHeader.Text = validareader("CasoTituloespanol", "CasoId", respuestastring3).Text;
                    //tbCasoNumero.Text = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                    tbCasoHeader.Text = validareader("SubTipoSolicitudId", "CasoId", respuestastring3).Text;

                    lRegistro.Text = validareader("CasoNumConcedida", "CasoId", respuestastring3).Text;
                    tbRegistroHeader.Text = validareader("CasoNumConcedida", "CasoId", respuestastring3).Text;
                    tbExpedienteHeader.Text = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                    tbFecharesolucion.Text = validareader("CasoFechaResolucion", "CasoId", respuestastring3).Text;
                    tbFecharesolucion.Text = validareader("CasoFechaResolucion", "CasoId", respuestastring3).Text;
                    tbParte.Text = validareader("ParteRepresentadaId", "CasoId", respuestastring3).Text;
                    tbSentido.Text = validareader("SentidoResolucionId", "CasoId", respuestastring3).Text;

                    //20220303 Fsalazar 
                    gSTipoSolicitudId = validareader("TipoSolicitudId", "CasoId", respuestastring3).Text;
                    //20220303 Fsalazar
                    //20220303 Fsalazar
                    gSCasoNumero = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                    //20220303 Fsalazar

                    agregamosidiomas(validareader("IdiomaId", "CasoId", respuestastring3).Text);
                    //Consultamos el pais si existe el paisid
                    //String sIdpais = validareader("PaisId", "CasoId", respuestastring3).Text;
                    //if (sIdpais != "")
                    //{
                    //    MySqlDataReader respuestaPais = con.getdatareader("select * from pais where PaisId = " + sIdpais);
                    //    while (respuestaPais.Read())
                    //    {
                    //        lPais.Text = validareader("PaisNombre", "PaisId", respuestaPais).Text;
                    //    }
                    //    respuestaPais.Close();
                    //}
                    ////consultamos al cliente
                    ///

                    String sIdpais = validareader("PaisId", "CasoId", respuestastring3).Text;
                    if (sIdpais != "")
                    {
                        conect con_02 = new conect();
                        MySqlDataReader respuestaPais = con_02.getdatareader("select * from pais where PaisId = " + sIdpais);
                        while (respuestaPais.Read())
                        {
                            //lPais.Text = validareader("PaisNombre", "PaisId", respuestaPais).Text;
                            tbPaisHeader.Text = validareader("PaisNombre", "PaisId", respuestaPais).Text;
                        }
                        respuestaPais.Close();
                        con_02.Cerrarconexion();
                    }
                        



                    String sClienteid = "";
                    conect con_03 = new conect();
                    MySqlDataReader respuestaCliente = con_03.getdatareader("Select * from casocliente, cliente where casocliente.CasoId = " + validareader("CasoId", "CasoId", respuestastring3).Text + " and cliente.clienteid =  casocliente.clienteid;");
                    while (respuestaCliente.Read())
                    {
                        //lCliente.Text = validareader("ClienteNombre", "ClienteId", respuestaCliente).Text;
                        tbClienteHeader.Text = validareader("ClienteNombre", "ClienteId", respuestaCliente).Text;
                        sClienteid = validareader("ClienteId", "ClienteId", respuestaCliente).Text;

                        //20220303 Fsalazar mandamos el numero de cliente
                        gSclienteid = sClienteid;
                        //20220303 Fsalazar Fin de modificación
                    }
                    respuestaCliente.Close();
                    con_03.Cerrarconexion();
                    //consultamos al contacto del cliente


                    //20220311FSV Agregamos los datos del corresponsal
                    conect con_04 = new conect();
                    String sQryCorresponsal =   "SELECT caso_contencioso.CasoId, " +
                                                "DAMEALCONTACTOCASO(casocorresponsal.ClienteId) AS ClienteCorresponsal,   " +
                                                "DAMEALCONTACTOCASO(casocorresponsal.ContactoId) AS ContactoCorresponsal,   " +
                                                "DAMEELCORREOCONTACTOCASO(casocorresponsal.ContactoId) AS CorreoCorresponsal " +
                                                "FROM caso_contencioso, casocorresponsal, cliente " +
                                                "WHERE caso_contencioso.CasoId = casocorresponsal.CasoId " +
                                                "AND casocorresponsal.ClienteId = cliente.ClienteId " +
                                                "AND  caso_contencioso.CasoId  LIKE '%" + sCasoidgenera + "%';";
                    MySqlDataReader respuestaCorresponsal = con_04.getdatareader(sQryCorresponsal);
                    while (respuestaCorresponsal.Read())
                    {
                        //lCorresponsal.Text = validareader("ClienteCorresponsal", "CasoId", respuestaCorresponsal).Text;
                        //lCotaccorresponsal.Text = validareader("ContactoCorresponsal", "CasoId", respuestaCorresponsal).Text;
                        tbCorresponsalHeader.Text = validareader("ClienteCorresponsal", "CasoId", respuestaCorresponsal).Text;
                        tbContactoCorresponsalHeader.Text = validareader("ContactoCorresponsal", "CasoId", respuestaCorresponsal).Text;
                        richTextBox4.Text = validareader("CorreoCorresponsal", "CasoId", respuestaCorresponsal).Text;
                    }
                    respuestaCorresponsal.Close();
                    con_04.Cerrarconexion();
                    //20220311FSV Fin de Modificaicón



                    if (sClienteid != "")
                    {
                        conect con_05 = new conect();
                        MySqlDataReader respuestaContacto = con_05.getdatareader("select * from contacto where Clienteid = " + sClienteid);
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
                        con_05.Cerrarconexion();
                    }

                    String idUsuario = validareader("UsuarioId", "CasoId", respuestastring3).Text;
                    if (idUsuario != "")
                    {
                        conect con_06 = new conect();
                        MySqlDataReader respuestaUser = con_06.getdatareader("select * from usuario where UsuarioId = " + idUsuario);
                        while (respuestaUser.Read())
                        {
                            //lResponsable.Text = validareader("UsuarioNombre", "UsuarioId", respuestaUser).Text;
                            tbResponsableHeader.Text = validareader("UsuarioNombre", "UsuarioId", respuestaUser).Text;
                        }
                        respuestaUser.Close();
                        con_06.Cerrarconexion();
                    }

                    String sReferencia = validareader("CasoId", "CasoId", respuestastring3).Text;
                    if (sReferencia != "")
                    {
                        conect con_07 = new conect();
                        MySqlDataReader respuestaReferencia = con_07.getdatareader("select * from referencia where CasoId = " + sReferencia);
                        while (respuestaReferencia.Read())
                        {
                            //lReferencia.Text = validareader("ReferenciaNombre", "referenciaid", respuestaReferencia).Text;
                            tbReferenciaHeader.Text = validareader("ReferenciaNombre", "referenciaid", respuestaReferencia).Text;
                        }
                        respuestaReferencia.Close();
                        con_07.Cerrarconexion();
                    }

                    //lTitular.Text = validareader("CasoTitular", "CasoId", respuestastring3).Text;
                    tbTitularHeader.Text = validareader("CasoTitular", "CasoId", respuestastring3).Text;
                    //tbCasoNumero.Text = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                    tbCasoHeader.Text = validareader("CasoNumero", "CasoId", respuestastring3).Text;

                    //Datos Generales

                    //Estatus caso
                    String sEstatuscasoid = validareader("EstatusCasoId", "CasoId", respuestastring3).Text;
                    if (sEstatuscasoid != "")
                    {
                        conect con_08 = new conect();
                        MySqlDataReader respuestaEstatus = con_08.getdatareader("select * from estatuscaso where EstatusCasoId = " + sEstatuscasoid);
                        while (respuestaEstatus.Read())
                        {
                            tbEstatusHeader.Text = validareader("EstatusCasoDescrip", "EstatusCasoId", respuestaEstatus).Text;
                        }
                        respuestaEstatus.Close();
                        con_08.Cerrarconexion();
                    }

                    //Tipo solicitud ID
                    String sTipoSolicitudId = validareader("TipoSolicitudId", "CasoId", respuestastring3).Text;
                    if (sTipoSolicitudId != "")
                    {
                        conect con_09 = new conect();
                        MySqlDataReader respuestaTiposolic = con_09.getdatareader("select * from tiposolicitud where TipoSolicitudId = " + sTipoSolicitudId);
                        while (respuestaTiposolic.Read())
                        {
                            tbTipo.Text = validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestaTiposolic).Text;
                        }
                        respuestaTiposolic.Close();
                        con_09.Cerrarconexion();
                    }


                    //subtiposolicitudoid



                    //Tipo solicitud ID
                    String sSubTipoSolicitudId = validareader("SubTipoSolicitudId", "CasoId", respuestastring3).Text;
                    if (sSubTipoSolicitudId != "")
                    {
                        conect con_10 = new conect();
                        MySqlDataReader respuestasubTiposol = con_10.getdatareader("select * from subtiposolicitud where SubTipoSolicitudId = " + sSubTipoSolicitudId);
                        while (respuestasubTiposol.Read())
                        {
                            tbAcciones.Text = validareader("SubTipoSolicitudDescripcion", "SubTipoSolicitudId", respuestasubTiposol).Text;
                        }
                        respuestasubTiposol.Close();
                        con_10.Cerrarconexion();
                    }

                    tbExpediente.Text = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                    tbEncargadoimpi.Text = validareader("CasoEncargadoExterno", "CasoId", respuestastring3).Text;
                    //tbEstatus.Text = validareader("EstatusCasoId", "CasoId", respuestastring3).Text;
                    tbInstitucion.Text = validareader("CasoFechaAlta", "CasoId", respuestastring3).Text;
                    
                    //CasoFechaPresentacion
                    ////dependiendo el idioma ponemos 
                    rtTitulo.Text = validareader("CasoTituloespanol", "CasoId", respuestastring3).Text;
                    rtTituloidionaoriginal.Text = validareader("CasoTituloIngles", "CasoId", respuestastring3).Text; //20220425FSV Agremaos titulo en ingles
                    tbFechaconcesion.Text = validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text; //20220329FSV Agremaos campo
                    tbClientduedate.Text = validareader("CasoFechaFilingCliente", "CasoId", respuestastring3).Text; //20220328FSV Agremaos titulo en ingles
                    rtbObservaciones.Text = validareader("CasoObservaciones", "CasoId", respuestastring3).Text; //20220329FSV Agremaos campo


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




                    //20220328FSV Llenamos la seccion 
                    carga_documentos_IMPI(sCasoId, gSTipoSolicitudId);
                    //20220328FSV


                    //consultamos los plazos
                    consultaplazo_nuevos();


                    //String sTipoMarcaId = validareader("TipoMarcaId", "CasoId", respuestastring3).Text;
                    //if (sTipoMarcaId != "")
                    //{
                    //    MySqlDataReader respuestasubTipomarca = con.getdatareader("select * from tipomarca where TipoMarcaId = " + sTipoMarcaId);
                    //    while (respuestasubTipomarca.Read())
                    //    {
                    //        cbDTipomarca.Text = validareader("TipoMarcaDescrip", "TipoMarcaId", respuestasubTipomarca).Text;
                    //    }
                    //    respuestasubTipomarca.Close();
                    //}
                    icontgeneradom++;
                    //icontadorbusqueda = 0;
                }
                
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
                //progressBar1.Value = 100;
                //progressBar1.Hide();
                this.Enabled = true;
            }

        }

        public void consultacartasyescritos(int idTipodoc, ComboBox cbTipodoc) {
            try {
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
                                "     gruposubtipodocumento.GrupoId = 3 " +//MARCAS
                                "         AND gruposubtipodocumento.SubtipodocumentoId = subtipodocumento.SubtipodocumentoId " +
                                "         AND TipoDocumentoId = " + idTipodoc + " "+//carta o escrito
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
            } catch (Exception exs) {
                new filelog("", ""+exs.Message);
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



                //if (rtTitulo_update || btituloidiomaoriginal_update || bExpediente_update )
                if (bTitulo_update || btituloidiomaoriginal_update || bExpediente_update || bFechaConcesion_update || bClienteduedate_update || bFechaResolucion_update || bEncargadoExterno_update || bObservaciones_update)
                {
                    String sUpdateset = "";

                    if (bExpediente_update)
                    {
                        sUpdateset += ", CasoNumeroExpedienteLargo = '" + tbExpediente.Text + "'";
                    }
                    if (bFechaConcesion_update)
                    {
                        sUpdateset += ", CasoFechaConcesion =  STR_TO_DATE('" + tbFechaconcesion.Text + "', '%d-%m-%Y')";
                    }
                    if (bClienteduedate_update)
                    {
                        sUpdateset += ", CasoFechaFilingCliente = STR_TO_DATE('" + tbClientduedate.Text + "', '%d-%m-%Y')";
                    }
                    if (bFechaResolucion_update)
                    {
                        sUpdateset += ", CasoFechaResolucion = STR_TO_DATE('" + tbFecharesolucion.Text + "', '%d-%m-%Y')";
                    }
                    if (btituloidiomaoriginal_update)
                    {
                       sUpdateset += ", CasoTituloingles = '" + rtTituloidionaoriginal.Text + "'";
                    }
                    if (bEncargadoExterno_update)
                    {
                        sUpdateset += ", CasoEncargadoExterno = '" + tbEncargadoimpi.Text + "'";
                    }
                    if (bObservaciones_update)
                    {
                        sUpdateset += ", CasoObservaciones = '" + rtbObservaciones.Text + "'";
                    }



                    conect con = new conect();
                    String sIdspatentes = "UPDATE `caso_contencioso` SET `CasoTituloespanol` = '" + rtTitulo.Text + "' " + sUpdateset +
                        //" WHERE `caso_contencioso`.`CasoId` = " + sCasoId + " AND `caso_contencioso`.`TipoSolicitudId` = " + sTiposolicitudGlobal;
                        " WHERE `caso_contencioso`.`CasoId` = " + sCasoId + " AND `caso_contencioso`.`TipoSolicitudId` = " + gSTipoSolicitudId;
                    

                    MySqlDataReader resp_numpatentes = con.getdatareader(sIdspatentes);
                    resp_numpatentes.Read();

                    if (resp_numpatentes == null){
                        MessageBox.Show("Error al hacer el update. " + sIdspatentes);}
                    else{
                        MessageBox.Show("Cambios guardados correctamente.");}

                    resp_numpatentes.Close();
                    con.Cerrarconexion();
                }
                else{
                    MessageBox.Show("Cambios guardados correctamente.");}


                /*

                if (bPlazolegal || rtTitulo_update || bTipo_update || bExpediente_update || bNumregistro_update || bSubtipo_update || bClienteduedate_update || bFecharecimpi_update || bFechaconcesion_update || bCapitulo_update || bPlazolegal_update || bFechadivulgacion_update || bFechavigencia_update || bFechacarta_update || bFechainternacional_update || bFechapublicacion_update || btituloidiomaoriginal_update || bAreaimpi_update || bClasediesno_update)
                {
                    String sUpdateset = "";

                    if (bExpediente_update)
                    {
                        sUpdateset += ", CasoNumeroExpedienteLargo = '" + tbExpediente.Text + "'";
                    }
                    if (bNumregistro_update)
                    {
                        sUpdateset += ", CasoNumConcedida = '" + tbRegistro.Text + "'";
                    }
                    if (bPlazolegal)
                    {
                        sUpdateset += ", CasoFechaLegal = '" + tbPlazolegal.Text + "'";
                    }

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
                        sUpdateset += ", CasoTituloingles = '" + rtTituloidionaoriginal.Text + "'";
                    }

                    if (bClasediesno_update)
                    {
                        sUpdateset += "";
                    }
                    conect con = new conect();
                    String sIdspatentes = "UPDATE `caso_patente` SET `CasoTituloespanol` = '" + rtTitulo.Text + "' " + sUpdateset +
                        " WHERE `caso_patente`.`CasoId` = " + sCasoId + " AND `caso_patente`.`TipoSolicitudId` = " + sTiposolicitudGlobal;
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


                */




            }
            catch (Exception E)
            {
                MessageBox.Show("Error al intentar guardar cambios.");
                new filelog(loguin.sId, "liena 4286: " + E.ToString());
            }
        }



        public void resetvariable()
        {
            bTitulo_update = false;
            btituloidiomaoriginal_update = false;
            bEncargadoExterno_update = false;
            bObservaciones_update = false;
            //bTituloIngles_update = false;
            bExpediente_update = false;
            bFechaConcesion_update = false;
            bClienteduedate_update = false;
            bFechaResolucion_update = false;
        }




        private void button39_Click_1(object sender, EventArgs e)
        {
            //bBanderaadelanteatras = false;
            //int iCasoid = System.Convert.ToInt32(sCasoId) - 1;
            //generadom(iCasoid + "");
            //try
            //{
            //    iIndiceids_global = iIndiceids_global - 1;
            //    generadom(sArrayids[iIndiceids_global] + "");

            //}
            //catch (Exception E)
            //{
            //    iIndiceids_global = sArrayids.Length - 1;
            //    generadom(sArrayids[iIndiceids_global] + "");
            //}
            try
            {
                if (bTitulo_update || btituloidiomaoriginal_update || bExpediente_update || bFechaConcesion_update || bClienteduedate_update || bFechaResolucion_update || bEncargadoExterno_update || bObservaciones_update)
                {
                    DialogResult boton = MessageBox.Show("¿Desea guardar los cambios?", "Guardar cambios", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (boton == DialogResult.OK)
                    {
                        upcatescampos();
                        //resetvariable();
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
            }
            catch (Exception E)
            {
                new filelog(loguin.sId, "linea 4044: " + E.ToString());
            }


        }

        private void button38_Click(object sender, EventArgs e)
        {
            try
            {
                if (bTitulo_update || btituloidiomaoriginal_update || bExpediente_update || bFechaConcesion_update || bClienteduedate_update || bFechaResolucion_update || bEncargadoExterno_update || bObservaciones_update)
                   {
                        DialogResult boton = MessageBox.Show("¿Desea guardar los cambios?", "Guardar cambios", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (boton == DialogResult.OK)
                        {
                            upcatescampos();
                            //resetvariable();
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
                new filelog(loguin.sId, "linea 1119: "+E.ToString());
            }

    //bBanderaadelanteatras = true;
    //int iCasoid = System.Convert.ToInt32(sCasoId) + 1;
    //generadom(iCasoid + "");

    //try
    //{
    //    iIndiceids_global = iIndiceids_global + 1;
    //    generadom(sArrayids[iIndiceids_global] + "");

    //}
    //catch (Exception E)
    //{
    //    iIndiceids_global = 0;
    //    generadom(sArrayids[iIndiceids_global] + "");
    //}




}

        private void button2_Click(object sender, EventArgs e)
        {
            //20220318FSV Regresar al form de búsqueda
            //consultacaso.Focus();
            consultacaso.Show();
            this.Close();
        }
        public void agregamosidiomas(String idIdiomaactual) {
            try {
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
            catch (Exception exs) {
                new filelog("agregamosidiomas", ""+exs.Message);
            }
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
                new filelog("al generar las cartas en contencioso", ""+ex.Message);
            }
        }

        private void fTcontencioso_lalo_FormClosing(object sender, FormClosingEventArgs e)
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
            //buscarcliente bForm = new buscarcliente(sCasoId, gSTipoSolicitudId, lContacto.Text, lCliente.Text);
            //20220324FSV Cambiamos etiqueta por cuadro de texto solo lectura
            //buscarcliente bForm = new buscarcliente(sCasoId, gSTipoSolicitudId, lContacto.Text, lCliente.Text);
            //buscarcliente bForm = new buscarcliente(sCasoId, gSTipoSolicitudId, lContacto.Text, tbClienteHeader.Text);
            buscarcliente bForm = new buscarcliente(sCasoId, gSTipoSolicitudId, tbContactoHeader.Text, tbClienteHeader.Text);
            //20220324FSV Fin de Modificación
            if (bForm.ShowDialog() == DialogResult.OK)
            {
                //lCliente.Text = bForm.sClienteidtext;
                tbClienteHeader.Text = bForm.sClienteidtext;
                //lContacto.Text = bForm.sContactoidtext;
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
            //addContacto objnuevocontacto = new addContacto(gSclienteid, lCliente.Text, gSContactoid, lContacto.Text, sCasoId, gSTipoSolicitudId);
            //addContacto objnuevocontacto = new addContacto(gSclienteid, tbClienteHeader.Text, gSContactoid, lContacto.Text, sCasoId, gSTipoSolicitudId);
            addContacto objnuevocontacto = new addContacto(gSclienteid, tbClienteHeader.Text, gSContactoid, tbContactoHeader.Text, sCasoId, gSTipoSolicitudId);
            //20220324FSV Fin de Modificacion
            if (objnuevocontacto.ShowDialog() == DialogResult.OK)
            {
                //lContacto.Text = objnuevocontacto.SgContactotext;
                tbContactoHeader.Text= objnuevocontacto.SgContactotext;
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

            }

        }

        private void label8_DoubleClick(object sender, EventArgs e)
        {
            updateEstatus updateestatus = new updateEstatus("1");
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

                    string ruta = obj.sFileupload + "\\" + sCarpetadocumentos + "\\" + gSCasoNumero + "\\edocs";
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
                            MessageBox.Show("Error al intentar agregar el documento, ueque la ruta ó el nombre del archivo.");
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
            CapturaSolicitud addescrito = new CapturaSolicitud(fCapuraform, loguin, "3", sCasoId);
            if (addescrito.ShowDialog() == DialogResult.OK)
            {
                generadom(sCasoId);
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
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

        private void button43_Click(object sender, EventArgs e)
        {
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
            upcatescampos();
            resetvariable();

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

        private void tbFechaconcesion_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechaconcesion);
        }

        private void tbFechaconcesion_ModifiedChanged(object sender, EventArgs e)
        {
            bFechaConcesion_update = true;
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

        private void tbFecharesolucion_ModifiedChanged(object sender, EventArgs e)
        {
            bFechaResolucion_update = true;
        }

        private void bAgregarplazo_Click(object sender, EventArgs e)
        {
            //AQUÍ abriremos una ventana para capturar los datos del plazo nuevo fecha estatus plazo  relacionado si es que existe etc ..
            try
            {
                agregaplazo obj = new agregaplazo(sCasoId, gSTipoSolicitudId, gSCasoNumero, loguin.sId);//loguin.sId es el usuario de la sesion
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

        private void button50_Click(object sender, EventArgs e)
        {
            Fatenderplazo obj = new Fatenderplazo(sCasoId, gSTipoSolicitudId, loguin, gSCasoNumero);
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

        private void dgDocumentoselectronicos_DoubleClick(object sender, EventArgs e)
        {
            String sRuta = "";
            try
            {

                if (dgDocumentoselectronicos.SelectedRows != null)
                {
                    sRuta = dgDocumentoselectronicos.SelectedRows[0].Cells[5].Value.ToString();//lv_documentelect.SelectedItems[0].SubItems[3].Text;
                    Process.Start(sRuta);
                    MessageBox.Show("Ruta: " + sRuta);
                }
            }
            catch (Exception Ex)
            {
                new filelog("ver edocs", "Error: " + Ex.Message);
                MessageBox.Show(Ex.Message + " " + sRuta);
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
                        String sValor = "";
                        if (!(row.Cells[ic - 1].Value is null))
                        {
                            sValor = row.Cells[ic - 1].Value.ToString();
                        }

                        if (sValor == "01/01/0001 12:00:00 a. m.")
                        {
                            sValor = "";
                        }
                        obj.SetCellValue(ir, ic, sValor);
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

        private void button29_Click(object sender, EventArgs e)
        {

        }
    }
}
