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
    public partial class Fclientedetalle : Form
    {
        public Form1 oFormlogin;
        public captura capFormcap;
        public Consulacliente ccpObj;
        public String datoid;
        String idcontacto;
        String iddireccion;
        String idInstruccion;
        String IdObservacion;
        public String PaisId;
        String estatuspais;
        public Fclientedetalle(String datocliente, Form1 form, captura Formcap)
        {
            oFormlogin = form;
            capFormcap = Formcap;
            datoid = datocliente;
            //ccpObj = ccObj;
            InitializeComponent();
            // comienza la consulta para la primera pantalla datos generales del contacto.

            conect contec = new conect();
            String kwery = "SELECT tipo_persona.clave_tipopersona,  tipo_persona.nombre_tipopersona FROM tipo_persona";
            MySqlDataReader respuestastring = contec.getdatareader(kwery);
            while (respuestastring.Read())
            {
                CB_tipopersonacliente.Items.Add(validareader("nombre_tipopersona", "clave_tipopersona", respuestastring));
            }
            respuestastring.Close();
            contec.Cerrarconexion();

            conect conet2 = new conect();
            String kwery2 = "SELECT idioma.IdiomaId , idioma.IdiomaDescripcion FROM  idioma";
            MySqlDataReader respuestastring2 = conet2.getdatareader(kwery2);
            while (respuestastring2.Read())
            {
                CB_idiomacliente.Items.Add(validareader("IdiomaDescripcion", "IdiomaId", respuestastring2));
            }
            respuestastring2.Close();
            conet2.Cerrarconexion();

            conect conect3 = new conect();
            String kwery3 = "SELECT tipocomunicacion.TipoComunicacionId , tipocomunicacion.TipoComunicacionDescrip FROM  tipocomunicacion";
            MySqlDataReader respuestastring3 = conect3.getdatareader(kwery3);
            while (respuestastring3.Read())
            {
                CB_comunicacioncliente.Items.Add(validareader("TipoComunicacionDescrip", "TipoComunicacionId", respuestastring3));
            }
            respuestastring3.Close();
            conet2.Cerrarconexion();

            conect conect4 = new conect();
            String kwery4 = "SELECT moneda.MonedaId , moneda.MonedaDescrip FROM  moneda";
            MySqlDataReader respuestastring4 = conect4.getdatareader(kwery4);
            while (respuestastring4.Read())
            {
                CB_monedacliente.Items.Add(validareader("MonedaDescrip", "MonedaId", respuestastring4));
            }
            respuestastring4.Close();
            conect4.Cerrarconexion();

            conect conect5 = new conect();
            String kwery5 = "SELECT tipoenviofac.TipoEnvioFacId , tipoenviofac.TipoEnvioFacDescrip FROM  tipoenviofac";
            MySqlDataReader respuestastring5 = conect5.getdatareader(kwery5);
            while (respuestastring5.Read())
            {
                CB_tipoenvio.Items.Add(validareader("TipoEnvioFacDescrip", "TipoEnvioFacId", respuestastring5));
            }
            respuestastring5.Close();
            conect5.Cerrarconexion();

            conect conect6 = new conect();
            String kwery6 = "SELECT tipocliente.TipoClienteId , tipocliente.TipoClienteDescrip  FROM  tipocliente";
            MySqlDataReader respuestastring6 = conect6.getdatareader(kwery6);
            while (respuestastring6.Read())
            {
                CB_tipoclientecliente.Items.Add(validareader("TipoClienteDescrip", "TipoClienteId", respuestastring6));
            }
            respuestastring6.Close();
            conect6.Cerrarconexion();

            //String kwery7 = "SELECT * FROM  usuario";
            //MySqlDataReader respuestastring7 = con1.getdatareader(kwery7);


            //while (respuestastring7.Read())
            //{
            //    CB_responsable.Items.Add(validareader("UsuarioName", "UsuarioId", respuestastring7));
            //}

            conect conect8 = new conect();
            String kwery8 = "SELECT proveedorfacelec.ProveedorFacElecId , proveedorfacelec.ProveedorFacElecDescrip  FROM  proveedorfacelec";
            MySqlDataReader respuestastring8 = conect8.getdatareader(kwery8);
            while (respuestastring8.Read())
            {
                CB_facturaelecliente.Items.Add(validareader("ProveedorFacElecDescrip", "ProveedorFacElecId", respuestastring8));
            }
            respuestastring8.Close();
            conect8.Cerrarconexion();

            conect conect83 = new conect();
            String kwery83 = "SELECT tipotarifa.TipoTarifaId , tipotarifa.TipotarifaDescrip  FROM  tipotarifa";
            MySqlDataReader respuestastring83 = conect83.getdatareader(kwery83);
            while (respuestastring83.Read())
            {
                CB_tarifacliente_cd.Items.Add(validareader("TipotarifaDescrip", "TipoTarifaId", respuestastring83));
            }
            respuestastring83.Close();
            conect8.Cerrarconexion();

            conect conectholder = new conect();
            String kweryholdercliente = "SELECT * FROM  holder";
            MySqlDataReader respuestaholdercliente = conectholder.getdatareader(kweryholdercliente);
            while (respuestaholdercliente.Read())
            {
                CB_holdercliente.Items.Add(validareader("HolderNombre", "HolderId", respuestaholdercliente));
            }
            respuestaholdercliente.Close();
            conectholder.Cerrarconexion();

            conect conectw2 = new conect();
            String query2 = "SELECT * FROM cliente where ClienteId = " + datoid + ";";
            MySqlDataReader respuestastring20 = conectw2.getdatareader(query2);

            //primer parametro es el texto, el segundo parametro es el id o valor y el tercer parametro es el mysqldatareader

            //siniNombreUtilClient = validareader("NombreUtilClient", "ClienteId", respuestastringinsert).Text;


            if (respuestastring20 != null)
            {
                while (respuestastring20.Read())
                {

                    //Guardamos el pais para usarlo en llenar los demas combos
                    PaisId = validareader("PaisId", "ClienteId", respuestastring20).Text;
                    if (PaisId != "")
                    {
                        estatuspais = "1";
                        conect cons = new conect();

                        String querys = "select * from pais where PaisId=" + PaisId;

                        MySqlDataReader respuestastrings = cons.getdatareader(querys);
                        while (respuestastrings.Read())
                        {
                            combopais.SelectedIndex = combopais.Items.Add(validareader("PaisClave", "PaisId", respuestastrings));
                        }

                        respuestastrings.Close();
                        cons.Cerrarconexion();

                        conect cons2 = new conect();

                        String querys2 = "select * from pais where PaisId !=" + PaisId;

                        MySqlDataReader respuestastrings2 = cons2.getdatareader(querys2);
                        while (respuestastrings2.Read())
                        {
                            combopais.Items.Add(validareader("PaisClave", "PaisId", respuestastrings2));
                        }

                        respuestastrings2.Close();
                        cons2.Cerrarconexion();


                        conect conss = new conect();
                        String querys1 = "select * from pais where PaisId=" + PaisId;
                        MySqlDataReader respuestastringss = conss.getdatareader(querys1);
                        while (respuestastringss.Read())
                        {
                            PaisNombre.SelectedIndex = PaisNombre.Items.Add(validareader("PaisNombre", "PaisId", respuestastringss));
         
                        }

                        respuestastringss.Close();
                        conss.Cerrarconexion();


                        conect conss2 = new conect();
                        String queryss2 = "select * from pais where PaisId=";
                        MySqlDataReader respuestastringss2 = conss2.getdatareader(queryss2);
                        while (respuestastringss2.Read())
                        {
                            PaisNombre.SelectedIndex = PaisNombre.Items.Add(validareader("PaisNombre", "PaisId", respuestastringss2));

                        }

                        respuestastringss.Close();
                        conss2.Cerrarconexion();

                        //
                        conect con = new conect();

                        String queryn = "select * from pais where PaisId=" + PaisId;

                        MySqlDataReader respuestastringn = con.getdatareader(queryn);
                        while (respuestastringn.Read())
                        {
                            Nacionalidad.SelectedIndex = Nacionalidad.Items.Add(validareader("PaisNacionalidad", "PaisId", respuestastringn));
                        }
                        //tipopersona_SelectedIndexChanged(seccion);
                        respuestastringn.Close();
                        con.Cerrarconexion();


                    }
                    else
                    {
                        //En algunos casos los clientes no traen el paisid y nacionalidad
   
                        if (PaisId == null || PaisId == "")
                        {
                            estatuspais = "2";
                            conect cons = new conect();

                            String querys = "select * from pais";

                            MySqlDataReader respuestastrings = cons.getdatareader(querys);
                            while (respuestastrings.Read())
                            {
                                combopais.Items.Add(validareader("PaisClave", "PaisId", respuestastrings));
                            }
                            respuestastrings.Close();
                            cons.Cerrarconexion();
                            conect conss = new conect();
                            MySqlDataReader respuestastringss = conss.getdatareader(querys);
                            while (respuestastringss.Read())
                            {
                               PaisNombre.Items.Add(validareader("PaisNombre", "PaisId", respuestastringss));
                            }
                            respuestastringss.Close();
                            conss.Cerrarconexion();
                            conect con = new conect();
                            String query = "select * from pais";

                            MySqlDataReader respuestastringn = con.getdatareader(query);
                            while (respuestastringn.Read())
                            {
                                Nacionalidad.Items.Add(validareader("PaisNacionalidad", "PaisId", respuestastringn));
                            }
                            //tipopersona_SelectedIndexChanged(seccion);
                            respuestastringn.Close();
                            con.Cerrarconexion();

                        }
                
                    }

                    TB_clienteid.Text = validareader("ClienteId", "ClienteId", respuestastring20).Text;
                    label49.Text = validareader("NombreUtilClient", "ClienteId", respuestastring20).Text;
                    TB_curpcliente.Text = validareader("ClienteCurp", "ClienteId", respuestastring20).Text;
                    TB_rfccliente.Text = validareader("ClienteRFC", "ClienteId", respuestastring20).Text;
                    TB_nombrecliente.Text = validareader("NombreUtilClient", "ClienteId", respuestastring20).Text;
                    TB_apellidopaternocliente.Text = validareader("ClienteApellidoPaterno", "ClienteId", respuestastring20).Text;
                    TB_apellidomaternoclietne.Text = validareader("ClienteApellidoMaterno", "ClienteId", respuestastring20).Text;
                    TB_sitioweb.Text = validareader("ClienteWebSite", "ClienteId", respuestastring20).Text;
                    TB_correo.Text = validareader("ClienteEmail", "ClienteId", respuestastring20).Text;
                    RTB_observacionescliente.Text = validareader("ClienteObservacion", "ClienteId", respuestastring20).Text;
                    //CB_holdercliente.Text = validareader("HolderId", "ClienteId", respuestastring20).Text;
                    if (validareader("HolderId", "ClienteId", respuestastring20).Text != "")
                    {
                        conect conectholder2 = new conect();
                        String kweryholdercliente2 = "SELECT * FROM  holder WHERE holder.HolderId = " + validareader("HolderId", "ClienteId", respuestastring20).Text + ";";
                        MySqlDataReader respuestaholdercliente2 = conectholder2.getdatareader(kweryholdercliente2);

                        respuestaholdercliente2.Read();
                        CB_holdercliente.Text = validareader("HolderNombre", "HolderId", respuestaholdercliente2).Text;
                        respuestaholdercliente.Close();
                        conectholder2.Cerrarconexion();

                    }
                    //CB_facturaelecliente.Text = validareader("ProveedorFacElecId", "ClienteId", respuestastring20).Text;
                    if (validareader("ProveedorFacElecId", "ClienteId", respuestastring20).Text != "")
                    {
                        conect conectfactura = new conect();
                        String kweryfacturaelec = "SELECT proveedorfacelec.ProveedorFacElecId , proveedorfacelec.ProveedorFacElecDescrip  FROM  proveedorfacelec WHERE proveedorfacelec.ProveedorFacElecId = " + validareader("ProveedorFacElecId", "ClienteId", respuestastring20).Text + ";";
                        MySqlDataReader respuestafacturaelec = conectfactura.getdatareader(kweryfacturaelec);
                        respuestafacturaelec.Read();
                        CB_facturaelecliente.Text = validareader("ProveedorFacElecDescrip", "ProveedorFacElecId", respuestafacturaelec).Text;
                        respuestafacturaelec.Close();
                        conectfactura.Cerrarconexion();

                    }
                    //CB_tipoclientecliente.Text = validareader("TipoClienteId", "ClienteId", respuestastring20).Text; 
                    if (validareader("TipoClienteId", "ClienteId", respuestastring20).Text != "")
                    {
                        conect conecttipocliente = new conect();
                        String kwerytipocliente = " SELECT tipocliente.TipoClienteId , tipocliente.TipoClienteDescrip  FROM  tipocliente WHERE tipocliente.TipoClienteId =  " + validareader("TipoClienteId", "ClienteId", respuestastring20).Text + ";";
                        MySqlDataReader respuestatipocliente = conecttipocliente.getdatareader(kwerytipocliente);
                        respuestatipocliente.Read();
                        CB_tipoclientecliente.Text = validareader("TipoClienteDescrip", "TipoClienteId", respuestatipocliente).Text;
                        respuestatipocliente.Close();
                        conecttipocliente.Cerrarconexion();

                    }
                    // CB_comunicacioncliente.Text = validareader("TipoComunicacionId", "ClienteId", respuestastring20).Text;  
                    if (validareader("TipoComunicacionId", "ClienteId", respuestastring20).Text != "")
                    {
                        conect conecttipocomunicacion = new conect();
                        String kwerytipocomunicacion = " SELECT tipocomunicacion.TipoComunicacionId , tipocomunicacion.TipoComunicacionDescrip FROM  tipocomunicacion WHERE tipocomunicacion.TipoComunicacionId =  " + validareader("TipoComunicacionId", "ClienteId", respuestastring20).Text + ";";
                        MySqlDataReader respuestatipocomunicacion = conecttipocomunicacion.getdatareader(kwerytipocomunicacion);
                        respuestatipocomunicacion.Read();
                        CB_comunicacioncliente.Text = validareader("TipoComunicacionDescrip", "TipoComunicacionId", respuestatipocomunicacion).Text;
                        respuestatipocomunicacion.Close();
                        conecttipocomunicacion.Cerrarconexion();

                    }
                    // CB_tipoenvio.Text = validareader("TipoEnvioFacId", "ClienteId", respuestastring20).Text;
                    if (validareader("TipoEnvioFacId", "ClienteId", respuestastring20).Text != "")
                    {
                        conect conecttipoenvio = new conect();
                        String kwerytipoenvio = " SELECT tipoenviofac.TipoEnvioFacId , tipoenviofac.TipoEnvioFacDescrip FROM  tipoenviofac WHERE tipoenviofac.TipoEnvioFacId =  " + validareader("TipoEnvioFacId", "ClienteId", respuestastring20).Text + ";";
                        MySqlDataReader respuestatipoenvio = conecttipoenvio.getdatareader(kwerytipoenvio);
                        respuestatipoenvio.Read();
                        CB_tipoenvio.Text = validareader("TipoEnvioFacDescrip", "TipoEnvioFacId", respuestatipoenvio).Text;
                        respuestatipoenvio.Close();
                        conecttipoenvio.Cerrarconexion();

                    }
                    //CB_idiomacliente.Text = validareader("IdiomaId", "ClienteId", respuestastring20).Text;
                    if (validareader("IdiomaId", "ClienteId", respuestastring20).Text != "")
                    {
                        conect conectidioma = new conect();
                        String kwerytipoidioma = " SELECT idioma.IdiomaId , idioma.IdiomaDescripcion FROM  idioma WHERE idioma.IdiomaId =  " + validareader("IdiomaId", "ClienteId", respuestastring20).Text + ";";
                        MySqlDataReader respuestatipoidioma = conectidioma.getdatareader(kwerytipoidioma);
                        respuestatipoidioma.Read();
                        CB_idiomacliente.Text = validareader("IdiomaDescripcion", "IdiomaId", respuestatipoidioma).Text;
                        respuestatipoidioma.Close();
                        conectidioma.Cerrarconexion();

                    }
                    //CB_tarifacliente_cd.Text = validareader("TipoTarifaId", "ClienteId", respuestastring20).Text;
                    if (validareader("TipoTarifaId", "ClienteId", respuestastring20).Text != "")
                    {
                        conect conecttipotarifa = new conect();
                        String kwerytipotarifa = " SELECT tipotarifa.TipoTarifaId , tipotarifa.TipotarifaDescrip  FROM  tipotarifa WHERE tipotarifa.TipoTarifaId =  " + validareader("TipoTarifaId", "ClienteId", respuestastring20).Text + ";";
                        MySqlDataReader respuestatipotarifa = conecttipotarifa.getdatareader(kwerytipotarifa);
                        respuestatipotarifa.Read();
                        CB_tarifacliente_cd.Text = validareader("TipotarifaDescrip", "TipoTarifaId", respuestatipotarifa).Text;
                        respuestatipotarifa.Close();
                        conecttipotarifa.Cerrarconexion();
                    }
                    //CB_monedacliente.Text = validareader("MonedaId", "ClienteId", respuestastring20).Text;
                    if (validareader("MonedaId", "ClienteId", respuestastring20).Text != "")
                    {
                        conect conectmoneda = new conect();
                        String kwerytipomoneda = "SELECT moneda.MonedaId , moneda.MonedaDescrip FROM  moneda WHERE moneda.MonedaId =  " + validareader("MonedaId", "ClienteId", respuestastring20).Text + ";";
                        MySqlDataReader respuestatipomoneda = conectmoneda.getdatareader(kwerytipomoneda);
                        respuestatipomoneda.Read();
                        CB_monedacliente.Text = validareader("MonedaDescrip", "MonedaId", respuestatipomoneda).Text;
                        respuestatipomoneda.Close();
                        conectmoneda.Cerrarconexion();

                    }
                    //CB_tipopersonacliente.Text = validareader("ClienteTipoPersonaSAT", "ClienteId", respuestastring20).Text; primero los text box luego los combo box
                    if (validareader("ClienteTipoPersonaSAT", "ClienteId", respuestastring20).Text != "")
                    {
                        conect conectpersona = new conect();
                        String kwerytipopersona = "SELECT tipo_persona.clave_tipopersona,  tipo_persona.nombre_tipopersona FROM tipo_persona WHERE tipo_persona.clave_tipopersona =  '" + validareader("ClienteTipoPersonaSAT", "ClienteId", respuestastring20).Text + "';";
                        MySqlDataReader respuestatipopersona = conectpersona.getdatareader(kwerytipopersona);
                        respuestatipopersona.Read();
                        CB_tipopersonacliente.Text = validareader("nombre_tipopersona", "clave_tipopersona", respuestatipopersona).Text;
                        respuestatipopersona.Close();
                        conectpersona.Cerrarconexion();

                    }


                }
                respuestastring20.Close();
                conectw2.Cerrarconexion();

            }



            // datos que se setean en la primera pantalla 





            //comienza la consulta para la segunda pantalla llamada contactos

            CheckB_habilitadocontacto.CheckState = CheckState.Checked;

            ComboboxItem cItemresult_act = new ComboboxItem();
            cItemresult_act.Text = "M";
            cItemresult_act.Value = 1;
            ComboboxItem cItemresult_inact = new ComboboxItem();
            cItemresult_inact.Text = "F";
            cItemresult_inact.Value = 2;

            CB_sexocontacto.Items.Add(cItemresult_act);
            CB_sexocontacto.Items.Add(cItemresult_inact);

            conect kwery3conect = new conect();
            String query3 = "SELECT * FROM contacto where ClienteId =" + datoid + ";";
            MySqlDataReader respuestastring30 = kwery3conect.getdatareader(query3);

            if (respuestastring30 == null)
            {
                MessageBox.Show("Fallo consulta del contacto");
            }
            else
            {
                int count = 0;
                while (respuestastring30.Read())
                {

                    ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);
                    listaagregar.SubItems.Add(validareader("ContactoNombre", "ContactoId", respuestastring30).Text);
                    listaagregar.SubItems.Add(validareader("ContactoNick", "ContactoId", respuestastring30).Text);
                    listaagregar.SubItems.Add(validareader("ContactoTelefono", "ContactoId", respuestastring30).Text);
                    listaagregar.SubItems.Add(validareader("ContactoEmail", "ContactoId", respuestastring30).Text);
                    listaagregar.SubItems.Add(validareader("ContactoArea", "ContactoId", respuestastring30).Text);
                    listaagregar.SubItems.Add(validareader("ContactoPuesto", "ContactoId", respuestastring30).Text);
                    listaagregar.SubItems.Add(validareader("ContactoSexo", "ContactoId", respuestastring30).Text);
                    if (validareader("ContactoIndAct", "ContactoId", respuestastring30).Text == "1")
                    {
                        listaagregar.SubItems.Add("Habilitado");
                    }
                    else
                    {
                        listaagregar.SubItems.Add("Inhabilitado");
                    }
                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listaagregar.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listaagregar.BackColor = Color.Azure;
                    }
                    listView1.Items.Add(listaagregar);
                    listView1.FullRowSelect = true;
                    count++;

                }
            }

            TB_contactonumcliente_cd.Text = datoid;

            // temina segunda pantalla 

            //Comienza la consulta de la tercera pantalla

            CheckB_direccionhab_cd.CheckState = CheckState.Checked;

            conect contecdireccionpais = new conect();
            String kwerydireccionpais = "SELECT pais.PaisId,  pais.PaisNombre FROM pais order by pais.PaisNombre asc";
            MySqlDataReader respuestadireccionpais = contecdireccionpais.getdatareader(kwerydireccionpais);
            while (respuestadireccionpais.Read())
            {
                CB_direccionpais_cd.Items.Add(validareader("PaisNombre", "PaisId", respuestadireccionpais));
            }
            respuestadireccionpais.Close();
            contecdireccionpais.Cerrarconexion();

            conect conecttipodireccion = new conect();
            String kwerytipodireccion = "SELECT tipodireccion.TipoDireccionId,  tipodireccion.TipoDireccionDescrip FROM tipodireccion";
            MySqlDataReader respuestatipodireccion = conecttipodireccion.getdatareader(kwerytipodireccion);
            while (respuestatipodireccion.Read())
            {
                CB_direcciontipo_cd.Items.Add(validareader("TipoDireccionDescrip", "TipoDireccionId", respuestatipodireccion));
            }
            respuestatipodireccion.Close();
            conecttipodireccion.Cerrarconexion();


            conect conectselect = new conect();
            String query4 = "SELECT " +
                                "direccion.DireccionID, " +
                                "DAMETIPODIRECCCION(direccion.TipoDireccionId) as Tipodireccion, " +
                                "direccion.DireccionCalle, " +
                                "direccion.DireccionNumExt, " +
                                "direccion.DireccionNumInt, " +
                                "direccion.DireccionCP, " +
                                "direccion.DireccionColonia, " +
                                "direccion.DireccionPoblacion, " +
                                "direccion.DireccionEstado, " +
                                "DameNombrePais(direccion.PaisId) as Clavepais, " +
                                "direccion.DireccionIndAct " +
                            "FROM direccion WHERE ClienteId =" + datoid + ";";
            MySqlDataReader respuestastring40 = conectselect.getdatareader(query4);

            if (respuestastring40 == null)
            {
                MessageBox.Show("Fallo la consulta");
            }
            else
            {
                while (respuestastring40.Read())
                {
                    // ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);

                    ListViewItem listaagregar = new ListViewItem(validareader("DireccionID", "DireccionID", respuestastring40).Text);
                    listaagregar.SubItems.Add(validareader("Tipodireccion", "DireccionID", respuestastring40).Text);
                    listaagregar.SubItems.Add(validareader("DireccionCalle", "DireccionID", respuestastring40).Text);
                    listaagregar.SubItems.Add(validareader("DireccionNumExt", "DireccionID", respuestastring40).Text);
                    listaagregar.SubItems.Add(validareader("DireccionNumInt", "DireccionID", respuestastring40).Text);
                    listaagregar.SubItems.Add(validareader("DireccionCP", "DireccionID", respuestastring40).Text);
                    listaagregar.SubItems.Add(validareader("DireccionColonia", "DireccionID", respuestastring40).Text);
                    listaagregar.SubItems.Add(validareader("DireccionPoblacion", "DireccionID", respuestastring40).Text);
                    listaagregar.SubItems.Add(validareader("DireccionEstado", "DireccionID", respuestastring40).Text);
                    listaagregar.SubItems.Add(validareader("Clavepais", "DireccionID", respuestastring40).Text);
                    if (validareader("DireccionIndAct", "DireccionID", respuestastring40).Text == "1")
                    {
                        listaagregar.SubItems.Add("Habilitado");
                    }
                    else
                    {
                        listaagregar.SubItems.Add("Inhabilitado");
                    }
                    listView2.Items.Add(listaagregar);
                    listView2.FullRowSelect = true;

                }


                TB_direccionnumcliente_cd.Text = datoid;
                respuestastring40.Close();
                conectselect.Cerrarconexion();
            }


            //comineza la cuarta consulta CB_instruccionestipo_cd CB_usuarioinstrucciones_cd

            conect conecinstucion = new conect();
            String kwerytipoinstruccion = "SELECT tipoinstruccion.TipoInstruccionId, tipoinstruccion.TipoInstruccionDescrip  FROM  tipoinstruccion ";
            MySqlDataReader respuestastringtipoinstuccion = conecinstucion.getdatareader(kwerytipoinstruccion);

            while (respuestastringtipoinstuccion.Read())
            {
                CB_instruccionestipo_cd.Items.Add(validareader("TipoInstruccionDescrip", "TipoInstruccionId", respuestastringtipoinstuccion));
            }
            respuestastringtipoinstuccion.Close();
            conecinstucion.Cerrarconexion();

            conect conectusuarioinstruccion = new conect();
            String kweryusuarioinstruccion = "SELECT usuario.UsuarioId , usuario.UsuarioName FROM usuario ";
            MySqlDataReader respuestausuarioinstruccion = conectusuarioinstruccion.getdatareader(kweryusuarioinstruccion);



            while (respuestausuarioinstruccion.Read())
            {
                CB_usuarioinstrucciones_cd.Items.Add(validareader("UsuarioName", "UsuarioId", respuestausuarioinstruccion));
            }
            respuestausuarioinstruccion.Close();
            conectusuarioinstruccion.Cerrarconexion();
            // DATE_FORMAT(caso_patente.CasoFechaInternacional,'%d-%m-%Y') AS CasoFechaInternacional
            conect conectinstruccion = new conect();
            String kweryconsultainstruccion = "SELECT "+
                                                "instruccion.InstruccionId, "+
                                                "DAMETIPOINSTRUCCION(instruccion.TipoInstruccionId) AS Tipoinstruccion, "+
                                                "instruccion.InstrucciondDescip, "+
                                                "DAMEELUSUARIO(instruccion.UsuarioId) AS Usuario, "+
                                                "DATE_FORMAT(instruccion.InstruccionFecha,'%d-%m-%Y') AS InstruccionFecha, " +
                                                "DATE_FORMAT(instruccion.InstruccionFechaRec,'%d-%m-%Y') AS InstruccionFechaRec, " +
                                                "instruccion.InstruccionIndAct "+
                                                "FROM instruccion WHERE ClienteId =" + datoid + ";";
            MySqlDataReader respuestaconsultainstruccion = conectinstruccion.getdatareader(kweryconsultainstruccion);

            if (respuestaconsultainstruccion == null)
            {
                MessageBox.Show("Fallo la consulta de la instrucción");
            }
            else
            {
                int count = 0;
                while (respuestaconsultainstruccion.Read())
                {
                    // ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);

                    ListViewItem listaagregar3 = new ListViewItem(validareader("InstruccionId", "InstruccionId", respuestaconsultainstruccion).Text);
                    listaagregar3.SubItems.Add(validareader("Tipoinstruccion", "InstruccionId", respuestaconsultainstruccion).Text);
                    listaagregar3.SubItems.Add(validareader("InstrucciondDescip", "InstruccionId", respuestaconsultainstruccion).Text);
                    listaagregar3.SubItems.Add(validareader("Usuario", "InstruccionId", respuestaconsultainstruccion).Text);
                    listaagregar3.SubItems.Add(validafechavacia(validareader("InstruccionFecha", "InstruccionId", respuestaconsultainstruccion).Text));
                    listaagregar3.SubItems.Add(validafechavacia(validareader("InstruccionFechaRec", "InstruccionId", respuestaconsultainstruccion).Text));
                    if (validareader("InstruccionIndAct", "InstruccionId", respuestaconsultainstruccion).Text == "1")
                    {
                        listaagregar3.SubItems.Add("Habilitado");
                    }
                    else
                    {
                        listaagregar3.SubItems.Add("Inhabilitado");
                    }

                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listaagregar3.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listaagregar3.BackColor = Color.Azure;
                    }

                    listView3.Items.Add(listaagregar3);
                    listView3.FullRowSelect = true;
                    count++;
                }

                respuestaconsultainstruccion.Close();
                conectinstruccion.Cerrarconexion();

            }


            DTP_fechainstruccion_instruciones_cd.CustomFormat = "yyyy-MM-dd";
            DTP_fechainstruccion_instruciones_cd.Format = DateTimePickerFormat.Custom;

            DTP_fecharegistro.CustomFormat = "yyyy-MM-dd";
            DTP_fecharegistro.Format = DateTimePickerFormat.Custom;

            CHECKB_instruccion_cd.CheckState = CheckState.Checked;

            // termina cuarta consulta 

            // comienza quinta consulta para quinta pantalla

            conect consultapatentecaso = new conect();
            String kwerypatentecaso = "SELECT "+
                                        " caso_patente.CasoId, "+
                                        " DameLaReferencia(caso_patente.CasoId) AS referencia, "+
                                        " caso_patente.CasoNumeroExpedienteLargo, "+
                                        " DATE_FORMAT(caso_patente.CasoFechaLegal,'%d-%m-%Y') AS CasoFechaLegal, " +
                                        " caso_patente.CasoNumConcedida, "+
                                        " DATE_FORMAT(caso_patente.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, " +
                                        " DATE_FORMAT(caso_patente.CasoFechaVigencia,'%d-%m-%Y') AS CasoFechaVigencia, " +
                                        " DameTipoSolicitudDescrip(caso_patente.TipoSolicitudId) AS TipoDeSolicitud, "+
                                        " DameEstatusCasoDescrip(caso_patente.EstatusCasoId) AS Estatus , "+
                                        " caso_patente.CasoTitular, "+
                                        " caso_patente.CasoTituloespanol, "+
                                        " Dameelusuario(caso_patente.ResponsableId) AS Responsable, "+
                                        " Get_Prioridad(caso_patente.CasoId) As Prioridades , "+
                                        " Get_Interesados(caso_patente.CasoId) AS Interesados ,  "+
                                        " Get_AnualidadFechaLimitePago(caso_patente.CasoId) AS Fechalimitepago, " +
                                        " Damealcontactocaso (casocliente.contactoid) AS Contactos, " +
                                        " Dameelcorreocontactocaso (casocliente.contactoid) AS Correos, " +
                                        " Get_anualidadespagadas (caso_patente.CasoId) AS Anualidades, " +
                                        " DameNombrePais(caso_patente.PaisId) AS Pais "+
                                    "FROM "+
                                        " caso_patente, "+
                                        " cliente, "+
                                        " casocliente "+
                                    "WHERE  "+
                                        " cliente.ClienteId = " + datoid +
                                        " AND caso_patente.CasoId = casocliente.CasoId "+
                                        " AND casocliente.ClienteId = cliente.ClienteId;";
            MySqlDataReader respuestapatente = consultapatentecaso.getdatareader(kwerypatentecaso);

            if (respuestapatente != null)
            {
                int count = 0;
                while (respuestapatente.Read())
                {
                    //ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);
       
                    ListViewItem listacasos = new ListViewItem(validareader("CasoId", "CasoId", respuestapatente).Text);// numero
                    listacasos.SubItems.Add(validareader("referencia", "CasoId", respuestapatente).Text);  // refererencia
                    listacasos.SubItems.Add(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestapatente).Text); // expediente
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaLegal", "CasoId", respuestapatente).Text)); // fecha legal - fecha recepcion
                    listacasos.SubItems.Add(validareader("CasoNumConcedida", "CasoId", respuestapatente).Text); // registro
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaConcesion", "CasoId", respuestapatente).Text)); // fecha consecion
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaVigencia", "CasoId", respuestapatente).Text)); // fecha vigencia 
                    listacasos.SubItems.Add(validareader("TipoDeSolicitud", "CasoId", respuestapatente).Text);// tipo de solicitud
                    listacasos.SubItems.Add(validareader("Estatus", "CasoId", respuestapatente).Text);  // estatus
                    listacasos.SubItems.Add(validareader("CasoTitular", "CasoId", respuestapatente).Text);  // titular
                    listacasos.SubItems.Add(validareader("CasoTituloespanol", "CasoId", respuestapatente).Text);  // titulo
                    listacasos.SubItems.Add(""); // clase
                    listacasos.SubItems.Add(validareader("Contactos", "CasoId", respuestapatente).Text); // contacto
                    listacasos.SubItems.Add(validareader("Correos", "CasoId", respuestapatente).Text); // correo
                    listacasos.SubItems.Add(validareader("Responsable", "CasoId", respuestapatente).Text); //responsable
                    listacasos.SubItems.Add(validareader("Prioridades", "CasoId", respuestapatente).Text);  // prioridades
                    listacasos.SubItems.Add(validareader("Interesados", "CasoId", respuestapatente).Text); // interesados
                    listacasos.SubItems.Add(validareader("Anualidades", "CasoId", respuestapatente).Text);  // anualidades a pagar
                    listacasos.SubItems.Add(validafechavacia(validareader("Fechalimitepago", "CasoId", respuestapatente).Text)); // fecha prox pago
                    listacasos.SubItems.Add(validareader("Pais", "CasoId", respuestapatente).Text);  // pais 
                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listacasos.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listacasos.BackColor = Color.Azure;
                    }
                    listView4.Items.Add(listacasos);
                    listView4.FullRowSelect = true;
                    count++;
                }

                respuestapatente.Close();
                consultapatentecaso.Cerrarconexion();
            }


            // sigue casos marca


            conect consultamarcas = new conect();
            String kweryconsultamarcas = "SELECT "+
                                            " caso_marcas.CasoId, "+
                                            " DameLaReferencia(caso_marcas.CasoId) AS Referencia, "+
                                            " caso_marcas.CasoNumeroExpedienteLargo, "+
                                            " DATE_FORMAT(caso_marcas.CasoFechaLegal,'%d-%m-%Y') AS CasoFechaLegal, "+
                                            " caso_marcas.CasoNumConcedida, "+
                                            " DATE_FORMAT(caso_marcas.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, "+
                                            " DATE_FORMAT(caso_marcas.CasoFechaVigencia,'%d-%m-%Y') AS CasoFechaVigencia, "+
                                            " DameTipoSolicitudDescrip(caso_marcas.TipoSolicitudId) AS Tipodesolicitud, "+
                                            " DameEstatusCasoDescrip(caso_marcas.EstatusCasoId) AS Estatus, "+
                                            " caso_marcas.CasoTitular, "+
                                            " caso_marcas.CasoTituloespanol, "+
                                            " Dameelusuario(caso_marcas.ResponsableId) AS Responsable, "+
                                            " Get_Prioridad(caso_marcas.CasoId) AS Prioridades, " +
                                            " Get_Interesados(caso_marcas.CasoId) AS Interesados, "+
                                            " Damealcontactocaso (casocliente.contactoid) AS Contactos, " +
                                            " Dameelcorreocontactocaso (casocliente.contactoid) AS Correos, " +
                                            " DameNombrePais(caso_marcas.PaisId) AS Pais, " +
                                            " Damelaclase(caso_marcas.CasoId) as Clase "+
                                        " FROM "+
                                            " caso_marcas, "+
                                            " casocliente, "+
                                            " cliente " +
                                        " WHERE "+
                                            " cliente.ClienteId = " + datoid +
                                              "  AND casocliente.CasoId = caso_marcas.CasoId "+
                                              "  AND casocliente.ClienteId = cliente.ClienteId";
            MySqlDataReader respuestaconsultamarcas = consultamarcas.getdatareader(kweryconsultamarcas);
            

            if (respuestaconsultamarcas != null)
            {
                int count = 0;

                while (respuestaconsultamarcas.Read())
                {
                    //ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);

                    ListViewItem listacasos = new ListViewItem(validareader("CasoId", "CasoId", respuestaconsultamarcas).Text);// numero
                    listacasos.SubItems.Add(validareader("Referencia", "CasoId", respuestaconsultamarcas).Text);  // refererencia
                    listacasos.SubItems.Add(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestaconsultamarcas).Text); // expediente
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaLegal", "CasoId", respuestaconsultamarcas).Text)); // fecha legal - fecha recepcion
                    listacasos.SubItems.Add(validareader("CasoNumConcedida", "CasoId", respuestaconsultamarcas).Text); // registro
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaConcesion", "CasoId", respuestaconsultamarcas).Text)); // fecha consecion
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaVigencia", "CasoId", respuestaconsultamarcas).Text)); // fecha vigencia 
                    listacasos.SubItems.Add(validareader("TipoDeSolicitud", "CasoId", respuestaconsultamarcas).Text);// tipo de solicitud
                    listacasos.SubItems.Add(validareader("Estatus", "CasoId", respuestaconsultamarcas).Text);  // estatus
                    listacasos.SubItems.Add(validareader("CasoTitular", "CasoId", respuestaconsultamarcas).Text);  // titular
                    listacasos.SubItems.Add(validareader("CasoTituloespanol", "CasoId", respuestaconsultamarcas).Text);  // titulo
                    listacasos.SubItems.Add(validareader("Clase", "CasoId", respuestaconsultamarcas).Text); // clase
                    listacasos.SubItems.Add(validareader("Contactos", "CasoId", respuestaconsultamarcas).Text); // contacto
                    listacasos.SubItems.Add(validareader("Correos", "CasoId", respuestaconsultamarcas).Text); // correo
                    listacasos.SubItems.Add(validareader("Responsable", "CasoId", respuestaconsultamarcas).Text); //responsable
                    listacasos.SubItems.Add(validareader("Prioridades", "CasoId", respuestaconsultamarcas).Text);  // prioridades
                    listacasos.SubItems.Add(validareader("Interesados", "CasoId", respuestaconsultamarcas).Text); // interesados
                    listacasos.SubItems.Add("");  // anualidades a pagar
                    listacasos.SubItems.Add(""); // fecha prox pago
                    listacasos.SubItems.Add(validareader("Pais", "CasoId", respuestaconsultamarcas).Text);  // pais 
                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listacasos.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listacasos.BackColor = Color.Azure;
                    }
                    listView4.Items.Add(listacasos);
                    listView4.FullRowSelect = true;
                    count++;
                }

                respuestaconsultamarcas.Close();
                consultamarcas.Cerrarconexion();

            }
        // termina la consulta de marcas

       // comienza la consulta de indautor de registro de obra registro de obra

            conect consultaregistroobra = new conect();
            String kweryregistrodeobraconsulta = "SELECT "+
                                                    " caso_registrodeobra.CasoId, "+
                                                    " DameLaReferencia(caso_registrodeobra.CasoId) AS Referencia, " +
                                                    " caso_registrodeobra.CasoNumeroExpedienteLargo, "+
                                                    " DATE_FORMAT(caso_registrodeobra.CasoFechaLegal,'%d-%m-%Y') AS CasoFechaLegal, "+
                                                    " caso_registrodeobra.CasoNumConcedida, "+
                                                    " DATE_FORMAT(caso_registrodeobra.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, "+ 
                                                    " DATE_FORMAT(caso_registrodeobra.CasoFechaVigencia,'%d-%m-%Y') AS CasoFechaVigencia,  "+
                                                    " DameTipoSolicitudDescrip(caso_registrodeobra.TipoSolicitudId) AS Tipodesolicitud, "+
                                                    " DameEstatusCasoDescrip(caso_registrodeobra.EstatusCasoId) AS Estatus, "+
                                                    " caso_registrodeobra.CasoTitular,  "+
                                                    " caso_registrodeobra.CasoTituloespanol, "+
                                                    " Dameelusuario(caso_registrodeobra.ResponsableId) AS Responsable, "+
                                                    " Get_Prioridad(caso_registrodeobra.CasoId) AS Prioridades , "+
                                                    " Get_Interesados(caso_registrodeobra.CasoId) AS Interesados, "+
                                                    " Damealcontactocaso (casocliente.contactoid) AS Contactos, " +
                                                    " Dameelcorreocontactocaso (casocliente.contactoid) AS Correos, " +
                                                    " DameNombrePais(caso_registrodeobra.PaisId) AS Pais "+
                                                " FROM "+
                                                    " caso_registrodeobra, "+
                                                    " casocliente, "+
                                                    " cliente "+
                                                " WHERE "+
                                                   " cliente.ClienteId = " + datoid +
                                                        " AND casocliente.CasoId = caso_registrodeobra.CasoId "+
                                                        " AND casocliente.ClienteId = cliente.ClienteId";


            MySqlDataReader respuestaconsultaregistro = consultaregistroobra.getdatareader(kweryregistrodeobraconsulta);

            if (respuestaconsultaregistro != null)
            {
                int count = 0;

                while (respuestaconsultaregistro.Read())
                {
                    //ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);

                    ListViewItem listacasos = new ListViewItem(validareader("CasoId", "CasoId", respuestaconsultaregistro).Text);// numero
                    listacasos.SubItems.Add(validareader("Referencia", "CasoId", respuestaconsultaregistro).Text);  // refererencia
                    listacasos.SubItems.Add(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestaconsultaregistro).Text); // expediente
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaLegal", "CasoId", respuestaconsultaregistro).Text)); // fecha legal - fecha recepcion
                    listacasos.SubItems.Add(validareader("CasoNumConcedida", "CasoId", respuestaconsultaregistro).Text); // registro
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaConcesion", "CasoId", respuestaconsultaregistro).Text)); // fecha consecion
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaVigencia", "CasoId", respuestaconsultaregistro).Text)); // fecha vigencia 
                    listacasos.SubItems.Add(validareader("TipoDeSolicitud", "CasoId", respuestaconsultaregistro).Text);// tipo de solicitud
                    listacasos.SubItems.Add(validareader("Estatus", "CasoId", respuestaconsultaregistro).Text);  // estatus
                    listacasos.SubItems.Add(validareader("CasoTitular", "CasoId", respuestaconsultaregistro).Text);  // titular
                    listacasos.SubItems.Add(validareader("CasoTituloespanol", "CasoId", respuestaconsultaregistro).Text);  // titulo
                    listacasos.SubItems.Add(""); // clase
                    listacasos.SubItems.Add(validareader("Contactos", "CasoId", respuestaconsultaregistro).Text); // contacto
                    listacasos.SubItems.Add(validareader("Correos", "CasoId", respuestaconsultaregistro).Text); // correo
                    listacasos.SubItems.Add(validareader("Responsable", "CasoId", respuestaconsultaregistro).Text); //responsable
                    listacasos.SubItems.Add(validareader("Prioridades", "CasoId", respuestaconsultaregistro).Text);  // prioridades
                    listacasos.SubItems.Add(validareader("Interesados", "CasoId", respuestaconsultaregistro).Text); // interesados
                    listacasos.SubItems.Add("");  // anualidades a pagar
                    listacasos.SubItems.Add(""); // fecha prox pago
                    listacasos.SubItems.Add(validareader("Pais", "CasoId", respuestaconsultaregistro).Text);  // pais 
                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listacasos.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listacasos.BackColor = Color.Azure;
                    }
                    listView4.Items.Add(listacasos);
                    listView4.FullRowSelect = true;
                    count++;
                }

                respuestaconsultaregistro.Close();
                consultaregistroobra.Cerrarconexion();
            }


           // temirmina consulta registro de obra

            // Comienza la consulta de reserva de derechos 

            conect consultareserva = new conect();
            String kweryreserva = "SELECT "+
                                    " caso_reservadederechos.CasoId, "+
	                                " DameLaReferencia(caso_reservadederechos.CasoId) AS Referencia, "+
                                    " caso_reservadederechos.CasoNumeroExpedienteLargo, "+
                                    " DATE_FORMAT(caso_reservadederechos.CasoFechaLegal,'%d-%m-%Y') AS CasoFechaLegal, "+
                                    " caso_reservadederechos.CasoNumConcedida, "+
                                    " DATE_FORMAT(caso_reservadederechos.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, "+
                                    " DATE_FORMAT(caso_reservadederechos.CasoFechaVigencia,'%d-%m-%Y') AS CasoFechaVigencia,  "+
                                    " DameTipoSolicitudDescrip(caso_reservadederechos.TipoSolicitudId) AS Tipodesolicitud, "+
                                    " DameEstatusCasoDescrip(caso_reservadederechos.EstatusCasoId) AS Estatus, "+
                                    " caso_reservadederechos.CasoTitular, "+
                                    " caso_reservadederechos.CasoTituloespanol, "+
                                    " Dameelusuario(caso_reservadederechos.ResponsableId) AS Responsable, "+
                                    " Get_Prioridad(caso_reservadederechos.CasoId) AS Prioridades, "+
                                    " Get_Interesados(caso_reservadederechos.CasoId) AS Interesados, "+
                                    " Damealcontactocaso (casocliente.contactoid) AS Contactos, " +
                                    " Dameelcorreocontactocaso (casocliente.contactoid) AS Correos, " +
                                    " DameNombrePais(caso_reservadederechos.PaisId) AS Pais "+
                                " FROM "+
                                    " caso_reservadederechos, "+
                                    " casocliente, "+
                                    " cliente "+
                               " WHERE "+
                                    " cliente.ClienteId = " + datoid +
                                    "    AND caso_reservadederechos.CasoId = casocliente.CasoId "+
                                    "    AND casocliente.ClienteId = cliente.ClienteId";

            MySqlDataReader respuestaconsultareserva = consultareserva.getdatareader(kweryreserva);

            if (respuestaconsultareserva != null)
            {
                int count = 0;
                while (respuestaconsultareserva.Read())
                {
                    //ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);

                    ListViewItem listacasos = new ListViewItem(validareader("CasoId", "CasoId", respuestaconsultareserva).Text);// numero
                    listacasos.SubItems.Add(validareader("Referencia", "CasoId", respuestaconsultareserva).Text);  // refererencia
                    listacasos.SubItems.Add(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestaconsultareserva).Text); // expediente
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaLegal", "CasoId", respuestaconsultareserva).Text)); // fecha legal - fecha recepcion
                    listacasos.SubItems.Add(validareader("CasoNumConcedida", "CasoId", respuestaconsultareserva).Text); // registro
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaConcesion", "CasoId", respuestaconsultareserva).Text)); // fecha consecion
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaVigencia", "CasoId", respuestaconsultareserva).Text)); // fecha vigencia 
                    listacasos.SubItems.Add(validareader("TipoDeSolicitud", "CasoId", respuestaconsultareserva).Text);// tipo de solicitud
                    listacasos.SubItems.Add(validareader("Estatus", "CasoId", respuestaconsultareserva).Text);  // estatus
                    listacasos.SubItems.Add(validareader("CasoTitular", "CasoId", respuestaconsultareserva).Text);  // titular
                    listacasos.SubItems.Add(validareader("CasoTituloespanol", "CasoId", respuestaconsultareserva).Text);  // titulo
                    listacasos.SubItems.Add(""); // clase
                    listacasos.SubItems.Add(validareader("Contactos", "CasoId", respuestaconsultareserva).Text); // contacto
                    listacasos.SubItems.Add(validareader("Correos", "CasoId", respuestaconsultareserva).Text); // correo
                    listacasos.SubItems.Add(validareader("Responsable", "CasoId", respuestaconsultareserva).Text); //responsable
                    listacasos.SubItems.Add(validareader("Prioridades", "CasoId", respuestaconsultareserva).Text);  // prioridades
                    listacasos.SubItems.Add(validareader("Interesados", "CasoId", respuestaconsultareserva).Text); // interesados
                    listacasos.SubItems.Add("");  // anualidades a pagar
                    listacasos.SubItems.Add(""); // fecha prox pago
                    listacasos.SubItems.Add(validareader("Pais", "CasoId", respuestaconsultareserva).Text);  // pais 
                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listacasos.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listacasos.BackColor = Color.Azure;
                    }
                    listView4.Items.Add(listacasos);
                    listView4.FullRowSelect = true;
                    count++;
                }

                respuestaconsultareserva.Close();
                consultareserva.Cerrarconexion();

            }


            // termina la consulta de reserva

            // comienza la consulta de contensioso

            conect consultacontesioso = new conect();
            String kwerycontensiosoconsulta = "SELECT "+ 
                                                " caso_contencioso.CasoId, "+
                                                " DameLaReferencia(caso_contencioso.CasoId) AS Referencia, "+
                                                " caso_contencioso.CasoNumeroExpedienteLargo, "+
                                                " DATE_FORMAT(caso_contencioso.CasoFechaLegal,'%d-%m-%Y') AS CasoFechaLegal, "+
                                                " caso_contencioso.CasoNumConcedida, "+
                                                " DATE_FORMAT(caso_contencioso.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, "+
                                                " DATE_FORMAT(caso_contencioso.CasoFechaVigencia,'%d-%m-%Y') AS CasoFechaVigencia, "+
                                                " DameTipoSolicitudDescrip(caso_contencioso.TipoSolicitudId) AS Tipodesolicitud, "+
                                                " DameEstatusCasoDescrip(caso_contencioso.EstatusCasoId) AS Estatus, "+
                                                " caso_contencioso.CasoTitular, "+
                                                " caso_contencioso.CasoTituloespanol, "+
                                                " Dameelusuario(caso_contencioso.ResponsableId) AS Responsable, "+
                                                " Get_Prioridad(caso_contencioso.CasoId) AS Prioridades, "+
                                                " Get_Interesados(caso_contencioso.CasoId) AS Interesados, "+
                                                " Damealcontactocaso (casocliente.contactoid) AS Contactos, " +
                                                " Dameelcorreocontactocaso (casocliente.contactoid) AS Correos, " +
                                                " DameNombrePais(caso_contencioso.PaisId) AS Pais "+
                                            " FROM "+
                                                " caso_contencioso, "+
                                                " casocliente, "+ 
                                                " cliente "+
                                            " WHERE "+
                                                " cliente.ClienteId = " + datoid +
                                                  "  AND caso_contencioso.CasoId = casocliente.CasoId "+
                                                  "  AND casocliente.ClienteId = cliente.ClienteId";

            MySqlDataReader respuestaconsultacontensioso = consultacontesioso.getdatareader(kwerycontensiosoconsulta);

            if (respuestaconsultacontensioso != null)
            {
                int count = 0;
                while (respuestaconsultacontensioso.Read())
                {
                    //ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);

                    ListViewItem listacasos = new ListViewItem(validareader("CasoId", "CasoId", respuestaconsultacontensioso).Text);// numero
                    listacasos.SubItems.Add(validareader("Referencia", "CasoId", respuestaconsultacontensioso).Text);  // refererencia
                    listacasos.SubItems.Add(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestaconsultacontensioso).Text); // expediente
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaLegal", "CasoId", respuestaconsultacontensioso).Text)); // fecha legal - fecha recepcion
                    listacasos.SubItems.Add(validareader("CasoNumConcedida", "CasoId", respuestaconsultacontensioso).Text); // registro
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaConcesion", "CasoId", respuestaconsultacontensioso).Text)); // fecha consecion
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaVigencia", "CasoId", respuestaconsultacontensioso).Text)); // fecha vigencia 
                    listacasos.SubItems.Add(validareader("TipoDeSolicitud", "CasoId", respuestaconsultacontensioso).Text);// tipo de solicitud
                    listacasos.SubItems.Add(validareader("Estatus", "CasoId", respuestaconsultacontensioso).Text);  // estatus
                    listacasos.SubItems.Add(validareader("CasoTitular", "CasoId", respuestaconsultacontensioso).Text);  // titular
                    listacasos.SubItems.Add(validareader("CasoTituloespanol", "CasoId", respuestaconsultacontensioso).Text);  // titulo
                    listacasos.SubItems.Add(""); // clase
                    listacasos.SubItems.Add(validareader("Contactos", "CasoId", respuestaconsultacontensioso).Text); // contacto
                    listacasos.SubItems.Add(validareader("Correos", "CasoId", respuestaconsultacontensioso).Text); // correo
                    listacasos.SubItems.Add(validareader("Responsable", "CasoId", respuestaconsultacontensioso).Text); //responsable
                    listacasos.SubItems.Add(validareader("Prioridades", "CasoId", respuestaconsultacontensioso).Text);  // prioridades
                    listacasos.SubItems.Add(validareader("Interesados", "CasoId", respuestaconsultacontensioso).Text); // interesados
                    listacasos.SubItems.Add("");  // anualidades a pagar
                    listacasos.SubItems.Add(""); // fecha prox pago
                    listacasos.SubItems.Add(validareader("Pais", "CasoId", respuestaconsultacontensioso).Text);  // pais 
                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listacasos.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listacasos.BackColor = Color.Azure;
                    }
                    listView4.Items.Add(listacasos);
                    listView4.FullRowSelect = true;
                    count++;
                }

                respuestaconsultareserva.Close();
                consultareserva.Cerrarconexion();

            }


            // termina la consulta de contesioso

            // empieza la consulta de oposicion

            conect consultaoposicion = new conect();
            String kweryoposicion ="SELECT "+
                                    " caso_oposicion.CasoId, "+
                                    " DameLaReferencia(caso_oposicion.CasoId) AS Referencia, "+
                                    " caso_oposicion.CasoNumeroExpedienteLargo, "+
                                    " DameTipoSolicitudDescrip(caso_oposicion.TipoSolicitudId) AS Tipodesolicitud, "+
                                    " DameEstatusCasoDescrip(caso_oposicion.EstatusCasoId) AS Estatus, "+
                                    " caso_oposicion.CasoTitular, "+
                                    " caso_oposicion.CasoTituloespanol, "+
                                    " Dameelusuario(caso_oposicion.ResponsableId) AS Responsable, "+
                                    " Get_Prioridad(caso_oposicion.CasoId) AS Prioridades, "+
                                    " Get_Interesados(caso_oposicion.CasoId) AS Interesados, "+
                                    " Damealcontactocaso (casocliente.contactoid) AS Contactos, " +
                                    " Dameelcorreocontactocaso (casocliente.contactoid) AS Correos, " +
                                    " DameNombrePais(caso_oposicion.PaisId) AS Pais  "+
                                " FROM "+
                                    " caso_oposicion, "+
                                    " casocliente, "+ 
                                    " cliente "+
                                " WHERE "+
                                    " cliente.ClienteId = " + datoid +
                                     " AND caso_oposicion.CasoId = casocliente.CasoId "+
                                     " AND casocliente.ClienteId = cliente.ClienteId";

            MySqlDataReader respuestaconsultaoposicion = consultaoposicion.getdatareader(kweryoposicion);


            if (respuestaconsultaoposicion != null)
            {
                int count = 0;
                while (respuestaconsultaoposicion.Read())
                {
                    //ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);

                    ListViewItem listacasos = new ListViewItem(validareader("CasoId", "CasoId", respuestaconsultaoposicion).Text);// numero
                    listacasos.SubItems.Add(validareader("Referencia", "CasoId", respuestaconsultaoposicion).Text);  // refererencia
                    listacasos.SubItems.Add(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestaconsultaoposicion).Text); // expediente
                    listacasos.SubItems.Add(""); // fecha legal - fecha recepcion
                    listacasos.SubItems.Add(""); // registro
                    listacasos.SubItems.Add(""); // fecha consecion
                    listacasos.SubItems.Add(""); // fecha vigencia 
                    listacasos.SubItems.Add(validareader("TipoDeSolicitud", "CasoId", respuestaconsultaoposicion).Text);// tipo de solicitud
                    listacasos.SubItems.Add(validareader("Estatus", "CasoId", respuestaconsultaoposicion).Text);  // estatus
                    listacasos.SubItems.Add(validareader("CasoTitular", "CasoId", respuestaconsultaoposicion).Text);  // titular
                    listacasos.SubItems.Add(validareader("CasoTituloespanol", "CasoId", respuestaconsultaoposicion).Text);  // titulo
                    listacasos.SubItems.Add(""); // clase
                    listacasos.SubItems.Add(validareader("Contactos", "CasoId", respuestaconsultaoposicion).Text); // contacto
                    listacasos.SubItems.Add(validareader("Correos", "CasoId", respuestaconsultaoposicion).Text); // correo
                    listacasos.SubItems.Add(validareader("Responsable", "CasoId", respuestaconsultaoposicion).Text); //responsable
                    listacasos.SubItems.Add(validareader("Prioridades", "CasoId", respuestaconsultaoposicion).Text);  // prioridades
                    listacasos.SubItems.Add(validareader("Interesados", "CasoId", respuestaconsultaoposicion).Text); // interesados
                    listacasos.SubItems.Add("");  // anualidades a pagar
                    listacasos.SubItems.Add(""); // fecha prox pago
                    listacasos.SubItems.Add(validareader("Pais", "CasoId", respuestaconsultaoposicion).Text);  // pais 
                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listacasos.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listacasos.BackColor = Color.Azure;
                    }
                    listView4.Items.Add(listacasos);
                    listView4.FullRowSelect = true;
                    count++;
                }

                respuestaconsultaoposicion.Close();
                consultaoposicion.Cerrarconexion();

            }


            // comienza la consulta de oposicion 



            // termina quinta ocnsulta 

            // comienza consulta sexta pantalla

            // comienza consulta patentes

            conect  consultacorrespatente = new conect();
            String Kwerycorrespatente = "SELECT "+
                                            " caso_patente.CasoId, "+
                                            " caso_patente.CasoNumeroExpedienteLargo, "+
                                            " DATE_FORMAT(caso_patente.CasoFechaLegal,'%d-%m-%Y') AS CasoFechaLegal, "+
                                            " caso_patente.CasoNumConcedida, "+
                                            " DATE_FORMAT(caso_patente.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, "+
                                            " DameTipoSolicitudDescrip(caso_patente.TipoSolicitudId) AS Tipodesolicitud, "+
                                            " DameEstatusCasoDescrip(caso_patente.EstatusCasoId) AS Estatus, "+
                                            " caso_patente.CasoTitular, "+
                                            " caso_patente.CasoTituloespanol, "+
                                            " Damealcontactocaso (casocorresponsal.ContactoId) AS Contactos, "+
	                                        " Dameelcorreocontactocaso (casocorresponsal.ContactoId) AS Correos, "+ 
                                            " Dameelusuario(caso_patente.ResponsableId) AS Responsable, "+
	                                        " DameLaReferencia(caso_patente.CasoId) AS Referencia, "+
                                            " Get_Interesados(caso_patente.CasoId) AS Interesados, "+
                                            " Get_AnualidadFechaLimitePago(caso_patente.CasoId) AS Fechalimitepago, " +
                                            " Get_anualidadespagadas(caso_patente.CasoId) AS Anualidades, " +
                                            " Dameelquinquenio(caso_patente.CasoId) AS Quinquenio, " +
                                            " DameNombrePais(caso_patente.PaisId) AS Pais "+
                                        " FROM "+
                                            " caso_patente, "+
                                            " casocorresponsal, "+
                                            " cliente "+
                                        " WHERE "+
                                           " cliente.ClienteId = " + datoid +
                                                " AND caso_patente.CasoId = casocorresponsal.CasoId "+
                                                " AND casocorresponsal.ClienteId = cliente.ClienteId";

            MySqlDataReader respuestacorresponsalpatente = consultacorrespatente.getdatareader(Kwerycorrespatente);



            if (respuestacorresponsalpatente != null)
            {
                int count = 0;
                while (respuestacorresponsalpatente.Read())
                {
                    //ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);

                    ListViewItem listacasos = new ListViewItem(validareader("CasoId", "CasoId", respuestacorresponsalpatente).Text);// numero
                    listacasos.SubItems.Add(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestacorresponsalpatente).Text); // expediente
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaLegal", "CasoId", respuestacorresponsalpatente).Text)); // fecha legal - fecha recepcion
                    listacasos.SubItems.Add(validareader("CasoNumConcedida", "CasoId", respuestacorresponsalpatente).Text); // registro
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaConcesion", "CasoId", respuestacorresponsalpatente).Text)); // fecha consecion
                    listacasos.SubItems.Add(validareader("TipoDeSolicitud", "CasoId", respuestacorresponsalpatente).Text);// tipo de solicitud
                    listacasos.SubItems.Add(validareader("Estatus", "CasoId", respuestacorresponsalpatente).Text);  // estatus
                    listacasos.SubItems.Add(validareader("CasoTitular", "CasoId", respuestacorresponsalpatente).Text);  // titular
                    listacasos.SubItems.Add(validareader("CasoTituloespanol", "CasoId", respuestacorresponsalpatente).Text);  // titulo
                    listacasos.SubItems.Add(""); // clase
                    listacasos.SubItems.Add(validareader("Contactos", "CasoId", respuestacorresponsalpatente).Text); // contacto
                    listacasos.SubItems.Add(validareader("Correos", "CasoId", respuestacorresponsalpatente).Text); // correo
                    listacasos.SubItems.Add(validareader("Responsable", "CasoId", respuestacorresponsalpatente).Text); //responsable
                    listacasos.SubItems.Add(validareader("Referencia", "CasoId", respuestacorresponsalpatente).Text); //referencia
                    listacasos.SubItems.Add(validareader("Interesados", "CasoId", respuestacorresponsalpatente).Text); //interesado
                    listacasos.SubItems.Add(validareader("Anualidades", "CasoId", respuestacorresponsalpatente).Text); //anualidad

                    String proximoquinquenio = validareader("Quinquenio", "CasoId", respuestacorresponsalpatente).Text;

                    String[] separadas;
                    String quinquenio;
                    separadas = proximoquinquenio.Split('~');

                    //separadas [0]
                    //separadas [1]

                    if (separadas[1].Equals("0"))
                    {
                        quinquenio = "";
                    }
                    else
                    {
                        String valorquinquenio = separadas[0].Substring(0, 1);

                        quinquenio = valorquinquenio;
                    }

                    listacasos.SubItems.Add(quinquenio); //proximo quinquenio
                    listacasos.SubItems.Add(validafechavacia(validareader("Fechalimitepago", "CasoId", respuestacorresponsalpatente).Text)); // fecha prox pago
                    listacasos.SubItems.Add(validareader("Pais", "CasoId", respuestacorresponsalpatente).Text);  // pais 
                   
                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listacasos.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listacasos.BackColor = Color.Azure;
                    }
                    listView5.Items.Add(listacasos);
                    listView5.FullRowSelect = true;
                    count++;
                }

                respuestacorresponsalpatente.Close();
                consultacorrespatente.Cerrarconexion();
            
            }

            // termina la consulta de patentes 
            
            // comienza la consulta de marcas

            conect conectcorremarca = new conect();
            String kwerycorresponsalmarca = "SELECT  "+
                                                " caso_marcas.CasoId, "+
                                                " caso_marcas.CasoNumeroExpedienteLargo, "+
                                                " DATE_FORMAT(caso_marcas.CasoFechaLegal, '%d-%m-%Y') AS CasoFechaLegal, "+
                                                " caso_marcas.CasoNumConcedida, "+
                                                " DATE_FORMAT(caso_marcas.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, "+
                                                " DAMETIPOSOLICITUDDESCRIP(caso_marcas.TipoSolicitudId) AS Tipodesolicitud, "+
                                                " DAMEESTATUSCASODESCRIP(caso_marcas.EstatusCasoId) AS Estatus, "+
                                                " caso_marcas.CasoTitular, "+
                                                " caso_marcas.CasoTituloespanol, "+
                                                " DAMEALCONTACTOCASO(casocorresponsal.ContactoId) AS Contactos, "+
                                                " DAMEELCORREOCONTACTOCASO(casocorresponsal.ContactoId) AS Correos, "+
                                                " DAMEELUSUARIO(caso_marcas.ResponsableId) AS Responsable, "+
                                                " DAMELAREFERENCIA(caso_marcas.CasoId) AS Referencia, "+
                                                " GET_INTERESADOS(caso_marcas.CasoId) AS Interesados, "+
                                                " DAMENOMBREPAIS(caso_marcas.PaisId) AS Pais, "+
                                                " Damelaclase(caso_marcas.CasoId) AS Clase "+
                                            " FROM "+
                                                " caso_marcas, "+
                                                " casocorresponsal, "+
                                                " cliente "+
                                            " WHERE "+
                                                " cliente.ClienteId = " + datoid +
                                                  "   AND caso_marcas.CasoId = casocorresponsal.CasoId "+
                                                   "  AND casocorresponsal.ClienteId = cliente.ClienteId";

            MySqlDataReader respuestacorresponsalmarca = conectcorremarca.getdatareader(kwerycorresponsalmarca);


            if (respuestacorresponsalmarca != null)
            {
                int count = 0;
                while (respuestacorresponsalmarca.Read())
                {
                    //ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);

                    ListViewItem listacasos = new ListViewItem(validareader("CasoId", "CasoId", respuestacorresponsalmarca).Text);// numero
                    listacasos.SubItems.Add(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestacorresponsalmarca).Text); // expediente
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaLegal", "CasoId", respuestacorresponsalmarca).Text)); // fecha legal - fecha recepcion
                    listacasos.SubItems.Add(validareader("CasoNumConcedida", "CasoId", respuestacorresponsalmarca).Text); // registro
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaConcesion", "CasoId", respuestacorresponsalmarca).Text)); // fecha consecion
                    listacasos.SubItems.Add(validareader("TipoDeSolicitud", "CasoId", respuestacorresponsalmarca).Text);// tipo de solicitud
                    listacasos.SubItems.Add(validareader("Estatus", "CasoId", respuestacorresponsalmarca).Text);  // estatus
                    listacasos.SubItems.Add(validareader("CasoTitular", "CasoId", respuestacorresponsalmarca).Text);  // titular
                    listacasos.SubItems.Add(validareader("CasoTituloespanol", "CasoId", respuestacorresponsalmarca).Text);  // titulo
                    listacasos.SubItems.Add(validareader("Clase", "CasoId", respuestacorresponsalmarca).Text); // clase
                    listacasos.SubItems.Add(validareader("Contactos", "CasoId", respuestacorresponsalmarca).Text); // contacto
                    listacasos.SubItems.Add(validareader("Correos", "CasoId", respuestacorresponsalmarca).Text); // correo
                    listacasos.SubItems.Add(validareader("Responsable", "CasoId", respuestacorresponsalmarca).Text); //responsable
                    listacasos.SubItems.Add(validareader("Referencia", "CasoId", respuestacorresponsalmarca).Text); //referencia
                    listacasos.SubItems.Add(validareader("Interesados", "CasoId", respuestacorresponsalmarca).Text); //interesado
                    listacasos.SubItems.Add(""); //anualidad
                    listacasos.SubItems.Add(""); //proximo quinquenio
                    listacasos.SubItems.Add(""); // fecha prox pago
                    listacasos.SubItems.Add(validareader("Pais", "CasoId", respuestacorresponsalmarca).Text);  // pais 

                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listacasos.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listacasos.BackColor = Color.Azure;
                    }
                    listView5.Items.Add(listacasos);
                    listView5.FullRowSelect = true;
                    count++;
                }

                respuestacorresponsalmarca.Close();
                conectcorremarca.Cerrarconexion();

            }

            // termina consulta de marcas

            // empieza consulta registro de obra


            conect conectcorreregistro = new conect();
            String kwerycorresregistro = "SELECT "+
                                            " caso_registrodeobra.CasoId, "+
                                            " caso_registrodeobra.CasoNumeroExpedienteLargo, "+
                                            " DATE_FORMAT(caso_registrodeobra.CasoFechaLegal, '%d-%m-%Y') AS CasoFechaLegal, "+
                                            " caso_registrodeobra.CasoNumConcedida, "+
                                            " DATE_FORMAT(caso_registrodeobra.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, "+
                                            " DAMETIPOSOLICITUDDESCRIP(caso_registrodeobra.TipoSolicitudId) AS Tipodesolicitud, "+
                                            " DAMEESTATUSCASODESCRIP(caso_registrodeobra.EstatusCasoId) AS Estatus, "+
                                            " caso_registrodeobra.CasoTitular, "+
                                            " caso_registrodeobra.CasoTituloespanol, "+
                                            " DAMEALCONTACTOCASO(casocorresponsal.ContactoId) AS Contactos, "+
                                            " DAMEELCORREOCONTACTOCASO(casocorresponsal.ContactoId) AS Correos, "+
                                            " DAMEELUSUARIO(caso_registrodeobra.ResponsableId) AS Responsable, "+
                                            " DAMELAREFERENCIA(caso_registrodeobra.CasoId) AS Referencia, "+
                                            " GET_INTERESADOS(caso_registrodeobra.CasoId) AS Interesados, "+
                                            " GET_ANUALIDADFECHALIMITEPAGO(caso_registrodeobra.CasoId) AS Fechalimitepago, "+
                                            " DAMENOMBREPAIS(caso_registrodeobra.PaisId) AS Pais "+
                                        " FROM "+
                                           "  caso_registrodeobra, "+
                                           "  casocorresponsal, "+
                                           "  cliente "+
                                       "  WHERE "+
                                           " cliente.ClienteId = " + datoid +
                                              "  AND caso_registrodeobra.CasoId = casocorresponsal.CasoId "+
                                              "  AND casocorresponsal.ClienteId = cliente.ClienteId ";


            MySqlDataReader respuestacorresponsalregistro = conectcorreregistro.getdatareader(kwerycorresregistro);



            if (respuestacorresponsalregistro != null)
            {
                int count = 0;
                while (respuestacorresponsalregistro.Read())
                {
                    //ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);

                    ListViewItem listacasos = new ListViewItem(validareader("CasoId", "CasoId", respuestacorresponsalregistro).Text);// numero
                    listacasos.SubItems.Add(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestacorresponsalregistro).Text); // expediente
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaLegal", "CasoId", respuestacorresponsalregistro).Text)); // fecha legal - fecha recepcion
                    listacasos.SubItems.Add(validareader("CasoNumConcedida", "CasoId", respuestacorresponsalregistro).Text); // registro
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaConcesion", "CasoId", respuestacorresponsalregistro).Text)); // fecha consecion
                    listacasos.SubItems.Add(validareader("TipoDeSolicitud", "CasoId", respuestacorresponsalregistro).Text);// tipo de solicitud
                    listacasos.SubItems.Add(validareader("Estatus", "CasoId", respuestacorresponsalregistro).Text);  // estatus
                    listacasos.SubItems.Add(validareader("CasoTitular", "CasoId", respuestacorresponsalregistro).Text);  // titular
                    listacasos.SubItems.Add(validareader("CasoTituloespanol", "CasoId", respuestacorresponsalregistro).Text);  // titulo
                    listacasos.SubItems.Add(""); // clase
                    listacasos.SubItems.Add(validareader("Contactos", "CasoId", respuestacorresponsalregistro).Text); // contacto
                    listacasos.SubItems.Add(validareader("Correos", "CasoId", respuestacorresponsalregistro).Text); // correo
                    listacasos.SubItems.Add(validareader("Responsable", "CasoId", respuestacorresponsalregistro).Text); //responsable
                    listacasos.SubItems.Add(validareader("Referencia", "CasoId", respuestacorresponsalregistro).Text); //referencia
                    listacasos.SubItems.Add(validareader("Interesados", "CasoId", respuestacorresponsalregistro).Text); //interesado
                    listacasos.SubItems.Add(""); //anualidad
                    listacasos.SubItems.Add(""); //proximo quinquenio
                    listacasos.SubItems.Add(validafechavacia(validareader("Fechalimitepago", "CasoId", respuestacorresponsalregistro).Text)); // fecha prox pago
                    listacasos.SubItems.Add(validareader("Pais", "CasoId", respuestacorresponsalregistro).Text);  // pais 

                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listacasos.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listacasos.BackColor = Color.Azure;
                    }
                    listView5.Items.Add(listacasos);
                    listView5.FullRowSelect = true;
                    count++;
                }

                respuestacorresponsalregistro.Close();
                conectcorreregistro.Cerrarconexion();

            }
           // terminar consulta corresponsal registro

            // comienza consulta conrresponsa reserva 

            conect coneccorresreserva = new conect();

            String kwerycorerreserva = "SELECT "+
                                        " caso_reservadederechos.CasoId, "+
                                        " caso_reservadederechos.CasoNumeroExpedienteLargo, "+
                                        " DATE_FORMAT(caso_reservadederechos.CasoFechaLegal, '%d-%m-%Y') AS CasoFechaLegal, "+
                                        " caso_reservadederechos.CasoNumConcedida, "+
                                        " DATE_FORMAT(caso_reservadederechos.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, "+
                                        " DAMETIPOSOLICITUDDESCRIP(caso_reservadederechos.TipoSolicitudId) AS Tipodesolicitud, "+
                                        " DAMEESTATUSCASODESCRIP(caso_reservadederechos.EstatusCasoId) AS Estatus, "+
                                        " caso_reservadederechos.CasoTitular, "+
                                        " caso_reservadederechos.CasoTituloespanol, "+
                                        " DAMEALCONTACTOCASO(casocorresponsal.ContactoId) AS Contactos, "+
                                        " DAMEELCORREOCONTACTOCASO(casocorresponsal.ContactoId) AS Correos, "+
                                        " DAMEELUSUARIO(caso_reservadederechos.ResponsableId) AS Responsable, "+
                                        " DAMELAREFERENCIA(caso_reservadederechos.CasoId) AS Referencia, "+
                                        " GET_INTERESADOS(caso_reservadederechos.CasoId) AS Interesados, "+
                                        " GET_ANUALIDADFECHALIMITEPAGO(caso_reservadederechos.CasoId) AS Fechalimitepago, "+
                                        " DAMENOMBREPAIS(caso_reservadederechos.PaisId) AS Pais "+
                                    " FROM "+
                                       " caso_reservadederechos, "+
                                       " casocorresponsal, "+
                                       "  cliente "+
                                    " WHERE "+
                                       " cliente.ClienteId = " + datoid +
                                       "    AND caso_reservadederechos.CasoId = casocorresponsal.CasoId "+
                                       "    AND casocorresponsal.ClienteId = cliente.ClienteId ";


            MySqlDataReader respuestacorresponsalreserva = coneccorresreserva.getdatareader(kwerycorerreserva);

            if (respuestacorresponsalreserva != null)
            {
                int count = 0;
                while (respuestacorresponsalreserva.Read())
                {
                    //ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);

                    ListViewItem listacasos = new ListViewItem(validareader("CasoId", "CasoId", respuestacorresponsalreserva).Text);// numero
                    listacasos.SubItems.Add(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestacorresponsalreserva).Text); // expediente
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaLegal", "CasoId", respuestacorresponsalreserva).Text)); // fecha legal - fecha recepcion
                    listacasos.SubItems.Add(validareader("CasoNumConcedida", "CasoId", respuestacorresponsalreserva).Text); // registro
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaConcesion", "CasoId", respuestacorresponsalreserva).Text)); // fecha consecion
                    listacasos.SubItems.Add(validareader("TipoDeSolicitud", "CasoId", respuestacorresponsalreserva).Text);// tipo de solicitud
                    listacasos.SubItems.Add(validareader("Estatus", "CasoId", respuestacorresponsalreserva).Text);  // estatus
                    listacasos.SubItems.Add(validareader("CasoTitular", "CasoId", respuestacorresponsalreserva).Text);  // titular
                    listacasos.SubItems.Add(validareader("CasoTituloespanol", "CasoId", respuestacorresponsalreserva).Text);  // titulo
                    listacasos.SubItems.Add(""); // clase
                    listacasos.SubItems.Add(validareader("Contactos", "CasoId", respuestacorresponsalreserva).Text); // contacto
                    listacasos.SubItems.Add(validareader("Correos", "CasoId", respuestacorresponsalreserva).Text); // correo
                    listacasos.SubItems.Add(validareader("Responsable", "CasoId", respuestacorresponsalreserva).Text); //responsable
                    listacasos.SubItems.Add(validareader("Referencia", "CasoId", respuestacorresponsalreserva).Text); //referencia
                    listacasos.SubItems.Add(validareader("Interesados", "CasoId", respuestacorresponsalreserva).Text); //interesado
                    listacasos.SubItems.Add(""); //anualidad
                    listacasos.SubItems.Add(""); //proximo quinquenio
                    listacasos.SubItems.Add(validafechavacia(validareader("Fechalimitepago", "CasoId", respuestacorresponsalreserva).Text)); // fecha prox pago
                    listacasos.SubItems.Add(validareader("Pais", "CasoId", respuestacorresponsalreserva).Text);  // pais 

                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listacasos.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listacasos.BackColor = Color.Azure;
                    }
                    listView5.Items.Add(listacasos);
                    listView5.FullRowSelect = true;
                    count++;
                }

                respuestacorresponsalreserva.Close();
                coneccorresreserva.Cerrarconexion();

            }


            // termina la consulta de reserva

            // comienza consulta caso oposicion 

            conect coneccorresconte = new conect();
            String kwerycontencioso = " SELECT "+
                                        " caso_contencioso.CasoId, "+
                                        " caso_contencioso.CasoNumeroExpedienteLargo, "+
                                        " DATE_FORMAT(caso_contencioso.CasoFechaLegal, '%d-%m-%Y') AS CasoFechaLegal, "+
                                        " caso_contencioso.CasoNumConcedida, "+
                                        " DATE_FORMAT(caso_contencioso.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, "+
                                        " DAMETIPOSOLICITUDDESCRIP(caso_contencioso.TipoSolicitudId) AS Tipodesolicitud, "+
                                        " DAMEESTATUSCASODESCRIP(caso_contencioso.EstatusCasoId) AS Estatus, "+
                                        " caso_contencioso.CasoTitular, "+
                                        " caso_contencioso.CasoTituloespanol, "+
                                        " DAMEALCONTACTOCASO(casocorresponsal.ContactoId) AS Contactos, "+
                                        " DAMEELCORREOCONTACTOCASO(casocorresponsal.ContactoId) AS Correos, "+
                                        " DAMEELUSUARIO(caso_contencioso.ResponsableId) AS Responsable, "+
                                        " DAMELAREFERENCIA(caso_contencioso.CasoId) AS Referencia, "+
                                        " GET_INTERESADOS(caso_contencioso.CasoId) AS Interesados, "+
                                        " GET_ANUALIDADFECHALIMITEPAGO(caso_contencioso.CasoId) AS Fechalimitepago, "+
                                        " DAMENOMBREPAIS(caso_contencioso.PaisId) AS Pais "+
                                    " FROM "+
                                       " caso_contencioso, "+
                                       " casocorresponsal, "+
                                       " cliente "+
                                    " WHERE "+
                                        " cliente.ClienteId = " + datoid +
                                          "  AND caso_contencioso.CasoId = casocorresponsal.CasoId "+
                                          "  AND casocorresponsal.ClienteId = cliente.ClienteId";


            MySqlDataReader respuestacorresponsalcontencioso = coneccorresconte.getdatareader(kwerycontencioso);

            if (respuestacorresponsalcontencioso != null)
            {
                int count = 0;
                while (respuestacorresponsalcontencioso.Read())
                {
                    //ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);

                    ListViewItem listacasos = new ListViewItem(validareader("CasoId", "CasoId", respuestacorresponsalcontencioso).Text);// numero
                    listacasos.SubItems.Add(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestacorresponsalcontencioso).Text); // expediente
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaLegal", "CasoId", respuestacorresponsalcontencioso).Text)); // fecha legal - fecha recepcion
                    listacasos.SubItems.Add(validareader("CasoNumConcedida", "CasoId", respuestacorresponsalcontencioso).Text); // registro
                    listacasos.SubItems.Add(validafechavacia(validareader("CasoFechaConcesion", "CasoId", respuestacorresponsalcontencioso).Text)); // fecha consecion
                    listacasos.SubItems.Add(validareader("TipoDeSolicitud", "CasoId", respuestacorresponsalcontencioso).Text);// tipo de solicitud
                    listacasos.SubItems.Add(validareader("Estatus", "CasoId", respuestacorresponsalcontencioso).Text);  // estatus
                    listacasos.SubItems.Add(validareader("CasoTitular", "CasoId", respuestacorresponsalcontencioso).Text);  // titular
                    listacasos.SubItems.Add(validareader("CasoTituloespanol", "CasoId", respuestacorresponsalcontencioso).Text);  // titulo
                    listacasos.SubItems.Add(""); // clase
                    listacasos.SubItems.Add(validareader("Contactos", "CasoId", respuestacorresponsalcontencioso).Text); // contacto
                    listacasos.SubItems.Add(validareader("Correos", "CasoId", respuestacorresponsalcontencioso).Text); // correo
                    listacasos.SubItems.Add(validareader("Responsable", "CasoId", respuestacorresponsalcontencioso).Text); //responsable
                    listacasos.SubItems.Add(validareader("Referencia", "CasoId", respuestacorresponsalcontencioso).Text); //referencia
                    listacasos.SubItems.Add(validareader("Interesados", "CasoId", respuestacorresponsalcontencioso).Text); //interesado
                    listacasos.SubItems.Add(""); //anualidad
                    listacasos.SubItems.Add(""); //proximo quinquenio
                    listacasos.SubItems.Add(validafechavacia(validareader("Fechalimitepago", "CasoId", respuestacorresponsalcontencioso).Text)); // fecha prox pago
                    listacasos.SubItems.Add(validareader("Pais", "CasoId", respuestacorresponsalcontencioso).Text);  // pais 

                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listacasos.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listacasos.BackColor = Color.Azure;
                    }
                    listView5.Items.Add(listacasos);
                    listView5.FullRowSelect = true;
                    count++;
                }

                respuestacorresponsalcontencioso.Close();
                coneccorresconte.Cerrarconexion();

            }

            // termina la consulta de contencioso 

            // comienza la consulta de oposicion 

            conect coneccorreopos = new conect();
            String kwerycorreopos = "SELECT "+
                                        " caso_oposicion.CasoId, "+
                                        " caso_oposicion.CasoNumeroExpedienteLargo, "+
                                        " DAMETIPOSOLICITUDDESCRIP(caso_oposicion.TipoSolicitudId) AS Tipodesolicitud, "+
                                        " DAMEESTATUSCASODESCRIP(caso_oposicion.EstatusCasoId) AS Estatus, "+
                                        " caso_oposicion.CasoTitular, "+
                                        " caso_oposicion.CasoTituloespanol, "+
                                        " DAMEALCONTACTOCASO(casocorresponsal.ContactoId) AS Contactos, "+
                                        " DAMEELCORREOCONTACTOCASO(casocorresponsal.ContactoId) AS Correos, "+
                                        " DAMEELUSUARIO(caso_oposicion.ResponsableId) AS Responsable, "+
                                        " DAMELAREFERENCIA(caso_oposicion.CasoId) AS Referencia, "+
                                        " GET_INTERESADOS(caso_oposicion.CasoId) AS Interesados, "+
                                        " GET_ANUALIDADFECHALIMITEPAGO(caso_oposicion.CasoId) AS Fechalimitepago, "+
                                        " DAMENOMBREPAIS(caso_oposicion.PaisId) AS Pais "+
                                    " FROM "+
                                       "  caso_oposicion, "+
                                       "  casocorresponsal, "+
                                       "  cliente "+
                                    " WHERE "+
                                         " cliente.ClienteId = " + datoid +
                                          "  AND caso_oposicion.CasoId = casocorresponsal.CasoId "+
                                          "  AND casocorresponsal.ClienteId = cliente.ClienteId";


            MySqlDataReader respuestacorresponsaloposicion = coneccorreopos.getdatareader(kwerycorreopos);

            if (respuestacorresponsaloposicion != null)
            {
                int count = 0;
                while (respuestacorresponsaloposicion.Read())
                {
                    //ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);

                    ListViewItem listacasos = new ListViewItem(validareader("CasoId", "CasoId", respuestacorresponsaloposicion).Text);// numero
                    listacasos.SubItems.Add(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestacorresponsaloposicion).Text); // expediente
                    listacasos.SubItems.Add(""); // fecha legal - fecha recepcion
                    listacasos.SubItems.Add(""); // registro
                    listacasos.SubItems.Add(""); // fecha consecion
                    listacasos.SubItems.Add(validareader("TipoDeSolicitud", "CasoId", respuestacorresponsaloposicion).Text);// tipo de solicitud
                    listacasos.SubItems.Add(validareader("Estatus", "CasoId", respuestacorresponsaloposicion).Text);  // estatus
                    listacasos.SubItems.Add(validareader("CasoTitular", "CasoId", respuestacorresponsaloposicion).Text);  // titular
                    listacasos.SubItems.Add(validareader("CasoTituloespanol", "CasoId", respuestacorresponsaloposicion).Text);  // titulo
                    listacasos.SubItems.Add(""); // clase
                    listacasos.SubItems.Add(validareader("Contactos", "CasoId", respuestacorresponsaloposicion).Text); // contacto
                    listacasos.SubItems.Add(validareader("Correos", "CasoId", respuestacorresponsaloposicion).Text); // correo
                    listacasos.SubItems.Add(validareader("Responsable", "CasoId", respuestacorresponsaloposicion).Text); //responsable
                    listacasos.SubItems.Add(validareader("Referencia", "CasoId", respuestacorresponsaloposicion).Text); //referencia
                    listacasos.SubItems.Add(validareader("Interesados", "CasoId", respuestacorresponsaloposicion).Text); //interesado
                    listacasos.SubItems.Add(""); //anualidad
                    listacasos.SubItems.Add(""); //proximo quinquenio
                    listacasos.SubItems.Add(validafechavacia(validareader("Fechalimitepago", "CasoId", respuestacorresponsaloposicion).Text)); // fecha prox pago
                    listacasos.SubItems.Add(validareader("Pais", "CasoId", respuestacorresponsaloposicion).Text);  // pais 

                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listacasos.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listacasos.BackColor = Color.Azure;
                    }
                    listView5.Items.Add(listacasos);
                    listView5.FullRowSelect = true;
                    count++;
                }

                respuestacorresponsaloposicion.Close();
                coneccorreopos.Cerrarconexion();

            }



//////////////////////  continua 7tma pantalla 

            conect contecusuarioobservaciones = new conect();
            String kweryusuarioobservaciones = " SELECT usuario.UsuarioId , usuario.UsuarioName FROM usuario ";
            MySqlDataReader respuestaobservaciousuario = contecusuarioobservaciones.getdatareader(kweryusuarioobservaciones);
            while (respuestaobservaciousuario.Read())
            {
                CB_observacionesusuario_cd.Items.Add(validareader("UsuarioName", "UsuarioId", respuestaobservaciousuario));
            }
            respuestaobservaciousuario.Close();
            contecusuarioobservaciones.Cerrarconexion();

            DTP_fecharegistro_cd.CustomFormat = "yyyy-MM-dd";
            DTP_fecharegistro_cd.Format = DateTimePickerFormat.Custom;



            conect conecobserva = new conect();
            String kweryobser = "SELECT " +
                                " observacion.ObservacionId, " +
                                " observacion.ObservacionTexto, " +
                                " DAMEELUSUARIO(observacion.UsuarioId) AS Usuario, " +
                                " DATE_FORMAT(observacion.ObservacionFecha, '%d-%m-%Y') AS ObservacionFecha " +
                            " FROM " +
                               " observacion " +
                               " WHERE observacion.ClienteId =  " + datoid + ";";
            MySqlDataReader respuestaconsultaobservacion = conecobserva.getdatareader(kweryobser);

                        if (respuestaconsultaobservacion == null)
                        {
                            MessageBox.Show("Fallo la consulta de la instrucción");
                        }
                        else
                        {
                            int count = 0;
                            while (respuestaconsultaobservacion.Read())
                            {
                                // ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);

                                ListViewItem listaagregar3 = new ListViewItem(validareader("ObservacionId", "ObservacionId", respuestaconsultaobservacion).Text);
                                listaagregar3.SubItems.Add(validareader("ObservacionTexto", "ObservacionId", respuestaconsultaobservacion).Text);
                                listaagregar3.SubItems.Add(validareader("Usuario", "ObservacionId", respuestaconsultaobservacion).Text);
                                listaagregar3.SubItems.Add(validareader("ObservacionFecha", "ObservacionId", respuestaconsultaobservacion).Text);
                                int residuo = count % 2;
                                if (residuo == 0)
                                {
                                    listaagregar3.BackColor = Color.LightGray;
                                }
                                else
                                {
                                    listaagregar3.BackColor = Color.Azure;
                                }

                                listView6.Items.Add(listaagregar3);
                                listView6.FullRowSelect = true;
                                count++;
                            }

                            respuestaconsultaobservacion.Close();
                            conecobserva.Cerrarconexion();


                        }


            /*
            conect casocliente = new conect ();
            String consultacasocliente = "SELECT * FROM casocliente ";
            MySqlDataReader respuestacoacliente = casocliente.getdatareader(consultacasocliente);
            if (respuestacoacliente == null)
            {
                MessageBox.Show("fallo");
            }else{
                while (respuestacoacliente.Read())
                {
                    String casoid = validareader("CasoId", "CasoClienteId", respuestacoacliente).Text;
                    String casoclienteid = validareader("CasoClienteId", "CasoClienteId", respuestacoacliente).Text;
                    String tipodesolicitudid = "";
                    conect consultacaso = new conect ();
                    String kweryconsultacaso = "SELECT * FROM caso WHERE CasoId = "+casoid+";";
                    MySqlDataReader respuestacaso = consultacaso.getdatareader(kweryconsultacaso);


                        while (respuestacaso.Read())
                        {
                             tipodesolicitudid = validareader("TipoSolicitudId", "CasoId", respuestacaso).Text;
                        }
                        respuestacaso.Close();
                        consultacaso.Cerrarconexion();

                        conect conectupdate = new conect();
                        String UPDATECASOCLIENTE = "UPDATE `casocliente` SET " +
                                                    " `TipoSolicitudId` = " + tipodesolicitudid +
                                                   " WHERE `CasoClienteId` = " + casoclienteid + ";";

                        MySqlDataReader respuestaupdate = conectupdate.getdatareader(UPDATECASOCLIENTE);

                            if(respuestaupdate == null){
                                MessageBox.Show("fallo");
                            }else
                            {
                              // MessageBox.Show("Cambio hecho");
                                respuestaupdate.Close();
                                conectupdate.Cerrarconexion();
                            }
                    


                     
                }
                
                respuestacoacliente.Close();
                casocliente.Cerrarconexion();
            }
             este comentario es para ponerle al casocliente el tipo de solicitud. a la tabla tipo de solicitud
            */ 

            // aqui acaba la inicializacion de los componentes

            
            // aqui comienza la consuta 8 




            // aqui termina la consulta 8

            // aqui comienza la consulta de la pantalla 9

            conect conectresulmen = new conect();
            String kweryresumen = "SELECT casocliente.CasoClienteId , casocliente.CasoId, DameTipoSolicitudDescrip(casocliente.TipoSolicitudId) AS TipoSoliciutd FROM casocliente WHERE casocliente.ClienteId = " + datoid + ";";
            MySqlDataReader respuestaresumen = conectresulmen.getdatareader(kweryresumen);

            if (respuestaresumen == null)
            {
                MessageBox.Show("Fallo la consulta de los resumenes de caso");
            }
            else
            {
                int count = 0;

                while (respuestaresumen.Read())
                {
                    ListViewItem listaresumencasos = new ListViewItem(validareader("CasoId", "CasoClienteId", respuestaresumen).Text);
                    listaresumencasos.SubItems.Add(validareader("TipoSoliciutd", "CasoClienteId", respuestaresumen).Text);


                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listaresumencasos.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listaresumencasos.BackColor = Color.Azure;
                    }
                    listView8.Items.Add(listaresumencasos);
                    listView8.FullRowSelect = true;
                    count++;
                }


                respuestaresumen.Close();
                conectresulmen.Cerrarconexion();
            }

            // aqui termina la consulta de la pantalla 9


            conect conectipodocu = new conect();
            String kwerytipodoc = "SELECT tipodocumento.TipoDocumentoId,  tipodocumento.TipoDocumentoDescrip FROM tipodocumento";
            MySqlDataReader respuestastringtipodoc = conectipodocu.getdatareader(kwerytipodoc);
            while (respuestastringtipodoc.Read())
            {
                CB_tipodocumento_documento_cd.Items.Add(validareader("TipoDocumentoDescrip", "TipoDocumentoId", respuestastringtipodoc));
            }
            respuestastringtipodoc.Close();
            conectipodocu.Cerrarconexion();

            conect contecusuariodoc = new conect();
            String kweryusuariodoc = "SELECT usuario.UsuarioId,  usuario.UsuarioName FROM usuario";
            MySqlDataReader respuestastringusuariodoc = contecusuariodoc.getdatareader(kweryusuariodoc);
            while (respuestastringusuariodoc.Read())
            {
                CB_usuariosdocumento_cd.Items.Add(validareader("UsuarioName", "UsuarioId", respuestastringusuariodoc));
            }
            respuestastringusuariodoc.Close();
            contecusuariodoc.Cerrarconexion();

            if (estatuspais == "2")
            {
                MessageBox.Show("Este cliente no tiene asignado un pais, favor de asignar(Datos Generales)");
            }

        }




        public void actualizatablainstuccion()
        {
            listView3.Items.Clear(); 
            conect conectinstruccion = new conect();
            String kweryconsultainstruccion = "SELECT " +
                                                "instruccion.InstruccionId, " +
                                                "DAMETIPOINSTRUCCION(instruccion.TipoInstruccionId) AS Tipoinstruccion, " +
                                                "instruccion.InstrucciondDescip, " +
                                                "DAMEELUSUARIO(instruccion.UsuarioId) AS Usuario, " +
                                                "DATE_FORMAT(instruccion.InstruccionFecha,'%d-%m-%Y') AS InstruccionFecha, " +
                                                "DATE_FORMAT(instruccion.InstruccionFechaRec,'%d-%m-%Y') AS InstruccionFechaRec, " +
                                                "instruccion.InstruccionIndAct " +
                                                "FROM instruccion WHERE ClienteId =" + datoid + ";";
            MySqlDataReader respuestaconsultainstruccion = conectinstruccion.getdatareader(kweryconsultainstruccion);

            if (respuestaconsultainstruccion == null)
            {
                MessageBox.Show("Fallo la consulta de la instrucción");
            }
            else
            {
                int count = 0;
                while (respuestaconsultainstruccion.Read())
                {
                    // ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);

                    ListViewItem listaagregar3 = new ListViewItem(validareader("InstruccionId", "InstruccionId", respuestaconsultainstruccion).Text);
                    listaagregar3.SubItems.Add(validareader("Tipoinstruccion", "InstruccionId", respuestaconsultainstruccion).Text);
                    listaagregar3.SubItems.Add(validareader("InstrucciondDescip", "InstruccionId", respuestaconsultainstruccion).Text);
                    listaagregar3.SubItems.Add(validareader("Usuario", "InstruccionId", respuestaconsultainstruccion).Text);
                    listaagregar3.SubItems.Add(validareader("InstruccionFecha", "InstruccionId", respuestaconsultainstruccion).Text);
                    listaagregar3.SubItems.Add(validareader("InstruccionFechaRec", "InstruccionId", respuestaconsultainstruccion).Text);
                    if (validareader("InstruccionIndAct", "InstruccionId", respuestaconsultainstruccion).Text == "1")
                    {
                        listaagregar3.SubItems.Add("Habilitado");
                    }
                    else
                    {
                        listaagregar3.SubItems.Add("Inhabilitado");
                    }

                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listaagregar3.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listaagregar3.BackColor = Color.Azure;
                    }

                    listView3.Items.Add(listaagregar3);
                    listView3.FullRowSelect = true;
                    count++;
                }

                respuestaconsultainstruccion.Close();
                conectinstruccion.Cerrarconexion();

            }


            DTP_fechainstruccion_instruciones_cd.CustomFormat = "yyyy-MM-dd";
            DTP_fechainstruccion_instruciones_cd.Format = DateTimePickerFormat.Custom;

            DTP_fecharegistro.CustomFormat = "yyyy-MM-dd";
            DTP_fecharegistro.Format = DateTimePickerFormat.Custom;

           
        }


        public void actualizatablaobservaciones()
        {
            listView6.Items.Clear();
            conect contecusuarioobservaciones = new conect();
            String kweryusuarioobservaciones = " SELECT usuario.UsuarioId , usuario.UsuarioName FROM usuario ";
            MySqlDataReader respuestaobservaciousuario = contecusuarioobservaciones.getdatareader(kweryusuarioobservaciones);
            while (respuestaobservaciousuario.Read())
            {
                CB_observacionesusuario_cd.Items.Add(validareader("UsuarioName", "UsuarioId", respuestaobservaciousuario));
            }
            respuestaobservaciousuario.Close();
            contecusuarioobservaciones.Cerrarconexion();

            DTP_fecharegistro_cd.CustomFormat = "yyyy-MM-dd";
            DTP_fecharegistro_cd.Format = DateTimePickerFormat.Custom;



            conect conecobserva = new conect();
            String kweryobser = "SELECT " +
                                " observacion.ObservacionId, " +
                                " observacion.ObservacionTexto, " +
                                " DAMEELUSUARIO(observacion.UsuarioId) AS Usuario, " +
                                " DATE_FORMAT(observacion.ObservacionFecha, '%d-%m-%Y') AS ObservacionFecha " +
                            " FROM " +
                               " observacion " +
                               " WHERE observacion.ClienteId =  " + datoid + ";";
            MySqlDataReader respuestaconsultaobservacion = conecobserva.getdatareader(kweryobser);

            if (respuestaconsultaobservacion == null)
            {
                MessageBox.Show("Fallo la consulta de la instrucción");
            }
            else
            {
                int count = 0;
                while (respuestaconsultaobservacion.Read())
                {
                    // ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);

                    ListViewItem listaagregar3 = new ListViewItem(validareader("ObservacionId", "ObservacionId", respuestaconsultaobservacion).Text);
                    listaagregar3.SubItems.Add(validareader("ObservacionTexto", "ObservacionId", respuestaconsultaobservacion).Text);
                    listaagregar3.SubItems.Add(validareader("Usuario", "ObservacionId", respuestaconsultaobservacion).Text);
                    listaagregar3.SubItems.Add(validareader("ObservacionFecha", "ObservacionId", respuestaconsultaobservacion).Text);
                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listaagregar3.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listaagregar3.BackColor = Color.Azure;
                    }

                    listView6.Items.Add(listaagregar3);
                    listView6.FullRowSelect = true;
                    count++;
                }

                respuestaconsultaobservacion.Close();
                conecobserva.Cerrarconexion();


            }

        }

        public void limpiarcasillasobservaciones()
        {
            CB_observacionesusuario_cd.Text = "Seleccione";
            DTP_fecharegistro_cd.Value = DateTime.Today;
            RTB_Observaciones_cd.Text = "";
            BT_Agregaobservacion_cd.Enabled = true;
            IdObservacion = null;
        }

        public void limpiatablainstruccion()
        {
            // dateTimePicker1.MaxDate = DateTime.Today;
            CB_instruccionestipo_cd.Text = "Seleccione";
            CB_usuarioinstrucciones_cd.Text = "Seleccione";
            DTP_fechainstruccion_instruciones_cd.Value = DateTime.Today;
            DTP_fecharegistro.Value = DateTime.Today;
            RTB_instrucioninst_cd.Text = "";
            idInstruccion = null;
            // DateTimePiker.Value = new DateTime(2012, 05, 28);
            BT_nuevainstruccion_cde.Enabled = true;
            CHECKB_instruccion_cd.CheckState = CheckState.Checked;
        }

        public ComboboxItem validareader(String campoText, String campoValue, MySqlDataReader mresultado)
        {
            ComboboxItem cItemresult = new ComboboxItem();
            try { 
                
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
            }catch( Exception Ex){
                cItemresult.Text = "";
                cItemresult.Value = "";
            }
            
            return cItemresult;
        }

        private void BT_detalleclientemenu_Click(object sender, EventArgs e)
        {
            capFormcap.Show();
            this.Close();
        }

        private void BT_detallecsalir_Click(object sender, EventArgs e)
        {
            capFormcap.Close();
            oFormlogin.Close();
            this.Close();
        }

        private void BT_modificarcliente_cd_Click(object sender, EventArgs e)
        {
          try {

              String sIdioma = "NULL";
              String sTipoPersona = "NULL";
              String sClienteTipo = "NULL";
              String sComunicacion = "NULL";
              String sTipoEnvio = "NULL";
              String sTipoTarifa = "NULL";

              String sMoneda = "NULL";
              String sHolder = "NULL";
              String sProvedorFactura = "NULL";

                     if (CB_tipopersonacliente.SelectedItem != null)
                    {
                        sTipoPersona = (CB_tipopersonacliente.SelectedItem as ComboboxItem).Value.ToString(); 
                    }

                     if (CB_idiomacliente.SelectedItem != null)
                     {
                         sIdioma = (CB_idiomacliente.SelectedItem as ComboboxItem).Value.ToString();
                     }

                    if (CB_comunicacioncliente.SelectedItem != null)
                    {
                        sComunicacion = (CB_comunicacioncliente.SelectedItem as ComboboxItem).Value.ToString();
                    }

                    if (CB_monedacliente.SelectedItem != null)
                    {
                        sMoneda = (CB_monedacliente.SelectedItem as ComboboxItem).Value.ToString();
                    }

                    if (CB_tipoenvio.SelectedItem != null)
                    {
                        sTipoEnvio = (CB_tipoenvio.SelectedItem as ComboboxItem).Value.ToString();
                    }

                    if (CB_tipoclientecliente.SelectedItem != null)
                    {
                        sClienteTipo = (CB_tipoclientecliente.SelectedItem as ComboboxItem).Value.ToString();
                    }
                   


                    if (CB_facturaelecliente.SelectedItem != null)
                    {
                        sProvedorFactura = (CB_facturaelecliente.SelectedItem as ComboboxItem).Value.ToString();
                    }

                    if (CB_holdercliente.SelectedItem != null)
                    {
                        sHolder = (CB_holdercliente.SelectedItem as ComboboxItem).Value.ToString();
                    }

                    if (CB_tarifacliente_cd.SelectedItem != null)
                    {
                        sTipoTarifa = (CB_tarifacliente_cd.SelectedItem as ComboboxItem).Value.ToString();
                    }

                    conect conectmodificar = new conect();
                String query123 = "UPDATE `cliente` SET " 
                                    +"ClienteRFC = '" + TB_rfccliente.Text
                                    + "',  ClienteTipoPersonaSAT= '" + sTipoPersona
                                    +"', NombreUtilClient = '" + TB_nombrecliente.Text +
                                    "',  MonedaId= " + sMoneda +
                                    ",  IdiomaId= " + sIdioma +
                                    ",  HolderId= " + sHolder +
                                    ",  TipoEnvioFacId= " + sTipoEnvio +
                                    ",  TipoComunicacionId= " + sComunicacion +
                                    ",  TipoClienteId= " + sClienteTipo +
                                    ", ClienteWebSite = '" + TB_sitioweb.Text +
                                    "', ClienteApellidoPaterno = '" + TB_apellidopaternocliente.Text +
                                    "', ClienteApellidoMaterno = '" + TB_apellidomaternoclietne.Text + 
                                    "', ClienteCurp = '" + TB_curpcliente.Text + 
                                    "',  ProveedorFacElecId= " + sProvedorFactura +
                                    ", ClienteEmail = '" + TB_correo.Text +
                                    "',  TipoTarifaId= " + sTipoTarifa + 
                                    ", ClienteObservacion = '" + RTB_observacionescliente.Text + 
                                    "'  WHERE ClienteId =" + datoid + ";";
            MySqlDataReader respuestastring123 = conectmodificar.getdatareader(query123);

            if (respuestastring123 == null)
            {
                MessageBox.Show("No se pudo modificar el cliente.");
            }
            else
            {
                MessageBox.Show("Se modificó cliente: " +datoid);
                respuestastring123.Close();
                conectmodificar.Cerrarconexion();
            }
           }
            catch (Exception E)
           {

            //escribimos en log
            Console.WriteLine("{0} Exception caught.", E);
            MessageBox.Show("Fallo " + E);
                            }

        }

        //contacto cliente
        private void BT_contactonuevoc_ccd_Click(object sender, EventArgs e)
        {
            if (!TB_contactonombrecd.Text.Equals("")    )
            {
                String sSexoContacto = "";
                if (CB_sexocontacto.SelectedItem != null)
                {
                    sSexoContacto = (CB_sexocontacto.SelectedItem as ComboboxItem).Text;
                }
                String habilitado = "0";
                if (CheckB_habilitadocontacto.CheckState == CheckState.Checked)
                {
                    habilitado = "1";
                }
                try
                {
                    conect conectinsert = new conect();
                    String queryinsert = "INSERT INTO `contacto` "+
                        "(`ContactoId`, `ClienteId`, `ContactoNick`, `ContactoNombre`, `ContactoTelefono`, `ContactoEmail`, `ContactoSexo`, `ContactoArea`, `ContactoPuesto`, `ContactoIndAct`, `InteresadoId`, `GrupoId`) "+
                        "VALUES (NULL, '" + 
                                    datoid + 
                                    "', '" + 
                                    TB_contactonick_cd.Text + 
                                    "', '" + 
                                    TB_contactonombrecd.Text + 
                                    "', '" + 
                                    TB_contactotelefono_cd.Text +
                                    "', '" + 
                                    TB_contactocorreo_cd.Text + 
                                    "', '" +
                                    sSexoContacto + 
                                    "',' " + 
                                    TB_contactorarea_cd.Text + 
                                    "', '" + 
                                    TB_contactopuesto_cd.Text + 
                                    "'," +
                                    habilitado+
                                   ", NULL, NULL);";
                    MySqlDataReader respuestastringinsert = conectinsert.getdatareader(queryinsert);
                    if (respuestastringinsert == null)
                    {
                        MessageBox.Show("No se pudo agregar un nuevo contacto");
                    }
                    else
                    {
                        respuestastringinsert.Close();
                        conectinsert.Cerrarconexion();
                        MessageBox.Show("Se Agrego un nuevo contacto");

                        limpiarcasillascontacto_cd();
                        actualizatablacontacto();
                    }
         


                }
                catch (Exception E)
                {
                    //escribimos en log
                    Console.WriteLine("{0} Exception caught.", E);
                    MessageBox.Show("Verifique la conexión a la red, o que la base de datos esté disponible");
                }
            }else{
                MessageBox.Show("Debe agregar mínimo el nombre del contacto.");
            }

        }

        public void limpiarcasillascontacto_cd()
        {

            TB_contactonombrecd.Text = "";
            TB_contactonick_cd.Text = "";            
            TB_contactotelefono_cd.Text = "";
            TB_contactorarea_cd.Text = "";
            TB_contactopuesto_cd.Text = "";
            TB_contactocorreo_cd.Text = "";
            CB_sexocontacto.Text = "Seleccione";
            CheckB_habilitadocontacto.CheckState = CheckState.Checked;
            idcontacto = null;
            BT_contactonuevoc_ccd.Enabled = true;
        }

        private void TB_limpiarcontacto_cd_Click(object sender, EventArgs e)
        {
            limpiarcasillascontacto_cd();
        }

        public void actualizatablacontacto() {
            listView1.Items.Clear(); 
            conect conectinstruccion = new conect();
            String kweryconsultainstruccion = "SELECT " +
                                                "instruccion.InstruccionId, " +
                                                "DAMETIPOINSTRUCCION(instruccion.TipoInstruccionId) AS Tipoinstruccion, " +
                                                "instruccion.InstrucciondDescip, " +
                                                "DAMEELUSUARIO(instruccion.UsuarioId) AS Usuario, " +
                                                "DATE_FORMAT(instruccion.InstruccionFecha,'%d-%m-%Y') AS InstruccionFecha, " +
                                                "DATE_FORMAT(instruccion.InstruccionFechaRec,'%d-%m-%Y') AS InstruccionFechaRec, " +
                                                "instruccion.InstruccionIndAct " +
                                                "FROM instruccion WHERE ClienteId =" + datoid + ";";
            MySqlDataReader respuestaconsultainstruccion = conectinstruccion.getdatareader(kweryconsultainstruccion);

            if (respuestaconsultainstruccion == null)
            {
                MessageBox.Show("Fallo la consulta de la instrucción");
            }
            else
            {
                int count = 0;
                while (respuestaconsultainstruccion.Read())
                {
                    // ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);

                    ListViewItem listaagregar3 = new ListViewItem(validareader("InstruccionId", "InstruccionId", respuestaconsultainstruccion).Text);
                    listaagregar3.SubItems.Add(validareader("Tipoinstruccion", "InstruccionId", respuestaconsultainstruccion).Text);
                    listaagregar3.SubItems.Add(validareader("InstrucciondDescip", "InstruccionId", respuestaconsultainstruccion).Text);
                    listaagregar3.SubItems.Add(validareader("Usuario", "InstruccionId", respuestaconsultainstruccion).Text);
                    listaagregar3.SubItems.Add(validareader("InstruccionFecha", "InstruccionId", respuestaconsultainstruccion).Text);
                    listaagregar3.SubItems.Add(validareader("InstruccionFechaRec", "InstruccionId", respuestaconsultainstruccion).Text);
                    if (validareader("InstruccionIndAct", "InstruccionId", respuestaconsultainstruccion).Text == "1")
                    {
                        listaagregar3.SubItems.Add("Habilitado");
                    }
                    else
                    {
                        listaagregar3.SubItems.Add("Inhabilitado");
                    }

                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listaagregar3.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listaagregar3.BackColor = Color.Azure;
                    }

                    listView3.Items.Add(listaagregar3);
                    listView3.FullRowSelect = true;
                    count++;
                }

                respuestaconsultainstruccion.Close();
                conectinstruccion.Cerrarconexion();

            }


            DTP_fechainstruccion_instruciones_cd.CustomFormat = "yyyy-MM-dd";
            DTP_fechainstruccion_instruciones_cd.Format = DateTimePickerFormat.Custom;

            DTP_fecharegistro.CustomFormat = "yyyy-MM-dd";
            DTP_fecharegistro.Format = DateTimePickerFormat.Custom;

            CHECKB_instruccion_cd.CheckState = CheckState.Checked;
            conect kwery3conect = new conect();
            String query3 = "SELECT * FROM contacto where ClienteId =" + datoid + ";";
            MySqlDataReader respuestastring30 = kwery3conect.getdatareader(query3);

            if (respuestastring30 == null)
            {
                MessageBox.Show("Fallo consulta del contacto");
            }
            else
            {
                int count = 0;
                while (respuestastring30.Read())
                {

                    ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);
                    listaagregar.SubItems.Add(validareader("ContactoNombre", "ContactoId", respuestastring30).Text);
                    listaagregar.SubItems.Add(validareader("ContactoNick", "ContactoId", respuestastring30).Text);
                    listaagregar.SubItems.Add(validareader("ContactoTelefono", "ContactoId", respuestastring30).Text);
                    listaagregar.SubItems.Add(validareader("ContactoEmail", "ContactoId", respuestastring30).Text);
                    listaagregar.SubItems.Add(validareader("ContactoArea", "ContactoId", respuestastring30).Text);
                    listaagregar.SubItems.Add(validareader("ContactoPuesto", "ContactoId", respuestastring30).Text);
                    listaagregar.SubItems.Add(validareader("ContactoSexo", "ContactoId", respuestastring30).Text);
                    if (validareader("ContactoIndAct", "ContactoId", respuestastring30).Text == "1")
                    {
                        listaagregar.SubItems.Add("Habilitado");
                    }
                    else
                    {
                        listaagregar.SubItems.Add("Inhabilitado");
                    }
                    int residuo = count % 2;
                    if (residuo == 0)
                    {
                        listaagregar.BackColor = Color.LightGray;
                    }
                    else
                    {
                        listaagregar.BackColor = Color.Azure;
                    }
                    listView1.Items.Add(listaagregar);
                    listView1.FullRowSelect = true;
                    count++;

                }
            }

        
        }


        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            idcontacto = listView1.SelectedItems[0].SubItems[0].Text;
            TB_contactonombrecd.Text = listView1.SelectedItems[0].SubItems[1].Text;
            TB_contactonick_cd.Text = listView1.SelectedItems[0].SubItems[2].Text;
            CB_sexocontacto.Text = listView1.SelectedItems[0].SubItems[7].Text;
            TB_contactotelefono_cd.Text = listView1.SelectedItems[0].SubItems[3].Text;           
            TB_contactorarea_cd.Text = listView1.SelectedItems[0].SubItems[5].Text;
            TB_contactopuesto_cd.Text = listView1.SelectedItems[0].SubItems[6].Text;
            TB_contactocorreo_cd.Text = listView1.SelectedItems[0].SubItems[4].Text;

            if (listView1.SelectedItems[0].SubItems[8].Text == "Habilitado")
            {
                CheckB_habilitadocontacto.CheckState = CheckState.Checked;
            }
            else
            {
                CheckB_habilitadocontacto.CheckState = CheckState.Unchecked;

            }

            BT_contactonuevoc_ccd.Enabled = false;

        }

        private void BT_modificarcontacto_cd_Click(object sender, EventArgs e)
        {
         try { 
            if(idcontacto == null){
                MessageBox.Show("Debes seleccionar un contacto");
            }else{
                String sSexoContacto = "";
                if (CB_sexocontacto.SelectedItem != null)
                {
                    sSexoContacto = (CB_sexocontacto.SelectedItem as ComboboxItem).Text;
                }
                String habilitado = "0";
                if (CheckB_habilitadocontacto.CheckState == CheckState.Checked)
                {
                    habilitado = "1";
                }
                conect conecupdatecontacto = new conect();
                String query123 = "UPDATE `contacto` SET ContactoNombre = '"+ 
                    TB_contactonombrecd.Text + 
                    "',  ContactoNick= '"  + 
                    TB_contactonick_cd.Text +
                    "', ContactoSexo = '" +
                    sSexoContacto +
                    "', ContactoIndAct = '" +
                    habilitado +
                    "', ContactoTelefono = '" + 
                    TB_contactotelefono_cd.Text +  
                    "', ContactoArea = '" + 
                    TB_contactorarea_cd.Text + 
                    "', ContactoPuesto = '" + 
                    TB_contactopuesto_cd.Text +
                    "', ContactoEmail = '" + 
                    TB_contactocorreo_cd.Text + 
                    "'  WHERE ContactoId =" + idcontacto + ";";
                MySqlDataReader respuestastring123 = conecupdatecontacto.getdatareader(query123);
                if (respuestastring123 == null)
                {
                    MessageBox.Show("No se logro modificar el contacto");
                }
                else
                {
                    respuestastring123.Close();
                    conecupdatecontacto.Cerrarconexion();
                    MessageBox.Show("Se modificó contacto: " + idcontacto);
                    limpiarcasillascontacto_cd();
                    actualizatablacontacto();
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

        //pantalladireccion



        private void BT_modificardireccion_cd_Click(object sender, EventArgs e)
        {

          try
          {
            if (iddireccion == null)
            {
                MessageBox.Show("Debes seleccionar una dirección");
            }   
            else
            {
                String habilitado = "0";
                if (CheckB_habilitadocontacto.CheckState == CheckState.Checked)
                {
                    habilitado = "1";
                }

                String sPais = "0";
                if (CB_direccionpais_cd.SelectedItem != null)
                {
                    sPais = (CB_direccionpais_cd.SelectedItem as ComboboxItem).Value.ToString();
                }

                String sTipoDireccion = "0";
                if (CB_direcciontipo_cd.SelectedItem != null)
                {
                    sTipoDireccion = (CB_direcciontipo_cd.SelectedItem as ComboboxItem).Value.ToString();
                }

                conect conecmodificardireccion = new conect();
                String query123 = "UPDATE `direccion` SET "+
                "DireccionCalle = '" + TB_direccioncalle_cd.Text + 
                "',  DireccionNumExt= '" + TB_direccionnumext_cd.Text +
                     "',  DireccionNumInt= '" + TB_direccionnumint_cd.Text +
                     "',  DireccionColonia= '" + TB_direccioncolonia_cd.Text +
                     "',  DireccionCP= '" + TB_direccioncp_cd.Text +
                     "',  DireccionPoblacion= '" + TB_direccionpoblacion_cd.Text +
                     "',  DireccionEstado= '" + TB_direccionestado_cd.Text +
                     "',  DireccionIndAct= " + habilitado +
                     ",  PaisId= " + sPais +
                    ",  TipoDireccionId= " + sTipoDireccion + "  WHERE DireccionID =" + iddireccion + ";";
                MySqlDataReader respuestastring123 = conecmodificardireccion.getdatareader(query123);
                if (respuestastring123 == null)
                {
                    MessageBox.Show("No se logro modificar dirección");
                }
                else
                {
                    MessageBox.Show("Se modificó dirección: " + iddireccion);
                    respuestastring123.Close();
                    conecmodificardireccion.Cerrarconexion();

                    limpiarcasillasdireccion_cd();
                    actualizatabladireccion();

                }
                
            }
          }

                catch (Exception E)
                {
                    //escribimos en log
                    Console.WriteLine("{0} Exception caught.", E);
                    MessageBox.Show("Verifique la conexión a la red, o que la base de datos esté disponible");
                }

        }

        private void BT_direccionlimpiar_cd_Click(object sender, EventArgs e)
        {
            limpiarcasillasdireccion_cd();
       
        }

        private void BT_direccionnuevo_cd_Click(object sender, EventArgs e)
        {
            //!TB_nombre_cliente.Text.Equals("")||
                            try
                            {

                                if (!TB_direccionpoblacion_cd.Text.Equals("") ||!TB_direccioncalle_cd.Text.Equals("") || !TB_direccioncolonia_cd.Text.Equals("") || !TB_direccionnumext_cd.Text.Equals("") ){
                                   
                                    String sPais = "0";
                                if (CB_direccionpais_cd.SelectedItem != null)
                                {
                                    sPais = (CB_direccionpais_cd.SelectedItem as ComboboxItem).Value.ToString();
                                }

                                String sTipoDireccion = "0";
                                if (CB_direcciontipo_cd.SelectedItem != null)
                                {
                                    sTipoDireccion = (CB_direcciontipo_cd.SelectedItem as ComboboxItem).Value.ToString();
                                }

                                String habilitado = "0";
                                if (CheckB_direccionhab_cd.CheckState == CheckState.Checked)
                                {
                                    habilitado = "1";
                                }

                                conect conectnuevadireccion = new conect();
                                String kweryinsertdireccion= "INSERT INTO `direccion` "+
                                                                        "(`DireccionID`, "+
                                                                        "`DireccionCalle`, "+
                                                                        "`DireccionNumExt`, "+
                                                                        "`DireccionNumInt`, "+
                                                                        "`DireccionColonia`, "+
                                                                        "`DireccionPoblacion`, "+
                                                                        "`DireccionEstado`, "+
                                                                        "`DireccionCP`, "+
                                                                        "`DireccionIndAct`, "+
                                                                        "`PaisId`, "+
                                                                        "`ContactoId`, "+
                                                                        "`InteresadoId`, "+
                                                                        "`ClienteId`, "+
                                                                        "`CasoId`, "+
                                                                        "`TipoDireccionId`) "+
                                                                        "VALUES "+
                                                                        "( NULL, '"+
                                                                         TB_direccioncalle_cd.Text +
                                                                         "','"+
                                                                         TB_direccionnumext_cd.Text+
                                                                         "','"+
                                                                         TB_direccionnumint_cd.Text+   
                                                                         "','"+
                                                                         TB_direccioncolonia_cd.Text+
                                                                         "','"+
                                                                         TB_direccionpoblacion_cd.Text+
                                                                         "','"+
                                                                         TB_direccionestado_cd.Text+
                                                                         "','"+
                                                                         TB_direccioncp_cd.Text+
                                                                         "',"+
                                                                         habilitado+
                                                                         ","+
                                                                         sPais+
                                                                         " ,NULL, NULL,"+
                                                                         datoid+
                                                                         ", NULL,"+
                                                                         sTipoDireccion +
                                                                         ");";

                                MySqlDataReader respuestastringinsertdireccion = conectnuevadireccion.getdatareader(kweryinsertdireccion);
                                if (respuestastringinsertdireccion == null)
                                {
                                    MessageBox.Show("No se pudo agregar un nuevo contacto");
                                }
                                else
                                {
                                    respuestastringinsertdireccion.Close();
                                    conectnuevadireccion.Cerrarconexion();
                                    MessageBox.Show("Se Agrego una nueva dirección");

                                    limpiarcasillasdireccion_cd();
                                    actualizatabladireccion();
                                }

                                }else{

                                    MessageBox.Show("Debes llenar por lo menos un dato calle, colonia población");

                                }

                                
                            
                            }
                            catch (Exception E)
                            {
                                //escribimos en log
                                Console.WriteLine("{0} Exception caught.", E);
                                MessageBox.Show("Verifique la conexión a la red, o que la base de datos esté disponible");
                            }
            
        }

        public void limpiarcasillasdireccion_cd()
        {

            TB_direccioncalle_cd.Text = "";
            TB_direccionnumext_cd.Text = "";
            TB_direccionnumint_cd.Text = "";
            TB_direccioncolonia_cd.Text = "";
            TB_direccioncp_cd.Text = "";
            TB_direccionpoblacion_cd.Text = "";
            TB_direccionestado_cd.Text = "";
            CB_direccionpais_cd.Text = "Seleccione";
            CB_direcciontipo_cd.Text = "Seleccione";
            CheckB_direccionhab_cd.CheckState = CheckState.Checked;
            iddireccion = null;
            BT_direccionnuevo_cd.Enabled = true;
        }

        public void actualizatabladireccion()
        {
            listView2.Items.Clear();
            conect conectselect = new conect();
            String query4 = "SELECT " +
                                "direccion.DireccionID, " +
                                "DAMETIPODIRECCCION(direccion.TipoDireccionId) as Tipodireccion, " +
                                "direccion.DireccionCalle, " +
                                "direccion.DireccionNumExt, " +
                                "direccion.DireccionNumInt, " +
                                "direccion.DireccionCP, " +
                                "direccion.DireccionColonia, " +
                                "direccion.DireccionPoblacion, " +
                                "direccion.DireccionEstado, " +
                                "DameNombrePais(direccion.PaisId) as Clavepais, " +
                                "direccion.DireccionIndAct " +
                            "FROM direccion WHERE ClienteId =" + datoid + ";";
            MySqlDataReader respuestastring40 = conectselect.getdatareader(query4);

            if (respuestastring40 == null)
            {
                MessageBox.Show("Fallo la consulta");
            }
            else
            {
                while (respuestastring40.Read())
                {
                    // ListViewItem listaagregar = new ListViewItem(validareader("ContactoId", "ContactoId", respuestastring30).Text);

                    ListViewItem listaagregar = new ListViewItem(validareader("DireccionID", "DireccionID", respuestastring40).Text);
                    listaagregar.SubItems.Add(validareader("Tipodireccion", "DireccionID", respuestastring40).Text);
                    listaagregar.SubItems.Add(validareader("DireccionCalle", "DireccionID", respuestastring40).Text);
                    listaagregar.SubItems.Add(validareader("DireccionNumExt", "DireccionID", respuestastring40).Text);
                    listaagregar.SubItems.Add(validareader("DireccionNumInt", "DireccionID", respuestastring40).Text);
                    listaagregar.SubItems.Add(validareader("DireccionCP", "DireccionID", respuestastring40).Text);
                    listaagregar.SubItems.Add(validareader("DireccionColonia", "DireccionID", respuestastring40).Text);
                    listaagregar.SubItems.Add(validareader("DireccionPoblacion", "DireccionID", respuestastring40).Text);
                    listaagregar.SubItems.Add(validareader("DireccionEstado", "DireccionID", respuestastring40).Text);
                    listaagregar.SubItems.Add(validareader("Clavepais", "DireccionID", respuestastring40).Text);
                    if (validareader("DireccionIndAct", "DireccionID", respuestastring40).Text == "1")
                    {
                        listaagregar.SubItems.Add("Habilitado");
                    }
                    else
                    {
                        listaagregar.SubItems.Add("Inhabilitado");
                    }
                    listView2.Items.Add(listaagregar);
                    listView2.FullRowSelect = true;

                }


                TB_direccionnumcliente_cd.Text = datoid;
                respuestastring40.Close();
                conectselect.Cerrarconexion();
            }

        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            iddireccion = listView2.SelectedItems[0].SubItems[0].Text;
            CB_direcciontipo_cd.Text = listView2.SelectedItems[0].SubItems[1].Text;
            TB_direccioncalle_cd.Text = listView2.SelectedItems[0].SubItems[2].Text;
            TB_direccionnumext_cd.Text = listView2.SelectedItems[0].SubItems[3].Text;
            TB_direccionnumint_cd.Text = listView2.SelectedItems[0].SubItems[4].Text;
            TB_direccioncp_cd.Text = listView2.SelectedItems[0].SubItems[5].Text;
            TB_direccioncolonia_cd.Text = listView2.SelectedItems[0].SubItems[6].Text;
            TB_direccionpoblacion_cd.Text = listView2.SelectedItems[0].SubItems[7].Text;
            TB_direccionestado_cd.Text = listView2.SelectedItems[0].SubItems[8].Text;
            CB_direccionpais_cd.Text = listView2.SelectedItems[0].SubItems[9].Text;


            if (listView2.SelectedItems[0].SubItems[10].Text == "Habilitado")
            {
                CheckB_direccionhab_cd.CheckState = CheckState.Checked;
            }
            else
            {
                CheckB_direccionhab_cd.CheckState = CheckState.Unchecked;

            }
            BT_direccionnuevo_cd.Enabled = false;

        }

        private void BT_eliminarcontacto_Click(object sender, EventArgs e)
        {
            try { 
                  if(idcontacto == null)
                  {
                        MessageBox.Show("Debes seleccionar un contacto");
                   }else
                  {
                  

                    var confirmResult = MessageBox.Show("¿Seguro que desea ELIMINAR este contacto ?", "Eliminar Contacto", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes) {

                        String sDelete = " DELETE FROM contacto WHERE contacto.ContactoId =  '" + idcontacto + "';";
                        conect con6 = new conect();
                        MySqlDataReader respuesta_delete = con6.getdatareader(sDelete);
                        if (respuesta_delete == null)
                        {
                            MessageBox.Show("No se puede eliminar al contacto, puede existir actividad en el sistema");
                        }
                        else
                        {
                            respuesta_delete.Close();
                            con6.Cerrarconexion();
                            MessageBox.Show("Contacto borrado correctamente");
                            actualizatablacontacto();
                            limpiarcasillascontacto_cd();
                            
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

        private void BT_eliminardireccion_cp_Click(object sender, EventArgs e)
        {
                 try
                {

                    if (iddireccion == null)
                    {
                        MessageBox.Show("Debes seleccionar una dirección");
                    }
                    else
                    { 
                        var confirmResult = MessageBox.Show("¿Seguro que desea ELIMINAR esta dirección ?", "Eliminar Dirección", MessageBoxButtons.YesNo);
                        if (confirmResult == DialogResult.Yes) 
                        {
                            String sDelete = " DELETE FROM direccion WHERE direccion.DireccionID = '" + iddireccion + "';";
                            conect coneliminardireccion = new conect();
                            MySqlDataReader respuesta_delete_direccion = coneliminardireccion.getdatareader(sDelete);
                            if (respuesta_delete_direccion == null)
                            {
                                MessageBox.Show("No se puedo eliminar dirección");
                            }
                            else
                            {
                                respuesta_delete_direccion.Close();
                                coneliminardireccion.Cerrarconexion();
                                MessageBox.Show("Direccion borrada correctamente");
                                actualizatabladireccion();
                                limpiarcasillasdireccion_cd();

                            }

                        }
                    
                    }

           
                }
                            catch (Exception E)
                            {
                                //escribimos en log
                                Console.WriteLine("{0} Exception caught.", E);
                                MessageBox.Show("Verifique la conexión a la red, o que la base de datos esté disponible");
                            }
     
        }

        private void BT_limpiarinstruccion_cd_Click(object sender, EventArgs e)
        {
            limpiatablainstruccion();
        }

        private void BT_nuevainstruccion_cde_Click(object sender, EventArgs e)
        {
              try
                 {
                     if (!RTB_instrucioninst_cd.Text.Equals("") && CB_usuarioinstrucciones_cd.SelectedItem != null && CB_instruccionestipo_cd.SelectedItem != null)
                     {
                         String habilitado = "0";
                         if (CHECKB_instruccion_cd.CheckState == CheckState.Checked)
                         {
                             habilitado = "1";
                         }

                         DTP_fecharegistro.CustomFormat = "yyyy-MM-dd";
                         DTP_fecharegistro.Format = DateTimePickerFormat.Custom;

                         DTP_fechainstruccion_instruciones_cd.CustomFormat = "yyyy-MM-dd";
                         DTP_fechainstruccion_instruciones_cd.Format = DateTimePickerFormat.Custom;

                         //dtpDate.Value.Date.ToString("yyyy-MM-dd HH:mm")

                         conect conectnuevainstruccion = new conect();
                         String kwerynuevainstruc = "INSERT INTO `instruccion` " +
                                                                   " (`InstruccionId`, " +
                                                                   " `TipoInstruccionId`, " +
                                                                   " `InstrucciondDescip`, " +
                                                                   " `ClienteId`, " +
                                                                   " `CasoId`, " +
                                                                   " `UsuarioId`, " +
                                                                   " `InstruccionFecha`, " +
                                                                   " `InstruccionFechaRec`, " +
                                                                   " `InstruccionIndAct`, " +
                                                                   " `InteresadoId`) " +
                                                                   " VALUES " +
                                                                  "( NULL, " +
                                                                  (CB_instruccionestipo_cd.SelectedItem as ComboboxItem).Value +
                                                                  ",'" +
                                                                  RTB_instrucioninst_cd.Text +
                                                                  "'," +
                                                                  datoid +
                                                                  ", NULL, " +
                                                                  (CB_usuarioinstrucciones_cd.SelectedItem as ComboboxItem).Value +
                                                                  ",'" +
                                                                  DTP_fechainstruccion_instruciones_cd.Value.Date.ToString("yyyy-MM-dd HH:mm") +
                                                                  "','" +
                                                                  DTP_fecharegistro.Value.Date.ToString("yyyy-MM-dd HH:mm") +
                                                                  "'," +
                                                                  habilitado +
                                                                  "," +
                                                                  "NULL);";
                         MySqlDataReader respuestanuevainstruccion = conectnuevainstruccion.getdatareader(kwerynuevainstruc);

                         if (respuestanuevainstruccion == null)
                         {
                             MessageBox.Show("No se pudo agregar una nueva instrucción");
                         }
                         else 
                         {
                             respuestanuevainstruccion.Close();
                             conectnuevainstruccion.Cerrarconexion();
                             MessageBox.Show("Se Agrego una nueva instruccion para este cliente.");
                             limpiatablainstruccion();
                             actualizatablainstuccion();
                         }


                     }
                     else
                     {
                         MessageBox.Show("Debes Seleccionar un tipo de instruccion, un usuario y la instruccion no debe de ir vacía ");
                     }

                  }
                  catch (Exception E)
                  {
                    Console.WriteLine("{0} Exception caught.", E);
                    MessageBox.Show("Verifique la conexión a la red, o que la base de datos esté disponible");
                   }

        }

        private void listView3_DoubleClick(object sender, EventArgs e)
        {

            idInstruccion = listView3.SelectedItems[0].SubItems[0].Text;
            CB_instruccionestipo_cd.Text = listView3.SelectedItems[0].SubItems[1].Text;
            RTB_instrucioninst_cd.Text = listView3.SelectedItems[0].SubItems[2].Text;
            CB_usuarioinstrucciones_cd.Text = listView3.SelectedItems[0].SubItems[3].Text;
            //DTP_fechainstruccion_instruciones_cd.Value = new DateTime (listView3.SelectedItems[4].SubItems[1].Text);
            //DTP_fecharegistro.listView3 = listView3.SelectedItems[0].SubItems[5].Text;
            String datofecharegistro = listView3.SelectedItems[0].SubItems[5].Text;
            String datofechainstruccion = listView3.SelectedItems[0].SubItems[4].Text;

            String anoregistro = datofecharegistro.Substring(6, 4);
            String mesregistro = datofecharegistro.Substring(3, 2);
            String diaregistro = datofecharegistro.Substring(0, 2);


            String anoinstuccion = datofechainstruccion.Substring(6, 4);
            String mesinstruccion = datofechainstruccion.Substring(3, 2);
            String diainstuccion = datofechainstruccion.Substring(0, 2);

            DTP_fechainstruccion_instruciones_cd.Value = new DateTime(Int32.Parse(anoinstuccion), Int32.Parse(mesinstruccion), Int32.Parse(diainstuccion));
            DTP_fecharegistro.Value = new DateTime(Int32.Parse(anoregistro), Int32.Parse(mesregistro), Int32.Parse(diaregistro));

           // DateTimePiker.Value = new DateTime(2012, 05, 28);
            if (listView3.SelectedItems[0].SubItems[6].Text == "Habilitado")
            {
                CHECKB_instruccion_cd.CheckState = CheckState.Checked;
            }
            else
            {
                CHECKB_instruccion_cd.CheckState = CheckState.Unchecked;

            }
            BT_nuevainstruccion_cde.Enabled = false;

        }

        private void BT_modificarinsftruccion_cd_Click(object sender, EventArgs e)
        {
            try 
            {
                if (idInstruccion == null)
                {
                    MessageBox.Show("Debes elegir una instrucción");
                }
                else
                {
                    String habilitado = "0";
                    if (CHECKB_instruccion_cd.CheckState == CheckState.Checked)
                    {
                        habilitado = "1";
                    }
                    if (!RTB_instrucioninst_cd.Text.Equals("") && CB_usuarioinstrucciones_cd.SelectedItem != null && CB_instruccionestipo_cd.SelectedItem != null)
                    {

                        conect conecmodificarinstruccion = new conect();
                        String kweryinstruccion = "UPDATE `instruccion`   SET "+
                                                        " `TipoInstruccionId` = " + (CB_instruccionestipo_cd.SelectedItem as ComboboxItem).Value +
                                                        ", `InstrucciondDescip` = '" + RTB_instrucioninst_cd.Text +
                                                        "', `ClienteId` = " + datoid  +
                                                        ", `UsuarioId` = " + (CB_usuarioinstrucciones_cd.SelectedItem as ComboboxItem ).Value +
                                                        ", `InstruccionFecha` = '" + DTP_fechainstruccion_instruciones_cd.Value.Date.ToString("yyyy-MM-dd HH:mm") +
                                                        "', `InstruccionFechaRec` = '"+ DTP_fecharegistro.Value.Date.ToString("yyyy-MM-dd HH:mm") +
                                                        "', `InstruccionIndAct` = " + habilitado +
                                                        " WHERE `InstruccionId` = " + idInstruccion + ";";
                        MySqlDataReader respuestamodificarinstruccion = conecmodificarinstruccion.getdatareader(kweryinstruccion);

                        if (respuestamodificarinstruccion == null)
                        {
                            MessageBox.Show("No se pudo modificar la instrucción");
                        }
                        else
                        {
                            MessageBox.Show("Se mofificó instrucción :" + idInstruccion);
                            respuestamodificarinstruccion.Close();
                            conecmodificarinstruccion.Cerrarconexion();
                            limpiatablainstruccion();
                            actualizatablainstuccion();

                        }

                    
                    }
                    else
                    {
                        MessageBox.Show("Debes Seleccionar un tipo de instruccion, un usuario y la instruccion no debe de ir vacía ");
                    }
                   

                }
            }
            catch (Exception E)
            {
                Console.WriteLine("{0} Exception caught.", E);
                MessageBox.Show("Verifique la conexión a la red, o que la base de datos esté disponible");
            }
        }

        private void BT_eliminarinstruccion_cd_Click(object sender, EventArgs e)
        {
            try
            {
                if (idInstruccion == null)
                {
                    MessageBox.Show("Debes seleccionar una instrucción");
                }
                else
                {
                        var confirmResult = MessageBox.Show("¿Seguro que desea ELIMINAR esta instrucción ?", "Eliminar Instrucción", MessageBoxButtons.YesNo);
                        if (confirmResult == DialogResult.Yes)
                        {
                            conect conectdeleteinstruccion = new conect();
                            String deleteinstruccion = "DELETE FROM instruccion WHERE instruccion.InstruccionId = " + idInstruccion + ";";
                            MySqlDataReader respuesta_delete_instruccion = conectdeleteinstruccion.getdatareader(deleteinstruccion);

                            if (respuesta_delete_instruccion == null)
                            {
                                MessageBox.Show("No se puede eliminar la instrucción");
                            }
                            else
                            {
                                respuesta_delete_instruccion.Close();
                                conectdeleteinstruccion.Cerrarconexion();
                                MessageBox.Show("Instrucción borrada correctamente");
                                actualizatablainstuccion();
                                limpiatablainstruccion();
                            }

                        }
                }

            }
            catch (Exception E)
            {
                Console.WriteLine("{0} Exception caught.", E);
                MessageBox.Show("Verifique la conexión a la red, o que la base de datos esté disponible");
            }

        }





        public String validafechavacia(String fecha)
        {
            String fechanueva = fecha;
            String datoabuscar = "0000";
            //fechanueva.IndexOf(datoabuscar);

            if (fechanueva.IndexOf(datoabuscar) == -1)
            {
                return fechanueva;
            }
            else
            {
                return "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

                        try
            {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(1);
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];
            int i = 1;
            int i2 = 1;
            foreach (ListViewItem listacasos in listView4.Items)
            {
                i = 1;
                foreach (ListViewItem.ListViewSubItem listacasos2 in listacasos.SubItems)
                {
                    ws.Cells[i2, i] = listacasos2.Text;
                    i++;
                }
                i2++;
            }
            MessageBox.Show("Temino la exportación");

            }
                        catch (Exception E)
                        {
                            Console.WriteLine("{0} Exception caught.", E);
                            //MessageBox.Show("Verifique la conexión a la red, o que la base de datos esté disponible");
                        }


        }

        private void button5_Click(object sender, EventArgs e)
        {

            try
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(1);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];
                int i = 1;
                int i2 = 1;
                foreach (ListViewItem listacasos in listView5.Items)
                {
                    i = 1;
                    foreach (ListViewItem.ListViewSubItem listacasos2 in listacasos.SubItems)
                    {
                        ws.Cells[i2, i] = listacasos2.Text;
                        i++;
                    }
                    i2++;
                }
                MessageBox.Show("Temino la exportación");

            }
            catch (Exception E)
            {
                Console.WriteLine("{0} Exception caught.", E);
                //MessageBox.Show("Verifique la conexión a la red, o que la base de datos esté disponible");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            limpiarcasillasobservaciones();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                if (CB_observacionesusuario_cd != null && RTB_Observaciones_cd.Text != "")
                {
                    
                         DTP_fecharegistro_cd.CustomFormat = "yyyy-MM-dd";
                         DTP_fecharegistro_cd.Format = DateTimePickerFormat.Custom;

                    conect conectnuevaobservacion = new conect();
                    String kwerynuevaobservacion = "INSERT INTO `observacion` "+
                                                       " (`ObservacionId`, "+
                                                       " `CasoId`, "+
                                                       " `ClienteId`, "+
                                                       " `UsuarioId`, "+
                                                       " `ObservacionFecha`, "+
                                                       " `ObservacionTexto`) "+
                                                       " VALUES "+
                                                       "( NULL, NULL, " +
                                                       datoid +
                                                        "," +
                                                        (CB_observacionesusuario_cd.SelectedItem as ComboboxItem).Value+
                                                        ",'" +
                                                        DTP_fecharegistro_cd.Value.Date.ToString("yyyy-MM-dd HH:mm") +
                                                        "','" +
                                                        RTB_Observaciones_cd.Text+ "');";

                    MySqlDataReader respuestanuevaobservacion = conectnuevaobservacion.getdatareader(kwerynuevaobservacion);

                    if (respuestanuevaobservacion == null)
                    {
                        MessageBox.Show("No se pudo agregar una nueva instrucción");
                    }
                    else
                    {
                        respuestanuevaobservacion.Close();
                        conectnuevaobservacion.Cerrarconexion();
                        MessageBox.Show("Se Agrego una nueva observación para este cliente.");
                        limpiarcasillasobservaciones();
                        actualizatablaobservaciones();
                    }

                }
                else
                {
                    MessageBox.Show("Debes LLenar todos los campos");
                }
                                              
            }
            
            catch (Exception E)
            {
            
                Console.WriteLine("{0} Exception caught.", E);
                
                //MessageBox.Show("Verifique la conexión a la red, o que la base de datos esté disponible");
                
            }

        }

        private void listView6_DoubleClick(object sender, EventArgs e)
        {
            IdObservacion = listView6.SelectedItems[0].SubItems[0].Text;
            RTB_Observaciones_cd.Text = listView6.SelectedItems[0].SubItems[1].Text;
            CB_observacionesusuario_cd.Text = listView6.SelectedItems[0].SubItems[2].Text;

            String datofechaobservacion = listView6.SelectedItems[0].SubItems[3].Text;

            String anoobservacion = datofechaobservacion.Substring(6, 4);
            String mesobservacion = datofechaobservacion.Substring(3, 2);
            String diaobservacion = datofechaobservacion.Substring(0, 2);

            DTP_fecharegistro_cd.Value = new DateTime(Int32.Parse(anoobservacion), Int32.Parse(mesobservacion), Int32.Parse(diaobservacion));

            BT_Agregaobservacion_cd.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try 
            {
                if(IdObservacion == null)
                {
                    MessageBox.Show("Debes elegir una observación");
                }
                else
                {
                    if (CB_observacionesusuario_cd.SelectedItem != null && !RTB_Observaciones_cd.Equals(""))
                    {
                        conect conectmodificarobservacion = new conect();
                        String kweryobservacionmodificar = "UPDATE `observacion` SET " +
                                                                    "`UsuarioId` =  " + (CB_observacionesusuario_cd.SelectedItem as ComboboxItem ).Value +
                                                                    ",`ObservacionFecha` =  '" + DTP_fecharegistro_cd.Value.Date.ToString("yyyy-MM-dd HH:mm") +
                                                                    "',`ObservacionTexto` =  '" + RTB_Observaciones_cd.Text +
                                                                    "' WHERE `ObservacionId` = " + IdObservacion  + ";";

                        MySqlDataReader respuestamodificarobservacion = conectmodificarobservacion.getdatareader(kweryobservacionmodificar);

                        if (respuestamodificarobservacion == null)
                        {
                            MessageBox.Show("No se pudo modificar la instrucción");
                        }
                        else
                        {
                            MessageBox.Show("Se mofificó observacions :" + IdObservacion);
                            respuestamodificarobservacion.Close();
                            conectmodificarobservacion.Cerrarconexion();
                            limpiarcasillasobservaciones();
                            actualizatablaobservaciones();

                        }

                    }
                    else
                    {
                        MessageBox.Show("Debes seleccionar un usuario y la observación no debe de ir vacía");
                    }
                }
            }
            catch (Exception E)
            {
              Console.WriteLine("{0} Exception caught.", E);
             // MessageBox.Show("Verifique la conexión a la red, o que la base de datos esté disponible");
            }
           
        }

        private void BT_eliminarobservaciones_cd_Click(object sender, EventArgs e)
        {
                        try
                        {
                            if (IdObservacion == null)
                            {
                                MessageBox.Show("Debes seleccionar una observación");
                            }
                            else
                            {
                                                        var confirmResult = MessageBox.Show("¿Seguro que desea ELIMINAR esta observación ?", "Eliminar Observación", MessageBoxButtons.YesNo);
                                                        if (confirmResult == DialogResult.Yes)
                                                        {
                                                            conect conecdeleteobservacion = new conect();
                                                            String deleteobservacion = "DELETE FROM observacion WHERE observacion.ObservacionId = " + IdObservacion + ";";
                                                            MySqlDataReader respuesta_delete_observacion = conecdeleteobservacion.getdatareader(deleteobservacion);


                                                            if (respuesta_delete_observacion == null)
                                                            {
                                                                MessageBox.Show("No se puede eliminar la observación");
                                                            }
                                                            else
                                                            {
                                                                respuesta_delete_observacion.Close();
                                                                conecdeleteobservacion.Cerrarconexion();
                                                                MessageBox.Show("Observacion borrada correctamente");
                                                                actualizatablaobservaciones();
                                                                limpiarcasillasobservaciones();
                                             
                                                            }

 
                                                        }

                            }
                        
                        }
                        catch (Exception E)
                        {
                            Console.WriteLine("{0} Exception caught.", E);
                            MessageBox.Show("Verifique la conexión a la red, o que la base de datos esté disponible");
                        }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            try
            {
                var fileContent = string.Empty;
                var filePath = string.Empty;
                String sNamefile = "";
                String[] aName;
                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {

                    openFileDialog.InitialDirectory = mdoc;
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        filePath = openFileDialog.FileName;
                        aName = filePath.Split('\\');
                        sNamefile = aName[aName.Length - 1];
                    }
                }
                configuracionfiles obj = new configuracionfiles();
                obj.configuracionfilesinicio();


            }
            catch (Exception E)
            {
                //MessageBox.Show("");
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
                        try
                        {
                            if (CB_tipodocumento_documento_cd.SelectedItem != null && CB_usuariosdocumento_cd.SelectedItem != null && RTB_descripcion_documento_cd.Text != "" && TB_nombredoc_documento_cd.Text != "")
                            {
                                conect conectnuevodoc = new conect();
                                //String kwerynuevodoc = "INSERT INTO `documento` " +
                                //                                    "(`DocumentoId`, "+
                                //                                    "`DocumentoCodigoBarras`, "+
                                //                                    "`SubTipoDocumentoId`, "+
                                //                                    "`DocumentoFecha`, "+
                                //                                    "`DocumentoFolio`, "+
                                //                                    "`DocumentoFechaRecepcion`, "+
                                //                                    "`DocumentoFechaVencimiento`, "+
                                //                                    "`DocumentoFechaCaptura`, "+
                                //                                    "`DocumentoFechaEscaneo`, "+
                                //                                    "`DocumentoObservacion`, "+
                                //                                    "`DocumentoIdRef`, "+
                                //                                    "`UsuarioId`, "+
                                //                                    "`CompaniaMensajeriaId`, "+ 
                                //                                    "`DocumentoFechaEnvio`, "+
                                //                                    "`DocumentoNumeroGuia`, "+
                                //                                    "`DocumentoFechaEntrega`, "+
                                //                                    "`usuarioIdPreparo`) "+
                                //                                    "VALUES "+
                                //                                    " (NULL,
                                //                                    " <{DocumentoCodigoBarras: }>,
                                //                                    " <{SubTipoDocumentoId: }>,
                                //                                    " <{DocumentoFecha: }>,
                                //                                    " <{DocumentoFolio: }>,
                                //                                    " <{DocumentoFechaRecepcion: }>,
                                //                                    " <{DocumentoFechaVencimiento: }>,
                                //                                    " <{DocumentoFechaCaptura: }>,
                                //                                    " <{DocumentoFechaEscaneo: }>,
                                //                                    " <{DocumentoObservacion: }>,
                                //                                    " <{DocumentoIdRef: }>,
                                //                                    " <{UsuarioId: }>,
                                //                                    " <{CompaniaMensajeriaId: }>,
                                //                                    " <{DocumentoFechaEnvio: }>,
                                //                                    " <{DocumentoNumeroGuia: }>,
                                //                                    " <{DocumentoFechaEntrega: }>,
                                //                                    " <{usuarioIdPreparo: }>);";

                            }
                            else
                            {
                                MessageBox.Show("Los datos no deben de ir vacios");
                            }
                        
                        }

                        catch (Exception E)
                        {

                            Console.WriteLine("{0} Exception caught.", E);

                            //MessageBox.Show("Verifique la conexión a la red, o que la base de datos esté disponible");

                        }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            capFormcap.Show();
            this.Close();
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
            conect con8 = new conect();
            String kwery8 = "SELECT proveedorfacelec.ProveedorFacElecId , proveedorfacelec.ProveedorFacElecDescrip FROM  proveedorfacelec where ProveedorFacElecId=3 ";
            MySqlDataReader respuestastring8 = con8.getdatareader(kwery8);
            while (respuestastring8.Read())
            {
                CB_facturaelecliente.SelectedIndex = CB_facturaelecliente.Items.Add(validareader("ProveedorFacElecDescrip", "ProveedorFacElecId", respuestastring8));
            }
            respuestastring8.Close();
            con8.Cerrarconexion();

            conect con5 = new conect();
            String kwery5 = "SELECT tipoenviofac.TipoEnvioFacId , tipoenviofac.TipoEnvioFacDescrip FROM  tipoenviofac where TipoEnvioFacId=3";
            MySqlDataReader respuestastring5 = con5.getdatareader(kwery5);
            while (respuestastring5.Read())
            {
                CB_tipoenvio.SelectedIndex = CB_tipoenvio.Items.Add(validareader("TipoEnvioFacDescrip", "TipoEnvioFacId", respuestastring5));
            }
            respuestastring5.Close();
            con5.Cerrarconexion();

            conect con3 = new conect();
            String kwery3 = "SELECT tipocomunicacion.TipoComunicacionId , tipocomunicacion.TipoComunicacionDescrip  FROM  tipocomunicacion where TipoComunicacionId=1";
            MySqlDataReader respuestastring3 = con3.getdatareader(kwery3);
            while (respuestastring3.Read())
            {
                CB_comunicacioncliente.SelectedIndex = CB_comunicacioncliente.Items.Add(validareader("TipoComunicacionDescrip", "TipoComunicacionId", respuestastring3));
            }
            respuestastring3.Close();
            con3.Cerrarconexion();

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
            conect consp = new conect();
            String queryss = "select * from pais where PaisId=" + valor;

            MySqlDataReader respuestastringss = consp.getdatareader(queryss);
            while (respuestastringss.Read())
            {
                CB_direccionpais_cd.SelectedIndex = CB_direccionpais_cd.Items.Add(validareader("PaisNombre", "PaisId", respuestastringss));
            }
            respuestastringss.Close();
            consp.Cerrarconexion();

            if (valor == "148")
            {
                CB_tipopersonacliente.Text = "";
                CB_tipopersonacliente.Items.Clear();

                String querytipo = "Select * from tipo_persona where id_tipo_persona in(1,3) ";

                MySqlDataReader respuestatipo = con.getdatareader(querytipo);
                while (respuestatipo.Read())
                {
                    CB_tipopersonacliente.Items.Add(validareader("nombre_tipopersona", "id_tipo_persona", respuestatipo));
                }
                //tipopersona_SelectedIndexChanged(seccion);
                respuestatipo.Close();
                con.Cerrarconexion();


                conect conect4 = new conect();
                String kwery4 = "SELECT moneda.MonedaId , moneda.MonedaDescrip FROM  moneda where MonedaId=2";
                MySqlDataReader respuestastring4 = conect4.getdatareader(kwery4);
                while (respuestastring4.Read())
                {
                    CB_monedacliente.SelectedIndex = CB_monedacliente.Items.Add(validareader("MonedaDescrip", "MonedaId", respuestastring4));
                }
                respuestastring4.Close();
                conect4.Cerrarconexion();
                label32.Visible = true;
                TB_direccioncolonia_cd.Visible = true;
            }
            else
            {
                CB_tipopersonacliente.Items.Clear();
                CB_tipopersonacliente.Text = "";
                String querytipo = "Select * from tipo_persona where id_tipo_persona in(2,4) ";

                MySqlDataReader respuestatipo = con.getdatareader(querytipo);
                while (respuestatipo.Read())
                {
                    CB_tipopersonacliente.Items.Add(validareader("nombre_tipopersona", "id_tipo_persona", respuestatipo));
                }
                //tipopersona_SelectedIndexChanged(seccion);
                respuestatipo.Close();
                con.Cerrarconexion();

                label32.Visible = false;
                TB_direccioncolonia_cd.Visible = false;
            }
            //Si el pais es alguno de estos IDPais entonces el valor predeterminado sera ingles
            if (valor == "45" || valor == "213" || valor == "74" || valor == "104")
            {
                String kwery2 = "SELECT * FROM  idioma where IdiomaId=1";
                MySqlDataReader respuestastring2 = con.getdatareader(kwery2);
                while (respuestastring2.Read())
                {
                    CB_idiomacliente.SelectedIndex = CB_idiomacliente.Items.Add(validareader("IdiomaDescripcion", "IdiomaId", respuestastring2));
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
                    CB_idiomacliente.SelectedIndex = CB_idiomacliente.Items.Add(validareader("IdiomaDescripcion", "IdiomaId", respuestastring2));
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
                    CB_idiomacliente.Items.Add(validareader("IdiomaDescripcion", "IdiomaId", respuestastring2));
                    //CB_idioma_cliente.Items
                }
                respuestastring2.Close();
            }
            return;
        }
    }
}
