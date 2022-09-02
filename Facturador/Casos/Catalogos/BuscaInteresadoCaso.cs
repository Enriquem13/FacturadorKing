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
    public partial class BuscaInteresadoCaso : Form
    {
        String casoid;
        String tiposolitud;

        public BuscaInteresadoCaso() { InitializeComponent(); }

        public BuscaInteresadoCaso(String sCasoId, String sTiposolicitud)
        {
            try {
                InitializeComponent();

                casoid = sCasoId;
                tiposolitud = sTiposolicitud;
                String filtrointeresados = "";
                switch (sTiposolicitud) {
                    case "1": { filtrointeresados = "1, 2, 3, 4, 5, 19"; } break;//caso_oatentes
                    case "2": { filtrointeresados = "1, 2, 3, 4, 5, 19"; } break;//caso_oatentes
                    case "3": { filtrointeresados = "1, 2, 3, 4, 5, 19"; } break;//caso_oatentes
                    case "4": { filtrointeresados = "1, 2, 3, 4, 5, 19"; } break;//caso_oatentes
                    case "5": { filtrointeresados = "1, 2, 3, 4, 5, 19"; } break;//caso_oatentes
                    case "19": { filtrointeresados = "1, 2, 3, 4, 5, 19"; } break;//caso_oatentes
                    case "7": { filtrointeresados = "1, 2, 3, 4, 5, 19, 7, 8, 9, 77, 10, 11, 12, 17, 13, 14, 6, 15, 16"; } break;//caso_marcas
                    case "8": { filtrointeresados = "1, 2, 3, 4, 5, 19, 7, 8, 9, 77, 10, 11, 12, 17, 13, 14, 6, 15, 16"; } break;//caso_marcas
                    case "9": { filtrointeresados = "1, 2, 3, 4, 5, 19, 7, 8, 9, 77, 10, 11, 12, 17, 13, 14, 6, 15, 16"; } break;//caso_marcas
                    case "77": { filtrointeresados = "1, 2, 3, 4, 5, 19, 7, 8, 9, 77, 10, 11, 12, 17, 13, 14, 6, 15, 16"; } break;//caso_marcas
                    case "10": { filtrointeresados = "1, 2, 3, 4, 5, 19, 7, 8, 9, 77, 10, 11, 12, 17, 13, 14, 6, 15, 16"; } break;//grupo 3
                    case "11": { filtrointeresados = "1, 2, 3, 4, 5, 19, 7, 8, 9, 77, 10, 11, 12, 17, 13, 14, 6, 15, 16"; } break;//grupo 3
                    case "12": { filtrointeresados = "1, 2, 3, 4, 5, 19, 7, 8, 9, 77, 10, 11, 12, 17, 13, 14, 6, 15, 16"; } break;//grupo 3
                    case "17": { filtrointeresados = "1, 2, 3, 4, 5, 19, 7, 8, 9, 77, 10, 11, 12, 17, 13, 14, 6, 15, 16"; } break;//grupo 3
                    case "13": { filtrointeresados = "1, 2, 3, 4, 5, 19, 7, 8, 9, 77, 10, 11, 12, 17, 13, 14, 6, 15, 16"; } break;//4
                    case "14": { filtrointeresados = "1, 2, 3, 4, 5, 19, 7, 8, 9, 77, 10, 11, 12, 17, 13, 14, 6, 15, 16"; } break;//5
                    case "6": { filtrointeresados = "1, 2, 3, 4, 5, 19, 7, 8, 9, 77, 10, 11, 12, 17, 13, 14, 6, 15, 16"; } break;//6
                    case "15": { filtrointeresados = "1, 2, 3, 4, 5, 19, 7, 8, 9, 77, 10, 11, 12, 17, 13, 14, 6, 15, 16"; } break;//7
                    case "16": { filtrointeresados = "1, 2, 3, 4, 5, 19, 7, 8, 9, 77, 10, 11, 12, 17, 13, 14, 6, 15, 16"; } break;//8

                }

                conect conectipoderelacion = new conect();
                String kwerytipoderelacion = "SELECT tiporelacion.TipoRelacionId , tiporelacion.TipoRelacionDescrip FROM tiporelacion where TipoRelacionId in("+ filtrointeresados + ");";
                MySqlDataReader respuestastringtipoderelacion = conectipoderelacion.getdatareader(kwerytipoderelacion);
                while (respuestastringtipoderelacion.Read())
                {
                    CB_tipoderelacion.Items.Add(validareader("TipoRelacionDescrip", "TipoRelacionId", respuestastringtipoderelacion));
                }
                respuestastringtipoderelacion.Close();
                conectipoderelacion.Cerrarconexion();
            }
            catch (Exception Exs) {
                new filelog("Linea 37, BuscaInteresadoCaso: ", " error: "+Exs.StackTrace);
            }
            


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





        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TB_nombre_consultac_KeyDown(object sender, KeyEventArgs e)
        {
            // click
            if (e.KeyCode == Keys.Enter)
            {
                //MessageBox.Show("Mensaje enviado");
                BT_buscarclientec_Click(sender, e);
            }
        }

        public void relacionarinventor() {
            try
            {
                if (listView1.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Debes Seleccionar un interesado.");
                }

                if (CB_tipoderelacion.SelectedItem == null)
                {
                    MessageBox.Show("Debes seleccionar un interesado y un tipo de relación");
                }
                else
                {
                    String sInteresadoID = listView1.SelectedItems[0].SubItems[0].Text;//id
                    String sInteresadoNombre = listView1.SelectedItems[0].SubItems[1].Text;//id
                    String iddireccion = "";
                    String sInteresadoid = "";
                    //validamos que el usuario no exista en la lista
                    conect conetvalida = new conect();
                    
                    String squeryvalidacion = "select count(*) as numinventor from casoinventor where InventorId = '" + sInteresadoID 
                                            + "' and CasoId = '" + casoid + "' and TipoSolicitudId = '" + tiposolitud + "';";

                    MySqlDataReader sRespuestavalida = conetvalida.getdatareader(squeryvalidacion);

                    if (sRespuestavalida != null)
                    {
                        sRespuestavalida.Read();
                        sInteresadoid = validareader("numinteresado", "numinteresado", sRespuestavalida).Text;
                        sRespuestavalida.Close();
                    }
                    conetvalida.Cerrarconexion();
                    if (sInteresadoid != "")
                    {
                        int iNuminte = Int16.Parse(sInteresadoid);
                        if (iNuminte >= 1)
                        {
                            MessageBox.Show("No se puede agregar el interesado porque ya está dentro de éste caso.");
                            return;
                        }
                    }



                    //Fin de la validación


                    var confirmResult = MessageBox.Show("¿Seguro que desea agregar " + sInteresadoNombre + "al caso " + casoid + " ?", "Agregar Interesado", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {


                        conect conectdireccion = new conect();
                        String keweryiddireccion = "SELECT DireccionID FROM direccion WHERE direccion.InteresadoId = " + sInteresadoID + ";";
                        MySqlDataReader respuestastringiddireccion = conectdireccion.getdatareader(keweryiddireccion);

                        if (respuestastringiddireccion != null)
                        {
                            respuestastringiddireccion.Read();
                            iddireccion = validareader("DireccionID", "DireccionID", respuestastringiddireccion).Text;
                            respuestastringiddireccion.Close();
                        }
                        conectdireccion.Cerrarconexion();
                        conect conecsecuencia = new conect();
                        String kwwerysecuencia = "SELECT casoinventor.CasoInventorId,  casoinventor.CasoInteresadoSecuencia AS NumeroSecuencia " +
                                                "FROM casoinventor WHERE CasoId = " + casoid + " ORDER BY CasoInteresadoSecuencia DESC LIMIT 1 ;";
                        MySqlDataReader respuestasecuencia = conecsecuencia.getdatareader(kwwerysecuencia);
                        int interesado = 0;
                        if (respuestasecuencia == null)
                        {
                            MessageBox.Show("Fallo al internar consultar la secuencia");
                        }
                        else
                        {
                            respuestasecuencia.Read();
                            String interesadosecuencia = validareader("NumeroSecuencia", "CasoInventorId", respuestasecuencia).Text;

                            if (interesadosecuencia == "")
                            {
                                interesado = 1;
                            }
                            else
                            {
                                interesado = Int32.Parse(interesadosecuencia) + 1;
                            }

                            //    interesado = interesado + Int32.Parse(interesadosecuencia);
                            respuestasecuencia.Close();
                            conecsecuencia.Cerrarconexion();



                            conect conectinsert = new conect();
                            String kweryinsert = "INSERT INTO `casoinventor` " +
                                                    " (`CasoInventorId`, " +
                                                    " `InventorId`, " +
                                                    " `CasoId`, " +
                                                    " `TipoSolicitudId`, " +
                                                    " `CasoInteresadoSecuencia`, " +
                                                    " `TipoRelacionId`, " +
                                                    " `DireccionId` " +
                                                    " )   VALUES " +
                                                    " (NULL, " +
                                                    sInteresadoID +
                                                    ", " +
                                                    casoid +
                                                    ",  " +
                                                    tiposolitud +
                                                    ", " +
                                                    interesado +
                                                    ", " +
                                                    //(CB_tipoderelacion.SelectedItem as ComboboxItem).Value +   //aquí debe ser 2 inventor
                                                    " 2 " +
                                                    ", " +
                                                    iddireccion +
                                                    ");";
                            MySqlDataReader respuestastringinsert = conectinsert.getdatareader(kweryinsert);
                            if (respuestastringinsert == null)
                            {
                                MessageBox.Show("No se pudo agregar este interesado al caso");
                            }
                            else
                            {
                                MessageBox.Show("Se agrego correctamente este interesado al caso.");
                                respuestastringinsert.Close();
                                conectinsert.Cerrarconexion();
                                listView1.Items.Clear();
                                TB_nombre_consultac.Text = "";
                                CB_tipoderelacion.Text = "Seleccione";
                            }
                        }
                    }

                }
            }
            catch (Exception exs)
            {
                MessageBox.Show("No se pudo agregar titular: " + exs.Message);
            }
        }

        public void relacionartitular() {
            try {
                if (listView1.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Debes Seleccionar un interesado.");
                }

                if (CB_tipoderelacion.SelectedItem == null)
                {
                    MessageBox.Show("Debes seleccionar un interesado y un tipo de relación");
                }
                else
                {
                    String sInteresadoID = listView1.SelectedItems[0].SubItems[0].Text;//id
                    String sInteresadoNombre = listView1.SelectedItems[0].SubItems[1].Text;//id
                    String iddireccion = "";
                    String sInteresadoid = "";
                    //validamos que el usuario no exista en la lista
                    conect conetvalida = new conect();
                    String squeryvalidacion = "select count(*) as numinteresado from casointeresado where InteresadoId = '" + sInteresadoID +
                                                "' and CasoId = '" + casoid + "' and TipoSolicitudId = '" + tiposolitud + "';";
                    MySqlDataReader sRespuestavalida = conetvalida.getdatareader(squeryvalidacion);

                    if (sRespuestavalida != null)
                    {
                        sRespuestavalida.Read();
                        sInteresadoid = validareader("numinteresado", "numinteresado", sRespuestavalida).Text;
                        sRespuestavalida.Close();
                    }
                    conetvalida.Cerrarconexion();
                    if (sInteresadoid != "")
                    {
                        int iNuminte = Int16.Parse(sInteresadoid);
                        if (iNuminte >= 1)
                        {
                            MessageBox.Show("No se puede agregar el interesado porque ya está dentro de éste caso.");
                            return;
                        }
                    }



                    //Fin de la validación


                    var confirmResult = MessageBox.Show("¿Seguro que desea agregar " + sInteresadoNombre + "al caso " + casoid + " ?", "Agregar Interesado", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {


                        conect conectdireccion = new conect();
                        String keweryiddireccion = "SELECT DireccionID FROM direccion WHERE direccion.InteresadoId = " + sInteresadoID + ";";
                        MySqlDataReader respuestastringiddireccion = conectdireccion.getdatareader(keweryiddireccion);

                        if (respuestastringiddireccion != null)
                        {
                            respuestastringiddireccion.Read();
                            iddireccion = validareader("DireccionID", "DireccionID", respuestastringiddireccion).Text;
                            respuestastringiddireccion.Close();
                        }
                        conectdireccion.Cerrarconexion();
                        conect conecsecuencia = new conect();
                        String kwwerysecuencia = "SELECT casointeresado.CasoInteresadoId,  casointeresado.CasoInteresadoSecuencia AS NumeroSecuencia FROM casointeresado WHERE CasoId = " + casoid + " ORDER BY CasoInteresadoSecuencia DESC LIMIT 1 ;";
                        MySqlDataReader respuestasecuencia = conecsecuencia.getdatareader(kwwerysecuencia);
                        int interesado = 0;
                        if (respuestasecuencia == null)
                        {
                            MessageBox.Show("Fallo al internar consultar la secuencia");
                        }
                        else
                        {
                            respuestasecuencia.Read();
                            // String Idcasointeresado = validareader("CasoInteresadoId", "CasoInteresadoId", respuestasecuencia).Text;
                            String interesadosecuencia = validareader("NumeroSecuencia", "CasoInteresadoId", respuestasecuencia).Text;

                            if (interesadosecuencia == "")
                            {
                                interesado = 1;
                            }
                            else
                            {
                                interesado = Int32.Parse(interesadosecuencia) + 1;
                            }

                            //    interesado = interesado + Int32.Parse(interesadosecuencia);
                            respuestasecuencia.Close();
                            conecsecuencia.Cerrarconexion();



                            conect conectinsert = new conect();
                            String kweryinsert = "INSERT INTO `casointeresado` " +
                                                    " (`CasoInteresadoId`, " +
                                                    " `InteresadoId`, " +
                                                    " `CasoId`, " +
                                                    " `TipoSolicitudId`, " +
                                                    " `CasoInteresadoSecuencia`, " +
                                                    " `TipoRelacionId`, " +
                                                    " `DireccionId` " +
                                                    " )   VALUES " +
                                                    " (NULL, " +
                                                    sInteresadoID +
                                                    ", " +
                                                    casoid +
                                                    ",  " +
                                                    tiposolitud +
                                                    ", " +
                                                    interesado +
                                                    ", " +
                                                    //(CB_tipoderelacion.SelectedItem as ComboboxItem).Value +   //aquí debe ser 1
                                                    " 1 " +
                                                    ", " +
                                                    iddireccion +
                                                    ");";
                            MySqlDataReader respuestastringinsert = conectinsert.getdatareader(kweryinsert);
                            if (respuestastringinsert == null)
                            {
                                MessageBox.Show("No se pudo agregar este interesado al caso");
                            }
                            else
                            {
                                MessageBox.Show("Se agrego correctamente este interesado al caso.");
                                respuestastringinsert.Close();
                                conectinsert.Cerrarconexion();
                                listView1.Items.Clear();
                                TB_nombre_consultac.Text = "";
                                CB_tipoderelacion.Text = "Seleccione";
                            }
                        }
                    }

                }
            } catch (Exception exs) {
                MessageBox.Show("No se pudo agregar titular: "+exs.Message);
            }
        }

        
        public void asociarinteresado2(){
            try
            {
                //Aquí debemos elegir el tipo de relacion para poder insertar en la tabla correcta ya sea interesado o inventor o en las dos 
                //podemos elegir entre 1, 2, o 3
                switch ((CB_tipoderelacion.SelectedItem as ComboboxItem).Value) {
                    case "1": { relacionartitular(); }break;
                    case "2": { relacionarinventor(); } break;
                    case "3": { relacionartitular(); } break;//caso_oatentes
                    case "4": { relacionartitular(); } break;//caso_oatentes
                    case "5": { relacionartitular(); } break;//caso_oatentes
                    case "19": { relacionartitular(); } break;//caso_oatentes
                    case "7": { relacionartitular(); } break;//caso_marcas
                    case "8": { relacionartitular(); } break;//caso_marcas
                    case "9": { relacionartitular(); } break;//caso_marcas
                    case "77": { relacionartitular(); } break;//caso_marcas
                    case "10": { relacionartitular(); } break;//grupo 3
                    case "11": { relacionartitular(); } break;//grupo 3
                    case "12": { relacionartitular(); } break;//grupo 3
                    case "17": { relacionartitular(); } break;//grupo 3
                    case "13": { relacionartitular(); } break;//4
                    case "14": { relacionartitular(); } break;//5
                    case "6": { relacionartitular(); } break;//6
                    case "15": { relacionartitular(); } break;//7
                    case "16": { relacionartitular(); } break;//8
                }
            }
            catch (Exception E)
            {
                //escribimos en log

            }
        }

        private void BT_asociaracaso_Click(object sender, EventArgs e)
        {
            asociarinteresado2();
        }

        public void buscartitular() {
            try
            {
                listView1.Items.Clear();
                if (TB_nombre_consultac.Text.Trim().Equals("") || CB_tipoderelacion.SelectedItem == null)
                {
                    MessageBox.Show("El cambo de busqueda esta vacío, Seleccione tipo y nombre");
                }
                else
                {
                    conect conectbusqint = new conect();
                    String kweryconsulta = "";

                    kweryconsulta = "SELECT " +
                                          " interesado.InteresadoID, " +
                                          "interesado.NombreUtilInt, " +
                                          //" interesado.InteresadoNombre, " +
                                          //" interesado.InteresadoApPaterno, " +
                                          // " interesado.InteresadoApMaterno, " +
                                          " interesado.InteresadoShort " +
                                      " FROM " +
                                          " interesado " +
                                          " WHERE  "+
                                          " NombreUtilInt LIKE '%" + TB_nombre_consultac.Text + "%' " + ";";

                    //" WHERE interesado.InteresadoNombre LIKE '%" + TB_nombre_consultac.Text + "%' " +
                    //" OR interesado.InteresadoApPaterno LIKE '%" + TB_nombre_consultac.Text + "%' " +
                    //" OR interesado.InteresadoApMaterno LIKE '%" + TB_nombre_consultac.Text + "%' " +
                    ////" OR interesado.InteresadoShort LIKE '%" + TB_nombre_consultac.Text + "%' " + ";";
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
                            listaagregar.SubItems.Add(validareader("NombreUtilInt", "InteresadoID", respuestastringinsert).Text); // nombre
                            // listaagregar.SubItems.Add(validareader("InteresadoApPaterno", "InteresadoID", respuestastringinsert).Text);  // apellido paterno
                            //listaagregar.SubItems.Add(validareader("InteresadoApMaterno", "InteresadoID", respuestastringinsert).Text);  // apellido materno
                            //listaagregar.SubItems.Add(validareader("InteresadoShort", "InteresadoID", respuestastringinsert).Text);  //nombre corto
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
                // log 
            }

        }

        public void buscarinventor() {
            try
            {
                listView1.Items.Clear();
                if (TB_nombre_consultac.Text.Trim().Equals("") || CB_tipoderelacion.SelectedItem == null)
                {
                    MessageBox.Show("El cambo de busqueda esta vacío, Seleccione tipo y nombre");
                }
                else
                {
                    conect conectbusqint = new conect();
                    String kweryconsulta = "SELECT " +
                                             " inventor.InventorID, " +
                                             " CONCAT ( COALESCE(InventorNombre, ''  ), ' ', " +
                                             " COALESCE(InventorApPaterno, ''), ' ', " +
                                             " NombreUtilInt, " +
                                             " inventor.InventorShort " +
                                             " FROM " +
                                             " inventor " +
                                             " WHERE  " +
                                             "NombreUtilInt LIKE '%" + TB_nombre_consultac.Text + "%' " + " ;";
                                             

                    //" WHERE interesado.InteresadoNombre LIKE '%" + TB_nombre_consultac.Text + "%' " +
                    //" OR interesado.InteresadoApPaterno LIKE '%" + TB_nombre_consultac.Text + "%' " +
                    //" OR interesado.InteresadoApMaterno LIKE '%" + TB_nombre_consultac.Text + "%' " +
                    ////" OR interesado.InteresadoShort LIKE '%" + TB_nombre_consultac.Text + "%' " + ";";
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
                            ListViewItem listaagregar = new ListViewItem(validareader("InventorID", "InventorID", respuestastringinsert).Text); // id interesado
                            listaagregar.SubItems.Add(validareader("interesadonombrecompleto", "InventorID", respuestastringinsert).Text); // nombre
                            listaagregar.SubItems.Add(validareader("InventorShort", "InventorID", respuestastringinsert).Text);  //nombre corto
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
                // log 
            }
        
        }

        private void BT_buscarclientec_Click(object sender, EventArgs e)
        {
            //consultamos  sTablainteresado 
            try {
                    if (sTablainteresado=="titular") {
                        buscartitular();
                    }
                    else { //inventor
                        buscarinventor();
                    }
                }catch (Exception exsp) {
                MessageBox.Show("No se pudo consultar"+exsp.Message);
            }
           
        }

        //Muestra el nombre que seleciono 
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count > 0)
            {
                //Nombre del seleccionado
                textBox_selected.Text = listView1.SelectedItems[0].SubItems[1].Text;
            }
        }
        public String sTablainteresado = "";

        private void CB_tipoderelacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                if (CB_tipoderelacion.SelectedItem != null) {
                    String sTiporelcion = (CB_tipoderelacion.SelectedItem as ComboboxItem).Value.ToString();
                    if (sTiporelcion=="1") {//es titular
                        //consultamos a los interesados
                        sTablainteresado = "titular";
                    }
                    else {
                        if (sTiporelcion == "2"){//es inventor
                            //consultamos a los inventores
                            sTablainteresado = "inventor";
                        }
                    }
                }
                
            }
            catch (Exception exs) {
                MessageBox.Show(exs.Message);
            }
        }
    }
}
