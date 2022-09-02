using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturador
{
    public partial class FGeneraCartas : Form
    {


        
        public Form1 oFormlogin;
        public captura capFormcap;
        conect con1 = new conect();
        public String valuecob;
        public FGeneraCartas(Form1 form, captura Formcap)
        {
            oFormlogin = form;
            capFormcap = Formcap;
            InitializeComponent();

  

            con1 = new conect();
            String kwery0 = "SELECT * FROM grupo";
            MySqlDataReader respuestastring0 = con1.getdatareader(kwery0);


            while (respuestastring0.Read())
            {
                CB_tiposolicitudgeneracartas.Items.Add(validareader("GrupoDescripcion", "GrupoId", respuestastring0));
            }

            string[] ubicacion = Directory.GetFiles(@"C:\Pclientes\Cartas");//<--aqui va la ruta de la carpeta donde estan los documentos

            for (int i = 0; i < ubicacion.Length; i++)
            {
                CB_Cartageneracartas.Items.Add(Path.GetFileName(ubicacion[i]));//combobox el que mostrara todos los nombres

            }

        }

        private void BT_menugeneracartas_Click(object sender, EventArgs e)
        {
            capFormcap.Show();
            this.Close();
        }

        private void BT_salirgeneracartas_Click(object sender, EventArgs e)
        {
            capFormcap.Close();
            oFormlogin.Close();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String valorcarta = CB_Cartageneracartas.Text;
            if (CB_tiposolicitudgeneracartas.SelectedItem == null)
            {
                MessageBox.Show("Debes Seleccionar un tiposolicitd");
            }
            else
            {
                generacartas prueba = new generacartas();
                valuecob = (CB_tiposolicitudgeneracartas.SelectedItem as ComboboxItem).Value.ToString();
                prueba.generacartass(TB_Casoidgenerecartas.Text, valuecob, valorcarta );

                TB_Casoidgenerecartas.Text = "";
                CB_Cartageneracartas.Text = "Seleccione";
                CB_tiposolicitudgeneracartas.Text = "Seleccione";
                MessageBox.Show("Se ah generado Correctamente");

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




    }

}
