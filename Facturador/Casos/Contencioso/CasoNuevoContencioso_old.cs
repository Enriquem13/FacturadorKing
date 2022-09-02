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
    public partial class CasoNuevoContencioso_old : Form
    {
        public Form1 fLoguin;
        public captura captura;
        public String sTipodesolicitudg;
        //20220311FSV Agregamos abreviatura pais
        public String[] paises = new String[250];
        public String[] paisesclave = new String[250];
        //20220311FSV Fin de modificación


        public int iGrupotipo;
        public CasoNuevoContencioso_old(int iGrupo, captura capturaform, Form1 loguin)
        {
            fLoguin = loguin;
            captura = capturaform;
            sTipodesolicitudg = iGrupo.ToString();
            iGrupotipo =  iGrupo;
            InitializeComponent();
            

            DateTime Hoy = DateTime.Today;
            string fecha_actual = Hoy.ToString("dd-MM-yyyy");
            TexboxFecha.Text = fecha_actual;

            //cliente 
            conect conect1 = new conect();
            String query2 = "select ClienteNombre, ClienteId from cliente order by cliente.ClienteNombre;";
            MySqlDataReader respuestastringclient = conect1.getdatareader(query2);
            while (respuestastringclient.Read())
            {
                comboBoxClientes.Items.Add(validareader("ClienteNombre", "ClienteId", respuestastringclient));
            }
            respuestastringclient.Close();
            conect1.Cerrarconexion();

            //Interesados
            conect conect2 = new conect();
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
            MySqlDataReader respuestastringointeresado = conect2.getdatareader(query4);
            while (respuestastringointeresado.Read())
            {
                comboBoxInteresado.Items.Add(validareader("InteresadoNombre", "InteresadoID", respuestastringointeresado));
            }
            respuestastringointeresado.Close();
            conect2.Cerrarconexion();

            //agregamos el Tipo de solicitud que estan permitidos para este grupo 
            conect conect3 = new conect();
            String query = "select TipoSolicitudDescrip, TipoSolicitudDescrip, TipoSolicitudId from tiposolicitud where tiposolicitudGrupo = " + sTipodesolicitudg;
            MySqlDataReader respuestastring = conect3.getdatareader(query);

            while (respuestastring.Read())
            {
                comboTiposolicitud.Items.Add(validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestastring));
            }
            respuestastring.Close();
            conect3.Cerrarconexion();


            //20220311FSV Agregamos el dato del pais
            conect conect_pais = new conect();
            String query5 = "select PaisNombre, PaisId, PaisClave from pais order by PaisNombre;";
            MySqlDataReader respuestastringopais = conect_pais.getdatareader(query5);
            //int paisint = 0;
            while (respuestastringopais.Read())
            {
                cbPaiscaso.Items.Add(validareader("PaisNombre", "PaisId", respuestastringopais));
                //cbCvpais.Items.Add(validareader("PaisClave", "PaisId", respuestastringopais));

                paises[Convert.ToInt32(validareader("PaisClave", "PaisId", respuestastringopais).Value.ToString())] = validareader("PaisClave", "PaisId", respuestastringopais).Text;
                paisesclave[Convert.ToInt32(validareader("PaisClave", "PaisId", respuestastringopais).Value.ToString())] = validareader("PaisNombre", "PaisId", respuestastringopais).Text;
                //paisint++;
            }
            respuestastringopais.Close();
            conect_pais.Cerrarconexion();

            //Seleccionamos el país por Default
            cbPaiscaso.Text = "MEXICO";

            //20220311FVS Fin de modificaicon


            conect conect4 = new conect();
            String sQresponsable = "select UsuarioName, UsuarioId from usuario;";
            MySqlDataReader respuresponsable = conect4.getdatareader(sQresponsable);
            //agregamos los responsables (Usuarios)
            while (respuresponsable.Read())
            {
                comboBoxResponsable.Items.Add(validareader("UsuarioName", "UsuarioId", respuresponsable));
            }
            comboBoxResponsable.Text = fLoguin.sUsername;
            comboBoxResponsable.SelectedValue = fLoguin.sId;
            respuresponsable.Close();
            conect4.Cerrarconexion();


            //combobox de responsables disponibles
            conect conect5 = new conect();
            String sResponsablequery = "select ResponsableClave, ResponsableId, ResponsableNombre from responsable;";
            MySqlDataReader respuestastrinresponsable = conect5.getdatareader(sResponsablequery);
            //int paisint = 0;
            while (respuestastrinresponsable.Read())
            {
                comboBoxFirma.Items.Add(validareader("ResponsableNombre", "ResponsableId", respuestastrinresponsable));
                comboBoxFirma.Text = validareader("ResponsableNombre", "ResponsableId", respuestastrinresponsable).Text;
                //paisint++;
            }
            respuestastrinresponsable.Close();
            conect5.Cerrarconexion();
            //combo box de idiomas disponibles

            conect conect6 = new conect();
            String query3 = "select IdiomaId, IdiomaDescripcion from idioma;";
            MySqlDataReader respuestastringidiom = conect6.getdatareader(query3);
            while (respuestastringidiom.Read())
            {
                comboBoxIdioma.Items.Add(validareader("IdiomaDescripcion", "IdiomaId", respuestastringidiom));
            }
            respuestastringidiom.Close();
            conect6.Cerrarconexion();

            //agregamos los valores combobox sólo necesarios para éste grupo 
            //String queryconten = "select * FROM tipo_asunto_contencioso";
            //MySqlDataReader resp_conten = conect.getdatareader(queryconten);
            //while (resp_conten.Read())
            //{
            //    cbtipo_asunto_contencioso.Items.Add(validareader("Nombre", "id_tipo_asunto_contencioso", resp_conten));
            //}
            //resp_conten.Close();


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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            captura.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            captura.Close();
            fLoguin.Close();
        }

        private void comboBoxClientes_SelectedIndexChanged(object sender, EventArgs e)
        {

            //20220316 LImpiamos la direccion 
            richTextBoxDireccliente.Text = "";

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

            //
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
            Boolean bCaso = false, bCasocliente = false, bcasointeresado = false, breferencias = false, bPrioridades = false;
            try
            {
                //hay que validar cuales campos son obligatorios y cuals son opcionales

                String sComboTiposolicitud = validacombobox(comboTiposolicitud);//*
                //String sComboTiposolicitud = (comboTiposolicitud.SelectedItem as ComboboxItem).Value.ToString();//*

                String sComboboxSubtipo = validacombobox(cbtipo_asunto_contencioso);//.SelectedItem as ComboboxItem).Value.ToString();//*


                //String sComboboxSubtipo2 = validacombobox(comboboxSubtipo2);//.SelectedItem as ComboboxItem).Value.ToString();//*
                //String sComboboxFecha = (comboboxFecha.SelectedItem as ComboboxItem).Value.ToString();//


                String sComboBoxClientes = validacombobox(comboBoxClientes);//.SelectedItem as ComboboxItem).Value.ToString();//casocliente
                String sComboBoxContacto = validacombobox(comboBoxContacto);//.SelectedItem as ComboboxItem).Value.ToString();//casocliente
                String sComboBoxInteresado = validacombobox(comboBoxInteresado);//.SelectedItem as ComboboxItem).Value.ToString();//casointeresado

                // String sComboBoxClase = (comboBoxClase.SelectedItem as ComboboxItem).Value.ToString();//pendiente
                String sComboBoxIdioma = validacombobox(comboBoxIdioma);//.SelectedItem as ComboboxItem).Value.ToString();//*
                String sCcomboBoxFirma = validacombobox(comboBoxFirma);//.SelectedItem as ComboboxItem).Value.ToString();//pendiente
                String sComboBoxResponsable = validacombobox(comboBoxResponsable);//.SelectedItem as ComboboxItem).Value.ToString();//*


                //Dar formatos de fecha 
                String sTextboxFecha = TexboxFecha.Text;//Fecha carta   CasoFechaCartaCliente *
                String stextClientduedate = textClientduedate.Text;//CasoFechaFilingCliente client due date *
                //String stextBoxPlazolegal = textBoxPlazolegal.Text;//textBoxPlazolegalcasofechalegal  CasoFechaLegal
                String stextBoxFechainternacional = "";// textBoxFechainternacional.Text;//CasoFechaInternacional fecha internacional
                String stextBoxReferencia = textBoxReferencia.Text;//pendiente
                String srichTextBoxTitulo = richTextBoxTitulo.Text;//Casotitulo
                String sresponsableid = fLoguin.sId;
                String sCasodenominacion = ""; 

                //if (sComboBoxIdioma != "2")
                //{
                //    sCasodenominacion = srichTextBoxTitulo;
                //    srichTextBoxTitulo = "";
                //}


                //validamos los campos que son obligatorios para poder agregar el caso
                //validamos fecha, referencia, titulo, tiposolicitud. cliente, contacto, interesado
                if (sTextboxFecha != "" && stextBoxReferencia != "" && srichTextBoxTitulo != "" && sComboTiposolicitud != "" && sComboBoxClientes != "" && sComboBoxInteresado != "")
                {
                    sTextboxFecha = cambiaformatofecha(sTextboxFecha);
                    stextClientduedate = cambiaformatofecha(stextClientduedate);
                    //stextBoxPlazolegal = cambiaformatofecha(stextBoxPlazolegal);
                    stextBoxFechainternacional = cambiaformatofecha(stextBoxFechainternacional);


                    conect con = new conect();
                    String sGetcasonumero = "select casoid, casonumero from caso_contencioso order by casoid desc limit 1;";
                    MySqlDataReader respuestastringcasonum = con.getdatareader(sGetcasonumero);
                    String sCasonumero = "";
                    while (respuestastringcasonum.Read())
                    {
                        sCasonumero = validareader("casonumero", "casoid", respuestastringcasonum).Text.ToString();
                    }
                    respuestastringcasonum.Close();
                    var result = sCasonumero.Substring(0, sCasonumero.LastIndexOf('-'));
                    //extencion
                    var resultextencion = sCasonumero.Substring(sCasonumero.LastIndexOf('-')+1);


                    //MessageBox.Show(result);
                    int iValor = Int32.Parse(result) + 1;
                    sCasonumero = iValor + "-" + resultextencion;

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
                                             //inserta caso_Contencioso
                    
                    //20220311FSV Agregamos el pais en el insert
                    int iIdpaiscaso = 0;
                    for (int yuno = 0; yuno < paises.Length; yuno++)
                    {
                        if (paises[yuno] == tbClavepaiscaso.Text)
                        {
                            iIdpaiscaso = yuno;

                        }
                    }
                    //20220311FSV Fin de modificaion

                    //20220307 FSV Este insert no funciona, hay que cambiar valores por default
                    /*
                    String insertcontencioso = " INSERT INTO `caso_contencioso` " +
                                                " (`CasoId`, " +
                                                " `TipoSolicitudId`, " +
                                                " `SubTipoSolicitudId`, " +
                                                " `CasoTituloespanol`, " +
                                                " `CasoTituloingles`, " +
                                                " `IdiomaId`, " +
                                                " `CasoFechaConcesion`, " +
                                                " `CasoFechaLegal`, " +
                                                " `CasoFechaPresentacion`, " +
                                                " `CasoFechaDivulgacionPrevia`, " +
                                                " `CasoFechaRecepcion`, " +
                                                " `CasoFechaVigencia`, " +
                                                " `CasoNumConcedida`, " +
                                                " `CasoNumeroExpedienteLargo`, " +
                                                " `CasoNumero`, " +
                                                " `ResponsableId`, " +
                                                " `TipoMarcaId`, " +
                                                " `CasoFechaAlta`, " +
                                                " `CasoTipoCaptura`, " +
                                                " `CasoTitular`, " +
                                                " `CasoFechaFilingSistema`, " +
                                                " `CasoFechaFilingCliente`, " +
                                                " `CasoFechaCartaCliente`, " +
                                                " `EstatusCasoId`, " +
                                                " `UsuarioId`, " +
                                                " `PaisId`, " +
                                                " `CasoFechaPruebaUsoSig`, " +
                                                " `TipoReservaId`, " +
                                                " `ParteRepresentadaId`, " +
                                                " `SentidoResolucionId`, " +
                                                " `CasoFechaResolucion`, " +
                                                " `CasoEncargadoExterno`, " +
                                                " `caso_contenciosocol`, " +
                                                " `Act`) " +
                                                " VALUES " +
                                                " ('', " +
                                                " '" + sComboTiposolicitud + "', " +
                                                " '" + sComboboxSubtipo + "', " +
                                                " '" + srichTextBoxTitulo + "', " +
                                                " '" + sCasodenominacion + "', " +
                                                " '" + sComboBoxIdioma + "', " +
                                                " '', " +
                                                " '', " +
                                                " '', " +
                                                " '', " +
                                                " '', " +
                                                " '', " +
                                                " '', " +
                                                " '', " +
                                                " '" + sCasonumero + "', " +
                                                " '" + sresponsableid + "', " +
                                                " '', " +
                                                " '" + sTextboxFecha + "', " +
                                                " '', " +
                                                " '', " +//este es el interesado con prioridad 1 en texto
                                                " '" + sTextboxFecha + "', " +
                                                " '" + stextClientduedate + "', " +
                                                " '', " +
                                                " '" + sEstatudID + "', " +
                                                " '" + sresponsableid + "', " +
                                                " '', " +
                                                " '', " +
                                                " '', " +
                                                " '', " +
                                                " '', " +
                                                " '', " +
                                                " '', " +
                                                " '', " +
                                                " ''); ";  */

                    String insertcontencioso = " INSERT INTO `caso_contencioso` " +
                                                " (`CasoId`, " +
                                                " `TipoSolicitudId`, " +
                                                " `SubTipoSolicitudId`, " +
                                                " `CasoTituloespanol`, " +
                                                " `CasoTituloingles`, " +
                                                " `IdiomaId`, " +
                                                " `CasoFechaConcesion`, " +
                                                " `CasoFechaLegal`, " +
                                                " `CasoFechaPresentacion`, " +
                                                " `CasoFechaDivulgacionPrevia`, " +
                                                " `CasoFechaRecepcion`, " +
                                                " `CasoFechaVigencia`, " +
                                                " `CasoNumConcedida`, " +
                                                " `CasoNumeroExpedienteLargo`, " +
                                                " `CasoNumero`, " +
                                                " `ResponsableId`, " +
                                                " `TipoMarcaId`, " +
                                                " `CasoFechaAlta`, " +
                                                " `CasoTipoCaptura`, " +
                                                " `CasoTitular`, " +
                                                " `CasoFechaFilingSistema`, " +
                                                " `CasoFechaFilingCliente`, " +
                                                " `CasoFechaCartaCliente`, " +
                                                " `EstatusCasoId`, " +
                                                " `UsuarioId`, " +
                                                " `PaisId`, " +
                                                " `CasoFechaPruebaUsoSig`, " +
                                                " `TipoReservaId`, " +
                                                " `ParteRepresentadaId`, " +
                                                " `SentidoResolucionId`, " +
                                                " `CasoFechaResolucion`, " +
                                                " `CasoEncargadoExterno`, " +
                                                " `caso_contenciosocol`) " +
                                                " VALUES " +
                                                " (null, " +
                                                " '" + sComboTiposolicitud + "', " +
                                                " null, " +

                                                " '" + srichTextBoxTitulo + "', " +
                                                " '" + sCasodenominacion + "', " +
                                                " '" + sComboBoxIdioma + "', " +

                                                " 0, " +
                                                " 0, " +
                                                " 0, " +
                                                " 0, " +

                                                " 0, " +
                                                " 0, " +
                                                " '', " +
                                                " '', " +

                                                " '" + sCasonumero + "', " +
                                                " '" + sresponsableid + "', " +
                                                " 0, " +

                                                " '" + sTextboxFecha + "', " +
                                                " '', " +
                                                " '', " +//este es el interesado con prioridad 1 en texto
                                                " '" + sTextboxFecha + "', " +

                                                " '" + stextClientduedate + "', " +
                                                //20220311FSV Agregamos fecha CasoFechaFilingSistema
                                                //" 0, " +
                                                " '" + sTextboxFecha + "', " +
                                                //20220311FSV Fin de modificación
                                                " '" + sEstatudID + "', " +
                                                " '" + sresponsableid + "', " +
                                                //20220311FSV Agregamos el pais
                                                //" 0, " +
                                                " '" + iIdpaiscaso + "', " +
                                                //20220311FSV
                                                " 0, " +
                                                " 0, " +
                                                " 0, " +

                                                " 0, " +
                                                " 0, " +
                                                " '', " +
                                                " ''); ";






                    //String insert_marcas = "INSERT INTO `caso_marcas` " +
                    //                         "(`CasoId`, " +
                    //                         "`TipoSolicitudId`, " +
                    //                         "`SubTipoSolicitudId`, " +
                    //                         "`CasoTituloespanol`, " +
                    //                         "`CasoTituloingles`, " +
                    //                         "`IdiomaId`, " +
                    //                         "`CasoFechaConcesion`, " +
                    //                         "`CasoFechaLegal`, " +
                    //                         "`CasoFechaDivulgacionPrevia`, " +
                    //                         "`CasoFechaRecepcion`, " +
                    //                         "`CasoFechaVigencia`, " +
                    //                         "`CasoNumeroConcedida`, " +
                    //                         "`CasoNumeroExpedienteLargo`, " +
                    //                         "`CasoNumero`, " +
                    //                         "`ResponsableId`, " +
                    //                         "`TipoMarcaId`, " +
                    //                         "`CasoLeyendaNoReservable`, " +
                    //                         "`CasoFechaAlta`, " +
                    //                         "`CasoTipoCaptura`, " +
                    //                         "`CasoTitular`, " +
                    //                         "`CasoFechaFilingSistema`, " +
                    //                         "`CasoFechaFilingCliente`, " +
                    //                         "`CasoFechaCartaCliente`, " +
                    //                         "`EstatusCasoId`, " +
                    //                         "`UsuarioId`, " +
                    //                         "`PaisId`, " +
                    //                         "`CasoFechaPruebaUsoSig`, " +
                    //                         "`CasoNumConcedida`, " +
                    //                         "`CasoFechaprobouso`, " +
                    //                         "`CasoFechainiciouso`) " +
                    //                         "VALUES " +
                    //                         "('', " +
                    //                         "'" + sComboTiposolicitud + "', " +
                    //                         "'" + sComboboxSubtipo + "', " +
                    //                         "'" + srichTextBoxTitulo + "', " +
                    //                         "'" + sCasodenominacion + "', " +
                    //                         "'" + sComboBoxIdioma + "', " +
                    //                         "'', " +
                    //                         "'', " +
                    //                         "'', " +
                    //                         "'', " +
                    //                         "'', " +
                    //                         "'', " +
                    //                         "'', " +
                    //                         "'" + sCasonumero + "', " +
                    //                         "'" + sresponsableid + "', " +
                    //                         "'', " +
                    //                         "'', " +
                    //                         "'" + sTextboxFecha + "', " +
                    //                         "'', " +
                    //                         "'', " +
                    //                         "'', " +
                    //                         "'', " +
                    //                         "'', " +
                    //                         "'', " +
                    //                         "'" + sresponsableid + "', " +
                    //                         "'', " +
                    //                         "'', " +
                    //                         "'', " +
                    //                         "'', " +
                    //                         "''); ";
                    //20220307 Falta el ID marca error al mandarlo vacio.... le cambiamos para ponerlo nulo
                    MySqlDataReader respuestastring = con.getdatareader(insertcontencioso);
                    respuestastring.Close();

                    //20220308 Hay que buscarlo de la tabla contenciosos
                    //String sGetid = "SELECT * FROM `caso_patente` order by CasoId desc limit 1";
                    String sGetid = "SELECT * FROM `caso_contencioso` order by CasoId desc limit 1";
                    //20220308 Fin de modificación
                    MySqlDataReader respuestastringid = con.getdatareader(sGetid);
                    String sCasoid = "";
                    while (respuestastringid.Read())
                    {
                        sCasoid = validareader("CasoId", "CasoId", respuestastringid).Value.ToString();
                        //MessageBox.Show("El caso se agrego correctamente con CasoID: " + sCasoid + " Casonumero: " + sCasonumero);
                        bCaso = true;
                    }
                    respuestastringid.Close();



                    String insertacliente = "INSERT INTO `casocliente` " +
                                            "(`CasoClienteId`, " +
                                            "`ClienteId`, " +
                                            "`contactoid`, " +
                                            "`CasoId`, " +
                                            "`TipoSolicitudId`, " +
                                            "`CasoClienteIndCorres`) " +
                                            "VALUES " +
                                            //20220308 Cambiamos por null
                                            //"('', " +
                                            "(null, " +
                                            //20220308 Fin de Modificacion
                                            sComboBoxClientes + ", " +
                                            sComboBoxContacto + ", " +
                                            sCasoid + ", " +
                                            sComboTiposolicitud + ", " +
                                            "0);";
                    MySqlDataReader respuestastringinsertclient = con.getdatareader(insertacliente);
                    if (respuestastringinsertclient.RecordsAffected == 1)
                    {
                        bCasocliente = true;
                        //MessageBox.Show("Se inserto en casoclientes");
                    }
                    respuestastringinsertclient.Close();



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
                                                //20220308 Cambiamos por null
                                                //"'', " +
                                                "null, " +
                                                //20220308 Fin de modificaicon
                                                sComboBoxInteresado + ", " +
                                                sCasoid + ", " +
                                                sComboTiposolicitud + ", " +
                                                "1," +
                                                "1," +
                                                "null);";
                    MySqlDataReader respuestastringinscasoint = con.getdatareader(insertcasointeresado);
                    if (respuestastringinscasoint.RecordsAffected == 1)
                    {
                        //MessageBox.Show("Se inserto en casointeresado");
                        bcasointeresado = true;
                    }
                    respuestastringinscasoint.Close();



                    //agregar a¿rowaffected y validar que se inserto
                    String insertreferencia = " INSERT INTO `referencia` " +
                                                " (`ReferenciaId`, " +
                                                " `CasoId`, " +
                                                " `TipoSolicitudId`, " +
                                                " `TipoReferenciaId`, " +
                                                " `ReferenciaNombre`) " +
                                                " VALUES (" +
                                                //20220308 Fsalazar mandar nulo en vez de vacio
                                                //" '', " +
                                                " null, " +
                                                //20220308 Fin Modificacion
                                                sCasoid + ", " +
                                                sComboTiposolicitud + ", " +
                                                "1, " +
                                                "'" + stextBoxReferencia + "'); ";
                    MySqlDataReader respinsertreferencia = con.getdatareader(insertreferencia);
                    if (respinsertreferencia.RecordsAffected == 1)
                    {
                        //MessageBox.Show("Se inserto en referencia");
                        breferencias = true;
                    }
                    respinsertreferencia.Close();




                    if (bCaso && bCasocliente && bcasointeresado && breferencias)
                    {
                        MessageBox.Show("El caso se agrego correctamente con el CasoNúmero: " + sCasonumero);
                        DialogResult results = MessageBox.Show("¿Desea agregar un caso nuevo del mismo tipo?", "Agregar Caso Marcas", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (results == DialogResult.Yes)
                        {
                            //code for Yes
                            CasoNuevoContencioso_old nuevocasothis = new CasoNuevoContencioso_old(iGrupotipo, captura, fLoguin);
                            nuevocasothis.Show();
                            this.Close();
                        }
                        else if (results == DialogResult.No)
                        {
                            //code for No
                            captura.Show();
                            this.Close();
                        }//}
                        //else if (results == DialogResult.Cancel)
                        //{
                        //    //code for Cancel
                        //}
                    }
                    else
                        {

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
            }
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

        private void textClientduedate_KeyPress(object sender, KeyPressEventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
        {
            addnuevotitular addnuevotitular = new addnuevotitular(this);
            if (addnuevotitular.ShowDialog() == DialogResult.OK)
            {
                comboBoxInteresado.Items.Add(addnuevotitular.cBcomboInteresadotitular);
                comboBoxInteresado.Text = addnuevotitular.sNombrenuevotitular;
            }
        }

        private void comboBoxResponsable_SelectedIndexChanged(object sender, EventArgs e)
        {

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
    }

}