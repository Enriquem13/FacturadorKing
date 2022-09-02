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
    public partial class CatalagoEstatus : Form
    {
        public String vari;
        funcionesdicss obj_dicss = new funcionesdicss();
        public CatalagoEstatus()
        {
            
            InitializeComponent();
            inicaocmpnentes();
        }

        public void inicaocmpnentes() {
            try {
                conect con = new conect();
                String sQuerytipoSol = "select * from tipodocumento where TipoDocumentoId in(1, 2, 8, 9);";

                MySqlDataReader respuestastringtoiposl = con.getdatareader(sQuerytipoSol);
                while (respuestastringtoiposl.Read())
                {
                    cbtipodedocumento.Items.Add(obj_dicss.validareader("TipoDocumentoDescrip", "TipoDocumentoId", respuestastringtoiposl));
                }
                respuestastringtoiposl.Close();
                con.Cerrarconexion();

                conect con_2 = new conect();
                String sQuery_plazo = "select * from grupoplazo";
                MySqlDataReader respuesta_grupoplazo = con_2.getdatareader(sQuery_plazo);
                while (respuesta_grupoplazo.Read())
                {
                    cbtipoplazo.Items.Add(obj_dicss.validareader("GrupoPlazoDescripcion", "GrupoPlazoId", respuesta_grupoplazo));
                }
                respuesta_grupoplazo.Close();
                con_2.Cerrarconexion();

                actualizarelacionsubtipodocumentoplazo();
               
            }catch(Exception Ex){
                MessageBox.Show("Error");
                new filelog("agrega plazos", "Error: " + Ex);
            }
            

        }


        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            
            //TipoDocumentoId
            //select * from subtipodocumento;
            
        }

        private void cbtipodocumento_SelectedValueChanged(object sender, EventArgs e)
        {
            actualizadocumento();
        }
        public void actualizadocumento() {
            try
            {
                if (cbtipodedocumento.Text != "")
                {
                    object sIdtipodocumento = (cbtipodedocumento.SelectedItem as ComboboxItem).Value;
                    switch (sIdtipodocumento + "")
                    {
                        case "1":
                            {
                                tb_accion.Text = "Genera";
                            } break;
                        case "2":
                            {
                                tb_accion.Text = "Atiende";
                            } break;
                        case "8":
                            {
                                tb_accion.Text = "Atiende";
                            } break;
                        case "9":
                            {
                                tb_accion.Text = "Genera";
                            } break;
                        default:
                            {
                                tb_accion.Text = "";
                            } break;

                    }
                    conect con = new conect();
                    String sQuerytipoSol = "select * from subtipodocumento where TipoDocumentoId = " + sIdtipodocumento + " order by SubTipoDocumentoDescrip;";
                    MySqlDataReader respuestastringtoiposl = con.getdatareader(sQuerytipoSol);
                    cbDocumento.Items.Clear();
                    cbDocumento.Text = "";
                    while (respuestastringtoiposl.Read())
                    {
                        cbDocumento.Items.Add(obj_dicss.validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", respuestastringtoiposl));
                    }
                    respuestastringtoiposl.Close();
                    con.Cerrarconexion();
                }
                else
                {
                    tb_accion.Text = "";
                }
            }
            catch (Exception Ex)
            {
                new filelog("linea 109", Ex.Message);
            }
        }

        private void cbPlazo_SelectedValueChanged(object sender, EventArgs e)
        {
            actualizatipoplazo();
        }

        public void actualizatipoplazo(){
            //cbtipoplazo
            try
            {
                object sIdtipoplazo = (cbtipoplazo.SelectedItem as ComboboxItem).Value;
                conect con_2 = new conect();
                String sQuery_2 = "select * from tipoplazo where GrupoPlazoId = " + sIdtipoplazo + ";";
                MySqlDataReader respuesta_2 = con_2.getdatareader(sQuery_2);
                cbPlazo.Items.Clear();
                cbPlazo.Text = "";
                while (respuesta_2.Read())
                {
                    cbPlazo.Items.Add(obj_dicss.validareader("TipoPlazoDescrip", "TipoPlazoId", respuesta_2));
                }
                respuesta_2.Close();
                con_2.Cerrarconexion();
            }
            catch (Exception Ex)
            {
                new filelog("linea: ", Ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                //si tiene prorroga tiene dos meses hasta el día de hoy
                if (cbDocumento.Text != "" && cbPlazo.Text != "" && tb_tiempomes.Text != "" && tb_tiempomes.Text != "")
                {
                    String sSubtipodocumentoid = (cbDocumento.SelectedItem as ComboboxItem).Value.ToString();
                    String sTipoplazoid = (cbPlazo.SelectedItem as ComboboxItem).Value.ToString();
                    String sTimpomes = tb_tiempomes.Text;
                    String stimpodias = tb_tiempomes.Text;
                    String sAccion = tb_accion.Text;

                    conect con_2 = new conect();
                    String sInserrelacionplazossubtipodocumento = " INSERT INTO `relacionplazosubtipodocumento`" +
                                                                    " (`id_grupoplazosubtipodocumento`," +
                                                                    " `subtipodocumentoid`," +
                                                                    " `tipoplazoid`," +
                                                                    " `tiempo_meses`," +
                                                                    " `tiempo_dias`," +
                                                                    " `accion`)" +
                                                                    " VALUES" +
                                                                    " (NULL," +
                                                                    " '" + sSubtipodocumentoid + "'," +
                                                                    " '" + sTipoplazoid + "'," +
                                                                    " '" + sTimpomes + "'," +
                                                                    " '" + stimpodias + "'," +
                                                                    " '" + sAccion + "');";
                    MySqlDataReader respuesta_2 = con_2.getdatareader(sInserrelacionplazossubtipodocumento);
                    if(respuesta_2.RecordsAffected==1){
                        MessageBox.Show("Se agregó correctamente.");
                        actualizarelacionsubtipodocumentoplazo();
                    }
                }
                else {
                    MessageBox.Show("Debe seleccionar un Documento, plazo y tiempo del plazo.");   
                }

            }catch(Exception Ex){
                MessageBox.Show("Verifique la selección de los campos.");   
            }
        }

        private void actualizarelacionsubtipodocumentoplazo()
        {
            try {
                conect con = new conect();
                String sQuerytipoSol = " SELECT " +
                                        " relacionplazosubtipodocumento.*," +
                                        " Get_grupoplazo(relacionplazosubtipodocumento.tipoplazoid) AS grupoplazodescrip," +
                                        " Get_tipodocumento(relacionplazosubtipodocumento.subtipodocumentoid) AS tipodocumentodescrip," +
                                        " GET_TIPOPLAZO(relacionplazosubtipodocumento.tipoplazoid) AS tipoplazodescrip," +
                                        " GET_SUBTIPODOCUMENTO(relacionplazosubtipodocumento.subtipodocumentoid) AS subtipodocumentodescrip" +
                                        " FROM" +
                                        " relacionplazosubtipodocumento;";

                MySqlDataReader respuestastringtoiposl = con.getdatareader(sQuerytipoSol);
                cbDocumento.Items.Clear();

                dgTaablerelacionsubtipodocumnetoplazos.Rows.Clear();
                while (respuestastringtoiposl.Read())
                {
                    String sid = obj_dicss.validareader("id_grupoplazosubtipodocumento", "id_grupoplazosubtipodocumento", respuestastringtoiposl).Text;
                    String ssubtipodocumentoid = obj_dicss.validareader("subtipodocumentoid", "id_grupoplazosubtipodocumento", respuestastringtoiposl).Text;
                    String stipoplazoid = obj_dicss.validareader("tipoplazoid", "id_grupoplazosubtipodocumento", respuestastringtoiposl).Text;
                    String saccion = obj_dicss.validareader("accion", "id_grupoplazosubtipodocumento", respuestastringtoiposl).Text;
                    String stiempo_meses = obj_dicss.validareader("tiempo_meses", "id_grupoplazosubtipodocumento", respuestastringtoiposl).Text;
                    String stiempo_dias = obj_dicss.validareader("tiempo_dias", "id_grupoplazosubtipodocumento", respuestastringtoiposl).Text;
                    String stipodocumentodescrip = obj_dicss.validareader("tipodocumentodescrip", "id_grupoplazosubtipodocumento", respuestastringtoiposl).Text;
                    String stipoplazodescrip = obj_dicss.validareader("tipoplazodescrip", "id_grupoplazosubtipodocumento", respuestastringtoiposl).Text;
                    String ssubtipodocumentodescrip = obj_dicss.validareader("subtipodocumentodescrip", "id_grupoplazosubtipodocumento", respuestastringtoiposl).Text;
                    String sgrupoplazodescrip = obj_dicss.validareader("grupoplazodescrip", "id_grupoplazosubtipodocumento", respuestastringtoiposl).Text;
                    dgTaablerelacionsubtipodocumnetoplazos.Rows.Add(sid, ssubtipodocumentoid, stipodocumentodescrip, ssubtipodocumentodescrip, saccion, sgrupoplazodescrip, stipoplazoid, stipoplazodescrip, stiempo_meses + "  Meses, " + stiempo_dias + " días");
                }
                respuestastringtoiposl.Close();
                con.Cerrarconexion();
            }catch(Exception Ex){
                MessageBox.Show("Ocurrió un error al intentar cargar la relacion de subtipo de documentos con los tipo plazo");
                new filelog("linea:211 Catalogoestatus", Ex.Message);
            }
        }

        private void cbtipodedocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbtipodedocumento.Text != "")
                {
                    object sIdtipodocumento = (cbtipodedocumento.SelectedItem as ComboboxItem).Value;
                    switch (sIdtipodocumento + "")
                    {
                        case "1":
                            {
                                tb_accion.Text = "Genera";
                            } break;
                        case "2":
                            {
                                tb_accion.Text = "Atiende";
                            } break;
                        case "8":
                            {
                                tb_accion.Text = "Atiende";
                            } break;
                        case "9":
                            {
                                tb_accion.Text = "Genera";
                            } break;
                        default:
                            {
                                tb_accion.Text = "";
                            } break;

                    }
                    actualizacomboboxdetipodocumento(sIdtipodocumento);
                }
                else
                {
                    tb_accion.Text = "";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Verifique la selección");
                new filelog("Revisar linea 271", Ex.Message);
            }
        }

        public void actualizacomboboxdetipodocumento(object sIdtipodocumento){
            try {
                conect con = new conect();
                String sQuerytipoSol = "select * from subtipodocumento where TipoDocumentoId = " + sIdtipodocumento + " ;";
                MySqlDataReader respuestastringtoiposl = con.getdatareader(sQuerytipoSol);
                cbDocumento.Items.Clear();
                cbDocumento.Text = "";
                while (respuestastringtoiposl.Read())
                {
                    cbDocumento.Items.Add(obj_dicss.validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", respuestastringtoiposl));
                }
                respuestastringtoiposl.Close();
                con.Cerrarconexion();
            }catch(Exception Ex){
                new filelog("verifique la linea 279", Ex.Message);
            }
        }

        private void cbtipodedocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (cbtipodedocumento.Text == "")
                {
                   tb_accion.Text = "";
                }
            }
            catch (Exception Ex)
            {

            }
        }

        private void dgTaablerelacionsubtipodocumnetoplazos_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0]!=null)
            {

                String ssubtipodocumentoid = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells[0].Value.ToString();
                String ssubtipodocumentoid_2 = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells["id_relacionsubtipodotipoplazo"].Value.ToString();
                String stipodocumentoid = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells["tipodocumentoid"].Value.ToString();
                String sTipodocumentodescrip = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells["subtipodocumentodescrip"].Value.ToString();
                String sdocdescrip = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells["docdescrip"].Value.ToString();
                String saccion = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells["accion"].Value.ToString();
                String sgrupoplazodescrip = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells["sgrupoplazodescrip"].Value.ToString();
                String stipoplazoid = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells["tipoplazoid"].Value.ToString();
                String stipoplazodescrip = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells["tipoplazodescrip"].Value.ToString();
                String sTiempoplazo = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells["Tiempoplazo"].Value.ToString();

                cbtipodedocumento.Text = sTipodocumentodescrip;
                cbtipoplazo.Text = sgrupoplazodescrip;
                actualizadocumento();
                actualizatipoplazo();
                //se seleccionaron los campos generales para después cargar un especifico


                if (sdocdescrip != "")
                {
                    cbDocumento.Text = sdocdescrip;
                }
                else {
                    cbDocumento.Items.Clear();
                }


                if (stipoplazodescrip != "")
                {
                    cbPlazo.Text = stipoplazodescrip;
                }
                else {
                    cbPlazo.Items.Clear();                
                }

                if(sTiempoplazo.Trim()!=""){
                    String []mesesdias =  sTiempoplazo.Split(',');
                    tb_tiempomes.Text = mesesdias[0].Trim().Replace("Meses", "");
                    tb_tiempodias.Text = mesesdias[1].Trim().Replace("días", "");
                }

                btn_modificar.Enabled = true;
                btn_agregar.Enabled = false;
                dgTaablerelacionsubtipodocumnetoplazos.Enabled = false;
            }else{
                MessageBox.Show("Debe seleccionar un registro.");
            }
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {//hacemos el update con los datos cargados
            try
            {
                if (cbDocumento.Text != "" && cbPlazo.Text != "" && tb_tiempomes.Text != "" && tb_tiempomes.Text != "")
                {
                    //tualizacomboboxdetipodocumento((cbDocumento.SelectedItem as ComboboxItem).Value);
                    String sDataid = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells["id_relacionsubtipodotipoplazo"].Value.ToString();
                    String sSubtipodocumentoid = (cbDocumento.SelectedItem as ComboboxItem).Value.ToString();
                    String sTipoplazoid = (cbPlazo.SelectedItem as ComboboxItem).Value.ToString();
                    String sTimpomes = tb_tiempomes.Text;
                    String stimpodias = tb_tiempomes.Text;
                    String sAccion = tb_accion.Text;

                    conect con_2 = new conect();//aquí hacemos el update
                    String sUpdate = " UPDATE `relacionplazosubtipodocumento`" +
                                        " SET" +
                                        " `subtipodocumentoid` = '" + sSubtipodocumentoid + "' ," +
                                        " `tipoplazoid` = '" + sTipoplazoid + "' ," +
                                        " `tiempo_meses` = '" + sTimpomes + "' ," +
                                        " `tiempo_dias` = '" + stimpodias + "' ," +
                                        " `accion` = '" + sAccion + "' " +
                                        " WHERE `id_grupoplazosubtipodocumento` = " + sDataid + ";";
                    //String sInserrelacionplazossubtipodocumento = " INSERT INTO `relacionplazosubtipodocumento`" +
                    //                                                " (`id_grupoplazosubtipodocumento`," +
                    //                                                " `subtipodocumentoid`," +
                    //                                                " `tipoplazoid`," +
                    //                                                " `tiempo_meses`," +
                    //                                                " `tiempo_dias`," +
                    //                                                " `accion`)" +
                    //                                                " VALUES" +
                    //                                                " (''," +
                    //                                                " '" + sSubtipodocumentoid + "'," +
                    //                                                " '" + sTipoplazoid + "'," +
                    //                                                " '" + sTimpomes + "'," +
                    //                                                " '" + stimpodias + "'," +
                    //                                                " '" + sAccion + "');";
                    MySqlDataReader respuesta_2 = con_2.getdatareader(sUpdate);
                    if (respuesta_2.RecordsAffected == 1)
                    {
                        MessageBox.Show("Se modificó correctamente.");
                        actualizarelacionsubtipodocumentoplazo();
                        cbtipodedocumento.Text = "";
                        cbDocumento.Items.Clear();
                        cbDocumento.Text = "";
                        tb_accion.Text = "";
                        cbtipoplazo.Text = "";
                        cbPlazo.Items.Clear();
                        cbPlazo.Text = "";
                        tb_tiempomes.Text = "";
                        tb_tiempodias.Text = "";

                        btn_modificar.Enabled = false;
                        btn_agregar.Enabled = true;
                        dgTaablerelacionsubtipodocumnetoplazos.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un Documento, plazo y tiempo del plazo.");
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Valores incorrectos, verifique el formulario.");
                new filelog("line 393, CatalogoEstatus", "MEssage:"+Ex.Message);
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            try {
                
                cbtipodedocumento.Text = "";
                cbDocumento.Items.Clear();
                cbDocumento.Text = "";
                tb_accion.Text = "";
                cbtipoplazo.Text = "";
                cbPlazo.Items.Clear();
                cbPlazo.Text = "";
                tb_tiempomes.Text = "";
                tb_tiempodias.Text = "";

                btn_modificar.Enabled = false;
                btn_agregar.Enabled = true;
                dgTaablerelacionsubtipodocumnetoplazos.Enabled = true;
            }catch(Exception Ex){
                new filelog("line: 411", "Message: "+Ex.Message);
            } 
            
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0]!=null)
                {
                    String sDataid = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells["id_relacionsubtipodotipoplazo"].Value.ToString();
                    
                    String stipodocumentoid = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells["tipodocumentoid"].Value.ToString();
                    String sTipodocumentodescrip = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells["subtipodocumentodescrip"].Value.ToString();
                    String sdocdescrip = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells["docdescrip"].Value.ToString();
                    String saccion = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells["accion"].Value.ToString();
                    String sgrupoplazodescrip = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells["sgrupoplazodescrip"].Value.ToString();
                    String stipoplazoid = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells["tipoplazoid"].Value.ToString();
                    String stipoplazodescrip = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells["tipoplazodescrip"].Value.ToString();
                    String sTiempoplazo = dgTaablerelacionsubtipodocumnetoplazos.SelectedRows[0].Cells["Tiempoplazo"].Value.ToString();

                    var confirmResult = MessageBox.Show("¿Seguro que desea eliminar el registro seleccionado?",
                                     "Confirmación Eliminar!!",
                                     MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        conect con_2 = new conect();//aquí hacemos el update
                        String sUpdate = " Delete from  `relacionplazosubtipodocumento`" +
                                         " WHERE `id_grupoplazosubtipodocumento` = " + sDataid + ";";
                        MySqlDataReader respuesta_2 = con_2.getdatareader(sUpdate);
                        if (respuesta_2.RecordsAffected == 1)
                        {
                            MessageBox.Show("Se Eliminó correctamente.");
                            actualizarelacionsubtipodocumentoplazo();
                        
                        }
                    }
                    
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un Registro.");
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error al intentar eliminar.");
                new filelog("line 393, CatalogoEstatus", "MEssage:" + Ex.Message);
            }
        }
    }
}
