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
    public partial class casoNuevoReservadederechos : Form
    {
        public Form1 fLoguin;
        public captura captura;
        public String sTipodesolicitudg;
        public casoNuevoReservadederechos(int iGrupo, captura capturaform, Form1 loguin)
        {
            fLoguin = loguin;
            captura = capturaform;
            sTipodesolicitudg = iGrupo.ToString();
            InitializeComponent();
            conect conect = new conect();

            //cliente
            String query2 = "select ClienteNombre, ClienteId from cliente order by cliente.ClienteNombre;";
            MySqlDataReader respuestastringclient = conect.getdatareader(query2);
            while (respuestastringclient.Read())
            {
                comboBoxClientes.Items.Add(validareader("ClienteNombre", "ClienteId", respuestastringclient));
            }
            respuestastringclient.Close();

            //Interesado
            String query4 = "select InteresadoID, InteresadoNombre from interesado order by InteresadoNombre;";
            MySqlDataReader respuestastringointeresado = conect.getdatareader(query4);
            while (respuestastringointeresado.Read())
            {
                comboBoxInteresado.Items.Add(validareader("InteresadoNombre", "InteresadoID", respuestastringointeresado));
            }
            respuestastringointeresado.Close();

            //agregamos el Tipo de solicitud que estan permitidos para este grupo 
            String query = "select TipoSolicitudDescrip, TipoSolicitudDescrip, TipoSolicitudId from tiposolicitud where tiposolicitudGrupo = " + sTipodesolicitudg;
            MySqlDataReader respuestastring = conect.getdatareader(query);
            while (respuestastring.Read())
            {
                comboTiposolicitud.Items.Add(validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestastring));
            }
            respuestastring.Close();

            //agregamos los responsables (Usuarios)
            String sQresponsable = "select UsuarioName, UsuarioId from usuario;";
            MySqlDataReader respuresponsable = conect.getdatareader(sQresponsable);
            while (respuresponsable.Read())
            {
                comboBoxResponsable.Items.Add(validareader("UsuarioName", "UsuarioId", respuresponsable));
            }
            comboBoxResponsable.Text = fLoguin.sUsername;
            comboBoxResponsable.SelectedValue = fLoguin.sId;
            respuresponsable.Close();

            //combobox de responsables disponibles
            String sResponsablequery = "select ResponsableClave, ResponsableId, ResponsableNombre from responsable;";
            MySqlDataReader respuestastrinresponsable = conect.getdatareader(sResponsablequery);
            //int paisint = 0;
            while (respuestastrinresponsable.Read())
            {
                comboBoxFirma.Items.Add(validareader("ResponsableNombre", "ResponsableId", respuestastrinresponsable));
                comboBoxFirma.Text = validareader("ResponsableNombre", "ResponsableId", respuestastrinresponsable).Text;
                //paisint++;
            }
            respuestastrinresponsable.Close();
            //combo box de idiomas disponibles
            String query3 = "select IdiomaId, IdiomaDescripcion from idioma;";
            MySqlDataReader respuestastringidiom = conect.getdatareader(query3);
            while (respuestastringidiom.Read())
            {
                comboBoxIdioma.Items.Add(validareader("IdiomaDescripcion", "IdiomaId", respuestastringidiom));
            }
            respuestastringidiom.Close();


            //comobo box necesarios sólo para este grupo
            //agregamos los tipos de reserva de derechos
            String sQreservaderechos = "select TipoReservaId, TipoReservaDesc, TipoReservaExplicacion from tiporeserva";
            MySqlDataReader resp_tiposreservaderechos = conect.getdatareader(sQreservaderechos);
            while (resp_tiposreservaderechos.Read())
            {
                comboboxSubtipo.Items.Add(validareader("TipoReservaDesc", "TipoReservaId", resp_tiposreservaderechos));
            }
            resp_tiposreservaderechos.Close();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            captura.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            captura.Close();
            fLoguin.Close();
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
                validareader("DireccionColonia", "DireccionID", respuestastringclient).Text + " \n" +
                validareader("DireccionEstado", "DireccionID", respuestastringclient).Text + "" +
                validareader("DireccionCP", "DireccionID", respuestastringclient).Text + " \n" +
                validareader("DireccionPoblacion", "DireccionID", respuestastringclient).Text + "" +
                validareader("PaisNombre", "DireccionID", respuestastringclient).Text;
            }
            respuestastringclient.Close();

            //
            //Borramos los cantactos anteriores, si es que los ubiera
            comboBoxContacto.Text = "";
            richTextBox1.Text = "";
            comboBoxContacto.Items.Clear();
            String query3 = "select ContactoId, ContactoNombre, ContactoTelefono, ContactoEmail from contacto where clienteid = " + (comboBoxClientes.SelectedItem as ComboboxItem).Value.ToString();
            MySqlDataReader respuestastringcontacto = con.getdatareader(query3);

            while (respuestastringcontacto.Read())
            {
                comboBoxContacto.Items.Add(validareader("ContactoNombre", "ContactoId", respuestastringcontacto));
            }
            respuestastringcontacto.Close();
        }

        private void comboBoxContacto_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            conect con = new conect();
            String sQuerycontact = "select * from contacto where  ContactoId = " + (comboBoxContacto.SelectedItem as ComboboxItem).Value.ToString();
            String sCorreocontacto = "";
            MySqlDataReader resp_correoscontact = con.getdatareader(sQuerycontact);
            while (resp_correoscontact.Read())
            {
                sCorreocontacto += validareader("ContactoEmail", "ContactoEmail", resp_correoscontact).Text;
            }
            resp_correoscontact.Close();
            richTextBox1.Text = sCorreocontacto;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            addClientenuevo aClientenuevo = new addClientenuevo(this);
            if (aClientenuevo.ShowDialog() == DialogResult.OK)
            {
                //comboBoxClientes.SelectedItem = aClientenuevo.Cliente;
                //agregamos los combobox a las listas de clientes y contactos
                comboBoxClientes.Items.Add(aClientenuevo.Cliente);
                comboBoxContacto.Items.Add(aClientenuevo.Contacto);
                //seleccionamos el valor de los combobox 
                comboBoxClientes.Text = aClientenuevo.Cliente.Text;
                comboBoxContacto.Text = aClientenuevo.Contacto.Text;
                //
                richTextBox1.Text = aClientenuevo.Contactocorreo;
            }
        }
    }
}
