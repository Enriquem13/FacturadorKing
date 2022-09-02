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
    public partial class bCasoparadoc : Form
    {
        public captura captura;
        public Form1 fLoguin;
        public String[] paises = new String[250];
        public String[] paisesclave = new String[250];
        public String sGTipocaso;

        //creamos el get para mandar el dato de casoid
        public String sCasoid { get; set; }
        public bCasoparadoc(int iGrupo, captura capturaform, Form1 loguin)
        {
            fLoguin = loguin;
            captura = capturaform;
            InitializeComponent();
            sGTipocaso = iGrupo + "";
            this.Text = "Busqueda de caso para agregar un Escrito ";
            switch (sGTipocaso)
            {
                case "1":
                    {
                        this.BackColor = Color.Pink;
                        this.Text = this.Text + " ( Grupo Patentes)";
                    } break;
                case "2":
                    {
                        this.BackColor = Color.FromArgb(255, 255, 192);
                        this.Text = this.Text + " ( Grupo Marcas)";
                    } break;
                case "3":
                    {
                        this.BackColor = Color.Yellow;
                        this.Text = this.Text + " ( Grupo Contencioso)";
                    } break;
                case "4":
                    {
                        this.BackColor = SystemColors.Control;
                        this.Text = this.Text + " ( Grupo Consulta)";
                    } break;
                case "5":
                    {
                        this.BackColor = Color.FromArgb(255, 192, 128);
                        this.Text = this.Text + " ( Grupo Oposicion a solicitudes)";
                    } break;
                case "6":
                    {
                        this.BackColor = SystemColors.Control;
                        this.Text = this.Text + " ( Grupo Consulta)";
                    } break;
                case "7":
                    {
                        this.BackColor = Color.SkyBlue;
                        this.Text = this.Text + " ( Grupo Derechos de autor)";
                    } break;
                case "8":
                    {
                        this.BackColor = Color.LightGreen;
                        this.Text = this.Text + " ( Grupo Reserva de derechos)";
                    } break;
                default:
                    {
                        MessageBox.Show("Debe seleccionar un tipo correcto");
                    } break;
            }
            conect con = new conect();
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

        private void button1_Click(object sender, EventArgs e)
        {
            //borramos el listview 
            conect con = new conect();
            listViewCasos.Items.Clear();
            int rowcolor = 0;
            String sTablaconsulta = "";
            //Para saber a que tabla de bemos consultar debemos preguntar por el grupo
            switch (sGTipocaso)
            {
                    case "1": 
                        {
                            sTablaconsulta = "caso_patente";
                        } break;
                    case "2": 
                        {
                            sTablaconsulta = "caso_marcas";
                        } break;
                    case "3": 
                        {
                            sTablaconsulta = "caso_contencioso";
                        } break;
                    case "4": 
                        {
                            sTablaconsulta = "caso_consulta";
                        } break;
                    case "5": 
                        {
                            sTablaconsulta = "caso_oposicion";
                        } break;
                    case "6":
                        {
                            sTablaconsulta = "";
                        } break;
                    case "7":
                        {
                            sTablaconsulta = "caso_registrodeobra";
                        } break;
                    case "8":
                        {
                            sTablaconsulta = "caso_reservadederechos";
                        } break;
                    default:
                        {
                            MessageBox.Show("Debe seleccionar un tipo correcto");
                        } break;
                }
            try
            {
                String sQuerywhere = "";
                String sQuerywherecaso = "";
                String stablaconsulta = "";

                if (cbTiposolicitud.SelectedItem != null) {
                    sQuerywhere += " AND " + sTablaconsulta + ".tiposolicitudid = " + (cbTiposolicitud.SelectedItem as ComboboxItem).Value;
                    sQuerywhere += " AND Tiposolicitud.tiposolicitudid = " + sTablaconsulta + ".tiposolicitudid";
                    stablaconsulta += ",Tiposolicitud";
                }


                if (!string.IsNullOrEmpty(tbexpediente.Text)) {
                    sQuerywhere += " AND " + sTablaconsulta + ".CasoNumeroExpedienteLargo like '%" + tbexpediente.Text + "%'";
                }


                if (!string.IsNullOrEmpty(tbDenominacion.Text)) {
                    sQuerywhere += " AND ( " + sTablaconsulta + ".CasoTituloingles like '%" + tbDenominacion.Text + "%' OR " + sTablaconsulta + ".CasoTituloespanol LIKE '%" + tbDenominacion.Text + "%')";
                }


                if (!string.IsNullOrEmpty(tbregistro.Text)) {
                    sQuerywhere += " AND " + sTablaconsulta + ".CasoNumConcedida like '%" + tbregistro.Text + "%'";
                }


                if (!string.IsNullOrEmpty(tbCasoid.Text)) {
                    sQuerywhere += " AND " + sTablaconsulta + ".CasoId =" + tbCasoid.Text;
                }


                if (!string.IsNullOrEmpty(textBoxCasonumero.Text)) {
                    sQuerywhere += " AND " + sTablaconsulta + ".CasoNumero like '%" + textBoxCasonumero.Text + "%'";
                }


                if (cbPais.SelectedItem != null) {
                    sQuerywhere += " AND " + sTablaconsulta + ".PaisId = '" + (cbPais.SelectedItem as ComboboxItem).Value + "'";
                    sQuerywhere += " AND Pais.PaisId = " + sTablaconsulta + ".PaisId";
                    stablaconsulta += ",Pais";
                }
                    

                if (sQuerywhere != "" || sQuerywherecaso != "")
                {
                    sQuerywhere = sQuerywhere.Substring(4, sQuerywhere.Length - 4);
                    String sQuerybusqueda = "";
                    sQuerybusqueda = " SELECT  " +
                                        " * " +
                                        " FROM " +
                                        " " + sTablaconsulta + " " +
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
                        String sClientesdatos = "";
                        String sClientes =  " SELECT  " +
                                            "     * " +
                                            " FROM " +
                                            "     " + sTablaconsulta + ", " +
                                            "     cliente, " +
                                            "     casocliente " +
                                            " WHERE " +
                                            "     " + sTablaconsulta + ".CasoId like '%" + sCasoidconsulta + "%' " +
                                            "     AND " + sTablaconsulta + ".CasoId = casocliente.CasoId " +
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
                        
                        ListViewItem listaitems = new ListViewItem(sPaisclave);
                        //listaitems.SubItems.Add(sMarcasdescription);
                        listaitems.SubItems.Add(sCasoidconsulta);
                        listaitems.SubItems.Add(validareader("CasoNumero", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(sTiposolicitudDescrip);
                        //listaitems.SubItems.Add(validareader("TipoSolicitudDescrip", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(validareader("CasoNumConcedida", "CasoId", respuestastring3).Text);
                        //listaitems.SubItems.Add(validareader("InteresadoNombre", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(sQueryintere);
                        listaitems.SubItems.Add(validareader("CasoTituloespanol", "CasoId", respuestastring3).Text + "/ " + validareader("CasoTituloingles", "CasoId", respuestastring3).Text);
                        listaitems.SubItems.Add(sClientesdatos);
                        listaitems.SubItems.Add(sPrioridades);
                        listaitems.SubItems.Add("referencia");
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

        private void button4_Click(object sender, EventArgs e)
        {
            captura.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            captura.Close();
            fLoguin.Close();
        }

        private void cbPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iValuepais = Convert.ToInt32((cbPais.SelectedItem as ComboboxItem).Value.ToString());
            tbClave.Text = paises[iValuepais];
        }

        private void listViewCasos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listViewCasos_DoubleClick(object sender, EventArgs e)
        {
            String sClavepaiscaso = listViewCasos.SelectedItems[0].SubItems[0].Text;
            sCasoid = listViewCasos.SelectedItems[0].SubItems[1].Text;
            DialogResult = DialogResult.OK;
            this.Close();
            listViewCasos.Items.Clear();
            tbexpediente.Clear();
            tbInteresado.Clear();
            tbPrioridad.Clear();
            tbDenominacion.Clear();
            tbregistro.Clear();
            tbCliente.Clear();
            tbreferencia.Clear();
            tbCasoid.Clear();
            //fTmarcas objmarca = new fTmarcas(fLoguin, captura, this, sCasoIdcaso);
            //this.Hide();
            //objmarca.Show();

            //switch (sTipocaso)
            //{
            //    case "Patente":
            //        {
            //            consultacaso cConsultaid = new consultacaso(fLoguin, captura, this, sCasoIdcaso);
            //            this.Hide();
            //            cConsultaid.Show();
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
                        
            //        } break;
            //    case "Nombre Comercial":
            //        {
            //            // MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Aviso Comercial":
            //        {
            //            //MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Procedimiento contencioso":
            //        {
            //            fTcontencioso obj = new fTcontencioso(fLoguin, captura, this, sCasoIdcaso);
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
            //            fToposiciones obbj = new fToposiciones(fLoguin, captura, this, sCasoIdcaso);
            //            obbj.Show();

            //        } break;
            //    case "Registro de Obra":
            //        {
            //            fTderechosdeautor objDerechosaut = new fTderechosdeautor(fLoguin, captura, this, sCasoIdcaso);
            //            //consultacaso cConsultaid = new consultacaso(loguin, captura, this, sCasoIdcaso);
            //            //this.Hide();
            //            objDerechosaut.Show();
            //        } break;
            //    case "Reserva de Derechos":
            //        {

            //            fTreservaderechos objReserva = new fTreservaderechos(fLoguin, captura, this, sCasoIdcaso);
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

        private void bMarcas_Load(object sender, EventArgs e)
        {

        }

        private void cbTiposolicitud_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

    }
}
