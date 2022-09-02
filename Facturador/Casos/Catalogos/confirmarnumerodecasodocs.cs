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
    public partial class confirmarnumerodecasodocs : Form
    {
        public String sNumerocaso { get; set;}
        public confirmarnumerodecasodocs()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sNumerocaso = tbSnumerocaso.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbSnumerocaso_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //MessageBox.Show("Mensaje enviado");
                button1_Click(sender, e);
            }
        }
    }
}
