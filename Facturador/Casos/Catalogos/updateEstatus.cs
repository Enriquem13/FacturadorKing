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
    public partial class updateEstatus : Form
    {
        public String sValueestatus { get; set; }
        public String sTextoestatus { get; set; }
        public updateEstatus(string sGrupoid)
        {
            InitializeComponent();
            conect con = new conect();
            String sGetids = "SELECT " +
                                "    estatuscaso.*, " +
                                " grupoid "+
                                " FROM "+
                                "    grupoestatuscaso, "+
                                "    estatuscaso "+
                                " WHERE "+
                                "    grupoestatuscaso.EstatusCasoId = estatuscaso.EstatusCasoId " +
                                "        AND grupoid = "+ sGrupoid +
                                " GROUP BY estatuscaso.EstatusCasoId" +
                                " order by estatuscaso.EstatusCasoDescrip;";
            //String sGetids = "select estatuscaso.*, grupoid from subtipodocumentoestatuscaso, estatuscaso where grupoid = " + sGrupoid + " group by estatuscaso.EstatusCasoId;";
            MySqlDataReader resp_getestatus = con.getdatareader(sGetids);
            int iIndiceids = 0;
            while (resp_getestatus.Read())
            {
                // validareader("Casoid", "Casoid", resp_getidspatentes).Text;
                cbUpdateestatus.Items.Add(validareader("EstatusCasoDescrip", "estatuscasoid", resp_getestatus));
            }
            resp_getestatus.Close();
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
    }
}
