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
    public partial class addClientenuevo : Form
    {
        public Form1 login;
        Form fRemitentecasonuevo;
        public ComboboxItem Cliente{ get; set;}
        public ComboboxItem Contacto { get; set; }
        public String Contactocorreo { get; set; }
        public addClientenuevo(Form objremitente, Form1 fLogin, int Tiposolicitud)
        {
            fRemitentecasonuevo = objremitente;
            InitializeComponent();


            login = fLogin;
            String IDUsuario = login.sId;
            fisica.Enabled = false;
            moral.Enabled = false;
            conect rcpais = new conect();
            String pais = "SELECT * FROM  pais where PaisNombre not like'%OFICINA%' AND  PaisNombre not like'%ORGANIZACION%'";
            MySqlDataReader rpais = rcpais.getdatareader(pais);
            while (rpais.Read())
            {
                combopais.Items.Add(validareader("PaisClave", "PaisId", rpais));
                //CB_idioma_cliente.Items
            }
            rpais.Close();
            rcpais.Cerrarconexion();

            conect con1 = new conect();
            String paisnombre = "SELECT concat_ws('--',PaisClave,PaisNombre) as PaisNombre,PaisId  FROM  pais where PaisNombre not like'%OFICINA%' AND  PaisNombre not like'%ORGANIZACION%'";
            MySqlDataReader rpaiss = con1.getdatareader(paisnombre);
            while (rpaiss.Read())
            {
                PaisNombre.Items.Add(validareader("PaisNombre", "PaisId", rpaiss));
                //CB_idioma_cliente.Items
            }
            rpaiss.Close();
            con1.Cerrarconexion();

            conect conectMoneda = new conect();
            //Agregamos lista de monedas
            String kwery4 = "SELECT * FROM  moneda";
            MySqlDataReader respuestastring4 = conectMoneda.getdatareader(kwery4);
            while (respuestastring4.Read())
            {
                cbModena.Items.Add(validareader("MonedaDescrip", "MonedaId", respuestastring4));
            }
            respuestastring4.Close();
            conectMoneda.Cerrarconexion();
            conect tipoccliente = new conect();
            //Agregamos tipos de cliente
            String kwery6 = "SELECT * FROM  tipocliente";
            MySqlDataReader respuestastring6 = tipoccliente.getdatareader(kwery6);
            while (respuestastring6.Read())
            {
                cbTipocliente.Items.Add(validareader("TipoClienteDescrip", "TipoClienteId", respuestastring6));
            }
            respuestastring6.Close();
            tipoccliente.Cerrarconexion();
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

        private void button1_Click(object sender, EventArgs e)//cerrar las conexiones
        {
            String fechaactual = DateTime.Now.ToString("yyyy/MM/dd");
            String sResponsable = "NULL";
            try {
                String TipoP = "";
                if ((cbIdioma.SelectedItem as ComboboxItem).Text != ""
                    && (cbModena.SelectedItem as ComboboxItem).Text != ""
                    && (cbTipocliente.SelectedItem as ComboboxItem).Text != "")

                {
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

                        conect con1 = new conect();
                        String PersonaUtil = fnombre.Text + fpaterno.Text + fmaterno.Text;
                    String sQueryInsert = "INSERT INTO `cliente` (`ClienteId`, " +
                                          "`ClienteClave`, " +
                                          "`PaisId`, " +
                                          "`NombreUtilClient`, " +
                                          "`RazonSocialClient`, " +
                                          "`IdiomaId`, " +
                                          "`TipoClienteId`, " +
                                          "`MonedaId`, " +
                                          "`ClienteTipoPersonaSAT`, " +
                                          "`ClienteNombre`, " +
                                          "`ClienteApellidoPaterno`, " +
                                          "`ClienteApellidoMaterno`, " +
                                          "`IDNacionalidad`, " +
                                          "`ClienteFechaAlta`, " +
                                          "`ClienteFechaAlta`," +
                                          "`UsuarioCaptura`," +
                                          "`ResponsableId`) " +
                                          "VALUES (NULL, " +
                                          "'" + tbClavecliente.Text + "', " +
                                          "'" + (combopais.SelectedItem as ComboboxItem).Value + "', " +
                                          "'" + PersonaUtil + "', " +
                                          "'" + PersonaUtil + "', " +
                                          "'" + (cbIdioma.SelectedItem as ComboboxItem).Value + "', " +
                                          "'" + (cbTipocliente.SelectedItem as ComboboxItem).Value + "', " +
                                          "'" + (cbModena.SelectedItem as ComboboxItem).Value + "', " +
                                          "'" + TipoP + "', " +
                                            "'" + fnombre.Text + "', " +
                                           "'" + fpaterno.Text + "', " +
                                            "'" + fmaterno.Text + "', " +

                                          "'" + (Nacionalidad.SelectedItem as ComboboxItem).Value + "', " +
                                         // "'" + (combopais.SelectedItem as ComboboxItem).Value + "', " +
                                          "now(), " +
                                           fechaactual + "'," +
                                            login.sId +
                                            "," +
                                          "'1');";
                    MySqlDataReader respuestastring6 = con1.getdatareader(sQueryInsert);
                    if (respuestastring6 != null) {
                        respuestastring6.Close();
                        String squerymaxId = "select * from cliente order by clienteid desc limit 1;";
                        String sClienteidmax = "";
                        MySqlDataReader resp_masixcliente = con1.getdatareader(squerymaxId);
                        while (resp_masixcliente.Read())
                        {
                            sClienteidmax = validareader("clienteid", "clienteid", resp_masixcliente).Text;
                        }
                        resp_masixcliente.Close();

                        if (sClienteidmax != "" && tbNombrecontacto.Text != "")//si se inserto y se pudo consultar entonces hacemos el insert del nuevo contacto con el id del cliente que acabamos de agregar
                        {
                            String sQueryinsertcontacto = "INSERT INTO `contacto` (" +
                                                          "`ContactoId`, " +
                                                          "`ClienteId`, " +
                                                          "`ContactoNick`, " +
                                                          "`ContactoNombre`, " +
                                                          "`ContactoTelefono`, " +
                                                          "`ContactoEmail`, " +
                                                          "`ContactoSexo`, " +
                                                          "`ContactoArea`, " +
                                                          "`ContactoPuesto`, " +
                                                          "`ContactoIndAct`, " +
                                                          "`InteresadoId`, " +
                                                          "`GrupoId`) VALUES (NULL, " +
                                                          "'" + sClienteidmax + "', " +
                                                          "'" + tbNicknamecontacto.Text + "', " +
                                                          "'" + tbNombrecontacto.Text + "', " +
                                                          "'" + tbTelefonocontacto.Text + "', " +
                                                          "'" + tbCorreocontacto.Text + "', " +
                                                          "NULL, " +
                                                          "NULL, " +
                                                          "NULL, " +
                                                          "'1', " +
                                                          "NULL, " +
                                                          "NULL);";
                            MySqlDataReader resp_insertcontacto = con1.getdatareader(sQueryinsertcontacto);
                            if (resp_insertcontacto != null)
                            {
                                String squerymaxcontactoId = "select * from contacto order by ContactoId desc limit 1;";
                                String sContactoidmax = "";
                                MySqlDataReader resp_masixcontacto = con1.getdatareader(squerymaxcontactoId);
                                while (resp_masixcontacto.Read())
                                {
                                    sContactoidmax = validareader("ContactoId", "ContactoId", resp_masixcontacto).Text;
                                }
                                resp_masixcontacto.Close();
                                //sContactoidmax
                                ComboboxItem combocliente = new ComboboxItem();
                                combocliente.Text = tbNombreEmpresa.Text;
                                combocliente.Value = sClienteidmax;
                                ComboboxItem combocontacto = new ComboboxItem();
                                combocontacto.Text = tbNombrecontacto.Text;
                                combocontacto.Value = sContactoidmax;

                                Contactocorreo = tbCorreocontacto.Text;
                                Cliente = combocliente;
                                Contacto = combocontacto;
                                DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else {
                                MessageBox.Show("Ocurrió un error al intentar agregar el contacto.");
                            }
                        }
                        else {
                            MessageBox.Show("Debe agregar los datos del conatcto para continuar.");
                        }
                    }
                    String idiomaDescrip = (cbIdioma.SelectedItem as ComboboxItem).Text;

                    int iNumerogrupoids = System.Convert.ToInt32((cbIdioma.SelectedItem as ComboboxItem).Value.ToString());
                }//termina if persona
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

                        String Nombre_Util = fnombre.Text + fpaterno.Text;
                        conect con1 = new conect();
                        String sQueryInsert = "INSERT INTO `cliente` (`ClienteId`, " +
                                              "`ClienteClave`, " +
                                              "`PaisId`, " +
                                              "`RazonSocialClient`, " +
                                              "`IdiomaId`, " +
                                              "`TipoClienteId`, " +
                                              "`MonedaId`, " +
                                              "`ClienteTipoPersonaSAT`, " +
                                              "`NombreUtilClient`, " +
                                              "`IDNacionalidad`, " +
                                              "`ClienteFechaAlta`, " +
                                              "`ResponsableId`) " +
                                              "VALUES (NULL, " +
                                              "'" + tbClavecliente.Text + "', " +
                                              "'" + (combopais.SelectedItem as ComboboxItem).Value + "', " +
                                              "'" + tbNombreEmpresa.Text + "', " +
                                              "'" + (cbIdioma.SelectedItem as ComboboxItem).Value + "', " +
                                              "'" + (cbTipocliente.SelectedItem as ComboboxItem).Value + "', " +
                                              "'" + (cbModena.SelectedItem as ComboboxItem).Value + "', " +
                                              "'" + TipoP + "', " +

                                                  "'" + tbNombreEmpresa.Text + "', " +
                                              "'" + (Nacionalidad.SelectedItem as ComboboxItem).Value + "', " +
                                              // "'" + (combopais.SelectedItem as ComboboxItem).Value + "', " +
                                              "now(), " +
                                              "'1');";
                        MySqlDataReader respuestastring6 = con1.getdatareader(sQueryInsert);
                        if (respuestastring6 != null)
                        {
                            respuestastring6.Close();
                            String squerymaxId = "select * from cliente order by clienteid desc limit 1;";
                            String sClienteidmax = "";
                            MySqlDataReader resp_masixcliente = con1.getdatareader(squerymaxId);
                            while (resp_masixcliente.Read())
                            {
                                sClienteidmax = validareader("clienteid", "clienteid", resp_masixcliente).Text;
                            }
                            resp_masixcliente.Close();

                            if (sClienteidmax != "" && tbNombrecontacto.Text != "")//si se inserto y se pudo consultar entonces hacemos el insert del nuevo contacto con el id del cliente que acabamos de agregar
                            {
                                String sQueryinsertcontacto = "INSERT INTO `contacto` (" +
                                                              "`ContactoId`, " +
                                                              "`ClienteId`, " +
                                                              "`ContactoNick`, " +
                                                              "`ContactoNombre`, " +
                                                              "`ContactoTelefono`, " +
                                                              "`ContactoEmail`, " +
                                                              "`ContactoSexo`, " +
                                                              "`ContactoArea`, " +
                                                              "`ContactoPuesto`, " +
                                                              "`ContactoIndAct`, " +
                                                              "`InteresadoId`, " +
                                                              "`GrupoId`) VALUES (NULL, " +
                                                              "'" + sClienteidmax + "', " +
                                                              "'" + tbNicknamecontacto.Text + "', " +
                                                              "'" + tbNombrecontacto.Text + "', " +
                                                              "'" + tbTelefonocontacto.Text + "', " +
                                                              "'" + tbCorreocontacto.Text + "', " +
                                                              "NULL, " +
                                                              "NULL, " +
                                                              "NULL, " +
                                                              "'1', " +
                                                              "NULL, " +
                                                              "NULL);";
                                MySqlDataReader resp_insertcontacto = con1.getdatareader(sQueryinsertcontacto);
                                if (resp_insertcontacto != null)
                                {
                                    String squerymaxcontactoId = "select * from contacto order by ContactoId desc limit 1;";
                                    String sContactoidmax = "";
                                    MySqlDataReader resp_masixcontacto = con1.getdatareader(squerymaxcontactoId);
                                    while (resp_masixcontacto.Read())
                                    {
                                        sContactoidmax = validareader("ContactoId", "ContactoId", resp_masixcontacto).Text;
                                    }
                                    resp_masixcontacto.Close();
                                    //sContactoidmax
                                    ComboboxItem combocliente = new ComboboxItem();
                                    combocliente.Text = tbNombreEmpresa.Text;
                                    combocliente.Value = sClienteidmax;
                                    ComboboxItem combocontacto = new ComboboxItem();
                                    combocontacto.Text = tbNombrecontacto.Text;
                                    combocontacto.Value = sContactoidmax;

                                    Contactocorreo = tbCorreocontacto.Text;
                                    Cliente = combocliente;
                                    Contacto = combocontacto;
                                    DialogResult = DialogResult.OK;
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Ocurrió un error al intentar agregar el contacto.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Debe agregar los datos del conatcto para continuar.");
                            }
                        }
                        String idiomaDescrip = (cbIdioma.SelectedItem as ComboboxItem).Text;

                        int iNumerogrupoids = System.Convert.ToInt32((cbIdioma.SelectedItem as ComboboxItem).Value.ToString());
                    }
                }

                else
                {
                    MessageBox.Show("Debe ingresar los campos obligatorios marcados con un *");
                }
            }catch(Exception E){
                MessageBox.Show("Verifique los campos y vuelva a intentar");
                //falta agregar el log para conocer el error
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cbTipocliente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void clean(object sender, EventArgs e)
        {
            combopais.Items.Clear();
            PaisNombre.Items.Clear();
            conect con1 = new conect();



            String pais = "SELECT * FROM  pais where PaisNombre not like'%OFICINA%' AND  PaisNombre not like'%ORGANIZACION%'";
            MySqlDataReader rpais = con1.getdatareader(pais);
            while (rpais.Read())
            {
                combopais.Items.Add(validareader("PaisClave", "PaisId", rpais));
                //CB_idioma_cliente.Items
            }
            rpais.Close();


            String paisnombre = "SELECT * FROM  pais where PaisNombre not like'%OFICINA%' AND  PaisNombre not like'%ORGANIZACION%'";
            MySqlDataReader rpaiss = con1.getdatareader(paisnombre);
            while (rpaiss.Read())
            {
                PaisNombre.Items.Add(validareader("PaisNombre", "PaisId", rpaiss));
                //CB_idioma_cliente.Items
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
     /*   private void comboclave_selectdIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String Combo = combopais.Text;
                if (Combo != "")
                {
                    //combopais.Items.Clear();
                    repeticion_clave();
                    return;
                
                }
                else
                {
                    repeticion_clave();
                }
              
            }
            catch (Exception E)
            {


            }

        }*/
        private void combopais_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String Combo = PaisNombre.Text;
                String Combo2 = combopais.Text;

                if (Combo !="" && Combo2 != "")
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
        private void groupbox_SelectedIndexChanged(object sender, EventArgs e)
        {
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
    }
}
