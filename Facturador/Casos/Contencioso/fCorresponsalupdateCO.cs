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
    public partial class fCorresponsalupdateCO : Form
    {
            public String sClienteidvalue {get; set;}
            public String sContactoidvalue { get; set; }
            public String sClienteidtext { get; set; }
            public String sContactoidtext { get; set; }
            public String rtCorreocontacto_pass { get; set; }

            public String sgCasoid = "", sgTiposolicitud="";

        //20220603FSV Agregamos banderas para saber si hay cambios para guardar
        public bool bNombre_update = false;
        public bool bNick_update = false;
        public bool bEmails_update = false;
        public bool bTelefono_update = false;




        public fCorresponsalupdateCO(String sCasoId, String sTiposolicitud, String sValorcontacto, String sTextocliente)
        {
            InitializeComponent();
            sgCasoid = sCasoId;
            sgTiposolicitud = sTiposolicitud;

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
        {
            string message = "Se actualizarán los datos del Corresponsal ¿Desea Continuar?";
            string caption = "Actualizar Datos Corresponsal";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {

                //Validamos los datos
                if (comboBoxClientes.SelectedIndex.Equals(-1))
                {
                    MessageBox.Show("El combo Corresponsal no puede estar vacio.", "Actualizar Datos Corresponsal");
                    comboBoxClientes.Focus();
                    return;
                }

                if (comboBoxContacto.SelectedIndex.Equals(-1))
                {
                    MessageBox.Show("El combo Contacto no puede estar vacio.", "Actualizar Datos Corresponsal");
                    comboBoxContacto.Focus();
                    return;
                }

                if (tbNombreContacto.Text == "")
                {
                    MessageBox.Show("El campo Nombre del Contacto no puede estar vacio.", "Actualizar Datos Corresponsal");
                    tbNombreContacto.Focus();
                    return;
                }
                //Fin de Validaciones


                //hacemos el update en casocliente y retornamos 
                //las variables ara mostrar en la vista casoconsulta
                //sgCasoid
                try
                {


                    //20220603FSVS Cambiamos



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
                        String query2 = "UPDATE `casocorresponsal` SET `ClienteId` = '" + oCienteid + "', `contactoid` = '" + oContactoidid +
                                        "' WHERE (`casoid` = '" + sgCasoid + "' and TipoSolicitudId='" + sgTiposolicitud + "');";
                        MySqlDataReader respuestastringclient = con.getdatareader(query2);
                        new filelog("linea 79 ", " : " + query2);
                        if (respuestastringclient.RecordsAffected == 1)
                        {
                            sClienteidtext = (comboBoxClientes.SelectedItem as ComboboxItem).Text;
                            sContactoidtext = (comboBoxContacto.SelectedItem as ComboboxItem).Text;
                            sClienteidvalue = oCienteid.ToString();

                            //20220606 Le pasamos los nuevos valores
                            sContactoidtext = tbNombreContacto.Text;
                            rtCorreocontacto_pass = richTextBox1.Text;


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

                            //20220606 Le pasamos los nuevos valores
                            sContactoidtext = tbNombreContacto.Text;
                            rtCorreocontacto_pass = richTextBox1.Text;


                            DialogResult = DialogResult.OK;
                        }
                        respinsertnewcasocorres.Close();
                    }
                    iFlagexistcorres = 0;




                    if (bNombre_update || bNick_update || bEmails_update || bTelefono_update)
                    {
                        actualizadatoscontacto();

                    }





                }
                catch (Exception exs)
                {
                    new filelog(" update actualiza cliente en caso cliente en busca cliente 93", ":" + exs.StackTrace);
                }



            }


             
        }



        private void comboBoxClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            conect con = new conect();
            String query2 = "SELECT ClienteId, ClienteNombre, ClienteEmail FROM cliente " +
                            "WHERE " +
                            " ClienteId =" + (comboBoxClientes.SelectedItem as ComboboxItem).Value.ToString();
            MySqlDataReader respuestastringclient = con.getdatareader(query2);

            while (respuestastringclient.Read())
            {
                richTextBoxDireccliente.Text = validareader("ClienteEmail", "ClienteId", respuestastringclient).Text;
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
            //20220603FSV Agregamos campos faltanes
            tbNombreContacto.Text = "";
            tbNick.Text = "";
            tbTelefono.Text = "";

            resetacampos();

        }



        private void comboBoxContacto_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            conect con = new conect();
            String sQuerycontact = "select * from contacto where  ContactoId = " + (comboBoxContacto.SelectedItem as ComboboxItem).Value.ToString(); ;
            String sCorreocontacto = "";
            //20220603FSV Agregamos campos faltanes
            String sNombreContacto = "";
            String sNickContacto = "";
            String sTelefonoContacto = "";

            MySqlDataReader resp_correoscontact = con.getdatareader(sQuerycontact);
            while (resp_correoscontact.Read())
            {
                sCorreocontacto += validareader("ContactoEmail", "ContactoEmail", resp_correoscontact).Text;
                //20220603FSV Agregamos campos faltanes
                sNombreContacto += validareader("ContactoNombre", "ContactoEmail", resp_correoscontact).Text;
                sNickContacto += validareader("ContactoNick", "ContactoEmail", resp_correoscontact).Text;
                sTelefonoContacto += validareader("ContactoTelefono", "ContactoEmail", resp_correoscontact).Text;

            }
            resp_correoscontact.Close();


            richTextBox1.Text = sCorreocontacto;
            rtCorreocontacto_pass = sCorreocontacto;

            //20220603FSV Agregamos campos faltanes
            tbNombreContacto.Text = sNombreContacto;
            tbNick.Text = sNickContacto;
            tbTelefono.Text= sTelefonoContacto;

            resetacampos();


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

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tbNombreContacto_ModifiedChanged(object sender, EventArgs e)
        {
            bNombre_update = true;
            tbNombreContacto.BackColor = Color.LightPink;
        }

        private void richTextBox1_ModifiedChanged(object sender, EventArgs e)
        {
            bEmails_update = true;
            richTextBox1.BackColor = Color.LightPink;
        }

        private void tbTelefono_ModifiedChanged(object sender, EventArgs e)
        {
            bTelefono_update = true;
            tbTelefono.BackColor = Color.LightPink;
        }

        private void tbNick_ModifiedChanged(object sender, EventArgs e)
        {
            bNick_update = true;
            tbNick.BackColor = Color.LightPink;
        }

        public void actualizadatoscontacto()
        {
            try
            {
                String sUpdateset = "";

                String sCorresponsalId = (comboBoxClientes.SelectedItem as ComboboxItem).Value.ToString();
                String sContactoId = (comboBoxContacto.SelectedItem as ComboboxItem).Value.ToString();


                if (bNick_update)
                {
                    sUpdateset += ", ContactoNick = '" + tbNick.Text + "'";
                }
                if (bNombre_update)
                {
                    string sCampoNombreContacto = tbNombreContacto.Text;
                    sCampoNombreContacto = sCampoNombreContacto.Replace("'", "''");
                    sUpdateset += ", ContactoNombre = '" + sCampoNombreContacto + "'";
                    sCampoNombreContacto = "";
                }
                if (bEmails_update)
                {
                    sUpdateset += ", ContactoEmail = '" + richTextBox1.Text + "'";
                }

                if (bTelefono_update)
                {
                    sUpdateset += ", ContactoTelefono = '" + tbTelefono.Text + "'";
                }

                sUpdateset = sUpdateset.Substring(1, sUpdateset.Length - 1 );

                conect concontacto = new conect();
                String sUpdateContacto = "UPDATE `contacto` SET  " + sUpdateset +
                    " WHERE `contacto`.`ContactoId` = " + sContactoId + " AND `contacto`.`ClienteId` = " + sCorresponsalId;


                MySqlDataReader resp_upcontacto = concontacto.getdatareader(sUpdateContacto);
                resp_upcontacto.Read();

                if (resp_upcontacto == null)
                {
                    MessageBox.Show("Error al hacer el update. " + sUpdateContacto);
                }
                else
                {
                    MessageBox.Show("Cambios guardados correctamente.");
                }

                resp_upcontacto.Close();
                concontacto.Cerrarconexion();




            }
            catch
            {
                MessageBox.Show("Error al intentar actualizar los datos del contacto corresponsal.");
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try {
                //pregunta de confirmacion
                var confirmResult2 = MessageBox.Show("¿Seguro que desea ELIMINAR la relación del corresponsal ?", "Eliminar Relación Corresponsal", MessageBoxButtons.YesNo);
                if (confirmResult2 == DialogResult.Yes)
                { 
                
                    conect conex = new conect();
                    int iFlagexistcorres = 0;
                    String sQryCorresponsal = "Delete " +
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

                    resetacampos();
                    sClienteidtext = "";
                    sContactoidtext = "";
                    sClienteidvalue = "";

                    //20220606 Le pasamos los nuevos valores
                    sContactoidtext = "";
                    rtCorreocontacto_pass = "";


                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception exs) {
                MessageBox.Show("Mensaje: "+exs.Message);
            }
        }

        public void resetacampos()
        {
            //20220603FSV Limpiamos todo
            bNombre_update = false;
            bNick_update = false;
            bEmails_update = false;
            bTelefono_update = false;

            richTextBox1.BackColor = Color.White;
            tbNombreContacto.BackColor = Color.White;
            tbNick.BackColor = Color.White;
            tbTelefono.BackColor = Color.White;
        }

    }
}
