using MySql.Data.MySqlClient;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Facturador
{
    public partial class bContencioso_old : Form
    {
        public captura captura;
        public Form1 fLoguin;
        public String[] paises = new String[250];
        public String[] paisesclave = new String[250];
        public bContencioso_old(int iGrupo, captura capturaform, Form1 loguin)
        {
            fLoguin = loguin;
            captura = capturaform;
            InitializeComponent();
            this.BackColor = Color.Yellow;
            conect con = new conect();
            String sQuerytipoSol = "select * from tiposolicitud where TipoSolicitudGrupo= " + iGrupo;
            MySqlDataReader respuestastringtoiposl = con.getdatareader(sQuerytipoSol);
            while (respuestastringtoiposl.Read())
            {
                cbTiposolicitud.Items.Add(validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestastringtoiposl));
            }
            respuestastringtoiposl.Close();

            //20220225 Agregamos la opcion todos en el combo
            cbTiposolicitud.Items.Add("Todos");


            String sQuerypais = "select * from pais;";
            MySqlDataReader respuestastringpais = con.getdatareader(sQuerypais);
            while (respuestastringpais.Read())
            {
                cbPais.Items.Add(validareader("PaisNombre", "PaisId", respuestastringpais));
            }
            respuestastringpais.Close();


            //select * from pais;
            String query5 = "select PaisNombre, PaisId, PaisClave from pais;";
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

            //20220318FSV Rearmar Query
            //(1) 

            conect con = new conect();
            dgViewBuscaContenciosos.Rows.Clear();
            int rowcolor = 0;


            try
            {
                String sQuerywhere = "";
                String sQuerywherecaso = "";
                //String stablaconsulta = "";

                //if (cbTiposolicitud.SelectedItem != null)
                //{
                //    sQuerywhere += " AND CC.tiposolicitudid = " + (cbTiposolicitud.SelectedItem as ComboboxItem).Value;
                //}

                if (cbTiposolicitud.SelectedItem != null)
                {
                    string sValorCombo = cbTiposolicitud.SelectedItem.ToString();
                    if (sValorCombo == "Todos"){
                        sQuerywhere += " AND CC.tiposolicitudid in (10,11,12,17) ";}
                    else{
                        sQuerywhere += " AND CC.tiposolicitudid = " + (cbTiposolicitud.SelectedItem as ComboboxItem).Value;}
                }


                if (!string.IsNullOrEmpty(textBoxCasonumero.Text))
                {
                    sQuerywhere += " AND CC.CasoNumero like '%" + textBoxCasonumero.Text + "%'";
                }

                if (!string.IsNullOrEmpty(tbexpediente.Text))
                {
                    sQuerywhere += " AND CC.CasoNumeroExpedienteLargo like '%" + tbexpediente.Text + "%'";
                }

                if (!string.IsNullOrEmpty(tbDenominacion.Text))
                {
                    sQuerywhere += " AND ( CC.CasoTituloingles like '%" + tbDenominacion.Text + "%' OR CC.CasoTituloespanol LIKE '%" + tbDenominacion.Text + "%')";
                }

                if (!string.IsNullOrEmpty(tbregistro.Text))
                {
                    sQuerywhere += " AND CC.CasoNumConcedida like '%" + tbregistro.Text + "%'";
                }

                if (!string.IsNullOrEmpty(tbCasoid.Text))
                {
                    sQuerywhere += " AND CC.CasoId =" + tbCasoid.Text;
                }

                if (cbPais.SelectedItem != null)
                {
                    sQuerywhere += " AND CC.PaisId = '" + (cbPais.SelectedItem as ComboboxItem).Value + "'";
                }

                if (!string.IsNullOrEmpty(tbCliente.Text))
                {
                    sQuerywhere += " AND GET_CLIENTE(CC.CasoId) like '%" + tbCliente.Text + "%'";
                }

                if (!string.IsNullOrEmpty(tbreferencia.Text))
                {
                    sQuerywhere += " AND Get_Referencia(CC.Casoid, CC.TipoSolicitudId) like '%" + tbreferencia.Text + "%'";
                }

                if (!string.IsNullOrEmpty(tbInteresado.Text))
                {
                    sQuerywhere += " AND GET_INTERESADOS_TIPOSOL(CC.Casoid, CC.TipoSolicitudId) like '%" + tbInteresado.Text + "%'";
                }

                
                if (cbHolder.SelectedItem != null){
                    sQuerywhere += " AND HD.HolderNombre = '" + (cbHolder.SelectedItem as ComboboxItem).Value + "'";}


                //Revisar
                //if (!string.IsNullOrEmpty(tbCliente.Text))
                //{
                //    sQuerywhere += " AND GET_CLIENTE(CC.CasoId) like '%" + tbCliente.Text + "%'";
                //}

                //if (!string.IsNullOrEmpty(tbreferencia.Text))
                //{
                //    sQuerywhere += " AND Get_Referencia(CC.Casoid, caso_patente.TipoSolicitudId) like '%" + tbreferencia.Text + "%'";
                //}

                //if (!string.IsNullOrEmpty(tbInteresado.Text)){
                //    sQuerywhere += " AND GET_INTERESADOS_TIPOSOL(Casoid, caso_patente.TipoSolicitudId) like '%" + tbInteresado.Text + "%'";}

                //if (!string.IsNullOrEmpty(tbPrioridad.Text)){
                //    sQuerywhere += " AND GET_PRIORIDAD(caso_patente.Casoid, caso_patente.TipoSolicitudId) like '%" + tbPrioridad.Text + "%'";}


                //Cambiar texto por combo
                //if (cbHolder.SelectedItem != null){
                //    sQuerywhere += " AND Dameelholder_patentes_all_casoid(caso_patente.CasoId, caso_patente.TipoSolicitudId) = '" + (cbHolder.SelectedItem as ComboboxItem).Value + "'";}




                if (sQuerywhere != "" || sQuerywherecaso != "")
                {
                    sQuerywhere = sQuerywhere.Substring(4, sQuerywhere.Length - 4);
                    String sQuerybusqueda = "";
                    sQuerybusqueda = " SELECT DISTINCT  " +
                                " PS.PaisClave AS PaisClave, " +
                                " CC.CasoId as CasoId, " +
                                " CC.CasoNumero as CasoNumero, " +
                                " TS.TipoSolicitudDescrip as TipoSolicitudDescrip, " +
                                " DameEstatusCasoDescrip(CC.EstatusCasoId) As Estatuscasodescrip, " +
                                //" DATE_FORMAT(CC.CasoFechaAlta, ' %d-%m-%Y') as FechaPresentacion, " +
                                " DATE_FORMAT(CC.CasoFechaRecepcion, ' %d-%m-%Y') as CasoFechaRecepcion, " +
                                " DATE_FORMAT(CC.CasoFechaPresentacion, ' %d-%m-%Y') as CasoFechaPresentacion, " +
                                " CC.CasoNumeroExpedienteLargo as CasoNumeroExpedienteLargo, " +
                                " DATE_FORMAT(CC.CasoFechaConcesion, ' %d-%m-%Y') as CasoFechaConcesion, " +
                                " CC.CasoNumConcedida as CasoNumConcedida, " +
                                " it.InteresadoNombre as InteresadoNombre, " +
                                " CC.CasoTituloespanol as CasoTituloespanol, " +
                                " Get_Cliente(CC.CasoId) as ClienteNombre, " +
                                " PR.PrioridadNumero as PrioridadNUmero, " +
                                " RF.ReferenciaNombre as ReferenciaNombre, " +
                                " HD.HolderNombre as HolderNombre" +
                                " FROM caso_contencioso CC LEFT JOIN pais PS ON CC.PaisID = PS.PaisID " +
                                " LEFT JOIN tiposolicitud TS ON CC.tiposolicitudId = TS.tiposolicitudID " +
                                " LEFT JOIN prioridad PR ON CC.CasoID = PR.CasoID AND CC.tiposolicitudid= PR.tiposolicitudID " +
                                " LEFT JOIN referencia RF ON CC.CasoID = RF.CasoID AND CC.tiposolicitudid= RF.tiposolicitudID " +
                                " LEFT JOIN casointeresado CI ON CC.CasoID = CI.CasoID AND CC.tiposolicitudid= CI.tiposolicitudID " +
                                " LEFT JOIN interesado IT ON CI.InteresadoID = IT.InteresadoID " +
                                " LEFT JOIN holder HD ON IT.HolderID = HD.HolderId " +
                                " WHERE " +
                                sQuerywhere;

                    MySqlDataReader respuestastring3 = con.getdatareader(sQuerybusqueda);
                    while (respuestastring3.Read())
                    {

                        String sCasoidconsulta = validareader("CasoId", "CasoId", respuestastring3).Text;
                        int residuo = rowcolor % 2;


                        DateTime dFechapresentacion = DateTime.MinValue;
                        try
                        {
                            if (validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text != "00-00-0000")
                            {
                                String fecha = validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text;
                                dFechapresentacion = Convert.ToDateTime(fecha);
                            }
                        }
                        catch (Exception exdate){
                        }

                        DateTime dFechaconcesion = DateTime.MinValue;
                        try
                        {
                            if (validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text != "00-00-0000")
                            {
                                dFechaconcesion = Convert.ToDateTime(validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text);
                            }
                        }
                        catch (Exception exdates){
                        }


                        DataGridViewRow dRows = (DataGridViewRow)dgViewBuscaContenciosos.Rows[0].Clone();

                        dRows.Cells[0].Value = validareader("PaisClave", "CasoId", respuestastring3).Text;
                        dRows.Cells[1].Value = sCasoidconsulta;
                        dRows.Cells[2].Value = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                        dRows.Cells[3].Value = validareader("TipoSolicitudDescrip", "CasoId", respuestastring3).Text;
                        dRows.Cells[4].Value = validareader("Estatuscasodescrip", "CasoId", respuestastring3).Text;
                        dRows.Cells[5].Value = dFechapresentacion;
                        dRows.Cells[6].Value = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                        dRows.Cells[7].Value = dFechaconcesion;
                        dRows.Cells[8].Value = validareader("CasoNumConcedida", "CasoId", respuestastring3).Text;
                        dRows.Cells[9].Value = validareader("InteresadoNombre", "CasoId", respuestastring3).Text;
                        dRows.Cells[10].Value = validareader("CasoTituloespanol", "CasoId", respuestastring3).Text;
                        dRows.Cells[11].Value = validareader("ClienteNombre", "CasoId", respuestastring3).Text;
                        dRows.Cells[12].Value = validareader("ReferenciaNombre", "CasoId", respuestastring3).Text;
                        dRows.Cells[13].Value = validareader("HolderNombre", "CasoId", respuestastring3).Text;

                        if (residuo == 0)
                        {
                            dRows.DefaultCellStyle.BackColor = Color.LightGray;
                            if (validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != ""){
                                dRows.Cells[5].Style.ForeColor = Color.Black;}
                            else{
                                dRows.Cells[5].Style.ForeColor = Color.LightGray;}

                            if (validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != ""){
                                dRows.Cells[7].Style.ForeColor = Color.Black;}
                            else{
                                dRows.Cells[7].Style.ForeColor = Color.LightGray;}
                        }
                        else
                        {
                            dRows.DefaultCellStyle.BackColor = Color.Azure;
                            if (validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != ""){
                                dRows.Cells[5].Style.ForeColor = Color.Black;}
                            else{
                                dRows.Cells[5].Style.ForeColor = Color.Azure;}

                            if (validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != ""){
                                dRows.Cells[7].Style.ForeColor = Color.Black;}
                            else{
                                dRows.Cells[7].Style.ForeColor = Color.Azure;}
                        }

                        dgViewBuscaContenciosos.Rows.Add(dRows);
                        rowcolor++;

                    }
                    respuestastring3.Close();
                    con.Cerrarconexion();
                    textBox10.Text = rowcolor + "";
                }
                else
                {
                    MessageBox.Show("Debe buscar por lo menos en un campo de busqueda");
                }


            }
            catch (Exception E)
            {
                textBox10.Text = rowcolor + "";
                MessageBox.Show("Se encontraron más de " + rowcolor + " la busqueda debe ser más especifica.");
            }


            ////borramos el listview 
            //conect con = new conect();
            //listViewCasos.Items.Clear();
            //int rowcolor = 0;
            //try
            //{
            //    String sQuerywhere = "";
            //    String sQuerywherecaso = "";
            //    String stablaconsulta = "";

            //    dgViewBuscaContenciosos.Rows.Clear();


            //    /*20220224 Agregamos todo el grupo
            //    if (cbTiposolicitud.SelectedItem != null) {
            //        sQuerywhere += " AND caso_contencioso.tiposolicitudid = " + (cbTiposolicitud.SelectedItem as ComboboxItem).Value;
            //        sQuerywhere += " AND Tiposolicitud.tiposolicitudid = caso_contencioso.tiposolicitudid";
            //        stablaconsulta += ",Tiposolicitud";
            //    }
            //    //20220225 fin de modificación*/


            //    //and cbTiposolicitud.SelectedItem.ToString() = "Todos"
            //    if (cbTiposolicitud.SelectedItem != null )
            //    {
            //        string sValorCombo = cbTiposolicitud.SelectedItem.ToString();
            //        if (sValorCombo == "Todos")
            //        {
            //            //sQuerywhere += " AND caso_contencioso.tiposolicitudid = " + (cbTiposolicitud.SelectedItem as ComboboxItem).Value;
            //            sQuerywhere += " AND caso_contencioso.tiposolicitudid in (10,11,12,17) ";
            //            sQuerywhere += " AND Tiposolicitud.tiposolicitudid = caso_contencioso.tiposolicitudid";
            //            stablaconsulta += ",Tiposolicitud";
            //        }
            //        else
            //        {
            //            sQuerywhere += " AND caso_contencioso.tiposolicitudid = " + (cbTiposolicitud.SelectedItem as ComboboxItem).Value;
            //            //sQuerywhere += " AND caso_contencioso.tiposolicitudid in (10,11,12,17) ";
            //            sQuerywhere += " AND Tiposolicitud.tiposolicitudid = caso_contencioso.tiposolicitudid";
            //            stablaconsulta += ",Tiposolicitud";
            //        }
            //    }
            //    //fin de modificacion


            //    if (!string.IsNullOrEmpty(tbexpediente.Text)) {
            //        sQuerywhere += " AND caso_contencioso.CasoNumeroExpedienteLargo like '%" + tbexpediente.Text + "%'";
            //    }


            //    if (!string.IsNullOrEmpty(tbDenominacion.Text)) {
            //        sQuerywhere += " AND ( caso_contencioso.CasoTituloingles like '%" + tbDenominacion.Text + "%' OR caso_contencioso.CasoTituloespanol LIKE '%" + tbDenominacion.Text + "%')";
            //    }


            //    if (!string.IsNullOrEmpty(tbregistro.Text)) {
            //        sQuerywhere += " AND caso_contencioso.CasoNumConcedida like '%" + tbregistro.Text + "%'";
            //    }


            //    if (!string.IsNullOrEmpty(tbCasoid.Text)) {
            //        sQuerywhere += " AND caso_contencioso.CasoId =" + tbCasoid.Text;
            //    }


            //    if (!string.IsNullOrEmpty(textBoxCasonumero.Text)) {
            //        sQuerywhere += " AND caso_contencioso.CasoNumero like '%" + textBoxCasonumero.Text + "%'";
            //    }


            //    if (cbPais.SelectedItem != null) {
            //        sQuerywhere += " AND caso_contencioso.PaisId = '" + (cbPais.SelectedItem as ComboboxItem).Value + "'";
            //        sQuerywhere += " AND Pais.PaisId = caso_contencioso.PaisId";
            //        stablaconsulta += ",Pais";
            //    }


            //    //20220221 Fsalazar - Se agregan filtros faltantes
            //    if (!string.IsNullOrEmpty(tbCliente.Text) && string.IsNullOrEmpty(textBox1.Text)){
            //        sQuerywhere += " AND cliente.ClienteNombre Like '%" + tbCliente.Text + "%'";
            //        sQuerywhere += " AND casocliente.CasoId = caso_contencioso.CasoId";
            //        sQuerywhere += " AND casocliente.ClienteId = cliente.ClienteId";
            //        stablaconsulta += ",casocliente,cliente";                    
            //    }

            //    if (!string.IsNullOrEmpty(tbCliente.Text) && !string.IsNullOrEmpty(textBox1.Text)){
            //        sQuerywhere += " AND cliente.ClienteNombre Like '%" + tbCliente.Text + "%'";
            //        sQuerywhere += " AND holder.HolderNombre Like '%" + textBox1.Text + "%'";
            //        sQuerywhere += " AND casocliente.CasoId = caso_contencioso.CasoId";
            //        sQuerywhere += " AND casocliente.ClienteId = cliente.ClienteId";
            //        sQuerywhere += " AND cliente.HolderId = holder.HolderId";
            //        stablaconsulta += ",casocliente,cliente,holder";
            //    }

            //    if (string.IsNullOrEmpty(tbCliente.Text) && !string.IsNullOrEmpty(textBox1.Text)){
            //        sQuerywhere += " AND holder.HolderNombre Like '%" + textBox1.Text + "%'";
            //        sQuerywhere += " AND casocliente.CasoId = caso_contencioso.CasoId";
            //        sQuerywhere += " AND casocliente.ClienteId = cliente.ClienteId";
            //        sQuerywhere += " AND cliente.HolderId = holder.HolderId";
            //        stablaconsulta += ",casocliente,cliente,holder";
            //    }

            //    if (!string.IsNullOrEmpty(tbreferencia.Text)) {
            //        sQuerywhere += " AND referencia.ReferenciaNombre Like '%" + tbreferencia.Text + "%'";
            //        sQuerywhere += " AND referencia.CasoId = caso_contencioso.CasoId";
            //        stablaconsulta += ",referencia";
            //    }

            //    if (!string.IsNullOrEmpty(tbInteresado.Text)){
            //        sQuerywhere += " AND interesado.InteresadoNombre Like '%" + tbInteresado.Text + "%'";
            //        sQuerywhere += " AND casointeresado.CasoId = caso_contencioso.CasoId";
            //        sQuerywhere += " AND casointeresado.InteresadoId = interesado.InteresadoId";
            //        stablaconsulta += ",casointeresado,interesado";
            //    }

            //    if (!string.IsNullOrEmpty(tbPrioridad.Text)){
            //        sQuerywhere += " AND prioridad.PrioridadNumero Like '%" + tbPrioridad.Text + "%'";
            //        sQuerywhere += " AND prioridad.CasoId = caso_contencioso.CasoId";
            //        stablaconsulta += ",referencia";
            //    }
            //    //20220221 Fsalazar - Fin de modificación


            //    if (sQuerywhere != "" || sQuerywherecaso != "")
            //    {
            //        sQuerywhere = sQuerywhere.Substring(4, sQuerywhere.Length - 4);
            //        String sQuerybusqueda = "";
            //        sQuerybusqueda = " SELECT  " +
            //                            " * " +
            //                            " FROM " +
            //                            " caso_contencioso" +
            //                            stablaconsulta +
            //                            " WHERE " +
            //                                sQuerywhere;
            //        MySqlDataReader respuestastring3 = con.getdatareader(sQuerybusqueda);
            //        while (respuestastring3.Read())
            //        {
            //            String sCasoidconsulta = validareader("CasoId", "CasoId", respuestastring3).Text;

            //            String sPrioridades = "";
            //            conect con_prioridad = new conect();
            //            String sQueryprio = "select PrioridadNumero, PrioridadId from  prioridad where casoid =" + sCasoidconsulta;
            //            MySqlDataReader respuestastringprio = con_prioridad.getdatareader(sQueryprio);
            //            while (respuestastringprio.Read())
            //            {
            //                sPrioridades += validareader("PrioridadNumero", "PrioridadId", respuestastringprio) + " ~ ";
            //            }
            //            respuestastringprio.Close();
            //            con_prioridad.Cerrarconexion();

            //            conect con_interesado = new conect();
            //            String sInteresadosconsul = " SELECT  " +
            //                                        "     * " +
            //                                        " FROM " +
            //                                        "     interesado, " +
            //                                        "     casointeresado " +
            //                                        " WHERE " +
            //                                        "     casointeresado.InteresadoId = interesado.InteresadoID " +
            //                                        "     AND casointeresado.CasoId =" + sCasoidconsulta;
            //            String sQueryintere = "";
            //            MySqlDataReader respuestastringinteresado = con_interesado.getdatareader(sInteresadosconsul);
            //            //int paisint = 0;
            //            while (respuestastringinteresado.Read())
            //            {
            //                sQueryintere += validareader("InteresadoNombre", "InteresadoId", respuestastringinteresado) + " ~ ";
            //            }
            //            respuestastringinteresado.Close();
            //            con_interesado.Cerrarconexion();
            //            int residuo = rowcolor % 2;
            //            //buscamos clases en productos
            //            String sMarcasdescription = "";
            //            String sQueryclasemarcas = "SELECT * FROM `casoproductos` WHERE casoid =" + sCasoidconsulta;
            //            MySqlDataReader respuestastring = con.getdatareader(sQueryclasemarcas);
            //            while (respuestastring.Read())
            //            {
            //                sMarcasdescription += validareader("CasoProductosClase", "CasoProductosId", respuestastring).Text + ",";
            //            }
            //            respuestastring.Close();
            //            if (sMarcasdescription.Length > 1)
            //            {
            //                sMarcasdescription = sMarcasdescription.Substring(0, sMarcasdescription.Length - 1);
            //            }
            //            else
            //            {
            //                sMarcasdescription = "";
            //            }
            //            //terminamos buscar clases en productos

            //            //iniciamos busqueda de clientes
            //            String sClientesdatos = "";
            //            String sClientes = " SELECT  " +
            //                                "     * " +
            //                                " FROM " +
            //                                "     caso_contencioso, " +
            //                                "     cliente, " +
            //                                "     casocliente " +
            //                                " WHERE " +
            //                                "     caso_contencioso.CasoId like '%" + sCasoidconsulta + "%' " +
            //                                "     AND caso_contencioso.CasoId = casocliente.CasoId " +
            //                                "     AND cliente.ClienteId = casocliente.ClienteId; ";

            //            MySqlDataReader respuestastringclientes = con.getdatareader(sClientes);
            //            while (respuestastringclientes.Read())
            //            {
            //                sClientesdatos += validareader("ClienteNombre", "ClienteID", respuestastringclientes) + " ~ ";
            //            }
            //            respuestastringclientes.Close();
            //            //terminamos busqueda de clientes
            //            //prueba de todos los casos
            //            MySqlDataReader resp_paiscaso = con.getdatareader("select * from Pais where PaisId = " + validareader("PaisId", "PaisId", respuestastring3).Text);
            //            String sPaisclave = "";
            //            while (resp_paiscaso.Read())
            //            {
            //                sPaisclave = validareader("PaisClave", "PaisId", resp_paiscaso).Text;
            //            }
            //            resp_paiscaso.Close();


            //            String sTiposolicitudDescrip = "";
            //            String sTiposolicitud = "select * from tiposolicitud where tiposolicitudId = " + validareader("tiposolicitudId", "CasoId", respuestastring3).Text;
            //            MySqlDataReader resp_tiposolicitud = con.getdatareader(sTiposolicitud);
            //            while (resp_tiposolicitud.Read())
            //            {
            //                sTiposolicitudDescrip = validareader("TipoSolicitudDescrip", "tiposolicitudId", resp_tiposolicitud).Text;//consultar Tiposolicitud
            //                //tbTipo.Text = validareader("TipoSolicitudDescrip", "CasoId", respuestastring3).Text;//consultar Tiposolicitud
            //            }
            //            resp_tiposolicitud.Close();
            //            //


            //            //20220222 Fsalazar llenamos columna referencia que estaba hardcodeada
            //            String sReferenciaDescrip = "";
            //            String sReferencia = "select * from referencia WHERE casoid =" + sCasoidconsulta;
            //            MySqlDataReader resp_referencia = con.getdatareader(sReferencia);
            //            while (resp_referencia.Read()){
            //                sReferenciaDescrip = validareader("ReferenciaNombre", "ReferenciaId", resp_referencia).Text;//consultar referencia
            //            }
            //            resp_referencia.Close();
            //            //20220222 Fin de modificacion*/

            //            //20220222 Fsalazar traemos el Holder
            //            String sHolderDescrip = "";
            //            String sHolder = "select * from caso_contencioso, casocliente, cliente, holder " +
            //                                " WHERE casocliente.CasoId = caso_contencioso.CasoId" +
            //                                " AND casocliente.ClienteId = cliente.ClienteId" +
            //                                " AND cliente.HolderId = holder.HolderId " +
            //                                " AND caso_contencioso.casoid =" + sCasoidconsulta;
            //            MySqlDataReader resp_holder = con.getdatareader(sHolder);
            //            while (resp_holder.Read()){
            //                sHolderDescrip = validareader("HolderNombre", "HolderId", resp_holder).Text;//consultar holder
            //            }
            //            resp_holder.Close();
            //            //20220222 Fin de modificacion*/



            //            //20220222 Fsalazar traemos las fechas agregadas
            //            String sEstatusCaso = "";
            //            String sFechaPresentacion = "";
            //            String sFechaConcesion = "";
            //            String sFechas = "SELECT CasoId, DameEstatusCasoDescrip(EstatusCasoId) As EstatusCaso, " +
            //                                "DATE_FORMAT(CasoFechaPresentacion, ' %d-%m-%Y') as CasoFechaPresentacion, " +
            //                                "DATE_FORMAT(CasoFechaConcesion, ' %d-%m-%Y') as CasoFechaConcesion " +
            //                                "FROM caso_contencioso WHERE CasoID = " + sCasoidconsulta;
            //            MySqlDataReader resp_fechas = con.getdatareader(sFechas);
            //            while (resp_fechas.Read())
            //            {
            //                sEstatusCaso = validareader("EstatusCaso", "CasoId", resp_fechas).Text;
            //                sFechaPresentacion = validareader("CasoFechaPresentacion", "CasoId", resp_fechas).Text;
            //                sFechaConcesion = validareader("CasoFechaConcesion", "CasoId", resp_fechas).Text;
            //            }
            //            resp_fechas.Close();
            //            //20220222 Fin de modificacion*/


            //            /* //20220225 Fsalzar aqui se llena el viejo listview que hay que quitar
            //            ListViewItem listaitems = new ListViewItem(sPaisclave);
            //            listaitems.SubItems.Add(sCasoidconsulta);
            //            listaitems.SubItems.Add(validareader("CasoNumero", "CasoId", respuestastring3).Text);
            //            listaitems.SubItems.Add(sTiposolicitudDescrip);
            //            listaitems.SubItems.Add(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text);
            //            listaitems.SubItems.Add(validareader("CasoNumConcedida", "CasoId", respuestastring3).Text);
            //            //listaitems.SubItems.Add(validareader("InteresadoNombre", "CasoId", respuestastring3).Text);
            //            listaitems.SubItems.Add(sQueryintere);
            //            listaitems.SubItems.Add(validareader("CasoTituloespanol", "CasoId", respuestastring3).Text);// + "/ " + validareader("CasoTituloingles", "CasoId", respuestastring3).Text);
            //            listaitems.SubItems.Add(sClientesdatos);
            //            listaitems.SubItems.Add(sPrioridades);
            //            //20220222 Falazar Quitamos Hardcode
            //            //listaitems.SubItems.Add("referencia");
            //            listaitems.SubItems.Add(sReferenciaDescrip);
            //            //2022022 Fin de modificacion
            //            //20220222 Agregamos Columna Holder que no estaba
            //            listaitems.SubItems.Add(sHolderDescrip);
            //            //20220222 Fin de modificacion

            //            ///Se formatea y se llena el list view
            //            if (residuo == 0)
            //            {
            //                listaitems.BackColor = Color.LightGray;
            //            }
            //            else
            //            {
            //                listaitems.BackColor = Color.Azure;
            //            }
            //            listViewCasos.Items.Add(listaitems);
            //            this.listViewCasos.FullRowSelect = true;
            //            rowcolor++;
            //            //Fin de formato y llenado
            //            20220225 Fsalazar fin quitar viejo list view */


            //            //20220225 Fsalazar llenamos y formateamos nuevo grid
            //            DataGridViewRow dRows = (DataGridViewRow)dgViewBuscaContenciosos.Rows[0].Clone();
            //            dRows.Cells[0].Value = sPaisclave;
            //            dRows.Cells[1].Value = sCasoidconsulta;
            //            dRows.Cells[2].Value = validareader("CasoNumero", "CasoId", respuestastring3).Text;
            //            dRows.Cells[3].Value = sTiposolicitudDescrip;
            //            dRows.Cells[4].Value = sEstatusCaso;
            //            dRows.Cells[5].Value = sFechaPresentacion;
            //            dRows.Cells[6].Value = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
            //            dRows.Cells[7].Value = sFechaConcesion;
            //            dRows.Cells[8].Value = validareader("CasoNumConcedida", "CasoId", respuestastring3).Text;
            //            dRows.Cells[9].Value = sQueryintere;
            //            dRows.Cells[10].Value = validareader("CasoTituloespanol", "CasoId", respuestastring3).Text;
            //            dRows.Cells[11].Value = sClientesdatos;
            //            /*dRows.Cells[12].Value = sPrioridades;
            //            dRows.Cells[13].Value = sReferenciaDescrip;
            //            dRows.Cells[14].Value = sHolderDescrip;*/
            //            dRows.Cells[12].Value = sReferenciaDescrip;
            //            dRows.Cells[13].Value = sHolderDescrip;


            //            if (residuo == 0){
            //                dRows.DefaultCellStyle.BackColor = Color.LightGray;
            //            }
            //            else{
            //                dRows.DefaultCellStyle.BackColor = Color.Azure;
            //            }

            //            dgViewBuscaContenciosos.Rows.Add(dRows);
            //            rowcolor++;
            //            //20220225 Fsalazar llenar y formatear el nuevo grid

            //        }
            //        respuestastring3.Close();
            //        textBox10.Text = rowcolor + "";
            //    }
            //    else
            //    {
            //        MessageBox.Show("Debe buscar por lo menos en un campo de busqueda");
            //    }
            //}
            //catch (Exception E)
            //{
            //    textBox10.Text = rowcolor + "";
            //    MessageBox.Show("Se encontraron más de " + rowcolor + " la busqueda debe ser más especifica.");
            //}


        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listViewCasos_DoubleClick(object sender, EventArgs e)
        {
            String sClavepaiscaso = listViewCasos.SelectedItems[0].SubItems[0].Text;
            String sClasemarcas = listViewCasos.SelectedItems[0].SubItems[1].Text;
            String sCasoIdcaso = listViewCasos.SelectedItems[0].SubItems[2].Text;
            String sCasonumerocaso = listViewCasos.SelectedItems[0].SubItems[3].Text;
            String sTipocaso = listViewCasos.SelectedItems[0].SubItems[4].Text;
            String sExpedientecaso = listViewCasos.SelectedItems[0].SubItems[5].Text;
            String sRegistrocaso = listViewCasos.SelectedItems[0].SubItems[6].Text;
            String sInteresadocaso = listViewCasos.SelectedItems[0].SubItems[7].Text;
            String sTitulodenomcaso = listViewCasos.SelectedItems[0].SubItems[8].Text;
            String sClientecaso = listViewCasos.SelectedItems[0].SubItems[9].Text;
            String sPrioridadcaso = listViewCasos.SelectedItems[0].SubItems[10].Text;
            //String sReferenciacaso = listViewCasos.SelectedItems[0].SubItems[11].Text;


            listViewCasos.Items.Clear();
            tbexpediente.Clear();
            tbInteresado.Clear();
            
            tbDenominacion.Clear();
            tbregistro.Clear();
            tbCliente.Clear();
            tbreferencia.Clear();
            tbCasoid.Clear();
            //fTcontencioso OBJCONTENT = new fTcontencioso(fLoguin, captura, this, sClasemarcas);
            //fTmarcas objmarca = new fTmarcas(fLoguin, captura, this, sCasoIdcaso);
            //OBJCONTENT.Show();
        }

        private void bContencioso_old_FormClosing(object sender, FormClosingEventArgs e)
        {
            captura.Show();
        }

        private void listViewCasos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bContencioso_old_Load(object sender, EventArgs e)
        {

        }

        private void cbTiposolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*// 20220228 Fsalazar Inactivar temporalmente
            if (validaversion(loguin.sVersion))
            {
                return;
            }*/

            conect con = new conect();
            dgViewBuscaContenciosos.Rows.Clear();
            int rowcolor = 0;

            try
            {
                String sQuerybusqueda = " SELECT DISTINCT " +
                                " PS.PaisClave AS PaisClave, " +
                                " CC.CasoId as CasoId, " +
                                " CC.CasoNumero as CasoNumero, " +
                                //" TS.TipoSolicitudDescripcion as TipoSolicitudDescrip, " +
                                " TS.TipoSolicitudDescrip as TipoSolicitudDescrip, " +
                                " DameEstatusCasoDescrip(CC.EstatusCasoId) As Estatuscasodescrip, " +
                                //" DATE_FORMAT(CC.CasoFechaRecepcion, ' %d-%m-%Y') as FechaPresentacion, " +
                                //" DATE_FORMAT(CC.CasoFechaAlta, ' %d-%m-%Y') as FechaPresentacion, " +
                                //fin de modificacion
                                " DATE_FORMAT(CC.CasoFechaRecepcion, ' %d-%m-%Y') as CasoFechaRecepcion, " +
                                " DATE_FORMAT(CC.CasoFechaPresentacion, ' %d-%m-%Y') as CasoFechaPresentacion, " +
                                //Fin de Modificacion
                                " CC.CasoNumeroExpedienteLargo as CasoNumeroExpedienteLargo, " +
                                " DATE_FORMAT(CC.CasoFechaConcesion, ' %d-%m-%Y') as CasoFechaConcesion, " +
                                " CC.CasoNumConcedida as CasoNumConcedida, " +
                                " it.InteresadoNombre as InteresadoNombre, " +
                                " CC.CasoTituloespanol as CasoTituloespanol, " +
                                " Get_Cliente(CC.CasoId) as ClienteNombre, " +
                                " PR.PrioridadNumero as PrioridadNUmero, " +
                                " RF.ReferenciaNombre as ReferenciaNombre, " +
                                " HD.HolderNombre as HolderNombre" +
                                " FROM caso_contencioso CC LEFT JOIN pais PS ON CC.PaisID = PS.PaisID " +
                                " LEFT JOIN tiposolicitud TS ON CC.tiposolicitudId = TS.tiposolicitudID " +
                                " LEFT JOIN prioridad PR ON CC.CasoID = PR.CasoID AND CC.tiposolicitudid= PR.tiposolicitudID " +
                                " LEFT JOIN referencia RF ON CC.CasoID = RF.CasoID AND CC.tiposolicitudid= RF.tiposolicitudID " +
                                " LEFT JOIN casointeresado CI ON CC.CasoID = CI.CasoID AND CC.tiposolicitudid= CI.tiposolicitudID " +
                                " LEFT JOIN interesado IT ON CI.InteresadoID = IT.InteresadoID " +
                                " LEFT JOIN holder HD ON IT.HolderID = HD.HolderId " +
                                " ORDER BY CasoId desc limit " + tbLimitcasos.Text + ";";
                MySqlDataReader respuestastring3 = con.getdatareader(sQuerybusqueda);

                while (respuestastring3.Read())
                {
                    String sCasoidconsulta = validareader("CasoId", "CasoId", respuestastring3).Text;
                    int residuo = rowcolor % 2;

                    //DateTime dFechapresentacion = DateTime.MinValue;
                    DateTime dFechapresentacion = DateTime.MinValue;
                    try
                    {
                        if (validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text != "00-00-0000")
                        {
                            String fecha = validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text;
                            dFechapresentacion = Convert.ToDateTime(fecha);
                        }
                    }
                    catch (Exception exdate)
                    {
                    }

                    DateTime dFechaconcesion = DateTime.MinValue;
                    try
                    {
                        if (validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text != "00-00-0000")
                        {
                            dFechaconcesion = Convert.ToDateTime(validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text);
                        }
                    }
                    catch (Exception exdates)
                    {
                    }




                    DataGridViewRow dRows = (DataGridViewRow)dgViewBuscaContenciosos.Rows[0].Clone();

                        dRows.Cells[0].Value = validareader("PaisClave", "CasoId", respuestastring3).Text;
                        dRows.Cells[1].Value = sCasoidconsulta;
                        dRows.Cells[2].Value = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                        dRows.Cells[3].Value = validareader("TipoSolicitudDescrip", "CasoId", respuestastring3).Text;
                        dRows.Cells[4].Value = validareader("Estatuscasodescrip", "CasoId", respuestastring3).Text;
                        //dRows.Cells[5].Value = validareader("FechaPresentacion", "CasoId", respuestastring3).Text;
                        dRows.Cells[5].Value = dFechapresentacion;
                        dRows.Cells[6].Value = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                        //dRows.Cells[7].Value = validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text;
                        dRows.Cells[7].Value = dFechaconcesion;
                        dRows.Cells[8].Value = validareader("CasoNumConcedida", "CasoId", respuestastring3).Text;
                        dRows.Cells[9].Value = validareader("InteresadoNombre", "CasoId", respuestastring3).Text;
                        dRows.Cells[10].Value = validareader("CasoTituloespanol", "CasoId", respuestastring3).Text;
                        dRows.Cells[11].Value = validareader("ClienteNombre", "CasoId", respuestastring3).Text;
                        /*dRows.Cells[12].Value = validareader("PrioridadNUmero", "CasoId", respuestastring3).Text;
                        dRows.Cells[13].Value = validareader("ReferenciaNombre", "CasoId", respuestastring3).Text;
                        dRows.Cells[14].Value = validareader("HolderNombre", "CasoId", respuestastring3).Text;*/
                        dRows.Cells[12].Value = validareader("ReferenciaNombre", "CasoId", respuestastring3).Text;
                        dRows.Cells[13].Value = validareader("HolderNombre", "CasoId", respuestastring3).Text;

                    if (residuo == 0)
                    {
                        dRows.DefaultCellStyle.BackColor = Color.LightGray;
                        if (validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "")
                        {
                            dRows.Cells[5].Style.ForeColor = Color.Black;
                        }
                        else
                        {
                            dRows.Cells[5].Style.ForeColor = Color.LightGray;
                        }

                        if (validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "")
                        {
                            dRows.Cells[7].Style.ForeColor = Color.Black;
                        }
                        else
                        {
                            dRows.Cells[7].Style.ForeColor = Color.LightGray;
                        }
                    }
                    else
                    {
                        dRows.DefaultCellStyle.BackColor = Color.Azure;
                        if (validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "")
                        {
                            dRows.Cells[5].Style.ForeColor = Color.Black;
                        }
                        else
                        {
                            dRows.Cells[5].Style.ForeColor = Color.Azure;
                        }

                        if (validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "")
                        {
                            dRows.Cells[7].Style.ForeColor = Color.Black;
                        }
                        else
                        {
                            dRows.Cells[7].Style.ForeColor = Color.Azure;
                        }
                    }



                    dgViewBuscaContenciosos.Rows.Add(dRows);
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

        }

 

        private void dgViewBuscaContenciosos_DoubleClick(object sender, EventArgs e)
        {


        }


        private void bContencioso_old_Resize(object sender, EventArgs e)
        {
            //20220301 Agregamos metodo sobre el grid
            dgViewBuscaContenciosos.Location = new Point(this.dgViewBuscaContenciosos.Location.X, 75);
            dgViewBuscaContenciosos.Size = new Size(dgViewBuscaContenciosos.Width, this.Height - 300);
        }

        private void btExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                generaexcel(dgViewBuscaContenciosos);
            }
            catch (Exception E)
            {
                Console.Write("Se canceló la exportación");
                new filelog(fLoguin.sId, E.ToString());

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

        private void dgViewBuscaContenciosos_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (dgViewBuscaContenciosos.SelectedRows != null)
                {
                    String sCasoIdcaso = dgViewBuscaContenciosos.SelectedRows[0].Cells[1].Value.ToString();
                    dgViewBuscaContenciosos.Rows.Clear();
                    tbexpediente.Clear();
                    tbInteresado.Clear();
                    
                    tbDenominacion.Clear();
                    tbregistro.Clear();
                    tbCliente.Clear();
                    tbreferencia.Clear();
                    tbCasoid.Clear();
                    //consultacaso cConsultaid = new consultacaso(loguin, captura, this, sCasoIdcaso);
                    this.Hide();
                    //cConsultaid.Show();

                    //fTcontencioso OBJCONTENT = new fTcontencioso(fLoguin, captura, bContencioso_old, sCasoIdcaso);
                    //OBJCONTENT.Show();


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

        private void dgViewBuscaContenciosos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
