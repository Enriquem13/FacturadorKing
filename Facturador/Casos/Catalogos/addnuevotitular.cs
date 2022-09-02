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
    public partial class addnuevotitular : Form
    {
        public Form1 login;
        String sIdusuario = "";

        Form fRemitentecasonuevo;
        public ComboboxItem cBcomboInteresadotitular { get; set; }
        public String ab;
        public int sTiposolicitudiscaso;
        public String sNombrenuevotitular  {get; set;}
        public int iTitularid { get; set; }
        public addnuevotitular(Form objremitente, Form1 fLogin, int Tiposolicitud)
        {

            login = fLogin;
            String IDUsuario = login.sId;
            InitializeComponent();
            sTiposolicitudiscaso = Tiposolicitud;
            fRemitentecasonuevo = objremitente;
            fisica.Enabled = false;
            moral.Enabled = false;
            

            conect con1 = new conect();
            String pais = "SELECT * FROM  pais where PaisNombre not like'%OFICINA%' AND  PaisNombre not like'%ORGANIZACION%'";
            MySqlDataReader rpais = con1.getdatareader(pais);
            while (rpais.Read())
            {
                combopais.Items.Add(validareader("PaisClave", "PaisId", rpais));
                //CB_idioma_cliente.Items
            }
            rpais.Close();

            //Agregamos lista de monedas
            String kwery4 = "SELECT * FROM  moneda";
            MySqlDataReader respuestastring4 = con1.getdatareader(kwery4);
            while (respuestastring4.Read())
            {
                cbModena.Items.Add(validareader("MonedaDescrip", "MonedaId", respuestastring4));
            }
            respuestastring4.Close();
            //Agregamos tipos de cliente
            String kwery6 = "SELECT * FROM  tipocliente";
            MySqlDataReader respuestastring6 = con1.getdatareader(kwery6);
            while (respuestastring6.Read())
            {
                cbTipocliente.Items.Add(validareader("TipoClienteDescrip", "TipoClienteId", respuestastring6));
            }
            respuestastring6.Close();
            String paisnombre = "SELECT concat_ws('--',PaisClave,PaisNombre) as PaisNombre,PaisId  FROM  pais where PaisNombre not like'%OFICINA%' AND  PaisNombre not like'%ORGANIZACION%'";
            MySqlDataReader rpaiss = con1.getdatareader(paisnombre);
            while (rpaiss.Read())
            {
                PaisNombre.Items.Add(validareader("PaisNombre", "PaisId", rpaiss));
                //CB_idioma_cliente.Items
            }
            rpaiss.Close();





            //llenamos el campo tipo
            conect conectw2 = new conect();
            String query2 = "SELECT * FROM tiposolicitud where TipoSolicitudId = " + Tiposolicitud + ";";
            MySqlDataReader respuestastring20 = conectw2.getdatareader(query2);
            while (respuestastring20.Read())
            {

                tipo.Text = validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestastring20).Text;
                //

            }
             

            conect conectsociedad = new conect();
            String kwery3 = "SELECT SociedadId, SociedadDescrip  FROM  sociedad";
            MySqlDataReader respuestastring3 = conectsociedad.getdatareader(kwery3);


            while (respuestastring3.Read())
            {
                CB_tiposociedad.Items.Add(validareader("SociedadDescrip", "SociedadId", respuestastring3));
            }
            respuestastring3.Close();
            conectsociedad.Cerrarconexion();

            conect conectholder = new conect();
            String kweryholder = "SELECT * FROM holder ";
            MySqlDataReader respuestastringholder = conectholder.getdatareader(kweryholder);

            while (respuestastringholder.Read())
            {
                CB_interesadoholder.Items.Add(validareader("HolderNombre", "HolderId", respuestastringholder));
            }
            respuestastringholder.Close();
            conectholder.Cerrarconexion();



            conect conecttipodireccion = new conect();
            String kwertipodireccion = "SELECT * FROM tipodireccion";
            MySqlDataReader respuestastringtipodireccion = conecttipodireccion.getdatareader(kwertipodireccion);

            while (respuestastringtipodireccion.Read())
            {
                CB_interesadoDtipodireccion.Items.Add(validareader("TipoDireccionDescrip", "TipoDireccionId", respuestastringtipodireccion));
            }
            respuestastringtipodireccion.Close();
            conecttipodireccion.Cerrarconexion();



            //conect conecttiporelacion = new conect();
            //String kwerytiporelacion = "SELECT TipoRelacionId, TipoRelacionDescrip FROM  tiporelacion ";
            //MySqlDataReader respuestastringtiporelacion = conecttiporelacion.getdatareader(kwerytiporelacion);
            //while (respuestastringtiporelacion.Read())
            //{
            //    CB_interesadoTipoderelacion.Items.Add(validareader("TipoRelacionDescrip", "TipoRelacionId", respuestastringtiporelacion));
            //}
            //respuestastringtiporelacion.Close();
            //conecttiporelacion.Cerrarconexion();

            //conect conecttiposolicitu = new conect();
            //String kwerytiposolicitud = "SELECT TipoSolicitudId, TipoSolicitudDescrip FROM tiposolicitud ";
            //MySqlDataReader respuestastringtiposolicitud = conecttiposolicitu.getdatareader(kwerytiposolicitud);
            //while (respuestastringtiposolicitud.Read())
            //{
            //    CB_interesadotiposolicitud.Items.Add(validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestastringtiposolicitud));
            //}
            //respuestastringtiposolicitud.Close();
            //conecttiposolicitu.Cerrarconexion();
        }


        private void Bagregar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
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

        private void button2_Click(object sender, EventArgs e)
        {

               
            if (
                
                tipopersona.SelectedItem != null && 

                combopais.SelectedItem != null && tipopersona.Text !=""

                )
            {
                if (!TB_nombreinteresado.Text.Trim().Equals("") || tbNombreEmpresa.Text != "") { 
                try
                {
                    String TipoP = "";
                    DateTime dateTime = DateTime.UtcNow.Date;
                    String sFechaalta = dateTime.ToString("yyyy/MM/dd hh:mm:sss");
                    String sResponsable = "NULL";
                    String interesadoid = "";
                    String tiposociedad = "NULL";

                    if (CB_tiposociedad.SelectedItem != null)
                    {
                        tiposociedad = (CB_tiposociedad.SelectedItem as ComboboxItem).Value.ToString();
                    }

                    String holder = "NULL";

                    if (CB_interesadoholder.SelectedItem != null)
                    {
                        holder = (CB_interesadoholder.SelectedItem as ComboboxItem).Value.ToString();
                    }

                    //segun el tipo de persona sera los datos que inserte
                    String Tipo = (tipopersona.SelectedItem as ComboboxItem).Text;
                    if ((tipopersona.SelectedItem as ComboboxItem).Text == "Física Extranjera" || (tipopersona.SelectedItem as ComboboxItem).Text == "Física Nacional")
                    {

                        if (Tipo == "Física Extranjera")
                        {
                            TipoP = "FE";
                        }
                        if (Tipo == "Física Nacional")
                        {
                            TipoP = "FN";
                        }
                        if (TB_nombreinteresado.Text != "")
                        {


                            conect coninsert = new conect();
                            String Nombre_Combinado = TB_nombreinteresado.Text + " " + TB_apellidopaternointeresado.Text + " " + TB_apellidomaternointeresado.Text;
                            String queryinsert = "INSERT INTO `interesado` " +
                                " (`InteresadoID`, " +
                                 " `NombreUtilInt`, " +
                                " `InteresadoTipoPersonaSAT`, " +
                                 " `IdiomaId`, " +
                                " `InteresadoNombre`, " +
                                " `InteresadoApPaterno`, " +
                                " `InteresadoApMaterno`, " +
                                "`InteresadoRFC`, " +
                                " `SociedadID`, " +
                                " `InteresadoRGP`, " +
                                " `InteresadoFechaAlta`, " +
                                " `PaisId`, " +
                                " `InteresadoIndAct`, " +
                                " `InteresadoShort`, " +
                                "`InteresadoPoder`, " +
                                " `InteresadoCurp`, " +
                                "`InteresadoMail`, " +
                                "`holderid`, " +
                                "`UsuarioId`, " +
                                "`UsuarioFechaCapturo`, " +
                                " `InteresadoTelefono`) " +
                                "VALUES  " +
                                "(NULL,'" +
                                "" + Nombre_Combinado + "', " +

                            "'" + (tipopersona.SelectedItem as ComboboxItem).Value + "', " +
                                "'" + (cbIdioma.SelectedItem as ComboboxItem).Value + "', '" +
                                TB_nombreinteresado.Text +
                                "', '" +
                                TB_apellidopaternointeresado.Text + "', '" +
                                TB_apellidomaternointeresado.Text + "', '" +

                                TB_rfc.Text + "', '" +

                                tiposociedad + "', '" +

                                TB_rgp.Text + "', '" +

                                sFechaalta + "', '" +

                                (Nacionalidad.SelectedItem as ComboboxItem).Value + "', " +
                                "'1', '" +
                                 TB_razonsocial.Text +
                                "', '" +
                                TB_poderinteresado.Text +
                                "', '" +
                                TB_curp.Text +
                                "', '" +
                                TB_correo_interesado.Text +
                                "', " +
                                holder +
                                ", '" +
                                login.sId +
                                "', '" +
                                sFechaalta + "', '" +
                                TB_telefono_interesado.Text + "');";
                            MySqlDataReader respuestastringinsert = coninsert.getdatareader(queryinsert);


                            if (respuestastringinsert == null)
                            {
                                MessageBox.Show("No se pudro agregar un nuevo interesado");
                            }
                            else
                            {

                                if (CB_interesadoDtipodireccion.SelectedItem != null && TB_interesadoDcalle.Text != "" && TB_interesadoDnumext.Text != "" && TB_interesadoDnumint.Text != "")
                                {
                                    conect conectinteresado = new conect();
                                    String query2 = "SELECT InteresadoID FROM interesado order by InteresadoID DESC  limit 1 ";
                                    MySqlDataReader respuestastring20 = conectinteresado.getdatareader(query2);

                                    if (respuestastring20 != null)
                                    {
                                        respuestastring20.Read();

                                        interesadoid = validareader("InteresadoID", "InteresadoID", respuestastring20).Text;

                                        String nombreutil = TB_interesadoDcalle.Text + TB_interesadoDnumext.Text + TB_interesadoDcolonia.Text ;
                                        conect conectinsert2 = new conect();
                                        String kweryinsert2 = "INSERT INTO `direccion` " +
                                                                        " (`DireccionID`, " +
                                                                        " `DireccionUtil`, " +
                                                                        " `DireccionCalle`, " +
                                                                        " `DireccionNumExt`, " +
                                                                        " `DireccionNumInt`, " +
                                                                        " `DireccionColonia`, " +
                                                                        //" `DireccionPoblacion`, " +
                                                                        " `DireccionEstado`, " +
                                                                        " `DireccionCP`, " +
                                                                        " `DireccionIndAct`, " +
                                                                        " `PaisId`, " +
                                                                           " `InteresadoId`, " +
                                                                        " `UsuarioId`, " +
                                                                        " `UsuarioFechaCapturo`, " +
                                                                        " `TipoDireccionId`) " +
                                                                        " VALUES " +
                                                                        "(NULL,'" +
                                                                        nombreutil +
                                                                         "', '" +
                                                                         TB_interesadoDcalle.Text +
                                                                         "', '" +
                                                                         TB_interesadoDnumext.Text +
                                                                         "', '" +
                                                                         TB_interesadoDnumint.Text +
                                                                         "', '" +
                                                                         TB_interesadoDcolonia.Text +
                                                                         "', '" +
                                                                         //TB_interesadoDpoblacion.Text +
                                                                         "', '" +
                                                                         TB_interesadoDestado.Text +
                                                                         "', '" +
                                                                         TB_interesadoDcp.Text +
                                                                         "', 1, " +
                                                                         (combopais.SelectedItem as ComboboxItem).Value +
                                                                         ", " +
                                                                         interesadoid +
                                                                         ", '" +
                                                                         login.sId +
                                                                         "', '" +
                                                                         sFechaalta + "', " +
                                                                         (CB_interesadoDtipodireccion.SelectedItem as ComboboxItem).Value + ");";
                                        MySqlDataReader respuestastringinsert2 = conectinsert2.getdatareader(kweryinsert2);
                                        if (respuestastringinsert2 == null)
                                        {
                                            MessageBox.Show("No se pudo agregar un nuevo interesado");
                                        }
                                        else
                                        {
                                            respuestastringinsert2.Close();
                                            conectinsert2.Cerrarconexion();
                                        }
                                        respuestastring20.Close();
                                        conectinteresado.Cerrarconexion();
                                        conect conect_consulta = new conect();
                                        String squerymaxcontactoId = "select * from interesado  order by interesadoid desc limit 1;";
                                        String sInteresadoididmax = "";
                                        MySqlDataReader resp_masixcontacto = conect_consulta.getdatareader(squerymaxcontactoId);
                                        while (resp_masixcontacto.Read())
                                        {
                                            sInteresadoididmax = validareader("interesadoid", "interesadoid", resp_masixcontacto).Text;
                                        }
                                        resp_masixcontacto.Close();
                                        conect_consulta.Cerrarconexion();
                                        ComboboxItem comboInteresadotitular = new ComboboxItem();
                                        comboInteresadotitular.Text = TB_nombreinteresado.Text;
                                        comboInteresadotitular.Value = sInteresadoididmax;
                                        MessageBox.Show("Se agrego el titular nuevo");
                                        cBcomboInteresadotitular = comboInteresadotitular;
                                        sNombrenuevotitular = TB_nombreinteresado.Text;
                                        DialogResult = DialogResult.OK;
                                        this.Close();
                                    }
                                }
                                else
                                {

                                }
                            }
                        }
                        else
                        {
                            if (Tipo == "Moral Nacional")
                            {
                                TipoP = "MN";
                            }
                            if (Tipo == "Moral Extranjera")
                            {
                                TipoP = "ME";
                            }
                            conect coninsert = new conect();
                            String Nombre_Combinado = TB_nombreinteresado.Text + " " + TB_apellidopaternointeresado.Text + " " + TB_apellidomaternointeresado.Text;
                            String queryinsert = "INSERT INTO `interesado` " +
                                " (`InteresadoID`, " +
                                 " `NombreUtilInt`, " +
                                  " `RazonSocial`, " +
                                " `InteresadoTipoPersonaSAT`, " +
                                 " `IdiomaId`, " +
                                "`InteresadoRFC`, " +
                                " `SociedadID`, " +
                                " `InteresadoRGP`, " +
                                " `InteresadoFechaAlta`, " +
                                " `PaisId`, " +
                                " `InteresadoIndAct`, " +
                                " `InteresadoShort`, " +
                                "`InteresadoPoder`, " +
                                " `InteresadoCurp`, " +
                                "`InteresadoMail`, " +
                                "`holderid`, " +
                                "`UsuarioId`, " +
                                "`UsuarioFechaCapturo`, " +
                                " `InteresadoTelefono`) " +
                                "VALUES  " +
                                "(NULL,'" +
                                "" + tbNombreEmpresa + "', " +
                                 "'" + tbNombreEmpresa + "', " +
                                "'" + (tipopersona.SelectedItem as ComboboxItem).Value + "', " +
                                "'" + (cbIdioma.SelectedItem as ComboboxItem).Value + "', '" +

                                TB_rfc.Text + "', '" +

                                tiposociedad + "', " +

                                TB_rgp.Text + ", '" +

                                sFechaalta + "', '" +

                                (Nacionalidad.SelectedItem as ComboboxItem).Value + "', " +

                                "'1', '" +
                                 TB_razonsocial.Text +
                                "', '" +
                                TB_poderinteresado.Text +
                                "', '" +
                                TB_curp.Text +
                                "', '" +
                                TB_correo_interesado.Text +
                                "', " +
                                holder +
                                ", '" +
                                login.sId +
                                "', '" +
                                sFechaalta + "', '" +
                                TB_telefono_interesado.Text + "');";
                            MySqlDataReader respuestastringinsert = coninsert.getdatareader(queryinsert);


                            if (respuestastringinsert == null)
                            {
                                MessageBox.Show("No se pudro agregar un direccion a nuevo interesado");
                            }
                            else
                            {

                                if (CB_interesadoDtipodireccion.SelectedItem != null && TB_interesadoDcalle.Text != "" && TB_interesadoDnumext.Text != "" && TB_interesadoDnumint.Text != "")
                                {
                                    conect conectinteresado = new conect();
                                    String query2 = "SELECT InteresadoID FROM interesado order by InteresadoID DESC  limit 1 ";
                                    MySqlDataReader respuestastring20 = conectinteresado.getdatareader(query2);

                                    if (respuestastring20 != null)
                                    {
                                        respuestastring20.Read();

                                        interesadoid = validareader("InteresadoID", "InteresadoID", respuestastring20).Text;
                                        String nombreutil = TB_interesadoDcalle.Text + "" + TB_interesadoDnumext.Text + "" + TB_interesadoDcolonia.Text + "" + TB_interesadoDpoblacion.Text ;
                                        conect conectinsert2 = new conect();
                                        String kweryinsert2 = "INSERT INTO `direccion` " +
                                                                        " (`DireccionID`, " +
                                                                        " `DireccionUtil`, " +
                                                                        " `DireccionCalle`, " +
                                                                        " `DireccionNumExt`, " +
                                                                        " `DireccionNumInt`, " +
                                                                        " `DireccionColonia`, " +
                                                                        //" `DireccionPoblacion`, " +
                                                                        " `DireccionEstado`, " +
                                                                        " `DireccionCP`, " +
                                                                        " `DireccionIndAct`, " +
                                                                        " `PaisId`, " +
                                                                        " `InteresadoId`, " +
                                                                        " `UsuarioId`, " +
                                                                        " `UsuarioFechaCapturo`, " +
                                                                        " `TipoDireccionId`) " +
                                                                        " VALUES " +
                                                                        "(NULL,'" +
                                                                        nombreutil +
                                                                         "', '" +
                                                                         TB_interesadoDcalle.Text +
                                                                         "', '" +
                                                                         TB_interesadoDnumext.Text +
                                                                         "', '" +
                                                                         TB_interesadoDnumint.Text +
                                                                         "', '" +
                                                                         TB_interesadoDcolonia.Text +
                                                                         "', '" +
                                                                         //TB_interesadoDpoblacion.Text +
                                                                         "', '" +
                                                                         TB_interesadoDestado.Text +
                                                                         "', '" +
                                                                         TB_interesadoDcp.Text +
                                                                         "', 1, " +
                                                                         (combopais.SelectedItem as ComboboxItem).Value +
                                                                         ", " +
                                                                         interesadoid +
                                                                         ", '" +
                                                                         login.sId +
                                                                         "', '" +
                                                                         sFechaalta + "', " +
                                                                         (CB_interesadoDtipodireccion.SelectedItem as ComboboxItem).Value + ");";
                                        MySqlDataReader respuestastringinsert2 = conectinsert2.getdatareader(kweryinsert2);
                                        if (respuestastringinsert2 == null)
                                        {
                                            MessageBox.Show("No se pudo agregar un nuevo interesado");
                                        }
                                        else
                                        {
                                            respuestastringinsert2.Close();
                                            conectinsert2.Cerrarconexion();
                                        }
                                        respuestastring20.Close();
                                        conectinteresado.Cerrarconexion();
                                        conect conect_consulta = new conect();
                                        String squerymaxcontactoId = "select * from interesado  order by interesadoid desc limit 1;";
                                        String sInteresadoididmax = "";
                                        MySqlDataReader resp_masixcontacto = conect_consulta.getdatareader(squerymaxcontactoId);
                                        while (resp_masixcontacto.Read())
                                        {
                                            sInteresadoididmax = validareader("interesadoid", "interesadoid", resp_masixcontacto).Text;
                                        }
                                        resp_masixcontacto.Close();
                                        conect_consulta.Cerrarconexion();
                                        ComboboxItem comboInteresadotitular = new ComboboxItem();
                                        comboInteresadotitular.Text = TB_nombreinteresado.Text;
                                        comboInteresadotitular.Value = sInteresadoididmax;
                                        MessageBox.Show("Se agrego el titular nuevo");
                                        cBcomboInteresadotitular = comboInteresadotitular;
                                        sNombrenuevotitular = TB_nombreinteresado.Text;
                                        DialogResult = DialogResult.OK;
                                        this.Close();
                                    }
                                }
                                else
                                {

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex);
                    DialogResult = DialogResult.Cancel;
                    this.Close();
                }
                }
            }
            
        }
     public void repeticion_pais()
        {
            conect cons = new conect();

            string valor = (combopais.SelectedItem as ComboboxItem).Value.ToString();
            String querys = "select * from pais where PaisId=" + valor;

            MySqlDataReader respuestastrings = cons.getdatareader(querys);
            while (respuestastrings.Read())
            {
                PaisNombre.SelectedIndex = PaisNombre.Items.Add(validareader("PaisNombre", "PaisId", respuestastrings));
            }

            //tipopersona_SelectedIndexChanged(seccion);

            //tipopersona_SelectedIndexChanged(seccion);
            Nacionalidad.Items.Clear();
            Nacionalidad.Text = "";
            //string tipo = (comboTiposolicitud.SelectedItem as ComboboxItem).Value.ToString();
            conect con = new conect();

            //string valor = (combopais.SelectedItem as ComboboxItem).Value.ToString();
            String query = "select * from pais where PaisId=" + valor;

            MySqlDataReader respuestastring = con.getdatareader(query);
            while (respuestastring.Read())
            {
                Nacionalidad.SelectedIndex = Nacionalidad.Items.Add(validareader("PaisNacionalidad", "PaisId", respuestastring));
            }
            string seccion = valor;
            //tipopersona_SelectedIndexChanged(seccion);
            respuestastring.Close();
            con.Cerrarconexion();
            respuestastrings.Close();
            cons.Cerrarconexion();
            String queryss = "select * from pais where PaisId!=" + valor;

            MySqlDataReader respuestastringss = cons.getdatareader(queryss);
            while (respuestastringss.Read())
            {
                Nacionalidad.Items.Add(validareader("PaisNacionalidad", "PaisId", respuestastringss));
            }
            respuestastringss.Close();
            cons.Cerrarconexion();
            ///combo para el pais en la opcion de domicilio
            String query2 = "select * from pais where PaisId=" + valor;

            MySqlDataReader respuestastring4 = con.getdatareader(query2);
            while (respuestastring4.Read())
            {
                comboBox2.SelectedIndex = comboBox2.Items.Add(validareader("PaisNombre", "PaisId", respuestastring4));
            }
            string seccion2 = valor;
            //tipopersona_SelectedIndexChanged(seccion);
            respuestastring.Close();
            con.Cerrarconexion();
            respuestastrings.Close();
            cons.Cerrarconexion();
            String queryss3 = "select * from pais where PaisId!=" + valor;

            MySqlDataReader respuestastringss3 = cons.getdatareader(queryss3);
            while (respuestastringss3.Read())
            {
                comboBox2.Items.Add(validareader("PaisNombre", "PaisId", respuestastringss3));
            }
            respuestastringss3.Close();
            cons.Cerrarconexion();

            ///

            if (valor == "148")
            {
                tipopersona.Text = "";
                tipopersona.Items.Clear();

                String querytipo = "Select * from tipo_persona where id_tipo_persona in(1,3) ";

                MySqlDataReader respuestatipo = con.getdatareader(querytipo);
                while (respuestatipo.Read())
                {
                    tipopersona.Items.Add(validareader("nombre_tipopersona", "id_tipo_persona", respuestatipo));
                }
                //tipopersona_SelectedIndexChanged(seccion);
                respuestatipo.Close();
                con.Cerrarconexion();
            }
            else
            {
                tipopersona.Items.Clear();
                tipopersona.Text = "";
                String querytipo = "Select * from tipo_persona where id_tipo_persona in(2,4) ";

                MySqlDataReader respuestatipo = con.getdatareader(querytipo);
                while (respuestatipo.Read())
                {
                    tipopersona.Items.Add(validareader("nombre_tipopersona", "id_tipo_persona", respuestatipo));
                }
                //tipopersona_SelectedIndexChanged(seccion);
                respuestatipo.Close();
                con.Cerrarconexion();
            }
            //Si el pais es alguno de estos IDPais entonces el valor predeterminado sera ingles
            if (valor == "45" || valor == "213" || valor == "74" || valor == "104")
            {
                String kwery2 = "SELECT * FROM  idioma where IdiomaId=1";
                MySqlDataReader respuestastring2 = con.getdatareader(kwery2);
                while (respuestastring2.Read())
                {
                    cbIdioma.SelectedIndex = cbIdioma.Items.Add(validareader("IdiomaDescripcion", "IdiomaId", respuestastring2));
                    //CB_idioma_cliente.Items
                }
                respuestastring2.Close();
            }
            else if (valor == "148" || valor == "46" || valor == "10" || valor == "60" || valor == "162" || valor == "43")
            {
                String kwery2 = "SELECT * FROM  idioma where IdiomaId=2";
                MySqlDataReader respuestastring2 = con.getdatareader(kwery2);
                while (respuestastring2.Read())
                {
                    cbIdioma.SelectedIndex = cbIdioma.Items.Add(validareader("IdiomaDescripcion", "IdiomaId", respuestastring2));
                    //CB_idioma_cliente.Items
                }
                respuestastring2.Close();
            }
            else
            {
                String kwery2 = "SELECT * FROM  idioma";
                MySqlDataReader respuestastring2 = con.getdatareader(kwery2);
                while (respuestastring2.Read())
                {
                    cbIdioma.Items.Add(validareader("IdiomaDescripcion", "IdiomaId", respuestastring2));
                    //CB_idioma_cliente.Items
                }
                respuestastring2.Close();
            }
            return;
        }

        private void tipopersona_SelectedIndexChanged(object sender, EventArgs e)
        {


            try
            {
                if ((tipopersona.SelectedItem as ComboboxItem).Value.ToString().Equals("1") || (tipopersona.SelectedItem as ComboboxItem).Value.ToString().Equals("2"))
                {
                    fisica.Enabled = true;
                    moral.Enabled = false;

                }

                if ((tipopersona.SelectedItem as ComboboxItem).Value.ToString().Equals("3") || (tipopersona.SelectedItem as ComboboxItem).Value.ToString().Equals("4"))
                {
                    moral.Enabled = true;
                    fisica.Enabled = false;

                }
            }
            catch
            {

            }
        }
        private void combopais_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                String Combo = PaisNombre.Text;
                String Combo2 = combopais.Text;

                if (Combo != "" && Combo2 != "")
                {
                    repeticion_pais();
                    return;
                }
                else
                {
                    repeticion_pais();
                }

            }
            catch (Exception E)
            {


            }
        }
    }

}
