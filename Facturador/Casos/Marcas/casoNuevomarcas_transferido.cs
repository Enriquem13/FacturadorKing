
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Facturador.Casos.Marcas
{
    public partial class casoNuevomarcas_transferido : Form
    {
        public Form1 login;
        public captura cFcaptura;
        public String[] paises = new String[250];
        public String[] paisesclave = new String[250];
        public int sTiposolicitudiscaso;
        public int TipoSol;
        funcionesdicss objfuncionesdicss;
        public String sEstatusid  = "";
        public casoNuevomarcas_transferido(int iTiposolicitud, captura cap, Form1 loguinparam)
        {
            try
            {
                login = loguinparam;
                cFcaptura = cap;
                TipoSol = 7;
                sTiposolicitudiscaso = iTiposolicitud;
                InitializeComponent();
                objfuncionesdicss = new funcionesdicss();
                ComboboxItem item = new ComboboxItem();
                item.Text = login.sUsername;
                item.Value = login.sId;

                groupPrioridades.Enabled = false;
                groupProtocolo.Enabled = false;
                /*comboBoxResponsable.Items.Add(item);
                comboBoxResponsable.Text = login.sUsername;*/

                //select * from tiposolicitud
                conect conect_1 = new conect();

                String sQresponsable = "select UsuarioNombre, UsuarioId from usuario where UsuarioIndActivo = 1;";
                MySqlDataReader respuresponsable = conect_1.getdatareader(sQresponsable);

                while (respuresponsable.Read())
                {
                    comboBoxResponsable.Items.Add(validareader("UsuarioNombre", "UsuarioId", respuresponsable));
                }
                comboBoxResponsable.Text = login.sUsername;
                comboBoxResponsable.SelectedValue = login.sId;
                respuresponsable.Close();
                conect_1.Cerrarconexion();


                conect conect_2 = new conect();
                String query = "select TipoSolicitudDescrip, TipoSolicitudDescrip, TipoSolicitudId from tiposolicitud WHERE TipoSolicitudGrupo = " + iTiposolicitud;
                MySqlDataReader respuestastring = conect_2.getdatareader(query);
                while (respuestastring.Read())
                {
                    comboTiposolicitud.Items.Add(validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestastring));
                }
                respuestastring.Close();
                conect_2.Cerrarconexion();


                DateTime Hoy = DateTime.Today;
                string fecha_actual = Hoy.ToString("dd-MM-yyyy");
                TexboxFecha.Text = fecha_actual;

                //Agregamos al cliente
                conect conect_3 = new conect();
                String query2 = "select NombreUtilClient, ClienteId from cliente order by cliente.NombreUtilClient;";
                MySqlDataReader respuestastringclient = conect_3.getdatareader(query2);
                while (respuestastringclient.Read())
                {
                    comboBoxClientes.Items.Add(validareader("NombreUtilClient", "ClienteId", respuestastringclient));
                }
                respuestastringclient.Close();
                conect_3.Cerrarconexion();

                //Agregamos los idiomas
                conect conect_4 = new conect();
                String query3 = "select IdiomaId, IdiomaDescripcion from idioma;";
                MySqlDataReader respuestastringidiom = conect_4.getdatareader(query3);
                while (respuestastringidiom.Read())
                {
                    comboBoxIdioma.Items.Add(validareader("IdiomaDescripcion", "IdiomaId", respuestastringidiom));
                }
                respuestastringidiom.Close();
                conect_4.Cerrarconexion();
                //
                conect con_tres = new conect();
                String sQueryclases = "SELECT * FROM `clasificadornizavigente` ORDER BY CasoProductosClase ASC";
                MySqlDataReader respuestastringclases = con_tres.getdatareader(sQueryclases);
                while (respuestastringclases.Read())
                {
                    comboBoxClase.Items.Add(validareader("CasoProductosClase", "CasoProductosClase", respuestastringclases));
                }
                respuestastringclases.Close();
                con_tres.Cerrarconexion();

                //
                //select InteresadoID, InteresadoNombre from interesado;
                conect conect_5 = new conect();
                String query4 = "select InteresadoID, InteresadoNombre from interesado order by InteresadoNombre;";
                MySqlDataReader respuestastringointeresado = conect_5.getdatareader(query4);
                while (respuestastringointeresado.Read())
                {
                    comboBoxInteresado.Items.Add(validareader("InteresadoNombre", "InteresadoID", respuestastringointeresado));
                }
                respuestastringointeresado.Close();
                conect_5.Cerrarconexion();

                //select * from pais;
                //conect conect_6 = new conect();
                //String query5 = "select PaisNombre, PaisId, PaisClave from pais;";
                //MySqlDataReader respuestastringopais = conect_6.getdatareader(query5);
                ////int paisint = 0;
                //while (respuestastringopais.Read())
                //{
                //    comboBoxPais.Items.Add(validareader("PaisNombre", "PaisId", respuestastringopais));
                //    paises[Convert.ToInt32(validareader("PaisClave", "PaisId", respuestastringopais).Value.ToString())] = validareader("PaisClave", "PaisId", respuestastringopais).Text;
                //    paisesclave[Convert.ToInt32(validareader("PaisClave", "PaisId", respuestastringopais).Value.ToString())] = validareader("PaisNombre", "PaisId", respuestastringopais).Text;
                //    //paisint++;
                //}
                //respuestastringopais.Close();
                //conect_6.Cerrarconexion();



                //Consultamos los paises
                conect conect_pais = new conect();
                String query5 = "select PaisNombre, PaisId, PaisClave from pais order by PaisNombre;";
                MySqlDataReader respuestastringopais = conect_pais.getdatareader(query5);
                //int paisint = 0;
                while (respuestastringopais.Read())
                {
                    comboBoxPais.Items.Add(validareader("PaisNombre", "PaisId", respuestastringopais));
                    cbPaiscaso.Items.Add(validareader("PaisNombre", "PaisId", respuestastringopais));
                    cbCvpais.Items.Add(validareader("PaisClave", "PaisId", respuestastringopais));
                    paises[Convert.ToInt32(validareader("PaisClave", "PaisId", respuestastringopais).Value.ToString())] = validareader("PaisClave", "PaisId", respuestastringopais).Text;
                    paisesclave[Convert.ToInt32(validareader("PaisClave", "PaisId", respuestastringopais).Value.ToString())] = validareader("PaisNombre", "PaisId", respuestastringopais).Text;
                    //paisint++;
                }
                respuestastringopais.Close();
                conect_pais.Cerrarconexion();


                //Firma
                conect conect_7 = new conect();
                String sResponsablequery = "select ResponsableClave, ResponsableId, ResponsableNombre from responsable;";
                MySqlDataReader respuestastrinresponsable = conect_7.getdatareader(sResponsablequery);
                //int paisint = 0;
                while (respuestastrinresponsable.Read())
                {
                    comboBoxFirma.Items.Add(validareader("ResponsableNombre", "ResponsableId", respuestastrinresponsable));
                    comboBoxFirma.Text = validareader("ResponsableNombre", "ResponsableId", respuestastrinresponsable).Text;
                    //paisint++;
                }
                respuestastrinresponsable.Close();
                conect_7.Cerrarconexion();
                //comboBoxFirma.

                //Seleccionamos el país por Default
                cbPaiscaso.Text = "MEXICO";
            }
            catch (Exception E)
            {
                new filelog(login.sId, E.ToString());
            }
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
        public String validacombobox(ComboBox combo)
        {
            if (combo.SelectedItem != null)
            {
                return (combo.SelectedItem as ComboboxItem).Value.ToString();
            }
            else
            {
                return "";
            }
        }

        public void creamulticaso()
        {
            try
            {
                if (cbMulticaso.Text != "")
                {


                    for (int x = 0; x < int.Parse(cbMulticaso.Text); x++)
                    {
                        //MessageBox.Show("mensaje consecutivo" + x);
                        creacasomarcas();
                    }
                    //}
                    //else {
                    //    cbMulticaso.Focus();
                    //}
                }
            }
            catch (Exception exs)
            {
                new filelog("error linea 204", exs.StackTrace);
            }
        }
        public String sNumerosdecaso = "";
        public void creacasomarcas()
        {
            Boolean bCaso = false, bCasocliente = false, bcasointeresado = false, breferencias = false, bPrioridades = false;
            try
            {
                //hay que validar cuales campos son obligatorios y cuals son opcionales

                String sComboTiposolicitud = validacombobox(comboTiposolicitud);//*
                //String sComboTiposolicitud = (comboTiposolicitud.SelectedItem as ComboboxItem).Value.ToString();//*

                //String sComboboxSubtipo = validacombobox(comboboxSubtipo);//.SelectedItem as ComboboxItem).Value.ToString();//*
                String sID_Ley = validacombobox(ID_Ley);

                //String sComboboxSubtipo2 = validacombobox(comboboxSubtipo2);//.SelectedItem as ComboboxItem).Value.ToString();//*
                //String sComboboxFecha = (comboboxFecha.SelectedItem as ComboboxItem).Value.ToString();//


                String sComboBoxClientes = validacombobox(comboBoxClientes);//.SelectedItem as ComboboxItem).Value.ToString();//casocliente
                String sComboBoxContacto = validacombobox(comboBoxContacto);//.SelectedItem as ComboboxItem).Value.ToString();//casocliente
                String sComboBoxInteresado = validacombobox(comboBoxInteresado);//.SelectedItem as ComboboxItem).Value.ToString();//casointeresado
                String sComboBoxClase = validacombobox(comboBoxClase);//pendiente
                String sComboBoxIdioma = validacombobox(comboBoxIdioma);//.SelectedItem as ComboboxItem).Value.ToString();//*
                String sCcomboBoxFirma = validacombobox(comboBoxFirma);//.SelectedItem as ComboboxItem).Value.ToString();//pendiente
                String sComboBoxResponsable = validacombobox(comboBoxResponsable);
                String Id_Marca = validacombobox(comboTipomarca1);//.SelectedItem as ComboboxItem).Value.ToString();//*


                //Dar formatos de fecha 
                String sTextboxFecha = TexboxFecha.Text;//Fecha carta   CasoFechaCartaCliente *
                String stextClientduedate = textClientduedate.Text;//CasoFechaFilingCliente client due date *

                String Fecharegistrointernacional = tbFecharegistrointernacional.Text;
                String numregistrointernacional = tbNumregistrointernacional.Text;

                String stextBoxPlazolegal = textBoxPlazolegal.Text;//textBoxPlazolegalcasofechalegal  CasoFechaLegal
                String stextBoxFechainternacional = "";// textBoxFechainternacional.Text;//CasoFechaInternacional fecha internacional
                String stextBoxReferencia = textBoxReferencia.Text;//pendiente
                String stbNumeroregistro = tbNumeroregistro.Text; //Numero de Registro

                String srichTextBoxTitulo = richTextBoxTitulo.Text;//Casotitulo
                String sresponsableid = login.sId;
                String sCasodenominacion = "";
                /*if (sComboBoxIdioma != "2")
                {
                    sCasodenominacion = srichTextBoxTitulo;
                    srichTextBoxTitulo = "";
                }*/
                if (textClientduedate.Text == "")
                {
                    MessageBox.Show("El campo de la Fecha Client due date no puede estar vacia.");
                    return;
                }

                if (comboBoxIdioma.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un idioma para el caso");
                    return;
                }
                //validamos los campos que son obligatorios para poder agregar el caso
                //validamos fecha, titulo, tiposolicitud. cliente, contacto, interesado
                if (sTextboxFecha != "" && srichTextBoxTitulo != "" && sComboTiposolicitud != "" && sComboBoxClientes != "" && sComboBoxInteresado != "" && sEstatusid!="" && comboBoxContacto.SelectedItem !=null)
                {
                    sTextboxFecha = cambiaformatofecha(sTextboxFecha);
                    stextClientduedate = cambiaformatofecha(stextClientduedate);
                    Fecharegistrointernacional = cambiaformatofecha(Fecharegistrointernacional);
                    stextBoxPlazolegal = cambiaformatofecha(stextBoxPlazolegal);
                    stextBoxFechainternacional = cambiaformatofecha(stextBoxFechainternacional);

                    String stbFechalegal = cambiaformatofecha(tbFechalegal.Text);
                    String stextBox1 = cambiaformatofecha(textBox1.Text);

                    //String stbFecharecepcion = cambiaformatofecha(tbFecharecepcion.Text);
                    String stbFechaconsecion = cambiaformatofecha(tbFechaconsecion.Text);
                    String stbFechaprobouso = cambiaformatofecha(tbFechaprobouso.Text);
                    String stbFechaInicioUso = cambiaformatofecha(tbFechaInicioUso.Text);
                    String stbFechavigencia = cambiaformatofecha(tbFechavigencia.Text);
                    if (Fecharegistrointernacional == "")
                    {
                        Fecharegistrointernacional = "0000-00-00";
                    }

                    conect con_1 = new conect();
                    String sGetcasonumero = "select casoid, casonumero from caso_marcas order by casoid desc limit 1;";
                    MySqlDataReader respuestastringcasonum = con_1.getdatareader(sGetcasonumero);
                    String sCasonumero = "";
                    while (respuestastringcasonum.Read())
                    {
                        sCasonumero = validareader("casonumero", "casoid", respuestastringcasonum).Text.ToString();
                    }
                    respuestastringcasonum.Close();
                    con_1.Cerrarconexion();

                    var result = sCasonumero.Substring(0, sCasonumero.LastIndexOf('-'));
                    //MessageBox.Show(result);
                    int iValor = Int32.Parse(result) + 1;
                    sCasonumero = iValor + "-TM";

                    //hasta aquí analizamos el Caso Numero
                    //if (sCasonumero.Length == 7)
                    //{
                    //    String extencion = sCasonumero.Substring(4, 3);
                    //    sCasonumero = sCasonumero.Substring(0, 4);
                    //    int iValor = Int32.Parse(sCasonumero) + 1;
                    //    sCasonumero = iValor + extencion;
                    //}
                    //else
                    //{
                    //    if (sCasonumero.Length == 4)
                    //    {
                    //        int iValor = Int32.Parse(sCasonumero.Substring(0,8)) + 1;
                    //        sCasonumero = iValor + "";
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("El último caso de éste tipo es: " + sCasonumero + " y no se reconoce el formato");
                    //    }
                    //}
                    String sEstatudID = sEstatusid; // por default EstatuscasoID
                    int iIdpaiscaso = 0;
                    for (int yuno = 0; yuno < paises.Length; yuno++)
                    {
                        if (paises[yuno] == tbClavepaiscaso.Text)
                        {
                            iIdpaiscaso = yuno;

                        }
                    }
                    if (stbFechalegal == "")
                    {
                        stbFechalegal = "0000-00-00";
                    }
                    if (stextBox1 == "")
                    {
                        stextBox1 = "0000-00-00";
                    }
                    //if (stbFecharecepcion == "")
                    //{
                    //stbFecharecepcion = "0000-00-00";
                    //}

                    if (stbFechaconsecion == "")
                    {
                        stbFechaconsecion = "0000-00-00";
                    }
                    if (Id_Marca == "")
                    {
                        Id_Marca = "0";
                    }
                    if (stbFechaprobouso == "")
                    {
                        stbFechaprobouso = "0000-00-00";
                    }

                    if (stbFechaInicioUso == "")
                    {
                        stbFechaInicioUso = "0000-00-00";
                    }

                    if (stbFechavigencia == "")
                    {
                        stbFechavigencia = "0000-00-00";
                    }

                    //inserta caso_ marcas
                    String TipoCaptura = "2";
                    conect con_2 = new conect();
                    String insert_marcas = "INSERT INTO `caso_marcas` " +
                                             "(`CasoId`, " +
                                             "`TipoSolicitudId`, " +//Marca nombre,Aviso comercial
                                             //"`SubTipoSolicitudId`, " +//
                                             "`CasoTituloespanol`, " +//campo vacio
                                             "`CasoTituloingles`, " +//Nombre de la marca
                                             "`IdiomaId`, " +
                                             "`CasoNumero`, " +//Caso expediente interno
                                             "`ResponsableId`, " +

                                             "`numregistrointernacional`, " +//campos nuevos
                                             "`Fecharegistrointernacional`, " +//campos nuevos
                                             "`CasoFechaAlta`, " +
                                             "`CasoFechaCartaCliente`, " +

                                             "`CasoFechaFilingSistema`, " +
                                             "`CasoFechaFilingCliente`, " +
                                             "`EstatusCasoId`, " +
                                             "`UsuarioId`, " +

                                             "`CasoFechaLegal`, " +//caso fecha recepcion o presentacion
                                             "`Fecha_Vigencia_Internacional`, " +
                                             "`TipoMarcaId`," +//Subtipos de Marca Ejem: Nominativa Diseño,Tridimensional, Olfatoria
                                             "`CasoFechaRecepcion`, " +//Pendiente
                                             "`CasoFechaConcesion`, " +
                                             "`CasoFechaprobouso`, " +//Fecha en que se presento la ultima declaracion d uso estricta
                                             "`CasoFechainiciouso`, " +// Fecha en que el titular utilizo por primera ves la marca aunque no estuviera registrada
                                             "`CasoFechaVigencia`, " +//
                                             "`CasoNumeroExpedienteLargo`, " +//expediente
                                             "`CasoNumConcedida`, " +//Num registros
                                             "`CasoFechaDeclaUso`, " +
                                             "`Id_Ley   `, " +
                                                     "`CasoTipoCaptura`," +
                                             "`PaisId`) " +
                                     
                                             "VALUES " +
                                             "(NULL, " +
                                             "'" + sComboTiposolicitud + "', " +
                                             //"'" + sComboboxSubtipo + "', " +
                                             "'" + srichTextBoxTitulo + "', " +
                                             "'" + srichTextBoxTitulo + "', " +
                                             "'" + sComboBoxIdioma + "', " +

                                             "'" + sCasonumero + "', " +
                                             "'" + sresponsableid + "', " +

                                             "'" + numregistrointernacional + "', " +//numero registro internacional
                                             "'" + Fecharegistrointernacional + "', " +//fecha nuero internacional

                                             "'" + sTextboxFecha + "', " +
                                             "'" + sTextboxFecha + "', " +

                                             "'" + sTextboxFecha + "', " +
                                             "'" + stextClientduedate + "', " +

                                             "'" + sEstatudID + "', " +
                                             "'" + sresponsableid + "', " +
                                             "'" + stbFechalegal + "', " +
                                             "'" + stextBox1 + "', " +
                                             "'" + Id_Marca + "', " +
                                             "'" + stbFechalegal + "', " +
                                             "'" + stbFechaconsecion + "', " +
                                             "'" + stbFechaprobouso + "', " +
                                             "'" + stbFechaInicioUso + "', " +
                                             "'" + stbFechavigencia + "', " +
                                             "'" + tbExpediente.Text + "', " +
                                             "'" + tbNumeroregistro.Text + "', " + 
                                              "'" + stbFechaprobouso + "', " +
                                             "'" + sID_Ley + "', " +
                                             "'" + TipoCaptura + "', " +
                                             "'" + iIdpaiscaso + "'); ";

                    String sGetid = "SELECT * FROM `caso_marcas` order by CasoId desc limit 1";
                    MySqlDataReader respuestastring = con_2.getdatareader(insert_marcas);
                    respuestastring.Close();
                    con_2.Cerrarconexion();

                    conect con_3 = new conect();
                    MySqlDataReader respuestastringid = con_3.getdatareader(sGetid);
                    String sCasoid = "";
                    while (respuestastringid.Read())
                    {
                        sCasoid = validareader("CasoId", "CasoId", respuestastringid).Value.ToString();
                        //MessageBox.Show("El caso se agrego correctamente con CasoID: " + sCasoid + " Casonumero: " + sCasonumero);
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
                                            "(NULL, " +
                                            sComboBoxClientes + ", " +
                                            sComboBoxContacto + ", " +
                                            sCasoid + ", " +
                                            sComboTiposolicitud + ", " +
                                            "0);";
                    MySqlDataReader respuestastringinsertclient = con_4.getdatareader(insertacliente);
                    if (respuestastringinsertclient.RecordsAffected == 1)
                    {
                        bCasocliente = true;
                        //MessageBox.Show("Se inserto en casoclientes");
                    }
                    respuestastringinsertclient.Close();
                    con_4.Cerrarconexion();
                    //casoproductos
  

                    //
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
                                                "NULL, " +
                                                sComboBoxInteresado + ", " +
                                                sCasoid + ", " +
                                                sComboTiposolicitud + ", " +
                                                "1," +
                                                "1," +
                                                "null);";
                    MySqlDataReader respuestastringinscasoint = con_5.getdatareader(insertcasointeresado);
                    if (respuestastringinscasoint.RecordsAffected == 1)
                    {
                        //MessageBox.Show("Se inserto en casointeresado");
                        bcasointeresado = true;
                    }
                    respuestastringinscasoint.Close();
                    con_5.Cerrarconexion();
                    //agregar a¿rowaffected y validar que se inserto
                    if (stextBoxReferencia.Trim()!="") {
                        conect con_6 = new conect();
                        String insertreferencia = " INSERT INTO `referencia` " +
                                                    " (`ReferenciaId`, " +
                                                    " `CasoId`, " +
                                                    " `TipoSolicitudId`, " +
                                                    " `TipoReferenciaId`, " +
                                                    " `ReferenciaNombre`) " +
                                                    " VALUES (" +
                                                    " NULL, " +
                                                    sCasoid + ", " +
                                                    sComboTiposolicitud + ", " +
                                                    "1, " +
                                                    "'" + stextBoxReferencia + "'); ";
                        MySqlDataReader respinsertreferencia = con_6.getdatareader(insertreferencia);
                        if (respinsertreferencia.RecordsAffected == 1)
                        {
                            //MessageBox.Show("Se inserto en referencia");
                            breferencias = true;
                        }
                        respinsertreferencia.Close();
                        con_6.Cerrarconexion();
                    }
                    String casoProducto = "DEFAULT";
                    conect con_13 = new conect();
                  //String ConsultaProductos
                    String sConsultaplazos = " INSERT INTO `casoproductos` " +
                         " (`CasoProductosId`, " +
                         " `CasoProductosDescripcion`, " +
                         " `CasoProductosClase`, " +
                         " `CasoId`, " +
                         " `TipoSolicitudId`) " +
                         " VALUES " +
                         " (null , " +
                         " '" + casoProducto + "' , " +
                         " '" + sComboBoxClase + "' , " +
                         " '" + sCasoid + "' , " +
                         " '" + sComboTiposolicitud + "' ); ";
                    MySqlDataReader respinsertreferencias = con_13.getdatareader(sConsultaplazos);
                    if (respinsertreferencias.RecordsAffected == 1)
                    {
                        //MessageBox.Show("Se inserto en referencia");
                        breferencias = true;
                    }
                    respinsertreferencias.Close();
                    con_13.Cerrarconexion();
                
                //Insertamos las prioridades capturadas al agregar un caso nuevo del grupo 1

                for (int z = 0; z < listViewPrioridades.Items.Count; z++)
                    {

                        //para validar que la fecha sea correcta
                        String sFechaprioridad = validafechacorecta(listViewPrioridades.Items[z].SubItems[1].Text);

                        //obtenemos el id del pais
                        int iIdpais = 0;
                        for (int y = 0; y < paisesclave.Length; y++)
                        {
                            if (paisesclave[y] == listViewPrioridades.Items[z].SubItems[2].Text)
                            {
                                iIdpais = y;
                            }
                        }
                        //obtenemos el id del tipo de prioridad
                        int iIdtipoprioridad = 0;

                        if ("PCT" == listViewPrioridades.Items[z].SubItems[3].Text) { iIdtipoprioridad = 1; }
                        else { iIdtipoprioridad = 2; }

                        conect conect_prio = new conect();
                        String sInsertprioridades = " INSERT INTO `prioridad`" +
                                                    " (`PrioridadId`," +
                                                    " `CasoId`," +
                                                    " `TipoSolicitudId`, " +
                                                    " `PrioridadNumero`," +
                                                    " `PrioridadFecha`," +
                                                    " `PaisID`," +
                                                    " `TipoPrioridadId`)" +
                                                    " VALUES" +
                                                    " (NULL," +
                                                    " " + sCasoid + ", " +
                                                    sComboTiposolicitud + ", " +
                                                    " '" + listViewPrioridades.Items[z].SubItems[0].Text + "'," +
                                                    " '" + sFechaprioridad + "', " +
                                                    " '" + iIdpais + "'," +
                                                    " '" + iIdtipoprioridad + "');";
                        MySqlDataReader resp_insertprioridades = conect_prio.getdatareader(sInsertprioridades);
                        if (resp_insertprioridades.RecordsAffected == 1)
                        {
                            bCasoprioridades = true;
                        }
                        resp_insertprioridades.Close();
                        conect_prio.Cerrarconexion();
                    }
                    if (listViewPrioridades.Items.Count == 0)
                    {
                        bCasoprioridades = true;
                    }
                    /*Aquí agregamos los plazos*/
                    /*
                    * Creamos el plazo en la tabla Plazo la relacion con casoid y tiposolicitudid 
                    */
                    String sPlazosid = "";
                    bool bBanderacreadetalleplazo = false;
                    conect conect_plazosid = new conect();
                    String sQuery_plazos = " INSERT INTO `plazos`(`Plazosid`,`CasoId`,`TipoSolicitudId`,`Fecha_creacion`)VALUES " +
                                                    " (NULL, " +
                                                    " '" + sCasoid + "', " +
                                                    " '" + sComboTiposolicitud + "', " +
                                                    " curdate()); ";

                    MySqlDataReader resp_plazos = conect_plazosid.getdatareader(sQuery_plazos);
                    if (resp_plazos.RecordsAffected == 1)
                    {
                        conect conect_plazosid_relacion = new conect();
                        String sQuery_plazos_relacion_general = "select * from plazos order by  plazosid desc limit 1;";
                        MySqlDataReader resp_plazos_relacion = conect_plazosid_relacion.getdatareader(sQuery_plazos_relacion_general);
                        while (resp_plazos_relacion.Read())
                        {
                            sPlazosid = validareader("Plazosid", "Plazosid", resp_plazos_relacion).Text;
                        }
                        resp_plazos_relacion.Close();
                        conect_plazosid_relacion.Cerrarconexion();

                        bBanderacreadetalleplazo = true;
                    }
                    resp_plazos.Close();
                    conect_plazosid.Cerrarconexion();

                    String sFechahoy = DateTime.Today.ToString("yyyy'/'MM'/'dd");
                    String sFechapresentacion = DateTime.ParseExact(textClientduedate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbDocumentofecharecepcion.Text
                    conect conect_plazoid = new conect();
                    String sQuery_plazo_general = " INSERT INTO `plazos_detalle` " +
                                                    " (`Plazos_detalleid`, " +
                                                    " `Plazosid`, " +//

                                                    " `usuario_creo_plazodetalle`, " +
                                                    " `Tipo_plazoid`, " +
                                                    " `Estatus_plazoid`, " +

                                                    " `Fecha_Vencimiento`) " +
                                                    " VALUES (NULL," +
                                                    " '" + sPlazosid + "', " +

                                                    " '" + login.sId + "', " +
                                                    " '2', " +
                                                    " '1', " +

                                                    " '" + sFechapresentacion + "');";

                    MySqlDataReader resp_plazo = conect_plazoid.getdatareader(sQuery_plazo_general);
                    if (resp_plazo.RecordsAffected == 1)
                    {
                        //ibBanderacreadetalleplazo_contador++;
                        //MessageBox.Show("Se creó un plazo.");
                        /*aqui validamos que se inserto el plazo*/
                    }
                    resp_plazo.Close();
                    conect_plazoid.Cerrarconexion();
                    /*Generamos el plazo de avisar al cliente de que se va presentar su solicitud*/
                    DateTime hoy = DateTime.Today;
                    DateTime hoymasundia = hoy.AddDays(1);
                    //Date dFechavigencia = sFechapresentacion
                    String sFechaVigencia = hoymasundia.ToString("yyyy-MM-dd");
                    conect conect_plazoid_instruccion = new conect();
                    String sQuery_plazo_instruccion = " INSERT INTO `plazos_detalle` " +
                                                    " (`Plazos_detalleid`, " +
                                                    " `Plazosid`, " +//

                                                    " `usuario_creo_plazodetalle`, " +
                                                    " `Tipo_plazoid`, " +
                                                    " `Estatus_plazoid`, " +

                                                    " `Fecha_Vencimiento`) " +
                                                    " VALUES (NULL," +
                                                    " '" + sPlazosid + "', " +

                                                    " '" + login.sId + "', " +
                                                    " '1', " +
                                                    " '1', " +

                                                    " '" + sFechaVigencia + "');";

                    //" '" + tipoplazoid_avisocliente + "', " +
                    //" '1', " +
                    //" '" + documentoid + "', " +
                    //" '" + login.sId + "');";

                    MySqlDataReader resp_plazo_instruccion = conect_plazoid_instruccion.getdatareader(sQuery_plazo_instruccion);
                    if (resp_plazo_instruccion.RecordsAffected == 1)
                    {
                        //ibBanderacreadetalleplazo_contador++;
                        //MessageBox.Show("Se creó un plazo.");
                        /*aqui validamos que se inserto el plazo*/
                    }
                    resp_plazo_instruccion.Close();
                    conect_plazoid_instruccion.Cerrarconexion();
                    /*FIN Generamos el plazo de avisar al cliente de que se va presentar su solicitud*/

                    /*Aquí Validaremos que se creé el plazo de envir a traducir*/

                    /*FIN Aquí Validaremos que se creé el plazo de envir a traducir*/

                    /*Fin de agregar plapzos*/



                    if (bCaso && bCasocliente && bcasointeresado &&  cbMulticaso.Text == "")
                    {
                        MessageBox.Show("El caso se agrego correctamente con el CasoNúmero: " + sCasonumero);
                        DialogResult results = MessageBox.Show("¿Desea agregar un caso nuevo del mismo tipo?", "Agregar Caso Marcas", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (results == DialogResult.Yes)
                        {
                            //code for Yes
                            Nuevocaso_marcas nuevocasothis = new Nuevocaso_marcas(sTiposolicitudiscaso, cFcaptura, login);
                            nuevocasothis.Show();
                            this.Close();
                        }
                        else if (results == DialogResult.No)
                        {
                            //code for No
                            cFcaptura.Show();
                            this.Close();
                        }//}
                        //else if (results == DialogResult.Cancel)
                        //{
                        //    //code for Cancel
                        //}
                    }
                    else
                    {
                        //aqui ya sabemos que se agregaron los casos multiples
                        sNumerosdecaso += " " + sCasonumero + " ";

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
                new filelog(login.sId, E.ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)//para insertar
        {

            var date1 = "05-11-2020";
            CultureInfo myCIintl = new CultureInfo("es-ES", false);
            try
            {
                {

                    // primero hacemos las validaciones y luego hacemos la pregunta de confirmación de multiples casos
                    if (textClientduedate.Text == "")
                    {
                        MessageBox.Show("El campo de la Fecha Client due date no puede estar vacia.");
                        return;
                    }
                    if (tbEstatus_header.Text == "")
                    {
                        MessageBox.Show("El campo estatus no de ir vacio");
                        return;
                    }
                    if (comboBoxClientes.SelectedItem == null)
                    {
                        MessageBox.Show("El campo cliente no debe ir vacio");
                        return;
                    }
                    if (comboBoxContacto.SelectedItem == null)
                    {
                        MessageBox.Show("El campo Contacto no debe ir vacio");
                        return;
                    }
                    if (richTextBoxTitulo.Text == "")
                    {
                        MessageBox.Show("El campo Marca no debe ir vacio");
                        return;
                    }
                    if (tbFechalegal.Text == tbFechaconsecion.Text)
                    {
                        MessageBox.Show("El campo fecha legal no puede ser igual a los otros campos");
                        return;
                    }
                    if (tbFechalegal.Text == "")
                    { 
                        MessageBox.Show("La Fecha Presentacion no puede ir vacia.");
                            return;
                    }
                    if (tbFechalegal.Text != "")
                    {

                        DateTime FechaSt = Convert.ToDateTime(date1, myCIintl);
                        DateTime Fechalegal = Convert.ToDateTime(tbFechalegal.Text, myCIintl);
                        if (Fechalegal < FechaSt && ID_Ley.Text == "Nueva Ley con prioridad")
                        {
                            MessageBox.Show("No puede tener Ley Nueva con prioridad con Fecha Presentacion Anterior 05-11-2020");
                            return;


                        }
                        var combo = ID_Ley.Text;
                        if (Fechalegal < FechaSt && ID_Ley.Text == "Nueva Ley") 
                        {
                            MessageBox.Show("No puede tener Ley Nueva con Fecha Presentacion Anterior 05-11-2020");
                            return;

                        }
                        if (Fechalegal > FechaSt && ID_Ley.Text == "Vieja Ley con prioridad")
                        {
                            MessageBox.Show("No puede tener Ley Vieja con prioridad con Fecha Presentacion posterior 05-11-2020");
                            return;


                        }
                        if (Fechalegal > FechaSt && ID_Ley.Text == "Vieja Ley")
                        {
                            MessageBox.Show("No puede tener Ley Vieja con Fecha Presentacion posterior 05-11-2020");
                            return;

                        }
                    }
                    ////
                    ///
                    /// 
                    /// 
                    if (textBoxFecha.Text != "")
                    {

                        DateTime FechaSt2 = Convert.ToDateTime(date1, myCIintl);
                        DateTime Fechalegal2 = Convert.ToDateTime(textBoxFecha.Text, myCIintl);
                        if (Fechalegal2 < FechaSt2 && ID_Ley.Text == "Nueva Ley con prioridad")
                        {
                            MessageBox.Show("No puede tener Ley Nueva con prioridad con Fecha Presentacion Anterior 05-11-2020");
                            return;


                        }
                        var combo = ID_Ley.Text;
                        if (Fechalegal2 < FechaSt2 && ID_Ley.Text == "Nueva Ley")
                        {
                            MessageBox.Show("No puede tener Ley Nueva con Fecha Presentacion Anterior 05-11-2020");
                            return;

                        }
                        if (Fechalegal2 > FechaSt2 && ID_Ley.Text == "Vieja Ley con prioridad")
                        {
                            MessageBox.Show("No puede tener Ley Vieja con prioridad con Fecha Presentacion posterior 05-11-2020");
                            return;


                        }
                        if (Fechalegal2 > FechaSt2 && ID_Ley.Text == "Vieja Ley")
                        {
                            MessageBox.Show("No puede tener Ley Vieja con Fecha Presentacion posterior 05-11-2020");
                            return;

                        }
                    }

                    //if (tbFechalegal.Text != "" && tbNumeroregistro.Text == "")
                    //{
                    //    MessageBox.Show("Debe de llenar el Numero de Registro");
                    //    return;
                    //}
                    String a = tbFechaconsecion.Text;
                    /*if (tbFechaconsecion.Text != "" )
                    {
                        if (tbFechavigencia.Text == "" || tbFechaprobouso.Text == "")
                        {
                            MessageBox.Show("Debe de llenar Fecha Vigencia y Fecha Decla");
                            return;
                        }
                    }*/
                    if (tbFechavigencia.Text  != "")
                    {
                        if ( tbFechaprobouso.Text == "")
                        {
                            MessageBox.Show("Debe de llenar Fecha Consecion y Fecha Decla");
                            return;
                        }
                    }
                    if (tbFechaprobouso.Text != "" && tbFechavigencia.Text == "")
                    {
                        MessageBox.Show("Debe de llenar Fecha Vigencia y Fecha Consecion");
                        return;
                    }

                    if (comboBoxIdioma.SelectedItem == null)
                    {
                        MessageBox.Show("Debe seleccionar un idioma para el caso");
                        return;
                    }
                    if (cbMulticaso.Text == "" || cbMulticaso == null)
                    {
                        creacasomarcas();
                    }
                    else
                    {
                        //sNumerosdecaso = "";
                        //var confirmResult = MessageBox.Show("¿Seguro que desea agregar " + cbMulticaso.Text + " casos con la misma información?", "Agregar Multiples casos",
                        //                     MessageBoxButtons.YesNo);
                        //if (confirmResult == DialogResult.Yes)
                        //{
                        //    creamulticaso();
                        //    MessageBox.Show("Los números de casos creados son:" + sNumerosdecaso);
                        //    cFcaptura.Show();
                        //    this.Close();
                        //}
                        //else
                        //{
                        //    cbMulticaso.Focus();
                        //}

                    }
                }
            }
            catch (Exception exs)
            {
                new filelog(" error cree caso linea 666: ", exs.StackTrace);

            }


            //creamos el caso tomando los valores del form 
            //tipo de solicitud     comboTiposolicitud
            //subTipo de solicitud  comboboxSubtipo
            //subsubtipo de solicitud comboboxSubtipo2
            //fecha carta    comboboxFecha
            //client due date    textClientduedate
            //textBox3 plazo legal 
            //textBoxFechainternacional fecha internacional
            //comboBoxClientes id cliente
            //comboBoxContacto     contacto id_contacto 
            //textBoxReferencia  Referencia
            //comboBoxInteresado   Tiutlar
            //richTextBoxTitulo   Titulo del caso 
            //comboBoxClase   clase
            //comboBoxIdioma  idioma
            //listViewPrioridades  prioridades
            //comboBoxFirma  Firma
            //comboBoxResponsable  responsable
            //Boolean bCaso = false, bCasocliente = false, bcasointeresado = false, breferencias = false, bPrioridades = false;
            //try
            //{
            //    //hay que validar cuales campos son obligatorios y cuals son opcionales

            //    String sComboTiposolicitud = validacombobox(comboTiposolicitud);//*
            //    //String sComboTiposolicitud = (comboTiposolicitud.SelectedItem as ComboboxItem).Value.ToString();//*

            //    String sComboboxSubtipo = validacombobox(comboboxSubtipo);//.SelectedItem as ComboboxItem).Value.ToString();//*


            //    //String sComboboxSubtipo2 = validacombobox(comboboxSubtipo2);//.SelectedItem as ComboboxItem).Value.ToString();//*
            //    //String sComboboxFecha = (comboboxFecha.SelectedItem as ComboboxItem).Value.ToString();//


            //    String sComboBoxClientes = validacombobox(comboBoxClientes);//.SelectedItem as ComboboxItem).Value.ToString();//casocliente
            //    String sComboBoxContacto = validacombobox(comboBoxContacto);//.SelectedItem as ComboboxItem).Value.ToString();//casocliente
            //    String sComboBoxInteresado = validacombobox(comboBoxInteresado);//.SelectedItem as ComboboxItem).Value.ToString();//casointeresado

            //    // String sComboBoxClase = (comboBoxClase.SelectedItem as ComboboxItem).Value.ToString();//pendiente
            //    String sComboBoxIdioma = validacombobox(comboBoxIdioma);//.SelectedItem as ComboboxItem).Value.ToString();//*
            //    String sCcomboBoxFirma = validacombobox(comboBoxFirma);//.SelectedItem as ComboboxItem).Value.ToString();//pendiente
            //    String sComboBoxResponsable = validacombobox(comboBoxResponsable);//.SelectedItem as ComboboxItem).Value.ToString();//*


            //    //Dar formatos de fecha 
            //    String sTextboxFecha = TexboxFecha.Text;//Fecha carta   CasoFechaCartaCliente *
            //    String stextClientduedate = textClientduedate.Text;//CasoFechaFilingCliente client due date *
            //    String stextBoxPlazolegal = textBoxPlazolegal.Text;//textBoxPlazolegalcasofechalegal  CasoFechaLegal
            //    String stextBoxFechainternacional = "";// textBoxFechainternacional.Text;//CasoFechaInternacional fecha internacional
            //    String stextBoxReferencia = textBoxReferencia.Text;//pendiente
            //    String srichTextBoxTitulo = richTextBoxTitulo.Text;//Casotitulo
            //    String sresponsableid = login.sId;
            //    String sCasodenominacion = "";
            //    /*if (sComboBoxIdioma != "2")
            //    {
            //        sCasodenominacion = srichTextBoxTitulo;
            //        srichTextBoxTitulo = "";
            //    }*/
            //    if (textClientduedate.Text == "")
            //    {
            //        MessageBox.Show("El campo de la Fecha Client due date no puede estar vacia.");
            //        return;
            //    }

            //    if (comboBoxIdioma.SelectedItem == null) {
            //        MessageBox.Show("Debe seleccionar un idioma para el caso");
            //        return;
            //    }
            //    //validamos los campos que son obligatorios para poder agregar el caso
            //    //validamos fecha, referencia, titulo, tiposolicitud. cliente, contacto, interesado
            //    if (sTextboxFecha != "" && stextBoxReferencia != "" && srichTextBoxTitulo != "" && sComboTiposolicitud != "" && sComboBoxClientes != "" && sComboBoxInteresado != "")
            //    {
            //        sTextboxFecha = cambiaformatofecha(sTextboxFecha);
            //        stextClientduedate = cambiaformatofecha(stextClientduedate);
            //        stextBoxPlazolegal = cambiaformatofecha(stextBoxPlazolegal);
            //        stextBoxFechainternacional = cambiaformatofecha(stextBoxFechainternacional);

            //        conect con_1 = new conect();
            //        String sGetcasonumero = "select casoid, casonumero from caso_marcas order by casoid desc limit 1;";
            //        MySqlDataReader respuestastringcasonum = con_1.getdatareader(sGetcasonumero);
            //        String sCasonumero = "";
            //        while (respuestastringcasonum.Read())
            //        {
            //            sCasonumero = validareader("casonumero", "casoid", respuestastringcasonum).Text.ToString();
            //        }
            //        respuestastringcasonum.Close();
            //        con_1.Cerrarconexion();

            //        var result = sCasonumero.Substring(0, sCasonumero.LastIndexOf('-'));
            //        //MessageBox.Show(result);
            //        int iValor = Int32.Parse(result) + 1;
            //        sCasonumero = iValor + "-TM";

            //        //hasta aquí analizamos el Caso Numero
            //        //if (sCasonumero.Length == 7)
            //        //{
            //        //    String extencion = sCasonumero.Substring(4, 3);
            //        //    sCasonumero = sCasonumero.Substring(0, 4);
            //        //    int iValor = Int32.Parse(sCasonumero) + 1;
            //        //    sCasonumero = iValor + extencion;
            //        //}
            //        //else
            //        //{
            //        //    if (sCasonumero.Length == 4)
            //        //    {
            //        //        int iValor = Int32.Parse(sCasonumero.Substring(0,8)) + 1;
            //        //        sCasonumero = iValor + "";
            //        //    }
            //        //    else
            //        //    {
            //        //        MessageBox.Show("El último caso de éste tipo es: " + sCasonumero + " y no se reconoce el formato");
            //        //    }
            //        //}
            //        String sEstatudID = "1"; // por default EstatuscasoID
            //        int iIdpaiscaso = 0;
            //        for (int yuno = 0; yuno < paises.Length; yuno++)
            //        {
            //            if (paises[yuno] == tbClavepaiscaso.Text)
            //            {
            //                iIdpaiscaso = yuno;

            //            }
            //        }

            //        //inserta caso_ marcas
            //        conect con_2 = new conect();
            //        String insert_marcas = "INSERT INTO `caso_marcas` " +
            //                                 "(`CasoId`, " +
            //                                 "`TipoSolicitudId`, " +
            //                                 "`SubTipoSolicitudId`, " +
            //                                 "`CasoTituloespanol`, " +
            //                                 "`CasoTituloingles`, " +
            //                                 "`IdiomaId`, " +
            //                                 "`CasoNumero`, " +
            //                                 "`ResponsableId`, " +

            //                                 "`CasoFechaAlta`, " +
            //                                 "`CasoFechaCartaCliente`, "+


            //                                 "`CasoFechaFilingSistema`, " +
            //                                 "`CasoFechaFilingCliente`, " +
            //                                 "`EstatusCasoId`, " +
            //                                 "`UsuarioId`, " +
            //                                 "`PaisId`) " +
            //                                 "VALUES " +
            //                                 "(NULL, " +
            //                                 "'" + sComboTiposolicitud + "', " +
            //                                 "'" + sComboboxSubtipo + "', " +
            //                                 "'" + srichTextBoxTitulo + "', " +
            //                                 "'" + srichTextBoxTitulo + "', " +
            //                                 "'" + sComboBoxIdioma + "', " +

            //                                 "'" + sCasonumero + "', " +
            //                                 "'" + sresponsableid + "', " +

            //                                 "'" + sTextboxFecha + "', " +
            //                                 "'" + sTextboxFecha + "', " +

            //                                 "'" + sTextboxFecha + "', " +
            //                                 "'" + stextClientduedate + "', " +

            //                                 "'" + sEstatudID + "', " +
            //                                 "'" + sresponsableid + "', " +
            //                                 "'" + iIdpaiscaso + "'); ";

            //        String sGetid = "SELECT * FROM `caso_marcas` order by CasoId desc limit 1";
            //        MySqlDataReader respuestastring = con_2.getdatareader(insert_marcas);
            //        respuestastring.Close();
            //        con_2.Cerrarconexion();

            //        conect con_3 = new conect();
            //        MySqlDataReader respuestastringid = con_3.getdatareader(sGetid);
            //        String sCasoid = "";
            //        while (respuestastringid.Read())
            //        {
            //            sCasoid = validareader("CasoId", "CasoId", respuestastringid).Value.ToString();
            //            //MessageBox.Show("El caso se agrego correctamente con CasoID: " + sCasoid + " Casonumero: " + sCasonumero);
            //            bCaso = true;

            //        }
            //        respuestastringid.Close();
            //        con_3.Cerrarconexion();

            //        conect con_4 = new conect();
            //        String insertacliente = "INSERT INTO `casocliente` " +
            //                                "(`CasoClienteId`, " +
            //                                "`ClienteId`, " +
            //                                "`contactoid`, " +
            //                                "`CasoId`, " +
            //                                "`TipoSolicitudId`, " +
            //                                "`CasoClienteIndCorres`) " +
            //                                "VALUES " +
            //                                "(NULL, " +
            //                                sComboBoxClientes + ", " +
            //                                sComboBoxContacto + ", " +
            //                                sCasoid + ", " +
            //                                sComboTiposolicitud + ", " +
            //                                "0);";
            //        MySqlDataReader respuestastringinsertclient = con_4.getdatareader(insertacliente);
            //        if (respuestastringinsertclient.RecordsAffected == 1)
            //        {
            //            bCasocliente = true;
            //            //MessageBox.Show("Se inserto en casoclientes");
            //        }
            //        respuestastringinsertclient.Close();
            //        con_4.Cerrarconexion();

            //        conect con_5 = new conect();
            //        String insertcasointeresado = "INSERT INTO `casointeresado` " +
            //                                    "(`CasoInteresadoId`, " +
            //                                    "`InteresadoId`, " +
            //                                    "`CasoId`, " +
            //                                    "`TipoSolicitudId`, " +
            //                                    "`CasoInteresadoSecuencia`, " +
            //                                    "`TipoRelacionId`, " +
            //                                    "`DireccionId`) " +
            //                                    "VALUES " +
            //                                    "( " +
            //                                    "NULL, " +
            //                                    sComboBoxInteresado + ", " +
            //                                    sCasoid + ", " +
            //                                    sComboTiposolicitud + ", " +
            //                                    "1," +
            //                                    "1," +
            //                                    "null);";
            //        MySqlDataReader respuestastringinscasoint = con_5.getdatareader(insertcasointeresado);
            //        if (respuestastringinscasoint.RecordsAffected == 1)
            //        {
            //            //MessageBox.Show("Se inserto en casointeresado");
            //            bcasointeresado = true;
            //        }
            //        respuestastringinscasoint.Close();
            //        con_5.Cerrarconexion();
            //        //agregar a¿rowaffected y validar que se inserto
            //        conect con_6 = new conect();
            //        String insertreferencia = " INSERT INTO `referencia` " +
            //                                    " (`ReferenciaId`, " +
            //                                    " `CasoId`, " +
            //                                    " `TipoSolicitudId`, " +
            //                                    " `TipoReferenciaId`, " +
            //                                    " `ReferenciaNombre`) " +
            //                                    " VALUES (" +
            //                                    " NULL, " +
            //                                    sCasoid + ", " +
            //                                    sComboTiposolicitud + ", " +
            //                                    "1, " +
            //                                    "'" + stextBoxReferencia + "'); ";
            //        MySqlDataReader respinsertreferencia = con_6.getdatareader(insertreferencia);
            //        if (respinsertreferencia.RecordsAffected == 1)
            //        {
            //            //MessageBox.Show("Se inserto en referencia");
            //            breferencias = true;
            //        }
            //        respinsertreferencia.Close();
            //        con_6.Cerrarconexion();

            //        //Insertamos las prioridades capturadas al agregar un caso nuevo del grupo 1

            //        for (int z = 0; z < listViewPrioridades.Items.Count; z++)
            //        {

            //            //para validar que la fecha sea correcta
            //            String sFechaprioridad = validafechacorecta(listViewPrioridades.Items[z].SubItems[1].Text);

            //            //obtenemos el id del pais
            //            int iIdpais = 0;
            //            for (int y = 0; y < paisesclave.Length; y++)
            //            {
            //                if (paisesclave[y] == listViewPrioridades.Items[z].SubItems[2].Text)
            //                {
            //                    iIdpais = y;
            //                }
            //            }
            //            //obtenemos el id del tipo de prioridad
            //            int iIdtipoprioridad = 0;

            //            if ("PCT" == listViewPrioridades.Items[z].SubItems[3].Text){ iIdtipoprioridad = 1; }
            //            else { iIdtipoprioridad = 2; }

            //            conect conect_prio = new conect();
            //            String sInsertprioridades = " INSERT INTO `prioridad`" +
            //                                        " (`PrioridadId`," +
            //                                        " `CasoId`," +
            //                                        " `TipoSolicitudId`, " +
            //                                        " `PrioridadNumero`," +
            //                                        " `PrioridadFecha`," +
            //                                        " `PaisID`," +
            //                                        " `TipoPrioridadId`)" +
            //                                        " VALUES" +
            //                                        " (NULL," +
            //                                        " " + sCasoid + ", " +
            //                                        sComboTiposolicitud + ", " +
            //                                        " '" + listViewPrioridades.Items[z].SubItems[0].Text + "'," +
            //                                        " '" + sFechaprioridad + "', " +
            //                                        " '" + iIdpais + "'," +
            //                                        " '" + iIdtipoprioridad + "');";
            //            MySqlDataReader resp_insertprioridades = conect_prio.getdatareader(sInsertprioridades);
            //            if (resp_insertprioridades.RecordsAffected == 1)
            //            {
            //                bCasoprioridades = true;
            //            }
            //            resp_insertprioridades.Close();
            //            conect_prio.Cerrarconexion();
            //        }
            //        if (listViewPrioridades.Items.Count == 0)
            //        {
            //            bCasoprioridades = true;
            //        }
            //        /*Aquí agregamos los plazos*/
            //        /*
            //        * Creamos el plazo en la tabla Plazo la relacion con casoid y tiposolicitudid 
            //        */
            //        String sPlazosid = "";
            //        bool bBanderacreadetalleplazo = false;
            //        conect conect_plazosid = new conect();
            //        String sQuery_plazos = " INSERT INTO `plazos`(`Plazosid`,`CasoId`,`TipoSolicitudId`,`Fecha_creacion`)VALUES " +
            //                                        " (NULL, " +
            //                                        " '" + sCasoid + "', " +
            //                                        " '" + sComboTiposolicitud + "', " +
            //                                        " curdate()); ";

            //        MySqlDataReader resp_plazos = conect_plazosid.getdatareader(sQuery_plazos);
            //        if (resp_plazos.RecordsAffected == 1)
            //        {
            //            conect conect_plazosid_relacion = new conect();
            //            String sQuery_plazos_relacion_general = "select * from plazos order by  plazosid desc limit 1;";
            //            MySqlDataReader resp_plazos_relacion = conect_plazosid_relacion.getdatareader(sQuery_plazos_relacion_general);
            //            while (resp_plazos_relacion.Read())
            //            {
            //                sPlazosid = validareader("Plazosid", "Plazosid", resp_plazos_relacion).Text;
            //            }
            //            resp_plazos_relacion.Close();
            //            conect_plazosid_relacion.Cerrarconexion();

            //            bBanderacreadetalleplazo = true;
            //        }
            //        resp_plazos.Close();
            //        conect_plazosid.Cerrarconexion();

            //        String sFechahoy = DateTime.Today.ToString("yyyy'/'MM'/'dd");
            //        String sFechapresentacion = DateTime.ParseExact(textClientduedate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbDocumentofecharecepcion.Text
            //        conect conect_plazoid = new conect();
            //        String sQuery_plazo_general = " INSERT INTO `plazos_detalle` " +
            //                                        " (`Plazos_detalleid`, " +
            //                                        " `Plazosid`, " +//

            //                                        " `usuario_creo_plazodetalle`, " +
            //                                        " `Tipo_plazoid`, " +
            //                                        " `Estatus_plazoid`, " +

            //                                        " `Fecha_Vencimiento`) " +
            //                                        " VALUES (NULL," +
            //                                        " '" + sPlazosid + "', " +

            //                                        " '" + login.sId + "', " +
            //                                        " '2', " +
            //                                        " '1', " +

            //                                        " '" + sFechapresentacion + "');";

            //        MySqlDataReader resp_plazo = conect_plazoid.getdatareader(sQuery_plazo_general);
            //        if (resp_plazo.RecordsAffected == 1)
            //        {
            //            //ibBanderacreadetalleplazo_contador++;
            //            //MessageBox.Show("Se creó un plazo.");
            //            /*aqui validamos que se inserto el plazo*/
            //        }
            //        resp_plazo.Close();
            //        conect_plazoid.Cerrarconexion();
            //        /*Generamos el plazo de avisar al cliente de que se va presentar su solicitud*/
            //        DateTime hoy = DateTime.Today;
            //        DateTime hoymasundia = hoy.AddDays(1);
            //        //Date dFechavigencia = sFechapresentacion
            //        String sFechaVigencia = hoymasundia.ToString("yyyy-MM-dd");
            //        conect conect_plazoid_instruccion = new conect();
            //        String sQuery_plazo_instruccion = " INSERT INTO `plazos_detalle` " +
            //                                        " (`Plazos_detalleid`, " +
            //                                        " `Plazosid`, " +//

            //                                        " `usuario_creo_plazodetalle`, " +
            //                                        " `Tipo_plazoid`, " +
            //                                        " `Estatus_plazoid`, " +

            //                                        " `Fecha_Vencimiento`) " +
            //                                        " VALUES (NULL," +
            //                                        " '" + sPlazosid + "', " +

            //                                        " '" + login.sId + "', " +
            //                                        " '1', " +
            //                                        " '1', " +

            //                                        " '" + sFechaVigencia + "');";

            //        //" '" + tipoplazoid_avisocliente + "', " +
            //        //" '1', " +
            //        //" '" + documentoid + "', " +
            //        //" '" + login.sId + "');";

            //        MySqlDataReader resp_plazo_instruccion = conect_plazoid_instruccion.getdatareader(sQuery_plazo_instruccion);
            //        if (resp_plazo_instruccion.RecordsAffected == 1)
            //        {
            //            //ibBanderacreadetalleplazo_contador++;
            //            //MessageBox.Show("Se creó un plazo.");
            //            /*aqui validamos que se inserto el plazo*/
            //        }
            //        resp_plazo_instruccion.Close();
            //        conect_plazoid_instruccion.Cerrarconexion();
            //        /*FIN Generamos el plazo de avisar al cliente de que se va presentar su solicitud*/

            //        /*Aquí Validaremos que se creé el plazo de envir a traducir*/

            //        /*FIN Aquí Validaremos que se creé el plazo de envir a traducir*/

            //        /*Fin de agregar plapzos*/



            //        if (bCaso && bCasocliente && bcasointeresado && breferencias)
            //        {
            //            MessageBox.Show("El caso se agrego correctamente con el CasoNúmero: " + sCasonumero);
            //            DialogResult results = MessageBox.Show("¿Desea agregar un caso nuevo del mismo tipo?", "Agregar Caso Marcas", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //            if (results == DialogResult.Yes)
            //            {
            //                //code for Yes
            //                Nuevocaso_marcas nuevocasothis = new Nuevocaso_marcas(sTiposolicitudiscaso, cFcaptura, login);
            //                nuevocasothis.Show();
            //                this.Close();
            //            }else if (results == DialogResult.No)
            //            {
            //                //code for No
            //                cFcaptura.Show();
            //                this.Close();
            //            }//}
            //            //else if (results == DialogResult.Cancel)
            //            //{
            //            //    //code for Cancel
            //            //}
            //        }
            //        else
            //        {

            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Debe llenar los campos obligarorios para caso");
            //    }
            //}
            //catch (Exception E)
            //{
            //    MessageBox.Show("Verifique que todos los campos estén correctos. " + E);
            //    new filelog(login.sId, E.ToString());
            //}

        }
        public void comboclase_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                conect con_tres = new conect();
                String sQueryclases = "SELECT * FROM `clasificadornizavigente` ORDER BY CasoProductosClase ASC";
                MySqlDataReader respuestastringclases = con_tres.getdatareader(sQueryclases);
                while (respuestastringclases.Read())
                {
                    comboBoxClase.Items.Add(validareader("CasoProductosClase", "ClasificadorNizaDescripcion", respuestastringclases));
                }
                respuestastringclases.Close();
                con_tres.Cerrarconexion();

            }
            catch (Exception E)
            {

                new filelog(login.sId, E.ToString());
            }

        }
          private void comboTiposolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ID_Ley.Items.Clear();
                ID_Ley.Text = "";
                //string tipo = (comboTiposolicitud.SelectedItem as ComboboxItem).Value.ToString();
                conect con = new conect();
                String query = "select * from ley" ;
                string valor = (comboTiposolicitud.SelectedItem as ComboboxItem).Value.ToString();
                MySqlDataReader respuestastring = con.getdatareader(query);
                while (respuestastring.Read())
                {
                    ID_Ley.Items.Add(validareader("Nombre", "ID_Ley", respuestastring));
                }
                string seccion = valor;
                ComboTipomarca_SelectedIndexChanged(seccion);
                respuestastring.Close();
                con.Cerrarconexion();


            }
            catch (Exception E)
            {

                new filelog(login.sId, E.ToString());
            }
        }

      /*  public void comboTiposolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                comboboxSubtipo.Items.Clear();
                comboboxSubtipo.Text = "";
                string tipo = (comboTiposolicitud.SelectedItem as ComboboxItem).Value.ToString();

                    conect con = new conect();
                    String query = "select SubTipoSolicitudId, SubTipoSolicitudDescripcion from subtiposolicitud where tiposolicitudID =" + (comboTiposolicitud.SelectedItem as ComboboxItem).Value.ToString();
                    string valor = (comboTiposolicitud.SelectedItem as ComboboxItem).Value.ToString();
                    MySqlDataReader respuestastring = con.getdatareader(query);
                    while (respuestastring.Read())
                    {
                        comboboxSubtipo.Items.Add(validareader("SubTipoSolicitudDescripcion", "SubTipoSolicitudId", respuestastring));

                    }
                    string seccion = valor;
                    ComboTipomarca_SelectedIndexChanged(seccion);
                    respuestastring.Close();
                    con.Cerrarconexion();
                
            }
            catch (Exception E)
            {

                new filelog(login.sId, E.ToString());
            }
        }*/
        public void ComboTipomarca_SelectedIndexChanged(string seccion)
        {

            comboTipomarca1.Items.Clear();
            comboTipomarca1.Text = "";
            string secc = seccion;
            if (secc != "8")

            {
                if (secc != "9")
                { 
                try
                {

                    comboTipomarca1.Items.Clear();
                    comboTipomarca1.Text = "";

                    conect con = new conect();

                        String query = "select TipoMarcaId, TipoMarcaDescrip from tipomarca where TipoMarcaIndAct = 1";
                        MySqlDataReader respuestastring = con.getdatareader(query);
                    while (respuestastring.Read())
                    {
                        comboTipomarca1.Items.Add(validareader("TipoMarcaDescrip", "TipoMarcaId", respuestastring));
                    }
                    respuestastring.Close();
                    con.Cerrarconexion();

                }
                catch (Exception E)
                {

                    new filelog(login.sId, E.ToString());
                }

            }
        }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            cFcaptura.Close();
            login.Close();

        }
        private void comboboxLey_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //comboboxSubtipo2.Items.Clear();
                comboBoxTipodos.Items.Clear();
                String a = (ID_Ley.SelectedItem as ComboboxItem).Value.ToString();
                //comboboxSubtipo2.Text = "";
                if ((ID_Ley.SelectedItem as ComboboxItem).Value.ToString().Equals("10"))
                {
                    //cuantas prioridades debe tener y dejamos la del registro como valor 0
                    ////comboboxSubtipo2.Enabled = true;
                    ////conect con = new conect();
                    ////String query = "select * from tipopct;";
                    ////MySqlDataReader respuestastring = con.getdatareader(query);
                    ////while (respuestastring.Read())
                    ////{
                    ////    comboboxSubtipo2.Items.Add(validareader("TipoPctDescrip", "TipoPctId", respuestastring));
                    ////}
                    //comboBoxTipodos = "";  //sólo puede agregar PCT para las nuevas prioridades  

                    ComboboxItem comboTipodos = new ComboboxItem();
                    comboTipodos.Text = "PCT";
                    comboTipodos.Value = "1";
                    comboBoxTipodos.Items.Add(comboTipodos);

                    ComboboxItem comboTipotres = new ComboboxItem();
                    comboTipotres.Text = "París";
                    comboTipotres.Value = "2";
                    comboBoxTipodos.Items.Add(comboTipotres);
                    groupPrioridades.Enabled = true;

                    //comboBoxNprioridades.Text = "0";
                }
                else
                {
                    if ((ID_Ley.SelectedItem as ComboboxItem).Value.ToString().Equals("2"))
                    {
                        //regular no lleva prioridades
                        groupPrioridades.Enabled = false;
                        groupProtocolo.Enabled = false;
                        //comboboxSubtipo2.Enabled = false;
                        //comboBoxNprioridades.Text = "";
                    }
                    else
                    {
                        //cuantas prioridades debe tener?
                        groupPrioridades.Enabled = false;
                        groupProtocolo.Enabled = false;
                        //comboBoxTipodos = "";  //asignamos PCT y parís y el otro 
                        //comboboxSubtipo2.Enabled = false;
                        ComboboxItem comboTipotres = new ComboboxItem();
                        comboTipotres.Text = "París";
                        comboTipotres.Value = "2";
                        comboBoxTipodos.Items.Add(comboTipotres);
                    }
                    if ((ID_Ley.SelectedItem as ComboboxItem).Value.ToString().Equals("5"))
                    {
                        groupProtocolo.Enabled = true;
                        groupPrioridades.Enabled = false;
                    }
                    else
                    {
                        groupProtocolo.Enabled = false;
                        //groupPrioridades.Enabled = false;
                    }
                    if ((ID_Ley.SelectedItem as ComboboxItem).Value.ToString().Equals("3") || (ID_Ley.SelectedItem as ComboboxItem).Value.ToString().Equals("4"))
                    {
                        groupProtocolo.Enabled = false;
                        groupPrioridades.Enabled = true;
                    }
                    else
                    {
                        //groupProtocolo.Enabled = false;
                        //groupPrioridades.Enabled = false;
                    }
                    //comboBoxNprioridades.Items.Clear();
                    //comboBoxNprioridades.Items.Add("1");
                    //comboBoxNprioridades.Items.Add("2");
                    //comboBoxNprioridades.Items.Add("3");
                    //comboBoxNprioridades.Items.Add("4");
                    //comboBoxNprioridades.Items.Add("5");
                    //comboBoxNprioridades.Items.Add("6");
                    //comboBoxNprioridades.Items.Add("7");
                    //comboBoxNprioridades.Items.Add("8");
                    //comboBoxNprioridades.Items.Add("9");
                    //comboBoxNprioridades.Items.Add("10");
                    //comboBoxNprioridades.Items.Add("11");
                    //comboBoxNprioridades.Items.Add("12");
                    //comboBoxNprioridades.Items.Add("13");
                    //comboBoxNprioridades.Items.Add("14");
                    //comboBoxNprioridades.Items.Add("15");

                    //comboBoxNprioridades.Text = "1";
                    //quitar el cero 
                    //comboBoxNprioridades.Items.Equals("0")

                }

            }
            catch (Exception E)
            {

                new filelog(login.sId, E.ToString());
            }
        }

        /*
        private void comboboxSubtipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //comboboxSubtipo2.Items.Clear();
                comboBoxTipodos.Items.Clear();
                String a = (comboboxSubtipo.SelectedItem as ComboboxItem).Value.ToString();
                //comboboxSubtipo2.Text = "";
                if ((comboboxSubtipo.SelectedItem as ComboboxItem).Value.ToString().Equals("1"))
                {
                    //cuantas prioridades debe tener y dejamos la del registro como valor 0
                    ////comboboxSubtipo2.Enabled = true;
                    ////conect con = new conect();
                    ////String query = "select * from tipopct;";
                    ////MySqlDataReader respuestastring = con.getdatareader(query);
                    ////while (respuestastring.Read())
                    ////{
                    ////    comboboxSubtipo2.Items.Add(validareader("TipoPctDescrip", "TipoPctId", respuestastring));
                    ////}
                    //comboBoxTipodos = "";  //sólo puede agregar PCT para las nuevas prioridades  

                    ComboboxItem comboTipodos = new ComboboxItem();
                    comboTipodos.Text = "PCT";
                    comboTipodos.Value = "1";
                    comboBoxTipodos.Items.Add(comboTipodos);

                    ComboboxItem comboTipotres = new ComboboxItem();
                    comboTipotres.Text = "París";
                    comboTipotres.Value = "2";
                    comboBoxTipodos.Items.Add(comboTipotres);
                    groupPrioridades.Enabled = true;

                    //comboBoxNprioridades.Text = "0";
                }
                else
                {
                    if ((comboboxSubtipo.SelectedItem as ComboboxItem).Value.ToString().Equals("3"))
                    {
                        //regular no lleva prioridades
                        groupPrioridades.Enabled = false;
                        groupProtocolo.Enabled = false;
                        //comboboxSubtipo2.Enabled = false;
                        //comboBoxNprioridades.Text = "";
                    }
                    else  
                    {
                        //cuantas prioridades debe tener?
                        groupPrioridades.Enabled = false;
                        groupProtocolo.Enabled = false;
                        //comboBoxTipodos = "";  //asignamos PCT y parís y el otro 
                        //comboboxSubtipo2.Enabled = false;
                        ComboboxItem comboTipotres = new ComboboxItem();
                        comboTipotres.Text = "París";
                        comboTipotres.Value = "2";
                        comboBoxTipodos.Items.Add(comboTipotres);
                    }
                    if ((comboboxSubtipo.SelectedItem as ComboboxItem).Value.ToString().Equals("4"))
                        {
                            groupProtocolo.Enabled = true;
                            groupPrioridades.Enabled = false;
                        }
                        else
                        {
                            groupProtocolo.Enabled = false;
                            //groupPrioridades.Enabled = false;
                        }
                        if ((comboboxSubtipo.SelectedItem as ComboboxItem).Value.ToString().Equals("2") || (comboboxSubtipo.SelectedItem as ComboboxItem).Value.ToString().Equals("5")) 
                        {
                            groupProtocolo.Enabled = false;
                            groupPrioridades.Enabled = true;
                        }
                        else
                        {
                            //groupProtocolo.Enabled = false;
                            //groupPrioridades.Enabled = false;
                        }
                        //comboBoxNprioridades.Items.Clear();
                        //comboBoxNprioridades.Items.Add("1");
                        //comboBoxNprioridades.Items.Add("2");
                        //comboBoxNprioridades.Items.Add("3");
                        //comboBoxNprioridades.Items.Add("4");
                        //comboBoxNprioridades.Items.Add("5");
                        //comboBoxNprioridades.Items.Add("6");
                        //comboBoxNprioridades.Items.Add("7");
                        //comboBoxNprioridades.Items.Add("8");
                        //comboBoxNprioridades.Items.Add("9");
                        //comboBoxNprioridades.Items.Add("10");
                        //comboBoxNprioridades.Items.Add("11");
                        //comboBoxNprioridades.Items.Add("12");
                        //comboBoxNprioridades.Items.Add("13");
                        //comboBoxNprioridades.Items.Add("14");
                        //comboBoxNprioridades.Items.Add("15");

                        //comboBoxNprioridades.Text = "1";
                        //quitar el cero 
                        //comboBoxNprioridades.Items.Equals("0")
                    
                }

            }
            catch (Exception E)
            {

                new filelog(login.sId, E.ToString());
            }
        }
        */
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
            con.Cerrarconexion();

            //Borramos los cantactos anteriores, si es que los ubiera
            comboBoxContacto.Text = "";
            richTextBox1.Text = "";
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
        }


        private void textClientduedate_KeyPress_1(object sender, KeyPressEventArgs e)
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


            if (textClientduedate.Text.Length == 2)
            {
                textClientduedate.Text = textClientduedate.Text + "-";
                textClientduedate.SelectionStart = textClientduedate.Text.Length;

            }
            if (textClientduedate.Text.Length == 5)
            {
                textClientduedate.Text = textClientduedate.Text + "-";
                textClientduedate.SelectionStart = textClientduedate.Text.Length;
            }

        }

        private void comboBoxPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int iValuepais = Convert.ToInt32((comboBoxPais.SelectedItem as ComboboxItem).Value.ToString());
                cbCvpais.Text = paises[iValuepais];
            }
            catch (Exception ex)
            {

            }



        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBoxNumero.TextLength >= 3)
            {
                if (textBoxNumero.Text.Substring(0, 3).Equals("PCT"))
                {

                    comboBoxTipodos.SelectedItem = "PCT";
                    comboBoxTipodos.Text = "PCT";

                    if (textBoxNumero.TextLength >= 10)
                    {

                        for (int x = 0; x < paises.Length; x++)
                        {
                            if (paises[x] == textBoxNumero.Text.Substring(4, 2))
                            {
                                comboBoxPais.SelectedItem = paisesclave[x];
                                comboBoxPais.Text = paisesclave[x];
                            }
                            //else {
                            //    comboBoxPais.SelectedItem = "";
                            //    comboBoxPais.Text = "";
                            //}
                        }
                    }
                }
            }
        }

        private void textBoxcve_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)//fecha en formato
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


            if (textBoxFecha.Text.Length == 2)
            {
                textBoxFecha.Text = textBoxFecha.Text + "-";
                textBoxFecha.SelectionStart = textBoxFecha.Text.Length;

            }
            if (textBoxFecha.Text.Length == 5)
            {
                textBoxFecha.Text = textBoxFecha.Text + "-";
                textBoxFecha.SelectionStart = textBoxFecha.Text.Length;
            }

        }
        private String validafechacorecta(String Fechaentrada)
        {//verificamos que es una fecha valida y la convertimos a un formato date mysql
            String sFechasalida = "";
            try
            {
                sFechasalida = DateTime.ParseExact(Fechaentrada, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbDocumentofecharecepcion.Text;
            }
            catch (Exception E)
            {
                sFechasalida = "";
            }
            return sFechasalida;
        }
        public void agregaprioridades()
        {


        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            if (textBoxNumero.Text != "" && textBoxFecha.Text != "" && comboBoxPais.Text != "" && comboBoxTipodos.Text != "")
            {
                ListViewItem lPrioridades = new ListViewItem(textBoxNumero.Text);
                lPrioridades.SubItems.Add(textBoxFecha.Text);
                lPrioridades.SubItems.Add(comboBoxPais.Text);
                lPrioridades.SubItems.Add(comboBoxTipodos.Text);
                listViewPrioridades.Items.Add(lPrioridades);
                textBoxNumero.Text = "";
                textBoxFecha.Text = "";
                comboBoxPais.Text = "";
                comboBoxTipodos.Text = "";
            }
            else
            {
                MessageBox.Show("Debe completar los campos para agregar una prioridad");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            cFcaptura.Show();
            this.Close();
        }
        //private void btn_recuperarudes_Click(object sender, EventArgs e)
        //{
        //    if (textBoxPlazolegal.Text == "012685")
        //    {
        //textBoxPlazolegal.Text = textBoxFecha.Text;
        //}
        //    else
        //    {
        //        "la acción que desees hacer si no es correcta la contraseña"
        //}
        //}

        private void TexboxFecha_KeyPress(object sender, KeyPressEventArgs e)
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


            if (TexboxFecha.Text.Length == 2)
            {
                TexboxFecha.Text = TexboxFecha.Text + "-";
                TexboxFecha.SelectionStart = TexboxFecha.Text.Length;

            }
            if (TexboxFecha.Text.Length == 5)
            {
                TexboxFecha.Text = TexboxFecha.Text + "-";
                TexboxFecha.SelectionStart = TexboxFecha.Text.Length;
            }
        }

        private void textBoxPlazolegal_KeyPress(object sender, KeyPressEventArgs e)
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


            if (textBoxPlazolegal.Text.Length == 2)
            {
                textBoxPlazolegal.Text = textBoxPlazolegal.Text + "-";
                textBoxPlazolegal.SelectionStart = textBoxPlazolegal.Text.Length;

            }
            if (textBoxPlazolegal.Text.Length == 5)
            {
                textBoxPlazolegal.Text = textBoxPlazolegal.Text + "-";
                textBoxPlazolegal.SelectionStart = textBoxPlazolegal.Text.Length;
            }
        }

        private void textBoxFechainternacional_KeyPress(object sender, KeyPressEventArgs e)
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


            //if (textBoxFechainternacional.Text.Length == 2)
            //{
            //    textBoxFechainternacional.Text = textBoxFechainternacional.Text + "-";
            //    textBoxFechainternacional.SelectionStart = textBoxFechainternacional.Text.Length;

            //}
            //if (textBoxFechainternacional.Text.Length == 5)
            //{
            //    textBoxFechainternacional.Text = textBoxFechainternacional.Text + "-";
            //    textBoxFechainternacional.SelectionStart = textBoxFechainternacional.Text.Length;
            //}
        }

        public String cambiaformatofecha(String Fechauno)
        {
            if (Fechauno != "")
            {
                //Fechauno se espera con el formato dd-mm-yyyy
                String año = Fechauno.Substring(6, 4);//yyyy
                String mes = Fechauno.Substring(3, 2);//mm
                String dia = Fechauno.Substring(0, 2);//dd
                //retorna yyyy-mm-dd
                return año + "-" + mes + "-" + dia;
            }
            else
            {
                return "";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(comboBoxNprioridades.Text) == 0 && comboboxSubtipo.Text == "PCT")
            //{//se habilita el recuadro de prioridades para uno sólo para PCT
            //    groupPrioridades.Enabled = true;
            //}
            //else
            //{
            //    if (Convert.ToInt32(comboBoxNprioridades.Text) == 0 && comboboxSubtipo.Text != "PCT")
            //    {//se habilita para uno
            //        groupPrioridades.Enabled = false;
            //    }
            //    else
            //    {
            //        if (Convert.ToInt32(comboBoxNprioridades.Text) > 0 && comboboxSubtipo.Text != "Regular")
            //        {
            //            groupPrioridades.Enabled = true;
            //        }
            //    }
            //}
        }

        private void comboboxSubtipo2_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            addClientenuevo aClientenuevo = new addClientenuevo(this);
            if (aClientenuevo.ShowDialog() == DialogResult.OK)
            {
                comboBoxClientes.Items.Add(aClientenuevo.Cliente);
                comboBoxContacto.Items.Add(aClientenuevo.Contacto);
                comboBoxClientes.Text = aClientenuevo.Cliente.Text;
                comboBoxContacto.Text = aClientenuevo.Contacto.Text;
                richTextBoxDireccliente.Text = "";//
                richTextBox1.Text = aClientenuevo.Contactocorreo;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            addnuevotitular addnuevotitular = new addnuevotitular(this, login, TipoSol);
            if (addnuevotitular.ShowDialog() == DialogResult.OK)
            {
                comboBoxInteresado.Items.Add(addnuevotitular.cBcomboInteresadotitular);
                comboBoxInteresado.Text = addnuevotitular.sNombrenuevotitular;
            }
        }

        private void bEliminarprioridades_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewPrioridades.SelectedItems.Count > 0)
                {
                    /*if (listViewPrioridades.SelectedItems[0].SubItems[3].Text == "PCT")
                    {
                        bPrioridadpctagregada = true;
                    }*/
                    listViewPrioridades.SelectedItems[0].Remove();

                    //textBoxFechainternacional.Text = "";
                    textBoxPlazolegal.Text = "";
                    //validarfechalegal();
                    /*if (listViewPrioridades.Items.Count == 0)//quiere decir que ya no hay prioridades
                    {
                        dFechaprioridadmenor = Convert.ToDateTime("01-01-1900");
                    }*/
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una prioridad para eliminarla de la lista.");
                }
            }
            catch (Exception E)
            {
                new filelog(login.sId, E.ToString());
            }
            //    try
            //    {
            //        if (listViewPrioridades.SelectedItems.Count > 0)
            //        {

            //            listViewPrioridades.SelectedItems[0].Remove();

            //            textBoxFechainternacional.Text = "";
            //            textBoxPlazolegal.Text = "";
            //            validarfechalegal();
            //            if (listViewPrioridades.Items.Count == 0)//quiere decir que ya no hay prioridades
            //            {
            //                dFechaprioridadmenor = Convert.ToDateTime("01-01-1900");
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("Debe seleccionar una prioridad para eliminarla de la lista.");
            //        }
            //    }
            //    catch (Exception E)
            //    {
            //        new filelog(login.sId, E.ToString());
            //    }
            //}




        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            try
            {
                String datocliente = (comboBoxClientes.SelectedItem as ComboboxItem).Value.ToString();
                if (datocliente != "")
                {
                    Fclientedetalle detalle = new Fclientedetalle(datocliente, login, cFcaptura);
                    detalle.ShowDialog();
                    //this.Hide();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Debe seleccionar un cliente para agregar el contacto.");
            }
        }

        private void cbPaiscaso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int iValuepais = Convert.ToInt32((cbPaiscaso.SelectedItem as ComboboxItem).Value.ToString());
                tbClavepaiscaso.Text = paises[iValuepais];
            }
            catch (Exception eX)
            {
                new filelog("casonuevo", eX.Message);
            }
        }

        private void tbClavepaiscaso_TextChanged(object sender, EventArgs e)
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
                cbPaiscaso.Text = paisesclave[iValoractual];
            }
            catch (Exception Ex)
            {
                new filelog("casnuevo", Ex.Message);
            }
        }

        private void cbCvpais_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                int iValuepais = Convert.ToInt32((cbCvpais.SelectedItem as ComboboxItem).Value.ToString());
                comboBoxPais.Text = paisesclave[iValuepais];
            }
            catch (Exception Ex)
            {
            }

        }

        public bool bCasoprioridades { get; set; }

        private void TexboxFecha_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(TexboxFecha);
        }

        private void textClientduedate_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(textClientduedate);
        }

        private void textBoxPlazolegal_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(textBoxPlazolegal);
        }

        private void textBoxFecha_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(textBoxFecha);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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


            if (tbFecharegistrointernacional.Text.Length == 2)
            {
                tbFecharegistrointernacional.Text = tbFecharegistrointernacional.Text + "-";
                tbFecharegistrointernacional.SelectionStart = tbFecharegistrointernacional.Text.Length;

            }
            if (tbFecharegistrointernacional.Text.Length == 5)
            {
                tbFecharegistrointernacional.Text = tbFecharegistrointernacional.Text + "-";
                tbFecharegistrointernacional.SelectionStart = tbFecharegistrointernacional.Text.Length;
            }
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFecharegistrointernacional);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBoxDireccliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void richTextBoxTitulo_TextChanged(object sender, EventArgs e)
        {

        }

        private void TexboxFecha_KeyPress_1(object sender, KeyPressEventArgs e)
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


            if (TexboxFecha.Text.Length == 2)
            {
                TexboxFecha.Text = TexboxFecha.Text + "-";
                TexboxFecha.SelectionStart = TexboxFecha.Text.Length;

            }
            if (TexboxFecha.Text.Length == 5)
            {
                TexboxFecha.Text = TexboxFecha.Text + "-";
                TexboxFecha.SelectionStart = TexboxFecha.Text.Length;
            }
        }

        private void textClientduedate_KeyPress(object sender, KeyPressEventArgs e)
        {
            validafechaformato(textClientduedate, sender, e);
        }

        private void tbFechalegal_KeyPress(object sender, KeyPressEventArgs e)
        {
            validafechaformato(tbFechalegal, sender, e);
        }

        //private void tbFecharecepcion_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    validafechaformato(tbFecharecepcion, sender, e);
        //}

        private void tbFechaconsecion_KeyPress(object sender, KeyPressEventArgs e)
        {
            validafechaformato(tbFechaconsecion, sender, e);
        }

        private void tbFechaprobouso_KeyPress(object sender, KeyPressEventArgs e)
        {
            validafechaformato(tbFechaprobouso, sender, e);
        }

        public void validafechaformato(TextBox tbFecha, object sender, KeyPressEventArgs e) {
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


            if (tbFecha.Text.Length == 2)
            {
                tbFecha.Text = tbFecha.Text + "-";
                tbFecha.SelectionStart = tbFecha.Text.Length;

            }
            if (tbFecha.Text.Length == 5)
            {
                tbFecha.Text = tbFecha.Text + "-";
                tbFecha.SelectionStart = tbFecha.Text.Length;
            }

        }

        private void tbFechaInicioUso_KeyPress(object sender, KeyPressEventArgs e)
        {
            validafechaformato(tbFechaInicioUso, sender, e);
        }

        private void tbFechavigencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            validafechaformato(tbFechavigencia, sender, e);
        }

        private void TexboxFecha_Validating_1(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(TexboxFecha);
        }

        private void textClientduedate_Validating_1(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(textClientduedate);
        }

        private void tbFechalegal_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechalegal);
        }
        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(textBox1);
        }
        //private void tbFecharecepcion_Validating(object sender, CancelEventArgs e)
        //{
        //    objfuncionesdicss.validafecha(tbFecharecepcion);
        //}

        private void tbFechaconsecion_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechaconsecion);
        }

        private void tbFechaprobouso_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechaprobouso);
        }

        private void tbFechaInicioUso_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechaInicioUso);
        }

        private void tbFechavigencia_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFechavigencia);
        }

        private void tbEstatus_header_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                updateEstatus updateestatus = new updateEstatus("2");
                if (updateestatus.ShowDialog() == DialogResult.OK)
                {
                    String value = updateestatus.sValueestatus;//hacemos el update con este valor y ponemos el Texto en el form
                    String texti = updateestatus.sTextoestatus;//hacemos el update con este valor y ponemos el Texto en el form
                    //conect con_4 = new conect();
                    sEstatusid = value;
                    //String updateestatuscaso = "UPDATE `caso_marcas` SET `EstatusCasoId` = '" + value + "' WHERE (`CasoId` = '" + sCasoId + "' and TipoSolicitudId = " + gSTipoSolicitudId + ");";
                    //MySqlDataReader resp_updatecaso = con_4.getdatareader(updateestatuscaso);
                    //if (resp_updatecaso != null)
                    //{
                    //tbEstatus.Text = texti;
                    tbEstatus_header.Text = texti;
                    //}
                    //resp_updatecaso.Close();
                    //con_4.Cerrarconexion();
                }
            }
            catch (Exception Ex)
            {
                new filelog("linea", "Error: " + Ex.Message);
            }
        }

        private void tbFecharegistrointernacional_KeyPress(object sender, KeyPressEventArgs e)
        {
            validafechaformato(tbFecharegistrointernacional, sender, e);
        }

        private void tbFecharegistrointernacional_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(tbFecharegistrointernacional);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            cFcaptura.Show();
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
            cFcaptura.Close();
            login.Close();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            addnuevotitular addnuevotitular = new addnuevotitular(this, login, TipoSol);
            if (addnuevotitular.ShowDialog() == DialogResult.OK)
            {
                comboBoxInteresado.Items.Add(addnuevotitular.cBcomboInteresadotitular);
                comboBoxInteresado.Text = addnuevotitular.sNombrenuevotitular;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label61_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            validafechaformato(textBox1, sender, e);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
