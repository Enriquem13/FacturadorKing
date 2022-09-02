using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturador
{
    public partial class Nuevocaso_marcas : Form
    {

        public Form1 login;
        public captura cFcaptura;
        public String[] paises = new String[250];
        public String[] paisesclave = new String[250];
        public int sTiposolicitudiscaso;
        funcionesdicss objfuncionesdicss;

        //public static conect conect;
        public Nuevocaso_marcas(int iTiposolicitud, captura cap, Form1 loguinparam)
        {
            try
            {
                login = loguinparam;
                cFcaptura = cap;
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

                String sQresponsable = "select UsuarioNombre, UsuarioId from usuario where UsuarioIndActivo = 1";
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
                String query2 = "Select  distinct cliente.ClienteId,casocliente.ContactoId, concat_ws(' ', cliente.NombreUtilClient,cliente.ClienteApellidoPaterno,cliente.ClienteApellidoMaterno)as NombreUtilClient from casocliente, cliente where " +

                        //" casocliente.TipoSolicitudId = " + iTiposolicitud +
                        " cliente.clienteid =  casocliente.clienteid group by cliente.ClienteId order by NombreUtilClient;";
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

                //select InteresadoID, InteresadoNombre from interesado;
                conect conect_5 = new conect();
                String query4 = " SELECT  " +
                                "     Interesado.InteresadoId, concat_ws(' ',InteresadoNombre,InteresadoApPaterno,InteresadoApMaterno) as InteresadoNombre " +
                                " FROM " +
                                "     interesado, " +
                                "     casointeresado " +
                                " WHERE " +
                                "     casointeresado.InteresadoId = Interesado.InteresadoId " +
                                "     and casointeresado.TipoRelacionId in(1, 3) " +
                                " group by Interesado.InteresadoId " +
                                " ORDER BY InteresadoNombre; ";
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

        public void creamulticaso() {
            try {
                if (cbMulticaso.Text!="") {
                    

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
            catch (Exception exs) {
                new filelog("error linea 204", exs.StackTrace);
            }
        }
        public String sNumerosdecaso = "";
        public void creacasomarcas() {
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

                // String sComboBoxClase = (comboBoxClase.SelectedItem as ComboboxItem).Value.ToString();//pendiente
                String sComboBoxIdioma = validacombobox(comboBoxIdioma);//.SelectedItem as ComboboxItem).Value.ToString();//*
                String sCcomboBoxFirma = validacombobox(comboBoxFirma);//.SelectedItem as ComboboxItem).Value.ToString();//pendiente
                String sComboBoxResponsable = validacombobox(comboBoxResponsable);//.SelectedItem as ComboboxItem).Value.ToString();//*
                String Id_Marca = validacombobox(Tipo_Marca1);

                //Dar formatos de fecha 
                String sTextboxFecha = TexboxFecha.Text;//Fecha carta   CasoFechaCartaCliente *
                String stextClientduedate = textClientduedate.Text;//CasoFechaFilingCliente client due date *

                String Fecharegistrointernacional = tbFecharegistrointernacional.Text;
                String numregistrointernacional = tbNumregistrointernacional.Text;

                String stextBoxPlazolegal = textBoxPlazolegal.Text;//textBoxPlazolegalcasofechalegal  CasoFechaLegal
                String stextBoxFechainternacional = "";// textBoxFechainternacional.Text;//CasoFechaInternacional fecha internacional
                String stextBoxReferencia = textBoxReferencia.Text;//pendiente
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
                //validamos fecha, referencia, titulo, tiposolicitud. cliente, contacto, interesado
                if (sTextboxFecha != "" && stextBoxReferencia != "" && srichTextBoxTitulo != "" && sComboTiposolicitud != "" && sComboBoxClientes != "" && sComboBoxInteresado != "")
                {
                    sTextboxFecha = cambiaformatofecha(sTextboxFecha);
                    stextClientduedate = cambiaformatofecha(stextClientduedate);
                    Fecharegistrointernacional = cambiaformatofecha(Fecharegistrointernacional);
                    stextBoxPlazolegal = cambiaformatofecha(stextBoxPlazolegal);
                    stextBoxFechainternacional = cambiaformatofecha(stextBoxFechainternacional);

                    if (Fecharegistrointernacional=="") {
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
                    String sEstatudID = "1"; // por default EstatuscasoID
                    int iIdpaiscaso = 0;
                    for (int yuno = 0; yuno < paises.Length; yuno++)
                    {
                        if (paises[yuno] == tbClavepaiscaso.Text)
                        {
                            iIdpaiscaso = yuno;

                        }
                    }

                    //pendiente numregistrointernacional puede ser opcional porque rompe el query
                    String TipoCaptura = "1";
                    //inserta caso_ marcas
                    conect con_2 = new conect();
                    String insert_marcas = "INSERT INTO `caso_marcas` " +
                                             "(`CasoId`, " +
                                             "`TipoSolicitudId`, " +
                                             "`TipoMarcaId`, " +
                                             //"`SubTipoSolicitudId`, " +
                                             "`CasoTituloespanol`, " +
                                             "`CasoTituloingles`, " +
                                             "`IdiomaId`, " +
                                             "`CasoNumero`, " +
                                             "`ResponsableId`, " +


                                             "`numregistrointernacional`, " +//campos nuevos
                                             "`Fecharegistrointernacional`, " +//campos nuevos

                                             "`CasoFechaAlta`, " +
                                             "`CasoFechaCartaCliente`, " +

                                             "`CasoTipoCaptura`," +
                                             "`CasoFechaFilingSistema`, " +
                                             "`CasoFechaFilingCliente`, " +
                                             "`EstatusCasoId`, " +
                                             "`UsuarioId`, " +
                                             "`Id_Ley`," +
                                             "`PaisId`) " +
                                             "VALUES " +
                                             "(NULL, " +
                                             "'" + sComboTiposolicitud + "', " +
                                             "'" + Id_Marca + "', " +
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
                                             "'" + TipoCaptura + "', " +

                                             "'" + sTextboxFecha + "', " +
                                             "'" + stextClientduedate + "', " +

                                             "'" + sEstatudID + "', " +
                                             "'" + sresponsableid + "', " +
                                             "'" + sID_Ley + "', " +
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



                    if (bCaso && bCasocliente && bcasointeresado && breferencias && cbMulticaso.Text=="")
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
                            //si creamos un nuevo caso de marcas debemos abrir el caso de marcas creado
                            //sCasoid
                            bMarcas obmarcas = new bMarcas(2, cFcaptura, login);
                            fTmarcas oFtmarcas = new fTmarcas(login, cFcaptura, obmarcas, sCasoid);
                            //code for No
                            cFcaptura.Show();
                            oFtmarcas.Show();
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
                        sNumerosdecaso += " " + sCasonumero +" ";

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
            try {
                // primero hacemos las validaciones y luego hacemos la pregunta de confirmación de multiples casos
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


                if (cbMulticaso.Text == "")
                {
                    creacasomarcas();
                }
                else
                {
                    sNumerosdecaso = "";
                    var confirmResult = MessageBox.Show("¿Seguro que desea agregar " + cbMulticaso.Text + " casos con la misma información?", "Agregar Multiples casos",
                                         MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        creamulticaso();
                        MessageBox.Show("Los números de casos creados son:" + sNumerosdecaso);
                        cFcaptura.Show();
                        this.Close();
                    }
                    else {
                        cbMulticaso.Focus();
                    }

                }
            }
            catch (Exception exs) {
                new filelog(" error cre caso linea 666: ", exs.StackTrace);

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
                String query = "select * from ley where ID_Ley= 1" ;
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

        public void ComboTipomarca_SelectedIndexChanged(string seccion)
        {

            Tipo_Marca1.Items.Clear();
            Tipo_Marca1.Text = "";
            string secc = seccion;
            if (secc != "8")

            {
                if (secc != "9")
                {
                    try
                    {

                        Tipo_Marca1.Items.Clear();
                        Tipo_Marca1.Text = "";

                        conect con = new conect();
                        String query = "select TipoMarcaId, TipoMarcaDescrip from tipomarca where TipoMarcaIndAct =" + "1";
                        MySqlDataReader respuestastring = con.getdatareader(query);
                        while (respuestastring.Read())
                        {
                            Tipo_Marca1.Items.Add(validareader("TipoMarcaDescrip", "TipoMarcaId", respuestastring));
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


        /* private void comboboxSubtipo_SelectedIndexChanged(object sender, EventArgs e)
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
            try {
                int iValuepais = Convert.ToInt32((comboBoxPais.SelectedItem as ComboboxItem).Value.ToString());
                cbCvpais.Text = paises[iValuepais];
            }catch(Exception ex){
                
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
        public void agregaprioridades() {
            

        }
        private void pictureBox1_Click(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
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
            addnuevotitular addnuevotitular = new addnuevotitular(this);
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

        private void button7_Click(object sender, EventArgs e)
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

        private void cbCvpais_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                int iValuepais = Convert.ToInt32((cbCvpais.SelectedItem as ComboboxItem).Value.ToString());
                comboBoxPais.Text = paisesclave[iValuepais];
            }catch(Exception Ex){
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
    }
}