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
    public partial class CasonuevoConsulta : Form
    {
        public Form1 fLoguin;
        public captura captura;
        public String sTipodesolicitudg;

        //20220311FSV Agregamos abreviatura pais
        public int iGrupotipo;
        public String[] paises = new String[250];
        public String[] paisesclave = new String[250];
        //20220311FSV Fin de modificación
        public int TipoSol;
        //20220330FSV 
        funcionesdicss objfuncionesdicss = new funcionesdicss();




        public String validacombobox(ComboBox combo)
        {
            //20220314FSV Agregamos método para formatear contenido de combos
            if (combo.SelectedItem != null){
                return (combo.SelectedItem as ComboboxItem).Value.ToString();}
            else{
                return "";}
        }

        public String cambiaformatofecha(String Fechauno)
        {
            //20220314FSV Agregamos método para formatear fechas
            if (Fechauno != ""){
                String año = Fechauno.Substring(6, 4);//yyyy
                String mes = Fechauno.Substring(3, 2);//mm
                String dia = Fechauno.Substring(0, 2);//dd
                return año + "-" + mes + "-" + dia;
            }
            else{
                return "";}
        }
        public CasonuevoConsulta(int iGrupo, captura capturaform, Form1 loguin)
        {
            fLoguin = loguin;
            captura = capturaform;
            sTipodesolicitudg = iGrupo.ToString();
            InitializeComponent();
            conect conect = new conect();
            //cliente 
            String query2 = "select ClienteNombre, ClienteId from cliente order by cliente.ClienteNombre;";
            MySqlDataReader respuestastringclient = conect.getdatareader(query2);
            while (respuestastringclient.Read())
            {
                comboBoxClientes.Items.Add(validareader("ClienteNombre", "ClienteId", respuestastringclient));
            }
            respuestastringclient.Close();
            conect.Cerrarconexion();

            //interesados
            conect conect2 = new conect();
            String query4 = "select InteresadoID, InteresadoNombre from interesado order by InteresadoNombre;";
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


            conect conect4 = new conect();
            String sQresponsable = "select UsuarioNombre, UsuarioId from usuario where UsuarioIndActivo = 1;";
            MySqlDataReader respuresponsable = conect4.getdatareader(sQresponsable);
            //agregamos los responsables (Usuarios)
            while (respuresponsable.Read())
            {
                comboBoxResponsable.Items.Add(validareader("UsuarioNombre", "UsuarioId", respuresponsable));
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
            cbPaiscaso.Text = "MEXICO";
            //20220311FVS Fin de modificaicon


            //agregamos los valores combobox sólo necesarios para éste grupo
            //20220311FSV Quitamos este valor para que no deje pasar
            /*String query_consultas = "select * from tipo_consultas;";
            MySqlDataReader resp_consul = conect.getdatareader(query_consultas);
            while (resp_consul.Read())
            {
                comboboxSubtipo.Items.Add(validareader("Nombre", "tipo_consultasid", resp_consul));
            }
            resp_consul.Close();*/
            //20220311FSV Fin de modificación

        }

        private void button3_Click(object sender, EventArgs e)
        {
            captura.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            captura.Show();
            this.Close();

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
            try
            {
                conect con6 = new conect();
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
                MySqlDataReader respuestastringclient = con6.getdatareader(query2);

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
                con6.Cerrarconexion();

                //
                //Borramos los cantactos anteriores, si es que los ubiera
                comboBoxContacto.Text = "";
                richTextBox1.Text = "";
                comboBoxContacto.Items.Clear();
                conect con7 = new conect();
                String query3 = "select ContactoId, ContactoNombre, ContactoTelefono, ContactoEmail from contacto where clienteid = " + (comboBoxClientes.SelectedItem as ComboboxItem).Value.ToString();
                MySqlDataReader respuestastringcontacto = con7.getdatareader(query3);

                while (respuestastringcontacto.Read())
                {
                    comboBoxContacto.Items.Add(validareader("ContactoNombre", "ContactoId", respuestastringcontacto));
                }
                respuestastringcontacto.Close();
                con7.Cerrarconexion();
            }
            catch (Exception exs) {
                new filelog("", ""+exs.Message);
            }
            
        }

        private void comboBoxContacto_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            conect con8 = new conect();
            String sQuerycontact = "select * from contacto where  ContactoId = " + (comboBoxContacto.SelectedItem as ComboboxItem).Value.ToString();
            String sCorreocontacto = "";
            MySqlDataReader resp_correoscontact = con8.getdatareader(sQuerycontact);
            while (resp_correoscontact.Read())
            {
                sCorreocontacto += validareader("ContactoEmail", "ContactoEmail", resp_correoscontact).Text;
            }
            resp_correoscontact.Close();
            con8.Cerrarconexion();
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
                //20220314FSV (1) Inicio Recopilar datos a Grabar
                String sComboTiposolicitud = validacombobox(comboTiposolicitud);
                TipoSol = Convert.ToInt32( validacombobox(comboTiposolicitud));
                String sComboboxSubtipo = validacombobox(comboboxSubtipo);
                String sComboBoxClientes = validacombobox(comboBoxClientes);
                String sComboBoxContacto = validacombobox(comboBoxContacto);
                String sComboBoxInteresado = validacombobox(comboBoxInteresado);
                String sComboBoxIdioma = validacombobox(comboBoxIdioma);
                String sCcomboBoxFirma = validacombobox(comboBoxFirma);
                String sComboBoxResponsable = validacombobox(comboBoxResponsable);
                String sTextboxFecha = TexboxFecha.Text;
                String stextClientduedate = textClientduedate.Text;
                String stextBoxFechainternacional = "";
                String stextBoxReferencia = textBoxReferencia.Text;
                String srichTextBoxTitulo = richTextBoxTitulo.Text;
                String srtbMotivo = rtbMotivo.Text;
                String sresponsableid = fLoguin.sId;
                String sCasodenominacion = "";
                //20220314FSV (1) Inicio Recopilar datos a Grabar

                int iIdpaiscaso = 0;
                for (int yuno = 0; yuno < paises.Length; yuno++)
                {
                    if (paises[yuno] == tbClavepaiscaso.Text)
                    {
                        iIdpaiscaso = yuno;

                    }
                }



                //if (sComboBoxIdioma != "2")
                //{
                //    sCasodenominacion = srichTextBoxTitulo;
                //    srichTextBoxTitulo = "";
                //}

                String sTituloespanol = "";
                String sTituloIngles = "";

                if (sComboBoxIdioma == "2")
                {//está en español
                    sTituloespanol = srichTextBoxTitulo;
                }
                else
                {
                    sTituloIngles = srichTextBoxTitulo;
                }

                if (comboBoxIdioma.Text == "")
                {
                    MessageBox.Show("Debe seleccionar un idioma para el caso");
                    return;
                }



                if (sTextboxFecha != "" && stextBoxReferencia != "" && srichTextBoxTitulo != "" && sComboTiposolicitud != "" && sComboBoxClientes != "" && sComboBoxInteresado != "")
                {
                    sTextboxFecha = cambiaformatofecha(sTextboxFecha);
                    stextClientduedate = cambiaformatofecha(stextClientduedate);
                    stextBoxFechainternacional = cambiaformatofecha(stextBoxFechainternacional);


                    conect con9 = new conect();
                    String sGetcasonumero = "select casoid, casonumero from caso_consulta order by casoid desc limit 1;";
                    MySqlDataReader respuestastringcasonum = con9.getdatareader(sGetcasonumero);
                    String sCasonumero = "";
                    while (respuestastringcasonum.Read())
                    {
                        sCasonumero = validareader("casonumero", "casoid", respuestastringcasonum).Text.ToString();
                    }
                    respuestastringcasonum.Close();
                    con9.Cerrarconexion();
                    var result = sCasonumero.Substring(0, sCasonumero.LastIndexOf('-'));
                    var resultextencion = sCasonumero.Substring(sCasonumero.LastIndexOf('-') + 1);
                    int iValor = Int32.Parse(result) + 1; 
                    sCasonumero = iValor + "-" + resultextencion;

                    String sEstatudID = "1";

                    //String insertcontencioso = " INSERT INTO `caso_consulta_tmp1` " +
                    String insertcontencioso = " INSERT INTO `caso_consulta` " +
                    
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
                                                //" `caso_consultacol`) " +
                                                " `caso_consultacol`,`CasoMotivo`) " +
                                                " VALUES " +
                                                " (null, " +
                                                " '" + sComboTiposolicitud + "', " +
                                                //20220404FSV Agregamos el subtipo de solicitud
                                                //" null, " +
                                                " '" + sComboboxSubtipo + "', " +
                                                //20220404FSV Fin de modificacion
                                                //" '" + srichTextBoxTitulo + "', " +
                                                //" '" + sCasodenominacion + "', " +
                                                " '" + sTituloespanol + "', " +
                                                " '" + sTituloIngles + "', " +
                                                
                                                " '" + sComboBoxIdioma + "', " +
                                                " 0, " +
                                                " 0, " +
                                                //" 0, " +
                                                " '" + sTextboxFecha + "', " + //Fecha presentacion
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
                                                " '', " +
                                                " '" + sTextboxFecha + "', " +
                                                " '" + stextClientduedate + "', " +
                                                " '" + sTextboxFecha + "', " +
                                                " '" + sEstatudID + "', " +
                                                " '" + sresponsableid + "', " +
                                                " '" + iIdpaiscaso + "', " +
                                                " 0, " +
                                                //" ''); ";
                                                " '','" + srtbMotivo + "'); ";

                    //Insertamos el caso consulta
                    conect con10 = new conect();
                    MySqlDataReader respuestastring = con10.getdatareader(insertcontencioso);
                    if (respuestastring.RecordsAffected ==1) {
                        new filelog("", " Insert correcto caso_consulta");
                    }
                    respuestastring.Close();
                    con10.Cerrarconexion();

                    //Extraemos el ID asignado por el campo autonumérico
                    conect con11 = new conect();
                    String sGetid = "SELECT * FROM `caso_consulta` order by CasoId desc limit 1";
                    MySqlDataReader respuestastringid = con11.getdatareader(sGetid);
                    String sCasoid = "";
                    while (respuestastringid.Read())
                    {
                        sCasoid = validareader("CasoId", "CasoId", respuestastringid).Value.ToString();
                        bCaso = true;
                    }
                    respuestastringid.Close();
                    con11.Cerrarconexion();



                    //Insertamos en claso cliente
                    conect con12 = new conect();
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
                    MySqlDataReader respuestastringinsertclient = con12.getdatareader(insertacliente);
                    if (respuestastringinsertclient.RecordsAffected == 1)
                    {
                        bCasocliente = true;
                    }
                    respuestastringinsertclient.Close();
                    con12.Cerrarconexion();


                    //insertamos en caso interesado
                    conect con13 = new conect();
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
                    MySqlDataReader respuestastringinscasoint = con13.getdatareader(insertcasointeresado);
                    if (respuestastringinscasoint.RecordsAffected == 1)
                    {
                        bcasointeresado = true;
                    }
                    respuestastringinscasoint.Close();
                    con13.Cerrarconexion();


                    //insertamos en referencia
                    conect con14 = new conect();
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
                    MySqlDataReader respinsertreferencia = con14.getdatareader(insertreferencia);
                    if (respinsertreferencia.RecordsAffected == 1)
                    {
                        breferencias = true;
                    }
                    respinsertreferencia.Close();
                    con14.Cerrarconexion();


                    //validamos
                    if (bCaso && bCasocliente && bcasointeresado && breferencias)
                    {
                        MessageBox.Show("El caso se agrego correctamente con el CasoNúmero: " + sCasonumero);
                        DialogResult results = MessageBox.Show("¿Desea agregar un caso nuevo del mismo tipo?", "Agregar Caso Marcas", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (results == DialogResult.Yes)
                        {
                            CasonuevoConsulta nuevocasothis = new CasonuevoConsulta(iGrupotipo, captura, fLoguin);
                            nuevocasothis.Show();
                            this.Close();
                        }
                        else if (results == DialogResult.No)
                        {
                            captura.Show();
                            this.Close();
                        }
                    }
                    
                }
                else{
                    MessageBox.Show("Debe llenar los campos obligarorios para caso");}

            }
            catch (Exception E){
            
                MessageBox.Show("Verifique que todos los campos estén correctos. " + E);
                new filelog("", " mensaje: " + E.Message);
            }
            //20220314FSV Fin alta nuevo caso consulta

        }

        private void comboTiposolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboboxSubtipo.Items.Clear();
            comboboxSubtipo.Text = "";
            conect con = new conect();
            String query = "select SubTipoSolicitudId, SubTipoSolicitudDescripcion from subtiposolicitud where tiposolicitudID =" + (comboTiposolicitud.SelectedItem as ComboboxItem).Value.ToString();
            MySqlDataReader respuestastring = con.getdatareader(query);
            while (respuestastring.Read())
            {
                comboboxSubtipo.Items.Add(validareader("SubTipoSolicitudDescripcion", "SubTipoSolicitudId", respuestastring));
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

        private void button6_Click(object sender, EventArgs e)
        {
            addnuevotitular addnuevotitular = new addnuevotitular(this, fLoguin,TipoSol);
            if (addnuevotitular.ShowDialog() == DialogResult.OK)
            {
                comboBoxInteresado.Items.Add(addnuevotitular.cBcomboInteresadotitular);
                comboBoxInteresado.Text = addnuevotitular.sNombrenuevotitular;
            }
        }

        private void TexboxFecha_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(TexboxFecha);
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

        private void TexboxFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacamposfecha(TexboxFecha, e);

            //if (Char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = false;
            //}
            //else if (Char.IsControl(e.KeyChar))
            //{
            //    e.Handled = false;
            //}
            //else if (Char.IsSeparator(e.KeyChar))
            //{
            //    e.Handled = false;
            //}
            //else
            //{
            //    e.Handled = true;
            //}
            //if (TexboxFecha.Text.Length == 2)
            //{
            //    TexboxFecha.Text = TexboxFecha.Text + "-";
            //    TexboxFecha.SelectionStart = TexboxFecha.Text.Length;
            //}
            //if (TexboxFecha.Text.Length == 5)
            //{
            //    TexboxFecha.Text = TexboxFecha.Text + "-";
            //    TexboxFecha.SelectionStart = TexboxFecha.Text.Length;
            //}
        }

        private void textClientduedate_Validating(object sender, CancelEventArgs e)
        {
            objfuncionesdicss.validafecha(textClientduedate);
        }

        private void textClientduedate_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacamposfecha(textClientduedate, e);
            //if (Char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = false;
            //}
            //else if (Char.IsControl(e.KeyChar))
            //{
            //    e.Handled = false;
            //}
            //else if (Char.IsSeparator(e.KeyChar))
            //{
            //    e.Handled = false;
            //}
            //else
            //{
            //    e.Handled = true;
            //}

            //if (textClientduedate.Text.Length == 2)
            //{
            //    textClientduedate.Text = textClientduedate.Text + "-";
            //    textClientduedate.SelectionStart = textClientduedate.Text.Length;
            //}
            //if (textClientduedate.Text.Length == 5)
            //{
            //    textClientduedate.Text = textClientduedate.Text + "-";
            //    textClientduedate.SelectionStart = textClientduedate.Text.Length;
            //}
        }

        private void buscapaisporclave()
        {
            try {
                conect con = new conect();
                string sClavePais = tbClavepaiscaso.Text;

                String query5 = "SELECT PaisNombre, PaisId, PaisClave FROM pais WHERE PaisClave = '" + sClavePais + "';";
                MySqlDataReader respuestastringopais = con.getdatareader(query5);
                while (respuestastringopais.Read())
                {
                    String sPaisNombre = validareader("PaisNombre", "PaisId", respuestastringopais).Text;
                    cbPaiscaso.Text = sPaisNombre;
                }
                respuestastringopais.Close();
                con.Cerrarconexion();
            }
            catch (Exception exs) {
                new filelog("" ," mensaje: "+exs.Message);
            }
            
        }

        private void tbClavepaiscaso_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscapaisporclave();
            }
        }

        private void TexboxFecha_Leave(object sender, EventArgs e)
        {
            TexboxFecha.Text = TexboxFecha.Text.Replace("/", "-").Replace(".", "-");
        }

        private void textClientduedate_Leave(object sender, EventArgs e)
        {
            textClientduedate.Text = textClientduedate.Text.Replace("/", "-").Replace(".", "-");
        }

        private void TexboxFecha_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
