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
    public partial class Fpais : Form
    {
        public Form1 oFormlogin;
        public captura capFormcap;
        conect con1;
        public Fpais(Form1 form, captura Formcap)
        {
            oFormlogin = form;
            capFormcap = Formcap;
            InitializeComponent();

            con1 = new conect();
            String query2 = "SELECT * FROM pais ";
            MySqlDataReader respuestastring20 = con1.getdatareader(query2);

            String siniPaisId = "";
            String siniPaisNombre = "";
            String siniPaisNacionalidad = "";
            String siniPaisNombreIngles = "";
            String siniPaisNacionalidadIngles = "";
            String siniPaisClave = "";

            while (respuestastring20.Read())
            {
                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("PaisId")))
                {
                    siniPaisId = respuestastring20.GetString(respuestastring20.GetOrdinal("PaisId"));
                }
                else
                {
                    siniPaisId = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("PaisNombre")))
                {
                    siniPaisNombre = respuestastring20.GetString(respuestastring20.GetOrdinal("PaisNombre"));
                }
                else
                {
                    siniPaisNombre = "";
                }
                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("PaisNacionalidad")))
                {
                    siniPaisNacionalidad = respuestastring20.GetString(respuestastring20.GetOrdinal("PaisNacionalidad"));
                }
                else
                {
                    siniPaisNacionalidad = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("PaisNombreIngles")))
                {
                    siniPaisNombreIngles = respuestastring20.GetString(respuestastring20.GetOrdinal("PaisNombreIngles"));
                }
                else
                {
                    siniPaisNombreIngles = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("PaisNacionalidadIngles")))
                {
                    siniPaisNacionalidadIngles = respuestastring20.GetString(respuestastring20.GetOrdinal("PaisNacionalidadIngles"));
                }
                else
                {
                    siniPaisNacionalidadIngles = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("PaisClave")))
                {
                    siniPaisClave = respuestastring20.GetString(respuestastring20.GetOrdinal("PaisClave"));
                }
                else
                {
                    siniPaisClave = "";
                }


                ListViewItem listaagregar = new ListViewItem(siniPaisClave);
                listaagregar.SubItems.Add(siniPaisNombre);
                listaagregar.SubItems.Add(siniPaisNacionalidad);
                listaagregar.SubItems.Add(siniPaisNombreIngles);
                listaagregar.SubItems.Add(siniPaisNacionalidadIngles);
                listView1.Items.Add(listaagregar);
            }





        }

        private void Bagregar_Click(object sender, EventArgs e)
        {
            if(!TB_clavepais.Text.Equals("") && !TB_nombre_pais_esp.Text.Equals("") && !TB_nacionalidad_esp.Text.Equals("") 
                && !TB_nombrepais_ing.Text.Equals("") && !TB_inglesnacionalidad.Text.Equals("")){


                    try
                    {
                        String queryinsert = "INSERT INTO `pais` (`PaisId`, `PaisClave`, `PaisNombre`, `PaisNick`, `PaisNacionalidad`, `PaisIndAct`, `PaisNombreIngles`, `PaisNacionalidadIngles`)"
                            + "VALUES (NULL,'" + TB_clavepais.Text + "', '" + TB_nombre_pais_esp.Text + "', " + " NULL,'" +TB_nacionalidad_esp.Text+
                            "', " + " 1,'" + TB_nombrepais_ing.Text + "', '" + TB_inglesnacionalidad.Text + "');";
                        MySqlDataReader respuestastringinsert = con1.getdatareader(queryinsert);

                        if (respuestastringinsert == null)
                        {
                            MessageBox.Show("Fallo");
                        }
                        else
                        {
                            MessageBox.Show("Se inserto");
                            ListViewItem listaagregar = new ListViewItem(TB_clavepais.Text);
                            listaagregar.SubItems.Add(TB_nombre_pais_esp.Text);
                            listaagregar.SubItems.Add(TB_nacionalidad_esp.Text);
                            listaagregar.SubItems.Add(TB_nombrepais_ing.Text);
                            listaagregar.SubItems.Add(TB_inglesnacionalidad.Text);
                            listView1.Items.Add(listaagregar);

                            limpiarcasillas();

                        }


                    }
                    catch (Exception E)
                    {
                        Console.WriteLine("{0} Exception caught.", E);
                    }

            }else{
                MessageBox.Show("Debe llenar todos los campos");
            }
        }

        public void limpiarcasillas()
        {
            TB_clavepais.Text = "";
            TB_nombre_pais_esp.Text = "";
            TB_nacionalidad_esp.Text = "";
            TB_nombrepais_ing.Text = "";
            TB_inglesnacionalidad.Text = "";
        }
        private void BT_menupais_Click(object sender, EventArgs e)
        {
            capFormcap.Show();
            this.Close();
        }

        private void BT_salirpais_Click(object sender, EventArgs e)
        {
            capFormcap.Close();
            oFormlogin.Close();
            this.Close();
        }

 


    }
}
