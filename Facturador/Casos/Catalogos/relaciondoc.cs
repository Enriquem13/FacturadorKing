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
    public partial class relaciondoc : Form
    {
        funcionesdicss objfuncionesdicss = new funcionesdicss();
        public relaciondoc()
        {
            InitializeComponent();
            String strRutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string[] ubicacion = Directory.GetFiles(strRutaArchivo + @"\casosking\Cartas");//<--aqui va la ruta de la carpeta donde estan los documentos
            for (int i = 0; i < ubicacion.Length; i++)
            {
                comboBox2.Items.Add(Path.GetFileName(ubicacion[i]));//combobox el que mostrara todos los nombres
                String[] ubicacions = Path.GetFileName(ubicacion[i]).Split('\\');
                String[] nombre = ubicacions[ubicacions.Length - 1].Split('.');
            }


            //agregamsos las cartas
            conect con_3_cartas = new conect();
            String sQueryescritosdisponibles = "SELECT " +
                                                       "     * " +
                                                       " FROM " +
                                                       "    estatuscasosubtipodocumento, " +
                                                       "    subtipodocumento " +
                                                       " WHERE " +
                                                       "         estatuscasosubtipodocumento.GrupoId = 1" +
                                                       "         AND subtipodocumento.SubTipoDocumentoId = estatuscasosubtipodocumento.SubTipoDocumentoId " +
                                                       "         AND subtipodocumento.TipoDocumentoId = 3 " +
                                                       "         AND subtipodocumento.SubTipoDocumentoIndAct = 1 " +
                                                       "         group by estatuscasosubtipodocumento.SubTipoDocumentoId order by subtipodocumento.SubTipoDocumentoDescrip;";
            //String sQueryescritosdisponibless = "select * from estatuscasosubtipodocumento, subtipodocumento where estatuscasosubtipodocumento.EstatusCasoId = " + sEstatusidint + " and estatuscasosubtipodocumento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId and estatuscasosubtipodocumento.GrupoId =1 and subtipodocumento.TipoDocumentoId = 2 group by subtipodocumento.SubTipoDocumentoId;";
            //String sQueryescritosdisponibles = "select subtipodocumento.SubTipoDocumentoDescrip, estatuscasosubtipodocumentores.SubTipoDocumentoId from estatuscasosubtipodocumentores, subtipodocumento where estatuscasoid = " + tbEstatus.Text + " and estatuscasosubtipodocumentores.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId;";
            MySqlDataReader resp_escritos = con_3_cartas.getdatareader(sQueryescritosdisponibles);
            while (resp_escritos.Read())
            {
                String sId = objfuncionesdicss.validareader("SubTipoDocumentoDescrip", "subtipodocumentoId", resp_escritos).Value.ToString();
                comboBox1.Items.Add(objfuncionesdicss.validareader("SubTipoDocumentoDescrip", "subtipodocumentoId", resp_escritos));//Estatus
            }
            resp_escritos.Close();
            con_3_cartas.Cerrarconexion();
               
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String sNombrefile = (comboBox1.SelectedItem as ComboboxItem).Value.ToString();
            conect con_3_cartas = new conect();
            //String sQueryescritosdisponibles = "UPDATE `subtipodocumento` SET `urlDoc` = '" + comboBox2.Text + "' WHERE `subtipodocumentoId` = '" + sNombrefile + "';";
            String sQueryescritosdisponibles = "select * from `subtipodocumento`;";
            MySqlDataReader resp_escritos = con_3_cartas.getdatareader(sQueryescritosdisponibles);
            resp_escritos.Read();
            comboBox1.Items.Remove(comboBox1.SelectedItem);

        }
    }
}
