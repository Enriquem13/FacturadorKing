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
    public partial class FSolicitudeProteccion : Form
    {
        public FSolicitudeProteccion()
        {
            InitializeComponent();
        }

        private void BT_cancelarsolicitud_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TB_anexo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void TB_anexo3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void TB_anexo4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void TB_anexo5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void TB_anexo6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void TB_anexo7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void TB_anexo8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void TB_anexo9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void TB_anexo10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void TB_anexo11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void TB_anexo12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void CheckB2anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB2anexosolicitud.Checked)
            {
                TB_anexo2.Enabled = true;
            }
            else
            {
                TB_anexo2.Enabled = false;
                TB_anexo2.Text = "";
            }
        }

        private void CheckB3anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB3anexosolicitud.Checked)
            {
                TB_anexo3.Enabled = true;
            }
            else
            {
                TB_anexo3.Enabled = false;
                TB_anexo3.Text = "";
            }
        }

        private void CheckB4anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB4anexosolicitud.Checked)
            {
                TB_anexo4.Enabled = true;
            }
            else
            {
                TB_anexo4.Enabled = false;
                TB_anexo4.Text = "";
            }
        }

        private void CheckB5anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB5anexosolicitud.Checked)
            {
                TB_anexo5.Enabled = true;
            }
            else
            {
                TB_anexo5.Enabled = false;
                TB_anexo5.Text = "";
            }
        }

        private void CheckB6anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB6anexosolicitud.Checked)
            {
                TB_anexo6.Enabled = true;
            }
            else
            {
                TB_anexo6.Enabled = false;
                TB_anexo6.Text = "";
            }
        }

        private void CheckB7anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB7anexosolicitud.Checked)
            {
                TB_anexo7.Enabled = true;
            }
            else
            {
                TB_anexo7.Enabled = false;
                TB_anexo7.Text = "";
            }
        }

        private void CheckB8anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB8anexosolicitud.Checked)
            {
                TB_anexo8.Enabled = true;
            }
            else
            {
                TB_anexo8.Enabled = false;
                TB_anexo8.Text = "";
            }
        }

        private void CheckB9anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB9anexosolicitud.Checked)
            {
                TB_anexo9.Enabled = true;
            }
            else
            {
                TB_anexo9.Enabled = false;
                TB_anexo9.Text = "";
            }
        }

        private void CheckB10anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB10anexosolicitud.Checked)
            {
                TB_anexo10.Enabled = true;
            }
            else
            {
                TB_anexo10.Enabled = false;
                TB_anexo10.Text = "";
            }
        }

        private void CheckB11anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB11anexosolicitud.Checked)
            {
                TB_anexo11.Enabled = true;
            }
            else
            {
                TB_anexo11.Enabled = false;
                TB_anexo11.Text = "";
            }
        }

        private void CheckB12anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB12anexosolicitud.Checked)
            {
                TB_anexo12.Enabled = true;
            }
            else
            {
                TB_anexo12.Enabled = false;
                TB_anexo12.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }




    }
}
