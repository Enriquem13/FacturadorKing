
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
    public partial class Finteresado : Form
    {
        public Form1 login;
        public Form1 oFormlogin;
        public captura capFormcap;
        int IDCASO;
        int IDTIPOSOL;

        String idinteresado;
        public Finteresado(Form1 form, captura Formcap, int Casoid = 0, int tiposolicitudid = 0)
        {
            InitializeComponent();

            //BT_regresar.Enabled = false;

            IDCASO = Casoid;
            IDTIPOSOL = tiposolicitudid;
            oFormlogin = form;
            login = form;



            oFormlogin = form;
            capFormcap = Formcap;
            fisica.Visible = false;
            moral.Visible = false;
            conect con1 = new conect();
            String pais = "SELECT * FROM  pais where PaisNombre not like'%OFICINA%' AND  PaisNombre not like'%ORGANIZACION%'";
            MySqlDataReader rpais = con1.getdatareader(pais);
            while (rpais.Read())
            {
                combopais.Items.Add(validareader("PaisClave", "PaisId", rpais));
                //CB_idioma_cliente.Items
            }
            rpais.Close();
            con1.Cerrarconexion();


            String paisnombre = "SELECT concat_ws('--',PaisClave,PaisNombre) as PaisNombre,PaisId  FROM  pais where PaisNombre not like'%OFICINA%' AND  PaisNombre not like'%ORGANIZACION%'";
            MySqlDataReader rpaiss = con1.getdatareader(paisnombre);
            while (rpaiss.Read())
            {
                PaisNombre.Items.Add(validareader("PaisNombre", "PaisId", rpaiss));
                //CB_idioma_cliente.Items
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

            conect conectdireccionpais = new conect();
            String kwerypaisdireccion = "SELECT PaisId, PaisNombre FROM pais order by PaisNombre ";
            MySqlDataReader respuestastringpaisdireccion = conectdireccionpais.getdatareader(kwerypaisdireccion);
            while (respuestastringpaisdireccion.Read())
            {
                CB_interesadoDpais.Items.Add(validareader("PaisNombre", "PaisId", respuestastringpaisdireccion));
            }
            respuestastringtipodireccion.Close();
            conectdireccionpais.Cerrarconexion();

            conect conecttiporelacion = new conect();
            String kwerytiporelacion = "SELECT TipoRelacionId, TipoRelacionDescrip FROM  tiporelacion ";
            MySqlDataReader respuestastringtiporelacion = conecttiporelacion.getdatareader(kwerytiporelacion);
            while (respuestastringtiporelacion.Read())
            {
                CB_interesadoTipoderelacion.Items.Add(validareader("TipoRelacionDescrip", "TipoRelacionId", respuestastringtiporelacion));
            }
            respuestastringtiporelacion.Close();
            conecttiporelacion.Cerrarconexion();

            conect conecttiposolicitu = new conect();
            String kwerytiposolicitud = "SELECT TipoSolicitudId, TipoSolicitudDescrip FROM tiposolicitud ";
            MySqlDataReader respuestastringtiposolicitud = conecttiposolicitu.getdatareader(kwerytiposolicitud);
            while (respuestastringtiposolicitud.Read())
            {
                CB_interesadotiposolicitud.Items.Add(validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestastringtiposolicitud));
            }
            respuestastringtiposolicitud.Close();
            conecttiposolicitu.Cerrarconexion();
            //conect conectinteresado = new conect(); CB_interesadoTipoderelacion  CB_interesadotiposolicitud

            //String query2 = "SELECT interesado.InteresadoID, " +
            //                        " interesado.InteresadoNombre, " +
            //                        " interesado.InteresadoApPaterno, " +
            //                        " interesado.InteresadoApMaterno, " +
            //                        " interesado.InteresadoRFC, " +
            //                        " interesado.InteresadoTipoPersonaSAT, " +
            //                        " Damelasociedad (interesado.SociedadID) AS SOCIEDAD, " +
            //                        " interesado.InteresadoRGP, " +
            //                        " Damelanacionalidad (interesado.PaisId) AS NACIONALIDAD, " +
            //                        " interesado.InteresadoRFC, " +
            //                        " interesado.InteresadoCurp, " +
            //                        " interesado.InteresadoMail, " +
            //                        " interesado.InteresadoTelefono " +
            //                        "  FROM interesado";

            //String query2 = "SELECT * FROM interesado ";

            //MySqlDataReader respuestastring20 = conectinteresado.getdatareader(query2);

            //String siniInteresadoID = "";
            //String siniInteresadoTipoPersonaSAT = "";
            //String siniInteresadoNombre = "";
            //String siniInteresadoApPaterno = "";
            //String siniInteresadoApMaterno = "";
            //String siniInteresadoRFC = "";
            //String siniSociedadID = "";
            //String sinInteresadoRGP = "";
            ////String siniInteresadoFechaAlta = "";
            //String siniPaisId = "";
            //String siniInteresadoPoder = "";
            //String siniInteresadoCurp = "";
            //String siniInteresadoMail = "";
            //String siniInteresadoTelefono = "";

            //while (respuestastring20.Read())
            //{

            //    siniInteresadoID = validareader("InteresadoID", "InteresadoID", respuestastring20).Text;
            //    siniInteresadoNombre = validareader("InteresadoNombre", "InteresadoID", respuestastring20).Text;
            //    siniInteresadoApPaterno = validareader("InteresadoApPaterno", "InteresadoID", respuestastring20).Text;
            //    siniInteresadoApMaterno = validareader("InteresadoApMaterno", "InteresadoID", respuestastring20).Text;
            //    siniInteresadoRFC = validareader("InteresadoRFC", "InteresadoID", respuestastring20).Text;
            //    siniInteresadoTipoPersonaSAT = validareader("InteresadoTipoPersonaSAT", "InteresadoID", respuestastring20).Text;
            //    siniSociedadID = "";
            //    sinInteresadoRGP = validareader("InteresadoRGP", "InteresadoID", respuestastring20).Text;
            //    siniPaisId = "";
            //    siniInteresadoPoder = validareader("InteresadoPoder", "InteresadoID", respuestastring20).Text;
            //    siniInteresadoCurp = validareader("InteresadoCurp", "InteresadoID", respuestastring20).Text;
            //    siniInteresadoMail = validareader("InteresadoMail", "InteresadoID", respuestastring20).Text;
            //    siniInteresadoTelefono = validareader("InteresadoTelefono", "InteresadoID", respuestastring20).Text;


            //    ListViewItem listaagregar = new ListViewItem(siniInteresadoID);
            //    listaagregar.SubItems.Add(siniInteresadoNombre);
            //    listaagregar.SubItems.Add(siniInteresadoApPaterno);
            //    listaagregar.SubItems.Add(siniInteresadoApMaterno);
            //    listaagregar.SubItems.Add(siniInteresadoRFC);
            //    listaagregar.SubItems.Add(siniInteresadoTipoPersonaSAT);
            //    listaagregar.SubItems.Add(siniSociedadID);
            //    listaagregar.SubItems.Add(sinInteresadoRGP);
            //    listaagregar.SubItems.Add(siniPaisId);
            //    listaagregar.SubItems.Add(siniInteresadoPoder);
            //    listaagregar.SubItems.Add(siniInteresadoCurp);
            //    listaagregar.SubItems.Add(siniInteresadoMail);
            //    listaagregar.SubItems.Add(siniInteresadoTelefono);
            //    listView1.Items.Add(listaagregar);
            //}

            //listView1.View = View.Details;
            //listView1.FullRowSelect = true;
            //respuestastring20.Close();
            //conectinteresado.Cerrarconexion();

            if (Casoid != 0)
            {
                //BT_regresar.Enabled = true;
                // BT_salirinteresado.Enabled = false;
                BT_menuinteresado.Enabled = false;
                TB_casoid.Text = Casoid.ToString();
                TB_casoid.ReadOnly = true;
                CheckBinteresadocaso.CheckState = CheckState.Checked;
                groupBox3.Enabled = true;
                CheckBinteresadocaso.Enabled = false;
                conect conecttiposolicitud = new conect();
                String kwerytiposolicitud2 = "SELECT TipoSolicitudId, TipoSolicitudDescrip FROM tiposolicitud WHERE TipoSolicitudId =  " + IDTIPOSOL + ";";
                MySqlDataReader respuestatiposoli = conecttiposolicitud.getdatareader(kwerytiposolicitud2);

                respuestatiposoli.Read();
                String tiposolides = validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestatiposoli).Text;
                respuestatiposoli.Close();
                conecttiposolicitud.Cerrarconexion();
                CB_interesadotiposolicitud.Text = tiposolides;
                CB_interesadotiposolicitud.Enabled = false;
            }


        }


        private void Bagregar_Click(object sender, EventArgs e)
        {

            if (!TB_nombreinteresado.Text.Trim().Equals("") &&
                tipopersona.SelectedItem != null &&
                Nacionalidad.SelectedItem != null &&
                CB_interesadoDpais.SelectedItem != null &&
                CB_interesadoDtipodireccion.SelectedItem != null &&
                !TB_interesadoDcalle.Text.Trim().Equals(""))
            {

                try
                {
                    DateTime dateTime = DateTime.UtcNow.Date;
                    String sFechaalta = dateTime.ToString("yyyy/MM/dd hh:mm:sss");
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
                    String TipoP = "";
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


                        String Nombre_Combinado = TB_nombreinteresado.Text + " " + TB_apellidopaternointeresado.Text + " " + TB_apellidomaternointeresado.Text;
                        conect coninsert = new conect();
                        String queryinsert = "INSERT INTO `interesado` " +
                            " (`InteresadoID`, " +
                            " `InteresadoTipoPersonaSAT`, " +
                            " `NombreUtilInt`, " +
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
                            TipoP +
                            "', '" +
                            Nombre_Combinado +
                            "', '" +
                            TB_nombreinteresado.Text.Replace("'", "´") +
                            "', '" +
                            TB_apellidopaternointeresado.Text.Replace("'", "´") +
                            "', '" +
                            TB_apellidomaternointeresado.Text.Replace("'", "´") +
                            "', '" +
                            TB_rfc.Text.Replace("'", "´") +
                            "', " +
                            tiposociedad +
                            ", '" +
                            TB_rgp.Text.Replace("'", "´") +
                            "', '" +
                            sFechaalta +
                            "', " +
                            (Nacionalidad.SelectedItem as ComboboxItem).Value +
                            ", " +
                            "'1', '" +
                             TB_razonsocial.Text.Replace("'", "´") +
                            "', '" +
                            TB_poderinteresado.Text.Replace("'", "´") +
                            "', '" +
                            TB_curp.Text.Replace("'", "´") +
                            "', '" +
                            TB_correo_interesado.Text.Replace("'", "´") +
                            "', " +
                            holder +
                            ", '" +
                                                            login.sId +
                                "', '" +
                                sFechaalta + "', '" +
                            TB_telefono_interesado.Text.Replace("'", "´") + "');";
                        MySqlDataReader respuestastringinsert = coninsert.getdatareader(queryinsert);
                        if (respuestastringinsert == null)
                        {
                            MessageBox.Show("No se pudo agregar un nuevo interesado (interesado), verifique los datos del interesado");
                            new filelog("error: ", " insert interesado ->" + queryinsert);
                        }
                        else
                        {


                            conect conectinteresado = new conect();
                            String query2 = "SELECT InteresadoID FROM interesado order by InteresadoID DESC  limit 1 ";
                            MySqlDataReader respuestastring20 = conectinteresado.getdatareader(query2);

                            if (respuestastring20 != null)
                            {
                                respuestastring20.Read();

                                interesadoid = validareader("InteresadoID", "InteresadoID", respuestastring20).Text;
                                String Direccioncombinada = TB_nombreinteresado.Text + " " + TB_apellidopaternointeresado.Text + " " + TB_apellidomaternointeresado.Text;
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
                                                                Direccioncombinada +
                                                                "', '" +
                                                                 TB_interesadoDcalle.Text.Replace("'", "´") +
                                                                 "', '" +
                                                                 TB_interesadoDnumext.Text.Replace("'", "´") +
                                                                 "', '" +
                                                                 TB_interesadoDnumint.Text.Replace("'", "´") +
                                                                 "', '" +
                                                                 TB_interesadoDcolonia.Text.Replace("'", "´") +
                                                                 "', '" +
                                                                 //TB_interesadoDpoblacion.Text.Replace("'", "´") +
                                                                 "', '" +
                                                                 TB_interesadoDestado.Text.Replace("'", "´") +
                                                                 "', '" +
                                                                 TB_interesadoDcp.Text.Replace("'", "´") +
                                                                 "', 1, " +
                                                                 (CB_interesadoDpais.SelectedItem as ComboboxItem).Value +
                                                                 ", " +
                                                                 interesadoid +
                                                                 ", " +
                                                                 oFormlogin.sId +
                                                                 ", '" +
                                                                 sFechaalta +
                                                                 "', " +
                                                                 (CB_interesadoDtipodireccion.SelectedItem as ComboboxItem).Value + ");";
                                MySqlDataReader respuestastringinsert2 = conectinsert2.getdatareader(kweryinsert2);
                                if (respuestastringinsert2 == null)
                                {
                                    MessageBox.Show("No se pudo agregar un nuevo interesado (direccion), Verifique los datos de la dirección");
                                    new filelog("error: ", " direccion: ->" + kweryinsert2);
                                }
                                else
                                {

                                    respuestastringinsert2.Close();
                                    conectinsert2.Cerrarconexion();
                                }


                                respuestastring20.Close();
                                conectinteresado.Cerrarconexion();


                                //if (CheckB_habilitadocontacto.CheckState == CheckState.Checked)
                                //{
                                //    habilitado = "1";
                                //}


                            }

                            MessageBox.Show("Interesado agregado correctamente");
                            respuestastringinsert.Close();
                            coninsert.Cerrarconexion();
                            //this.Close();
                        }
                        limpiarcasilas();
                        this.Close();
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

                        String Nombre_Combinado = TB_nombreinteresado.Text + " " + TB_apellidopaternointeresado.Text + " " + TB_apellidomaternointeresado.Text;
                        conect coninsert = new conect();
                        String queryinsert = "INSERT INTO `interesado` " +
                            " (`InteresadoID`, " +
                            " `InteresadoTipoPersonaSAT`, " +
                            " `NombreUtilInt`, " +
                            " `RazonSocialInt`, " +
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
                            TipoP +
                            "', '" +
                            tbNombreEmpresa.Text.Replace("'", "´") +
                            "', '" +
                            tbNombreEmpresa.Text.Replace("'", "´") +
                            "', '" +

                            TB_rfc.Text.Replace("'", "´") +
                            "', " +
                            tiposociedad +
                            ", '" +
                            TB_rgp.Text.Replace("'", "´") +
                            "', '" +
                            sFechaalta +
                            "', " +
                            (Nacionalidad.SelectedItem as ComboboxItem).Value +
                            ", " +
                            "'1', '" +
                             TB_razonsocial.Text.Replace("'", "´") +
                            "', '" +
                            TB_poderinteresado.Text.Replace("'", "´") +
                            "', '" +
                            TB_curp.Text.Replace("'", "´") +
                            "', '" +
                            TB_correo_interesado.Text.Replace("'", "´") +
                            "', " +
                            holder +
                            ", '" +
                         login.sId +
                        "', '" +
                        sFechaalta + "', '" +
                            TB_telefono_interesado.Text.Replace("'", "´") + "');";
                        MySqlDataReader respuestastringinsert = coninsert.getdatareader(queryinsert);
                        if (respuestastringinsert == null)
                        {
                            MessageBox.Show("No se pudo agregar un nuevo interesado (interesado), verifique los datos del interesado");
                            new filelog("error: ", " insert interesado ->" + queryinsert);
                        }
                        else
                        {


                            conect conectinteresado = new conect();
                            String query2 = "SELECT InteresadoID FROM interesado order by InteresadoID DESC  limit 1 ";
                            MySqlDataReader respuestastring20 = conectinteresado.getdatareader(query2);

                            if (respuestastring20 != null)
                            {
                                respuestastring20.Read();

                                interesadoid = validareader("InteresadoID", "InteresadoID", respuestastring20).Text;

                                conect conectinsert2 = new conect();
                                String Direccioncombinada = TB_interesadoDcalle.Text + " " + TB_interesadoDnumext.Text + " " + TB_interesadoDcolonia.Text;
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
                                                                    Direccioncombinada +
                                                                    "', '" +
                                                                     TB_interesadoDcalle.Text.Replace("'", "´") +
                                                                     "', '" +
                                                                     TB_interesadoDnumext.Text.Replace("'", "´") +
                                                                     "', '" +
                                                                     TB_interesadoDnumint.Text.Replace("'", "´") +
                                                                     "', '" +
                                                                     TB_interesadoDcolonia.Text.Replace("'", "´") +
                                                                     "', '" +
                                                                     //TB_interesadoDpoblacion.Text.Replace("'", "´") +
                                                                     "', '" +
                                                                     TB_interesadoDestado.Text.Replace("'", "´") +
                                                                     "', '" +
                                                                     TB_interesadoDcp.Text.Replace("'", "´") +
                                                                     "', 1, " +
                                                                     (CB_interesadoDpais.SelectedItem as ComboboxItem).Value +
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
                                    MessageBox.Show("No se pudo agregar un nuevo interesado (direccion), Verifique los datos de la dirección");
                                    new filelog("error: ", " direccion: ->" + kweryinsert2);
                                }
                                else
                                {

                                    respuestastringinsert2.Close();
                                    conectinsert2.Cerrarconexion();
                                }


                                respuestastring20.Close();
                                conectinteresado.Cerrarconexion();


                                //if (CheckB_habilitadocontacto.CheckState == CheckState.Checked)
                                //{
                                //    habilitado = "1";
                                //}


                                if (CheckBinteresadocaso.CheckState == CheckState.Checked)
                                {
                                    if (!TB_casoid.Text.Trim().Equals("") && CB_interesadoTipoderelacion.SelectedItem != null && CB_interesadotiposolicitud.SelectedItem != null)
                                    {
                                        String secuencia = "";
                                        String tiposolicitudid = "";
                                        int secuenciaint = 0;
                                        conect conectsecuencia = new conect();
                                        String kwerysecuenci = "SELECT CasoInteresadoId, CasoInteresadoSecuencia FROM casointeresado WHERE  CasoId = " + TB_casoid.Text + " order by CasoInteresadoSecuencia DESC LIMIT 1 ";
                                        MySqlDataReader respuestastringsecuencia = conectsecuencia.getdatareader(kwerysecuenci);

                                        if (respuestastringsecuencia != null)
                                        {
                                            respuestastringsecuencia.Read();
                                            secuencia = validareader("CasoInteresadoSecuencia", "CasoInteresadoId", respuestastringsecuencia).Text;
                                            conectsecuencia.Cerrarconexion();

                                            if (secuencia == "")
                                            {
                                                secuenciaint = 1;
                                            }
                                            else
                                            {
                                                secuenciaint = Int32.Parse(secuencia) + 1;
                                            }
                                        }


                                        conect conectdireccion = new conect();
                                        String keweryiddireccion = "SELECT DireccionID FROM direccion WHERE direccion.InteresadoId = " + interesadoid + ";";
                                        MySqlDataReader respuestastringiddireccion = conectdireccion.getdatareader(keweryiddireccion);

                                        if (respuestastringiddireccion != null)
                                        {
                                            respuestastringiddireccion.Read();
                                            String iddireccion = validareader("DireccionID", "DireccionID", respuestastringiddireccion).Text;
                                            respuestastringiddireccion.Close();
                                            conectdireccion.Cerrarconexion();

                                            String stiporelacion = (CB_interesadoTipoderelacion.SelectedItem as ComboboxItem).Value.ToString();
                                            if (stiporelacion == "3")
                                            {
                                                agregarInventor();
                                                stiporelacion = "1";
                                            }

                                            //agregarInventor();

                                            conect conectinsert3 = new conect();
                                            String kweryinsert3 = "INSERT INTO `casointeresado` " +
                                                                        " (`CasoInteresadoId`, " +
                                                                        " `InteresadoId`, " +
                                                                        " `CasoId`, " +
                                                                        " `TipoSolicitudId`, " +
                                                                        " `CasoInteresadoSecuencia`, " +
                                                                        " `TipoRelacionId`, " +
                                                                        " `DireccionId`) " +
                                                                        " VALUES " +
                                                                        "(NULL," +
                                                                        interesadoid +
                                                                        ", '" +
                                                                        TB_casoid.Text +
                                                                        "', " +
                                                                        (CB_interesadotiposolicitud.SelectedItem as ComboboxItem).Value +
                                                                        ", " +
                                                                        secuenciaint +
                                                                        ", " +
                                                                        //(CB_interesadoTipoderelacion.SelectedItem as ComboboxItem).Value+
                                                                        stiporelacion +
                                                                        ", " +
                                                                        iddireccion + ");";
                                            MySqlDataReader respuestastringinsert3 = conectinsert3.getdatareader(kweryinsert3);

                                            if (respuestastringinsert3 == null)
                                            {
                                                MessageBox.Show("No se pudo agregar el interesado al caso (casointeresado)");
                                                new filelog("error: ", " casointeresado: ->" + kweryinsert3);
                                            }
                                            else
                                            {


                                                respuestastringinsert3.Close();
                                                conectinsert3.Cerrarconexion();
                                            }



                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show("Los campos relacionados con el caso no pueden ir vacíos, debe de proporcionar el id del caso y el tipo de relación, si desconoce el id del caso puede agregar al interesado directamente desde la pantalla del caso.");
                                    }
                                }
                            }

                            MessageBox.Show("Interesado agregado correctamente");
                            respuestastringinsert.Close();
                            coninsert.Cerrarconexion();
                            //this.Close();
                        }
                        limpiarcasilas();
                        this.Close();
                    }
                }

                catch (Exception E)
                {
                    //escribimos en log
                    Console.WriteLine("{0} Exception caught.", E);
                    MessageBox.Show("Verifique la conexión a la red, o que la base de datos esté disponible" + E);
                }




            }
            else
            {
                MessageBox.Show("Debes agregarle un nombre al interesado, un tipo de persona y una nacionalidad mínimo, a la direccion se debe de agregar minimo, el pais y el tipo de dirección.");
            }
        }

        public void agregarInventor()
        {
            if (CB_interesadoTipoderelacion.SelectedItem == null)
            {
                MessageBox.Show("Debe agregar un tipo de relación para el nuevo interesado.");
                CB_interesadoTipoderelacion.Focus();
                return;
            }
            if (!TB_nombreinteresado.Text.Trim().Equals("") &&
                tipopersona.SelectedItem != null &&
                Nacionalidad.SelectedItem != null &&
                CB_interesadoDpais.SelectedItem != null &&
                CB_interesadoDtipodireccion.SelectedItem != null &&
                !TB_interesadoDcalle.Text.Trim().Equals(""))
            {

                try
                {
                    DateTime dateTime = DateTime.UtcNow.Date;
                    String sFechaalta = dateTime.ToString("yyyy/MM/dd hh:mm:sss");
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


                    conect coninsert = new conect();
                    String queryinsert = "INSERT INTO `inventor` " +
                        " (`InventorID`, " +
                        " `InventorTipoPersonaSAT`, " +
                        " `InventorNombre`, " +
                        " `InventorApPaterno`, " +
                        " `InventorApMaterno`, " +
                        "`InventorRFC`, " +
                        " `SociedadID`, " +
                        " `InventorRGP`, " +
                        " `InventorFechaAlta`, " +
                        " `PaisId`, " +
                        " `InventorIndAct`, " +
                        " `InventorShort`, " +
                        " `InventorPoder`, " +
                        " `InventorCurp`, " +
                        " `InventorMail`, " +
                        " `holderid`, " +
                        " `InventorTelefono`) " +
                        "VALUES  " +
                        "(NULL,'" +
                        (tipopersona.SelectedItem as ComboboxItem).Value +
                        "', '" +
                        TB_nombreinteresado.Text.Replace("'", "´") +
                        "', '" +
                        TB_apellidopaternointeresado.Text.Replace("'", "´") +
                        "', '" +
                        TB_apellidomaternointeresado.Text.Replace("'", "´") +
                        "', '" +
                        TB_rfc.Text.Replace("'", "´") +
                        "', " +
                        tiposociedad +
                        ", '" +
                        TB_rgp.Text.Replace("'", "´") +
                        "', '" +
                        sFechaalta +
                        "', " +
                        (Nacionalidad.SelectedItem as ComboboxItem).Value +
                        ", " +
                        "'1', '" +
                         TB_razonsocial.Text.Replace("'", "´") +
                        "', '" +
                        TB_poderinteresado.Text.Replace("'", "´") +
                        "', '" +
                        TB_curp.Text.Replace("'", "´") +
                        "', '" +
                        TB_correo_interesado.Text.Replace("'", "´") +
                        "', " +
                        holder +
                        ", '" +
                        TB_telefono_interesado.Text.Replace("'", "´") + "');";
                    MySqlDataReader respuestastringinsert = coninsert.getdatareader(queryinsert);
                    if (respuestastringinsert == null)
                    {
                        MessageBox.Show("No se pudo agregar un nuevo inventor (inventor), verifique los datos del inventor");
                        new filelog("error: ", " insert interesado ->" + queryinsert);
                    }
                    else
                    {
                        conect conectinteresado = new conect();
                        String query2 = "SELECT InventorID FROM inventor order by InventorID DESC  limit 1 ";
                        MySqlDataReader respuestastring20 = conectinteresado.getdatareader(query2);

                        if (respuestastring20 != null)
                        {
                            respuestastring20.Read();

                            interesadoid = validareader("InventorID", "InventorID", respuestastring20).Text;

                            conect conectinsert2 = new conect();
                            String kweryinsert2 = "INSERT INTO `direccion` " +
                                                            " (`DireccionID`, " +
                                                            " `DireccionCalle`, " +
                                                            " `DireccionNumExt`, " +
                                                            " `DireccionNumInt`, " +
                                                            " `DireccionColonia`, " +
                                                            " `DireccionPoblacion`, " +
                                                            " `DireccionEstado`, " +
                                                            " `DireccionCP`, " +
                                                            " `DireccionIndAct`, " +
                                                            " `PaisId`, " +
                                                            " `InteresadoId`, " +
                                                            " `TipoDireccionId`) " +
                                                            " VALUES " +
                                                            "(NULL,'" +
                                                             TB_interesadoDcalle.Text.Replace("'", "´") +
                                                             "', '" +
                                                             TB_interesadoDnumext.Text.Replace("'", "´") +
                                                             "', '" +
                                                             TB_interesadoDnumint.Text.Replace("'", "´") +
                                                             "', '" +
                                                             TB_interesadoDcolonia.Text.Replace("'", "´") +
                                                             "', '" +
                                                             TB_interesadoDpoblacion.Text.Replace("'", "´") +
                                                             "', '" +
                                                             TB_interesadoDestado.Text.Replace("'", "´") +
                                                             "', '" +
                                                             TB_interesadoDcp.Text.Replace("'", "´") +
                                                             "', 1, " +
                                                             (CB_interesadoDpais.SelectedItem as ComboboxItem).Value +
                                                             ", " +
                                                             interesadoid +
                                                             ", " +
                                                             (CB_interesadoDtipodireccion.SelectedItem as ComboboxItem).Value + ");";
                            MySqlDataReader respuestastringinsert2 = conectinsert2.getdatareader(kweryinsert2);
                            if (respuestastringinsert2 == null)
                            {
                                MessageBox.Show("No se pudo agregar un nuevo interesado (direccion), Verifique los datos de la dirección");
                                new filelog("error: ", " direccion: ->" + kweryinsert2);
                            }
                            else
                            {
                                respuestastringinsert2.Close();
                                conectinsert2.Cerrarconexion();
                            }

                            respuestastring20.Close();
                            conectinteresado.Cerrarconexion();
                            //if (CheckB_habilitadocontacto.CheckState == CheckState.Checked)
                            //{
                            //    habilitado = "1";
                            //}


                            if (CheckBinteresadocaso.CheckState == CheckState.Checked)
                            {
                                if (!TB_casoid.Text.Trim().Equals("") && CB_interesadoTipoderelacion.SelectedItem != null && CB_interesadotiposolicitud.SelectedItem != null)
                                {
                                    String secuencia = "";
                                    String tiposolicitudid = "";
                                    int secuenciaint = 0;
                                    conect conectsecuencia = new conect();
                                    String kwerysecuenci = "SELECT CasoInventorId, CasoInteresadoSecuencia FROM casoinventor WHERE  CasoId = "
                                                            + TB_casoid.Text + " order by CasoInteresadoSecuencia DESC LIMIT 1 ";
                                    MySqlDataReader respuestastringsecuencia = conectsecuencia.getdatareader(kwerysecuenci);

                                    if (respuestastringsecuencia != null)
                                    {
                                        respuestastringsecuencia.Read();
                                        secuencia = validareader("CasoInteresadoSecuencia", "CasoInventorId", respuestastringsecuencia).Text;
                                        conectsecuencia.Cerrarconexion();

                                        if (secuencia == "")
                                        {
                                            secuenciaint = 1;
                                        }
                                        else
                                        {
                                            secuenciaint = Int32.Parse(secuencia) + 1;
                                        }
                                    }


                                    conect conectdireccion = new conect();
                                    String keweryiddireccion = "SELECT DireccionID FROM direccion WHERE direccion.InteresadoId = " + interesadoid + ";";
                                    MySqlDataReader respuestastringiddireccion = conectdireccion.getdatareader(keweryiddireccion);

                                    if (respuestastringiddireccion != null)
                                    {
                                        respuestastringiddireccion.Read();
                                        String iddireccion = validareader("DireccionID", "DireccionID", respuestastringiddireccion).Text;
                                        respuestastringiddireccion.Close();
                                        conectdireccion.Cerrarconexion();

                                        String stiporelacion = (CB_interesadoTipoderelacion.SelectedItem as ComboboxItem).Value.ToString();
                                        if (stiporelacion == "3")
                                        {
                                            stiporelacion = "2";
                                        }
                                        conect conectinsert3 = new conect();
                                        String kweryinsert3 = "INSERT INTO `casoinventor` " +
                                                                    " (`CasoInventorId`, " +
                                                                    " `InventorId`, " +
                                                                    " `CasoId`, " +
                                                                    " `TipoSolicitudId`, " +
                                                                    " `CasoInteresadoSecuencia`, " +
                                                                    " `TipoRelacionId`, " +
                                                                    " `DireccionId`) " +
                                                                    " VALUES " +
                                                                    "(NULL," +
                                                                    interesadoid +
                                                                    ", '" +
                                                                    TB_casoid.Text +
                                                                    "', " +
                                                                    (CB_interesadotiposolicitud.SelectedItem as ComboboxItem).Value +
                                                                    ", " +
                                                                    secuenciaint +
                                                                    ", " +
                                                                    stiporelacion +
                                                                    ", " +
                                                                    iddireccion + ");";
                                        MySqlDataReader respuestastringinsert3 = conectinsert3.getdatareader(kweryinsert3);

                                        if (respuestastringinsert3 == null)
                                        {
                                            MessageBox.Show("No se pudo agregar el interesado al caso (casointeresado)");
                                            new filelog("error: ", " casointeresado: ->" + kweryinsert3);
                                        }
                                        else
                                        {
                                            respuestastringinsert3.Close();
                                            conectinsert3.Cerrarconexion();
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Los campos relacionados con el caso no pueden ir vacíos, debe de proporcionar el id del caso y el tipo de relación, si desconoce el id del caso puede agregar al interesado directamente desde la pantalla del caso.");
                                }
                            }
                        }

                        MessageBox.Show("Inventor agregado correctamente");
                        respuestastringinsert.Close();
                        coninsert.Cerrarconexion();
                        //this.Close();
                    }

                }
                catch (Exception E)
                {
                    //escribimos en log
                    Console.WriteLine("{0} Exception caught.", E);
                    //MessageBox.Show("Verifique la conexión a la red, o que la base de datos esté disponible" + E);
                }
            }
            else
            {
                //MessageBox.Show("Debes agregarle un nombre al interesado, un tipo de persona y una nacionalidad mínimo, a la direccion se debe de agregar minimo, el pais y el tipo de dirección.");
            }
        }

        public void actualizartabla()
        {

            //listView1.Items.Clear(); 
            //conect conectinteresado = new conect();
            //String query2 = "SELECT * FROM interesado";
            //MySqlDataReader respuestastring20 = conectinteresado.getdatareader(query2);

            //String siniInteresadoID = "";
            //String siniInteresadoTipoPersonaSAT = "";
            //String siniInteresadoNombre = "";
            //String siniInteresadoApPaterno = "";
            //String siniInteresadoApMaterno = "";
            //String siniInteresadoRFC = "";
            //String siniSociedadID = "";
            //String sinInteresadoRGP = "";
            ////String siniInteresadoFechaAlta = "";
            //String siniPaisId = "";
            //String siniInteresadoPoder = "";
            //String siniInteresadoCurp = "";
            //String siniInteresadoMail = "";
            //String siniInteresadoTelefono = "";

            //while (respuestastring20.Read())
            //{

            //    siniInteresadoID = validareader("InteresadoID", "InteresadoID", respuestastring20).Text;
            //    siniInteresadoNombre = validareader("InteresadoNombre", "InteresadoID", respuestastring20).Text;
            //    siniInteresadoApPaterno = validareader("InteresadoApPaterno", "InteresadoID", respuestastring20).Text;
            //    siniInteresadoApMaterno = validareader("InteresadoApMaterno", "InteresadoID", respuestastring20).Text;
            //    siniInteresadoRFC = validareader("InteresadoRFC", "InteresadoID", respuestastring20).Text;
            //    siniInteresadoTipoPersonaSAT = validareader("InteresadoTipoPersonaSAT", "InteresadoID", respuestastring20).Text;
            //    siniSociedadID = validareader("SociedadID", "InteresadoID", respuestastring20).Text;
            //    sinInteresadoRGP = validareader("InteresadoRGP", "InteresadoID", respuestastring20).Text;
            //    siniPaisId = validareader("PaisId", "InteresadoID", respuestastring20).Text;
            //    siniInteresadoPoder = validareader("InteresadoRFC", "InteresadoID", respuestastring20).Text;
            //    siniInteresadoCurp = validareader("InteresadoCurp", "InteresadoID", respuestastring20).Text;
            //    siniInteresadoMail = validareader("InteresadoMail", "InteresadoID", respuestastring20).Text;
            //    siniInteresadoTelefono = validareader("InteresadoTelefono", "InteresadoID", respuestastring20).Text;


            //    ListViewItem listaagregar = new ListViewItem(siniInteresadoID);
            //    listaagregar.SubItems.Add(siniInteresadoNombre);
            //    listaagregar.SubItems.Add(siniInteresadoApPaterno);
            //    listaagregar.SubItems.Add(siniInteresadoApMaterno);
            //    listaagregar.SubItems.Add(siniInteresadoRFC);
            //    listaagregar.SubItems.Add(siniInteresadoTipoPersonaSAT);
            //    listaagregar.SubItems.Add(siniSociedadID);
            //    listaagregar.SubItems.Add(sinInteresadoRGP);
            //    listaagregar.SubItems.Add(siniPaisId);
            //    listaagregar.SubItems.Add(siniInteresadoPoder);
            //    listaagregar.SubItems.Add(siniInteresadoCurp);
            //    listaagregar.SubItems.Add(siniInteresadoMail);
            //    listaagregar.SubItems.Add(siniInteresadoTelefono);
            //    listView1.Items.Add(listaagregar);
            //}

            //listView1.View = View.Details;
            //listView1.FullRowSelect = true;
            //respuestastring20.Close();
            //conectinteresado.Cerrarconexion();

        }


        private void button1_Click(object sender, EventArgs e)
        {

        }


        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        public class ListboxItemss
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
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

        private void Bmodificar_Click(object sender, EventArgs e)
        {

        }

        public void limpiarcasilas()
        {
            //idinteresado = null;
            TB_nombreinteresado.Text = "";
            TB_apellidopaternointeresado.Text = "";
            TB_apellidomaternointeresado.Text = "";
            TB_correo_interesado.Text = "";
            TB_telefono_interesado.Text = "";
            TB_razonsocial.Text = "";
            TB_curp.Text = "";
            TB_rfc.Text = "";
            TB_poderinteresado.Text = "";
            TB_rgp.Text = "";
            Nacionalidad.Text = "Seleccione";
            CB_tiposociedad.Text = "Seleccione";
            tipopersona.Text = "Seleccione";

            TB_interesadoDcalle.Text = "";
            TB_interesadoDnumint.Text = "";
            TB_interesadoDestado.Text = "";
            TB_interesadoDcp.Text = "";
            CB_interesadoDtipodireccion.Text = "Seleccione";
            TB_interesadoDnumext.Text = "";
            TB_interesadoDcolonia.Text = "";
            TB_interesadoDpoblacion.Text = "";
            CB_interesadoDpais.Text = "Seleccione";

            TB_casoid.Text = "";
            CB_interesadoTipoderelacion.Text = "Seleccione";
            CB_interesadoTipoderelacion.Text = "Seleccione";
        }

        private void BT_menuinteresado_Click(object sender, EventArgs e)
        {
            capFormcap.Show();
            this.Close();
        }

        private void BT_salirinteresado_Click(object sender, EventArgs e)
        {


            capFormcap.Close();
            oFormlogin.Close();
            this.Close();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            ////CB_direcciontipo_cd.Text = listView2.SelectedItems[0].SubItems[1].Text;
            //idinteresado = listView1.SelectedItems[0].SubItems[0].Text;
            //if (!listView1.SelectedItems[0].SubItems[0].Equals(null))
            //{
            //    //Fclientedetalle detalle = new Fclientedetalle(idinteresado, oFormlogin, capFormcap);

            //    FInteresadoDetalle detalleinteresado = new FInteresadoDetalle(idinteresado, oFormlogin, capFormcap);
            //    detalleinteresado.Show();
            //    this.Hide();

            //}


            //TB_nombreinteresado.Text = listView1.SelectedItems[0].SubItems[1].Text;
            //TB_apellidopaternointeresado.Text = listView1.SelectedItems[0].SubItems[2].Text;
            //TB_apellidomaternointeresado.Text = listView1.SelectedItems[0].SubItems[3].Text;
            //TB_rfc.Text = listView1.SelectedItems[0].SubItems[4].Text;
            //CBtipo_persona.Text = listView1.SelectedItems[0].SubItems[5].Text; // ojo checar este
            //CB_tiposociedad.Text = listView1.SelectedItems[0].SubItems[6].Text;
            //TB_rgp.Text = listView1.SelectedItems[0].SubItems[7].Text;
            //CB_nacionalidad_interesado.Text = listView1.SelectedItems[0].SubItems[8].Text;
            //TB_poderinteresado.Text = listView1.SelectedItems[0].SubItems[9].Text;
            //TB_curp.Text = listView1.SelectedItems[0].SubItems[10].Text;
            //TB_correo_interesado.Text = listView1.SelectedItems[0].SubItems[11].Text;
            //TB_telefono_interesado.Text = listView1.SelectedItems[0].SubItems[12].Text;

            //Bagregar.Enabled = false;




        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            limpiarcasilas();
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBinteresadocaso.Checked)
            {
                groupBox3.Enabled = true;
            }
            else
            {
                groupBox3.Enabled = false;
                TB_casoid.Text = "";
                CB_interesadoTipoderelacion.Text = "Seleccione";
                CB_interesadotiposolicitud.Text = "Seleccione";

            }
        }

        private void TB_casoid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void BT_regresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tipopersona_SelectedIndexChanged(object sender, EventArgs e)
        {


            try
            {
                if ((tipopersona.SelectedItem as ComboboxItem).Value.ToString().Equals("1") || (tipopersona.SelectedItem as ComboboxItem).Value.ToString().Equals("2"))
                {
                    fisica.Visible = true;
                    moral.Visible = false;

                }

                if ((tipopersona.SelectedItem as ComboboxItem).Value.ToString().Equals("3") || (tipopersona.SelectedItem as ComboboxItem).Value.ToString().Equals("4"))
                {
                    moral.Visible = true;
                    fisica.Visible = false;

                }
            }
            catch
            {

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

        private void Finteresado_FormClosing(object sender, FormClosingEventArgs e)
        {
            //capFormcap.Show();
            //this.Close();
        }
    }
}
