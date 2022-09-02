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
    public partial class Ftipo_cliente : Form
    {
        public Form1 oFormlogin;
        public captura capFormcap;
        conect con1;
        public Ftipo_cliente(Form1 form, captura Formcap)
        {
            oFormlogin = form;
            capFormcap = Formcap;
            InitializeComponent();

            con1 = new conect();
            String query2 = "SELECT * FROM tipocliente ";
            MySqlDataReader respuestastring20 = con1.getdatareader(query2);

            String siniTipoClienteId = "";
            String siniTipoClienteDescrip = "";

            while (respuestastring20.Read())
            {
                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("TipoClienteId")))
                {
                    siniTipoClienteId = respuestastring20.GetString(respuestastring20.GetOrdinal("TipoClienteId"));
                }
                else
                {
                    siniTipoClienteId = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("TipoClienteDescrip")))
                {
                    siniTipoClienteDescrip = respuestastring20.GetString(respuestastring20.GetOrdinal("TipoClienteDescrip"));
                }
                else
                {
                    siniTipoClienteDescrip = "";
                }

                ListViewItem listaagregar = new ListViewItem(siniTipoClienteDescrip);
                listView1.Items.Add(listaagregar);

            }


            //listView1.View = View.Details;
            //listView1.FullRowSelect = true;
            //listView1.Columns.Add("Tipo Cliente", 300);
            
        }

        private void Ftipo_cliente_Load(object sender, EventArgs e)
        {

        }

        private void Bagregar_Click(object sender, EventArgs e)
        {
            if (!TB_tipocliente.Text.Equals(""))
            {
                try
                {
              
                    String queryinsert = "INSERT INTO `tipocliente` (`TipoClienteId`, `TipoClienteDescrip`, `TipoClienteIndAct`) VALUES (NULL,'" + TB_tipocliente.Text + "', '" + 1 + "');";
                    MySqlDataReader respuestastringinsert = con1.getdatareader(queryinsert);

                    if (respuestastringinsert == null)
                    {
                        MessageBox.Show("Fallo");
                    }
                    else
                    {
                        MessageBox.Show("Se inserto");
                        

                        ListViewItem listaagregar = new ListViewItem(TB_tipocliente.Text);
                        listView1.Items.Add(listaagregar);
                        TB_tipocliente.Text = "";
                    }

                }
                catch (Exception E)
                {
                    Console.WriteLine("{0} Exception caught.", E);
                }
            }
            else {
                MessageBox.Show("Debe llenar el campo");
            
            }


        }

        private void Beliminar_Click(object sender, EventArgs e)
        {

        }

        private void BT_menutipocliente_Click(object sender, EventArgs e)
        {
            capFormcap.Show();
            this.Close();
        }

        private void BT_salirtipocliente_Click(object sender, EventArgs e)
        {
            capFormcap.Close();
            oFormlogin.Close();
            this.Close();
        }
    }
}
