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
    public partial class Nuevocaso : Form
    {
        public Form1 login;
        public captura cFcaptura;
        public String []paises = new String[250];
        public String[] paisesclave = new String[250];
        public String sGrupoId;
        public int iGrupoid;
        public String[] aIdtipoprioridad = new String[250];
        public bool bPrioridadpctagregada = true;
        public int TipoSol;
        funcionesdicss objfuncionesdicss = new funcionesdicss();

        public Boolean bcasopatente = false, bCasocliente = false, bCasointeresado = false, bCasoreferencias = false;
        
        //public static conect conect;
        //Form1 loguinparam, captura cap, String sGrupoID
        public Nuevocaso(int iTiposolicitud, captura cap, Form1 loguinparam)
        {
            login = loguinparam;
            cFcaptura = cap;
            iGrupoid = iTiposolicitud;
            sGrupoId = iTiposolicitud +"";
            InitializeComponent();
            listViewPrioridades.FullRowSelect = true;
            ComboboxItem item = new ComboboxItem();
            item.Text = login.sUsername;
            item.Value = login.sId;
            groupPrioridades.Enabled = false;
            /*comboBoxResponsable.Items.Add(item);
            comboBoxResponsable.Text = login.sUsername;*/
            //Consultamos los usuarios para asignar el responsable
            conect conect = new conect();
            String sQresponsable = "select UsuarioNombre, UsuarioId from usuario where UsuarioIndActivo = 1;";
            MySqlDataReader respuresponsable = conect.getdatareader(sQresponsable);
            while (respuresponsable.Read())
            {
                comboBoxResponsable.Items.Add(validareader("UsuarioNombre", "UsuarioId", respuresponsable));
            }
            comboBoxResponsable.Text = login.sUsername;
            comboBoxResponsable.SelectedValue = login.sId;
            respuresponsable.Close();
            conect.Cerrarconexion();
            //consultamos el tipo de solicitud
            conect conect_solicitud = new conect();
            String query = "select TipoSolicitudDescrip, TipoSolicitudDescrip, TipoSolicitudId from tiposolicitud where tiposolicitudGrupo = " + iTiposolicitud;
            MySqlDataReader respuestastring = conect_solicitud.getdatareader(query);
            while (respuestastring.Read())
            {
                comboTiposolicitud.Items.Add(validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestastring));
            }
            respuestastring.Close();
            conect_solicitud.Cerrarconexion();
            //String query_R = "select TipoSolicitudDescrip, TipoSolicitudDescrip, TipoSolicitudId from tiposolicitud;";
            //MySqlDataReader respuestastring_R = conect.getdatareader(query_R);

            //while (respuestastring_R.Read())
            //{
            //    comboTiposolicitud.Items.Add(validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestastring_R));
            //}
            //respuestastring_R.Close();
            //Calculamos la fecha actual
            DateTime Hoy = DateTime.Today;
            string fecha_actual = Hoy.ToString("dd-MM-yyyy");
            TexboxFecha.Text = fecha_actual;
            //Consultamos el catálogo de clientes
            conect conect_clientes = new conect();
            String query2 = "select ClienteNombre, ClienteId from cliente order by cliente.ClienteNombre;";
            MySqlDataReader respuestastringclient = conect_clientes.getdatareader(query2);
            while (respuestastringclient.Read())
            {
                comboBoxClientes.Items.Add(validareader("ClienteNombre", "ClienteId", respuestastringclient));
            }
            respuestastringclient.Close();
            conect_clientes.Cerrarconexion();

            //Consultamos los idiomas
            conect conect_idiomas = new conect();
            String query3 = "select IdiomaId, IdiomaDescripcion from idioma;";
            MySqlDataReader respuestastringidiom = conect_idiomas.getdatareader(query3);
            while (respuestastringidiom.Read())
            {
                comboBoxIdioma.Items.Add(validareader("IdiomaDescripcion", "IdiomaId", respuestastringidiom));
            }
            respuestastringidiom.Close();
            conect_idiomas.Cerrarconexion();

            //Consultamos el catálogo de interesados
            conect conect_interesados = new conect();
            String query4 = " SELECT  " +
                            "     Interesado.InteresadoId, InteresadoNombre " +
                            " FROM " +
                            "     interesado, " +
                            "     casointeresado " +
                            " WHERE " +
                            "     casointeresado.InteresadoId = Interesado.InteresadoId " +
                            "     and casointeresado.TipoRelacionId in(1, 3) " +
                            " group by Interesado.InteresadoId " +
                            " ORDER BY InteresadoNombre; ";
            MySqlDataReader respuestastringointeresado = conect_interesados.getdatareader(query4);
            while (respuestastringointeresado.Read())
            {
                comboBoxInteresado.Items.Add(validareader("InteresadoNombre", "InteresadoID", respuestastringointeresado));
            }
            respuestastringointeresado.Close();
            conect_interesados.Cerrarconexion();

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
            

            //Consultamos los responsables disponibles en la tabla responsable
            conect conect_Responsable = new conect();
            String sResponsablequery = "select ResponsableClave, ResponsableId, ResponsableNombre from responsable;";
            MySqlDataReader respuestastrinresponsable = conect_Responsable.getdatareader(sResponsablequery);
            //int paisint = 0;
            while (respuestastrinresponsable.Read())
            {
                comboBoxFirma.Items.Add(validareader("ResponsableNombre", "ResponsableId", respuestastrinresponsable));
                comboBoxFirma.Text = validareader("ResponsableNombre", "ResponsableId", respuestastrinresponsable).Text;
                //paisint++;
            }
            respuestastrinresponsable.Close();
            conect_Responsable.Cerrarconexion();
            //comboBoxFirma.

            //Seleccionamos el país por Default
            cbPaiscaso.Text = "MEXICO";
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
        public String validacombobox(ComboBox combo){
            if(combo.SelectedItem !=null){
                return (combo.SelectedItem as ComboboxItem).Value.ToString();
            }else{
                return "";
            }
         
        }
        private String validafechacorectaformato(String Fechaentrada, String sFormatoentrada, String sFechaSalida)
        {//verificamos que es una fecha valida y la convertimos a un formato date mysql
            String sFechasalida = "";
            try
            {
                sFechasalida = DateTime.ParseExact(Fechaentrada, sFormatoentrada, CultureInfo.InvariantCulture).ToString(sFechaSalida); //tbDocumentofecharecepcion.Text;
            }
            catch (Exception E)
            {
                sFechasalida = "";
            }
            return sFechasalida;
        }
        private void button1_Click(object sender, EventArgs e)//para insertar un nuevo caso
        {
            try {
                //
                //hay que validar cuales campos son obligatorios y cuals son opcionales
                String sComboTiposolicitud = validacombobox(comboTiposolicitud);//*
                TipoSol = Convert.ToInt32(validacombobox(comboTiposolicitud));
                //String sComboTiposolicitud = (comboTiposolicitud.SelectedItem as ComboboxItem).Value.ToString();//*
                String sComboboxSubtipo = validacombobox(comboboxSubtipo);//.SelectedItem as ComboboxItem).Value.ToString();//*
                String sComboboxSubtipo2 = validacombobox(comboboxSubtipo2);//.SelectedItem as ComboboxItem).Value.ToString();//*
                //String sComboboxFecha = (comboboxFecha.SelectedItem as ComboboxItem).Value.ToString();//

                String sComboBoxClientes = validacombobox(comboBoxClientes);//.SelectedItem as ComboboxItem).Value.ToString();//casocliente
                String sComboBoxContacto = validacombobox(comboBoxContacto);//.SelectedItem as ComboboxItem).Value.ToString();//casocliente
                String sComboBoxInteresado = validacombobox(comboBoxInteresado);//.SelectedItem as ComboboxItem).Value.ToString();//casointeresado
                String sStringComboBoxInteresado = "";
                
                /***
                 * comboTiposolicitud
                 */
                
                if (comboBoxInteresado.SelectedItem != null)
                {
                    sStringComboBoxInteresado = (comboBoxInteresado.SelectedItem as ComboboxItem).Text;
                }
                //String sComboBoxClase = (comboBoxClase.SelectedItem as ComboboxItem).Value.ToString();//pendiente
                String sComboBoxIdioma = validacombobox(comboBoxIdioma);//.SelectedItem as ComboboxItem).Value.ToString();//*
                String sCcomboBoxFirma = validacombobox(comboBoxFirma);//.SelectedItem as ComboboxItem).Value.ToString();//pendiente
                String sComboBoxResponsable = validacombobox(comboBoxResponsable);//.SelectedItem as ComboboxItem).Value.ToString();//*
                //Dar formatos de fecha 
                String sTextboxFecha = TexboxFecha.Text;//Fecha carta   CasoFechaCartaCliente *
                String stextClientduedate = textClientduedate.Text;//CasoFechaFilingCliente client due date *
                String stextBoxPlazolegal = textBoxPlazolegal.Text;//textBoxPlazolegalcasofechalegal  CasoFechaLegal
                String stextBoxFechainternacional = textBoxFechainternacional.Text;//CasoFechaInternacional fecha internacional
                String stextBoxReferencia = textBoxReferencia.Text;//pendiente
                String srichTextBoxTitulo = richTextBoxTitulo.Text;//Casotitulo
                String sresponsableid = login.sId;
                String sTituloespanol = "";
                String sTituloIngles = "";
                String sFechasistema = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                /*if (stextBoxPlazolegal != "" && (comboTiposolicitud.SelectedItem as ComboboxItem).Value != "3" && (comboTiposolicitud.SelectedItem as ComboboxItem).Value != "4")
                {//decimos que es diferente de diseños, entonces le sumamos 8 meses
                    DateTime dtextBoxPlazolegal = DateTime.Parse(stextBoxPlazolegal);
                    dtextBoxPlazolegal = dtextBoxPlazolegal.AddMonths(12);
                }
                else {
                    if ((comboTiposolicitud.SelectedItem as ComboboxItem).Value == "3" && (comboTiposolicitud.SelectedItem as ComboboxItem).Value == "4")
                    {
                        DateTime dtextBoxPlazolegal = DateTime.Parse(stextBoxPlazolegal);
                        dtextBoxPlazolegal = dtextBoxPlazolegal.AddMonths(6);
                    }
                }*/

                //validar que no sea menor a la fecha de hoy textClientduedate
                try
                {
                    if (textClientduedate.Text=="")
                    {
                        MessageBox.Show("El campo de la Fecha Client due date no puede estar vacia.");
                        return;
                    }
                    String svaltextClientduedate = textClientduedate.Text;
                    DateTime oDate = DateTime.Parse(svaltextClientduedate);

                    DateTime FechAc = DateTime.Now.Date;
                    if (oDate < FechAc) // Si la fecha indicada es menor o igual a la fecha actual
                    {
                        MessageBox.Show("Le fecha Client due date (Fecha en que debe presentarse ante IMPI) no puede ser menor a la fecha acutal.");
                        if (textClientduedate.CanFocus)
                        {
                            textClientduedate.Focus();
                        }
                        return;
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Le fecha Client due date es incorrecta.");
                    if (textClientduedate.CanFocus)
                    {
                        textClientduedate.Focus();
                    }
                    return;
                }

                if (sComboBoxIdioma == "2"){//está en español
                    sTituloespanol = srichTextBoxTitulo;
                    //srichTextBoxTitulo = "";
                }else{
                    sTituloIngles = srichTextBoxTitulo;
                }

                if (comboBoxIdioma.Text =="") {
                    MessageBox.Show("Debe seleccionar un idioma para el caso");
                    return;
                }
                //validamos los campos que son obligatorios para poder agregar el caso
                //validamos fecha, referencia, titulo, tiposolicitud. cliente, contacto, interesado
                if (tbClavepaiscaso.Text!="" && sTextboxFecha != "" && stextBoxReferencia != "" && srichTextBoxTitulo != "" && sComboTiposolicitud != "" && sComboBoxClientes != "" && sComboBoxInteresado != "")
                {
                    if (validafechacorecta(TexboxFecha.Text) != "" && validafechacorecta(textClientduedate.Text) != "")
                    {
                        if (sComboboxSubtipo != "3")//Regular ley local, no debemos validar la fecha internacional
                        {
                            String sValorsubtipo = (comboboxSubtipo.SelectedItem as ComboboxItem).Value.ToString();
                            if (validafechacorectaformato(textBoxFechainternacional.Text, "dd-MM-yyyy", "yyyy'/'MM'/'dd") == "" && 
                                (comboboxSubtipo.SelectedItem as ComboboxItem).Value.ToString()=="1")//hay que validar si es pct
                            {
                                MessageBox.Show("Verifique la fecha Internacional.");
                                return;
                            }
                        }

                        /**
                         * Validamos si el numero de referencia ya existe
                         * y validamos si ya existe el número de prioridades
                         * 
                         */
                            int iNum_ref = 0;
                            conect con_ref = new conect();
                            String sGetcasonumero_ref = "select count(*) as Num_ref from referencia where ReferenciaNombre like '%" + textBoxReferencia.Text + "%';";
                            MySqlDataReader respuestastringcasonum_ref = con_ref.getdatareader(sGetcasonumero_ref);
                            while (respuestastringcasonum_ref.Read())
                            {
                                iNum_ref = int.Parse(validareader("Num_ref", "Num_ref", respuestastringcasonum_ref).Text);
                            }
                            respuestastringcasonum_ref.Close();
                            con_ref.Cerrarconexion();

                            if (iNum_ref > 0)
                            {
                                MessageBox.Show("La referencia Ya existe, debe agregar una distinta.");
                                return;
                            }
                        /**
                         * Fin
                         * Validamos si el numero de referencia ya existe
                         * y validamos si ya existe el número de prioridades
                         */

                        sTextboxFecha = cambiaformatofecha(sTextboxFecha);
                        stextClientduedate = cambiaformatofecha(stextClientduedate);
                        stextBoxPlazolegal = cambiaformatofecha(stextBoxPlazolegal);
                        stextBoxFechainternacional = cambiaformatofecha(stextBoxFechainternacional);
                        //Consultamos el último insertado casonumero para poder generar el nuevo caso número del siguiente caso.
                        conect con = new conect();
                        String sGetcasonumero = "select casoid, casonumero from caso_patente order by casoid desc limit 1;";
                        MySqlDataReader respuestastringcasonum = con.getdatareader(sGetcasonumero);
                        String sCasonumero = "";
                        while (respuestastringcasonum.Read())
                        {
                            sCasonumero = validareader("casonumero", "casoid", respuestastringcasonum).Text.ToString();
                        }
                        respuestastringcasonum.Close();
                        con.Cerrarconexion();

                        if (sCasonumero.Length == 7)
                        {
                            String extencion = sCasonumero.Substring(4, 3);
                            sCasonumero = sCasonumero.Substring(0, 4);
                            int iValor = Int32.Parse(sCasonumero) + 1;
                            sCasonumero = iValor + extencion;
                        }
                        else
                        {
                            if (sCasonumero.Length == 4)
                            {
                                int iValor = Int32.Parse(sCasonumero) + 1;
                                sCasonumero = iValor + "";
                            }
                            else
                            {
                                MessageBox.Show("El último caso de éste tipo es: " + sCasonumero + " y no se reconoce el formato");
                            }
                        }
                        //obtenemos el id del pais
                        int iIdpaiscaso = 0;
                        for (int yuno = 0; yuno < paises.Length; yuno++)
                        {
                            if (paises[yuno] == tbClavepaiscaso.Text)
                            {
                                iIdpaiscaso = yuno;
                                
                            }
                        }

                        //validamos fechas vacias
                        String sFechaintenacionalinsert = "";
                        if (textBoxFechainternacional.Text == "") {
                            sFechaintenacionalinsert = "null";
                        }
                        else {
                            sFechaintenacionalinsert = "'"+validafechacorectaformato(textBoxFechainternacional.Text, "dd-MM-yyyy", "yyyy'/'MM'/'dd")+"'";
                        }

                        if (stextBoxPlazolegal == "")
                        {
                            stextBoxPlazolegal = "null";
                        }
                        else {
                            stextBoxPlazolegal = "'"+ stextBoxPlazolegal + "'";
                        }

                        //validamos tipo pct
                        if (sComboboxSubtipo2 == "")
                        {
                            sComboboxSubtipo2 = "0";
                        }

                        String sEstatudID = "1"; // por default EstatuscasoID
                        //inserta el nuevo caso en la tabla caso_ patente
                        conect conect_caso = new conect();
                        String sQueryinsertpat = "INSERT INTO `caso_patente` (`CasoId`, " +
                                                                                    " `TipoSolicitudId`, " +
                                                                                    " `SubTipoSolicitudId`, " +
                                                                                    " `TipoPctId`, " +
                                                                                    " `CasoTituloespanol`, " +
                                                                                    " `CasoTituloingles`, " +
                                                                                    " `IdiomaId`, " +
                                                                                    " `CasoFechaLegal`, " +

                                                                                    
                                                                                    
                                                                                    
                                                                                    
                                                                                    " `CasoNumero`, " +
                                                                                    " `ResponsableId`, " +


                                                                                    
                                                                                    " `CasoTitular`, " +
                                                                                    " `EstatusCasoId`, " +

                                                                                    " `UsuarioId`, " +
                                                                                    " `CasoFechaInternacional`, " +
                                                                                    " `PaisId`, " +
                                                                                    " `CasoFechaFilingCliente`, " +
                                                                                    " `CasoFechaFilingSistema`, " +
                                                                                    " `CasoFechaCartaCliente`)  " +
                                                                                    " VALUES " +
                                                                                    " (null, " +
                                                                                    " '" + sComboTiposolicitud + "', " +
                                                                                    " '" + sComboboxSubtipo + "', " +
                                                                                    " " + sComboboxSubtipo2 + ", " +
                                                                                    " '" + sTituloespanol + "', " +
                                                                                    " '" + sTituloIngles + "', " +
                                                                                    " '" + sComboBoxIdioma + "', " + 
                                                                                    " " + stextBoxPlazolegal + ", " +//CASOFECHALEGAL
                                                                                    " '" + sCasonumero + "', " +
                                                                                    " '" + sresponsableid + "', " +
                                                                                    " '" + sStringComboBoxInteresado + "', " +
                                                                                    " '1', " +
                                                                                    " '" + sresponsableid + "', " +

                                                                                    " " + sFechaintenacionalinsert + ", " +//Fecha Internacional
                                                                                    " '" + iIdpaiscaso + "', " +//aqui va pais
                                                                                    " '" + stextClientduedate + "', " +
                                                                                    " '" + sFechasistema + "', " +
                                                                                    " '" + sTextboxFecha + "'); ";
                        //Consultamos el último caso insertado para obtener su casoid
                        conect conect_consultacaso = new conect();
                        String sGetid = "SELECT * FROM `caso_patente` order by CasoId desc limit 1";
                        MySqlDataReader respuestastring = conect_caso.getdatareader(sQueryinsertpat);
                        respuestastring.Close();
                        conect_caso.Cerrarconexion();
                        MySqlDataReader respuestastringid = conect_consultacaso.getdatareader(sGetid);
                        String sCasoid = "";
                        while (respuestastringid.Read())
                        {
                            sCasoid = validareader("CasoId", "CasoId", respuestastringid).Value.ToString();
                            bcasopatente = true;
                        }
                        respuestastringid.Close();
                        conect_consultacaso.Cerrarconexion();

                        //Agregamos al cliente insertando en casocliente
                        conect conect_cliente = new conect();
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
                                                sCasoid + ", " +
                                                sComboTiposolicitud + ", " +
                                                "0);";
                        MySqlDataReader respuestastringinsertclient = conect_cliente.getdatareader(insertacliente);

                        if (respuestastringinsertclient.RecordsAffected == 1)
                        {
                            //MessageBox.Show("Se inserto en casoclientes");
                            bCasocliente = true;
                        }
                        respuestastringinsertclient.Close();
                        conect_cliente.Cerrarconexion();

                        //Agregamos el interesado al caso en su tabla casointeresado
                        conect conect_interesado = new conect();
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
                                                    sComboBoxInteresado + ", " +
                                                    sCasoid + ", " +
                                                    sComboTiposolicitud + ", " +
                                                    "1," +
                                                    "1," +
                                                    "null);";
                        MySqlDataReader respuestastringinscasoint = conect_interesado.getdatareader(insertcasointeresado);
                        if (respuestastringinscasoint.RecordsAffected == 1)
                        {
                            //MessageBox.Show("Se inserto en casointeresado");
                            bCasointeresado = true;
                        }
                        respuestastringinscasoint.Close();
                        conect_interesado.Cerrarconexion();

                        /*Debe Seleccionar una dirección de lo contrario no habrá relación*/
                        /*cambio de comentar esta linea 30_09_2021*/
                        //FSelectdireccionint obj = new FSelectdireccionint(sComboBoxInteresado, sCasoid, sCasonumero, "Titular", "", sComboTiposolicitud);
                        //obj.ShowDialog();

                        //agregar a¿rowaffected y validar que se inserto
                        //Agregamos la Referencia capturada
                        conect conect_ref = new conect();
                        String insertreferencia = " INSERT INTO `referencia` " +
                                                    " (`ReferenciaId`, " +
                                                    " `CasoId`, " +
                                                    " `TipoSolicitudId`, " +
                                                    " `TipoReferenciaId`, " +
                                                    " `ReferenciaNombre`) " +
                                                    " VALUES (" +
                                                    " null, " +
                                                    sCasoid + ", " +
                                                    sComboTiposolicitud + ", " +
                                                    "1, " +
                                                    "'" + stextBoxReferencia + "'); ";
                        MySqlDataReader respinsertreferencia = conect_ref.getdatareader(insertreferencia);
                        if (respinsertreferencia.RecordsAffected == 1)
                        {
                            //MessageBox.Show("Se inserto en referencia");
                            bCasoreferencias = true;
                        }
                        respinsertreferencia.Close();
                        conect_ref.Cerrarconexion();
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

                            if ("PCT" == listViewPrioridades.Items[z].SubItems[3].Text)
                            { iIdtipoprioridad = 1; }
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
                                                        " (null," +
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
                        if (listViewPrioridades.Items.Count==0) {
                            bCasoprioridades = true;
                        }


                        if (bcasopatente && bCasocliente && bCasointeresado && bCasoreferencias)
                        {
                            /*Si se agregó el caso completo entonces insertamos el plazo general con l*/
                            //conect conect_plazoid = new conect();
                            //String sQuery_plazo_general = " INSERT INTO `plazo_general` " +
                            //                                " (`Tipo_plazoid_impi`, " +
                            //                                " `Estatus_plazoid_impi`, " +
                            //                                " `CasoId`, " +
                            //                                " `TipoSolicitudId`, " +
                            //                                " `usuario_id_capturo_plazo`) " +
                            //                                " VALUES (" +
                            //                                " '2', " +
                            //                                " '1', " +
                            //                                " '"+sCasoid+"', " +
                            //                                " '" + sComboTiposolicitud + "', " +
                            //                                " '" + login.sId+ "');";
                            //MySqlDataReader resp_plazo = conect_plazoid.getdatareader(sQuery_plazo_general);
                            //if (resp_plazo.RecordsAffected == 1)
                            //{
                            //    /*aqui validamos que se inserto el plazo*/
                            //}
                            //resp_plazo.Close();
                            //conect_plazoid.Cerrarconexion();
                            //Nuevo plazo
                                /*
                                 * Creamos el plazo en la tabla Plazo la relacion con casoid y tiposolicitudid 
                                 */
                                String sPlazosid = "";
                                bool bBanderacreadetalleplazo = false;
                                conect conect_plazosid = new conect();
                                String sQuery_plazos = " INSERT INTO `plazos`(`Plazosid`,`CasoId`,`TipoSolicitudId`,`Fecha_creacion`)VALUES " +
                                                                " (null, " +
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
                                    //MessageBox.Show("Se creó un plazo.");
                                    /*aqui validamos que se inserto el plazo*/
                                }
                                resp_plazos.Close();
                                conect_plazosid.Cerrarconexion();

                                String sFechahoy = DateTime.Today.ToString("yyyy'/'MM'/'dd");
                                String sFechapresentacion = DateTime.ParseExact(textClientduedate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbDocumentofecharecepcion.Text
                                //String sFechapresentacion = DateTime.Parse(textClientduedate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                                conect conect_plazoid = new conect();
                                String sQuery_plazo_general = " INSERT INTO `plazos_detalle` " +
                                                                " (`Plazos_detalleid`, " +
                                                                " `Plazosid`, " +//
                                                                
                                                                " `usuario_creo_plazodetalle`, " +
                                                                " `Tipo_plazoid`, " +
                                                                " `Estatus_plazoid`, " +
                                                                " `Fecha_Vencimiento`) " +
                                                                " VALUES (null," +
                                                                " '"+sPlazosid+"', " +
                                                                
                                                                " '" + login.sId + "', " +
                                                                " 2, " +
                                                                " '1', " +
                                                                " '" + sFechapresentacion + "');";

                                                                //" '" + tipoplazoid_avisocliente + "', " +
                                                                //" '1', " +
                                                                //" '" + documentoid + "', " +
                                                                //" '" + login.sId + "');";

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
                                                                    " VALUES (null," +
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
                                    if (cbEnviaratraducir.Checked)
                                    {//debemos generar plazos de enviar a traducir para éste caso
                                        //DateTime  sfwer = DateTime.ParseExact(textClientduedate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                                        conect conect_plazoid_acusar_traduccion = new conect();
                                        String sQuery_plazo_traduccion = " INSERT INTO `plazos_detalle` " +
                                                                        " (`Plazos_detalleid`, " +
                                                                        " `Plazosid`, " +//
                                                                        " `usuario_creo_plazodetalle`, " +
                                                                        " `Tipo_plazoid`, " +
                                                                        " `Estatus_plazoid`, " +
                                                                        " `Fecha_Vencimiento`) " +
                                                                        " VALUES (null," +
                                                                        " '" + sPlazosid + "', " +
                                                                        " '" + login.sId + "', " +
                                                                        " '19', " +
                                                                        " '1', " +
                                                                        " '" + sFechaVigencia + "');";

                                        MySqlDataReader resp_plazo_traduccion = conect_plazoid_acusar_traduccion.getdatareader(sQuery_plazo_traduccion);
                                        if (resp_plazo_traduccion.RecordsAffected == 1)
                                        {
                                            //ibBanderacreadetalleplazo_contador++;
                                            //MessageBox.Show("Se creó un plazo.");
                                            /*aqui validamos que se inserto el plazo*/
                                        }
                                        resp_plazo_traduccion.Close();
                                        conect_plazoid_acusar_traduccion.Cerrarconexion();
                                    }
                               /*FIN Aquí Validaremos que se creé el plazo de envir a traducir*/

                            /*FiN*/


                            
                            if (bCasoprioridades)
                            {
                                MessageBox.Show("El caso se agrego correctamente con el CasoNúmero: " + sCasonumero);
                                /*
                                 */
                                DialogResult results = MessageBox.Show("¿Desea agregar un caso nuevo del mismo tipo?", "Agregar Caso Patentes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (results == DialogResult.Yes)
                                {
                                    //code for Yes
                                    Nuevocaso nuevocasothis = new Nuevocaso(iGrupoid, cFcaptura, login);
                                    nuevocasothis.Show();
                                    this.Close();
                                }
                                else if (results == DialogResult.No)
                                {
                                    //code for No
                                    cFcaptura.Show();
                                    this.Close();
                                    Consutlacaso ConsultacasoObj = new Consutlacaso(login,cFcaptura, int.Parse(sComboTiposolicitud));
                                    consultacaso ObjCaso = new consultacaso(login, cFcaptura, ConsultacasoObj, sCasoid);
                                    ObjCaso.ShowDialog();
                                    ConsultacasoObj.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("El caso se agrego con el CasoNúmero: " + sCasonumero + " Revisar las prioridades.");
                                //code for No
                                cFcaptura.Show();
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        
                        MessageBox.Show("Las fechas son incorrectas.");
                    }
                }
                else {
                    MessageBox.Show("Debe llenar los campos obligarorios para caso");
                }
            }catch(Exception E){
                MessageBox.Show("Verifique que todos los campos estén correctos. "+E);
            }


        }
        public void agregaplazo(String sCasoid, String sComboTiposolicitud, String subtipodocumentoid, DateTime dFecha_notificacion_impi, String documentoid)
        {
            try{
                
                
                String relacion_plazo_subtipodocumentoid = "";
                String tipoplazoid_impi = "";
                String tipoplazoid_avisocliente = "";
                String activo = "";

                String sSubTipoDocumentoIndPlazo = "";
                String sSubTipoDocumentoPlazoMeses = "";
                String sSubTipoDocumentoPlazoDias = "";

                /*Consultamos si genera plazo en la tabla Subtipodocumento y cuantos meses y dias se le suman*/
                //select * from subtipodocumento where subtipodocumentoid = 108;
                /*
                 * SubTipoDocumentoIndPlazo = 1 Indica si genera plazo,
                 * SubTipoDocumentoPlazoMeses = Número de meses que se le suma a la fecha de Notificacion
                 * SubTipoDocumentoPlazoDias = Número de días que se le suma a la fecha de Notificacion adicional a los mesese
                 
                 El resultado es Fecha_Vencimiento_regular_impi
                  
                 */

                

               

                    /*
                     * Creamos el plazo en la tabla Plazo la relacion con casoid y tiposolicitudid 
                     */
                    String sPlazosid = "";
                    bool bBanderacreadetalleplazo = false;
                    conect conect_plazosid = new conect();
                    String sQuery_plazos = " INSERT INTO `plazos`(`Plazosid`,`CasoId`,`TipoSolicitudId`,`Fecha_creacion`)VALUES " +
                                                    " (null, " +
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
                        //MessageBox.Show("Se creó un plazo.");
                        /*aqui validamos que se inserto el plazo*/
                    }
                    resp_plazos.Close();
                    conect_plazosid.Cerrarconexion();


                    /* Validamos que tipo de plazo tiene el subtipodocumentoid generado anteriormente 
                     * en la tabla relacion_plazo_subtipodocumento, es quien nos 
                     * indica el plazo impi y el plazo aviso al cliente 
                     * relacionado al subtipodocumentoid
                     */
                    int ibBanderacreadetalleplazo_contador = 0;
                    if (bBanderacreadetalleplazo)
                    {
                        //preguntamos por el plazo DIFERENTE DE AVISO para saber si se agrega aquí, 
                        //de ser un plazo de aviso al cliente debe sumarle un día a la fecha actual y esa sera la fecha vigencia
                        conect conect_plazoid_relacion = new conect();
                        //String sQuery_plazo_relacion_general = " select * from relacion_plazo_subtipodocumento where subtipodocumentoid = " + subtipodocumentoid;
                        String sQuery_plazo_relacion_general = " SELECT  " +
                                                                "     * " +
                                                                " FROM " +
                                                                "     relacion_plazo_subtipodocumento, " +
                                                                "     tipoplazos " +
                                                                " WHERE " +
                                                                "     relacion_plazo_subtipodocumento.tipoplazosid = tipoplazos.tipoplazosid " +
                                                                //" 	AND tipoplazos.aviso is null " +
                                                                " 	AND relacion_plazo_subtipodocumento.subtipodocumentoid = " + subtipodocumentoid +" ;";
                        MySqlDataReader resp_plazo_relacion = conect_plazoid_relacion.getdatareader(sQuery_plazo_relacion_general);
                        while (resp_plazo_relacion.Read())
                        {
                            relacion_plazo_subtipodocumentoid = validareader("subtipodocumentoid", "relacion_plazo_subtipodocumentoid", resp_plazo_relacion).Text;
                            tipoplazoid_impi = validareader("tipoplazosid", "relacion_plazo_subtipodocumentoid", resp_plazo_relacion).Text;
                            /**
                             Consultamos las fechas del plazo
                             */
                            conect conect_plazoid_activo = new conect();
                            String sQuery_plazo_relacion_activo = " select * from subtipodocumento where SubTipoDocumentoId = " + relacion_plazo_subtipodocumentoid;
                            MySqlDataReader resp_plazo_activo = conect_plazoid_activo.getdatareader(sQuery_plazo_relacion_activo);
                            while (resp_plazo_activo.Read())
                            {
                                
                                sSubTipoDocumentoIndPlazo = validareader("SubTipoDocumentoIndPlazo", "SubTipoDocumentoId", resp_plazo_activo).Text;
                                sSubTipoDocumentoPlazoMeses = validareader("SubTipoDocumentoPlazoMeses", "SubTipoDocumentoId", resp_plazo_activo).Text;
                                sSubTipoDocumentoPlazoDias = validareader("SubTipoDocumentoPlazoDias", "SubTipoDocumentoId", resp_plazo_activo).Text;
                            }
                            resp_plazo_activo.Close();
                            conect_plazoid_activo.Cerrarconexion();

                            
                                //tipoplazoid_avisocliente = validareader("tipoplazoid_avisocliente", "relacion_plazo_subtipodocumentoid", resp_plazo_relacion).Text;
                                activo = validareader("aviso", "aviso", resp_plazo_relacion).Text;

                                /*Calculamos las fechas a insertar en el plazo*/
                                DateTime sFechaplazoregular = dFecha_notificacion_impi;
                                try {
                                    sFechaplazoregular = sFechaplazoregular.AddMonths(Int16.Parse(sSubTipoDocumentoPlazoMeses));
                                    sFechaplazoregular = sFechaplazoregular.AddDays(Int16.Parse(sSubTipoDocumentoPlazoDias));
                                }catch(Exception Ex){

                                }
                                DateTime sFechaplazoregular3meses = sFechaplazoregular.AddMonths(1);
                                DateTime sFechaplazoregular4meses = sFechaplazoregular3meses.AddMonths(1);

                                /*Aqui vamos, solo falta insertar el plazo y llamar a ésta función*/
                    
                                //.AddMonths(Int16.Parse());
                

                                /*aqui validamos que se inserto el plazo*/
                                /*
                                 Necesitamos un tipoplazoid_impi y 
                                 * Fecha_notificacion_impi  <-----
                                 * Fecha_Vencimiento_regular_impi <-- Calculado
                                 * Fecha_vencimiento_3m_impi <-- Calculado
                                 * Fecha_vencimiento_4m_impi <-- Calculado
                                 */
                                String sFechanotificacion = dFecha_notificacion_impi.ToString("yyyy-MM-dd");
                                String ssFechaplazoregular = sFechaplazoregular.ToString("yyyy-MM-dd");
                                String ssFechaplazoregular3meses = sFechaplazoregular3meses.ToString("yyyy-MM-dd");
                                String ssFechaplazoregular4meses = sFechaplazoregular4meses.ToString("yyyy-MM-dd");
                                if (activo == "1")//Nos indica que es un aviso y sólo aumenta un día a la fecha actual como vigencia
                                {//las demás fechas van vacias
                                    sFechanotificacion = "";
                                    sFechaplazoregular = DateTime.Today;//asignamos la fecha actual
                                    sFechaplazoregular = sFechaplazoregular.AddDays(1);
                                    ssFechaplazoregular = sFechaplazoregular.ToString("yyyy-MM-dd");
                                    ssFechaplazoregular3meses = "";
                                    ssFechaplazoregular4meses = "";
                                }

                                conect conect_plazoid = new conect();
                                String sQuery_plazo_general = " INSERT INTO `plazos_detalle` " +
                                                                " (`Plazos_detalleid`, " +
                                                                " `Plazosid`, " +//
                                                                " `documentoid`, " +//documentoid
                                                                " `usuario_creo_plazodetalle`, " +
                                                                " `Tipo_plazoid`, " +
                                                                " `Estatus_plazoid`, " +
                                                                " `Fecha_notificacion`, " +
                                                                " `Fecha_Vencimiento`, " +
                                                                " `Fecha_vencimiento_3m`, " +
                                                                " `Fecha_vencimiento_4m`) " +
                                                                " VALUES (null," +
                                                                " '" + sPlazosid + "', " +
                                                                " '" + documentoid + "', " +
                                                                " '" + login.sId + "', " +
                                                                " '" + tipoplazoid_impi + "', " +
                                                                " '1', " +
                                                                " '" + sFechanotificacion + "', " +
                                                                " '" + ssFechaplazoregular + "', " +
                                                                " '" + ssFechaplazoregular3meses + "', " +
                                                                " '" + ssFechaplazoregular4meses + "');";

                                                                //" '" + tipoplazoid_avisocliente + "', " +
                                                                //" '1', " +
                                                                //" '" + documentoid + "', " +
                                                                //" '" + login.sId + "');";

                                MySqlDataReader resp_plazo = conect_plazoid.getdatareader(sQuery_plazo_general);
                                if (resp_plazo.RecordsAffected == 1)
                                {
                                    ibBanderacreadetalleplazo_contador++;
                                    //MessageBox.Show("Se creó un plazo.");
                                    /*aqui validamos que se inserto el plazo*/
                                }
                                resp_plazo.Close();
                                conect_plazoid.Cerrarconexion();
                           // }//cerramos el if de SubTipoDocumentoIndPlazo
                        }
                        resp_plazo_relacion.Close();
                        conect_plazoid_relacion.Cerrarconexion();
                    }//cerramos el if
                    MessageBox.Show("Se agregraron " + ibBanderacreadetalleplazo_contador + " de subPlazos");
            
            }catch(Exception Ex){

            }
        }
        private String validafechacorecta(String Fechaentrada){//verificamos que es una fecha valida y la convertimos a un formato date mysql
            String sFechasalida = "";
            try { 
                sFechasalida = DateTime.ParseExact(Fechaentrada, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy'/'MM'/'dd"); //tbDocumentofecharecepcion.Text;
            }catch(Exception E){
                sFechasalida = "";
            }
            return sFechasalida;
        }

        private void comboTiposolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboboxSubtipo.Items.Clear();
            comboboxSubtipo.Text = "";
            comboboxSubtipo2.Items.Clear();
            comboboxSubtipo2.Text = "";
            conect con = new conect();
            String query = "select SubTipoSolicitudId, SubTipoSolicitudDescripcion from subtiposolicitud where tiposolicitudID =" + (comboTiposolicitud.SelectedItem as ComboboxItem).Value.ToString();
            MySqlDataReader respuestastring = con.getdatareader(query);
            while (respuestastring.Read())
            {
                comboboxSubtipo.Items.Add(validareader("SubTipoSolicitudDescripcion", "SubTipoSolicitudId", respuestastring));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            cFcaptura.Close();
            login.Close();
            
        }

        private void comboboxSubtipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboboxSubtipo2.Items.Clear();
            comboBoxTipodos.Items.Clear();
            comboboxSubtipo2.Text = "";
            if ((comboboxSubtipo.SelectedItem as ComboboxItem).Value.ToString().Equals("1"))
            {
                //cuantas prioridades debe tener y dejamos la del registro como valor 0
                comboboxSubtipo2.Enabled = true;
                conect con = new conect();
                String query = "select * from tipopct;";
                MySqlDataReader respuestastring = con.getdatareader(query);
                while (respuestastring.Read())
                {
                    comboboxSubtipo2.Items.Add(validareader("TipoPctDescrip", "TipoPctId", respuestastring));
                }
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
            }else {
                if ((comboboxSubtipo.SelectedItem as ComboboxItem).Value.ToString().Equals("3"))
                {
                    //regular no lleva prioridades
                    groupPrioridades.Enabled = false;
                    comboboxSubtipo2.Enabled = false;
                    //comboBoxNprioridades.Text = "";
                }else{
                    //cuantas prioridades debe tener?
                    groupPrioridades.Enabled = true;
                    //comboBoxTipodos = "";  //asignamos PCT y parís y el otro 
                    comboboxSubtipo2.Enabled = false;
                    ComboboxItem comboTipotres = new ComboboxItem();
                    comboTipotres.Text = "París";
                    comboTipotres.Value = "2";
                    comboBoxTipodos.Items.Add(comboTipotres);
                    
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
        }

        private void comboBoxClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            conect con = new conect();
            String query2 = "SELECT "+
                            "direccion.DireccionID, " +
                            "direccion.DireccionCalle, " +
                            "direccion.DireccionColonia, "+
                            "direccion.DireccionEstado, "+
                            "direccion.DireccionCP, "+
                            "direccion.DireccionPoblacion, "+
                            "pais.PaisNombre "+
                            "FROM "+
                            "    direccion, "+
                            "    pais "+
                            "WHERE "+
                            "    direccion.PaisId = pais.PaisId "+
                            "AND direccion.ClienteId ="+ (comboBoxClientes.SelectedItem as ComboboxItem).Value.ToString();
            MySqlDataReader respuestastringclient = con.getdatareader(query2);
            
            while (respuestastringclient.Read())
            {
                richTextBoxDireccliente.Text = validareader("DireccionCalle", "DireccionID", respuestastringclient).Text + " " +
                validareader("DireccionColonia", "DireccionID", respuestastringclient).Text +" "+
                validareader("DireccionEstado", "DireccionID", respuestastringclient).Text + "" +
                validareader("DireccionCP", "DireccionID", respuestastringclient).Text + " " +
                validareader("DireccionPoblacion", "DireccionID", respuestastringclient).Text + "" +
                validareader("PaisNombre", "DireccionID", respuestastringclient).Text;
            }
            respuestastringclient.Close();
            con.Cerrarconexion();

            conect con_dos = new conect();

            //Borramos los cantactos anteriores, si es que los ubiera
            comboBoxContacto.Text = "";
            richTextBox1.Text = "";
            comboBoxContacto.Items.Clear();
            String query3 = "select ContactoId, ContactoNombre, ContactoTelefono, ContactoEmail from contacto where clienteid = " + (comboBoxClientes.SelectedItem as ComboboxItem).Value.ToString();
            MySqlDataReader respuestastringcontacto = con_dos.getdatareader(query3);

            while (respuestastringcontacto.Read())
            {
                comboBoxContacto.Items.Add(validareader("ContactoNombre", "ContactoId", respuestastringcontacto));
            }
            respuestastringcontacto.Close();
            con_dos.Cerrarconexion();
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
            }
            catch (Exception exs) {
                new filelog(" linea 1201 ", " : "+exs.StackTrace);
            }
            //paises[Convert.ToInt32((comboBoxPais.SelectedItem as ComboboxItem).Value.ToString())];
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            try { 
            
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
                else {
                    comboBoxTipodos.Text = "París";
                }
            }
            catch (Exception Ex)
            {
                //new filelog("linea 1200", "Error:"+Ex.Message);
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


            if (textBoxFecha.Text.Length ==2)
            {
                textBoxFecha.Text = textBoxFecha.Text+"-";
                textBoxFecha.SelectionStart = textBoxFecha.Text.Length;

            }
            if (textBoxFecha.Text.Length == 5)
            {
                textBoxFecha.Text = textBoxFecha.Text+"-";
                textBoxFecha.SelectionStart = textBoxFecha.Text.Length;
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //if (comboBoxNprioridades.Text == "")
            //{
            //    comboBoxNprioridades.Focus();
            //    //MessageBox.Show("Debe indicar cuantas prioridades necesita, antes de agregar prioridades.");
            //}
            //else { 
                if (textBoxNumero.Text != "" && textBoxFecha.Text != "" && comboBoxPais.Text != "" && comboBoxTipodos.Text != "")
                {
                    if (textBoxNumero.Text.Substring(0, 3).Equals("PCT"))
                    {
                        if (!bPrioridadpctagregada)
                        {
                            MessageBox.Show("Ya existe una prioridad tipo PCT.");
                            return;
                        }
                        if (textBoxFecha.Text.Substring(6, 4).Equals(textBoxNumero.Text.Substring(6, 4)))
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
                            bPrioridadpctagregada = false;

                        }
                        else {
                            //MessageBox.Show(textBoxFecha.Text.Substring(6, 4) + " vs " + textBoxNumero.Text.Substring(6, 4));
                            MessageBox.Show("La fecha de prioridad no coincide con el número de prioridad");
                        }
                    }
                    else {
                        ListViewItem lPrioridades = new ListViewItem(textBoxNumero.Text);
                        lPrioridades.SubItems.Add(textBoxFecha.Text);
                        lPrioridades.SubItems.Add(comboBoxPais.Text);
                        lPrioridades.SubItems.Add(comboBoxTipodos.Text);
                        listViewPrioridades.Items.Add(lPrioridades);

                        textBoxNumero.Text = "";
                        textBoxFecha.Text = "";
                        comboBoxPais.Text = "";
                        comboBoxTipodos.Text = "";
                        //MessageBox.Show(textBoxFecha.Text.Substring(0, 3));
                    }
                }else{
                    MessageBox.Show("Debe completar los campos para agregar una prioridad");
                }
            //}
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


            if (textBoxFechainternacional.Text.Length == 2)
            {
                textBoxFechainternacional.Text = textBoxFechainternacional.Text + "-";
                textBoxFechainternacional.SelectionStart = textBoxFechainternacional.Text.Length;

            }
            if (textBoxFechainternacional.Text.Length == 5)
            {
                textBoxFechainternacional.Text = textBoxFechainternacional.Text + "-";
                textBoxFechainternacional.SelectionStart = textBoxFechainternacional.Text.Length;
            }
        }

        public String cambiaformatofecha(String Fechauno) {
            if (Fechauno != "")
            {
                //Fechauno se espera con el formato dd-mm-yyyy
                String año = Fechauno.Substring(6, 4);//yyyy
                String mes = Fechauno.Substring(3, 2);//mm
                String dia = Fechauno.Substring(0, 2);//dd
                //retorna yyyy-mm-dd
                return año + "-" + mes + "-" + dia;
            }
            else {
                return "";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(comboBoxNprioridades.Text) == 0 && comboboxSubtipo.Text == "PCT")
            //{//se habilita el recuadro de prioridades para uno sólo para PCT
            //    groupPrioridades.Enabled = true;   
            //}
            //else {
            //    if (Convert.ToInt32(comboBoxNprioridades.Text) == 0 && comboboxSubtipo.Text != "PCT")
            //    {//se habilita para uno
            //        groupPrioridades.Enabled = false;   
            //    }
            //    else {
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
            String sCorreocontacto ="";
            MySqlDataReader resp_correoscontact = con.getdatareader(sQuerycontact);
            while (resp_correoscontact.Read())
            {
                sCorreocontacto += validareader("ContactoEmail", "ContactoEmail", resp_correoscontact).Text;
            }
            resp_correoscontact.Close();
            richTextBox1.Text = sCorreocontacto;
        }

        private void comboBoxInteresado_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                richTextBoxDireccliente.Text = "";//
                richTextBox1.Text = aClientenuevo.Contactocorreo;
            }
        }

        public bool bCasoprioridades { get; set; }
        public DateTime dFechaprioridadmenor = Convert.ToDateTime("01-01-1900");

        private void button5_Click(object sender, EventArgs e)
        {
            try {
                if (textBoxNumero.Text != "" && textBoxFecha.Text != "" && comboBoxPais.Text != "" && comboBoxTipodos.Text != "")
                {
                    if (validafechacorecta(textBoxFecha.Text) != "")
                    {
                        //new filelog("comparacion", dFechaprioridadmenor.ToString().Substring(0,10) + " vs " + "01/01/1900");
                        //las funciones pueden variarrespecto al formato de feche que tiene el equipo de 12 o 24 hrs
                        //por eso tomamos sólo la fecha para comparar
                        if (dFechaprioridadmenor.ToString().Substring(0, 10) == "01/01/1900")//Validamos que sea fecha menor
                        {
                            dFechaprioridadmenor = Convert.ToDateTime(textBoxFecha.Text);
                        }
                        else
                        {//quiere decir que por lo menos ya se agrego una prioridad
                            if (comboBoxTipodos.Text != "PCT" && dFechaprioridadmenor < Convert.ToDateTime(textBoxFecha.Text))//comparamos la fecha para saber si es mayor y si es return 
                            {//quitamos la validacion de las fechas antiguas para las prioridades
                                //MessageBox.Show("La fecha de la prioridad no puede ser mayor a las fechas de las prioridades existentes.");
                                //return;
                            }else {
                                dFechaprioridadmenor = Convert.ToDateTime(textBoxFecha.Text);
                            }
                        }
                        if (textBoxNumero.Text.Substring(0, 3).Equals("PCT") && comboBoxTipodos.Text == "PCT")
                        {
                            if (!bPrioridadpctagregada)
                            {
                                MessageBox.Show("Ya existe una prioridad tipo PCT.");
                                return;
                            }
                            if (textBoxFecha.Text.Substring(6, 4).Equals(textBoxNumero.Text.Substring(6, 4)))
                            {
                                if (validarprioridades())
                                {
                                    ListViewItem lPrioridades = new ListViewItem(textBoxNumero.Text);
                                    lPrioridades.SubItems.Add(textBoxFecha.Text);
                                    lPrioridades.SubItems.Add(comboBoxPais.Text);
                                    lPrioridades.SubItems.Add(comboBoxTipodos.Text);
                                    listViewPrioridades.Items.Add(lPrioridades);
                                    //agregamos ésta variable(bPrioridadpctagregada) para validar que ya existe una prioridad tipo PCT agregada a la lista
                                    //y sólo se debe agregar una prioridad tipo PCT por lo que lo limitamos con etsa variable
                                    bPrioridadpctagregada = true;
                                    //si la prioridad es PCT ésta fecha es la que se debe tomar en cuenta para Fecha Internacional
                                    textBoxFechainternacional.Text = textBoxFecha.Text;
                                    //vaciamos los campos
                                    textBoxNumero.Text = "";
                                    textBoxFecha.Text = "";
                                    comboBoxPais.Text = "";
                                    comboBoxTipodos.Text = "";
                                    cbCvpais.Text = "";
                                    validarfechalegal();
                                }
                            }
                            else
                            {
                                //MessageBox.Show(textBoxFecha.Text.Substring(6, 4) + " vs " + textBoxNumero.Text.Substring(6, 4));
                                MessageBox.Show("La fecha de prioridad no coincide con el número de prioridad");
                            }
                        }
                        else
                        {
                            if (comboBoxTipodos.Text == "PCT")
                            {
                                MessageBox.Show("El tipo de prioridad no coincide con el número de prioridad.");
                                return;
                            }else{
                                if (validarprioridades())
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
                                    cbCvpais.Text = "";
                                    validarfechalegal();
                                }
                                
                            }
                            
                            //MessageBox.Show(textBoxFecha.Text.Substring(0, 3));
                        }
                    }
                    else
                    {
                        MessageBox.Show("La fecha de la prioridad que intenta agregar es incorrecta.");
                        textBoxFecha.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar los campos para agregar una prioridad");
                }
            }catch(Exception E)
            {
                new filelog(login.sId, E.ToString());
            }
        }

        public bool validarprioridades() {
            bool Result = false;
            try { 

                /**
                * Validamos si el numero de referencia ya existe
                * 
                */
                int iNum_ref = 0;
                String sCAsonumero = "";
                conect con_prio = new conect();
                String sGetcasonumero_prio = " SELECT  " +
                                                "     COUNT(*) AS num_prio, " +
                                                "     caso_patente.CasoNumero " +
                                                " FROM " +
                                                "     prioridad, " +
                                                "     caso_patente " +
                                                " WHERE " +
                                                " 	caso_patente.casoid = prioridad.casoid " +
                                                "     and caso_patente.TipoSolicitudId = prioridad.TipoSolicitudId " +
                                                "     and PrioridadNumero LIKE '%" + textBoxNumero.Text + "%'; "; 
                MySqlDataReader respuestastringcasonum_prio = con_prio.getdatareader(sGetcasonumero_prio);
                while (respuestastringcasonum_prio.Read())
                {
                    iNum_ref = int.Parse(validareader("num_prio", "num_prio", respuestastringcasonum_prio).Text);
                    sCAsonumero = validareader("CasoNumero", "CasoNumero", respuestastringcasonum_prio).Text;
                }
                respuestastringcasonum_prio.Close();
                con_prio.Cerrarconexion();

                if (iNum_ref > 0)
                {
                    if (MessageBox.Show("Esta prioridad esta repetida en el caso: "+ sCAsonumero + " , ¿seguro que quiere agregarla?", "Prioridad Repetida", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Result = true;
                    }else{
                        Result = false;
                    }
                }
                if (sCAsonumero=="")/*Quiere decir que no está repetida*/
                {
                    Result = true;
                }
                /**
                * Fin
                * Validamos si el numero de prioridad ya existe
                */
            }catch(Exception Ex){
                Result = false;
            }
            return Result;
        }
        public void validarfechalegal(){//calida la fecha más antigua para ponerla en el campo de fecha legal
            String datemenor = "";
            bool bMesesaddPCT = false;
            if (listViewPrioridades.Items.Count ==1)
            {
                //textBoxPlazolegal.Text = listViewPrioridades.Items[0].SubItems[1].Text;
                DateTime fechasumadas = Convert.ToDateTime(listViewPrioridades.Items[0].SubItems[1].Text);
                if (listViewPrioridades.Items[0].SubItems[3].Text == "PCT")
                {
                    fechasumadas = fechasumadas.AddMonths(30);
                }
                else {
                    //Aquí sumamos 12 o 6 meses a la fecha más antiguas de las prioridades
                    if ((comboTiposolicitud.SelectedItem as ComboboxItem).Value.ToString() == "3" || (comboTiposolicitud.SelectedItem as ComboboxItem).Value.ToString() == "4")
                    {
                        fechasumadas = fechasumadas.AddMonths(6);
                    }
                    else
                    {//Los tipos son de Diseños Tiposoicitud 3 y 4 son diseños.
                        fechasumadas = fechasumadas.AddMonths(12);
                    }
                    //fechasumadas = fechasumadas.AddMonths(12);
                }
                
                textBoxPlazolegal.Text = fechasumadas.ToString().Substring(0, 10);
                return;
            }
            for (int z = 0; z < listViewPrioridades.Items.Count -1; z++){
                if (listViewPrioridades.Items[z].SubItems[3].Text == "PCT"){
                    bMesesaddPCT = true;
                }
                if (listViewPrioridades.Items[z+1].SubItems[3].Text == "PCT")
                {
                    bMesesaddPCT = true;
                }
                //listViewPrioridades.SelectedItems[0].SubItems[1].Text
                String fecha_uno = listViewPrioridades.Items[z].SubItems[1].Text;
                String fecha_dos = listViewPrioridades.Items[z + 1].SubItems[1].Text;
                DateTime date1 = Convert.ToDateTime(fecha_uno);
                DateTime date2 = Convert.ToDateTime(fecha_dos);
                int value = DateTime.Compare(date1, date2);
                
                if(value > 0){
                    Console.WriteLine(fecha_uno + "es mayor a " + fecha_dos);
                    datemenor = fecha_dos;
                }
            }
            //textBoxPlazolegal.Text = datemenor;
            DateTime fechasumada = Convert.ToDateTime(datemenor);
            if (bMesesaddPCT)
            {
                fechasumada = fechasumada.AddMonths(30);
            }
            else {
                //Aquí sumamos 12 o 6 meses a la fecha más antiguas de las prioridades
                if ((comboTiposolicitud.SelectedItem as ComboboxItem).Value.ToString() == "3" || (comboTiposolicitud.SelectedItem as ComboboxItem).Value.ToString() == "4")
                {
                    fechasumada = fechasumada.AddMonths(6);
                }
                else {//Los tipos son de Diseños Tiposoicitud 3 y 4 son diseños.
                    fechasumada = fechasumada.AddMonths(12);
                }
                
            }
            textBoxPlazolegal.Text = fechasumada.ToString().Substring(0, 10);
        }

        private void bEliminarprioridades_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewPrioridades.SelectedItems.Count > 0)
                {
                    if (listViewPrioridades.SelectedItems[0].SubItems[3].Text == "PCT")
                    {
                        bPrioridadpctagregada = true;
                    }
                    listViewPrioridades.SelectedItems[0].Remove();
                    
                    textBoxFechainternacional.Text = "";
                    textBoxPlazolegal.Text="";
                    validarfechalegal();
                    if (listViewPrioridades.Items.Count == 0)//quiere decir que ya no hay prioridades
                    {
                        dFechaprioridadmenor = Convert.ToDateTime("01-01-1900");
                    }
                }else{
                    MessageBox.Show("Debe seleccionar una prioridad para eliminarla de la lista.");
                }
            }catch(Exception E){
                new filelog(login.sId, E.ToString());
            }
            
        }

        private void cbPaiscaso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int iValuepais = Convert.ToInt32((cbPaiscaso.SelectedItem as ComboboxItem).Value.ToString());
                tbClavepaiscaso.Text = paises[iValuepais];
            }
            catch(Exception eX) {
                new filelog("casonuevo", eX.Message);
            }
            
        }

        private void tbClavepaiscaso_TextChanged(object sender, EventArgs e)
        {
            //int iValuepais = Convert.ToInt32((cbPaiscaso.SelectedItem as ComboboxItem).Value.ToString());
            //tbClavepaiscaso.Text = paisesclave[iValuepais];
            try {
                int iValoractual = 0;
                for (int x = 0; x < paises.Length; x++)
                {
                    if (paises[x] == tbClavepaiscaso.Text)
                    {
                        iValoractual = x;
                    }
                }
                cbPaiscaso.Text = paisesclave[iValoractual];
            }catch(Exception Ex){
                new filelog("casnuevo", Ex.Message);
            }
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            addnuevotitular addnuevotitular = new addnuevotitular(this,login,TipoSol);
            if (addnuevotitular.ShowDialog() == DialogResult.OK)
            {
                comboBoxInteresado.Items.Add(addnuevotitular.cBcomboInteresadotitular);
                comboBoxInteresado.Text = addnuevotitular.sNombrenuevotitular;
                //comboBoxClientes.Items.Add(aClientenuevo.Cliente);
                //comboBoxContacto.Items.Add(aClientenuevo.Contacto);
                //comboBoxClientes.Text = aClientenuevo.Cliente.Text;
                //comboBoxContacto.Text = aClientenuevo.Contacto.Text;
                //richTextBox1.Text = aClientenuevo.Contactocorreo;
            }
        }

        private void textBoxPlazolegal_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(textBoxPlazolegal);
        }

        private void TexboxFecha_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(TexboxFecha);
        }

        private void textClientduedate_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(textClientduedate);
        }

        private void textBoxFechainternacional_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(textBoxFechainternacional);
        }

        private void textBoxPlazolegal_TextChanged(object sender, EventArgs e)
        {
            //Al crear un caso nuevo Cient due date no puede ser mayor al plazo legal

            //SE COMENTA PARA HACER PRUEBAS
            
            //try
            //{
            //    DateTime dDatePlazolegal, dDatetextClientduedate;
            //    DateTime.TryParse(textBoxPlazolegal.Text, out dDatePlazolegal);
            //    DateTime.TryParse(textClientduedate.Text, out dDatetextClientduedate);
            //    if (dDatetextClientduedate > dDatePlazolegal)
            //    {
            //        textClientduedate.Text = "";
            //        textClientduedate.Focus();
            //        MessageBox.Show("La fecha Client Due Date, no puede ser mayor a la del plazo Legal.");
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }

        private void textClientduedate_TextChanged(object sender, EventArgs e)
        {
            //SE COMENTA PARA HACER PRUEBAS

            //try
            //{
            //    if (textBoxPlazolegal.Text!="") {
            //        if (textClientduedate.Text.Length == 10)
            //        {
            //            DateTime dDatePlazolegal, dDatetextClientduedate;
            //            DateTime.TryParse(textBoxPlazolegal.Text, out dDatePlazolegal);
            //            DateTime.TryParse(textClientduedate.Text, out dDatetextClientduedate);
            //            if (dDatetextClientduedate > dDatePlazolegal)
            //            {
            //                textClientduedate.Text = "";
            //                textClientduedate.Focus();
            //                MessageBox.Show("La fecha Client Due Date, no puede ser mayor a la del plazo Legal.");
            //            }
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{

            //}
        }

        private void textBoxFecha_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //pueden pegar fechas con los siguientes formatos 
                //ddmmyyyy
                //dd.mm.yyyy
                //dd/mm/yyyy
                //dd-mm-yyyy

                if (textBoxFecha.Text.Length == 8)
                { //validamos el primer formato ddmmyyyy
                    Boolean bsondigitos = true;
                    for (int x = 0; x < textBoxFecha.Text.Length; x++)
                    {
                        if (!Char.IsDigit(textBoxFecha.Text[x]))
                        {
                            bsondigitos = false;
                        }
                    }
                    if (bsondigitos)
                    { //todos los caracateres osn numeros y son 8 por lo que supondremos que son ddmmyyyy
                        String sfechaconcarateres = textBoxFecha.Text.Substring(0, 2) + "-" + textBoxFecha.Text.Substring(2, 2) + "-" + textBoxFecha.Text.Substring(4, 4);
                        textBoxFecha.Text = sfechaconcarateres;
                    }
                }
                String spuntouno = textBoxFecha.Text.Substring(2, 1);
                String spuntodos = textBoxFecha.Text.Substring(5, 1);

                if (textBoxFecha.Text.Length == 10 && spuntouno == "." && spuntodos == ".")//entonces validamos el segundo formato dd.mm.yyyy
                {
                    Boolean bsondigitos = true;
                    for (int x = 0; x < textBoxFecha.Text.Length; x++)
                    {
                        if ((!Char.IsDigit(textBoxFecha.Text[x])) && x != 2 && x != 5)
                        {
                            bsondigitos = false;
                        }
                    }
                    if (bsondigitos)
                    {
                        textBoxFecha.Text = textBoxFecha.Text.Replace('.', '-');
                    }
                }

            }
            catch (Exception exs)
            {
                new filelog("analizando la prioridad", " : " + exs.StackTrace);
            }
        }

        private void cbCvpais_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iValuepais = Convert.ToInt32((cbCvpais.SelectedItem as ComboboxItem).Value.ToString());
            //(comboBoxPais.SelectedItem as ComboboxItem).Value = iValuepais;
            //textBoxcve.Text = paises[iValuepais];
            comboBoxPais.Text = paisesclave[iValuepais];
        }

        private void textBoxNumero_TextChanged(object sender, EventArgs e)
        {
            try
            {
                String sPrioridad = textBoxNumero.Text.Trim();
                textBoxNumero.Text = sPrioridad;

                if (sPrioridad.Length >= 3)
                {
                    if (sPrioridad.Substring(0, 3).Equals("PCT"))
                    {

                        comboBoxTipodos.SelectedItem = "PCT";
                        comboBoxTipodos.Text = "PCT";

                        if (sPrioridad.Length >= 10)
                        {

                            for (int x = 0; x < paises.Length; x++)
                            {
                                if (paises[x] == sPrioridad.Substring(4, 2))
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
                else
                {
                    comboBoxTipodos.Text = "París";
                }
            }
            catch (Exception Ex)
            {
                //new filelog("linea 1200", "Error:"+Ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                String datocliente = (comboBoxClientes.SelectedItem as ComboboxItem).Value.ToString();
                if (datocliente!="")
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
    }
}
