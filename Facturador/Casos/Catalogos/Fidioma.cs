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
    public partial class Fidioma : Form
    {

        public Form1 oFormlogin;
        public captura capFormcap;
        conect con1;
        public Fidioma(Form1 form, captura Formcap)
        {
            oFormlogin = form;
            capFormcap = Formcap;
            InitializeComponent();

            con1 = new conect();
            String query2 = "SELECT * FROM idioma";
            MySqlDataReader respuestastring20 = con1.getdatareader(query2);

            String IdiomaId = "";
            String IdiomaClave = "";
            String IdiomaDescripcion = "";

            while (respuestastring20.Read())
            {
                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("IdiomaId")))
                {
                    IdiomaId = respuestastring20.GetString(respuestastring20.GetOrdinal("IdiomaId"));
                }
                else
                {
                    IdiomaId = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("IdiomaClave")))
                {
                    IdiomaClave = respuestastring20.GetString(respuestastring20.GetOrdinal("IdiomaClave"));
                }
                else
                {
                    IdiomaClave = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("IdiomaDescripcion")))
                {
                    IdiomaDescripcion = respuestastring20.GetString(respuestastring20.GetOrdinal("IdiomaDescripcion"));
                }
                else
                {
                    IdiomaDescripcion = "";
                }

                ListViewItem listaagregar = new ListViewItem(IdiomaDescripcion);
                listaagregar.SubItems.Add(IdiomaClave);
                listView1.Items.Add(listaagregar);


            }

            //listView1.View = View.Details;
            //listView1.FullRowSelect = true;
            //listView1.Columns.Add("Idioma", 260);
            //listView1.Columns.Add("Clave", 80);
        }

        private void Bagregar_Click(object sender, EventArgs e)
        {

            if(!TB_nombreidioma.Text.Equals("") && !TB_claveidoma.Text.Equals("")){

                try
                {
                    String queryinsert = "INSERT INTO `idioma` (`IdiomaId`, `IdiomaClave`, `IdiomaDescripcion`, `IdiomaIndAct`) VALUES (NULL, '" + TB_claveidoma.Text + "', '" + TB_nombreidioma.Text +  "', '" +1+ "');";
                    MySqlDataReader respuestastringinsert = con1.getdatareader(queryinsert);


                    if (respuestastringinsert == null)
                    {
                        MessageBox.Show("Fallo");
                    }
                    else
                    {
                        MessageBox.Show("Se inserto");
                        ListViewItem listaagregar = new ListViewItem(TB_nombreidioma.Text);
                        listaagregar.SubItems.Add(TB_claveidoma.Text);
                        listView1.Items.Add(listaagregar);
                        TB_nombreidioma.Text = "";
                        TB_claveidoma.Text = "";
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

        private void BT_menuidioma_Click(object sender, EventArgs e)
        {
            capFormcap.Show();
            this.Close();
        }

        private void BT_saliridioma_Click(object sender, EventArgs e)
        {

            capFormcap.Close();
            oFormlogin.Close();
            this.Close();


        }

        private void Bmodificar_Click(object sender, EventArgs e)
        {

        }

        private void Beliminar_Click(object sender, EventArgs e)
        {

        }



    }
}
