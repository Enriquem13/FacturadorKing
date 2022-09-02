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
    public partial class FInteresadoDetalle : Form
    {
        public Form1 oFormlogin;
        public captura capFormcap;
        public string idinteresado;
        public string IDTABLA;
        public string sDireccionid;
        public int casoid;
        public DataGridView dataGridView1_global;
        public int sIdtiposolicituid;
        public FInteresadoDetalle(String datointeresado, Form1 form, captura Formcap, int bandera = 0, int caso = 0, int sTiposolicitudid = 0)
        {
            InitializeComponent();
            oFormlogin = form;
            capFormcap = Formcap;
            idinteresado = datointeresado;
            casoid = caso;
            sIdtiposolicituid = sTiposolicitudid;

            //try
            //{
            //    conect conectipoderelacion = new conect();
            //    String kwerytipoderelacion = "SELECT tiporelacion.TipoRelacionId , tiporelacion.TipoRelacionDescrip FROM tiporelacion;";
            //    MySqlDataReader respuestastringtipoderelacion = conectipoderelacion.getdatareader(kwerytipoderelacion);
            //    while (respuestastringtipoderelacion.Read())
            //    {
            //        CB_tipoderelacion.Items.Add(validareader("TipoRelacionDescrip", "TipoRelacionId", respuestastringtipoderelacion));
            //    }
            //    respuestastringtipoderelacion.Close();
            //    conectipoderelacion.Cerrarconexion();
            //}
            //catch (Exception Exs)
            //{
            //    new filelog("Linea 46, FInteresadoDetalle: ", " error: " + Exs.StackTrace);
            //}

            conect contipopersona = new conect();
            String kwery = "SELECT  clave_tipopersona, nombre_tipopersona FROM tipo_persona";
            MySqlDataReader respuestastring = contipopersona.getdatareader(kwery);
            while (respuestastring.Read())
            {
                
                CBtipo_personainteresadodetalle.Items.Add(validareader("nombre_tipopersona", "clave_tipopersona", respuestastring));
            }

            respuestastring.Close();
            contipopersona.Cerrarconexion();

            conect conecnacionalidad = new conect();
            String kwery2 = "SELECT PaisNacionalidad,PaisId   FROM  pais";
            MySqlDataReader respuestastring2 = conecnacionalidad.getdatareader(kwery2);


            while (respuestastring2.Read())
            {
                
                CB_nacionalidad_interesadodetalle.Items.Add(validareader("PaisNacionalidad", "PaisId", respuestastring2));

            }
            respuestastring2.Close();
            conecnacionalidad.Cerrarconexion();

            conect conectsociedad = new conect();
            String kwery3 = "SELECT SociedadId, SociedadDescrip  FROM  sociedad";
            MySqlDataReader respuestastring3 = conectsociedad.getdatareader(kwery3);


            while (respuestastring3.Read())
            {
                CB_tiposociedadinteresadodetalle.Items.Add(validareader("SociedadDescrip", "SociedadId", respuestastring3));
            }
            respuestastring3.Close();
            conectsociedad.Cerrarconexion();

            conect conectholder = new conect();
            String kweryholder = "SELECT * FROM holder";
            MySqlDataReader respuestastringholder = conectholder.getdatareader(kweryholder);

            while (respuestastringholder.Read())
            {
                CB_holderinteresadodetalle.Items.Add(validareader("HolderNombre", "HolderId", respuestastringholder));
            }
            respuestastringholder.Close();
            conectholder.Cerrarconexion();

            conect conecttipodireccion = new conect();
            String kwerytipodireccion = "SELECT TipoDireccionId, TipoDireccionDescrip FROM tipodireccion";
            MySqlDataReader respuestastringtipodireccion = conecttipodireccion.getdatareader(kwerytipodireccion);
            while (respuestastringtipodireccion.Read())
            {
                CB_interesadoDtipodireccion.Items.Add(validareader("TipoDireccionDescrip", "TipoDireccionId", respuestastringtipodireccion));
            }
            respuestastringtipodireccion.Close();
            conecttipodireccion.Cerrarconexion();

            conect conecpaisdireccion = new conect();
            String kwerypaisdireccion = "SELECT PaisId, PaisNombre FROM pais  order by PaisNombre ";
            MySqlDataReader respuestastringpaisdireccion = conecpaisdireccion.getdatareader(kwerypaisdireccion);
            while (respuestastringpaisdireccion.Read())
            {
                CB_interesadoDpais.Items.Add(validareader("PaisNombre", "PaisId", respuestastringpaisdireccion));
            }
            respuestastringpaisdireccion.Close();
            conecpaisdireccion.Cerrarconexion();

                        conect conectinteresado = new conect();
                        String query2 = "SELECT interesado.InteresadoID, " +
                                                " interesado.InteresadoNombre, " +
                                                " interesado.InteresadoApPaterno, " +
                                                " interesado.InteresadoApMaterno, " +
                                                " interesado.InteresadoRFC, " +
                                                " interesado.InteresadoTipoPersonaSAT, " +
                                                " Damelasociedad (interesado.SociedadID) AS SOCIEDAD, " +
                                                " interesado.InteresadoRGP, " +
                                                " Damelanacionalidad (interesado.PaisId) AS NACIONALIDAD, " +
                                                " interesado.InteresadoPoder, " +
                                                " interesado.InteresadoCurp, " +
                                                " interesado.InteresadoMail, " +
                                                " interesado.InteresadoShort, " +
                                                " Dameelholder (interesado.holderid)as HOLDER, " +
                                                " interesado.InteresadoTelefono " +
                                                "  FROM interesado WHERE  interesado.InteresadoID =  " +idinteresado+";";

                        

                        MySqlDataReader respuestastring20 = conectinteresado.getdatareader(query2);
                        if (respuestastring20 != null)
                        {

                            respuestastring20.Read();

                            IDTABLA = validareader("InteresadoID", "InteresadoID", respuestastring20).Text;
                            TB_nombreinteresadodetalle.Text = validareader("InteresadoNombre", "InteresadoID", respuestastring20).Text;
                            TB_apellidopaternointeresadodetalle.Text = validareader("InteresadoApPaterno", "InteresadoID", respuestastring20).Text;
                            TB_apellidomaternointeresadodetalle.Text = validareader("InteresadoApMaterno", "InteresadoID", respuestastring20).Text;
                            TB_rfcinteresadodetalle.Text = validareader("InteresadoRFC", "InteresadoID", respuestastring20).Text;
                            
                            CB_tiposociedadinteresadodetalle.Text = validareader("SOCIEDAD", "InteresadoID", respuestastring20).Text;
                            TB_rgpinteresadodetalle.Text = validareader("InteresadoRGP", "InteresadoID", respuestastring20).Text;
                            CB_nacionalidad_interesadodetalle.Text = validareader("NACIONALIDAD", "InteresadoID", respuestastring20).Text;
                            TB_poderinteresadodetallle.Text = validareader("InteresadoPoder", "InteresadoID", respuestastring20).Text;
                            TB_curpinteresadodetalle.Text = validareader("InteresadoCurp", "InteresadoID", respuestastring20).Text;
                            TB_correo_interesadodetalle.Text = validareader("InteresadoMail", "InteresadoID", respuestastring20).Text;
                            TB_telefono_interesadodetallle.Text = validareader("InteresadoTelefono", "InteresadoID", respuestastring20).Text;
                            TB_razonsocialinteresadodetalle.Text = validareader("InteresadoShort", "InteresadoID", respuestastring20).Text;
                            CB_holderinteresadodetalle.Text = validareader("HOLDER", "InteresadoID", respuestastring20).Text;
                            String TIPOPERSONA = "";

                            label49.Text = validareader("InteresadoNombre", "InteresadoID", respuestastring20).Text + " " + validareader("InteresadoApPaterno", "InteresadoID", respuestastring20).Text + " " + validareader("InteresadoApMaterno", "InteresadoID", respuestastring20).Text;
                            
                            switch (validareader("InteresadoTipoPersonaSAT", "InteresadoID", respuestastring20).Text)
                            {
                                case "FN":
                                    TIPOPERSONA = "Física Nacional";
                                    break;
                                case "FE":
                                    TIPOPERSONA = "Física Extranjera";
                                    break;
                                case "MN":
                                    TIPOPERSONA = "Moral Nacional";
                                    break;
                                case "ME":
                                    TIPOPERSONA = "Moral Extranjera";
                                    break;

                            }

                            CBtipo_personainteresadodetalle.Text = TIPOPERSONA;
                        }
            
                        respuestastring20.Close();
                        conectinteresado.Cerrarconexion();

            // Temina la primera pantalla


            //cargamos las direcciones
            cargardireccionesdelinteresado();
            //conect conectdireccioninteresado = new conect();
            //String kwerydireccioninteresado = "SELECT  direccion.DireccionCalle,  "+
            //                                         " direccion.DireccionNumExt, "+
            //                                         " direccion.DireccionNumInt, "+
            //                                         " direccion.DireccionColonia, "+
            //                                         " direccion.DireccionPoblacion, "+
            //                                         " direccion.DireccionEstado, "+
            //                                         " direccion.DireccionCP, "+
            //                                         " direccion.DireccionID, " +
            //                                         " DameNombrePais(direccion.PaisId) AS NOMBREPAIS, " +
            //                                         " direccion.PaisId AS PaisId, " +
            //                                         " direccion.TipoDireccionId AS TipoDireccionId, " +
            //                                         " Dametipodirecccion (direccion.TipoDireccionId) AS TIPODIRECCION " +
            //                                         " FROM direccion " +
            //                                         " WHERE direccion.InteresadoId =  " + idinteresado + ";";
            //MySqlDataReader respuestastringdireccioninteresado = conectdireccioninteresado.getdatareader(kwerydireccioninteresado);
            //bool bInteresadoexiste = true;
            //dataGridView1.Rows.Clear();
            //while (respuestastringdireccioninteresado.Read())
            //{
            //    string sDireccionID = validareader("DireccionID", "DireccionID", respuestastringdireccioninteresado).Text;
            //    string sDireccionCalle = validareader("DireccionCalle", "DireccionID", respuestastringdireccioninteresado).Text;
            //    string sDireccionNumExt = validareader("DireccionNumExt", "DireccionID", respuestastringdireccioninteresado).Text;
            //    string sDireccionNumInt = validareader("DireccionNumInt", "DireccionID", respuestastringdireccioninteresado).Text;
            //    string sDireccionColonia = validareader("DireccionColonia", "DireccionID", respuestastringdireccioninteresado).Text;
            //    string sDireccionPoblacion = validareader("DireccionPoblacion", "DireccionID", respuestastringdireccioninteresado).Text;
            //    string sDireccionEstado = validareader("DireccionEstado", "DireccionID", respuestastringdireccioninteresado).Text;
            //    string sDireccionCP = validareader("DireccionCP", "DireccionID", respuestastringdireccioninteresado).Text;
            //    string sNOMBREPAIS = validareader("NOMBREPAIS", "DireccionID", respuestastringdireccioninteresado).Text;
            //    string sPaisId = validareader("PaisId", "DireccionID", respuestastringdireccioninteresado).Text;
            //    string sTIPODIRECCION = validareader("TIPODIRECCION", "DireccionID", respuestastringdireccioninteresado).Text;
            //    string sTipoDireccionId = validareader("TipoDireccionId", "DireccionID", respuestastringdireccioninteresado).Text;
            //    /*ListViewItem listitem = new ListViewItem(validareader("DireccionID", "DireccionID", respuestastringdireccioninteresado).Text);
            //    listitem.SubItems.Add(sDireccionID);
            //    listitem.SubItems.Add(sDireccionCalle);
            //    listitem.SubItems.Add(sDireccionNumExt);
            //    listitem.SubItems.Add(sDireccionNumInt);
            //    listitem.SubItems.Add(sDireccionColonia);
            //    listitem.SubItems.Add(sDireccionPoblacion);
            //    listitem.SubItems.Add(sDireccionEstado);
            //    listitem.SubItems.Add(sDireccionCP);
            //    listitem.SubItems.Add(sNOMBREPAIS);
            //    listitem.SubItems.Add(sTIPODIRECCION);*/
            //    //lvdireccionesinteresados.Items.Add(listitem);
            //    dataGridView1.Rows.Add(sDireccionID, sDireccionCalle, sDireccionNumExt, sDireccionNumInt, sDireccionColonia, sDireccionPoblacion, sDireccionEstado, sDireccionCP, sNOMBREPAIS, sPaisId, sTipoDireccionId, sTIPODIRECCION);
            //    //sDireccionid = validareader("DireccionID", "DireccionID", respuestastringdireccioninteresado).Text;
            //    //TB_interesadoDcalle.Text = validareader("DireccionCalle", "DireccionID", respuestastringdireccioninteresado).Text;
            //    //TB_interesadoDnumext.Text = validareader("DireccionNumExt", "DireccionID", respuestastringdireccioninteresado).Text;
            //    //TB_interesadoDnumint.Text = validareader("DireccionNumInt", "DireccionID", respuestastringdireccioninteresado).Text;
            //    //TB_interesadoDcolonia.Text = validareader("DireccionColonia", "DireccionID", respuestastringdireccioninteresado).Text;
            //    //TB_interesadoDpoblacion.Text = validareader("DireccionPoblacion", "DireccionID", respuestastringdireccioninteresado).Text;
            //    //TB_interesadoDestado.Text = validareader("DireccionEstado", "DireccionID", respuestastringdireccioninteresado).Text;
            //    //TB_interesadoDcp.Text = validareader("DireccionCP", "DireccionID", respuestastringdireccioninteresado).Text;
            //    //CB_interesadoDpais.Text = validareader("NOMBREPAIS", "DireccionID", respuestastringdireccioninteresado).Text;
            //    //CB_interesadoDtipodireccion.Text = validareader("TIPODIRECCION", "DireccionID", respuestastringdireccioninteresado).Text;
            //    bInteresadoexiste = false;
            //}
            //respuestastringdireccioninteresado.Close();
            //conectdireccioninteresado.Cerrarconexion();
            //if (bInteresadoexiste)
            //{
            //    MessageBox.Show("No hay dirección para el intersado " + TB_nombreinteresadodetalle.Text);
            //}
            if (bandera != 0)
            {
                BT_Cerrar.Enabled = true;
                //BT_salirinteresadodetalle.Enabled = false;
                BT_menuinteresadodetalle.Enabled = false;
            }
        }

        public void cargardireccionesdelinteresado() {
            conect conectdireccioninteresado = new conect();
            String kwerydireccioninteresado = "SELECT  direccion.DireccionCalle,  " +
                                                     " direccion.DireccionNumExt, " +
                                                     " direccion.DireccionNumInt, " +
                                                     " direccion.DireccionColonia, " +
                                                     " direccion.DireccionPoblacion, " +
                                                     " direccion.DireccionEstado, " +
                                                     " direccion.DireccionCP, " +
                                                     " direccion.DireccionID, " +
                                                     " DameNombrePais(direccion.PaisId) AS NOMBREPAIS, " +
                                                     " direccion.PaisId AS PaisId, " +
                                                     " direccion.TipoDireccionId AS TipoDireccionId, " +
                                                     " Dametipodirecccion (direccion.TipoDireccionId) AS TIPODIRECCION " +
                                                     " FROM direccion " +
                                                     " WHERE direccion.InteresadoId =  " + idinteresado + ";";
            MySqlDataReader respuestastringdireccioninteresado = conectdireccioninteresado.getdatareader(kwerydireccioninteresado);
            bool bInteresadoexiste = true;
            dataGridView1.Rows.Clear();
            while (respuestastringdireccioninteresado.Read())
            {
                string sDireccionID = validareader("DireccionID", "DireccionID", respuestastringdireccioninteresado).Text;
                string sDireccionCalle = validareader("DireccionCalle", "DireccionID", respuestastringdireccioninteresado).Text;
                string sDireccionNumExt = validareader("DireccionNumExt", "DireccionID", respuestastringdireccioninteresado).Text;
                string sDireccionNumInt = validareader("DireccionNumInt", "DireccionID", respuestastringdireccioninteresado).Text;
                string sDireccionColonia = validareader("DireccionColonia", "DireccionID", respuestastringdireccioninteresado).Text;
                string sDireccionPoblacion = validareader("DireccionPoblacion", "DireccionID", respuestastringdireccioninteresado).Text;
                string sDireccionEstado = validareader("DireccionEstado", "DireccionID", respuestastringdireccioninteresado).Text;
                string sDireccionCP = validareader("DireccionCP", "DireccionID", respuestastringdireccioninteresado).Text;
                string sNOMBREPAIS = validareader("NOMBREPAIS", "DireccionID", respuestastringdireccioninteresado).Text;
                string sPaisId = validareader("PaisId", "DireccionID", respuestastringdireccioninteresado).Text;
                string sTIPODIRECCION = validareader("TIPODIRECCION", "DireccionID", respuestastringdireccioninteresado).Text;
                string sTipoDireccionId = validareader("TipoDireccionId", "DireccionID", respuestastringdireccioninteresado).Text;
                
                dataGridView1.Rows.Add(sDireccionID, sDireccionCalle, sDireccionNumExt, sDireccionNumInt, sDireccionColonia, sDireccionPoblacion, sDireccionEstado, sDireccionCP, sNOMBREPAIS, sPaisId, sTipoDireccionId, sTIPODIRECCION);
            }
            respuestastringdireccioninteresado.Close();
            conectdireccioninteresado.Cerrarconexion();
            dataGridView1_global = dataGridView1;
        }

        private void BT_menuinteresadodetalle_Click(object sender, EventArgs e)
        {
            capFormcap.Show();
            this.Close();
        }

        private void BT_salirinteresadodetalle_Click(object sender, EventArgs e)
        {
            capFormcap.Close();
            oFormlogin.Close();
            this.Close();
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
                
            }catch(Exception Ex){
                
                cItemresult.Text = "";
                cItemresult.Value = "";
            
            }
            return cItemresult;

            
        }

        private void BT_eliminardetalleinteresado_Click(object sender, EventArgs e)
        {
            try {

                if (IDTABLA == null)
                {
                    MessageBox.Show("No hay ningun interesado.");
                }else{

                    conect conectnumcasos = new conect();
                    String kwerynumcasos = "SELECT COUNT(*) FROM casointeresado WHERE casointeresado.InteresadoId = " + IDTABLA + "  group by CasoId;";
                    MySqlDataReader respuestanumcasos = conectnumcasos.getdatareader(kwerynumcasos);
                    if (respuestanumcasos != null)
                    {
                        int contador = 0;
                        while (respuestanumcasos.Read())
                        {

                            contador++;
                        }
                        respuestanumcasos.Close();
                        conectnumcasos.Cerrarconexion();

                        if (contador > 0)
                        {
                            //MessageBox.Show("El interesado esta asociado a mas de un caso, no se puede eliminar");


                                //
                                if (casoid != 0)
                                {
                                    var confirmResult2 = MessageBox.Show("¿Seguro que desea ELIMINAR este interesado ?", "Eliminar Interesado", MessageBoxButtons.YesNo);
                                    if (confirmResult2 == DialogResult.Yes)
                                    {

                                        conect conectcasointeresado = new conect();
                                        String kweryconect = "DELETE  FROM casointeresado WHERE CasoId =  " + casoid + " AND InteresadoId = " + IDTABLA + ";";
                                        MySqlDataReader respuesta_deletecasointeresado = conectcasointeresado.getdatareader(kweryconect);
                                        if (respuesta_deletecasointeresado == null)
                                        {
                                            MessageBox.Show("No se pudo eliminar casointeresado");
                                        }
                                        else
                                        {
                                            respuesta_deletecasointeresado.Close();
                                            conectcasointeresado.Cerrarconexion();
                                            this.Hide();

                                        }
                                    }
                                }
                                else
                                {
                                    //
                                    var confirmResult3 = MessageBox.Show("El interesado esta asociado a mas de un caso, ¿Seguro que desea ELIMINAR este interesado ?", "Eliminar Interesado", MessageBoxButtons.YesNo);
                                    if (confirmResult3 == DialogResult.Yes)
                                    {
                                        conect conectdeletteinteresado = new conect();
                                        String kwerydeleteinteresado = "DELETE FROM interesado WHERE interesado.InteresadoID =  " + IDTABLA + ";";
                                        MySqlDataReader respuesta_delete = conectdeletteinteresado.getdatareader(kwerydeleteinteresado);
                                        if (respuesta_delete == null)
                                        {
                                            MessageBox.Show("No se puede eliminar a este interesado");
                                        }
                                        else
                                        {

                                            if (casoid != 0)
                                            {
                                                conect conectcasointeresado = new conect();
                                                String kweryconect = "DELETE  FROM casointeresado WHERE CasoId =  " + casoid + " AND InteresadoId = " + IDTABLA + ";";
                                                MySqlDataReader respuesta_deletecasointeresado = conectcasointeresado.getdatareader(kweryconect);
                                                if (respuesta_deletecasointeresado == null)
                                                {
                                                    MessageBox.Show("No se pudo eliminar casointeresado");
                                                }
                                                else
                                                {
                                                    respuesta_deletecasointeresado.Close();
                                                    conectcasointeresado.Cerrarconexion();
                                                }

                                            }

                                            respuesta_delete.Close();
                                            conectdeletteinteresado.Cerrarconexion();
                                            MessageBox.Show("Intresado borrado correctamente");

                                            conect conecdeletedireccioninteresado = new conect();
                                            String kwerydeleteinteresadodireccion = "DELETE FROM direccion WHERE direccion.InteresadoId = " + IDTABLA + ";";
                                            MySqlDataReader respuesta_deletedireccion = conecdeletedireccioninteresado.getdatareader(kwerydeleteinteresadodireccion);
                                            if (respuesta_delete == null)
                                            {
                                                MessageBox.Show("No se pudo eliminar la direccion asociada a este interesado");
                                            }
                                            else
                                            {
                                                respuesta_deletedireccion.Close();
                                                conecdeletedireccioninteresado.Cerrarconexion();
                                                //   MessageBox.Show("Direccion asociada a este interesado borrada correctamente.")
                                            }

                                            if (casoid != 0)
                                            {
                                                this.Hide();
                                            }
                                            else
                                            {
                                                FConsultaInteresado fconsultainteresa = new FConsultaInteresado(oFormlogin, capFormcap);
                                                fconsultainteresa.Show();
                                                this.Hide();
                                            }
                                        } 
                                    }
                            }

                            // el interesado esta ligado a mas de un caso
                        }
                        else
                        {
                            var confirmResult = MessageBox.Show("¿Seguro que desea ELIMINAR este interesado ?", "Eliminar Interesado", MessageBoxButtons.YesNo);
                            if (confirmResult == DialogResult.Yes)
                            {
                                conect conectdeletteinteresado = new conect();
                                String kwerydeleteinteresado = "DELETE FROM interesado WHERE interesado.InteresadoID =  " + IDTABLA + ";";
                                MySqlDataReader respuesta_delete = conectdeletteinteresado.getdatareader(kwerydeleteinteresado);
                                if (respuesta_delete == null)
                                {
                                    MessageBox.Show("No se puede eliminar a este interesado");
                                }
                                else
                                {

                                    if (casoid != 0)
                                    {
                                        conect conectcasointeresado = new conect();
                                        String kweryconect = "DELETE  FROM casointeresado WHERE CasoId =  " + casoid + " AND InteresadoId = " + IDTABLA + ";";
                                        MySqlDataReader respuesta_deletecasointeresado = conectcasointeresado.getdatareader(kweryconect);
                                        if (respuesta_deletecasointeresado == null)
                                        {
                                            MessageBox.Show("No se pudo eliminar casointeresado");
                                        }
                                        else
                                        {
                                            respuesta_deletecasointeresado.Close();
                                            conectcasointeresado.Cerrarconexion();
                                        }

                                    }

                                    respuesta_delete.Close();
                                    conectdeletteinteresado.Cerrarconexion();
                                    MessageBox.Show("Intresado borrado correctamente");

                                    conect conecdeletedireccioninteresado = new conect();
                                    String kwerydeleteinteresadodireccion = "DELETE FROM direccion WHERE direccion.InteresadoId = " + IDTABLA + ";";
                                    MySqlDataReader respuesta_deletedireccion = conecdeletedireccioninteresado.getdatareader(kwerydeleteinteresadodireccion);
                                    if (respuesta_delete == null)
                                    {
                                        MessageBox.Show("No se pudo eliminar la direccion asociada a este interesado");
                                    }
                                    else
                                    {
                                        respuesta_deletedireccion.Close();
                                        conecdeletedireccioninteresado.Cerrarconexion();
                                        //   MessageBox.Show("Direccion asociada a este interesado borrada correctamente.")
                                    }

                                    if (casoid != 0)
                                    {
                                        this.Hide();
                                    }
                                    else
                                    {
                                        FConsultaInteresado fconsultainteresa = new FConsultaInteresado(oFormlogin, capFormcap);
                                        fconsultainteresa.Show();
                                        this.Hide();
                                    }
                                }
                            }
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

        private void BT_modificardetalleinteresado_Click(object sender, EventArgs e)
        {
            try
            {
                    //                if (CB_interesadoholder.SelectedItem != null)
                    //{
                    //    holder = (CB_interesadoholder.SelectedItem as ComboboxItem).Value.ToString();
                    //}

                String holderid = "NULL";

                if (CB_holderinteresadodetalle.SelectedItem != null)
                {
                    holderid = (CB_holderinteresadodetalle.SelectedItem as ComboboxItem).Value.ToString();
                }

                if (CB_nacionalidad_interesadodetalle.SelectedItem != null && CB_tiposociedadinteresadodetalle.SelectedItem != null && CBtipo_personainteresadodetalle.SelectedItem != null && TB_nombreinteresadodetalle.Text !="")
                {
                    conect conectmodificar = new conect();
                    String kwerymodificas = "UPDATE `interesado` SET "+
                                                       " `InteresadoTipoPersonaSAT` = '" + (CBtipo_personainteresadodetalle.SelectedItem as ComboboxItem).Value.ToString()+
                                                       "', `InteresadoNombre` = '"+TB_nombreinteresadodetalle.Text+
                                                       "', `InteresadoApPaterno` = '" +TB_apellidopaternointeresadodetalle.Text+
                                                       "', `InteresadoApMaterno` = '"+TB_apellidomaternointeresadodetalle.Text+
                                                       "', `InteresadoRFC` = '"+TB_rfcinteresadodetalle.Text+
                                                       "', `SociedadID` = "+ (CB_tiposociedadinteresadodetalle.SelectedItem as ComboboxItem).Value.ToString()+
                                                       ", `InteresadoRGP` = '"+TB_rgpinteresadodetalle.Text+
                                                       "', `PaisId` = "+ (CB_nacionalidad_interesadodetalle.SelectedItem as ComboboxItem).Value.ToString()+
                                                       ", `InteresadoShort` = '"+TB_razonsocialinteresadodetalle.Text+
                                                       "', `InteresadoPoder` = '"+TB_poderinteresadodetallle.Text+
                                                       "', `InteresadoCurp` = '" +TB_curpinteresadodetalle.Text+
                                                       "', `InteresadoMail` = '" +TB_correo_interesadodetalle.Text+
                                                       "', `InteresadoTelefono` = '"+TB_telefono_interesadodetallle.Text+
                                                       "', `holderid` =  "+holderid+
                                                       " WHERE `InteresadoID` =  " + IDTABLA + ";";
                    MySqlDataReader respuestaupdate = conectmodificar.getdatareader(kwerymodificas);

                    if (respuestaupdate == null)
                    {
                        MessageBox.Show("No se pudo modificar al interesado");
                    }
                    else
                    {
                        MessageBox.Show("Se modifico interesado: "+IDTABLA);
                        respuestaupdate.Close();
                        conectmodificar.Cerrarconexion();
                      //  FConsultaInteresado fconsultainteresa = new FConsultaInteresado(oFormlogin, capFormcap);
                       // fconsultainteresa.Show();
                        //this.Hide();

                    }


                }
                else
                {
                    MessageBox.Show("Debes llenar mínimo el nombre, la nacionalidad, el tipo de sociedad y el tipo de persona");
                }
            }
            catch (Exception E)
            {

                //escribimos en log
                Console.WriteLine("{0} Exception caught.", E);
                MessageBox.Show("Fallo " + E);
            }
        }

        public void agregarmosnuevadireccion() {
            try {

                if (TB_interesadoDcalle.Text == "")
                {
                    MessageBox.Show("Debe ingresar calle");
                    return;
                }

                if (CB_interesadoDpais.Text == "")
                {
                    MessageBox.Show("Debe ingresar país");
                    return;
                }
                
                if(CB_interesadoDtipodireccion.Text==""){
                    MessageBox.Show("Debe ingresar tipo de dirección");
                    return;
                }
                
                conect conectmodificardireccion = new conect();
                String sInsertdireccionuneva = " INSERT INTO `direccion` " +
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
                                                " `ContactoId`, " +
                                                " `InteresadoId`, " +
                                                " `ClienteId`, " +
                                                " `CasoId`, " +
                                                " `TipoDireccionId`) " +
                                                " VALUES " +
                                                " (  NULL, " +
                                                "   '" + TB_interesadoDcalle.Text + "', " +
                                                "   '" + TB_interesadoDnumext.Text + "', " +
                                                "   '" + TB_interesadoDnumint.Text + "', " +
                                                "   '"+ TB_interesadoDcolonia.Text +"', " +
                                                "   '" + TB_interesadoDpoblacion.Text + "', " +
                                                "   '" + TB_interesadoDestado.Text + "', " +
                                                "   '" + TB_interesadoDcp.Text + "', " +
                                                "   '1', " +
                                                "   '" + (CB_interesadoDpais.SelectedItem as ComboboxItem).Value.ToString() + "', " +
                                                "   '0', " +
                                                "   '" + idinteresado + "', " +
                                                "   '0', " +
                                                "   '0', " +
                                                "   '" + (CB_interesadoDtipodireccion.SelectedItem as ComboboxItem).Value.ToString() + "') ";
                MySqlDataReader respuestaupdate2 = conectmodificardireccion.getdatareader(sInsertdireccionuneva);
                if (respuestaupdate2 != null)
                {
                    MessageBox.Show("La dirección se agrego correctamente");
                    try{
                        sDireccionid = "";
                        TB_interesadoDcalle.Text = "";
                        TB_interesadoDnumext.Text = "";
                        TB_interesadoDnumint.Text = "";
                        TB_interesadoDcolonia.Text = "";
                        TB_interesadoDpoblacion.Text = "";
                        TB_interesadoDestado.Text = "";
                        TB_interesadoDcp.Text = "";
                        CB_interesadoDpais.Text = "";
                        CB_interesadoDtipodireccion.Text = "";
                        BT_modificardireccion.Enabled = false;
                        btn_agregarnuevo.Enabled = true;
                        cargardireccionesdelinteresado();
                    }catch(Exception Ex){
                    
                    }
                    
                    /*BT_modificardireccion.Enabled = false;
                    btn_agregarnuevo.Enabled = true;*/
                }
                else {
                    MessageBox.Show("Error al intentar agregar la dirección");
                }
            }catch(Exception Ex){
                new filelog(oFormlogin.sId, "Error al intentar insert direccion al interesado con id = " + idinteresado + ":   " + Ex.ToString());
            }
        }


        

        private void BT_modificardireccion_Click(object sender, EventArgs e)
        {
            //
            try
            {
                //
                if (CB_interesadoDpais.SelectedItem != null && CB_interesadoDtipodireccion.SelectedItem != null && !TB_interesadoDcalle.Text.Trim().Equals(""))
                {
                    if (sDireccionid == null) {//debido a que no existe ningúna dirección
                        MessageBox.Show("Debe seleccionar una dirección para modificar");
                        return;
                    }
                    conect conectmodificardireccion = new conect();
                    String kwerymodificadireccion =" UPDATE `direccion` SET "+
                                                       " `DireccionCalle` = '" + TB_interesadoDcalle.Text+
                                                       "', `DireccionNumExt` = '" +TB_interesadoDnumext.Text+
                                                       "', `DireccionNumInt` = '"+ TB_interesadoDnumint.Text+
                                                       "', `DireccionColonia` = '" +TB_interesadoDcolonia.Text+
                                                       "', `DireccionPoblacion` = '" +TB_interesadoDpoblacion.Text+
                                                       "', `DireccionEstado` = '"+ TB_interesadoDestado.Text+
                                                       "', `DireccionCP` = '"+TB_interesadoDcp.Text+
                                                       "', `PaisId` = " + (CB_interesadoDpais.SelectedItem as ComboboxItem).Value.ToString() +
                                                       " ,`TipoDireccionId` = " + (CB_interesadoDtipodireccion.SelectedItem as ComboboxItem).Value.ToString() +
                                                       " WHERE `DireccionID` =  " + sDireccionid + ";";
                    MySqlDataReader respuestaupdate2 = conectmodificardireccion.getdatareader(kwerymodificadireccion);
                    if (respuestaupdate2 == null)
                    {
                        MessageBox.Show("No se pudo modificar la dirección del interesado.");
                        conectmodificardireccion.Cerrarconexion();
                    }
                    else
                    {
                        MessageBox.Show("Se modifico dirección relacionada con el interesado: " + sDireccionid);
                        respuestaupdate2.Close();
                        conectmodificardireccion.Cerrarconexion();
                        sDireccionid = "";
                        TB_interesadoDcalle.Text = "";
                        TB_interesadoDnumext.Text = "";
                        TB_interesadoDnumint.Text = "";
                        TB_interesadoDcolonia.Text = "";
                        TB_interesadoDpoblacion.Text = "";
                        TB_interesadoDestado.Text = "";
                        TB_interesadoDcp.Text = "";
                        CB_interesadoDpais.Text = "";
                        CB_interesadoDtipodireccion.Text = "";
                        BT_modificardireccion.Enabled = false;
                        btn_agregarnuevo.Enabled = true;
                        //FConsultaInteresado fconsultainteresa = new FConsultaInteresado(oFormlogin, capFormcap);
                        //fconsultainteresa.Show();
                       // this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Debes seleccionar minimo, la calle, el pais y el tipo de dirección.");
                }
            }
            catch (Exception E)
            {
                //escribimos en log
                //Console.WriteLine("{0} Exception caught.", E);
                MessageBox.Show("Fallo " + E);
            }
        }

        private void BT_Cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void dataGridView1_RowDividerDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            try {
                sDireccionid = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();//Direccionid seleccionado
                TB_interesadoDcalle.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                TB_interesadoDnumext.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                TB_interesadoDnumint.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                TB_interesadoDcolonia.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                TB_interesadoDpoblacion.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                TB_interesadoDestado.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                TB_interesadoDcp.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                CB_interesadoDpais.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
                CB_interesadoDtipodireccion.Text = dataGridView1.SelectedRows[0].Cells[11].Value.ToString();
                BT_modificardireccion.Enabled = true;
                btn_agregarnuevo.Enabled = false;
            }catch(Exception Ex){

            
            }
            
            
        }

        private void btn_agregarnuevo_Click(object sender, EventArgs e)
        {
            agregarmosnuevadireccion();        
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            sDireccionid = "";
            TB_interesadoDcalle.Text = "";
            TB_interesadoDnumext.Text = "";
            TB_interesadoDnumint.Text = "";
            TB_interesadoDcolonia.Text = "";
            TB_interesadoDpoblacion.Text = "";
            TB_interesadoDestado.Text = "";
            TB_interesadoDcp.Text = "";
            CB_interesadoDpais.Text = "";
            CB_interesadoDtipodireccion.Text = "";

            BT_modificardireccion.Enabled = false;
            btn_agregarnuevo.Enabled = true;
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            try {
                
                if ( MessageBox.Show("¿Seguro que desea borrar ésta dirección?, La dirección ya no estará disponible en ningún caso que contenta éste interesado.",
                                     "Confirmación de borrar Dirección.",
                                     MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // If 'Yes', do something here.
                    conect conect_delete = new conect();
                    String sDireccionid = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    if (sDireccionid!="")
                    {
                        String query_delete = "DELETE FROM direccion WHERE DireccionID = " + sDireccionid;
                        MySqlDataReader respuestdelete = conect_delete.getdatareader(query_delete);
                        if (respuestdelete == null)
                        {
                            MessageBox.Show("Error al intentar borrar la dirección.");
                            new filelog(oFormlogin.sId, "Error: " + "Error al intentar borrar la dirección: " + query_delete);
                        }
                        else {
                            MessageBox.Show("Borrado correctamente.");
                            respuestdelete.Close();
                            
                            new filelog(oFormlogin.sId, "Dirección con id " + sDireccionid + "eliminado por: usuario id= " + oFormlogin.sId);
                            cargardireccionesdelinteresado();
                        }
                        conect_delete.Cerrarconexion();
                    }
                }
            }catch(Exception Ex){
                new filelog(oFormlogin.sId, "Error: " + Ex.ToString());
            }
            
            
        }

        private void CB_tipoderelacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    //hacemos el update del interesado seleccionado 
            //    conect conectrelacion_update = new conect();
            //    String squeryupdate = "UPDATE `casointeresado` SET `TipoRelacionId` = '" + (CB_tipoderelacion.SelectedItem as ComboboxItem).Value + 
            //                            "' WHERE InteresadoId = " + idinteresado +
            //                            " and  CasoId = " + casoid + " and TipoSolicitudId= '" + sIdtiposolicituid + "'; ";
            //    MySqlDataReader respuestastringdireccioninteresado_update = conectrelacion_update.getdatareader(squeryupdate);
            //    //MessageBox.Show(respuestastringdireccioninteresado_update.RecordsAffected + " Relación actualizada.");
            //    respuestastringdireccioninteresado_update.Close();
            //    conectrelacion_update.Cerrarconexion();
            //}
            //catch (Exception exs)
            //{
            //    new filelog(" error", "" + exs.StackTrace);
            //}
        }
    }
}
