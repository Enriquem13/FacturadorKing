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
    public partial class Cliente : Form
    {
        public Form1 login;
        public Form1 oFormlogin;
        public captura capFormcap;
        //conect con1;
        public String datoid;
        private Form1 form1contruct;
        private Form fBusquedanuevo;
        String idcontacto;
        String iddireccion;
        String idInstruccion;
        String IdObservacion;
        public String rpaiss;
        private Consulacliente consulacliente;
        public String Usuario;
        public Cliente(Form1 form, captura Formcap, Form fBusquedan)
        {
            fBusquedanuevo = fBusquedan;
            oFormlogin = form;
            capFormcap = Formcap;
            InitializeComponent();
            ComboboxItem item = new ComboboxItem();
            item.Text = oFormlogin.sUsername;
            item.Value = oFormlogin.sId;
            fisica.Visible = false;
            moral.Visible = false;
            listView12.Visible = false;
            listView13.Visible = false;
            listView14.Visible = false;
            listView1.Visible = false;
            conect conect_1 = new conect();
            CB_responsable.Text = oFormlogin.sUsername;

            String sQresponsable = "select UsuarioName, UsuarioId from usuario where UsuarioId =" + oFormlogin.sId;
            MySqlDataReader respuresponsable = conect_1.getdatareader(sQresponsable);

            while (respuresponsable.Read())
            {
                CB_responsable.SelectedIndex = CB_responsable.Items.Add(validareader("UsuarioName", "UsuarioId", respuresponsable));
            }

            respuresponsable.Close();
            conect_1.Cerrarconexion();

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

            conect con3 = new conect();
            String kwery3 = "SELECT tipocomunicacion.TipoComunicacionId , tipocomunicacion.TipoComunicacionDescrip  FROM  tipocomunicacion";
            MySqlDataReader respuestastring3 = con3.getdatareader(kwery3);
            while (respuestastring3.Read())
            {
                CB_comunicacion.Items.Add(validareader("TipoComunicacionDescrip", "TipoComunicacionId", respuestastring3));
            }
            respuestastring3.Close();
            con3.Cerrarconexion();



            conect conectdireccionpais = new conect();
            String kwerypaisdireccion = "SELECT PaisId, PaisNombre FROM pais order by PaisNombre ";
            MySqlDataReader respuestastringpaisdireccion = conectdireccionpais.getdatareader(kwerypaisdireccion);
            while (respuestastringpaisdireccion.Read())
            {
                CB_interesadoDpais.Items.Add(validareader("PaisNombre", "PaisId", respuestastringpaisdireccion));
            }
            respuestastringpaisdireccion.Close();
            conectdireccionpais.Cerrarconexion();

            conect con4 = new conect();
            String kwery4 = "SELECT  moneda.MonedaId , moneda.MonedaDescrip  FROM  moneda";
            MySqlDataReader respuestastring4 = con4.getdatareader(kwery4);
            while (respuestastring4.Read())
            {
                CB_moneda.Items.Add(validareader("MonedaDescrip", "MonedaId", respuestastring4));
            }
            respuestastring4.Close();
            con4.Cerrarconexion();

            conect con5 = new conect();
            String kwery5 = "SELECT tipoenviofac.TipoEnvioFacId , tipoenviofac.TipoEnvioFacDescrip FROM  tipoenviofac";
            MySqlDataReader respuestastring5 = con5.getdatareader(kwery5);
            while (respuestastring5.Read())
            {
                CB_envio.Items.Add(validareader("TipoEnvioFacDescrip", "TipoEnvioFacId", respuestastring5));
            }
            respuestastring5.Close();
            con5.Cerrarconexion();

            conect con6 = new conect();
            String kwery6 = "SELECT tipocliente.TipoClienteId , tipocliente.TipoClienteDescrip FROM  tipocliente";
            MySqlDataReader respuestastring6 = con6.getdatareader(kwery6);
            while (respuestastring6.Read())
            {
                CB_cliente_tipo.Items.Add(validareader("TipoClienteDescrip", "TipoClienteId", respuestastring6));
            }
            respuestastring6.Close();
            con6.Cerrarconexion();




            conect con8 = new conect();
            String kwery8 = "SELECT proveedorfacelec.ProveedorFacElecId , proveedorfacelec.ProveedorFacElecDescrip FROM  proveedorfacelec where ProveedorFacElecId=3 ";
            MySqlDataReader respuestastring8 = con8.getdatareader(kwery8);
            while (respuestastring8.Read())
            {
                CB_proovedor_cliente_fac.Items.Add(validareader("ProveedorFacElecDescrip", "ProveedorFacElecId", respuestastring8));
            }
            respuestastring8.Close();
            con8.Cerrarconexion();

            conect con9 = new conect();
            String kwery9 = "SELECT holder.HolderId , holder.HolderNombre FROM  holder where HolderId=8";
            MySqlDataReader respuestastring9 = con9.getdatareader(kwery9);
            while (respuestastring9.Read())
            {
                CB_holder_cliente.Items.Add(validareader("HolderNombre", "HolderId", respuestastring9));
            }
            respuestastring9.Close();
            con9.Cerrarconexion();

            conect con10 = new conect();
            String kwery10 = "SELECT tipotarifa.TipoTarifaId , tipotarifa.TipotarifaDescrip FROM  tipotarifa";
            MySqlDataReader respuestastring10 = con10.getdatareader(kwery10);
            while (respuestastring10.Read())
            {
                CB_tipo_tarifa.Items.Add(validareader("TipotarifaDescrip", "TipoTarifaId", respuestastring10));
            }
            respuestastring9.Close();
            con10.Cerrarconexion();
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
            String kweryusuarioinstruccion = "SELECT usuario.UsuarioId , usuario.UsuarioName FROM usuario where UsuarioIndActivo=1 ";
            MySqlDataReader respuestausuarioinstruccion = conectusuarioinstruccion.getdatareader(kweryusuarioinstruccion);



            /* while (respuestausuarioinstruccion.Read())
             {
                 CB_usuarioinstrucciones_cd.Items.Add(validareader("UsuarioName", "UsuarioId", respuestausuarioinstruccion));
             }
             respuestausuarioinstruccion.Close();
             conectusuarioinstruccion.Cerrarconexion();
             // DATE_FORMAT(caso_patente.CasoFechaInternacional,'%d-%m-%Y') AS CasoFechaInternacional
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

             //


             // comienza quinta consulta para quinta pantalla

             conect consultapatentecaso = new conect();
             String kwerypatentecaso = "SELECT " +
                                         " caso_patente.CasoId, " +
                                         " DameLaReferencia(caso_patente.CasoId) AS referencia, " +
                                         " caso_patente.CasoNumeroExpedienteLargo, " +
                                         " DATE_FORMAT(caso_patente.CasoFechaLegal,'%d-%m-%Y') AS CasoFechaLegal, " +
                                         " caso_patente.CasoNumConcedida, " +
                                         " DATE_FORMAT(caso_patente.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, " +
                                         " DATE_FORMAT(caso_patente.CasoFechaVigencia,'%d-%m-%Y') AS CasoFechaVigencia, " +
                                         " DameTipoSolicitudDescrip(caso_patente.TipoSolicitudId) AS TipoDeSolicitud, " +
                                         " DameEstatusCasoDescrip(caso_patente.EstatusCasoId) AS Estatus , " +
                                         " caso_patente.CasoTitular, " +
                                         " caso_patente.CasoTituloespanol, " +
                                         " Dameelusuario(caso_patente.ResponsableId) AS Responsable, " +
                                         " Get_Prioridad(caso_patente.CasoId) As Prioridades , " +
                                         " Get_Interesados(caso_patente.CasoId) AS Interesados ,  " +
                                         " Get_AnualidadFechaLimitePago(caso_patente.CasoId) AS Fechalimitepago, " +
                                         " Damealcontactocaso (casocliente.contactoid) AS Contactos, " +
                                         " Dameelcorreocontactocaso (casocliente.contactoid) AS Correos, " +
                                         " Get_anualidadespagadas (caso_patente.CasoId) AS Anualidades, " +
                                         " DameNombrePais(caso_patente.PaisId) AS Pais " +
                                     "FROM " +
                                         " caso_patente, " +
                                         " cliente, " +
                                         " casocliente " +
                                     "WHERE  " +
                                         " cliente.ClienteId = " + datoid +
                                         " AND caso_patente.CasoId = casocliente.CasoId " +
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
             String kweryconsultamarcas = "SELECT " +
                                             " caso_marcas.CasoId, " +
                                             " DameLaReferencia(caso_marcas.CasoId) AS Referencia, " +
                                             " caso_marcas.CasoNumeroExpedienteLargo, " +
                                             " DATE_FORMAT(caso_marcas.CasoFechaLegal,'%d-%m-%Y') AS CasoFechaLegal, " +
                                             " caso_marcas.CasoNumConcedida, " +
                                             " DATE_FORMAT(caso_marcas.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, " +
                                             " DATE_FORMAT(caso_marcas.CasoFechaVigencia,'%d-%m-%Y') AS CasoFechaVigencia, " +
                                             " DameTipoSolicitudDescrip(caso_marcas.TipoSolicitudId) AS Tipodesolicitud, " +
                                             " DameEstatusCasoDescrip(caso_marcas.EstatusCasoId) AS Estatus, " +
                                             " caso_marcas.CasoTitular, " +
                                             " caso_marcas.CasoTituloespanol, " +
                                             " Dameelusuario(caso_marcas.ResponsableId) AS Responsable, " +
                                             " Get_Prioridad(caso_marcas.CasoId) AS Prioridades, " +
                                             " Get_Interesados(caso_marcas.CasoId) AS Interesados, " +
                                             " Damealcontactocaso (casocliente.contactoid) AS Contactos, " +
                                             " Dameelcorreocontactocaso (casocliente.contactoid) AS Correos, " +
                                             " DameNombrePais(caso_marcas.PaisId) AS Pais, " +
                                             " Damelaclase(caso_marcas.CasoId) as Clase " +
                                         " FROM " +
                                             " caso_marcas, " +
                                             " casocliente, " +
                                             " cliente " +
                                         " WHERE " +
                                             " cliente.ClienteId = " + datoid +
                                               "  AND casocliente.CasoId = caso_marcas.CasoId " +
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
             String kweryregistrodeobraconsulta = "SELECT " +
                                                     " caso_registrodeobra.CasoId, " +
                                                     " DameLaReferencia(caso_registrodeobra.CasoId) AS Referencia, " +
                                                     " caso_registrodeobra.CasoNumeroExpedienteLargo, " +
                                                     " DATE_FORMAT(caso_registrodeobra.CasoFechaLegal,'%d-%m-%Y') AS CasoFechaLegal, " +
                                                     " caso_registrodeobra.CasoNumConcedida, " +
                                                     " DATE_FORMAT(caso_registrodeobra.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, " +
                                                     " DATE_FORMAT(caso_registrodeobra.CasoFechaVigencia,'%d-%m-%Y') AS CasoFechaVigencia,  " +
                                                     " DameTipoSolicitudDescrip(caso_registrodeobra.TipoSolicitudId) AS Tipodesolicitud, " +
                                                     " DameEstatusCasoDescrip(caso_registrodeobra.EstatusCasoId) AS Estatus, " +
                                                     " caso_registrodeobra.CasoTitular,  " +
                                                     " caso_registrodeobra.CasoTituloespanol, " +
                                                     " Dameelusuario(caso_registrodeobra.ResponsableId) AS Responsable, " +
                                                     " Get_Prioridad(caso_registrodeobra.CasoId) AS Prioridades , " +
                                                     " Get_Interesados(caso_registrodeobra.CasoId) AS Interesados, " +
                                                     " Damealcontactocaso (casocliente.contactoid) AS Contactos, " +
                                                     " Dameelcorreocontactocaso (casocliente.contactoid) AS Correos, " +
                                                     " DameNombrePais(caso_registrodeobra.PaisId) AS Pais " +
                                                 " FROM " +
                                                     " caso_registrodeobra, " +
                                                     " casocliente, " +
                                                     " cliente " +
                                                 " WHERE " +
                                                    " cliente.ClienteId = " + datoid +
                                                         " AND casocliente.CasoId = caso_registrodeobra.CasoId " +
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
             String kweryreserva = "SELECT " +
                                     " caso_reservadederechos.CasoId, " +
                                     " DameLaReferencia(caso_reservadederechos.CasoId) AS Referencia, " +
                                     " caso_reservadederechos.CasoNumeroExpedienteLargo, " +
                                     " DATE_FORMAT(caso_reservadederechos.CasoFechaLegal,'%d-%m-%Y') AS CasoFechaLegal, " +
                                     " caso_reservadederechos.CasoNumConcedida, " +
                                     " DATE_FORMAT(caso_reservadederechos.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, " +
                                     " DATE_FORMAT(caso_reservadederechos.CasoFechaVigencia,'%d-%m-%Y') AS CasoFechaVigencia,  " +
                                     " DameTipoSolicitudDescrip(caso_reservadederechos.TipoSolicitudId) AS Tipodesolicitud, " +
                                     " DameEstatusCasoDescrip(caso_reservadederechos.EstatusCasoId) AS Estatus, " +
                                     " caso_reservadederechos.CasoTitular, " +
                                     " caso_reservadederechos.CasoTituloespanol, " +
                                     " Dameelusuario(caso_reservadederechos.ResponsableId) AS Responsable, " +
                                     " Get_Prioridad(caso_reservadederechos.CasoId) AS Prioridades, " +
                                     " Get_Interesados(caso_reservadederechos.CasoId) AS Interesados, " +
                                     " Damealcontactocaso (casocliente.contactoid) AS Contactos, " +
                                     " Dameelcorreocontactocaso (casocliente.contactoid) AS Correos, " +
                                     " DameNombrePais(caso_reservadederechos.PaisId) AS Pais " +
                                 " FROM " +
                                     " caso_reservadederechos, " +
                                     " casocliente, " +
                                     " cliente " +
                                " WHERE " +
                                     " cliente.ClienteId = " + datoid +
                                     "    AND caso_reservadederechos.CasoId = casocliente.CasoId " +
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
             String kwerycontensiosoconsulta = "SELECT " +
                                                 " caso_contencioso.CasoId, " +
                                                 " DameLaReferencia(caso_contencioso.CasoId) AS Referencia, " +
                                                 " caso_contencioso.CasoNumeroExpedienteLargo, " +
                                                 " DATE_FORMAT(caso_contencioso.CasoFechaLegal,'%d-%m-%Y') AS CasoFechaLegal, " +
                                                 " caso_contencioso.CasoNumConcedida, " +
                                                 " DATE_FORMAT(caso_contencioso.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, " +
                                                 " DATE_FORMAT(caso_contencioso.CasoFechaVigencia,'%d-%m-%Y') AS CasoFechaVigencia, " +
                                                 " DameTipoSolicitudDescrip(caso_contencioso.TipoSolicitudId) AS Tipodesolicitud, " +
                                                 " DameEstatusCasoDescrip(caso_contencioso.EstatusCasoId) AS Estatus, " +
                                                 " caso_contencioso.CasoTitular, " +
                                                 " caso_contencioso.CasoTituloespanol, " +
                                                 " Dameelusuario(caso_contencioso.ResponsableId) AS Responsable, " +
                                                 " Get_Prioridad(caso_contencioso.CasoId) AS Prioridades, " +
                                                 " Get_Interesados(caso_contencioso.CasoId) AS Interesados, " +
                                                 " Damealcontactocaso (casocliente.contactoid) AS Contactos, " +
                                                 " Dameelcorreocontactocaso (casocliente.contactoid) AS Correos, " +
                                                 " DameNombrePais(caso_contencioso.PaisId) AS Pais " +
                                             " FROM " +
                                                 " caso_contencioso, " +
                                                 " casocliente, " +
                                                 " cliente " +
                                             " WHERE " +
                                                 " cliente.ClienteId = " + datoid +
                                                   "  AND caso_contencioso.CasoId = casocliente.CasoId " +
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
             String kweryoposicion = "SELECT " +
                                     " caso_oposicion.CasoId, " +
                                     " DameLaReferencia(caso_oposicion.CasoId) AS Referencia, " +
                                     " caso_oposicion.CasoNumeroExpedienteLargo, " +
                                     " DameTipoSolicitudDescrip(caso_oposicion.TipoSolicitudId) AS Tipodesolicitud, " +
                                     " DameEstatusCasoDescrip(caso_oposicion.EstatusCasoId) AS Estatus, " +
                                     " caso_oposicion.CasoTitular, " +
                                     " caso_oposicion.CasoTituloespanol, " +
                                     " Dameelusuario(caso_oposicion.ResponsableId) AS Responsable, " +
                                     " Get_Prioridad(caso_oposicion.CasoId) AS Prioridades, " +
                                     " Get_Interesados(caso_oposicion.CasoId) AS Interesados, " +
                                     " Damealcontactocaso (casocliente.contactoid) AS Contactos, " +
                                     " Dameelcorreocontactocaso (casocliente.contactoid) AS Correos, " +
                                     " DameNombrePais(caso_oposicion.PaisId) AS Pais  " +
                                 " FROM " +
                                     " caso_oposicion, " +
                                     " casocliente, " +
                                     " cliente " +
                                 " WHERE " +
                                     " cliente.ClienteId = " + datoid +
                                      " AND caso_oposicion.CasoId = casocliente.CasoId " +
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

             conect consultacorrespatente = new conect();
             String Kwerycorrespatente = "SELECT " +
                                             " caso_patente.CasoId, " +
                                             " caso_patente.CasoNumeroExpedienteLargo, " +
                                             " DATE_FORMAT(caso_patente.CasoFechaLegal,'%d-%m-%Y') AS CasoFechaLegal, " +
                                             " caso_patente.CasoNumConcedida, " +
                                             " DATE_FORMAT(caso_patente.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, " +
                                             " DameTipoSolicitudDescrip(caso_patente.TipoSolicitudId) AS Tipodesolicitud, " +
                                             " DameEstatusCasoDescrip(caso_patente.EstatusCasoId) AS Estatus, " +
                                             " caso_patente.CasoTitular, " +
                                             " caso_patente.CasoTituloespanol, " +
                                             " Damealcontactocaso (casocorresponsal.ContactoId) AS Contactos, " +
                                             " Dameelcorreocontactocaso (casocorresponsal.ContactoId) AS Correos, " +
                                             " Dameelusuario(caso_patente.ResponsableId) AS Responsable, " +
                                             " DameLaReferencia(caso_patente.CasoId) AS Referencia, " +
                                             " Get_Interesados(caso_patente.CasoId) AS Interesados, " +
                                             " Get_AnualidadFechaLimitePago(caso_patente.CasoId) AS Fechalimitepago, " +
                                             " Get_anualidadespagadas(caso_patente.CasoId) AS Anualidades, " +
                                             " Dameelquinquenio(caso_patente.CasoId) AS Quinquenio, " +
                                             " DameNombrePais(caso_patente.PaisId) AS Pais " +
                                         " FROM " +
                                             " caso_patente, " +
                                             " casocorresponsal, " +
                                             " cliente " +
                                         " WHERE " +
                                            " cliente.ClienteId = " + datoid +
                                                 " AND caso_patente.CasoId = casocorresponsal.CasoId " +
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
             String kwerycorresponsalmarca = "SELECT  " +
                                                 " caso_marcas.CasoId, " +
                                                 " caso_marcas.CasoNumeroExpedienteLargo, " +
                                                 " DATE_FORMAT(caso_marcas.CasoFechaLegal, '%d-%m-%Y') AS CasoFechaLegal, " +
                                                 " caso_marcas.CasoNumConcedida, " +
                                                 " DATE_FORMAT(caso_marcas.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, " +
                                                 " DAMETIPOSOLICITUDDESCRIP(caso_marcas.TipoSolicitudId) AS Tipodesolicitud, " +
                                                 " DAMEESTATUSCASODESCRIP(caso_marcas.EstatusCasoId) AS Estatus, " +
                                                 " caso_marcas.CasoTitular, " +
                                                 " caso_marcas.CasoTituloespanol, " +
                                                 " DAMEALCONTACTOCASO(casocorresponsal.ContactoId) AS Contactos, " +
                                                 " DAMEELCORREOCONTACTOCASO(casocorresponsal.ContactoId) AS Correos, " +
                                                 " DAMEELUSUARIO(caso_marcas.ResponsableId) AS Responsable, " +
                                                 " DAMELAREFERENCIA(caso_marcas.CasoId) AS Referencia, " +
                                                 " GET_INTERESADOS(caso_marcas.CasoId) AS Interesados, " +
                                                 " DAMENOMBREPAIS(caso_marcas.PaisId) AS Pais, " +
                                                 " Damelaclase(caso_marcas.CasoId) AS Clase " +
                                             " FROM " +
                                                 " caso_marcas, " +
                                                 " casocorresponsal, " +
                                                 " cliente " +
                                             " WHERE " +
                                                 " cliente.ClienteId = " + datoid +
                                                   "   AND caso_marcas.CasoId = casocorresponsal.CasoId " +
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
             String kwerycorresregistro = "SELECT " +
                                             " caso_registrodeobra.CasoId, " +
                                             " caso_registrodeobra.CasoNumeroExpedienteLargo, " +
                                             " DATE_FORMAT(caso_registrodeobra.CasoFechaLegal, '%d-%m-%Y') AS CasoFechaLegal, " +
                                             " caso_registrodeobra.CasoNumConcedida, " +
                                             " DATE_FORMAT(caso_registrodeobra.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, " +
                                             " DAMETIPOSOLICITUDDESCRIP(caso_registrodeobra.TipoSolicitudId) AS Tipodesolicitud, " +
                                             " DAMEESTATUSCASODESCRIP(caso_registrodeobra.EstatusCasoId) AS Estatus, " +
                                             " caso_registrodeobra.CasoTitular, " +
                                             " caso_registrodeobra.CasoTituloespanol, " +
                                             " DAMEALCONTACTOCASO(casocorresponsal.ContactoId) AS Contactos, " +
                                             " DAMEELCORREOCONTACTOCASO(casocorresponsal.ContactoId) AS Correos, " +
                                             " DAMEELUSUARIO(caso_registrodeobra.ResponsableId) AS Responsable, " +
                                             " DAMELAREFERENCIA(caso_registrodeobra.CasoId) AS Referencia, " +
                                             " GET_INTERESADOS(caso_registrodeobra.CasoId) AS Interesados, " +
                                             " GET_ANUALIDADFECHALIMITEPAGO(caso_registrodeobra.CasoId) AS Fechalimitepago, " +
                                             " DAMENOMBREPAIS(caso_registrodeobra.PaisId) AS Pais " +
                                         " FROM " +
                                            "  caso_registrodeobra, " +
                                            "  casocorresponsal, " +
                                            "  cliente " +
                                        "  WHERE " +
                                            " cliente.ClienteId = " + datoid +
                                               "  AND caso_registrodeobra.CasoId = casocorresponsal.CasoId " +
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

             String kwerycorerreserva = "SELECT " +
                                         " caso_reservadederechos.CasoId, " +
                                         " caso_reservadederechos.CasoNumeroExpedienteLargo, " +
                                         " DATE_FORMAT(caso_reservadederechos.CasoFechaLegal, '%d-%m-%Y') AS CasoFechaLegal, " +
                                         " caso_reservadederechos.CasoNumConcedida, " +
                                         " DATE_FORMAT(caso_reservadederechos.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, " +
                                         " DAMETIPOSOLICITUDDESCRIP(caso_reservadederechos.TipoSolicitudId) AS Tipodesolicitud, " +
                                         " DAMEESTATUSCASODESCRIP(caso_reservadederechos.EstatusCasoId) AS Estatus, " +
                                         " caso_reservadederechos.CasoTitular, " +
                                         " caso_reservadederechos.CasoTituloespanol, " +
                                         " DAMEALCONTACTOCASO(casocorresponsal.ContactoId) AS Contactos, " +
                                         " DAMEELCORREOCONTACTOCASO(casocorresponsal.ContactoId) AS Correos, " +
                                         " DAMEELUSUARIO(caso_reservadederechos.ResponsableId) AS Responsable, " +
                                         " DAMELAREFERENCIA(caso_reservadederechos.CasoId) AS Referencia, " +
                                         " GET_INTERESADOS(caso_reservadederechos.CasoId) AS Interesados, " +
                                         " GET_ANUALIDADFECHALIMITEPAGO(caso_reservadederechos.CasoId) AS Fechalimitepago, " +
                                         " DAMENOMBREPAIS(caso_reservadederechos.PaisId) AS Pais " +
                                     " FROM " +
                                        " caso_reservadederechos, " +
                                        " casocorresponsal, " +
                                        "  cliente " +
                                     " WHERE " +
                                        " cliente.ClienteId = " + datoid +
                                        "    AND caso_reservadederechos.CasoId = casocorresponsal.CasoId " +
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
             String kwerycontencioso = " SELECT " +
                                         " caso_contencioso.CasoId, " +
                                         " caso_contencioso.CasoNumeroExpedienteLargo, " +
                                         " DATE_FORMAT(caso_contencioso.CasoFechaLegal, '%d-%m-%Y') AS CasoFechaLegal, " +
                                         " caso_contencioso.CasoNumConcedida, " +
                                         " DATE_FORMAT(caso_contencioso.CasoFechaConcesion,'%d-%m-%Y') AS CasoFechaConcesion, " +
                                         " DAMETIPOSOLICITUDDESCRIP(caso_contencioso.TipoSolicitudId) AS Tipodesolicitud, " +
                                         " DAMEESTATUSCASODESCRIP(caso_contencioso.EstatusCasoId) AS Estatus, " +
                                         " caso_contencioso.CasoTitular, " +
                                         " caso_contencioso.CasoTituloespanol, " +
                                         " DAMEALCONTACTOCASO(casocorresponsal.ContactoId) AS Contactos, " +
                                         " DAMEELCORREOCONTACTOCASO(casocorresponsal.ContactoId) AS Correos, " +
                                         " DAMEELUSUARIO(caso_contencioso.ResponsableId) AS Responsable, " +
                                         " DAMELAREFERENCIA(caso_contencioso.CasoId) AS Referencia, " +
                                         " GET_INTERESADOS(caso_contencioso.CasoId) AS Interesados, " +
                                         " GET_ANUALIDADFECHALIMITEPAGO(caso_contencioso.CasoId) AS Fechalimitepago, " +
                                         " DAMENOMBREPAIS(caso_contencioso.PaisId) AS Pais " +
                                     " FROM " +
                                        " caso_contencioso, " +
                                        " casocorresponsal, " +
                                        " cliente " +
                                     " WHERE " +
                                         " cliente.ClienteId = " + datoid +
                                           "  AND caso_contencioso.CasoId = casocorresponsal.CasoId " +
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
             String kwerycorreopos = "SELECT " +
                                         " caso_oposicion.CasoId, " +
                                         " caso_oposicion.CasoNumeroExpedienteLargo, " +
                                         " DAMETIPOSOLICITUDDESCRIP(caso_oposicion.TipoSolicitudId) AS Tipodesolicitud, " +
                                         " DAMEESTATUSCASODESCRIP(caso_oposicion.EstatusCasoId) AS Estatus, " +
                                         " caso_oposicion.CasoTitular, " +
                                         " caso_oposicion.CasoTituloespanol, " +
                                         " DAMEALCONTACTOCASO(casocorresponsal.ContactoId) AS Contactos, " +
                                         " DAMEELCORREOCONTACTOCASO(casocorresponsal.ContactoId) AS Correos, " +
                                         " DAMEELUSUARIO(caso_oposicion.ResponsableId) AS Responsable, " +
                                         " DAMELAREFERENCIA(caso_oposicion.CasoId) AS Referencia, " +
                                         " GET_INTERESADOS(caso_oposicion.CasoId) AS Interesados, " +
                                         " GET_ANUALIDADFECHALIMITEPAGO(caso_oposicion.CasoId) AS Fechalimitepago, " +
                                         " DAMENOMBREPAIS(caso_oposicion.PaisId) AS Pais " +
                                     " FROM " +
                                        "  caso_oposicion, " +
                                        "  casocorresponsal, " +
                                        "  cliente " +
                                     " WHERE " +
                                          " cliente.ClienteId = " + datoid +
                                           "  AND caso_oposicion.CasoId = casocorresponsal.CasoId " +
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
             String kweryusuarioobservaciones = " SELECT usuario.UsuarioId , usuario.UsuarioName FROM usuario where UsuarioIndActivo=1";
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
            String kweryusuariodoc = "SELECT usuario.UsuarioId,  usuario.UsuarioName FROM usuario where UsuarioIndActivo=1";
            MySqlDataReader respuestastringusuariodoc = contecusuariodoc.getdatareader(kweryusuariodoc);
            while (respuestastringusuariodoc.Read())
            {
                CB_usuariosdocumento_cd.Items.Add(validareader("UsuarioName", "UsuarioId", respuestastringusuariodoc));
            }
            respuestastringusuariodoc.Close();
            contecusuariodoc.Cerrarconexion();


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
        private void BT_eliminarcontacto_Click(object sender, EventArgs e)
        {
            try
            {
                if (idcontacto == null)
                {
                    MessageBox.Show("Debes seleccionar un contacto");
                }
                else
                {


                    var confirmResult = MessageBox.Show("¿Seguro que desea ELIMINAR este contacto ?", "Eliminar Contacto", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {

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

        public void actualizatablacontacto()
        {
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

        //contacto cliente
        private void BT_contactonuevoc_ccd_Click(object sender, EventArgs e)
        {
            if (!TB_contactonombrecd.Text.Equals(""))
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
                    String queryinsert = "INSERT INTO `contacto` " +
                        "(`ContactoId`, `ClienteId`, `ContactoNick`, `ContactoNombre`, `ContactoTelefono`, `ContactoEmail`, `ContactoSexo`, `ContactoArea`, `ContactoPuesto`, `ContactoIndAct`, `InteresadoId`, `GrupoId`) " +
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
                                    habilitado +
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
            }
            else
            {
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
        private void BT_limpiarinstruccion_cd_Click(object sender, EventArgs e)
        {
            limpiatablainstruccion();
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
                        String kweryinstruccion = "UPDATE `instruccion`   SET " +
                                                        " `TipoInstruccionId` = " + (CB_instruccionestipo_cd.SelectedItem as ComboboxItem).Value +
                                                        ", `InstrucciondDescip` = '" + RTB_instrucioninst_cd.Text +
                                                        "', `ClienteId` = " + datoid +
                                                        ", `UsuarioId` = " + (CB_usuarioinstrucciones_cd.SelectedItem as ComboboxItem).Value +
                                                        ", `InstruccionFecha` = '" + DTP_fechainstruccion_instruciones_cd.Value.Date.ToString("yyyy-MM-dd HH:mm") +
                                                        "', `InstruccionFechaRec` = '" + DTP_fecharegistro.Value.Date.ToString("yyyy-MM-dd HH:mm") +
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
        public Cliente(Form1 form1contruct, Consulacliente consulacliente)
        {
            // TODO: Complete member initialization
            this.form1contruct = form1contruct;
            this.consulacliente = consulacliente;
        }


        private void update()
        {

            limpiarcasillas();

        }

        private void delete()
        {
            if (MessageBox.Show("Seguro que desea eliminar ?", "Eliminar", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning) == DialogResult.OK)
            {
                listView1.Items.RemoveAt(listView1.SelectedIndices[0]);

                limpiarcasillas();

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
        private void Bagregar_Click(object sender, EventArgs e)
        {

            String fechaactual = DateTime.Now.ToString("yyyy/MM/dd");

            conect coninsert = new conect();

            if (combopais.SelectedItem != null)
            {
                //Se agrega validacionnes
                if (CB_idioma_cliente.SelectedItem == null)
                {
                    MessageBox.Show("El idioma del cliente no puede estar vacio");
                    return;
                }
                if (tipopersona.SelectedItem == null)
                {
                    MessageBox.Show("El Tipo persona no puede ir vacio");
                    return;
                }
                if (CB_cliente_tipo.SelectedItem == null)
                {
                    MessageBox.Show("El Tipo de Cliente no puede ir vacio");
                    return;
                }
                //

                /*
                String Mexico = (CB_interesadoDpais.SelectedItem as ComboboxItem).Value.ToString();
                if ( Mexico == "148" )
                {
                    MessageBox.Show("Los campos de direccion no pueden estar vacios.");
                 
                }*/
                String ClienteId = "";
                String sIdioma = "NULL";
                String sTipoPersona = "NULL";
                String sClienteTipo = "NULL";
                String sComunicacion = "NULL";
                String sTipoEnvio = "NULL";
                String sTipoTarifa = "NULL";
                String sResponsable = "NULL";
                String sMoneda = "NULL";
                String sHolder = "NULL";
                String sProvedorFactura = "NULL";
                try
                {


                    if (CB_idioma_cliente.SelectedItem != null)
                    {
                        sIdioma = (CB_idioma_cliente.SelectedItem as ComboboxItem).Value.ToString();
                    }

                    if (CB_comunicacion.SelectedItem != null)
                    {
                        sComunicacion = (CB_comunicacion.SelectedItem as ComboboxItem).Value.ToString();
                    }

                    if (CB_moneda.SelectedItem != null)
                    {
                        sMoneda = (CB_moneda.SelectedItem as ComboboxItem).Value.ToString();
                    }

                    if (CB_envio.SelectedItem != null)
                    {
                        sTipoEnvio = (CB_envio.SelectedItem as ComboboxItem).Value.ToString();
                    }

                    if (CB_cliente_tipo.SelectedItem != null)
                    {
                        sClienteTipo = (CB_cliente_tipo.SelectedItem as ComboboxItem).Value.ToString();
                    }

                    if (CB_responsable.SelectedItem != null)
                    {
                        sResponsable = (CB_responsable.SelectedItem as ComboboxItem).Value.ToString();
                    }

                    if (CB_proovedor_cliente_fac.SelectedItem != null)
                    {
                        sProvedorFactura = (CB_proovedor_cliente_fac.SelectedItem as ComboboxItem).Value.ToString();
                    }

                    if (CB_holder_cliente.SelectedItem != null)
                    {
                        sHolder = (CB_holder_cliente.SelectedItem as ComboboxItem).Value.ToString();
                    }

                    if (CB_tipo_tarifa.SelectedItem != null)
                    {
                        sTipoTarifa = (CB_tipo_tarifa.SelectedItem as ComboboxItem).Value.ToString();
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

                        conect conect_1 = new conect();

                        String sQresponsable = "select UsuarioName, UsuarioId from usuario where UsuarioId =" + oFormlogin.sId;
                        MySqlDataReader respuresponsable = conect_1.getdatareader(sQresponsable);
                        while (respuresponsable.Read())
                        {
                            Usuario = validareader("UsuarioName", "UsuarioId", respuresponsable).Text;
                        }
                        respuresponsable.Close();
                        conect_1.Cerrarconexion();

                        conect con1 = new conect();
                        String PersonaUtil = TB_nombre_cliente.Text + " " + TB_apellidoclientep.Text + " " + TB_apellidoclientem.Text;


                        String queryinsert = "INSERT INTO `cliente`" +
                    "(`ClienteId`," +
                    "`ClienteClave`," +
                    "`PaisId`," +
                    "`IdNacionalidad`," +
                    "`NombreUtilClient`," +
                    "`ClienteNombre`," +
                    "`ClienteApellidoPaterno`," +
                    "`ClienteApellidoMaterno`," +
                    "`ClienteTipoPersonaSAT`," +
                    "`ClienteRFC`," +
                    "`ClienteCurp`," +
                    "`ClienteVAT`," +
                    "`IdiomaId`," +
                    "`TipoComunicacionId`," +
                    "`TipoClienteId`," +
                    "`MonedaId`," +
                    "`ClienteWebSite`," +
                    "`ClienteEmail`," +
                    "`ProveedorFacElecId`," +
                    "`TipoEnvioFacId`," +
                    "`ClienteObservacion`," +
                    "`ClienteRecMarca`," +
                    "`ClienteRecPatente`," +
                    "`ClienteFechaAlta`," +
                    "`UsuarioCaptura`," +

                    "`TipoTarifaId`," +
                     "`UsuarioFechaCapturo`," +
                    "`HolderId`)" +
                    "VALUES(NULL," +
                          " NULL, '" +
                    "" + (combopais.SelectedItem as ComboboxItem).Value + "', " +
                    "'" + (combopais.SelectedItem as ComboboxItem).Value + "', '" +
                          PersonaUtil +
                          "', '" +
                          TB_nombre_cliente.Text +
                          "','" +
                          TB_apellidoclientep.Text +
                          "','" +
                          TB_apellidoclientem.Text +
                          "','" +
                          TipoP +
                          "', '" +
                          TB_rfc.Text +
                          "', '" +
                          TB_cup.Text +
                          "', NULL," +
                          sIdioma +
                          ", " +
                          sComunicacion +
                          ", " +
                          sClienteTipo +
                          ", " +
                          sMoneda +
                          ", '" +
                          TB_sitiow_cliente.Text +
                          "','" +
                          TB_correo_cliente.Text +
                          "'," +
                          sProvedorFactura +
                          ", " +
                          sTipoEnvio +
                          ", '" +
                          TB_observaciones_cliente.Text +
                          "', NULL, NULL, '" +
                          fechaactual + "'," +
                          sResponsable +
                          ",'" +
                          sTipoTarifa +
                          "','" +
                           fechaactual + "'," +
                          sHolder + " );";
                        MySqlDataReader respuestastringinsert = coninsert.getdatareader(queryinsert);
                        if (respuestastringinsert == null)
                        {
                            MessageBox.Show("Error al intentar insertar el cliente, verifique los campos.");
                        }
                        else
                        {

                            MessageBox.Show("Se creo el cliente correctamente");
                            //Se agrega codigo para agregar domicilio





                            listView12.Visible = true;
                            listView13.Visible = true;
                            listView1.Visible = false;
                            listView14.Visible = false;



                            //se selecciona la informacion del cliente que se acaba de crear para mostrarla
                            respuestastringinsert.Close();
                            coninsert.Cerrarconexion();
                            conect conlista = new conect();
                            String Querylista = "SELECT " +
                                                  "cliente.ClienteId, " +
                                                  "cliente.ClienteNombre, " +
                                                  "cliente.ClienteApellidoPaterno, " +
                                                  "cliente.ClienteApellidoMaterno, " +
                                                  "DameelIdioma(cliente.IdiomaId) AS Idioma, " +
                                                  "cliente.ClienteTipoPersonaSAT, " +
                                                  "cliente.ClienteRFC, " +
                                                  "cliente.ClienteCurp, " +
                                                  "Damelasociedad(cliente.TipoClienteId) AS TipoCliente, " +
                                                  "cliente.ClienteEmail, " +
                                                  "Dameeltipodecomunicacion(cliente.TipoComunicacionId) AS TipoComunicacion, " +
                                                  "Dameeltipodeenvio(cliente.TipoEnvioFacId) AS TipoEnvio,  " +
                                                  "Dametipotarifa (cliente.TipoTarifaId) AS TipoFactura, " +
                                                  "Dameelusuario (cliente.ResponsableId) AS Responsable, " +
                                                  "cliente.ClienteWebSite, " +
                                                  "Damelamoneda (cliente.MonedaId) AS Moneda, " +
                                                  "Dameelholder (cliente.HolderId) AS Holder, " +
                                                  "Dametipofactura  (cliente.ProveedorFacElecId) AS FacturaElectronica, " +
                                                  "cliente.ClienteObservacion " +
                                                  "FROM cliente order by ClienteId desc limit 1";
                            MySqlDataReader respuestlistaaqwery = conlista.getdatareader(Querylista);

                            if (respuestlistaaqwery != null)
                            {
                                while (respuestlistaaqwery.Read())
                                {
                                    //ListViewItem listaitems = new ListViewItem(validareader("PaisClave", "CasoId", respuestastring3).Text);
                                    ListViewItem listaagregar2 = new ListViewItem(validareader("ClienteId", "ClienteId", respuestlistaaqwery).Text);//cliente id
                                    listaagregar2.SubItems.Add(validareader("ClienteNombre", "ClienteId", respuestlistaaqwery).Text);// NOMBRE
                                    listaagregar2.SubItems.Add(validareader("ClienteApellidoPaterno", "ClienteId", respuestlistaaqwery).Text);// NOMBRE
                                    listaagregar2.SubItems.Add(validareader("ClienteApellidoMaterno", "ClienteId", respuestlistaaqwery).Text);// NOMBRE
                                    ListViewItem listaagregar = new ListViewItem(validareader("Idioma", "ClienteId", respuestlistaaqwery).Text);  //idioma***
                                    listaagregar.SubItems.Add(validareader("ClienteTipoPersonaSAT", "ClienteId", respuestlistaaqwery).Text);  //tipopersona***
                                    listaagregar.SubItems.Add(validareader("TipoCliente", "ClienteId", respuestlistaaqwery).Text);  // cliente tipo
                                    listaagregar.SubItems.Add(validareader("TipoFactura", "ClienteId", respuestlistaaqwery).Text); // tipo tarifa
                                    listaagregar.SubItems.Add(validareader("Moneda", "ClienteId", respuestlistaaqwery).Text); // moneda
                                    listaagregar.SubItems.Add(validareader("Holder", "ClienteId", respuestlistaaqwery).Text); // holder
                                    listaagregar.SubItems.Add(validareader("TipoComunicacion", "ClienteId", respuestlistaaqwery).Text); // comunicacion
                                    listaagregar.SubItems.Add(validareader("TipoEnvio", "ClienteId", respuestlistaaqwery).Text); // tipoenvio
                                    listaagregar.SubItems.Add(validareader("ClienteRFC", "ClienteId", respuestlistaaqwery).Text); // rfc
                                    listaagregar.SubItems.Add(validareader("ClienteCurp", "ClienteId", respuestlistaaqwery).Text); // curp

                                    listaagregar.SubItems.Add(validareader("ClienteEmail", "ClienteId", respuestlistaaqwery).Text); // correo



                                    //listaagregar.SubItems.Add(validareader("FacturaElectronica", "ClienteId", respuestlistaaqwery).Text); // tipo factura
                                    listaagregar.SubItems.Add(validareader("ClienteWebSite", "ClienteId", respuestlistaaqwery).Text); // sitio web
                                    listaagregar.SubItems.Add(validareader("ClienteEmail", "ClienteId", respuestlistaaqwery).Text); // correo

                                    listaagregar.SubItems.Add(Usuario); // responsable
                                    listaagregar.SubItems.Add(validareader("ClienteObservacion", "ClienteId", respuestlistaaqwery).Text); // observaciones
                                    listView12.Items.Add(listaagregar);
                                    listView13.Items.Add(listaagregar2);
                                    this.listView12.FullRowSelect = true;
                                    this.listView13.FullRowSelect = true;


                                }
                                respuestlistaaqwery.Close();
                                conlista.Cerrarconexion();
                                limpiarcasillas();
                            }

                            DialogResult results = MessageBox.Show("¿Desea agregar Domicilio?", "Cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (results == DialogResult.Yes)
                            {
                                //code for Yes
                                tabControl1.SelectedIndex = 1;
                                tabControl1.Show();
                            }
                            else if (results == DialogResult.No)
                            {
                                //code for No


                            }

                        }
                        respuestastringinsert.Close();


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

                        listView1.Visible = true;
                        listView12.Visible = false;
                        listView13.Visible = false;
                        listView14.Visible = true;
                        conect conect_1 = new conect();

                        String sQresponsable = "select UsuarioName, UsuarioId from usuario where UsuarioId =" + oFormlogin.sId;
                        MySqlDataReader respuresponsable = conect_1.getdatareader(sQresponsable);
                        while (respuresponsable.Read())
                        {
                            Usuario = validareader("UsuarioName", "UsuarioId", respuresponsable).Text;
                        }
                        respuresponsable.Close();
                        conect_1.Cerrarconexion();

                        conect con1 = new conect();
                        String PersonaUtil = TB_nombre_cliente.Text + " " + TB_apellidoclientep.Text + " " + TB_apellidoclientem.Text;


                        String queryinsert = "INSERT INTO `cliente`" +
                        "(`ClienteId`," +
                        "`ClienteClave`," +
                        "`PaisId`," +
                        "`IdNacionalidad`," +
                        "`NombreUtilClient`," +
                        "`RazonSocialClient`," +
                        "`ClienteTipoPersonaSAT`," +
                        "`ClienteRFC`," +
                        "`ClienteCurp`," +
                        "`ClienteVAT`," +
                        "`IdiomaId`," +
                        "`TipoComunicacionId`," +
                        "`TipoClienteId`," +
                        "`MonedaId`," +
                        "`ClienteWebSite`," +
                        "`ClienteEmail`," +
                        "`ProveedorFacElecId`," +
                        "`TipoEnvioFacId`," +
                        "`ClienteObservacion`," +
                        "`ClienteRecMarca`," +
                        "`ClienteRecPatente`," +
                        "`ClienteFechaAlta`," +
                        "`UsuarioCaptura`," +
                        "`TipoTarifaId`," +
                        "`UsuarioFechaCapturo`," +
                        "`HolderId`)" +
                        "VALUES(NULL," +
                              " NULL, '" +
                        "" + (combopais.SelectedItem as ComboboxItem).Value + "', " +
                        "'" + (combopais.SelectedItem as ComboboxItem).Value + "', '" +
                              tbNombreEmpresa.Text +
                              "','" +
                              tbNombreEmpresa.Text +
                              "','" +
                              TipoP +
                              "', '" +
                              TB_rfc.Text +
                              "', '" +
                              TB_cup.Text +
                              "', NULL," +
                              sIdioma +
                              ", " +
                              sComunicacion +
                              ", " +
                              sClienteTipo +
                              ", " +
                              sMoneda +
                              ", '" +
                              TB_sitiow_cliente.Text +
                              "','" +
                              TB_correo_cliente.Text +
                              "'," +
                              sProvedorFactura +
                              ", " +
                              sTipoEnvio +
                              ", '" +
                              TB_observaciones_cliente.Text +
                              "', NULL, NULL, '" +
                              fechaactual + "'," +
                              sResponsable +
                              ",'" +
                              sTipoTarifa +
                                                            "','" +
                               fechaactual + "'," +
                              sHolder + " );";
                        MySqlDataReader respuestastringinsert = coninsert.getdatareader(queryinsert);
                        if (respuestastringinsert == null)
                        {
                            MessageBox.Show("Error al intentar insertar el cliente, verifique los campos.");
                        }
                        else
                        {
                            //PErwsonal moral domicilio






                            //

                            MessageBox.Show("Se agrego el cliente correctamente");
                            respuestastringinsert.Close();
                            coninsert.Cerrarconexion();
                            conect conlista = new conect();
                            String Querylista = "SELECT " +
                                                  "cliente.ClienteId, " +
                                                  "cliente.NombreUtilClient, " +
                                                  "cliente.ClienteApellidoPaterno, " +
                                                  "cliente.ClienteApellidoMaterno, " +
                                                  "DameelIdioma(cliente.IdiomaId) AS Idioma, " +
                                                  "cliente.ClienteTipoPersonaSAT, " +
                                                  "cliente.ClienteRFC, " +
                                                  "cliente.ClienteCurp, " +
                                                  "Dameelclientetipo(cliente.TipoClienteId) AS TipoCliente, " +
                                                  "cliente.ClienteEmail, " +
                                                  "Dameeltipodecomunicacion(cliente.TipoComunicacionId) AS TipoComunicacion, " +
                                                  "Dameeltipodeenvio(cliente.TipoEnvioFacId) AS TipoEnvio,  " +
                                                  "Dametipotarifa (cliente.TipoTarifaId) AS TipoFactura, " +
                                                  "Dameelusuario (cliente.ResponsableId) AS Responsable, " +
                                                  "cliente.ClienteWebSite, " +
                                                  "Damelamoneda (cliente.MonedaId) AS Moneda, " +
                                                  "Dameelholder (cliente.HolderId) AS Holder, " +
                                                  "Dametipofactura  (cliente.ProveedorFacElecId) AS FacturaElectronica, " +
                                                  "cliente.ClienteObservacion " +
                                                  "FROM cliente order by ClienteId desc limit 1";
                            MySqlDataReader respuestlistaaqwery = conlista.getdatareader(Querylista);

                            if (respuestlistaaqwery != null)
                            {
                                while (respuestlistaaqwery.Read())
                                {

                                    //ListViewItem listaitems = new ListViewItem(validareader("PaisClave", "CasoId", respuestastring3).Text);
                                    ListViewItem listaagregar2 = new ListViewItem(validareader("ClienteId", "ClienteId", respuestlistaaqwery).Text);//cliente id
                                    listaagregar2.SubItems.Add(validareader("NombreUtilClient", "ClienteId", respuestlistaaqwery).Text);// NOMBRE
                                    ListViewItem listaagregar = new ListViewItem(validareader("Idioma", "ClienteId", respuestlistaaqwery).Text);  //idioma***
                                    listaagregar.SubItems.Add(validareader("ClienteTipoPersonaSAT", "ClienteId", respuestlistaaqwery).Text);  //tipopersona***
                                    listaagregar.SubItems.Add(validareader("TipoCliente", "ClienteId", respuestlistaaqwery).Text);  // cliente tipo
                                    listaagregar.SubItems.Add(validareader("TipoFactura", "ClienteId", respuestlistaaqwery).Text); // tipo tarifa
                                    listaagregar.SubItems.Add(validareader("Moneda", "ClienteId", respuestlistaaqwery).Text); // moneda
                                    listaagregar.SubItems.Add(validareader("Holder", "ClienteId", respuestlistaaqwery).Text); // holder
                                    listaagregar.SubItems.Add(validareader("TipoComunicacion", "ClienteId", respuestlistaaqwery).Text); // comunicacion
                                    listaagregar.SubItems.Add(validareader("TipoEnvio", "ClienteId", respuestlistaaqwery).Text); // tipoenvio
                                    listaagregar.SubItems.Add(validareader("ClienteRFC", "ClienteId", respuestlistaaqwery).Text); // rfc
                                    listaagregar.SubItems.Add(validareader("ClienteCurp", "ClienteId", respuestlistaaqwery).Text); // curp

                                    listaagregar.SubItems.Add(validareader("ClienteEmail", "ClienteId", respuestlistaaqwery).Text); // correo



                                    //listaagregar.SubItems.Add(validareader("FacturaElectronica", "ClienteId", respuestlistaaqwery).Text); // tipo factura
                                    listaagregar.SubItems.Add(validareader("ClienteWebSite", "ClienteId", respuestlistaaqwery).Text); // sitio web

                                    listaagregar.SubItems.Add(Usuario); // responsable
                                    listaagregar.SubItems.Add(validareader("ClienteObservacion", "ClienteId", respuestlistaaqwery).Text); // observaciones
                                    listView1.Items.Add(listaagregar);
                                    this.listView1.FullRowSelect = true;
                                    listView14.Items.Add(listaagregar2);
                                    this.listView14.FullRowSelect = true;


                                }
                                respuestlistaaqwery.Close();
                                conlista.Cerrarconexion();
                                limpiarcasillas();
                            }

                            DialogResult results = MessageBox.Show("¿Desea agregar Domicilio?", "Cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (results == DialogResult.Yes)
                            {
                                //code for Yes
                                tabControl1.SelectedIndex = 1;
                                tabControl1.Show();

                            }
                            else if (results == DialogResult.No)
                            {
                                //code for No


                            }


                        }
                        respuestastringinsert.Close();


                    }
                }
                catch (Exception E)
                {
                    //escribimos en log
                    Console.WriteLine("{0} Exception caught.", E);
                    MessageBox.Show("Fallo " + E);
                }
            }
            else
            {
                MessageBox.Show("Debe llenar el pais");
            }



        }



        private void Bmodificar_Click(object sender, EventArgs e)
        {
            update();
        }

        private void Beliminar_Click(object sender, EventArgs e)
        {
            delete();

            limpiarcasillas();
        }

        private void bt1_Click(object sender, EventArgs e)
        {

            //Fcontacto fcontacto = new Fcontacto(oFormlogin, this);
            //fcontacto.Show();
            //this.Hide();

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
            respuestastrings.Close();
            cons.Cerrarconexion();
            conect con8 = new conect();
            String kwery8 = "SELECT proveedorfacelec.ProveedorFacElecId , proveedorfacelec.ProveedorFacElecDescrip FROM  proveedorfacelec where ProveedorFacElecId=3 ";
            MySqlDataReader respuestastring8 = con8.getdatareader(kwery8);
            while (respuestastring8.Read())
            {
                CB_proovedor_cliente_fac.SelectedIndex = CB_proovedor_cliente_fac.Items.Add(validareader("ProveedorFacElecDescrip", "ProveedorFacElecId", respuestastring8));
            }
            respuestastring8.Close();
            con8.Cerrarconexion();

            conect con5 = new conect();
            String kwery5 = "SELECT tipoenviofac.TipoEnvioFacId , tipoenviofac.TipoEnvioFacDescrip FROM  tipoenviofac where TipoEnvioFacId=3";
            MySqlDataReader respuestastring5 = con5.getdatareader(kwery5);
            while (respuestastring5.Read())
            {
                CB_envio.SelectedIndex = CB_envio.Items.Add(validareader("TipoEnvioFacDescrip", "TipoEnvioFacId", respuestastring5));
            }
            respuestastring5.Close();
            con5.Cerrarconexion();

            conect con3 = new conect();
            String kwery3 = "SELECT tipocomunicacion.TipoComunicacionId , tipocomunicacion.TipoComunicacionDescrip  FROM  tipocomunicacion where TipoComunicacionId=1";
            MySqlDataReader respuestastring3 = con3.getdatareader(kwery3);
            while (respuestastring3.Read())
            {
                CB_comunicacion.SelectedIndex = CB_comunicacion.Items.Add(validareader("TipoComunicacionDescrip", "TipoComunicacionId", respuestastring3));
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
                CB_interesadoDpais.SelectedIndex =  CB_interesadoDpais.Items.Add(validareader("PaisNombre", "PaisId", respuestastringss));
            }
            respuestastringss.Close();
            consp.Cerrarconexion();

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


                conect conect4 = new conect();
                String kwery4 = "SELECT moneda.MonedaId , moneda.MonedaDescrip FROM  moneda where MonedaId=2";
                MySqlDataReader respuestastring4 = conect4.getdatareader(kwery4);
                while (respuestastring4.Read())
                {
                    CB_moneda.SelectedIndex = CB_moneda.Items.Add(validareader("MonedaDescrip", "MonedaId", respuestastring4));
                }
                respuestastring4.Close();
                conect4.Cerrarconexion();
                label32.Visible = true;
                TB_interesadoDcolonia.Visible = true;
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

                label32.Visible = false;
                TB_interesadoDcolonia.Visible = false;
            }
            //Si el pais es alguno de estos IDPais entonces el valor predeterminado sera ingles
            if (valor == "45" || valor == "213" || valor == "74" || valor == "104")
            {
                String kwery2 = "SELECT * FROM  idioma where IdiomaId=1";
                MySqlDataReader respuestastring2 = con.getdatareader(kwery2);
                while (respuestastring2.Read())
                {
                    CB_idioma_cliente.SelectedIndex = CB_idioma_cliente.Items.Add(validareader("IdiomaDescripcion", "IdiomaId", respuestastring2));
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
                    CB_idioma_cliente.SelectedIndex = CB_idioma_cliente.Items.Add(validareader("IdiomaDescripcion", "IdiomaId", respuestastring2));
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
                    CB_idioma_cliente.Items.Add(validareader("IdiomaDescripcion", "IdiomaId", respuestastring2));
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

        public void limpiarcasillas()
        {

            TB_rfc.Text = "";
            TB_nombre_cliente.Text = "";
            TB_apellidoclientep.Text = "";
            TB_apellidoclientem.Text = "";
            TB_cup.Text = "";
            TB_correo_cliente.Text = "";
            TB_sitiow_cliente.Text = "";
            CB_idioma_cliente.Text = "Seleccione";
            CB_comunicacion.Text = "Seleccione";
            CB_envio.Text = "Seleccione";
            CB_moneda.Text = "Seleccione";
            CB_cliente_tipo.Text = "Seleccione";
            CB_responsable.Text = "Seleccione";
            CB_proovedor_cliente_fac.Text = "Seleccione";
            TB_observaciones_cliente.Text = "";
            CB_holder_cliente.Text = "Seleccione";
            CB_tipo_tarifa.Text = "Seleccione";
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

        private void button2_Click(object sender, EventArgs e)
        {
            capFormcap.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            capFormcap.Show();
            this.Close();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            String datocliente = listView1.SelectedItems[0].SubItems[0].Text;
            if (!listView1.SelectedItems[0].SubItems[0].Equals(null))
            {

                Fclientedetalle detalle = new Fclientedetalle(datocliente, form1contruct, capFormcap);
                detalle.Show();
                this.Hide();

            }
        }

        private void fisica_Enter(object sender, EventArgs e)
        {

        }

        private void TB_interesadoDestado_TextChanged(object sender, EventArgs e)
        {

        }
        public void direccionlistview(){

            conect conlista2 = new conect();
            String Querylista2 = "SELECT " +
                                  "direccion.DireccionCalle, " +
                                   "direccion.ClienteId, " +
                                  "direccion.DireccionNumExt, " +
                                  "direccion.DireccionNumInt, " +
                                  "direccion.DireccionColonia, " +
                                  "direccion.DireccionMunicipio, " +
                                  "direccion.DireccionCP, " +
                                  "direccion.DireccionEstado, " +
                                  "direccion.PaisId, " +
                                  "direccion.DireccionID " +
                                  "FROM direccion order by ClienteId desc limit 1";
            MySqlDataReader respuestlistaaqwery2 = conlista2.getdatareader(Querylista2);

            if (respuestlistaaqwery2 != null)
            {
                while (respuestlistaaqwery2.Read())
                {
                    String Pais = validareader("PaisId", "DireccionID", respuestlistaaqwery2).Text;
                    conect conect_1 = new conect();

                    String rpais_q = "select PaisNombre, PaisId from pais where PaisId =" + Pais;
                    MySqlDataReader rpais = conect_1.getdatareader(rpais_q);
                    while (rpais.Read())
                    {
                        rpaiss = validareader("PaisNombre", "PaisId", rpais).Text;
                    }
                    rpais.Close();
                    conect_1.Cerrarconexion();
                    //ListViewItem listaitems = new ListViewItem(validareader("PaisClave", "CasoId", respuestastring3).Text);
                    ListViewItem listaagregar2 = new ListViewItem(validareader("ClienteId", "DireccionID", respuestlistaaqwery2).Text);//cliente id
                    listaagregar2.SubItems.Add(validareader("DireccionCalle", "DireccionID", respuestlistaaqwery2).Text);// NOMBRE
                    listaagregar2.SubItems.Add(validareader("DireccionNumExt", "DireccionID", respuestlistaaqwery2).Text);// apeliido paterno
                    listaagregar2.SubItems.Add(validareader("DireccionNumInt", "DireccionID", respuestlistaaqwery2).Text);
                    listaagregar2.SubItems.Add(validareader("DireccionColonia", "DireccionID", respuestlistaaqwery2).Text);
                    listaagregar2.SubItems.Add(validareader("DireccionMunicipio", "DireccionID", respuestlistaaqwery2).Text);
                    listaagregar2.SubItems.Add(validareader("DireccionEstado", "DireccionID", respuestlistaaqwery2).Text);
                    // apellido materno
                    listaagregar2.SubItems.Add(validareader("DireccionCP", "ClienteId", respuestlistaaqwery2).Text);  //idioma***
                    listaagregar2.SubItems.Add(rpaiss); // rfc

                    listView2.Items.Add(listaagregar2);
                    this.listView2.FullRowSelect = true;


                }
                respuestlistaaqwery2.Close();
                conlista2.Cerrarconexion();
                limpiarcasillas();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            String ClienteId = "";
            conect conectinteresado = new conect();
            String query2 = "SELECT ClienteId FROM Cliente order by ClienteId DESC  limit 1 ";
            MySqlDataReader respuestastring20 = conectinteresado.getdatareader(query2);
            if (CB_interesadoDpais.SelectedItem != null)
            {
                string valor = (CB_interesadoDpais.SelectedItem as ComboboxItem).Value.ToString();
                if (valor == "148") 
                {

                    if (respuestastring20 != null)
                    {

                        respuestastring20.Read();

                        ClienteId = validareader("ClienteId", "ClienteId", respuestastring20).Text;

                        conect conectinsert2 = new conect();
                        String DireccionUtil = TB_interesadoDcalle.Text.Replace("'", "´") + " " + TB_interesadoDnumext.Text.Replace("'", "´") + " " + TB_interesadoDnumint.Text.Replace("'", "´") + " " + TB_interesadoDcolonia.Text.Replace("'", "´") + " " + TB_interesadoDpoblacion.Text.Replace("'", "´") + TB_interesadoDestado.Text.Replace("'", "´");
                        String kweryinsert2 = "INSERT INTO `direccion` " +
                                                        " (`DireccionID`, " +
                                                        " `DireccionUtil`, " +
                                                        " `DireccionCalle`, " +
                                                        " `DireccionNumExt`, " +
                                                        " `DireccionNumInt`, " +
                                                        " `DireccionColonia`, " +
                                                        " `DireccionMunicipio`, " +
                                                        " `DireccionEstado`, " +
                                                        " `DireccionCP`, " +
                                                        " `DireccionIndAct`, " +
                                                        " `PaisId`, " +
                                                        " `ClienteId`) " +

                                                        " VALUES " +
                                                        "(NULL,'" +
                                                        DireccionUtil +
                                                        "','" +
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
                                                         ClienteId + ");";
                        MySqlDataReader respuestastringinsert2 = conectinsert2.getdatareader(kweryinsert2);
                        if (respuestastringinsert2 == null)
                        {
                            MessageBox.Show("No se pudo agregar un nuevo cliente domicilio (direccion), Verifique los datos de la dirección");
                            new filelog("error: ", " direccion: ->" + kweryinsert2);
                        }
                        else
                        {

                            respuestastringinsert2.Close();
                            conectinsert2.Cerrarconexion();
                        }


                        respuestastring20.Close();
                        conectinteresado.Cerrarconexion();
                        
                    }
                    direccionlistview();
                }
                else
                {
                    if (respuestastring20 != null)
                    {

                        respuestastring20.Read();

                        ClienteId = validareader("ClienteId", "ClienteId", respuestastring20).Text;

                        conect conectinsert2 = new conect();
                        String DireccionUtil = TB_interesadoDcalle.Text.Replace("'", "´") + " " + TB_interesadoDnumext.Text.Replace("'", "´") + " " + TB_interesadoDnumint.Text.Replace("'", "´") + " " + TB_interesadoDcolonia.Text.Replace("'", "´") + " " + TB_interesadoDpoblacion.Text.Replace("'", "´") + TB_interesadoDestado.Text.Replace("'", "´");
                        String kweryinsert2 = "INSERT INTO `direccion` " +
                                                        " (`DireccionID`, " +
                                                        " `DireccionUtil`, " +
                                                        " `DireccionCalle`, " +
                                                        " `DireccionNumExt`, " +
                                                        " `DireccionNumInt`, " +
                                                        //" `DireccionColonia`, " +
                                                        " `Direcciudad`, " +
                                                        " `DireccionEstado`, " +
                                                        " `DireccionCP`, " +
                                                        " `DireccionIndAct`, " +
                                                        " `PaisId`, " +
                                                        " `ClienteId`) " +
                                                        " VALUES " +
                                                        "(NULL,'" +
                                                        DireccionUtil +
                                                        "','" +
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
                                                         ClienteId + ");";
                        MySqlDataReader respuestastringinsert2 = conectinsert2.getdatareader(kweryinsert2);
                        if (respuestastringinsert2 == null)
                        {
                            MessageBox.Show("No se pudo agregar un nuevo cliente domicilio (direccion), Verifique los datos de la dirección");
                            new filelog("error: ", " direccion: ->" + kweryinsert2);
                        }
                        else
                        {

                            respuestastringinsert2.Close();
                            conectinsert2.Cerrarconexion();
                        }


                        respuestastring20.Close();
                        conectinteresado.Cerrarconexion();

                    }
                    direccionlistview();
                }
            }
        }
    }

}