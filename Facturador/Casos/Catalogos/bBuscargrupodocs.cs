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
    public partial class bBuscargrupodocs : Form
    {
        public captura cObjcap;
        public Form1 ologuin;
        public ComboboxItem comGrop { get; set; }
        public bBuscargrupodocs(captura cObj, Form1 loguin)
        {
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
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else {
                MessageBox.Show("Debe seleccionar un Tipo de caso");
            }
        }

        private void cbTipodecaso_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                switch (sTiposolicitud)
                {
                    
                    case "Patentes":
                        {
                            this.BackColor = Color.Pink;
                            this.Text = this.Text + " ( Grupo Patentes)";
                        } break;
                    case "Marcas":
                        {
                            this.BackColor = Color.FromArgb(255, 255, 192);
                            this.Text = this.Text + " ( Grupo Marcas)";
                        } break;
                    case "Contencioso":
                        {
                            this.BackColor = Color.Yellow;
                            this.Text = this.Text + " ( Grupo Contencioso)";
                        } break;
                    case "Consulta":
                        {
                            this.BackColor = SystemColors.Control;
                            this.Text = this.Text + " ( Grupo Consulta)";
                        } break;
                    case "Oposicion a solicitudes":
                        {
                            
                            this.BackColor = Color.FromArgb(255, 192, 128);
                            this.Text = this.Text + " ( Grupo Oposicion a solicitudes)";
                        } break;
/*                    case "Variedades vegetales":
                        {
                            Informacion.Text = "Todos los Tipos de Solicitudes";
                        } break;*/
                    case "Derechos de autor":
                        {
                            this.BackColor = Color.SkyBlue;
                            this.Text = this.Text + " ( Grupo Derechos de autor)";
                        } break;
                    case "Reserva de derechos":
                        {
                            this.BackColor = Color.LightGreen;
                            this.Text = this.Text + " ( Grupo Reserva de derechos)";
                        } break;
                    default: { this.BackColor = SystemColors.Control; } break;
                }
                //(cbTipodecaso.SelectedItem as ComboBox).Valu;
            }
            else
            {
                Informacion.Text = "Todos los Tipos de Solicitudes";
                //MessageBox.Show("Debe seleccionar un Tipo de caso");
            }
        }

        private void cbTipodecaso_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                comGrop = new ComboboxItem();
                switch (e.KeyCode)
                {
                    //caso1
                    case Keys.D1:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 1;
                        } break;
                    case Keys.NumPad1:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 1;
                        } break;
                    //caso2
                    case Keys.D2:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 2;
                        } break;

                    case Keys.NumPad2:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 2;
                        } break;

                    //caso3
                    case Keys.D3:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 3;
                        } break;
                    case Keys.NumPad3:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 3;
                        } break;

                    //caso4
                    case Keys.D4:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 4;
                        } break;
                    case Keys.NumPad4:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 4;
                        } break;

                    //caso5
                    case Keys.D5:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 5;
                        } break;
                    case Keys.NumPad5:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 5;
                        } break;

                    //caso6
                    case Keys.D6:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 6;
                        } break;
                    case Keys.NumPad6:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 6;
                        } break;

                    //caso7
                    case Keys.D7:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 7;
                        } break;
                    case Keys.NumPad7:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 7;
                        } break;
                    //caso8
                    case Keys.D8:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 8;
                        } break;
                    case Keys.NumPad8:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 8;
                        } break;
                    case Keys.Enter: {
                        if((cbTipodecaso.SelectedItem as ComboboxItem).Text!=""){
                            comGrop = (cbTipodecaso.SelectedItem as ComboboxItem);
                        }
                        
                    }break;
                    
                }
                //despues de asignar un valor validamos y ejecutamos
                if (comGrop.Value != null)
                {
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }catch(Exception E){
                new filelog(ologuin.sId, E.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                comGrop = new ComboboxItem();
                comGrop.Text = "";
                comGrop.Value = 1;
                DialogResult = DialogResult.OK;
                this.Close();
            }catch(Exception E){
                new filelog(ologuin.sId, E.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                comGrop = new ComboboxItem();
                comGrop.Text = "";
                comGrop.Value = 2;
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception E)
            {
                new filelog(ologuin.sId, E.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                comGrop = new ComboboxItem();
                comGrop.Text = "";
                comGrop.Value = 3;
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception E)
            {
                new filelog(ologuin.sId, E.ToString());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                comGrop = new ComboboxItem();
                comGrop.Text = "";
                comGrop.Value = 4;
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception E)
            {
                new filelog(ologuin.sId, E.ToString());
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                comGrop = new ComboboxItem();
                comGrop.Text = "";
                comGrop.Value = 5;
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception E)
            {
                new filelog(ologuin.sId, E.ToString());
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                comGrop = new ComboboxItem();
                comGrop.Text = "";
                comGrop.Value = 6;
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception E)
            {
                new filelog(ologuin.sId, E.ToString());
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                comGrop = new ComboboxItem();
                comGrop.Text = "";
                comGrop.Value = 7;
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception E)
            {
                new filelog(ologuin.sId, E.ToString());
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                comGrop = new ComboboxItem();
                comGrop.Text = "";
                comGrop.Value = 8;
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception E)
            {
                new filelog(ologuin.sId, E.ToString());
            }
        }

        private void bBuscargrupodocs_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                comGrop = new ComboboxItem();
                switch (e.KeyCode)
                {
                    //caso1
                    case Keys.D1:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 1;
                        } break;
                    case Keys.NumPad1:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 1;
                        } break;
                    //caso2
                    case Keys.D2:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 2;
                        } break;

                    case Keys.NumPad2:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 2;
                        } break;

                    //caso3
                    case Keys.D3:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 3;
                        } break;
                    case Keys.NumPad3:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 3;
                        } break;

                    //caso4
                    case Keys.D4:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 4;
                        } break;
                    case Keys.NumPad4:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 4;
                        } break;

                    //caso5
                    case Keys.D5:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 5;
                        } break;
                    case Keys.NumPad5:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 5;
                        } break;

                    //caso6
                    case Keys.D6:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 6;
                        } break;
                    case Keys.NumPad6:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 6;
                        } break;

                    //caso7
                    case Keys.D7:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 7;
                        } break;
                    case Keys.NumPad7:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 7;
                        } break;
                    //caso8
                    case Keys.D8:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 8;
                        } break;
                    case Keys.NumPad8:
                        {
                            comGrop.Text = "";
                            comGrop.Value = 8;
                        } break;
                    case Keys.Enter:
                        {
                            if ((cbTipodecaso.SelectedItem as ComboboxItem).Text != "")
                            {
                                comGrop = (cbTipodecaso.SelectedItem as ComboboxItem);
                            }

                        } break;

                }
                //despues de asignar un valor validamos y ejecutamos
                if (comGrop.Value != null)
                {
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception E)
            {
                new filelog(ologuin.sId, E.ToString());
            }
        }
    }
}
