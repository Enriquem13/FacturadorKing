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

namespace Facturador
{
    public partial class bReservadederechos : Form
    {
        public String[] paises = new String[250];
        public String[] paisesclave = new String[250];
        public captura captura;
        public Form1 fLoguin;
        public bReservadederechos(int iGrupo, captura capturaform, Form1 loguin)
        {
            fLoguin = loguin;
            captura = capturaform;
            InitializeComponent();
            conect con = new conect();
            this.BackColor = Color.LightGreen;
            String sQuerytipoSol = "select * from tiposolicitud where TipoSolicitudGrupo= " + iGrupo;
            MySqlDataReader respuestastringtoiposl = con.getdatareader(sQuerytipoSol);
            while (respuestastringtoiposl.Read())
            {
                cbTiposolicitud.Items.Add(validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestastringtoiposl));
            }
            respuestastringtoiposl.Close();


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
        private void button1_Click(object sender, EventArgs e)
        {
            //pintar el grid de la reserva de derechos 
            conect con = new conect();
            listViewCasos.Items.Clear();
            int rowcolor = 0;

            try
            {
                String sQuerywhere = "";
                String sQuerywherecaso = "";
                String stablaconsulta = "";

                if (cbTiposolicitud.SelectedItem != null)
                {
                    sQuerywhere += " AND caso_reservadederechos.tiposolicitudid = " + (cbTiposolicitud.SelectedItem as ComboboxItem).Value;
                    sQuerywhere += " AND Tiposolicitud.tiposolicitudid = caso_reservadederechos.tiposolicitudid";
                    stablaconsulta += ",Tiposolicitud";
                }


                if (!string.IsNullOrEmpty(tbexpediente.Text))
                {
                    sQuerywhere += " AND caso_reservadederechos.CasoNumeroExpedienteLargo like '%" + tbexpediente.Text + "%'";
                }


                if (!string.IsNullOrEmpty(tbDenominacion.Text))
                {
                    sQuerywhere += " AND caso_reservadederechos.CasoTitular like '%" + tbDenominacion.Text + "%' ";//OR caso_reservadederechos.CasoTituloespanol LIKE '%" + tbDenominacion.Text + "%')";
                }


                if (!string.IsNullOrEmpty(tbregistro.Text))
                {
                    sQuerywhere += " AND caso_reservadederechos.CasoNumConcedida like '%" + tbregistro.Text + "%'";
                }


                if (!string.IsNullOrEmpty(tbCasoid.Text))
                {
                    sQuerywhere += " AND caso_reservadederechos.CasoId =" + tbCasoid.Text;
                }


                if (!string.IsNullOrEmpty(textBoxCasonumero.Text))
                {
                    sQuerywhere += " AND caso_reservadederechos.CasoNumero like '%" + textBoxCasonumero.Text + "%'";
                }


                if (cbPais.SelectedItem != null)
                {
                    sQuerywhere += " AND caso_reservadederechos.PaisId = '" + (cbPais.SelectedItem as ComboboxItem).Value + "'";
                    sQuerywhere += " AND Pais.PaisId = caso_reservadederechos.PaisId";
                    stablaconsulta += ",Pais";
                }


                if (sQuerywhere != "" || sQuerywherecaso != "")
                {
                    sQuerywhere = sQuerywhere.Substring(4, sQuerywhere.Length - 4);
                    String sQuerybusqueda = "";
                    sQuerybusqueda = " SELECT  " +
                                        "DATE_FORMAT(caso_reservadederechos.CasoFechaVigencia,'%d-%m-%Y') AS CasoFechaVigencia, " +
                                        "DATE_FORMAT(caso_reservadederechos.CasoFechaRecepcion,'%d-%m-%Y') AS CasoFechaRecepcion, " +
                                        "DATE_FORMAT(caso_reservadederechos.CasoFechaLegal,'%d-%m-%Y') AS CasoFechaLegal, " +
                                        "CasoTitular, " +
                                        "CasoNumeroExpedienteLargo, " +
                                        "caso_reservadederechos.tiposolicitudId, " +
                                        "PaisId, " +
                                        "CasoId " +
                                        " FROM " +
                                        " caso_reservadederechos" +
                                        stablaconsulta +
                                        " WHERE " +
                                            sQuerywhere;
                    MySqlDataReader respuestastring3 = con.getdatareader(sQuerybusqueda);
                    while (respuestastring3.Read())
                    {
                        String sCasoidconsulta = validareader("CasoId", "CasoId", respuestastring3).Text;

                        String sPrioridades = "";
                        String sQueryprio = "select PrioridadNumero, PrioridadId from  prioridad where casoid =" + sCasoidconsulta;
                        MySqlDataReader respuestastringprio = con.getdatareader(sQueryprio);
                        while (respuestastringprio.Read())
                        {
                            sPrioridades += validareader("PrioridadNumero", "PrioridadId", respuestastringprio) + " ~ ";
                        }
                        respuestastringprio.Close();
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

                        //iniciamos busqueda de clientes
                        String sClientesdatos = "";
                        String sClientes = " SELECT  " +
                                            "     * " +
                                            " FROM " +
                                            "     caso_reservadederechos, " +
                                            "     cliente, " +
                                            "     casocliente " +
                                            " WHERE " +
                                            "     caso_reservadederechos.CasoId like '%" + sCasoidconsulta + "%' " +
                                            "     AND caso_reservadederechos.CasoId = casocliente.CasoId " +
                                            "     AND cliente.ClienteId = casocliente.ClienteId; ";

                        MySqlDataReader respuestastringclientes = con.getdatareader(sClientes);
                        while (respuestastringclientes.Read())
                        {
                            sClientesdatos += validareader("ClienteNombre", "ClienteID", respuestastringclientes) + " ~ ";
                        }
                        respuestastringclientes.Close();
                        //terminamos busqueda de clientes


                        //prueba de todos los casos
                        //País del caso
                        MySqlDataReader resp_paiscaso = con.getdatareader("select * from Pais where PaisId = " + validareader("PaisId", "PaisId", respuestastring3).Text);
                        String sPaisclave = "";
                        while (resp_paiscaso.Read())
                        {
                            sPaisclave = validareader("PaisClave", "PaisId", resp_paiscaso).Text;
                        }
                        resp_paiscaso.Close();

                        String sTiposolicitudDescrip = "";
                        String sTiposolicitud = "select * from tiposolicitud where tiposolicitudId = " + validareader("tiposolicitudId", "CasoId", respuestastring3).Text;
                        MySqlDataReader resp_tiposolicitud = con.getdatareader(sTiposolicitud);
                        while (resp_tiposolicitud.Read())
                        {
                            sTiposolicitudDescrip = validareader("TipoSolicitudDescrip", "tiposolicitudId", resp_tiposolicitud).Text;//consultar Tiposolicitud
                            //tbTipo.Text = validareader("TipoSolicitudDescrip", "CasoId", respuestastring3).Text;//consultar Tiposolicitud

                        }
                        resp_tiposolicitud.Close();

                        ListViewItem listaitems = new ListViewItem(sClientesdatos);//nombre
                        listaitems.SubItems.Add("");// genero
                        listaitems.SubItems.Add(sTiposolicitudDescrip);//Especie
                        listaitems.SubItems.Add(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text);//Clave
                        listaitems.SubItems.Add(validareader("CasoTitular", "CasoId", respuestastring3).Text);//titular
                        //fecha presentacion 
                        //1er fecha...
                        
                        //listaitems.SubItems.Add(validareader("TipoSolicitudDescrip", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(validareader("CasoFechaLegal", "CasoId", respuestastring3).Text);//CasoFechaLegal
                        listaitems.SubItems.Add(validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text);//CasoFechaLegal
                        listaitems.SubItems.Add(validareader("CasoFechaVigencia", "CasoId", respuestastring3).Text);//CasoFechaLegal
                        //listaitems.SubItems.Add(validareader("CasoNumConcedida", "CasoId", respuestastring3).Text);
                        //listaitems.SubItems.Add(validareader("InteresadoNombre", "CasoId", respuestastring3).Text);
                        //listaitems.SubItems.Add(sQueryintere);
                        //listaitems.SubItems.Add(validareader("CasoTitular", "CasoId", respuestastring3).Text);
                        //listaitems.SubItems.Add(sClientesdatos);


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

        private void cbPais_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int iValuepais = Convert.ToInt32((cbPais.SelectedItem as ComboboxItem).Value.ToString());
            tbClave.Text = paises[iValuepais];
        }

        private void bReservadederechos_FormClosing(object sender, FormClosingEventArgs e)
        {
            captura.Show();
        }
    }
}
