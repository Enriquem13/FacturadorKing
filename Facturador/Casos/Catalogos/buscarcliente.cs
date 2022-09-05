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
    public partial class buscarcliente : Form
    {
            public Form1 fLoguin;
            public String sClienteidvalue {get; set;}
            public String sContactoidvalue { get; set; }
            public String sClienteidtext { get; set; }
            public String sContactoidtext { get; set; }
            public String rtCorreocontacto_pass { get; set; }
            public String sgCasoid = "", sgTiposolicitud="";
        public int tiposol;
        public buscarcliente(String sCasoId, String sTiposolicitud, String sValorcontacto, String sTextocliente,Form1 loguin)
        {
            InitializeComponent();
            sgCasoid = sCasoId;
            sgTiposolicitud = sTiposolicitud;
            fLoguin = loguin;
            tiposol = Convert.ToInt32(sgTiposolicitud);
            conect conect_clientes = new conect();
            String query2 = "select ClienteNombre, ClienteId from cliente order by cliente.ClienteNombre;";
            MySqlDataReader respuestastringclient = conect_clientes.getdatareader(query2);
            while (respuestastringclient.Read())
            {
                comboBoxClientes.Items.Add(validareader("ClienteNombre", "ClienteId", respuestastringclient));
            }
            respuestastringclient.Close();
            conect_clientes.Cerrarconexion();

            //seteamos el cliente en turno
            comboBoxClientes.Text = sTextocliente;
            comboBoxContacto.Text = sValorcontacto;
            //(comboBoxClientes.SelectedItem as ComboboxItem).Text = sTextocliente;
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
        {   //hacemos el update en casocliente y retornamos 
            //las variables ara mostrar en la vista casoconsulta
            //sgCasoid
            try {
                conect conup = new conect();
                object oCienteid = (comboBoxClientes.SelectedItem as ComboboxItem).Value;
                object oContactoidid = (comboBoxContacto.SelectedItem as ComboboxItem).Value;
                String query2 = "UPDATE `casocliente` SET `ClienteId` = '" + oCienteid + "', `contactoid` = '" + oContactoidid +
                                "' WHERE (`casoid` = '" + sgCasoid + "' and TipoSolicitudId='" + sgTiposolicitud + "');";

                MySqlDataReader respuestastringclient = conup.getdatareader(query2);
                new filelog("linea 79 ", " : "+ query2);
                if (respuestastringclient.RecordsAffected == 1)
                {
                    sClienteidtext = (comboBoxClientes.SelectedItem as ComboboxItem).Text;
                    sContactoidtext = (comboBoxContacto.SelectedItem as ComboboxItem).Text;
                    sClienteidvalue = oCienteid.ToString();
                    DialogResult = DialogResult.OK;
                }
                else if(respuestastringclient.RecordsAffected == 0)
                {
                    conect conins = new conect();
                    String queryins = "Insert Into `casocliente` VAlues (null,"+ oCienteid + ", " + oContactoidid + ", " + sgCasoid +
                                    "," + sgTiposolicitud + ",0);";

                    MySqlDataReader respuestastringins= conins.getdatareader(queryins);
                    sClienteidtext = (comboBoxClientes.SelectedItem as ComboboxItem).Text;
                    sContactoidtext = (comboBoxContacto.SelectedItem as ComboboxItem).Text;
                    sClienteidvalue = oCienteid.ToString();
                    DialogResult = DialogResult.OK;
                    respuestastringins.Close();
                    conins.Cerrarconexion();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al intentar modificar");
                    DialogResult = DialogResult.Cancel;
                }
                respuestastringclient.Close();
                conup.Cerrarconexion();
               
            }
            catch (Exception exs) {
                new filelog(" update actualiza cliente en caso cliente en busca cliente 93", ":"+exs.StackTrace);
            }
            

            
        }

        private void comboBoxClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            conect con = new conect();
            String query2 = "SELECT " +
                            "direccion.DireccionID, " +
                            "direccion.DireccionCalle, " +
                            "direccion.DireccionColonia, " +
                            "direccion.DireccionEstado, " +
                            "direccion.DireccionCP, " +
                            "direccion.DireccionPoblacion, " +
                            "pais.PaisNombre " +
                            "FROM " +
                            "    direccion, " +
                            "    pais " +
                            "WHERE " +
                            "    direccion.PaisId = pais.PaisId " +
                            "AND direccion.ClienteId =" + (comboBoxClientes.SelectedItem as ComboboxItem).Value.ToString();
            MySqlDataReader respuestastringclient = con.getdatareader(query2);

            while (respuestastringclient.Read())
            {
                richTextBoxDireccliente.Text = validareader("DireccionCalle", "DireccionID", respuestastringclient).Text + " " +
                validareader("DireccionColonia", "DireccionID", respuestastringclient).Text + " " +
                validareader("DireccionEstado", "DireccionID", respuestastringclient).Text + "" +
                validareader("DireccionCP", "DireccionID", respuestastringclient).Text + " " +
                validareader("DireccionPoblacion", "DireccionID", respuestastringclient).Text + "" +
                validareader("PaisNombre", "DireccionID", respuestastringclient).Text;
            }
            respuestastringclient.Close();
            con.Cerrarconexion();
            //Borramos los cantactos anteriores, si es que los ubiera
            comboBoxContacto.Text = "";
            //richTextBoxDireccliente.Text = "";
            comboBoxContacto.Items.Clear();
            conect con_2 = new conect();
            String query3 = "select ContactoId, ContactoNombre, ContactoTelefono, ContactoEmail from contacto where clienteid = " + (comboBoxClientes.SelectedItem as ComboboxItem).Value.ToString();
            MySqlDataReader respuestastringcontacto = con_2.getdatareader(query3);

            while (respuestastringcontacto.Read())
            {
                comboBoxContacto.Items.Add(validareader("ContactoNombre", "ContactoId", respuestastringcontacto));
            }
            respuestastringcontacto.Close();
            con_2.Cerrarconexion();
            richTextBox1.Text = "";
        }

        private void comboBoxContacto_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            conect con = new conect();
            String sQuerycontact = "select * from contacto where  ContactoId = " + (comboBoxContacto.SelectedItem as ComboboxItem).Value.ToString(); ;
            String sCorreocontacto = "";
            MySqlDataReader resp_correoscontact = con.getdatareader(sQuerycontact);
            while (resp_correoscontact.Read())
            {
                sCorreocontacto += validareader("ContactoEmail", "ContactoEmail", resp_correoscontact).Text;
            }
            resp_correoscontact.Close();
            con.Cerrarconexion();
            richTextBox1.Text = sCorreocontacto;
            rtCorreocontacto_pass = sCorreocontacto;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            addClientenuevo aClientenuevo = new addClientenuevo(this, fLoguin, tiposol);
            if (aClientenuevo.ShowDialog() == DialogResult.OK)
            {
                comboBoxClientes.Items.Add(aClientenuevo.Cliente);
                comboBoxContacto.Items.Add(aClientenuevo.Contacto);
                comboBoxClientes.Text = aClientenuevo.Cliente.Text;
                comboBoxContacto.Text = aClientenuevo.Contacto.Text;
                richTextBox1.Text = aClientenuevo.Contactocorreo;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addClientenuevo aClientenuevo = new addClientenuevo(this, fLoguin,tiposol);
            if (aClientenuevo.ShowDialog() == DialogResult.OK)
            {
                comboBoxClientes.Items.Add(aClientenuevo.Cliente);
                comboBoxContacto.Items.Add(aClientenuevo.Contacto);
                comboBoxClientes.Text = aClientenuevo.Cliente.Text;
                comboBoxContacto.Text = aClientenuevo.Contacto.Text;
                richTextBox1.Text = aClientenuevo.Contactocorreo;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
