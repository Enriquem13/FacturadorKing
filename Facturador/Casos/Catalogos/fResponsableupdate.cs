using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Facturador
{
    public partial class fResponsableupdate : Form
    {
        public String sValueResponsable { get; set; }
        public String sTextoResponsable { get; set; }
        funcionesdicss obj = new funcionesdicss();
        public fResponsableupdate(string sidResponsable)
        {
            InitializeComponent();
            conect con = new conect();
            String sGetids = "select * from usuario order by UsuarioNombre;";
            MySqlDataReader resp_getestatus = con.getdatareader(sGetids);
            int iIndiceids = 0;
            while (resp_getestatus.Read())
            {
                // validareader("Casoid", "Casoid", resp_getidspatentes).Text;
                ComboboxItem objcombo = new ComboboxItem();
                objcombo.Text = obj.validareader("UsuarioNombre", "UsuarioId", resp_getestatus).Text + " " + obj.validareader("UsuarioPaterno", "UsuarioId", resp_getestatus).Text;
                objcombo.Value = obj.validareader("UsuarioNombre", "UsuarioId", resp_getestatus).Value;

                cbUpdateestatus.Items.Add(objcombo);
            }
            resp_getestatus.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sValueResponsable = (cbUpdateestatus.SelectedItem as ComboboxItem).Value.ToString();
            sTextoResponsable = (cbUpdateestatus.SelectedItem as ComboboxItem).Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
