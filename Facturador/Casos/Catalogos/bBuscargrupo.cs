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
    public partial class bBuscargrupo : Form
    {
        public captura cObjcap;
        public Form1 ologuin;
        public ComboboxItem comGrop { get; set; }
        public bBuscargrupo(captura cObj, Form1 loguin)
        {
            try { 
                ologuin = loguin;
                cObjcap = cObj;
                InitializeComponent();
                button3.BackColor = Color.Pink;//Patentes
                button4.BackColor = Color.FromArgb(255, 255, 192);//Marcas
                button5.BackColor = Color.Yellow;//Contencioso
                button6.BackColor = SystemColors.Control;//Consulta
                button7.BackColor = Color.FromArgb(255, 192, 128);//Oposision
                button8.BackColor = SystemColors.Control;//Variedad Vegetal
                button9.BackColor = Color.SkyBlue;//Derechos de autor
                button10.BackColor = Color.LightGreen;//reserva de derechos

                conect con = new conect();
                String query = "select * from grupo;";
                MySqlDataReader respuestastiposol = con.getdatareader(query);
                while (respuestastiposol.Read())
                {
                    cbTipodecaso.Items.Add(validareader("GrupoDescripcion", "GrupoId", respuestastiposol));
                }
                respuestastiposol.Close();
                con.Cerrarconexion();
                //ComboboxItem cAll = new ComboboxItem();
                //cAll.Text = "Todos";
                //cAll.Value = 0;
                //cbTipodecaso.Items.Add(cAll);
            }
            catch (Exception E)
            {
                new filelog(loguin.sId, E.ToString());
            }
            //ComboboxItem cDerechosdeautor = new ComboboxItem();
            //cDerechosdeautor.Text = "Derechos de autor";
            //cDerechosdeautor.Value = 8;
            //ComboboxItem cReservaderechos = new ComboboxItem();
            //cReservaderechos.Text = "Reserva de derechos";
            //cReservaderechos.Value = 9;
            //cbTipodecaso.Items.Add(cDerechosdeautor);
            //cbTipodecaso.Items.Add(cReservaderechos);
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
            if (cbTipodecaso.SelectedItem != null)
            {
                comGrop = (cbTipodecaso.SelectedItem as ComboboxItem);
                String sTiposolicitud = (cbTipodecaso.SelectedItem as ComboboxItem).Text;
                int iTiposolicitud = Int32.Parse((cbTipodecaso.SelectedItem as ComboboxItem).Value.ToString());
                //String sTiposolenv = "";
                //Consutlacaso obj = new Consutlacaso(ologuin, cObjcap, 1);
                //obj.Show();
                //this.Hide();
                switch (sTiposolicitud)
                {
                    case "Todos":
                        {
                            bAll consul = new bAll(iTiposolicitud, cObjcap, ologuin);
                            consul.Show();
                            this.Close();
                            //this.Close();
                        } break;
                    case "Patentes":
                        {

                            Consutlacaso consul = new Consutlacaso(ologuin, cObjcap, iTiposolicitud);
                            consul.Show();
                            this.Close();
                            //this.Close();
                        } break;
                    case "Marcas":
                        {
                            bMarcas consul = new bMarcas(iTiposolicitud, cObjcap, ologuin);
                            consul.Show();
                            this.Close();
                            //this.Close();
                            //MessageBox.Show("En desarrollo");
                        } break;
                    case "Contencioso":
                        {
                            bContencioso consul = new bContencioso(iTiposolicitud, cObjcap, ologuin);
                            consul.Show();
                            this.Close();
                            //MessageBox.Show("En desarrollo");
                        } break;
                    case "Consulta":
                        {
                            bConsulta consul = new bConsulta(iTiposolicitud, cObjcap, ologuin);
                            consul.Show();
                            this.Close();
                            //this.Close();
                            //MessageBox.Show("En desarrollo");
                        } break;
                    case "Oposicion a solicitudes":
                        {
                            bOposicion consul = new bOposicion(iTiposolicitud, cObjcap, ologuin);
                            consul.Show();
                            this.Close();
                            //this.Close();
                        } break;
                    case "Variedades vegetales":
                        {
                            bVariedadv conul = new bVariedadv(iTiposolicitud, cObjcap, ologuin);
                            conul.Show();
                            this.Close();
                        } break;
                    case "Derechos de autor":
                        {
                            bDerechoautor conult = new bDerechoautor(7, cObjcap, ologuin);
                            conult.Show();
                            this.Close();
                            //this.Close();
                        } break;
                    case "Reserva de derechos":
                        {
                            bReservadederechos conul = new bReservadederechos(8, cObjcap, ologuin);
                            conul.Show();
                            this.Close();
                        } break;
                        
                    
                }
                //(cbTipodecaso.SelectedItem as ComboBox).Valu;
            }
            else {
                MessageBox.Show("Debe seleccionar un Tipo de caso");
            }
        }

        private void cbTipodecaso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { 
                this.Text = "Seleccione Tipo de Caso";
                if (cbTipodecaso.SelectedItem != null)
                {
                    String sTiposolicitud = (cbTipodecaso.SelectedItem as ComboboxItem).Text;
                    int iTiposolicitud = Int32.Parse((cbTipodecaso.SelectedItem as ComboboxItem).Value.ToString());
                    //String sTiposolenv = "";
                    //Consutlacaso obj = new Consutlacaso(ologuin, cObjcap, 1);
                    //obj.Show();
                    //this.Hide();
                    conect con = new conect();
                    String querytipo = "select * from tiposolicitud where TipoSolicitudGrupo = " + iTiposolicitud;
                    MySqlDataReader respuestastipo = con.getdatareader(querytipo);
                    Informacion.Text = "";
                    while (respuestastipo.Read())
                    {
                        //cbTipodecaso.Items.Add(validareader("GrupoDescripcion", "GrupoId", respuestastipo));
                        Informacion.Text += validareader("TipoSolicitudDescrip", "tiposolicitudid", respuestastipo) + " \n\n";
                    }
                    respuestastipo.Close();
                    con.Cerrarconexion();
                    switch (sTiposolicitud)
                    {
                        case "Todos":
                            {
                                this.Text = this.Text + " ( TODO TIPO DE CASOS)";
                                this.BackColor = SystemColors.Control;
                            
                            } break;
                        case "Patentes":
                            {

                                this.Text = this.Text + " ( Grupo Patentes)";
                                this.BackColor = Color.Pink;
                                //Informacion.Text = "Todos los Tipos de Solicitudes";
                            } break;
                        case "Marcas":
                            {
                                this.Text = this.Text + " ( Grupo Marcas)";
                                this.BackColor = Color.FromArgb(255, 255, 192);
                            } break;
                        case "Contencioso":
                            {
                                this.Text = this.Text + " ( Grupo Contencioso)";
                                this.BackColor = Color.Yellow;
                            } break;
                        case "Consulta":
                            {
                                this.Text = this.Text + " ( Grupo Consulta)";
                                this.BackColor = SystemColors.Control;
                            } break;
                        case "Oposicion a solicitudes":
                            {
                                this.Text = this.Text + " ( Grupo Oposicion a solicitudes)";
                                this.BackColor = Color.FromArgb(255, 192, 128);
                            } break;
                        case "Variedades vegetales":
                            {
                                this.Text = this.Text + " ( Grupo Variedades vegetales)";
                                this.BackColor = SystemColors.Control;
                            } break;
                        case "Derechos de autor":
                            {
                                this.Text = this.Text + " ( Grupo Derechos de autor)";
                                this.BackColor = Color.SkyBlue;
                            } break;
                        case "Reserva de derechos":
                            {
                                this.Text = this.Text + " ( Grupo Reserva de derechos)";
                                this.BackColor = Color.LightGreen;
                            } break;
                    }
                    //(cbTipodecaso.SelectedItem as ComboBox).Valu;
                }
                else
                {
                    Informacion.Text = "Todos los Tipos de Solicitudes";
                    //MessageBox.Show("Debe seleccionar un Tipo de caso");
                }
            }catch(Exception E){
                new filelog(ologuin.sId, E.ToString());            
            }
        }

        private void cbTipodecaso_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    //MessageBox.Show("Mensaje enviado");
            //    switch (e.KeyCode)
            //    {
            //        case Keys.D1:
            //            {
            //                Consutlacaso consul = new Consutlacaso(ologuin, cObjcap, 1);
            //                consul.Show();
            //                this.Close();
            //            } break;
            //        case Keys.NumPad1:
            //            {
            //                Consutlacaso consul = new Consutlacaso(ologuin, cObjcap, 1);
            //                consul.Show();
            //                this.Close();
            //            } break;

            //    }
            //    //button1_Click(sender, e);
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cObjcap.Show();
            this.Close();
        }

        private void bBuscargrupo_Load(object sender, EventArgs e)
        {

        }

        private void bBuscargrupo_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.KeyCode == Keys.D1)
            {
            }*/
            switch (e.KeyCode)
                {
                    //caso1
                case Keys.D1:{
                    Consutlacaso consul = new Consutlacaso(ologuin, cObjcap, 1);
                    consul.Show();
                    this.Close();
                }break;
                case Keys.NumPad1:{
                    Consutlacaso consul = new Consutlacaso(ologuin, cObjcap, 1);
                    consul.Show();
                    this.Close();
                }break;
                    //caso2
                case Keys.D2:
                    {
                        bMarcas consul = new bMarcas(2, cObjcap, ologuin);
                        consul.Show();
                        this.Close();
                    } break;

                case Keys.NumPad2:
                    {
                        bMarcas consul = new bMarcas(2, cObjcap, ologuin);
                        consul.Show();
                        this.Close();
                    } break;

                    //caso3
                case Keys.D3:
                    {
                        bContencioso consul = new bContencioso(3, cObjcap, ologuin);
                        consul.Show();
                        this.Close();
                    } break;
                case Keys.NumPad3:
                    {
                        bContencioso consul = new bContencioso(3, cObjcap, ologuin);
                        consul.Show();
                        this.Close();
                    } break;

                    //caso4
                case Keys.D4:
                    {
                        bConsulta consul = new bConsulta(4, cObjcap, ologuin);
                        consul.Show();
                        this.Close();
                    } break;
                case Keys.NumPad4:
                    {
                        bConsulta consul = new bConsulta(4, cObjcap, ologuin);
                        consul.Show();
                        this.Close();
                    } break;

                    //caso5
                case Keys.D5:
                    {
                        bOposicion consul = new bOposicion(5, cObjcap, ologuin);
                        consul.Show();
                        this.Close();
                    } break;
                case Keys.NumPad5:
                    {
                        bOposicion consul = new bOposicion(5, cObjcap, ologuin);
                        consul.Show();
                        this.Close();
                    } break;

                    //caso6
                case Keys.D6:
                    {
                        bVariedadv conul = new bVariedadv(6, cObjcap, ologuin);
                        conul.Show();
                        this.Close();
                    } break;
                case Keys.NumPad6:
                    {
                        bVariedadv conul = new bVariedadv(6, cObjcap, ologuin);
                        conul.Show();
                        this.Close();
                    } break;

                    //caso7
                case Keys.D7:
                    {
                        bDerechoautor conult = new bDerechoautor(7, cObjcap, ologuin);
                        conult.Show();
                        this.Close();
                    } break;
                case Keys.NumPad7:
                    {
                        bDerechoautor conult = new bDerechoautor(7, cObjcap, ologuin);
                        conult.Show();
                        this.Close();
                    } break;


                    //caso8
                case Keys.D8:
                    {
                        bReservadederechos conul = new bReservadederechos(8, cObjcap, ologuin);
                        conul.Show();
                        this.Close();
                    } break;
                case Keys.NumPad8:
                    {
                        bReservadederechos conul = new bReservadederechos(8, cObjcap, ologuin);
                        conul.Show();
                        this.Close();
                    } break;
                case Keys.Enter:
                    {
                        if ((cbTipodecaso.SelectedItem as ComboboxItem).Text != "")
                        {
                            button1_Click(sender, e);
                            return;
                        }

                    } break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Consutlacaso consul = new Consutlacaso(ologuin, cObjcap, 1);
            consul.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bMarcas consul = new bMarcas(2, cObjcap, ologuin);
            consul.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bContencioso consul = new bContencioso(3, cObjcap, ologuin);
            consul.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bConsulta consul = new bConsulta(4, cObjcap, ologuin);
            consul.Show();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bOposicion consul = new bOposicion(5, cObjcap, ologuin);
            consul.Show();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bVariedadv conul = new bVariedadv(6, cObjcap, ologuin);
            conul.Show();
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bDerechoautor conult = new bDerechoautor(7, cObjcap, ologuin);
            conult.Show();
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            bReservadederechos conul = new bReservadederechos(8, cObjcap, ologuin);
            conul.Show();
            this.Close();
        }

    }
}
