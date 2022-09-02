using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace emailking
{
    public partial class detalleenvio : Form
    {
        public detalleenvio(String sCasoid)
        {
            InitializeComponent();
            listView1.View = View.Details;
            // Allow the user to edit item text.
            listView1.LabelEdit = true;
            // Allow the user to rearrange columns.
            listView1.AllowColumnReorder = true;
            // Select the item and subitems when selection is made.
            listView1.FullRowSelect = true;
            // Display grid lines.
            listView1.GridLines = true;
            // Sort the items in the list in ascending order.
            listView1.Sorting = SortOrder.Ascending;
            String queryselectreviewbycasoid = "use jobconfig; SELECT * FROM jobconfig.job_sended where casoid= "+ sCasoid +" order by FechaSended desc;";
            conect_robot con2 = new conect_robot();
            MySqlDataReader respuestasselectbycasoid = con2.getdatareader(queryselectreviewbycasoid);
            while (respuestasselectbycasoid.Read())
            {
                ListViewItem listaagregar = new ListViewItem(validareader("CasoId", "CasoId", respuestasselectbycasoid).Text);
                listaagregar.SubItems.Add(validareader("FechaSended", "CasoId", respuestasselectbycasoid).Text);
                listaagregar.SubItems.Add(validareader("RelacionDocumentoLink", "CasoId", respuestasselectbycasoid).Text);
                listView1.Items.Add(listaagregar);
            }
            respuestasselectbycasoid.Close();
            con2.Cerrarconexion();
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
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                String sRutaarchivo = listView1.SelectedItems[0].SubItems[2].Text;
                tbCorreodetalle.Text = sRutaarchivo;
                Process.Start(sRutaarchivo);
                //MessageBox.Show("La ruta del archivo es: " + sRutaarchivo);
                //sRutaarchivo = sRutaarchivo.Replace('\\', '\\');
                //OpenFileDialog AbrirOpenFile = new OpenFileDialog();
                //AbrirOpenFile.InitialDirectory = sRutaarchivo;
                ////AbrirOpenFile.Filter = "Todos(*.*)|*.*";
                //AbrirOpenFile.ShowDialog();
            }
            catch (Exception E)
            {
                MessageBox.Show("Debe seleccionar un registro para ver su detalle");
            }
        }
    }
}
