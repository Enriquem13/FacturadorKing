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
    public partial class fTitularactualiza : Form
    {
        public String sValueTitular { get; set; }
        public String sTextoTitular { get; set; }
        funcionesdicss obj = new funcionesdicss();
        
        
        public fTitularactualiza(string sidResponsable)
        {
            InitializeComponent();
            conect con = new conect();
            String sGetids = "select InteresadoID, InteresadoNombre from interesado order by InteresadoNombre;";
            MySqlDataReader resp_getestatus = con.getdatareader(sGetids);
            while (resp_getestatus.Read())
            {
                ComboboxItem objcombo = new ComboboxItem();
                objcombo.Text = obj.validareader("InteresadoNombre", "InteresadoID", resp_getestatus).Text;
                objcombo.Value = obj.validareader("InteresadoNombre", "InteresadoID", resp_getestatus).Value;

                cbUpdateestatus.Items.Add(objcombo);
            }
            resp_getestatus.Close();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            sValueTitular = (cbUpdateestatus.SelectedItem as ComboboxItem).Value.ToString();
            sTextoTitular = (cbUpdateestatus.SelectedItem as ComboboxItem).Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
