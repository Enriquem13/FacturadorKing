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
    public partial class FOficina : Form
    {
        public Form1 oFormlogin;
        public captura capFormcap;
        public string oficinaID;
        public String apoderadoID;
        public String autorizadoID;
        public FOficina(Form1 form, captura Formcap)
        {
            oFormlogin = form;
            capFormcap = Formcap;
            //oficinaID = idoficina;
            InitializeComponent();

            conect contecdireccionpais = new conect();
            String kwerydireccionpais = "SELECT pais.PaisId,  pais.PaisNombre FROM pais order by pais.PaisNombre asc";
            MySqlDataReader respuestadireccionpais = contecdireccionpais.getdatareader(kwerydireccionpais);
            while (respuestadireccionpais.Read())
            {
                CB_oficinapais.Items.Add(validareader("PaisNombre", "PaisId", respuestadireccionpais));
            }
            respuestadireccionpais.Close();
            contecdireccionpais.Cerrarconexion();

            conect conectoficina = new conect();
            String kweryoficina = "SELECT * FROM oficina;";
            MySqlDataReader respuestaoficina = conectoficina.getdatareader(kweryoficina);

            if (respuestaoficina != null)
            {


                int count = 0;
                while (respuestaoficina.Read())
                {


                    String pais = validareader("OficinaPaisId", "id_oficina", respuestaoficina).Text;
                    String paisnombre = "";
                    conect contecdireccionpais2 = new conect();
                    String kwerydireccionpais2 = "SELECT pais.PaisId,  pais.PaisNombre FROM pais WHERE PaisId = " + pais + " order by pais.PaisNombre asc";
                    MySqlDataReader respuestadireccionpais2 = contecdireccionpais2.getdatareader(kwerydireccionpais2);
                    respuestadireccionpais2.Read();

                    paisnombre = validareader("PaisNombre", "PaisId", respuestadireccionpais2).Text;
                    respuestadireccionpais2.Close();
                    contecdireccionpais2.Cerrarconexion();

                    String datoprioridad = "No Prioritaria";
                    if (validareader("PrioridadAct", "id_oficina", respuestaoficina).Text == "1")
                    {
                        datoprioridad = "Prioritaria";
                    }

                    // CB_catalogooficinapais.Items.Add(validareader("PaisNombre", "PaisId", respuestalistv));
                    ListViewItem listaagregar3 = new ListViewItem(validareader("id_oficina", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(validareader("OficinaCalle", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(validareader("OficinaNumExt", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(validareader("OficinaNumInt", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(validareader("OficinaColonia", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(validareader("OficinaLocalidad", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(validareader("OficinaEstado", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(paisnombre);
                    listaagregar3.SubItems.Add(validareader("OficinaCP", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(validareader("OficinaTelefono", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(validareader("OficinaCorreo", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(datoprioridad);


                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listaagregar3.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listaagregar3.BackColor = Color.Azure;
                    }
                    Listviewoficina.Items.Add(listaagregar3);
                    Listviewoficina.FullRowSelect = true;
                    count++;
                }

                respuestaoficina.Close();
                conectoficina.Cerrarconexion();
            }

            // aqui termina la pantalla 1

            // aqui comienza la pantalla 2

            conect conectapoderado = new conect();
            String kweryapoderado = "SELECT " +
                                       " apoderado.id_apoderado, " +
                                       " apoderado.ApoderadoNonbre, " +
                                       " apoderado.ApoderadoApellidoPat, " +
                                       " apoderado.ApoderadoApellidoMat, " +
                                       " apoderado.ApoderadoCURP, " +
                                       " apoderado.ApoderadoRFC, " +
                                       " apoderado.PrioridadAct, " +
                                       " apoderado.ApoderadoRegistroGeneraldePoderes " +
                                   " FROM " +
                                       " apoderado";
            MySqlDataReader respuestaapoderado = conectapoderado.getdatareader(kweryapoderado);

            if(respuestaapoderado !=null){
                
                int count = 0;
                while (respuestaapoderado.Read())
                {

                    String datoprioridad = "No Prioritaria";
                    if (validareader("PrioridadAct", "id_apoderado", respuestaapoderado).Text == "1")
                    {
                        datoprioridad = "Prioritaria";
                    }

                    ListViewItem listaagregar3 = new ListViewItem(validareader("id_apoderado", "id_apoderado", respuestaapoderado).Text);
                    listaagregar3.SubItems.Add(validareader("ApoderadoNonbre", "id_apoderado", respuestaapoderado).Text);
                    listaagregar3.SubItems.Add(validareader("ApoderadoApellidoPat", "id_apoderado", respuestaapoderado).Text);
                    listaagregar3.SubItems.Add(validareader("ApoderadoApellidoMat", "id_apoderado", respuestaapoderado).Text);
                    listaagregar3.SubItems.Add(validareader("ApoderadoCURP", "id_apoderado", respuestaapoderado).Text);
                    listaagregar3.SubItems.Add(validareader("ApoderadoRFC", "id_apoderado", respuestaapoderado).Text);
                    listaagregar3.SubItems.Add(validareader("ApoderadoRegistroGeneraldePoderes", "id_apoderado", respuestaapoderado).Text);
                    listaagregar3.SubItems.Add(datoprioridad);
                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listaagregar3.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listaagregar3.BackColor = Color.Azure;
                    }
                    listView1.Items.Add(listaagregar3);
                    listView1.FullRowSelect = true;
                    count++;
                }



                respuestaapoderado.Close();
                conectapoderado.Cerrarconexion();
            }

            // AQUI TERMINA LA PANTALLA 2

            // AQUI EMPIEZA LA PANTALLA 3


            conect conectautotizado = new conect();
            String kweyautorizado = "SELECT " +
                                       " autorizado.id_autorizado, " +
                                       " autorizado.AutorizadoNombre, " +
                                       " autorizado.AutorizadoApeliidoPat, " +
                                       " autorizado.AutorizadoApellidoMat, " +
                                       " autorizado.PrioridadAct, " +
                                       " autorizado.AutorizadoCURP " +
                                   " FROM " +
                                       " autorizado";
            MySqlDataReader respuestaautorizado = conectautotizado.getdatareader(kweyautorizado);

            if (respuestaautorizado !=null)
            {

                int count = 0;
                while (respuestaautorizado.Read())
                {

                    String datoprioridad = "No Prioritaria";
                    if (validareader("PrioridadAct", "id_autorizado", respuestaautorizado).Text == "1")
                    {
                        datoprioridad = "Prioritaria";
                    }

                    ListViewItem listaagregar4 = new ListViewItem(validareader("id_autorizado", "id_autorizado", respuestaautorizado).Text);
                    listaagregar4.SubItems.Add(validareader("AutorizadoNombre", "id_autorizado", respuestaautorizado).Text);
                    listaagregar4.SubItems.Add(validareader("AutorizadoApeliidoPat", "id_autorizado", respuestaautorizado).Text);
                    listaagregar4.SubItems.Add(validareader("AutorizadoApellidoMat", "id_autorizado", respuestaautorizado).Text);
                    listaagregar4.SubItems.Add(validareader("AutorizadoCURP", "id_autorizado", respuestaautorizado).Text);
                    listaagregar4.SubItems.Add(datoprioridad);
                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listaagregar4.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listaagregar4.BackColor = Color.Azure;
                    }
                    listView2.Items.Add(listaagregar4);
                    listView2.FullRowSelect = true;
                    count++;
                }

                respuestaautorizado.Close();
                conectautotizado.Cerrarconexion();
            }

            String prioritario = "0";
            for (int y = 0; y < Listviewoficina.Items.Count; y++)
            {
                if (Listviewoficina.Items[y].SubItems[11].Text == "Prioritaria")
                {
                    prioritario = "1";
                }
            }

            if (prioritario == "0")
            {
                checkBox1.Enabled = true;
            }


            String prioritarioapoderado = "0";

            for (int x = 0; x < listView1.Items.Count; x++)
            {
                if (listView1.Items[x].SubItems[7].Text == "Prioritaria")
                {
                    prioritarioapoderado = "1";
                }
            }

            if (prioritarioapoderado == "0")
            {
                checkBox2.Enabled = true;
            }

            String prioritarioautorizado = "0";
            for (int z = 0; z<listView2.Items.Count; z++)
            {
                if (listView2.Items[z].SubItems[5].Text == "Prioritaria")
                {
                    prioritarioautorizado = "1";
                }
            }

            if (prioritarioautorizado == "0")
            {
                checkBox3.Enabled = true;
            }


        }

        private void BT_menumoneda_Click(object sender, EventArgs e)
        {
            capFormcap.Show();
            this.Close();
        }

        public ComboboxItem validareader(String campoText, String campoValue, MySqlDataReader mresultado)
        {

            ComboboxItem cbvacio = new ComboboxItem();
            cbvacio.Text = "";
            cbvacio.Value = "";
            try
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
            catch (Exception E)
            {
                return cbvacio;

            }
        }

        private void BT_nuevadireccion_Click(object sender, EventArgs e)
        {
            try {

                String prioridad = "0";
                    if( checkBox1.CheckState == CheckState.Checked){
                        prioridad = "1";
                    }

                if (!TB_oficinatelefono.Text.Trim().Equals("") && !TB_oficinanumext.Text.Trim().Equals("") && !TB_oficinalocalidad.Text.Trim().Equals("")
                    && !TB_oficinaestado.Text.Trim().Equals("") && !TB_oficinacorreo.Text.Trim().Equals("") && !TB_oficinacolonia.Text.Trim().Equals("")
                    && !TB_oficinacodigopostal.Text.Trim().Equals("") &&!TB_oficinacalle.Text.Trim().Equals("") && CB_oficinapais.SelectedItem != null)
                {
                    conect conecinsert = new conect();
                    String kweryinsert = "INSERT INTO `oficina` "+
                                            " (`id_oficina`, "+
                                            " `OficinaCalle`, "+
                                            " `OficinaNumExt`, "+
                                            " `OficinaNumInt`, "+
                                            " `OficinaColonia`, "+
                                            " `OficinaLocalidad`, "+
                                            " `OficinaEstado`, "+
                                            " `OficinaPaisId`, "+
                                            " `OficinaCP`, "+
                                            " `OficinaTelefono`, "+
                                            " `OficinaCorreo`, "+
                                            " `PrioridadAct`) "+
                                            " VALUES "+
                                             "( NULL, '" +
                                             TB_oficinacalle.Text+
                                             "','" +
                                             TB_oficinanumext.Text+
                                             "','" +
                                             TB_oficinanumint.Text+
                                             "','" +
                                             TB_oficinacolonia.Text+
                                             "','" +
                                             TB_oficinalocalidad.Text+
                                             "','" +
                                             TB_oficinaestado.Text+
                                             "','" +
                                             (CB_oficinapais.SelectedItem as ComboboxItem).Value+
                                             "','" +
                                             TB_oficinacodigopostal.Text+
                                             "','" +
                                             TB_oficinatelefono.Text+
                                             "','" +
                                             TB_oficinacorreo.Text+
                                             "','" +
                                             prioridad +"');";
                    MySqlDataReader respuestastringinsert = conecinsert.getdatareader(kweryinsert);

                    if (respuestastringinsert == null)
                    {
                        MessageBox.Show("No se pudo agregar una nueva dirección");
                    }
                    else
                    {
                        respuestastringinsert.Close();
                        conecinsert.Cerrarconexion();
                        MessageBox.Show("Se agrego una nueva dirección");
                        actualizartablaoficina();
                        limpiarcasillasoficina();
                    }

                }
                else
                {
                    MessageBox.Show("Debes completar todos los campos.");
                }
            
            }
            catch (Exception E)
            {
                //escribimos en log
                Console.WriteLine("{0} Exception caught.", E);
                MessageBox.Show("Fallo " + E);
            }   
        }

        private void BT_limpiaroficina_Click(object sender, EventArgs e)
        {
            limpiarcasillasoficina();
            actualizartablaoficina();
        }


        public void limpiarcasillasoficina()
        {
            checkBox1.Enabled = false;
            CB_oficinapais.Text = "Seleccione";
            TB_oficinatelefono.Text = "";
            TB_oficinanumint.Text = "";
            TB_oficinanumext.Text = "";
            TB_oficinalocalidad.Text ="";
            TB_oficinaestado.Text = "";
            TB_oficinacorreo.Text = "";
            TB_oficinacolonia.Text = "";
            TB_oficinacodigopostal.Text = "";
            TB_oficinacalle.Text = "";
            TB_oficinanumero.Text = "";
            checkBox1.CheckState = CheckState.Unchecked;
            BT_modificaoficina.Enabled = false;
            BT_nuevadireccion.Enabled = true;

        }

        public void actualizartablaoficina()
        {
            Listviewoficina.Items.Clear();
            conect conectoficina = new conect();
            String kweryoficina = "SELECT * FROM oficina;";
            MySqlDataReader respuestaoficina = conectoficina.getdatareader(kweryoficina);

            if (respuestaoficina != null)
            {


                int count = 0;
                while (respuestaoficina.Read())
                {


                    String pais = validareader("OficinaPaisId", "id_oficina", respuestaoficina).Text;
                    String paisnombre = "";
                    conect contecdireccionpais2 = new conect();
                    String kwerydireccionpais2 = "SELECT pais.PaisId,  pais.PaisNombre FROM pais WHERE PaisId = " + pais + " order by pais.PaisNombre asc";
                    MySqlDataReader respuestadireccionpais2 = contecdireccionpais2.getdatareader(kwerydireccionpais2);
                    respuestadireccionpais2.Read();

                    paisnombre = validareader("PaisNombre", "PaisId", respuestadireccionpais2).Text;
                    respuestadireccionpais2.Close();
                    contecdireccionpais2.Cerrarconexion();

                    String datoprioridad = "No Prioritaria";
                    if (validareader("PrioridadAct", "id_oficina", respuestaoficina).Text == "1")
                    {
                        datoprioridad = "Prioritaria";
                    }

                    // CB_catalogooficinapais.Items.Add(validareader("PaisNombre", "PaisId", respuestalistv));
                    ListViewItem listaagregar3 = new ListViewItem(validareader("id_oficina", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(validareader("OficinaCalle", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(validareader("OficinaNumExt", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(validareader("OficinaNumInt", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(validareader("OficinaColonia", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(validareader("OficinaLocalidad", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(validareader("OficinaEstado", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(paisnombre);
                    listaagregar3.SubItems.Add(validareader("OficinaCP", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(validareader("OficinaTelefono", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(validareader("OficinaCorreo", "id_oficina", respuestaoficina).Text);
                    listaagregar3.SubItems.Add(datoprioridad);


                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listaagregar3.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listaagregar3.BackColor = Color.Azure;
                    }
                    Listviewoficina.Items.Add(listaagregar3);
                    Listviewoficina.FullRowSelect = true;
                    count++;
                }

                respuestaoficina.Close();
                conectoficina.Cerrarconexion();
            }


            String prioritario = "0";
            for (int y = 0; y < Listviewoficina.Items.Count; y++)
            {
                if (Listviewoficina.Items[y].SubItems[11].Text == "Prioritaria")
                {
                    prioritario = "1";
                }
            }

            if (prioritario == "0")
            {
                checkBox1.Enabled = true;
            }
        }

        private void Listviewoficina_DoubleClick(object sender, EventArgs e)
        {


            BT_modificaoficina.Enabled = true;
            BT_nuevadireccion.Enabled = false;

            oficinaID = Listviewoficina.SelectedItems[0].SubItems[0].Text;
            TB_oficinanumero.Text = Listviewoficina.SelectedItems[0].SubItems[0].Text;
            TB_oficinacalle.Text = Listviewoficina.SelectedItems[0].SubItems[1].Text;
            TB_oficinanumext.Text = Listviewoficina.SelectedItems[0].SubItems[2].Text;
            TB_oficinanumint.Text = Listviewoficina.SelectedItems[0].SubItems[3].Text;
            TB_oficinacolonia.Text = Listviewoficina.SelectedItems[0].SubItems[4].Text;
            TB_oficinalocalidad.Text = Listviewoficina.SelectedItems[0].SubItems[5].Text;
            TB_oficinaestado.Text = Listviewoficina.SelectedItems[0].SubItems[6].Text;
            CB_oficinapais.Text = Listviewoficina.SelectedItems[0].SubItems[7].Text;
            TB_oficinacodigopostal.Text = Listviewoficina.SelectedItems[0].SubItems[8].Text;
            TB_oficinatelefono.Text = Listviewoficina.SelectedItems[0].SubItems[9].Text;
            TB_oficinacorreo.Text  = Listviewoficina.SelectedItems[0].SubItems[10].Text;

            if (Listviewoficina.SelectedItems[0].SubItems[11].Text == "Prioritaria")
            {
                checkBox1.Enabled = true;
                checkBox1.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox1.Enabled = false;
                checkBox1.CheckState = CheckState.Unchecked;
            }

            String prioritario = "0";
            for (int y = 0; y < Listviewoficina.Items.Count; y++)
            {
                if (Listviewoficina.Items[y].SubItems[11].Text == "Prioritaria")
                {
                    prioritario = "1";
                }
            }

            if (prioritario == "0")
            {
                checkBox1.Enabled = true;
            }

           
        }

        private void BT_eliminaroficina_Click(object sender, EventArgs e)
        {
            try
            {
                if (Listviewoficina.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Debes Seleccionar una oficina");
                }
                else
                {

                    if (Listviewoficina.SelectedItems[0].SubItems[11].Text == "Prioritaria")
                    {
                        MessageBox.Show("Esta es la oficina prioritaria, asigne una nueva oficina prioritaria e inténtelo de nuevo");
                    }
                    else
                    {
                        String oficinaid = Listviewoficina.SelectedItems[0].SubItems[0].Text;//id
                        var confirmResult = MessageBox.Show("¿Seguro que desea ELIMINAR esta oficina ?", "Eliminar Oficina", MessageBoxButtons.YesNo);
                        if (confirmResult == DialogResult.Yes) {
                            conect conectdelete = new conect();
                            String kwerydelete = "DELETE FROM oficina WHERE oficina.id_oficina =  " + oficinaid + ";";
                            MySqlDataReader respuesta_delete = conectdelete.getdatareader(kwerydelete);
                            if (respuesta_delete == null)
                            {
                                MessageBox.Show("No se pudo eliminar la oficina.");
                            }
                            else
                            {
                                MessageBox.Show("Oficina eliminada correctamente.");
                                respuesta_delete.Close();
                                conectdelete.Cerrarconexion();
                                limpiarcasillasoficina();
                                actualizartablaoficina();
                            }
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

        private void BT_modificaoficina_Click(object sender, EventArgs e)
        {
            try {

                if (oficinaID == null)
                {
                    MessageBox.Show("Debes seleccionar una oficina");
                }
                else
                {

                    string prioridad = "0";

                    if(checkBox1.CheckState == CheckState.Checked ){
                        prioridad = "1";
                    }
                    conect conectupdate = new conect();
                    String kweryupdatre = "UPDATE `oficina` SET "+
                                            " `OficinaCalle` =  '"+TB_oficinacalle.Text+
                                            "', `OficinaNumExt` = '" +TB_oficinanumext.Text+
                                            "', `OficinaNumInt` = '" +TB_oficinanumint.Text+ 
                                            "', `OficinaColonia` = '" +TB_oficinacolonia.Text+
                                            "', `OficinaLocalidad` = '" +TB_oficinalocalidad.Text+
                                            "', `OficinaEstado` = '" +TB_oficinaestado.Text+
                                            "', `OficinaPaisId` = '" +(CB_oficinapais.SelectedItem as ComboboxItem).Value+
                                            "', `OficinaCP` = '" +TB_oficinacodigopostal.Text+
                                            "', `OficinaTelefono` = '"+TB_oficinatelefono.Text+
                                            "', `OficinaCorreo` = '"+TB_oficinacorreo.Text+
                                            "', `PrioridadAct` = '"+prioridad+
                                           "' WHERE `id_oficina` = " + oficinaID + ";";
                    MySqlDataReader respuestastringupdate = conectupdate.getdatareader(kweryupdatre);
                    if (respuestastringupdate == null)
                    {
                        MessageBox.Show("No se logro modificar la oficina");
                    }
                    else
                    {
                        respuestastringupdate.Close();
                        conectupdate.Cerrarconexion();
                        limpiarcasillasoficina();
                        actualizartablaoficina();
                        MessageBox.Show("Se modifico oficina:" + oficinaID);
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

        public void limpiarcasillaapoderado()
        {
            TB_apoderadocurp.Text = "";
            TB_apoderadomaterno.Text = "";
            TB_apoderadonombre.Text = "";
            TB_apoderadonum.Text = "";
            TB_apoderadopaterno.Text = "";
            TB_apoderadoregistro.Text = "";
            TB_apoderadorfc.Text = "";
            BT_modificarapoderado.Enabled = false;
            BT_nuevoapoderado.Enabled = true;
            checkBox2.CheckState = CheckState.Unchecked;
            checkBox2.Enabled = false;

        }

        public void actualizartablaapoderado()
        {
            try {
                listView1.Items.Clear();
                conect conectapoderado = new conect();
                String kweryapoderado = "SELECT " +
                                           " apoderado.id_apoderado, " +
                                           " apoderado.ApoderadoNonbre, " +
                                           " apoderado.ApoderadoApellidoPat, " +
                                           " apoderado.ApoderadoApellidoMat, " +
                                           " apoderado.ApoderadoCURP, " +
                                           " apoderado.ApoderadoRFC, " +
                                           " apoderado.PrioridadAct, " +
                                           " apoderado.ApoderadoRegistroGeneraldePoderes " +
                                       " FROM " +
                                           " apoderado";
                MySqlDataReader respuestaapoderado = conectapoderado.getdatareader(kweryapoderado);

                if (respuestaapoderado != null)
                {

                    int count = 0;
                    while (respuestaapoderado.Read())
                    {

                        String datoprioridad = "No Prioritaria";
                        if (validareader("PrioridadAct", "id_apoderado", respuestaapoderado).Text == "1")
                        {
                            datoprioridad = "Prioritaria";
                        }

                        ListViewItem listaagregar3 = new ListViewItem(validareader("id_apoderado", "id_apoderado", respuestaapoderado).Text);
                        listaagregar3.SubItems.Add(validareader("ApoderadoNonbre", "id_apoderado", respuestaapoderado).Text);
                        listaagregar3.SubItems.Add(validareader("ApoderadoApellidoPat", "id_apoderado", respuestaapoderado).Text);
                        listaagregar3.SubItems.Add(validareader("ApoderadoApellidoMat", "id_apoderado", respuestaapoderado).Text);
                        listaagregar3.SubItems.Add(validareader("ApoderadoCURP", "id_apoderado", respuestaapoderado).Text);
                        listaagregar3.SubItems.Add(validareader("ApoderadoRFC", "id_apoderado", respuestaapoderado).Text);
                        listaagregar3.SubItems.Add(validareader("ApoderadoRegistroGeneraldePoderes", "id_apoderado", respuestaapoderado).Text);
                        listaagregar3.SubItems.Add(datoprioridad);
                        int residuo = count % 2;
                        if (residuo == 0)
                        {
                            listaagregar3.BackColor = Color.LightGray;
                        }
                        else
                        {
                            listaagregar3.BackColor = Color.Azure;
                        }
                        listView1.Items.Add(listaagregar3);
                        listView1.FullRowSelect = true;
                        count++;
                    }



                    respuestaapoderado.Close();
                    conectapoderado.Cerrarconexion();
                }

                String prioritarioapoderado = "0";

                for (int x = 0; x < listView1.Items.Count; x++)
                {
                    if (listView1.Items[x].SubItems[7].Text == "Prioritaria")
                    {
                        prioritarioapoderado = "1";
                    }
                }

                if (prioritarioapoderado == "0")
                {
                    checkBox2.Enabled = true;
                }
               
            }
            catch (Exception E)
            {
                //escribimos en log
                Console.WriteLine("{0} Exception caught.", E);
                MessageBox.Show("Fallo " + E);
            }   
        }


        private void BT_nuevoapoderado_Click(object sender, EventArgs e)
        {

            try {
                if (!TB_apoderadonombre.Text.Trim().Equals("") && !TB_apoderadopaterno.Text.Trim().Equals("") && !TB_apoderadomaterno.Text.Trim().Equals(""))
                {
                    String prioridad = "0";
                    if (checkBox2.CheckState == CheckState.Checked)
                    {
                        prioridad = "1";
                    }

                    conect conectinsert = new conect();
                    String kweryinsert = "INSERT INTO `apoderado` "+
                                                    " (`id_apoderado`, "+
                                                    " `ApoderadoNonbre`, "+
                                                    " `ApoderadoApellidoPat`, "+
                                                    " `ApoderadoApellidoMat`, "+
                                                    " `ApoderadoCURP`, "+
                                                    " `ApoderadoRFC`, "+
                                                    "  `ApoderadoRegistroGeneraldePoderes`, "+
                                                    "  `PrioridadAct`) "+
                                                    " VALUES "+
                                                    "( NULL, '" +
                                                    TB_apoderadonombre.Text+
                                                    "','" +
                                                    TB_apoderadopaterno.Text+
                                                    "','" +
                                                    TB_apoderadomaterno.Text+
                                                    "','" +
                                                    TB_apoderadocurp.Text+
                                                    "','" +
                                                    TB_apoderadorfc.Text+
                                                    "','" +
                                                    TB_apoderadoregistro.Text+
                                                    "','" +
                                                    prioridad+
                                                    "');";
                    MySqlDataReader respuestastringinsert = conectinsert.getdatareader(kweryinsert);

                    if (respuestastringinsert == null)
                    {
                        MessageBox.Show("No se pudo agregar un nuevo apoderado");
                    }
                    else
                    {
                        MessageBox.Show("Se agrego un nuevo apoderado");
                        respuestastringinsert.Close();
                        conectinsert.Cerrarconexion();
                        limpiarcasillaapoderado();
                        actualizartablaapoderado();
                    }


                }
                else
                {
                    MessageBox.Show("Debes ingresar minimo el nombre del apoderado");
                }
            }
            catch (Exception E)
            {
                //escribimos en log
                Console.WriteLine("{0} Exception caught.", E);
                MessageBox.Show("Fallo " + E);
            } 
        }

        private void BT_limpiarapoderado_Click(object sender, EventArgs e)
        {
            actualizartablaapoderado();
            limpiarcasillaapoderado();
        }

        private void BT_eliminarapoderado_Click(object sender, EventArgs e)
        {
            //
            try
            {
                if (listView1.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Debes seleccionar un apoderado");
                }
                else
                {
                    String idapoderado = listView1.SelectedItems[0].SubItems[0].Text;//id
                    String nombre = listView1.SelectedItems[0].SubItems[1].Text;//id
                    var confirmResult = MessageBox.Show("¿Seguro que desea ELIMINAR este apoderado " + nombre + " ?", "Eliminar Apoderado", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes) {
                        conect conectdelete = new conect();
                        String kwerydeleteapoderado = "DELETE FROM apoderado WHERE id_apoderado = " + idapoderado + ";";
                        MySqlDataReader respuesta_delete = conectdelete.getdatareader(kwerydeleteapoderado);
                        if (respuesta_delete == null)
                        {
                            MessageBox.Show("No se pudo eliminar el apoderado"+nombre);
                        }
                        else
                        {
                            MessageBox.Show("Apoderado eliminado correctamente.");
                            respuesta_delete.Close();
                            conectdelete.Cerrarconexion();
                            limpiarcasillaapoderado();
                            actualizartablaapoderado();
                        }


                    }

                }
            }
            catch (Exception E)
            {
                //
               Console.WriteLine("{0} Exception caught.", E);
               MessageBox.Show("Fallo " + E);
             }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            BT_modificarapoderado.Enabled = true;
            BT_nuevoapoderado.Enabled = false;
            apoderadoID = listView1.SelectedItems[0].SubItems[0].Text;
            TB_apoderadonum.Text = listView1.SelectedItems[0].SubItems[0].Text;
            TB_apoderadonombre.Text = listView1.SelectedItems[0].SubItems[1].Text;
            TB_apoderadopaterno.Text = listView1.SelectedItems[0].SubItems[2].Text;
            TB_apoderadomaterno.Text = listView1.SelectedItems[0].SubItems[3].Text;
            TB_apoderadocurp.Text = listView1.SelectedItems[0].SubItems[4].Text;
            TB_apoderadorfc.Text = listView1.SelectedItems[0].SubItems[5].Text;
            TB_apoderadoregistro.Text = listView1.SelectedItems[0].SubItems[6].Text;

            if (listView1.SelectedItems[0].SubItems[7].Text == "Prioritaria")
            {
                checkBox2.Enabled = true;
                checkBox2.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox2.Enabled = false;
                checkBox2.CheckState = CheckState.Unchecked;
            }

            String prioritarioapoderado = "0";

            for (int x = 0; x < listView1.Items.Count; x++)
            {
                if (listView1.Items[x].SubItems[7].Text == "Prioritaria")
                {
                    prioritarioapoderado = "1";
                }
            }

            if (prioritarioapoderado == "0")
            {
                checkBox2.Enabled = true;
            }


        }

        private void BT_modificarapoderado_Click(object sender, EventArgs e)
        {
            try
            {
                //apoderadoID
                if (apoderadoID == null)
                {
                    MessageBox.Show("Debes seleccionar un apoderado");
                }
                else
                {
                    
                    string prioridad = "0";
                    if(checkBox2.CheckState == CheckState.Checked ){
                        prioridad = "1";
                    }
                    conect conectapoderadoupdate = new conect();
                    String kweryapoderadoupdate = "UPDATE `apoderado` SET "+
                                                   " `ApoderadoNonbre` = '"+TB_apoderadonombre.Text+
                                                   "', `ApoderadoApellidoPat` = '" +TB_apoderadopaterno.Text+
                                                   "', `ApoderadoApellidoMat` = '" +TB_apoderadomaterno.Text+
                                                   "', `ApoderadoCURP` = '" +TB_apoderadocurp.Text+
                                                   "', `ApoderadoRFC` = '" +TB_apoderadorfc.Text+
                                                   "', `ApoderadoRegistroGeneraldePoderes` = '" +TB_apoderadoregistro.Text+
                                                   "',`PrioridadAct` =  '" + prioridad +
                                                   "' WHERE `id_apoderado` =" + apoderadoID + ";";
                    MySqlDataReader respuestastringupdate = conectapoderadoupdate.getdatareader(kweryapoderadoupdate);

                    if (respuestastringupdate == null)
                    {
                        MessageBox.Show("No se logro modificar el apoderado");
                    }
                    else
                    {
                        respuestastringupdate.Close();
                        conectapoderadoupdate.Cerrarconexion();
                        limpiarcasillaapoderado();
                        actualizartablaapoderado();
                        MessageBox.Show("Se modifico apoderado");
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

        private void BT_nuevoautorizado_Click(object sender, EventArgs e)
        {
            try {

                if (!TB_autorizadopaterno.Text.Trim().Equals("") && !TB_autorizadonombre.Text.Trim().Equals("") && !TB_autorizadomaterno.Text.Trim().Equals(""))
                {

                    String prioridad = "0";
                    if (checkBox3.CheckState == CheckState.Checked)
                    {
                        prioridad = "1";
                    }
                    conect conectinsert = new conect();
                    String kweryinsert = "INSERT INTO `autorizado` "+
                                                " (`id_autorizado`, "+
                                                " `AutorizadoNombre`, "+
                                                " `AutorizadoApeliidoPat`, "+
                                                " `AutorizadoApellidoMat`, "+
                                                " `AutorizadoCURP`, "+
                                                " `PrioridadAct`) "+
                                                " VALUES " +
                                                 "( NULL, '" +
                                                 TB_autorizadonombre.Text+
                                                 "','" +
                                                 TB_autorizadopaterno.Text+
                                                 "','" +
                                                 TB_autorizadomaterno.Text+
                                                 "','" +
                                                 TB_autorizadocurp.Text +
                                                 "','" +
                                                 prioridad+
                                                 "');";
                    MySqlDataReader respuestastringinsert = conectinsert.getdatareader(kweryinsert);

                    if (respuestastringinsert == null)
                    {
                        MessageBox.Show("No se pudo agregar un nuevo autorizado");
                    }
                    else
                    {
                        MessageBox.Show("Se agrego un nuevo autorizado");
                        respuestastringinsert.Close();
                        conectinsert.Cerrarconexion();
                        limpiarcasillasautorizado();
                        actualizatablaautorizado();
                    }

                                                
                }
                else
                {
                    MessageBox.Show("Debes llenar minimo el nombre del autorizado.");
                }
 
            }
            catch (Exception E)
            {
                //escribimos en log
                Console.WriteLine("{0} Exception caught.", E);
                MessageBox.Show("Fallo " + E);
            }   
        }

        public void limpiarcasillasautorizado()
        {
            BT_nuevoautorizado.Enabled = true;
            BT_modificarautorizado.Enabled = false;

            TB_autorizadocurp.Text = "";
            TB_autorizadomaterno.Text = "";
            TB_autorizadonombre.Text = "";
            TB_autorizadonum.Text = "";
            TB_autorizadopaterno.Text = "";

            checkBox3.Enabled = false;
            checkBox3.CheckState = CheckState.Unchecked;
            
        }

        public void actualizatablaautorizado()
        {
            listView2.Items.Clear();
            conect conectautotizado = new conect();
            String kweyautorizado = "SELECT " +
                                       " autorizado.id_autorizado, " +
                                       " autorizado.AutorizadoNombre, " +
                                       " autorizado.AutorizadoApeliidoPat, " +
                                       " autorizado.AutorizadoApellidoMat, " +
                                       " autorizado.PrioridadAct, " +
                                       " autorizado.AutorizadoCURP " +
                                   " FROM " +
                                       " autorizado";
            MySqlDataReader respuestaautorizado = conectautotizado.getdatareader(kweyautorizado);

            if (respuestaautorizado != null)
            {

                int count = 0;
                while (respuestaautorizado.Read())
                {

                    String datoprioridad = "No Prioritaria";
                    if (validareader("PrioridadAct", "id_autorizado", respuestaautorizado).Text == "1")
                    {
                        datoprioridad = "Prioritaria";
                    }

                    ListViewItem listaagregar4 = new ListViewItem(validareader("id_autorizado", "id_autorizado", respuestaautorizado).Text);
                    listaagregar4.SubItems.Add(validareader("AutorizadoNombre", "id_autorizado", respuestaautorizado).Text);
                    listaagregar4.SubItems.Add(validareader("AutorizadoApeliidoPat", "id_autorizado", respuestaautorizado).Text);
                    listaagregar4.SubItems.Add(validareader("AutorizadoApellidoMat", "id_autorizado", respuestaautorizado).Text);
                    listaagregar4.SubItems.Add(validareader("AutorizadoCURP", "id_autorizado", respuestaautorizado).Text);
                    listaagregar4.SubItems.Add(datoprioridad);
                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listaagregar4.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listaagregar4.BackColor = Color.Azure;
                    }
                    listView2.Items.Add(listaagregar4);
                    listView2.FullRowSelect = true;
                    count++;
                }

                respuestaautorizado.Close();
                conectautotizado.Cerrarconexion();
            }

            String prioritarioautorizado = "0";
            for (int z = 0; z < listView2.Items.Count; z++)
            {
                if (listView2.Items[z].SubItems[5].Text == "Prioritaria")
                {
                    prioritarioautorizado = "1";
                }
            }

            if (prioritarioautorizado == "0")
            {
                checkBox3.Enabled = true;
            }
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            //autorizadoID
            BT_modificarautorizado.Enabled = true;
            BT_nuevoautorizado.Enabled = false;
            autorizadoID = listView2.SelectedItems[0].SubItems[0].Text;
            TB_autorizadonum.Text = listView2.SelectedItems[0].SubItems[0].Text;
            TB_autorizadonombre.Text = listView2.SelectedItems[0].SubItems[1].Text;
            TB_autorizadopaterno.Text = listView2.SelectedItems[0].SubItems[2].Text;
            TB_autorizadomaterno.Text = listView2.SelectedItems[0].SubItems[3].Text;
            TB_autorizadocurp.Text = listView2.SelectedItems[0].SubItems[4].Text;

            if (listView2.SelectedItems[0].SubItems[5].Text == "Prioritaria")
            {
                checkBox3.Enabled = true;
                checkBox3.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox3.Enabled = false;
                checkBox3.CheckState = CheckState.Unchecked;
            }

            String prioritarioautorizado = "0";
            for (int z = 0; z < listView2.Items.Count; z++)
            {
                if (listView2.Items[z].SubItems[5].Text == "Prioritaria")
                {
                    prioritarioautorizado = "1";
                }
            }

            if (prioritarioautorizado == "0")
            {
                checkBox3.Enabled = true;
            }
        }

        private void BT_limpiarautorizado_Click(object sender, EventArgs e)
        {
            actualizatablaautorizado();
            limpiarcasillasautorizado();
        }

        private void BT_eliminarautorizado_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView2.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Debes seleccionar un autorizado");
                }
                else
                {
                    String idautorizado = listView2.SelectedItems[0].SubItems[0].Text;//id
                    var confirmResult = MessageBox.Show("¿Seguro que desea ELIMINAR este autorizado ?", "Eliminar Autorizado", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes) {
                        conect conectdelete = new conect();
                        String kwerydelete = "DELETE FROM autorizado WHERE id_autorizado = " + idautorizado + ";";
                        MySqlDataReader respuesta_delete = conectdelete.getdatareader(kwerydelete);
                        if (respuesta_delete == null)
                        {
                            MessageBox.Show("No se pudo eliminar al autorizado.");
                        }
                        else
                        {
                            MessageBox.Show("Autorizado eliminado correctamente.");
                            respuesta_delete.Close();
                            conectdelete.Cerrarconexion();
                            limpiarcasillasautorizado();
                            actualizatablaautorizado();
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

        private void BT_modificarautorizado_Click(object sender, EventArgs e)
        {
            try {
                if (autorizadoID == null)
                {
                    MessageBox.Show("Debes seleccionar un autorizado");
                }
                else
                {

                    String prioridad = "0";
                    if (checkBox3.CheckState == CheckState.Checked)
                    {
                        prioridad = "1";
                    }

                    conect conectupdate = new conect();
                    String kweryupdate = "UPDATE `autorizado` SET "+
                                            " `AutorizadoNombre` = '" +TB_autorizadonombre.Text+
                                            "', `AutorizadoApeliidoPat` = '" +TB_autorizadopaterno.Text+
                                            "', `AutorizadoApellidoMat` = '"+TB_autorizadomaterno.Text+
                                            "', `AutorizadoCURP` = '"+TB_autorizadocurp.Text+
                                            "', `PrioridadAct` = '" + prioridad +
                                            "' WHERE `id_autorizado` = " + autorizadoID + ";";
                    MySqlDataReader respuestastringupdate = conectupdate.getdatareader(kweryupdate);

                    if (respuestastringupdate == null)
                    {
                        MessageBox.Show("No se logro modificar al autorizado");
                    }
                    else
                    {
                        respuestastringupdate.Close();
                        conectupdate.Cerrarconexion();
                        limpiarcasillasautorizado();
                        actualizatablaautorizado();
                        MessageBox.Show("Se modifico autorizado:" + autorizadoID);
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




    }
}
