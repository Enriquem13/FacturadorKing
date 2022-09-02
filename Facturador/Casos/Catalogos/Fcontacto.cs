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
    public partial class Fcontacto : Form
    {
        public Form1 oFormlogin;
        public captura capFormcap;
        conect con1;
        public Fcontacto(Form1 form, captura Formcap)
        {
            oFormlogin = form;
            capFormcap = Formcap;
            InitializeComponent();


            con1 = new conect();
            String query2 = "SELECT * FROM contacto";
            MySqlDataReader respuestastring20 = con1.getdatareader(query2);

            String siniContactoId = "";
            String siniClienteId = "";
            String siniContactoNick = "";
            String siniContactoNombre = "";
            String siniContactoTelefono = "";
            String siniContactoEmail = "";
            String siniContactoSexo = "";
            String siniContactoArea = "";
            String ContactoPuesto = "";
            String siniInteresadoId = "";

            while (respuestastring20.Read())
            {
                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("ContactoId")))
                {
                    siniContactoId = respuestastring20.GetString(respuestastring20.GetOrdinal("ContactoId"));
                }
                else
                {
                    siniContactoId = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("ClienteId")))
                {
                    siniClienteId = respuestastring20.GetString(respuestastring20.GetOrdinal("ClienteId"));
                }
                else
                {
                    siniClienteId = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("ContactoNick")))
                {
                    siniContactoNick = respuestastring20.GetString(respuestastring20.GetOrdinal("ContactoNick"));
                }
                else
                {
                    siniContactoNick = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("ContactoNombre")))
                {
                    siniContactoNombre = respuestastring20.GetString(respuestastring20.GetOrdinal("ContactoNombre"));
                }
                else
                {
                    siniContactoNombre = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("ContactoTelefono")))
                {
                    siniContactoTelefono = respuestastring20.GetString(respuestastring20.GetOrdinal("ContactoTelefono"));
                }
                else
                {
                    siniContactoTelefono = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("ContactoEmail")))
                {
                    siniContactoEmail = respuestastring20.GetString(respuestastring20.GetOrdinal("ContactoEmail"));
                }
                else
                {
                    siniContactoEmail = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("ContactoSexo")))
                {
                    siniContactoSexo = respuestastring20.GetString(respuestastring20.GetOrdinal("ContactoSexo"));
                }
                else
                {
                    siniContactoSexo = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("ContactoArea")))
                {
                    siniContactoArea = respuestastring20.GetString(respuestastring20.GetOrdinal("ContactoArea"));
                }
                else
                {
                    siniContactoArea = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("ContactoPuesto")))
                {
                    ContactoPuesto = respuestastring20.GetString(respuestastring20.GetOrdinal("ContactoPuesto"));
                }
                else
                {
                    ContactoPuesto = "";
                }

                if (!respuestastring20.IsDBNull(respuestastring20.GetOrdinal("InteresadoId")))
                {
                    siniInteresadoId = respuestastring20.GetString(respuestastring20.GetOrdinal("InteresadoId"));
                }
                else
                {
                    siniInteresadoId = "";
                }

                ListViewItem listaagregar = new ListViewItem(siniContactoNombre);
                listaagregar.SubItems.Add(siniContactoNick);
                listaagregar.SubItems.Add(siniClienteId);
                listaagregar.SubItems.Add(siniContactoEmail);
                listaagregar.SubItems.Add(siniContactoTelefono);
                listaagregar.SubItems.Add(siniContactoArea);
                listaagregar.SubItems.Add(ContactoPuesto);  
                listaagregar.SubItems.Add(siniContactoSexo);
                listView1.Items.Add(listaagregar);
            }

            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Nombre", 80);
            listView1.Columns.Add("Nick Name", 150);
            listView1.Columns.Add("# Cliente", 150);
            listView1.Columns.Add("Correo", 150);
            listView1.Columns.Add("Teléfono", 150);
            listView1.Columns.Add("Area", 50);
            listView1.Columns.Add("Puesto", 50);
            listView1.Columns.Add("Sexo", 50);

        }

        private void label4_Click(object sender, EventArgs e)
        {

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

        private void Bagregar_Click(object sender, EventArgs e)
        {

            if(!TB_nombre_contacto.Text.Equals("") && !TB_areacontacto.Text.Equals("") && !TB_correocontacto.Text.Equals("") &&
                !TB_telefonocontacto.Text.Equals("") && !TB_puestocontacto.Text.Equals("") && !TB_nicknamecontacto.Text.Equals(""))
            {

                try
                {



                    String queryinsert = "INSERT INTO `contacto` (`ContactoId`, `ClienteId`, `ContactoNick`, `ContactoNombre`, `ContactoTelefono`, `ContactoEmail`, `ContactoSexo`, `ContactoArea`, `ContactoPuesto`, `ContactoIndAct`, `InteresadoId`) VALUES "+
                        "(NULL, NULL,  '"+TB_nicknamecontacto.Text+ "', '" + TB_nombre_contacto.Text+ "', '"+ TB_telefonocontacto.Text+ "', '" +TB_correocontacto.Text+ "', '" +TB_sexo.Text+ "', '"+TB_areacontacto.Text+ "', '"+
                        TB_puestocontacto.Text+  "',  '1', NULL);";
                    MySqlDataReader respuestastringinsert = con1.getdatareader(queryinsert);
                    if (respuestastringinsert == null)
                    {
                        MessageBox.Show("Fallo");
                    }
                    else
                    {
                        MessageBox.Show("Se inserto");

                        ListViewItem listaagregar = new ListViewItem(TB_nombre_contacto.Text);
                        listaagregar.SubItems.Add(TB_nicknamecontacto.Text);
                        listaagregar.SubItems.Add("1");
                        listaagregar.SubItems.Add(TB_correocontacto.Text);
                        listaagregar.SubItems.Add(TB_telefonocontacto.Text);
                        listaagregar.SubItems.Add(TB_areacontacto.Text);
                        listaagregar.SubItems.Add(TB_puestocontacto.Text);
                        listaagregar.SubItems.Add(TB_sexo.Text);
                        listView1.Items.Add(listaagregar);

                        limpiarcasillas();
    

                    }


                }
                catch (Exception E)
                {
                    //escribimos en log
                    Console.WriteLine("{0} Exception caught.", E);
                    MessageBox.Show("Verifique la conexión a la red, o que la base de datos esté disponible");
                }

            }else{
                MessageBox.Show("Debe llenar todos los campos");
            }

        }

        private void Bmodificar_Click(object sender, EventArgs e)
        {

        }

        private void Beliminar_Click(object sender, EventArgs e)
        {

        }

        private void limpiarcasillas()
        {
            TB_comentarioscontacto.Text = "";
            TB_areacontacto.Text = "";
            TB_correocontacto.Text = "";
            TB_telefonocontacto.Text = "";
            TB_correocontacto.Text = "";
            TB_nombre_contacto.Text = "";
            TB_puestocontacto.Text = "";
            TB_nicknamecontacto.Text = "";
            TB_sexo.Text = "";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void BT_salircontacto_Click(object sender, EventArgs e)
        {

            capFormcap.Close();
            oFormlogin.Close();
            this.Close();

        }

        private void BT_menucontacto_Click(object sender, EventArgs e)
        {
            capFormcap.Show();
            this.Close();
        }







    }
}
