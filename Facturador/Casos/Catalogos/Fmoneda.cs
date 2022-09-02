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
    public partial class Fmoneda : Form
    {
        public Form1 oFormlogin;
        public captura capFormcap;
        conect con1;
        public Fmoneda(Form1 form, captura Formcap)
        {

            oFormlogin = form;
            capFormcap = Formcap;
            InitializeComponent();

            con1 = new conect();
            String query2 = "SELECT * FROM moneda ";
            MySqlDataReader respuestastring20 = con1.getdatareader(query2);

            String siniMonedaId = "";
            String siniMonedaDescrip = "";
            String siniMonedaSimbolo = "";
            String MonedaDescripSufijo = "";

            while (respuestastring20.Read())
            {
                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("MonedaId")))
                {
                    siniMonedaId = respuestastring20.GetString(respuestastring20.GetOrdinal("MonedaId"));
                }
                else
                {
                    siniMonedaId = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("MonedaDescrip")))
                {
                    siniMonedaDescrip = respuestastring20.GetString(respuestastring20.GetOrdinal("MonedaDescrip"));
                }
                else
                {
                    siniMonedaDescrip = "";
                }


                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("MonedaSimbolo")))
                {
                    siniMonedaSimbolo = respuestastring20.GetString(respuestastring20.GetOrdinal("MonedaSimbolo"));
                }
                else
                {
                    siniMonedaSimbolo = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("MonedaDescripSufijo")))
                {
                    MonedaDescripSufijo = respuestastring20.GetString(respuestastring20.GetOrdinal("MonedaDescripSufijo"));
                }
                else
                {
                    MonedaDescripSufijo = "";
                }

                ListViewItem listaagregar = new ListViewItem(siniMonedaDescrip);
                listaagregar.SubItems.Add(siniMonedaSimbolo);
                listaagregar.SubItems.Add(MonedaDescripSufijo);
                listView1.Items.Add(listaagregar);

            }
            //listView1.View = View.Details;
            //listView1.FullRowSelect = true;
            //listView1.Columns.Add("Moneda", 100);
            //listView1.Columns.Add("Simbolo", 100);
            //listView1.Columns.Add("Subfijo", 100);

        }

        private void Bagregar_Click(object sender, EventArgs e)
        {

            if(!TB_nombremoneda.Text.Equals("") && !TB_simbolomoneda.Text.Equals("") && !TB_descripcionmoneda.Text.Equals("")){


                try
                {
                    String queryinsert = "INSERT INTO `moneda` (`MonedaId`, `MonedaDescrip`, `MonedaSimbolo`, `MonedaIndAct`, `MonedaDescripSufijo`)"+
                        " VALUES (NULL, '" + TB_nombremoneda.Text + "', '" + TB_simbolomoneda.Text + "', '1','" + TB_descripcionmoneda.Text + "');";
                    MySqlDataReader respuestastringinsert = con1.getdatareader(queryinsert);

                    if (respuestastringinsert == null)
                    {
                        MessageBox.Show("Fallo");
                    }
                    else
                    {
                        MessageBox.Show("Se inserto");

                        ListViewItem listaagregar = new ListViewItem(TB_nombremoneda.Text);
                        listaagregar.SubItems.Add(TB_simbolomoneda.Text);
                        listaagregar.SubItems.Add(TB_descripcionmoneda.Text);
                        listView1.Items.Add(listaagregar);

                        TB_nombremoneda.Text = "";
                        TB_simbolomoneda.Text = "";
                        TB_descripcionmoneda.Text = "";

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

        private void BT_menumoneda_Click(object sender, EventArgs e)
        {
            capFormcap.Show();
            this.Close();
        }

        private void BT_salirmoneda_Click(object sender, EventArgs e)
        {
            capFormcap.Close();
            oFormlogin.Close();
            this.Close();

        }
    }
}
