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
    public partial class addContacto : Form
    {
        public String SgContactotext { get; set; }
        public String SgContactocorreos { get; set; }
        public String sClienteidg = "";
        public String sContactoidg = "";
        public String sClientenameg = "";
        public String sCasoidg = "";
        public String sgTiposolicitudid = "";
        public addContacto(String sClienteid, String sClientename, String sContactoid, String sContactotext, String sCasoid, String sTiposolicitudid)
        {
            InitializeComponent();
            try {
                lClientename.Text = sClientename;
                sClienteidg = sClienteid;
                sContactoidg = sContactoid;
                sClientenameg = sClientename;
                sCasoidg = sCasoid;
                sgTiposolicitudid = sTiposolicitudid;

                //Agregamos ID del Cliente
                //label9.Text = sClienteid;
                lClientename.Text = sClientename + " - " + sClienteid;

                conect con_2 = new conect();
                //String query3 = "select ContactoId, ContactoNombre, ContactoTelefono, ContactoEmail "+
                String query3 = "select ContactoId, ContactoNombre, ContactoTelefono, ContactoEmail, ContactoNick " +
                    "from contacto where clienteid = '" + sClienteid +"'";
                MySqlDataReader respuestastringcontacto = con_2.getdatareader(query3);


                //while (respuestastringcontacto.Read())
                //{
                //    comboBoxContacto.Items.Add(validareader("ContactoNombre", "ContactoId", respuestastringcontacto));
                //}

                String sContactoEmail = "";
                String sContactoNick = "";
                String sContactoNombre = "";
                String sContactoTelefono = "";

                while (respuestastringcontacto.Read())
                {
                    comboBoxContacto.Items.Add(validareader("ContactoNombre", "ContactoId", respuestastringcontacto));

                    sContactoEmail = validareader("ContactoEmail", "ContactoId", respuestastringcontacto).Text;
                    sContactoNick = validareader("ContactoNick", "ContactoId", respuestastringcontacto).Text;
                    sContactoNombre = validareader("ContactoNombre", "ContactoId", respuestastringcontacto).Text;
                    sContactoTelefono = validareader("ContactoTelefono", "ContactoId", respuestastringcontacto).Text;


                }

                respuestastringcontacto.Close();
                con_2.Cerrarconexion();
                comboBoxContacto.Text = sContactotext;



                richTextBox1.Text = sContactoEmail;
                tbTelefono.Text = sContactoTelefono;
                tbNick.Text = sContactoNick;
                tbNombreContacto.Text = sContactoNombre;






            }
            catch (Exception e)
            {
                MessageBox.Show("Error al intentar cambiar el contacto");
                new filelog("Addcontacto", "Mensaje: "+e);
            }
        }

        /*private void comboBoxContacto_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            conect con = new conect();
            String sQuerycontact = "select * from contacto where  ContactoId = " + (comboBoxContacto.SelectedItem as ComboboxItem).Value.ToString();
            String ContactoNick = "";
            String ContactoNombre = "";
            String ContactoTelefono = "";
            String ContactoEmail = "";
            MySqlDataReader resp_correoscontact = con.getdatareader(sQuerycontact);
            while (resp_correoscontact.Read())
            {
                ContactoEmail += validareader("ContactoEmail", "ContactoEmail", resp_correoscontact).Text;
                ContactoNick += validareader("ContactoNick", "ContactoEmail", resp_correoscontact).Text;
                ContactoNombre += validareader("ContactoNombre", "ContactoEmail", resp_correoscontact).Text;
                ContactoTelefono += validareader("ContactoTelefono", "ContactoEmail", resp_correoscontact).Text;
            }
            resp_correoscontact.Close();
            con.Cerrarconexion();
            richTextBox1.Text = ContactoEmail;
            tbTelefono.Text = ContactoTelefono;
            tbNick.Text = ContactoNick;
        }*/

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

            string message = "Se agregará un nuevo contacto al Cliente ¿Desea Continuar?";
            string caption = "Agregar Contacto";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {

                //Validaciones
                if (textBNick.Text == "")
                {
                    MessageBox.Show("El campo Nick no puede estar vacio.", "Agregar Nuevo Contacto");
                    textBNick.Focus();
                    return;
                }

                if (textBNombre.Text == "")
                {
                    MessageBox.Show("El campo Nombre no puede estar vacio.", "Agregar Nuevo Contacto");
                    textBNombre.Focus();
                    return;
                }

                if (textBTelefono.Text == "")
                {
                    MessageBox.Show("El campo Teléfono no puede estar vacio.", "Agregar Nuevo Contacto");
                    textBTelefono.Focus();
                    return;
                }

                if (textbEmailnuevo.Text == "")
                {
                    MessageBox.Show("El campo Email no puede estar vacio.", "Agregar Nuevo Contacto");
                    textbEmailnuevo.Focus();
                    return;
                }





                //agregamos un nuevo contacto a ese cliente en la tabla contacto y le hacemos un update para que se 
                //actualice el casocliente con el caso el cliente y el contacto
                conect con = new conect();
                String sInsert = "INSERT INTO `contacto`(`ContactoId`,`ClienteId`,`ContactoNick`,`ContactoNombre`,`ContactoTelefono`,`ContactoEmail`)" +
                                "VALUES(NULL,'" + sClienteidg + "','" + textBNick.Text + "','" + textBNombre.Text + "','" + textBTelefono.Text + "','" + textbEmailnuevo.Text + "');";
                MySqlDataReader resp_correoscontact = con.getdatareader(sInsert);
                if (resp_correoscontact.RecordsAffected == 1)
                {
                    //consultamos el ultimo registro insertado 
                    conect con_tres = new conect();
                    String sQuerycontact_select_tres = "Select * from contacto order by contactoid desc limit 1;";
                    MySqlDataReader resp_correoscontact_select = con_tres.getdatareader(sQuerycontact_select_tres);
                    resp_correoscontact_select.Read();
                    String sContactonuevoid = validareader("ContactoId", "ContactoId", resp_correoscontact_select).Text;
                    resp_correoscontact_select.Close();
                    con_tres.Cerrarconexion();

                    //hacemos el udate pra actualizar al nuevo contacto con el cliente y el caso 
                    conect con_dos = new conect();
                    String sQuerycontact_update = "Update casocliente Set contactoid='" + sContactonuevoid + "' " +
                                                  "Where "
                                                  + "CasoId = " + sCasoidg + " and " +
                                                  "TipoSolicitudId = " + sgTiposolicitudid + " and " +
                                                  "ClienteId = " + sClienteidg;

                    MySqlDataReader resp_correoscontact_casocliente = con_dos.getdatareader(sQuerycontact_update);
                    if (resp_correoscontact_casocliente.RecordsAffected == 1)
                    {

                        MessageBox.Show("Se agrego un nuevo contacto  al Cliente " + sClientenameg + " y se seleccionó para éste caso.");
                        SgContactocorreos = textbEmailnuevo.Text;
                        SgContactotext = textBNombre.Text;
                        resp_correoscontact.Close();
                        con.Cerrarconexion();
                        DialogResult = DialogResult.OK;
                    }
                    resp_correoscontact_casocliente.Close();
                    con_dos.Cerrarconexion();
                }
                else
                {
                    MessageBox.Show("Error al intentar Agregar el Contacto");
                    new filelog("addContacto", "linea 112 agregando contacto");
                }

                DialogResult = DialogResult.OK;


            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button3_Click(object sender, EventArgs e)
        {//Botón para guardar uno existente
            try
            {


                string message = "Se modificarán los datos del contacto ¿Desea Continuar?";
                string caption = "Modificar Contacto";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {


                    if (tbNombreContacto.Text == "")
                    {
                        MessageBox.Show("El campo Nombre no puede estar vacio.", "Modificar datos de Contacto");
                        tbNombreContacto.Focus();
                        return;
                    }














                    //cambio de en el casocliente
                    conect con_dos = new conect();
                    String sQuerycontact_update = "Update casocliente Set contactoid='" + (comboBoxContacto.SelectedItem as ComboboxItem).Value.ToString() + "' " +
                                                  "Where CasoId = " + sCasoidg + " and " +
                                                  "ClienteId = " + sClienteidg;
                    MySqlDataReader resp_correoscontact_casocliente = con_dos.getdatareader(sQuerycontact_update);
                    if (resp_correoscontact_casocliente.RecordsAffected == 1)
                    {
                        //actualizacion de contacto
                        conect con = new conect();
                        //20220531FSV Modificamos tambien el nombre
                        //String sQuerycontact = "Update contacto Set ContactoNick='" + tbNick.Text + "',  ContactoTelefono='" + tbTelefono.Text + "', ContactoEmail='" + richTextBox1 .Text+ "' Where ContactoId = " + (comboBoxContacto.SelectedItem as ComboboxItem).Value.ToString();
                        String sQuerycontact = "Update contacto Set ContactoNick='" + tbNick.Text + "',  ContactoNombre='" + tbNombreContacto.Text + "', ContactoTelefono='" + tbTelefono.Text + "', ContactoEmail='" + richTextBox1.Text + "' Where ContactoId = " + (comboBoxContacto.SelectedItem as ComboboxItem).Value.ToString();
                        MySqlDataReader resp_correoscontact = con.getdatareader(sQuerycontact);
                        if (resp_correoscontact.RecordsAffected == 1)
                        {
                            SgContactocorreos = richTextBox1.Text;
                            //20220602FSV Cambiamos y tomamos el valor editado del cuadro de texto
                            //SgContactotext = comboBoxContacto.Text;
                            SgContactotext = tbNombreContacto.Text;
                            //20220602FSV Fin de modificación

                            resp_correoscontact.Close();
                            con.Cerrarconexion();
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("Error al intentar modificar el Contacto");
                            new filelog("addContacto", "linea 127 modificando contacto");
                        }
                        resp_correoscontact.Close();
                        con.Cerrarconexion();
                    }
                    resp_correoscontact_casocliente.Close();
                    con_dos.Cerrarconexion();

                }

            }catch (Exception E) {
                MessageBox.Show("Error al intentar seleccionar al cliente Mensaje:"+E);
            }
            
            //sContactoidg update al contacto con sContactoidg y tomando los valores del formulario
            //hacemos primero un update al contacto por si hubiese una modificacion
            //luego un update a la tabla casocliente para actualizar el caso con el cliente seleccionado
        }

        private void comboBoxContacto_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            conect con = new conect();
            String sQuerycontact = "select * from contacto where  ContactoId = " + (comboBoxContacto.SelectedItem as ComboboxItem).Value.ToString();
            String ContactoNick = "";
            String ContactoNombre = "";
            String ContactoTelefono = "";
            String ContactoEmail = "";
            MySqlDataReader resp_correoscontact = con.getdatareader(sQuerycontact);
            while (resp_correoscontact.Read())
            {
                ContactoEmail += validareader("ContactoEmail", "ContactoEmail", resp_correoscontact).Text;
                ContactoNick += validareader("ContactoNick", "ContactoEmail", resp_correoscontact).Text;
                ContactoNombre += validareader("ContactoNombre", "ContactoEmail", resp_correoscontact).Text;
                ContactoTelefono += validareader("ContactoTelefono", "ContactoEmail", resp_correoscontact).Text;
            }
            resp_correoscontact.Close();
            con.Cerrarconexion();
            richTextBox1.Text = ContactoEmail;
            tbTelefono.Text = ContactoTelefono;
            tbNick.Text = ContactoNick;
            //20220531FSV Agregamos nombre
            tbNombreContacto.Text = ContactoNombre;
            //20220513FSV Fin de modificaión
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
