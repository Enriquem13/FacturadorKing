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
    public partial class capturaanualidades : Form
    {
        public capturaanualidades()
        {
            InitializeComponent();
        }

        private void tbDocumentofecharecepcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            if (tbDocumentofecharecepcion.Text.Length == 2)
            {
                tbDocumentofecharecepcion.Text = tbDocumentofecharecepcion.Text + "-";
                tbDocumentofecharecepcion.SelectionStart = tbDocumentofecharecepcion.Text.Length;

            }
            if (tbDocumentofecharecepcion.Text.Length == 5)
            {
                tbDocumentofecharecepcion.Text = tbDocumentofecharecepcion.Text + "-";
                tbDocumentofecharecepcion.SelectionStart = tbDocumentofecharecepcion.Text.Length;
            }
        }
        public String sFechainicioanualidades = "";
        public String snumeroanualidades = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if (tbDocumentofecharecepcion.Text != "" && tbNumero.Text != "")
            {
                sFechainicioanualidades = tbDocumentofecharecepcion.Text;
                snumeroanualidades = tbNumero.Text;
                this.Close();
            }
            else {
                MessageBox.Show("Debe agregar una fecha y numero de anualidades para genera anualidades.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
