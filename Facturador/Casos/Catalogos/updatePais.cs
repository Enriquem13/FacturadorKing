using MySql.Data.MySqlClient;
//using MySql.Data.MySqlClient;
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
    public partial class updatePais : Form
    {
        public String sValueestatus { get; set; }
        public String sTextoestatus { get; set; }
        public updatePais()
        {
            InitializeComponent();
            conect con = new conect();
            String sGetids = "select * from pais;";
            MySqlDataReader resp_getestatus = con.getdatareader(sGetids);
            int iIndiceids = 0;
            while (resp_getestatus.Read())
            {
                // validareader("Casoid", "Casoid", resp_getidspatentes).Text;
                cbUpdateestatus.Items.Add(validareader("PaisNombre", "PaisId", resp_getestatus));
            }
            resp_getestatus.Close();
            con.Cerrarconexion();

            tbClave.Focus();


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
            sValueestatus = (cbUpdateestatus.SelectedItem as ComboboxItem).Value.ToString();
            sTextoestatus = (cbUpdateestatus.SelectedItem as ComboboxItem).Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void buscapaisporclave()
        {
            conect con = new conect();
            string sClavePais = tbClave.Text;

            String query5 = "SELECT PaisNombre, PaisId, PaisClave FROM pais WHERE PaisClave = '" + sClavePais + "';";
            MySqlDataReader respuestastringopais = con.getdatareader(query5);
            while (respuestastringopais.Read())
            {
                String sPaisNombre = validareader("PaisNombre", "PaisId", respuestastringopais).Text;
                cbUpdateestatus.Text = sPaisNombre;
            }
        }

        private void tbClave_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscapaisporclave();
            }
        }
    }
}
