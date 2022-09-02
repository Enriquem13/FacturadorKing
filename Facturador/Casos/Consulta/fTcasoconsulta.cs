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
using System.Diagnostics;

namespace Facturador
{
    public partial class fTcasoconsulta : Form
    {
        public Form1 loguin;
        public captura captura;

        //20220310 Fsalazar
        public captura fCapuraform;
        //20220310 Fsalazar


        //20220401FSV Cambiar el formulario que se consulta
        //public bContencioso consultacaso;
        public bConsulta consultarcasoconsulta;
        //20220401FSV Fin modificaicon

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
        //public String sCarpetadocumentos = "DigitalizadoConsulta\\edocs";
        public String sCarpetadocumentos = "Edocs\\Consulta";
        //20220309 Fsalazar fin de modificación

        //20220328FSV 
        funcionesdicss objfuncionesdicss = new funcionesdicss();
        public String Subtipodocumentoidultimoescrito;
        

        public bool bExpediente_update = false;
        public bool bFechaCarta_update = false;
        public bool bClienteduedate_update = false;
        public bool bFechaPresentacion_update = false;
        public bool bFechaConclusion_update = false;
        public bool FechaPendiente_update = false;
        public bool bTitulo_update = false;
        public bool btituloidiomaoriginal_update = false;
        public bool bEncargadoExterno_update = false;
        public bool bMotivo_update = false;
        public bool bObservaciones_update = false;


        //public bool bTituloIngles_update = false;
        public bool bFechaConcesion_update = false;

        //20220328FSV


        //public fTcasoconsulta(Form1 loguinp, captura cap, bContencioso consul, String sCasoIdcaso)
        public fTcasoconsulta(Form1 loguinp, captura cap, bConsulta consul, String sCasoIdcaso)
        {
            loguin = loguinp;
            cap = captura;
            //sTipogrupoglobal = consul.sGTipocaso;
            //consultacaso = consul;
            consultarcasoconsulta = consul;


            sCasoId = sCasoIdcaso;
            InitializeComponent();

            conect con = new conect();
            //String sIds = "select count(*) as numpatentes from caso_contencioso";
            String sIds = "select count(*) as numconsultas from caso_consulta";

            MySqlDataReader resp_numids = con.getdatareader(sIds);
            resp_numids.Read();

            //String sNumerodeids = validareader("numpatentes", "numpatentes", resp_numids).Text;
            String sNumerodeids = validareader("numconsultas", "numconsultas", resp_numids).Text;

            resp_numids.Close();


            int iNumerogrupoids = System.Convert.ToInt32(sNumerodeids);
            sArrayids = new String[iNumerogrupoids];


            //String sGetids = "select * from caso_contencioso";
            String sGetids = "select * from caso_consulta";
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
            tbContactoCorresponsalHeader.Text = "";
            tbCorresponsalHeader.Text = "";
            richTextBox4.Text = "";

            //20220329FSV Limpiamos objetos
            rtTitulo.Text = "";
            rtTituloidionaoriginal.Text = "";
            tbEncargadoimpi.Text = "";
            rtbMotivo.Text = "";
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
                
                /*progressBar1.Show();
                progressBar1.Value = 0;*/
                this.Enabled = false;
                int icontgeneradom = 0;

                conect conexquery = new conect();
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
                                " DATE_FORMAT(FechaDueDate, '%d-%m-%Y') as  CasoFechaFilingCliente,    " +
                                " DATE_FORMAT(CasoFechaCartaCliente , '%d-%m-%Y') as  CasoFechaCartaCliente,   "+
                                                                      "   Get_Interesados_tiposol(Casoid, TipoSolicitudId) as NombreUtilInt, " +
                                " DATE_FORMAT(CasoFechaRecepcion , '%d-%m-%Y') as  CasoFechaRecepcion,   " +
                                " DATE_FORMAT(FechaPendiente , '%d-%m-%Y') as  FechaPendiente,   " +
                                " EstatusCasoId,   " +
                                " UsuarioId,    "+
                                " PaisId,    "+
                                " CasoEncargadoExterno, "+
                                " DATE_FORMAT(CasoFechaResolucion , '%d-%m-%Y') as CasoFechaResolucion, SentidoResolucionId, ParteRepresentadaId, " +
                                " DATE_FORMAT(CasoFechaPruebaUsoSig , '%d-%m-%Y') as  CasoFechaPruebaUsoSig,   "+
                                //20220329FSV Agregamos el campo de observacion
                                " CasoObservaciones, CasoMotivo,    " +
                                //20220322FSV Fin de modificaccion
                                //20220401FSV Cambiamos la tabla
                                " CasoNumConcedida    FROM    caso_consulta " +
                                //20220412DSV No se porque le pusieron like
                                //"WHERE   caso_consulta.CasoId  LIKE '%" + sCasoidgenera + "%'";
                                "WHERE   caso_consulta.CasoId  = '" + sCasoidgenera + "'";
                                //20220401FSV Fin de Modificacion
                
                //Leemos caso consulta de la tabla destinada
                MySqlDataReader respuestastring3 = conexquery.getdatareader(sQuery);
                //progressBar1.Value = 10;
                while (respuestastring3.Read())
                {
                    //Variables Globales
                    gSCasoNumero = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                    gSTipoSolicitudId = validareader("TipoSolicitudId", "CasoId", respuestastring3).Text;
                    

                    //Seccion de Encabezado
                    tbTituloHeader.Text = validareader("CasoTituloespanol", "CasoId", respuestastring3).Text;
                    tbCasoHeader.Text = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                    tbTitularHeader.Text = validareader("CasoTitular", "CasoId", respuestastring3).Text;
                    
                    tbExpedienteHeader.Text = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                    tbExpediente.Text = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;




                    //Traemos las Fechas
                    if (validareader("CasoFechaAlta", "CasoId", respuestastring3).Text != "00-00-0000")
                    {
                        tbInstitucion.Text = validareader("CasoFechaAlta", "CasoId", respuestastring3).Text;
                    }
                    if (validareader("CasoFechaCartaCliente", "CasoId", respuestastring3).Text != "00-00-0000")
                    {
                        tbFechaCarta.Text = validareader("CasoFechaCartaCliente", "CasoId", respuestastring3).Text;
                    }
                    if (validareader("CasoFechaFilingCliente", "CasoId", respuestastring3).Text != "00-00-0000")
                    {
                        tbClientduedate.Text = validareader("CasoFechaFilingCliente", "CasoId", respuestastring3).Text;
                    }
                    if (validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text != "00-00-0000")
                    {
                        tbFechaPresentacion.Text = validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text;
                    }
                    if (validareader("CasoFechaResolucion", "CasoId", respuestastring3).Text != "00-00-0000")
                    {
                        tbFechaConclusion.Text = validareader("CasoFechaResolucion", "CasoId", respuestastring3).Text;
                    }
                    if (validareader("FechaPendiente", "CasoId", respuestastring3).Text != "00-00-0000")
                    {
                        FechaPendiente.Text = validareader("FechaPendiente", "CasoId", respuestastring3).Text;
                    }

                    //Pestaña Datos Generales
                    rtTitulo.Text = validareader("CasoTituloespanol", "CasoId", respuestastring3).Text;
                    rtTituloidionaoriginal.Text = validareader("CasoTituloIngles", "CasoId", respuestastring3).Text; //20220425FSV Agremaos titulo en ingles
                    tbEncargadoimpi.Text = validareader("CasoEncargadoExterno", "CasoId", respuestastring3).Text;
                    rtbMotivo.Text = validareader("CasoMotivo", "CasoId", respuestastring3).Text; //20220329FSV Agremaos campo
                    rtbObservaciones.Text = validareader("CasoObservaciones", "CasoId", respuestastring3).Text; //20220329FSV Agremaos campo

                    //No se muestran
                    tbParte.Text = validareader("ParteRepresentadaId", "CasoId", respuestastring3).Text;
                    tbSentido.Text = validareader("SentidoResolucionId", "CasoId", respuestastring3).Text;



                    //Datos de Pais
                    conect conexcounty = new conect();
                    String sIdpais = validareader("PaisId", "CasoId", respuestastring3).Text;
                    if (sIdpais != "")
                    {
                        MySqlDataReader respuestaPais = conexcounty.getdatareader("select * from pais where PaisId = " + sIdpais);
                        while (respuestaPais.Read())
                        {
                            //lPais.Text = validareader("PaisNombre", "PaisId", respuestaPais).Text;
                            tbPaisHeader.Text = validareader("PaisNombre", "PaisId", respuestaPais).Text;
                        }
                        respuestaPais.Close();
                        conexcounty.Cerrarconexion();
                    }


                    //Datos de Cliente de casocliente - falta filtro por tipo solicitus
                    //String sClienteid = "";
                    //MySqlDataReader respuestaCliente = con.getdatareader("Select * from casocliente, cliente where casocliente.CasoId = " + validareader("CasoId", "CasoId", respuestastring3).Text + " and cliente.clienteid =  casocliente.clienteid;");
                    conect conexcostumer = new conect();
                    String squerycliente = "Select * from casocliente, cliente where " +
                                            " casocliente.CasoId = " + validareader("CasoId", "CasoId", respuestastring3).Text +
                                            " and casocliente.TipoSolicitudId = " + gSTipoSolicitudId +
                                            " and cliente.clienteid =  casocliente.clienteid;";
                    String sClienteid = "";
                    MySqlDataReader respuestaCliente = conexcostumer.getdatareader(squerycliente);
                    while (respuestaCliente.Read())
                    {
                        //lCliente.Text = validareader("NombreUtilClient", "ClienteId", respuestaCliente).Text;
                        tbClienteHeader.Text = validareader("NombreUtilClient", "ClienteId", respuestaCliente).Text;
                        sClienteid = validareader("ClienteId", "ClienteId", respuestaCliente).Text;

                        //20220303 Fsalazar mandamos el numero de cliente
                        gSclienteid = sClienteid;
                        //20220303 Fsalazar Fin de modificación
                    }
                    respuestaCliente.Close();
                    conexcostumer.Cerrarconexion();
                    //consultamos al contacto del cliente



                    //20220408FSV Agregamos Interesado o Titular -  - considera idacaso y tipo de solicitud
                    conect conextitular = new conect();
                    String sQryTitular = "SELECT casointeresado.CasoId,NombreUtilInt " +
                                                "FROM casointeresado RIGHT JOIN interesado   " +
                                                "ON casointeresado.InteresadoID=interesado.InteresadoID   " +
                                                "WHERE casointeresado.TipoSolicitudId = " + gSTipoSolicitudId + 
                                                " AND casointeresado.CasoId = " + sCasoidgenera + ";";
                    MySqlDataReader respuestaTitular = conextitular.getdatareader(sQryTitular);
                    while (respuestaTitular.Read())
                    {
                        tbTitularHeader.Text = validareader("NombreUtilInt", "CasoId", respuestaTitular).Text;
                    }
                    respuestaTitular.Close();
                    conextitular.Cerrarconexion();
                    //20220311FSV Fin Titular



                    //20220311FSV Agregamos Corresponsal - considera idacaso y tipo de solicitud
                    conect conexcorresponsal = new conect();
                    String sQryCorresponsal = "SELECT caso_consulta.CasoId, " +
                                                "cliente.NombreUtilClient AS ClienteCorresponsal,   " +
                                                "DAMEALCONTACTOCASO(casocorresponsal.ContactoId) AS ContactoCorresponsal,   " +
                                                "cliente.ClienteEmail AS CorreoCorresponsal " +
                                                "FROM caso_consulta, casocorresponsal, cliente " +
                                                "WHERE caso_consulta.CasoId = casocorresponsal.CasoId " +
                                                "AND casocorresponsal.ClienteId = cliente.ClienteId " +
                                                "AND casocorresponsal.CasoId  = " + sCasoidgenera +
                                                " AND casocorresponsal.TipoSolicitudId  = '" + gSTipoSolicitudId + "';";
                    MySqlDataReader respuestaCorresponsal = conexcorresponsal.getdatareader(sQryCorresponsal);
                    while (respuestaCorresponsal.Read())
                    {
                        //tbCorresponsalHeader.Text = validareader("ClienteCorresponsal", "CasoId", respuestaCorresponsal).Text;
                        //tbContactoCorresponsalHeader.Text = validareader("ContactoCorresponsal", "CasoId", respuestaCorresponsal).Text;
                        tbCorresponsalHeader.Text = validareader("ClienteCorresponsal", "CasoId", respuestaCorresponsal).Text;
                        richTextBox4.Text = validareader("CorreoCorresponsal", "CasoId", respuestaCorresponsal).Text;
                        tbContactoCorresponsalHeader.Text = validareader("ContactoCorresponsal", "CasoId", respuestaCorresponsal).Text;
                    }
                    respuestaCorresponsal.Close();
                    conexcorresponsal.Cerrarconexion();
                    //20220311FSV Fin Corresponsal


                    //Leemos el contacto - no necesita el tipo de solicitud
                    conect conexcontacto = new conect();
                    if (sClienteid != "")
                    {
                        MySqlDataReader respuestaContacto = conexcontacto.getdatareader("select * from contacto where Clienteid = " + sClienteid);
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


                    //Leemos el usuario
                    conect conexusuario = new conect();
                    String idUsuario = validareader("UsuarioId", "CasoId", respuestastring3).Text;
                    if (idUsuario != "")
                    {
                        MySqlDataReader respuestaUser = conexusuario.getdatareader("select * from usuario where UsuarioId = " + idUsuario);
                        while (respuestaUser.Read())
                        {
                            //lResponsable.Text = validareader("UsuarioNombre", "UsuarioId", respuestaUser).Text;
                            tbResponsableHeader.Text = validareader("UsuarioNombre", "UsuarioId", respuestaUser).Text;
                        }
                        respuestaUser.Close();
                        conexusuario.Cerrarconexion();
                    }


                    //Leemos la Referencia, aqui falta agregar el tipo de solicitud
                    //String sReferencia = validareader("CasoId", "CasoId", respuestastring3).Text;
                    try {
                            conect con_referencia = new conect();
                            String sQryReferencia = "select * from referencia where CasoId = " + objfuncionesdicss.validareader("CasoId", "CasoId", respuestastring3).Text + " and TipoSolicitudId= " + objfuncionesdicss.validareader("TipoSolicitudId", "TipoSolicitudId", respuestastring3).Text + " ; ";
                            MySqlDataReader respuestaReferencia = con_referencia.getdatareader(sQryReferencia);
                            while (respuestaReferencia.Read())
                            {
                                //lReferencia.Text = validareader("ReferenciaNombre", "referenciaid", respuestaReferencia).Text;
                                tbReferenciaHeader.Text = validareader("ReferenciaNombre", "referenciaid", respuestaReferencia).Text;
                            }
                            respuestaReferencia.Close();
                        con_referencia.Cerrarconexion();
                        }
                    catch (Exception Exsreferencia)
                        {
                               new filelog("Consultamos la referencia", Exsreferencia.StackTrace.ToString());
                        }


                    //SECCION DATOS GENRALES

                    //Estatus caso (REVISAR EL GRUPO)
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
                    //subtiposolicitudoid



                    //SUB Tipo solicitud ID
                    conect conexsubtiposol = new conect();
                    String sSubTipoSolicitudId = validareader("SubTipoSolicitudId", "CasoId", respuestastring3).Text;
                    if (sSubTipoSolicitudId != "")
                    {
                        MySqlDataReader respuestasubTiposol = conexsubtiposol.getdatareader("select * from tipoconsulta where TipoConsultaId = " + sSubTipoSolicitudId);
                        while (respuestasubTiposol.Read())
                        {
                            tbAcciones.Text = validareader("tipoconsultacolDescripEsp", "TipoConsultaId", respuestasubTiposol).Text;
                        }
                        respuestasubTiposol.Close();
                        conexsubtiposol.Cerrarconexion();
                    }


                    try
                    {
                        conect con_idiomas2 = new conect();
                        String sIdiomas2 = "select * from idioma";
                        MySqlDataReader resp_idioma2 = con_idiomas2.getdatareader(sIdiomas2);
                        while (resp_idioma2.Read())
                        {
                            ComboboxItem prueba = objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2);
                            cbIdioma.Items.Add(objfuncionesdicss.validareader("IdiomaDescripcion", "idiomaId", resp_idioma2));//consultar idioma
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
                            resp_idioma.Close();
                            con_idioma.Cerrarconexion();
                        }
                    }
                    catch (Exception ExsIdioma)
                    {
                        new filelog("Consultamos idiomas linea 492", ExsIdioma.StackTrace.ToString());
                    }





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
                    //carga_documentos_IMPI(sCasoId, gSTipoSolicitudId);
                    //20220328FSV




                    //Carga Facturas - Revisar si diferencia entre tipo de solicitud
                    //20220406 Carga Facturas
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
                    //20220406FSV Fin Carga Facturas



                    consultamosdocumentoselectronicos();


                    icontgeneradom++;
                    //icontadorbusqueda = 0;
                }

                //Cerramos las conexiones
                respuestastring3.Close();
                conexquery.Cerrarconexion();
                
                this.Enabled = true;
                

            }
            catch (Exception E)
            {
                MessageBox.Show("No se ecnontraron más casos");
                //progressBar1.Value = 100;
                //progressBar1.Hide();
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

        }

        public void upcatescampos()
        {

            try
            {
                if (bTitulo_update || FechaPendiente_update || btituloidiomaoriginal_update || bExpediente_update || bFechaCarta_update || bClienteduedate_update || bFechaPresentacion_update || bFechaConclusion_update || bEncargadoExterno_update || bMotivo_update || bObservaciones_update)
                {
                    String sUpdateset = "";

                    if (bExpediente_update)
                    {
                        sUpdateset += ", CasoNumeroExpedienteLargo = '" + tbExpediente.Text + "'";
                    }
                    
                    //Actualización de Fechas
                    if (bFechaCarta_update)
                    {
                        sUpdateset += ", CasoFechaCartaCliente =  STR_TO_DATE('" + tbFechaCarta.Text + "', '%d-%m-%Y')";
                    }
                    if (bClienteduedate_update)
                    {
                        sUpdateset += ", CasoFechaFilingCliente = STR_TO_DATE('" + tbClientduedate.Text + "', '%d-%m-%Y')";
                    }
                    if (bFechaPresentacion_update)
                    {
                        sUpdateset += ", CasoFechaRecepcion = STR_TO_DATE('" + tbFechaPresentacion.Text + "', '%d-%m-%Y')";
                    }
                    if (bFechaConclusion_update)
                    {
                        sUpdateset += ", CasoFechaResolucion = STR_TO_DATE('" + tbFechaConclusion.Text + "', '%d-%m-%Y')";
                    }
                    if (FechaPendiente_update)
                    {
                        sUpdateset += ", FechaPendiente = STR_TO_DATE('" + FechaPendiente.Text + "', '%d-%m-%Y')";
                    }

                    //Seccion Datos Generales
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
                        sUpdateset += ", CasoMotivo = '" + rtbMotivo.Text + "'";
                    }
                    if (bObservaciones_update)
                    {
                        sUpdateset += ", CasoObservaciones = '" + rtbObservaciones.Text + "'";
                    }




                    conect con = new conect();
                    String sIdsconsultas = "UPDATE `caso_consulta` SET `CasoTituloespanol` = '" + rtTitulo.Text + "' " + sUpdateset +
                        " WHERE `caso_consulta`.`CasoId` = " + sCasoId + " AND `caso_consulta`.`TipoSolicitudId` = " + gSTipoSolicitudId;


                    MySqlDataReader resp_numpatentes = con.getdatareader(sIdsconsultas);
                    resp_numpatentes.Read();

                    if (bTitulo_update)
                    {
                        tbTituloHeader.Text = rtTitulo.Text;
                    }

                    if (bExpediente_update)
                    {
                        tbExpedienteHeader.Text= tbExpediente.Text;
                    }


                    if (resp_numpatentes == null)
                    {
                        MessageBox.Show("Error al hacer el update. " + sIdsconsultas);
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

            bExpediente_update = false;
            bFechaConcesion_update = false;

            bFechaCarta_update = false;
            bClienteduedate_update = false;
            bFechaPresentacion_update = false;
            bFechaConclusion_update = false;
            FechaPendiente_update = false;

            bTitulo_update = false;
            btituloidiomaoriginal_update = false;
            bEncargadoExterno_update = false;
            bMotivo_update = false;
            bObservaciones_update = false;

            //bTituloIngles_update = false;

            cbIdioma.Items.Clear();
            cbIdioma.Text = "";


        }




        private void button39_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (bTitulo_update || btituloidiomaoriginal_update || bExpediente_update || bFechaCarta_update || bClienteduedate_update || bFechaPresentacion_update || bEncargadoExterno_update || bMotivo_update || bObservaciones_update)
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
                if (bTitulo_update || btituloidiomaoriginal_update || bExpediente_update || bFechaCarta_update || bClienteduedate_update || bFechaPresentacion_update || bEncargadoExterno_update || bMotivo_update || bObservaciones_update)
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


}

        private void button2_Click(object sender, EventArgs e)
        {
            //20220318FSV Regresar al form de búsqueda
            //consultacaso.Focus();
            //consultacaso.Show();
            consultarcasoconsulta.Show();


            this.Close();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            String valorcarta = cbCartas.Text;
            if (cbCartas.SelectedItem == null)
            {
                MessageBox.Show("Debes Seleccionar un tiposolicitd");
            }
            else
            {
                //generacartas prueba = new generacartas();
                ////valuecob = (CB_tiposolicitudgeneracartas.SelectedItem as ComboboxItem).Value.ToString();//numero de tiposolicitud
                //prueba.generacartass(sCasoId, sTipogrupoglobal, valorcarta);//casoId, tiposolicitud, nombre carta
                //MessageBox.Show("Se ah generado Correctamente");

            }
        }

        private void fTcasoconsulta_FormClosing(object sender, FormClosingEventArgs e)
        {
            //consultacaso.Focus();
            consultarcasoconsulta.Show();

        }

        private void label19_MouseMove(object sender, MouseEventArgs e)
        {
            label19.BackColor = System.Drawing.Color.OrangeRed;
        }

        private void label19_MouseLeave(object sender, EventArgs e)
        {
            label19.BackColor = Color.Orange;
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
            label21.BackColor = System.Drawing.Color.OrangeRed;
        }

        private void label21_MouseLeave(object sender, EventArgs e)
        {
            label21.BackColor = Color.Orange;
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
            label27.BackColor = System.Drawing.Color.OrangeRed;
        }

        private void label27_MouseLeave(object sender, EventArgs e)
        {
            label27.BackColor = Color.Orange;
        }

        private void label8_MouseMove(object sender, MouseEventArgs e)
        {
            //label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(181)))), ((int)(((byte)(1)))));
            label8.BackColor = Color.OrangeRed;
        }

        private void label8_MouseLeave(object sender, EventArgs e)
        {
            label8.BackColor = Color.Orange;
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
            label36.BackColor = Color.Orange;
        }

        private void label36_MouseMove(object sender, MouseEventArgs e)
        {
            label36.BackColor = System.Drawing.Color.OrangeRed;
        }

        private void label36_DoubleClick(object sender, EventArgs e)
        {
            
            fResponsableupdate updateResponsable = new fResponsableupdate("1");
            if (updateResponsable.ShowDialog() == DialogResult.OK)
            {
                String value = updateResponsable.sValueResponsable;
                String texti = updateResponsable.sTextoResponsable;

                conect con_4 = new conect();
                String updateresponsableidcaso = "UPDATE `caso_consulta` SET `ResponsableId` = '" + value + "', `UsuarioId` = '" + value +
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
                    String[] sfilesss = obj.sFileupload.Split('\\');

                    string ruta = "\\\\" + sfilesss[2] + "\\" + sfilesss[3] + "\\" + sCarpetadocumentos + "\\" + gSCasoNumero;// + "\\edocs";
                    //string ruta = obj.sFileupload + "\\" + sCarpetadocumentos + "\\" + gSCasoNumero + "\\edocs";
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
            CapturaSolicitud addescrito = new CapturaSolicitud(fCapuraform, loguin, "1", sCasoId);
            if (addescrito.ShowDialog() == DialogResult.OK)
            {
                generadom(sCasoId);
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            CapturaEscritos addescrito = new CapturaEscritos(fCapuraform, loguin, "1", sCasoId);
            if (addescrito.ShowDialog() == DialogResult.OK)
            {
                generadom(sCasoId);
            }
        }

        private void button42_Click(object sender, EventArgs e)
        {

        }

        private void button43_Click(object sender, EventArgs e)
        {
            Capturadetitulo addtitulo = new Capturadetitulo(fCapuraform, loguin, "1", sCasoId);
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
            string message = "Se actualizarán los datos del Caso Consulta ¿Desea Continuar?";
            string caption = "Caso Consulta";
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

        private void tbFechaconcesion_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbFechaconcesion_Validating(object sender, CancelEventArgs e)
        {

        }

        private void tbFechaconcesion_ModifiedChanged(object sender, EventArgs e)
        {

        }

        private void tbFechaPresentacion_KeyPress(object sender, KeyPressEventArgs e)
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
            if (tbFechaPresentacion.Text.Length == 2)
            {
                tbFechaPresentacion.Text = tbFechaPresentacion.Text + "-";
                tbFechaPresentacion.SelectionStart = tbFechaPresentacion.Text.Length;
            }
            if (tbFechaPresentacion.Text.Length == 5)
            {
                tbFechaPresentacion.Text = tbFechaPresentacion.Text + "-";
                tbFechaPresentacion.SelectionStart = tbFechaPresentacion.Text.Length;
            }
        }

        private void tbFechaPresentacion_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechaPresentacion);
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

        private void tbFechaPresentacion_ModifiedChanged(object sender, EventArgs e)
        {
            bFechaPresentacion_update = true;
        }

        private void tbFechaCarta_KeyPress(object sender, KeyPressEventArgs e)
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
            if (tbFechaCarta.Text.Length == 2)
            {
                tbFechaCarta.Text = tbFechaCarta.Text + "-";
                tbFechaCarta.SelectionStart = tbFechaCarta.Text.Length;
            }
            if (tbFechaCarta.Text.Length == 5)
            {
                tbFechaCarta.Text = tbFechaCarta.Text + "-";
                tbFechaCarta.SelectionStart = tbFechaCarta.Text.Length;
            }
        }

        private void tbFechaCarta_ModifiedChanged(object sender, EventArgs e)
        {
            bFechaCarta_update = true;
        }

        private void tbFechaCarta_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechaCarta);
        }

        private void rtbMotivo_ModifiedChanged(object sender, EventArgs e)
        {
            bMotivo_update = true;
        }

        private void label29_DoubleClick(object sender, EventArgs e)
        {
            
            fTitularactualiza actualizarTitular = new fTitularactualiza("1");
            if (actualizarTitular.ShowDialog() == DialogResult.OK)
            {
                String value = actualizarTitular.sValueTitular;
                String texti = actualizarTitular.sTextoTitular;
                conect con_4 = new conect();
                //String actualizarTitularidcaso = "UPDATE `caso_consulta` SET `ResponsableId` = '" + value + "', `UsuarioId` = '" + value +
                //                                "' WHERE (`CasoId` = '" + sCasoId + "' and TipoSolicitudId ='" + gSTipoSolicitudId + "');";
                
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

        private void label29_MouseLeave(object sender, EventArgs e)
        {
            label29.BackColor = Color.Orange;
        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void label29_MouseMove(object sender, MouseEventArgs e)
        {
            label29.BackColor = System.Drawing.Color.OrangeRed;
        }

        private void label33_MouseLeave(object sender, EventArgs e)
        {
            label33.BackColor = Color.Orange;
        }

        private void label33_DoubleClick(object sender, EventArgs e)
        {
            //fCorresponsalactualiza actualizarCorresponsal = new fCorresponsalactualiza("1");
            //if (actualizarCorresponsal.ShowDialog() == DialogResult.OK)
            //{
            //    String value = actualizarCorresponsal.sValueCorresponsal;
            //    String texti = actualizarCorresponsal.sTextoCorresponsal;
            //    conect con_4 = new conect();
            //    //String actualizarTitularidcaso = "UPDATE `caso_consulta` SET `ResponsableId` = '" + value + "', `UsuarioId` = '" + value +
            //    //                                "' WHERE (`CasoId` = '" + sCasoId + "' and TipoSolicitudId ='" + gSTipoSolicitudId + "');";

            //    String actualizarCorresponsalidcaso = "UPDATE casocorresponsal SET ContactoID = '" + value + "'" +
            //                                    " WHERE (`CasoId` = '" + sCasoId + "' and ClienteId ='" + gSclienteid + "');";

            //    MySqlDataReader resp_updateResp = con_4.getdatareader(actualizarCorresponsalidcaso);
            //    if (resp_updateResp != null)
            //    {
            //        //lResponsable.Text = texti;
            //        //tbResponsableHeader.Text = texti;
            //        //tbTitularHeader.Text = texti;
            //        tbCorresponsalHeader.Text = texti;
            //    }
            //    resp_updateResp.Close();
            //    con_4.Cerrarconexion();
            //}


            fBuscarcorresponsalCC bForm = new fBuscarcorresponsalCC(sCasoId, gSTipoSolicitudId, tbContactoCorresponsalHeader.Text, tbCorresponsalHeader.Text);
            if (bForm.ShowDialog() == DialogResult.OK)
            {
                tbCorresponsalHeader.Text = bForm.sClienteidtext;
                tbContactoCorresponsalHeader.Text = bForm.sContactoidtext;
                gSclienteid = bForm.sClienteidvalue;
                richTextBox4.Text = bForm.rtCorreocontacto_pass;
            }


        }

        private void label33_MouseMove(object sender, MouseEventArgs e)
        {
            label33.BackColor = System.Drawing.Color.OrangeRed;
        }

        private void label29_Click_1(object sender, EventArgs e)
        {

        }

        private void tbClientduedate_Leave(object sender, EventArgs e)
        {
            tbClientduedate.Text = tbClientduedate.Text.Replace("/","-").Replace(".","-");
        }

        private void tbFechaCarta_Leave(object sender, EventArgs e)
        {
            tbFechaCarta.Text = tbFechaCarta.Text.Replace("/", "-").Replace(".", "-");
        }

        private void tbFechaPresentacion_Leave(object sender, EventArgs e)
        {
            tbFechaPresentacion.Text = tbFechaPresentacion.Text.Replace("/", "-").Replace(".", "-");
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void tbFechaConclusion_ModifiedChanged(object sender, EventArgs e)
        {
            bFechaConclusion_update = true;
        }
        private void tbFechaPendiente_ModifiedChanged(object sender, EventArgs e)
        {
            FechaPendiente_update = true;
        }

        private void tbFechaConclusion_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechaConclusion);
        }

        private void tbFechaConclusion_KeyPress(object sender, KeyPressEventArgs e)
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
            if (tbFechaConclusion.Text.Length == 2)
            {
                tbFechaConclusion.Text = tbFechaConclusion.Text + "-";
                tbFechaConclusion.SelectionStart = tbFechaConclusion.Text.Length;
            }
            if (tbFechaConclusion.Text.Length == 5)
            {
                tbFechaConclusion.Text = tbFechaConclusion.Text + "-";
                tbFechaConclusion.SelectionStart = tbFechaConclusion.Text.Length;
            }
        }

        private void tbFechaConclusion_Leave(object sender, EventArgs e)
        {
            tbFechaConclusion.Text = tbFechaConclusion.Text.Replace("/", "-").Replace(".", "-");
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
                MessageBox.Show(Ex.Message + " " + sRuta);
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
                    con_.Cerrarconexion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe seleccionar un registro");
                new filelog("Error eliminar documento", " Error: " + ex.StackTrace);

            }
        }
    }
}
