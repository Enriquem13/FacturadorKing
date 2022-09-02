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
    public partial class CapturaEscritos : Form
    {
        captura capform;
        Form1 login;
        String sEstatusidint = "";
        String sCasoidactual = "";
        public String sgTiposolicitudid="";
        public String sgSubtiposolicitudid = "";
        public String sgCasoFechaInternacional;
        public String sgCasoFechaRecepcion;
        String sTablaconsulta = "";
        String sGrop;
        String sCarpetadocumentos = "";
        public String sCasoidgeneralGlabal = "0";
        public String sGEstatusCasoId;
        public bool sBAnderadesdecaso = false;
        public String sfilePath;
        public String sfilePath_2;
        public String sCarpetacaso;
        private String sgPlazoid;
        public bool bgAtendiooficio = true;
        funcionesdicss objfuncionesdicss;
        public String sCasoDisenoClasificacion = "";
        public String sgCasoDisenoClasificacion = "";


        public String sEstatusnuevodepuesdensert { get; set; }
        public CapturaEscritos(captura capformulario, Form1 loguin, String sgroup, String sCasoidgeneral)
        {
            try
            {
                //new filelog("Entramos en el escrito", " linea 44 ");
                capform = capformulario;
                login = loguin;
                sGrop = sgroup;
                objfuncionesdicss = new funcionesdicss();
                InitializeComponent();
                //tbExpediente__.Enabled = false;
                switch (sgroup)
                {
                    case "1":
                        {
                            sTablaconsulta = "caso_patente";
                            this.Text = this.Text + " ( Grupo Patentes)";
                            this.BackColor = Color.Pink;
                            sCarpetadocumentos = "DigitalizadoPatentes\\documentosimpi";
                            sCarpetacaso = tbCasonum.Text;
                        } break;
                    case "2":
                        {
                            sTablaconsulta = "caso_marcas";
                            this.Text = this.Text + " ( Grupo Marcas)";
                            this.BackColor = Color.FromArgb(255, 255, 192);
                            sCarpetadocumentos = "DigitalizadoMarcas\\documentosimpi";
                            sCarpetacaso = tbCasonum.Text;
                        } break;
                    case "3":
                        {
                            sTablaconsulta = "caso_contencioso";
                            this.Text = this.Text + " ( Grupo Contencioso)";
                            this.BackColor = Color.Yellow;
                            sCarpetadocumentos = "Casocontencioso";
                            sCarpetacaso = tbCasonum.Text;
                        } break;
                    case "4":
                        {
                            sTablaconsulta = "caso_consulta";
                            this.Text = this.Text + " ( Grupo Consulta)";
                            this.BackColor = SystemColors.Control;
                            sCarpetadocumentos = "Consulta";
                            sCarpetacaso = tbCasonum.Text;
                        } break;
                    case "5":
                        {
                            sTablaconsulta = "caso_oposicion";
                            this.Text = this.Text + " ( Grupo Oposicion a solicitudes)";
                            this.BackColor = Color.FromArgb(255, 192, 128);
                            sCarpetadocumentos = "Oposicion";
                            sCarpetacaso = tbCasonum.Text;
                        } break;
                    case "6":
                        {
                            sTablaconsulta = "";
                            this.Text = this.Text + " ( Grupo Variedades vegetales)";
                            this.BackColor = SystemColors.Control;
                            sCarpetadocumentos = "Variedadesveg";
                            sCarpetacaso = tbCasonum.Text;
                        } break;
                    case "7":
                        {
                            sTablaconsulta = "caso_registrodeobra";
                            this.Text = this.Text + " ( Grupo Derechos de autor)";
                            this.BackColor = Color.SkyBlue;
                            sCarpetadocumentos = "Registrodeobra";
                            sCarpetacaso = tbCasonum.Text;
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
            }catch(Exception E){
                String ruta = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\casosking\\";
                if (!Directory.Exists(ruta))
                {
                    System.IO.Directory.CreateDirectory(ruta);
                }
                String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                StringBuilder sb = new StringBuilder();
                sb.Append(fechalog + ":userid:" + login.sId + " Error:" + E + "\n");
                System.IO.File.AppendAllText(ruta + "sistema_casosking.log", sb.ToString());
                sb.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sBAnderadesdecaso)
            {
                this.Close();
            }
            else
            {
                capform.Show();
                this.Close();
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
                        if (EstatusCasoId == "1")
                        {
                            //tbExpediente__.Enabled = true;
                            sGEstatusCasoId = EstatusCasoId;
                        }

                        conect con1_estatuscaso = new conect();
                        String sConsultaestatus = "SELECT * FROM estatuscaso where EstatusCasoId = '" + EstatusCasoId + "' ";
                        sCasoidactual = validareader("Casoid", "Casoid", respuestastring6).Text;
                        //CasoDisenoClasificacion
                        if (sTablaconsulta == "caso_patente")
                        {
                            sgCasoDisenoClasificacion = validareader("CasoDisenoClasificacion", "CasoDisenoClasificacion", respuestastring6).Text;
                        }

                        sgTiposolicitudid = validareader("TipoSolicitudid", "Casoid", respuestastring6).Text;
                        sgSubtiposolicitudid = validareader("TipoSolicitudid", "Casoid", respuestastring6).Text;



                        if (sTablaconsulta == "caso_patente")
                        {
                            sCasoDisenoClasificacion = validareader("CasoDisenoClasificacion", "CasoDisenoClasificacion", respuestastring6).Text;
                        }
                        
                        String sFechainternacional = validareader("CasoFechaInternacional", "Casoid", respuestastring6).Text;
                        if (validareader("CasoFechaInternacional", "Casoid", respuestastring6).Text != "")
                        {
                            sgCasoFechaInternacional = validareader("CasoFechaInternacional", "Casoid", respuestastring6).Text.Substring(0, 10);
                        }else {
                            sgCasoFechaInternacional = "";
                        }

                        if (validareader("CasoFechaRecepcion", "Casoid", respuestastring6).Text!="")
                        {
                            sgCasoFechaRecepcion = validareader("CasoFechaRecepcion", "Casoid", respuestastring6).Text.Substring(0, 10);                        
                        }else{
                            sgCasoFechaRecepcion = "";
                        }
                        

                        MySqlDataReader resp_estatus = con1_estatuscaso.getdatareader(sConsultaestatus);
                        while (resp_estatus.Read())
                        {
                            tbEstatus.Text = validareader("EstatusCasoDescrip", "EstatusCasoId", resp_estatus).Text;//Estatus
                            sEstatusidint = validareader("EstatusCasoId", "EstatusCasoId", resp_estatus).Text;//Estatus
                        }
                        resp_estatus.Close();
                        con1_estatuscaso.Cerrarconexion();


                        conect con1_tiposol = new conect();
                        String sQuerytiposolicitud = "select * from tiposolicitud where TipoSolicitudid = " + validareader("TipoSolicitudid", "Casoid", respuestastring6).Text;
                        MySqlDataReader resp_tiposolicitud = con1_tiposol.getdatareader(sQuerytiposolicitud);
                        while (resp_tiposolicitud.Read())
                        {
                            tbTipo.Text = validareader("TipoSolicitudDescrip", "TipoSolicitudid", resp_tiposolicitud).Text;//Estatus
                        }
                        resp_tiposolicitud.Close();
                        con1_tiposol.Cerrarconexion();

                        cargarescritorpermitidos();


                        cbOficio.Items.Clear();
                        conect con_oficio = new conect();
                        //String sQueryoficioanterior = " SELECT " +
                        //                                " plazo_general.idPlazo_general," +
                        //                                " subtipodocumento.SubTipoDocumentoId," +
                        //                                " subtipodocumento.SubTipoDocumentoDescrip," +
                        //                                " documento.DocumentoCodigoBarras," +
                        //                                " plazo_general.*" +
                        //                                " FROM" +
                        //                                " plazo_general," +
                        //                                " documento," +
                        //                                " subtipodocumento" +
                        //                                " WHERE" +
                        //                                " plazo_general.documentoid = documento.documentoid" +
                        //                                " AND documento.subtipodocumentoid = subtipodocumento.subtipodocumentoid" +
                        //                                " AND subtipodocumento.TipoDocumentoId = 1" +
                        //                                " AND plazo_general.Estatus_plazoid_impi = 1" +
                        //                                " AND plazo_general.CasoId = " + sCasoidgeneralGlabal +
                        //                                " order by idPlazo_general desc limit 1 ;";

                        //String sQueryoficioanterior = " select " +//DocumentoCodigoBarras
                        //                                " plazos_detalle.plazosid," +
                        //                                " plazos_detalle.Plazos_detalleid," +
                        //                                " documento.DocumentoCodigoBarras," +
                        //                                " subtipodocumento.SubTipoDocumentoDescrip" +
                        //                                " from " +
                        //                                " plazos," +
                        //                                " plazos_detalle," +
                        //                                " documento," +
                        //                                " subtipodocumento" +
                        //                                " where " +
                        //                                " plazos.Plazosid = plazos_detalle.plazosid" +
                        //                                " and plazos_detalle.documentoid = documento.DocumentoId" +
                        //                                " and subtipodocumento.SubTipoDocumentoId = documento.SubTipoDocumentoId" +
                        //                                " AND subtipodocumento.TipoDocumentoId = 1" +
                        //                                " and plazos_detalle.Estatus_plazoid = 1" +
                        //                                " and plazos.casoid = " + sCasoidgeneralGlabal +
                        //                                " and plazos.TipoSolicitudId = " + sgTiposolicitudid+
                        //                                " limit 1;";
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
                                                        " ;";
                                                        //" limit 1;";
                        MySqlDataReader resp_oficioanterior = con_oficio.getdatareader(sQueryoficioanterior);
                        
                        while (resp_oficioanterior.Read())
                        {
                            ComboboxItem cbItemoficios = new ComboboxItem();
                            cbItemoficios.Text = validareader("plazosid", "plazosid", resp_oficioanterior).Text+"_"+validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoDescrip", resp_oficioanterior).Text + " - " + validareader("DocumentoCodigoBarras", "DocumentoCodigoBarras", resp_oficioanterior).Text;
                            cbItemoficios.Value = validareader("Plazos_detalleid", "Plazos_detalleid", resp_oficioanterior).Value;
                            sgPlazoid = validareader("plazosid", "plazosid", resp_oficioanterior).Text;
                            cbOficio.Items.Add(cbItemoficios);//Estatus
                            //sLDocmentoid.Add(validareader("DocumentoId", "DocumentoId", resp_oficioanterior).Text);
                        }
                        resp_oficioanterior.Close();
                        con_oficio.Cerrarconexion();
                        //cbOficio
                        String sPrepparousuario = "";
                        conect con2 = new conect();
                        String sQueryresponsables = "select UsuarioName, UsuarioId from usuario;";
                        MySqlDataReader resp_responsables = con2.getdatareader(sQueryresponsables);
                        while (resp_responsables.Read())
                        {
                            cbPreparo.Items.Add(validareader("UsuarioName", "UsuarioId", resp_responsables)); //= validareader("UsuarioName", "UsuarioId", resp_responsables).Text;//Estatus
                            if (login.sId == validareader("UsuarioId", "UsuarioId", resp_responsables).Text)
                            {
                                sPrepparousuario = validareader("UsuarioName", "UsuarioName", resp_responsables).Text;
                            }
                        }
                        resp_responsables.Close();
                        con2.Cerrarconexion();
                        cbPreparo.Text = sPrepparousuario;

                        /*fin de ciclo escritos*/
                        tbRegistro.Text = validareader("CasoNumConcedida", "Casoid", respuestastring6).Text;//registro
                        tbExpediente.Text = validareader("CasoNumeroExpedienteLargo", "Casoid", respuestastring6).Text;
                        rtbtitulo.Text = validareader("CasoTituloespanol", "Casoid", respuestastring6).Text + " /" + validareader("CasoTituloingles", "Casoid", respuestastring6).Text;
                    }
                    respuestastring6.Close();
                    con1.Cerrarconexion();
                }
                else
                {
                    MessageBox.Show("El Número de caso no existe en patentes");
                }

                
            }
            catch (Exception E)
            {
                MessageBox.Show("Warning: " + E);
                new filelog(login.sId, E.ToString());
            }
        }

        private void cargarescritorpermitidos()
        {
            if (sEstatusidint=="")
            {
                MessageBox.Show("Estatus del caso vacío");
                return;
            }
            //cargamos los escritos permitidos
            conect con1_escritos = new conect();
            cbEsritos.Items.Clear();
            /*Debemos agrgear los estatus y escritos disponibles segun las tablas y reaccionar dependiendo eso*/
            String sQueryescritosdisponibles = "SELECT " +
                                               "     * " +
                                               " FROM " +
                                               "    estatuscasosubtipodocumento, " +
                                               "    subtipodocumento " +
                                               " WHERE " +
                                               "     estatuscasosubtipodocumento.Estatuscasoid = " + sEstatusidint + "  " +
                                               "         AND estatuscasosubtipodocumento.GrupoId = " + sGrop +//El grupo falta validarlo con una variable dependiendo el tipo del caso 
                                               "         AND subtipodocumento.SubTipoDocumentoId = estatuscasosubtipodocumento.SubTipoDocumentoId " +
                                               "         AND subtipodocumento.TipoDocumentoId = 2 " +//en esta pantalla debe ser fijo el número 2 puesto que estamos en escritoa
                                               "         AND subtipodocumento.SubTipoDocumentoIndAct = 1 " +// el valos uno debe ser fijo puesto que validamos que el documento esté activo
                                               "         group by estatuscasosubtipodocumento.SubTipoDocumentoId order by subtipodocumento.SubTipoDocumentoDescrip;";
            //String sQueryescritosdisponibless = "select * from estatuscasosubtipodocumento, subtipodocumento where estatuscasosubtipodocumento.EstatusCasoId = " + sEstatusidint + " and estatuscasosubtipodocumento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId and estatuscasosubtipodocumento.GrupoId =1 and subtipodocumento.TipoDocumentoId = 2 group by subtipodocumento.SubTipoDocumentoId;";
            //String sQueryescritosdisponibles = "select subtipodocumento.SubTipoDocumentoDescrip, estatuscasosubtipodocumentores.SubTipoDocumentoId from estatuscasosubtipodocumentores, subtipodocumento where estatuscasoid = " + tbEstatus.Text + " and estatuscasosubtipodocumentores.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId;";
            MySqlDataReader resp_escritos = con1_escritos.getdatareader(sQueryescritosdisponibles);
            while (resp_escritos.Read())
            {
                String sIdescritp = validareader("SubTipoDocumentoId", "SubTipoDocumentoId", resp_escritos).Text;
                cbEsritos.Items.Add(validareader_documentos("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos, sIdescritp));//Estatus
            }
            resp_escritos.Close();
            con1_escritos.Cerrarconexion();
        }
        public ComboboxItem validareader(String campoText, String campoValue, MySqlDataReader mresultado)
        {
            ComboboxItem cItemresult = new ComboboxItem();
            try { 
                

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
            }catch(Exception e){
                cItemresult.Text = "";
                cItemresult.Value = "";
                return cItemresult;
            }
        }

        public ComboboxItem validareader_documentos(String campoText, String campoValue, MySqlDataReader mresultado, String id_documento)
        {
            ComboboxItem cItemresult = new ComboboxItem();

            if (!mresultado.IsDBNull(mresultado.GetOrdinal(campoText)))
            {
                cItemresult.Text = id_documento + " - "+mresultado.GetString(mresultado.GetOrdinal(campoText));
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
            int isGrop = Int32.Parse(sGrop);
            bCasoparadoc bCaso = new bCasoparadoc(isGrop, capform, login);
            if (bCaso.ShowDialog() == DialogResult.OK)
            {   //devolvemos el id del caso y aqui tambien utilizamos el case para saber en que tablas vamos a buscar ese id seleccionado

                String sQueryconulta = "";
                try {
                    sCasoidgeneralGlabal = bCaso.sCasoid;
                    cargacasoenform();
                //conect con1 = new conect();
                //sQueryconulta = "SELECT * FROM " + sTablaconsulta + " where Casoid = " + bCaso.sCasoid + "";
                //MySqlDataReader respuestastring6 = con1.getdatareader(sQueryconulta);
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
                //            String sQueryescritosdisponibles = "SELECT "+
                //                                               "     * "+
                //                                               " FROM "+
                //                                               "    estatuscasosubtipodocumento, "+
                //                                               "    subtipodocumento "+
                //                                               " WHERE "+
                //                                               "     estatuscasosubtipodocumento.Estatuscasoid = "+ sEstatusidint +"  "+
                //                                               "         AND estatuscasosubtipodocumento.GrupoId = "+ sGrop +//El grupo falta validarlo con una variable dependiendo el tipo del caso 
                //                                               "         AND subtipodocumento.SubTipoDocumentoId = estatuscasosubtipodocumento.SubTipoDocumentoId "+
                //                                               "         AND subtipodocumento.TipoDocumentoId = 2 "+//en esta pantalla debe ser fijo el número 2 puesto que estamos en escritoa
                //                                               "         AND subtipodocumento.SubTipoDocumentoIndAct = 1 "+// el valos uno debe ser fijo puesto que validamos que el documento esté activo
                //                                               "         group by estatuscasosubtipodocumento.SubTipoDocumentoId order by subtipodocumento.SubTipoDocumentoDescrip;";
                //            //String sQueryescritosdisponibless = "select * from estatuscasosubtipodocumento, subtipodocumento where estatuscasosubtipodocumento.EstatusCasoId = " + sEstatusidint + " and estatuscasosubtipodocumento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId and estatuscasosubtipodocumento.GrupoId =1 and subtipodocumento.TipoDocumentoId = 2 group by subtipodocumento.SubTipoDocumentoId;";
                //            //String sQueryescritosdisponibles = "select subtipodocumento.SubTipoDocumentoDescrip, estatuscasosubtipodocumentores.SubTipoDocumentoId from estatuscasosubtipodocumentores, subtipodocumento where estatuscasoid = " + tbEstatus.Text + " and estatuscasosubtipodocumentores.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId;";
                //            MySqlDataReader resp_escritos = con1.getdatareader(sQueryescritosdisponibles);
                //            while (resp_escritos.Read())
                //            {
                //               cbEsritos.Items.Add(validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos));//Estatus
                //            }
                //            resp_escritos.Close();
                //            String sQueryresponsables = "select UsuarioName, UsuarioId from usuario;";
                //            MySqlDataReader resp_responsables = con1.getdatareader(sQueryresponsables);
                //            while (resp_responsables.Read())
                //            {
                //                cbPreparo.Text = validareader("UsuarioName", "UsuarioId", resp_responsables).Text;//Estatus
                //            }
                //            resp_responsables.Close();
                //            /*fin de ciclo escritos*/
                //            tbRegistro.Text = validareader("CasoNumConcedida", "Casoid", respuestastring6).Text;//registro
                //            tbExpediente.Text = validareader("CasoNumeroExpedienteLargo", "Casoid", respuestastring6).Text;
                //            rtbtitulo.Text = validareader("CasoTituloespanol", "Casoid", respuestastring6).Text+" /"+validareader("CasoTituloingles", "Casoid", respuestastring6).Text;
                //        }
                //        respuestastring6.Close();
                //    }
                //    else {
                //        MessageBox.Show("El Número de caso no existe en patentes");
                //    }
                }
                catch (Exception E)
                {
                    new filelog(login.sId, E.ToString() + "Revisar: q1" + sQueryconulta);
                }
            }
            //bBuscargrupo obj = new bBuscargrupo(capform, login);
           
        }

        public void validaciones() {
            
            
        }
        public void validamos_anualidades_confechaactual(string sCasoid, string sTiposolicitud, string sFechapago)
        {
            /*Buscamos las anualidades de éste caso si vemos que existen, y las anualidades son del quinquenio 1 entonces preguntamos
             * si la fecha actual del sistema con la fecha limite de pago cambia el año entonces recorremos las excentas 
             * porque el año en el que se genero el cita apago ya estaría excento
             * 
             * Preguntamos por el quinquenio uno pendiente , por lo que podremos recalcular el numero de anualidades excentas dependiendo la fecha de pago
             */
            try
            {
                DateTime fec_Anopago = DateTime.ParseExact(sFechapago, "yyyy/MM/dd", CultureInfo.InvariantCulture);//.ToString("dd");
                String sAnualidadAno = "";
                conect con1 = new conect();
                String sConsultaestatus = " SELECT  " +
                                            "     min(AnualidadAno) As ano_excento " +
                                            " FROM " +
                                            "     anualidad " +
                                            " WHERE " +
                                            "     casoid = " + sCasoid + " AND TipoSolicitudId = " + sTiposolicitud + " " +
                                            "         AND EstatusAnualidadId = 1 " +
                                            "         AND AnualidadQuinquenio = 1; ";/*Aquí validamos que esté pendiente la anualidad y que sea del quinquenio 1 para poder modificar si no no */
                MySqlDataReader resp_estatus = con1.getdatareader(sConsultaestatus);
                while (resp_estatus.Read())
                {
                    sAnualidadAno = validareader("ano_excento", "ano_excento", resp_estatus).Text;//Estatus
                }
                resp_estatus.Close();
                con1.Cerrarconexion();
                if (sAnualidadAno!="")
                {/*Quiere decir que si es el primer quinquenio y que todas están pendientes*/
                    //DateTime sFechaActual = DateTime.Today;/*Tomamos la fecha sello impi*/tbDocumentofecharecepcion.Text
                    DateTime sFechaActual = DateTime.Parse(tbDocumentofecharecepcion.Text);/*Tomamos la fecha sello impi*/
                    
                    //DateTime sFechaActual = DateTime.;/*Tomamos la fecha sello impi*/
                    int sFechapropuestaexcenta = Int16.Parse(sAnualidadAno);
                    if (sFechaActual.Year > sFechapropuestaexcenta)
                    {   
                        //valiamos si el año de pago es mayor a la año del primer  quinquenio pendiente
                        String sAnualidadIndExe = "";
                        String sAnualidadQuinquenio = "";
                        String sAnualidadFechaLimitePago = "";
                        conect con_update = new conect();
                        String anualidades_update = " SELECT  " +
                                                    "     * " +
                                                    " FROM " +
                                                    "     anualidad " +
                                                    " WHERE " +
                                                    "     casoid = " + sCasoid + " AND TipoSolicitudId = " + sTiposolicitud + " order by AnualidadSecuencia Asc";
                        MySqlDataReader resp_Anualidades = con_update.getdatareader(anualidades_update);
                        int sCount = 0;
                        String s_compaAnualidadAno = "";
                        String sAnualidadId = "";
                        String sFechalimitedepagomodif = "";
                        bool bModificaFecha = true;
                        while (resp_Anualidades.Read())
                        {
                            /**
                             * AnualidadIndExe
                             * AnualidadQuinquenio
                             * AnualidadFechaLimitePago
                             */
                            sAnualidadIndExe = validareader("AnualidadIndExe", "AnualidadAno", resp_Anualidades).Text;//Estatus
                            sAnualidadQuinquenio = validareader("AnualidadQuinquenio", "AnualidadAno", resp_Anualidades).Text;//Estatus
                            sAnualidadFechaLimitePago = validareader("AnualidadFechaLimitePago", "AnualidadAno", resp_Anualidades).Text;//Estatus
                            s_compaAnualidadAno = validareader("AnualidadAno", "AnualidadAno", resp_Anualidades).Text;//Estatus
                            sAnualidadId = validareader("AnualidadId", "AnualidadId", resp_Anualidades).Text;//Estatus
                            
                            /*Sin son Excentas no hacemos nada*/
                            //sAnualidadAno
                            if (sAnualidadIndExe!="1")
                            {
                                int iAnocambiaexcento = int.Parse(sAnualidadAno);
                                int sAnualidad = int.Parse(s_compaAnualidadAno);
                                if (iAnocambiaexcento == sAnualidad)
                                {/*Excentamos este año*/
                                    conect con_4_anua = new conect();
                                    String updatevigenciacaso = "UPDATE `anualidad` SET `AnualidadIndExe` = '1',"+
                                                                " AnualidadQuinquenio = 0, " +
                                                                " EstatusAnualidadId = 4, " +
                                                                " AnualidadFechaLimitePago = null " +
                                                                " WHERE (`AnualidadId` = '" +
                                                                sAnualidadId + "');";
                                    MySqlDataReader resp_updatevig = con_4_anua.getdatareader(updatevigenciacaso);
                                    resp_updatevig.Close();
                                    con_4_anua.Cerrarconexion();
                                }

                                sCount++;
                                if (sCount == 5)//gurdamos la fecha limite de pago
                                {
                                    sFechalimitedepagomodif = sAnualidadFechaLimitePago;
                                }
                                if (sCount == 6)
                                {/*Generamos los quinquenios empezando desde el 1 y hasta 5*/
                                    
                                    DateTime dsAnualidadFechaLimitePago = DateTime.Parse(sFechalimitedepagomodif);
                                    /*calculamos el numero de quinquenio  AnualidadQuinquenio*/
                                    int iAnualidadquinquenio = int.Parse(sAnualidadQuinquenio);
                                    iAnualidadquinquenio = iAnualidadquinquenio - 1;
                                    conect con_4_anua = new conect();


                                    String sModificafecha =", AnualidadFechaLimitePago = '" + dsAnualidadFechaLimitePago.ToString("yyyy'/'MM'/'dd") + "' " ;
                                    if (bModificaFecha)
                                    {
                                        sModificafecha = ", AnualidadFechaLimitePago = '" + dsAnualidadFechaLimitePago.ToString("yyyy'/'MM'/'dd") + "' ";
                                        bModificaFecha = false;
                                    }
                                    else {
                                        sModificafecha = "";
                                    }

                                    String updatevigenciacaso = "UPDATE `anualidad` SET " +
                                                                " AnualidadQuinquenio = " + iAnualidadquinquenio + " " +
                                                                sModificafecha +
                                                                " WHERE (`AnualidadId` = '" +
                                                                sAnualidadId + "');";
                                    MySqlDataReader resp_updatevig = con_4_anua.getdatareader(updatevigenciacaso);
                                    resp_updatevig.Close();
                                    con_4_anua.Cerrarconexion();
                                    sCount = 1;
                                }
                                
                                
                            }
                            
                        }
                        resp_Anualidades.Close();
                        con_update.Cerrarconexion();
                    }

                }

            }catch (Exception Ex){
                new filelog("validamos_anualidades_confechaactual", " :"+Ex.Message);
            }
        }
        public void pago_anualialidades_subsecuentes(int iQuinquenioapagar, string sFechapago, string sCasoid, string sTiposolicitud){
            /*Validamos que el documento sea el pago de anualidades para que modifiquemos la fecha de pago de anualidad y cambiemos el estatus del quinquenio pagado*/
            //String DocumentoFecha = DateTime.ParseExact(sFechapresentacion, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbFechacalce.Text;// now();
            //String fec_dia = DateTime.ParseExact(sFechapresentacion, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd"); //tbFechacalce.Text;// now();
            //String fec_mes = DateTime.ParseExact(sFechapresentacion, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM"); //tbFechacalce.Text;// now();
            //String fec_ano = DateTime.ParseExact(sFechapresentacion, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy"); //tbFechacalce.Text;// now();
            validamos_anualidades_confechaactual(sCasoid, sTiposolicitud, sFechapago);
            try {
                String DocumentoFecha = DateTime.ParseExact(sFechapago, "yyyy/MM/dd", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd");//tbFechacalce.Text;// now();
                String sQueryupdatequinquenio = " update anualidad " +
                                                " set AnualidadFechaPago = '" + DocumentoFecha + "'," +
                                                " EstatusAnualidadId = 2 " +
                                                " where casoid = " + sCasoid +
                                                " AND TipoSolicitudId = " + sTiposolicitud +
                                                " AND EstatusAnualidadId = 1" +
                                                " AND AnualidadQuinquenio = " + iQuinquenioapagar + /*numero de la anualidad*/
                                                " ORDER BY AnualidadSecuencia ASC" +
                                                " limit 5;";
                conect con1 = new conect();
                MySqlDataReader resp_escritos = con1.getdatareader(sQueryupdatequinquenio);
                //new filelog("update del pago de anualidades", " :"+ sQueryupdatequinquenio);
                if (resp_escritos.RecordsAffected > 0)
                {
                    MessageBox.Show(resp_escritos.RecordsAffected + " Anualidades Pagadas.");
                }
                resp_escritos.Close();
                con1.Cerrarconexion();
                //actualiza_fecha_vigencia();

            }catch(Exception Ex){
                new filelog("", " :"+Ex.Message);
                MessageBox.Show("Error al intentar agregar el pago de las anualidades");
            }

        }

        public void pago_anualialidades_subsecuentes_DIS_MOD(int iQuinquenioapagar, string sFechapago, string sCasoid, string sTiposolicitud)
        {
            /*Validamos que el documento sea el pago de anualidades para que modifiquemos la fecha de pago de anualidad y cambiemos el estatus del quinquenio pagado*/
            //String DocumentoFecha = DateTime.ParseExact(sFechapresentacion, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbFechacalce.Text;// now();
            //String fec_dia = DateTime.ParseExact(sFechapresentacion, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd"); //tbFechacalce.Text;// now();
            //String fec_mes = DateTime.ParseExact(sFechapresentacion, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM"); //tbFechacalce.Text;// now();
            //String fec_ano = DateTime.ParseExact(sFechapresentacion, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy"); //tbFechacalce.Text;// now();
            validamos_anualidades_confechaactual(sCasoid, sTiposolicitud, sFechapago);
            try
            {
                String DocumentoFecha = DateTime.ParseExact(sFechapago, "yyyy/MM/dd", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd");//tbFechacalce.Text;// now();
                //String sQueryupdatequinquenio = " update anualidad " +
                //                                " set AnualidadFechaPago = '" + DocumentoFecha + "'," +
                //                                " EstatusAnualidadId = 2 " +
                //                                " where casoid = " + sCasoid +
                //                                " AND TipoSolicitudId = " + sTiposolicitud +
                //                                " AND EstatusAnualidadId = 1" +
                //                                " AND AnualidadQuinquenio = " + iQuinquenioapagar + /*numero de la anualidad*/
                //                                " ORDER BY AnualidadSecuencia ASC" +
                //                                " limit 5;";

                String sQueryupdatequinquenio = "  UPDATE anialidades_md_nuevos " +
                                                "    SET " +
                                                " fecha_pago = '" + DocumentoFecha +"', "+
                                                " estatusanualidad = 2 " +
                                                " WHERE " +
                                                " casoid = " + sCasoid +
                                                " AND TipoSolicitudId = " + sTiposolicitud +
                                                " AND estatusanualidad = 1 " +
                                                " AND secuencia = " + iQuinquenioapagar + "";

                                                //" set AnualidadFechaPago = '" + DocumentoFecha + "'," +
                                                //" EstatusAnualidadId = 2 " +
                                                //" where casoid = " + sCasoid +
                                                //" AND TipoSolicitudId = " + sTiposolicitud +
                                                //" AND EstatusAnualidadId = 1" +
                                                //" AND AnualidadQuinquenio = " + iQuinquenioapagar + /*numero de la anualidad*/
                                                //" ORDER BY AnualidadSecuencia ASC" +
                                                //" limit 5;";
                conect con1 = new conect();
                MySqlDataReader resp_escritos = con1.getdatareader(sQueryupdatequinquenio);
                if (resp_escritos.RecordsAffected > 0)
                {
                    MessageBox.Show(resp_escritos.RecordsAffected + " Rango de anualidades pagado.");
                }
                resp_escritos.Close();
                con1.Cerrarconexion();
                //actualiza_fecha_vigencia();

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error al intentar agregar el pago de las anualidades");
            }

        }

        public DateTime actualiza_fecha_vigencia(int iAnosadicionales)
        {/*Al ser llamdo desde pago de anualidades, sólo debe funcionar para el grupo de patentes*/
            DateTime dFechavigencia = DateTime.Today;//es Today para inicializar a variable
            try
            {
                conect con_consultafecha_legal = new conect();
                String sQuery = "select * from caso_patente where `CasoId` = '" + sCasoidactual + "' and TipoSolicitudId = '" + sgTiposolicitudid + "'";
                MySqlDataReader resp_consvig = con_consultafecha_legal.getdatareader(sQuery);
                resp_consvig.Read();
                String sCasoFechaLegal = validareader("CasoFechaLegal", "CasoFechaLegal", resp_consvig).Text;
                String sCasoFechaVigencia = validareader("CasoFechaVigencia", "CasoFechaVigencia", resp_consvig).Text;
                String sCasoFechaRecepcion = validareader("CasoFechaRecepcion", "CasoFechaRecepcion", resp_consvig).Text;
                String sCasoFechaInternacional = validareader("CasoFechaInternacional", "CasoFechaInternacional", resp_consvig).Text;

                resp_consvig.Close();
                con_consultafecha_legal.Cerrarconexion();
                if (sCasoFechaVigencia != "")
                {
                    
                    try
                    {
                        //DateTime.ParseExact(tbDocumentofecharecepcion.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                        dFechavigencia = DateTime.Parse(sCasoFechaVigencia);//, "dd-MM-yyyy", CultureInfo.InvariantCulture);//DateTime.Parse(sFechalegal);//DateTime.ParseExact(sFechalegal, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                        dFechavigencia = dFechavigencia.AddYears(iAnosadicionales);
                        //switch (sgTiposolicitudid)//Casos para generar anualidades
                        //{
                        //    case "1"://Solicitud de Patentes
                        //        {
                        //            dFechavigencia = dFechavigencia.AddYears(20);
                        //        } break;
                        //    case "2"://Modelo de utilidad
                        //        {
                        //            dFechavigencia = dFechavigencia.AddYears(10);
                        //        } break;
                        //    case "3"://Diseño (Modelo)
                        //        {
                        //            dFechavigencia = dFechavigencia.AddYears(15);
                        //        } break;
                        //    case "4"://Diseño Industrial (Dibujo)
                        //        {
                        //            dFechavigencia = dFechavigencia.AddYears(15);
                        //        } break;
                        //}
                        conect con_4_vig = new conect();
                        String updatevigenciacaso = "UPDATE `" + sTablaconsulta + "` SET `CasoFechaVigencia` = '" + dFechavigencia.ToString("yyyy/MM/dd") +
                            "' WHERE (`CasoId` = '" + sCasoidactual + "' and TipoSolicitudId = '" + sgTiposolicitudid + "' );";
                        MySqlDataReader resp_updatevig = con_4_vig.getdatareader(updatevigenciacaso);
                        resp_updatevig.Close();
                        con_4_vig.Cerrarconexion();
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("No se pudo calcular la fecha Vigencia.");
                        //return dFechavigencia;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("No se pudo calcular la fecha Vigencia." + Ex.Message);
                new filelog("1 No se pudo calcular la fecha Vigencia.", Ex.Message);
                
            }
            return dFechavigencia;
        }

        public DateTime consulta_fechavigencia() {
            DateTime dFechavigencia = DateTime.Today; 
            try {

                conect con_consultafecha_legal = new conect();
                String sQuery = "select * from " + sTablaconsulta + " where `CasoId` = '" + sCasoidactual + "' and TipoSolicitudId = '" + sgTiposolicitudid + "'";
                MySqlDataReader resp_consvig = con_consultafecha_legal.getdatareader(sQuery);
                resp_consvig.Read();
                String sCasoFechaLegal = validareader("CasoFechaLegal", "CasoFechaLegal", resp_consvig).Text;
                String sCasoFechaVigencia = validareader("CasoFechaVigencia", "CasoFechaVigencia", resp_consvig).Text;
                String sCasoFechaRecepcion = validareader("CasoFechaRecepcion", "CasoFechaRecepcion", resp_consvig).Text;
                String sCasoFechaInternacional = validareader("CasoFechaInternacional", "CasoFechaInternacional", resp_consvig).Text;
                if (sCasoFechaVigencia!="")
                {
                    dFechavigencia = DateTime.Parse(sCasoFechaVigencia);
                }
                resp_consvig.Close();
                con_consultafecha_legal.Cerrarconexion();
            }catch(Exception Ex){
                dFechavigencia = DateTime.Today;
            }
            return dFechavigencia;
        }

        public DateTime consulta_fechapresentacion()
        {
            DateTime dFechapresentacion = DateTime.Today;
            try
            {
                conect con_consultafecha_legal = new conect();
                String sQuery = "select * from " + sTablaconsulta + " where `CasoId` = '" + sCasoidactual + "' and TipoSolicitudId = '" + sgTiposolicitudid + "'";
                MySqlDataReader resp_consvig = con_consultafecha_legal.getdatareader(sQuery);
                resp_consvig.Read();
                String sCasoFechaLegal = validareader("CasoFechaLegal", "CasoFechaLegal", resp_consvig).Text;
                String sCasoFechaRecepcion = validareader("CasoFechaRecepcion", "CasoFechaRecepcion", resp_consvig).Text;
                String sCasoFechaInternacional = validareader("CasoFechaInternacional", "CasoFechaInternacional", resp_consvig).Text;
                if (sCasoFechaInternacional != "")
                {
                    dFechapresentacion = DateTime.Parse(sCasoFechaInternacional);
                }else{
                    if (sCasoFechaRecepcion != "")
                    {
                        dFechapresentacion = DateTime.Parse(sCasoFechaRecepcion);
                    }
                }
                resp_consvig.Close();
                con_consultafecha_legal.Cerrarconexion();
            }catch (Exception Ex){
                dFechapresentacion = DateTime.Today;
            }
            return dFechapresentacion;
        }
        public void actualiza_fecha_vigencia() {/*Al ser llamdo desde pago de anualidades, sólo debe funcionar para el grupo de patentes*/
            try
            {
                conect con_consultafecha_legal = new conect();
                String sQuery = "select * from caso_patente where `CasoId` = '" + sCasoidactual + "' and TipoSolicitudId = '" + sgTiposolicitudid + "'";
                MySqlDataReader resp_consvig = con_consultafecha_legal.getdatareader(sQuery);
                resp_consvig.Read();
                String sCasoFechaLegal = validareader("CasoFechaLegal", "CasoFechaLegal", resp_consvig).Text;
                String sCasoFechaVigencia = validareader("CasoFechaVigencia", "CasoFechaVigencia", resp_consvig).Text;
                String sCasoFechaRecepcion = validareader("CasoFechaRecepcion", "CasoFechaRecepcion", resp_consvig).Text;
                String sCasoFechaInternacional = validareader("CasoFechaInternacional", "CasoFechaInternacional", resp_consvig).Text;

                resp_consvig.Close();
                con_consultafecha_legal.Cerrarconexion();
                if (sCasoFechaVigencia=="")
                {
                
                    String sFechalegal = "";
                    DateTime dFechavigencia;
                    try { 
                        if (sCasoFechaInternacional != "" && sCasoFechaInternacional != "0000-00-00" && sCasoFechaInternacional != "0000/00/00")
                        {
                            sFechalegal = sCasoFechaInternacional;
                        }
                        else {
                            if (sCasoFechaRecepcion != "" && sCasoFechaRecepcion != "0000-00-00" && sCasoFechaRecepcion != "0000/00/00"){
                                sFechalegal = sCasoFechaRecepcion;
                            }else {
                                MessageBox.Show("No se pudo calcular la fecha Vigencia.");
                                return;
                            }
                        }
                        //DateTime.ParseExact(tbDocumentofecharecepcion.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                        dFechavigencia = DateTime.Parse(sFechalegal);//, "dd-MM-yyyy", CultureInfo.InvariantCulture);//DateTime.Parse(sFechalegal);//DateTime.ParseExact(sFechalegal, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                        switch (sgTiposolicitudid)//Casos para generar anualidades
                        {
                            case "1"://Solicitud de Patentes
                                {
                                    dFechavigencia = dFechavigencia.AddYears(20);
                                } break;
                            case "2"://Modelo de utilidad
                                {
                                    dFechavigencia = dFechavigencia.AddYears(10);
                                } break;
                            case "3"://Diseño (Modelo)
                                {
                                    dFechavigencia = dFechavigencia.AddYears(15);
                                } break;
                            case "4"://Diseño Industrial (Dibujo)
                                {
                                    dFechavigencia = dFechavigencia.AddYears(15);
                                } break;
                        }
                    }catch(Exception Ex){
                        MessageBox.Show("No se pudo calcular la fecha Vigencia.");
                        return;
                    }

                    conect con_4_vig = new conect();
                    String updatevigenciacaso = "UPDATE `" + sTablaconsulta + "` SET `CasoFechaVigencia` = '" + dFechavigencia.ToString("yyyy/MM/dd") +
                        "' WHERE (`CasoId` = '" + sCasoidactual + "' and TipoSolicitudId = '" + sgTiposolicitudid + "' );";
                    MySqlDataReader resp_updatevig = con_4_vig.getdatareader(updatevigenciacaso);
                    resp_updatevig.Close();
                    con_4_vig.Cerrarconexion();
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show("No se pudo calcular la fecha Vigencia." + Ex.Message);
                new filelog("1 No se pudo calcular la fecha Vigencia.", Ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //hacemos las validaciones
            DateTime dFechavigencia;
            if (tbCodigo.Text.Length != 16 && sGrop == "1")
            {
                MessageBox.Show("Verifique el código de barras, deben ser 16 caracteres");
                if (tbCodigo.CanFocus)
                {
                    tbCodigo.Focus();
                }
                return;
            }
            //tbFolio
            if (tbFolio.Text == "")
            {
                MessageBox.Show("Verifique el Folio");
                if (tbFolio.CanFocus)
                {
                    tbFolio.Focus();
                }
                return;
            }

            //cbEsritos
            if (cbEsritos.Text == "")
            {
                MessageBox.Show("Debe seleccionar un tipo de escrito para poder agregar un Escrito");
                if (cbEsritos.CanFocus)
                {
                    cbEsritos.Focus();
                }
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



            try
            {
                String stbFechacalce = tbFechacalce.Text;
                DateTime oDate = DateTime.Parse(stbFechacalce);

                DateTime sFechAc = DateTime.Now.Date;
                if (oDate > sFechAc) // Si la fecha indicada es menor o igual a la fecha actual
                {
                    MessageBox.Show("Le fecha no puede ser mayor a la fecha acutal.");
                    if (tbFechacalce.CanFocus)
                    {
                        tbFechacalce.Focus();
                    }
                    return;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Debe Seleccionar una Fecha correcta.");
                return;
            }
            //tbFechacalce

            if (tbFilename.Text=="")
            {
                MessageBox.Show("Debe seleccionar un archivo antes de continuar.");
                if (tbFilename.CanFocus)
                {
                    tbFilename.Focus();
                }
                return;
            }

            if (cbPreparo.Text =="")
            {
                MessageBox.Show("Debe seleccionar un usuario.");
                if (cbPreparo.CanFocus)
                {
                    cbPreparo.Focus();
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
                                String fechaactual = DateTime.Now.ToString("yyyy/MM/dd");
                                String SubTipoDocumentoId = (cbEsritos.SelectedItem as ComboboxItem).Value.ToString();
                                String DocumentoCodigoBarras = tbCodigo.Text;
                                String DocumentoFechaCaptura = new DateTime().ToString();// now();
                                String DocumentoFecha = "";
                                String DocumentoFolio = tbFolio.Text;
                                String DocumentoFechaRecepcion = DateTime.ParseExact(tbDocumentofecharecepcion.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd");// tbDocumentofecharecepcion.Text;
                                String DocumentoObservacion = rtObservacion.Text;
                                DateTime dFEchanotificaciongeneral = DateTime.ParseExact(tbDocumentofecharecepcion.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                /*Validamos el tipo de escrito para pagar el primer quinquenio de las anualidades dependiendo el tipo de patente*/
                                if (SubTipoDocumentoId == "1181") {

                                    switch (sgTiposolicitudid)//Casos para pagar el las anualidades primeras o subsecuentes
                                    {
                                       
                                        case "2":
                                            {
                                                DateTime dsFechapresentaciong = DateTime.ParseExact(sgCasoFechaRecepcion, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                DateTime dFechalimitenuevaley = DateTime.ParseExact("26-04-2018", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                                if (dsFechapresentaciong >= dFechalimitenuevaley)
                                                {
                                                    pago_anualialidades_subsecuentes_DIS_MOD(1, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);
                                                }
                                                else
                                                {
                                                    pago_anualialidades_subsecuentes(1, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);
                                                }
                                            }
                                            break;
                                        case "3":
                                            {
                                                if (sCasoDisenoClasificacion == "5")
                                                {
                                                    pago_anualialidades_subsecuentes_DIS_MOD(1, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);
                                                }
                                                else {
                                                    DateTime dsFechapresentaciong = DateTime.ParseExact(sgCasoFechaRecepcion, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                    DateTime dFechalimitenuevaley = DateTime.ParseExact("26-04-2018", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                                    if (dsFechapresentaciong >= dFechalimitenuevaley)
                                                    {
                                                        pago_anualialidades_subsecuentes_DIS_MOD(1, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);
                                                    }
                                                }

                                                
                                            }
                                            break;
                                        case "4": {

                                                if (sCasoDisenoClasificacion == "5")
                                                {
                                                    pago_anualialidades_subsecuentes_DIS_MOD(1, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);
                                                }
                                                else
                                                {
                                                    DateTime dsFechapresentaciong = DateTime.ParseExact(sgCasoFechaRecepcion, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                                    DateTime dFechalimitenuevaley = DateTime.ParseExact("26-04-2018", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                                    if (dsFechapresentaciong >= dFechalimitenuevaley)
                                                    {
                                                        pago_anualialidades_subsecuentes_DIS_MOD(1, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);
                                                    }
                                                }
                                            }
                                            break;
                                    }

                                    
                                }


                                if (SubTipoDocumentoId == "140" || SubTipoDocumentoId == "141" || SubTipoDocumentoId == "69" || SubTipoDocumentoId == "1229" || SubTipoDocumentoId == "1249") //|| SubTipoDocumentoId == "1228")
                                {//El tipo de Escrito validamos si es el pago de título con o sin figuras para generar las anualidades
                                    //generaplazodeanualidadessubsecunetes();
                                    //new filelog("Debemos pagar anualidades con el documento", " linea1139 SubTipoDocumentoId:" + SubTipoDocumentoId);
                                    String sFechaparaanualidad = "";
                                    if (sgCasoFechaInternacional != "")
                                    {
                                        sFechaparaanualidad = sgCasoFechaInternacional;
                                    }else{
                                        sFechaparaanualidad = sgCasoFechaRecepcion;
                                    }
                                    switch (sgTiposolicitudid)//Casos para pagar el las anualidades primeras o subsecuentes
                                    {
                                        case "1": {//Partentes
                                            //funcion para pagar el primer quinquenio 
                                            pago_anualialidades_subsecuentes(1, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);
                                        } break;//Solicitud de Patentes
                                        case "2": {//Modelos
                                                   //funcion para pagar el primer quinquenio 
                                                   //pago_anualialidades_subsecuentes();
                                                   //pago_anualialidades_subsecuentes(1, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);
                                                    //DateTime dsFechapresentaciong = DateTime.ParseExact(sgCasoFechaRecepcion, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                    //DateTime dFechalimitenuevaley = DateTime.ParseExact("26-04-2018", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                                    //if (dsFechapresentaciong >= dFechalimitenuevaley)
                                                    //{
                                                    //    pago_anualialidades_subsecuentes_DIS_MOD(1, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);
                                                    //}
                                                    //else
                                                    //{
                                                    //    pago_anualialidades_subsecuentes(1, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);

                                                    //}
                                                pago_anualialidades_subsecuentes(1, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);
                                            } break;//Modelo de utilidad
                                        case "3":
                                            {//Diseño (Modelo)

                                                DateTime dsFechapresentaciong = DateTime.ParseExact(sgCasoFechaRecepcion, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                DateTime dFechalimitenuevaley = DateTime.ParseExact("26-04-2018", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                                if (dsFechapresentaciong >= dFechalimitenuevaley)
                                                {
                                                    //pago_anualialidades_subsecuentes_DIS_MOD(1, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);
                                                }
                                                else{
                                                    pago_anualialidades_subsecuentes(1, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);

                                                }
                                            //funcion para pagar el primer quinquenio 
                                            //pago_anualialidades_subsecuentes();
                                        } break;//Diseño (Modelo)
                                        case "4":
                                            {//Diseño Industrial (Dibujo)

                                                DateTime dsFechapresentaciong = DateTime.ParseExact(sgCasoFechaRecepcion, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                                DateTime dFechalimitenuevaley = DateTime.ParseExact("26-04-2018", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                                if (dsFechapresentaciong >= dFechalimitenuevaley)
                                                {
                                                    //pago_anualialidades_subsecuentes_DIS_MOD(1, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);
                                                }
                                                else
                                                {
                                                    pago_anualialidades_subsecuentes(1, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);

                                                }
                                                //pago_anualialidades_subsecuentes(1, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);
                                                //funcion para pagar el primer quinquenio 
                                                //pago_anualialidades_subsecuentes();
                                            } break;//Diseño Industrial (Dibujo)
                                        case "5": { 
                                        
                                        } break;//Registro de patente
                                        case "19": {
                                            //funcion para pagar el primer quinquenio 
                                            //pago_anualialidades_subsecuentes();
                                            pago_anualialidades_subsecuentes(1, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);
                                        } break;//Esquea trazado de circuito
                                    }

                                    /*Tenemos que validar que sean los plazos correctos 
                                         * y validad si Tiposolicitud para genear los plazos correctos*/
                                    DateTime dFecha_vigencia = consulta_fechavigencia();
                                    DateTime dFechapresentacion = consulta_fechapresentacion();
                                    DateTime sFehcaultimopago = DateTime.Parse(tbDocumentofecharecepcion.Text);
                                    sFehcaultimopago = sFehcaultimopago.AddYears(5);//le sumamos 5 años al ultimo pago
                                    int iMesaniversario = dFechapresentacion.Month;//obtenemos el mes aniversario
                                    int iYear = sFehcaultimopago.Year;//obtenemos al año de la suma de los cinco años a la fecha del ultimo pago

                                    DateTime dFechaplzovigencia = new DateTime(iYear, iMesaniversario, 1);// formamos la fecha del plazo con el mes aniversario y el año del ultimo pago más cinco años

                                    DateTime dFechaplzovigencia_reporte = new DateTime(iYear, iMesaniversario, 1);
                                    dFechaplzovigencia_reporte = dFechaplzovigencia_reporte.AddMonths(-3);
                                    //generaplazo(dFechaplzovigencia_reporte, "33", sCasoidactual, sgTiposolicitudid, "0");//recordatorio de plazo de anualidades subsecuentes
                                    //generaplazo(dFechaplzovigencia, "6", sCasoidactual, sgTiposolicitudid, "0");//plazo de anualidades subsecuentes

                                    cambioestatus(SubTipoDocumentoId);
                                }

                                String SDocumentoFechaFirma = "";
                                try
                                {
                                    /*Debemos considerar para los escritos y solicitud la fecha de sello*/
                                    DocumentoFecha = DateTime.ParseExact(tbDocumentofecharecepcion.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbFechacalce.Text;// now();
                                    SDocumentoFechaFirma = DateTime.ParseExact(tbFechacalce.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbFechacalce.Text;// now();
                                }
                                catch (Exception E)
                                {//No hay fecha y de echo no debe haber
                                    DocumentoFecha = "";
                                }

                                String sDocumentoidfolio = "";
                                String sfoliodocumentocatendio = "";
                                String scodigodocumentocatendio = "";
                                //consultamos el documento si es que contestó alguno
                                if (cbOficio.SelectedItem !=null) {// si se selecciona un plazo a contestar tomamos el folio y documento relacionado a ese plazo de la tabla plazosdetalle
                                    String sPlazosdetalleid_folio = (cbOficio.SelectedItem as ComboboxItem).Value.ToString();
                                    if (sPlazosdetalleid_folio != "") {
                                        conect con_consultafolio = new conect();
                                        String sconsultafoliodocid = " SELECT  " +
                                                                    "     DocumentoCodigoBarras, " +
                                                                    "     DocumentoFolio " +
                                                                    " FROM " +
                                                                    "     plazos_detalle, " +
                                                                    "     documento " +
                                                                    " WHERE " +
                                                                    " plazos_detalle.Plazos_detalleid = " + sPlazosdetalleid_folio +
                                                                "   and plazos_detalle.DocumentoId = Documento.DocumentoId; ";
                                        MySqlDataReader resp_sConsultafolio = con_consultafolio.getdatareader(sconsultafoliodocid);
                                        resp_sConsultafolio.Read();
                                        sfoliodocumentocatendio = validareader("DocumentoFolio", "DocumentoFolio", resp_sConsultafolio).Text;
                                        scodigodocumentocatendio = validareader("DocumentoCodigoBarras", "DocumentoCodigoBarras", resp_sConsultafolio).Text;
                                        resp_sConsultafolio.Close();
                                        con_consultafolio.Cerrarconexion();
                                    }
                                }

                                conect con_inserdoc = new conect();
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
                                                            " `foliodocumentocontesto`, " +
                                                            " `codigobarrasdoccontesto`, " +

                                                            " `casoid`, " +
                                                            " `TipoSolicitudId`, " +
                                                            " `RelacionDocumentoLink`, " +

                                                            " `usuarioIdPreparo`) " +
                                                            " VALUES " +
                                                            " (null, " +
                                                            " '" + DocumentoCodigoBarras + "', " +
                                                            " '" + SubTipoDocumentoId + "', " +
                                                            " '" + SDocumentoFechaFirma + "', " +
                                                            " '" + DocumentoFolio + "', " +
                                                            " '" + DocumentoFecha + ": ', " +

                                                            " '" + fechaactual + "', " +

                                                            " '" + DocumentoObservacion + "', " +
                                                            " 0, " +
                                                            " 1, " +
                                                            " 0, " +
                                                            " '', " +
                                                            " '" + sfoliodocumentocatendio + "', " +//folio documento contestó
                                                            " '" + scodigodocumentocatendio + "', " +//folio documento contestó

                                                            " '" + sCasoidactual + "', " +//sCasoidactual documento contestó
                                                            " '" + sgTiposolicitudid + "', " +//sgTiposolicitudid documento contestó
                                                            " '" + sfilePath_2.Replace("\\", "\\\\") + "', " +//sfilePath_2 documento contestó

                                                            " 0); ";

                                MySqlDataReader resp_escritos = con_inserdoc.getdatareader(insertdocumento);
                                String documentoid = "";
                                if (resp_escritos != null)
                                {
                                    resp_escritos.Close();
                                    con_inserdoc.Cerrarconexion();
                                    conect con_2 = new conect();
                                    MySqlDataReader resp_docid = con_2.getdatareader("select DocumentoId from documento order by documentoid desc limit 1;");
                                    if (resp_docid != null)
                                    {
                                        resp_docid.Read();
                                        
                                        //pago de anualidades subsecuentes

                                        if (SubTipoDocumentoId == "153")/*Pago de anualidades de Diseño de 16 a 20 para generar el plazo*/
                                        {
                                            pagoanualidadesubsecuentes(sCasoidactual, sgTiposolicitudid, DocumentoFechaRecepcion);
                                        }
                                        //File.Copy(sfilePath, sfilePath_2);
                                        // movimos el insert al final ya que no podemos insertar antes de atender el plazo devido al trigger

                                        //conect con_inserrela = new conect();
                                        documentoid = validareader("DocumentoId", "DocumentoId", resp_docid).Text;
                                        //String insertrelaciondoc = " INSERT INTO `relaciondocumento` " +
                                        //                            " (`RelacionDocumentoId`, " +
                                        //                            " `DocumentoId`, " +
                                        //                            " `CasoId`, " +
                                        //                            " `TipoSolicitudId`, " +

                                        //                            " `RelacionDocumentoLink`) " +
                                        //                            " VALUES " +
                                        //                            " (null, " +
                                        //                            " '" + documentoid + "', " +
                                        //                            " '" + sCasoidactual + "', " +
                                        //                            " '" + sgTiposolicitudid + "', " +
                                        //                            " '" + sfilePath_2.Replace("\\", "\\\\") + "');";
                                        //MySqlDataReader esp_insertrelaciona = con_inserrela.getdatareader(insertrelaciondoc);
                                        //if (esp_insertrelaciona != null)
                                        //{
                                        //    esp_insertrelaciona.Close();
                                        //    con_inserrela.Cerrarconexion();

                                        /*Después de agregar el documento pagamos las anualidades subsecuentes y vrificamos si tiene plazos que agregar dependiendo del documento*/
                                        if (SubTipoDocumentoId == "1254")/*Pago de anualidades de Diseño de 16 a 20 para generar el plazo*/
                                                {
                                                    pago_anualialidades_subsecuentes(4, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);
                                                    //Debemos agregar 5 años a la fecha vigencia
                                                    dFechavigencia = consulta_fechavigencia();//actualiza_fecha_vigencia(5);
                                                    dFechavigencia = dFechavigencia.AddYears(4).AddMonths(6);
                                                    //generaplazo(dFechavigencia, "46", sCasoidactual, sgTiposolicitudid, documentoid);// Plazo Renovar diseño Industrial 21 a 25
                                                }

                                                if (SubTipoDocumentoId == "1255")/*Pago de anualidades de Diseño de 21 a 25  para generar el plazo*/
                                                {
                                                    pago_anualialidades_subsecuentes(5, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);
                                                    dFechavigencia = consulta_fechavigencia();//actualiza_fecha_vigencia(5);
                                                    dFechavigencia = dFechavigencia.AddYears(4).AddMonths(6);
                                                }

                                                if (SubTipoDocumentoId == "1253")/*Si éste es el documento y el tipo es Modelo*/
                                                {
                                                    pago_anualialidades_subsecuentes(3, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);
                                                    /*dFechavigencia = actualiza_fecha_vigencia(5);
                                                    dFechavigencia = dFechavigencia.AddYears(4).AddMonths(6);
                                                    generaplazo(dFechavigencia, "", sCasoidactual, sgTiposolicitudid, documentoid);*/
                                                }

                                            //new filelog("Debemos pagar anualidades con el documento", " linea 1338 SubTipoDocumentoId:" + SubTipoDocumentoId);
                                            cambioestatus(SubTipoDocumentoId);

                                            //conect con_5 = new conect();
                                            //String sconsultaeindtipo = "select SubTipoDocumentoIndTipo from subtipodocumento where SubTipoDocumentoId = " + SubTipoDocumentoId;
                                            //MySqlDataReader resp_consultaestipo = con_5.getdatareader(sconsultaeindtipo);
                                            //resp_consultaestipo.Read();
                                            //String sSubTipoDocumentoIndTipo = validareader("SubTipoDocumentoIndTipo", "SubTipoDocumentoIndTipo", resp_consultaestipo).Text;
                                            //if (resp_consultaestipo != null)
                                            //{
                                            //    /*if (tbExpediente__.Text != "" && sGEstatusCasoId == "1")
                                            //    {
                                            //        conect con_51 = new conect();
                                            //        String updateestatuscasoexp = "UPDATE `" + sTablaconsulta + "` SET `CasoNumeroExpedienteLargo` = '" + tbExpediente__.Text + "' WHERE (`CasoId` = '" + sCasoidactual + "');";
                                            //        MySqlDataReader resp_updatecasoexpediente = con_51.getdatareader(updateestatuscasoexp);
                                            //        if (resp_updatecasoexpediente != null)
                                            //        {
                                            //            resp_updatecasoexpediente.Close();
                                            //            con_51.Cerrarconexion();
                                            //        }
                                            //    }*/
                                            //    cambioestatus(sSubTipoDocumentoIndTipo);
                                            //}
                                        //}
                                    }
                                    resp_docid.Close();
                                    con_2.Cerrarconexion();

                                    
                                    
                                    //new filelog("Debemos pagar anualidades con el documento", " :" + dsFechapresentaciong.ToString("ddMMyyyy") + " < " + dFechalimitenuevaley.ToString("ddMMyyyy")) ;
                                    //if (dsFechapresentaciong >= dFechalimitenuevaley)
                                    if ((SubTipoDocumentoId == "153" || SubTipoDocumentoId == "1228" || SubTipoDocumentoId == "69") )//&& dsFechapresentaciong < dFechalimitenuevaley)
                                    { //Validamos el pago de las anualidades subsecuentes
                                        try {
                                            DateTime dsFechapresentaciong = DateTime.ParseExact(sgCasoFechaRecepcion, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                            DateTime dFechalimitenuevaley = DateTime.ParseExact("26-04-2018", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                        }
                                        catch (Exception ex) {
                                            MessageBox.Show("Excepcion: "+ex.Message);
                                            
                                            return;
                                        }
                                        

                                        //Cambiamos de lugar el pago de las anualidades para que al insertar el documento podamos calcular la fecha del siguiente pago de anualidades

                                        conect con_pagoanualidad = new conect();
                                        String sUltimaanualidadpagada = " SELECT  " +
                                                                        "     AnualidadQuinquenio " +
                                                                        " FROM " +
                                                                        "     anualidad " +
                                                                        " WHERE " +
                                                                        "     casoid = " + sCasoidactual +
                                                                        "         AND EstatusAnualidadId = 1 " +
                                                                        "         AND TipoSolicitudId = " + sgTiposolicitudid +
                                                                        " ORDER BY AnualidadSecuencia ASC " +
                                                                        " LIMIT 1; ";
                                        MySqlDataReader resp_escritos_anualidad = con_pagoanualidad.getdatareader(sUltimaanualidadpagada);
                                        resp_escritos_anualidad.Read();
                                        String sAnualidadQuinquenio = validareader("AnualidadQuinquenio", "AnualidadQuinquenio", resp_escritos_anualidad).Text;
                                        int sAnualidadapagar = 0;// Int32.Parse(sAnualidadQuinquenio);
                                        if (sAnualidadQuinquenio!="") {
                                            sAnualidadapagar = Int32.Parse(sAnualidadQuinquenio);
                                        }
                                        resp_escritos_anualidad.Close();
                                        con_pagoanualidad.Cerrarconexion();
                                        ////pago_anualialidades_subsecuentes(sAnualidadapagar, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);
                                        ///
                                        //Fin del pago de anualidades

                                        //new filelog("Despues del pago de las anualidades", " :");

                                        /*debemos atender el plazo de pago de anualidades*/
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
                                                                            "     and Tipo_plazoid = 6 " +//plazo de anualidades subsecuentes
                                                                            "     and CasoId = " + sCasoidactual +
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


                                        if (sPlazosdetalleidg!="")//para saber si atiende un plazo de anualidades subsecuentes
                                        {
                                            //atenderplazoconescrito(SubTipoDocumentoId, documentoid, sPlazosidg, sPlazosdetalleidg);/*Atender plazo de anualidades subsecuentes si existiese*/
                                            bgAtendiooficio = false;
                                        }


                                        /*Tenemos que validar que sean los plazos correctos 
                                         * y validad si Tiposolicitud para genear los plazos correctos*/
                                        DateTime dFecha_vigencia = consulta_fechavigencia();
                                        DateTime dFechapresentacion = consulta_fechapresentacion();
                                        DateTime sFehcaultimopago = DateTime.Parse(tbDocumentofecharecepcion.Text);
                                        sFehcaultimopago = sFehcaultimopago.AddYears(5);//le sumamos 5 años al ultimo pago
                                        int iMesaniversario = dFechapresentacion.Month;//obtenemos el mes aniversario
                                        int iYear = sFehcaultimopago.Year;//obtenemos al año de la suma de los cinco años a la fecha del ultimo pago

                                        if (sAnualidadQuinquenio == "2" && sgTiposolicitudid == "2")//si es la 2 y el tipo de solicitud es Modelo entonces 
                                        {
                                            DateTime dFechaplzovigencia = new DateTime(iYear, iMesaniversario, 1);// formamos la fecha del plazo con el mes aniversario y el año del ultimo pago más cinco años
                                            //generaplazo(dFechaplzovigencia, "6", sCasoidactual, sgTiposolicitudid, documentoid);
                                        }

                                        if ((sAnualidadQuinquenio == "2") && (sgTiposolicitudid == "3" || sgTiposolicitudid == "4"))//si el quinquenio es 3 o 4  y el tipo de solicitud es Diseño entonces
                                        {
                                            DateTime dFechaplzovigencia = new DateTime(iYear, iMesaniversario, 1);// formamos la fecha del plazo con el mes aniversario y el año del ultimo pago más cinco años
                                            //generaplazo(dFechaplzovigencia, "6", sCasoidactual, sgTiposolicitudid, documentoid);
                                        }


                                        dFecha_vigencia = dFecha_vigencia.AddMonths(-6);
                                        if ((sAnualidadQuinquenio == "3") && (sgTiposolicitudid == "3" || sgTiposolicitudid == "4"))//si el quinquenio es 3 o 4  y el tipo de solicitud es Diseño entonces
                                        {/*ya seria el plazo de 16 a 20 para los diseños*/
                                            DateTime dFechaplzovigencia = new DateTime(iYear, iMesaniversario, 1);// formamos la fecha del plazo con el mes aniversario y el año del ultimo pago más cinco años
                                            //generaplazo(dFecha_vigencia, "45", sCasoidactual, sgTiposolicitudid, documentoid);
                                        }
                                        
                                        
                                        
                                        /*FIN Editamos el plazo que debe tener de presentar solicitus*/

                                    }

                                }

                                /***
                                 *
                                 * Lo primero que debemos validar es si va atender un plazo para tomar el Plazosid y hacerlo pareja con plazos_detalleid
                                 * por lo que se encesitan los valores plazos_detalleid y PllazosId
                                 * 
                                 */
                                String sPlazosid_value = "";
                                String sPlazos_detalleid_value = "";
                                if (bgAtendiooficio)
                                {
                                    if (SubTipoDocumentoId != "1052")
                                    {
                                        if (cbOficio.SelectedItem != null)
                                        {
                                            /*Debe atender el plazo seleccionado con los siguientes Plazosid y plazos_detalleid*/
                                            String[] Ids = (cbOficio.SelectedItem as ComboboxItem).Text.Split('_');
                                            sPlazosid_value = Ids[0];
                                            sPlazos_detalleid_value = (cbOficio.SelectedItem as ComboboxItem).Value.ToString();
                                        }
                                        else
                                        {

                                            /*
                                             * Creamos el plazo en la tabla Plazo la relacion con casoid y tiposolicitudid 
                                             * generamos un nuevo plazo para reportar al cliente
                                             */
                                            conect conect_plazosid = new conect();
                                            String sQuery_plazos = " INSERT INTO `plazos`(`Plazosid`,`CasoId`,`TipoSolicitudId`,`Fecha_creacion`)VALUES " +
                                                                            " (null, " +
                                                                            " '" + sCasoidactual + "', " +
                                                                            " '" + sgTiposolicitudid + "', " +
                                                                            " curdate()); ";

                                            MySqlDataReader resp_plazos = conect_plazosid.getdatareader(sQuery_plazos);
                                            if (resp_plazos.RecordsAffected == 1)
                                            {
                                                conect conect_plazosid_relacion = new conect();
                                                String sQuery_plazos_relacion_general = "select * from plazos order by  plazosid desc limit 1;";
                                                MySqlDataReader resp_plazos_relacion = conect_plazosid_relacion.getdatareader(sQuery_plazos_relacion_general);
                                                while (resp_plazos_relacion.Read())
                                                {
                                                    sPlazosid_value = validareader("Plazosid", "Plazosid", resp_plazos_relacion).Text;
                                                }
                                                resp_plazos_relacion.Close();
                                                conect_plazosid_relacion.Cerrarconexion();
                                            }
                                            resp_plazos.Close();
                                            conect_plazosid.Cerrarconexion();
                                            sPlazos_detalleid_value = "";
                                        }
                                        //el oficio seleccionado tiene como value el idplazogeneral
                                        //atenderplazoconescrito(SubTipoDocumentoId, documentoid, sPlazosid_value, sPlazos_detalleid_value);
                                    }
                                }
                                //aquí terminamos y podriamos aggrear la funcion de insert documentorelaciona
                                
                                sinsertardocumentorelacion(documentoid);
                                generaplazosplazosdeoficio(SubTipoDocumentoId, documentoid, dFEchanotificaciongeneral, dFEchanotificaciongeneral, login.sId);
                                //String splazodetalleid = "";
                                atenderplazoconescrito(sPlazos_detalleid_value);
                                DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Debe seleccionar un caso y un escrito para poder guardar");
                            }

                        }
                        catch (Exception E)
                        {
                            MessageBox.Show("Error al intentar Cargar el documento "+E.Message);
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
                        if (sGrop != sCasogrupo)
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
        public void sinsertardocumentorelacion(String documentoid) {
            try {
                conect con_inserrela = new conect();
                //documentoid = validareader("DocumentoId", "DocumentoId", resp_docid).Text;
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
                MySqlDataReader esp_insertrelaciona = con_inserrela.getdatareader(insertrelaciondoc);
                esp_insertrelaciona.Close();
                con_inserrela.Cerrarconexion();


            } catch (Exception exs) {
                new filelog("insertrelaciondoc", " :"+exs.Message);
            }
            
        }

        public void pagoanualidadesubsecuentes(String sCasoidactual, String sgTiposolicitudid, String DocumentoFechaRecepcion) { 
            try {
                conect con_pagoanualidad = new conect();
                String sUltimaanualidadpagada = " SELECT  " +
                                                "     AnualidadQuinquenio " +
                                                " FROM " +
                                                "     anualidad " +
                                                " WHERE " +
                                                "     casoid = " + sCasoidactual +
                                                "         AND EstatusAnualidadId = 1 " +
                                                "         AND TipoSolicitudId = " + sgTiposolicitudid +
                                                " ORDER BY AnualidadSecuencia ASC " +
                                                " LIMIT 1; ";
                MySqlDataReader resp_escritos_anualidad = con_pagoanualidad.getdatareader(sUltimaanualidadpagada);
                resp_escritos_anualidad.Read();
                String sAnualidadQuinquenio = validareader("AnualidadQuinquenio", "AnualidadQuinquenio", resp_escritos_anualidad).Text;
                int sAnualidadapagar = Int32.Parse(sAnualidadQuinquenio);
                resp_escritos_anualidad.Close();
                con_pagoanualidad.Cerrarconexion();
                pago_anualialidades_subsecuentes(sAnualidadapagar, DocumentoFechaRecepcion, sCasoidactual, sgTiposolicitudid);
            }
            catch (Exception exs) {
                new filelog("Pago de anualidades subsecuentes", ""+exs.Message);
            }
        }

        public void cambioestatus(string SubTipoDocumentoId)
        {
            try {

                conect con_5 = new conect();
                String sconsultaeindtipo = "select SubTipoDocumentoIndTipo from subtipodocumento where SubTipoDocumentoId = " + SubTipoDocumentoId;
                MySqlDataReader resp_consultaestipo = con_5.getdatareader(sconsultaeindtipo);
                resp_consultaestipo.Read();
                String sSubTipoDocumentoIndTipo = validareader("SubTipoDocumentoIndTipo", "SubTipoDocumentoIndTipo", resp_consultaestipo).Text;
                if (resp_consultaestipo != null && sSubTipoDocumentoIndTipo!="")
                {
                    /*if (tbExpediente__.Text != "" && sGEstatusCasoId == "1")
                    {
                        conect con_51 = new conect();
                        String updateestatuscasoexp = "UPDATE `" + sTablaconsulta + "` SET `CasoNumeroExpedienteLargo` = '" + tbExpediente__.Text + "' WHERE (`CasoId` = '" + sCasoidactual + "');";
                        MySqlDataReader resp_updatecasoexpediente = con_51.getdatareader(updateestatuscasoexp);
                        if (resp_updatecasoexpediente != null)
                        {
                            resp_updatecasoexpediente.Close();
                            con_51.Cerrarconexion();
                        }
                    }*/
                    //cambioestatus(sSubTipoDocumentoIndTipo);
                }
                con_5.Cerrarconexion();
                resp_consultaestipo.Close();

                if (sSubTipoDocumentoIndTipo!="") 
                    {
                    conect con_3 = new conect();
                    String sConsultaestatussiguiente = "select EstatusCasoId, Get_Estatusdescrip(EstatusCasoId) as EstatusCasoDescrip from " +
                                                        " subtipodocumentoestatuscaso where SubTipoDocumentoIndTipo = " +
                                                        sSubTipoDocumentoIndTipo +
                                                        " and grupoid = " + sGrop;
                    MySqlDataReader resp_consultaestatuscaso = con_3.getdatareader(sConsultaestatussiguiente);
                    try
                    {
                        resp_consultaestatuscaso.Read();
                        String sEstatusactual = validareader("EstatusCasoId", "EstatusCasoId", resp_consultaestatuscaso).Text;
                        String sEstatusCasoDescrip = validareader("EstatusCasoDescrip", "EstatusCasoId", resp_consultaestatuscaso).Text;
                        resp_consultaestatuscaso.Close();
                        con_3.Cerrarconexion();

                        if (sEstatusactual != "")
                        {
                            conect con_4 = new conect();
                            String updateestatuscaso = "UPDATE `" + sTablaconsulta +
                                "` SET `EstatusCasoId` = '" + sEstatusactual +
                                "' WHERE (`CasoId` = '" + sCasoidactual + "');";
                            MySqlDataReader resp_updatecaso = con_4.getdatareader(updateestatuscaso);
                            if (resp_updatecaso != null)
                            {
                                resp_updatecaso.Close();
                                con_4.Cerrarconexion();

                                MessageBox.Show("Documento agregado correctamente.");
                                sEstatusnuevodepuesdensert = sEstatusCasoDescrip;
                                DialogResult = DialogResult.OK;
                                capform.Show();
                                this.Close();
                            }
                        }


                    }
                    catch (Exception E)
                    {
                        //no hay cambio de estatus porque este documento no esta asignado para cambiar de estatus.
                        new filelog("Debemos pagar anualidades con el documento", " :"+E.Message);
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
            catch (Exception Ex) { 
            }
        }

        /**
         * Agregamos ésta funciona para crear plazos dentro de los escritos, cuando son casos especiales como reportar las renovaciones de Casos Tipo Diseño Industrial
         * y de los casos Modelo de Utilidad al subir un documento Renovacion en los casos mixtos,  ó al agregar el pago de las renovacion de los 5 años siguientes
         * En Marcas para generar los plazso de declaración de Uso de 3 y 6 años
         * 
         */
        public void generaplazo(DateTime ssFechaplazoregular, String sTipoplazo, String sCasoid, String sTiposolicitud, String documentoid)
        {
            try
            {
                String sPlazosid = "";
                conect conect_plazosid = new conect();
                String sQuery_plazos = " INSERT INTO `plazos`(`Plazosid`,`CasoId`,`TipoSolicitudId`,`Fecha_creacion`)VALUES " +
                                                " (null, " +
                                                " '" + sCasoid + "', " +
                                                " '" + sTiposolicitud + "', " +
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
                }
                resp_plazos.Close();
                conect_plazosid.Cerrarconexion();

                if (sPlazosid != "")//Valida que se haya generado el plazo correctamente
                {
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
                                                    " '" + login.sId + "', " +
                                                    " '" + sTipoplazo + "', " +
                                                    " '1', " +
                                                    
                                                    " '" + ssFechaplazoregular.ToString("yyyy/MM/dd") + "');";

                    MySqlDataReader resp_plazo = conect_plazoid.getdatareader(sQuery_plazo_general);
                    if (resp_plazo.RecordsAffected == 1)
                    {
                        //new filelog("Plazo agregardo: plazosid : " + sPlazosid, " usuario :" + login.sId);
                    }
                    resp_plazo.Close();
                    conect_plazoid.Cerrarconexion();

                }
                else
                {
                    MessageBox.Show("No se pudo generar el plazo.");
                }
            }
            catch (Exception Ex)
            {
                new filelog("generaplazo", ":" + Ex.Message);
            }
        }

        public void generaplazodeanualidadessubsecunetes(String sCasoid, String Tiposolicitudid)
        {
            //tipoplazoid_impi == "33"
            /*
            * Éste aviso debe tener como fecha de vigencia  5 años después de la notificación y tomar el mes de la fecha presentación y al resultado restarle 
            * 3 meses para poder avisar al cliente con anterioridad al pago de las anualidades subsecuentes.
            * Se tomará los siguientes campos para tomar el mes
            * 1.- CasoFechaInternacional
            * 2.- CasoFechaLegal
            * 3.- CasoFechaRecepcion
            */
            //try {
            //    String CasoFechaInternacional = "", CasoFechaLegal = "", CasoFechaRecepcion = "";
            //    conect conect_plazosid_relacion = new conect();
            //    String sQuery_plazos_relacion_general = "select * from caso_patente where casoid =" + sCasoid + " and Tiposolicitudid = " + Tiposolicitudid;// +" order by  plazosid desc limit 1;" + sCasoid;
            //    MySqlDataReader resp_plazos_relacion = conect_plazosid_relacion.getdatareader(sQuery_plazos_relacion_general);
            //    while (resp_plazos_relacion.Read())
            //    {
            //        try
            //        {
            //            CasoFechaInternacional = validareader("CasoFechaInternacional", "casoid", resp_plazos_relacion).Text;
            //            CasoFechaLegal = validareader("CasoFechaLegal", "casoid", resp_plazos_relacion).Text;
            //            CasoFechaRecepcion = validareader("CasoFechaRecepcion", "casoid", resp_plazos_relacion).Text;
            //        }catch (Exception Ex){
            //        }
            //    }
            //    resp_plazos_relacion.Close();
            //    conect_plazosid_relacion.Cerrarconexion();
            //    String sFechamesaniversario = "";
            //    if (CasoFechaInternacional != "")
            //    {
            //        sFechamesaniversario = CasoFechaInternacional;
            //    }else{
            //        if (CasoFechaLegal != "")
            //        {
            //            sFechamesaniversario = CasoFechaLegal;
            //        }else{
            //            sFechamesaniversario = CasoFechaRecepcion;
            //        }
            //    }
            //    if (sFechamesaniversario != "")
            //    {
            //        int iMes = DateTime.Parse(sFechamesaniversario).Month;

            //        /* FIN Éste aviso debe tener como fecha de vigencia  5 años ...*/
            //        dFecha_notificacion_impi = dFecha_notificacion_impi.AddYears(5);
            //        int ianoVigencia = dFecha_notificacion_impi.Year;

            //        sFechaplazoregular = new DateTime(ianoVigencia, iMes, 1);
            //        sFechaplazoregular = sFechaplazoregular.AddMonths(-3);
            //        ssFechaplazoregular = sFechaplazoregular.ToString("yyyy-MM-dd");
            //        ssFechaplazoregular3meses = "";
            //        ssFechaplazoregular4meses = "";
            //    }
            //    else
            //    {
            //        MessageBox.Show("No existen Fechas internacional, Legal o recepción, No se puede crear plazo de reportar plazo de anualidades subsecuentes.");
            //        return;
            //    }
            //    conect conect_plazoid = new conect();
            //    String sQuery_plazo_general = " INSERT INTO `plazos_detalle` " +
            //                                    " (`Plazos_detalleid`, " +
            //                                    " `Plazosid`, " +//
            //                                    " `documentoid`, " +//documentoid
            //                                    " `usuario_creo_plazodetalle`, " +
            //                                    " `Tipo_plazoid`, " +
            //                                    " `Estatus_plazoid`, " +
            //                                    " `Fecha_notificacion`, " +
            //                                    " `Fecha_Vencimiento`, " +
            //                                    " `Fecha_vencimiento_3m`, " +
            //                                    " `Fecha_vencimiento_4m`) " +
            //                                    " VALUES (''," +
            //                                    " '" + sPlazosid + "', " +
            //                                    " '" + documentoid + "', " +
            //                                    " '" + login.sId + "', " +
            //                                    " '" + tipoplazoid_impi + "', " +
            //                                    " '1', " +
            //                                    " '" + sFechanotificacion + "', " +
            //                                    " '" + ssFechaplazoregular + "', " +
            //                                    " '" + ssFechaplazoregular3meses + "', " +
            //                                    " '" + ssFechaplazoregular4meses + "');";

            //    //" '" + tipoplazoid_avisocliente + "', " +
            //    //" '1', " +
            //    //" '" + documentoid + "', " +
            //    //" '" + login.sId + "');";

            //    MySqlDataReader resp_plazo = conect_plazoid.getdatareader(sQuery_plazo_general);
            //    if (resp_plazo.RecordsAffected == 1)
            //    {
            //        ibBanderacreadetalleplazo_contador++;
            //        //MessageBox.Show("Se creó un plazo.");
            //        /*aqui validamos que se inserto el plazo*/
            //    }
            //    resp_plazo.Close();
            //    conect_plazoid.Cerrarconexion();
            //}catch(Exception Ex){
            //}
        }

        


        /***
         * Ésta Función atenderplazoconescrito
         * atendera el plazo impi,
         * por lo que debe tomar la fecha actual, 
         * la persona que lo atendio,
         * el id del documento con el que lo atendió (si existe)
         * y colocará la fecha actuál más dos días como vigencia para el aviso al cliente 
         * 
         */
        public void atenderplazoconescrito(string SubTipoDocumentoId, string documentoid, String sPlazosid, String splazos_detalleid)
        {
            try {

                /**
                 * Consultamos el documento para saber que plazo le corresponde (Debería ser reportar escrito y debe ser uno).
                 */
                String relacion_plazo_subtipodocumentoid = "";
                String tipoplazoid_impi = "";
                String splazos_detalleid_ult = "";
                
                relacion_plazo_subtipodocumentoid = SubTipoDocumentoId;
                tipoplazoid_impi = "29";/*Reportar Escrito al cliente*/
                                
                DateTime dFechaactual = DateTime.Today;
                dFechaactual = dFechaactual.AddDays(1);/*Ëste plazo tiene una duración de un día*/
                String ssFechaplazoregular = dFechaactual.ToString("yyyy/MM/dd");
                object sPreparo = (cbPreparo.SelectedItem as ComboboxItem).Value;
                
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
                                                " '" + tipoplazoid_impi + "', " +
                                                " '1', " +
                                                
                                                " '" + ssFechaplazoregular + "');";                    

                MySqlDataReader resp_plazo = conect_plazoid.getdatareader(sQuery_plazo_general);
                if (resp_plazo.RecordsAffected == 1)
                {

                    /*aqui validamos que se inserto el plazo  y buscamos su id en el caso de que atienda un oficio se necesitará*/
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

                /*FIN creamos el subplazo del escrito , es un subplazo de aviso del escrito al cliente
                    Validamos que se eligiera un oficio para atender
                 */

                if (splazos_detalleid != "")//splazos_detalleid es el plazo que tenemos que atender
                {
                    DateTime sFechaselloAtyenidioplazo = DateTime.Parse(tbDocumentofecharecepcion.Text);//Fecha sello impi la fecha en que se atendio el plazo
                    DateTime sFechaactual = DateTime.Today;/**/
                    conect con1 = new conect();
                    String sQueryallescritos = " UPDATE `plazos_detalle` SET " +
                                                " `Atendio_Plazos_detalleid` = '" + splazos_detalleid_ult + "', " +
                                                " `Estatus_plazoid` = '2', " +
                                                " `Fecha_atendio_plazo` = '" + sFechaselloAtyenidioplazo.ToString("yyyy/MM/dd") + "', " +
                                                " `Fecha_atendio_plazo_sistema` = '" + sFechaactual.ToString("yyyy/MM/dd") + "', " +
                        //Fecha_atendio_plazo_sistema
                                                " `Usuarioid_atendio_plazo` = '" + login.sId + "' " +
                                                " WHERE (`Plazos_detalleid` = '" + splazos_detalleid + "');";
                    MySqlDataReader resp_escritos = con1.getdatareader(sQueryallescritos);
                    //while (resp_escritos.Read())
                    //{
                    //    String sIdescritp = validareader("SubTipoDocumentoId", "SubTipoDocumentoId", resp_escritos).Text;
                    //    cbEsritos.Items.Add(validareader_documentos("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos, sIdescritp));//Estatus
                    //}
                    resp_escritos.Close();
                    con1.Cerrarconexion();
                }
                
            }catch(Exception Ex){
                new filelog(" atenderplazoconescrito ", " : "+Ex.Message);

            }
        }

        public void atenderplazoconescrito(String ssplazos_detalleid_ult) {
            try
            {
                if (ssplazos_detalleid_ult!="") {
                    String splazos_detalleid = (cbOficio.SelectedItem as ComboboxItem).Value.ToString();
                    if (splazos_detalleid != "")//splazos_detalleid es el plazo que tenemos que atender
                    {
                        DateTime sFechaselloAtyenidioplazo = DateTime.Parse(tbDocumentofecharecepcion.Text);//Fecha sello impi la fecha en que se atendio el plazo
                        DateTime sFechaactual = DateTime.Today;/**/
                        conect con1 = new conect();
                        String sQueryallescritos = " UPDATE `plazos_detalle` SET " +
                                                    " `Atendio_Plazos_detalleid` = '" + ssplazos_detalleid_ult + "', " +
                                                    " `Estatus_plazoid` = '2', " +
                                                    " `Fecha_atendio_plazo` = '" + sFechaselloAtyenidioplazo.ToString("yyyy/MM/dd") + "', " +
                                                    " `Fecha_atendio_plazo_sistema` = '" + sFechaactual.ToString("yyyy/MM/dd") + "', " +
                                                    //Fecha_atendio_plazo_sistema
                                                    " `Usuarioid_atendio_plazo` = '" + login.sId + "' " +
                                                    " WHERE (`Plazos_detalleid` = '" + splazos_detalleid + "');";
                        MySqlDataReader resp_escritos = con1.getdatareader(sQueryallescritos);
                        //while (resp_escritos.Read())
                        //{
                        //   String sIdescritp = validareader("SubTipoDocumentoId", "SubTipoDocumentoId", resp_escritos).Text;
                        //   cbEsritos.Items.Add(validareader_documentos("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos, sIdescritp));//Estatus
                        //}
                        resp_escritos.Close();
                        con1.Cerrarconexion();
                    }
                }
                
            }
            catch (Exception exs) {
                MessageBox.Show("No se pudo atender el plazo del oficio selccionado: "+exs.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            cbEsritos.Items.Clear();
            conect con1 = new conect();
            String sQueryallescritos="";
            if (checkBox1.Checked == true)
            {
                sQueryallescritos = "SELECT " +
                                            "    * " +
                                            " FROM " +
                                            "     " +
                                            "    subtipodocumento, gruposubtipodocumento " +
                                            " WHERE " +
                                            "    gruposubtipodocumento.GrupoId = " + sGrop + 
                                            "	AND subtipodocumento.SubTipoDocumentoId = gruposubtipodocumento.SubTipoDocumentoId " +
                                            "    AND subtipodocumento.TipoDocumentoId = 2 " +
                                            "    AND subtipodocumento.SubTipoDocumentoIndAct = 1 " +// el valor uno debe ser fijo puesto que validamos que el documento esté activo
                                            "    group by subtipodocumento.SubTipoDocumentoId;";
                MySqlDataReader resp_escritos = con1.getdatareader(sQueryallescritos);
                while (resp_escritos.Read())
                {
                    String sIdescritp = validareader("SubTipoDocumentoId", "SubTipoDocumentoId", resp_escritos).Text;
                    ComboboxItem obj = new ComboboxItem();
                    obj.Value = validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos).Value;
                    obj.Text = validareader("SubTipoDocumentoId", "SubTipoDocumentoId", resp_escritos).Text + "-" + validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos).Text;
                    cbEsritos.Items.Add(obj);//Estatus
                }
                resp_escritos.Close();

            }else {
                cbEsritos.Items.Clear();
                if (sEstatusidint!="")
                {
                    sQueryallescritos = "SELECT " +
                                               "     * " +
                                               " FROM " +
                                               "    estatuscasosubtipodocumento, " +
                                               "    subtipodocumento " +
                                               " WHERE " +
                                               "     estatuscasosubtipodocumento.Estatuscasoid = " + sEstatusidint + "  " +
                                               "         AND estatuscasosubtipodocumento.GrupoId = " + sGrop +//El grupo falta validarlo con una variable dependiendo el tipo del caso 
                                               "         AND subtipodocumento.SubTipoDocumentoId = estatuscasosubtipodocumento.SubTipoDocumentoId " +
                                               "         AND subtipodocumento.TipoDocumentoId = 2 " +//en esta pantalla debe ser fijo el número 2 puesto que estamos en escritoa
                                               "         AND subtipodocumento.SubTipoDocumentoIndAct = 1 " +// el valos uno debe ser fijo puesto que validamos que el documento esté activo
                                               "         group by estatuscasosubtipodocumento.SubTipoDocumentoId order by subtipodocumento.SubTipoDocumentoDescrip;";
                     //sQueryallescritos =  "select * "+
                     //                    "from "+
                     //                    "estatuscasosubtipodocumentores, "+
                     //                    "subtipodocumento "+
                     //                    "where "+
                     //                    "estatuscasosubtipodocumentores.EstatusCasoId = " + sEstatusidint +
                     //                    " and estatuscasosubtipodocumentores.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId"+
                     //                    " and estatuscasosubtipodocumentores.GrupoId =1 "+
                     //                    "and subtipodocumento.TipoDocumentoId = 2 "+
                     //                    "group by subtipodocumento.SubTipoDocumentoId;";
                                           
                    MySqlDataReader resp_escritos = con1.getdatareader(sQueryallescritos);
                    while (resp_escritos.Read())
                    {

                        String sIdescritp = validareader("SubTipoDocumentoId", "SubTipoDocumentoId", resp_escritos).Text;
                        ComboboxItem obj = new ComboboxItem();
                        obj.Value = validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos).Value;
                        obj.Text = validareader("SubTipoDocumentoId", "SubTipoDocumentoId", resp_escritos).Text + "-" + validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_escritos).Text;
                        cbEsritos.Items.Add(obj);//Estatus
                    }
                    resp_escritos.Close();
                }else{
                    MessageBox.Show("Debe seleccionar un caso antes");
                }
            }
            con1.Cerrarconexion();
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
            if (cbEsritos.Text == "") {
                MessageBox.Show("Debe selecciona un tipo de escrito antes de agregar un archivo.");
                if (cbEsritos.CanFocus)
                {
                    cbEsritos.Focus();
                }
                return;
            }
            try { 
                var fileContent = string.Empty;
                var filePath = string.Empty;
                String sNamefile = "";
                String[] aName;
                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    
                    openFileDialog.InitialDirectory = mdoc;
                    //openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filePath = openFileDialog.FileName;
                        aName = filePath.Split('\\');
                        sNamefile = aName[aName.Length-1];
                        //Read the contents of the file into a stream
                        /*var fileStream = openFileDialog.OpenFile();

                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            fileContent = reader.ReadToEnd();
                        }*/
                    }
                }
                //\\192.168.1.95\documentosserver\Patentes
                configuracionfiles obj = new configuracionfiles();
                obj.configuracionfilesinicio();
                //File.Copy(filePath, "\\\\" + obj.sServer + "\\documentosserver\\Patentes\\queonda.jpg");
                //string ruta = obj.sFileupload + "documentosserver\\" + sCarpetadocumentos;
                string ruta = obj.sFileupload + "\\" + sCarpetadocumentos + "\\" + sCarpetacaso;
                
                //if (!Directory.Exists(ruta))//si no existe la carpeta la creamos
                //{
                //    Directory.CreateDirectory(ruta);
                //}
                if (!Directory.Exists(ruta))//si no existe la carpeta la creamos
                {
                    Directory.CreateDirectory(ruta);
                }
                //crear carpeta de cada caso 
                //sCarpetadocumentos
                string textoNormalizado = cbEsritos.Text.Normalize(NormalizationForm.FormD);
                //coincide todo lo que no sean letras y números ascii o espacio
                //y lo reemplazamos por una cadena vacía.Regex reg = new Regex("[^a-zA-Z0-9 ]");
                Regex reg = new Regex("[^a-zA-Z0-9 ]");
                string textoSinAcentos = reg.Replace(textoNormalizado, "");

                String sFechanow = DateTime.Now.ToString("yyyyMMddHHmmsss");

                sfilePath_2 = @ruta + "\\" + tbCasonum.Text + " _" + sFechanow +"_ "+ tbExpediente.Text.Replace("/", "") + "_" + textoSinAcentos + " " + sNamefile;
                tbFilename.Text = @"\\" + tbCasonum.Text + "_" + tbExpediente.Text.Replace("/", "") + "_" + textoSinAcentos + " " + sNamefile;
                /*cambio a oficios*/
                sfilePath = filePath;
                //sfilePath_2 = @ruta + "\\" + tbCasonum.Text + " " + tbExpediente.Text.Replace("/", "") + " " + cbEsritos.Text + " " + sNamefile;
                ////File.Copy(filePath, ruta + "\\" + tbCasonum.Text + "_" + tbExpediente.Text.Replace("/", "") + "_" + cbEsritos.Text + "_" + sNamefile);
                //tbFilename.Text = @"\\" + tbCasonum.Text + " " + tbExpediente.Text.Replace("/", "") + " " + cbEsritos.Text + " " + sNamefile;
                /*fin cambios a oficio*/
                //MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);
            }catch(Exception E){
                new filelog(login.sId, E.ToString());
            }
        }

        private void button4_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int i;
            for (i = 0; i < s.Length; i++)
                MessageBox.Show(s[i]);
        }

        private void button4_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void tbCodigo_TextChanged(object sender, EventArgs e)
        {
            tbFolio.Text = tbCodigo.Text;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            //generaanualialidades(20, "01-05-2014", "", "1");
        }

        private void tbDocumentofecharecepcion_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbDocumentofecharecepcion);
        }

        private void tbFechacalce_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechacalce);
        }

        //private void textBox1_DragDrop(object sender, DragEventArgs e)
        //{
        //    string[] files2 = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        //    //foreach (string file2 in files2)
        //        //MessageBox.Show(file2);
        //        //textBox1.Text = file2;
        //}

        //private void textBox1_DragEnter(object sender, DragEventArgs e)
        //{
        //    //Drag and drop effect in windows
        //    if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
        //    {
        //        e.Effect = DragDropEffects.All;
        //    }
        //}
    }
}
