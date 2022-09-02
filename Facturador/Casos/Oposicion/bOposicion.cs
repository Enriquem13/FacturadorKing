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
//20220511 Librerias Faltantes
using SpreadsheetLight;
using System.Diagnostics;


namespace Facturador
{
    public partial class bOposicion : Form
    {
        public captura captura;
        public Form1 fLoguin;
        public String[] paises = new String[250];
        public String[] paisesclave = new String[250];

        //20220503FSV Agregamos para hacer consultas
        public MySqlDataReader respuestastring3;

        public bOposicion(int iGrupo, captura capturaform, Form1 loguin)
        {
            fLoguin = loguin;
            captura = capturaform;
            InitializeComponent();
            //this.BackColor = Color.FromArgb(255, 192, 128);
            conect con = new conect();

            String sQuerytipoSol = "select * from tiposolicitud where TipoSolicitudGrupo= " + iGrupo;
            MySqlDataReader respuestastringtoiposl = con.getdatareader(sQuerytipoSol);
            while (respuestastringtoiposl.Read())
            {
                cbTiposolicitud.Items.Add(validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestastringtoiposl));
            }
            respuestastringtoiposl.Close();


            //String sQuerypais = "select * from pais;";
            //MySqlDataReader respuestastringpais = con.getdatareader(sQuerypais);
            //while (respuestastringpais.Read())
            //{
            //    cbPais.Items.Add(validareader("PaisNombre", "PaisId", respuestastringpais));
            //}
            //respuestastringpais.Close();
            ////select * from pais;
            //String query5 = "select PaisNombre, PaisId, PaisClave from pais order by PaisNombre;";
            //MySqlDataReader respuestastringopais = con.getdatareader(query5);
            ////int paisint = 0;
            //while (respuestastringopais.Read())
            //{
            //    //cbPais.Items.Add(validareader("PaisNombre", "PaisId", respuestastringopais));
            //    //paises[Convert.ToInt32(validareader("PaisClave", "PaisId", respuestastringopais).Value.ToString())] = validareader("PaisClave", "PaisId", respuestastringopais).Text;
            //}
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
                cbPais.Items.Add(combopias);
                paises[Convert.ToInt32(validareader("PaisClave", "PaisId", respuestastringopais).Value.ToString())] = validareader("PaisClave", "PaisId", respuestastringopais).Text;
                paisesclave[Convert.ToInt32(validareader("PaisClave", "PaisId", respuestastringopais).Value.ToString())] = validareader("PaisNombre", "PaisId", respuestastringopais).Text;
                //paisint++;
            }
            respuestastringopais.Close();
            con2.Cerrarconexion();


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
                                "        AND grupoid = " + iGrupo +
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

        private void cbPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iValuepais = Convert.ToInt32((cbPais.SelectedItem as ComboboxItem).Value.ToString());
            tbClave.Text = paises[iValuepais];
        }

        private void listViewCasos_DoubleClick(object sender, EventArgs e)
        {
            //doble click para mandar a su respectiva pantalla
            String sClavepaiscaso = listViewCasos.SelectedItems[0].SubItems[0].Text;
            
            String sCasoIdcaso = listViewCasos.SelectedItems[0].SubItems[1].Text;
            String sCasonumerocaso = listViewCasos.SelectedItems[0].SubItems[3].Text;
            String sTipocaso = listViewCasos.SelectedItems[0].SubItems[4].Text;
            String sExpedientecaso = listViewCasos.SelectedItems[0].SubItems[5].Text;
            String sRegistrocaso = listViewCasos.SelectedItems[0].SubItems[6].Text;
            String sInteresadocaso = listViewCasos.SelectedItems[0].SubItems[7].Text;
            String sTitulodenomcaso = listViewCasos.SelectedItems[0].SubItems[8].Text;
            //String sClientecaso = listViewCasos.SelectedItems[0].SubItems[9].Text;
            //String sPrioridadcaso = listViewCasos.SelectedItems[0].SubItems[10].Text;
            //String sReferenciacaso = listViewCasos.SelectedItems[0].SubItems[11].Text;

            listViewCasos.Items.Clear();
            tbExpedienteMarcaImitadora.Clear();
            tbInteresado.Clear();
            //tbPrioridad.Clear();
            tbDenominacion.Clear();
            tbCliente.Clear();
            tbreferencia.Clear();

            //Temporal Parametro 
            //fToposiciones objoposicion = new fToposiciones(fLoguin, captura, this, sCasoIdcaso);
            fToposiciones objoposicion = new fToposiciones(fLoguin, captura,  sCasoIdcaso);


            this.Hide();
            objoposicion.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conect con = new conect();
            listViewCasos.Items.Clear();

            //20220511 LImpiamos el Grid 
            dgViewBuscaoposiciones.Rows.Clear();


            int rowcolor = 0;

            try
            {
                String sQuerywhere = "";
                String sQuerywherecaso = "";
                String stablaconsulta = "";

                if (cbTiposolicitud.SelectedItem != null)
                {
                    sQuerywhere += " AND cop.tiposolicitudid = " + (cbTiposolicitud.SelectedItem as ComboboxItem).Value;
                }

                if (cbFiltroestatus.SelectedItem != null)
                {
                    sQuerywhere += " AND cop.EstatusCasoId = " + (cbFiltroestatus.SelectedItem as ComboboxItem).Value;
                }




                if (!string.IsNullOrEmpty(tbDenominacionmarcaimitadora.Text))
                {
                    sQuerywhere += " AND cop.MarcaImitadora like '%" + tbDenominacionmarcaimitadora.Text + "%' ";
                }

                if (!string.IsNullOrEmpty(tbNombreImitador.Text))
                {
                    sQuerywhere += " AND cop.NombreImitador like '%" + tbNombreImitador.Text + "%' ";
                }

                if (!string.IsNullOrEmpty(tbCasomarcaimitadora.Text))
                {
                    sQuerywhere += " AND cop.CasoNumero like '%" + tbCasomarcaimitadora.Text + "%' ";
                }

                if (!string.IsNullOrEmpty(tbExpedienteMarcaImitadora.Text))
                {
                    sQuerywhere += " AND cop.CasoNumeroExpedienteLargo like '%" + tbExpedienteMarcaImitadora.Text + "%' ";
                }




                if (!string.IsNullOrEmpty(tbMarcaOriginal.Text))
                {
                    sQuerywhere += " AND qcm.CasoTituloingles like '%" + tbMarcaOriginal.Text + "%' ";
                }

                if (!string.IsNullOrEmpty(tbCasoNumeroOriginal.Text))
                {
                    sQuerywhere += " AND qcm.CasoNumero like '%" + tbCasoNumeroOriginal.Text + "%' ";
                }

                if (!string.IsNullOrEmpty(tbInteresado.Text))
                {
                    sQuerywhere += " AND qcm.InteresadoNombre like '%" + tbInteresado.Text + "%'";
                }

                if (!string.IsNullOrEmpty(tbCliente.Text))
                {
                    sQuerywhere += " AND qcm.ClienteNombre like '%" + tbCliente.Text + "%'";
                }

                if (!string.IsNullOrEmpty(tbreferencia.Text))
                {
                    sQuerywhere += " AND qcm.referencia like '%" + tbreferencia.Text + "%'";
                }




                if (cbPais.SelectedItem != null)
                {
                    sQuerywhere += " AND cop.PaisId = '" + (cbPais.SelectedItem as ComboboxItem).Value + "'";
                }





                if (sQuerywhere != "" || sQuerywherecaso != "")
                {
                    sQuerywhere = sQuerywhere.Substring(4, sQuerywhere.Length - 4);
                    String sQuerybusqueda = "";

                    sQuerybusqueda =    " SELECT  " +
                                        " cop.CasoId, " +
                                        " cop.TipoSolicitudId, " +
                                        " cop.CasoTituloespanol, " +
                                        " cop.CasoTituloingles, " +
                                        " cop.CasoFechaPresentacion, " +
                                        " cop.CasoNumeroExpedienteLargo, " +
                                        " cop.CasoNumero, " +
                                        " cop.ResponsableId, " +
                                        " cop.CasoFechaAlta, " +
                                        " cop.CasoTitular, " +
                                        " cop.EstatusCasoId, " +
                                        " DameEstatusCasoDescrip(cop.EstatusCasoId) As Estatuscasodescrip, " +
                                        " cop.UsuarioId, " +
                                        " cop.PaisId, " +
                                        " cop.CasoComentario, " +
                                        " cop.CasoIdOriginal, " +
                                        " cop.TipoSolicitudIdOriginal, " +
                                        " cop.MarcaImitadora, " +
                                        " cop.NombreImitador, " +
                                        " DATE_FORMAT(cop.FecPublicacionImitadora, ' %d-%m-%Y') as FecPublicacionImitadora,  " +
                                        " DATE_FORMAT(cop.FecPresentacionImitadora, ' %d-%m-%Y') as FecPresentacionImitadora, " +
                                        " cop.FecPresentacionOpocision, " +
                                        " cop.FecPublicacionOposicion, " +
                                        " (qcm.PaisClave) as PaisClave, " +
                                        " (qcm.referencia) as Referencia, " +
                                        " (qcm.CasoTituloingles) as MarcaOriginal, " +
                                        " (qcm.CasoProductosClase) as ClaseMarca, " +
                                        " (qcm.CasoNumero) as CasoOriginal, " +
                                        " (qcm.InteresadoNombre) as Interesado, " +
                                        " (qcm.ClienteNombre) as Cliente " +
                                        " FROM " +
                                        " caso_oposicion cop  " +
                                        " LEFT OUTER JOIN consulta_casosmarcas qcm  " +
                                        " ON (cop.casoidoriginal = qcm.casoid and cop.tiposolicitudidoriginal = qcm.tiposolicitudid)  " +
                                        " WHERE " +
                                        sQuerywhere;


                    MySqlDataReader respuestastring3 = con.getdatareader(sQuerybusqueda);
                    while (respuestastring3.Read())
                    {
                        String sCasoidconsulta = validareader("CasoId", "CasoId", respuestastring3).Text;






                        //Formateamos Fechas
                        DateTime dFechapublicacion = DateTime.MinValue;
                        String fecha = "";
                        try
                        {
                            if (validareader("FecPublicacionImitadora", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("FecPublicacionImitadora", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                fecha = validareader("FecPublicacionImitadora", "CasoId", respuestastring3).Text.Trim();
                                dFechapublicacion = Convert.ToDateTime(fecha);
                            }
                        }
                        catch (Exception exdate)
                        {
                            new filelog("linea 670: " + fecha, exdate.Message);
                        }


                        DateTime dFechapresentacion = DateTime.MinValue;
                        String sdatofecha = "";
                        try
                        {
                            if (validareader("FecPresentacionImitadora", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("FecPresentacionImitadora", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                sdatofecha = validareader("FecPresentacionImitadora", "CasoId", respuestastring3).Text.Trim();
                                dFechapresentacion = Convert.ToDateTime(sdatofecha);
                            }
                        }
                        catch (Exception exdates)
                        {
                            new filelog("linea 683: fecha:" + sdatofecha, exdates.StackTrace);
                        }




                        //1.- PRIORIDADES
                        String sPrioridades = "";
                        String sQueryprio = "select PrioridadNumero, PrioridadId from  prioridad where casoid =" + sCasoidconsulta;
                        MySqlDataReader respuestastringprio = con.getdatareader(sQueryprio);
                        while (respuestastringprio.Read())
                        {
                            sPrioridades += validareader("PrioridadNumero", "PrioridadId", respuestastringprio) + " ~ ";
                        }
                        respuestastringprio.Close();
                        
                        

                        //2.- INTERESADO
                        String sInteresadosconsul = " SELECT  " +
                                                    "     * " +
                                                    " FROM " +
                                                    "     interesado, " +
                                                    "     casointeresado " +
                                                    " WHERE " +
                                                    "     casointeresado.InteresadoId = interesado.InteresadoID " +
                                                    "     AND casointeresado.CasoId =" + sCasoidconsulta;
                        String sQueryintere = "";
                        MySqlDataReader respuestastringinteresado = con.getdatareader(sInteresadosconsul);
                        //int paisint = 0;
                        while (respuestastringinteresado.Read())
                        {
                            sQueryintere += validareader("InteresadoNombre", "InteresadoId", respuestastringinteresado) + " ~ ";
                        }
                        respuestastringinteresado.Close();
                        int residuo = rowcolor % 2;
                        //buscamos clases en productos
                        //terminamos buscar clases en productos



                        //3.- CLIENTE
                        //iniciamos busqueda de clientes
                        String sClientesdatos = "";
                        String sClientes = " SELECT  " +
                                            "     * " +
                                            " FROM " +
                                            "     caso_oposicion, " +
                                            "     cliente, " +
                                            "     casocliente " +
                                            " WHERE " +
                                            "     caso_oposicion.CasoId like '%" + sCasoidconsulta + "%' " +
                                            "     AND caso_oposicion.CasoId = casocliente.CasoId " +
                                            "     AND cliente.ClienteId = casocliente.ClienteId; ";

                        MySqlDataReader respuestastringclientes = con.getdatareader(sClientes);
                        while (respuestastringclientes.Read())
                        {
                            sClientesdatos += validareader("ClienteNombre", "ClienteID", respuestastringclientes) + " ~ ";
                        }
                        respuestastringclientes.Close();
                        //terminamos busqueda de clientes



                        //4.- PAIS
                        String sPaisclave = "";




                        //5.- TIPO SOLICITUD
                        String sTiposolicitudDescrip = "";
                        String sTiposolicitud = "select * from tiposolicitud where tiposolicitudId = " + validareader("tiposolicitudId", "CasoId", respuestastring3).Text;
                        MySqlDataReader resp_tiposolicitud = con.getdatareader(sTiposolicitud);
                        while (resp_tiposolicitud.Read())
                        {
                            sTiposolicitudDescrip = validareader("TipoSolicitudDescrip", "tiposolicitudId", resp_tiposolicitud).Text;//consultar Tiposolicitud
                            //tbTipo.Text = validareader("TipoSolicitudDescrip", "CasoId", respuestastring3).Text;//consultar Tiposolicitud

                        }
                        resp_tiposolicitud.Close();




                        ListViewItem listaitems = new ListViewItem(sPaisclave);
                        listaitems.SubItems.Add(sCasoidconsulta);
                        listaitems.SubItems.Add(validareader("CasoNumero", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(sTiposolicitudDescrip);
                        //listaitems.SubItems.Add(validareader("TipoSolicitudDescrip", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text);
                        //listaitems.SubItems.Add(validareader("CasoNumConcedida", "CasoId", respuestastring3).Text);
                        //listaitems.SubItems.Add(validareader("InteresadoNombre", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(sQueryintere);
                        listaitems.SubItems.Add(validareader("CasoTitular", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(sClientesdatos);


                        String sReferenciadato = "";
                        String sReferenciaquery = "select * from referencia where Casoid = " + validareader("Casoid", "CasoId", respuestastring3).Text;
                        MySqlDataReader resp_referencia = con.getdatareader(sReferenciaquery);
                        while (resp_referencia.Read())
                        {
                            sReferenciadato = validareader("ReferenciaNombre", "referenciaid", resp_referencia).Text;//consultar Tiposolicitud
                            //tbTipo.Text = validareader("TipoSolicitudDescrip", "CasoId", respuestastring3).Text;//consultar Tiposolicitud

                        }
                        resp_referencia.Close();
                        //listaitems.SubItems.Add(sPrioridades);
                        listaitems.SubItems.Add(sReferenciadato);
                        if (residuo == 0)
                        {
                            listaitems.BackColor = Color.LightGray;
                        }
                        else
                        {
                            listaitems.BackColor = Color.Azure;
                        }
                        listViewCasos.Items.Add(listaitems);
                        this.listViewCasos.FullRowSelect = true;
                        rowcolor++;



                        DataGridViewRow dRows = (DataGridViewRow)dgViewBuscaoposiciones.Rows[0].Clone();
                        try
                        {
                            dRows.Cells[0].Value = validareader("PaisClave", "CasoId", respuestastring3).Text;
                            dRows.Cells[1].Value = sCasoidconsulta;
                            dRows.Cells[2].Value = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                            dRows.Cells[3].Value = sTiposolicitudDescrip;
                            dRows.Cells[4].Value = validareader("Estatuscasodescrip", "CasoId", respuestastring3).Text;//estatus
                            dRows.Cells[5].Value = validareader("MarcaImitadora", "CasoId", respuestastring3).Text;
                            dRows.Cells[6].Value = validareader("MarcaOriginal", "CasoId", respuestastring3).Text;
                            dRows.Cells[7].Value = validareader("ClaseMarca", "CasoId", respuestastring3).Text;
                            dRows.Cells[8].Value = dFechapublicacion;
                            dRows.Cells[9].Value = dFechapresentacion;
                            dRows.Cells[10].Value = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                            dRows.Cells[11].Value = validareader("NombreImitador", "CasoId", respuestastring3).Text;
                            dRows.Cells[12].Value = validareader("CasoOriginal", "CasoId", respuestastring3).Text;
                            dRows.Cells[13].Value = validareader("Interesado", "CasoId", respuestastring3).Text;
                            dRows.Cells[14].Value = validareader("Cliente", "CasoId", respuestastring3).Text;
                            dRows.Cells[15].Value = validareader("Referencia", "CasoId", respuestastring3).Text;
                            

                        }
                        catch (Exception exs)
                        {
                            new filelog("linea 706", exs.StackTrace);
                        }


                        //Pintamos las Celdas
                        if (residuo == 0)
                        {
                            dRows.DefaultCellStyle.BackColor = Color.LightGray;
                            if (validareader("FecPublicacionImitadora", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("FecPublicacionImitadora", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                dRows.Cells[8].Style.ForeColor = Color.Black;
                            }
                            else
                            {
                                dRows.Cells[8].Style.ForeColor = Color.LightGray;
                            }

                            if (validareader("FecPresentacionImitadora", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("FecPresentacionImitadora", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                dRows.Cells[9].Style.ForeColor = Color.Black;
                            }
                            else
                            {
                                dRows.Cells[9].Style.ForeColor = Color.LightGray;
                            }
                        }
                        else
                        {
                            dRows.DefaultCellStyle.BackColor = Color.Azure;
                            if (validareader("FecPublicacionImitadora", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("FecPublicacionImitadora", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                dRows.Cells[8].Style.ForeColor = Color.Black;
                            }
                            else
                            {
                                dRows.Cells[8].Style.ForeColor = Color.Azure;
                            }

                            if (validareader("FecPresentacionImitadora", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("FecPresentacionImitadora", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                dRows.Cells[9].Style.ForeColor = Color.Black;
                            }
                            else
                            {
                                dRows.Cells[9].Style.ForeColor = Color.Azure;
                            }
                        }



                        dgViewBuscaoposiciones.Rows.Add(dRows);




                    }
                    respuestastring3.Close();
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
                String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                StringBuilder sb = new StringBuilder();
                sb.Append(fechalog + ":user:" + fLoguin.sId + ": " + E + "evento\n");
                File.AppendAllText("c:\\facturador\\" + "events.log", sb.ToString());
                sb.Clear();
                MessageBox.Show("Se encontraron más de " + rowcolor + " la busqueda debe ser más especifica.");
            }
        }

        private void tbClave_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                if (tbClave.Text.Length > 1)
                {

                    String valorclave = tbClave.Text;
                    int index = Array.IndexOf(paises, valorclave);
                    cbPais.Text = tbClave.Text + " - " + paisesclave[index];
                }
            }
            catch (Exception Ex)
            {

            }

        }

        private void bOposicion_FormClosing(object sender, FormClosingEventArgs e)
        {
            captura.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            conect con = new conect();
            //listViewCasos.Items.Clear();
            dgViewBuscaoposiciones.Rows.Clear();
            int rowcolor = 0;

            try
            {
                String sQuerywhere = "";
                String sQuerywherecaso = "";
                String sCampoconsulta = "";
                String stablaconsulta = "";
                String sWhereconsulta = "";


                String sQuerybusqueda = " SELECT  " +
                                        " cop.CasoId, " +
                                        " cop.TipoSolicitudId, " +
                                        " cop.CasoTituloespanol, " +
                                        " cop.CasoTituloingles, " +
                                        " cop.CasoFechaPresentacion, " +
                                        " cop.CasoNumeroExpedienteLargo, " +
                                        " cop.CasoNumero, " +
                                        " cop.ResponsableId, " +
                                        " cop.CasoFechaAlta, " +
                                        " cop.CasoTitular, " +
                                        " DameEstatusCasoDescrip(cop.EstatusCasoId) As Estatuscasodescrip, " +
                                        " cop.UsuarioId, " +
                                        " cop.PaisId, " +
                                        " cop.CasoComentario, " +
                                        " cop.CasoIdOriginal, " +
                                        " cop.TipoSolicitudIdOriginal, " +
                                        " cop.MarcaImitadora, " +
                                        " cop.NombreImitador, " +
                                        " DATE_FORMAT(cop.FecPublicacionImitadora, ' %d-%m-%Y') as FecPublicacionImitadora,  " +
                                        " DATE_FORMAT(cop.FecPresentacionImitadora, ' %d-%m-%Y') as FecPresentacionImitadora, " +
                                        //" Get_Referencia(cop.CasoIdOriginal, cop.TipoSolicitudIdOriginal) as referencia, " +
                                        " cop.FecPresentacionOpocision, " +
                                        " cop.FecPublicacionOposicion, " +
                                        " (qcm.referencia) as Referencia, " +
                                        //" (qcm.CasoNumero) as CasoNumero, " +
                                        " (qcm.PaisClave) as PaisClave, " +
                                        " (qcm.CasoTituloingles) as MarcaOriginal, " +
                                        " (qcm.CasoProductosClase) as ClaseMarca, " +
                                        " (qcm.CasoNumero) as CasoOriginal, " +
                                        " (qcm.InteresadoNombre) as Interesado, " +
                                        " (qcm.ClienteNombre) as Cliente " +
                                        " FROM " +
                                        " caso_oposicion cop  " +
                                        " LEFT OUTER JOIN consulta_casosmarcas qcm  " +
                                        " ON (cop.casoidoriginal = qcm.casoid and cop.tiposolicitudidoriginal = qcm.tiposolicitudid)  " +
                                         " order by cop.CasoId desc limit " + tbLimitcasos.Text + ";";



                respuestastring3 = con.getdatareader(sQuerybusqueda);
                while (respuestastring3.Read())
                {
                    String sCasoidconsulta = validareader("CasoId", "CasoId", respuestastring3).Text;
                    int residuo = rowcolor % 2;



                    //Formateamos Fechas
                    DateTime dFechapublicacion = DateTime.MinValue;
                    String fecha = "";
                    try
                    {
                        if (validareader("FecPublicacionImitadora", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("FecPublicacionImitadora", "CasoId", respuestastring3).Text.Trim() != "")
                        {
                            fecha = validareader("FecPublicacionImitadora", "CasoId", respuestastring3).Text.Trim();
                            dFechapublicacion = Convert.ToDateTime(fecha);
                        }
                    }
                    catch (Exception exdate)
                    {
                        new filelog("linea 670: " + fecha, exdate.Message);
                    }


                    DateTime dFechapresentacion = DateTime.MinValue;
                    String sdatofecha = "";
                    try
                    {
                        if (validareader("FecPresentacionImitadora", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("FecPresentacionImitadora", "CasoId", respuestastring3).Text.Trim() != "")
                        {
                            sdatofecha = validareader("FecPresentacionImitadora", "CasoId", respuestastring3).Text.Trim();
                            dFechapresentacion = Convert.ToDateTime(sdatofecha);
                        }
                    }
                    catch (Exception exdates)
                    {
                        new filelog("linea 683: fecha:" + sdatofecha, exdates.StackTrace);
                    }




                    //5.- TIPO SOLICITUD provisional antes de hacer la funcion para oposición
                    String sTiposolicitudDescrip = "";
                    String sTiposolicitud = "select * from tiposolicitud where tiposolicitudId = " + validareader("tiposolicitudId", "CasoId", respuestastring3).Text;
                    MySqlDataReader resp_tiposolicitud = con.getdatareader(sTiposolicitud);
                    while (resp_tiposolicitud.Read())
                    {
                        sTiposolicitudDescrip = validareader("TipoSolicitudDescrip", "tiposolicitudId", resp_tiposolicitud).Text;
                    }
                    resp_tiposolicitud.Close();



                    //Llenamos Grid
                    DataGridViewRow dRows = (DataGridViewRow)dgViewBuscaoposiciones.Rows[0].Clone();
                    try
                    {
                        dRows.Cells[0].Value = validareader("PaisClave", "CasoId", respuestastring3).Text;
                        dRows.Cells[1].Value = sCasoidconsulta;
                        dRows.Cells[2].Value = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                        dRows.Cells[3].Value = sTiposolicitudDescrip;
                        dRows.Cells[4].Value = validareader("Estatuscasodescrip", "CasoId", respuestastring3).Text;//estatus
                        dRows.Cells[5].Value = validareader("MarcaImitadora", "CasoId", respuestastring3).Text;
                        
                        dRows.Cells[6].Value = validareader("MarcaOriginal", "CasoId", respuestastring3).Text;
                        dRows.Cells[7].Value = validareader("ClaseMarca", "CasoId", respuestastring3).Text;

                        dRows.Cells[8].Value = dFechapublicacion;
                        dRows.Cells[9].Value = dFechapresentacion;
                        dRows.Cells[10].Value = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                        dRows.Cells[11].Value = validareader("NombreImitador", "CasoId", respuestastring3).Text;

                        dRows.Cells[12].Value = validareader("CasoOriginal", "CasoId", respuestastring3).Text;
                        dRows.Cells[13].Value = validareader("Interesado", "CasoId", respuestastring3).Text;
                        dRows.Cells[14].Value = validareader("Cliente", "CasoId", respuestastring3).Text;
                        dRows.Cells[15].Value = validareader("referencia", "CasoId", respuestastring3).Text;
                        

                    }
                    catch (Exception exs)
                    {
                        new filelog("linea 706", exs.StackTrace);
                    }


                    //Pintamos las Celdas
                    if (residuo == 0)
                    {
                        dRows.DefaultCellStyle.BackColor = Color.LightGray;
                        if (validareader("FecPublicacionImitadora", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("FecPublicacionImitadora", "CasoId", respuestastring3).Text.Trim() != "")
                        {
                            dRows.Cells[8].Style.ForeColor = Color.Black;
                        }
                        else
                        {
                            dRows.Cells[8].Style.ForeColor = Color.LightGray;
                        }

                        if (validareader("FecPresentacionImitadora", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("FecPresentacionImitadora", "CasoId", respuestastring3).Text.Trim() != "")
                        {
                            dRows.Cells[9].Style.ForeColor = Color.Black;
                        }
                        else
                        {
                            dRows.Cells[9].Style.ForeColor = Color.LightGray;
                        }
                    }
                    else
                    {
                        dRows.DefaultCellStyle.BackColor = Color.Azure;
                        if (validareader("FecPublicacionImitadora", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("FecPublicacionImitadora", "CasoId", respuestastring3).Text.Trim() != "")
                        {
                            dRows.Cells[8].Style.ForeColor = Color.Black;
                        }
                        else
                        {
                            dRows.Cells[8].Style.ForeColor = Color.Azure;
                        }

                        if (validareader("FecPresentacionImitadora", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("FecPresentacionImitadora", "CasoId", respuestastring3).Text.Trim() != "")
                        {
                            dRows.Cells[9].Style.ForeColor = Color.Black;
                        }
                        else
                        {
                            dRows.Cells[9].Style.ForeColor = Color.Azure;
                        }
                    }



                    dgViewBuscaoposiciones.Rows.Add(dRows);
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

        private void dgViewBuscaoposiciones_DoubleClick(object sender, EventArgs e)
        {
            
            try
            {
                if (dgViewBuscaoposiciones.SelectedRows != null)
                {

                    String sCasoIdcaso = dgViewBuscaoposiciones.SelectedRows[0].Cells[1].Value.ToString();//listViewCasos.Items[0].SubItems[1].Text;
                    dgViewBuscaoposiciones.Rows.Clear();
                    tbExpedienteMarcaImitadora.Clear();
                    tbInteresado.Clear();
                    tbDenominacion.Clear();
                    tbCliente.Clear();
                    tbreferencia.Clear();

                    //Temporal parametro formulario
                    //fToposiciones objoposicion = new fToposiciones(fLoguin, captura, this, sCasoIdcaso);
                    fToposiciones objoposicion = new fToposiciones(fLoguin, captura, sCasoIdcaso);

                    //20220516cerramos la forma, posterior se volverá a abrir
                    //this.Hide();
                    this.Close();
                    //Fin de modificación

                    objoposicion.Show();
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

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                generaexcel(dgViewBuscaoposiciones);
            }
            catch (Exception E)
            {
                Console.Write("Se canceló la exportación");
                new filelog(fLoguin.sId, E.ToString());
                MessageBox.Show(E.Message);
            }
        }

        public void generaexcel(DataGridView tabla)
        {
            try
            {

                SLDocument obj = new SLDocument();
                SLStyle estilofechas = obj.CreateStyle();
                estilofechas.FormatCode = "dd/mm/yyyy";

                button25.Enabled = false;

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
