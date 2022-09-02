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
    public partial class Consutlacaso : Form
    {
        public Form1 loguin;
        public captura captura;
        public MySqlDataReader respuestastring3;
        public MySqlDataReader respuestastringint;
        public MySqlDataReader respuestastringprio;
        public int iTiposolicitud;
        public String sGTipocaso;
        public String[] paises = new String[250];
        public String[] paisesclave = new String[250];

        private funcionesdicss funcionesgenerales = new funcionesdicss();
        
        //public static conect con;
        public String Casonumero{get; set;}
        public Consutlacaso(Form1 formlog, captura cap, int iTiposol)
        {
            
            loguin = formlog;
            captura = cap;
            iTiposolicitud = iTiposol;
            sGTipocaso = iTiposol+"";
            InitializeComponent();
            
            try {
                funcionesgenerales.activaaviso(tbAvisoprueba);
                this.BackColor = Color.Pink;
                conect con = new conect();
                this.Text = "Buscar caso (Patentes) :";
                String sQuerytipoSol = "select * from tiposolicitud where TipoSolicitudGrupo= " + iTiposol;//gtipode grupo
                MySqlDataReader respuestastringtoiposl = con.getdatareader(sQuerytipoSol);
                while (respuestastringtoiposl.Read())
                {
                    cbTiposolicitud.Items.Add(validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestastringtoiposl));
                }
                respuestastringtoiposl.Close();
                con.Cerrarconexion();

                //select * from pais;
                conect con2 = new conect();
                String query5 = "select PaisNombre, PaisId, PaisClave from pais order by PaisNombre;";
                MySqlDataReader respuestastringopais = con2.getdatareader(query5);
                //int paisint = 0;
                while (respuestastringopais.Read())
                {
                    ComboboxItem combopias = new ComboboxItem();
                    combopias.Text = validareader("PaisClave", "PaisId", respuestastringopais).Text + " - " + validareader("PaisNombre", "PaisId", respuestastringopais).Text;
                    combopias.Value = validareader("PaisClave", "PaisId", respuestastringopais).Value;


                    //cbPais.Items.Add(validareader("PaisNombre", "PaisId", respuestastringopais));
                    cbPais.Items.Add(combopias);
                    paises[Convert.ToInt32(validareader("PaisClave", "PaisId", respuestastringopais).Value.ToString())] = validareader("PaisClave", "PaisId", respuestastringopais).Text;
                    paisesclave[Convert.ToInt32(validareader("PaisClave", "PaisId", respuestastringopais).Value.ToString())] = validareader("PaisNombre", "PaisId", respuestastringopais).Text;
                    //paisint++;
                }
                respuestastringopais.Close();
                con2.Cerrarconexion();

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
            }
            catch (Exception exs) {
                new filelog("", "" + exs.Message);
            }
            


        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (validaversion(loguin.sVersion))
            {
                return;
            }
            //borramos el listview 
            conect con = new conect();
            //listViewCasos.Items.Clear();
            dgViewBuscapatentes.Rows.Clear();
            int rowcolor = 0;
            
            try {
                String sQuerywhere="";
                String sQuerywherecaso = "";
                String sCampoconsulta = "";
                String stablaconsulta = "";
                String sWhereconsulta = "";
                //!string.IsNullOrEmpty(textBoxCasonumero.Text)
                if (cbTiposolicitud.SelectedItem != null) {
                    sQuerywhere += " AND caso_patente.tiposolicitudid = " + (cbTiposolicitud.SelectedItem as ComboboxItem).Value;
                    sQuerywhere += " AND Tiposolicitud.tiposolicitudid = caso_patente.tiposolicitudid";
                    stablaconsulta += ",Tiposolicitud";
                }
                    

                if (!string.IsNullOrEmpty(tbexpediente.Text)) {
                    sQuerywhere += " AND caso_patente.CasoNumeroExpedienteLargo like '%" + tbexpediente.Text + "%'";
                }
                    

                if (!string.IsNullOrEmpty(tbDenominacion.Text)) {
                    sQuerywhere += " AND ( caso_patente.CasoTituloingles like '%" + tbDenominacion.Text + "%' OR caso_patente.CasoTituloespanol LIKE '%" + tbDenominacion.Text + "%')";
                }
                    

                if (!string.IsNullOrEmpty(tbregistro.Text)) {
                    sQuerywhere += " AND caso_patente.CasoNumConcedida like '%" + tbregistro.Text + "%'";
                }
                    

                if (!string.IsNullOrEmpty(tbCasoid.Text)) {
                    sQuerywhere += " AND caso_patente.CasoId =" + tbCasoid.Text;
                }
                    

                if (!string.IsNullOrEmpty(textBoxCasonumero.Text)) {
                    sQuerywhere += " AND caso_patente.CasoNumero like '%" + textBoxCasonumero.Text + "%'";
                }
                    

                if (cbPais.SelectedItem != null) {
                    sQuerywhere += " AND caso_patente.PaisId = '" + (cbPais.SelectedItem as ComboboxItem).Value + "'";
                    sQuerywhere += " AND Pais.PaisId = caso_patente.PaisId";
                    stablaconsulta += ",Pais";
                }

                if (!string.IsNullOrEmpty(tbInteresado.Text))//filtro para interesado
                {
                    sQuerywhere += " AND GET_INTERESADOS_TIPOSOL(Casoid, caso_patente.TipoSolicitudId) like '%" + tbInteresado.Text + "%'";
                }
                
                if (!string.IsNullOrEmpty(tbPrioridad.Text))//filtro para prioridades
                {
                    sQuerywhere += " AND GET_PRIORIDAD(caso_patente.Casoid, caso_patente.TipoSolicitudId) like '%" + tbPrioridad.Text + "%'";
                }

                if (!string.IsNullOrEmpty(tbreferencia.Text))//filtro para referencias
                {
                    sQuerywhere += " AND Get_Referencia(caso_patente.Casoid, caso_patente.TipoSolicitudId) like '%" + tbreferencia.Text + "%'";
                }
                //tbHolder
                //if (!string.IsNullOrEmpty(tbHolder_.Text))//filtro para Holder
                //{
                //    sQuerywhere += " AND Dameelholder_patentes_casoid(caso_patente.CasoId, caso_patente.TipoSolicitudId) like '%" + tbHolder_.Text + "%'";
                //}

                if (cbHolder.SelectedItem != null)
                {
                    sQuerywhere += " AND Dameelholder_patentes_all_casoid(caso_patente.CasoId, caso_patente.TipoSolicitudId) = '" + (cbHolder.SelectedItem as ComboboxItem).Value + "'";

                }

                if (!string.IsNullOrEmpty(tbCliente.Text))//filtro para Cliente
                {
                    sQuerywhere += " AND GET_CLIENTE_TIPOSOL(CasoId, caso_patente.TipoSolicitudId) like '%" + tbCliente.Text + "%'";
                }

                if (sQuerywhere != "" || sQuerywherecaso != "")
                {
                    sQuerywhere = sQuerywhere.Substring(4, sQuerywhere.Length - 4);
                    String sQuerybusqueda = "";
                    sQuerybusqueda =    " SELECT  " +
                                        " caso_patente.CasoId as CasoId, " +
                                        " GET_CLIENTE_TIPOSOL(CasoId, caso_patente.TipoSolicitudId) as cliente_Nombre, " +
                                        " caso_patente.TipoSolicitudId as TipoSolicitudId, " +
                                        " caso_patente.SubTipoSolicitudId as SubTipoSolicitudId, " +
                                        " caso_patente.TipoPctId as TipoPctId, " +
                                        " caso_patente.CasoTituloespanol as CasoTituloespanol, " +
                                        " caso_patente.CasoTituloingles as CasoTituloingles, " +
                                        " caso_patente.IdiomaId as IdiomaId, " +
                                        " DATE_FORMAT(caso_patente.CasoFechaConcesion, ' %d-%m-%Y') as CasoFechaConcesion, " +
                                        " DATE_FORMAT(caso_patente.CasoFechaRecepcion, ' %d-%m-%Y') as CasoFechaRecepcion, " +
                                        " caso_patente.CasoFechaVigencia as CasoFechaVigencia, " +
                                        " caso_patente.CasoFechaPublicacionSolicitud as CasoFechaPublicacionSolicitud, " +
                                        " caso_patente.CasoFechaLegal as CasoFechaLegal, " +
                                        " caso_patente.CasoNumConcedida as CasoNumConcedida, " +
                                        " caso_patente.CasoNumeroExpedienteLargo as CasoNumeroExpedienteLargo, " +
                                        " caso_patente.CasoNumero as CasoNumero, " +
                                        " caso_patente.ResponsableId as ResponsableId, " +
                                        " caso_patente.CasoTipoCaptura as CasoTipoCaptura, " +
                                        " caso_patente.CasoTitular as CasoTitular, " +
                                        " caso_patente.EstatusCasoId as EstatusCasoId, " +
                                        " caso_patente.UsuarioId as UsuarioId, " +
                                        " caso_patente.AreaImpiId as AreaImpiId, " +
                                        " caso_patente.CasoFechaInternacional as CasoFechaInternacional, " +
                                        " caso_patente.PaisId as PaisId, " +
                                        " caso_patente.CasoFechaPruebaUsoSig as CasoFechaPruebaUsoSig, " +
                                        " caso_patente.CasoFechaFilingCliente as CasoFechaFilingCliente, " +
                                        " caso_patente.CasoFechaFilingSistema as CasoFechaFilingSistema, " +
                                        " caso_patente.CasoFechaDivulgacionPrevia as CasoFechaDivulgacionPrevia, " +
                                        " caso_patente.CasoFechaCartaCliente as CasoFechaCartaCliente, " +
                                        " DameEstatusCasoDescrip(caso_patente.EstatusCasoId) As Estatuscasodescrip, " +
                                        " Get_prioridad(caso_patente.Casoid, caso_patente.TipoSolicitudId) as PrioridadNumero, " +
                                        " Get_Interesados_tiposol(Casoid, caso_patente.TipoSolicitudId) as InteresadoNombre, " +
                                        " Get_Tipodesolicitud(caso_patente.Casoid) as TipoSolicitudDescrip, " +
                                        " Get_Referencia(caso_patente.Casoid, caso_patente.TipoSolicitudId) as referencia, " +
                                        " Get_Paisclave_patente(caso_patente.Casoid) as PaisClave, " +
                                        " Dameelholder_patentes_all_casoid(caso_patente.CasoId, caso_patente.TipoSolicitudId) As holdernombre," +
                                        " caso_patente.Divicionalid as Divicionalid " +
                                        " FROM " +
                                        " caso_patente" +
                                            stablaconsulta +
                                        " WHERE " +
                                            sQuerywhere ;/*Agregar el limit*/
                    //" Dameelholder_casoid(caso_patente.CasoId, caso_patente.TipoSolicitudId) As holdernombre," +
                    //" DameEstatusCasoDescrip(caso_patente.EstatusCasoId) As Estatuscasodescrip, " +
                    //" DATE_FORMAT(caso_patente.CasoFechaConcesion, ' %d-%m-%Y') as CasoFechaConcesion, " +
                    //" DATE_FORMAT(caso_patente.CasoFechaRecepcion, ' %d-%m-%Y') as CasoFechaRecepcion, " +

                    respuestastring3 = con.getdatareader(sQuerybusqueda);
                    while (respuestastring3.Read())
                    {
                        String sCasoidconsulta = validareader("CasoId", "CasoId", respuestastring3).Text;

                        String sPrioridades = validareader("PrioridadNumero", "CasoId", respuestastring3).Text; ;
                        //conect con2 = new conect();
                        //String sQueryprio = "select PrioridadNumero, PrioridadId from  prioridad where casoid =" + sCasoidconsulta;
                        //MySqlDataReader respuestastringprio = con2.getdatareader(sQueryprio);
                        ////int paisint = 0;
                        //while (respuestastringprio.Read())
                        //{
                        //    sPrioridades += validareader("PrioridadNumero", "PrioridadId", respuestastringprio)+" ~ ";
                        //}
                        //respuestastringprio.Close();
                        //con2.Cerrarconexion();
                        //String sInteresadosconsul = " SELECT  " +
                        //                            "     * " +
                        //                            " FROM " +
                        //                            "     interesado, " +
                        //                            "     casointeresado " +
                        //                            " WHERE " +
                        //                            "     casointeresado.InteresadoId = interesado.InteresadoID " +
                        //                            "     AND casointeresado.CasoId =" + sCasoidconsulta;
                        //String sQueryintere = "";
                        //conect con3 = new conect();
                        //MySqlDataReader respuestastringinteresado = con3.getdatareader(sInteresadosconsul);
                        ////int paisint = 0;
                        //while (respuestastringinteresado.Read())
                        //{
                        //    sQueryintere += validareader("InteresadoNombre", "InteresadoId", respuestastringinteresado) + " ~ ";
                        //}
                        //respuestastringinteresado.Close();
                        //con3.Cerrarconexion();
                        int residuo = rowcolor % 2;
                        //prueba de todos los casos
                        //País del caso
                        //conect con4 = new conect();
                        //MySqlDataReader resp_paiscaso = con4.getdatareader("select * from Pais where PaisId = " + validareader("PaisId", "PaisId", respuestastring3).Text);
                        //String sPaisclave = "";
                        //while (resp_paiscaso.Read())
                        //{
                        //    sPaisclave = validareader("PaisClave", "PaisId", resp_paiscaso).Text;
                        //}
                        //resp_paiscaso.Close();
                        //con4.Cerrarconexion();

                        //DataGridViewRow dgvrRenglon = new DataGridViewRow();
                        DateTime dFechapresentacion = DateTime.MinValue;
                        try {
                            if (validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text != "00-00-0000")
                            {
                                String fecha = validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text;
                                dFechapresentacion = Convert.ToDateTime(fecha);
                            }
                        }
                        catch (Exception exdate) { 
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
                        //String sFechapresentacion = validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text;
                        //String sFechaconcesion = validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text;

                        DataGridViewRow dRows = (DataGridViewRow)dgViewBuscapatentes.Rows[0].Clone();
                        dRows.Cells[0].Value = validareader("PaisClave", "CasoId", respuestastring3).Text;
                        dRows.Cells[1].Value = sCasoidconsulta;
                        dRows.Cells[2].Value = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                        dRows.Cells[3].Value = validareader("TipoSolicitudDescrip", "CasoId", respuestastring3).Text;

                        dRows.Cells[4].Value = validareader("Estatuscasodescrip", "CasoId", respuestastring3).Text;//estatus
                        
                        dRows.Cells[5].Value = dFechapresentacion; //fecha presentacion

                        dRows.Cells[6].Value = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;


                        dRows.Cells[7].Value = dFechaconcesion;//fecha Concesión


                        dRows.Cells[8].Value = validareader("CasoNumConcedida", "CasoId", respuestastring3).Text;
                        dRows.Cells[9].Value = validareader("InteresadoNombre", "CasoId", respuestastring3).Text;
                        dRows.Cells[10].Value = validareader("CasoTituloespanol", "CasoId", respuestastring3).Text + "/ " + validareader("CasoTituloingles", "CasoId", respuestastring3).Text;
                        dRows.Cells[11].Value = validareader("cliente_Nombre", "CasoId", respuestastring3).Text;
                        dRows.Cells[12].Value = sPrioridades;
                        dRows.Cells[13].Value = validareader("referencia", "CasoId", respuestastring3).Text;
                        dRows.Cells[14].Value = validareader("holdernombre", "CasoId", respuestastring3).Text;
                        dRows.Cells[15].Value = validareader("Divicionalid", "CasoId", respuestastring3).Text;

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
                                //dRows.Cells[5].Value = 0;
                            }

                            if (validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                dRows.Cells[7].Style.ForeColor = Color.Black;
                            }
                            else
                            {
                                dRows.Cells[7].Style.ForeColor = Color.LightGray;
                                //dRows.Cells[7].Value = 0;
                            }
                        }
                        else{
                            dRows.DefaultCellStyle.BackColor = Color.Azure;
                            if (validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                dRows.Cells[5].Style.ForeColor = Color.Black;
                            }
                            else
                            {
                                dRows.Cells[5].Style.ForeColor = Color.Azure;
                                //dRows.Cells[5].Value = 0;
                            }

                            if (validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                dRows.Cells[7].Style.ForeColor = Color.Black;
                            }
                            else
                            {
                                dRows.Cells[7].Style.ForeColor = Color.Azure;
                                //dRows.Cells[7].Value = 0;
                            }
                        }


                        ListViewItem listaitems = new ListViewItem(validareader("PaisClave", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(sCasoidconsulta);
                        listaitems.SubItems.Add(validareader("CasoNumero", "CasoId", respuestastring3).Text);

                        //conect con5 = new conect();
                        //String sQuerytiposolicituddesc = "select * from Tiposolicitud where Tiposolicitud.tiposolicitudid =" + validareader("tiposolicitudid", "tiposolicitudid", respuestastring3).Text;
                        //MySqlDataReader resp_tiposol = con5.getdatareader(sQuerytiposolicituddesc);
                        ////int paisint = 0;
                        //String sTiposoldescrip = "";
                        //while (resp_tiposol.Read())
                        //{
                        //    sTiposoldescrip += validareader("TipoSolicitudDescrip", "tiposolicitudid", resp_tiposol);
                        //}
                        //resp_tiposol.Close();
                        //con5.Cerrarconexion();
                        //listaitems.SubItems.Add(validareader("TipoSolicitudDescrip", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(validareader("TipoSolicitudDescrip", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(validareader("CasoNumConcedida", "CasoId", respuestastring3).Text);
                        //listaitems.SubItems.Add(validareader("InteresadoNombre", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(validareader("InteresadoNombre", "CasoId", respuestastring3).Text);//
                        listaitems.SubItems.Add(validareader("CasoTituloespanol", "CasoId", respuestastring3).Text + "/ " + validareader("CasoTituloingles", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(validareader("cliente_Nombre", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(sPrioridades);
                        listaitems.SubItems.Add(validareader("referencia", "CasoId", respuestastring3).Text);
                        if (residuo == 0)
                        {
                            listaitems.BackColor = Color.LightGray;
                        }
                        else
                        {
                            listaitems.BackColor = Color.Azure;
                        }
                        //listViewCasos.Items.Add(listaitems);
                        //this.listViewCasos.FullRowSelect = true;
                        rowcolor++;


                        dgViewBuscapatentes.Rows.Add(dRows);
                    }
                    respuestastring3.Close();
                    textBox10.Text = rowcolor + "";
                }
                else {
                    MessageBox.Show("Debe buscar por lo menos en un campo de busqueda");
                }

                
                
            }
            catch (Exception E)
            {
                if (respuestastring3 != null)
                    respuestastring3.Close();
                //con.Cerrarconexion();
                textBox10.Text = rowcolor + "";
                MessageBox.Show("Se encontraron más de "+ rowcolor +" la busqueda debe ser más especifica.");

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

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void listViewCasos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // MessageBox.Show(listViewCasos.Items + "");
            String sCasoIdcaso = dgViewBuscapatentes.SelectedRows[0].Cells[1].Value.ToString();
            //String sClavepaiscaso = listViewCasos.SelectedItems[0].SubItems[0].Text;
            //String sCasoIdcaso = listViewCasos.SelectedItems[0].SubItems[1].Text;
            //String sCasonumerocaso = listViewCasos.SelectedItems[0].SubItems[2].Text;
            //String sTipocaso = listViewCasos.SelectedItems[0].SubItems[3].Text;
            //String sExpedientecaso = listViewCasos.SelectedItems[0].SubItems[4].Text;
            //String sRegistrocaso = listViewCasos.SelectedItems[0].SubItems[5].Text;
            //String sInteresadocaso = listViewCasos.SelectedItems[0].SubItems[6].Text;
            //String sTitulodenomcaso = listViewCasos.SelectedItems[0].SubItems[7].Text;
            //String sClientecaso = listViewCasos.SelectedItems[0].SubItems[8].Text;
            //String sPrioridadcaso = listViewCasos.SelectedItems[0].SubItems[9].Text;
            //String sReferenciacaso = listViewCasos.SelectedItems[0].SubItems[10].Text;


            //listViewCasos.Items.Clear();
            tbexpediente.Clear();
            tbInteresado.Clear();
            tbPrioridad.Clear();
            tbDenominacion.Clear();
            tbregistro.Clear();
            tbCliente.Clear();
            tbreferencia.Clear();
            tbCasoid.Clear();
            consultacaso cConsultaid = new consultacaso(loguin, captura, this, sCasoIdcaso);
            this.Hide();
            cConsultaid.Show();
            //switch (sTipocaso)
            //{
            //    case "Patente":
            //        {
                        
            //        } break;
            //    case "Modelo de utilidad":
            //        {
            //            //MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Dis. industrial (Modelo)":
            //        {
            //            //MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Dis. industrial (Dibujo)":
            //        {
            //            //MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Patente PCT(ante wipo)":
            //        {

            //        } break;
            //    case "Variedad vegetal":
            //        {
            //            //MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Marca":
            //        {
            //            fTmarcas objmarca = new fTmarcas(loguin, captura, this, sCasoIdcaso);
            //            objmarca.Show();
            //        } break;
            //    case "Nombre Comercial":
            //        {
            //           // MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Aviso Comercial":
            //        {
            //            //MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Procedimiento contencioso":
            //        {
            //            fTcontencioso obj = new fTcontencioso(loguin, captura, this, sCasoIdcaso);
            //            obj.Show();
            //            conect con = new conect();
            //        } break;
            //    case "Juicio de Nulidad":
            //        {
            //            //MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Juicio de Amparo":
            //        {
            //            //MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Consulta":
            //        {
            //            //MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Oposición":
            //        {
            //            fToposiciones obbj = new fToposiciones(loguin, captura, this, sCasoIdcaso);
            //            obbj.Show();
                        
            //        } break;
            //    case "Registro de Obra":
            //        {
            //            fTderechosdeautor objDerechosaut = new fTderechosdeautor(loguin, captura, this, sCasoIdcaso);
            //            //consultacaso cConsultaid = new consultacaso(loguin, captura, this, sCasoIdcaso);
            //            //this.Hide();
            //            objDerechosaut.Show();
            //        } break;
            //    case "Reserva de Derechos":
            //        {
                        
            //            fTreservaderechos objReserva = new fTreservaderechos(loguin, captura, this, sCasoIdcaso);
            //            //consultacaso cConsultaid = new consultacaso(loguin, captura, this, sCasoIdcaso);
            //            //this.Hide();
            //            objReserva.Show();

            //        } break;
            //    case "Recurso de revisión":
            //        {
            //            MessageBox.Show("En desarrollo");
            //        } break;
            //    case "trazado de circuito":
            //        {
            //            MessageBox.Show("En desarrollo");
            //        } break;
            //    case "nombre de dominio":
            //        {
            //            MessageBox.Show("En desarrollo");
            //        } break;
            //}
        }

        private void textBoxCasonumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            //listViewCasos.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            captura.Show();
            this.Close();
        }

        private void listViewCasos_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            MessageBox.Show("pendiente ordenar columna");
        }

        private void listViewCasos_KeyUp(object sender, KeyEventArgs e)
        {
            //if (sender != this.listViewCasos) return;

            //if (e.Control && e.KeyCode == Keys.C)
            //    CopySelectedValuesToClipboard(); 
        }
        private void CopySelectedValuesToClipboard()
        {
            //var builder = new StringBuilder();
            //foreach (ListViewItem item in listViewCasos.SelectedItems)
            //    builder.AppendLine(item.SubItems[0].Text + "~" + item.SubItems[1].Text + item.SubItems[2].Text + item.SubItems[3].Text + " " + item.SubItems[4].Text + "~" + item.SubItems[5].Text + "~" + item.SubItems[6].Text + "~" + item.SubItems[7].Text + "~" + item.SubItems[8].Text + "~" + item.SubItems[9].Text + "~" + item.SubItems[10].Text);//aqui agregamos todos los  campos que queramos copiar del listview
            //Clipboard.SetText(builder.ToString());
        }


        private void cbPais_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            try {
                String valorcombo = (cbPais.SelectedItem as ComboboxItem).Value.ToString();
                int iValuepais = Convert.ToInt32((cbPais.SelectedItem as ComboboxItem).Value.ToString());
                tbClave.Text = paises[iValuepais];
            }
            catch (Exception Ex){
                new filelog("", "E:" + Ex.Message);
            }
            
        }

        private void textBoxCasonumero_KeyDown(object sender, KeyEventArgs e)
        {
            try { 
                if (e.KeyCode == Keys.Enter)
                {
                    //MessageBox.Show("Mensaje enviado");
                    button1_Click(sender, e);
                    if (textBox10.Text=="1")
                    {
                        // MessageBox.Show(listViewCasos.Items + "");
                        //System.Threading.Thread.Sleep(100);
                        String sCasoIdcaso = dgViewBuscapatentes.Rows[0].Cells[1].Value.ToString();//listViewCasos.Items[0].SubItems[1].Text;
                        dgViewBuscapatentes.Rows.Clear();
                        tbexpediente.Clear();
                        tbInteresado.Clear();
                        tbPrioridad.Clear();
                        tbDenominacion.Clear();
                        tbregistro.Clear();
                        tbCliente.Clear();
                        tbreferencia.Clear();
                        tbCasoid.Clear();
                        consultacaso cConsultaid = new consultacaso(loguin, captura, this, sCasoIdcaso);
                        this.Hide();
                        cConsultaid.Show();
                    }
                
                }
            }catch(Exception E){
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (validaversion(loguin.sVersion))
            {
                return;
            }
            conect con = new conect();
            //listViewCasos.Items.Clear();
            dgViewBuscapatentes.Rows.Clear();
            int rowcolor = 0;

            try
            {
                String sQuerywhere = "";
                String sQuerywherecaso = "";
                String sCampoconsulta = "";
                String stablaconsulta = "";
                String sWhereconsulta = "";

                //20220427FVS Agregra Filtro Estatus en Ultimos Casos
                if (cbFiltroestatus.SelectedItem != null)
                {
                    sQuerywhere += " caso_patente.EstatusCasoId = '" + (cbFiltroestatus.SelectedItem as ComboboxItem).Value + "'";
                }
                //20220427FVS Agregra Filtro Estatus en Ultimos Casos

                String sQuerybusqueda = " SELECT  " +
                                " caso_patente.CasoId as CasoId, " +
                                " GET_CLIENTE_TIPOSOL(CasoId, caso_patente.TipoSolicitudId) as cliente_Nombre, " +
                                " caso_patente.TipoSolicitudId as TipoSolicitudId, " +
                                " caso_patente.SubTipoSolicitudId as SubTipoSolicitudId, " +
                                " caso_patente.TipoPctId as TipoPctId, " +
                                " caso_patente.CasoTituloespanol as CasoTituloespanol, " +
                                " caso_patente.CasoTituloingles as CasoTituloingles, " +
                                " caso_patente.IdiomaId as IdiomaId, " +
                                //" caso_patente.CasoFechaConcesion as CasoFechaConcesion, " +
                                //" caso_patente.CasoFechaRecepcion as CasoFechaRecepcion, " +
                                " DATE_FORMAT(caso_patente.CasoFechaConcesion, ' %d-%m-%Y') as CasoFechaConcesion, " +
                                " DATE_FORMAT(caso_patente.CasoFechaRecepcion, ' %d-%m-%Y') as CasoFechaRecepcion, " +
                                " caso_patente.CasoFechaVigencia as CasoFechaVigencia, " +
                                " caso_patente.CasoFechaPublicacionSolicitud as CasoFechaPublicacionSolicitud, " +
                                " caso_patente.CasoFechaLegal as CasoFechaLegal, " +
                                " caso_patente.CasoNumConcedida as CasoNumConcedida, " +
                                " caso_patente.CasoNumeroExpedienteLargo as CasoNumeroExpedienteLargo, " +
                                " caso_patente.CasoNumero as CasoNumero, " +
                                " caso_patente.ResponsableId as ResponsableId, " +
                                " caso_patente.CasoTipoCaptura as CasoTipoCaptura, " +
                                " caso_patente.CasoTitular as CasoTitular, " +
                                " caso_patente.EstatusCasoId as EstatusCasoId, " +
                                " caso_patente.UsuarioId as UsuarioId, " +
                                " caso_patente.AreaImpiId as AreaImpiId, " +
                                " caso_patente.CasoFechaInternacional as CasoFechaInternacional, " +
                                " caso_patente.PaisId as PaisId, " +
                                " caso_patente.CasoFechaPruebaUsoSig as CasoFechaPruebaUsoSig, " +
                                " caso_patente.CasoFechaFilingCliente as CasoFechaFilingCliente, " +
                                " caso_patente.CasoFechaFilingSistema as CasoFechaFilingSistema, " +
                                " caso_patente.CasoFechaDivulgacionPrevia as CasoFechaDivulgacionPrevia, " +
                                " caso_patente.CasoFechaCartaCliente as CasoFechaCartaCliente, " +
                                " Dameelholder_patentes_all_casoid(caso_patente.CasoId, caso_patente.TipoSolicitudId) As holdernombre," +
                                " DameEstatusCasoDescrip(caso_patente.EstatusCasoId) As Estatuscasodescrip, " +
                                " Get_prioridad(caso_patente.Casoid, caso_patente.TipoSolicitudId) as PrioridadNumero, " +
                                " Get_Interesados_tiposol(Casoid, TipoSolicitudId) as InteresadoNombre, " +
                                " Get_Tipodesolicitud(Casoid) as TipoSolicitudDescrip, " +
                                " Get_Referencia(Casoid, TipoSolicitudId) as referencia, " +
                                " Get_Paisclave_patente(Casoid) as PaisClave, " +
                                " caso_patente.Divicionalid as Divicionalid " +
                                " FROM caso_patente ";

                                //" FROM  caso_patente " +
                                //" order by CasoId desc limit " + tbLimitcasos.Text + ";";
                if (sQuerywhere != "") 
                    {
                        sQuerybusqueda = sQuerybusqueda + " WHERE " + sQuerywhere;
                    }
                    sQuerybusqueda = sQuerybusqueda + " order by CasoId desc limit " + tbLimitcasos.Text + ";";
                    sQuerywhere = "";



                    respuestastring3 = con.getdatareader(sQuerybusqueda);
                    while (respuestastring3.Read())
                    {
                        String sCasoidconsulta = validareader("CasoId", "CasoId", respuestastring3).Text;
                        int residuo = rowcolor % 2;
                        DateTime dFechapresentacion = DateTime.MinValue;
                        String fecha = "";
                        try
                        {
                            if (validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                fecha = validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim();
                                dFechapresentacion = Convert.ToDateTime(fecha);
                            }
                        }
                        catch (Exception exdate)
                        {
                            new filelog("linea 670: "+fecha, exdate.Message);
                        }


                        DateTime dFechaconcesion = DateTime.MinValue;
                        String sdatofecha = "";
                        try
                        {
                            if (validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                sdatofecha = validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim();
                                dFechaconcesion = Convert.ToDateTime(sdatofecha);
                            }
                        }
                        catch (Exception exdates)
                        {
                        new filelog("linea 683: fecha:"+ sdatofecha, exdates.StackTrace);
                        }
                        //String sFechapresentacion = validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text;
                        //String sFechaconcesion = validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text;
                        DataGridViewRow dRows = (DataGridViewRow)dgViewBuscapatentes.Rows[0].Clone();
                        try {
                        
                            dRows.Cells[0].Value = validareader("PaisClave", "CasoId", respuestastring3).Text;
                            dRows.Cells[1].Value = sCasoidconsulta;
                            dRows.Cells[2].Value = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                            dRows.Cells[3].Value = validareader("TipoSolicitudDescrip", "CasoId", respuestastring3).Text;
                            dRows.Cells[4].Value = validareader("Estatuscasodescrip", "CasoId", respuestastring3).Text;//estatus
                            dRows.Cells[5].Value = dFechapresentacion;// dFechapresentacion; //fecha presentacion
                            //if (validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "")
                            //{
                            //    dRows.Cells[5].Value = dFechapresentacion;// dFechapresentacion; //fecha presentacion
                            //}
                            //else {
                            //    dRows.Cells[5].Value = DBNull.Value;// dFechapresentacion; //fecha presentacion
                            //}
                            dRows.Cells[6].Value = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                            dRows.Cells[7].Value = dFechaconcesion;//fecha Concesión    
                            //if (validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "")
                            //{
                            //    dRows.Cells[7].Value = dFechaconcesion;//fecha Concesión    
                            //}
                            //else {
                            //    dRows.Cells[7].Value = DBNull.Value;
                            //}
                            
                            dRows.Cells[8].Value = validareader("CasoNumConcedida", "CasoId", respuestastring3).Text;
                            dRows.Cells[9].Value = validareader("InteresadoNombre", "CasoId", respuestastring3).Text;
                            dRows.Cells[10].Value = validareader("CasoTituloespanol", "CasoId", respuestastring3).Text + "/ " + validareader("CasoTituloingles", "CasoId", respuestastring3).Text;
                            dRows.Cells[11].Value = validareader("cliente_Nombre", "CasoId", respuestastring3).Text;
                            dRows.Cells[12].Value = validareader("PrioridadNumero", "CasoId", respuestastring3).Text;
                            dRows.Cells[13].Value = validareader("referencia", "CasoId", respuestastring3).Text;
                            dRows.Cells[14].Value = validareader("holdernombre", "CasoId", respuestastring3).Text;
                            dRows.Cells[15].Value = validareader("Divicionalid", "CasoId", respuestastring3).Text;

                        } catch (Exception exs) {
                            new filelog("linea 706", exs.StackTrace);
                        }
                        

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
                                //dRows.Cells[5].Value = 0;
                            }

                            if (validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                dRows.Cells[7].Style.ForeColor = Color.Black;
                            }else
                            {
                                dRows.Cells[7].Style.ForeColor = Color.LightGray;
                                //dRows.Cells[7].Value = 0;
                            }
                        }else{
                            dRows.DefaultCellStyle.BackColor = Color.Azure;
                            if (validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                dRows.Cells[5].Style.ForeColor = Color.Black;
                            }
                            else {
                                dRows.Cells[5].Style.ForeColor = Color.Azure;
                                //dRows.Cells[5].Value = 0;
                            }

                            if (validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                dRows.Cells[7].Style.ForeColor = Color.Black;
                            }else {
                                dRows.Cells[7].Style.ForeColor = Color.Azure;
                                //dRows.Cells[7].Value = 0;
                            }
                        }
                        dgViewBuscapatentes.Rows.Add(dRows);
                        rowcolor++;
                    }
                    respuestastring3.Close();
                    con.Cerrarconexion();
                    textBox10.Text = rowcolor + "";
            }
            catch (Exception E)
            {
                if (respuestastring3 != null)
                    respuestastring3.Close();
                //con.Cerrarconexion();
                textBox10.Text = rowcolor + "";
                MessageBox.Show("Se encontraron más de " + rowcolor + " la busqueda debe ser más especifica.");

            }

        }

        private void tbClave_TextChanged(object sender, EventArgs e)
        {
            //habilitaremos para buscar clave por país
        }

        private void tbClave_TextChanged_1(object sender, EventArgs e)
        {
            try {
                if (tbClave.Text.Length >1) { 
                
                    String valorclave = tbClave.Text;
                    int index = Array.IndexOf(paises, valorclave);
                    cbPais.Text = tbClave.Text+" - "+paisesclave[index];
                }
                //int iValuepais = Convert.ToInt32((cbPais.SelectedItem as ComboboxItem).Value.ToString());
                //tbClave.Text = paises[iValuepais];
            }
            catch (Exception Ex) { 
            
            }
            
        }

        private void Consutlacaso_FormClosing(object sender, FormClosingEventArgs e)
        {
            captura.Show();
        }

        private void tbLimitcasos_KeyDown(object sender, KeyEventArgs e)
        {
            try {
                if (e.KeyCode == Keys.Enter)
                {
                    button2.PerformClick();
                }
            }
            catch (Exception exs) { 
            }
            
        }

        private void cbTiposolicitud_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //MessageBox.Show("Mensaje enviado");
                    button1_Click(sender, e);
                    if (textBox10.Text == "1")
                    {
                        // MessageBox.Show(listViewCasos.Items + "");
                        //System.Threading.Thread.Sleep(100);
                        String sCasoIdcaso = dgViewBuscapatentes.Rows[0].Cells[1].Value.ToString();//listViewCasos.Items[0].SubItems[1].Text;
                        dgViewBuscapatentes.Rows.Clear();
                        tbexpediente.Clear();
                        tbInteresado.Clear();
                        tbPrioridad.Clear();
                        tbDenominacion.Clear();
                        tbregistro.Clear();
                        tbCliente.Clear();
                        tbreferencia.Clear();
                        tbCasoid.Clear();
                        consultacaso cConsultaid = new consultacaso(loguin, captura, this, sCasoIdcaso);
                        this.Hide();
                        cConsultaid.Show();
                    }

                }
            }
            catch (Exception E)
            {

            }
        }

        private void Consutlacaso_Resize(object sender, EventArgs e)
        { 
            dgViewBuscapatentes.Location = new Point(this.dgViewBuscapatentes.Location.X, 75);
            dgViewBuscapatentes.Size = new Size(dgViewBuscapatentes.Width, this.Height - 300);
        }

        private void dgViewBuscapatentes_DoubleClick(object sender, EventArgs e)
        {
            try {
                if (dgViewBuscapatentes.SelectedRows != null)
                {

                    String sCasoIdcaso = dgViewBuscapatentes.SelectedRows[0].Cells[1].Value.ToString();//listViewCasos.Items[0].SubItems[1].Text;
                    dgViewBuscapatentes.Rows.Clear();
                    tbexpediente.Clear();
                    tbInteresado.Clear();
                    tbPrioridad.Clear();
                    tbDenominacion.Clear();
                    tbregistro.Clear();
                    tbCliente.Clear();
                    tbreferencia.Clear();
                    tbCasoid.Clear();
                    consultacaso cConsultaid = new consultacaso(loguin, captura, this, sCasoIdcaso);
                    this.Hide();
                    cConsultaid.Show();
                }
                else {

                    MessageBox.Show("Debe seleccionar un caso.");
                }
            }
            catch (Exception exs) {
                new filelog("Mensaje:", " :"+exs.Message);
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

                        if (sFormat=="d" && sValor != "01/01/0001 12:00:00 a. m." && sValor !="") {
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
                        else {
                            if (sValor == "01/01/0001")
                            {
                                sValor = "";
                            }
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
        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                generaexcel(dgViewBuscapatentes);
                //var excelApp = new Excel.Application();
                //excelApp.Visible = true;
                ////Crea un nuevo libro
                //excelApp.Workbooks.Add();
                ////Crear una hoja
                //Excel._Worksheet workSheet = excelApp.ActiveSheet;
                ////En versiones anteriores de C# se requiere una conversión explícita
                ////Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;
                ////Estableciendo los encabezados de columna
                //workSheet.Cells[3, "A"] = "Pais";
                //workSheet.Cells[3, "B"] = "Casoid";
                //workSheet.Cells[3, "C"] = "Caso";
                //workSheet.Cells[3, "D"] = "Tipo";
                //workSheet.Cells[3, "E"] = "Estatus";
                //workSheet.Cells[3, "F"] = "Fecha Presentación";
                //workSheet.Cells[3, "G"] = "Expediente";
                //workSheet.Cells[3, "H"] = "Fecha Concesión";
                //workSheet.Cells[3, "I"] = "Registro";
                //workSheet.Cells[3, "J"] = "Interesado";
                //workSheet.Cells[3, "K"] = "Título/denominación";
                //workSheet.Cells[3, "L"] = "Cliente";
                //workSheet.Cells[3, "M"] = "Prioridad";
                //workSheet.Cells[3, "N"] = "Referencia";
                //workSheet.Cells[3, "O"] = "Holder";
                //workSheet.Cells[3, "P"] = "Divicional";


                //workSheet.Cells[1, "A"] = "Casos king: ";
                //workSheet.Range["A1", "F1"].Merge();
                //workSheet.Range["A1", "F1"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //workSheet.Range["A1", "F1"].Font.Bold = true;
                //workSheet.Range["A1", "F1"].Font.Size = 20;
                //var row = 3;

                //for (int i = 0; i < dgViewBuscapatentes.Rows.Count; i++)
                //{
                //    workSheet.Cells[i + 4, "A"] = dgViewBuscapatentes.Rows[i].Cells[0].Value;
                //    workSheet.Cells[i + 4, "B"] = dgViewBuscapatentes.Rows[i].Cells[1].Value;
                //    workSheet.Cells[i + 4, "C"] = dgViewBuscapatentes.Rows[i].Cells[2].Value;
                //    workSheet.Cells[i + 4, "D"] = dgViewBuscapatentes.Rows[i].Cells[3].Value;
                //    workSheet.Cells[i + 4, "E"] = dgViewBuscapatentes.Rows[i].Cells[4].Value;
                //    workSheet.Cells[i + 4, "F"] = dgViewBuscapatentes.Rows[i].Cells[5].Value;
                //    workSheet.Cells[i + 4, "G"] = dgViewBuscapatentes.Rows[i].Cells[6].Value;
                //    workSheet.Cells[i + 4, "H"] = dgViewBuscapatentes.Rows[i].Cells[7].Value;
                //    workSheet.Cells[i + 4, "I"] = dgViewBuscapatentes.Rows[i].Cells[8].Value;
                //    workSheet.Cells[i + 4, "J"] = dgViewBuscapatentes.Rows[i].Cells[9].Value;
                //    workSheet.Cells[i + 4, "K"] = dgViewBuscapatentes.Rows[i].Cells[10].Value;
                //    workSheet.Cells[i + 4, "L"] = dgViewBuscapatentes.Rows[i].Cells[11].Value;
                //    workSheet.Cells[i + 4, "M"] = dgViewBuscapatentes.Rows[i].Cells[12].Value;
                //    workSheet.Cells[i + 4, "N"] = dgViewBuscapatentes.Rows[i].Cells[13].Value;
                //    workSheet.Cells[i + 4, "O"] = dgViewBuscapatentes.Rows[i].Cells[14].Value;
                //    workSheet.Cells[i + 4, "P"] = dgViewBuscapatentes.Rows[i].Cells[15].Value;

                //    //workSheet.Cells[i + 4, "AP"] = listView1.Items[i].SubItems[41].Text;
                //    //workSheet.Cells[i + 4, "AQ"] = listView1.Items[i].SubItems[42].Text;
                //    row = i;
                //}
                ////foreach (var acct in listView1.Items)
                ////{
                ////    row++;
                ////    workSheet.Cells[row, "A"] = acct.;
                ////    workSheet.Cells[row, "B"] = acct;
                ////    workSheet.Cells[row, "C"] = acct;
                ////}

                //workSheet.Columns[1].AutoFit();
                //workSheet.Columns[2].AutoFit();
                //workSheet.Columns[3].AutoFit();

                ////Aplicando un autoformato a la tabla
                //workSheet.Range["A3", "P" + (row + 4)].AutoFormat(Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic2);
            }
            catch (Exception E)
            {
                Console.Write("Se canceló la exportación");
                new filelog(loguin.sId, E.ToString());
                MessageBox.Show(""+E.Message);

            }
        }

        private void cbHolder_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //MessageBox.Show("Mensaje enviado");
                    button1_Click(sender, e);
                    if (textBox10.Text == "1")
                    {
                        // MessageBox.Show(listViewCasos.Items + "");
                        //System.Threading.Thread.Sleep(100);
                        String sCasoIdcaso = dgViewBuscapatentes.Rows[0].Cells[1].Value.ToString();//listViewCasos.Items[0].SubItems[1].Text;
                        dgViewBuscapatentes.Rows.Clear();
                        tbexpediente.Clear();
                        tbInteresado.Clear();
                        tbPrioridad.Clear();
                        tbDenominacion.Clear();
                        tbregistro.Clear();
                        tbCliente.Clear();
                        tbreferencia.Clear();
                        tbCasoid.Clear();
                        consultacaso cConsultaid = new consultacaso(loguin, captura, this, sCasoIdcaso);
                        this.Hide();
                        cConsultaid.Show();
                    }

                }
            }
            catch (Exception E)
            {

            }
        }

        public bool validaversion(String sVersion)
        {
            bool breinicia = false;
            try
            {
                conect con_filev = new conect();
                String sQuery = "SELECT * FROM act_version order by idact_version desc limit 1;";
                MySqlDataReader resp_consltv = con_filev.getdatareader(sQuery);
                String sIdversionact = "";
                String sFechaversion = "";
                while (resp_consltv.Read())
                {
                    sIdversionact = validareader("v_actual", "v_actual", resp_consltv).Text;
                    sFechaversion = validareader("fecha", "fecha", resp_consltv).Text;
                    if (sIdversionact != sVersion)
                    {
                        MessageBox.Show("Deben actualizar la versión de casos king");
                        breinicia = true;
                    }
                }
                con_filev.Cerrarconexion();
                resp_consltv.Close();

                //if (breinicia) {
                //    buscarclienteform.Show();
                //    this.Close();
                //}
                return breinicia;
            }
            catch (Exception exs)
            {
                return breinicia;
            }

        }

        private void tbAvisoprueba_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

