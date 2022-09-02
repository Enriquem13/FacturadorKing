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
    public partial class FConsultaInteresado : Form
    {
        public captura capFormcap;
        public Form1 form1contruct;
        String idinteresado;
        public FConsultaInteresado(Form1 form, captura Formcap)
        {
            InitializeComponent();
            form1contruct = form;
            capFormcap = Formcap;
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

        private void BT_nuevocliente_Click(object sender, EventArgs e)
        {
            Finteresado finteresado = new Finteresado(form1contruct, capFormcap);
            finteresado.Show();
            this.Hide();
      
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

        public void consultainteresados()
        {
             try
              {
                  listView1.Items.Clear();

                  if (TB_nombre_consultac.Text.Trim().Equals(""))
                  {
                      MessageBox.Show("El campo de busqueda esta vacío");
                  }
                  else
                  {
                      conect conectbusqint = new conect();
                      String kweryconsulta = "SELECT " +
                                                " interesado.InteresadoID, " +
                                                " CONCAT ( COALESCE(InteresadoNombre, ''  ), ' ', " +
                                                " COALESCE(InteresadoApPaterno, ''), ' ', " +
                                                " COALESCE(InteresadoApMaterno, '')) AS interesadonombrecompleto, " +
                                                " interesado.InteresadoShort " +
                                            " FROM " +
                                                " interesado " +
                                                " WHERE   CONCAT ( COALESCE(InteresadoNombre, ''  ), ' ', " +
                                                " COALESCE(InteresadoApPaterno, ''), ' ', " +
                                                " COALESCE(InteresadoApMaterno, '')) LIKE '%" + TB_nombre_consultac.Text + "%' " + ";";
                      MySqlDataReader respuestastringinsert = conectbusqint.getdatareader(kweryconsulta);

                      if (respuestastringinsert == null)
                      {
                          MessageBox.Show("Fallo consulta");
                      }
                      else
                      {
                          int count = 0;
                          while (respuestastringinsert.Read())
                          {
                              int residuo = count % 2;
                              ListViewItem listaagregar = new ListViewItem(validareader("InteresadoID", "InteresadoID", respuestastringinsert).Text); // id interesado
                              listaagregar.SubItems.Add(validareader("interesadonombrecompleto", "InteresadoID", respuestastringinsert).Text); // nombre
                              //listaagregar.SubItems.Add(validareader("InteresadoApPaterno", "InteresadoID", respuestastringinsert).Text);  // apellido paterno
                              //listaagregar.SubItems.Add(validareader("InteresadoApMaterno", "InteresadoID", respuestastringinsert).Text);  // apellido materno
                              listaagregar.SubItems.Add(validareader("InteresadoShort", "InteresadoID", respuestastringinsert).Text);  //nombre corto
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
                          conectbusqint.Cerrarconexion();
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

        private void BT_buscarclientec_Click(object sender, EventArgs e)
        {
            consultainteresados();
        }

        private void TB_nombre_consultac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //MessageBox.Show("Mensaje enviado");
                BT_buscarclientec_Click(sender, e);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            idinteresado = listView1.SelectedItems[0].SubItems[0].Text;
            if (!listView1.SelectedItems[0].SubItems[0].Equals(null))
            {
                //Fclientedetalle detalle = new Fclientedetalle(idinteresado, oFormlogin, capFormcap);

                FInteresadoDetalle detalleinteresado = new FInteresadoDetalle(idinteresado, form1contruct, capFormcap);
                detalleinteresado.Show();
                this.Hide();

            }
        }

    }
}
