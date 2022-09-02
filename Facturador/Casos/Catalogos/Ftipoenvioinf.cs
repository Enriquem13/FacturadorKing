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
    public partial class Ftipoenvioinf : Form
    {
        public Form1 oFormlogin;
        public captura capFormcap;
        conect con1;
        public Ftipoenvioinf(Form1 form, captura Formcap)
        {
            oFormlogin = form;
            capFormcap = Formcap;
            InitializeComponent();

            con1 = new conect();
            String query2 = "SELECT * FROM tipoenviofac";
            MySqlDataReader respuestastring20 = con1.getdatareader(query2);

            String siniTipoEnvioFacId = "";
            String siniTipoEnvioFacDescrip = "";
            String siniTipoEnvioFacObserv = "";

            while (respuestastring20.Read())
            {
                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("TipoEnvioFacId")))
                {
                    siniTipoEnvioFacId = respuestastring20.GetString(respuestastring20.GetOrdinal("TipoEnvioFacId"));
                }
                else
                {
                    siniTipoEnvioFacId = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("TipoEnvioFacDescrip")))
                {
                    siniTipoEnvioFacDescrip = respuestastring20.GetString(respuestastring20.GetOrdinal("TipoEnvioFacDescrip"));
                }
                else
                {
                    siniTipoEnvioFacDescrip = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("TipoEnvioFacObserv")))
                {
                    siniTipoEnvioFacObserv = respuestastring20.GetString(respuestastring20.GetOrdinal("TipoEnvioFacObserv"));
                }
                else
                {
                    siniTipoEnvioFacObserv = "";
                }

                ListViewItem listaagregar = new ListViewItem(siniTipoEnvioFacDescrip);
                listaagregar.SubItems.Add(siniTipoEnvioFacObserv);
                listView1.Items.Add(listaagregar);

            }


        }

        private void Bagregar_Click(object sender, EventArgs e)

        {

            if (!TB_nombre_tipoenvio.Text.Equals("") && !TB_observaciones_tipoenvio.Text.Equals(""))
            {
                try
                {

                    String queryinsert = "INSERT INTO `tipoenviofac` (`TipoEnvioFacId`, `TipoEnvioFacDescrip`, `TipoEnvioFacObserv`, `TipoEnvioFacIndAct`) VALUES (NULL,'" + TB_nombre_tipoenvio.Text + "', '" +TB_observaciones_tipoenvio.Text+ "', '" + 1 + "');";
                    MySqlDataReader respuestastringinsert = con1.getdatareader(queryinsert);

                    if (respuestastringinsert == null)
                    {
                        MessageBox.Show("Fallo");
                    }
                    else
                    {
                        MessageBox.Show("Se inserto");

                        ListViewItem listaagregar = new ListViewItem(TB_nombre_tipoenvio.Text);
                        listaagregar.SubItems.Add(TB_observaciones_tipoenvio.Text);
                        listView1.Items.Add(listaagregar);
                        TB_nombre_tipoenvio.Text = "";
                        TB_observaciones_tipoenvio.Text = "";
                    }

                }
                catch (Exception E)
                {
                    Console.WriteLine("{0} Exception caught.", E);
                }


            }
            else {
                MessageBox.Show("Debe llenar todos los campos");
            }



        }

        private void Beliminar_Click(object sender, EventArgs e)
        {

        }

        private void BT_menuenvio_Click(object sender, EventArgs e)
        {
            capFormcap.Show();
            this.Close();

        }

        private void BT_salir_Click(object sender, EventArgs e)
        {
            capFormcap.Close();
            oFormlogin.Close();
            this.Close();
        }

    }
}
