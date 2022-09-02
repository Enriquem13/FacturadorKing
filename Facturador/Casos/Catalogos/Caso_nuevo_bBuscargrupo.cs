using Facturador.Casos.Marcas;
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
    public partial class Caso_nuevo_bBuscargrupo : Form
    {
        public captura cObjcap;
        public Form1 ologuin;
        private ComboboxItem comGrop;
        public Boolean iTransferido = false;// 0 caso nuevo 1 casotransferido
        public Caso_nuevo_bBuscargrupo(captura cObj, Form1 loguin)
        {
            try {
                ologuin = loguin;
                cObjcap = cObj;
                cObjcap.Hide();
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


                }catch(Exception E){
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
            try{
                if (cbTipodecaso.SelectedItem != null)
                {
                    String sTiposolicitud = (cbTipodecaso.SelectedItem as ComboboxItem).Text;
                    String sTipogruposolicitud = (cbTipodecaso.SelectedItem as ComboboxItem).Value.ToString();//pasamos el valor del grupo 
                    int iTiposolicitud = Int32.Parse((cbTipodecaso.SelectedItem as ComboboxItem).Value.ToString());
                
                    switch (sTiposolicitud)
                    {
                        //case "Todos":
                        //    {
                        //        bAll consul = new bAll(iTiposolicitud, cObjcap, ologuin);
                        //        consul.Show();
                        //        this.Close();
                        //        //this.Close();
                        //    } break;
                        case "Patentes":
                            {

                                Nuevocaso ncaso = new Nuevocaso(iTiposolicitud, cObjcap, ologuin);
                                cObjcap.Hide();
                                ncaso.Show();
                                this.Close();
                            } break;
                        case "Marcas":
                            {
                                //Nuevocaso_marcas ncaso = new Nuevocaso_marcas(iTiposolicitud, cObjcap, ologuin);
                                //cObjcap.Hide();
                                //ncaso.Show();
                                //this.Close();
                                if (iTransferido)
                                {
                                    casoNuevomarcas_transferido ncaso = new casoNuevomarcas_transferido(2, cObjcap, ologuin);
                                    this.Close();
                                    ncaso.Show();
                                }
                                else
                                {
                                    Nuevocaso_marcas ncaso = new Nuevocaso_marcas(2, cObjcap, ologuin);
                                    this.Close();
                                    ncaso.Show();
                                }
                            } break;
                        case "Contencioso":
                            {
                                CasoNuevoContencioso cobjNuevo = new CasoNuevoContencioso(iTiposolicitud, cObjcap, ologuin);
                                cobjNuevo.Show();
                                cObjcap.Hide();
                                this.Close();
                            } break;
                        case "Consulta":
                            {
                                CasonuevoConsulta objConsulta = new CasonuevoConsulta(iTiposolicitud, cObjcap, ologuin);
                                objConsulta.Show();
                                cObjcap.Hide();
                                this.Close();
                            } break;
                        case "Oposicion a solicitudes":
                            {
                                CasoNuevoOposicion objConsulta = new CasoNuevoOposicion(iTiposolicitud, cObjcap, ologuin);
                                objConsulta.Show();
                                cObjcap.Hide();
                                this.Close();
                            } break;
                        case "Variedades vegetales":
                            {
                                MessageBox.Show("En desarrollo");
                            } break;
                        case "Derechos de autor":
                            {
                                casoNuevoDerechosdeautor objConsulta = new casoNuevoDerechosdeautor(iTiposolicitud, cObjcap, ologuin);
                                objConsulta.Show();
                                cObjcap.Hide();
                                this.Close();
                            } break;
                        case "Reserva de derechos":
                            {
                                casoNuevoReservadederechos objConsulta = new casoNuevoReservadederechos(iTiposolicitud, cObjcap, ologuin);
                                objConsulta.Show();
                                cObjcap.Hide();
                                this.Close();
                            } break;
                    }
                    //(cbTipodecaso.SelectedItem as ComboBox).Valu;
                }
                else {
                    MessageBox.Show("Debe seleccionar un Tipo de caso");
                }
            }catch(Exception E){
                new filelog(ologuin.sId, E.ToString());
            }
        }

        private void cbTipodecaso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                this.Text = "Seleccione Tipo de Caso nuevo";
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

        private void button2_Click(object sender, EventArgs e)
        {
            cObjcap.Show();
            this.Close();
        }

        private void cbTipodecaso_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    //MessageBox.Show("Mensaje enviado");
            //    button1_Click(sender, e);
            //}
            try
            {
                comGrop = new ComboboxItem();
                switch (e.KeyCode)
                {
                    //caso1
                    case Keys.D1:
                        {
                            if (iTransferido) {
                                NuevocasoTransferido ncaso = new NuevocasoTransferido(1, cObjcap, ologuin);
                                this.Hide();
                                ncaso.Show();
                            } else {
                                Nuevocaso ncaso = new Nuevocaso(1, cObjcap, ologuin);
                                this.Hide();
                                ncaso.Show();
                            }
                            
                        } break;
                    case Keys.NumPad1:
                        {
                            if (iTransferido)
                            {
                                NuevocasoTransferido ncaso = new NuevocasoTransferido(1, cObjcap, ologuin);
                                this.Hide();
                                ncaso.Show();
                            }
                            else
                            {
                                Nuevocaso ncaso = new Nuevocaso(1, cObjcap, ologuin);
                                this.Hide();
                                ncaso.Show();
                            }
                        } break;
                    //caso2
                    case Keys.D2:
                        {
                            //Nuevocaso_marcas ncaso = new Nuevocaso_marcas(2, cObjcap, ologuin);
                            //this.Hide();
                            //ncaso.Show();
                            if (iTransferido)
                            {
                                casoNuevomarcas_transferido ncaso = new casoNuevomarcas_transferido(2, cObjcap, ologuin);
                                this.Close();
                                ncaso.Show();
                            }
                            else
                            {
                                Nuevocaso_marcas ncaso = new Nuevocaso_marcas(2, cObjcap, ologuin);
                                this.Close();
                                ncaso.Show();
                            }
                        } break;

                    case Keys.NumPad2:
                        {
                            //Nuevocaso_marcas ncaso = new Nuevocaso_marcas(2, cObjcap, ologuin);
                            //this.Hide();
                            //ncaso.Show();
                            if (iTransferido)
                            {
                                casoNuevomarcas_transferido ncaso = new casoNuevomarcas_transferido(2, cObjcap, ologuin);
                                this.Close();
                                ncaso.Show();
                            }
                            else
                            {
                                Nuevocaso_marcas ncaso = new Nuevocaso_marcas(2, cObjcap, ologuin);
                                this.Close();
                                ncaso.Show();
                            }
                        } break;

                    //caso3
                    case Keys.D3:
                        {
                            CasoNuevoContencioso cobjNuevo = new CasoNuevoContencioso(3, cObjcap, ologuin);
                            cobjNuevo.Show();
                            this.Hide();
                        } break;
                    case Keys.NumPad3:
                        {
                            CasoNuevoContencioso cobjNuevo = new CasoNuevoContencioso(3, cObjcap, ologuin);
                            cobjNuevo.Show();
                            this.Hide();
                        } break;

                    //caso4
                    case Keys.D4:
                        {
                            CasonuevoConsulta objConsulta = new CasonuevoConsulta(4, cObjcap, ologuin);
                            objConsulta.Show();
                            this.Hide();
                        } break;
                    case Keys.NumPad4:
                        {
                            CasonuevoConsulta objConsulta = new CasonuevoConsulta(4, cObjcap, ologuin);
                            objConsulta.Show();
                            this.Hide();
                        } break;

                    //caso5
                    case Keys.D5:
                        {
                            CasoNuevoOposicion objConsulta = new CasoNuevoOposicion(5, cObjcap, ologuin);
                            objConsulta.Show();
                            this.Hide();
                        } break;
                    case Keys.NumPad5:
                        {
                            CasoNuevoOposicion objConsulta = new CasoNuevoOposicion(5, cObjcap, ologuin);
                            objConsulta.Show();
                            this.Hide();
                        } break;

                    //caso6
                    case Keys.D6:
                        {
                            MessageBox.Show("En desarrollo");
                        } break;
                    case Keys.NumPad6:
                        {
                            MessageBox.Show("En desarrollo");
                        } break;

                    //caso7
                    case Keys.D7:
                        {
                            casoNuevoDerechosdeautor objConsulta = new casoNuevoDerechosdeautor(7, cObjcap, ologuin);
                            objConsulta.Show();
                            this.Hide();
                        } break;
                    case Keys.NumPad7:
                        {
                            casoNuevoDerechosdeautor objConsulta = new casoNuevoDerechosdeautor(7, cObjcap, ologuin);
                            objConsulta.Show();
                            this.Hide();
                        } break;
                    //caso8
                    case Keys.D8:
                        {
                            casoNuevoReservadederechos objConsulta = new casoNuevoReservadederechos(8, cObjcap, ologuin);
                            objConsulta.Show();
                            this.Hide();
                        } break;
                    case Keys.NumPad8:
                        {
                            casoNuevoReservadederechos objConsulta = new casoNuevoReservadederechos(8, cObjcap, ologuin);
                            objConsulta.Show();
                            this.Hide();
                        } break;
                    case Keys.Enter:
                        {
                            if ((cbTipodecaso.SelectedItem as ComboboxItem).Text != "")
                            {
                                button1_Click(sender, e);
                            }

                        } break;

                }
                //despues de asignar un valor validamos y ejecutamos
            }
            catch (Exception E)
            {
                new filelog(ologuin.sId, E.ToString());
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            cObjcap.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //caso_transferido ncaso = new caso_transferido(1, cObjcap, ologuin);
            //ncaso.Text = ncaso.Text + "Transferido";
            //this.Hide();
            //ncaso.Show();
            if (iTransferido)
            {
                NuevocasoTransferido ncaso = new NuevocasoTransferido(1, cObjcap, ologuin);
                this.Hide();
                ncaso.Show();
            }
            else {
                Nuevocaso ncaso = new Nuevocaso(1, cObjcap, ologuin);
                ncaso.Show();
                this.Hide();

                //cObjcap.Hide();
            }

            //}
            //else { 

            //}

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (iTransferido)
            {
                casoNuevomarcas_transferido ncaso = new casoNuevomarcas_transferido(2, cObjcap, ologuin);
                this.Hide();
                ncaso.Show();
            }else {
                Nuevocaso_marcas ncaso = new Nuevocaso_marcas(2, cObjcap, ologuin);
                this.Hide();
                ncaso.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CasoNuevoContencioso cobjNuevo = new CasoNuevoContencioso(3, cObjcap, ologuin);
            cobjNuevo.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CasonuevoConsulta objConsulta = new CasonuevoConsulta(4, cObjcap, ologuin);
            objConsulta.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CasoNuevoOposicion objConsulta = new CasoNuevoOposicion(5, cObjcap, ologuin);
            objConsulta.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("En desarrollo");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Reservadederecho objConsulta = new Reservadederecho();
            casoNuevoDerechosdeautor objConsulta = new casoNuevoDerechosdeautor(7, cObjcap, ologuin);
            objConsulta.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            casoNuevoReservadederechos objConsulta = new casoNuevoReservadederechos(8, cObjcap, ologuin);
            objConsulta.Show();
            this.Hide();
        }

        private void Caso_nuevo_bBuscargrupo_Load(object sender, EventArgs e)
        {
            try {
                if (iTransferido)
                {
                    lbCasonumevotransfer.Visible = true;
                }
                else
                {
                    lbCasonumevotransfer.Visible = false;
                }
            }
            catch (Exception exsd) {
                new filelog("", ""+exsd.Message);
            }
        }
    }
}
