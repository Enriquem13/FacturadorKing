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
    public partial class Fprovedorfact : Form
    {
        public Form1 oFormlogin;
        public captura capFormcap;
        conect con1;
        public Fprovedorfact(Form1 form, captura Formcap)
        {
            oFormlogin = form;
            capFormcap = Formcap;
            InitializeComponent();

            con1 = new conect();
            String query2 = "SELECT * FROM proveedorfacelec ";
            MySqlDataReader respuestastring20 = con1.getdatareader(query2);

            String siniProveedorFacElecId = "";
            String siniProveedorFacElecDescrip = "";
            String siniProveedorFacElecWebSite = "";
            String siniProveedorFacElecObserv = "";

            while (respuestastring20.Read())
            {
                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("ProveedorFacElecId")))
                {
                    siniProveedorFacElecId = respuestastring20.GetString(respuestastring20.GetOrdinal("ProveedorFacElecId"));
                }
                else
                {
                    siniProveedorFacElecId = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("ProveedorFacElecDescrip")))
                {
                    siniProveedorFacElecDescrip = respuestastring20.GetString(respuestastring20.GetOrdinal("ProveedorFacElecDescrip"));
                }
                else
                {
                    siniProveedorFacElecDescrip = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("ProveedorFacElecWebSite")))
                {
                    siniProveedorFacElecWebSite = respuestastring20.GetString(respuestastring20.GetOrdinal("ProveedorFacElecWebSite"));
                }
                else
                {
                    siniProveedorFacElecWebSite = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("ProveedorFacElecObserv")))
                {
                    siniProveedorFacElecObserv = respuestastring20.GetString(respuestastring20.GetOrdinal("ProveedorFacElecObserv"));
                }
                else
                {
                    siniProveedorFacElecObserv = "";
                }

                ListViewItem listaagregar = new ListViewItem(siniProveedorFacElecDescrip);
                listaagregar.SubItems.Add(siniProveedorFacElecWebSite);
                listaagregar.SubItems.Add(siniProveedorFacElecObserv);
                listView1.Items.Add(listaagregar);

            }

            //listView1.View = View.Details;
            //listView1.FullRowSelect = true;
            //listView1.Columns.Add("Proveedor", 100);
            //listView1.Columns.Add("Sito Web", 100);
            //listView1.Columns.Add("Descripción", 100);
        }

        private void Bagregar_Click(object sender, EventArgs e)
        {

            if(!TB_nombre_proovedorfac.Text.Equals("") && !TB_sitio_provedorfac.Text.Equals("") && !TB_observaciones_provfac.Text.Equals("")){

                try
                {
                    String queryinsert = "INSERT INTO `proveedorfacelec` (`ProveedorFacElecId`, `ProveedorFacElecDescrip`, `ProveedorFacElecWebSite`, `ProveedorFacElecObserv`, `ProveedorFacElecIndAct`)" 
                        +"VALUES (NULL, '" + TB_nombre_proovedorfac.Text + "', '" + TB_sitio_provedorfac.Text + "', '" + TB_observaciones_provfac.Text + "', '" + 1 + "');";
                    MySqlDataReader respuestastringinsert = con1.getdatareader(queryinsert);

                    if (respuestastringinsert == null)
                    {
                        MessageBox.Show("Fallo");
                    }
                    else
                    {
                        MessageBox.Show("Se inserto");

                        ListViewItem listaagregar = new ListViewItem(TB_nombre_proovedorfac.Text);
                        listaagregar.SubItems.Add(TB_sitio_provedorfac.Text);
                        listaagregar.SubItems.Add(TB_observaciones_provfac.Text);
                        listView1.Items.Add(listaagregar);


                        TB_nombre_proovedorfac.Text = "";
                        TB_observaciones_provfac.Text = "";
                        TB_sitio_provedorfac.Text = "";
                
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

        private void Beliminar_Click(object sender, EventArgs e)
        {

        }

        private void BT_menuproveedor_Click(object sender, EventArgs e)
        {
            capFormcap.Show();
            this.Close();
        }

        private void BT_salirproveedor_Click(object sender, EventArgs e)
        {
            capFormcap.Close();
            oFormlogin.Close();
            this.Close();

        }



    }
}
