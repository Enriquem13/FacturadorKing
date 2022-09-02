using MySql.Data.MySqlClient;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Facturador
{
    public partial class plazos_consulta : Form
    {
        funcionesdicss funcionesgenerales = new funcionesdicss();
        funcionesdicss objfuncionesdicss = new funcionesdicss();
        public String[,] sArray;// = new String[1, 2];//utilizaremos este array para consultar los plazos
        public Form1 login;
        public captura capturaobj;

        public plazos_consulta(Form1 loguin, captura  cap)
        {
            login = loguin;
            capturaobj = cap;
            InitializeComponent();
            iniciacomponentes();
        }

        public void iniciacomponentes() {
            //seleccionamos las opciones de grupos
            conect con_grupo = new conect();
            String squery_grupo = "select * from grupo;";
            MySqlDataReader resp_grupo = con_grupo.getdatareader(squery_grupo);
            while (resp_grupo.Read())
            {
                cbGrupo.Items.Add(funcionesgenerales.validareader("GrupoDescripcion", "GrupoId", resp_grupo));
            }
            resp_grupo.Close();
            con_grupo.Cerrarconexion();
            //FIN seleccionamos las opciones de grupos
            /*Consultamos si es que tiene referencia con el en la tabla de usuarios*/
            try {
                conect con_grupo_usr = new conect();
                String squery_grupo_usr = "SELECT * FROM usuario, grupo where usuario.grupoprefetentePlazos = grupo.GrupoId and UsuarioId = " + login.sId +";";
                MySqlDataReader resp_grupo_usr = con_grupo.getdatareader(squery_grupo_usr);
                while (resp_grupo_usr.Read())
                {
                    cbGrupo.Text = funcionesgenerales.validareader("GrupoDescripcion", "UsuarioId", resp_grupo_usr).Text;
                }
                resp_grupo_usr.Close();
                con_grupo_usr.Cerrarconexion();
            }catch(Exception exs){
                new filelog("", ""+exs.Message);
            }
            

            //seleccionamos las opciones de grupos
            //conect con_grupo_plazo = new conect();
            //String squery_grupo_plazo = "SELECT * FROM grupoplazo;";
            //MySqlDataReader resp_grupo_plazo = con_grupo_plazo.getdatareader(squery_grupo_plazo);
            //while (resp_grupo_plazo.Read())
            //{
            //    cbgrupo_plazo.Items.Add(funcionesgenerales.validareader("GrupoPlazoDescripcion", "GrupoPlazoId", resp_grupo_plazo));
            //}
            //resp_grupo_plazo.Close();
            //con_grupo_plazo.Cerrarconexion();
            //FIN seleccionamos las opciones de grupos
            //
            //Seleccionamos las opciones para los estados posibles para plazos
            conect con_estatus_plazo = new conect();
            String squery_estatus_plazo = "select * from estatusplazo;";
            MySqlDataReader resp_estatus_plazo = con_estatus_plazo.getdatareader(squery_estatus_plazo);
            while (resp_estatus_plazo.Read())
            {
                cbEstadosplazos.Items.Add(funcionesgenerales.validareader("EstatusPlazoDescrip", "EstatusPlazoId", resp_estatus_plazo));
            }
            resp_estatus_plazo.Close();
            con_estatus_plazo.Cerrarconexion();
            //select * from estatusplazo;
            //
            //ocultamos la columna donde ira el id 
            //dataGridView1.Columns["plazoid"].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public DateTime dConviertefecha(String sFechaconvert) {
            DateTime dFechaconvert = DateTime.MinValue;
            try {
                //if (if (validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && 
                //validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "")) {
                if (sFechaconvert!= "00-00-0000" && sFechaconvert !="") {//si es diferente de fechas vacias no lo intetes convertir para que no caiga en la excepcion y no tarde tiempo
                    dFechaconvert = Convert.ToDateTime(sFechaconvert);
                }
                    
            } catch (Exception exs) {
                dFechaconvert = DateTime.MinValue;
            }
            return dFechaconvert;
        }

        public void consultarplazos_view(String squery) {
            try
            {
                String sNumplazos = "", sWhere = "";

                ComboboxItem cGrupo_Seleccionado = (cbGrupo.SelectedItem as ComboboxItem);
                ComboboxItem cEstadosplazos = (cbEstadosplazos.SelectedItem as ComboboxItem);
                ComboboxItem cgrupo_plazo = (cbgrupo_plazo.SelectedItem as ComboboxItem);
                ComboboxItem cTipo_plazo = (cbTipo_plazo.SelectedItem as ComboboxItem);
                String sDesde = tFechadesde.Text;
                String sHasta = tFechahasta.Text;

                if (cGrupo_Seleccionado is null || cEstadosplazos is null)
                {
                    MessageBox.Show("Debe seleccionar un grupo y estatus");
                    return;
                }

                if (!(cTipo_plazo is null))
                { //si no es null quiere decir que se selecciono un tipoplazo
                    sWhere += " and Tipo_plazoid=  " + cTipo_plazo.Value;
                }


                if (cbiniciorenov.Text != "")
                { //si no es null quiere decir que se selecciono un tipoplazo
                    sWhere += " and year(inicio_renovacion) =  " + cbiniciorenov.Text;
                }

                if (cbfinrenov.Text != "")
                { //si no es null quiere decir que se selecciono un tipoplazo
                    sWhere += " and year(fin_renovacion) =  " + cbfinrenov.Text;
                }
                //String swherefechas = " and Fecha_notificacion between '"+ dtdesde .Text+ "' and '"+ dthasta + "'";

                String sTablaplazos = "";
                if (cGrupo_Seleccionado != null)
                {

                    switch (cGrupo_Seleccionado.Value)
                    {
                        case "0": { } break;
                        case "1": { sTablaplazos = "plazos_patentes_view"; } break;
                        case "2": { sTablaplazos = "plazos_marcas_view"; } break;
                        case "3": { sTablaplazos = ""; } break;
                        case "4": { sTablaplazos = ""; } break;
                        case "5": { sTablaplazos = ""; } break;
                    }

                }
                //validamos si hay un criterio de busqueda sobre los filtros ya aplicados preguntando si hay un valor en txBusqueda
                String sBusquedalike = ";";
                if (tbBusqueda.Text != "") { 
                    sBusquedalike = " AND (TipoDocumentoDescrip like '%" + tbBusqueda.Text + "%' OR " +
                                    " SubTipoDocumentoDescrip like '%" + tbBusqueda.Text + "%' OR " +
                                    " TipoPlazoDescrip like '%" + tbBusqueda.Text + "%' OR " +
                                    " EstatusPlazoDescrip like '%" + tbBusqueda.Text + "%' OR " +
                                    " usuariocreo like '%" + tbBusqueda.Text + "%' OR " +
                                    " usuarioatendio like '%" + tbBusqueda.Text + "%' OR " +
                                    " CasoNumero like '%" + tbBusqueda.Text + "%' OR " +
                                    " CasoId like '%" + tbBusqueda.Text + "%' OR " +
                                    " TipoSolicitudId like '%" + tbBusqueda.Text + "%' OR " +
                                    " CasoTituloespanol like '%" + tbBusqueda.Text + "%' OR " +
                                    " CasoTituloingles like '%" + tbBusqueda.Text + "%' OR " +
                                    " Plazos_detalleid like '%" + tbBusqueda.Text + "%' OR " +
                                    " Plazosid like '%" + tbBusqueda.Text + "%' OR " +
                                    " documentoid like '%" + tbBusqueda.Text + "%' OR " +
                                    " usuario_creo_plazodetalle like '%" + tbBusqueda.Text + "%' OR " +
                                    " Tipo_plazoid like '%" + tbBusqueda.Text + "%' OR " +
                                    " Estatus_plazoid like '%" + tbBusqueda.Text + "%' OR " +
                                    " Fecha_notificacion like '%" + tbBusqueda.Text + "%' OR " +
                                    " Mesdiferencia like '%" + tbBusqueda.Text + "%' OR " +
                                    " Name_exp_9 like '%" + tbBusqueda.Text + "%' OR " +
                                    " Fecha_vencimiento_3m like '%" + tbBusqueda.Text + "%' OR " +
                                    " Fecha_vencimiento_4m like '%" + tbBusqueda.Text + "%' OR " +
                                    " Fecha_atendio_plazo like '%" + tbBusqueda.Text + "%' OR " +
                                    " Usuarioid_atendio_plazo like '%" + tbBusqueda.Text + "%' OR " +
                                    " Motivo_cancelacion_plazo like '%" + tbBusqueda.Text + "%' OR " +
                                    " Fecha_cancelacion_plazo like '%" + tbBusqueda.Text + "%' OR " +
                                    " usuario_cancelo like '%" + tbBusqueda.Text + "%' OR " +
                                    " Atendio_Plazos_detalleid like '%" + tbBusqueda.Text + "%' OR " +
                                    " Fecha_atendio_plazo_sistema like '%" + tbBusqueda.Text + "%' OR " +
                                    " AnualidadId like '%" + tbBusqueda.Text + "%' OR " +
                                    " TipoSolicitudDescripcion like '%" + tbBusqueda.Text + "%' OR " +
                                    " ReferenciaNombre like '%" + tbBusqueda.Text + "%' OR " +
                                    " ClienteNombre like '%" + tbBusqueda.Text + "%' OR " +
                                    " InteresadoNombre like '%" + tbBusqueda.Text + "%' OR " +
                                    " EstatusCasoDescrip like '%" + tbBusqueda.Text + "%' OR " +
                                    " PaisNombre like '%" + tbBusqueda.Text + "%' OR " +
                                    " inicio_renovacion like '%" + tbBusqueda.Text + "%' OR " +
                                    " fin_renovacion like '%" + tbBusqueda.Text + "%' OR " +
                                    " declaracion_de_uso like '%" + tbBusqueda.Text + "%' OR " +
                                    " rango_anualidades like '%" + tbBusqueda.Text + "%' OR " +
                                    " fecha_pago_anualidades like '%" + tbBusqueda.Text + "%' OR " +
                                    " CasoProductosClase like '%" + tbBusqueda.Text + "%' OR " +
                                    " seguimientocliente like '%" + tbBusqueda.Text + "%' OR " +
                                    " seguimientointeresado like '%" + tbBusqueda.Text + "%') ;";
                }

                conect con_count_plazo = new conect();
                String squery_count_plazo = " SET lc_time_names = 'es_MX';SELECT * FROM " + sTablaplazos +
                                            //"     " + sTablaplazos + ".*, " +
                                            //"     TipoDocumentoDescrip, " +
                                            //"     subtipodocumento.SubTipoDocumentoDescrip " +
                                            //" FROM " +
                                            //"     "+ sTablaplazos + " " +
                                            //"         LEFT JOIN " +
                                            //"     documento ON "+ sTablaplazos + ".documentoid = documento.documentoid " +
                                            //"         LEFT JOIN " +
                                            //"     subtipodocumento ON subtipodocumento.SubTipoDocumentoId = documento.SubTipoDocumentoId " +
                                            //"         LEFT JOIN " +
                                            //"     tipodocumento ON subtipodocumento.TipoDocumentoId = tipodocumento.TipoDocumentoId" +
                                            " WHERE  seguimientocliente is null and seguimientointeresado is null and " + sTablaplazos + ".Estatus_plazoid = " + cEstadosplazos.Value + " " + sWhere + " "+ sBusquedalike;

                MySqlDataReader resp_count_plazo = con_count_plazo.getdatareader(squery_count_plazo);
                int iCountrows = 0;
                dataGridView1.Rows.Clear();
                while (resp_count_plazo.Read())
                {
                    //sNumplazos = funcionesgenerales.validareader("numplazos", "numplazos", resp_count_plazo).Text;
                    //String sCasoNumero = funcionesgenerales.validareader("CasoNumero", "CasoId", resp_count_plazo).Text;
                    String sFecha_Vencimiento_regular_impi = funcionesgenerales.validareader("Name_exp_9", "Plazos_detalleid", resp_count_plazo).Text;
                    String sFecha_Vencimiento_4meses = funcionesgenerales.validareader("Fecha_vencimiento_4m", "Plazos_detalleid", resp_count_plazo).Text;

                    String sPlazos_detalleid = funcionesgenerales.validareader("Plazos_detalleid", "Plazos_detalleid", resp_count_plazo).Text;
                    String sPlazosid = funcionesgenerales.validareader("Plazosid", "Plazosid", resp_count_plazo).Text;
                    //TipoPlazoDescrip

                    //campos agregados 
                    String sCasoTituloingles = funcionesgenerales.validareader("CasoTituloingles", "CasoTituloingles", resp_count_plazo).Text;//CasoTituloingles este es la marca
                    String sCasoTituloespanol = funcionesgenerales.validareader("CasoTituloespanol", "CasoTituloespanol", resp_count_plazo).Text;

                    String sEstatusCasoDescrip = funcionesgenerales.validareader("EstatusCasoDescrip", "EstatusCasoDescrip", resp_count_plazo).Text;
                    String sTipoSolicitudDescripcion = funcionesgenerales.validareader("TipoSolicitudDescripcion", "TipoSolicitudDescripcion", resp_count_plazo).Text;
                    String sClienteNombre = funcionesgenerales.validareader("ClienteNombre", "ClienteNombre", resp_count_plazo).Text;
                    String sInteresadoNombre = funcionesgenerales.validareader("InteresadoNombre", "InteresadoNombre", resp_count_plazo).Text;
                    String sPaisNombre = funcionesgenerales.validareader("PaisNombre", "PaisNombre", resp_count_plazo).Text;
                    String sCasoProductosClase = funcionesgenerales.validareader("CasoProductosClase", "CasoProductosClase", resp_count_plazo).Text;
                    //String sCasoProductosDescripcion = funcionesgenerales.validareader("CasoProductosDescripcion", "CasoProductosDescripcion", resp_count_plazo).Text;

                    //CasoProductosDescripcion



                    String sTipoPlazoDescrip = funcionesgenerales.validareader("TipoPlazoDescrip", "TipoPlazoDescrip", resp_count_plazo).Text;
                    String sFecha_notificacion = funcionesgenerales.validareader("Fecha_notificacion", "Fecha_notificacion", resp_count_plazo).Text;
                    String sEstatusPlazoDescrip = funcionesgenerales.validareader("EstatusPlazoDescrip", "EstatusPlazoDescrip", resp_count_plazo).Text;
                    String sTipoDocumentoDescrip = funcionesgenerales.validareader("TipoDocumentoDescrip", "TipoDocumentoDescrip", resp_count_plazo).Text;
                    String susuariocreo = funcionesgenerales.validareader("usuariocreo", "usuariocreo", resp_count_plazo).Text;
                    String sCasoNumero = funcionesgenerales.validareader("CasoNumero", "CasoNumero", resp_count_plazo).Text;


                    //camspo con filtros 

                    String sinicio_renovacion = funcionesgenerales.validareader("inicio_renovacion", "inicio_renovacion", resp_count_plazo).Text;
                    String sfin_renovacion = funcionesgenerales.validareader("fin_renovacion", "fin_renovacion", resp_count_plazo).Text;
                    String sdeclaracion_de_uso = funcionesgenerales.validareader("declaracion_de_uso", "declaracion_de_uso", resp_count_plazo).Text;
                    String srango_anualidades = funcionesgenerales.validareader("rango_anualidades", "rango_anualidades", resp_count_plazo).Text;
                    String sfecha_pago_anualidades = funcionesgenerales.validareader("fecha_pago_anualidades", "fecha_pago_anualidades", resp_count_plazo).Text;



                    String sCasoId = funcionesgenerales.validareader("CasoId", "CasoId", resp_count_plazo).Text;
                    String sTipoSolicitudId = funcionesgenerales.validareader("TipoSolicitudId", "TipoSolicitudId", resp_count_plazo).Text;

                    String sMesdiferencia = funcionesgenerales.validareader("Mesdiferencia", "Mesdiferencia", resp_count_plazo).Text;
                    String sFecha_atendio_plazo = funcionesgenerales.validareader("Fecha_atendio_plazo", "Fecha_atendio_plazo", resp_count_plazo).Text;
                    String sSubTipoDocumentoDescrip = funcionesgenerales.validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoDescrip", resp_count_plazo).Text;




                    //String sClienteNombre = funcionesgenerales.validareader("ClienteNombre", "CasoId", resp_count_plazo).Text;
                    //String sCasoTitular = funcionesgenerales.validareader("CasoTitular", "CasoId", resp_count_plazo).Text;
                    //String sDocumentoFechaRecepcion = funcionesgenerales.validareader("DocumentoFechaRecepcion", "CasoId", resp_count_plazo).Text;
                    //String sEstatusCasoDescrip = funcionesgenerales.validareader("EstatusCasoDescrip", "CasoId", resp_count_plazo).Text;
                    //String sDocumentoId = funcionesgenerales.validareader("DocumentoId", "CasoId", resp_count_plazo).Text;
                    //String sDocumentoCodigoBarras = funcionesgenerales.validareader("DocumentoCodigoBarras", "CasoId", resp_count_plazo).Text;
                    //String sSubTipoDocumentoDescrip = funcionesgenerales.validareader("SubTipoDocumentoDescrip", "CasoId", resp_count_plazo).Text;
                    //String sEstatusPlazoDescrip = funcionesgenerales.validareader("EstatusPlazoDescrip", "CasoId", resp_count_plazo).Text;
                    //String sSubTipoDocumentoId = funcionesgenerales.validareader("SubTipoDocumentoId", "CasoId", resp_count_plazo).Text;
                    //String sTipoPlazoDescrip = funcionesgenerales.validareader("TipoPlazoDescrip", "CasoId", resp_count_plazo).Text;
                    //String sTipoPlazoId = funcionesgenerales.validareader("TipoPlazoId", "CasoId", resp_count_plazo).Text;
                    //String sPlazoFecha = funcionesgenerales.validareader("PlazoFecha", "CasoId", resp_count_plazo).Text;
                    //String sfigura = funcionesgenerales.validareader("figura", "CasoId", resp_count_plazo).Text;
                    //String susuario_capturo = funcionesgenerales.validareader("usuario_capturo", "CasoId", resp_count_plazo).Text;
                    //String susuario_responsable = funcionesgenerales.validareader("usuario_responsable", "CasoId", resp_count_plazo).Text;
                    //String scontactosdelcliente = funcionesgenerales.validareader("contactosdelcliente", "CasoId", resp_count_plazo).Text;
                    //String sreferenciascliente = funcionesgenerales.validareader("referenciascliente", "CasoId", resp_count_plazo).Text;
                    //String sCasoDenominacion = funcionesgenerales.validareader("CasoDenominacion", "CasoId", resp_count_plazo).Text;
                    //String sCasoNumeroExpedienteLargo = funcionesgenerales.validareader("CasoNumeroExpedienteLargo", "CasoId", resp_count_plazo).Text;
                    //String sinteresados = funcionesgenerales.validareader("interesados", "CasoId", resp_count_plazo).Text;
                    //String sestatuscasoid = funcionesgenerales.validareader("estatuscasoid", "CasoId", resp_count_plazo).Text;
                    //String sTipoTareaId = funcionesgenerales.validareader("TipoTareaId", "CasoId", resp_count_plazo).Text;
                    //String sGrupoid = funcionesgenerales.validareader("Grupoid", "CasoId", resp_count_plazo).Text;
                    //String sdiferenciafecha = funcionesgenerales.validareader("diferenciafecha", "CasoId", resp_count_plazo).Text;
                    //String sPlazoMotivoCancelacion = funcionesgenerales.validareader("PlazoMotivoCancelacion", "CasoId", resp_count_plazo).Text;
                    //String sPlazoFechaAtencion = funcionesgenerales.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", resp_count_plazo).Text;
                    //String splazoid = funcionesgenerales.validareader("plazoid", "plazoid", resp_count_plazo).Text;
                    //String sTipoSolicitudId = funcionesgenerales.validareader("TipoSolicitudId", "plazoid", resp_count_plazo).Text;
                    //String sTipoTareaDescrip = funcionesgenerales.validareader("TipoTareaDescrip", "plazoid", resp_count_plazo).Text;
                    iCountrows++;
                    DateTime dFecha_Vencimiento_regular_impi = DateTime.MinValue;//Convert.ToDateTime(sFecha_Vencimiento_regular_impi);


                    DataGridViewRow dRows = (DataGridViewRow)dataGridView1.Rows[0].Clone();

                    dRows.Cells[0].Value = sPlazos_detalleid;
                    dRows.Cells[1].Value = sPlazosid;
                    dRows.Cells[2].Value = sCasoId;
                    dRows.Cells[3].Value = sTipoSolicitudId;
                    dRows.Cells[4].Value = sTipoPlazoDescrip;
                    dRows.Cells[5].Value = sEstatusPlazoDescrip;
                    dRows.Cells[6].Value = dConviertefecha(sFecha_Vencimiento_4meses);
                    dRows.Cells[7].Value = sMesdiferencia;
                    dRows.Cells[8].Value = dConviertefecha(sFecha_notificacion);
                    dRows.Cells[9].Value = dConviertefecha(sFecha_Vencimiento_regular_impi);
                    dRows.Cells[10].Value = sTipoDocumentoDescrip;
                    dRows.Cells[11].Value = sSubTipoDocumentoDescrip;
                    dRows.Cells[12].Value = susuariocreo;
                    dRows.Cells[13].Value = sCasoNumero;
                    dRows.Cells[14].Value = susuariocreo;
                    dRows.Cells[15].Value = sEstatusCasoDescrip;
                    dRows.Cells[16].Value = sTipoSolicitudDescripcion;
                    dRows.Cells[17].Value = sClienteNombre;
                    dRows.Cells[18].Value = sInteresadoNombre;
                    dRows.Cells[19].Value = sCasoTituloingles;
                    dRows.Cells[20].Value = sCasoProductosClase;
                    dRows.Cells[21].Value = sPaisNombre;

                    dRows.Cells[22].Value = sinicio_renovacion;
                    dRows.Cells[23].Value = sfin_renovacion;
                    dRows.Cells[24].Value = sdeclaracion_de_uso;
                    dRows.Cells[25].Value = srango_anualidades;
                    dRows.Cells[26].Value = sfecha_pago_anualidades;

                    if (dConviertefecha(sFecha_notificacion) == DateTime.MinValue)
                    {
                        dRows.Cells[8].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        dRows.Cells[8].Style.ForeColor = Color.Black;
                    }
                    dataGridView1.Rows.Add(dRows);

                }

                iRowscount.Text = "" + iCountrows;
                resp_count_plazo.Close();
                con_count_plazo.Cerrarconexion();
            }
            catch (Exception exs)
            {
                new filelog("cargar plazos", "" + exs.Message);
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            consultarplazos_view("");
            //String sNumplazos="";
            //ComboboxItem cGrupo_Seleccionado = (cbGrupo.SelectedItem as ComboboxItem);
            //ComboboxItem cEstadosplazos = (cbEstadosplazos.SelectedItem as ComboboxItem);
            //ComboboxItem cgrupo_plazo = (cbgrupo_plazo.SelectedItem as ComboboxItem);
            //ComboboxItem cTipo_plazo = (cbTipo_plazo.SelectedItem as ComboboxItem);
            //String sDesde = dtdesde.Text;
            //String sHasta = dthasta.Text;


            //if (cGrupo_Seleccionado != null && cEstadosplazos != null)
            //{
            //    int iCountgeneral = 0;
            //    Action act = () =>
            //    {
            //        String asWhere = "";
            //        //"     Dameelusuario(p.UsuarioIdCancelo) as usuario_cancelo, " +
            //        //Validamos si está seleccionado el combobox para crear el where correspondiente y filtrar si está seleccionado.
            //        if (cgrupo_plazo != null) { asWhere += " AND tp.GrupoPlazoId = " + cgrupo_plazo.Value; }
            //        if (cTipo_plazo != null) { asWhere += " AND tp.TipoPlazoId = " + cTipo_plazo.Value; }
            //        if (sDesde != "" && sHasta != "") { asWhere += "  AND (p.PlazoFecha between CAST('" + sDesde + "' AS DATE) and CAST('" + sHasta + "' AS DATE))"; }
            //        String sConsultaplazos = " SELECT  " +
            //                                    "     c.CasoNumero, " +
            //                                    "     c.casoId, " +
            //                                    "     cl.ClienteNombre, " +
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
            //                                    "     DAMEELUSUARIO(d.UsuarioId) AS usuario_capturo, "+
            //                                    "     Dameelusuario(p.UsuarioId) as usuario_responsable, "+
            //                                    "     GetNombrecontactobyclienteid(cc.ClienteId) as contactosdelcliente,"+
            //                                    "     Get_referenciasclientefromcasoidtiposolid(c.casoId, c.TipoSolicitudId) as referenciascliente,"+
            //                                    "     c.CasoDenominacion,"+
            //                                    "     c.CasoNumeroExpedienteLargo,"+
            //                                    "     Get_Interesados(c.casoId) as interesados," +
            //                                    "     e.estatuscasoid," +
            //                                    "     tp.TipoTareaId," +
            //                                    "     tptar.TipoTareaDescrip," +
            //                                    "     tp.Grupoid, " +
            //                                    "     TIMESTAMPDIFF(MONTH, d.DocumentoFechaRecepcion, now()) as diferenciafecha, " +
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
            //                                    "     tipoplazo tp ON tp.TipoPlazoId = p.TipoPlazoId "+
            //                                    "         JOIN " +
            //                                    "     tipotarea tptar ON tptar.TipoTareaId = tp.TipoTareaId " +
            //                                    " WHERE ep.EstatusPlazoId = " +cEstadosplazos.Value +" "+
            //                                    " AND tp.Grupoid = " + cGrupo_Seleccionado.Value +
            //                                    " " + asWhere + " ;";
            //                                    //"limit 500";
            //        //contamos los plazos
            //        conect con_count_plazo = new conect();
            //        String squery_count_plazo = " SELECT  count(*) as numplazos" +
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
            //                                    " WHERE ep.EstatusPlazoId = " + cEstadosplazos.Value + " " +
            //                                    " AND tp.Grupoid = " + cGrupo_Seleccionado.Value +
            //                                    " " + asWhere + " ;";

            //        MySqlDataReader resp_count_plazo = con_count_plazo.getdatareader(squery_count_plazo);
            //        while (resp_count_plazo.Read())
            //        {
            //             sNumplazos = funcionesgenerales.validareader("numplazos", "numplazos", resp_count_plazo).Text;
            //        }
            //        resp_count_plazo.Close();
            //        con_count_plazo.Cerrarconexion();

            //        int iNum = Int32.Parse(sNumplazos);
            //        sArray = new String[iNum, 29];
            //        //Fin contamos los plazos
            //        conect conin_plazos = new conect();
            //        MySqlDataReader respuestastrig_plazos = conin_plazos.getdatareader(sConsultaplazos);
            //        int iRows = 0;
            //        /*using (loadinprocess form = new loadinprocess(consultamoslosplazos))
            //        {
            //            form.ShowDialog();
            //        }*/
            //        while (respuestastrig_plazos.Read())
            //        {
            //            String []saRow = new String[28];
            //            String sCasoNumero = funcionesgenerales.validareader("CasoNumero", "CasoId", respuestastrig_plazos).Text;
            //            String scasoId = funcionesgenerales.validareader("casoId", "CasoId", respuestastrig_plazos).Text;
            //            String sClienteNombre = funcionesgenerales.validareader("ClienteNombre", "CasoId", respuestastrig_plazos).Text;
            //            String sCasoTitular = funcionesgenerales.validareader("CasoTitular", "CasoId", respuestastrig_plazos).Text;
            //            String sDocumentoFechaRecepcion = funcionesgenerales.validareader("DocumentoFechaRecepcion", "CasoId", respuestastrig_plazos).Text;
            //            String sEstatusCasoDescrip = funcionesgenerales.validareader("EstatusCasoDescrip", "CasoId", respuestastrig_plazos).Text;
            //            String sDocumentoId = funcionesgenerales.validareader("DocumentoId", "CasoId", respuestastrig_plazos).Text;
            //            String sDocumentoCodigoBarras = funcionesgenerales.validareader("DocumentoCodigoBarras", "CasoId", respuestastrig_plazos).Text;
            //            String sSubTipoDocumentoDescrip = funcionesgenerales.validareader("SubTipoDocumentoDescrip", "CasoId", respuestastrig_plazos).Text;
            //            String sEstatusPlazoDescrip = funcionesgenerales.validareader("EstatusPlazoDescrip", "CasoId", respuestastrig_plazos).Text;
            //            String sSubTipoDocumentoId = funcionesgenerales.validareader("SubTipoDocumentoId", "CasoId", respuestastrig_plazos).Text;
            //            String sTipoPlazoDescrip = funcionesgenerales.validareader("TipoPlazoDescrip", "CasoId", respuestastrig_plazos).Text;
            //            String sTipoPlazoId = funcionesgenerales.validareader("TipoPlazoId", "CasoId", respuestastrig_plazos).Text;
            //            String sPlazoFecha = funcionesgenerales.validareader("PlazoFecha", "CasoId", respuestastrig_plazos).Text;
            //            String sfigura = funcionesgenerales.validareader("figura", "CasoId", respuestastrig_plazos).Text;
            //            String susuario_capturo = funcionesgenerales.validareader("usuario_capturo", "CasoId", respuestastrig_plazos).Text;
            //            String susuario_responsable = funcionesgenerales.validareader("usuario_responsable", "CasoId", respuestastrig_plazos).Text;
            //            String scontactosdelcliente = funcionesgenerales.validareader("contactosdelcliente", "CasoId", respuestastrig_plazos).Text;
            //            String sreferenciascliente = funcionesgenerales.validareader("referenciascliente", "CasoId", respuestastrig_plazos).Text;
            //            String sCasoDenominacion = funcionesgenerales.validareader("CasoDenominacion", "CasoId", respuestastrig_plazos).Text;
            //            String sCasoNumeroExpedienteLargo = funcionesgenerales.validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastrig_plazos).Text;
            //            String sinteresados = funcionesgenerales.validareader("interesados", "CasoId", respuestastrig_plazos).Text;
            //            String sestatuscasoid = funcionesgenerales.validareader("estatuscasoid", "CasoId", respuestastrig_plazos).Text;
            //            String sTipoTareaId = funcionesgenerales.validareader("TipoTareaId", "CasoId", respuestastrig_plazos).Text;
            //            String sGrupoid = funcionesgenerales.validareader("Grupoid", "CasoId", respuestastrig_plazos).Text;
            //            String sdiferenciafecha = funcionesgenerales.validareader("diferenciafecha", "CasoId", respuestastrig_plazos).Text;
            //            String sPlazoMotivoCancelacion = funcionesgenerales.validareader("PlazoMotivoCancelacion", "CasoId", respuestastrig_plazos).Text;
            //            //String susuario_cancelo = funcionesgenerales.validareader("usuario_cancelo", "CasoId", respuestastrig_plazos).Text;
            //            String sPlazoFechaAtencion = funcionesgenerales.validareader("PlazoFechaAtencion", "PlazoFechaAtencion", respuestastrig_plazos).Text;
            //            String splazoid = funcionesgenerales.validareader("plazoid", "plazoid", respuestastrig_plazos).Text;
            //            String sTipoSolicitudId = funcionesgenerales.validareader("TipoSolicitudId", "plazoid", respuestastrig_plazos).Text;
            //            String sTipoTareaDescrip = funcionesgenerales.validareader("TipoTareaDescrip", "plazoid", respuestastrig_plazos).Text;
            //            //TipoTareaDescrip
            //            if (sPlazoFecha!="")
            //            {
            //                sPlazoFecha = sPlazoFecha.Substring(0, 10);
            //            }
            //            //String sPlazoFechaProrroga = funcionesgenerales.validareader("PlazoFechaProrroga", "CasoId", respuestastrig_plazos).Text;
            //            //TipoSolicitudId
            //            /*dataGridView1.Rows.Add(sTipoPlazoDescrip, 
            //                sEstatusPlazoDescrip, 
            //                "plazo final", 
            //                "mes", 
            //                sDocumentoFechaRecepcion, 
            //                "Vencimiento Original", 
            //                "Figura", 
            //                sSubTipoDocumentoDescrip, 
            //                "Usuario oficio captura", 
            //                "referencia");*/
            //            /*saRow[0] = sTipoPlazoDescrip;
            //            saRow[1] = sEstatusPlazoDescrip;
            //            saRow[2] = "Plazo Final";
            //            saRow[3] = "mes";
            //            saRow[4] = sDocumentoFechaRecepcion;
            //            saRow[5] = "Vencimiento Original";
            //            saRow[6] = "Figura";
            //            saRow[7] = sSubTipoDocumentoDescrip;
            //            saRow[8] = "Usuario oficio captura";
            //            saRow[9] = "referencia";*/
            //            sArray[iRows, 0] = sTipoPlazoDescrip;//tipo de plazo//Tipo de Plazo 
            //            sArray[iRows, 1] = sEstatusPlazoDescrip;//estatus//Estatus
            //            sArray[iRows, 2] = sPlazoFecha;//plazo fecha//Plazo Final
            //            sArray[iRows, 3] = sdiferenciafecha;//MEs//Mes //se calcula sobre la fecha atendio  y el estatus considerar
            //            sArray[iRows, 4] = sDocumentoFechaRecepcion;//Norificacióm es //Notificado en
            //            sArray[iRows, 5] = sPlazoFecha;//Vencimiento original//Vencimiento Original
            //            sArray[iRows, 6] = sfigura;// figura//Figura
            //            sArray[iRows, 7] = sSubTipoDocumentoId;//Documento//Documento
            //            sArray[iRows, 8] = susuario_capturo;//Usuario oficio captura
            //            sArray[iRows, 9] = sCasoNumero;//referencia king //Referencia King
            //            sArray[iRows, 10] = susuario_responsable;//Responsable king //Responsable King
            //            sArray[iRows, 11] = sreferenciascliente;//referencia Cliente //Referencia Cliente
            //            sArray[iRows, 12] = scontactosdelcliente;//referencia king //Contacto Cliente
            //            sArray[iRows, 13] = sCasoDenominacion;//referencia king //Titulo
            //            sArray[iRows, 14] = sCasoNumeroExpedienteLargo;//referencia king //Expediente
            //            sArray[iRows, 15] = sinteresados;//referencia king //Titular
            //            sArray[iRows, 16] = sClienteNombre;//referencia king //Cliente
            //            sArray[iRows, 17] = sTipoPlazoId;//referencia king //Tipo de Plazo Id
            //            sArray[iRows, 18] = scasoId;//referencia king //Tipo de Plazo Id
            //            sArray[iRows, 19] = sestatuscasoid;//referencia king //Caso Id
            //            sArray[iRows, 20] = sTipoTareaId;//referencia king //Id estatus
            //            sArray[iRows, 21] = sGrupoid;//referencia king //Tarea
            //            sArray[iRows, 22] = sPlazoMotivoCancelacion;//referencia king //Grupo
            //            sArray[iRows, 23] = "usurio cancelo ";//referencia king //Motivo Cancelación
            //            sArray[iRows, 24] = sTipoTareaDescrip;//referencia king //Usuario prorrogó o canceló
            //            sArray[iRows, 25] = sPlazoFechaAtencion;//Descripción tarea
            //            sArray[iRows, 26] = sPlazoFechaAtencion;//Fecha de atención
            //            sArray[iRows, 27] = splazoid;//Fecha de atención
            //            sArray[iRows, 28] = sTipoSolicitudId;//Fecha de atención

            //            iRows++;
            //            //dataGridView1.Rows.Add(sCasoNumero, "sfde", "wofno", "spkdjcposdmf", "sñdknvolenfvpe", "sñpdfpwprjfponv", "sñklvdncpwcp");
            //        }
            //        iCountgeneral = iRows;
            //        respuestastrig_plazos.Close();
            //        conin_plazos.Cerrarconexion();
            //    };
            //    using (loadingconsulta form = new loadingconsulta(act))
            //    {
            //        form.ShowDialog();
            //    }
            //    iRowscount.Text = iCountgeneral + "";
            //    dataGridView1.Rows.Clear();
            //    for (int xc = 0; xc < iCountgeneral; xc++)
            //    {
            //        //String[] sRosvalue = new String[10];
            //        //sRosvalue = sArray[0].OfType<object>().Select(o => o.ToString()).ToArray();
            //        //string []sRosvalues = sRosvalue[0];
            //        dataGridView1.Rows.Add(
            //            sArray[xc, 0],
            //            sArray[xc, 1],
            //            sArray[xc, 2],
            //            sArray[xc, 3],
            //            sArray[xc, 4],
            //            sArray[xc, 5],
            //            sArray[xc, 6],
            //            sArray[xc, 7],
            //            sArray[xc, 8],
            //            sArray[xc, 9],
            //            sArray[xc, 10],
            //            sArray[xc, 11],
            //            sArray[xc, 12],
            //            sArray[xc, 13],
            //            sArray[xc, 14],
            //            sArray[xc, 15],
            //            sArray[xc, 16],
            //            sArray[xc, 17],
            //            sArray[xc, 18],
            //            sArray[xc, 19],
            //            sArray[xc, 20],
            //            sArray[xc, 21],
            //            sArray[xc, 22],
            //            sArray[xc, 23],
            //            sArray[xc, 24],
            //            sArray[xc, 25],
            //            sArray[xc, 26],
            //            sArray[xc, 27],
            //            sArray[xc, 28]
            //       );
            //    }
            //    iCountgeneral = 0;
            //}
            //else {
            //    MessageBox.Show("Debe por lo menos seleccionar un Grupo y un estado para consultar plazos.");
            //}
        }
        public void consultamoslosplazos() { 
        
        }

        private void cbgrupo_plazo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ////String sGrupo = cbgrupo_plazo
                //String sGrupo = "";// (cbGrupo.SelectedItem as ComboboxItem).Text;

                //if (cbGrupo.SelectedItem != null)
                //{
                //    sGrupo = (cbGrupo.SelectedItem as ComboboxItem).Value.ToString();
                //}
                //conect con_grupo_plazo = new conect();
                //String squery_grupo_plazo = "SELECT * FROM grupoplazo where GrupoId = " + sGrupo + ";";
                //MySqlDataReader resp_grupo_plazo = con_grupo_plazo.getdatareader(squery_grupo_plazo);
                //while (resp_grupo_plazo.Read())
                //{
                //    cbgrupo_plazo.Items.Add(funcionesgenerales.validareader("GrupoPlazoDescripcion", "GrupoPlazoId", resp_grupo_plazo));
                //}
                //resp_grupo_plazo.Close();
                //con_grupo_plazo.Cerrarconexion();
                cbTipo_plazo.Text = "";
                ComboboxItem cGrupo_Seleccionado = (cbGrupo.SelectedItem as ComboboxItem);
                ComboboxItem cGrupoplazo_Seleccionado = (cbgrupo_plazo.SelectedItem as ComboboxItem);
                if (cGrupo_Seleccionado != null && cGrupoplazo_Seleccionado != null)
                {
                    //seleccionamos las opciones de grupos
                    conect con_grupo_plazo = new conect();
                    String squery_grupo_plazo = "Select * from tipoplazo where GrupoPlazoId = " + cGrupoplazo_Seleccionado.Value + " and Grupoid = " + cGrupo_Seleccionado.Value + ";";
                    MySqlDataReader resp_grupo_plazo = con_grupo_plazo.getdatareader(squery_grupo_plazo);
                    cbTipo_plazo.Enabled = true;
                    cbTipo_plazo.Items.Clear();
                    while (resp_grupo_plazo.Read())
                    {
                        cbTipo_plazo.Items.Add(funcionesgenerales.validareader("TipoPlazoDescrip", "TipoPlazoId", resp_grupo_plazo));
                    }
                    resp_grupo_plazo.Close();
                    con_grupo_plazo.Cerrarconexion();
                }
                else
                {
                    cbTipo_plazo.Enabled = false;
                    cbTipo_plazo.Items.Clear();
                }

                //FIN seleccionamos las opciones de grupos
                //MessageBox.Show(cSeleccionado.Text);
            }
            catch (Exception ex)
            {
                new filelog("plazos.cs <--", ex.ToString());
            }
        }

        public void cargagrupos() {
            try
            {
                //String sGrupo = cbgrupo_plazo
                String sGrupo = "";// (cbGrupo.SelectedItem as ComboboxItem).Text;

                if (cbGrupo.SelectedItem != null)
                {
                    sGrupo = (cbGrupo.SelectedItem as ComboboxItem).Value.ToString();
                }
                conect con_grupo_plazo = new conect();
                String squery_grupo_plazo = "SELECT * FROM grupoplazo where GrupoId = " + sGrupo + ";";
                MySqlDataReader resp_grupo_plazo = con_grupo_plazo.getdatareader(squery_grupo_plazo);
                while (resp_grupo_plazo.Read())
                {
                    cbgrupo_plazo.Items.Add(funcionesgenerales.validareader("GrupoPlazoDescripcion", "GrupoPlazoId", resp_grupo_plazo));
                }
                resp_grupo_plazo.Close();
                con_grupo_plazo.Cerrarconexion();


                //cbTipo_plazo.Text = "";
                //ComboboxItem cGrupo_Seleccionado = (cbGrupo.SelectedItem as ComboboxItem);
                //ComboboxItem cGrupoplazo_Seleccionado = (cbgrupo_plazo.SelectedItem as ComboboxItem);
                //if (cGrupo_Seleccionado != null && cGrupoplazo_Seleccionado != null)
                //{
                //    //seleccionamos las opciones de grupos
                //    conect con_grupo_plazo = new conect();
                //    String squery_grupo_plazo = "Select * from tipoplazo where GrupoPlazoId = " + cGrupoplazo_Seleccionado.Value + " and Grupoid = " + cGrupo_Seleccionado.Value + ";";
                //    MySqlDataReader resp_grupo_plazo = con_grupo_plazo.getdatareader(squery_grupo_plazo);
                //    cbTipo_plazo.Enabled = true;
                //    cbTipo_plazo.Items.Clear();
                //    while (resp_grupo_plazo.Read())
                //    {
                //        cbTipo_plazo.Items.Add(funcionesgenerales.validareader("TipoPlazoDescrip", "TipoPlazoId", resp_grupo_plazo));
                //    }
                //    resp_grupo_plazo.Close();
                //    con_grupo_plazo.Cerrarconexion();
                //}
                //else {
                //    cbTipo_plazo.Enabled = false;
                //    cbTipo_plazo.Items.Clear();
                //}

                //FIN seleccionamos las opciones de grupos
                //MessageBox.Show(cSeleccionado.Text);
            }
            catch (Exception ex)
            {
                new filelog("plazos.cs <--", ex.ToString());
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //(sender as row)
            try
            {
                String sTiposolicitudid = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                //String plazoid = dataGridView1.SelectedRows[0].Cells[27].Value.ToString();
                String casoid = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                String grupoid = (cbGrupo.SelectedItem as ComboboxItem).Value.ToString();
                switch (grupoid)
                {
                    //case "Todos":
                    //    {
                    //        bAll consul = new bAll(iTiposolicitud, cObjcap, ologuin);
                    //        consul.Show();
                    //        this.Close();
                    //        //this.Close();
                    //    } break;
                    case "1":
                        {
                            Consutlacaso buscarcliente = new Consutlacaso(login, capturaobj, Int32.Parse(sTiposolicitudid));//éste es de patente 
                            buscarcliente.sGTipocaso = grupoid;
                            
                            consultacaso cConsultaid = new consultacaso(login, capturaobj, buscarcliente, casoid);//éste es de patente 
                            cConsultaid.button2.Enabled = false;
                            cConsultaid.btn_salir.Enabled = true;
                            cConsultaid.ShowDialog();
                        } break;
                    case "2":
                        {
                            bMarcas consul = new bMarcas(Int32.Parse(sTiposolicitudid), capturaobj, login);
                            consul.sGTipocaso = grupoid;

                            fTmarcas cMarcas = new fTmarcas(login, capturaobj, consul, casoid);
                            cMarcas.button2.Enabled = false;
                            //cMarcas.button3.Enabled = true;
                            cMarcas.ShowDialog();
                            //this.Close();
                            //MessageBox.Show("En desarrollo");
                        } break;
                    //case "Contencioso":
                    //    {
                    //        bContencioso consul = new bContencioso(iTiposolicitud, cObjcap, ologuin);
                    //        consul.Show();
                    //        this.Close();
                    //        //MessageBox.Show("En desarrollo");
                    //    } break;
                    //case "Consulta":
                    //    {
                    //        bConsulta consul = new bConsulta(iTiposolicitud, cObjcap, ologuin);
                    //        consul.Show();
                    //        this.Close();
                    //        //this.Close();
                    //        //MessageBox.Show("En desarrollo");
                    //    } break;
                    //case "Oposicion a solicitudes":
                    //    {
                    //        bOposicion consul = new bOposicion(iTiposolicitud, cObjcap, ologuin);
                    //        consul.Show();
                    //        this.Close();
                    //        //this.Close();
                    //    } break;
                    //case "Variedades vegetales":
                    //    {
                    //        bVariedadv conul = new bVariedadv(iTiposolicitud, cObjcap, ologuin);
                    //        conul.Show();
                    //        this.Close();
                    //    } break;
                    //case "Derechos de autor":
                    //    {
                    //        bDerechoautor conult = new bDerechoautor(7, cObjcap, ologuin);
                    //        conult.Show();
                    //        this.Close();
                    //        //this.Close();
                    //    } break;
                    //case "Reserva de derechos":
                    //    {
                    //        bReservadederechos conul = new bReservadederechos(8, cObjcap, ologuin);
                    //        conul.Show();
                    //        this.Close();
                    //    } break;


                }
                
                //MessageBox.Show("El valor es:" + plazoid + "   datagridvalue: " + e.RowIndex.ToString());
            }
            catch(Exception E) {
                MessageBox.Show(E.Message);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try {
                String plazoid = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                String sTipoplazo = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                String sFechavigencia = dataGridView1.SelectedRows[0].Cells[6].Value.ToString().Substring(0,10);
                if (plazoid != "")
                {
                    atenderplazo atender = new atenderplazo(plazoid, sFechavigencia, sTipoplazo, login);
                    atender.ShowDialog();
                }
                else {
                    MessageBox.Show("Debe seleccionar un plazo.");
                }
            }catch(Exception E){
                MessageBox.Show("Debe seleccionar un plazo para poder atender");
            }
            
        }

        private void plazos_Load(object sender, EventArgs e)
        {
            //dthasta.Format = DateTimePickerFormat.Custom;
            
        }

        private void tFechahasta_TextChanged(object sender, EventArgs e)
        {

        }

        private void tFechadesde_KeyPress(object sender, KeyPressEventArgs e)
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


            if (tFechadesde.Text.Length == 2)
            {
                tFechadesde.Text = tFechadesde.Text + "-";
                tFechadesde.SelectionStart = tFechadesde.Text.Length;

            }
            if (tFechadesde.Text.Length == 5)
            {
                tFechadesde.Text = tFechadesde.Text + "-";
                tFechadesde.SelectionStart = tFechadesde.Text.Length;
            }
        }

        private void tFechahasta_KeyPress(object sender, KeyPressEventArgs e)
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


            if (tFechahasta.Text.Length == 2)
            {
                tFechahasta.Text = tFechahasta.Text + "-";
                tFechahasta.SelectionStart = tFechahasta.Text.Length;

            }
            if (tFechahasta.Text.Length == 5)
            {
                tFechahasta.Text = tFechahasta.Text + "-";
                tFechahasta.SelectionStart = tFechahasta.Text.Length;
            }
        }

        private void tFechadesde_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tFechadesde);
        }

        private void tFechahasta_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tFechahasta);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            //aqui vamos al caso  donde está el plazo
            //buscamos el grupo para saber que tipo de objeto vamos abrir y obtenemos el número del caso id con el caso numero o lo traemos de la vista
            // cGrupo_Seleccionado    // es el grupo de tipo de caso
            //ComboboxItem cGrupo_Seleccionado = (cbGrupo.SelectedItem as ComboboxItem);
            //if (!(cGrupo_Seleccionado is null)) {
            //    switch (cGrupo_Seleccionado.Value.ToString())
            //    {
            //        case "1": {
            //                Consutlacaso objbusqueda = new Consutlacaso(login, capturaobj,1);
            //                consultacaso cConsultaid = new consultacaso(login, capturaobj, objbusqueda, "+69");
            //                this.Hide();
            //                cConsultaid.Show();
                        
            //        }break;//patentes
            //        case "2": { } break;//marcas
            //        case "3": { } break;//etc etc ...
            //        case "4": { } break;
            //    }
            //}
            


        }

        private void button25_Click(object sender, EventArgs e)
        {
            lbGeneraexcel.Text = "Generando excel ...";

            generaexcel(dataGridView1);
            //try
            //{
            //    var excelApp = new Excel.Application();
            //    excelApp.Visible = true;
            //    //Crea un nuevo libro
            //    excelApp.Workbooks.Add();
            //    //Crear una hoja
            //    Excel._Worksheet workSheet = excelApp.ActiveSheet;
            //    //En versiones anteriores de C# se requiere una conversión explícita
            //    //Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;
            //    //Estableciendo los encabezados de columna
            //    workSheet.Cells[3, "A"] = "Tipoplazo";
            //    workSheet.Cells[3, "B"] = "Estatus";
            //    workSheet.Cells[3, "C"] = "plazo final";
            //    workSheet.Cells[3, "D"] = "Mes";
            //    workSheet.Cells[3, "E"] = "Fecha notificación";
            //    workSheet.Cells[3, "F"] = "Vencimiento original";
            //    workSheet.Cells[3, "G"] = "Tipo documento";
            //    workSheet.Cells[3, "H"] = "Documento";
            //    workSheet.Cells[3, "I"] = "Usuario oficio captura";
            //    workSheet.Cells[3, "J"] = "Referencia king";
            //    workSheet.Cells[3, "K"] = "Responsable";



            //    workSheet.Cells[1, "A"] = " Plazos Casos king: ";
            //    workSheet.Range["A1", "F1"].Merge();
            //    workSheet.Range["A1", "F1"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //    workSheet.Range["A1", "F1"].Font.Bold = true;
            //    workSheet.Range["A1", "F1"].Font.Size = 20;
            //    var row = 3;

            //    for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //    {
            //        workSheet.Cells[i + 4, "A"] = dataGridView1.Rows[i].Cells[4].Value;
            //        workSheet.Cells[i + 4, "B"] = dataGridView1.Rows[i].Cells[5].Value;
            //        workSheet.Cells[i + 4, "C"] = dataGridView1.Rows[i].Cells[6].Value;
            //        workSheet.Cells[i + 4, "D"] = dataGridView1.Rows[i].Cells[7].Value;
            //        workSheet.Cells[i + 4, "E"] = dataGridView1.Rows[i].Cells[8].Value;
            //        workSheet.Cells[i + 4, "F"] = dataGridView1.Rows[i].Cells[9].Value;
            //        workSheet.Cells[i + 4, "G"] = dataGridView1.Rows[i].Cells[10].Value;
            //        workSheet.Cells[i + 4, "H"] = dataGridView1.Rows[i].Cells[11].Value;
            //        workSheet.Cells[i + 4, "I"] = dataGridView1.Rows[i].Cells[12].Value;
            //        workSheet.Cells[i + 4, "J"] = dataGridView1.Rows[i].Cells[13].Value;
            //        workSheet.Cells[i + 4, "K"] = dataGridView1.Rows[i].Cells[14].Value;
            //        //workSheet.Cells[i + 4, "L"] = dataGridView1.Rows[i].Cells[11].Value;
            //        //workSheet.Cells[i + 4, "M"] = dataGridView1.Rows[i].Cells[12].Value;
            //        //workSheet.Cells[i + 4, "N"] = dataGridView1.Rows[i].Cells[13].Value;
            //        //workSheet.Cells[i + 4, "O"] = dataGridView1.Rows[i].Cells[14].Value;
            //        //workSheet.Cells[i + 4, "P"] = dataGridView1.Rows[i].Cells[15].Value;

            //        //workSheet.Cells[i + 4, "AP"] = listView1.Items[i].SubItems[41].Text;
            //        //workSheet.Cells[i + 4, "AQ"] = listView1.Items[i].SubItems[42].Text;
            //        row = i;
            //    }
            //    //foreach (var acct in listView1.Items)
            //    //{
            //    //    row++;
            //    //    workSheet.Cells[row, "A"] = acct.;
            //    //    workSheet.Cells[row, "B"] = acct;
            //    //    workSheet.Cells[row, "C"] = acct;
            //    //}

            //    workSheet.Columns[1].AutoFit();
            //    workSheet.Columns[2].AutoFit();
            //    workSheet.Columns[3].AutoFit();

            //    //Aplicando un autoformato a la tabla
            //    workSheet.Range["A3", "k" + (row + 4)].AutoFormat(Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic2);
            //}
            //catch (Exception E)
            //{
            //    Console.Write("Se canceló la exportación");
            //    new filelog(login.sId, E.ToString());

            //}
        }
        public void generaexcel(DataGridView tabla) {
            try {

                SLDocument obj = new SLDocument();
                button25.Enabled = false;
                
                //agregamos el nombre de las columnas
                int ic = 1;
                foreach (DataGridViewColumn column in tabla.Columns) {
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
                        if (!(row.Cells[ic - 1].Value is null)) {
                            sValor = row.Cells[ic - 1].Value.ToString();
                        }

                        if (sValor == "01/01/0001 12:00:00 a. m.") {
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
                obj.SaveAs(ruta+"excel_"+ fechalog + ".xlsx");
                //abrirmos el archivo
                Process.Start(ruta + "excel_" + fechalog + ".xlsx");
                button25.Enabled = true;
                lbGeneraexcel.Text = "";
            }
            catch (Exception exs) {
                new filelog("error al generar excel ", " :"+exs.Message);
                MessageBox.Show(exs.Message);
                button25.Enabled = true;
                lbGeneraexcel.Text = "";
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            String sQuerybusqueda = " select * from plazos_patentes_view where " +
                                    " TipoDocumentoDescrip like '%" + tbBusqueda.Text + "%' AND " +
                                    " SubTipoDocumentoDescrip like '%" + tbBusqueda.Text + "%' AND " +
                                    " TipoPlazoDescrip like '%" + tbBusqueda.Text + "%' AND " +
                                    " EstatusPlazoDescrip like '%" + tbBusqueda.Text + "%' AND " +
                                    " usuariocreo like '%" + tbBusqueda.Text + "%' AND " +
                                    " usuarioatendio like '%" + tbBusqueda.Text + "%' AND " +
                                    " CasoNumero like '%" + tbBusqueda.Text + "%' AND " +
                                    " CasoId like '%" + tbBusqueda.Text + "%' AND " +
                                    " TipoSolicitudId like '%" + tbBusqueda.Text + "%' AND " +
                                    " CasoTituloespanol like '%" + tbBusqueda.Text + "%' AND " +
                                    " CasoTituloingles like '%" + tbBusqueda.Text + "%' AND " +
                                    " Plazos_detalleid like '%" + tbBusqueda.Text + "%' AND " +
                                    " Plazosid like '%" + tbBusqueda.Text + "%' AND " +
                                    " documentoid like '%" + tbBusqueda.Text + "%' AND " +
                                    " usuario_creo_plazodetalle like '%" + tbBusqueda.Text + "%' AND " +
                                    " Tipo_plazoid like '%" + tbBusqueda.Text + "%' AND " +
                                    " Estatus_plazoid like '%" + tbBusqueda.Text + "%' AND " +
                                    " Fecha_notificacion like '%" + tbBusqueda.Text + "%' AND " +
                                    " Mesdiferencia like '%" + tbBusqueda.Text + "%' AND " +
                                    " Name_exp_9 like '%" + tbBusqueda.Text + "%' AND " +
                                    " Fecha_vencimiento_3m like '%" + tbBusqueda.Text + "%' AND " +
                                    " Fecha_vencimiento_4m like '%" + tbBusqueda.Text + "%' AND " +
                                    " Fecha_atendio_plazo like '%" + tbBusqueda.Text + "%' AND " +
                                    " Usuarioid_atendio_plazo like '%" + tbBusqueda.Text + "%' AND " +
                                    " Motivo_cancelacion_plazo like '%" + tbBusqueda.Text + "%' AND " +
                                    " Fecha_cancelacion_plazo like '%" + tbBusqueda.Text + "%' AND " +
                                    " usuario_cancelo like '%" + tbBusqueda.Text + "%' AND " +
                                    " Atendio_Plazos_detalleid like '%" + tbBusqueda.Text + "%' AND " +
                                    " Fecha_atendio_plazo_sistema like '%" + tbBusqueda.Text + "%' AND " +
                                    " AnualidadId like '%" + tbBusqueda.Text + "%' AND " +
                                    " TipoSolicitudDescripcion like '%" + tbBusqueda.Text + "%' AND " +
                                    " ReferenciaNombre like '%" + tbBusqueda.Text + "%' AND " +
                                    " ClienteNombre like '%" + tbBusqueda.Text + "%' AND " +
                                    " InteresadoNombre like '%" + tbBusqueda.Text + "%' AND " +
                                    " EstatusCasoDescrip like '%" + tbBusqueda.Text + "%' AND " +
                                    " PaisNombre like '%" + tbBusqueda.Text + "%' AND " +
                                    " inicio_renovacion like '%" + tbBusqueda.Text + "%' AND " +
                                    " fin_renovacion like '%" + tbBusqueda.Text + "%' AND " +
                                    " declaracion_de_uso like '%" + tbBusqueda.Text + "%' AND " +
                                    " rango_anualidades like '%" + tbBusqueda.Text + "%' AND " +
                                    " fecha_pago_anualidades like '%" + tbBusqueda.Text + "%' AND " +
                                    " CasoProductosClase like '%" + tbBusqueda.Text + "%' AND " +
                                    " seguimientocliente like '%" + tbBusqueda.Text + "%' AND " +
                                    "seguimientointeresado like '%" + tbBusqueda.Text + "%';";
            consultarplazos_view(sQuerybusqueda);
        }

        private void cbGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //String sGrupo = cbgrupo_plazo
                String sGrupo = "";// (cbGrupo.SelectedItem as ComboboxItem).Text;
                if (cbGrupo.SelectedItem != null)
                {
                    sGrupo = (cbGrupo.SelectedItem as ComboboxItem).Value.ToString();
                }
                conect con_grupo_plazo = new conect();
                String squery_grupo_plazo = "SELECT * FROM grupoplazo where GrupoId = " + sGrupo + ";";
                MySqlDataReader resp_grupo_plazo = con_grupo_plazo.getdatareader(squery_grupo_plazo);
                cbgrupo_plazo.Items.Clear();
                while (resp_grupo_plazo.Read())
                {
                    cbgrupo_plazo.Items.Add(funcionesgenerales.validareader("GrupoPlazoDescripcion", "GrupoPlazoId", resp_grupo_plazo));
                }
                resp_grupo_plazo.Close();
                con_grupo_plazo.Cerrarconexion();
            }
            catch (Exception exs) {
                new filelog("", ""+exs.Message);

            }

        }
    }
}

