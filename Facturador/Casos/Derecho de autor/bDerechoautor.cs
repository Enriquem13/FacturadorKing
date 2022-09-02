
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

namespace Facturador
{
    public partial class bDerechoautor : Form
    {
        public captura captura;
        public Form1 fLoguin;
        public String[] paises = new String[250];
        public String[] paisesclave = new String[250];
        public String sgTipoSolicitudId = "";

        public bDerechoautor(int iGrupo, captura capturaform, Form1 loguin)
        {
            fLoguin = loguin;
            captura=capturaform;
            InitializeComponent();
            this.BackColor = Color.SkyBlue;
            conect con = new conect();
            String sQueryclases = "SELECT * FROM tipo_obra;";
            MySqlDataReader respuestastringclases = con.getdatareader(sQueryclases);
            while (respuestastringclases.Read())
            {
                tbTipodeobra.Items.Add(validareader("descripcion", "tipo_obraid", respuestastringclases));
            }
            respuestastringclases.Close();

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
            respuestastringopais.Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //inicia busqueda d Derechos de Autor
            conect con = new conect();
            listViewCasos.Items.Clear();
            int rowcolor = 0;
            try
            {
                String sQuerywhere = "";
                String sQuerywherecaso = "";
                String stablaconsulta = "";

                if (cbTiposolicitud.SelectedItem != null)
                    sQuerywhere += " AND caso_registrodeobra.tiposolicitudid = " + (cbTiposolicitud.SelectedItem as ComboboxItem).Value;
                    sQuerywhere += " AND Tiposolicitud.tiposolicitudid = caso_registrodeobra.tiposolicitudid";
                    stablaconsulta += ",Tiposolicitud";

                if (!string.IsNullOrEmpty(tbexpediente.Text))
                    sQuerywhere += " AND caso_registrodeobra.CasoNumeroExpedienteLargo like '%" + tbexpediente.Text + "%'";

                if (!string.IsNullOrEmpty(tbDenominacion.Text))
                    sQuerywhere += " AND ( caso_registrodeobra.CasoTituloingles like '%" + tbDenominacion.Text + "%' OR caso_registrodeobra.CasoTituloespanol LIKE '%" + tbDenominacion.Text + "%')";

                if (!string.IsNullOrEmpty(tbregistro.Text))
                    sQuerywhere += " AND caso_registrodeobra.CasoNumConcedida like '%" + tbregistro.Text + "%'";

                if (!string.IsNullOrEmpty(tbCasoid.Text))
                    sQuerywhere += " AND caso_registrodeobra.CasoId =" + tbCasoid.Text;

                if (!string.IsNullOrEmpty(textBoxCasonumero.Text))
                    sQuerywhere += " AND caso_registrodeobra.CasoNumero like '%" + textBoxCasonumero.Text + "%'";

                if (cbPais.SelectedItem != null)
                {
                    sQuerywhere += " AND caso_registrodeobra.PaisId = '" + (cbPais.SelectedItem as ComboboxItem).Value + "'";
                    sQuerywhere += " AND Pais.PaisId = caso_registrodeobra.PaisId";
                    stablaconsulta += ",Pais";
                }


                if (sQuerywhere != "" || sQuerywherecaso != "")
                {
                    sQuerywhere = sQuerywhere.Substring(4, sQuerywhere.Length - 4);
                    String sQuerybusqueda = "";
                    sQuerybusqueda = " SELECT  " +
                                        //" caso_registrodeobra.CasoId, " +
                                        //" caso_registrodeobra.TipoSolicitudId, " +
                                        //" caso_registrodeobra.CasoDenominacion, " +
                                        //" caso_registrodeobra.CasoTitulo, " +
                                        //" caso_registrodeobra.IdiomaId, " +
                                        //" DATE_FORMAT(caso_registrodeobra.CasoFechaLegal , '%d-%m-%Y') as  CasoFechaLegal, " +
                                        //" DATE_FORMAT(caso_registrodeobra.CasoFechaRecepcion , '%d-%m-%Y') as  CasoFechaRecepcion, " +
                                        //" caso_registrodeobra.CasoNumConcedida, " +
                                        //" caso_registrodeobra.CasoNumeroExpedienteLargo, " +
                                        //" caso_registrodeobra.CasoNumero, " +
                                        //" caso_registrodeobra.ResponsableId, " +
                                        //" caso_registrodeobra.TipoMarcaId, " +
                                        //" caso_registrodeobra.CasoTipoCaptura, " +
                                        //" caso_registrodeobra.CasoTitular, " +
                                        //" caso_registrodeobra.EstatusCasoId, " +
                                        //" caso_registrodeobra.PaisId, " +
                                        //" caso_registrodeobra.UsuarioId, " +
                                        //" DATE_FORMAT(caso_registrodeobra.CasoFechaCartaCliente , '%d-%m-%Y') as  CasoFechaCartaCliente, " +
                                        //" DATE_FORMAT(caso_registrodeobra.CasoFechaVigencia , '%d-%m-%Y') as  CasoFechaVigencia " +
                                        " caso_registrodeobra.CasoId," +
                                        " caso_registrodeobra.TipoSolicitudId," +
                                        " caso_registrodeobra.CasoTituloingles," +
                                        " caso_registrodeobra.CasoTituloespanol," +
                                        " caso_registrodeobra.IdiomaId," +
                                        " DATE_FORMAT(caso_registrodeobra.CasoFechaLegal," +
                                                " '%d-%m-%Y') AS CasoFechaLegal," +
                                        " DATE_FORMAT(caso_registrodeobra.CasoFechaRecepcion," +
                                                " '%d-%m-%Y') AS CasoFechaRecepcion," +
                                        " caso_registrodeobra.CasoNumConcedida," +
                                        " caso_registrodeobra.CasoNumeroExpedienteLargo," +
                                        " caso_registrodeobra.CasoNumero," +
                                        " caso_registrodeobra.ResponsableId," +
                                        " Tiposolicitud.* ," +
                                        " caso_registrodeobra.CasoTipoCaptura," +
                                        " caso_registrodeobra.CasoTitular," +
                                        " caso_registrodeobra.EstatusCasoId," +
                                        " caso_registrodeobra.PaisId," +
                                        " caso_registrodeobra.UsuarioId," +
                                        " DATE_FORMAT(caso_registrodeobra.CasoFechaCartaCliente," +
                                                " '%d-%m-%Y') AS CasoFechaCartaCliente," +
                                        " DATE_FORMAT(caso_registrodeobra.CasoFechaVigencia," +
                                                " '%d-%m-%Y') AS CasoFechaVigencia" +
                                        " FROM " +
                                        " caso_registrodeobra" +
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
                        String sMarcasdescription = "";
                        String sQueryclasemarcas = "SELECT * FROM `casoproductos` WHERE casoid =" + sCasoidconsulta;
                        MySqlDataReader respuestastring = con.getdatareader(sQueryclasemarcas);
                        while (respuestastring.Read())
                        {
                            sMarcasdescription += validareader("CasoProductosClase", "CasoProductosId", respuestastring).Text + ",";
                        }
                        respuestastring.Close();
                        if (sMarcasdescription.Length > 1)
                        {
                            sMarcasdescription = sMarcasdescription.Substring(0, sMarcasdescription.Length - 1);
                        }
                        else
                        {
                            sMarcasdescription = "";
                        }
                        //terminamos buscar clases en productos
                        //iniciamos busqueda de clientes
                        //String sClientesdatos = "";
                        //String sClientes = " SELECT * "+
                        //                    " FROM " +
                        //                    "     caso_registrodeobra, " +
                        //                    "     cliente, " +
                        //                    "     casocliente " +
                        //                    " WHERE " +
                        //                    "     caso_registrodeobra.CasoId = " + sCasoidconsulta + "" +
                        //                    "     AND caso_registrodeobra.CasoId = casocliente.CasoId " +
                        //                    "     AND cliente.ClienteId = casocliente.ClienteId; ";

                        //MySqlDataReader respuestastringclientes = con.getdatareader(sClientes);
                        //while (respuestastringclientes.Read())
                        //{
                        //    sClientesdatos += validareader("ClienteNombre", "ClienteID", respuestastringclientes) + " ~ ";
                        //}
                        //respuestastringclientes.Close();
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
                        //pais
                        //caso numero
                        //casoid
                        //nombre de la obra 
                        //tipo de la obra
                        //Autor
                        //fecha presentacion
                        //fecha concesion
                        //expediente
                        //num de registro


                        ListViewItem listaitems = new ListViewItem(sPaisclave);
                        //listaitems.SubItems.Add(sMarcasdescription);
                        listaitems.SubItems.Add(sCasoidconsulta);
                        listaitems.SubItems.Add(validareader("CasoNumero", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(validareader("CasoTituloingles", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(validareader("TipoSolicitudDescrip", "CasoId", respuestastring3).Text);//TipoSolicitudId
                        listaitems.SubItems.Add(validareader("CasoTitular", "CasoId", respuestastring3).Text);
                        //listaitems.SubItems.Add(validareader("InteresadoNombre", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(sQueryintere);//CasoFechaLegal
                        listaitems.SubItems.Add(validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text);// + "/ " + validareader("CasoTituloingles", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(validareader("CasoFechaLegal", "CasoId", respuestastring3).Text);// + "/ " + validareader("CasoTituloingles", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text);// + "/ " + validareader("CasoTituloingles", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(validareader("CasoNumConcedida", "CasoId", respuestastring3).Text);// + "/ " + validareader("CasoTituloingles", "CasoId", respuestastring3).Text);
                        //listaitems.SubItems.Add(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text);
                        sgTipoSolicitudId = validareader("TipoSolicitudId", "CasoId", respuestastring3).Text;
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
                MessageBox.Show("Se encontraron más de " + rowcolor + " la busqueda debe ser más especifica.");
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

        private void listViewCasos_DoubleClick(object sender, EventArgs e)
        {
            String sClavepaiscaso = listViewCasos.SelectedItems[0].SubItems[0].Text;
            String sCasoIdcaso = listViewCasos.SelectedItems[0].SubItems[1].Text;
            String sCasoIdcasoSD = listViewCasos.SelectedItems[0].SubItems[2].Text;
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
            //tbPrioridad.Clear();
            tbDenominacion.Clear();
            tbregistro.Clear();
            tbCliente.Clear();
            tbreferencia.Clear();
            tbCasoid.Clear();
            fTderechosdeautor objmarca = new fTderechosdeautor(fLoguin, captura, this, sCasoIdcaso , sgTipoSolicitudId);//Tiposolicitud
            this.Hide();
            objmarca.Show();
        }

        private void bDerechoautor_FormClosing(object sender, FormClosingEventArgs e)
        {
            captura.Show();
        }
    }
}
