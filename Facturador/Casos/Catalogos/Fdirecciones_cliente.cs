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
    public partial class Fdirecciones_cliente : Form
    {

        public Form1 oFormlogin;
        public captura capFormcap;
        conect con1;
        public Fdirecciones_cliente(Form1 form, captura Formcap)


        {
            oFormlogin = form;
            capFormcap = Formcap;
            InitializeComponent();

            con1 = new conect();
            String kwery = "SELECT PaisNombre,PaisId  FROM pais";
            MySqlDataReader respuestastring = con1.getdatareader(kwery);

            while (respuestastring.Read())
            {
                CB_paisdirecciones.Items.Add(validareader("PaisNombre", "PaisId", respuestastring));
            }


            String kwery2 = "SELECT TipoDireccionDescrip,TipoDireccionId  FROM tipodireccion";
            MySqlDataReader respuestastring2 = con1.getdatareader(kwery2);

            while (respuestastring2.Read())
            {
                CB_tipodirecciones.Items.Add(validareader("TipoDireccionDescrip", "TipoDireccionId", respuestastring2));
            }

      
            String query2 = "SELECT * FROM direccion";
            MySqlDataReader respuestastring20 = con1.getdatareader(query2);

            String siniDireccionID = "";
            String siniDireccionCalle = "";
            String siniDireccionNumExt = "";
            String siniDireccionNumInt = "";
            String siniDireccionColonia = "";
            String siniDireccionPoblacion = "";
            String siniDireccionEstado = "";
            String siniDireccionCP = "";
            String sinPaisId = "";
            String siniTipoDireccionId = "";


            while (respuestastring20.Read())
            {
                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("DireccionID")))
                {
                    siniDireccionID = respuestastring20.GetString(respuestastring20.GetOrdinal("DireccionID"));
                }
                else
                {
                    siniDireccionID = "";
                }
                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("DireccionCalle")))
                {
                    siniDireccionCalle = respuestastring20.GetString(respuestastring20.GetOrdinal("DireccionCalle"));
                }
                else
                {
                    siniDireccionCalle = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("DireccionNumExt")))
                {
                    siniDireccionNumExt = respuestastring20.GetString(respuestastring20.GetOrdinal("DireccionNumExt"));
                }
                else
                {
                    siniDireccionNumExt = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("DireccionNumInt")))
                {
                    siniDireccionNumInt = respuestastring20.GetString(respuestastring20.GetOrdinal("DireccionNumInt"));
                }
                else
                {
                    siniDireccionNumInt = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("DireccionColonia")))
                {
                    siniDireccionColonia = respuestastring20.GetString(respuestastring20.GetOrdinal("DireccionColonia"));
                }
                else
                {
                    siniDireccionColonia = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("DireccionPoblacion")))
                {
                    siniDireccionPoblacion = respuestastring20.GetString(respuestastring20.GetOrdinal("DireccionPoblacion"));
                }
                else
                {
                    siniDireccionPoblacion = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("DireccionEstado")))
                {
                    siniDireccionEstado = respuestastring20.GetString(respuestastring20.GetOrdinal("DireccionEstado"));
                }
                else
                {
                    siniDireccionEstado = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("DireccionCP")))
                {
                    siniDireccionCP = respuestastring20.GetString(respuestastring20.GetOrdinal("DireccionCP"));
                }
                else
                {
                    siniDireccionCP = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("DireccionCP")))
                {
                    siniDireccionCP = respuestastring20.GetString(respuestastring20.GetOrdinal("DireccionCP"));
                }
                else
                {
                    siniDireccionCP = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("PaisId")))
                {
                    sinPaisId = respuestastring20.GetString(respuestastring20.GetOrdinal("PaisId"));
                }
                else
                {
                    sinPaisId = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("TipoDireccionId")))
                {
                    siniTipoDireccionId = respuestastring20.GetString(respuestastring20.GetOrdinal("TipoDireccionId"));
                }
                else
                {
                    siniTipoDireccionId = "";
                }

                ListViewItem listaagregar = new ListViewItem(siniDireccionCalle);
                listaagregar.SubItems.Add(siniDireccionNumExt);
                listaagregar.SubItems.Add(siniDireccionNumInt);
                listaagregar.SubItems.Add(siniDireccionColonia);
                listaagregar.SubItems.Add(siniDireccionPoblacion);
                listaagregar.SubItems.Add(siniDireccionEstado);
                listaagregar.SubItems.Add(sinPaisId);
                listaagregar.SubItems.Add(siniTipoDireccionId);
                listView1.Items.Add(listaagregar);

            }

            //listView1.View = View.Details;
            //listView1.FullRowSelect = true;

            //listView1.Columns.Add("Nombre dela calle", 80);
            //listView1.Columns.Add("Numero exterior", 150);
            //listView1.Columns.Add("Numero Interior", 150);
            //listView1.Columns.Add("Colonia", 150);
            //listView1.Columns.Add("Población ", 50);
            //listView1.Columns.Add("Estado", 50);
            //listView1.Columns.Add("País", 50);
            //listView1.Columns.Add("Tipo de dirección", 150);
            

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
        public class ListboxItemss
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
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

        private void BT_agregar_Click(object sender, EventArgs e)
        {

            if(!TB_calledireciones.Text.Equals("") && !TB_codigopostal.Text.Equals("") && !TB_Colonia.Text.Equals("")
                && !TB_numeroexterior.Text.Equals("") && !TB_estado.Text.Equals("") && !TB_poblacion.Text.Equals("")){

                    try
                    {

 
                        String queryinsert = "INSERT INTO `direccion` (`DireccionID`, `DireccionCalle`, `DireccionNumExt`, `DireccionNumInt`, `DireccionColonia`, `DireccionPoblacion`, `DireccionEstado`, `DireccionCP`, `DireccionIndAct`, `PaisId`, `ContactoId`, `InteresadoId`, `ClienteId`, `CasoId`, `TipoDireccionId`) "+
                            "VALUES (NULL,'" + TB_calledireciones.Text + "', '" + TB_numeroexterior.Text + "', '" + TB_numerointerior.Text + "', '" + TB_Colonia.Text + "', '" + TB_poblacion.Text + "', '" + TB_estado.Text + "', '" + TB_codigopostal.Text + "', '" + "1" + "', '" + (CB_paisdirecciones.SelectedItem as ComboboxItem).Value
                            + " '" + ", '10696', '21145', '10696', '27634'," + " '" + (CB_tipodirecciones.SelectedItem as ComboboxItem).Value + "');";
                        MySqlDataReader respuestastringinsert = con1.getdatareader(queryinsert);

                        if (respuestastringinsert == null)
                        {
                            MessageBox.Show("Fallo");
                        }
                        else
                        {
                            MessageBox.Show("Se inserto");
                            limpiarcasillas();


                            ListViewItem listaagregar = new ListViewItem(TB_calledireciones.Text);
                            listaagregar.SubItems.Add(TB_numeroexterior.Text);
                            listaagregar.SubItems.Add(TB_numerointerior.Text);
                            listaagregar.SubItems.Add(TB_Colonia.Text);
                            listaagregar.SubItems.Add(TB_poblacion.Text);
                            listaagregar.SubItems.Add(TB_estado.Text);
                            listaagregar.SubItems.Add((CB_paisdirecciones.SelectedItem as ComboboxItem).Text);
                            listaagregar.SubItems.Add((CB_tipodirecciones.SelectedItem as ComboboxItem).Text);
                            listView1.Items.Add(listaagregar);

                        }
                    }
                    catch (Exception E)
                    {
                        Console.WriteLine("{0} Exception caught.", E);
                        MessageBox.Show("Verifique la conexión a la red, o que la base de datos esté disponible");
                    }

            }else{
                 MessageBox.Show("Debe llenar todos los campos");
            }

        }

        private void BT_modificar_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        public void limpiarcasillas()
        {
            TB_buscar_direcciones.Text = "";
            TB_calledireciones.Text = "";
            TB_codigopostal.Text = "";
            TB_Colonia.Text = "";
            TB_estado.Text = "";
            TB_numerointerior.Text = "";
            TB_estado.Text = "";
            TB_numeroexterior.Text = "";
            TB_poblacion.Text = "";
            CB_paisdirecciones.Text = "Seleccione";
            CB_tipodirecciones.Text = "Seleccione";
        }

        private void BT_menu_Click(object sender, EventArgs e)
        {
            capFormcap.Show();
            this.Close();
        }

        private void BT_salirdireccion_Click(object sender, EventArgs e)
        {
            capFormcap.Close();
            oFormlogin.Close();
            this.Close();
        }



    }




}
