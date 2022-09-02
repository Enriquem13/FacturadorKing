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
//20220520 Agregamos ref para forma de plazos
using Facturador.plazos_forms;
using System.Globalization;
using SpreadsheetLight;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace Facturador
{
    public partial class fToposiciones : Form
    {
        public Form1 loguin;
        public captura captura;

        //Temporal parametro formulario
        //public bOposicion consultacaso;
        public String gSTipoSolicitudId = "";
        public String gSclienteid = "";
        public String gSContactoid = "";
        public String gSCasoNumero = "";
        funcionesdicss objfuncionesdicss = new funcionesdicss();
        public String sCarpetadocumentos = "Edocs\\Oposiciones";


        public String sCasoId;
        public String sCasomarcasoriginal = "";
        public int iIndiceids_global = 0;
        public String[] sArrayids;
        public Image obj = null;
        //Temporal parametro formulario
        //public fToposiciones(Form1 loguinp, captura cap, bOposicion consul, String sCasoIdcaso)
        public fToposiciones(Form1 loguinp, captura cap, String sCasoIdcaso)
        {
            loguin = loguinp;
            captura = cap;

            //Temporal Parametro Formulario
            //consultacaso = consul;


            sCasoId = sCasoIdcaso;
            InitializeComponent();
            //empieza +
            conect con = new conect();
            String sIds = "select count(*) as numpatentes from caso_oposicion";
            MySqlDataReader resp_numids = con.getdatareader(sIds);
            resp_numids.Read();
            String sNumerodeids = validareader("numpatentes", "numpatentes", resp_numids).Text;
            resp_numids.Close();
            int iNumerogrupoids = System.Convert.ToInt32(sNumerodeids);
            sArrayids = new String[iNumerogrupoids];


            String sGetids = "select * from caso_oposicion";
            MySqlDataReader resp_getids = con.getdatareader(sGetids);
            int iIndiceids = 0;
            while (resp_getids.Read())
            {
                sArrayids[iIndiceids] = validareader("Casoid", "Casoid", resp_getids).Text;
                iIndiceids++;
            }
            resp_getids.Close();
            iIndiceids_global = Array.IndexOf(sArrayids, sCasoId);
            
            //terminaciclo caso_marcas
            limpiarcontenido();

            //Llenamos el combo de tipo de marca imitadora
            cbTipoMarcaImitadora.Items.Clear();
            conect con_tipomarcas = new conect();
            String query = "Select * from tipomarca order by TipoMarcaDescrip;";
            MySqlDataReader respuestastringtdm = con_tipomarcas.getdatareader(query);
            while (respuestastringtdm.Read())
            {
                cbTipoMarcaImitadora.Items.Add(validareader("TipoMarcaDescrip", "TipoMarcaId", respuestastringtdm));
            }
            respuestastringtdm.Close();
            con_tipomarcas.Cerrarconexion();


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

        private void button3_Click(object sender, EventArgs e)
        {
            loguin.Close();
            captura.Close();

            //Temporal parametro formulario
            //consultacaso.Close();

            this.Close();
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

        private void button39_Click(object sender, EventArgs e)
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

        public void limpiarcontenido()
        {
            //tbExpediente.Text = "";
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
            tbReferencia.Text = "";
            rtCorreocontacto.Text = "";
            tbExpedienteDG.Text = "";
            cbTipoMarcaImitadora.Text = "";
            /*cbClasemarca.Text = "";
            tbclase.Text = "";
            lvProductos.Items.Clear();*/
        }
        public void consultaimagen(PictureBox pBox,  String slCasoid, String glSTipoSolicitudId){
            //Cargar logo si existe
            // consultamos la marca logo
            try
            {
                configuracionfiles objfile = new configuracionfiles();
                objfile.configuracionfilesinicio();
                String sFileexist = objfile.sFileupload + @"\logos_marcas\0" + slCasoid + @"\0" + slCasoid + ".gif";
                if (File.Exists(sFileexist))
                {
                    //aqui buscamos el logo si existe en la carpeta y lo insertamos
                    //y preguntamos si ya existe en la base para agregarlo
                    int icount = 0;

                    conect con_imglogo = new conect();
                    String simglogo = "select count(*) As num from imagen_logo where casoid = " + slCasoid + " and TipoSolicitudId = " + glSTipoSolicitudId + ";";
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

                        String sRutaInsert = objfile.sFileupload + @"\logos_marcas\0" + slCasoid + @"\0" + slCasoid + "_" + sDatetime + ".gif";
                        System.IO.File.Copy(sFileexist, sRutaInsert, true);
                        System.IO.File.Delete(sFileexist);
                        conect con_insert_imglogo = new conect();
                        String simglogo_insert = "INSERT INTO `imagen_logo`(`ruta`,`casoid`,`TipoSolicitudId`,`fecha`)VALUES('" + sRutaInsert.Replace("\\", "\\\\") + "','" + slCasoid + "','" + glSTipoSolicitudId + "',now());" + ";";
                        MySqlDataReader resp_insert_imglogo = con_insert_imglogo.getdatareader(simglogo_insert);
                        if (resp_insert_imglogo.RecordsAffected > 0)
                        {//quiere decir que hicimos el insert correctamente
                            obj = Image.FromFile(sRutaInsert);
                            pBox.Image = obj;
                        }
                        resp_insert_imglogo.Close();
                        con_insert_imglogo.Cerrarconexion();
                    }
                    else
                    {//si tiene más de uno lo consultamos y lo colocamos en el picturebox

                        String simglogo_consulta = "select * from imagen_logo where casoid = " + slCasoid + " and TipoSolicitudId = " + glSTipoSolicitudId + " order by fecha desc limit 1;";
                        conect con_consul_imglogo = new conect();
                        MySqlDataReader resp_consul_imglogo = con_consul_imglogo.getdatareader(simglogo_consulta);
                        if (resp_consul_imglogo.Read())
                        {//quiere decir que hicimos el insert correctamente
                            String sRutaactual = objfuncionesdicss.validareader("ruta", "ruta", resp_consul_imglogo).Text;
                            obj = Image.FromFile(sRutaactual); ;//
                            pBox.Image = obj;
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
                    String simglogo = "select count(*) As num from imagen_logo where casoid = " + slCasoid + " and TipoSolicitudId = " + glSTipoSolicitudId + ";";
                    MySqlDataReader resp_imglogo = con_imglogo.getdatareader(simglogo);
                    while (resp_imglogo.Read())
                    {
                        icount = int.Parse(objfuncionesdicss.validareader("num", "num", resp_imglogo).Text);// objfuncionesdicss.validareader("num", "num", resp_imglogo));//consultar idioma
                    }
                    resp_imglogo.Close();
                    con_imglogo.Cerrarconexion();

                    if (icount > 0)
                    {//si la consulta arrojo algun resultado colocamos esa ruta
                        String simglogo_consulta = "select * from imagen_logo where casoid = " + slCasoid + " and TipoSolicitudId = " + glSTipoSolicitudId + " order by fecha desc limit 1;";
                        conect con_consul_imglogo = new conect();
                        MySqlDataReader resp_consul_imglogo = con_consul_imglogo.getdatareader(simglogo_consulta);
                        while (resp_consul_imglogo.Read())
                        {//quiere decir que hicimos el insert correctamente
                            String sRutaactual = objfuncionesdicss.validareader("ruta", "ruta", resp_consul_imglogo).Text;
                            obj = Image.FromFile(sRutaactual); ;//
                            pBox.Image = obj;
                        }
                        resp_consul_imglogo.Close();
                        con_consul_imglogo.Cerrarconexion();
                    }
                    else
                    {//si no tiene resultados entonces no hay imagen para ésta Marca
                        //pbImagenImitadora.Image = null;
                        pBox.Image = null;
                    }
                }
            }
            catch (Exception exs)
            {
                new filelog("Al cargar logo", " :" + exs.Message);
            }
        }
        public void generadom(String sCasoidgenera)
        {
            try
            {
                limpiarcontenido();
                sCasoId = sCasoidgenera;


                //20220520 Activar el botón agregar plazo
                if (loguin.sUsuarioCodigo == "1" || loguin.sUsuarioCodigo == "3")
                {
                    bAgregarplazo.Visible = true;
                }


                
                //configuracionfiles objfile2 = new configuracionfiles();
                //objfile2.configuracionfilesinicio();
                //String sFileexist2 = objfile2.sFileupload + @"\logos_marcas\OPO" + sCasoId + @"\OPO" + sCasoId + ".gif";
                //if (File.Exists(sFileexist2))
                //{
                //    pbImagenImitadora.Image = Image.FromFile(sFileexist2);
                //}
                //else
                //{
                //    pbImagenImitadora.Image = null;
                //}


                //lCasoID.Text = sCasoidgenera;
                conect con_oposiciones = new conect();
                this.Enabled = false;
                int icontgeneradom = 0;
                String sQuery = "SELECT CasoId, " +
                                " TipoSolicitudId," +//subtiposolicitudoid
                                " SubTipoSolicitudId," +//subtiposolicitudoid
                                " CasoNumeroExpedienteLargo,    CasoNumero,    ResponsableId," +
                                "  TipoMarcaId,  " +
                                " DATE_FORMAT(CasoFechaAlta , '%d-%m-%Y') as  CasoFechaAlta, " +
                                " CasoTipoCaptura,   " +
                                " CasoTitular,    " +
                                " MarcaImitadora,    " +
                                " NombreImitador,    " +
                                " CasoComentario,    " +
                                " DameEstatusCasoDescrip(EstatusCasoId) As Estatuscasodescrip,    " +
                                " DATE_FORMAT(CasoFechaFilingSistema , '%d-%m-%Y') as  CasoFechaFilingSistema,   " +
                                " DATE_FORMAT(CasoFechaFilingCliente , '%d-%m-%Y') as  CasoFechaFilingCliente,    " +
                                " DATE_FORMAT(CasoFechaCartaCliente , '%d-%m-%Y') as  CasoFechaCartaCliente,   " +
                                " DATE_FORMAT(CasoFechaPresentacion , '%d-%m-%Y') as  CasoFechaPresentacion,   " +
                                " EstatusCasoId,   " +
                                " UsuarioId,    " +
                                " CasoIdOriginal,    " +
                                " TipoSolicitudIdOriginal,    " +
                                " DATE_FORMAT(CasoFechaAlta , '%d-%m-%Y') as  CasoFechaAlta,   " +
                                " DATE_FORMAT(FecPublicacionImitadora , '%d-%m-%Y') as  FecPublicacionImitadora,   " +
                                " DATE_FORMAT(FecPresentacionImitadora , '%d-%m-%Y') as  FecPresentacionImitadora,   " +
                                " DATE_FORMAT(FecPresentacionOpocision , '%d-%m-%Y') as  FecPresentacionOpocision,   " +
                                " DATE_FORMAT(fechaplazovenceopocision , '%d-%m-%Y') as  fechaplazovenceopocision,   " +
                                " Get_Referencia(Casoid, TipoSolicitudId) as Referencia, " +
                                " PaisId,    " +
                                " Sentidoopocision,    " +
                                " DATE_FORMAT(CasoFechaConclusion , '%d-%m-%Y') as  CasoFechaConclusion, " +
                                " Clase    " +
                                " FROM    caso_oposicion " +
                                "WHERE   caso_oposicion.CasoId  = " + sCasoidgenera + "";

                MySqlDataReader respuestastring3 = con_oposiciones.getdatareader(sQuery);
                while (respuestastring3.Read())
                {
                    tbCasoOposicion.Text = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                    tbCasoIdOposicion.Text = sCasoidgenera;
                    gSCasoNumero = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                    gSTipoSolicitudId = validareader("TipoSolicitudId", "CasoId", respuestastring3).Text;
                    tbMarcaImitadora.Text = validareader("MarcaImitadora", "CasoId", respuestastring3).Text;
                    tbEstatus.Text = validareader("Estatuscasodescrip", "CasoId", respuestastring3).Text;
                    tbReferencia.Text= validareader("Referencia", "CasoId", respuestastring3).Text;
                    tbTitular.Text = validareader("CasoTitular", "CasoId", respuestastring3).Text;
                    tbExpedienteDG.Text = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                    tbDMarcaImitadora.Text= validareader("MarcaImitadora", "CasoId", respuestastring3).Text;
                    tbNombreImitador.Text = validareader("NombreImitador", "CasoId", respuestastring3).Text;
                    comboBoxClase.Text= validareader("Clase", "CasoId", respuestastring3).Text;
                    rtbComentarios.Text= validareader("CasoComentario", "CasoId", respuestastring3).Text;
                    tbDFecRecepcion.Text = validareader("CasoFechaAlta", "CasoId", respuestastring3).Text;
                    tbDFecPubImitadora.Text = validareader("FecPublicacionImitadora", "CasoId", respuestastring3).Text;
                    tbDFecPresImitadora.Text = validareader("fechaplazovenceopocision", "CasoId", respuestastring3).Text;
                    String sFechepresentacionoposicion = validareader("FecPresentacionOpocision", "CasoId", respuestastring3).Text;
                    tbSentidoopocision.Text = validareader("Sentidoopocision", "CasoId", respuestastring3).Text;
                    tbConcluyofecha.Text = validareader("CasoFechaConclusion", "CasoId", respuestastring3).Text;
                    string sCasoIdDefendido = validareader("CasoIdOriginal", "CasoId", respuestastring3).Text;
                    string sCasoIdopocision = validareader("CasoId", "CasoId", respuestastring3).Text;
                    string sTipoSolicitudIdOriginal = validareader("TipoSolicitudIdOriginal", "TipoSolicitudIdOriginal", respuestastring3).Text;
                    sCasomarcasoriginal = sCasoIdDefendido;
                    //TipoSolicitudIdOriginal
                    /*
                     cargamos dos imgenes las de el caso original y la del rival
                     */
                    consultaimagen(pbDimage, sCasoIdDefendido, sTipoSolicitudIdOriginal);//de marcas
                    consultaimagen(pbImagenImitadora, sCasoIdopocision, gSTipoSolicitudId);//imitadora gSTipoSolicitudId debería ser 14

                    if (sFechepresentacionoposicion == "00-00-0000") {
                        tbDFecPresOposicion.Text = "";
                    }else {
                        tbDFecPresOposicion.Text = sFechepresentacionoposicion;
                    }
                    
                    //Consultamos el pais si existe el paisid
                    String sIdpais = validareader("PaisId", "CasoId", respuestastring3).Text;
                    if (sIdpais != "")
                    {
                        conect con_country = new conect();
                        MySqlDataReader respuestaPais = con_country.getdatareader("select * from pais where PaisId = " + sIdpais);
                        while (respuestaPais.Read())
                        {
                            tbPais.Text= validareader("PaisNombre", "PaisId", respuestaPais).Text;
                        }
                        respuestaPais.Close();
                        con_country.Cerrarconexion();
                    }


                    //Consultamos al Cliente
                    conect con_cliente = new conect();
                    String squerycliente = "Select * from casocliente, cliente where " +
                        " casocliente.CasoId = " + validareader("CasoId", "CasoId", respuestastring3).Text +
                        " and casocliente.TipoSolicitudId = " + gSTipoSolicitudId +
                        " and cliente.clienteid =  casocliente.clienteid;";
                    String sClienteid = "";
                    MySqlDataReader respuestaCliente = con_cliente.getdatareader(squerycliente);
                    while (respuestaCliente.Read())
                    {
                        tbCliente.Text = validareader("ClienteNombre", "ClienteId", respuestaCliente).Text;
                        sClienteid = validareader("ClienteId", "ClienteId", respuestaCliente).Text;
                        gSContactoid = validareader("ContactoId", "ContactoId", respuestaCliente).Text;
                        gSclienteid = sClienteid;
                    }
                    respuestaCliente.Close();
                    con_cliente.Cerrarconexion();


                    //Consultamos al Contacto
                    if (sClienteid != "")
                    {
                        conect con_detalle_cliente = new conect();
                        MySqlDataReader respuestaContacto = con_detalle_cliente.getdatareader("select * from contacto where ContactoId = " + gSContactoid);
                        while (respuestaContacto.Read())
                        {
                            tbContacto.Text = validareader("ContactoNombre", "ContactoId", respuestaContacto).Text;
                            rtCorreocontacto.Text = validareader("ContactoEmail", "ContactoId", respuestaContacto).Text;
                            gSContactoid = validareader("contactoid", "contactoid", respuestaContacto).Text;//consultar idioma
                        }
                        respuestaContacto.Close();
                        con_detalle_cliente.Cerrarconexion();
                    }


                    //Corresponsal
                    String sQryCorresponsal = "SELECT caso_oposicion.CasoId, " +
                                                "cliente.ClienteNombre AS ClienteCorresponsal,   " +
                                                "DAMEALCONTACTOCASO(casocorresponsal.ContactoId) AS ContactoCorresponsal,   " +
                                                "cliente.ClienteEmail AS CorreoCorresponsal " +
                                                "FROM caso_oposicion, casocorresponsal, cliente " +
                                                "WHERE caso_oposicion.CasoId = casocorresponsal.CasoId " +
                                                "AND casocorresponsal.ClienteId = cliente.ClienteId " +
                                                "AND casocorresponsal.CasoId  = " + sCasoidgenera +
                                                " AND casocorresponsal.TipoSolicitudId  = '" + gSTipoSolicitudId + "';";
                    conect con_corresponsal = new conect();
                    MySqlDataReader respuestaCorresponsal = con_corresponsal.getdatareader(sQryCorresponsal);
                    while (respuestaCorresponsal.Read())
                    {
                        tbCorresponsal.Text = validareader("ClienteCorresponsal", "CasoId", respuestaCorresponsal).Text;
                        richTextBox4.Text = validareader("CorreoCorresponsal", "CasoId", respuestaCorresponsal).Text;
                        textBox8.Text = validareader("ContactoCorresponsal", "CasoId", respuestaCorresponsal).Text;
                    }
                    respuestaCorresponsal.Close();
                    con_corresponsal.Cerrarconexion();


                    //Consultamos al responsable
                    String idUsuario = validareader("ResponsableId", "CasoId", respuestastring3).Text;
                    if (idUsuario != "")
                    {
                        conect con_responsable = new conect();
                        MySqlDataReader respuestaUser = con_responsable.getdatareader("select * from usuario where UsuarioId = " + idUsuario);
                        while (respuestaUser.Read())
                        {
                            tbResponsable.Text= validareader("UsuarioNombre", "UsuarioId", respuestaUser).Text;
                        }
                        respuestaUser.Close();
                        con_responsable.Cerrarconexion();
                    }


                    //Tipo de Marca Caso Imitador
                    String sTipoMarcaId = validareader("TipoMarcaId", "CasoId", respuestastring3).Text;
                    if (sTipoMarcaId != "")
                    {
                        conect con_tipomarca = new conect();
                        MySqlDataReader respuestasubTipomarca = con_tipomarca.getdatareader("select * from tipomarca where TipoMarcaId = " + sTipoMarcaId);
                        while (respuestasubTipomarca.Read())
                        {
                            cbTipoMarcaImitadora.Text = validareader("TipoMarcaDescrip", "TipoMarcaId", respuestasubTipomarca).Text;
                        }
                        respuestasubTipomarca.Close();
                        con_tipomarca.Cerrarconexion();
                    }




                    //Datos de marca defendida
                    generadommarcas(sCasoIdDefendido);

                    //Desactivamos controles
                    sellacontroles();

                    //Llenamos la pestaña de plazos
                    consultaplazo_nuevos();

                    //Cargamos los documentos oposición IMPI
                    cargamos_documentoimpi_datagridview(sCasoidgenera, gSTipoSolicitudId);

                    //Cargamos los documentos electrónicos
                    consultamosdocumentoselectronicos();

                    icontgeneradom++;
                }
                
                //Termina el ciclo while y cerramos las conexiones
                respuestastring3.Close();
                con_oposiciones.Cerrarconexion();

                this.Enabled = true;

            }
            catch (Exception E)
            {
                MessageBox.Show("No se ecnontraron más casos");
                this.Enabled = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Temporal parametro formulario
            //consultacaso.Show();
            //captura.Show();
            //que nos muestre el formulario de busqueda
            bOposicion frmbusqueda = new bOposicion(5, captura, loguin);
            //captura.Show();
            frmbusqueda.Show();
            //fin de modificacion
           
            this.Close();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void fToposiciones_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Temporal parametro formulario
            //consultacaso.Show();
            //captura.Show();
            bOposicion frmbusqueda = new bOposicion(5, captura, loguin);
            //captura.Show();
            frmbusqueda.Show();
        }

        public void generadommarcas(String sCasoidgenera)
        {
            try
            {

                String sCasoIdmarca = sCasoidgenera;

                //Cargar logo original si existe
                //configuracionfiles objfile = new configuracionfiles();
                //objfile.configuracionfilesinicio();
                //String sFileexist = objfile.sFileupload + @"\logos_marcas\0" + sCasoIdmarca + @"\0" + sCasoIdmarca + ".gif";
                //if (File.Exists(sFileexist))
                //{
                //    pbDimage.Image = Image.FromFile(sFileexist);
                //}
                //else
                //{
                //    pbDimage.Image = null;
                //}


                conect con_casosmarcas = new conect();
                String sQuery = "SELECT " +
                                "    CasoId," +
                                "    TipoSolicitudId," +
                                "    SubTipoSolicitudId," +
                                "    CasoTituloespanol," +
                                "    CasoTituloingles," +
                                "    Get_IdiomaCliente(CasoId, TipoSolicitudId) As IdiomaId," +
                                "    DATE_FORMAT(CasoFechaConcesion , '%d-%m-%Y') as  CasoFechaConcesion," +
                                "    DATE_FORMAT(CasoFechaLegal , '%d-%m-%Y') as  CasoFechaLegal," +
                                "    DATE_FORMAT(CasoFechaRecepcion , '%d-%m-%Y') as  CasoFechaRecepcion," +
                                "    DATE_FORMAT(CasoFechaVigencia , '%d-%m-%Y') as  CasoFechaVigencia," +
                                
                                "    CasoNumeroExpedienteLargo," +
                                "    CasoNumero," +
                                "    ResponsableId," +
                                "    TipoMarcaId," +
                                "    CasoLeyendaNoReservable," +
                                "    DATE_FORMAT(CasoFechaAlta , '%d-%m-%Y') as  CasoFechaAlta," +
                                "    CasoTipoCaptura," +
                                "    CasoTitular," +
                                "    DATE_FORMAT(CasoFechaFilingSistema , '%d-%m-%Y') as  CasoFechaFilingSistema," +
                                "    DATE_FORMAT(CasoFechaFilingCliente , '%d-%m-%Y') as  CasoFechaFilingCliente," +
                                "    DATE_FORMAT(CasoFechaCartaCliente , '%d-%m-%Y') as  CasoFechaCartaCliente," +
                                "   Get_Interesados_tiposol(Casoid, TipoSolicitudId) as InteresadoNombre, " +
                                "    EstatusCasoId," +
                                "    UsuarioId," +
                                "    PaisId," +
                                "    numregistrointernacional," +
                                "    productoidiomageneral," +
                                "    DATE_FORMAT(Fecharegistrointernacional , '%d-%m-%Y') as Fecharegistrointernacional," +
                                " Get_Paisclave_marcas(Casoid) as PaisClave, " +
                                " Get_Referencia(Casoid, TipoSolicitudId) as referencia, " +
                                " Get_Clase_productos(Casoid) as CasoProductosClase, " +
                                "    DATE_FORMAT(CasoFechaPruebaUsoSig , '%d-%m-%Y') as  CasoFechaPruebaUsoSig," +
                                "    CasoNumConcedida," +
                                "    DATE_FORMAT(CasoFechaprobouso , '%d-%m-%Y') as  CasoFechaprobouso," +
                                "    DATE_FORMAT(CasoFechaDeclaUso , '%d-%m-%Y') as  CasoFechaDeclaUso," +
                                "    DATE_FORMAT(CasoFechainiciouso , '%d-%m-%Y') as  CasoFechainiciouso" +
                                " FROM" +
                                "    caso_marcas" +
                                " WHERE " +
                                "        caso_marcas.CasoId = '" + sCasoIdmarca + "'";
                MySqlDataReader respuestastring4 = con_casosmarcas.getdatareader(sQuery);
                while (respuestastring4.Read())
                {
                    
                    tbCasoIdOriginal.Text = validareader("CasoId", "CasoId", respuestastring4).Text;
                    tbCasoOriginal.Text = validareader("CasoNumero", "CasoId", respuestastring4).Text;
                    tbExpedienteOriginal.Text = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring4).Text;
                    tbRegistro.Text = validareader("CasoNumConcedida", "CasoId", respuestastring4).Text;
                    tbl_pais.Text = validareader("PaisClave", "CasoId", respuestastring4).Text;
                    tbMarcaOriginal.Text = validareader("CasoTituloingles", "CasoId", respuestastring4).Text;
                    tbClaseOriginal.Text = validareader("CasoProductosClase", "CasoId", respuestastring4).Text;
                    tbDfecharecepcion.Text = validafechasvacias(validareader("CasoFechaRecepcion", "CasoId", respuestastring4).Text);
                    tbDFechaconcesion.Text = validafechasvacias(validareader("CasoFechaConcesion", "CasoId", respuestastring4).Text);
                    tbDFechacarta.Text = validafechasvacias(validareader("CasoFechaCartaCliente", "CasoId", respuestastring4).Text);
                    tbDFechainiciouso.Text = validafechasvacias(validareader("CasoFechainiciouso", "CasoId", respuestastring4).Text);
                    tbDFechavigencia.Text = validafechasvacias(validareader("CasoFechaVigencia", "CasoId", respuestastring4).Text);

                    //Consultamos el Tipo de Marca
                    String sTipoMarcaId = validareader("TipoMarcaId", "CasoId", respuestastring4).Text;
                    if (sTipoMarcaId != "")
                    {
                        conect con_tipomarca = new conect();
                        MySqlDataReader respuestasubTipomarca = con_tipomarca.getdatareader("select * from tipomarca where TipoMarcaId = " + sTipoMarcaId);
                        while (respuestasubTipomarca.Read())
                        {
                            tbTipoMarca.Text = validareader("TipoMarcaDescrip", "TipoMarcaId", respuestasubTipomarca).Text;
                        }
                        respuestasubTipomarca.Close();
                        con_tipomarca.Cerrarconexion();
                    }

                }

                respuestastring4.Close();
                con_casosmarcas.Cerrarconexion();

            }
            catch (Exception E)
            {
                new filelog(loguin.sId, E.ToString());
            }
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

        public void sellacontroles()
        {
            tbCasoOriginal.ReadOnly = true;
            tbCasoIdOriginal.ReadOnly = true;
            tbCasoOriginal.ReadOnly = true;
            tbMarcaOriginal.ReadOnly = true;
            tbExpedienteOriginal.ReadOnly = true;
            tbRegistro.ReadOnly = true;
            tbCasoOriginal.ReadOnly = true;
            tbl_pais.ReadOnly = true;
            tbDfecharecepcion.ReadOnly = true;
            tbDFechaconcesion.ReadOnly = true;
            tbDFechacarta.ReadOnly = true;
            tbDFechavigencia.ReadOnly = true;
            tbDFechainiciouso.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*Aquí debemos hacer el update para guardar la información modificada*/


            string message = "Se actualizarán los datos del Caso de Oposición ¿Desea Continuar?";
            string caption = "Consulta Caso Oposición";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {

                //Validaciones
                if (tbDMarcaImitadora.Text == "")
                {
                    MessageBox.Show("El campo Marca Imitadora no puede estar vacio.", "Consulta Caso Oposición");
                    tbDMarcaImitadora.Focus();
                    return;
                }

                if (cbTipoMarcaImitadora.SelectedIndex.Equals(-1))
                {
                    MessageBox.Show("El campo Tipo de Marca Imitadora no puede estar vacio.", "Consulta Caso Oposición");
                    cbTipoMarcaImitadora.Focus();
                    return;
                }

                if (tbNombreImitador.Text == "")
                {
                    MessageBox.Show("El campo Nombre del Imitador no puede estar vacio.", "Consulta Caso Oposición");
                    tbNombreImitador.Focus();
                    return;
                }

                if (comboBoxClase.SelectedIndex.Equals(-1))
                {
                    MessageBox.Show("El campo Clase no puede estar vacio.", "Consulta Caso Oposición");
                    comboBoxClase.Focus();
                    return;
                }

                if (tbExpedienteDG.Text == "")
                {
                    MessageBox.Show("El campo Expediente no puede estar vacio.", "Consulta Caso Oposición");
                    tbExpedienteDG.Focus();
                    return;
                }

                if (rtbComentarios.Text == "")
                {
                    MessageBox.Show("El campo Comentario no puede estar vacio.", "Consulta Caso Oposición");
                    rtbComentarios.Focus();
                    return;
                }

                if (tbDFecRecepcion.Text == "")
                {
                    MessageBox.Show("El campo Fecha de Recepción no puede estar vacio.", "Consulta Caso Oposición");
                    tbDFecRecepcion.Focus();
                    return;
                }

                if (tbDFecPubImitadora.Text == "")
                {
                    MessageBox.Show("El campo Plazo para Presentar Oposición a Marca Imitadora no puede estar vacio.", "Consulta Caso Oposición");
                    tbDFecPubImitadora.Focus();
                    return;
                }


                try
                {
                    String sTipomarca = "0";// (cbDTipomarca.SelectedItem as ComboboxItem).Value.ToString();
                    if (!(cbTipoMarcaImitadora.SelectedItem is null))
                    {
                        sTipomarca = (cbTipoMarcaImitadora.SelectedItem as ComboboxItem).Value.ToString();
                    }

                    String sCasoaModificar = tbCasoIdOposicion.Text;
                    String sUpdtaecaso_mascas = " UPDATE caso_oposicion SET  " +
                                                " MarcaImitadora = '" + tbDMarcaImitadora.Text + "', " +
                                                " NombreImitador = '" + tbNombreImitador.Text + "', " +
                                                " CasoNumeroExpedienteLargo = '" + tbExpedienteDG.Text + "', " +
                                                " Clase = '" + comboBoxClase.Text + "', " +
                                                " TipoMarcaId = '" + sTipomarca + "', " +
                                                " CasoComentario = '" + rtbComentarios.Text + "', " +
                                                " CasoFechaAlta = DATE(STR_TO_DATE('" + tbDFecRecepcion.Text + "', '%d-%m-%Y')), " +
                                                " CasoFechaRecepcion = DATE(STR_TO_DATE('" + tbDFecRecepcion.Text + "', '%d-%m-%Y')), " +
                                                " FecPublicacionImitadora = DATE(STR_TO_DATE('" + tbDFecPubImitadora.Text + "', '%d-%m-%Y')), " +
                                                " FecPresentacionImitadora = DATE(STR_TO_DATE('" + tbDFecPresImitadora.Text + "', '%d-%m-%Y')), " +
                                                " CasoFechaConclusion = DATE(STR_TO_DATE('" + tbConcluyofecha.Text + "', '%d-%m-%Y')), " +
                                                " Sentidoopocision = '"+ tbSentidoopocision .Text+ "'," +
                                                " FecPresentacionOpocision = DATE(STR_TO_DATE('" + tbDFecPresOposicion.Text + "', '%d-%m-%Y')) " +
                                                " WHERE CasoId = '" + sCasoaModificar + "' AND TipoSolicitudId = '" + gSTipoSolicitudId + "'; ";
                    conect con1 = new conect();
                    MySqlDataReader resp_query = con1.getdatareader(sUpdtaecaso_mascas);
                    if (resp_query.RecordsAffected > 0)
                    {
                        MessageBox.Show("Se modificó correctamente.");
                        generadom(sCasoId);
                    }
                    resp_query.Close();
                    con1.Cerrarconexion();

                    //resp_updatecaso.Close();
                    //con_4.Cerrarconexion();
                }
                catch (Exception Ex)
                {
                    new filelog("linea: 650  UPDATE caso_opsicion ", ": " + Ex.Message);
                }



            }



        }

        private void bAgregarplazo_Click(object sender, EventArgs e)
        {


            //AQUÍ abriremos una ventana para capturar los datos del plazo nuevo fecha estatus plazo  relacionado si es que existe etc ..
            try
            {
                agregaplazo obj = new agregaplazo(sCasoId, gSTipoSolicitudId, tbCasoOposicion.Text, loguin.sId, 5);//loguin.sId es el usuario de la sesion
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

        public void consultaplazo_nuevos()
        {
            try
            {
                conect con_tcon_edocs = new conect();
                String sConsultaplazos = " select * from plazo_general_vista " +
                                         "where casoid = " + sCasoId +
                                         " and TipoSolicitudId = " + gSTipoSolicitudId + " order by Plazos_detalleid";
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
                    dRows.Cells[10].Value = sValidafechavacia(sFecha_vencimiento_3m_impi);
                    dRows.Cells[11].Value = sValidafechavacia(sFecha_vencimiento_4m_impi);
                    dRows.Cells[12].Value = sValidafechavacia(sFecha_atendio_plazo_impi);
                    dRows.Cells[13].Value = satendio_plazoimpi;
                    dRows.Cells[14].Value = sDoc_atendio;
                    dRows.Cells[15].Value = sMotivo_cancelacion_plazo_impi;
                    dRows.Cells[16].Value = sValidafechavacia(sFecha_cancelacion_plazo_impi);
                    dRows.Cells[17].Value = sUsuariocancelo;
                    dgPlazos.Rows.Add(dRows);


                }
                con_tcon_edocs.Cerrarconexion();
                resp_tedocs.Close();

            }
            catch (Exception Ex)
            {
                new filelog("plazos_oposicion: ", "Error: " + Ex.Message);
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

        private void button46_Click(object sender, EventArgs e)
        {
            
            try
            {
                Fatenderplazo obj = new Fatenderplazo(sCasoId, gSTipoSolicitudId, loguin, tbCasoOposicion.Text);
                obj.ShowDialog();
                generadom(sCasoId);
            }
            catch (Exception Ex)
            {
                new filelog("linea: 850", "Error: " + Ex.Message);
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

        private void button44_Click(object sender, EventArgs e)
        {
            //CapturaSolicitud addescrito = new CapturaSolicitud(fCapuraform, loguin, "2", sCasoId);
            CapturaSolicitud addescrito = new CapturaSolicitud(captura, loguin, "5", sCasoId);
            if (addescrito.ShowDialog() == DialogResult.OK)
            {
                generadom(sCasoId);
            }
        }


        public void cargamos_documentoimpi_datagridview(string sCasoiddom, string gSTipoSolicitudId)
        {
            try
            {
                dGV_docimentos_IMPI.Rows.Clear();
                
                
                //20220511 Comentamos objeto que no existe
                //lvdocumentosimpi.Items.Clear();
                
                
                
                //documentosimpi
                conect con2 = new conect();
                String squeryadocumentos = " SELECT " +
                                            "     documento.DocumentoCodigoBarras," +
                                            "     documento.SubTipoDocumentoId," +
                                            " documento.DocumentoId,  " +

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
                    try
                    {
                        if (sMes != "")
                        {
                            iMes = Int32.Parse(sMes) / 30;
                        }
                    }
                    catch (Exception exs)
                    {
                        new filelog("Error: al calcular el mes ", "" + exs.Message);
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
                new filelog("cargando documentos IMPI oposiciones", ":" + Ex.Message);
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

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                Fatenderplazo obj = new Fatenderplazo(sCasoId, gSTipoSolicitudId, loguin, gSCasoNumero);
                obj.ShowDialog();
                generadom(sCasoId);
            }
            catch (Exception Ex)
            {
                new filelog("linea: 1406", "Error: " + Ex.Message);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button43_Click(object sender, EventArgs e)
        {
            //20220522 Adecuamos parametro
            //Capturadetitulo addtitulo = new Capturadetitulo(fCapuraform, loguin, "2", sCasoId);
            Capturadetitulo addtitulo = new Capturadetitulo(captura, loguin, "5", sCasoId);
            if (addtitulo.ShowDialog() == DialogResult.OK)
            {
                generadom(sCasoId);
                //tbEstatus.Text = addescrito.sEstatusnuevodepuesdensert;
                //tbEstatus_header.Text = addescrito.sEstatusnuevodepuesdensert;
            }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            //CapturadeOficios addoficio = new CapturadeOficios(fCapuraform, loguin, "2", sCasoId, "");
            CapturadeOficios addoficio = new CapturadeOficios(captura, loguin, "5", sCasoId, "");
            if (addoficio.ShowDialog() == DialogResult.OK)
            {
                generadom(sCasoId);
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            //CapturaEscritos addescrito = new CapturaEscritos(fCapuraform, loguin, "2", sCasoId);
            CapturaEscritos addescrito = new CapturaEscritos(captura, loguin, "5", sCasoId);
            if (addescrito.ShowDialog() == DialogResult.OK)
            {
                generadom(sCasoId);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    String sCasoIdOposicion = tbCasoIdOposicion.Text;

            //    configuracionfiles objfile = new configuracionfiles();
            //    objfile.configuracionfilesinicio();
            //    //String sFileexist = objfile.sFileupload + @"\logos_marcas\0" + sCasoId;// + @"\0" + sCasoId + ".gif";
            //    String sFileexist = objfile.sFileupload + @"\logos_marcas\OPO" + sCasoIdOposicion;// + @"\0" + sCasoId + ".gif";
            //    if (!File.Exists(sFileexist))
            //    {
            //        System.IO.Directory.CreateDirectory(sFileexist);
            //    }
            //    openFileDialog1.ShowDialog();
            //    string filename = System.IO.Path.GetFileName(openFileDialog1.FileName);
            //    if (filename == "openFileDialog1")
            //    {
            //        MessageBox.Show("Seleccione un archivo Válido.");
            //    }
            //    else
            //    {
            //        //we already define our connection globaly. We are just calling the object of connection.
            //        //con.Open();
            //        //SqlCommand cmd = new SqlCommand("insert into doc (document)values('\\Document\\" + filename + "')", con);

            //        //Path que habre por 
            //        string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
            //        //System.IO.File.Copy(openFileDialog1.FileName, sFileexist + @"\0" + sCasoId + ".gif", true);
            //        System.IO.File.Copy(openFileDialog1.FileName, sFileexist + @"\OPO" + sCasoIdOposicion + ".gif", true);

            //        //pbDimage.Image = Image.FromFile(sFileexist + @"\0" + sCasoId + ".gif");
            //        pbImagenImitadora.Image = Image.FromFile(sFileexist + @"\OPO" + sCasoIdOposicion + ".gif");

            //        MessageBox.Show("Imagen Cargada correctamente.");

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            /*
             * Cargamos la imagen 
             * 
             */
            try
            {
                configuracionfiles objfile = new configuracionfiles();
                objfile.configuracionfilesinicio();
                String sFileexist = objfile.sFileupload + @"\logos_opocision\0" + sCasoId;// + @"\0" + sCasoId + ".gif";
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
                }
                else
                {
                    //we already define our connection globaly. We are just calling the object of connection.
                    //con.Open();
                    //SqlCommand cmd = new SqlCommand("insert into doc (document)values('\\Document\\" + filename + "')", con);

                    //Path que habre por 
                    //string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
                    //cerramos el archivo para poder modificarlo

                    //insertamos a imagen 

                    String sDatetime = DateTime.Now.ToString("ddMMyyyyHHmmss").ToString();

                    String sRutaInsert = objfile.sFileupload + @"\logos_opocision\0" + sCasoId + @"\0" + sCasoId + "_" + sDatetime + ".gif";
                    System.IO.File.Copy(openFileDialog1.FileName, sRutaInsert, true);
                    conect con_insert_imglogo = new conect();
                    //gSTipoSolicitudId debe ser 14
                    String simglogo_insert = "INSERT INTO `imagen_logo`(`ruta`,`casoid`,`TipoSolicitudId`,`fecha`)VALUES('" + sRutaInsert.Replace("\\", "\\\\") + "','" + sCasoId + "','" + gSTipoSolicitudId + "',now());" + ";";
                    MySqlDataReader resp_insert_imglogo = con_insert_imglogo.getdatareader(simglogo_insert);
                    if (resp_insert_imglogo.RecordsAffected > 0)
                    {//quiere decir que hicimos el insert correctamente
                        obj = Image.FromFile(sRutaInsert.Replace("\\", "\\\\"));
                        pbImagenImitadora.Image = obj;
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

        private void label48_DoubleClick(object sender, EventArgs e)
        {
            updateEstatus updateestatus = new updateEstatus("5");
            if (updateestatus.ShowDialog() == DialogResult.OK)
            {
                String value = updateestatus.sValueestatus;//hacemos el update con este valor y ponemos el Texto en el form
                String texti = updateestatus.sTextoestatus;//hacemos el update con este valor y ponemos el Texto en el form
                conect con_4 = new conect();
                String updateestatuscaso = "UPDATE `caso_oposicion` SET `EstatusCasoId` = '" + value + "' WHERE (`CasoId` = '" + tbCasoIdOposicion.Text + "');";
                MySqlDataReader resp_updatecaso = con_4.getdatareader(updateestatuscaso);
                if (resp_updatecaso != null)
                {
                    tbEstatus.Text = texti;
                }

            }
        }

        private void label48_MouseLeave(object sender, EventArgs e)
        {
            label48.BackColor = Color.MediumOrchid;
        }

        private void label48_MouseMove(object sender, MouseEventArgs e)
        {
            label48.BackColor = Color.Thistle;
        }

        private void tbDFecRecepcion_Validated(object sender, EventArgs e)
        {

        }

        private void tbDFecRecepcion_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbDFecRecepcion);
        }

        private void tbDFecPubImitadora_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbDFecPubImitadora);
        }

        private void tbDFecPresOposicion_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbDFecPresOposicion);
        }

        private void tbDFecRecepcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacamposfecha(tbDFecRecepcion, e);
        }

        public void validacamposfecha(TextBox tbElement, KeyPressEventArgs e)
        {
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

        private void tbDFecPubImitadora_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacamposfecha(tbDFecPubImitadora, e);
        }

        private void tbDFecPresOposicion_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacamposfecha(tbDFecPresOposicion, e);
        }

        private void tbDFecRecepcion_Leave(object sender, EventArgs e)
        {
            tbDFecRecepcion.Text = tbDFecRecepcion.Text.Replace("/", "-").Replace(".", "-");
        }

        private void tbDFecPubImitadora_Leave(object sender, EventArgs e)
        {
            tbDFecPubImitadora.Text = tbDFecPubImitadora.Text.Replace("/", "-").Replace(".", "-");
        }

        private void tbDFecPresOposicion_Leave(object sender, EventArgs e)
        {
            tbDFecPresOposicion.Text = tbDFecPresOposicion.Text.Replace("/", "-").Replace(".", "-");
        }

        private void dgDocumentoselectronicos_DoubleClick(object sender, EventArgs e)
        {
            String sRuta = "";
            try
            {

                if (dgDocumentoselectronicos.SelectedRows != null)
                {
                    sRuta = dgDocumentoselectronicos.SelectedRows[0].Cells[5].Value.ToString();
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
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        filePath = openFileDialog.FileName;
                        aName = filePath.Split('\\');
                        sNamefile = aName[aName.Length - 1];
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
                    string textoNormalizado = sNamefile.Normalize(NormalizationForm.FormD);
                    Regex reg = new Regex("[^a-zA-Z0-9 ]");
                    string textoSinAcentos = reg.Replace(textoNormalizado, "");


                    //string ruta = obj.sFileupload + "\\" + sCarpetadocumentos + "\\" + gSCasoNumero + "\\edocs";
                    String[] sfilesss = obj.sFileupload.Split('\\');
                    string ruta = "\\\\" + sfilesss[2] + "\\" + sfilesss[3] + "\\" + sCarpetadocumentos + "\\" + gSCasoNumero;// + "\\edocs";

                    String sfilePath_2 = @ruta + "\\" + gSCasoNumero + " " + tbExpediente.Text.Replace("/", "") + " " + sNamefile;
                    
                    
                    textBox38.Text = sfilePath_2;
                    if (!System.IO.Directory.Exists(ruta))
                    {
                        System.IO.Directory.CreateDirectory(ruta);
                    }
                    System.IO.File.Copy(filePath, sfilePath_2, true);
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
                                " '" + textBox37.Text.Trim() + "', " +
                                " '" + sCasoId + "', " +
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

        private void tbDFecRecepcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbConcluyofecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacamposfecha(tbConcluyofecha, e);
        }

        private void tbConcluyofecha_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbConcluyofecha);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try {

                bMarcas oBmarcas = new bMarcas(2, captura, loguin);
                fTmarcas objmarca = new fTmarcas(loguin, captura, oBmarcas, sCasomarcasoriginal);
                objmarca.Show();

            }
            catch (Exception exs) {
                new filelog("", ""+exs.Message);

            }
            
        }
    }
}
