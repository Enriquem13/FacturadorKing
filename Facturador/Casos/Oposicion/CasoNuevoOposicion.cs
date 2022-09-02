using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturador
{
    public partial class CasoNuevoOposicion : Form
    {
        public Form1 fLoguin;
        public captura captura;
        public String sTipodesolicitudg;


        //20220330FSV 
        public int iQuieninvoca;
        public int iCasoIdMarca;
        public String sCasoId;
        public String gSTipoSolicitudId = "";
        public String sgCasoIdOriginal = "";
        public String sgTipoSolIdOriginal = "";
        public String gSclienteid = "";
        public String gSContactoid = "";
        public String gSInteresadoIdOriginal = "";

        public String[] paises = new String[250];
        public String[] paisesclave = new String[250];
        public Image obj = null;
        funcionesdicss objfuncionesdicss = new funcionesdicss();



        public String validacombobox(ComboBox combo)
        {
            //20220314FSV Agregamos método para formatear contenido de combos
            if (combo.SelectedItem != null)
            {
                return (combo.SelectedItem as ComboboxItem).Value.ToString();
            }
            else
            {
                return "";
            }
        }

        public String cambiaformatofecha(String Fechauno)
        {
            //20220314FSV Agregamos método para formatear fechas
            if (Fechauno != "")
            {
                String año = Fechauno.Substring(6, 4);//yyyy
                String mes = Fechauno.Substring(3, 2);//mm
                String dia = Fechauno.Substring(0, 2);//dd
                return año + "-" + mes + "-" + dia;
            }
            else
            {
                return "";
            }
        }


        //public CasoNuevoOposicion(int iGrupo, captura capturaform, Form1 loguin)
        //Le agregamos dos parámetros OPCIONALES para que cargue el caso al abrir desde marcas (no ha funcioando)
        public CasoNuevoOposicion(int iGrupo, captura capturaform, Form1 loguin, int oiCasoidmarca=0)
        {
            fLoguin = loguin;
            captura = capturaform;
            sTipodesolicitudg = iGrupo.ToString();
            iCasoIdMarca = oiCasoidmarca;
            string sCasoIdMarca = iCasoIdMarca.ToString();


            InitializeComponent();


            /*Cargamos los responsables */
            try {
                conect conect_1 = new conect();

                String sQresponsables = "select UsuarioNombre, UsuarioId from usuario where UsuarioIndActivo = 1;";
                MySqlDataReader respuresponsables = conect_1.getdatareader(sQresponsables);

                while (respuresponsables.Read())
                {
                    comboBoxResponsableopo.Items.Add(validareader("UsuarioNombre", "UsuarioId", respuresponsables));
                }
                comboBoxResponsableopo.Text = loguin.sUsername;
                comboBoxResponsableopo.SelectedValue = loguin.sId;
                respuresponsables.Close();
                conect_1.Cerrarconexion();
            }
            catch (Exception exsd) {
                new filelog("", " mensaje:"+exsd.Message);
            }





            //Cliente

            //hacer correcciones de conexiones de aqui en adelante
            try {
                conect conect_cliente = new conect();
                String query2 = "select ClienteNombre, ClienteId from cliente order by cliente.ClienteNombre;";
                MySqlDataReader respuestastringclient = conect_cliente.getdatareader(query2);
                while (respuestastringclient.Read())
                {
                    comboBoxClientes.Items.Add(validareader("ClienteNombre", "ClienteId", respuestastringclient));
                }
                respuestastringclient.Close();
                conect_cliente.Cerrarconexion();
            }
            catch (Exception exs) {
                new filelog("", ""+exs.Message);
            }



            //Interesados
            try {
                conect conect_interesado = new conect();
                String query4 = "select InteresadoID, InteresadoNombre from interesado order by InteresadoNombre;";
                MySqlDataReader respuestastringointeresado = conect_interesado.getdatareader(query4);
                while (respuestastringointeresado.Read())
                {
                    comboBoxInteresado.Items.Add(validareader("InteresadoNombre", "InteresadoID", respuestastringointeresado));
                }
                respuestastringointeresado.Close();
                conect_interesado.Cerrarconexion();
            } catch (Exception exs) {
                new filelog("", ""+exs.Message);
            }
            

            try
            {
                //agregamos el Tipo de solicitud que estan permitidos para este grupo 
                conect conect_tiposol = new conect();
                String query = "select TipoSolicitudDescrip, TipoSolicitudDescrip, TipoSolicitudId from tiposolicitud where tiposolicitudGrupo = " + sTipodesolicitudg;
                MySqlDataReader respuestastring = conect_tiposol.getdatareader(query);

                while (respuestastring.Read())
                {
                    comboTiposolicitud.Items.Add(validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestastring));
                }
                respuestastring.Close();
                conect_tiposol.Cerrarconexion();

            }
            catch (Exception exs)
            {
                new filelog("", "" + exs.Message);
            }
            

            //agregamos los responsables (Usuarios)
            try
            {
                conect conect_users = new conect();
                String sQresponsable = "select UsuarioName, UsuarioId from usuario;";
                MySqlDataReader respuresponsable = conect_users.getdatareader(sQresponsable);
                while (respuresponsable.Read())
                {
                    comboBoxResponsable.Items.Add(validareader("UsuarioName", "UsuarioId", respuresponsable));
                }
                comboBoxResponsable.Text = fLoguin.sUsername;
                comboBoxResponsable.SelectedValue = fLoguin.sId;
                respuresponsable.Close();
                conect_users.Cerrarconexion();
            }
            catch (Exception exs)
            {
                new filelog("", "" + exs.Message);
            }

            


            //combobox de responsables disponibles
            try
            {
                conect conect_resp = new conect();
                String sResponsablequery = "select ResponsableClave, ResponsableId, ResponsableNombre from responsable;";
                MySqlDataReader respuestastrinresponsable = conect_resp.getdatareader(sResponsablequery);
                //int paisint = 0;
                while (respuestastrinresponsable.Read())
                {
                    comboBoxFirma.Items.Add(validareader("ResponsableNombre", "ResponsableId", respuestastrinresponsable));
                    comboBoxFirma.Text = validareader("ResponsableNombre", "ResponsableId", respuestastrinresponsable).Text;
                    //paisint++;
                }
                respuestastrinresponsable.Close();
                conect_resp.Cerrarconexion();
            }
            catch (Exception exs)
            {
                new filelog("", "" + exs.Message);
            }

            


            //combo box de idiomas disponibles
            try
            {
                conect conect_idioma = new conect();
                String query3 = "select IdiomaId, IdiomaDescripcion from idioma;";
                MySqlDataReader respuestastringidiom = conect_idioma.getdatareader(query3);
                while (respuestastringidiom.Read())
                {
                    comboBoxIdioma.Items.Add(validareader("IdiomaDescripcion", "IdiomaId", respuestastringidiom));
                }
                respuestastringidiom.Close();
                conect_idioma.Cerrarconexion();
            }
            catch (Exception exs)
            {
                new filelog("", "" + exs.Message);
            }
          
            //Combo Box Tipo Marcas
            //cbDTipomarca.Items.Clear();
            //conect con_tipomarcas = new conect();
            //String sQryTipoMarcas = "select TipoMarcaId, TipoMarcaDescrip from tipomarca where TipoMarcaIndAct = 1 order by TipoMarcaDescrip;";
            //MySqlDataReader respuestastringtdm = con_tipomarcas.getdatareader(sQryTipoMarcas);
            //while (respuestastringtdm.Read())
            //{
            //    cbDTipomarca.Items.Add(validareader("TipoMarcaDescrip", "TipoMarcaId", respuestastringtdm));
            //}
            //respuestastringtdm.Close();
            //con_tipomarcas.Cerrarconexion();


            //Combo paisesorden de compra fecha vigencia fecha inicio
            try
            {
            }
            catch (Exception exs)
            {
                new filelog("", "" + exs.Message);
            }
            conect conect_pais = new conect();
            String query5 = "select PaisNombre, PaisId, PaisClave from pais order by PaisNombre;";
            MySqlDataReader respuestastringopais = conect_pais.getdatareader(query5);
            while (respuestastringopais.Read())
            {
                cbCvpais.Items.Add(validareader("PaisClave", "PaisId", respuestastringopais));
                comboBoxPais.Items.Add(validareader("PaisNombre", "PaisId", respuestastringopais));
                //comboBoxPais.Items.Add(validareader("PaisNombre", "PaisId", respuestastringopais));

                paises[Convert.ToInt32(validareader("PaisNombre", "PaisId", respuestastringopais).Value.ToString())] = validareader("PaisNombre", "PaisId", respuestastringopais).Text;
                paisesclave[Convert.ToInt32(validareader("PaisClave", "PaisId", respuestastringopais).Value.ToString())] = validareader("PaisClave", "PaisId", respuestastringopais).Text;
            }
            respuestastringopais.Close();
            conect_pais.Cerrarconexion();







            //Calculamos la fecha actual
            DateTime Hoy = DateTime.Today;
            string fecha_actual = Hoy.ToString("dd-MM-yyyy");
            tbDFechaAlta.Text = fecha_actual;

            //VALORES POR DEFAULT
            comboBoxIdioma.Text = "Español";
            comboTiposolicitud.Text = "Oposición";
            cbCvpais.Text = "MX";
            tbMarcaImitadora.Focus();
            try
            {
            }
            catch (Exception exs)
            {
                new filelog("", "" + exs.Message);
            }
            conect conect_2somarca = new conect();
            String querysomarca = "select TipoSolicitudDescrip, TipoSolicitudDescrip, TipoSolicitudId from tiposolicitud WHERE TipoSolicitudGrupo = 2";
            MySqlDataReader respuestastringsomarca = conect_2somarca.getdatareader(querysomarca);
            while (respuestastringsomarca.Read())
            {
                comboTipomarca1.Items.Add(validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestastringsomarca));
            }
            respuestastringsomarca.Close();
            conect_2somarca.Cerrarconexion();
            //Se invoca la forma desde marcas
            if (iCasoIdMarca > 0)
            {
                generadom(sCasoIdMarca);
                sellacontroles();


            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            
            captura.Show();
            this.Close();//
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

        private void button1_Click(object sender, EventArgs e)
        {
            string message = "Se creará un nuevo Caso de Oposición ¿Está Seguro?";
            string caption = "Casos de Oposición";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {

                //Validaciones
                if (tbMarcaImitadora.Text == ""){
                    MessageBox.Show("El campo Marca Imitadora no puede estar vacio.","Caso Oposición");
                    tbMarcaImitadora.Focus();
                    return;}
                //cbDTipomarca
                //if (cbDTipomarca.SelectedIndex.Equals(-1))
                //{
                //    MessageBox.Show("El campo Tipo de Marca Imitadora no puede estar vacio.", "Caso Oposición");
                //    cbDTipomarca.Focus();
                //    return;
                //}

                if (tbNombreImitador.Text == ""){
                    MessageBox.Show("El campo Nombre del Imitador no puede estar vacio.", "Caso Oposición");
                    tbNombreImitador.Focus();
                    return;}

                if (comboBoxClase.SelectedIndex.Equals(-1))
                {
                    MessageBox.Show("El campo Clase no puede estar vacio.", "Caso Oposición");
                    comboBoxClase.Focus();
                    return;
                }



                if (tbExpedienteImitador.Text == "")
                {
                    MessageBox.Show("El campo Expediente no puede estar vacio.", "Caso Oposición");
                    tbExpedienteImitador.Focus();
                    return;
                }

                if (tbRefImi.Text == "")
                {
                    MessageBox.Show("El campo Referencia no puede estar vacio.", "Caso Oposición");
                    tbRefImi.Focus();
                    return;
                }

                if (tbDFecPubImitadora.Text == "")
                {
                    MessageBox.Show("El campo Plazo para Presentar Oposición a Marca Imitadora no puede estar vacio.", "Caso Oposición");
                    tbDFecPubImitadora.Focus();
                    return;
                }

                altaoposicion();

            }

        }


        public void generadom(String sCasoidgenera)
        {
            try
            {
                //InitializeComponent();
                sCasoId = sCasoidgenera;
                conect con_casosmarcas = new conect();
                String sQuery = "SELECT " +
                                "    CasoId," +
                                "    TipoSolicitudId," +
                                "    SubTipoSolicitudId," +
                                "    CasoTituloespanol," + 
                                "    CasoTituloingles," +
                                "    Get_IdiomaCliente(CasoId, TipoSolicitudId) As IdiomaId," +
                                "    DATE_FORMAT(CasoFechaConcesion , '%d-%m-%Y') as  CasoFechaConcesion," +
                                "    DATE_FORMAT(Fecha_Vigencia_internacional, '%d-%m-%Y') as Fecha_Vigencia_internacional," +
                                "    DATE_FORMAT(CasoFechaLegal , '%d-%m-%Y') as  CasoFechaLegal," +
                                "    DATE_FORMAT(CasoFechaRecepcion , '%d-%m-%Y') as  CasoFechaRecepcion," +
                                "    DATE_FORMAT(CasoFechaVigencia , '%d-%m-%Y') as  CasoFechaVigencia," +
                                "    CasoNumeroExpedienteLargo," +
                                "    CasoNumero," +
                                "    ResponsableId," +
                                "    TipoMarcaId," +
                                "    CasoLeyendaNoReservable," +
                                "    DATE_FORMAT(CasoFechaAlta , '%d-%m-%Y') as  CasoFechaAlta," +
                                "    CasoTipoCaptura," +
                                "    CasoTitular," +
                                "    DATE_FORMAT(CasoFechaFilingSistema , '%d-%m-%Y') as  CasoFechaFilingSistema," +
                                "    DATE_FORMAT(CasoFechaFilingCliente , '%d-%m-%Y') as  CasoFechaFilingCliente," +
                                "    DATE_FORMAT(CasoFechaCartaCliente , '%d-%m-%Y') as  CasoFechaCartaCliente," +
                                "   Get_Interesados_tiposol(Casoid, TipoSolicitudId) as InteresadoNombre, " +
                                "    EstatusCasoId," +
                                "    UsuarioId," +
                                "    PaisId," +
                                "    numregistrointernacional," +
                                "    productoidiomageneral," +
                                "    DATE_FORMAT(Fecharegistrointernacional , '%d-%m-%Y') as Fecharegistrointernacional," +
                                " Get_Paisclave_marcas(Casoid) as PaisClave, " +
                                " Get_Referencia(Casoid, TipoSolicitudId) as referencia, " +
                                " Get_Clase_productos(Casoid) as CasoProductosClase, " +
                                "    DATE_FORMAT(CasoFechaPruebaUsoSig , '%d-%m-%Y') as  CasoFechaPruebaUsoSig," +
                                "    CasoNumConcedida," +
                                "    DATE_FORMAT(CasoFechaprobouso , '%d-%m-%Y') as  CasoFechaprobouso," +
                                "    DATE_FORMAT(CasoFechaDeclaUso , '%d-%m-%Y') as  CasoFechaDeclaUso," +
                                "    DATE_FORMAT(CasoFechainiciouso , '%d-%m-%Y') as  CasoFechainiciouso" +
                                " FROM" +
                                "    caso_marcas" +
                                " WHERE " +
                                "        caso_marcas.CasoId = '" + sCasoidgenera + "'";
                MySqlDataReader respuestastring3 = con_casosmarcas.getdatareader(sQuery);
                while (respuestastring3.Read())
                {
                    //sgTipoSolicitudId = validareader("TipoSolicitudId", "CasoId", respuestastring3).Text;
                    sgTipoSolIdOriginal = validareader("TipoSolicitudId", "CasoId", respuestastring3).Text;
                    tbCasoIdOriginal.Text = validareader("CasoId", "CasoId", respuestastring3).Text;
                    tbCasoOriginal.Text = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                    Fecha_Vigencia_internacional.Text = validafechasvacias(validareader("Fecha_Vigencia_internacional", "CasoId", respuestastring3).Text);
                    //objmarcaactual = new caso_marcas(tbCasoid.Text, gSTipoSolicitudId);
                    //gsIdioma = objfuncionesdicss.validareader("IdiomaId", "CasoId", respuestastring3).Text;
                    textBox11.Text = validareader("numregistrointernacional", "CasoId", respuestastring3).Text;
                    
                    tbExpedienteOriginal.Text = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                    tbRegistro.Text = validareader("CasoNumConcedida", "CasoId", respuestastring3).Text;
                    tbClase.Text = validareader("CasoProductosClase", "CasoId", respuestastring3).Text;
                    tbl_pais.Text = validareader("PaisClave", "CasoId", respuestastring3).Text;
                    //gSCasoNumero = validareader("CasoNumero", "CasoId", respuestastring3).Text;

                    textBox4.Text = validafechasvacias(validareader("CasoFechaDeclaUso", "CasoId", respuestastring3).Text);
                    tbMarcaOriginal.Text = validareader("CasoTituloingles", "CasoId", respuestastring3).Text;
                    comboBoxInteresado.Text = validareader("InteresadoNombre", "CasoId", respuestastring3).Text;

                    tbDfecharecepcion.Text = validafechasvacias(validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text);
                    tbDFechaconcesion.Text = validafechasvacias(validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text);
                    tbDFechacarta.Text = validafechasvacias(validareader("CasoFechaCartaCliente", "CasoId", respuestastring3).Text);
                    tbDFechainiciouso.Text = validafechasvacias(validareader("CasoFechainiciouso", "CasoId", respuestastring3).Text);
                    tbDFechavigencia.Text = validafechasvacias(validareader("CasoFechaVigencia", "CasoId", respuestastring3).Text);
                    gSTipoSolicitudId = validareader("TipoSolicitudId", "CasoId", respuestastring3).Text;
                    // consultamos la marca logo
                    try
                    {
                        configuracionfiles objfile = new configuracionfiles();
                        objfile.configuracionfilesinicio();
                        String sFileexist = objfile.sFileupload + @"\logos_marcas\0" + sCasoId + @"\0" + sCasoId + ".gif";
                        if (File.Exists(sFileexist))
                        {
                            //aqui buscamos el logo si existe en la carpeta y lo insertamos
                            //y preguntamos si ya existe en la base para agregarlo
                            int icount = 0;

                            conect con_imglogo = new conect();
                            String simglogo = "select count(*) As num from imagen_logo where casoid = " + sCasoId + "  "  + ";";
                            MySqlDataReader resp_imglogo = con_imglogo.getdatareader(simglogo);
                            while (resp_imglogo.Read())
                            {
                                icount = int.Parse(objfuncionesdicss.validareader("num", "num", resp_imglogo).Text);// objfuncionesdicss.validareader("num", "num", resp_imglogo));//consultar idioma
                            }
                            resp_imglogo.Close();
                            con_imglogo.Cerrarconexion();

                            if (icount == 0)
                            { //quiere decir que no está agregado en la base y hay que agregarlo y mostrar posteriomente con la nueva ruta y nombre
                              //INSERT

                                String sDatetime = DateTime.Now.ToString("ddMMyyyyHHmmss").ToString();

                                String sRutaInsert = objfile.sFileupload + @"\logos_marcas\0" + sCasoId + @"\0" + sCasoId + "_" + sDatetime + ".gif";
                                System.IO.File.Copy(sFileexist, sRutaInsert, true);
                                System.IO.File.Delete(sFileexist);
                                conect con_insert_imglogo = new conect();
                                String simglogo_insert = "INSERT INTO `imagen_logo`(`ruta`,`casoid`,`TipoSolicitudId`,`fecha`)VALUES('" + sRutaInsert.Replace("\\", "\\\\") + "','" + sCasoId + "','" + gSTipoSolicitudId + "',now());" + ";";
                                MySqlDataReader resp_insert_imglogo = con_insert_imglogo.getdatareader(simglogo_insert);
                                if (resp_insert_imglogo.RecordsAffected > 0)
                                {//quiere decir que hicimos el insert correctamente
                                    obj = Image.FromFile(sRutaInsert);
                                    pbDimage.Image = obj;
                                }
                                resp_insert_imglogo.Close();
                                con_insert_imglogo.Cerrarconexion();
                            }
                            else
                            {//si tiene más de uno lo consultamos y lo colocamos en el picturebox

                                String simglogo_consulta = "select * from imagen_logo where casoid = " + sCasoId + " and TipoSolicitudId = " + gSTipoSolicitudId + " order by fecha desc limit 1;";
                                conect con_consul_imglogo = new conect();
                                MySqlDataReader resp_consul_imglogo = con_consul_imglogo.getdatareader(simglogo_consulta);
                                if (resp_consul_imglogo.Read())
                                {//quiere decir que hicimos el insert correctamente
                                    String sRutaactual = objfuncionesdicss.validareader("ruta", "ruta", resp_consul_imglogo).Text;
                                    obj = Image.FromFile(sRutaactual); ;//
                                    pbDimage.Image = obj;
                                }
                                resp_consul_imglogo.Close();
                                con_consul_imglogo.Cerrarconexion();
                            }
                        }
                        else
                        {
                            int icount = 0;

                            conect con_imglogo = new conect();
                            String simglogo = "select count(*) As num from imagen_logo where casoid = " + sCasoId + " and TipoSolicitudId = " + gSTipoSolicitudId + ";";
                            MySqlDataReader resp_imglogo = con_imglogo.getdatareader(simglogo);
                            while (resp_imglogo.Read())
                            {

                                icount = int.Parse(objfuncionesdicss.validareader("num", "num", resp_imglogo).Text);// objfuncionesdicss.validareader("num", "num", resp_imglogo));//consultar idioma
                            }
                            resp_imglogo.Close();
                            con_imglogo.Cerrarconexion();

                            if (icount > 0)
                            {//si la consulta arrojo algun resultado colocamos esa ruta
                                String simglogo_consulta = "select * from imagen_logo where casoid = " + sCasoId + " and TipoSolicitudId = " + gSTipoSolicitudId + " order by fecha desc limit 1;";
                                conect con_consul_imglogo = new conect();
                                MySqlDataReader resp_consul_imglogo = con_consul_imglogo.getdatareader(simglogo_consulta);
                                while (resp_consul_imglogo.Read())
                                {//quiere decir que hicimos el insert correctamente
                                    String sRutaactual = objfuncionesdicss.validareader("ruta", "ruta", resp_consul_imglogo).Text;
                                    obj = Image.FromFile(sRutaactual); ;//
                                    pbDimage.Image = obj;
                                }
                                resp_consul_imglogo.Close();
                                con_consul_imglogo.Cerrarconexion();
                            }
                            else
                            {//si no tiene resultados entonces no hay imagen para ésta Marca
                                pbDimage.Image = null;
                            }
                        }
                    }
                    catch (Exception exs)
                    {
                        new filelog("Al cargar logo", " :" + exs.Message);
                    }

                    //consultamos al cliente
                    conect con_cliente = new conect();
                    String squerycliente = "Select * from casocliente, cliente where " +
                        " casocliente.CasoId = " + validareader("CasoId", "CasoId", respuestastring3).Text +
                        //" and casocliente.TipoSolicitudId = " + gSTipoSolicitudId +
                        " and casocliente.TipoSolicitudId = " + sgTipoSolIdOriginal +
                        " and cliente.clienteid =  casocliente.clienteid;";
                    String sClienteid = "";
                    MySqlDataReader respuestaCliente = con_cliente.getdatareader(squerycliente);
                    while (respuestaCliente.Read())
                    {
                        //tblCliente.Text = validareader("ClienteNombre", "ClienteId", respuestaCliente).Text;
                        comboBoxClientes.Text = validareader("ClienteNombre", "ClienteId", respuestaCliente).Text;
                        sClienteid = validareader("ClienteId", "ClienteId", respuestaCliente).Text;
                        
                        gSContactoid = validareader("ContactoId", "ContactoId", respuestaCliente).Text;
                        gSclienteid = sClienteid;
                    }
                    respuestaCliente.Close();
                    con_cliente.Cerrarconexion();
                    //Consultamos el tipo de marca
                    /*conect con = new conect();
                    String query = "select TipoMarcaId, TipoMarcaDescrip from tipomarca where TipoMarcaIndAct =" + "1";
                    MySqlDataReader respuestastring = con.getdatareader(query);
                    while (respuestastring.Read())
                    {
                        comboTipomarca1.Items.Add(validareader("TipoMarcaDescrip", "TipoMarcaId", respuestastring));
                    }
                    respuestastring.Close();
                    con.Cerrarconexion();*/
                    //consultamos al contacto del cliente
                    if (sClienteid != "")
                    {
                        conect con_detalle_cliente = new conect();
                        MySqlDataReader respuestaContacto = con_detalle_cliente.getdatareader("select * from contacto where ContactoId = " + gSContactoid);
                        while (respuestaContacto.Read())
                        {
                            comboBoxContacto.Text = validareader("ContactoNombre", "ContactoId", respuestaContacto).Text;
                            richTextBox1.Text = validareader("ContactoEmail", "ContactoId", respuestaContacto).Text;
                            //gSContactoid = validareader("contactoid", "contactoid", respuestaContacto).Text;//consultar idioma
                        }
                        respuestaContacto.Close();
                        con_detalle_cliente.Cerrarconexion();
                    }

                    //consultamos al Interesado
                    if (sClienteid != "")
                    {
                        conect con_detalle_interesado = new conect();
                        String squeryinteresado = "select interesadoid, casoid, tiposolicitudid from casointeresado where " +
                                                " casoid = " + validareader("CasoId", "CasoId", respuestastring3).Text +
                                                " and TipoSolicitudId = " + sgTipoSolIdOriginal + "; ";
                        MySqlDataReader respuestaInteresado = con_detalle_interesado.getdatareader(squeryinteresado);
                        while (respuestaInteresado.Read())
                        {
                            gSInteresadoIdOriginal = validareader("interesadoid", "interesadoid", respuestaInteresado).Text;
                            //int iInteresadoinOriginal = Int32.Parse(gSInteresadoIdOriginal);
                            //comboBoxInteresado.SelectedItem = iInteresadoinOriginal;
                            //comboBoxInteresado.SelectedValue = gSInteresadoIdOriginal; //no muestra nada
                            //comboBoxInteresado.SelectedValue = gSInteresadoIdOriginal;
                            //comboBoxInteresado.SelectedIndex = iInteresadoinOriginal; truena esta opcion
                        }
                        respuestaInteresado.Close();
                        con_detalle_interesado.Cerrarconexion();
                    }

                    //Consultamos el Tipo de Marca
                    String sTipoMarcaId = validareader("TipoMarcaId", "CasoId", respuestastring3).Text;
                    if (sTipoMarcaId != "")
                    {
                        conect con_tipomarca = new conect();
                        MySqlDataReader respuestasubTipomarca = con_tipomarca.getdatareader("select * from tipomarca where TipoMarcaId = " + sTipoMarcaId);
                        while (respuestasubTipomarca.Read())
                        {
                            tbTipoMarca.Text = validareader("TipoMarcaDescrip", "TipoMarcaId", respuestasubTipomarca).Text;
                        }
                        respuestasubTipomarca.Close();
                        con_tipomarca.Cerrarconexion();
                    }
                   //Consultamos el tipo de solicitud
                    String stiposoli = validareader("TipoSolicitudId", "CasoId", respuestastring3).Text;
                    if (stiposoli != "")
                    {
                        conect con_tiposoli = new conect();
                        MySqlDataReader respuestatiposoli = con_tiposoli.getdatareader("select * from tiposolicitud where TipoSolicitudId = " + stiposoli);
                        while (respuestatiposoli.Read())
                        {
                            textBox2.Text = validareader("TipoSolicitudDescripcion", "TipoSolicitudId", respuestatiposoli).Text;
                        }
                        respuestatiposoli.Close();
                        con_tiposoli.Cerrarconexion();
                    }
                    //Consultamos el tipo de solicitud
                    String ley = validareader("SubTipoSolicitudId", "CasoId", respuestastring3).Text;
                    if (ley != "")
                    {
                        conect con_tiposley = new conect();
                        MySqlDataReader respuestatipoley = con_tiposley.getdatareader("select * from subtiposolicitud where SubTipoSolicitudId = " + ley);
                        while (respuestatipoley.Read())
                        {
                            textBox5.Text = validareader("SubTipoSolicitudDescripcion", "SubTipoSolicitudId", respuestatipoley).Text;
                        }
                        respuestatipoley.Close();
                        con_tiposley.Cerrarconexion();
                    }


                }



            }
            catch (Exception E) {
                new filelog(fLoguin.sId, "Error oposicion 785: "+E.ToString());
            }
        
        
        }

        public String validafechasvacias(String sFecha)
        {
            String resultado = "";
            try
            {
                if (sFecha == "0000-00-00" || sFecha == "0000/00/00" || sFecha == "00-00-0000")
                {
                    resultado = "";
                }
                else
                {
                    resultado = sFecha;
                }
            }
            catch (Exception Ex)
            {
                resultado = "";
            }
            return resultado;
        }

        public void sellacontroles()
        {
            tbCasoOriginal.ReadOnly = true;
            tbCasoIdOriginal.ReadOnly = true;
            tbMarcaOriginal.ReadOnly = true;
            tbExpedienteOriginal.ReadOnly = true;
            tbRegistro.ReadOnly = true;
            tbl_pais.ReadOnly = true;
            tbDfecharecepcion.ReadOnly = true;
            tbDFechaconcesion.ReadOnly = true;
            tbDFechacarta.ReadOnly = true;
            tbDFechavigencia.ReadOnly = true;
            tbDFechainiciouso.ReadOnly = true;
            comboBoxInteresado.Enabled = false;
            comboBoxClientes.Enabled = false;
            richTextBoxDireccliente.ReadOnly = true;
            comboBoxContacto.Enabled = false;
            richTextBox1.ReadOnly = true;
        }

        public void altaoposicion()
        {
            Boolean bCaso = false, bCasocliente = false, bcasointeresado = false, breferencias = false, bPrioridades = false;
            try
            {
                
                //20220314FSV (1) Inicio Recopilar datos a Grabar
                String sComboTiposolicitud = validacombobox(comboTiposolicitud);
                String sComboBoxResponsable = validacombobox(comboBoxResponsableopo);
                String sComboBoxTipoMarca = validacombobox(cbDTipomarca);
                String sComboBoxTipoMarcaRival = validacombobox(comboTipomarca1);

                if (sComboBoxTipoMarca == "")
                    sComboBoxTipoMarca = "0";

                // comboTipomarca1    //es Tipo de Solicitud para las marcas en éste caso será el tipo solicitud del rival únicamente del rival

                //String sComboBoxClase = validacombobox(comboBoxClase);
                String sComboBoxClase = comboBoxClase.Text;//Pendiente
                String sCcomboBoxPais = validacombobox(comboBoxPais);
                String scbCvpais = validacombobox(cbCvpais);
                String sComboboxSubtipo = validacombobox(comboboxSubtipo);
                String sComboBoxClientes = validacombobox(comboBoxClientes);
                String sComboBoxContacto = validacombobox(comboBoxContacto);
                String sComboBoxInteresado = validacombobox(comboBoxInteresado);
                String sComboBoxIdioma = validacombobox(comboBoxIdioma);
                String sCcomboBoxFirma = validacombobox(comboBoxFirma);
                //Nuevos campos
                String Id_Marca = validacombobox(comboTipomarca1);
                //
                string sMarcaImitadora = tbMarcaImitadora.Text;
                string sNombreImitador = tbNombreImitador.Text;
                string sExpedienteImitador = tbExpedienteImitador.Text;
                String sReferenciaImitadora = tbRefImi.Text;
                string sComentario = rtbComentario.Text;
             
                String sFechaAlta = tbDFechaAlta.Text;
                String sFechaPublicacion = tbDFecPubImitadora.Text;
                String sFechaPresentacion = tbDFecPlazoPresImitadora.Text;

                //String sFechaOposicion = tbDFecOposicion.Text;
                String sresponsableid = fLoguin.sId;
                string sCasoIdOriginal = tbCasoIdOriginal.Text;
                String sInteresadoOriginal = comboBoxInteresado.Text;
                //20220314FSV (1) Inicio Recopilar datos a Grabar




                //if (sMarcaImitadora != "" && sNombreImitador != "" && sExpedienteImitador != "" && sFechaAlta != "" && sFechaPublicacion != "" && sFechaPresentacion != "" && sFechaOposicion != "")
                if (sMarcaImitadora != "" && sNombreImitador != "" && sExpedienteImitador != "" && sFechaAlta != "" && sFechaPublicacion != "" && sFechaPresentacion != "" )
                {
                    //sTextboxFecha = cambiaformatofecha(sTextboxFecha);
                    //stextClientduedate = cambiaformatofecha(stextClientduedate);
                    //stextBoxFechainternacional = cambiaformatofecha(stextBoxFechainternacional);
                    sFechaAlta = cambiaformatofecha(sFechaAlta);
                    sFechaPublicacion = cambiaformatofecha(sFechaPublicacion);
                    sFechaPresentacion = cambiaformatofecha(sFechaPresentacion);
                    //sFechaOposicion = cambiaformatofecha(sFechaOposicion);
                    String sAnioPres = "2022";
                    try {
                        sAnioPres = sFechaAlta.Substring(0, 4);
                    }
                    catch (Exception exs) {
                        new filelog("896", ""+exs.Message);
                    }
                    


                    //Obtenemos el Folio denominado "Caso Numero"
                    conect con_1 = new conect();
                    String sGetcasonumero = "select casoid, casonumero from caso_oposicion order by casoid desc limit 1;";
                    MySqlDataReader respuestastringcasonum = con_1.getdatareader(sGetcasonumero);
                    String sCasonumero = "";
                    while (respuestastringcasonum.Read())
                    {
                        sCasonumero = validareader("casonumero", "casoid", respuestastringcasonum).Text.ToString();
                    }
                    respuestastringcasonum.Close();
                    con_1.Cerrarconexion();
                    var result = "";// sCasonumero.Substring(0, sCasonumero.LastIndexOf('-'));
                    var resultextencion = "OP";// sCasonumero.Substring(sCasonumero.LastIndexOf('-') + 1);
                    int iValor = 1000;// Int32.Parse(result) + 1;
                    if (sCasonumero=="") {
                        sCasonumero = iValor + "-" + resultextencion;
                    }
                    //sCasonumero = iValor + "-" + resultextencion;
                    try {

                        result = sCasonumero.Substring(0, sCasonumero.LastIndexOf('-'));
                        resultextencion = sCasonumero.Substring(sCasonumero.LastIndexOf('-') + 1);
                        iValor = Int32.Parse(result) + 1;

                        sCasonumero = iValor + "-" + resultextencion;
                    }
                    catch (Exception exs) {
                        new filelog("al calcular el número consegutivo de caso: 924 ", ""+exs.Message);
                    }
                    
                    String sEstatudID = "1";
                    conect con_2 = new conect();
                    String insertcontencioso = " INSERT INTO `caso_oposicion` " +
                                                " (`CasoId`, " +
                                                " `TipoSolicitudId`, " +
                                                " `CasoTituloespanol`, " +
                                                " `CasoTituloingles`, " +
                                                " `CasoFechaPresentacion`, " +
                                                " `CasoNumeroExpedienteLargo`, " +
                                                " `CasoNumero`, " +
                                                " `ResponsableId`, " +
                                                " `CasoFechaAlta`, " +
                                                " `CasoTipoCaptura`, " +
                                                " `CasoTitular`, " +
                                                " `CasoFechaFilingSistema`, " +
                                                " `CasoFechaFilingCliente`, " +
                                                " `CasoFechaCartaCliente`, " +
                                                " `EstatusCasoId`, " +
                                                " `UsuarioId`, " +
                                                " `PaisId`, " +
                                                " `CasoComentario`, " +
                                                " `Act`, " +
                                                " `CasoIdOriginal`, " +
                                                " `TipoSolicitudIdOriginal`, " +
                                                " `TipoSolicitudIdRival`, " +
                                                " `MarcaImitadora`, " +
                                                " `NombreImitador`, " +
                                                " `FecPublicacionImitadora`, " +
                                                " `fechaplazovenceopocision`, " +
                                                //" `FecPresentacionImitadora`, " +
                                                " `FecPresentacionOpocision`, " +
                                                " `FecPublicacionOposicion`, " +
                                                " `Observaciones`, " +
                                                " `Clase`, " +
                                                " `TipoMarcaId`, " +
                                                " `CasoFechaRecepcion`) " +
                                                " VALUES " +
                                                " (null, " +
                                                " 14, " + //No se usa el subtipo de solicitud// Se cambio por el valor del combo de Datos del contrario
                                                
                                                //" '" + Id_Marca + "', " +
                                                " 0, " + //Titulo Español
                                                " '', " + //Titulo inglés
                                                " '" + sAnioPres + "', " +
                                                " '" + sExpedienteImitador + "', " +
                                                " '" + sCasonumero + "', " +
                                                " '" + sComboBoxResponsable + "', " + //Responsable
                                                " '" + sFechaAlta + "', " +
                                                " 2, " + //Tipo de Captura
                                                " '" + sInteresadoOriginal + "', " + //Titular o Interesado
                                                " 0, " +
                                                " 0, " +
                                                " 0, " +
                                                " '" + sEstatudID + "', " +
                                                " '" + sresponsableid + "', " + //
                                                " '" + sCcomboBoxPais + "', " + //PaisId
                                                " '" + sComentario + "', " +
                                                " 0, " + //Act
                                                " '" + sCasoIdOriginal + "', " + // Caso Id Original
                                                 " '" + sgTipoSolIdOriginal + "', " + //Tipo Sol Original
                                                 " '" + sComboBoxTipoMarcaRival + "', " + //Tipo Sol rival
                                                 
                                                " '" + sMarcaImitadora + "', " +
                                                " '" + sNombreImitador + "', " +
                                                " '" + sFechaPublicacion + "', " +
                                                " '" + sFechaPresentacion + "', " +
                                                //" '" + sFechaOposicion + "', " +
                                                " 0, " +
                                                " 0, " +
                                                " '', " +
                                                " '" + sComboBoxClase + "', " +
                                                " '" + sComboBoxTipoMarca + "', " +
                                                " '" + sFechaAlta + "'); ";

                    MySqlDataReader respuestastring = con_2.getdatareader(insertcontencioso);
                    respuestastring.Close();
                    con_2.Cerrarconexion();



                    //Obtiene el nuevo id del caso
                    conect con_3 = new conect();
                    String sGetid = "SELECT * FROM `caso_oposicion` order by CasoId desc limit 1";
                    MySqlDataReader respuestastringid = con_3.getdatareader(sGetid);
                    String sCasoidOposicion = "";
                    while (respuestastringid.Read())
                    {
                        sCasoidOposicion = validareader("CasoId", "CasoId", respuestastringid).Value.ToString();
                        bCaso = true;
                    }
                    respuestastringid.Close();
                    con_3.Cerrarconexion();



                    conect con_4 = new conect();

                    String insertacliente = "INSERT INTO `casocliente` " +
                                            "(`CasoClienteId`, " +
                                            "`ClienteId`, " +
                                            "`contactoid`, " +
                                            "`CasoId`, " +
                                            "`TipoSolicitudId`, " +
                                            "`CasoClienteIndCorres`) " +
                                            "VALUES " +
                                            "(null, " +
                                            sComboBoxClientes + ", " +
                                            sComboBoxContacto + ", " +
                                            sCasoidOposicion + ", " +
                                            sComboTiposolicitud + ", " +
                                            "0);";
                    MySqlDataReader respuestastringinsertclient = con_4.getdatareader(insertacliente);
                    if (respuestastringinsertclient.RecordsAffected == 1)
                    {
                        bCasocliente = true;
                    }
                    respuestastringinsertclient.Close();
                    con_4.Cerrarconexion();



                    conect con_5 = new conect();
                    String insertcasointeresado = "INSERT INTO `casointeresado` " +
                                                "(`CasoInteresadoId`, " +
                                                "`InteresadoId`, " +
                                                "`CasoId`, " +
                                                "`TipoSolicitudId`, " +
                                                "`CasoInteresadoSecuencia`, " +
                                                "`TipoRelacionId`, " +
                                                "`DireccionId`) " +
                                                "VALUES " +
                                                "( " +
                                                "null, " +
                                                //sComboBoxInteresado + ", " +
                                                gSInteresadoIdOriginal + ", " +
                                                sCasoidOposicion + ", " +
                                                sComboTiposolicitud + ", " +
                                                "1," +
                                                "1," +
                                                "null);";
                    MySqlDataReader respuestastringinscasoint = con_5.getdatareader(insertcasointeresado);
                    if (respuestastringinscasoint.RecordsAffected == 1)
                    {
                        bcasointeresado = true;
                    }
                    respuestastringinscasoint.Close();
                    con_5.Cerrarconexion();

                    conect con_6 = new conect();
                    String insertreferencia = " INSERT INTO `referencia` " +
                                                " (`ReferenciaId`, " +
                                                " `CasoId`, " +
                                                " `TipoSolicitudId`, " +
                                                " `TipoReferenciaId`, " +
                                                " `ReferenciaNombre`) " +
                                                " VALUES (" +
                                                " null, " +
                                                sCasoidOposicion + ", " +
                                                sComboTiposolicitud + ", " +
                                                "1, " +
                                                "'" + tbReferenciaImitadora.Text + "'); ";
                    MySqlDataReader respinsertreferencia = con_6.getdatareader(insertreferencia);
                    if (respinsertreferencia.RecordsAffected == 1)
                    {
                        breferencias = true;
                    }
                    respinsertreferencia.Close();
                    con_6.Cerrarconexion();




                    if (bCaso && bCasocliente && bcasointeresado && breferencias)
                    //if (bCaso)
                    {
                        MessageBox.Show("El caso se agrego correctamente con el CasoNúmero: " + sCasonumero);
                        //DialogResult results = MessageBox.Show("¿Desea agregar un caso nuevo del mismo tipo?", "Agregar Caso Oposición", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        //if (results == DialogResult.Yes)
                        //{
                        //    //CasoNuevoContencioso nuevocasothis = new CasoNuevoContencioso(iGrupotipo, captura, fLoguin);
                            
                        //    CasoNuevoContencioso nuevocasothis = new CasoNuevoContencioso(5, captura, fLoguin);
                            
                        //    nuevocasothis.Show();
                        //    this.Close();
                        //}
                        //else if (results == DialogResult.No)
                        //{
                        fToposiciones objoposicion = new fToposiciones(fLoguin, captura, sCasoidOposicion);
                        objoposicion.Show();
                        captura.Show();
                        this.Close();
                        //}
                    }

                }
                else
                {
                    MessageBox.Show("Debe llenar los campos obligarorios para caso");
                }

            }
            catch (Exception E)
            {
                MessageBox.Show("Verifique que todos los campos estén correctos. " + E);
                new filelog("", ""+E.Message);
            }
            //20220314FSV Fin alta nuevo caso consulta
        }

        private void comboBoxPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int iValuepais = Convert.ToInt32((cbCvpais.SelectedItem as ComboboxItem).Value.ToString());
                comboBoxPais.Text = paises[iValuepais];
            }
            catch (Exception exs)
            {
                new filelog(" linea 1201 ", " : " + exs.StackTrace);
            }
            //paises[Convert.ToInt32((comboBoxPais.SelectedItem as ComboboxItem).Value.ToString())];
        }


        private void tbDFechaAlta_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbDFechaAlta);
        }

        private void tbDFecPubImitadora_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbDFecPubImitadora);
        }

        private void tbDFechaAlta_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacamposfecha(tbDFechaAlta, e);
        }

        public void validacamposfecha(TextBox tbElement, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsDigit(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsSeparator(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }


                if (tbElement.Text.Length == 2)
                {
                    tbElement.Text = tbElement.Text + "-";
                    tbElement.SelectionStart = tbElement.Text.Length;

                }
                if (tbElement.Text.Length == 5)
                {
                    tbElement.Text = tbElement.Text + "-";
                    tbElement.SelectionStart = tbElement.Text.Length;
                }
            }
            catch (Exception Ex)
            {
                new filelog("validavaloresfecha", Ex.Message);
            }
        }

        private void tbDFechaAlta_Leave(object sender, EventArgs e)
        {
            tbDFechaAlta.Text = tbDFechaAlta.Text.Replace("/", "-").Replace(".", "-");
        }

        private void tbDFecPubImitadora_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacamposfecha(tbDFecPubImitadora, e);
        }

        private void tbDFecPubImitadora_Leave(object sender, EventArgs e)
        {
            
            if(tbDFecPubImitadora.Text != "")
            {
                tbDFecPubImitadora.Text = tbDFecPubImitadora.Text.Replace("/", "-").Replace(".", "-");
                String sFecPubOposicion = tbDFecPubImitadora.Text;
                try 
                { 
                    DateTime dtFecPubOposicion = Convert.ToDateTime(sFecPubOposicion);         
                    DateTime dtFecPresentacion = dtFecPubOposicion.AddMonths(1);
                    String sFecPubliccacion = dtFecPresentacion.ToString("dd-MM-yyyy");
                    tbDFecPlazoPresImitadora.Text = sFecPubliccacion;
                }
                catch(FormatException)
                {
                    MessageBox.Show("Imposible Convertir dato " + sFecPubOposicion + " en Fecha", "Caso Oposición");
                }
            }

        }

        /*private void tbClavepaiscaso_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int iValoractual = 0;
                for (int x = 0; x < paises.Length; x++)
                {
                    if (paises[x] == tbClavepaiscaso.Text)
                    {
                        iValoractual = x;
                    }
                }
                comboBoxPais.Text = paisesclave[iValoractual];
            }
            catch (Exception Ex)
            {
                new filelog("casnuevo", Ex.Message);
            }
        }*/

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void comboTipomarca1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*Si es marca muestra los tipode de marca  de lo contrario debe estar vacío */
            try {
                if (comboTipomarca1.SelectedItem != null) {
                    if ((comboTipomarca1.SelectedItem as ComboboxItem).Value.ToString() == "7")
                    {
                        cbDTipomarca.Items.Clear();
                        conect con_tipomarcas = new conect();
                        String sQryTipoMarcas = "select TipoMarcaId, TipoMarcaDescrip from tipomarca where TipoMarcaIndAct = 1 order by TipoMarcaDescrip;";
                        MySqlDataReader respuestastringtdm = con_tipomarcas.getdatareader(sQryTipoMarcas);
                        while (respuestastringtdm.Read())
                        {
                            cbDTipomarca.Items.Add(validareader("TipoMarcaDescrip", "TipoMarcaId", respuestastringtdm));
                        }
                        respuestastringtdm.Close();
                        con_tipomarcas.Cerrarconexion();
                        cbDTipomarca.Visible = true;
                        label42.Visible = true;
                    }
                    else {
                        cbDTipomarca.Visible = false;
                        label42.Visible = false;
                        cbDTipomarca.Items.Clear();
                    }
                }


                
            }
            catch (Exception exs) {
                new filelog("", ""+exs.Message);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacamposfecha(textBox1, e);
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(textBox1);
        }
    }
}
