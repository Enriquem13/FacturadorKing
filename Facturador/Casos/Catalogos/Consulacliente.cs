using MySql.Data.MySqlClient;
//using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturador
{
    public partial class Consulacliente : Form
    {
        public captura capFormcap;
        public Form1 form1contruct;

        public Consulacliente(Form1 form, captura Formcap)
        {
            form1contruct = form;
            capFormcap = Formcap;
            InitializeComponent();
        }



        private void BT_nuevocliente_Click(object sender, EventArgs e)
        {
            Form obj = this;
            Cliente cliente = new Cliente(form1contruct, capFormcap, obj);
            cliente.Show();
            this.Hide();
        }

        private void BT_menuconsultalciente_Click(object sender, EventArgs e)
        {
            capFormcap.Show();
            this.Close();

        }

        private void BT_salirconsultacliente_Click(object sender, EventArgs e)
        {
            capFormcap.Show();
            this.Close();
        }

        private void BT_buscarclientec_Click(object sender, EventArgs e)
        {


            buscacliente();
           
        }

        public void buscacliente()
        {
            try
            {

                listView1.Items.Clear();

                String tablaconsulta = "";
                String WHEREcliente = "";
                String WHEREcontacto = "";
       
                if (TB_nombre_consultac.Text.Trim().Equals("") && TB_contactocc_cd.Text.Trim().Equals(""))
                {

                    MessageBox.Show("Debes llenar algún campo");

                }
                else
                {


                    if (!string.IsNullOrEmpty(TB_nombre_consultac.Text.Trim()))
                    {
                        WHEREcliente = "AND CONCAT(COALESCE(ClienteNombre, ''), ' ', "+
                                       " COALESCE(ClienteApellidoPaterno, ''), ' ',  "+
                                       " COALESCE(ClienteApellidoMaterno, '')) LIKE '%" + TB_nombre_consultac.Text + "%' ";
                                      


                                      // "OR ClienteApellidoMaterno  LIKE '%" + TB_nombre_consultac.Text + "%' ";
                    }

                    if (!string.IsNullOrEmpty(TB_contactocc_cd.Text.Trim()))
                    {
                        //MessageBox.Show("BUSCO AL CONTACTO");
                        
                        tablaconsulta = "contacto, ";
                        WHEREcontacto = " AND cliente.ClienteId = contacto.ClienteId AND contacto.ContactoNombre  LIKE '%" + TB_contactocc_cd.Text + "%' ";


                    }
                    conect conect = new conect();

                    String queryinsert = "SELECT " +
                      " cliente.ClienteId, " +
                      " CONCAT(COALESCE(ClienteNombre, ''), ' '," +
                      " COALESCE(ClienteApellidoPaterno, ''), ' ',  "+
                      " COALESCE(ClienteApellidoMaterno, '')) AS clientenombrecompleto, "+
                      " cliente.ClienteApellidoPaterno, " +
                      " Traemeladireccioncliente(cliente.ClienteId) AS direccioncliente, " +
                      " Cuentameloscasos(cliente.ClienteId) AS numerodecasos, " +
                      " Traemeelcontacto(cliente.ClienteId) AS ContactoCliente " +
                      "FROM " +
                      tablaconsulta +
                      " cliente " +
                      "WHERE cliente.ClienteId = cliente.ClienteId " +
                      WHEREcliente +
                      WHEREcontacto +
                      " GROUP BY cliente.ClienteId ORDER BY cliente.ClienteNombre ASC;";

                    MySqlDataReader respuestastringinsert = conect.getdatareader(queryinsert);

                    if (respuestastringinsert == null)
                    {
                        MessageBox.Show("Fallo consulta.");
                    }
                    else
                    {

                        int count = 0;
                        while (respuestastringinsert.Read())
                        {
                            int residuo = count % 2;
                            ListViewItem listaagregar = new ListViewItem(validareader("ClienteId", "ClienteId", respuestastringinsert).Text); // id
                            listaagregar.SubItems.Add(validareader("clientenombrecompleto", "ClienteId", respuestastringinsert).Text);  //cliente nombre
                            listaagregar.SubItems.Add(validareader("direccioncliente", "ClienteId", respuestastringinsert).Text);  // direccion cliente
                            listaagregar.SubItems.Add(validareader("ContactoCliente", "ClienteId", respuestastringinsert).Text);  // contactor cliente
                            listaagregar.SubItems.Add(validareader("numerodecasos", "ClienteId", respuestastringinsert).Text);  //numero de casos por cliente
                            if (residuo == 0)
                            {
                                listaagregar.BackColor = Color.LightGray;
                            }
                            else
                            {
                                listaagregar.BackColor = Color.Azure;
                            }
                            listView1.Items.Add(listaagregar);   //funcion para agregarlos
                            listView1.FullRowSelect = true;   //funcion para selecccionarlos
                            count++;

                        }

                        respuestastringinsert.Close();
                        conect.Cerrarconexion();


                        if (count == 0)
                        {
                            MessageBox.Show("No se encontraron coincidencias");
                        }
                    }
                }
            }
            catch (Exception E)
            {
                //escribimos en log
                Console.WriteLine("{0} Exception caught.", E);
                MessageBox.Show("Fallo " + E);
            }
        }


        public String spDatocliente;
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            //TB_nombre_consultac.Text = listView1.SelectedItems[0].SubItems[0].Text;
            try {
                String datocliente = listView1.SelectedItems[0].SubItems[0].Text;//clienteid
                spDatocliente = datocliente;
                if (!listView1.SelectedItems[0].SubItems[0].Equals(null))
                {
                    Fclientedetalle detalle = new Fclientedetalle(spDatocliente, form1contruct, capFormcap);
                    detalle.ShowDialog();
                    this.Hide();
                }
            }catch(Exception Ex){
            }
            
        }


        public void abredetalle()
        {
            Fclientedetalle detalle = new Fclientedetalle(spDatocliente, form1contruct, capFormcap);
          
            detalle.ShowDialog();

            
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

        private void TB_nombre_consultac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //MessageBox.Show("Mensaje enviado");
                BT_buscarclientec_Click(sender, e);
            }
        }

        private void TB_contactocc_cd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //MessageBox.Show("Mensaje enviado");
                BT_buscarclientec_Click(sender, e);
            }
        }




    }
}
