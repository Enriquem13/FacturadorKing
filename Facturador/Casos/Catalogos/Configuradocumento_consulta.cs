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
using Word = Microsoft.Office.Interop.Word;

namespace Facturador
{
    public partial class Configuradocumento_consulta : Form
    {
        public Form1 ologuin;
        public Configuradocumento_consulta(Form1 Sologuin)
        {
            try
            {
                ologuin = Sologuin;
                InitializeComponent();
                conect con = new conect();
                String query = "select * from grupo;";
                MySqlDataReader respuestastiposol = con.getdatareader(query);
                while (respuestastiposol.Read())
                {
                    cbGrupos.Items.Add(validareader("GrupoDescripcion", "GrupoId", respuestastiposol));
                }
                respuestastiposol.Close();
                con.Cerrarconexion();
            }catch(Exception E){
                new filelog(ologuin.sId, E.ToString());            
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbCampos.Text != "")
                {
                    String sDatosseleccionado = cbCampos.Text;
                    ListViewItem lisview = new ListViewItem(cbCampos.Text);
                    lvDatos.Items.Add(lisview);
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un dato para agregarlo a la lista");
                }
            }catch(Exception E){
                new filelog(ologuin.sId, E.ToString());            
            }
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var fileContent = string.Empty;
                var filePath = string.Empty;
                String sNamefile = "";
                String[] aName;
                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.InitialDirectory = mdoc;
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        filePath = openFileDialog.FileName;
                        aName = filePath.Split('\\');
                        sNamefile = aName[aName.Length - 1];
                        //tbPathfile.Text = filePath;

                        String sruta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\casosking\" + "Nuevos_formatos\\IMPI-00-002_B_copia.docx";
                        //string[] readText = File.ReadAllLines(@"D:\CARTAP1.txt");
                        Word.Application application = new Word.Application();
                        Word.Document document = application.Documents.Open(filePath);
                        for (int i = 1; i <= document.Bookmarks.Count; i++)
                        {
                            object objI = i;
                            //application.Visible = true;
                            //document.Bookmarks[document.Bookmarks.get_Item(ref objI).Name].Select();
                            //application.Selection.TypeText("" + i);
                            string sMarcador = document.Bookmarks.get_Item(ref objI).Name;
                            ListViewItem list = new ListViewItem(sMarcador);
                            lvMarcadores.Items.Add(list);
                        }
                        //document.Save();
                        application.Quit();
                    }
                }
            }catch(Exception E){
                new filelog(ologuin.sId, E.ToString());            
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbGrupos.SelectedItem != null)
                {
                    cbCampos.Items.Clear();
                    String sTiposolicitud = (cbGrupos.SelectedItem as ComboboxItem).Value.ToString();
                    switch (sTiposolicitud)
                    {
                        case "1":
                            {
                                conect con = new conect();
                                String query = "SHOW COLUMNS FROM caso_patente;";
                                MySqlDataReader respuestastiposol = con.getdatareader(query);
                                while (respuestastiposol.Read())
                                {
                                    cbCampos.Items.Add(validareader("Field", "Field", respuestastiposol));
                                }
                                respuestastiposol.Close();
                                con.Cerrarconexion();

                                conect con_anualidad = new conect();
                                String query_anualidad = "SHOW COLUMNS FROM anualidad;";
                                MySqlDataReader respuestastiposol_anualidad = con_anualidad.getdatareader(query_anualidad);
                                while (respuestastiposol_anualidad.Read())
                                {
                                    cbCampos.Items.Add(validareader("Field", "Field", respuestastiposol_anualidad));
                                }
                                respuestastiposol_anualidad.Close();
                                con_anualidad.Cerrarconexion();
                            } break;
                        case "2":
                            {
                                conect con = new conect();
                                String query = "SHOW COLUMNS FROM caso_marcas;";
                                MySqlDataReader respuestastiposol = con.getdatareader(query);
                                while (respuestastiposol.Read())
                                {
                                    cbCampos.Items.Add(validareader("Field", "Field", respuestastiposol));
                                }
                                respuestastiposol.Close();
                                con.Cerrarconexion();

                                conect con_casoproductos = new conect();
                                String query_casoproductos = "SHOW COLUMNS FROM casoproductos;";
                                MySqlDataReader respuestastiposol_casoproductos = con_casoproductos.getdatareader(query_casoproductos);
                                while (respuestastiposol_casoproductos.Read())
                                {
                                    cbCampos.Items.Add(validareader("Field", "Field", respuestastiposol_casoproductos));
                                }
                                respuestastiposol_casoproductos.Close();
                                con_casoproductos.Cerrarconexion();
                            } break;
                        case "3":
                            {
                                conect con = new conect();
                                String query = "SHOW COLUMNS FROM caso_patente;";
                                MySqlDataReader respuestastiposol = con.getdatareader(query);
                                while (respuestastiposol.Read())
                                {
                                    cbCampos.Items.Add(validareader("Field", "Field", respuestastiposol));
                                }
                                respuestastiposol.Close();
                                con.Cerrarconexion();
                            } break;
                        case "4":
                            {
                                conect con = new conect();
                                String query = "SHOW COLUMNS FROM caso_patente;";
                                MySqlDataReader respuestastiposol = con.getdatareader(query);
                                while (respuestastiposol.Read())
                                {
                                    cbCampos.Items.Add(validareader("Field", "Field", respuestastiposol));
                                }
                                respuestastiposol.Close();
                                con.Cerrarconexion();
                            } break;
                        case "5":
                            {
                                conect con = new conect();
                                String query = "SHOW COLUMNS FROM caso_patente;";
                                MySqlDataReader respuestastiposol = con.getdatareader(query);
                                while (respuestastiposol.Read())
                                {
                                    cbCampos.Items.Add(validareader("Field", "Field", respuestastiposol));
                                }
                                respuestastiposol.Close();
                                con.Cerrarconexion();
                            } break;
                        case "6":
                            {
                                conect con = new conect();
                                String query = "SHOW COLUMNS FROM caso_patente;";
                                MySqlDataReader respuestastiposol = con.getdatareader(query);
                                while (respuestastiposol.Read())
                                {
                                    cbCampos.Items.Add(validareader("Field", "Field", respuestastiposol));
                                }
                                respuestastiposol.Close();
                                con.Cerrarconexion();
                            } break;
                        case "7":
                            {
                                conect con = new conect();
                                String query = "SHOW COLUMNS FROM caso_patente;";
                                MySqlDataReader respuestastiposol = con.getdatareader(query);
                                while (respuestastiposol.Read())
                                {
                                    cbCampos.Items.Add(validareader("Field", "Field", respuestastiposol));
                                }
                                respuestastiposol.Close();
                                con.Cerrarconexion();
                            } break;
                        case "8":
                            {
                                conect con = new conect();
                                String query = "SHOW COLUMNS FROM caso_patente;";
                                MySqlDataReader respuestastiposol = con.getdatareader(query);
                                while (respuestastiposol.Read())
                                {
                                    cbCampos.Items.Add(validareader("Field", "Field", respuestastiposol));
                                }
                                respuestastiposol.Close();
                                con.Cerrarconexion();
                            } break;
                    }
                    conect con_2 = new conect();
                    String query_2 = "SHOW COLUMNS FROM cliente;";
                    MySqlDataReader respuestastiposol_2 = con_2.getdatareader(query_2);
                    while (respuestastiposol_2.Read())
                    {
                        cbCampos.Items.Add(validareader("Field", "Field", respuestastiposol_2));
                    }
                    respuestastiposol_2.Close();
                    con_2.Cerrarconexion();

                    conect con_3 = new conect();
                    String query_3 = "SHOW COLUMNS FROM interesado;";
                    MySqlDataReader respuestastiposol_3 = con_3.getdatareader(query_3);
                    while (respuestastiposol_3.Read())
                    {
                        cbCampos.Items.Add(validareader("Field", "Field", respuestastiposol_3));
                    }
                    respuestastiposol_3.Close();
                    con_3.Cerrarconexion();

                    conect con_4 = new conect();
                    String query_4 = "SHOW COLUMNS FROM prioridad;";
                    MySqlDataReader respuestastiposol_4 = con_4.getdatareader(query_4);
                    while (respuestastiposol_4.Read())
                    {
                        cbCampos.Items.Add(validareader("Field", "Field", respuestastiposol_4));
                    }
                    respuestastiposol_4.Close();
                    con_4.Cerrarconexion();

                    conect con_5 = new conect();
                    String query_5 = "SHOW COLUMNS FROM referencia;";
                    MySqlDataReader respuestastiposol_5 = con_5.getdatareader(query_5);
                    while (respuestastiposol_5.Read())
                    {
                        cbCampos.Items.Add(validareader("Field", "Field", respuestastiposol_5));
                    }
                    respuestastiposol_5.Close();
                    con_5.Cerrarconexion();

                }
            }catch(Exception E){
                new filelog(ologuin.sId, E.ToString());            
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try{

                //seleccionamos un ergistro del primer list view y luego uno del segundo list view para agregarlo al tercer list view
                if (lvMarcadores.SelectedItems.Count > 0 && lvDatos.SelectedItems.Count > 0)
                {
                    String sMarcadorseleccionado = lvMarcadores.SelectedItems[0].SubItems[0].Text;
                    String sDatosseleccionado = lvDatos.SelectedItems[0].SubItems[0].Text;
                    ListViewItem list = new ListViewItem(sMarcadorseleccionado);
                    list.SubItems.Add(sDatosseleccionado);
                    lvRelacion.Items.Add(list);
                }
                else {
                    MessageBox.Show("Debe selecciona un marcador y un dato para poder crear la relación.");
                }
            }catch(Exception E){
                new filelog(ologuin.sId, E.ToString());            
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try{
                if (lvRelacion.SelectedItems.Count > 0)
                {
                    lvRelacion.SelectedItems[0].Remove();
                }
                else {
                    MessageBox.Show("Debe seleccionar un registro de la relación para eliminar");
                }
            }catch(Exception E){
                new filelog(ologuin.sId, E.ToString());            
            }
        }
    }
}
