using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturador
{
    public partial class fBuscarcorresponsal : Form
    {
            public String sClienteidvalue {get; set;}
            public String sContactoidvalue { get; set; }
            public String sClienteidtext { get; set; }
            public String sContactoidtext { get; set; }
            public String rtCorreocontacto_pass { get; set; }
            public String sgCasoid = "", sgTiposolicitud="";
            
        public fBuscarcorresponsal(String sCasoId, String sTiposolicitud, String sValorcontacto, String sTextocliente)
        {
            InitializeComponent();
            sgCasoid = sCasoId;
            sgTiposolicitud = sTiposolicitud;
            //richID.Text = sgCasoid;
            //conect conect_caso_marcas = new conect();
            //String queryPais = "select p.PaisNombre, p.PaisId from Pais p, caso_marcas c" +
            //                " WHERE p.PaisId = c.PaisId AND CasoId  = " + sgCasoid;
            
            //MySqlDataReader respuestastringpais = conect_caso_marcas.getdatareader(queryPais);
            //while (respuestastringpais.Read())
            //{
            //    //string  Paisd = Convert.ToString(queryPais);
            //    //Pais.Text.ad(validareader("ClienteNombre", "ClienteId", respuestastringpais));
            //    Pais.Text = validareader("PaisNombre", "PaisId", respuestastringpais).Text;
            //}
            //respuestastringpais.Close();
            //conect_caso_marcas.Cerrarconexion();

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

                //Buscamos primero la relacion casocorresponsal
                conect conex = new conect();
                int iFlagexistcorres=0;
                String sQryCorresponsal = "SELECT CasoCorresponsalId, ClienteId, ContactoId, CasoId, TipoSolicitudId" +
                            " FROM casocorresponsal" +
                            " WHERE CasoId  = " + sgCasoid +
                            " AND TipoSolicitudId  = '" + sgTiposolicitud + "';";
                MySqlDataReader respuestaCorresponsal = conex.getdatareader(sQryCorresponsal);
                while (respuestaCorresponsal.Read())
                {
                    iFlagexistcorres = 1;
                }
                respuestaCorresponsal.Close();
                conex.Cerrarconexion();
                //Fin busqueda

                conect con = new conect();
                object oCienteid = (comboBoxClientes.SelectedItem as ComboboxItem).Value;
                object oContactoidid = (comboBoxContacto.SelectedItem as ComboboxItem).Value;


                if (iFlagexistcorres == 1)
                {
                    String query2 = "UPDATE `casocorresponsal` SET `ClienteId` = '" + oCienteid + "', `contactoid` = '" + oContactoidid +
                                    "' WHERE (`casoid` = '" + sgCasoid + "' and TipoSolicitudId='" + sgTiposolicitud + "');";
                    MySqlDataReader respuestastringclient = con.getdatareader(query2);
                    new filelog("linea 79 ", " : " + query2);
                    if (respuestastringclient.RecordsAffected == 1)
                    {
                        sClienteidtext = (comboBoxClientes.SelectedItem as ComboboxItem).Text;
                        sContactoidtext = (comboBoxContacto.SelectedItem as ComboboxItem).Text;
                        sClienteidvalue = oCienteid.ToString();
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error al intentar modificar");
                        DialogResult = DialogResult.Cancel;
                    }
                    respuestastringclient.Close();

                }
                else
                {
                    String insertnewcasocorres = " INSERT INTO `casocorresponsal` " +
                               " (`ClienteId`, " +
                               " `ContactoId`, " +
                               " `CasoId`, " +
                               " `TipoSolicitudId`) " +
                               " VALUES (" +
                                oCienteid + ", " +
                                oContactoidid + ", " +
                                sgCasoid + ", " +
                                sgTiposolicitud + ");";
                    MySqlDataReader respinsertnewcasocorres = con.getdatareader(insertnewcasocorres);
                    if (respinsertnewcasocorres.RecordsAffected == 1)
                    {
                        sClienteidtext = (comboBoxClientes.SelectedItem as ComboboxItem).Text;
                        sContactoidtext = (comboBoxContacto.SelectedItem as ComboboxItem).Text;
                        sClienteidvalue = oCienteid.ToString();
                        DialogResult = DialogResult.OK;
                    }
                    respinsertnewcasocorres.Close();
                }
                iFlagexistcorres = 0;


            }
            catch (Exception exs) {
                new filelog(" update actualiza cliente en caso cliente en busca cliente 93", ":"+exs.StackTrace);
            }
             
        }



        private void comboBoxClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                tbContactoid.Text = "";
                conect con = new conect();
                String query2 = "SELECT ClienteId, ClienteNombre, ClienteEmail FROM cliente " +
                                "WHERE " +
                                " ClienteId =" + (comboBoxClientes.SelectedItem as ComboboxItem).Value.ToString();
                //MySqlDataReader respuestastringclient = con.getdatareader(query2);

                //while (respuestastringclient.Read())
                //{
                //    richTextBoxDireccliente.Text = validareader("ClienteEmail", "ClienteId", respuestastringclient).Text;
                //}
                //respuestastringclient.Close();
                //con.Cerrarconexion();


                richID.Text = (comboBoxClientes.SelectedItem as ComboboxItem).Value.ToString();
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

                /*consultamos el Nombre del País*/
                try {
                    conect con_2_pais = new conect();
                    String queryPais = "select pais.* from cliente, direccion, pais where direccion.ClienteId = Cliente.ClienteId and pais.paisid = direccion.PaisId and cliente.clienteid = " + (comboBoxClientes.SelectedItem as ComboboxItem).Value.ToString();
                    MySqlDataReader resp_pais = con_2_pais.getdatareader(queryPais);
                    while (resp_pais.Read())
                    {
                        Pais.Text = validareader("PaisNombre", "PaisNombre", resp_pais).Text;
                    }
                    resp_pais.Close();
                    con_2_pais.Cerrarconexion();
                }
                catch (Exception exs) {
                    new filelog("", ""+exs.Message);
                }
                
            }
            catch (Exception exs) { 
            }
            
            

        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //comboBoxContacto.Items.Clear();
                //Buscamos primero la relacion casocorresponsal
                conect conex = new conect();
                int iFlagexistcorres = 0;
                String sQryCorresponsal = "SELECT CasoCorresponsalId, ClienteId, ContactoId, CasoId, TipoSolicitudId" +
                            " FROM casocorresponsal" +
                            " WHERE CasoId  = " + sgCasoid +
                            " AND TipoSolicitudId  = '" + sgTiposolicitud + "';";
                MySqlDataReader respuestaCorresponsal = conex.getdatareader(sQryCorresponsal);
                while (respuestaCorresponsal.Read())
                {
                    iFlagexistcorres = 1;
                }
                respuestaCorresponsal.Close();
                conex.Cerrarconexion();
                //Fin busqueda

                conect con = new conect();
                object oCienteid = (comboBoxClientes.SelectedItem as ComboboxItem).Value;
                object oContactoidid = (comboBoxContacto.SelectedItem as ComboboxItem).Value;


                if (iFlagexistcorres == 1)
                {
                    String query2 = "DELETE FROM `casocorresponsal`" +
                                    " WHERE (`casoid` = '" + sgCasoid + "' and TipoSolicitudId='" + sgTiposolicitud + "');";
                    MySqlDataReader respuestastringclient = con.getdatareader(query2);
                    new filelog("linea 79 ", " : " + query2);
                    if (respuestastringclient.RecordsAffected == 1)
                    {
                        sClienteidtext = "";
                        sContactoidtext = "";
                        sClienteidvalue = "";
                        DialogResult = DialogResult.OK;
                        comboBoxContacto.Items.Clear();
                        MessageBox.Show("Se a eliminado la relación.");
                    }
                }
                if (iFlagexistcorres == 0)
                {
                    MessageBox.Show("No existe relacion de contacto corresponsal.");
                    comboBoxContacto.Items.Clear();
                    DialogResult = DialogResult.OK;
                    return;
                }
                iFlagexistcorres = 0;

            }
            catch (Exception exs)
            {
                new filelog(" No se logro eliminar el contacto", ":" + exs.StackTrace);
            }

        }
        private void comboBoxContacto_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try {
                richTextBox1.Text = "";
                conect con = new conect();
                String sQuerycontact = "select * from contacto where  ContactoId = " + (comboBoxContacto.SelectedItem as ComboboxItem).Value.ToString(); ;
                String sCorreocontacto = "";
                MySqlDataReader resp_correoscontact = con.getdatareader(sQuerycontact);
                String sCasoid = "";
                sCasoid = (comboBoxContacto.SelectedItem as ComboboxItem).Value.ToString();
                tbContactoid.Text = sCasoid;
                while (resp_correoscontact.Read())
                {
                    sCorreocontacto += validareader("ContactoEmail", "ContactoEmail", resp_correoscontact).Text;
                }
                resp_correoscontact.Close();
                richTextBox1.Text = sCorreocontacto;
                rtCorreocontacto_pass = sCorreocontacto;
                MySqlDataReader respuestastringclient = con.getdatareader(sQuerycontact);

                while (respuestastringclient.Read())
                {
                    richTextBoxDireccliente.Text = validareader("ContactoEmail", "ContactoId", respuestastringclient).Text;
                }
                respuestastringclient.Close();
                con.Cerrarconexion();
                richTextBox1.Text = "";
            }catch (Exception exs) {
                new filelog("", ""+exs.Message);
            }

            
        }



        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void button4_Click(object sender, EventArgs e)
        {
            addClientenuevo aClientenuevo = new addClientenuevo(this);
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
            addClientenuevo aClientenuevo = new addClientenuevo(this);
            if (aClientenuevo.ShowDialog() == DialogResult.OK)
            {
                comboBoxClientes.Items.Add(aClientenuevo.Cliente);
                comboBoxContacto.Items.Add(aClientenuevo.Contacto);
                comboBoxClientes.Text = aClientenuevo.Cliente.Text;
                comboBoxContacto.Text = aClientenuevo.Contacto.Text;
                richTextBox1.Text = aClientenuevo.Contactocorreo;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
