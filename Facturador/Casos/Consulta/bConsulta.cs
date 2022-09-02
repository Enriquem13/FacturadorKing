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
using Excel = Microsoft.Office.Interop.Excel;
//20220511 Librerias Faltantes
using SpreadsheetLight;
using System.Diagnostics;

namespace Facturador
{
    public partial class bConsulta : Form
    {
        public captura captura;
        public Form1 fLoguin;
        public String sGTipocaso;
        public String[] paises = new String[250];
        public String[] paisesclave = new String[250];
        public bConsulta(int iGrupo, captura capturaform, Form1 loguin)
        {
            fLoguin = loguin;
            captura = capturaform;
            sGTipocaso = iGrupo + "";

            InitializeComponent();
            conect con = new conect();
           

            String sQuerytipoSol = "select * from tiposolicitud where TipoSolicitudGrupo= " + iGrupo;
            MySqlDataReader respuestastringtoiposl = con.getdatareader(sQuerytipoSol);
            while (respuestastringtoiposl.Read())
            {
                cbTiposolicitud.Items.Add(validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestastringtoiposl));
                
            }

            cbTiposolicitud.Text = "Consulta";
            respuestastringtoiposl.Close();


            String sQuerySubTipo = "SELECT TipoConsultaId, tipoconsultacolDescripEsp FROM tipoconsulta ;";
            MySqlDataReader respuestastringsubtipo = con.getdatareader(sQuerySubTipo);
            while (respuestastringsubtipo.Read())
            {
                cbSubTipoSol.Items.Add(validareader("tipoconsultacolDescripEsp", "TipoConsultaId", respuestastringsubtipo));
            }
            respuestastringsubtipo.Close();



            //20220427FSV Cargamos Estatus del caso para Filtrar
            conect con_estatus = new conect();
            String sGetids = "SELECT " +
                                "    estatuscaso.*, " +
                                " grupoid " +
                                " FROM " +
                                "    grupoestatuscaso, " +
                                "    estatuscaso " +
                                " WHERE " +
                                "    grupoestatuscaso.EstatusCasoId = estatuscaso.EstatusCasoId " +
                                "        AND grupoid = " + sGTipocaso +
                                " GROUP BY estatuscaso.EstatusCasoId" +
                                " order by estatuscaso.EstatusCasoDescrip;";

            MySqlDataReader resp_getestatus = con.getdatareader(sGetids);
            while (resp_getestatus.Read())
            {
                cbFiltroestatus.Items.Add(validareader("EstatusCasoDescrip", "estatuscasoid", resp_getestatus));
            }
            resp_getestatus.Close();
            //20220427FSV


            //select * from pais;
            //20220408FSV Ordenar por nombre de pais
            //String query5 = "select PaisNombre, PaisId, PaisClave from pais;";
            String query5 = "select PaisNombre, PaisId, PaisClave from pais Order By PaisNombre;";
            //20220408FSV Fin de modificacion
            MySqlDataReader respuestastringopais = con.getdatareader(query5);
            //int paisint = 0;
            while (respuestastringopais.Read())
            {
                cbPais.Items.Add(validareader("PaisNombre", "PaisId", respuestastringopais));
                paises[Convert.ToInt32(validareader("PaisClave", "PaisId", respuestastringopais).Value.ToString())] = validareader("PaisClave", "PaisId", respuestastringopais).Text;
                paisesclave[Convert.ToInt32(validareader("PaisClave", "PaisId", respuestastringopais).Value.ToString())] = validareader("PaisNombre", "PaisId", respuestastringopais).Text;

                //paisint++;
            }


            //cbHolder
            conect con_holder = new conect();
            String query_holder = "select * from holder order by HolderNombre;";
            MySqlDataReader respuestastringo_holder = con_holder.getdatareader(query_holder);
            while (respuestastringo_holder.Read())
            {
                ComboboxItem combopias_hold = new ComboboxItem();
                combopias_hold.Text = validareader("HolderId", "HolderId", respuestastringo_holder).Text + " - " + validareader("HolderNombre", "HolderNombre", respuestastringo_holder).Text;
                combopias_hold.Value = validareader("HolderNombre", "HolderNombre", respuestastringo_holder).Value;
                cbHolder.Items.Add(combopias_hold);
            }
            respuestastringo_holder.Close();
            con_holder.Cerrarconexion();
            //FIN HOLDER



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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            captura.Close();
            fLoguin.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            captura.Show();
            this.Close();
        }

        private void cbPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iValuepais = Convert.ToInt32((cbPais.SelectedItem as ComboboxItem).Value.ToString());
            tbClave.Text = paises[iValuepais];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conect con = new conect();
            dgViewBuscaConsultas.Rows.Clear();
            int rowcolor = 0;
            try
                {
                String sQuerywhere = "";
                String sQuerywherecaso = "";
                
                //20220330FSV Inicia Filtros
                if (cbTiposolicitud.SelectedItem != null){
                    sQuerywhere += " AND CCO.tiposolicitudid = " + (cbTiposolicitud.SelectedItem as ComboboxItem).Value;}

                if (cbSubTipoSol.SelectedItem != null){
                    sQuerywhere += " AND CCO.SubTipoSolicitudId = " + (cbSubTipoSol.SelectedItem as ComboboxItem).Value;}

                if (!string.IsNullOrEmpty(textBoxCasonumero.Text)){
                    sQuerywhere += " AND CCO.CasoNumero like '%" + textBoxCasonumero.Text + "%'";}

                if (!string.IsNullOrEmpty(tbexpediente.Text)){
                    sQuerywhere += " AND CCO.CasoNumeroExpedienteLargo like '%" + tbexpediente.Text + "%'";}

                if (!string.IsNullOrEmpty(tbDenominacion.Text)){
                    sQuerywhere += " AND ( CCO.CasoTituloingles like '%" + tbDenominacion.Text + "%' OR CCO.CasoTituloespanol LIKE '%" + tbDenominacion.Text + "%')";}

                if (!string.IsNullOrEmpty(tbMotivo.Text)){
                    sQuerywhere += " AND CCO.CasoMotivo like '%" + tbMotivo.Text + "%'";}

                if (!string.IsNullOrEmpty(tbCasoid.Text)){
                    sQuerywhere += " AND CCO.CasoId like '%" + tbCasoid.Text + "%'";}

                if (cbPais.SelectedItem != null){
                    sQuerywhere += " AND CCO.PaisId = '" + (cbPais.SelectedItem as ComboboxItem).Value + "'";}

                if (!string.IsNullOrEmpty(tbCliente.Text)){
                    sQuerywhere += " AND GET_CLIENTE(CCO.CasoId) like '%" + tbCliente.Text + "%'";}

                if (!string.IsNullOrEmpty(tbreferencia.Text)){
                    sQuerywhere += " AND Get_Referencia(CCO.Casoid, CCO.TipoSolicitudId) like '%" + tbreferencia.Text + "%'";}

                if (!string.IsNullOrEmpty(tbInteresado.Text)){
                    sQuerywhere += " AND GET_INTERESADOS_TIPOSOL(CCO.Casoid, CCO.TipoSolicitudId) like '%" + tbInteresado.Text + "%'";}

                if (cbHolder.SelectedItem != null){
                    sQuerywhere += " AND HD.HolderNombre = '" + (cbHolder.SelectedItem as ComboboxItem).Value + "'";}


                if (sQuerywhere != "" || sQuerywherecaso != "")
                {
                    sQuerywhere = sQuerywhere.Substring(4, sQuerywhere.Length - 4);
                    String sQuerybusqueda = "";
                    sQuerybusqueda = " SELECT DISTINCT  " +
                                " PS.PaisClave AS PaisClave, " +
                                " CCO.CasoId as CasoId, " +
                                " CCO.CasoNumero as CasoNumero, " +
                                " TS.TipoSolicitudDescrip as TipoSolicitudDescrip, " +
                                " SUB.SubtipoSolicitudDescripcion as SubtipoSolicitudDescripcion, " +
                                " DameEstatusCasoDescrip(CCO.EstatusCasoId) As Estatuscasodescrip, " +
                                " DATE_FORMAT(CCO.CasoFechaRecepcion, ' %d-%m-%Y') as CasoFechaRecepcion, " +
                                " DATE_FORMAT(CCO.CasoFechaPresentacion, ' %d-%m-%Y') as CasoFechaPresentacion, " +
                                " CCO.CasoNumeroExpedienteLargo as CasoNumeroExpedienteLargo, " +
                                " DATE_FORMAT(CCO.CasoFechaConcesion, ' %d-%m-%Y') as CasoFechaConcesion, " +
                                " DATE_FORMAT(CCO.CasoFechaResolucion, '%d-%m-%Y') as CasoFechaResolucion, " +
                                " CCO.CasoNumConcedida as CasoNumConcedida, " +
                                " CCO.CasoMotivo as CasoMotivo, " +
                                " CCO.CasoObservaciones as CasoObservaciones, " +
                                " it.InteresadoNombre as InteresadoNombre, " +
                                " CCO.CasoTituloespanol as CasoTituloespanol, " +
                                //20220531FSV Discriminar por tipo de solicitud
                                //" Get_Cliente(CCO.CasoId) as NombreUtilClient, " +
                                " Get_Cliente_tiposol(CCO.CasoId,CCO.TipoSolicitudId) as NombreUtilClient, " +
                                //20220531FSV Fin de modificación
                                " PR.PrioridadNumero as PrioridadNUmero, " +
                                " RF.ReferenciaNombre as ReferenciaNombre, " +
                                " HD.HolderNombre as HolderNombre" +
                                " FROM caso_consulta CCO LEFT JOIN pais PS ON CCO.PaisID = PS.PaisID " +
                                " LEFT JOIN tiposolicitud TS ON CCO.tiposolicitudId = TS.tiposolicitudID " +
                                " LEFT JOIN subtiposolicitud SUB ON CCO.SubTipoSolicitudId = SUB.SubTipoSolicitudId  " +
                                " LEFT JOIN prioridad PR ON CCO.CasoID = PR.CasoID AND CCO.tiposolicitudid= PR.tiposolicitudID " +
                                " LEFT JOIN referencia RF ON CCO.CasoID = RF.CasoID AND CCO.tiposolicitudid= RF.tiposolicitudID " +
                                " LEFT JOIN casointeresado CI ON CCO.CasoID = CI.CasoID AND CCO.tiposolicitudid= CI.tiposolicitudID " +
                                " LEFT JOIN interesado IT ON CI.InteresadoID = IT.InteresadoID " +
                                " LEFT JOIN holder HD ON IT.HolderID = HD.HolderId " +
                                " WHERE " +
                                sQuerywhere;

                    MySqlDataReader respuestastring3 = con.getdatareader(sQuerybusqueda);
                    while (respuestastring3.Read())
                    {

                        String sCasoidconsulta = validareader("CasoId", "CasoId", respuestastring3).Text;
                        int residuo = rowcolor % 2;


                        //DateTime dFechapresentacion = DateTime.MinValue;
                        DateTime? dFechapresentacion = null;
                        try
                        {
                            if (validareader("CasoFechaPresentacion", "CasoId", respuestastring3).Text != "00-00-0000")
                            {
                                String fecha = validareader("CasoFechaPresentacion", "CasoId", respuestastring3).Text;
                                dFechapresentacion = Convert.ToDateTime(fecha);
                            }
                        }
                        catch (Exception)
                        {
                        }

                        //DateTime dFechapresentacion = DateTime.MinValue;
                        DateTime? dFechaconclusion = null;
                        try
                        {
                            if (validareader("CasoFechaResolucion", "CasoId", respuestastring3).Text != "00-00-0000")
                            {
                                String sFechaConclusion = validareader("CasoFechaResolucion", "CasoId", respuestastring3).Text;
                                dFechaconclusion = Convert.ToDateTime(sFechaConclusion);
                            }
                        }
                        catch (Exception)
                        {
                        }


                        DataGridViewRow dRows = (DataGridViewRow)dgViewBuscaConsultas.Rows[0].Clone();
                        dRows.Cells[0].Value = validareader("PaisClave", "CasoId", respuestastring3).Text;
                        dRows.Cells[1].Value = sCasoidconsulta;
                        dRows.Cells[2].Value = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                        dRows.Cells[3].Value = validareader("SubtipoSolicitudDescripcion", "CasoId", respuestastring3).Text;
                        dRows.Cells[4].Value = validareader("Estatuscasodescrip", "CasoId", respuestastring3).Text;
                        dRows.Cells[5].Value = dFechapresentacion;
                        dRows.Cells[6].Value = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                        dRows.Cells[7].Value = dFechaconclusion;
                        dRows.Cells[8].Value = validareader("CasoTituloespanol", "CasoId", respuestastring3).Text;
                        dRows.Cells[9].Value = validareader("InteresadoNombre", "CasoId", respuestastring3).Text;
                        dRows.Cells[10].Value = validareader("CasoMotivo", "CasoId", respuestastring3).Text;
                        dRows.Cells[11].Value = validareader("CasoObservaciones", "CasoId", respuestastring3).Text;
                        dRows.Cells[12].Value = validareader("NombreUtilClient", "CasoId", respuestastring3).Text;
                        dRows.Cells[13].Value = validareader("ReferenciaNombre", "CasoId", respuestastring3).Text;
                        dRows.Cells[14].Value = validareader("HolderNombre", "CasoId", respuestastring3).Text;

                        if (residuo == 0)
                        {
                            dRows.DefaultCellStyle.BackColor = Color.LightGray;
                            if (validareader("CasoFechaPresentacion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaPresentacion", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                dRows.Cells[5].Style.ForeColor = Color.Black;
                            }
                            else
                            {
                                dRows.Cells[5].Style.ForeColor = Color.LightGray;
                            }
                        }
                        else
                        {
                            dRows.DefaultCellStyle.BackColor = Color.Azure;
                            if (validareader("CasoFechaPresentacion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaPresentacion", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                dRows.Cells[5].Style.ForeColor = Color.Black;
                            }
                            else
                            {
                                dRows.Cells[5].Style.ForeColor = Color.Azure;
                            }
                        }
                        dgViewBuscaConsultas.Rows.Add(dRows);
                        rowcolor++;
                    }
                    respuestastring3.Close();
                    con.Cerrarconexion();
                    textBox10.Text = rowcolor + "";
                }

                }
            catch
                {

                }


        }

        private void bConsulta_FormClosing(object sender, FormClosingEventArgs e)
        {
            captura.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //20220316FSV llenado de grid con ultimos casos de consulta
            conect con = new conect();
            dgViewBuscaConsultas.Rows.Clear();
            int rowcolor = 0;

            //20220427FVS Agregra Filtro Estatus en Ultimos Casos
            String sQuerywhere = "";
            if (cbFiltroestatus.SelectedItem != null)
            {
                sQuerywhere += " CC.EstatusCasoId = '" + (cbFiltroestatus.SelectedItem as ComboboxItem).Value + "'";
            }
            //20220427FVS Agregra Filtro Estatus en Ultimos Casos

            try
            {
                String sQuerybusqueda = " SELECT  " +
                                " PS.PaisClave AS PaisClave, " +
                                " CC.CasoId as CasoId, " +
                                " CC.CasoNumero as CasoNumero, " +
                                " TS.TipoSolicitudDescrip as TipoSolicitudDescrip, " +
                                " SUB.SubtipoSolicitudDescripcion as SubtipoSolicitudDescripcion, " +
                                " DameEstatusCasoDescrip(CC.EstatusCasoId) As Estatuscasodescrip, " +
                                //" DATE_FORMAT(CC.CasoFechaRecepcion, ' %d-%m-%Y') as FechaPresentacion, " +
                                " DATE_FORMAT(CC.CasoFechaPresentacion, ' %d-%m-%Y') as FechaPresentacion, " +
                                " DATE_FORMAT(CC.CasoFechaResolucion, '%d-%m-%Y') as CasoFechaResolucion, " +
                                //20220316 Caso fecha de alta
                                " CC.CasoNumeroExpedienteLargo as CasoNumeroExpedienteLargo, " +
                                " DATE_FORMAT(CC.CasoFechaConcesion, ' %d-%m-%Y') as CasoFechaConcesion, " +
                                " CC.CasoNumConcedida as CasoNumConcedida, " +
                                " it.InteresadoNombre as InteresadoNombre, " +
                                " CC.CasoTituloespanol as CasoTituloespanol, " +
                                //20220531FSV Discriminar por tipo de solicitud
                                //" Get_Cliente(CC.CasoId) as NombreUtilClient, " +
                                " Get_Cliente_tiposol(CC.CasoId,CC.TipoSolicitudId) as NombreUtilClient, " +
                                //20220531FSV Fin de modificación
                                " PR.PrioridadNumero as PrioridadNUmero, " +
                                " CC.CasoMotivo as CasoMotivo, " +
                                " CC.CasoObservaciones as CasoObservaciones, " +
                                " RF.ReferenciaNombre as ReferenciaNombre, " +
                                " HD.HolderNombre as HolderNombre" +
                                " FROM caso_consulta CC LEFT JOIN pais PS ON CC.PaisID = PS.PaisID " +
                                " LEFT JOIN tiposolicitud TS ON CC.tiposolicitudId = TS.tiposolicitudID " +
                                " LEFT JOIN subtiposolicitud SUB ON CC.SubTipoSolicitudId = SUB.SubTipoSolicitudId  " +
                                " LEFT JOIN prioridad PR ON CC.CasoID = PR.CasoID AND CC.tiposolicitudid= PR.tiposolicitudID " +
                                " LEFT JOIN referencia RF ON CC.CasoID = RF.CasoID AND CC.tiposolicitudid= RF.tiposolicitudID " +
                                " LEFT JOIN casointeresado CI ON CC.CasoID = CI.CasoID AND CC.tiposolicitudid= CI.tiposolicitudID " +
                                " LEFT JOIN interesado IT ON CI.InteresadoID = IT.InteresadoID " +
                                " LEFT JOIN casocliente CS ON CC.CasoID = CS.CasoID AND cc.tiposolicitudid = CS.tiposolicitudid" +
                                " LEFT JOIN cliente CL ON CS.ClienteId = CL.ClienteId " +
                                " LEFT JOIN holder HD ON CL.HolderID = HD.HolderId ";


                                //" LEFT JOIN holder HD ON CL.HolderID = HD.HolderId " +
                                //" ORDER BY CasoId desc limit " + tbLimitcasos.Text + ";";
                if (sQuerywhere != "")
                {
                    sQuerybusqueda = sQuerybusqueda + " WHERE " + sQuerywhere;
                }
                sQuerybusqueda = sQuerybusqueda + " order by CC.CasoId desc limit " + tbLimitcasos.Text + ";";
                sQuerywhere = "";


                MySqlDataReader respuestastring3 = con.getdatareader(sQuerybusqueda);

                while (respuestastring3.Read())
                {
                    String sCasoidconsulta = validareader("CasoId", "CasoId", respuestastring3).Text;
                    int residuo = rowcolor % 2;


                    DateTime? dFechapresentacion = null;
                    try
                    {
                        if (validareader("FechaPresentacion", "CasoId", respuestastring3).Text != "00-00-0000")
                        {
                            String fecha = validareader("FechaPresentacion", "CasoId", respuestastring3).Text;
                            dFechapresentacion = Convert.ToDateTime(fecha);
                        }
                    }
                    catch (Exception)
                    {
                    }


                    //DateTime dFechapresentacion = DateTime.MinValue;
                    DateTime? dFechaconclusion = null;
                    try
                    {
                        if (validareader("CasoFechaResolucion", "CasoId", respuestastring3).Text != "00-00-0000")
                        {
                            String sFechaConclusion = validareader("CasoFechaResolucion", "CasoId", respuestastring3).Text;
                            dFechaconclusion = Convert.ToDateTime(sFechaConclusion);
                        }
                    }
                    catch (Exception)
                    {
                    }



                    DataGridViewRow dRows = (DataGridViewRow)dgViewBuscaConsultas.Rows[0].Clone();
                    dRows.Cells[0].Value = validareader("PaisClave", "CasoId", respuestastring3).Text;
                    dRows.Cells[1].Value = sCasoidconsulta;
                    dRows.Cells[2].Value = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                    dRows.Cells[3].Value = validareader("SubtipoSolicitudDescripcion", "CasoId", respuestastring3).Text;
                    dRows.Cells[4].Value = validareader("Estatuscasodescrip", "CasoId", respuestastring3).Text;
                    //dRows.Cells[5].Value = validareader("FechaPresentacion", "CasoId", respuestastring3).Text;
                    dRows.Cells[5].Value = dFechapresentacion;
                    dRows.Cells[6].Value = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                    dRows.Cells[7].Value = dFechaconclusion;

                    dRows.Cells[8].Value = validareader("CasoTituloespanol", "CasoId", respuestastring3).Text;
                    dRows.Cells[9].Value = validareader("InteresadoNombre", "CasoId", respuestastring3).Text;
                    dRows.Cells[10].Value = validareader("CasoMotivo", "CasoId", respuestastring3).Text;
                    dRows.Cells[11].Value = validareader("CasoObservaciones", "CasoId", respuestastring3).Text;
                    dRows.Cells[12].Value = validareader("NombreUtilClient", "CasoId", respuestastring3).Text;
                    dRows.Cells[13].Value = validareader("ReferenciaNombre", "CasoId", respuestastring3).Text;
                    dRows.Cells[14].Value = validareader("HolderNombre", "CasoId", respuestastring3).Text;


                    if (residuo == 0)
                    {
                        dRows.DefaultCellStyle.BackColor = Color.LightGray;
                    }
                    else
                    {
                        dRows.DefaultCellStyle.BackColor = Color.Azure;
                    }
                    dgViewBuscaConsultas.Rows.Add(dRows);
                    rowcolor++;






                }
                respuestastring3.Close();
                con.Cerrarconexion();
                textBox10.Text = rowcolor + "";
            }
            catch (Exception Exn)
            {
                MessageBox.Show("Se encontraron más de " + rowcolor + " la busqueda debe ser más especifica. " + Exn);
            }
            //20220316FSV Fin llena grid ultimos casos de consulta


        }

        private void btExportarExcel_Click(object sender, EventArgs e)
        {


            //try
            //{
            //    var excelApp = new Excel.Application();
            //    excelApp.Visible = true;
            //    excelApp.Workbooks.Add();
            //    Excel._Worksheet workSheet = excelApp.ActiveSheet;

            //    workSheet.Cells[1, "A"] = "Pais";
            //    workSheet.Cells[1, "B"] = "Casoid";
            //    workSheet.Cells[1, "C"] = "Caso";
            //    workSheet.Cells[1, "D"] = "Subtipo Consulta";
            //    workSheet.Cells[1, "E"] = "Estatus";
            //    workSheet.Cells[1, "F"] = "Fecha Presentación";
            //    workSheet.Cells[1, "G"] = "Expediente";
            //    workSheet.Cells[1, "H"] = "Titulo/Denominacion";
            //    workSheet.Cells[1, "I"] = "Interesado";
            //    workSheet.Cells[1, "J"] = "Motivo Consulta";
            //    workSheet.Cells[1, "K"] = "Observaciones";
            //    workSheet.Cells[1, "L"] = "Cliente";
            //    workSheet.Cells[1, "M"] = "Referencia";
            //    workSheet.Cells[1, "N"] = "Holder";

            //    //workSheet.Cells[1, "A"] = "Casos king: ";
            //    //workSheet.Range["A1", "F1"].Merge();
            //    //workSheet.Range["A1", "F1"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //    //workSheet.Range["A1", "F1"].Font.Bold = true;
            //    //workSheet.Range["A1", "F1"].Font.Size = 20;
            //    //var row = 3;
            //    var row = 1;

            //    for (int i = 0; i < dgViewBuscaConsultas.Rows.Count; i++)
            //    {
            //        workSheet.Cells[i + 2, "A"] = dgViewBuscaConsultas.Rows[i].Cells[0].Value;
            //        workSheet.Cells[i + 2, "B"] = dgViewBuscaConsultas.Rows[i].Cells[1].Value;
            //        workSheet.Cells[i + 2, "C"] = dgViewBuscaConsultas.Rows[i].Cells[2].Value;
            //        workSheet.Cells[i + 2, "D"] = dgViewBuscaConsultas.Rows[i].Cells[3].Value;
            //        workSheet.Cells[i + 2, "E"] = dgViewBuscaConsultas.Rows[i].Cells[4].Value;
            //        workSheet.Cells[i + 2, "F"] = dgViewBuscaConsultas.Rows[i].Cells[5].Value;
            //        workSheet.Cells[i + 2, "G"] = dgViewBuscaConsultas.Rows[i].Cells[6].Value;
            //        workSheet.Cells[i + 2, "H"] = dgViewBuscaConsultas.Rows[i].Cells[7].Value;
            //        workSheet.Cells[i + 2, "I"] = dgViewBuscaConsultas.Rows[i].Cells[8].Value;
            //        workSheet.Cells[i + 2, "J"] = dgViewBuscaConsultas.Rows[i].Cells[9].Value;
            //        workSheet.Cells[i + 2, "K"] = dgViewBuscaConsultas.Rows[i].Cells[10].Value;
            //        workSheet.Cells[i + 2, "L"] = dgViewBuscaConsultas.Rows[i].Cells[11].Value;
            //        workSheet.Cells[i + 2, "M"] = dgViewBuscaConsultas.Rows[i].Cells[12].Value;
            //        workSheet.Cells[i + 2, "N"] = dgViewBuscaConsultas.Rows[i].Cells[13].Value;
            //        //workSheet.Cells[i + 4, "O"] = dgViewBuscaConsultas.Rows[i].Cells[14].Value;
            //        row = i;
            //    }
                
            //    workSheet.Columns[1].AutoFit();
            //    workSheet.Columns[2].AutoFit();
            //    workSheet.Columns[3].AutoFit();
            //    workSheet.Columns[4].AutoFit();
            //    workSheet.Columns[5].AutoFit();
            //    workSheet.Columns[6].AutoFit();

            //    //workSheet.Range["A3", "P" + (row + 4)].AutoFormat(Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic2);
            //    workSheet.Range["A1", "N" + (row + 2)].RowHeight=15;

            //}
            //catch (Exception E)
            //{
            //    Console.Write("Se canceló la exportación");
            //    new filelog(fLoguin.sId, E.ToString());
            //}


            try
            {
                generaexcel(dgViewBuscaConsultas);
            }
            catch (Exception E)
            {
                Console.Write("Se canceló la exportación");
                new filelog(fLoguin.sId, E.ToString());
                MessageBox.Show(E.Message);
            }



        }

        private void bConsulta_Load(object sender, EventArgs e)
        {

        }

        private void buscapaisporclave()
        {
            conect con = new conect();
            string sClavePais = tbClave.Text;

            String query5 = "SELECT PaisNombre, PaisId, PaisClave FROM pais WHERE PaisClave = '" + sClavePais + "';";
            MySqlDataReader respuestastringopais = con.getdatareader(query5);
            while (respuestastringopais.Read())
            {
                String sPaisNombre = validareader("PaisNombre", "PaisId", respuestastringopais).Text;
                cbPais.Text = sPaisNombre;
            }


        }

        private void dgViewBuscaConsultas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgViewBuscaConsultas.SelectedRows != null)
                {
                    String sCasoIdcaso = dgViewBuscaConsultas.SelectedRows[0].Cells[1].Value.ToString();
                    dgViewBuscaConsultas.Rows.Clear();
                    tbexpediente.Clear();
                    tbInteresado.Clear();

                    tbDenominacion.Clear();
                    tbMotivo.Clear();
                    tbCliente.Clear();
                    tbreferencia.Clear();
                    tbCasoid.Clear();
                    //consultacaso cConsultaid = new consultacaso(loguin, captura, this, sCasoIdcaso);
                    this.Hide();
                    //cConsultaid.Show();

                    fTcasoconsulta OBJCONTENT = new fTcasoconsulta(fLoguin, captura, this, sCasoIdcaso);
                    // error momentaneo efTcasoconsulta OBJCONTENT = new fTcasoconsulta(fLoguin, captura, this, sCasoIdcaso);
                    //20220331 quiyar error momentaneo
                    OBJCONTENT.Show();

                }
                else
                {
                    MessageBox.Show("Debe seleccionar un caso.");
                }
            }
            catch (Exception exs)
            {
                new filelog("Mensaje:", " :" + exs.Message);
            }
        }

        private void tbClave_Enter(object sender, EventArgs e)
        {
         
        }

        private void tbClave_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscapaisporclave();
            }
        }


        public void generaexcel(DataGridView tabla)
        {
            try
            {

                SLDocument obj = new SLDocument();
                SLStyle estilofechas = obj.CreateStyle();
                estilofechas.FormatCode = "dd/mm/yyyy";

                btExportarExcel.Enabled = false;

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

                        //, "MM/dd/yyyy"
                        ic++;
                    }
                    ir++;
                }
                //generamos la ruta
                String fechalog = DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss");
                String ruta = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\casosking\\";

                //guardamos el archivo
                obj.SaveAs(ruta + "excel_marcas" + fechalog + ".xlsx");
                //abrirmos el archivo
                Process.Start(ruta + "excel_marcas" + fechalog + ".xlsx");

            }
            catch (Exception exs)
            {
                new filelog("error al generar excel ", " :" + exs.Message);
                MessageBox.Show(exs.Message);

            }
        }


    }
}
