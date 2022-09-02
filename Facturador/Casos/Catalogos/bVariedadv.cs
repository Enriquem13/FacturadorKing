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
    public partial class bVariedadv : Form
    {
        public String[] paises = new String[250];
        public String[] paisesclave = new String[250];
        public captura captura;
        public Form1 fLoguin;
        public bVariedadv(int iGrupo, captura capturaform, Form1 loguin)
        {
            fLoguin = loguin;
            captura = capturaform;
            InitializeComponent();
            conect con = new conect();

            String sQuerytipoSol = "select * from tiposolicitud where TipoSolicitudGrupo= " + iGrupo;
            MySqlDataReader respuestastringtoiposl = con.getdatareader(sQuerytipoSol);
            while (respuestastringtoiposl.Read())
            {
                cbTiposolicitud.Items.Add(validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestastringtoiposl));
            }
            respuestastringtoiposl.Close();


            String sQuerypais = "select * from pais;";
            MySqlDataReader respuestastringpais = con.getdatareader(sQuerypais);
            while (respuestastringpais.Read())
            {
                cbPais.Items.Add(validareader("PaisNombre", "PaisId", respuestastringpais));
            }
            respuestastringpais.Close();


            //select * from pais;
            String query5 = "select PaisNombre, PaisId, PaisClave from pais;";
            MySqlDataReader respuestastringopais = con.getdatareader(query5);
            //int paisint = 0;
            while (respuestastringopais.Read())
            {
                cbPais.Items.Add(validareader("PaisNombre", "PaisId", respuestastringopais));
                paises[Convert.ToInt32(validareader("PaisClave", "PaisId", respuestastringopais).Value.ToString())] = validareader("PaisClave", "PaisId", respuestastringopais).Text;
                paisesclave[Convert.ToInt32(validareader("PaisClave", "PaisId", respuestastringopais).Value.ToString())] = validareader("PaisNombre", "PaisId", respuestastringopais).Text;

                //paisint++;
            }   
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

        private void button4_Click(object sender, EventArgs e)
        {
            captura.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            captura.Close();
            fLoguin.Close();
        }

        private void cbPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iValuepais = Convert.ToInt32((cbPais.SelectedItem as ComboboxItem).Value.ToString());
            tbClave.Text = paises[iValuepais];
        }
    }
}
