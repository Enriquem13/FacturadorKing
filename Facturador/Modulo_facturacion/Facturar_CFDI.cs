using Facturador.PlantillaFactura;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturador
{
    public partial class Facturar_CFDI : Form
    {
        funcionesdicss fun_dicss = new funcionesdicss();
        String[] cbServicios;
        String[] honorarios_pesos;
        String[] derechos_pesos;
        String[] honorarios_dolares;
        String[] derechos_dolares;
        String[] honorarios_euros;
        String[] derechos_euros;
        List<obj_factura_concepto> conceptosfac;
        consultacaso conFormcaso;
        public Facturar_CFDI()
        {
            InitializeComponent();
             conceptosfac = new List<obj_factura_concepto>();
            /*Cargamos el tipo de cambio*/
            //WebClient client = new WebClient();
            //string respuesta = client.DownloadString("https://dicss.com.mx/");
            //MessageBox.Show(respuesta);
            cbServicios = new String[100];
            honorarios_pesos = new String[100];
            derechos_pesos = new String[100];
            honorarios_dolares = new String[100];
            derechos_dolares = new String[100];
            honorarios_euros = new String[100];
            derechos_euros = new String[100];
            radioButton1.Checked = true;

            label_fechahoy.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //fun_dicss = new funcionesdicss();
            conect con_2 = new conect();
            String sGetgrupo = "select * from grupo";
            MySqlDataReader resp_getidspatentes = con_2.getdatareader(sGetgrupo);
            while (resp_getidspatentes.Read())
            {
                cbGrupo.Items.Add(fun_dicss.validareader("GrupoId", "GrupoDescripcion", resp_getidspatentes));
            }
            resp_getidspatentes.Close();
            con_2.Cerrarconexion();

            
        }

        public void inicializacaso(consultacaso Formcaso) {
            conFormcaso = Formcaso;
            tNumCaso.Text = Formcaso.gSCasoNumero;
            cbGrupo.Text = "Patente";
            tNcliente.Text = Formcaso.gSclienteid;
            cClientebox.Text = Formcaso.lCliente_texbox.Text;
            textTipo.Text = Formcaso.lCliente_texbox.Text;
            textSubtipo.Text = Formcaso.tbSubtipo.Text;
            textCapitulo.Text = Formcaso.tbCapitulo.Text;
            textFechacarta.Text = Formcaso.tbFechacarta.Text;
            textClientduedate.Text = Formcaso.tbClientduedate.Text;
            textPlazolegal.Text = Formcaso.tbPlazolegal.Text;
            textNumref.Text = Formcaso.lReferencia.Text;
            textConcecion.Text = Formcaso.tbFechaconcesion.Text;
            textPublicacion.Text = Formcaso.tbFechapublicacion.Text;
            textNumexp.Text = Formcaso.tbExpediente.Text;
            textDivulgacion.Text = Formcaso.tbFechadivulgacion.Text;
            textNumreg.Text = Formcaso.tbRegistro.Text;
            textVigencia.Text = Formcaso.tbFechavigencia.Text;
            textCasodiseno.Text = Formcaso.tbCasodiseno.Text;
            textIdioma.Text = Formcaso.cbIdioma.Text;
            textTitulo.Text = Formcaso.rtTitulo.Text;
            textDenom.Text = Formcaso.rtTituloidionaoriginal.Text;
            /*tNumservicio.Text = "";
            comboServicio.Text = "";
            combSatservicios.Text = "";*/
            textEstatus.Text = Formcaso.tbEstatus_header.Text;
            
            /*hacemos el recorrido buscando el docuemnto tipo 2*/
            //for (var xcount = Formcaso.lvdocumentosimpi.Items.Count; xcount > 0; xcount--)
            //{
            //    if(Formcaso.lvdocumentosimpi.Items[7].SubItems[1].ToString() == "Escrito"){
            //        sUltSubtipodocumentoid = Formcaso.lvdocumentosimpi.Items[7].SubItems[19].ToString();
            //    }
            //}

            /*Mostramos los interesados*/
            for (int x = 0; x<Formcaso.lvinteresados.Items.Count; x++)
            {
                String TipoRelacionDescrip = Formcaso.lvinteresados.Items[x].SubItems[0].Text;//idinteresado
                String InteresadoID = Formcaso.lvinteresados.Items[x].SubItems[1].Text;//idinteresado
                String interesadonombrecompleto = Formcaso.lvinteresados.Items[x].SubItems[2].Text;//idinteresado
                String nacionalidad = Formcaso.lvinteresados.Items[x].SubItems[3].Text;//idinteresado
                String direccioncompleta = Formcaso.lvinteresados.Items[x].SubItems[4].Text;//idinteresado
                String InteresadoPoder = Formcaso.lvinteresados.Items[x].SubItems[5].Text;//idinteresado
                String InteresadoRGP = Formcaso.lvinteresados.Items[x].SubItems[6].Text;//idinteresado
                String sRFCinteresado = Formcaso.InteresadoRFC[x];
                ComboboxItem interesado = new ComboboxItem();
                interesado.Text = interesadonombrecompleto + " RFC: " + sRFCinteresado + "Dirección: " + direccioncompleta;
                interesado.Value = InteresadoID;
                cbInteresados.Items.Add(interesado);
                /*
                 TipoRelacionDescrip
                 * InteresadoID
                 * interesadonombrecompleto
                 * nacionalidad
                 * direccioncompleta
                 * InteresadoPoder
                 * InteresadoRGP
                 * 
                 */
            }

            //lvinteresados
            /*Consultamos el catálogo de servicios para saber si el cliente tiene una tarifa especial
             y aquí validaremos si la tarifa al cliente va sobre el cliente , interesado o caso
             */

            conect con_conuslt_tarifa = new conect();
            //"SELECT * from tarifas_base_king;"
            String sServicios_tarifa = "SELECT count(*) as num from tarifas_base_king where tarifas_base_king.id_baseking = " + Formcaso.gSclienteid;
            MySqlDataReader resp_Servicios_tarifa = con_conuslt_tarifa.getdatareader(sServicios_tarifa);
            String sTarifas = "";
            while (resp_Servicios_tarifa.Read())
            { //resp_Servicios_tarifa
                sTarifas = fun_dicss.validareader("num", "num", resp_Servicios_tarifa).Value.ToString();
            }
            resp_Servicios_tarifa.Close();
            con_conuslt_tarifa.Cerrarconexion();

            int iTienetarifa = int.Parse(sTarifas);
            String sIdcliente = "";
            if (iTienetarifa > 0)
            {
                sIdcliente = Formcaso.gSclienteid;
            }else{
                sIdcliente = "0";
            }
            cargaconceptostarifas(Formcaso.Subtipodocumentoidultimoescrito);
        }
        public void cargaconceptostarifas(String sSubtipodocumentoid) {
            /*Agregamos el catálogo de servicios*/
            conect con_3 = new conect();
            //"SELECT * from tarifas_base_king;"
            //0 es para todos los clientes que no tengan tarifa especial, y aplica la tarifa general.
            String sConceptos = "";
            if (String.IsNullOrEmpty(sSubtipodocumentoid))
            {
                sConceptos = "SELECT * from tarifa_conceptos_king;";
            }
            else {
                sConceptos = " SELECT  " +
                            "     * " +
                            " FROM " +
                            "     tarifa_conceptos_king, " +
                            "     subtipodocumento, " +
                            "     relacion_subtipodocumento_tarifa " +
                            " WHERE " +
                            " relacion_subtipodocumento_tarifa.id_tarifa_concepto = tarifa_conceptos_king.id_concepto " +
                            " and relacion_subtipodocumento_tarifa.id_subtipodocumentoid = subtipodocumento.SubTipoDocumentoId " +
                            " and subtipodocumento.SubTipoDocumentoId = " + sSubtipodocumentoid;
            }
            MySqlDataReader resp_Servicios = con_3.getdatareader(sConceptos);
            while (resp_Servicios.Read())
            {
                ComboboxItem item = new ComboboxItem();
                item.Value = fun_dicss.validareader("concepto", "id_concepto", resp_Servicios).Value;
                item.Text = fun_dicss.validareader("concepto", "id_concepto", resp_Servicios).Value + " - " + fun_dicss.validareader("concepto", "id_concepto", resp_Servicios).Text;
                int indice = Int32.Parse(fun_dicss.validareader("concepto", "id_concepto", resp_Servicios).Value.ToString());
                cbServicios[indice] = fun_dicss.validareader("concepto", "id_concepto", resp_Servicios).Value + " - " + fun_dicss.validareader("concepto", "id_concepto", resp_Servicios).Text;
                /*int indice = Int32.Parse(fun_dicss.validareader("concepto", "id_concepto", resp_Servicios).Value.ToString());
                cbServicios[indice] = fun_dicss.validareader("concepto", "id_concepto", resp_Servicios).Text;

                honorarios_dolares[indice] = fun_dicss.validareader("honorarios dolares", "id_concepto", resp_Servicios).Text;
                derechos_dolares[indice] = fun_dicss.validareader("derechos dolares", "id_concepto", resp_Servicios).Text;

                honorarios_euros[indice] = fun_dicss.validareader("honorarios euros", "id_concepto", resp_Servicios).Text;
                derechos_euros[indice] = fun_dicss.validareader("derechos euros", "id_concepto", resp_Servicios).Text;

                honorarios_pesos[indice] = fun_dicss.validareader("honorarios pesos", "id_concepto", resp_Servicios).Text;
                derechos_pesos[indice] = fun_dicss.validareader("derechos pesos", "id_concepto", resp_Servicios).Text;*/
                comboServicio.Items.Add(item);
            }
            resp_Servicios.Close();
            con_3.Cerrarconexion();
        }
        public void cargartarifas(String sIdconcepto)
        {
            /*Agregamos el catálogo de servicios*/
            conect con_3 = new conect();
            //"SELECT * from tarifas_base_king;"
            //0 es para todos los clientes que no tengan tarifa especial, y aplica la tarifa general.

            //String sServicios = "SELECT * from tarifas_base_king;";
            /*2020-09-07-18:25*/
            String sServicios = " SELECT  " +
                                "     * " +
                                " FROM " +
                                "     tarifas_base_king " +
                                " WHERE " +
                                "     id_concepto = " + sIdconcepto + " AND id_baseking = 0;";

            MySqlDataReader resp_Servicios = con_3.getdatareader(sServicios);
            while (resp_Servicios.Read())
            {
                ComboboxItem item = new ComboboxItem();
                item.Value = fun_dicss.validareader("concepto", "id_concepto", resp_Servicios).Value;
                item.Text = fun_dicss.validareader("concepto", "id_concepto", resp_Servicios).Value + " - " + fun_dicss.validareader("concepto", "id_concepto", resp_Servicios).Text;
                int indice = Int32.Parse(fun_dicss.validareader("concepto", "id_concepto", resp_Servicios).Value.ToString());
                cbServicios[indice] = fun_dicss.validareader("concepto", "id_concepto", resp_Servicios).Text;


                honorarios_dolares[indice] = fun_dicss.validareader("honorarios dolares", "id_concepto", resp_Servicios).Text;
                derechos_dolares[indice] = fun_dicss.validareader("derechos dolares", "id_concepto", resp_Servicios).Text;

                honorarios_euros[indice] = fun_dicss.validareader("honorarios euros", "id_concepto", resp_Servicios).Text;
                derechos_euros[indice] = fun_dicss.validareader("derechos euros", "id_concepto", resp_Servicios).Text;

                honorarios_pesos[indice] = fun_dicss.validareader("honorarios pesos", "id_concepto", resp_Servicios).Text;
                derechos_pesos[indice] = fun_dicss.validareader("derechos pesos", "id_concepto", resp_Servicios).Text;


                comboServicio.Items.Add(item);
            }
            resp_Servicios.Close();
            con_3.Cerrarconexion();
        }
        private void button1_Click(object sender, EventArgs e)//btn_buscar
        {
            try {
                String sVCasoId = "";
                String sVTipoSolicitudId = "";
                String sVSubTipoSolicitudId = "";
                String sVTipoPctId = "";
                String sVCasoTituloespanol = "";
                String sVCasoTituloingles = "";
                String sVIdiomaId = "";
                String sVCasoFechaConcesion = "";
                String sVCasoFechaRecepcion = "";
                String sVCasoFechaVigencia = "";
                String sVCasoFechaPublicacionSolicitud = "";
                String sVCasoFechaLegal = "";
                String sVCasoNumConcedida = "";
                String sVCasoNumeroExpedienteLargo = "";
                String sVCasoNumero = "";
                String sVResponsableId = "";
                String sVCasoTipoCaptura = "";
                String sVCasoTitular = "";
                String sVEstatusCasoId = "";
                String sVUsuarioId = "";
                String sVAreaImpiId = "";
                String sVCasoFechaInternacional = "";
                String sVPaisId = "";
                String sVCasoFechaPruebaUsoSig = "";
                String sVCasoFechaFilingCliente = "";
                String sVCasoFechaFilingSistema = "";
                String sVCasoFechaDivulgacionPrevia = "";
                String sVCasoFechaCartaCliente = "";
                String sVDivicionalid = "";

                String stNumCaso = tNumCaso.Text.Trim();
                object sGruposeleccionado = (cbGrupo.SelectedItem as ComboboxItem).Value;
                if (stNumCaso != "" && sGruposeleccionado.ToString() != "")//validamos que esté seleccionado el grupo
                {
                    conect con_2 = new conect();
                    String sGrupo_tabla = get_tablaconsulta(sGruposeleccionado.ToString());
                    String sGetgrupo = "select * from " + sGrupo_tabla;
                    MySqlDataReader resp_getidspatentes = con_2.getdatareader(sGetgrupo);
                    while (resp_getidspatentes.Read())
                    {
                        sVCasoId = fun_dicss.validareader("CasoId", "CasoId", resp_getidspatentes).Text;
                        sVTipoSolicitudId = fun_dicss.validareader("TipoSolicitudId", "TipoSolicitudId", resp_getidspatentes).Text;
                        sVSubTipoSolicitudId = fun_dicss.validareader("SubTipoSolicitudId", "SubTipoSolicitudId", resp_getidspatentes).Text;
                        sVTipoPctId = fun_dicss.validareader("TipoPctId", "TipoPctId", resp_getidspatentes).Text;
                        sVCasoTituloespanol = fun_dicss.validareader("CasoTituloespanol", "CasoTituloespanol", resp_getidspatentes).Text;
                        sVCasoTituloingles = fun_dicss.validareader("CasoTituloingles", "CasoTituloingles", resp_getidspatentes).Text;
                        sVIdiomaId = fun_dicss.validareader("IdiomaId", "IdiomaId", resp_getidspatentes).Text;
                        sVCasoFechaConcesion = fun_dicss.validareader("CasoFechaConcesion", "CasoFechaConcesion", resp_getidspatentes).Text;
                        sVCasoFechaRecepcion = fun_dicss.validareader("CasoFechaRecepcion", "CasoFechaRecepcion", resp_getidspatentes).Text;
                        sVCasoFechaVigencia = fun_dicss.validareader("CasoFechaVigencia", "CasoFechaVigencia", resp_getidspatentes).Text;
                        sVCasoFechaPublicacionSolicitud = fun_dicss.validareader("CasoFechaPublicacionSolicitud", "CasoFechaPublicacionSolicitud", resp_getidspatentes).Text;
                        sVCasoFechaLegal = fun_dicss.validareader("CasoFechaLegal", "CasoFechaLegal", resp_getidspatentes).Text;
                        sVCasoNumConcedida = fun_dicss.validareader("CasoNumConcedida", "CasoNumConcedida", resp_getidspatentes).Text;
                        sVCasoNumeroExpedienteLargo = fun_dicss.validareader("CasoNumeroExpedienteLargo", "CasoNumeroExpedienteLargo", resp_getidspatentes).Text;
                        sVCasoNumero = fun_dicss.validareader("CasoNumero", "CasoNumero", resp_getidspatentes).Text;
                        sVResponsableId = fun_dicss.validareader("ResponsableId", "ResponsableId", resp_getidspatentes).Text;
                        sVCasoTipoCaptura = fun_dicss.validareader("CasoTipoCaptura", "CasoTipoCaptura", resp_getidspatentes).Text;
                        sVCasoTitular = fun_dicss.validareader("CasoTitular", "CasoTitular", resp_getidspatentes).Text;
                        sVEstatusCasoId = fun_dicss.validareader("EstatusCasoId", "EstatusCasoId", resp_getidspatentes).Text;
                        sVUsuarioId = fun_dicss.validareader("UsuarioId", "UsuarioId", resp_getidspatentes).Text;
                        sVAreaImpiId = fun_dicss.validareader("AreaImpiId", "AreaImpiId", resp_getidspatentes).Text;
                        sVCasoFechaInternacional = fun_dicss.validareader("CasoFechaInternacional", "CasoFechaInternacional", resp_getidspatentes).Text;
                        sVPaisId = fun_dicss.validareader("PaisId", "PaisId", resp_getidspatentes).Text;
                        sVCasoFechaPruebaUsoSig = fun_dicss.validareader("CasoFechaPruebaUsoSig", "CasoFechaPruebaUsoSig", resp_getidspatentes).Text;
                        sVCasoFechaFilingCliente = fun_dicss.validareader("CasoFechaFilingCliente", "CasoFechaFilingCliente", resp_getidspatentes).Text;
                        sVCasoFechaFilingSistema = fun_dicss.validareader("CasoFechaFilingSistema", "CasoFechaFilingSistema", resp_getidspatentes).Text;
                        sVCasoFechaDivulgacionPrevia = fun_dicss.validareader("CasoFechaDivulgacionPrevia", "CasoFechaDivulgacionPrevia", resp_getidspatentes).Text;
                        sVCasoFechaCartaCliente = fun_dicss.validareader("CasoFechaCartaCliente", "CasoFechaCartaCliente", resp_getidspatentes).Text;
                        sVDivicionalid = fun_dicss.validareader("Divicionalid", "Divicionalid", resp_getidspatentes).Text;
                    }
                    resp_getidspatentes.Close();
                    con_2.Cerrarconexion();

                    //tNumservicio = "";
                    //textIdioma = "";
                    //textFechacarta = "";
                    //textPlazolegal = "";
                    //textCapitulo = "";
                    //textClientduedate = "";
                    //textSubtipo = "";
                    //textTipo = "";
                    //textPublicacion = "";
                    //textCasodiseno = "";
                    //textEstatus = "";
                    //textFechainter = "";
                    //textVigencia = "";
                    //textDivulgacion = "";
                    //textConcecion = "";
                    //textRegimpi = "";
                    //textNumreg = "";
                    //textDenom = "";
                    //textBox9 = "";
                    //textBox8 = "";
                    //textBox7 = "";
                    //textBox6 = "";
                    //textTitulo = "";
                    //textNumexp = "";
                    //textReferencia = "";
                    //textNumref = "";
                    //tNcliente = "";
                    //tNumCaso = "";
                }
                else {
                    MessageBox.Show("Debe seleccionar un grupo y agregar un número de caso.");
                }
            }catch(Exception Ex){
                MessageBox.Show("Ocurrio un error al cargar la información del caso: ");
            }
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public String get_tablaconsulta(String grupo){
            String sResult = "";
            switch (grupo)
            {
                case "Patentes":{
                    sResult = "caso_patente";
                }break;
                case "Marcas":{
                    sResult = "caso_marcas";
                }break;
                case "Contencioso":{
                    sResult = "caso_contencioso";
                }break;
                case "Consulta":{
                    sResult = "";
                }break;
                case "Oposicion a solicitudes":{
                    sResult = "caso_oposicion";
                }break;
                case "Variedades vegetales":{
                    sResult = "";
                }break;
                case "Derechos de autor":{
                    sResult = "";
                }break;
                case "Reserva de derechos":{
                    sResult = "caso_reservadederechos";
                }break;
            }
            return sResult;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try {
                /*Botón para agrear servicios en la factura*/
                /*Debemos llenar el dGview_servicios de ls servicios de la factura*/
                String sNumservicio = tNumservicio.Text;
                String sServicioSAT = "";
                String sNombreservicio = comboServicio.Text;
                String sHonorarios = tb_Honorarios_.Text;
                String sCantidad = tbMonedacantidad.Text;
                String sDerechos = tb_derechos.Text;
                if (sHonorarios == "" && sDerechos=="")
                {
                    MessageBox.Show("Debe ingresar un monto correcto.");
                    return;
                }

                float fHonorarios = float.Parse(sHonorarios.Replace(",", ""));
                //float hCantidad = float.Parse(sCantidad.Replace(",", ""));
                float hDerechos = float.Parse(sDerechos.Replace(",", ""));

                /*Operación correcta debe ir aqui*/
                float total = fHonorarios + hDerechos;
                obj_factura_concepto obj_conceptos = new obj_factura_concepto(sNombreservicio, sHonorarios, sDerechos);
                conceptosfac.Add(obj_conceptos);
                dGview_servicios.Rows.Add(sNumservicio, sServicioSAT, sNombreservicio, sHonorarios, "", sDerechos, sCantidad);
            }catch(Exception ex){
                MessageBox.Show("Los montos son incorrectos.");
            }
            /*Servicio*/
        }

        private void tNumservicio_TextChanged(object sender, EventArgs e)
        {
            try {
                int iInice = Int32.Parse(tNumservicio.Text);
                comboServicio.Text = cbServicios[iInice];//"Submitting  a PCT patent application Chapter I, to enter national phase in Mexico Solicitud de patente PCT, para Fase Nacional en México, Capítulo I";// num;

                if (radioButton1.Checked)
                {
                    tb_Honorarios_.Text = honorarios_dolares[iInice];
                    tb_derechos.Text = derechos_dolares[iInice];
                }

                if (radioButton2.Checked)
                {
                    tb_Honorarios_.Text = honorarios_euros[iInice];
                    tb_derechos.Text = derechos_euros[iInice];
                }

                if (radioButton3.Checked)//pesos
                {
                    tb_Honorarios_.Text = honorarios_pesos[iInice];
                    tb_derechos.Text = derechos_pesos[iInice];
                }
                //calculaivasubtotaltotal();
            }catch(Exception E){
                comboServicio.Text = "";
                tb_Honorarios_.Text = "0";
                tb_derechos.Text = "0";
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                tb_Honorarios_.Enabled = true;
            }else{
                tb_Honorarios_.Enabled = false;
            }
            

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                tb_derechos.Enabled = true;
            }
            else
            {
                tb_derechos.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*Cerramos ésta ventana y activamos la antye*/
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            /*Generamos el PDF con los datos cargados*/
            if (tNumCaso.Text == "" || cbGrupo.Text == "" || tNcliente.Text == "" || dGview_servicios.RowCount == 0 )
            {//validamos que los datos necesarios estén llenos para ejecutar la mínima 
                MessageBox.Show("Faltan datos para generar la factura, revise e intente de nuevo."); 
                return;
            }
            
            String clienteid = tNcliente.Text;
            String sNombrecliente = cClientebox.Text;
            String sNumerocaso = tNumCaso.Text;
            String sReferenciacliente = textReferencia.Text;
            String sNumerservicio = tNumservicio.Text;
            String sServicioconcepto = comboServicio.Text;
            String Direccionuno = "";
            String Direcciondos = "";
            String Direccion_tres = "";
            String Direccion_cuatro = "";
            String RFC_cliente = "";

            /*datos del cliente a facturar*/
            if (rb_fac_cliente.Checked)//es cliente
            {
                conect con_direccioncliente = new conect();
                String sQuerycliente = " SELECT " +
                                        "     cliente.IdiomaId," +
                                        "     idioma.IdiomaDescripcion," +
                                        "     cliente.ClienteRFC," +
                                        "     direccion.*," +
                                        "     pais.PaisNombre," +
                                        "     pais.PaisNombreIngles" +
                                        " FROM" +
                                        "     direccion," +
                                        "     cliente," +
                                        "     pais," +
                                        "     idioma" +
                                        " WHERE" +
                                        "     direccion.clienteid = cliente.clienteid" +
                                        "         AND direccion.clienteid =" + tNcliente.Text +
                                        "         and cliente.IdiomaId = idioma.IdiomaId" +
                                        "         AND direccion.PaisId = pais.PaisId;";
                //String sQuerycliente = "select cliente.ClienteRFC, direccion.* from direccion, cliente where direccion.clienteid = cliente.clienteid  and direccion.clienteid = " + oFactura.cliente_id + ";";
                MySqlDataReader resp_direccion = con_direccioncliente.getdatareader(sQuerycliente);
                while (resp_direccion.Read())
                {
                    Direccionuno = fun_dicss.validareader("DireccionCalle", "clienteid", resp_direccion).Text + " " + fun_dicss.validareader("DireccionNumExt", "clienteid", resp_direccion).Text + " " + fun_dicss.validareader("DireccionNumInt", "clienteid", resp_direccion).Text;
                    Direcciondos = fun_dicss.validareader("DireccionColonia", "clienteid", resp_direccion).Text;
                    Direccion_tres = fun_dicss.validareader("DireccionCP", "clienteid", resp_direccion).Text + " " + fun_dicss.validareader("DireccionEstado", "clienteid", resp_direccion).Text;
                    RFC_cliente = fun_dicss.validareader("ClienteRFC", "clienteid", resp_direccion).Text;
                    if (fun_dicss.validareader("IdiomaId", "clienteid", resp_direccion).Text == "1")
                    {//Si es inglés
                        Direccion_cuatro = fun_dicss.validareader("PaisNombreIngles", "clienteid", resp_direccion).Text;
                    }
                    else
                    {
                        Direccion_cuatro = fun_dicss.validareader("PaisNombre", "clienteid", resp_direccion).Text;
                    }

                    if (Direcciondos == "")
                    {
                        Direcciondos = fun_dicss.validareader("DireccionCP", "clienteid", resp_direccion).Text + " " + fun_dicss.validareader("DireccionEstado", "clienteid", resp_direccion).Text;
                        if (fun_dicss.validareader("IdiomaId", "clienteid", resp_direccion).Text == "1")
                        {//Si es inglés
                            Direccion_tres = fun_dicss.validareader("PaisNombreIngles", "clienteid", resp_direccion).Text;
                        }
                        else
                        {
                            Direccion_tres = fun_dicss.validareader("PaisNombre", "clienteid", resp_direccion).Text;
                        }
                        Direccion_cuatro = "";
                    }

                }
                resp_direccion.Close();
                con_direccioncliente.Cerrarconexion();

                /**/
                /*clienteid = tNcliente.Text;
                sNombrecliente = cClientebox.Text;
                sNumerocaso = tNumCaso.Text;
                sReferenciacliente = textReferencia.Text;
                sNumerservicio = tNumservicio.Text;
                sServicioconcepto = comboServicio.Text;*/
            }else {
                /* Consultamos la información del interesado seleccionado */
                conect con_iunteresado = new conect();
                //String squery_interesado = "select * from interesado where InteresadoID = " + (cbInteresados.SelectedItem as ComboboxItem).Value.ToString() + ";";
                String squery_interesado = " SELECT " +
                                            "     *" +
                                            " FROM" +
                                            "     interesado," +
                                            "     direccion, pais" +
                                            " WHERE" +
                                            "    interesado.InteresadoID = "+ (cbInteresados.SelectedItem as ComboboxItem).Value.ToString() +" "+
                                            "    AND direccion.InteresadoId = interesado.InteresadoID"+
                                            "    AND interesado.PaisId = pais.PaisId";

                MySqlDataReader resp_getinteresado = con_iunteresado.getdatareader(squery_interesado);
                while (resp_getinteresado.Read())
                {
                    clienteid = "I-" + fun_dicss.validareader("InteresadoID", "InteresadoID", resp_getinteresado).Text;
                    sNombrecliente = fun_dicss.validareader("InteresadoNombre", "InteresadoID", resp_getinteresado).Text + "" + fun_dicss.validareader("InteresadoApPaterno", "InteresadoID", resp_getinteresado).Text + "" + fun_dicss.validareader("InteresadoApMaterno", "InteresadoID", resp_getinteresado).Text;
                    Direccionuno = fun_dicss.validareader("DireccionCalle", "InteresadoID", resp_getinteresado).Text + " " + fun_dicss.validareader("DireccionNumExt", "InteresadoID", resp_getinteresado).Text + " " + fun_dicss.validareader("DireccionNumInt", "InteresadoID", resp_getinteresado).Text;
                    Direcciondos = fun_dicss.validareader("DireccionColonia", "InteresadoID", resp_getinteresado).Text;
                    Direccion_tres = fun_dicss.validareader("DireccionCP", "InteresadoID", resp_getinteresado).Text + " " + fun_dicss.validareader("DireccionEstado", "InteresadoID", resp_getinteresado).Text;
                    RFC_cliente = fun_dicss.validareader("InteresadoRFC", "InteresadoID", resp_getinteresado).Text;
                    if (fun_dicss.validareader("IdiomaId", "InteresadoID", resp_getinteresado).Text == "1")
                    {//Si es inglés
                        Direccion_cuatro = fun_dicss.validareader("PaisNombreIngles", "InteresadoID", resp_getinteresado).Text;
                    }
                    else
                    {
                        Direccion_cuatro = fun_dicss.validareader("PaisNombre", "InteresadoID", resp_getinteresado).Text;
                    }
                    if (Direcciondos == "")
                    {
                        Direcciondos = fun_dicss.validareader("DireccionCP", "InteresadoID", resp_getinteresado).Text + " " + fun_dicss.validareader("DireccionEstado", "InteresadoID", resp_getinteresado).Text;
                        if (fun_dicss.validareader("IdiomaId", "InteresadoID", resp_getinteresado).Text == "1")
                        {//Si es inglés
                            Direccion_tres = fun_dicss.validareader("PaisNombreIngles", "InteresadoID", resp_getinteresado).Text;
                        }
                        else
                        {
                            Direccion_tres = fun_dicss.validareader("PaisNombre", "InteresadoID", resp_getinteresado).Text;
                        }
                        Direccion_cuatro = "";
                    }
                }
                resp_getinteresado.Close();
                con_iunteresado.Cerrarconexion();
            }
            obj_factura_pdf obj_pdf = new obj_factura_pdf(sNombrecliente, "1001", clienteid, "01", "150", conceptosfac);
            obj_pdf.referenciacliente = sReferenciacliente;
            obj_pdf.NuestraRef = textNumref.Text;
            obj_pdf.Direccionuno = Direccionuno;
            obj_pdf.Direcciondos = Direcciondos;
            obj_pdf.Direccion_tres = Direccion_tres;
            obj_pdf.Direccion_cuatro = Direccion_cuatro;
            obj_pdf.RFC_cliente = RFC_cliente;
            
            //obj_factura_concepto obj_conceptos = new obj_factura_concepto("concepto uno", "concepto descripcion ", "1000.00");
            //obj_factura_concepto obj_conceptos2 = new obj_factura_concepto("concepto dos", "concepto descripcion dos", "1450.65");
            //obj_factura_concepto obj_conceptos3 = new obj_factura_concepto("concepto dos", "concepto descripcion ", "450.00");
            //obj_factura_concepto obj_conceptos4 = new obj_factura_concepto("concepto dos", "concepto descripcion dos", "150.11");
            //obj_factura_concepto obj_conceptos5 = new obj_factura_concepto("concepto dos", "concepto descripcion dos", "10.00");
            //List<obj_factura_concepto> conceptosfac = new List<obj_factura_concepto>();
            //conceptosfac.Add(obj_conceptos);
            //conceptosfac.Add(obj_conceptos2);
            //conceptosfac.Add(obj_conceptos3);
            //conceptosfac.Add(obj_conceptos4);
            //conceptosfac.Add(obj_conceptos5);
            
            plantillafactura ejecut_tmp = new plantillafactura(obj_pdf);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)//Dolares
        {
            
            if (radioButton1.Checked)
            {
                label_moneda.Text = "(+16%) Dólares: $";
            }

            if (radioButton2.Checked)
            {
                label_moneda.Text = "(+16%) Euros: €";
            }

            if (radioButton3.Checked)
            {
                label_moneda.Text = "(+16%) Pesos: $";
            }
            recalculatarifa();

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)//Euros
        {
            if (radioButton1.Checked)
            {
                label_moneda.Text = "(+16%) Dólares: $";
            }

            if (radioButton2.Checked)
            {
                label_moneda.Text = "(+16%) Euros: €";
            }

            if (radioButton3.Checked)
            {
                label_moneda.Text = "(+16%) Pesos: $";
            }
            recalculatarifa();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)//Pesos
        {

            if (radioButton1.Checked)
            {
                label_moneda.Text = "(+16%) Dólares: $";
            }

            if (radioButton2.Checked)
            {
                label_moneda.Text = "(+16%) Euros: €";
            }

            if (radioButton3.Checked)
            {
                label_moneda.Text = "(+16%) Pesos: $";
            }
            recalculatarifa();
        }

        public void recalculatarifa(){
            String sNumtarifa  = tNumservicio.Text;
            tNumservicio.Text = "";
            tNumservicio.Text = sNumtarifa;
        }

        public  void calculaivasubtotaltotal(){
            try{
                /*tb_Honorarios_.Text;
                float tb_derechos.Text;*/
                float fDerechos  = float.Parse(tb_derechos.Text, CultureInfo.InvariantCulture.NumberFormat);
                float fHonorarios = float.Parse(tb_Honorarios_.Text, CultureInfo.InvariantCulture.NumberFormat);
                double fIvaderechos = fDerechos * 0.16;
                double fIvaHonorarios = fHonorarios * 0.16;

                tbIvahono.Text = fIvaHonorarios + "";
                tbIvaderechos.Text = fIvaderechos + "";

                double fIva = (fDerechos + fHonorarios) * 0.16;
                float sSubTotal = (fDerechos + fHonorarios);
                double sTotal = fIva + sSubTotal;
                tbMonedacantidad.Text = fIva + "";
                tb_subtotal.Text = sSubTotal + "";
                tb_total.Text = sTotal + "";
            }catch(Exception Ex){
                tb_derechos.Text = "0";
                tb_Honorarios_.Text = "0";
                //MessageBox.Show("Revise los montos ingresados.");
            }
            
        }
        private void comboServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {/*validar si est+a seleccionado*/
                if (comboServicio.SelectedItem == null) {
                    return;
                }

                String sId_concepto = (comboServicio.SelectedItem as ComboboxItem).Value.ToString();//Es el número de servicio
                int iInice = Int32.Parse(sId_concepto);
                tNumservicio.Text = iInice + "";
                if (sId_concepto =="")
                {
                    MessageBox.Show("Debe seleccionar un servicio para generar nota de remición o factura.");
                }
                String sIbbaseking = "";
                String sTablerelacion = "";
                /*Validamos el caso */
                    conect con_count_tarifa_caso = new conect();
                    String sQuerytarifascount_caso = "SELECT tarifas_base_king.*,  count(*) as count FROM tarifas_base_king where id_baseking = " + conFormcaso.gSCasoId + " and tabla_relacion = 'caso_patente';";
                    MySqlDataReader resp_get_tarifadatoscount_caso = con_count_tarifa_caso.getdatareader(sQuerytarifascount_caso);
                    resp_get_tarifadatoscount_caso.Read();
                    String sCounttarifa_caso = fun_dicss.validareader("count", "count", resp_get_tarifadatoscount_caso).Text;
                    if (Int16.Parse(sCounttarifa_caso) >0)//consultamos si el cliente tiene alguna tarifa
                    {
                        sIbbaseking = fun_dicss.validareader("id_baseking", "id_baseking", resp_get_tarifadatoscount_caso).Text;
                        sTablerelacion = fun_dicss.validareader("tabla_relacion", "tabla_relacion", resp_get_tarifadatoscount_caso).Text;
                    }else{
                        /*Validamos el Interesado */
                        
                        String sCounttarifa_interesado="0";
                        if (cbInteresados.SelectedItem != null)//validamos si hay un interesado
                        {
                            conect con_count_tarifa_interesado = new conect();
                            String sIdinteresado = (cbInteresados.SelectedItem as ComboboxItem).Value.ToString();
                            String sQuerytarifascount_interesado = "SELECT tarifas_base_king.*,  count(*) as count FROM tarifas_base_king where id_baseking = " + sIdinteresado + " and tabla_relacion = 'interesado';";
                            MySqlDataReader resp_get_tarifadatoscount_interesado = con_count_tarifa_interesado.getdatareader(sQuerytarifascount_interesado);
                            resp_get_tarifadatoscount_interesado.Read();
                            sCounttarifa_interesado = fun_dicss.validareader("count", "count", resp_get_tarifadatoscount_interesado).Text;
                            sIbbaseking = fun_dicss.validareader("id_baseking", "id_baseking", resp_get_tarifadatoscount_interesado).Text;
                            sTablerelacion = fun_dicss.validareader("tabla_relacion", "tabla_relacion", resp_get_tarifadatoscount_interesado).Text;
                            resp_get_tarifadatoscount_interesado.Close();
                            con_count_tarifa_interesado.Cerrarconexion();
                            if (Int16.Parse(sCounttarifa_interesado)==0)
                            {
                                sIbbaseking = "0";
                                sTablerelacion = "cliente";
                            }
                        }
                        else {
                            /*validamos si existe la tarifa para el cliente*/
                            String sCounttarifa = "0";
                            conect con_count_tarifa = new conect();
                            String sQuerytarifascount = "SELECT tarifas_base_king.*,  count(*) as count FROM tarifas_base_king where id_baseking = " + tNcliente.Text + " and tabla_relacion = 'cliente';";
                            MySqlDataReader resp_get_tarifadatoscount = con_count_tarifa.getdatareader(sQuerytarifascount);
                            resp_get_tarifadatoscount.Read();
                            sCounttarifa = fun_dicss.validareader("count", "count", resp_get_tarifadatoscount).Text;
                            if (Int16.Parse( sCounttarifa) > 0)//consultamos si el cliente tiene alguna tarifa
                            {
                                sIbbaseking = fun_dicss.validareader("id_baseking", "id_baseking", resp_get_tarifadatoscount).Text;
                                sTablerelacion = fun_dicss.validareader("tabla_relacion", "tabla_relacion", resp_get_tarifadatoscount).Text;
                            }else{//Si no hay tarifa para ninguyno de los tres entonces damos una tarifa estandar con el cliente 0
                                sIbbaseking = "0";
                                sTablerelacion = "cliente";
                            }
                            resp_get_tarifadatoscount.Close();
                            con_count_tarifa.Cerrarconexion();
                        }
                    }
                    resp_get_tarifadatoscount_caso.Close();
                    con_count_tarifa_caso.Cerrarconexion();

                /*Buscamos la tarifa con el id del cliente el id concepto y el id cliente interesado u caso*/
                conect con_2 = new conect();
                String sQuerytarifas = " SELECT  " +
                                        "     * " +
                                        " FROM " +
                                        "     tarifas_base_king " +
                                        " WHERE " +
                                        "     id_concepto = " + sId_concepto +
                                        " AND id_baseking = " + sIbbaseking +
                                        " AND tabla_relacion = '" + sTablerelacion + "'";
                MySqlDataReader resp_get_tarifadatos = con_2.getdatareader(sQuerytarifas);
                while (resp_get_tarifadatos.Read())
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Value = fun_dicss.validareader("id_concepto", "id_concepto", resp_get_tarifadatos).Value;
                    item.Text = fun_dicss.validareader("id_concepto", "id_concepto", resp_get_tarifadatos).Value + " - " + fun_dicss.validareader("id_concepto", "id_concepto", resp_get_tarifadatos).Text;
                    int indice = Int32.Parse(fun_dicss.validareader("id_concepto", "id_concepto", resp_get_tarifadatos).Value.ToString());
                    //cbServicios[indice] = fun_dicss.validareader("id_concepto", "id_concepto", resp_get_tarifadatos).Text;


                    honorarios_dolares[indice] = fun_dicss.validareader("honorarios dolares", "id_concepto", resp_get_tarifadatos).Text;
                    derechos_dolares[indice] = fun_dicss.validareader("derechos dolares", "id_concepto", resp_get_tarifadatos).Text;

                    honorarios_euros[indice] = fun_dicss.validareader("honorarios euros", "id_concepto", resp_get_tarifadatos).Text;
                    derechos_euros[indice] = fun_dicss.validareader("derechos euros", "id_concepto", resp_get_tarifadatos).Text;

                    honorarios_pesos[indice] = fun_dicss.validareader("honorarios pesos", "id_concepto", resp_get_tarifadatos).Text;
                    derechos_pesos[indice] = fun_dicss.validareader("derechos pesos", "id_concepto", resp_get_tarifadatos).Text;


                    //comboServicio.Items.Add(item);
                }
                resp_get_tarifadatos.Close();
                con_2.Cerrarconexion();
                /*Buscamos el número de servicio en las tarifas filtrando al cliente*/
                //int iInice = Int32.Parse(sId_concepto);
                ////comboServicio.Text = cbServicios[iInice];//"Submitting  a PCT patent application Chapter I, to enter national phase in Mexico Solicitud de patente PCT, para Fase Nacional en México, Capítulo I";// num;
                //tNumservicio.Text = iInice + "";

                //if (radioButton1.Checked)
                //{
                //    tb_Honorarios_.Text = honorarios_dolares[iInice];
                //    tb_derechos.Text = derechos_dolares[iInice];
                //}

                //if (radioButton2.Checked)
                //{
                //    tb_Honorarios_.Text = honorarios_euros[iInice];
                //    tb_derechos.Text = derechos_euros[iInice];
                //}

                //if (radioButton3.Checked)//pesos
                //{
                //    tb_Honorarios_.Text = honorarios_pesos[iInice];
                //    tb_derechos.Text = derechos_pesos[iInice];
                //}

                /*calculamos el total*/
                /*calculaivasubtotaltotal();*/
            }
            catch (Exception E)
            {
                comboServicio.Text = "";
            }
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void tb_derechos_TextChanged(object sender, EventArgs e)
        {
            calculaivasubtotaltotal();
        }

        private void tb_Honorarios__TextChanged(object sender, EventArgs e)
        {
            calculaivasubtotaltotal();
        }

        private void rb_fac_cliente_CheckedChanged(object sender, EventArgs e)
        {
            //rb_fac_cliente
            //rb_fac_interesado
            if (rb_fac_cliente.Checked)
            {
                tNcliente.Enabled = true;
                cClientebox.Enabled = true;
                cbInteresados.Enabled = false;
                cbInteresados.Text = "";
            }else {
                tNcliente.Enabled = false;
                cClientebox.Enabled = false;
                cbInteresados.Enabled = true;
            }
        }

        
    }
}
